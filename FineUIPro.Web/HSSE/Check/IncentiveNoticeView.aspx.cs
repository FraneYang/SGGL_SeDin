using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class IncentiveNoticeView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string IncentiveNoticeId
        {
            get
            {
                return (string)ViewState["IncentiveNoticeId"];
            }
            set
            {
                ViewState["IncentiveNoticeId"] = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        private string AttachUrl
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
                this.IncentiveNoticeId = Request.Params["IncentiveNoticeId"];
                this.txtCurrency.Text = "人民币";
                if (!string.IsNullOrEmpty(this.IncentiveNoticeId))
                {
                    BindGrid();
                    Model.Check_IncentiveNotice incentiveNotice = BLL.IncentiveNoticeService.GetIncentiveNoticeById(this.IncentiveNoticeId);
                    if (incentiveNotice != null)
                    {
                        this.txtIncentiveNoticeCode.Text = CodeRecordsService.ReturnCodeByDataId(this.IncentiveNoticeId);
                        if (!string.IsNullOrEmpty(incentiveNotice.UnitId))
                        {
                            var unit = UnitService.GetUnitByUnitId(incentiveNotice.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(incentiveNotice.TeamGroupId))
                        {
                            var teamGroup = TeamGroupService.GetTeamGroupById(incentiveNotice.TeamGroupId);
                            if (teamGroup != null)
                            {
                                this.txtTeamGroup.Text = teamGroup.TeamGroupName;
                            }
                        }
                        if (!string.IsNullOrEmpty(incentiveNotice.PersonId))
                        {
                            var person = BLL.PersonService.GetPersonById(incentiveNotice.PersonId);
                            if (person!=null)
                            {
                                this.txtPerson.Text = person.PersonName;
                            }
                        }
                       
                        if (incentiveNotice.IncentiveDate != null)
                        {
                            this.txtIncentiveDate.Text = string.Format("{0:yyyy-MM-dd}", incentiveNotice.IncentiveDate);
                        }
                        this.txtBasicItem.Text = incentiveNotice.BasicItem;
                        if (!string.IsNullOrEmpty(incentiveNotice.RewardType))
                        {
                            Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_RewardType).FirstOrDefault(x => x.ConstValue == incentiveNotice.RewardType);
                            if (c != null)
                            {
                                this.txtRewardType.Text = c.ConstText;
                            }
                        }
                        if (incentiveNotice.IncentiveMoney != null)
                        {
                            this.txtPayMoney.Text = Convert.ToString(incentiveNotice.IncentiveMoney);
                            this.rbtnIncentiveWay1.Checked = true;
                            this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPayMoney.Text));//转换大写
                        }
                        if (!string.IsNullOrEmpty(incentiveNotice.TitleReward))
                        {
                            this.txtTitleReward.Text = incentiveNotice.TitleReward;
                            this.rbtnIncentiveWay2.Checked = true;
                        }
                        if (!string.IsNullOrEmpty(incentiveNotice.MattleReward))
                        {
                            this.txtMattleReward.Text = incentiveNotice.MattleReward;
                            this.rbtnIncentiveWay3.Checked = true;
                        }
                        this.AttachUrl = incentiveNotice.AttachUrl;
                        this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.AttachUrl);
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(incentiveNotice.FileContents);
                        if (!string.IsNullOrEmpty(incentiveNotice.Currency))
                        {
                            this.txtCurrency.Text = incentiveNotice.Currency;
                        }
                    }
                }
            }
        }

        //办理记录
        public void BindGrid()
        {
            string strSql = @"select FlowOperateId, IncentiveNoticeId, OperateName, OperateManId, OperateTime, case when IsAgree='False' then '否' else '是' end  As IsAgree, Opinion,S.UserName from Check_IncentiveNoticeFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where IncentiveNoticeId= @IncentiveNoticeId";
            listStr.Add(new SqlParameter("@IncentiveNoticeId", IncentiveNoticeId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
        #endregion
    }
}