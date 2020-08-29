using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.LeakVacuum
{
    public partial class LV_ItemEndCheckEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string LVItemEndCheckId
        {
            get
            {
                return (string)ViewState["LVItemEndCheckId"];
            }
            set
            {
                ViewState["LVItemEndCheckId"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                LVItemEndCheckId = Request.Params["LVItemEndCheckId"];
                BLL.UserService.InitUserDropDownList(drpDealMan, this.CurrUser.LoginProjectId, true);
                BLL.UserService.InitUserDropDownList(drpCheckMan, this.CurrUser.LoginProjectId, true);
                if (!string.IsNullOrEmpty(LVItemEndCheckId))
                {
                    Model.HJGL_LV_ItemEndCheck EndCheck = BLL.LV_ItemEndCheckService.GetAItemEndCheckByID(LVItemEndCheckId);
                    if (EndCheck != null)
                    {
                        this.drpType.SelectedValue = EndCheck.ItemType;
                        this.txtRemark.Text = EndCheck.Remark;
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", EndCheck.CheckDate);
                        if (!string.IsNullOrEmpty(EndCheck.CheckMan))
                        {
                            this.drpCheckMan.SelectedValue = EndCheck.CheckMan;
                        }
                        if (!string.IsNullOrEmpty(EndCheck.DealMan))
                        {
                            this.txtOpinion.Hidden = false;
                            this.drpDealMan.SelectedValue = EndCheck.DealMan;
                        }
                        if (EndCheck.DealDate.HasValue || this.CurrUser.UserId !=EndCheck.DealMan ) {
                            txtOpinion.Text = EndCheck.Opinion;
                            this.drpType.Readonly = true;
                            this.drpCheckMan.Readonly = true;
                            this.drpDealMan.Readonly = true;
                            this.txtRemark.Readonly = true;
                            this.txtCheckDate.Readonly = true;
                            this.txtOpinion.Readonly = true;
                            this.btnSave.Hidden = true;
                        }
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string LV_PipeId = Request.Params["LV_PipeId"];
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.LV_ItemEndCheckMenuId, Const.BtnSave))
            {
                if (drpCheckMan.SelectedValue != BLL.Const._Null && drpDealMan.SelectedValue != BLL.Const._Null)
                {
                    Model.HJGL_LV_ItemEndCheck newItemEndCheck = new Model.HJGL_LV_ItemEndCheck();
                    newItemEndCheck.LV_PipeId = LV_PipeId;
                    newItemEndCheck.Remark = txtRemark.Text;
                    newItemEndCheck.CheckMan = drpCheckMan.SelectedValue;
                    newItemEndCheck.CheckDate = Funs.GetNewDateTime(txtCheckDate.Text.Trim());
                    newItemEndCheck.DealMan = drpDealMan.SelectedValue;
                    newItemEndCheck.ItemType = drpType.SelectedValue;
                    var EndCheck = BLL.LV_ItemEndCheckService.GetAItemEndCheckByID(LVItemEndCheckId);
                    if (EndCheck == null)
                    {
                        BLL.LV_ItemEndCheckService.AddAItemEndCheck(newItemEndCheck);
                    }
                    else
                    {
                        EndCheck.Opinion = txtOpinion.Text;
                        EndCheck.DealDate = DateTime.Now;
                        BLL.LV_ItemEndCheckService.UpdateAItemEndCheck(EndCheck);
                    }
                }
                else
                {
                    ShowNotify("必填项不能为空");
                    return;
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

    }
}