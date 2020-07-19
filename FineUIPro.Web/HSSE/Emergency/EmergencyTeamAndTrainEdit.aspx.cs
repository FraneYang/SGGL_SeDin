using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencyTeamAndTrainEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FileId
        {
            get
            {
                return (string)ViewState["FileId"];
            }
            set
            {
                ViewState["FileId"] = value;
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
        #endregion

        public List<Model.View_Emergency_EmergencyTeamItem> viewEmergencyTeamItemList;

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
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain = BLL.EmergencyTeamAndTrainService.GetEmergencyTeamAndTrainById(this.FileId);
                    if (EmergencyTeamAndTrain != null)
                    {
                        this.ProjectId = EmergencyTeamAndTrain.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = EmergencyTeamAndTrain.FileName;

                        if (!string.IsNullOrEmpty(EmergencyTeamAndTrain.UnitId))
                        {
                            this.drpUnit.SelectedValue = EmergencyTeamAndTrain.UnitId;
                            this.getDrpPerson();
                        }
                        if (!string.IsNullOrEmpty(EmergencyTeamAndTrain.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = EmergencyTeamAndTrain.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", EmergencyTeamAndTrain.CompileDate);
                        //    this.txtFileContent.Text = HttpUtility.HtmlDecode(EmergencyTeamAndTrain.FileContent);
                        viewEmergencyTeamItemList = (from x in Funs.DB.View_Emergency_EmergencyTeamItem
                                                     where x.FileId == this.FileId
                                                     select x).ToList();
                        Grid1.DataSource = viewEmergencyTeamItemList;
                        Grid1.DataBind();
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var FileCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectEmergencyTeamAndTrainMenuId, this.ProjectId);
                    //if (FileCodeTemplateRule != null)
                    //{
                    //    this.txtFileContent.Text = HttpUtility.HtmlDecode(FileCodeTemplateRule.Template);
                    //}

                    ////自动生成编码
                    this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectEmergencyTeamAndTrainMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtFileName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEmergencyTeamAndTrainMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
            this.getDrpPerson();
            UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
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
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
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
            Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain = new Model.Emergency_EmergencyTeamAndTrain
            {
                ProjectId = this.ProjectId,
                FileCode = this.txtFileCode.Text.Trim(),
                FileName = this.txtFileName.Text.Trim()
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                EmergencyTeamAndTrain.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                EmergencyTeamAndTrain.CompileMan = this.drpCompileMan.SelectedValue;
            }
            EmergencyTeamAndTrain.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            //EmergencyTeamAndTrain.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            ////单据状态
            EmergencyTeamAndTrain.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                EmergencyTeamAndTrain.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.FileId))
            {
                EmergencyTeamAndTrain.FileId = this.FileId;
                EmergencyTeamAndTrainService.UpdateEmergencyTeamAndTrain(EmergencyTeamAndTrain);
                LogService.AddSys_Log(this.CurrUser, EmergencyTeamAndTrain.FileCode, EmergencyTeamAndTrain.FileId, BLL.Const.ProjectEmergencyTeamAndTrainMenuId, BLL.Const.BtnModify);
                EmergencyTeamAndTrainService.DeleteEmergency_EmergencyTeamItem(this.FileId);
            }
            else
            {
                this.FileId = SQLHelper.GetNewID(typeof(Model.Emergency_EmergencyTeamAndTrain));
                EmergencyTeamAndTrain.FileId = this.FileId;
                BLL.EmergencyTeamAndTrainService.AddEmergencyTeamAndTrain(EmergencyTeamAndTrain);
                BLL.LogService.AddSys_Log(this.CurrUser, EmergencyTeamAndTrain.FileCode, EmergencyTeamAndTrain.FileId, BLL.Const.ProjectEmergencyTeamAndTrainMenuId, BLL.Const.BtnAdd);
            }

            var getViewList = this.CollectGridInfo();
            var getItems = from x in getViewList
                                       select new Model.Emergency_EmergencyTeamItem
                                       {
                                           EmergencyTeamItemId = x.EmergencyTeamItemId,
                                           FileId = this.FileId,
                                           PersonId = x.PersonId,
                                           Job = x.Job,
                                           Tel = x.Tel,
                                       };
            if (getItems.Count() > 0)
            {
                Funs.DB.Emergency_EmergencyTeamItem.InsertAllOnSubmit(getItems);
                Funs.DB.SubmitChanges();
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectEmergencyTeamAndTrainMenuId, this.FileId, (type == BLL.Const.BtnSubmit ? true : false), EmergencyTeamAndTrain.FileName, "../Emergency/EmergencyTeamAndTrainView.aspx?FileId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FileId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EmergencyTeamAndTrainAttachUrl&menuId={1}", FileId, BLL.Const.ProjectEmergencyTeamAndTrainMenuId)));
        }
        #endregion

        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            this.hdEmergencyTeamItemId.Text = string.Empty;
            this.drpPserson.SelectedIndex = 0;
            this.txtTel.Text = string.Empty;
            this.txtJob.Text = string.Empty;
        }
        #endregion

        #region 确定按钮
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            if (this.drpPserson.SelectedValue != Const._Null)
            {
                var getViewList = this.CollectGridInfo();
                getViewList = getViewList.Where(x => x.EmergencyTeamItemId != this.hdEmergencyTeamItemId.Text).ToList();
                if (getViewList.FirstOrDefault(x => x.PersonId == this.drpPserson.SelectedValue) == null)
                {
                    Model.View_Emergency_EmergencyTeamItem newView = new Model.View_Emergency_EmergencyTeamItem
                    {
                        EmergencyTeamItemId = SQLHelper.GetNewID(),
                        FileId = this.FileId,
                        PersonId = this.drpPserson.SelectedValue,
                        PersonName = this.drpPserson.SelectedText,
                        Job = this.txtJob.Text.Trim(),
                        Tel = this.txtTel.Text.Trim(),
                    };

                    getViewList.Add(newView);

                    this.Grid1.DataSource = getViewList;
                    this.Grid1.DataBind();
                    this.InitText();
                }
                else
                {
                    ShowNotify("当前人员已在队伍中。", MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 收集页面信息
        /// <summary>
        ///  收集页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.View_Emergency_EmergencyTeamItem> CollectGridInfo()
        {
            List<Model.View_Emergency_EmergencyTeamItem> getViewList = new List<Model.View_Emergency_EmergencyTeamItem>();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                Model.View_Emergency_EmergencyTeamItem newView = new Model.View_Emergency_EmergencyTeamItem
                {
                    EmergencyTeamItemId = Grid1.Rows[i].DataKeys[0].ToString(),
                    FileId = this.FileId,
                    PersonName = Grid1.Rows[i].Values[0].ToString(),
                    Job = Grid1.Rows[i].Values[1].ToString(),
                    Tel = Grid1.Rows[i].Values[2].ToString(),
                    PersonId = Grid1.Rows[i].Values[3].ToString(),
                };
                getViewList.Add(newView);
            }

            return getViewList;
        }
        #endregion

        #region Grid 操作事件
        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectGridInfo();
            var item = getViewList.FirstOrDefault(x => x.EmergencyTeamItemId == Grid1.SelectedRowID);
            if (item != null)
            {
                this.hdEmergencyTeamItemId.Text = item.EmergencyTeamItemId;
                this.drpPserson.SelectedValue = item.PersonId;
                this.txtJob.Text = item.Job;
                this.txtTel.Text = item.Tel;
            }
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                var getViewList = this.CollectGridInfo();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var item = getViewList.FirstOrDefault(x => x.EmergencyTeamItemId == rowID);
                    if (item != null)
                    {
                        getViewList.Remove(item);
                    }
                }

                this.Grid1.DataSource = getViewList;
                this.Grid1.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// 单位下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getDrpPerson();
        }

        /// <summary>
        /// 
        /// </summary>
        private void getDrpPerson()
        {
            this.drpPserson.Items.Clear();
            PersonService.InitPersonByProjectUnitDropDownList(this.drpPserson, this.ProjectId, this.drpUnit.SelectedValue, true);
        }

        /// <summary>
        /// 人员下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpPserson_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtJob.Text = string.Empty;
            this.txtTel.Text = string.Empty;
            if (this.drpPserson.SelectedValue != Const._Null)
            {
                var person = PersonService.GetPersonById(this.drpPserson.SelectedValue);
                if (person != null)
                {
                    this.txtTel.Text = person.Telephone;
                }
            } 
        }    
    }
}