using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class PunishNoticeEdit : PageBase
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
        ///// <summary>
        ///// 附件
        ///// </summary>
        //private string AttchUrl
        //{
        //    get
        //    {
        //        return (string)ViewState["AttchUrl"];
        //    }
        //    set
        //    {
        //        ViewState["AttchUrl"] = value;
        //    }
        //}

        #endregion
        public static List<Model.Check_PunishNoticeItem> viewPunishNoticeList = new List<Model.Check_PunishNoticeItem>();
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

                this.InitDropDownList();
                this.PunishNoticeId = Request.Params["PunishNoticeId"];
                this.txtCurrency.Text = "人民币";
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpPunishPersonId, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                if (!string.IsNullOrEmpty(this.PunishNoticeId))
                {
                    BindGrid();
                    BindGrid1();
                    Model.Check_PunishNotice punishNotice = BLL.PunishNoticeService.GetPunishNoticeById(this.PunishNoticeId);
                    if (punishNotice != null)
                    {
                        this.ProjectId = punishNotice.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPunishNoticeCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PunishNoticeId);
                        this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", punishNotice.PunishNoticeDate);
                        if (!string.IsNullOrEmpty(punishNotice.UnitId))
                        {
                            this.drpUnitId.SelectedValue = punishNotice.UnitId;
                        }
                        if (!string.IsNullOrEmpty(punishNotice.PunishPersonId))
                        {
                            this.drpPunishPersonId.SelectedValue = punishNotice.PunishPersonId;
                        }
                        this.txtIncentiveReason.Text = punishNotice.IncentiveReason;
                        this.txtBasicItem.Text = punishNotice.BasicItem;
                        if (punishNotice.PunishMoney.HasValue)
                        {
                            this.txtPunishMoney.Text = Convert.ToString(punishNotice.PunishMoney);
                            this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));
                        }

                        this.txtFileContents.Text = HttpUtility.HtmlDecode(punishNotice.FileContents);

                        if (!string.IsNullOrEmpty(punishNotice.Currency))
                        {
                            this.txtCurrency.Text = punishNotice.Currency;
                        }
                        if (!string.IsNullOrEmpty(punishNotice.PunishStates))
                        {
                            State = punishNotice.PunishStates;
                        }
                        if (State == "1")///状态1  签发人选择下一步批准人 并且发送抄送人员
                        {
                            this.IsAgree.Hidden = false;
                            this.GroupPanel2.Hidden = false;
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//总包项目经理
                            this.drpHandleMan.Label = "总包项目经理";
                            if (!string.IsNullOrEmpty(punishNotice.ApproveMan))
                            {
                                this.drpHandleMan.SelectedValue = punishNotice.ApproveMan;
                            }
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProfessionalEngineer, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//专业工程师
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//施工经理
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadMan, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//分包单位

                        }
                        if (State == "2")///状态2 批准人选择下一步接收人
                        {
                            this.IsAgree.Hidden = false;
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, punishNotice.UnitId, true);//分包单位
                            this.drpHandleMan.Label = "施工分包单位";
                        }
                        if (State == "3")
                        {
                            this.ckAccept.Hidden = false;
                            this.drpHandleMan.Hidden = true;
                        }
                    }
                }
            }
        }
        public void BindGrid()
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
        public void BindGrid1()
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
        

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, false);
        }
        

        #region 附件上传
        /// <summary>
        /// 回执单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNotice&menuId=" + Const.ProjectPunishNoticeMenuId, this.PunishNoticeId)));
        }

        /// <summary>
        /// 通知单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPunishNoticeUrl_Click(object sender, EventArgs e)
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPunishNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
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
            this.SaveData(BLL.Const.BtnSave);
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {

            var getUpdate = BLL.PunishNoticeService.GetPunishNoticeById(PunishNoticeId); 
            string PunishStates = Convert.ToInt32(Convert.ToInt32(State) + 1).ToString();
            if (getUpdate != null)
            {
                 if (PunishStates == BLL.Const.State_2) ////【签发】总包安全经理
                {
                    /// 不同意 打回 同意抄送专业工程师、施工经理、相关施工分包单位并提交【批准】总包项目经理
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        getUpdate.PunishStates = "0";
                        getUpdate.SginOpinion = reason.Text;
                    }
                    else
                    {
                        if (drpProfessionalEngineer.SelectedValue != BLL.Const._Null)
                        {
                            getUpdate.ProfessionalEngineerId = drpProfessionalEngineer.SelectedValue;
                        }
                        if (drpConstructionManager.SelectedValue != BLL.Const._Null)
                        { getUpdate.ConstructionManagerId = drpConstructionManager.SelectedValue; }
                        if (drpUnitHeadMan.SelectedValue != BLL.Const._Null)
                        {
                            getUpdate.UnitHeadManId = drpUnitHeadMan.SelectedValue;
                        }
                        if (drpHandleMan.SelectedValue != BLL.Const._Null)
                        {
                            getUpdate.ApproveMan = drpHandleMan.SelectedValue;
                            getUpdate.SignDate = DateTime.Now;
                            getUpdate.PunishStates = "2";
                        }
                        else
                        {
                            Alert.ShowInTop("总包项目经理不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                       
                        getUpdate.SginOpinion = "同意";
                    }
                    SaveOperate("总包安全经理签发");
                    Funs.DB.SubmitChanges();

                }
                else if (PunishStates == BLL.Const.State_3) ////【批准】总包项目经理
                {
                    /// 不同意 打回 同意下发【回执】施工分包单位
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        getUpdate.PunishStates = "1";
                        getUpdate.ApproveOpinion = reason.Text;
                    }
                    else
                    {
                        if (this.drpHandleMan.SelectedValue != BLL.Const._Null)
                        {
                            getUpdate.DutyPersonId = this.drpHandleMan.SelectedValue;
                            getUpdate.ApproveDate = DateTime.Now;
                            getUpdate.PunishStates = "3";
                        }
                        else
                        {
                            Alert.ShowInTop("施工分包单位不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        
                        getUpdate.ApproveOpinion = "同意";
                    }
                    Funs.DB.SubmitChanges();
                    SaveOperate("总包项目经理经理批准");
                }
                else if (PunishStates == BLL.Const.State_4)
                {
                    var sour = BLL.AttachFileService.GetAttachFile(getUpdate.PunishNoticeId, Const.ProjectPunishNoticeMenuId); 
                    if (sour != null)
                    {
                        getUpdate.DutyPersonDate = DateTime.Now;
                        getUpdate.States = Const.State_2;
                        getUpdate.PunishStates = "4";
                        Funs.DB.SubmitChanges();
                        SaveOperate("施工分包单位回执");

                        //// 回写专项检查明细表                            
                        var getcheck = Funs.DB.Check_CheckSpecialDetail.FirstOrDefault(x => x.DataId.Contains(getUpdate.PunishNoticeId));
                        if (getcheck != null)
                        {
                            getcheck.CompleteStatus = true;
                            getcheck.CompletedDate = DateTime.Now;
                            Funs.DB.SubmitChanges();
                            //// 根据明细ID判断是否全部整改完成 并更新专项检查状态
                            Check_CheckSpecialService.UpdateCheckSpecialStates(getcheck.CheckSpecialId);
                        }
                    }
                    else
                    {

                        Alert.ShowInTop("请上传回执单", MessageBoxIcon.Warning);
                        return;
                    }

                }

            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            Model.Check_PunishNoticeFlowOperate newFlowOperate = new Model.Check_PunishNoticeFlowOperate();
            newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PunishNoticeFlowOperate));
            newFlowOperate.PunishNoticeId = PunishNoticeId;
            newFlowOperate.OperateName = OperateName;
            newFlowOperate.OperateManId = CurrUser.UserId;
            newFlowOperate.OperateTime = DateTime.Now;
            if (this.rdbIsAgree.SelectedValue.Equals("false"))
            {
                newFlowOperate.IsAgree = false;
                newFlowOperate.Opinion = this.reason.Text;
            }
            else {
                newFlowOperate.IsAgree = true;
                newFlowOperate.Opinion = "同意";
            }
                Funs.DB.Check_PunishNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
                Funs.DB.SubmitChanges();
           

        }
        #endregion

        #endregion

        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpHandleMan.Items.Clear();
            Model.Check_PunishNotice PunishNotice = BLL.PunishNoticeService.GetPunishNoticeById(PunishNoticeId);
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                this.GroupPanel2.Hidden = true;
                this.NoAgree.Hidden = false;
                if (State == "1")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = PunishNotice.CompileMan;
                    this.drpHandleMan.Label = "打回编制人";
                }
                else if (State == "2")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = PunishNotice.SignMan;
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
                    BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId, PunishNotice.UnitId, true);//分包单位
                    this.drpHandleMan.Label = "施工分包单位";
                }
                this.drpHandleMan.SelectedIndex = 0;
                this.drpHandleMan.Readonly = false;
            }
        }
        
    }
}