using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class IncentiveNoticeAdd : PageBase
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
        /// 项目主键
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
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
                Funs.FineUIPleaseSelect(this.drpTeamGroup);
                Funs.FineUIPleaseSelect(this.drpPerson);
                BLL.ConstValue.InitConstValueDropDownList(this.drpRewardType, BLL.ConstValue.Group_RewardType, true);
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                //BindGrid(string.Empty);
                this.IncentiveNoticeId = Request.Params["IncentiveNoticeId"];
                this.txtCurrency.Text = "人民币";
                if (!string.IsNullOrEmpty(this.IncentiveNoticeId))
                {
                    BindGrid();
                    Model.Check_IncentiveNotice incentiveNotice = BLL.IncentiveNoticeService.GetIncentiveNoticeById(this.IncentiveNoticeId);
                    if (incentiveNotice != null)
                    {
                        this.ProjectId = incentiveNotice.ProjectId;
                        this.txtIncentiveNoticeCode.Text = CodeRecordsService.ReturnCodeByDataId(this.IncentiveNoticeId);
                        if (!string.IsNullOrEmpty(incentiveNotice.UnitId))
                        {
                            this.drpUnit.SelectedValue = incentiveNotice.UnitId;
                            BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.ProjectId, incentiveNotice.UnitId, true);
                            if (!string.IsNullOrEmpty(incentiveNotice.TeamGroupId))
                            {
                                this.drpTeamGroup.SelectedValue = incentiveNotice.TeamGroupId;
                            }

                            BLL.PersonService.InitPersonByProjectUnitDropDownList(this.drpPerson, this.ProjectId, incentiveNotice.UnitId, true);
                            if (!string.IsNullOrEmpty(incentiveNotice.PersonId))
                            {
                                this.drpPerson.SelectedValue = incentiveNotice.PersonId;
                            }
                        }
                        this.txtIncentiveDate.Text = string.Format("{0:yyyy-MM-dd}", incentiveNotice.IncentiveDate);
                        this.txtBasicItem.Text = incentiveNotice.BasicItem;
                        if (!string.IsNullOrEmpty(incentiveNotice.RewardType))
                        {
                            this.drpRewardType.SelectedValue = incentiveNotice.RewardType;
                        }
                        if (incentiveNotice.IncentiveMoney.HasValue)
                        {
                            this.txtPayMoney.Text = Convert.ToString(incentiveNotice.IncentiveMoney);
                            this.rbtnIncentiveWay1.Checked = true;
                            this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(this.txtPayMoney.Text));//转换大写
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
                        if (!string.IsNullOrEmpty(incentiveNotice.SignMan))
                        {
                            this.drpSignPerson.SelectedValue = incentiveNotice.SignMan;
                        }
                        if (!string.IsNullOrEmpty(incentiveNotice.Currency))
                        {
                            this.txtCurrency.Text = incentiveNotice.Currency;
                        }
                        if (!string.IsNullOrEmpty(incentiveNotice.States))
                        {
                            State = incentiveNotice.States;
                        }
                        
                    }
                }
                else
                {
                    //this.drpSignMan.SelectedValue = this.CurrUser.UserId;
                    this.txtIncentiveDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectIncentiveNoticeMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    ////自动生成编码
                    this.txtIncentiveNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectIncentiveNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
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

        #region DropDownList下拉选择事件
        /// <summary>
        ///  单位下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpTeamGroup.Items.Clear();
            this.drpPerson.Items.Clear();
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.ProjectId, this.drpUnit.SelectedValue, true);
                BLL.PersonService.InitPersonByProjectUnitDropDownList(this.drpPerson, this.ProjectId, this.drpUnit.SelectedValue, true);
                this.drpTeamGroup.SelectedValue = BLL.Const._Null;
                this.drpPerson.SelectedValue = BLL.Const._Null;
            }
            else
            {
                Funs.FineUIPleaseSelect(this.drpTeamGroup);
                Funs.FineUIPleaseSelect(this.drpPerson);
                this.drpTeamGroup.SelectedValue = BLL.Const._Null;
                this.drpPerson.SelectedValue = BLL.Const._Null;
            }
        }
        #endregion

        #region   奖励方式选择
        /// <summary>
        /// 经济奖励
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbtnIncentiveWay1_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (rbtnIncentiveWay1.Checked)
            {
                this.txtPayMoney.Readonly = false;
                this.rbtnIncentiveWay2.Checked = false;
                this.rbtnIncentiveWay3.Checked = false;
                this.txtTitleReward.Text = string.Empty;
                this.txtMattleReward.Text = string.Empty;
                this.txtTitleReward.Readonly = true;
                this.txtMattleReward.Readonly = true;
            }
            else
            {
                this.txtPayMoney.Enabled = false;
            }
        }

        /// <summary>
        /// 称号奖励
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbtnIncentiveWay2_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (rbtnIncentiveWay2.Checked)
            {
                this.txtTitleReward.Readonly = false;
                this.rbtnIncentiveWay1.Checked = false;
                this.rbtnIncentiveWay3.Checked = false;
                this.txtPayMoney.Text = string.Empty;
                this.txtBig.Text = string.Empty;
                this.txtMattleReward.Text = string.Empty;
                this.txtPayMoney.Readonly = true;
                this.txtMattleReward.Readonly = true;
            }
            else
            {
                this.txtTitleReward.Readonly = true;
            }
        }

        /// <summary>
        /// 物质奖励
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbtnIncentiveWay3_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (rbtnIncentiveWay3.Checked)
            {
                this.txtMattleReward.Readonly = false;
                this.rbtnIncentiveWay1.Checked = false;
                this.rbtnIncentiveWay2.Checked = false;
                this.txtMattleReward.Text = string.Empty;
                this.txtBig.Text = string.Empty;
                this.txtTitleReward.Text = string.Empty;
                this.txtPayMoney.Readonly = true;
                this.txtTitleReward.Readonly = true;
            }
            else
            {
                this.txtMattleReward.Readonly = true;
            }
        }
        #endregion

        #region  获取大写金额事件
        /// <summary>
        /// 获取大写金额事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPayMoney_Blur(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPayMoney.Text))
            {
                this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(this.txtPayMoney.Text));//转换大写
            }
            else
            {
                this.txtBig.Text = string.Empty;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFile_Click(object sender, EventArgs e)
        {
            if (btnFile.HasFile)
            {
                this.AttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnFile, this.AttachUrl, UploadFileService.IncentiveNoticeFilePath);
                this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.AttachUrl);
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受奖单位", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受奖单位", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Check_IncentiveNotice incentiveNotice = new Model.Check_IncentiveNotice
            {
                ProjectId = this.ProjectId,
                IncentiveNoticeCode = this.txtIncentiveNoticeCode.Text.Trim(),
                UnitId = this.drpUnit.SelectedValue,
                IncentiveDate = Funs.GetNewDateTime(this.txtIncentiveDate.Text.Trim())
            };
            if (this.drpTeamGroup.SelectedValue != BLL.Const._Null)
            {
                incentiveNotice.TeamGroupId = this.drpTeamGroup.SelectedValue;
            }
            if (this.drpPerson.SelectedValue != BLL.Const._Null)
            {
                incentiveNotice.PersonId = this.drpPerson.SelectedValue;
            }
            incentiveNotice.BasicItem = this.txtBasicItem.Text.Trim();
            if (this.drpRewardType.SelectedValue != BLL.Const._Null)
            {
                incentiveNotice.RewardType = this.drpRewardType.SelectedValue;
            }
            incentiveNotice.IncentiveMoney = Funs.GetNewDecimalOrZero(this.txtPayMoney.Text.Trim());
            incentiveNotice.TitleReward = this.txtTitleReward.Text.Trim();
            incentiveNotice.MattleReward = this.txtMattleReward.Text.Trim();
            incentiveNotice.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            incentiveNotice.AttachUrl = this.AttachUrl;
            incentiveNotice.CompileMan = this.CurrUser.UserId;
            incentiveNotice.CompileDate = Funs.GetNewDateTime(this.txtIncentiveDate.Text.Trim());
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                incentiveNotice.SignMan = this.drpSignPerson.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("总包安全工程师/安全经理不能为空！", MessageBoxIcon.Warning);
                return;
            }
            incentiveNotice.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                incentiveNotice.States = "1";
            }
            incentiveNotice.Currency = this.txtCurrency.Text.Trim();
            if (!string.IsNullOrEmpty(this.IncentiveNoticeId))
            {
                incentiveNotice.IncentiveNoticeId = this.IncentiveNoticeId;
                BLL.IncentiveNoticeService.UpdateIncentiveNotice(incentiveNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, incentiveNotice.IncentiveNoticeCode, incentiveNotice.IncentiveNoticeId, BLL.Const.ProjectIncentiveNoticeMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.IncentiveNoticeId = SQLHelper.GetNewID(typeof(Model.Check_IncentiveNotice));
                incentiveNotice.IncentiveNoticeId = this.IncentiveNoticeId;
                BLL.IncentiveNoticeService.AddIncentiveNotice(incentiveNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, incentiveNotice.IncentiveNoticeCode, incentiveNotice.IncentiveNoticeId, BLL.Const.ProjectIncentiveNoticeMenuId, BLL.Const.BtnAdd);
            }
            if (incentiveNotice.States == "1")
            {
                SaveOperate("总包安全工程师下发奖励单");
            }
        }
        #region 保存流程审核数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void SaveOperate(string OperateName)
        {
            Model.Check_IncentiveNoticeFlowOperate newFlowOperate = new Model.Check_IncentiveNoticeFlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_IncentiveNoticeFlowOperate)),
                IncentiveNoticeId = IncentiveNoticeId,
                OperateName = OperateName,
                OperateManId = CurrUser.UserId,
                OperateTime = DateTime.Now,
                IsAgree = true
            };
            Funs.DB.Check_IncentiveNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
            Funs.DB.SubmitChanges();

        }
        #endregion
        #endregion
    }
}