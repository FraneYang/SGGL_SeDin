using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class PunishNoticeView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PunishNoticeId
        {
            get
            {
                return (string)ViewState["PunishNoticeId"];
            }
            set
            {
                ViewState["PunishNoticeId"] = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        private string AttchUrl
        {
            get
            {
                return (string)ViewState["AttchUrl"];
            }
            set
            {
                ViewState["AttchUrl"] = value;
            }
        }
        #endregion

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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                
                this.PunishNoticeId = Request.Params["PunishNoticeId"];
                this.txtCurrency.Text = "人民币";
                if (!string.IsNullOrEmpty(this.PunishNoticeId))
                {
                    BindGrid();
                    BindGrid1();
                    Model.Check_PunishNotice punishNotice = BLL.PunishNoticeService.GetPunishNoticeById(this.PunishNoticeId);
                    if (punishNotice != null)
                    {
                        this.txtPunishNoticeCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PunishNoticeId);
                        if (punishNotice.PunishNoticeDate != null)
                        {
                            this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", punishNotice.PunishNoticeDate);
                        }
                        if (!string.IsNullOrEmpty(punishNotice.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(punishNotice.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(punishNotice.PunishPersonId))
                        {
                            var user = BLL.UserService.GetUserNameByUserId(punishNotice.PunishPersonId);
                            if (!string.IsNullOrEmpty(user))
                            {
                                this.txtPunishPersonId.Text = user;
                            }
                        }
                        Model.Sys_User user1 = BLL.UserService.GetUserByUserId(punishNotice.SignMan);
                        if (user1 != null)
                        {
                            this.txtSignMan.Text = user1.UserName;
                        }
                        Model.Sys_User user2 = BLL.UserService.GetUserByUserId(punishNotice.ApproveMan);
                        if (user2 != null)
                        {
                            this.txtApproveMan.Text = user2.UserName;
                        }
                        this.txtIncentiveReason.Text = punishNotice.IncentiveReason;
                        this.txtBasicItem.Text = punishNotice.BasicItem;
                        if (punishNotice.PunishMoney != null)
                        {
                            this.txtPunishMoney.Text = Convert.ToString(punishNotice.PunishMoney);
                            this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));//转换大写
                        }
                        this.AttchUrl = punishNotice.AttachUrl;
                        //this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttchUrl);
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(punishNotice.FileContents);
                        if (!string.IsNullOrEmpty(punishNotice.Currency))
                        {
                            this.txtCurrency.Text = punishNotice.Currency;
                        }
                    }
                };
            }
        }
        //处罚明细
        public void BindGrid1()
        {
            string strSql = @"select PunishNoticeItemId, PunishNoticeId, PunishContent, PunishMoney, SortIndex from Check_PunishNoticeItem ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where PunishNoticeId= @PunishNoticeId";
            listStr.Add(new SqlParameter("@PunishNoticeId", PunishNoticeId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

        }
        //办理记录
        public void BindGrid()
        {
            string strSql = @"select FlowOperateId, PunishNoticeId, OperateName, OperateManId, OperateTime, case when IsAgree='False' then '否' else '是' end  As IsAgree, Opinion,S.UserName from Check_PunishNoticeFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where PunishNoticeId= @PunishNoticeId";
            listStr.Add(new SqlParameter("@PunishNoticeId", PunishNoticeId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNotice&type=-1&menuId=" + BLL.Const.ProjectPunishNoticeMenuId, this.PunishNoticeId)));
        }

        /// <summary>
        /// 通知单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPunishNoticeUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNoticeStatistics&type=-1&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
        }
        #endregion
    }
}