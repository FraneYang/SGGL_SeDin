using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class TestingView : PageBase
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string DetectionTypeId = Request.Params["DetectionTypeId"];
                if (!string.IsNullOrEmpty(DetectionTypeId))
                {
                    Model.Base_DetectionType getDetectionType = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(DetectionTypeId);
                    if (getDetectionType != null)
                    {
                        this.txtDetectionTypeCode.Text = getDetectionType.DetectionTypeCode;
                        this.txtDetectionTypeName.Text = getDetectionType.DetectionTypeName;
                        this.txtSecuritySpace.Text = getDetectionType.SecuritySpace.ToString();
                        this.txtRemark.Text = getDetectionType.Remark;
                        this.txtInjuryDegree.Text = getDetectionType.InjuryDegree;
                        var getSysType = BLL.DropListService.HJGL_GetTestintTypeList().FirstOrDefault(x => x.Value == getDetectionType.SysType);
                        if (getSysType != null)
                        {
                            this.txtSysType.Text = getSysType.Text;
                        }
                    }
                }
            }
        }
        #endregion
    }
}