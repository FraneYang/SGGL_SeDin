using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class DefectEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string DefectId
        {
            get
            {
                return (string)ViewState["DefectId"];
            }
            set
            {
                ViewState["DefectId"] = value;
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
                this.txtDefectName.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.DefectId = Request.Params["DefectId"];
                if (!string.IsNullOrEmpty(this.DefectId))
                {
                    Model.Base_Defect Defect = BLL.Base_DefectService.GetDefectByDefectId(this.DefectId);
                    if (Defect != null)
                    {
                        this.txtDefectName.Text = Defect.DefectName;
                        this.txtDefectEngName.Text = Defect.DefectEngName;
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
            var q = new Model.SGGLDB(Funs.ConnString).Base_Defect.FirstOrDefault(x => x.DefectName == this.txtDefectName.Text.Trim() && (x.DefectId.ToString() != this.DefectId || this.DefectId == null));
            if (q != null)
            {
                Alert.ShowInTop("此缺陷名称已存在", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Defect newDefect = new Model.Base_Defect
            {
                DefectName = this.txtDefectName.Text.Trim(),
                DefectEngName = this.txtDefectEngName.Text.Trim(),
            };

            if (!string.IsNullOrEmpty(this.DefectId))
            {
                newDefect.DefectId = Convert.ToInt32(this.DefectId);
                BLL.Base_DefectService.UpdateDefect(newDefect);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DefectMenuId, Const.BtnDelete, this.DefectId);
            }
            else
            {
                var defects = from x in new Model.SGGLDB(Funs.ConnString).Base_Defect orderby x.DefectId descending select x;
                if (defects.Count() > 0)
                {
                    this.DefectId = (defects.First().DefectId + 1).ToString();
                }
                else
                {
                    this.DefectId = "1";
                }
                newDefect.DefectId = Convert.ToInt32(this.DefectId);
                BLL.Base_DefectService.AddDefect(newDefect);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DefectMenuId, Const.BtnDelete, this.DefectId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}