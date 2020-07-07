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
                        this.txtProjectPlace.Text = pauseNotice.ProjectPlace;
                        this.txtWrongContent.Text = pauseNotice.WrongContent;
                        if (pauseNotice.PauseTime.HasValue)
                        {
                            this.txtPauseTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", pauseNotice.PauseTime);
                        }
                        this.AttachUrl = pauseNotice.AttachUrl;
                        //this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
                        if (!string.IsNullOrEmpty(pauseNotice.PauseStates))
                        {
                            State = pauseNotice.PauseStates;
                        }
                        else
                        {
                            State = "0";
                        }
                        if (State == "0")
                        {
                            
                            this.next.Hidden = false;
                            this.btnSave.Hidden = false;
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId,Const.UnitId_SEDIN, true);
                            this.drpSignPerson.SelectedValue = pauseNotice.SignManId;
                            this.drpUnit.Readonly = false;
                            this.txtProjectPlace.Readonly = false;
                            this.txtWrongContent.Readonly = false;
                            this.txtPauseTime.Readonly = false;
                        }
                        else if (State == "1")
                        {
                            this.IsAgree.Hidden = false;
                            this.next1.Hidden = false;
                            UserService.InitFlowOperateControlUserDropDownList(this.drpApproveMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//总包项目经理
                            UserService.InitFlowOperateControlUserDropDownList(this.drpProfessionalEngineer, this.CurrUser.LoginProjectId,Const.UnitId_SEDIN, true);//专业工程师
                            UserService.InitFlowOperateControlUserDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId,Const.UnitId_SEDIN, true);//施工经理
                            UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadMan, this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, true);//分包单位
                            UserService.InitUserProjectIdUnitTypeDropDownList(this.drpSupervisorMan, this.CurrUser.LoginProjectId, Const.ProjectUnitType_3, true);//监理
                            UserService.InitUserProjectIdUnitTypeDropDownList(this.drpOwner, this.CurrUser.LoginProjectId, Const.ProjectUnitType_4, true);//业主

                        }
                        else if (State == "2")
                        {
                            this.next2.Hidden = false;
                            this.IsAgree.Hidden = false;
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpDutyPerson, this.CurrUser.LoginProjectId, pauseNotice.UnitId, true);//分包单位
                        }
                        else if (State == "3")
                        {
                            this.ckAccept.Hidden = false;
                        }

                    }
                }
                else
                {
                    
                    State = "0";
                    this.next.Hidden = false;
                    this.btnSave.Hidden = false;
                    BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId,Const.UnitId_SEDIN, true);
                    this.drpUnit.Readonly = false;
                    this.txtProjectPlace.Readonly = false;
                    this.txtWrongContent.Readonly = false;
                    this.txtPauseTime.Readonly = false;
                }


            }
        }
        #endregion

        public void BindGrid()
        {
            string strSql = @"select FlowOperateId, PauseNoticeId, OperateName, OperateManId, OperateTime, IsAgree, Opinion,S.UserName from Check_PauseNoticeFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
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
            Model.Check_PauseNotice pauseNotice = new Model.Check_PauseNotice
            {
                PauseNoticeCode = this.txtPauseNoticeCode.Text.Trim(),
                ProjectId = this.ProjectId,

                UnitId = this.drpUnit.SelectedValue,
                ProjectPlace = this.txtProjectPlace.Text.Trim(),
                WrongContent = this.txtWrongContent.Text.Trim(),
                PauseTime = Funs.GetNewDateTime(this.txtPauseTime.Text.Trim())
            };
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                pauseNotice.SignManId = this.drpSignPerson.SelectedValue;
            }
            pauseNotice.IsConfirm = false;
            pauseNotice.AttachUrl = this.AttachUrl;
            if (type == BLL.Const.BtnSubmit)
            {
                //pauseNotice.PauseStates = this.drpHandleType.SelectedValue;
                pauseNotice.PauseStates = Convert.ToInt32(Convert.ToInt32(State) + 1).ToString();
            }
            else
            {
                var isUpdate = Funs.DB.Check_PauseNotice.FirstOrDefault(x => x.PauseNoticeId == PauseNoticeId);
                if (isUpdate != null)
                {
                    if (string.IsNullOrEmpty(isUpdate.PauseStates))
                    {
                        pauseNotice.PauseStates = "0";
                    }
                    else
                    {
                        pauseNotice.PauseStates = isUpdate.PauseStates;
                    }
                }
                else
                {
                    pauseNotice.PauseStates = "0";
                }
            }
            if (!string.IsNullOrEmpty(this.PauseNoticeId))
            {
                Model.Check_PauseNotice isUpdate = Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
                if (type == BLL.Const.BtnSubmit)
                {

                    //isUpdate.PauseStates = this.drpHandleType.SelectedValue;
                    if (pauseNotice.PauseStates == BLL.Const.State_0 || pauseNotice.PauseStates == BLL.Const.State_1)
                    {
                        isUpdate.UnitId = this.drpUnit.SelectedValue;
                        isUpdate.ProjectPlace = this.txtProjectPlace.Text.Trim();
                        isUpdate.WrongContent = this.txtWrongContent.Text.Trim();
                        if (!string.IsNullOrEmpty(this.txtPauseTime.Text.Trim()))
                        {
                            isUpdate.PauseTime = Funs.GetNewDateTimeOrNow(this.txtPauseTime.Text.Trim());
                        }
                        if (pauseNotice.PauseStates == BLL.Const.State_1 && !string.IsNullOrEmpty(pauseNotice.SignManId))
                        {
                            isUpdate.SignManId = pauseNotice.SignManId;
                            isUpdate.PauseStates = "1";

                        }
                        BLL.Funs.DB.SubmitChanges();
                    }
                    else if (pauseNotice.PauseStates == BLL.Const.State_2)
                    {
                        /// 不同意 打回 同意抄送专业工程师、施工经理、项目经理 并下发分包接收人（也就是施工单位项目安全经理）
                        if (this.rdbIsAgree.SelectedValue.Equals("false"))
                        {
                            isUpdate.PauseStates = "0";

                        }
                        else
                        {
                            if (this.drpApproveMan.SelectedValue != BLL.Const._Null)
                            {
                                isUpdate.ApproveManId = this.drpApproveMan.SelectedValue;
                            }
                            else
                            {
                                Alert.ShowInTop("总包项目经理不能为空！", MessageBoxIcon.Warning);
                                return;
                            }
                            if (this.drpProfessionalEngineer.SelectedValue != BLL.Const._Null)
                            {
                                isUpdate.ProfessionalEngineerId = this.drpProfessionalEngineer.SelectedValue;
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
                        BLL.Funs.DB.SubmitChanges();
                        SaveData("总包施工经理签发", 1);

                    }
                    else if (pauseNotice.PauseStates == BLL.Const.State_3)
                    {
                        if (this.rdbIsAgree.SelectedValue.Equals("false"))
                        {
                            isUpdate.IsConfirm = false;
                            isUpdate.PauseStates = "1";
                            SaveData("总包项目经理批准", 0);
                        }
                        else
                        {
                            isUpdate.DutyPersonId = this.drpDutyPerson.SelectedValue;
                            isUpdate.ApproveDate = DateTime.Now;
                            SaveData("总包项目经理批准", 1);
                            isUpdate.PauseStates ="3";
                        }
                        BLL.Funs.DB.SubmitChanges();
                    }
                    else if (pauseNotice.PauseStates == BLL.Const.State_4)
                    {
                            isUpdate.States = "2";
                            isUpdate.DutyPersonDate = DateTime.Now;
                            SaveData("施工分包单位接受", 1);
                            isUpdate.PauseStates = "4";
                        BLL.Funs.DB.SubmitChanges();
                    }

                }
            }
            else
            {
                pauseNotice.States = "0";
                if (string.IsNullOrEmpty(this.hdPauseNoticeId.Text))
                {
                    pauseNotice.PauseNoticeId = SQLHelper.GetNewID(typeof(Model.Check_PauseNotice));
                }
                else {
                    pauseNotice.PauseNoticeId = this.hdPauseNoticeId.Text;
                }
                pauseNotice.CompileManId = this.CurrUser.UserId;
                pauseNotice.CompileDate = DateTime.Now;
                if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
                {
                    pauseNotice.SignManId = drpSignPerson.SelectedValue;
                }
                else
                {
                    Alert.ShowInTop("签发人不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                BLL.Check_PauseNoticeService.AddPauseNotice(pauseNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, pauseNotice.PauseNoticeCode, pauseNotice.PauseNoticeId, BLL.Const.ProjectPauseNoticeMenuId, BLL.Const.BtnAdd);
                this.PauseNoticeId = pauseNotice.PauseNoticeId;


                SaveData("总包安全工程师/安全经理下发暂停令", 1);


            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }

        /// <summary>
        /// 
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

        #region 保存流程审核数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void SaveData(string OperateName, int flag)
        {
            if (flag == 1)
            {
                Model.Check_PauseNoticeFlowOperate newFlowOperate = new Model.Check_PauseNoticeFlowOperate
                {
                    FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PauseNoticeFlowOperate)),
                    PauseNoticeId = PauseNoticeId,
                    OperateName = OperateName,
                    OperateManId = CurrUser.UserId,
                    OperateTime = DateTime.Now,
                    IsAgree = true
                };
                BLL.Funs.DB.Check_PauseNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
                BLL.Funs.DB.SubmitChanges();
            }
            else
            {
                Model.Check_PauseNoticeFlowOperate newFlowOperate = new Model.Check_PauseNoticeFlowOperate
                {
                    FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PauseNoticeFlowOperate)),
                    PauseNoticeId = PauseNoticeId,
                    OperateName = OperateName,
                    OperateManId = CurrUser.UserId,
                    OperateTime = DateTime.Now,
                    Opinion = this.reason.Text,
                    IsAgree = false
                };
                BLL.Funs.DB.Check_PauseNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
                BLL.Funs.DB.SubmitChanges();
            }

        }
        #endregion

        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                Model.Check_PauseNotice pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
                this.NoAgree.Hidden = false;
                this.HandleType.Hidden = true;
                if (State == "1")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = pauseNotice.CompileManId;
                }
                else if (State == "2")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = pauseNotice.SignManId;
                }
                this.BackMan.Hidden = false;
            }
            else
            {
                this.NoAgree.Hidden = true;
                this.HandleType.Hidden = false;
                this.BackMan.Hidden = true;

            }
        }
    }
}