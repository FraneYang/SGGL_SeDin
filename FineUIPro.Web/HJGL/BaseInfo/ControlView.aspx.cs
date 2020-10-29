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
                        this.txtDN.Text = DNCompare.DN.ToString();
                        this.txtPipeSize.Text = DNCompare.PipeSize.ToString();
                        if (DNCompare.OutSizeDia.HasValue)
                        {
                            this.txtOutSizeDia.Text = DNCompare.OutSizeDia.ToString();
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
                        if (DNCompare.STD.HasValue)
                        {
                            this.txtSTD.Text = DNCompare.STD.ToString();
                        }
                        if (DNCompare.SCH40.HasValue)
                        {
                            this.txtSCH40.Text = DNCompare.SCH40.ToString();
                        }
                        if (DNCompare.SCH60.HasValue)
                        {
                            this.txtSCH60.Text = DNCompare.SCH60.ToString();
                        }
                        if (DNCompare.XS.HasValue)
                        {
                            this.txtXS.Text = DNCompare.XS.ToString();
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
                        if (DNCompare.XXS.HasValue)
                        {
                            this.txtXXS.Text = DNCompare.XXS.ToString();
                        }
                        if (DNCompare.Size.HasValue)
                        {
                            this.txtSize.Text = DNCompare.Size.ToString();
                        }
                        if (DNCompare.Thickness.HasValue)
                        {
                            this.txtthickness.Text = DNCompare.Thickness.ToString();
                        }
                    }
                }
            }
        }
        #endregion
    }
}