using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class PressureEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PressureId
        {
            get
            {
                return (string)ViewState["PressureId"];
            }
            set
            {
                ViewState["PressureId"] = value;
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
                this.txtPressureCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.PressureId = Request.Params["PressureId"];
                if (!string.IsNullOrEmpty(this.PressureId))
                {
                    Model.Base_Pressure Pressure = BLL.Base_PressureService.GetPressureByPressureId(this.PressureId);
                    if (Pressure != null)
                    {
                        this.txtPressureCode.Text = Pressure.PressureCode;
                        this.txtPressureName.Text = Pressure.PressureName;
                        this.txtRemark.Text = Pressure.Remark;
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
            var q = Funs.DB.Base_Pressure.FirstOrDefault(x => x.PressureCode == this.txtPressureCode.Text.Trim() && (x.PressureId != this.PressureId || (this.PressureId == null && x.PressureId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此试压代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_Pressure.FirstOrDefault(x => x.PressureName == this.txtPressureName.Text.Trim() && (x.PressureId != this.PressureId || (this.PressureId == null && x.PressureId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此试压名称已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Pressure newPressure = new Model.Base_Pressure
            {
                PressureCode = this.txtPressureCode.Text.Trim(),
                PressureName = this.txtPressureName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (!string.IsNullOrEmpty(this.PressureId))
            {
                newPressure.PressureId = this.PressureId;
                BLL.Base_PressureService.UpdatePressure(newPressure);
                //BLL.LogService.AddLog( this.CurrUser.UserId, "修改试压类型");
            }
            else
            {
                this.PressureId = SQLHelper.GetNewID(typeof(Model.Base_Pressure));
                newPressure.PressureId = this.PressureId;
                BLL.Base_PressureService.AddPressure(newPressure);
                //BLL.LogService.AddLog( this.CurrUser.UserId, "添加试压类型");
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}