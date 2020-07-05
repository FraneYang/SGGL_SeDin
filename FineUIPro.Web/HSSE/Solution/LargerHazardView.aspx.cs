using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class LargerHazardView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string HazardId
        {
            get
            {
                return (string)ViewState["HazardId"];
            }
            set
            {
                ViewState["HazardId"] = value;
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
                this.HazardId = Request.Params["HazardId"];
                var largerHazard = BLL.LargerHazardService.GetLargerHazardByHazardId(this.HazardId);
                if (largerHazard != null)
                {
                    this.txtLargerHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HazardId);
                    if (!string.IsNullOrEmpty(largerHazard.HazardType))
                    {
                        this.txtHazardType.Text = ConstValue.drpConstItemList(ConstValue.Group_LargerHazardType).FirstOrDefault(x => x.ConstValue == largerHazard.HazardType).ConstText;
                    }
                    this.txtAddress.Text = largerHazard.Address;
                    if (largerHazard.ExpectedTime != null)
                    {
                        this.txtExpectedTime.Text = string.Format("{0:yyyy-MM-dd}", largerHazard.ExpectedTime);
                    }
                    if (largerHazard.RecordTime != null)
                    {
                        this.txtRecordTime.Text = string.Format("{0:yyyy-MM-dd}", largerHazard.RecordTime);
                    }
                    if (largerHazard.IsArgument == true)
                    {
                        this.txtIsArgument.Text = "是";
                    }
                    else
                    {
                        this.txtIsArgument.Text = "否";
                    }
                    this.txtDescriptions.Text = largerHazard.Descriptions;
                    this.txtRemark.Text = HttpUtility.HtmlDecode(largerHazard.Remark);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectLargerHazardListMenuId;
                this.ctlAuditFlow.DataId = this.HazardId;
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
            if (!string.IsNullOrEmpty(this.HazardId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LargerHazard&menuId={1}&type=-1", this.HazardId, BLL.Const.ProjectLargerHazardListMenuId)));
            }    
        }
        #endregion
    }
}