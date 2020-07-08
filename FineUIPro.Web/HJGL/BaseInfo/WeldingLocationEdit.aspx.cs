using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class WeldingLocationEdit : PageBase
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
                this.txtWeldingLocationCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                string WeldingLocationId = Request.Params["WeldingLocationId"];
                if (!string.IsNullOrEmpty(WeldingLocationId))
                {
                    Model.Base_WeldingLocation WeldingLocation = BLL.Base_WeldingLocationServie.GetWeldingLocationById(WeldingLocationId);
                    if (WeldingLocation != null)
                    {
                        this.txtWeldingLocationCode.Text = WeldingLocation.WeldingLocationCode;
                        this.txtWeldingLocationName.Text = WeldingLocation.WeldingLocationName;
                        this.txtRemark.Text = WeldingLocation.Remark;
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
            string WeldingLocationId = Request.Params["WeldingLocationId"];
            var q = new Model.SGGLDB(Funs.ConnString).Base_WeldingLocation.FirstOrDefault(x => x.WeldingLocationCode == this.txtWeldingLocationCode.Text.Trim() && (x.WeldingLocationId != WeldingLocationId || (WeldingLocationId == null && x.WeldingLocationId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此位置代号已存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_WeldingLocation.FirstOrDefault(x => x.WeldingLocationName == this.txtWeldingLocationName.Text.Trim() && (x.WeldingLocationId != WeldingLocationId || (WeldingLocationId == null && x.WeldingLocationId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此位置名称已存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_WeldingLocation newWeldingLocation = new Model.Base_WeldingLocation
            {
                WeldingLocationCode = this.txtWeldingLocationCode.Text.Trim(),
                WeldingLocationName = this.txtWeldingLocationName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (!string.IsNullOrEmpty(WeldingLocationId))
            {
                newWeldingLocation.WeldingLocationId = WeldingLocationId;
                BLL.Base_WeldingLocationServie.UpdateWeldingLocation(newWeldingLocation);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipingClassMenuId, Const.BtnModify, newPipingClass.PipingClassId);
            }
            else
            {
                newWeldingLocation.WeldingLocationId = SQLHelper.GetNewID(typeof(Model.Base_WeldingLocation));
                BLL.Base_WeldingLocationServie.AddWeldingLocation(newWeldingLocation);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipingClassMenuId, Const.BtnAdd, newPipingClass.PipingClassId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}