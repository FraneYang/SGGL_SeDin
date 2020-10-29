using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class PauseNoticeEdit : PageBase
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.PauseNoticeId = Request.Params["PauseNoticeId"];
                this.InitDropDownList();
                if (!string.IsNullOrEmpty(PauseNoticeId))
                {
                    BindGrid();
                    this.hdPauseNoticeId.Text = PauseNoticeId;
                    Model.Check_PauseNotice pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
                    if (pauseNotice != null)
                    {
                        this.ProjectId = pauseNotice.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PauseNoticeId);
                        if (!string.IsNullOrEmpty(pauseNotice.UnitId))
                        {
                            this.drpUnit.SelectedValue = pauseNotice.UnitId;
                        }
                        this.txtUnitWorkName.Text = UnitWorkService.GetNameById(pauseNotice.UnitWorkId);
                        this.txtWrongContent.Text = pauseNotice.WrongContent;
                        if (pauseNotice.PauseTime.HasValue)
                        {
                            this.txtPauseTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", pauseNotice.PauseTime);
                        }
                        this.AttachUrl = pauseNotice.AttachUrl;
                        if (!string.IsNullOrEmpty(pauseNotice.PauseStates))
                        {
                            State = pauseNotice.PauseStates;
                        }
                        else
                        {
                            State = "0";
                        }
                        if (State == "1")
                        {
                            this.IsAgree.Hidden = false;
                            
                            UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                            this.drpHandleMan.Label = "总包项目经理";
                            if (!string.IsNullOrEmpty(pauseNotice.ApproveManId)) {
                                this.drpHandleMan.SelectedValue = pauseNotice.ApproveManId;
                            }
                            this.GroupPanel2.Hidden = false;
                            gvCarryUser.DataSource = BLL.UserService.GetProjectRoleUserListByProjectId(this.CurrUser.LoginProjectId, Const.UnitId_SEDIN);
                            gvCarryUser.DataBind();//专业工程师                            
                            BLL.UserService.InitUserProjectIdUnitIdRoleIdDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, Const.ConstructionManager, true);
                            //施工经理
                            UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadMan, this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, true);//分包单位
                            UserService.InitUserProjectIdUnitTypeDropDownList(this.drpSupervisorMan, this.CurrUser.LoginProjectId, Const.ProjectUnitType_3, true);//监理
                            UserService.InitUserProjectIdUnitTypeDropDownList(this.drpOwner, this.CurrUser.LoginProjectId, Const.ProjectUnitType_4, true);//业主

                        }
                        else if (State == "2")
                        {
                            this.IsAgree.Hidden = false;
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, pauseNotice.UnitId, true);//分包单位
                            this.drpHandleMan.Label = "施工分包单位";
                        }
                        else if (State == "3")
                        {
                            this.BackMan.Hidden = true;
                            this.ckAccept.Hidden = false;
                        }

                    }
                }


            }
        }
        #endregion

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
        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.ProjectId, Const.ProjectUnitType_2, true);
        }

        #region  单位变化事件
        /// <summary>
        /// 单位变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPauseNoticeMenuId, this.ProjectId, this.drpUnit.SelectedValue);
            }
            else
            {
                this.txtPauseNoticeCode.Text = string.Empty;
            }
        }
        #endregion
        
        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检单位！", MessageBoxIcon.Warning);
                return;
            }
            this.SavePauseNotice(BLL.Const.BtnSubmit);
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检单位！", MessageBoxIcon.Warning);
                return;
            }
            this.SavePauseNotice(BLL.Const.BtnSave);
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SavePauseNotice(string type)
        {
            string PauseStates = Convert.ToInt32(Convert.ToInt32(State) + 1).ToString();
            Model.Check_PauseNotice isUpdate = Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
            if (isUpdate!=null)
            {
                
                if (PauseStates == BLL.Const.State_2)
                {
                    /// 不同意 打回 同意抄送专业工程师、施工经理、项目经理 并下发分包接收人（也就是施工单位项目安全经理）
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        isUpdate.PauseStates = "0";

                    }
                    else
                    {
                        if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.ApproveManId = this.drpHandleMan.SelectedValue;
                        }
                        else
                        {
                            Alert.ShowInTop("总包项目经理不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        if (!string.IsNullOrWhiteSpace(String.Join(",", this.txtCarryUser.Values)))
                        {
                            isUpdate.ProfessionalEngineerId = string.Join(",", txtCarryUser.Values);
                        }
                        if (this.drpConstructionManager.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.ConstructionManagerId = this.drpConstructionManager.SelectedValue;
                        }
                        if (this.drpUnitHeadMan.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.UnitHeadManId = this.drpUnitHeadMan.SelectedValue;
                        }
                        if (this.drpSupervisorMan.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.SupervisorManId = this.drpSupervisorMan.SelectedValue;
                        }
                        if (this.drpOwner.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.OwnerId = this.drpOwner.SelectedValue;
                        }
                        isUpdate.IsConfirm = true;
                        isUpdate.SignDate = DateTime.Now;
                        isUpdate.PauseStates = "2";
                    }
                    Funs.DB.SubmitChanges();
                    SaveData("总包项目经理签发");

                }
                else if (PauseStates == BLL.Const.State_3)
                {
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        isUpdate.IsConfirm = false;
                        isUpdate.PauseStates = "1";
                    }
                    else
                    {
                        if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.DutyPersonId = this.drpHandleMan.SelectedValue;
                        }
                        else
                        {
                            Alert.ShowInTop("施工分包单位不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        isUpdate.ApproveDate = DateTime.Now;
                        
                        isUpdate.PauseStates = "3";
                    }
                    Funs.DB.SubmitChanges();
                    SaveData("施工分包单位批准");
                }
                else if (PauseStates == BLL.Const.State_4)
                {
                    isUpdate.States = "2";
                    isUpdate.DutyPersonDate = DateTime.Now;
                    SaveData("施工分包单位接受");
                    isUpdate.PauseStates = "4";
                    Funs.DB.SubmitChanges();

                    //// 回写专项检查明细表                            
                    var getcheck = Funs.DB.Check_CheckSpecialDetail.FirstOrDefault(x => x.DataId.Contains(isUpdate.PauseNoticeId));
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
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }
        #region 上传附件
        
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoticeUrl_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.hdPauseNoticeId.Text))
            {
                this.hdPauseNoticeId.Text = SQLHelper.GetNewID(typeof(Model.Check_PauseNotice));
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPauseNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, this.hdPauseNoticeId.Text)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, this.hdPauseNoticeId.Text)));
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
        public void SaveData(string OperateName)
        {
            Model.Check_PauseNoticeFlowOperate newFlowOperate = new Model.Check_PauseNoticeFlowOperate();
            newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PauseNoticeFlowOperate));
            newFlowOperate.PauseNoticeId = PauseNoticeId;
            newFlowOperate.OperateName = OperateName;
            newFlowOperate.OperateManId = CurrUser.UserId;
            newFlowOperate.OperateTime = DateTime.Now;
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                newFlowOperate.Opinion = this.reason.Text;
                newFlowOperate.IsAgree = false;
            }
            else {
                newFlowOperate.Opinion ="同意";
                newFlowOperate.IsAgree = true;
            }
            Funs.DB.Check_PauseNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
            Funs.DB.SubmitChanges();

        }
        #endregion

        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpHandleMan.Items.Clear();
            var pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                this.GroupPanel2.Hidden = true;
                this.NoAgree.Hidden = false;
                if (State == "1")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = pauseNotice.CompileManId;
                    this.drpHandleMan.Label = "打回编制人";
                }
                else if (State == "2")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = pauseNotice.SignManId;
                    this.drpHandleMan.Label = "打回签发人";
                   
                }
                this.drpHandleMan.Readonly = true;
            }
            else
            {
                
                this.NoAgree.Hidden = true;
                if (State == "1") {
                    UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                    this.drpHandleMan.Label = "总包项目经理";
                    this.GroupPanel2.Hidden = false;
                }
                else if (State == "2") {
                    BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, pauseNotice.UnitId, true);//分包单位
                    this.drpHandleMan.Label = "施工分包单位";
                }
                this.drpHandleMan.SelectedIndex = 0;
                this.drpHandleMan.Readonly = false;
            }
        }
    }
}