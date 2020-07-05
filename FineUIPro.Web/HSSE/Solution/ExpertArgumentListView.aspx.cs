using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ExpertArgumentListView :PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string ExpertArgumentId
        {
            get
            {
                return (string)ViewState["ExpertArgumentId"];
            }
            set
            {
                ViewState["ExpertArgumentId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ExpertArgumentId = Request.Params["ExpertArgumentId"];
                var expertArgument = BLL.ExpertArgumentService.GetExpertArgumentById(this.ExpertArgumentId);
                if (expertArgument != null)
                {
                    this.txtExpertArgumentCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ExpertArgumentId);
                    if (!string.IsNullOrEmpty(expertArgument.HazardType))
                    {
                        this.txtHazardType.Text = ConstValue.drpConstItemList(ConstValue.Group_LargerHazardType).FirstOrDefault(x => x.ConstValue == expertArgument.HazardType).ConstText;
                    }
                    this.txtAddress.Text = expertArgument.Address;
                    if (expertArgument.ExpectedTime != null)
                    {
                        this.txtExpectedTime.Text = string.Format("{0:yyyy-MM-dd}", expertArgument.ExpectedTime);
                    }
                    if (expertArgument.IsArgument == true)
                    {
                        this.txtIsArgument.Text = "是";
                    }
                    else
                    {
                        this.txtIsArgument.Text = "否";
                    }
                    this.txtRemark.Text = HttpUtility.HtmlDecode(expertArgument.Remark);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectExpertArgumentMenuId;
                this.ctlAuditFlow.DataId = this.ExpertArgumentId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ExpertArgumentId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ExpertArgument&menuId={1}&type=-1", this.ExpertArgumentId, BLL.Const.ProjectExpertArgumentMenuId)));
            }
        }
        #endregion
    }
}