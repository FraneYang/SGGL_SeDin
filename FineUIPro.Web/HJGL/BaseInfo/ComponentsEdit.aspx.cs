using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class ComponentsEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ComponentsId
        {
            get
            {
                return (string)ViewState["ComponentsId"];
            }
            set
            {
                ViewState["ComponentsId"] = value;
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
                this.txtComponentsCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.ComponentsId = Request.Params["ComponentsId"];
                if (!string.IsNullOrEmpty(this.ComponentsId))
                {
                    Model.Base_Components Components = BLL.Base_ComponentsService.GetComponentsByComponentsId(this.ComponentsId);
                    if (Components != null)
                    {
                        this.txtComponentsCode.Text = Components.ComponentsCode;
                        this.txtComponentsName.Text = Components.ComponentsName;                 
                        this.txtRemark.Text = Components.Remark;
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
            var q = Funs.DB.Base_Components.FirstOrDefault(x => x.ComponentsCode == this.txtComponentsCode.Text.Trim() && (x.ComponentsId != this.ComponentsId || (this.ComponentsId == null && x.ComponentsId != null)) && x.ProjeceId == this.CurrUser.LoginProjectId);
            if (q != null)
            {
                Alert.ShowInTop("此组件代号已存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_Components.FirstOrDefault(x => x.ComponentsName == this.txtComponentsName.Text.Trim() && (x.ComponentsId != this.ComponentsId || (this.ComponentsId == null && x.ComponentsId != null)) && x.ProjeceId == this.CurrUser.LoginProjectId);
            if (q2 != null)
            {
                Alert.ShowInTop("此组件名称已存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Components newComponents = new Model.Base_Components
            {
                ComponentsCode = this.txtComponentsCode.Text.Trim(),
                ComponentsName = this.txtComponentsName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                ProjeceId = this.CurrUser.LoginProjectId
            };
            
            if (!string.IsNullOrEmpty(this.ComponentsId))
            {
                newComponents.ComponentsId = this.ComponentsId;
                BLL.Base_ComponentsService.UpdateComponents(newComponents);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ComponentsMenuId, Const.BtnModify, this.ComponentsId);
            }
            else
            {
                this.ComponentsId = SQLHelper.GetNewID(typeof(Model.Base_Components));
                newComponents.ComponentsId = this.ComponentsId;
                BLL.Base_ComponentsService.AddComponents(newComponents);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_ComponentsMenuId, Const.BtnSave, this.ComponentsId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}