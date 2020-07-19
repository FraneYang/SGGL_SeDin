using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class CheckSpecialView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckSpecialId
        {
            get
            {
                return (string)ViewState["CheckSpecialId"];
            }
            set
            {
                ViewState["CheckSpecialId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_CheckSpecialDetail> checkSpecialDetails = new List<Model.View_CheckSpecialDetail>();
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
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                checkSpecialDetails.Clear();

                this.CheckSpecialId = Request.Params["CheckSpecialId"];
                var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(this.CheckSpecialId);
                if (checkSpecial != null)
                {
                    this.txtCheckSpecialCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckSpecialId);
                    if (checkSpecial.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkSpecial.CheckTime);
                    }
                   
                    this.txtPartInPersonNames.Text = checkSpecial.PartInPersonNames;
                    this.txtSupCheckItemSet.Text = Technique_CheckItemSetService.GetCheckItemSetNameById(checkSpecial.CheckItemSetId);
                    this.txtPartInPersons.Text = checkSpecial.PartInPersons;
                    checkSpecialDetails = (from x in Funs.DB.View_CheckSpecialDetail
                                           where x.CheckSpecialId == this.CheckSpecialId
                                           orderby x.SortIndex select x).ToList();
                }
                Grid1.DataSource = checkSpecialDetails;
                Grid1.DataBind();
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
            if (!string.IsNullOrEmpty(this.CheckSpecialId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckSpecial&menuId={1}&type=-1", this.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId)));
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "click")
            {
                var detail = Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(Grid1.DataKeys[e.RowIndex][0].ToString());
                if (detail != null)
                {
                    if (detail.DataType == "1")
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesView.aspx?RectifyNoticesId={0}", detail.DataId, "查看 - ")));
                    }
                    else if (detail.DataType == "2")
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishNoticeView.aspx?PunishNoticeId={0}", detail.DataId, "查看 - ")));
                    }
                    else if (detail.DataType == "3")
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeView.aspx?PauseNoticeId={0}", detail.DataId, "查看 - ")));
                    }
                }
            }
        }
    }
}