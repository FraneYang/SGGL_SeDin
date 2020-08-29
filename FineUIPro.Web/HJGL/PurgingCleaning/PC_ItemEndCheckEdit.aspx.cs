using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.PurgingCleaning
{
    public partial class PC_ItemEndCheckEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string PCItemEndCheckId
        {
            get
            {
                return (string)ViewState["PCItemEndCheckId"];
            }
            set
            {
                ViewState["PCItemEndCheckId"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                PCItemEndCheckId = Request.Params["PCItemEndCheckId"];
                BLL.UserService.InitUserDropDownList(drpDealMan, this.CurrUser.LoginProjectId, true);
                BLL.UserService.InitUserDropDownList(drpCheckMan, this.CurrUser.LoginProjectId, true);
                if (!string.IsNullOrEmpty(PCItemEndCheckId))
                {
                    Model.HJGL_PC_ItemEndCheck EndCheck = BLL.PC_ItemEndCheckService.GetAItemEndCheckByID(PCItemEndCheckId);
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
            string PC_PipeId = Request.Params["PC_PipeId"];
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnSave))
            {
                if (drpCheckMan.SelectedValue != BLL.Const._Null && drpDealMan.SelectedValue != BLL.Const._Null)
                {
                    Model.HJGL_PC_ItemEndCheck newItemEndCheck = new Model.HJGL_PC_ItemEndCheck();
                    newItemEndCheck.PC_PipeId = PC_PipeId;
                    newItemEndCheck.Remark = txtRemark.Text;
                    newItemEndCheck.CheckMan = drpCheckMan.SelectedValue;
                    newItemEndCheck.CheckDate = Funs.GetNewDateTime(txtCheckDate.Text.Trim());
                    newItemEndCheck.DealMan = drpDealMan.SelectedValue;
                    newItemEndCheck.ItemType = drpType.SelectedValue;
                    var EndCheck = BLL.PC_ItemEndCheckService.GetAItemEndCheckByID(PCItemEndCheckId);
                    if (EndCheck == null)
                    {
                        BLL.PC_ItemEndCheckService.AddAItemEndCheck(newItemEndCheck);
                    }
                    else
                    {
                        EndCheck.Opinion = txtOpinion.Text;
                        EndCheck.DealDate = DateTime.Now;
                        BLL.PC_ItemEndCheckService.UpdateAItemEndCheck(EndCheck);
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