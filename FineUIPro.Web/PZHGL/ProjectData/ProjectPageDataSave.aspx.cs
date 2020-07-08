using BLL;
using Model;
using System;

namespace FineUIPro.Web.PZHGL.ProjectData
{
    public partial class ProjectPageDataSave : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string PageDataId
        {
            get
            {
                return (string)ViewState["PageDataId"];
            }
            set
            {
                ViewState["PageDataId"] = value;
            }
        }

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
                this.PageDataId = Request.Params["PageDataId"];
                if (!String.IsNullOrEmpty(this.PageDataId))
                {
                    var getPageData = ProjectPageDataService.GetPageDataByPageDataId(this.PageDataId);
                    if (getPageData != null)
                    {
                        this.txtSafeHours.Text = getPageData.SafeHours.ToString();
                        this.txtSitePersonNum .Text = getPageData.SitePersonNum.ToString();
                        this.txtSpecialEquipmentNum .Text = getPageData.SpecialEquipmentNum.ToString();
                        this.txtEntryTrainingNum .Text = getPageData.EntryTrainingNum.ToString();
                        this.txtHiddenDangerNum .Text = getPageData.HiddenDangerNum.ToString();
                        this.txtRectificationNum .Text = getPageData.RectificationNum.ToString();
                        this.txtRiskI .Text = getPageData.RiskI.ToString();
                        this.txtRiskII .Text = getPageData.RiskII.ToString();
                        this.txtRiskIII .Text = getPageData.RiskIII.ToString();
                        this.txtRiskIV .Text = getPageData.RiskIV.ToString();
                        this.txtRiskV .Text = getPageData.RiskV.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Wx_PageData project = new Wx_PageData
            {
                ProjectId = this.CurrUser.LoginProjectId,
                CreatDate = DateTime.Now,
                CreatManId = this.CurrUser.UserId,
                SafeHours = Funs.GetNewIntOrZero(this.txtSafeHours.Text),
                SitePersonNum = Funs.GetNewIntOrZero(this.txtSitePersonNum.Text),
                SpecialEquipmentNum = Funs.GetNewIntOrZero(this.txtSpecialEquipmentNum.Text),
                EntryTrainingNum = Funs.GetNewIntOrZero(this.txtEntryTrainingNum.Text),
                HiddenDangerNum = Funs.GetNewIntOrZero(this.txtHiddenDangerNum.Text),
                RectificationNum = Funs.GetNewIntOrZero(this.txtRectificationNum.Text),
                RiskI = Funs.GetNewIntOrZero(this.txtRiskI.Text),
                RiskII = Funs.GetNewIntOrZero(this.txtRiskII.Text),
                RiskIII = Funs.GetNewIntOrZero(this.txtRiskIII.Text),
                RiskIV = Funs.GetNewIntOrZero(this.txtRiskIV.Text),
                RiskV = Funs.GetNewIntOrZero(this.txtRiskV.Text),
            };
          
            if (String.IsNullOrEmpty(this.PageDataId))
            {
                project.PageDataId = SQLHelper.GetNewID(typeof(Model.Wx_PageData));
                this.PageDataId = project.PageDataId;
                BLL.ProjectPageDataService.AddPageData(project);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Format("{0:yyyy-MM-dd}", project.CreatDate) , project.PageDataId, BLL.Const.ProjectPageDataMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                project.PageDataId = this.PageDataId;
                BLL.ProjectPageDataService.UpdatePageData(project);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Format("{0:yyyy-MM-dd}", project.CreatDate), project.PageDataId, BLL.Const.ProjectPageDataMenuId, BLL.Const.BtnModify);
            }           
            ShowNotify("保存数据成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}