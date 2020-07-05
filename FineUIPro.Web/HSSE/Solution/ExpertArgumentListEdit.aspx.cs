using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ExpertArgumentListEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string ExpertArgumentId
        {
            get
            {
                return (string)ViewState["ExpertArgumentId"];
            }
            set
            {
                ViewState["ExpertArgumentId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //类型
                this.drpHazardType.DataTextField = "ConstText";
                this.drpHazardType.DataValueField = "ConstValue";
                this.drpHazardType.DataSource = ConstValue.drpConstItemList(ConstValue.Group_LargerHazardType);
                this.drpHazardType.DataBind();
                Funs.FineUIPleaseSelect(this.drpHazardType);
                //是否需要专家论证              
                BLL.ConstValue.InitConstValueRadioButtonList(this.rblIsArgument, ConstValue.Group_0001, "False");

                this.ExpertArgumentId = Request.Params["ExpertArgumentId"];
                var expertArgument = BLL.ExpertArgumentService.GetExpertArgumentById(this.ExpertArgumentId);
                if (expertArgument != null)
                {
                    this.ProjectId = expertArgument.ProjectId;
                    this.txtExpertArgumentCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ExpertArgumentId);
                    if (!string.IsNullOrEmpty(expertArgument.HazardType))
                    {
                        this.drpHazardType.SelectedValue = expertArgument.HazardType;
                    }
                    this.txtAddress.Text = expertArgument.Address;
                    this.txtExpectedTime.Text = string.Format("{0:yyyy-MM-dd}", expertArgument.ExpectedTime);
                    if (expertArgument.IsArgument == true)
                    {
                        this.rblIsArgument.SelectedValue = "True";
                    }
                    else
                    {
                        this.rblIsArgument.SelectedValue = "False";
                    }
                    this.txtRemark.Text = HttpUtility.HtmlDecode(expertArgument.Remark);
                    this.txtDescriptions.Text = expertArgument.Descriptions;
                }
                else
                {
                    ////自动生成编码
                    this.txtExpertArgumentCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectExpertArgumentMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtExpectedTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtRemark.Text = HttpUtility.HtmlDecode("描述");
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectExpertArgumentMenuId;
                this.ctlAuditFlow.DataId = this.ExpertArgumentId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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
            if (this.drpHazardType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择类型！", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            if (this.drpHazardType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择类型！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Solution_ExpertArgument expertArgument = new Model.Solution_ExpertArgument
            {
                ExpertArgumentCode = this.txtExpertArgumentCode.Text.Trim(),
                HazardType = this.drpHazardType.SelectedValue,
                Address = this.txtAddress.Text.Trim(),
                ExpectedTime = Funs.GetNewDateTime(this.txtExpectedTime.Text.Trim()),
                IsArgument = Convert.ToBoolean(this.rblIsArgument.SelectedValue),
                Remark = HttpUtility.HtmlEncode(this.txtRemark.Text.Trim()),
                Descriptions = this.txtDescriptions.Text.Trim(),
                ProjectId = this.ProjectId,
                ////单据状态
                States = BLL.Const.State_0
            };
            if (type == BLL.Const.BtnSubmit)
            {
                expertArgument.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ExpertArgumentId))
            {
                expertArgument.ExpertArgumentId = this.ExpertArgumentId;
                BLL.ExpertArgumentService.UpdateExpertArgument(expertArgument);
                BLL.LogService.AddSys_Log(this.CurrUser, expertArgument.ExpertArgumentCode, expertArgument.ExpertArgumentId, BLL.Const.ProjectExpertArgumentMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.ExpertArgumentId = SQLHelper.GetNewID(typeof(Model.Solution_ExpertArgument));
                expertArgument.ExpertArgumentId = this.ExpertArgumentId;
                expertArgument.RecardMan = this.CurrUser.UserId;
                expertArgument.RecordTime = DateTime.Now;
                this.ExpertArgumentId = expertArgument.ExpertArgumentId;
                BLL.ExpertArgumentService.AddExpertArgument(expertArgument);
                BLL.LogService.AddSys_Log(this.CurrUser, expertArgument.ExpertArgumentCode, expertArgument.ExpertArgumentId, BLL.Const.ProjectExpertArgumentMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectExpertArgumentMenuId, this.ExpertArgumentId, (type == BLL.Const.BtnSubmit ? true : false), this.drpHazardType.SelectedItem.Text, "../Solution/ExpertArgumentListView.aspx?ExpertArgumentId={0}");
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
            if (string.IsNullOrEmpty(this.ExpertArgumentId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ExpertArgument&menuId={1}", this.ExpertArgumentId, BLL.Const.ProjectExpertArgumentMenuId)));
        }
        #endregion

        private void SaveNew()
        {
            Model.Solution_ExpertArgument expertArgument = new Model.Solution_ExpertArgument
            {
                ExpertArgumentCode = this.txtExpertArgumentCode.Text.Trim(),
                HazardType = this.drpHazardType.SelectedValue,
                Address = this.txtAddress.Text.Trim(),
                ExpectedTime = Funs.GetNewDateTime(this.txtExpectedTime.Text.Trim()),
                IsArgument = Convert.ToBoolean(this.rblIsArgument.SelectedValue),
                Remark = HttpUtility.HtmlEncode(this.txtRemark.Text.Trim()),
                ProjectId = this.ProjectId,
                ////单据状态
                States = BLL.Const.State_0,
                ExpertArgumentId = SQLHelper.GetNewID(typeof(Model.Solution_ExpertArgument)),
                RecardMan = this.CurrUser.UserId,
                RecordTime = DateTime.Now
            };
            this.ExpertArgumentId = expertArgument.ExpertArgumentId;
            BLL.ExpertArgumentService.AddExpertArgument(expertArgument);
            BLL.LogService.AddSys_Log(this.CurrUser, expertArgument.ExpertArgumentCode, expertArgument.ExpertArgumentId,BLL.Const.ProjectExpertArgumentMenuId,BLL.Const.BtnAdd);
        }
    }
}