using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class TechnicalContactView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
                string technicalContactListId = Request.Params["TechnicalContactListId"];
                if (!string.IsNullOrEmpty(technicalContactListId))
                {
                    Model.Check_TechnicalContactList technicalContactList = TechnicalContactListService.GetTechnicalContactListByTechnicalContactListId(technicalContactListId);
                    string unitType = string.Empty;
                    txtCode.Text = technicalContactList.Code;
                    BindGrid(technicalContactListId);
                    if (!string.IsNullOrEmpty(technicalContactList.ProposedUnitId))
                    {
                        Model.Project_ProjectUnit unit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId,technicalContactList.ProposedUnitId);
                        Model.Base_Unit unit2 = BLL.UnitService.GetUnitByUnitId(technicalContactList.ProposedUnitId);
                        if(unit2!=null)
                        {
                            drpProposeUnit.Text = unit2.UnitName;
                        }
                        if (unit != null)
                        {
                            unitType = unit.UnitType;
                        }
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.UnitWorkId))
                    {
                        txtUnitWork.Text = UnitWorkService.GetUnitWorkName(technicalContactList.UnitWorkId);
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.CNProfessionalCode))
                    {
                        txtCNProfessional.Text = CNProfessionalService.GetCNProfessionalNameByCode(technicalContactList.CNProfessionalCode);
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.MainSendUnitId))
                    {
                        txtMainSendUnit.Text = UnitService.GetUnitNameByUnitId(technicalContactList.MainSendUnitId);
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.CCUnitIds))
                    {
                        List<string> units = technicalContactList.CCUnitIds.Split(',').ToList();
                        string unit = string.Empty;
                        foreach (var item in units)
                        {
                            unit += UnitService.GetUnitByUnitId(item).UnitName + ",";
                        }
                        if (!string.IsNullOrEmpty(unit))
                        {
                            txtCCUnit.Text = unit.Substring(0, unit.LastIndexOf(","));

                        }
                    }
                    string contactListType = technicalContactList.ContactListType;
                    string isReply = technicalContactList.IsReply;
                    if (!string.IsNullOrEmpty(technicalContactList.ContactListType))
                    {
                        rblContactListType.Text = technicalContactList.ContactListType == "1" ? "图纸类" : "非图纸类";
                    }
                    if (!string.IsNullOrEmpty(technicalContactList.IsReply))
                    {
                        rblIsReply.Text = technicalContactList.IsReply == "1" ? "需要回复" : "不需回复";
                    }
                    txtCause.Text = technicalContactList.Cause;
                    txtContents.Text = technicalContactList.Contents;
                    txtReOpinion.Text = technicalContactList.ReOpinion;
                    if (!string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        Model.Check_TechnicalContactListApprove approve = TechnicalContactListApproveService.GetSee(technicalContactListId, CurrUser.UserId);
                        if (approve != null)
                        {
                            approve.ApproveDate = DateTime.Now;
                            TechnicalContactListApproveService.UpdateTechnicalContactListApprove(approve);
                        }
                    }
                    if (technicalContactList.IsReply != "1")  //不需回复
                    {
                        this.ReOpinion.Hidden = true;
                        this.HideReplyFile.Hidden = true;
                    }
                    this.plReFile.Hidden = true;
                    if (unitType == BLL.Const.ProjectUnitType_2)  //分包发起
                    {
                        var file = from x in Funs.DB.AttachFile where x.ToKeyId == technicalContactListId + "re" select x;
                        if (file.Count() > 0)
                        {
                            this.plReFile.Hidden = false;
                        }
                    }
                }
            }
        }
        public void BindGrid(string technicalContactListId)
        {
            var data = TechnicalContactListApproveService.getListData(technicalContactListId);
            gvApprove.DataSource = data;
            gvApprove.DataBind();
        }

        protected void imgfile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/TechnicalContactList&menuId={1}", Request.Params["TechnicalContactListId"], Const.TechnicalContactListMenuId)));


        }

        protected void ReplyFile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type=-1&toKeyId={0}&path=FileUpload/TechnicalContactList&menuId={1}", Request.Params["TechnicalContactListId"] + "r", Const.TechnicalContactListMenuId)));
        }

        protected void imgBtnReFile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type=-1&toKeyId={0}&path=FileUpload/TechnicalContactList&menuId={1}", Request.Params["TechnicalContactListId"] + "re", Const.TechnicalContactListMenuId)));
        }
    }
}