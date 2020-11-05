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
    public partial class ConstructionPlanView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ConstructionPlanId
        {
            get
            {
                return (string)ViewState["ConstructionPlanId"];
            }
            set
            {
                ViewState["ConstructionPlanId"] = value;
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
                ConstructionPlanId = Request.Params["ConstructionPlanId"];
                BindData();
                if (!string.IsNullOrEmpty(ConstructionPlanId))
                {
                    HFConstructionPlanId.Text = ConstructionPlanId;
                    Model.ZHGL_ConstructionPlan constructionPlan = ConstructionPlanService.GetConstructionPlanById(ConstructionPlanId);
                    string unitType = string.Empty;
                    txtCode.Text = constructionPlan.Code;
                    if (constructionPlan.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructionPlan.CompileDate);
                    }
                    this.txtContent.Text = HttpUtility.HtmlDecode(constructionPlan.Content);
                    if (!string.IsNullOrEmpty(constructionPlan.State))
                    {
                        State = constructionPlan.State;
                    }
                    else
                    {
                        State = Const.ConstructionPlan_Compile;
                        //Url.Visible = false;//附件查看权限-1
                        ContactImg = -1;
                    }
                    if (State == Const.ConstructionPlan_Compile || State == Const.ConstructionPlan_ReCompile)
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
            var table = ConstructionPlanApproveService.getListData(ConstructionPlanId);
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
            if (string.IsNullOrEmpty(HFConstructionPlanId.Text))   //新增记录
            {
                HFConstructionPlanId.Text = SQLHelper.GetNewID(typeof(Model.ZHGL_ConstructionPlan));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/ConstructionPlan&menuId={2}",
            ContactImg, HFConstructionPlanId.Text, Const.ConstructionPlanMenuId)));
        }
    }
}