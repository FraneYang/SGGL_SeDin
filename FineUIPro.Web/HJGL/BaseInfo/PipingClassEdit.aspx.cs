using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PipingClassEdit : PageBase
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
                this.txtPipingClassCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                Base_PipingClassService.InitSteelTypeDropDownList(this.drpSteelType, true, "-请选择-");
                string pipingClassId = Request.Params["PipingClassId"];
                if (!string.IsNullOrEmpty(pipingClassId))
                {
                    Model.Base_PipingClass PipingClass = BLL.Base_PipingClassService.GetPipingClassByPipingClassId(pipingClassId);
                    if (PipingClass != null)
                    {
                        this.txtPipingClassCode.Text = PipingClass.PipingClassCode;
                        this.txtPipingClassName.Text = PipingClass.PipingClassName;                 
                        this.txtRemark.Text = PipingClass.Remark;
                        if (!string.IsNullOrEmpty(PipingClass.SteelType))
                        {
                            this.drpSteelType.SelectedValue = PipingClass.SteelType;
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
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string pipingClassId = Request.Params["PipingClassId"];
            var q = Funs.DB.Base_PipingClass.FirstOrDefault(x => x.PipingClassCode == this.txtPipingClassCode.Text.Trim() && (x.PipingClassId != pipingClassId || (pipingClassId == null && x.PipingClassId != null)) && x.ProjectId==this.CurrUser.LoginProjectId );
            if (q != null)
            {
                Alert.ShowInTop("此等级代号已存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_PipingClass.FirstOrDefault(x => x.PipingClassName == this.txtPipingClassName.Text.Trim() && (x.PipingClassId != pipingClassId || (pipingClassId == null && x.PipingClassId != null)) && x.ProjectId == this.CurrUser.LoginProjectId);
            if (q2 != null)
            {
                Alert.ShowInTop("此等级名称已存在！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpSteelType.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择钢材类型！", MessageBoxIcon.Warning);
                return;
            }
            Model.Base_PipingClass newPipingClass = new Model.Base_PipingClass
            {
                PipingClassCode = this.txtPipingClassCode.Text.Trim(),
                PipingClassName = this.txtPipingClassName.Text.Trim(),
                SteelType=this.drpSteelType.SelectedValue,
                Remark = this.txtRemark.Text.Trim(),
                ProjectId=this.CurrUser.LoginProjectId
            };

            if (!string.IsNullOrEmpty(pipingClassId))
            {
                newPipingClass.PipingClassId = pipingClassId;
                BLL.Base_PipingClassService.UpdatePipingClass(newPipingClass);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipingClassMenuId, Const.BtnModify, newPipingClass.PipingClassId);
            }
            else
            {
                newPipingClass.PipingClassId = SQLHelper.GetNewID(typeof(Model.Base_PipingClass));
                BLL.Base_PipingClassService.AddPipingClass(newPipingClass);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipingClassMenuId, Const.BtnAdd, newPipingClass.PipingClassId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}