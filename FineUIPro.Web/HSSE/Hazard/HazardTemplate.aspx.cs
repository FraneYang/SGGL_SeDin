using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class HazardTemplate : PageBase
    {
        #region 定义集合
        public string HazardListId
        {
            get
            {
                return (string)ViewState["HazardListId"];
            }
            set
            {
                ViewState["HazardListId"] = value;
            }
        }


        /// <summary>
        /// 工作阶段值
        /// </summary>
        public string WorkStageIds
        {
            get
            {
                return (string)ViewState["WorkStageIds"];
            }
            set
            {
                ViewState["WorkStageIds"] = value;
            }
        }

        private static List<Model.Hazard_HazardSelectedItem> hazardSelectedItems = new List<Model.Hazard_HazardSelectedItem>();
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<string> list = new List<string>();

        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                list = new List<string>();                
                WorkStageService.InitWorkPostDropDownList(this.drpWorkStages, true);
                this.HazardListId = Request.Params["HazardListId"];
                this.WorkStageIds = Request.Params["WorkStageIds"];
                if (!string.IsNullOrEmpty(this.WorkStageIds))
                {
                    this.drpWorkStages.SelectedValueArray = this.WorkStageIds.Split(',');                    
                }
                hazardSelectedItems = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(this.HazardListId);
                foreach (var item in hazardSelectedItems)
                {
                    if (!string.IsNullOrEmpty(item.HazardListTypeId))
                    {
                        Model.Technique_HazardListType hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(item.HazardListTypeId);
                        if (hazardListType != null)
                        {
                            if (hazardListType.IsCompany == true)
                            {
                                rblIsCompany.SelectedValue = "1";
                            }
                            else
                            {
                                rblIsCompany.SelectedValue = "0";
                            }
                        }
                    }
                }
                InitTreeMenu();
            }
        }
        #endregion

        #region 获取编辑页面上的工作阶段保存到list集合里
        /// <summary>
        /// 获取编辑页面上的工作阶段保存到list集合里
        /// </summary>
        private void GetHazardSelectedItem()
        {
            hazardSelectedItems = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(this.HazardListId);
            if (hazardSelectedItems.Count() > 0)
            {
                string i = string.Empty;
                string j = string.Empty;
                foreach (var item in hazardSelectedItems)
                {
                    foreach (var workStagesId in this.drpWorkStages.SelectedValueArray)
                    {
                        if (item.WorkStage == workStagesId)
                        {
                            i += item.HazardId + "," + workStagesId + "|";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(i))
                {
                    j = i.Substring(0, i.LastIndexOf("|"));
                    list.Add(j);
                }
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trHazardListType.Nodes.Clear();
            trHazardListType.ShowBorder = false;
            trHazardListType.ShowHeader = false;
            trHazardListType.EnableIcons = true;
            trHazardListType.AutoScroll = true;
            trHazardListType.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "危险源清单",
                NodeID = "0",
                Expanded = true
            };
            this.trHazardListType.Nodes.Add(rootNode);
            string[] workStages = this.drpWorkStages.SelectedValueArray;
            var works = BLL.WorkStageService.GetWorkStageList();
            foreach (var item in workStages)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = works.FirstOrDefault(x => x.WorkStageId == item).WorkStageName,
                    NodeID = item,
                    CommandArgument = item
                };
                rootNode.Nodes.Add(newNode);
                BoundTree(newNode, "0");           //newNode.Nodes,
            }
        }

        private void BoundTree(TreeNode node, string supHazardListTypeId)
        {            
            var dt = GetNewHazardListType(node, supHazardListTypeId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.HazardListTypeName,
                        ToolTip = dr.HazardListTypeName,
                        NodeID = node.NodeID + "#" + dr.HazardListTypeId,
                        CommandArgument = node.CommandArgument,
                        EnableClickEvent = true
                    };
                    node.Nodes.Add(tn);
                    BoundTree(tn, dr.HazardListTypeId);
                }
            }
        }
        #endregion

        #region 得到树方法
        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Technique_HazardListType> GetNewHazardListType(TreeNode tn, string parentId)
        {
            string nodeId = tn.NodeID;
            if (nodeId.Contains("#"))
            {
                nodeId = nodeId.Substring(nodeId.LastIndexOf('#') + 1);
            }

            if (parentId.Contains("#"))
            {
                parentId = parentId.Substring(parentId.LastIndexOf('#') + 1);
            }
            List<Model.Technique_HazardListType> hazardListType = new List<Model.Technique_HazardListType>();
            if (this.rblIsCompany.SelectedValue == "1")//本公司危险源
            {
                if (parentId == "0")
                {
                    hazardListType = (from x in Funs.DB.Technique_HazardListType where x.IsCompany == true && x.SupHazardListTypeId == parentId && x.WorkStage.Contains(nodeId) orderby x.HazardListTypeCode select x).ToList();
                    if (hazardListType.Count() == 0)
                    {
                        hazardListType = (from x in Funs.DB.Technique_HazardListType where x.IsCompany == true && x.SupHazardListTypeId == parentId && x.WorkStage == null orderby x.HazardListTypeCode select x).ToList();
                    }
                }
                else
                {
                    hazardListType = (from x in Funs.DB.Technique_HazardListType where x.IsCompany == true && x.SupHazardListTypeId == parentId orderby x.HazardListTypeCode select x).ToList();
                }
            }
            else
            {
                if (parentId == "0")
                {
                    hazardListType = (from x in Funs.DB.Technique_HazardListType where (x.IsCompany == false || x.IsCompany == null) && x.SupHazardListTypeId == parentId && x.WorkStage.Contains(nodeId) orderby x.HazardListTypeCode select x).ToList();
                    if (hazardListType == null)
                    {
                        hazardListType = (from x in Funs.DB.Technique_HazardListType where (x.IsCompany == false || x.IsCompany == null) && x.SupHazardListTypeId == parentId && x.WorkStage == null orderby x.HazardListTypeCode select x).ToList();
                    }

                }
                else
                {
                    hazardListType = (from x in Funs.DB.Technique_HazardListType where (x.IsCompany == false || x.IsCompany == null) && x.SupHazardListTypeId == parentId orderby x.HazardListTypeCode select x).ToList();
                }
            }
            return hazardListType;
        }
        #endregion

        #region 点击树节点
        /// <summary>
        /// 点击树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trHazardListType_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 绑定Grid
        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            string nodeId = this.trHazardListType.SelectedNode.NodeID;
            if (!string.IsNullOrEmpty(nodeId) && nodeId.Contains("#"))
            {
                nodeId = nodeId.Substring(nodeId.LastIndexOf('#') + 1);
            }
            string strSql = @"SELECT H.HazardId,T.HazardListTypeId,T.HazardListTypeCode,T.HazardListTypeName,H.HazardCode,H.HazardItems,H.DefectsType,"
                                    + @" H.MayLeadAccidents,H.HelperMethod,H.HazardJudge_L,H.HazardJudge_E,H.HazardJudge_C,H.HazardJudge_D,Const0007.ConstText AS HazardLevel,"
                                    + @"  H.ControlMeasures,H.CompileMan,H.CompileDate,H.AuditMan,H.AuditDate,H.IsPass,U.UserName AS CompileManName,UR.UserName AS AuditManName"
                                    + @" FROM Technique_HazardList AS H"
                                    + @" LEFT JOIN Technique_HazardListType AS T ON T.HazardListTypeId = H.HazardListTypeId"
                                    + @" LEFT JOIN Sys_User AS U ON U.UserId = H.CompileMan"
                                    + @" LEFT JOIN Sys_User AS UR ON UR.UserId = H.AuditMan"
                                    + @" LEFT JOIN Sys_Const AS Const0007 ON Const0007.ConstValue = H.HazardLevel and Const0007.GroupId = '" + ConstValue.Group_0007 + "'"
                                    + @"  WHERE T.HazardListTypeId = @HazardListTypeId ";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@HazardListTypeId", nodeId),
            };

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            if (list.Count() > 0)
            {
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string id = Grid1.DataKeys[i][0].ToString();
                    if (list.Contains(id))
                    {
                        //CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                        //checkField.SetCheckedState(i, true);
                        Grid1.Rows[i].Values[0] = true;
                    }
                }
            }
        }
        #endregion

        #region 表分页、排序
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// Grid排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;

            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region Grid行双击事件
        /// <summary>
        /// 双击行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }
        #endregion

        #region 增加明细
        /// <summary>
        /// 增加危险源清单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trHazardListType.SelectedNode != null)
            {
                if (this.trHazardListType.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../Technique/HazardListEdit.aspx?HazardListTypeId={0}", this.trHazardListType.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            GetHazardSelectedItem();
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString() + "," + this.trHazardListType.SelectedNode.CommandArgument;
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    if (!list.Contains(rowID))
                    {
                        list.Add(rowID);
                    }
                }
                else
                {
                    if (list.Contains(rowID))
                    {
                        list.Remove(rowID);
                    }
                }
            }
        }
        #endregion

        #region 编辑明细
        /// <summary>
        /// 编辑危险源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditDetail_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string hazardId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../Technique/HazardListEdit.aspx?HazardId={0}", hazardId, "编辑 - ")));
        }
        #endregion

        #region 删除明细
        /// <summary>
        /// 删除危险源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getD= BLL.HazardListService.GetHazardListById(rowID);
                    if (getD != null)
                    {
                        if (BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemByHazardId(rowID) != null)
                        {
                            Alert.ShowInTop("在项目级危险源评价清单中已使用该资源，无法删除！", MessageBoxIcon.Warning);
                            return;
                        }
                        BLL.LogService.AddSys_Log(this.CurrUser, getD.HazardCode, getD.HazardId, BLL.Const.HazardListMenuId, BLL.Const.BtnDelete);
                        BLL.HazardListService.DeleteHazardListById(rowID);
                       
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 关闭危险源清单弹出窗口
        /// <summary>
        /// 关闭危险源清单弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 确认按钮
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (list.Count == 0)
            {
                ShowNotify("请至少选择一项！", MessageBoxIcon.Warning);
                return;
            }
            string ids = string.Empty;
           
            foreach (var item in list)
            {
                ids += item + "|";
            }
            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0, ids.LastIndexOf("|"));
                Session["workStages"] = ids;
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            InitTreeMenu();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HazardListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region 选择是否本公司危险源
        /// <summary>
        /// 选择是否本公司工作阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblIsCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitTreeMenu();
        }
        #endregion
    }
}