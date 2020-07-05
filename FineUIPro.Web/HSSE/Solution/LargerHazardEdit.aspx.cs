using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class LargerHazardEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string HazardId
        {
            get
            {
                return (string)ViewState["HazardId"];
            }
            set
            {
                ViewState["HazardId"] = value;
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
                BLL.ConstValue.InitConstValueDropDownList(this.drpHazardType, ConstValue.Group_LargerHazardType, true);
                //是否需要专家论证              
                BLL.ConstValue.InitConstValueRadioButtonList(this.rblIsArgument, ConstValue.Group_0001, "False");
                
                this.HazardId = Request.Params["HazardId"];
                var largerHazard = BLL.LargerHazardService.GetLargerHazardByHazardId(this.HazardId);
                if (largerHazard != null)
                {
                    this.ProjectId = largerHazard.ProjectId;
                    this.txtLargerHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HazardId);
                    if (!string.IsNullOrEmpty(largerHazard.HazardType))
                    {
                        this.drpHazardType.SelectedValue = largerHazard.HazardType;
                    }
                    this.txtAddress.Text = largerHazard.Address;
                    if (largerHazard.ExpectedTime != null)
                    {
                        this.txtExpectedTime.Text = string.Format("{0:yyyy-MM-dd}", largerHazard.ExpectedTime);
                    }
                    if (largerHazard.RecordTime != null)
                    {
                        this.txtRecordTime.Text = string.Format("{0:yyyy-MM-dd}", largerHazard.RecordTime);
                    }
                    if (largerHazard.IsArgument == true)
                    {
                        this.rblIsArgument.SelectedValue = "True";
                    }
                    else
                    {
                        this.rblIsArgument.SelectedValue = "False";
                    }
                    this.txtRemark.Text = HttpUtility.HtmlDecode(largerHazard.Remark);
                    this.txtDescriptions.Text = largerHazard.Descriptions;
                }
                else
                {
                    ////自动生成编码
                    this.txtLargerHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectLargerHazardListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtExpectedTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtRemark.Text = HttpUtility.HtmlDecode("描述");
                    this.txtRecordTime.Text = string.Format("{0:yyyy-MM-dd}",DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectLargerHazardListMenuId;
                this.ctlAuditFlow.DataId = this.HazardId;
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

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Solution_LargerHazard largerHazard = new Model.Solution_LargerHazard
            {
                HazardCode = this.txtLargerHazardCode.Text.Trim(),
                HazardType = this.drpHazardType.SelectedValue,
                Address = this.txtAddress.Text.Trim(),
                ExpectedTime = Funs.GetNewDateTime(this.txtExpectedTime.Text.Trim()),
                IsArgument = Convert.ToBoolean(this.rblIsArgument.SelectedValue),
                Remark = HttpUtility.HtmlEncode(this.txtRemark.Text.Trim()),
                Descriptions = this.txtDescriptions.Text.Trim(),
                RecordTime = Funs.GetNewDateTime(this.txtRecordTime.Text.Trim()),
                ProjectId = this.ProjectId,
                ////单据状态
                States = BLL.Const.State_0
            };
            if (type == BLL.Const.BtnSubmit)
            {
                largerHazard.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.HazardId))
            {
                largerHazard.HazardId = this.HazardId;
                BLL.LargerHazardService.UpdateLargerHazard(largerHazard);
                BLL.LogService.AddSys_Log(this.CurrUser, largerHazard.HazardCode, largerHazard.HazardId,BLL.Const.ProjectLargerHazardListMenuId,BLL.Const.BtnModify);
            }
            else
            {
                largerHazard.HazardId = SQLHelper.GetNewID(typeof(Model.Solution_LargerHazard));
                largerHazard.RecardMan = this.CurrUser.UserId;
                this.HazardId = largerHazard.HazardId;
                BLL.LargerHazardService.AddLargerHazard(largerHazard);
                BLL.LogService.AddSys_Log(this.CurrUser, largerHazard.HazardCode, largerHazard.HazardId, BLL.Const.ProjectLargerHazardListMenuId, BLL.Const.BtnAdd);              
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectLargerHazardListMenuId, this.HazardId, (type == BLL.Const.BtnSubmit ? true : false), this.drpHazardType.SelectedItem.Text, "../Solution/LargerHazardView.aspx?HazardId={0}");
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HazardId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LargerHazard&menuId={1}", this.HazardId, BLL.Const.ProjectLargerHazardListMenuId)));
        }
        #endregion

        private void SaveNew()
        {
            Model.Solution_LargerHazard largerHazard = new Model.Solution_LargerHazard
            {
                HazardCode = this.txtLargerHazardCode.Text.Trim(),
                HazardType = this.drpHazardType.SelectedValue,
                Address = this.txtAddress.Text.Trim(),
                ExpectedTime = Funs.GetNewDateTime(this.txtExpectedTime.Text.Trim()),
                IsArgument = Convert.ToBoolean(this.rblIsArgument.SelectedValue),
                Remark = HttpUtility.HtmlEncode(this.txtRemark.Text.Trim()),
                ProjectId = this.ProjectId,
                ////单据状态
                States = BLL.Const.State_0,
                HazardId = SQLHelper.GetNewID(typeof(Model.Solution_LargerHazard)),
                RecardMan = this.CurrUser.UserId,
                RecordTime = Funs.GetNewDateTime(this.txtRecordTime.Text.Trim())
            };
            this.HazardId = largerHazard.HazardId;
            BLL.LargerHazardService.AddLargerHazard(largerHazard);
            BLL.LogService.AddSys_Log(this.CurrUser, largerHazard.HazardCode, largerHazard.HazardId,BLL.Const.ProjectLargerHazardListMenuId,BLL.Const.BtnAdd);
        }
    }
}