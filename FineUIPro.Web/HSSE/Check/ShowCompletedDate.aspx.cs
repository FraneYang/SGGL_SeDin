using BLL;
using System;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class ShowCompletedDate : PageBase
    {
        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                this.txtCompletedDate.Text = string.Format("{0:yyyy-MM-dd}",DateTime.Now);
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Request.Params["CheckDayDetailId"]))
            //{
            //    Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(Request.Params["CheckDayDetailId"]);
            //    if (detail != null)
            //    {
            //        detail.CompleteStatus = true;
            //        detail.CompletedDate = Funs.GetNewDateTime(this.txtCompletedDate.Text.Trim());
            //        BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
            //    }
            //}
            //else if (!string.IsNullOrEmpty(Request.Params["CheckSpecialDetailId"]))
            //{
            //    Model.Check_CheckSpecialDetail detail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(Request.Params["CheckSpecialDetailId"]);
            //    if (detail != null)
            //    {
            //        detail.CompleteStatus = true;
            //        detail.CompletedDate = Funs.GetNewDateTime(this.txtCompletedDate.Text.Trim());
            //        BLL.Check_CheckSpecialDetailService.UpdateCheckSpecialDetail(detail);
            //    }
            //}
             if (!string.IsNullOrEmpty(Request.Params["CheckColligationDetailId"]))
            {
                Model.Check_CheckColligationDetail detail = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(Request.Params["CheckColligationDetailId"]);
                if (detail != null)
                {
                    detail.CompleteStatus = true;
                    detail.CompletedDate = Funs.GetNewDateTime(this.txtCompletedDate.Text.Trim());
                    BLL.Check_CheckColligationDetailService.UpdateCheckColligationDetail(detail);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}