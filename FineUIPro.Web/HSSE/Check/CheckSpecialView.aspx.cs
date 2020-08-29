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
                    if (!String.IsNullOrEmpty(checkSpecial.CheckType))
                    {
                        this.rbType.SelectedValue = checkSpecial.CheckType;
                    }
                    this.txtPartInPersonNames.Text = checkSpecial.PartInPersonNames;
                    this.txtSupCheckItemSet.Text = Technique_CheckItemSetService.GetCheckItemSetNameById(checkSpecial.CheckItemSetId);
                    this.txtPartInPersons.Text = checkSpecial.PartInPersons;
                    checkSpecialDetails = (from x in Funs.DB.View_CheckSpecialDetail
                                           where x.CheckSpecialId == this.CheckSpecialId
                                           select x).ToList();
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
            string checkSpecialDetailId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "click")
            {
                var detail = Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(Grid1.DataKeys[e.RowIndex][0].ToString());
                if (detail != null)
                {
                    List<string> getList = Funs.GetStrListByStr(detail.DataId,'|');
                    foreach (var item in getList)
                    {
                        List<string> getItemList = Funs.GetStrListByStr(item, ',');
                        if (getItemList.Count() > 1)
                        {
                            if (getItemList[0].ToString() == "1")
                            {
                                var getRe = RectifyNoticesService.GetRectifyNoticesById(getItemList[1].ToString());
                                if (getRe != null)
                                {
                                    if (getRe.CompleteManId ==this.CurrUser.UserId &&( string.IsNullOrEmpty(getRe.States) || getRe.States == Const.State_0))
                                    {
                                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesAdd.aspx?RectifyNoticesId={0}", getRe.RectifyNoticesId, "编辑 - ")));
                                    }
                                    else
                                    {
                                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesView.aspx?RectifyNoticesId={0}", getRe.RectifyNoticesId, "查看 - ")));
                                    }
                                }
                            }
                            if (getItemList[0].ToString() == "2")
                            {
                                var getpu = PunishNoticeService.GetPunishNoticeById(getItemList[1].ToString());
                                if (getpu.CompileMan == this.CurrUser.UserId && (string.IsNullOrEmpty(getpu.PunishStates) || getpu.PunishStates == Const.State_0))
                                {
                                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishNoticeEdit.aspx?PunishNoticeId={0}", getpu.PunishNoticeId, "编辑 - ")));
                                }
                                else
                                {
                                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishNoticeView.aspx?PunishNoticeId={0}", getpu.PunishNoticeId, "查看 - ")));
                                }
                            }
                            if (getItemList[0].ToString() == "3")
                            {
                                var getpau = Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(getItemList[1].ToString());
                                if (getpau.CompileManId == this.CurrUser.UserId && (string.IsNullOrEmpty(getpau.PauseStates) || getpau.PauseStates == Const.State_0))
                                {
                                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeEdit.aspx?PauseNoticeId={0}", getpau.PauseNoticeId, "编辑 - ")));
                                }
                                else
                                {
                                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeView.aspx?PauseNoticeId={0}", getpau.PauseNoticeId, "查看 - ")));
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
          var  details = (from x in Funs.DB.View_CheckSpecialDetail
                                   where x.CheckSpecialId == this.CheckSpecialId
                                   select x).ToList();
            Grid1.DataSource = details;
            Grid1.DataBind();
        }
    }
}