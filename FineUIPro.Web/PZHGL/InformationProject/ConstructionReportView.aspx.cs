using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class ConstructionReportView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ConstructionReportId
        {
            get
            {
                return (string)ViewState["ConstructionReportId"];
            }
            set
            {
                ViewState["ConstructionReportId"] = value;
            }
        }
        public int ContactImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ContactImg"]);
            }
            set
            {
                ViewState["ContactImg"] = value;
            }
        }


        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConstructionReportId = Request.Params["ConstructionReportId"];
                BindData();
                if (!string.IsNullOrEmpty(ConstructionReportId))
                {
                    HFConstructionReportId.Text = ConstructionReportId;
                    Model.ZHGL_ConstructionReport constructionReport = ConstructionReportService.GetConstructionReportById(ConstructionReportId);
                    string unitType = string.Empty;
                    txtCode.Text = constructionReport.Code;
                    if (!string.IsNullOrEmpty(constructionReport.FileType))
                    {
                        this.txtFileType.Text = BLL.ConstructionReportService.ConvertFileType(constructionReport.FileType);
                    }
                    if (constructionReport.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructionReport.CompileDate);
                    }
                    this.txtContent.Text = HttpUtility.HtmlDecode(constructionReport.Content);
                    if (!string.IsNullOrEmpty(constructionReport.State))
                    {
                        State = constructionReport.State;
                    }
                    else
                    {
                        State = Const.ConstructionReport_Compile;
                        //Url.Visible = false;//附件查看权限-1
                        ContactImg = -1;
                    }
                    if (State == Const.ConstructionReport_Compile || State == Const.ConstructionReport_ReCompile)
                    {
                        ContactImg = 0;
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                    }
                    else
                    {
                        //------------
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                        //Url.Visible = true; 附件查看权限 - 1
                        ContactImg = -1;
                    }
                    //设置回复审批场景下的操作
                }
            }
        }

        private void BindData()
        {
            var table = ConstructionReportApproveService.getListData(ConstructionReportId);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 附件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFConstructionReportId.Text))   //新增记录
            {
                HFConstructionReportId.Text = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionReport));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/ConstructionReport&menuId={2}",
            ContactImg, HFConstructionReportId.Text, Const.ConstructionReportMenuId)));
        }
    }
}