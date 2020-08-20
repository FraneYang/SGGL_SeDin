using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PressurePipingClassEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PressurePipingClassId
        {
            get
            {
                return (string)ViewState["PressurePipingClassId"];
            }
            set
            {
                ViewState["PressurePipingClassId"] = value;
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
                this.txtPressurePipingClassCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.PressurePipingClassId = Request.Params["PressurePipingClassId"];
                if (!string.IsNullOrEmpty(this.PressurePipingClassId))
                {
                    Model.Base_PressurePipingClass pipingClass = BLL.Base_PressurePipingClassService.GetPressurePipingClass(this.PressurePipingClassId);
                    if (pipingClass != null)
                    {
                        this.txtPressurePipingClassCode.Text = pipingClass.PressurePipingClassCode;
                        drpPressurePipingType.SelectedValue = pipingClass.PressurePipingType;
                        this.txtRemark.Text = pipingClass.Remark;
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
            var q = Funs.DB.Base_PressurePipingClass.FirstOrDefault(x => x.PressurePipingClassCode == this.txtPressurePipingClassCode.Text.Trim() && (x.PressurePipingClassId != this.PressurePipingClassId || (this.PressurePipingClassId == null && x.PressurePipingClassId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此压力管道级别代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_PressurePipingClass newpipeClass = new Model.Base_PressurePipingClass
            {
                PressurePipingClassCode = this.txtPressurePipingClassCode.Text.Trim(),
                PressurePipingType = drpPressurePipingType.SelectedValue.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (!string.IsNullOrEmpty(this.PressurePipingClassId))
            {
                newpipeClass.PressurePipingClassId = this.PressurePipingClassId;
                BLL.Base_PressurePipingClassService.UpdatePressurePipingClass(newpipeClass);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GrooveTypeMenuId, Const.BtnModify, this.GrooveTypeId);
            }
            else
            {
                this.PressurePipingClassId = SQLHelper.GetNewID(typeof(Model.Base_PressurePipingClass));
                newpipeClass.PressurePipingClassId = this.PressurePipingClassId;
                BLL.Base_PressurePipingClassService.AddPressurePipingClass(newpipeClass);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GrooveTypeMenuId, Const.BtnAdd, this.GrooveTypeId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}