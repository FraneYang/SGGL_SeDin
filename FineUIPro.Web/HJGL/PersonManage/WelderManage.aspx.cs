using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using AspNet = System.Web.UI.WebControls;
using System.Text;
using BLL;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class WelderManage : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.InitTreeMenu();
            }
        }

        #region 绑定数据-焊工信息
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
            {
                Model.SitePerson_Person welder = BLL.WelderService.GetWelderById(this.tvControlItem.SelectedNodeID);
                if (welder != null)
                {
                    this.btnEdit.Hidden = false;
                    this.btnNew.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.txtWelderCode.Text = welder.WelderCode;
                    this.txtWelderName.Text = welder.PersonName;

                    if (!string.IsNullOrEmpty(welder.UnitId))
                    {
                        this.drpUnitId.Text = UnitService.GetUnitNameByUnitId(welder.UnitId);
                    }
                    this.rblSex.Text = welder.Sex == "1" ? "男" : "女";
                    if (welder.Birthday.HasValue)
                    {
                        this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", welder.Birthday);
                    }
                    this.txtCertificateCode.Text = welder.CertificateCode;
                    //if (string.IsNullOrEmpty(welder.CertificateCode))
                    //{
                    //    this.txtCertificateCode.Text = welder.IdentityCard;
                    //}
                    if (welder.CertificateLimitTime.HasValue)
                    {
                        this.txtCertificateLimitTime.Text = string.Format("{0:yyyy-MM-dd}", welder.CertificateLimitTime);
                    }
                    this.txtWelderLevel.Text = welder.WelderLevel;
                    if (welder.IsUsed == true)
                    {
                        cbIsOnDuty.Checked = true;
                    }
                    else
                    {
                        cbIsOnDuty.Checked = false;
                    }
                }
            }
        }

        #endregion
        #region 绑定数据-资质信息
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGvItem()
        {
            if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
            {
                string strSql = @"SELECT WelderQualifyId, WelderId, QualificationItem, LimitDate, CheckDate,
									 Thickness,Sizes,Thickness2,Sizes2,IsAudit,
									 Remark,WelderCode,PersonName,WeldingMethod,
                                     WeldingLocation,MaterialType,IsPrintShow,WeldType,IsCanWeldG
                              FROM View_Welder_WelderQualify
                              WHERE WelderId=@WelderId";

                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@WelderId", this.tvControlItem.SelectedNodeID));
                if (!string.IsNullOrEmpty(this.txtQualificationItem.Text))
                {
                    strSql += " and QualificationItem LIKE  @QualificationItem";
                    parms.Add(new SqlParameter("@QualificationItem", "%" + this.txtQualificationItem.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = parms.ToArray();
                DataTable dt = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = dt.Rows.Count;
                var table = this.GetPagedDataTable(Grid1, dt);

                Grid1.DataSource = table;
                Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.Grid1.Rows[i].Values[3].ToString()))
                    {
                        DateTime limitDate = Convert.ToDateTime(this.Grid1.Rows[i].Values[3].ToString());
                        if (DateTime.Now > limitDate)
                        {
                            this.Grid1.Rows[i].RowCssClass = "color3";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGvItem();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGvItem();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGvItem();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGvItem();
        }
        #endregion
        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();
            var getUnits = UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId, Const.ProjectUnitType_2);
            foreach (var item in getUnits)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.NodeID = item.UnitId;
                rootNode.Text = item.UnitName;
                this.tvControlItem.Nodes.Add(rootNode);
                var getWelders = (from x in Funs.DB.SitePerson_Person where x.ProjectId == this.CurrUser.LoginProjectId && x.WorkPostId == Const.WorkPost_Welder && x.UnitId == item.UnitId select x).ToList();
                foreach (var sitem in getWelders)
                {
                    TreeNode tn = new TreeNode();
                    tn.NodeID = sitem.PersonId;
                    if (sitem.IsUsed != true)
                    {
                        tn.Text = "<font color='#EE0000'>" + sitem.PersonName + "</font>";
                    }
                    else
                    {
                        tn.Text = sitem.PersonName;
                    }
                    tn.EnableClickEvent = true;
                    rootNode.Nodes.Add(tn);
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
        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
            this.BindGvItem();
        }
        #endregion
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("焊工信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            grid.PageSize = 10000;
            BindGrid();
            this.Grid1.Columns[10].Hidden = true;
            this.Grid1.Columns[11].Hidden = true;
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

                    sb.AppendFormat("<td>{0}</td>", html);

                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.WelderManageMenuId, button);
        }
        #endregion

        #region 焊工信息维护事件
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
            {
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }
            if (GetButtonPower(Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderManageEdit.aspx?PersonId={0}", this.tvControlItem.SelectedNodeID, "编辑 - ")));
            }
            else if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderManageView.aspx?PersonId={0}", this.tvControlItem.SelectedNodeID, "查看 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnDelete))
            {
                if (string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                string strShowNotify = string.Empty;
                var welder = BLL.WelderService.GetWelderById(this.tvControlItem.SelectedNodeID);
                if (welder != null)
                {
                    string cont = judgementDelete(this.tvControlItem.SelectedNodeID);
                    if (string.IsNullOrEmpty(cont))
                    {
                        var ItemCheck = from x in Funs.DB.Welder_WelderQualify where x.WelderId == this.tvControlItem.SelectedNodeID select x;
                        if (ItemCheck != null)
                        {
                            Funs.DB.Welder_WelderQualify.DeleteAllOnSubmit(ItemCheck);
                            Funs.DB.SubmitChanges();
                        }
                        BLL.WelderService.DeleteWelderById(this.tvControlItem.SelectedNodeID);
                    }
                    else
                    {
                        strShowNotify += "焊工管理" + "：" + welder.WelderCode + cont;
                    }
                }
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    BindGrid();
                    ShowNotify("删除成功！", MessageBoxIcon.Success);
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectUserId == id) != null)
            {
                content += "已在【项目焊工】中使用，不能删除！";
            }

            //if (Funs.DB.Pipeline_WeldJoint.FirstOrDefault(x => x.BackingWelderId == id) != null)
            //{
            //    content += "已在【焊接信息】中使用，不能删除！";
            //}

            return content;
        }
        #endregion
        #endregion

        #region 焊工资质信息维护事件
        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnModify))
            {
                if (!string.IsNullOrEmpty(this.tvControlItem.SelectedNodeID))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemEdit.aspx?PersonId={0}", this.tvControlItem.SelectedNodeID, "新增 - ")));
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 编辑
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
        /// 审核事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAudit_Click(object sender, EventArgs e)
        {
            Model.Project_ProjectUnit projectUnit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
            string unitType = string.Empty;
            if (projectUnit != null)
            {
                unitType = projectUnit.UnitType;
            }
            if (this.CurrUser.UserId == BLL.Const.sysglyId || this.CurrUser.UserId == BLL.Const.hfnbdId || unitType == BLL.Const.ProjectUnitType_1)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemEdit.aspx?WelderQualifyId={0}&IsAudit=1", Grid1.SelectedRowID, "审核 - ")));
            }
            else
            {
                Alert.ShowInTop("只有总包用户可以进行审核操作！", MessageBoxIcon.Warning);
            }
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
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }

            ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemEdit.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemView.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var welder = BLL.WelderQualifyService.GetWelderQualifyById(rowID);
                        if (welder != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.WelderQualifyService.DeleteWelderQualifyById(rowID);
                                //BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除安装组件信息");
                            }
                            //else
                            //{
                            //    strShowNotify += Resources.Lan.WelderQualification + "：" + welder.QualificationItem + cont;
                            //}
                        }
                    }
                    if (!string.IsNullOrEmpty(strShowNotify))
                    {
                        Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        BindGvItem();
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGvItem();
        }
        #endregion

        #region 查看按钮
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemView.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        #endregion      
        #endregion
    }
}