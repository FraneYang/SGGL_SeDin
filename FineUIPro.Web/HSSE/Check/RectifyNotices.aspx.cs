using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class RectifyNotices : PageBase
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
        /// <summary>
        /// 行号
        /// </summary>
        public int RowCount
        {
            get
            {
                return (int)ViewState["RowCount"];
            }
            set
            {
                ViewState["RowCount"] = value;
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                //btnNew.OnClientClick = Window1.GetShowReference("RectifyNoticesEdit.aspx") + "return false;";
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
                btnNew.OnClientClick = Window1.GetShowReference("RectifyNoticesEdit.aspx") + "return false;";
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT R.RectifyNoticesId,R.ProjectId,R.RectifyNoticesCode,R.UnitId ,Unit.UnitName,R.WorkAreaId,R.CheckManNames,R.CheckManIds
                        ,WorkAreaName= STUFF(( SELECT ',' + UnitWorkName FROM dbo.WBS_UnitWork where PATINDEX('%,' + RTRIM(UnitWorkId) + ',%',',' +R.WorkAreaId + ',')>0 FOR XML PATH('')), 1, 1,''),CheckPersonName= (STUFF(( SELECT ',' + UserName FROM dbo.Sys_User where PATINDEX('%,' + RTRIM(UserId) + ',%',',' +R.CheckManIds+ ',')>0 FOR XML PATH('')), 1, 1,'')+ (CASE WHEN CheckManNames IS NOT NULL AND CheckManNames !='' THEN ','+ CheckManNames ELSE '' END)),R.DutyPerson,R.CheckedDate,DutyPerson.UserName AS DutyPersonName,R.DutyPersonTime,R.CompleteDate,(CASE WHEN States = 0 THEN '待['+CompileMan.UserName+']提交' WHEN States = 1 THEN '待['+SignMan.UserName+']签发' WHEN States = 2 THEN '待['+DutyPerson.UserName+']整改' WHEN States = 3 THEN '待['+UnitHeadMan.UserName+']审核' WHEN States = 4 THEN '待['+CheckPerson.UserName+']复查' WHEN States = 5 THEN '已完成' ELSE '' END) AS StatesName
                        FROM Check_RectifyNotices AS R
                        LEFT JOIN Base_Project AS Project ON Project.ProjectId = R.ProjectId 
                        LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = R.UnitId 
						LEFT JOIN Sys_User AS CompileMan ON CompileMan.UserId=R.CompleteManId 
                        LEFT JOIN Sys_User AS SignMan ON SignMan.UserId=R.SignPerson  
                        LEFT JOIN Sys_User AS DutyPerson ON DutyPerson.UserId = R.DutyPerson
						LEFT JOIN Sys_User AS UnitHeadMan ON UnitHeadMan.UserId = R.UnitHeadManId
						LEFT JOIN Sys_User AS CheckPerson ON CheckPerson.UserId = R.CheckPerson
                        LEFT JOIN Sys_CodeRecords AS CodeRecords ON R.RectifyNoticesId = CodeRecords.DataId 
                        WHERE States IS NOT NULL  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND R.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND R.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }
            if (!string.IsNullOrEmpty(this.txtRectifyNoticesCode.Text.Trim()))
            {
                strSql += " AND RectifyNoticesCode LIKE @RectifyNoticesCode";
                listStr.Add(new SqlParameter("@RectifyNoticesCode", "%" + this.txtRectifyNoticesCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND Unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWorkAreaName.Text.Trim()))
            {
                strSql += " AND WorkArea.WorkAreaName LIKE @WorkAreaName";
                listStr.Add(new SqlParameter("@WorkAreaName", "%" + this.txtWorkAreaName.Text.Trim() + "%"));
            }
            if (this.rbStates.SelectedValue != "-1")
            {
                strSql += " AND R.States =@States";
                listStr.Add(new SqlParameter("@States", this.rbStates.SelectedValue));
            }
            if (this.rbrbHiddenHazardType.SelectedValue != "-1")
            {
                strSql += " AND R.HiddenHazardType =@HiddenHazardType";
                listStr.Add(new SqlParameter("@HiddenHazardType", this.rbrbHiddenHazardType.SelectedValue));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            this.RowCount = Grid1.RecordCount;
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

        #region 表排序、分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
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

        #region Grid双击事件 编辑
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            EditData(Grid1.SelectedRowID);
        }

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

            EditData(Grid1.SelectedRowID);
        }

        /// <summary>
        /// 
        /// </summary>
        private void EditData(string rectifyNoticeId)
        {
            bool flag = false;
            Model.Check_RectifyNotices RectifyNotices =  RectifyNoticesService.GetRectifyNoticesById(Grid1.SelectedRowID);
            if (RectifyNotices.States == "0" && this.CurrUser.UserId == RectifyNotices.CompleteManId)
            {
                    flag = true;
            }
            if (RectifyNotices.States == "1" && this.CurrUser.UserId == RectifyNotices.SignPerson) {
                flag = true;
            }
            else if (RectifyNotices.States == "2" && this.CurrUser.UserId == RectifyNotices.DutyPersonId)
            {
                    flag = true;
            }
            else if (RectifyNotices.States == "3" && this.CurrUser.UserId == RectifyNotices.UnitHeadManId)
            {
                    flag = true;
            }
            else if (RectifyNotices.States == "4" && this.CurrUser.UserId == RectifyNotices.CheckPerson)
            {
                    flag = true;
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesView.aspx?RectifyNoticesId={0}", rectifyNoticeId, "查看 - ")));
            }
            if(flag) {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesEdit.aspx?RectifyNoticesId={0}", rectifyNoticeId, "编辑 - ")));
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var RectifyNotices = BLL.RectifyNoticesService.GetRectifyNoticesById(rowID);
                    if (RectifyNotices != null)
                    {

                        LogService.AddSys_Log(this.CurrUser, RectifyNotices.RectifyNoticesCode, rowID, BLL.Const.ProjectRectifyNoticeMenuId, BLL.Const.BtnDelete);
                        RectifyNoticesService.DeleteRectifyNoticesById(rowID);
                        Model.Check_CheckSpecialDetail specialDetail = (from x in Funs.DB.Check_CheckSpecialDetail
                                                                        where x.RectifyNoticeId == rowID
                                                                        select x).FirstOrDefault();
                        Model.Check_CheckColligationDetail colligationDetail = (from x in Funs.DB.Check_CheckColligationDetail
                                                                                where x.RectifyNoticeId == rowID
                                                                                select x).FirstOrDefault();               
                        if (specialDetail != null)
                        {
                            specialDetail.RectifyNoticeId = null;
                            Funs.DB.SubmitChanges();
                        }
                        else if (colligationDetail != null)
                        {
                            colligationDetail.RectifyNoticeId = null;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）"); 
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectRectifyNoticesMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("隐患整改通知单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = Encoding.UTF8;
            this.Grid1.PageSize = this.RowCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length == 0)
        //    {
        //        Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
        //        return;
        //    }
        //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticePrint.aspx?RectifyNoticeId={0}", Grid1.SelectedRowID, "打印 - ")));
        //}

        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        /// <summary>
        /// 获取检察人员
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckPerson(object CheckManIds)
        {
            string CheckManName = string.Empty;
            if (CheckManIds != null)
            {
                string[] Ids = CheckManIds.ToString().Split(',');
                foreach (string t in Ids)
                {
                    var Name = BLL.UserService.GetUserNameByUserId(t);
                    if (Name != null)
                    {
                        CheckManName += Name + ",";
                    }
                }
            }
            if (CheckManName != string.Empty)
            {
                return CheckManName.Substring(0, CheckManName.Length - 1);
            }
            else
            {
                return "";
            }
        }
    }
}