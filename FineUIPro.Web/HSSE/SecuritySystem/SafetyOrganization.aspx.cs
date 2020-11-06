using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.SecuritySystem
{
    public partial class SafetyOrganization : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                ////权限按钮方法
                this.GetButtonPower();
                this.InitTreeMenu();
            }
        }

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trSafetyOrganization.Nodes.Clear();
            TreeNode rootNode = new TreeNode
            {
                Text = "安全组织机构",
                NodeID = "0",
                Expanded = true
            };
            this.trSafetyOrganization.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes);
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes)
        {
            var unitLists = BLL.ProjectUnitService.GetProjectUnitListByProjectId(this.ProjectId);
            if (unitLists.Count() > 0)
            {
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    unitLists = unitLists.Where(x => x.UnitId == this.CurrUser.UnitId).ToList();
                }

                TreeNode tn = null;
                foreach (var dr in unitLists)
                {
                    tn = new TreeNode();
                    var unitName = BLL.UnitService.GetUnitNameByUnitId(dr.UnitId);
                    if (unitName != null)
                    {
                        tn.Text = unitName;
                    }
                    tn.NodeID = dr.UnitId;
                    tn.EnableClickEvent = true;

                    var gunitType = BLL.ConstValue.GetConstByConstValueAndGroupId(dr.UnitType, BLL.ConstValue.Group_ProjectUnitType);
                    if (gunitType != null)
                    {
                        tn.ToolTip = gunitType.ConstText + "：" + unitName;
                    }
                    //tn.ToolTip = "编号：" + dr.SafetyOrganizationCode + "；<br/>机构名称：" + dr.SafetyOrganizationName + "；<br/>职责：" + dr.Duties + "；<br/>组成文件：" + dr.BundleFile + "；<br/>机构人员：" + dr.AgencyPersonnel;
                    nodes.Add(tn);
                }
            }
        }
        #endregion

        #region 绑定Grid
        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            string unitIdSelect = string.Empty;
            if (this.trSafetyOrganization.SelectedNode != null)
            {
                unitIdSelect = this.trSafetyOrganization.SelectedNode.NodeID;
            }
            string strSql = @"SELECT p.PersonId,p.CardNo,p.PersonName,p.ProjectId,p.UnitId,w.WorkPostName,p.Telephone,w.Remark"
                          + @" FROM SitePerson_Person AS P"
                          + @" LEFT JOIN  Base_WorkPost AS W ON P.WorkPostId =W.WorkPostId"
                          + @" WHERE w.PostType='1' AND p.ProjectId= '" + this.ProjectId + "' AND p.UnitId='" + unitIdSelect + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            //var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectSafetyOrganizationMenuId);
            //if (buttonList.Count() > 0)
            //{
            //    if (buttonList.Contains(BLL.Const.BtnAdd))
            //    {
            //        this.btnNewItem.Hidden = false;
            //    }
            //    if (buttonList.Contains(BLL.Const.BtnModify))
            //    {
            //        this.btnEditItem.Hidden = false;
            //        this.btnMenuEdit.Hidden = false;
            //    }
            //    if (buttonList.Contains(BLL.Const.BtnDelete))
            //    {
            //        this.btnDeleteItem.Hidden = false;
            //        this.btnMenuDelete.Hidden = false;
            //    }
            //}
        }
        #endregion

        #region GV 排序 翻页事件
        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
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

        /// <summary>
        /// 页码变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region GV 编辑 事件
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑明细按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditItem_Click(object sender, EventArgs e)
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

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SafetyOrganizationEdit.aspx?SafetyOrganizationId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }

        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewItem_Click(object sender, EventArgs e)
        {
            if (this.trSafetyOrganization.SelectedNode != null)
            {
                if (this.trSafetyOrganization.SelectedNode.Nodes.Count == 0 && !string.IsNullOrEmpty(this.trSafetyOrganization.SelectedNode.NodeID) && this.trSafetyOrganization.SelectedNode.NodeID != "0")
                {                    
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SafetyOrganizationEdit.aspx?UnitId={0}", this.trSafetyOrganization.SelectedNode.NodeID, "编辑 - ")));
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

        /// <summary>
        /// 删除明细按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteItem_Click(object sender, EventArgs e)
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
                    //var getV = BLL.SafetyOrganizationService.GetSafetyOrganizationById(rowID);
                    //if (getV != null)
                    //{
                    //    BLL.LogService.AddSys_Log(this.CurrUser, getV.Names, getV.SafetyOrganizationId, BLL.Const.ProjectSafetyOrganizationMenuId, BLL.Const.BtnDelete);
                    //    BLL.SafetyOrganizationService.DeleteSafetyOrganization(rowID);                       
                    //}                    
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        } 
        #endregion

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        
        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trSafetyOrganization_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全组织机构" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 5000;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}