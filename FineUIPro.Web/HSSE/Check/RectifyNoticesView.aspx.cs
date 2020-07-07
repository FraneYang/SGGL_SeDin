using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FineUIPro.Web.HSSE.Check
{
    public partial class RectifyNoticesView1 : PageBase
    {
        public string RectifyNoticesId
        {
            get
            {
                return (string)ViewState["RectifyNoticesId"];
            }
            set
            {
                ViewState["RectifyNoticesId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                RectifyNoticesId = Request.Params["RectifyNoticesId"];
                if (!string.IsNullOrEmpty(RectifyNoticesId))
                {
                    Model.Check_RectifyNotices RectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                    if (RectifyNotices != null) {
                        if (!string.IsNullOrEmpty(RectifyNotices.UnitId)) {
                            txtUnitId.Text = BLL.UnitService.GetUnitNameByUnitId(RectifyNotices.UnitId);
                        }
                        if (!string.IsNullOrEmpty(RectifyNotices.CheckManIds))
                        {
                            string CheckManId = string.Empty;
                            if (RectifyNotices.CheckManIds != null)
                            {
                                string[] Ids = RectifyNotices.CheckManIds.ToString().Split(',');
                                foreach (string t in Ids)
                                {
                                    var Name = BLL.UserService.GetUserNameByUserId(t);
                                    if (Name != null)
                                    {
                                        CheckManId += Name + ",";
                                    }
                                }
                            }
                            if (CheckManId != string.Empty)
                            {
                                
                                txtCheckPersonId.Text= CheckManId.Substring(0, CheckManId.Length - 1);
                            }
                        }
                        if (!string.IsNullOrEmpty(RectifyNotices.WorkAreaId) )
                        {
                            txtWorkAreaId.Text = BLL.UnitWorkService.GetUnitWorkName(RectifyNotices.WorkAreaId);
                        }
                        this.txtCheckPerson.Text = RectifyNotices.CheckManNames;
                        this.txtRectifyNoticesCode.Text = RectifyNotices.RectifyNoticesCode;
                        this.txtCheckedDate.Text = RectifyNotices.CheckedDate.ToString();
                        if (!string.IsNullOrEmpty(RectifyNotices.HiddenHazardType))
                        {
                            this.drpHiddenHazardType.SelectedValue = RectifyNotices.HiddenHazardType;
                        }
                        BindGrid1();
                        BindGrid();
                    }
                    
                }
            }
        }

        public void BindGrid1()
        {
            string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults, (case IsRectify when 'True' then '合格' when 'False' then '不合格' else '' end) As IsRectify  from [dbo].[Check_RectifyNoticesItem] ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where RectifyNoticesId = @RectifyNoticesId";
            listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        public void BindGrid()
        {
            string strSql = @"select FlowOperateId, RectifyNoticesId, OperateName, OperateManId, OperateTime, IsAgree, Opinion,S.UserName from Check_RectifyNoticesFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where RectifyNoticesId= @RectifyNoticesId";
            listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }

        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object RectifyNoticesItemId)
        {
            string url = string.Empty;
            if (RectifyNoticesItemId != null)
            {
                var RectifyNoticesItem = BLL.AttachFileService.GetAttachFile(RectifyNoticesItemId.ToString() + "#1", BLL.Const.ProjectRectifyNoticesMenuId);
                if (RectifyNoticesItem != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("~/", RectifyNoticesItem.AttachUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrlByImage(object RectifyNoticesItemId)
        {
            string url = string.Empty;
            if (RectifyNoticesItemId != null)
            {
                var RectifyNoticesItem = BLL.AttachFileService.GetAttachFile(RectifyNoticesItemId.ToString() + "#2", BLL.Const.ProjectRectifyNoticesMenuId);
                if (RectifyNoticesItem != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("~/", RectifyNoticesItem.AttachUrl);
                }
            }
            return url;
        }
    }
}