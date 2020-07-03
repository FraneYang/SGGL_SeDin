using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ControlEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string DNCompareId
        {
            get
            {
                return (string)ViewState["DNCompareId"];
            }
            set
            {
                ViewState["DNCompareId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.DNCompareId = Request.Params["DNCompareId"];
                if (!string.IsNullOrEmpty(this.DNCompareId))
                {
                    Model.Base_DNCompare DNCompare = BLL.Base_DNCompareService.GetDNCompareByDNCompareId(this.DNCompareId);
                    if (DNCompare != null)
                    {
                        this.txtPipeSize.Text = DNCompare.PipeSize.ToString();
                        this.txtDN.Text = DNCompare.DN.ToString();
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
        /// <param name="e"></param>s
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Base_DNCompare newDNCompare = new Model.Base_DNCompare();
            newDNCompare.PipeSize =Funs.GetNewDecimal(this.txtPipeSize.Text.Trim());
            newDNCompare.DN = Funs.GetNewInt(this.txtDN.Text.Trim());
            newDNCompare.OutSizeDia =Funs.GetNewDecimal(this.txtOutSizeDia.Text.Trim());
            newDNCompare.SCH10 = Funs.GetNewDecimal(this.txtSCH10.Text.Trim());
            newDNCompare.SCH20 = Funs.GetNewDecimal(this.txtSCH20.Text.Trim());
            newDNCompare.SCH30 = Funs.GetNewDecimal(this.txtSCH30.Text.Trim());
            newDNCompare.STD = Funs.GetNewDecimal(this.txtSTD.Text.Trim());
            newDNCompare.SCH40 = Funs.GetNewDecimal(this.txtSCH40.Text.Trim());
            newDNCompare.SCH60 = Funs.GetNewDecimal(this.txtSCH60.Text.Trim());
            newDNCompare.XS = Funs.GetNewDecimal(this.txtXS.Text.Trim());
            newDNCompare.SCH80 = Funs.GetNewDecimal(this.txtSCH80.Text.Trim());
            newDNCompare.SCH100 = Funs.GetNewDecimal(this.txtSCH100.Text.Trim());
            newDNCompare.SCH120 = Funs.GetNewDecimal(this.txtSCH120.Text.Trim());
            newDNCompare.SCH140 = Funs.GetNewDecimal(this.txtSCH140.Text.Trim());
            newDNCompare.SCH160 = Funs.GetNewDecimal(this.txtSCH160.Text.Trim());
            newDNCompare.XXS = Funs.GetNewDecimal(this.txtXXS.Text.Trim());            

            if (!string.IsNullOrEmpty(this.DNCompareId))
            {
                newDNCompare.DNCompareId = this.DNCompareId;
                BLL.Base_DNCompareService.UpdateDNCompare(newDNCompare);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ComponentsMenuId, Const.BtnModify, this.DNCompareId);
            }
            else
            {
                this.DNCompareId = SQLHelper.GetNewID(typeof(Model.Base_DNCompare));
                newDNCompare.DNCompareId = this.DNCompareId;
                BLL.Base_DNCompareService.AddDNCompare(newDNCompare);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ComponentsMenuId, Const.BtnAdd, this.DNCompareId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}