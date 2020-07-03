using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class DrawEdit : PageBase
    {
        /// <summary>
        /// 项目id
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.FineUIPleaseSelect(drprecover);
                BLL.DrawService.InitMainItemDropDownList(drpMainItem, this.CurrUser.LoginProjectId);
                BLL.DrawService.InitDesignCNNameDropDownList(drpDesignCN);
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string DrawId = Request.Params["DrawId"];
                drprecover.Enabled = false;
                if (!string.IsNullOrEmpty(DrawId))
                {

                    Model.Check_Draw Draw = BLL.DrawService.GetDrawByDrawId(DrawId);
                    if (Draw != null)
                    {
                        this.ProjectId = Draw.ProjectId;
                        this.txtDrawCode.Text = Draw.DrawCode;
                        this.txtDrawName.Text = Draw.DrawName;
                        if (!string.IsNullOrEmpty(Draw.MainItem))
                        {
                            this.drpMainItem.SelectedValue = Draw.MainItem;
                        }
                        if (!string.IsNullOrEmpty(Draw.DesignCN))
                        {
                            this.drpDesignCN.SelectedValue = Draw.DesignCN;
                        }
                        if (!string.IsNullOrEmpty(Draw.Recover))
                        {
                            this.drprecover.SelectedValue = Draw.Recover;
                        }
                        if (Draw.IsInvalid == true && Draw.IsInvalid != null)
                        {
                            this.rblIsInvalid.SelectedValue = "true";
                            drprecover.Enabled = true;
                        }
                        else
                        {
                            this.drprecover.SelectedIndex = 0;
                            this.drprecover.Enabled = false;
                        }

                        this.txtEdition.Text = Draw.Edition;
                        this.txtAcceptDate.Text = string.Format("{0:yyyy-MM-dd}", Draw.AcceptDate);
                        //this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", Draw.CompileDate);
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DrawMenuId, BLL.Const.BtnSave))
            {
                SaveData();

            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        private void SaveData()
        {
            string DrawId = Request.Params["DrawId"];
            Model.Check_Draw Draw = new Model.Check_Draw();
            Draw.DrawCode = this.txtDrawCode.Text.Trim();
            Draw.DrawName = this.txtDrawName.Text.Trim();
            Draw.ProjectId = this.CurrUser.LoginProjectId;
            Draw.MainItem = this.drpMainItem.SelectedValue;
            Draw.DesignCN = this.drpDesignCN.SelectedValue;
            Draw.Edition = this.txtEdition.Text.Trim();
            Draw.AcceptDate = Convert.ToDateTime(this.txtAcceptDate.Text.Trim());
            Draw.CompileMan = this.CurrUser.UserId;
            Draw.CompileDate = DateTime.Now;
            Draw.Recover = drprecover.SelectedValue;
            Draw.IsInvalid = Convert.ToBoolean(rblIsInvalid.SelectedValue);
            if (!string.IsNullOrEmpty(DrawId))
            {
                Draw.DrawId = DrawId;
                BLL.DrawService.UpdateCheckDraw(Draw);
            }
            else
            {
                Draw.DrawId = SQLHelper.GetNewID(typeof(Model.WBS_UnitWork));
                BLL.DrawService.AddCheckDraw(Draw);

            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void rblIsInvalid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblIsInvalid.SelectedValue == "true")
            {
                this.drprecover.Enabled = true;

            }
            else
            {
                this.drprecover.SelectedIndex = 0;
                this.drprecover.Enabled = false;
            }
        }
    }
}