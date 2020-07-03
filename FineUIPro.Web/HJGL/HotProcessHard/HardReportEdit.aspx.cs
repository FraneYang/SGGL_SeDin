using BLL;
using System;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HardReportId
        {
            get
            {
                return (string)ViewState["HardReportId"];
            }
            set
            {
                ViewState["HardReportId"] = value;
            }
        }

        /// <summary>
        /// 硬度委托明细主键
        /// </summary>
        public string HardTrustItemID
        {
            get
            {
                return (string)ViewState["HardTrustItemID"];
            }
            set
            {
                ViewState["HardTrustItemID"] = value;
            }
        }

        /// <summary>
        /// 焊口主键
        /// </summary>
        public string WeldJointId
        {
            get
            {
                return (string)ViewState["WeldJointId"];
            }
            set
            {
                ViewState["WeldJointId"] = value;
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
                this.txtHardReportNo.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.HardReportId = Request.Params["HardReportId"];
                this.HardTrustItemID = Request.Params["HardTrustItemID"];
                if (!string.IsNullOrEmpty(this.HardReportId))
                {
                    Model.View_HJGL_Hard_Report getHardReport = BLL.Hard_ReportService.GetViewHardReportByHardReportId(this.HardReportId);
                    if (getHardReport != null)
                    {
                        this.txtPipelineCode.Text = getHardReport.PipelineCode;
                        this.txtWeldJointCode.Text = getHardReport.WeldJointCode;
                        this.txtHardReportNo.Text = getHardReport.HardReportNo;
                        this.txtTestingPointNo.Text = getHardReport.TestingPointNo;
                        if (getHardReport.HardNessValue1 != null)
                        {
                            this.nbHardNessValue1.Text = getHardReport.HardNessValue1.ToString();
                        }
                        if (getHardReport.HardNessValue2 != null)
                        {
                            this.nbHardNessValue2.Text = getHardReport.HardNessValue2.ToString();
                        }
                        if (getHardReport.HardNessValue3 != null)
                        {
                            this.nbHardNessValue3.Text = getHardReport.HardNessValue3.ToString();
                        }
                        this.txtRemark.Text = getHardReport.Remark;
                        this.HardTrustItemID = getHardReport.HardTrustItemID;
                        this.WeldJointId = getHardReport.WeldJointId;
                    }
                }
                else
                {
                    Model.HJGL_Hard_TrustItem trustItem = BLL.Hard_TrustItemService.GetHardTrustItemById(this.HardTrustItemID);
                    if (trustItem != null)
                    {
                        Model.View_HJGL_WeldJoint weldJoint = BLL.WeldJointService.GetViewWeldJointById(trustItem.WeldJointId);
                        if (weldJoint != null)
                        {
                            this.txtPipelineCode.Text = weldJoint.PipelineCode;
                            this.txtWeldJointCode.Text = weldJoint.WeldJointCode;
                            this.WeldJointId = weldJoint.WeldJointId;
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

         /// <summary>
        /// 提交数据
        /// </summary>
        private void SaveData()
        {
            Model.HJGL_Hard_Report newHardReport = new Model.HJGL_Hard_Report
            {
                HardTrustItemID = this.HardTrustItemID,
                WeldJointId = this.WeldJointId,
                HardReportNo = this.txtHardReportNo.Text.Trim(),
                TestingPointNo = this.txtTestingPointNo.Text.Trim(),
                HardNessValue1 = Funs.GetNewInt(this.nbHardNessValue1.Text.Trim()),
                HardNessValue2 = Funs.GetNewInt(this.nbHardNessValue2.Text.Trim()),
                HardNessValue3 = Funs.GetNewInt(this.nbHardNessValue3.Text.Trim()),
                Remark = this.txtRemark.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.HardReportId))
            {
                newHardReport.HardReportId = this.HardReportId;
                BLL.Hard_ReportService.UpdateHard_Report(newHardReport);
                //BLL.Sys_LogService.AddLog(BLL.Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Resources.Lan.ModifyHardness);
            }
            else
            {
                this.HardReportId = SQLHelper.GetNewID(typeof(Model.HJGL_Hard_Report));
                newHardReport.HardReportId = this.HardReportId;
                BLL.Hard_ReportService.AddHard_Report(newHardReport);
                //BLL.Sys_LogService.AddLog(BLL.Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Resources.Lan.AddHardness);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            string edit = "0"; // 表示能打开附件上传窗口，但不能上传附件
            if (string.IsNullOrEmpty(HardReportId))
            {
                SaveData();
            }
            edit = "1";
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGL/HotProcessHard&menuId={1}&edit={2}", HardReportId, Const.HJGL_HotHardManageEditMenuId, edit)));
        }
        #endregion
    }
}