using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Unqualified
{
    public partial class WorkContactFinalFileView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string workContactId = Request.Params["WorkContactId"];
                if (!string.IsNullOrEmpty(workContactId))
                {
                    //ContactImg = -1;
                    //this.btnSave.Hidden = true;
                    //txtCode.Enabled = false;
                    //drpUnit.Enabled = false;
                    //txtMainSendUnit.Enabled = false;
                    //txtCCUnit.Enabled = false;
                    //txtCompileDate.Enabled = false;
                    //txtCause.Enabled = false;
                    //lbfile.Enabled = false;
                    var listUnit = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                    Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactId(workContactId);
                    drpUnit.Text = workContact.ProposedUnitId;

                    if (!string.IsNullOrEmpty(workContact.ProposedUnitId))
                    {
                        Model.Base_Unit unit = UnitService.GetUnitByUnitId(workContact.ProposedUnitId);
                        if (unit != null)
                        {
                            drpUnit.Text = unit.UnitName;
                        }
                    }

                    txtCode.Text = workContact.Code;
                    if (!string.IsNullOrEmpty(workContact.MainSendUnitIds))
                    {
                        if (workContact.MainSendUnitIds.Split(',').Count() == 1)
                        {
                            txtMainSendUnit.Text = UnitService.GetUnitNameByUnitId(workContact.MainSendUnitIds);
                        }
                        else
                        {
                            var lsIds = workContact.MainSendUnitIds.Split(',');
                            var list = listUnit.Where(p => lsIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                            txtMainSendUnit.Text = string.Join(",", list);
                        }


                    }
                    if (!string.IsNullOrEmpty(workContact.CCUnitIds))
                    {
                        if (workContact.CCUnitIds.Split(',').Count() == 1)
                        {
                            txtCCUnit.Text = UnitService.GetUnitNameByUnitId(workContact.CCUnitIds);
                        }
                        else
                        {
                            var lsIds = workContact.CCUnitIds.Split(',');
                            var list = listUnit.Where(p => lsIds.Contains(p.UnitId)).Select(p => p.UnitName).ToArray();
                            txtCCUnit.Text = string.Join(",", list);
                        }
                    }
                    if (workContact.CompileDate != null)
                    {
                        txtCompileDate.Text = Convert.ToDateTime(workContact.CompileDate).ToString("yyyy-MM-dd");
                    }
                    txtIsReply.Text = workContact.IsReply == "1" ? "需要答复" : "不需要答复";
                    txtCause.Text = workContact.Cause;
                }
            }
        }

        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/WorkContact&menuId={2}",
            -1, Request.Params["WorkContactId"], Const.WorkContactMenuId)));
        }
    }
}