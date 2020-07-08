using BLL;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencyTeamAndTrainView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FileId
        {
            get
            {
                return (string)ViewState["FileId"];
            }
            set
            {
                ViewState["FileId"] = value;
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
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain = BLL.EmergencyTeamAndTrainService.GetEmergencyTeamAndTrainById(this.FileId);
                    if (EmergencyTeamAndTrain != null)
                    {
                        ///读取编号
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = EmergencyTeamAndTrain.FileName;
                        this.txtUnit.Text = BLL.UnitService.GetUnitNameByUnitId(EmergencyTeamAndTrain.UnitId);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", EmergencyTeamAndTrain.CompileDate);
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(EmergencyTeamAndTrain.CompileMan);
                        //this.txtFileContent.Text = HttpUtility.HtmlDecode(EmergencyTeamAndTrain.FileContent);                        
                        Grid1.DataSource = (from x in new Model.SGGLDB(Funs.ConnString).View_Emergency_EmergencyTeamItem
                                            where x.FileId == this.FileId
                                            select x).ToList();
                        Grid1.DataBind();
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEmergencyTeamAndTrainMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
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
            if (!string.IsNullOrEmpty(this.FileId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EmergencyTeamAndTrainAttachUrl&menuId={1}&type=-1", FileId, BLL.Const.ProjectEmergencyTeamAndTrainMenuId)));
            }            
        }
        #endregion
    }
}