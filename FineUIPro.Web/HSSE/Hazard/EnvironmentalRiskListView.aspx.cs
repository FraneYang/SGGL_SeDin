using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class EnvironmentalRiskListView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string EnvironmentalRiskListId
        {
            get
            {
                return (string)ViewState["EnvironmentalRiskListId"];
            }
            set
            {
                ViewState["EnvironmentalRiskListId"] = value;
            }
        }
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Hazard_EnvironmentalRiskItem> environmentalRiskItems = new List<Model.View_Hazard_EnvironmentalRiskItem>();
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                environmentalRiskItems.Clear();
                this.EnvironmentalRiskListId = Request.Params["EnvironmentalRiskListId"];
                var environmentalRiskList = BLL.Hazard_EnvironmentalRiskListService.GetEnvironmentalRiskList(this.EnvironmentalRiskListId);
                if (environmentalRiskList != null)
                {
                    this.txtRiskCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.EnvironmentalRiskListId);
                    this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(environmentalRiskList.CompileMan);
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", environmentalRiskList.CompileDate);
                    this.txtContents.Text = HttpUtility.HtmlDecode(environmentalRiskList.Contents);

                    this.txtWorkArea.Text = environmentalRiskList.WorkAreaName;
                    this.txtControllingPerson.Text = BLL.UserService.GetUserNameByUserId(environmentalRiskList.ControllingPerson);
                    this.txtIdentificationDate.Text = string.Format("{0:yyyy-MM-dd}", environmentalRiskList.IdentificationDate);
                    environmentalRiskItems = (from x in new Model.SGGLDB(Funs.ConnString).View_Hazard_EnvironmentalRiskItem where x.EnvironmentalRiskListId == this.EnvironmentalRiskListId orderby x.EType, x.Code select x).ToList();
                    //if (!string.IsNullOrEmpty(Request.Params["IsImportant"]))
                    //{
                    //    environmentalRiskItems = environmentalRiskItems.Where(x => x.IsImportant == true).ToList();
                    //}
                }
                Grid1.DataSource = environmentalRiskItems;
                Grid1.DataBind();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEnvironmentalRiskListMenuId;
                this.ctlAuditFlow.DataId = this.EnvironmentalRiskListId;
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
            if (!string.IsNullOrEmpty(this.EnvironmentalRiskListId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId={1}&type=-1", this.EnvironmentalRiskListId, BLL.Const.ProjectEnvironmentalRiskListMenuId)));
            }
           
        }
        #endregion
    }
}