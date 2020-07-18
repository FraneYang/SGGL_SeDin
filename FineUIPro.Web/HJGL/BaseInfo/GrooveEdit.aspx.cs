using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class GrooveEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GrooveTypeId
        {
            get
            {
                return (string)ViewState["GrooveTypeId"];
            }
            set
            {
                ViewState["GrooveTypeId"] = value;
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
                this.txtGrooveTypeCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.GrooveTypeId = Request.Params["GrooveTypeId"];
                if (!string.IsNullOrEmpty(this.GrooveTypeId))
                {
                    Model.Base_GrooveType GrooveType = BLL.Base_GrooveTypeService.GetGrooveTypeByGrooveTypeId(this.GrooveTypeId);
                    if (GrooveType != null)
                    {
                        this.txtGrooveTypeCode.Text = GrooveType.GrooveTypeCode;
                        this.txtGrooveTypeName.Text = GrooveType.GrooveTypeName;
                        this.txtRemark.Text = GrooveType.Remark;
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
            var q = Funs.DB.Base_GrooveType.FirstOrDefault(x => x.GrooveTypeCode == this.txtGrooveTypeCode.Text.Trim() && (x.GrooveTypeId != this.GrooveTypeId || (this.GrooveTypeId == null && x.GrooveTypeId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此坡口类型代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_GrooveType.FirstOrDefault(x => x.GrooveTypeName == this.txtGrooveTypeName.Text.Trim() && (x.GrooveTypeId != this.GrooveTypeId || (this.GrooveTypeId == null && x.GrooveTypeId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此坡口类型名称已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_GrooveType newGrooveType = new Model.Base_GrooveType
            {
                GrooveTypeCode = this.txtGrooveTypeCode.Text.Trim(),
                GrooveTypeName = this.txtGrooveTypeName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (!string.IsNullOrEmpty(this.GrooveTypeId))
            {
                newGrooveType.GrooveTypeId = this.GrooveTypeId;
                BLL.Base_GrooveTypeService.UpdateGrooveType(newGrooveType);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GrooveTypeMenuId, Const.BtnModify, this.GrooveTypeId);
            }
            else
            {
                this.GrooveTypeId = SQLHelper.GetNewID(typeof(Model.Base_GrooveType));
                newGrooveType.GrooveTypeId = this.GrooveTypeId;
                BLL.Base_GrooveTypeService.AddGrooveType(newGrooveType);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_GrooveTypeMenuId, Const.BtnAdd, this.GrooveTypeId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}