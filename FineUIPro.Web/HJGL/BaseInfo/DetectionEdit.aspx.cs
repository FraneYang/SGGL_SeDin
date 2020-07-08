using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class DetectionEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string DetectionRateId
        {
            get
            {
                return (string)ViewState["DetectionRateId"];
            }
            set
            {
                ViewState["DetectionRateId"] = value;
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
                this.txtDetectionRateCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.DetectionRateId = Request.Params["DetectionRateId"];
                if (!string.IsNullOrEmpty(this.DetectionRateId))
                {
                    Model.Base_DetectionRate DetectionRate = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(this.DetectionRateId);
                    if (DetectionRate != null)
                    {
                        this.txtDetectionRateCode.Text = DetectionRate.DetectionRateCode;
                        this.txtDetectionRateValue.Text = DetectionRate.DetectionRateValue.ToString();                 
                        this.txtRemark.Text = DetectionRate.Remark;
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
            var q = new Model.SGGLDB(Funs.ConnString).Base_DetectionRate.FirstOrDefault(x => x.DetectionRateCode == this.txtDetectionRateCode.Text.Trim() && (x.DetectionRateId != this.DetectionRateId || (this.DetectionRateId == null && x.DetectionRateId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此探伤比例代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_DetectionRate.FirstOrDefault(x => x.DetectionRateValue == Funs.GetNewInt(this.txtDetectionRateValue.Text.Trim()) && (x.DetectionRateId != this.DetectionRateId || (this.DetectionRateId == null && x.DetectionRateId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此探伤比例值已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_DetectionRate newDetectionRate = new Model.Base_DetectionRate
            {
                DetectionRateCode = this.txtDetectionRateCode.Text.Trim(),
                DetectionRateValue = Funs.GetNewInt(this.txtDetectionRateValue.Text.Trim()),
                Remark = this.txtRemark.Text.Trim()
            };
            
            if (!string.IsNullOrEmpty(this.DetectionRateId))
            {
                newDetectionRate.DetectionRateId = this.DetectionRateId;
                BLL.Base_DetectionRateService.UpdateDetectionRate(newDetectionRate);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DetectionRateMenuId, Const.BtnModify, this.DetectionRateId);
            }
            else
            {
                this.DetectionRateId = SQLHelper.GetNewID(typeof(Model.Base_DetectionRate));
                newDetectionRate.DetectionRateId = this.DetectionRateId;
                BLL.Base_DetectionRateService.AddDetectionRate(newDetectionRate);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DetectionRateMenuId, Const.BtnAdd, this.DetectionRateId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}