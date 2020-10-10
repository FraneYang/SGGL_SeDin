using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class PauseNoticeView : PageBase
    {
        #region  定义项
        /// <summary>
        /// 工程暂停令主键
        /// </summary>
        public string PauseNoticeId
        {
            get
            {
                return (string)ViewState["PauseNoticeId"];
            }
            set
            {
                ViewState["PauseNoticeId"] = value;
            }
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
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
                LoadData();

                PauseNoticeId = Request.Params["PauseNoticeId"];
                if (!string.IsNullOrEmpty(PauseNoticeId))
                {
                    Model.Check_PauseNotice pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);

                    this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PauseNoticeId);
                    if (!string.IsNullOrEmpty(pauseNotice.UnitId))
                    {
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(pauseNotice.UnitId);
                        if (unit != null)
                        {
                            this.txtUnit.Text = unit.UnitName;
                        }
                    }
                    if (!string.IsNullOrEmpty(pauseNotice.SignManId))
                    {
                        txtSignMan.Text = BLL.UserService.getUserNamesUserIds(pauseNotice.SignManId);
                    }
                    if (!string.IsNullOrEmpty(pauseNotice.ApproveManId))
                    {
                        txtApproveMan.Text = BLL.UserService.getUserNamesUserIds(pauseNotice.ApproveManId);
                    }
                    if (!string.IsNullOrEmpty(pauseNotice.CompileManId))
                    {
                        txtSignPerson.Text = BLL.UserService.getUserNamesUserIds(pauseNotice.CompileManId);
                    }
                    //this.txtProjectPlace.Text = pauseNotice.ProjectPlace;
                    this.txtUnitWorkName.Text = UnitWorkService.GetNameById(pauseNotice.UnitWorkId);
                    if (pauseNotice.CompileDate != null)
                    {
                        this.txtComplieDate.Text = string.Format("{0:yyyy-MM-dd}", pauseNotice.CompileDate);
                    }
                    this.txtWrongContent.Text = pauseNotice.WrongContent;
                    if (pauseNotice.PauseTime.HasValue)
                    {
                        this.txtPauseTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", pauseNotice.PauseTime);
                    }
                }
                    BindGrid();
                }
            }
            public void BindGrid()
            {
                string strSql = @"select FlowOperateId, PauseNoticeId, OperateName, OperateManId, OperateTime, case when IsAgree='False' then '否' else '是' end  As IsAgree, Opinion,S.UserName from Check_PauseNoticeFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += "where PauseNoticeId= @PauseNoticeId";
                listStr.Add(new SqlParameter("@PauseNoticeId", PauseNoticeId));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                var table = this.GetPagedDataTable(gvFlowOperate, tb);
                gvFlowOperate.DataSource = table;
                gvFlowOperate.DataBind();
            }
            private void LoadData()
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
            }
            #endregion

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void btnNoticeUrl_Click(object sender, EventArgs e)
            {

                if (!string.IsNullOrEmpty(this.PauseNoticeId))
                {
                    var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPauseNoticeMenuId);
                    if (buttonList.Count() > 0)
                    {
                        PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, this.PauseNoticeId)));
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, this.PauseNoticeId)));
                    }
                }
            }
        }
    }