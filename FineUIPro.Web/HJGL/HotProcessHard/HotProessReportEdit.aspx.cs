using System;
using BLL;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HotProessReportId
        {
            get
            {
                return (string)ViewState["HotProessReportId"];
            }
            set
            {
                ViewState["HotProessReportId"] = value;
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

                HotProessReportId = Request.Params["HotProessReportId"];
                if (!string.IsNullOrEmpty(HotProessReportId))
                {
                    Model.HJGL_HotProess_Report report = BLL.HotProessReportService.GetHotProessReportById(HotProessReportId);
                    if (report != null)
                    {
                        if (!string.IsNullOrEmpty(report.WeldJointId))
                        {
                            this.hdWeldJointId.Text = report.WeldJointId;
                        }
                        if (report.PointCount.HasValue)
                        {
                            this.txtPointCount.Text = report.PointCount.ToString();
                        }
                        this.txtRequiredT.Text = report.RequiredT;
                        this.txtActualT.Text = report.ActualT;
                        this.txtRequestTime.Text = report.RequestTime;
                        this.txtActualTime.Text = report.ActualTime;
                        this.txtRecordChartNo.Text = report.RecordChartNo;
                    }
                }
                else
                {
                    string hotProessTrustItemId = Request.Params["HotProessTrustItemId"];
                    if (!string.IsNullOrEmpty(hotProessTrustItemId))
                    {
                        var hotProessTrustItem = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hotProessTrustItemId);
                        if (hotProessTrustItem != null)
                        {
                            if (!string.IsNullOrEmpty(hotProessTrustItem.WeldJointId))
                            {
                                this.hdWeldJointId.Text = hotProessTrustItem.WeldJointId;
                            }
                        }
                    }
                }
                var jot = BLL.WeldJointService.GetViewWeldJointById(this.hdWeldJointId.Text);
                if (jot != null)
                {
                    this.txtWeldJointCode.Text = jot.WeldJointCode;
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
            Model.HJGL_HotProess_Report newReport = new Model.HJGL_HotProess_Report();
            newReport.PointCount = Funs.GetNewInt(this.txtPointCount.Text.Trim());
            newReport.RequiredT = this.txtRequiredT.Text.Trim();
            newReport.ActualT = this.txtActualT.Text.Trim();
            newReport.RequestTime = this.txtRequestTime.Text.Trim();
            newReport.ActualTime = this.txtActualTime.Text.Trim();
            newReport.RecordChartNo = this.txtRecordChartNo.Text.Trim();
            if (!string.IsNullOrEmpty(HotProessReportId))
            {
                newReport.HotProessReportId = HotProessReportId;
                BLL.HotProessReportService.UpdateHotProessReport(newReport);
            }
            else
            {
                newReport.WeldJointId = this.hdWeldJointId.Text;
                newReport.HotProessTrustItemId = Request.Params["HotProessTrustItemId"];
                newReport.HotProessReportId = SQLHelper.GetNewID(typeof(Model.HJGL_HotProess_Report));
                HotProessReportId = newReport.HotProessReportId;
                BLL.HotProessReportService.AddHotProessReport(newReport);
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
            if (string.IsNullOrEmpty(HotProessReportId))
            {
                SaveData();
            }
            edit = "1";
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGL/HotProcessHard&menuId={1}&edit={2}", HotProessReportId, Const.HJGL_HotProessTrustMenuId, edit)));
        }
        #endregion
    }
}