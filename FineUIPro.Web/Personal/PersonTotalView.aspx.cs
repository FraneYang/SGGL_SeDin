using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Personal
{
    public partial class PersonTotalView : PageBase
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
                        if (roleId != null) {
                            this.txtRoleName.Text = BLL.RoleService.getRoleNamesRoleIds(roleId.RoleId);
                        }
                        
                        this.txtContents.Text = HttpUtility.HtmlDecode(totle.Content);
                    }
                }
            }
        }
        

        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonTotalId))
            {
                this.PersonTotalId = SQLHelper.GetNewID(typeof(Model.PersonTotal));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Person&menuId={1}&type=-1", PersonTotalId, BLL.Const.PersonTotalMenuId)));
        }
    }
}