using System;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ControlView : PageBase
    {
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string DNCompareId = Request.Params["DNCompareId"];
                if (!string.IsNullOrEmpty(DNCompareId))
                {
                    Model.Base_DNCompare DNCompare = BLL.Base_DNCompareService.GetDNCompareByDNCompareId(DNCompareId);
                    if (DNCompare != null)
                    {
                        this.txtPipeSize.Text = DNCompare.PipeSize.ToString();
                        this.txtDN.Text = DNCompare.DN.ToString();
                        if (DNCompare.OutSizeDia.HasValue)
                        {
                            this.txtOutSizeDia.Text = DNCompare.OutSizeDia.ToString();
                        }
                        if (DNCompare.Sch5S.HasValue)
                        {
                            this.txtSCH5S.Text = DNCompare.Sch5S.ToString();
                        }
                        if (DNCompare.Sch10S.HasValue)
                        {
                            this.txtSCH10S.Text = DNCompare.Sch10S.ToString();
                        }
                        if (DNCompare.SCH10.HasValue)
                        {
                            this.txtSCH10.Text = DNCompare.SCH10.ToString();
                        }
                        if (DNCompare.SCH20.HasValue)
                        {
                            this.txtSCH20.Text = DNCompare.SCH20.ToString();
                        }
                        if (DNCompare.SCH30.HasValue)
                        {
                            this.txtSCH30.Text = DNCompare.SCH30.ToString();
                        }
                        if (DNCompare.SCH40S.HasValue)
                        {
                            this.txtSCH40S.Text = DNCompare.SCH40S.ToString();
                        }
                        if (DNCompare.SCH40.HasValue)
                        {
                            this.txtSCH40.Text = DNCompare.SCH40.ToString();
                        }
                        if (DNCompare.SCH60.HasValue)
                        {
                            this.txtSCH60.Text = DNCompare.SCH60.ToString();
                        }
                        if (DNCompare.SCH80S.HasValue)
                        {
                            this.txtSCH80S.Text = DNCompare.SCH80S.ToString();
                        }
                        if (DNCompare.SCH80.HasValue)
                        {
                            this.txtSCH80.Text = DNCompare.SCH80.ToString();
                        }
                        if (DNCompare.SCH100.HasValue)
                        {
                            this.txtSCH100.Text = DNCompare.SCH100.ToString();
                        }
                        if (DNCompare.SCH120.HasValue)
                        {
                            this.txtSCH120.Text = DNCompare.SCH120.ToString();
                        }
                        if (DNCompare.SCH140.HasValue)
                        {
                            this.txtSCH140.Text = DNCompare.SCH140.ToString();
                        }
                        if (DNCompare.SCH160.HasValue)
                        {
                            this.txtSCH160.Text = DNCompare.SCH160.ToString();
                        }
                        if (DNCompare.Thickness1.HasValue)
                        {
                            this.txtThickness1.Text = DNCompare.Thickness1.ToString();
                        }
                        if (DNCompare.Thickness2.HasValue)
                        {
                            this.txtThickness2.Text = DNCompare.Thickness2.ToString();
                        }
                        if (DNCompare.Thickness3.HasValue)
                        {
                            this.txtThickness3.Text = DNCompare.Thickness3.ToString();
                        }
                        if (DNCompare.Thickness4.HasValue)
                        {
                            this.txtThickness4.Text = DNCompare.Thickness4.ToString();
                        }
                        if (DNCompare.Thickness5.HasValue)
                        {
                            this.txtThickness5.Text = DNCompare.Thickness5.ToString();
                        }
                        if (DNCompare.Thickness6.HasValue)
                        {
                            this.txtThickness6.Text = DNCompare.Thickness6.ToString();
                        }
                        if (DNCompare.Thickness7.HasValue)
                        {
                            this.txtThickness7.Text = DNCompare.Thickness7.ToString();
                        }
                        if (DNCompare.Thickness8.HasValue)
                        {
                            this.txtThickness8.Text = DNCompare.Thickness8.ToString();
                        }
                        if (DNCompare.Thickness9.HasValue)
                        {
                            this.txtThickness9.Text = DNCompare.Thickness9.ToString();
                        }
                        if (DNCompare.Thickness10.HasValue)
                        {
                            this.txtThickness10.Text = DNCompare.Thickness10.ToString();
                        }
                        if (DNCompare.Thickness11.HasValue)
                        {
                            this.txtThickness11.Text = DNCompare.Thickness11.ToString();
                        }
                    }
                }
            }
        }
        #endregion
    }
}