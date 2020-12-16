using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class PersonList : PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员主键
        /// </summary>
        public string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }

        /// <summary>
        /// 项目id
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                this.btnMenuDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                this.btnMenuDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                BLL.WorkPostService.InitWorkPostDropDownList(this.drpPost, true);
                this.InitTreeMenu();//加载树
            }
        }

        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvProjectAndUnit.Nodes.Clear();
            var project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
            if (project != null)
            {
                var personLists = BLL.PersonService.GetPersonList(project.ProjectId);
                TreeNode rootNode = new TreeNode();
                rootNode = new TreeNode
                {
                    Text = project.ProjectName,
                    NodeID = project.ProjectId
                };
                if (personLists.Count() > 0)
                {
                    var personIn =personLists.Where(x=>x.InTime<= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now));
                    rootNode.ToolTip = "当前项目人员总数：" + personLists.Count() + "；在场人员数：" + personIn.Count() + "；离场人员数：" + (personLists.Count() - personIn.Count());
                }
                else
                {
                    rootNode.ToolTip = "当前项目人员总数：0";
                }
                rootNode.Expanded = true;
                this.tvProjectAndUnit.Nodes.Add(rootNode);
                GetUnitLists(rootNode.Nodes, this.ProjectId, personLists);
            }
        }

        /// <summary>
        /// 加载单位
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        private void GetUnitLists(TreeNodeCollection nodes, string parentId, List<Model.SitePerson_Person> personLists)
        {
            List<Model.Base_Unit> unitLists = BLL.UnitService.GetUnitByProjectIdList(parentId);
            if (unitLists.Count() > 0)
            {
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(parentId, this.CurrUser.UnitId))
                {
                    unitLists = unitLists.Where(x => x.UnitId == this.CurrUser.UnitId).ToList();
                }

                //添加其他单位/无单位人员
                Model.Base_Unit otherUnit = new Model.Base_Unit
                {
                    UnitId = "0",
                    UnitName = "其他"
                };
                unitLists.Add(otherUnit);

                TreeNode newNode = null;
                foreach (var q in unitLists)
                {
                    newNode = new TreeNode
                    {
                        Text = q.UnitName,
                        NodeID = q.UnitId + "|" + parentId,
                        ToolTip = q.UnitName
                    };
                    if (personLists.Count() > 0)
                    {
                        var personUnitLists = personLists.Where(x => x.UnitId == q.UnitId);
                        if (q.UnitId == "0")
                        {
                            personUnitLists = personLists.Where(x => x.UnitId == null);
                        }

                        if (personUnitLists.Count() > 0)
                        {
                            var personIn = personUnitLists.Where(x => x.InTime <= System.DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= System.DateTime.Now));
                            newNode.ToolTip = q.UnitName + "人员总数：" + personUnitLists.Count() + "；在场人员数：" + personIn.Count() + "；离场人员数：" + (personUnitLists.Count() - personIn.Count());
                        }
                        else
                        {
                            newNode.ToolTip = q.UnitName + "人员总数：0";
                        }
                    }
                    else
                    {
                        newNode.ToolTip = q.UnitName + "人员总数：0";
                    }
                    newNode.EnableClickEvent = true;
                    nodes.Add(newNode);
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                if (this.tvProjectAndUnit != null && !string.IsNullOrEmpty(this.tvProjectAndUnit.SelectedNodeID))
                {
                    string id = this.tvProjectAndUnit.SelectedNodeID;
                    string unitId = string.Empty;
                    string projectId = string.Empty;
                    var str = id.Split('|');
                    if (str.Count() > 1)
                    {
                        unitId = str[0];
                        projectId = str[1];
                    }

                    string strSql = "select * from View_SitePerson_Person Where ProjectId=@ProjectId ";
                    List<SqlParameter> listStr = new List<SqlParameter>
                {
                    new SqlParameter("@ProjectId", this.ProjectId)
                };
                    if (!string.IsNullOrEmpty(unitId) && unitId != "0")
                    {
                        strSql += " AND UnitId =@UnitId ";
                        listStr.Add(new SqlParameter("@UnitId", unitId));
                    }
                    else
                    {
                        strSql += " AND UnitId IS NULL";
                    }

                    if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
                    {
                        strSql += " AND PersonName LIKE @PersonName";
                        listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
                    }

                    if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
                    {
                        strSql += " AND CardNo LIKE @CardNo";
                        listStr.Add(new SqlParameter("@CardNo", "%" + this.txtCardNo.Text.Trim() + "%"));
                    }
                    if (!string.IsNullOrEmpty(this.txtIdentityCard.Text.Trim()))
                    {
                        strSql += " AND IdentityCard LIKE @IdentityCard";
                        listStr.Add(new SqlParameter("@IdentityCard", "%" + this.txtIdentityCard.Text.Trim() + "%"));
                    }
                    if (!string.IsNullOrEmpty(this.drpTreamGroup.SelectedValue) && this.drpTreamGroup.SelectedValue != BLL.Const._Null)
                    {
                        strSql += " AND TeamGroupId = @TeamGroupId";
                        listStr.Add(new SqlParameter("@TeamGroupId", this.drpTreamGroup.SelectedValue));
                    }

                    if (this.drpPost.SelectedItemArray.Count() > 1 || (this.drpPost.SelectedValue != BLL.Const._Null && this.drpPost.SelectedItemArray.Count() == 1))
                    {
                        strSql += " AND (1=2 ";
                        int i = 0;
                        foreach (var item in this.drpPost.SelectedValueArray)
                        {
                            if (!string.IsNullOrEmpty(item) && item != BLL.Const._Null)
                            {
                                strSql += " OR WorkPostId = @WorkPostId" + i.ToString();
                                listStr.Add(new SqlParameter("@WorkPostId" + i.ToString(), item));
                            }

                            i++;
                        }

                        strSql += ")";
                    }
                    if (this.ckTrain.Checked)
                    {
                        strSql += " AND TrainCount =0";
                    }
                    if (this.ckIdCardInfoNotOK.Checked)
                    {
                        strSql += " AND (IdcardType is null or IdentityCard is null or PhotoUrl is null or (select count(*) from AttachFile where ToKeyId=PersonId+'#1')=0 or (select count(*) from AttachFile where ToKeyId=PersonId+'#5')=0)";
                    }

                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                    Grid1.RecordCount = tb.Rows.Count;
                    var table = this.GetPagedDataTable(Grid1, tb);
                    Grid1.DataSource = table;
                    Grid1.DataBind();

                    for (int i = 0; i < Grid1.Rows.Count; i++)
                    {
                        string personId = Grid1.Rows[i].DataKeys[0].ToString();

                        var isNull = from x in db.EduTrain_TrainRecordDetail
                                     join y in db.EduTrain_TrainRecord on x.TrainingId equals y.TrainingId
                                     where y.ProjectId == this.ProjectId && x.PersonId == personId
                                     select x;
                        if (isNull.Count() == 0) ////未参加过培训的人员
                        {
                            Grid1.Rows[i].RowCssClass = "Red";
                        }
                    }
                }
            }
        }
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvProjectAndUnit_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.drpTreamGroup.Items.Clear();
            if (this.tvProjectAndUnit.SelectedNodeID.Contains("|"))
            {
                string id = this.tvProjectAndUnit.SelectedNodeID;
                string unitId = string.Empty;
                string projectId = string.Empty;
                var str = id.Split('|');
                if (str.Count() > 1)
                {
                    unitId = str[0];
                    projectId = str[1];
                }
                BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTreamGroup, projectId, unitId, true);

                BindGrid();
            }
        }
        #endregion

        #region 页索引改变事件
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
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 增加按钮
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.tvProjectAndUnit.SelectedNodeID.Contains("|"))
            {
                string id = this.tvProjectAndUnit.SelectedNodeID;
                string[] str = id.Split('|');
                if (str.Count() > 1)
                {
                    string unitId = id.Split('|')[0];
                    string projectId = id.Split('|')[1];
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonListEdit.aspx?ProjectId={0}&&UnitId={1}", projectId, unitId, "编辑 - ")));
                }
            }
            else
            {
                Alert.ShowInTop("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 编辑
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
        /// 编辑
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            this.PersonId = Grid1.SelectedRowID;
            string id = this.tvProjectAndUnit.SelectedNodeID;
            string[] str = id.Split('|');
            if (str.Count() > 1)
            {
                string unitId = id.Split('|')[0];
                string projectId = id.Split('|')[1];
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonListEdit.aspx?PersonId={0}&&ProjectId={1}&&UnitId={2}", this.PersonId, projectId, unitId, "编辑 - ")));
            }
        }

        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }
        #endregion

        #region 删除
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
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                int i = 0;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        i++;
                        var getV = BLL.PersonService.GetPersonById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.PersonName, getV.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnDelete);
                            BLL.PersonService.DeletePerson(rowID);
                        }
                    }
                }
                BindGrid();
                if (i > 0)
                {
                    ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
                }
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            var q = from x in Funs.DB.QualityAudit_PersonQuality where x.PersonId == rowID select x;
            if (q.Count() > 0)
            {
                content += "人员资质中已存在该人员，无法删除！";
            }
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 判断按钮权限
        /// <summary>
        /// 判断按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnImport.Hidden = false;
                    this.btnPersonOut.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                    this.btnPersonUnit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                    this.btnPersonUnit.Hidden = false;
                }
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            //if (this.tvProjectAndUnit.SelectedNodeID.Contains("|"))
            //{
            //    string id = this.tvProjectAndUnit.SelectedNodeID;
            //    string unitId = string.Empty;
            //    string projectId = string.Empty;
            //    unitId = id.Split('|')[0];
            //    projectId = id.Split('|')[1];

            //}
            //else
            //{
            //    Alert.ShowInTop("请选择单位！", MessageBoxIcon.Warning);
            //    return;
            //}
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("PersonIn.aspx?ProjectId={0}", this.CurrUser.LoginProjectId, "导入 - ")));
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("人员信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //this.Grid1.PageSize = this.;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “PersonList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “PersonList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfI")
                    {
                        html = (row.FindControl("lbI") as AspNet.Label).Text;                        
                    }
                    //sb.AppendFormat("<td>{0}</td>", html);
                    sb.AppendFormat("<td style='vnd.ms-excel.numberformat:@;width:140px;'>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 批量出场
        /// <summary>
        /// 批量出场按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPersonOut_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("PersonOut.aspx?ProjectId={0}", this.CurrUser.LoginProjectId, "批量出场 - ")));
        }    
        
        /// <summary>
        /// 批量单位转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPersonUnit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId))
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("PersonUnitRefresh.aspx", "批量单位转换 - ")));
            }
            else
            {
                ShowNotify("非软件管理单位用户，不能调整人员单位!", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
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
            this.BindGrid();
        }
        #endregion     
            
        /// <summary>
        /// 批量生成二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQR_Click(object sender, EventArgs e)
        {
            var getPersons = from x in Funs.DB.SitePerson_Person
                             where x.ProjectId == this.CurrUser.LoginProjectId && x.IdentityCard != null && x.QRCodeAttachUrl == null
                             select x;
            int num = 0;
            if (getPersons.Count() > 0)
            {
                foreach (var item in getPersons)
                {
                    string url =  CreateQRCodeService.CreateCode_Simple("person$" + item.IdentityCard);
                    if (!string.IsNullOrEmpty(url))
                    {
                        item.QRCodeAttachUrl = url;
                        Funs.DB.SubmitChanges();
                        num++;
                    }
                }
            }
            ShowNotify("操作完成，新生成二维码"+ num.ToString()+"条", MessageBoxIcon.Success);
        }
    }
}