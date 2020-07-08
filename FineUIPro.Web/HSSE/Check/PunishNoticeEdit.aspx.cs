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
                    this.hdPunishNoticeId.Text = this.PunishNoticeId;
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
                        else
                        {
                            State = "0";
                        }
                        if (State == "0")///状态0 选择签发人
                        {
                            this.next.Hidden = false;
                            this.btnSave.Hidden = false;
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                            this.drpSignPerson.SelectedValue = punishNotice.SignMan;
                            btnAdd.Hidden = false;
                            GridColumn columndel = Grid1.FindColumn("del");
                            columndel.Hidden = false;
                            this.drpUnitId.Readonly = false;
                            this.drpPunishPersonId.Readonly = false;
                            this.txtBasicItem.Readonly = false;
                            this.txtPunishMoney.Readonly = false;
                            this.txtPunishNoticeDate.Readonly = false;
                            this.txtIncentiveReason.Readonly = false;
                            this.txtFileContents.Readonly = false;
                        }
                        if (State == "1")///状态1  签发人选择下一步批准人 并且发送抄送人员
                        {
                            this.IsAgree.Hidden = false;
                            this.next1.Hidden = false;
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpApproveMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//总包项目经理
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProfessionalEngineer, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//专业工程师
                            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//施工经理
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadMan, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//分包单位
                            ///禁用明细列表
                            var datas = Grid1.GetMergedData();
                            if (datas != null)
                            {
                                foreach (JObject mergedRow in Grid1.GetMergedData())
                                {
                                    int i = mergedRow.Value<int>("index");
                                    foreach (GridColumn column in Grid1.AllColumns)
                                    {
                                        Grid1.Rows[i].CellCssClasses[1] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[2] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[3] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                                    }
                                }
                            }

                        }
                        if (State == "2")///状态2 批准人选择下一步接收人
                        {
                            this.next2.Hidden = false;
                            this.IsAgree.Hidden = false;
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpDutyPerson, this.CurrUser.LoginProjectId, punishNotice.UnitId, true);//分包单位
                            ///禁用明细列表
                            var datas = Grid1.GetMergedData();
                            if (datas != null)
                            {
                                foreach (JObject mergedRow in Grid1.GetMergedData())
                                {
                                    int i = mergedRow.Value<int>("index");
                                    foreach (GridColumn column in Grid1.AllColumns)
                                    {
                                        Grid1.Rows[i].CellCssClasses[1] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[2] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[3] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                                    }
                                }
                            }
                        }
                        if (State == "3")
                        {
                            this.ckAccept.Hidden = false;
                            ///禁用明细列表
                            var datas = Grid1.GetMergedData();
                            if (datas != null)
                            {
                                foreach (JObject mergedRow in Grid1.GetMergedData())
                                {
                                    int i = mergedRow.Value<int>("index");
                                    foreach (GridColumn column in Grid1.AllColumns)
                                    {
                                        Grid1.Rows[i].CellCssClasses[1] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[2] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[3] = "f-grid-cell-uneditable";
                                        Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectPunishNoticeMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                    ////自动生成编码
                    this.txtPunishNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPunishNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
                    State = "0";
                    this.next.Hidden = false;
                    this.btnSave.Hidden = false;
                    btnAdd.Hidden = false;
                    GridColumn columndel = Grid1.FindColumn("del");
                    columndel.Hidden = false;
                    this.drpUnitId.Readonly = false;
                    this.drpPunishPersonId.Readonly = false;
                    this.txtBasicItem.Readonly = false;
                    this.txtPunishMoney.Readonly = false;
                    this.txtPunishNoticeDate.Readonly = false;
                    this.txtIncentiveReason.Readonly = false;
                    this.txtFileContents.Readonly = false;
                }
            }
            else
            {
                if (GetRequestEventArgument() == "UPDATE_SUMMARY")
                {
                    // 页面要求计算总金额的值
                    OutputSummaryData();
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
            string strSql = @"select FlowOperateId, PunishNoticeId, OperateName, OperateManId, OperateTime, IsAgree, Opinion,S.UserName from Check_PunishNoticeFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
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

        #region 合计金额
        private void OutputSummaryData()
        {
            decimal TotalMoney = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (!string.IsNullOrEmpty(values["PunishMoney"].ToString()))
                {
                    TotalMoney += values.Value<decimal>("PunishMoney");
                }
            }
            this.txtPunishMoney.Text = TotalMoney.ToString();
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, false);
        }

        #region  获取大写金额事件
        /// <summary>
        /// 获取大写金额事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPunishMoney_Blur(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPunishMoney.Text))
            {
                this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));
            }
            else
            {
                this.txtBig.Text = string.Empty;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 回执单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdPunishNoticeId.Text))
            {
                this.hdPunishNoticeId.Text = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNotice&menuId=" + Const.ProjectPunishNoticeMenuId, this.hdPunishNoticeId.Text)));
        }

        /// <summary>
        /// 通知单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPunishNoticeUrl_Click(object sender, EventArgs e)
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPunishNoticeMenuId);
            if (string.IsNullOrEmpty(this.hdPunishNoticeId.Text))
            {
                this.hdPunishNoticeId.Text = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
            }
            if (buttonList.Count() > 0)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.hdPunishNoticeId.Text)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.hdPunishNoticeId.Text)));
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
            if (this.drpUnitId.SelectedValue==BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受罚单位", MessageBoxIcon.Warning);
                return;
            }

            Model.Check_PunishNotice punishNotice = new Model.Check_PunishNotice
            {
                ProjectId = this.ProjectId,
                PunishNoticeCode = this.txtPunishNoticeCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue!=BLL.Const._Null)
            {
                punishNotice.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpPunishPersonId.SelectedValue!=BLL.Const._Null)
            {
                punishNotice.PunishPersonId = this.drpPunishPersonId.SelectedValue;
            }
            punishNotice.PunishNoticeDate = Funs.GetNewDateTime(this.txtPunishNoticeDate.Text.Trim());
            punishNotice.IncentiveReason = this.txtIncentiveReason.Text.Trim();
            punishNotice.BasicItem = this.txtBasicItem.Text.Trim();
            punishNotice.PunishMoney = Funs.GetNewDecimalOrZero(this.txtPunishMoney.Text.Trim());
            punishNotice.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            punishNotice.CompileMan = this.CurrUser.UserId;
            punishNotice.CompileDate = DateTime.Now;
            punishNotice.States = Const.State_0;
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                punishNotice.SignMan = this.drpSignPerson.SelectedValue;
            }
            punishNotice.Currency = this.txtCurrency.Text.Trim();
            var getUpdate = new Model.SGGLDB(Funs.ConnString).Check_PunishNotice.FirstOrDefault(x => x.PunishNoticeId == PunishNoticeId);
            if (type == BLL.Const.BtnSubmit)
            {
                punishNotice.PunishStates = Convert.ToInt32(Convert.ToInt32(State) + 1).ToString();
            }
            else
            {
                if (getUpdate != null)
                {
                    if (string.IsNullOrEmpty(getUpdate.PunishStates))
                    {
                        punishNotice.PunishStates = "0";
                    }
                    else
                    {
                        punishNotice.PunishStates = getUpdate.PunishStates;
                    }
                }
                else
                {
                    punishNotice.PunishStates = "0";
                }
            }
            //没有就新增一条处罚单
            if (getUpdate == null)
            {
                if (string.IsNullOrEmpty(this.hdPunishNoticeId.Text))
                {
                    punishNotice.PunishNoticeId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
                }
                else {
                    punishNotice.PunishNoticeId = this.hdPunishNoticeId.Text;
                }
                new Model.SGGLDB(Funs.ConnString).Check_PunishNotice.InsertOnSubmit(punishNotice);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectPunishNoticeMenuId, punishNotice.ProjectId, punishNotice.UnitId, punishNotice.PunishNoticeId, punishNotice.CompileDate);
                this.PunishNoticeId = punishNotice.PunishNoticeId;
                saveNoticesItemDetail();
                SaveOperate("总包安全工程师下发处罚单", 1);
            }
            else
            {
                ///根据状态编辑页面信息
                if (punishNotice.PunishStates == BLL.Const.State_0 || punishNotice.PunishStates == BLL.Const.State_1)////编制人 修改或提交
                {
                    if (this.drpUnitId.SelectedValue!=BLL.Const._Null)
                    {
                        getUpdate.UnitId = this.drpUnitId.SelectedValue;
                    }
                    getUpdate.PunishNoticeDate = Funs.GetNewDateTime(this.txtPunishNoticeDate.Text.Trim());
                    getUpdate.IncentiveReason = this.txtIncentiveReason.Text.Trim();
                    getUpdate.BasicItem = this.txtBasicItem.Text.Trim();
                    getUpdate.PunishMoney = Funs.GetNewDecimalOrZero(this.txtPunishMoney.Text.Trim());
                    getUpdate.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
                    getUpdate.CompileMan = this.CurrUser.UserId;
                    getUpdate.CompileDate = DateTime.Now;
                    if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
                    {
                        getUpdate.SignMan = this.drpSignPerson.SelectedValue;
                    }
                    getUpdate.Currency = this.txtCurrency.Text.Trim();
                    if (punishNotice.PunishStates == BLL.Const.State_1)
                    {
                        getUpdate.PunishStates = "1";
                        SaveOperate("总包安全工程师重新下发处罚单", 1);
                    }
                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                    saveNoticesItemDetail();
                }
                else if (punishNotice.PunishStates == BLL.Const.State_2) ////【签发】总包安全经理
                {
                    /// 不同意 打回 同意抄送专业工程师、施工经理、相关施工分包单位并提交【批准】总包项目经理
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        getUpdate.PunishStates = "0";
                        SaveOperate("总包安全经理签发", 0);
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
                        if (drpApproveMan.SelectedValue != BLL.Const._Null)
                        {
                            getUpdate.ApproveMan = drpApproveMan.SelectedValue;
                            getUpdate.SignDate = DateTime.Now;
                            getUpdate.PunishStates = "2";
                        }
                        else
                        {
                            Alert.ShowInTop("总包项目经理不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        SaveOperate("总包安全经理签发", 1);
                    }

                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();

                }
                else if (punishNotice.PunishStates == BLL.Const.State_3) ////【批准】总包项目经理
                {
                    /// 不同意 打回 同意下发【回执】施工分包单位
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        getUpdate.PunishStates = "1";
                        SaveOperate("总包项目经理经理批准", 0);
                    }
                    else
                    {
                        if (this.drpDutyPerson.SelectedValue != BLL.Const._Null)
                        {
                            getUpdate.DutyPersonId = this.drpDutyPerson.SelectedValue;
                            getUpdate.ApproveDate = DateTime.Now;
                            getUpdate.PunishStates = "3";
                        }
                        else
                        {
                            Alert.ShowInTop("施工分包单位不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        SaveOperate("总包项目经理经理批准", 1);
                    }
                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                }
                else if (punishNotice.PunishStates == BLL.Const.State_4)
                {
                    Model.AttachFile sour = new Model.AttachFile();
                    sour = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == getUpdate.PunishNoticeId && x.MenuId == Const.ProjectPunishNoticeMenuId);
                    if (sour != null)
                    {
                        getUpdate.DutyPersonDate = DateTime.Now;
                        getUpdate.States = Const.State_2;
                        getUpdate.PunishStates = "4";
                        new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                        SaveOperate("施工分包单位回执", 1);
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
        public void SaveOperate(string OperateName, int flag)
        {
            if (flag == 1)
            {
                Model.Check_PunishNoticeFlowOperate newFlowOperate = new Model.Check_PunishNoticeFlowOperate
                {
                    FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PunishNoticeFlowOperate)),
                    PunishNoticeId = PunishNoticeId,
                    OperateName = OperateName,
                    OperateManId = CurrUser.UserId,
                    OperateTime = DateTime.Now,
                    IsAgree = true
                };
                new Model.SGGLDB(Funs.ConnString).Check_PunishNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
            else
            {
                Model.Check_PunishNoticeFlowOperate newFlowOperate = new Model.Check_PunishNoticeFlowOperate
                {
                    FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PunishNoticeFlowOperate)),
                    PunishNoticeId = PunishNoticeId,
                    OperateName = OperateName,
                    OperateManId = CurrUser.UserId,
                    OperateTime = DateTime.Now,
                    Opinion = this.reason.Text,
                    IsAgree = false
                };
                new Model.SGGLDB(Funs.ConnString).Check_PunishNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }

        }
        #endregion

        #endregion

        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                Model.Check_PunishNotice PunishNotice = BLL.PunishNoticeService.GetPunishNoticeById(PunishNoticeId);
                this.NoAgree.Hidden = false;
                this.HandleType.Hidden = true;
                if (State == "1")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = PunishNotice.CompileMan;
                }
                else if (State == "2")
                {
                    BLL.UserService.InitUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                    this.drpHandleMan.SelectedValue = PunishNotice.SignMan;
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addPunishNoticeList();
            Model.Check_PunishNoticeItem notice = new Model.Check_PunishNoticeItem();
            notice.PunishNoticeItemId = SQLHelper.GetNewID(typeof(Model.Check_PunishNoticeItem));
            viewPunishNoticeList.Add(notice);
            //将gd数据保存在list中
            Grid1.DataSource = viewPunishNoticeList;
            Grid1.DataBind();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "delete")
            {
                viewPunishNoticeList.Remove(viewPunishNoticeList.FirstOrDefault(p => p.PunishNoticeItemId == itemId));
                Grid1.DataSource = viewPunishNoticeList;
                Grid1.DataBind();
            }
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
        }
        private void addPunishNoticeList()
        {
            viewPunishNoticeList.Clear();
            var data = Grid1.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string PunishNoticeItemId = values.Value<string>("PunishNoticeItemId");
                    string PunishContent = values.Value<string>("PunishContent");
                    string PunishMoney = values.Value<string>("PunishMoney");
                    var item = new Model.Check_PunishNoticeItem();
                    item.PunishNoticeItemId = PunishNoticeItemId;
                    item.PunishNoticeId = PunishNoticeId;
                    item.PunishContent = PunishContent;
                    item.PunishMoney = Funs.GetNewDecimal(PunishMoney);
                    viewPunishNoticeList.Add(item);
                }
                //item.RectifyResults = Grid1.Rows[i].Values[3].ToString()

            }


        }
        /// <summary>
        /// 保存处罚单明细
        /// </summary>
        public void saveNoticesItemDetail()
        {
            var data = Grid1.GetMergedData();
            if (data != null)
            {

                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string PunishNoticeItemId = values.Value<string>("PunishNoticeItemId");
                    string PunishContent = values.Value<string>("PunishContent");
                    string PunishMoney = values.Value<string>("PunishMoney");
                    AspNet.Label lblNumber = (AspNet.Label)Grid1.Rows[i].FindControl("lblNumber");
                    string SortIndex = lblNumber.Text.Trim();
                    Model.Check_PunishNoticeItem PunishNoticeItem = new Model.SGGLDB(Funs.ConnString).Check_PunishNoticeItem.FirstOrDefault(e => e.PunishNoticeItemId == PunishNoticeItemId);
                    if (PunishNoticeItem != null)
                    {
                        PunishNoticeItem.PunishNoticeItemId = PunishNoticeItemId;
                        PunishNoticeItem.PunishNoticeId = PunishNoticeId;
                        PunishNoticeItem.PunishContent = PunishContent;
                        PunishNoticeItem.PunishMoney = decimal.Round(Funs.GetNewDecimalOrZero(PunishMoney),2);
                        PunishNoticeItem.SortIndex = Funs.GetNewInt(SortIndex);
                        new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                    }
                    else
                    {

                        var item = new Model.Check_PunishNoticeItem();
                        item.PunishNoticeItemId = PunishNoticeItemId;
                        item.PunishNoticeId = PunishNoticeId;
                        item.PunishContent = PunishContent;
                        item.PunishMoney = decimal.Round(Funs.GetNewDecimalOrZero(PunishMoney),2);
                        item.SortIndex = Funs.GetNewInt(SortIndex);
                        new Model.SGGLDB(Funs.ConnString).Check_PunishNoticeItem.InsertOnSubmit(item);
                        new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                    }
                }

            }
        }
        /// <summary>
        /// 根据受罚单位定位受罚人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpPunishPersonId, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//分包单位
                this.drpPunishPersonId.SelectedIndex = 0;
            }
            else
            {
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpPunishPersonId, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
            }
        }
    }
}