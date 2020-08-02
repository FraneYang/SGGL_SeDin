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
                this.RectifyNoticesId = Request.Params["RectifyNoticesId"];
                if (!string.IsNullOrEmpty(RectifyNoticesId))
                {
                    var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                    if (getRectifyNotices != null)
                    {
                        this.txtProjectName.Text = ProjectService.GetProjectNameByProjectId(getRectifyNotices.ProjectId);
                        this.txtUnitId.Text = UnitService.GetUnitNameByUnitId(getRectifyNotices.UnitId);
                        this.txtCheckPersonId.Text = UserService.getUserNamesUserIds(getRectifyNotices.CheckManIds);
                        this.txtWorkAreaId.Text = BLL.UnitWorkService.GetUnitWorkName(getRectifyNotices.WorkAreaId);
                        this.txtCheckPerson.Text = getRectifyNotices.CheckManNames;
                        this.txtRectifyNoticesCode.Text = getRectifyNotices.RectifyNoticesCode;
                        this.txtCheckedDate.Text =string.Format("{0:yyyy-MM-dd}",getRectifyNotices.CheckedDate);
                        this.drpHiddenHazardType.Text = "一般";
                        if (getRectifyNotices.HiddenHazardType == "2")
                        {
                            this.drpHiddenHazardType.Text = "较大";
                        }
                        else if (getRectifyNotices.HiddenHazardType == "3")
                        {
                            this.drpHiddenHazardType.Text = "重大";
                        }
                        BindGrid1();
                        BindGrid();

                    }
                }
            }
        }

        public void BindGrid1()
        {
            string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults
                , (case IsRectify when 'True' then '合格' when 'False' then '不合格' else '' end) As IsRectify 
                from [dbo].[Check_RectifyNoticesItem] ";
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
            string strSql = @"select FlowOperateId, RectifyNoticesId, OperateName, OperateManId, OperateTime, IsAgree, Opinion,S.UserName 
                             ,(case when IsAgree=0 then '不同意' else '同意' end) as IsAgreeName                
                            from Check_RectifyNoticesFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
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
                    url = BLL.UploadAttachmentService.ShowImage("../../", RectifyNoticesItem.AttachUrl);
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
                    url = BLL.UploadAttachmentService.ShowImage("../../", RectifyNoticesItem.AttachUrl);
                }
            }
            return url;
        }
    }
}