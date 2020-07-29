using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.HiddenInspection
{
    public partial class HiddenRectificationList : PageBase
    {
        #region  定义项
        /// <summary>
        /// GV被选择项列表
        /// </summary>
        public List<string> ItemSelectedList
        {
            get
            {
                return (List<string>)ViewState["ItemSelectedList"];
            }
            set
            {
                ViewState["ItemSelectedList"] = value;
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
                GetButtonPower();
                this.ItemSelectedList = new List<string>();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.drpStates.DataValueField = "Id";
                this.drpStates.DataTextField = "Name";
                this.drpStates.DataSource = BLL.HSSE_Hazard_HazardRegisterService.GetStatesList(); ;
                this.drpStates.DataBind();
                Funs.FineUIPleaseSelect(this.drpStates);
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT * FROM View_Hazard_HazardRegister WHERE ProblemTypes='1' ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtCheckMan.Text.Trim()))
            {
                strSql += " AND CheckManName LIKE @CheckMan";
                listStr.Add(new SqlParameter("@CheckMan", "%" + this.txtCheckMan.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtType.Text.Trim()))
            {
                strSql += " AND RegisterTypesName LIKE @Type";
                listStr.Add(new SqlParameter("@Type", "%" + this.txtType.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWorkAreaName.Text.Trim()))
            {
                strSql += " AND WorkAreaName LIKE @WorkAreaName";
                listStr.Add(new SqlParameter("@WorkAreaName", "%" + this.txtWorkAreaName.Text.Trim() + "%"));
            }
            //if (this.ckType.SelectedValue != "0")
            //{
            //    strSql += " AND CheckCycle=@CheckCycle";
            //    listStr.Add(new SqlParameter("@CheckCycle", this.ckType.SelectedValue));
            //}
            if (!string.IsNullOrEmpty(this.txtResponsibilityUnitName.Text.Trim()))
            {
                strSql += " AND ResponsibilityUnitName LIKE @ResponsibilityUnitName";
                listStr.Add(new SqlParameter("@ResponsibilityUnitName", "%" + this.txtResponsibilityUnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                strSql += " AND CheckTime >= @StartTime";
                listStr.Add(new SqlParameter("@StartTime", this.txtStartTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                strSql += " AND CheckTime <= @EndTime";
                listStr.Add(new SqlParameter("@EndTime", this.txtEndTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtStartRectificationTime.Text.Trim()))
            {
                strSql += " AND RectificationTime >= @StartRectificationTime";
                listStr.Add(new SqlParameter("@StartRectificationTime", this.txtStartRectificationTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndRectificationTime.Text.Trim()))
            {
                strSql += " AND RectificationTime <= @EndRectificationTime";
                listStr.Add(new SqlParameter("@EndRectificationTime", this.txtEndRectificationTime.Text.Trim()));
            }
            if (this.drpStates.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND States LIKE @States";
                listStr.Add(new SqlParameter("@States", "%" + this.drpStates.SelectedValue + "%"));
            }
            if (!CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId))
            {
                strSql += " AND (ResponsibleUnit =@ResponsibleUnit OR SendUnitId=@SendUnitId)";
                listStr.Add(new SqlParameter("@ResponsibleUnit", this.CurrUser.UnitId));
                listStr.Add(new SqlParameter("@SendUnitId", this.CurrUser.UnitId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;         
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 表排序、分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {          
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuSee_Click(null, null);
        }
        #endregion

        #region 查看
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuSee_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string RegistrationId = Grid1.SelectedRowID;
            var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(RegistrationId);
            if (registration != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationView.aspx?HazardRegisterId={0}", RegistrationId, "查看 - ")));
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string RegistrationId = Grid1.SelectedRowID;
            var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(RegistrationId);
            if (registration != null)
            {
                if (registration.States == Const.State_1)    //待整改
                {
                    if (registration.CheckManId == this.CurrUser.UserId)    //当前人是检查人，可以在整改前继续编辑
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationAdd.aspx?HazardRegisterId={0}", RegistrationId, "编辑 - ")));
                    }
                    else
                    {
                        Alert.ShowInTop("您不是记录检查人，无法编辑！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (registration.States == Const.State_2)   //已整改待确认
                {
                    if (registration.ResponsibleMan == this.CurrUser.UserId)    //当前人是责任人，可以在确认前继续编辑
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationRectify.aspx?HazardRegisterId={0}", RegistrationId, "编辑 - ")));
                    }
                    else
                    {
                        Alert.ShowInTop("您不是记录责任人，无法编辑！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (registration.States == Const.State_3)   //已闭环
                {
                    Alert.ShowInTop("该记录已闭环，无法编辑！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion

        #region 整改
        /// <summary>
        /// 整改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuRectify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string RegistrationId = Grid1.SelectedRowID;
            var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(RegistrationId);
            if (registration != null)
            {
                if (registration.States == "1")    //待整改
                {
                    if (registration.ResponsibleMan == this.CurrUser.UserId)    //当前人是责任人，可以进行整改操作
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationRectify.aspx?HazardRegisterId={0}", RegistrationId, "编辑 - ")));
                    }
                    else
                    {
                        Alert.ShowInTop("您不是记录责任人，无法整改！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("该记录不是待整改状态，无法进行整改操作！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion

        #region 确认
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuConfirm_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string RegistrationId = Grid1.SelectedRowID;
            var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(RegistrationId);
            if (registration != null)
            {
                if (registration.States == Const.State_2)    //待确认
                {
                    if (registration.CheckManId == this.CurrUser.UserId)    //当前人是检查人，可以进行确认操作
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationConfirm.aspx?HazardRegisterId={0}", RegistrationId, "编辑 - ")));
                    }
                    else
                    {
                        Alert.ShowInTop("您不是记录检查人，无法确认！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("该记录不是待确认状态，无法进行确认操作！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion

        #region 编制
        /// <summary>
        /// 编制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationAdd.aspx", "登记 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string RegistrationId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.HSSE_Hazard_HazardRegister hazardRegister = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(RegistrationId);
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    if (!ItemSelectedList.Contains(RegistrationId))
                    {
                        ItemSelectedList.Add(RegistrationId);
                    }
                }
                else
                {
                    if (ItemSelectedList.Contains(RegistrationId))
                    {
                        ItemSelectedList.Remove(RegistrationId);
                    }
                }
            }
            if (e.CommandName == "del")
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnDelete))
                {
                    if (hazardRegister.States == Const.State_1 || this.CurrUser.UserId == BLL.Const.sysglyId)   //待整改
                    {
                        var getD = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(RegistrationId);
                        if (getD != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getD.HazardCode, getD.HazardRegisterId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnDelete);
                            BLL.HSSE_Hazard_HazardRegisterService.DeleteHazardRegisterByHazardRegisterId(RegistrationId);                            
                            BindGrid();
                            ShowNotify("删除成功!", MessageBoxIcon.Success);
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("已进入整改流程，无法删除！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                    return;
                }
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
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        Model.HSSE_Hazard_HazardRegister hazardRegister = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(rowID);
                        if (hazardRegister.States == "1" || this.CurrUser.UserId == BLL.Const.sysglyId)   //待整改
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, hazardRegister.HazardCode, hazardRegister.HazardRegisterId, BLL.Const.HSSE_HiddenRectificationListMenuId, BLL.Const.BtnDelete);
                            BLL.HSSE_Hazard_HazardRegisterService.DeleteHazardRegisterByHazardRegisterId(rowID);
                        }
                        else
                        {
                            Alert.ShowInTop("已进入整改流程，无法删除！", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    BindGrid();
                    ShowNotify("删除成功!", MessageBoxIcon.Success);
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取整改前图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../../", registration.RectificationImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../../", registration.RectificationImageUrl);
                }
            }
            return url;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("日常巡检" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 100000;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “HiddenRectificationList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “HiddenRectificationList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (column.ColumnID != "ckbIsSelected" && column.ColumnID != "tfImageUrl1" && column.ColumnID != "tfImageUrl2" && column.ColumnID != "Punish" && column.ColumnID != "Del")
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    if (column.ColumnID != "ckbIsSelected" && column.ColumnID != "tfImageUrl1" && column.ColumnID != "tfImageUrl2" && column.ColumnID != "Punish" && column.ColumnID != "Del")
                    {
                        string html = row.Values[column.ColumnIndex].ToString();
                        if (column.ColumnID == "tfPageIndex")
                        {
                            html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                        }
                        if (column.ColumnID == "tfImageUrl")
                        {
                            html = (row.FindControl("lbtnImageUrl") as AspNet.LinkButton).Text;
                        }
                        if (column.ColumnID == "tfRectificationImageUrl")
                        {
                            html = (row.FindControl("lbtnRectificationImageUrl") as AspNet.LinkButton).Text;
                        }
                        //if (column.ColumnID == "tfCutPayment")
                        //{
                        //    html = (row.FindControl("lbtnCutPayment") as AspNet.LinkButton).Text;
                        //}
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
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
            if (!string.IsNullOrEmpty(this.txtStartRectificationTime.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndRectificationTime.Text.Trim()))
            {
                if (Funs.GetNewDateTime(this.txtStartRectificationTime.Text.Trim()) > Funs.GetNewDateTime(this.txtEndRectificationTime.Text.Trim()))
                {
                    Alert.ShowInTop("开始时间不能大于结束时间！", MessageBoxIcon.Warning);
                    return;
                }
            }
            this.BindGrid();
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.hdRemark.Text))
            //{
            string hazardRegisterIds = string.Empty;
            foreach (var item in ItemSelectedList)
            {
                hazardRegisterIds += item + ",";
            }
            if (!string.IsNullOrEmpty(hazardRegisterIds))
            {
                hazardRegisterIds = hazardRegisterIds.Substring(0, hazardRegisterIds.LastIndexOf(","));
            }
       //     PageContext.RegisterStartupScript(Window4.GetShowReference(String.Format("HiddenRectificationPrint.aspx?HazardRegisterIds={0}&CheckType={1}", hazardRegisterIds,"0", "查看 - ")));            
            this.hdRemark.Text = string.Empty;
            //}
        }

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
            var buttonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HSSE_HiddenRectificationListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnRectify.Hidden = false;
                    this.btnConfirm.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    this.btnModify.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}