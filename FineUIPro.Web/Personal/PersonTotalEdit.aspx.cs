using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Personal
{
    public partial class PersonTotalEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 用户主键
        /// </summary>
        public string PersonTotalId
        {
            get
            {
                return (string)ViewState["PersonTotalId"];
            }
            set
            {
                ViewState["PersonTotalId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 用户编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.PersonTotalId = Request.Params["PersonTotalId"];
                BLL.UserService.InitUserUnitIdDropDownList(drpUser, Const.UnitId_SEDIN, true);
                if (!string.IsNullOrEmpty(this.PersonTotalId))
                {
                    var totle = BLL.PersonTotalService.GetPersonByPersonTotalId(this.PersonTotalId);
                    if (totle != null)
                    {
                        if (!string.IsNullOrEmpty(totle.UserId))
                        {
                            this.drpUser.SelectedValue = totle.UserId;
                        }
                        if (totle.StartTime.HasValue)
                        {
                            this.txtStartTime.Text = string.Format("{0:yyyy-MM-dd}", totle.StartTime);
                        }
                        if (totle.EndTime.HasValue)
                        {
                            this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", totle.EndTime);
                        }
                        var roleId = BLL.UserService.GetUserByUserId(totle.UserId);
                        if (roleId != null)
                        {
                            this.txtRoleName.Text = BLL.RoleService.getRoleNamesRoleIds(roleId.RoleId);
                        }
                        this.txtContents.Text = HttpUtility.HtmlDecode(totle.Content);
                    }
                }
                else
                {
                    var codeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(BLL.Const.PersonTotalMenuId);
                    if (codeTemplateRule != null)
                    {
                        this.txtContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
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
            if (this.drpUser.SelectedValue == Const._Null)
            {
                Alert.ShowInParent("请选择人员！", MessageBoxIcon.Warning);
                return;
            }

            Model.PersonTotal newTotal = new Model.PersonTotal
            {
                UserId = this.drpUser.SelectedValue,
                Content = HttpUtility.HtmlEncode(this.txtContents.Text),
                CompiledManId = this.CurrUser.UserId,
                CompiledDate = DateTime.Now
            };
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                newTotal.StartTime = Funs.GetNewDateTime(this.txtStartTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                newTotal.EndTime = Convert.ToDateTime(this.txtEndTime.Text.Trim());
            }
            if (string.IsNullOrEmpty(this.PersonTotalId))
            {
                this.PersonTotalId = SQLHelper.GetNewID(typeof(Model.PersonTotal));
                newTotal.PersonTotalId = this.PersonTotalId;
                PersonTotalService.AddPersonTotal(newTotal);
            }
            else
            {
                var Total = BLL.PersonTotalService.GetPersonByPersonTotalId(this.PersonTotalId);
                if (Total == null)
                {
                    newTotal.PersonTotalId = this.PersonTotalId;
                    PersonTotalService.AddPersonTotal(newTotal);
                }
                else {
                    newTotal.PersonTotalId = Total.PersonTotalId;
                    PersonTotalService.UpdatePersonTotal(newTotal);
                }
                
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        #endregion
        protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUser.SelectedValue != Const._Null)
            {
                var roleId = BLL.UserService.GetUserByUserId(this.drpUser.SelectedValue).RoleId;
                if (!string.IsNullOrEmpty(roleId))
                {
                    var roleName = BLL.RoleService.getRoleNamesRoleIds(roleId);
                    this.txtRoleName.Text = roleName;
                }
            }
        }

        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonTotalId))
            {
                this.PersonTotalId = SQLHelper.GetNewID(typeof(Model.PersonTotal));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Person&menuId={1}&type=0", PersonTotalId, BLL.Const.PersonTotalMenuId)));
        }
    }
}