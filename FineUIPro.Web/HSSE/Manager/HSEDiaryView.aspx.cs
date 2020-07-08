namespace FineUIPro.Web.HSSE.Manager
{
    using BLL;
    using System;
    using System.Linq;

    public partial class HSEDiaryView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HSEDiaryId
        {
            get
            {
                return (string)ViewState["HSEDiaryId"];
            }
            set
            {
                ViewState["HSEDiaryId"] = value;
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
                this.HSEDiaryId = Request.Params["HSEDiaryId"];
                if (!string.IsNullOrEmpty(this.HSEDiaryId))
                {
                    var getHSEDiary = new Model.SGGLDB(Funs.ConnString).Project_HSEDiary.FirstOrDefault(x => x.HSEDiaryId == this.HSEDiaryId);
                    if (getHSEDiary != null)
                    {                      
                        this.txtDiaryDate.Text = string.Format("{0:yyyy-MM-dd}", getHSEDiary.DiaryDate);
                        this.txtDailySummary.Text = getHSEDiary.DailySummary;
                        this.txtTomorrowPlan.Text = getHSEDiary.TomorrowPlan;
                        this.txtUserName.Text = UserService.GetUserNameByUserId(getHSEDiary.UserId);
                        this.txtValue1.Text = getHSEDiary.Value1;
                        this.txtValue2.Text = getHSEDiary.Value2;
                        this.txtValue3.Text = getHSEDiary.Value3;
                        this.txtValue4.Text = getHSEDiary.Value4;
                        this.txtValue5.Text = getHSEDiary.Value5;
                        this.txtValue6.Text = getHSEDiary.Value6;
                        this.txtValue7.Text = getHSEDiary.Value7;
                        this.txtValue8.Text = getHSEDiary.Value8;
                        this.txtValue9.Text = getHSEDiary.Value9;
                        this.txtValue10.Text = getHSEDiary.Value10;
                        if (getHSEDiary.DiaryDate.HasValue && !string.IsNullOrEmpty(getHSEDiary.UserId))
                        {
                           var getFlowOperteList= APIHSEDiaryService.ReturnFlowOperteList(getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue1.Text = APIHSEDiaryService.getValues1(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue2.Text = APIHSEDiaryService.getValues2(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue3.Text = APIHSEDiaryService.getValues3(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue4.Text = APIHSEDiaryService.getValues4(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue5.Text = APIHSEDiaryService.getValues5(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue6.Text = APIHSEDiaryService.getValues6(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue7.Text = APIHSEDiaryService.getValues7(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue8.Text = APIHSEDiaryService.getValues8(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue9.Text = APIHSEDiaryService.getValues9(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                            this.txtValue10.Text = APIHSEDiaryService.getValues10(getFlowOperteList, getHSEDiary.ProjectId, getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                        }
                    }
                }
            }
        }
        #endregion
    }
}