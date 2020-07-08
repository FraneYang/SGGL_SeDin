using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencyListView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EmergencyListId
        {
            get
            {
                return (string)ViewState["EmergencyListId"];
            }
            set
            {
                ViewState["EmergencyListId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();              
                this.EmergencyListId = Request.Params["EmergencyListId"];
                if (!string.IsNullOrEmpty(this.EmergencyListId))
                {
                    Model.Emergency_EmergencyList EmergencyList = BLL.EmergencyListService.GetEmergencyListById(this.EmergencyListId);
                    if (EmergencyList != null)
                    {
                        ///读取编号
                        this.txtEmergencyCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.EmergencyListId);
                        this.txtEmergencyName.Text = EmergencyList.EmergencyName;
                        this.drpUnit.Text = BLL.UnitService.GetUnitNameByUnitId(EmergencyList.UnitId);
                        var emergencyType =BLL.EmergencyTypeService.GetEmergencyTypeById(EmergencyList.EmergencyTypeId);
                        if(emergencyType != null)
                        {
                            this.drpEmergencyType.Text = emergencyType.EmergencyTypeName;
                        }
                        this.txtVersionCode.Text = EmergencyList.VersionCode;
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(EmergencyList.CompileMan);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", EmergencyList.CompileDate);
                        this.txtEmergencyContents.Text = HttpUtility.HtmlDecode(EmergencyList.EmergencyContents);
                    }
                }
                
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEmergencyListMenuId;
                this.ctlAuditFlow.DataId = this.EmergencyListId;
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
            if (!string.IsNullOrEmpty(this.EmergencyListId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EmergencyListAttachUrl&menuId={1}&type=-1", EmergencyListId, BLL.Const.ProjectEmergencyListMenuId)));
            }
        }
        #endregion
    }
}