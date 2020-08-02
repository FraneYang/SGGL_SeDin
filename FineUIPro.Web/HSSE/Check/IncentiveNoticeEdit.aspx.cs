using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class IncentiveNoticeEdit : PageBase
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
                //BindGrid(string.Empty);
                this.IncentiveNoticeId = Request.Params["IncentiveNoticeId"];
                this.txtCurrency.Text = "人民币";
                Model.Check_IncentiveNotice incentiveNotice = BLL.IncentiveNoticeService.GetIncentiveNoticeById(this.IncentiveNoticeId);
                if (incentiveNotice!=null)
                {
                    BindGrid();
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
                        if (!string.IsNullOrEmpty(incentiveNotice.Currency))
                        {
                            this.txtCurrency.Text = incentiveNotice.Currency;
                        }
                        if (!string.IsNullOrEmpty(incentiveNotice.States))
                        {
                            State = incentiveNotice.States;
                        }
                        if (State == "1")///状态1  签发人选择下一步批准人 并且发送抄送人员
                        {
                            this.IsAgree.Hidden = false;
                            this.GroupPanel2.Hidden = false;
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//总包项目经理
                            this.drpHandleMan.Label = "总包项目经理";
                            if (!string.IsNullOrEmpty(incentiveNotice.ApproveMan))
                            {
                                this.drpHandleMan.SelectedValue = incentiveNotice.ApproveMan;
                            }
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProfessionalEngineer, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//专业工程师
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//施工经理
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadMan, this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, true);//分包单位

                        }
                        if (State == "2")///状态2 批准人选择下一步接收人
                        {
                            this.IsAgree.Hidden = false;
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, incentiveNotice.UnitId, true);//分包单位
                            this.drpHandleMan.Label = "施工分包单位";
                        }
                        if (State == "3")
                        {
                            this.ckAccept.Hidden = false;
                            this.BackMan.Hidden = true;
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
        

        #region 提交
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
            string States = Convert.ToInt32(Convert.ToInt32(State) + 1).ToString();
            Model.Check_IncentiveNotice incentiveNotice = BLL.IncentiveNoticeService.GetIncentiveNoticeById(this.IncentiveNoticeId);
            if (incentiveNotice != null) {
                if (States == BLL.Const.State_2) ////【签发】总包安全经理
                {
                    /// 不同意 打回 同意抄送专业工程师、施工经理、相关施工分包单位并提交【批准】总包项目经理
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        incentiveNotice.States = "0";
                    }
                    else
                    {
                        if (drpProfessionalEngineer.SelectedValue != BLL.Const._Null)
                        {
                            incentiveNotice.ProfessionalEngineerId = drpProfessionalEngineer.SelectedValue;
                        }
                        if (drpConstructionManager.SelectedValue != BLL.Const._Null)
                        { incentiveNotice.ConstructionManagerId = drpConstructionManager.SelectedValue; }
                        if (drpUnitHeadMan.SelectedValue != BLL.Const._Null)
                        {
                            incentiveNotice.UnitHeadManId = drpUnitHeadMan.SelectedValue;
                        }
                        if (drpHandleMan.SelectedValue != BLL.Const._Null)
                        {
                            incentiveNotice.ApproveMan = drpHandleMan.SelectedValue;
                            incentiveNotice.SignDate = DateTime.Now;
                            incentiveNotice.States = "2";
                        }
                        else
                        {
                            Alert.ShowInTop("总包项目经理不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        
                    }
                    SaveOperate("总包安全经理签发");
                    Funs.DB.SubmitChanges();
                }
                else if (States == BLL.Const.State_3) ////【批准】总包项目经理
                {
                    /// 不同意 打回 同意下发【回执】施工分包单位
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        incentiveNotice.States = "1";
                    }
                    else
                    {
                        if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                        {
                            incentiveNotice.DutyPersonId = this.drpHandleMan.SelectedValue;
                            incentiveNotice.ApproveDate = DateTime.Now;
                            incentiveNotice.States = "3";
                        }
                        else
                        {
                            Alert.ShowInTop("施工分包单位不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        
                    }
                    Funs.DB.SubmitChanges();
                    SaveOperate("总包项目经理经理批准");
                }
                else if (States == BLL.Const.State_4)
                {
                    incentiveNotice.DutyPersonDate = DateTime.Now;
                    incentiveNotice.States = "4";
                    Funs.DB.SubmitChanges();
                    SaveOperate("施工分包单位回执");

                    //// 回写专项检查明细表                            
                    var getcheck = Funs.DB.Check_CheckSpecialDetail.FirstOrDefault(x => x.DataId.Contains(incentiveNotice.IncentiveNoticeId));
                    if (getcheck != null)
                    {
                        getcheck.CompleteStatus = true;
                        getcheck.CompletedDate = DateTime.Now;
                        Funs.DB.SubmitChanges();
                        //// 根据明细ID判断是否全部整改完成 并更新专项检查状态
                        Check_CheckSpecialService.UpdateCheckSpecialStates(getcheck.CheckSpecialId);
                    }

                }
            }
            
        }
        #endregion
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
            Model.Check_IncentiveNoticeFlowOperate newFlowOperate = new Model.Check_IncentiveNoticeFlowOperate();
            newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_IncentiveNoticeFlowOperate));
            newFlowOperate.IncentiveNoticeId = IncentiveNoticeId;
            newFlowOperate.OperateName = OperateName;
            newFlowOperate.OperateManId = CurrUser.UserId;
            newFlowOperate.OperateTime = DateTime.Now;
            if (this.rdbIsAgree.SelectedValue.Equals("false"))
            {
                newFlowOperate.IsAgree = false;
                newFlowOperate.Opinion = this.reason.Text;
            }
            else
            {
                newFlowOperate.IsAgree = true;
                newFlowOperate.Opinion = "同意";
            }
            Funs.DB.Check_IncentiveNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
            Funs.DB.SubmitChanges();


        }
        #endregion
        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpHandleMan.Items.Clear();
            Model.Check_IncentiveNotice incentiveNotice = BLL.IncentiveNoticeService.GetIncentiveNoticeById(this.IncentiveNoticeId);
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                this.GroupPanel2.Hidden = true;
                this.NoAgree.Hidden = false;
                if (State == "1")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = incentiveNotice.CompileMan;
                    this.drpHandleMan.Label = "打回编制人";
                }
                else if (State == "2")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = incentiveNotice.SignMan;
                    this.drpHandleMan.Label = "打回签发人";
                }
                this.drpHandleMan.Readonly = true;
            }
            else
            {

                this.NoAgree.Hidden = true;
                if (State == "1")
                {
                    BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//总包项目经理
                    this.drpHandleMan.Label = "总包项目经理";
                    this.GroupPanel2.Hidden = false;
                }
                else if (State == "2")
                {
                    BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, incentiveNotice.UnitId, true);//分包单位
                    this.drpHandleMan.Label = "施工分包单位";
                }
                this.drpHandleMan.SelectedIndex = 0;
                this.drpHandleMan.Readonly = false;
            }
        }
    }
}