using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Unqualified
{
    public partial class WorkContactView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
                string workContactId = Request.Params["WorkContactId"];
                if (!string.IsNullOrEmpty(workContactId))
                {
                    Model.Unqualified_WorkContact contactList = WorkContactService.GetWorkContactByWorkContactId(workContactId);
                    txtCode.Text = contactList.Code;
                    BindGrid(workContactId);
                    var listUnit = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                    if (!string.IsNullOrEmpty(contactList.ProposedUnitId))
                    {
                        Model.Base_Unit unit = UnitService.GetUnitByUnitId(contactList.ProposedUnitId);
                        if (unit != null)
                        {
                            drpUnit.Text = unit.UnitName;
                        }
                    }

                    if (!string.IsNullOrEmpty(contactList.MainSendUnitIds))
                    {
                        if (contactList.MainSendUnitIds.Split(',').Count() == 1)
                        {
                            txtMainSendUnit.Text = UnitService.GetUnitNameByUnitId(contactList.MainSendUnitIds);
                        }
                        else
                        {
                            var lsIds = contactList.MainSendUnitIds.Split(',');
                            var list = listUnit.Where(p => lsIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                            txtMainSendUnit.Text = string.Join(",", list);
                        }


                    }
                    if (!string.IsNullOrEmpty(contactList.CCUnitIds))
                    {
                        if (contactList.CCUnitIds.Split(',').Count() == 1)
                        {
                            txtCCUnit.Text = UnitService.GetUnitNameByUnitId(contactList.CCUnitIds);
                        }
                        else
                        {
                            var lsIds = contactList.CCUnitIds.Split(',');
                            var list = listUnit.Where(p => lsIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                            txtCCUnit.Text = string.Join(",", list);
                        }
                    }
                    if (!string.IsNullOrEmpty(contactList.IsReply))
                    {
                        rblIsReply.Text = contactList.IsReply == "1" ? "需要回复" : "不需要回复";
                    }
                    txtCause.Text = contactList.Cause;
                    txtContents.Text = contactList.Contents;
                    txtReOpinion.Text = contactList.ReOpinion;


                    if (contactList.IsReply == "1")
                    {
                        HideReplyFile.Hidden = false;
                        ReOpinion.Hidden = false;

                    }
                    else
                    {
                        HideReplyFile.Hidden = true;
                        ReOpinion.Hidden = true;
                    }
                    if (!string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        Model.Unqualified_WorkContactApprove approve = WorkContactApproveService.GetSee(workContactId, CurrUser.UserId);
                        if (approve != null)
                        {
                            approve.ApproveDate = DateTime.Now;
                            WorkContactApproveService.UpdateWorkContactApprove(approve);
                        }
                    }

                }
            }
        }
        public void BindGrid(string workContactId)
        {
            var data = WorkContactApproveService.getListData(workContactId);
            gvApprove.DataSource = data;
            gvApprove.DataBind();
        }
        ///// <summary>
        ///// 设置回复审批场景下的操作
        ///// </summary>
        //public void Reply()
        //{
        //    var workContact = WorkContactService.GetWorkContactByWorkContactId(Request.Params["WorkContactId"]);
        //    string unitType = string.Empty;
        //    Model.Base_Unit unit = UnitService.GetUnit(workContact.ProposedUnitId);
        //    if (unit != null)
        //    {
        //        unitType = unit.UnitType;
        //    }
        //    string state = WorkContactService.GetWorkContactByWorkContactId(workContact.WorkContactId).State;
        //    if (unitType.Equals(Const.ProjectUnitType_1))
        //    {
        //        if (state.Equals(Const.WorkContact_Audit1) || state.Equals(Const.WorkContact_Audit1R)
        //            || state.Equals(Const.WorkContact_Audit4))
        //        {
        //            HideReplyFile.Hidden = false;
        //            ReOpinion.Hidden = false;

        //        }
        //        else
        //        {
        //            HideReplyFile.Hidden = true;
        //            ReOpinion.Hidden = true;

        //        }

        //    }
        //    if (unitType.Equals(Const.ProjectUnitType_2))
        //    {
        //        if (state.Equals(Const.WorkContact_Audit2) || state.Equals(Const.WorkContact_Audit3)
        //                || state.Equals(Const.WorkContact_Audit2R))
        //        {
        //            HideReplyFile.Hidden = false;
        //            ReOpinion.Hidden = false;

        //        }
        //        else
        //        {
        //            HideReplyFile.Hidden = true;
        //            ReOpinion.Hidden = true;

        //        }
        //    }
        //}
        protected void ReplyFile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
          String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/WorkContact&menuId={2}",
          -1, Request.Params["WorkContactId"] + "r", Const.WorkContactMenuId)));
        }

        protected void imgfile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
           String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/WorkContact&menuId={2}",
        -1, Request.Params["WorkContactId"], Const.WorkContactMenuId)));
        }
    }
}