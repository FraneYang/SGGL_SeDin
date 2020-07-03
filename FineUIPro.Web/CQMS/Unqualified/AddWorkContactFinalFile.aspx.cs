using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Unqualified
{
    public partial class AddWorkContactFinalFile : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string WorkContactId
        {
            get
            {
                return (string)ViewState["WorkContactId"];
            }
            set
            {
                ViewState["WorkContactId"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpUnit, CurrUser.LoginProjectId, false);
                //主送单位
                gvMainSendUnit.DataSource = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                gvMainSendUnit.DataBind();
                //抄送单位
                gvCCUnit.DataSource = UnitService.GetUnitByProjectIdList(CurrUser.LoginProjectId);
                gvCCUnit.DataBind();

                txtCode.Text = SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Unqualified_WorkContact", "Code", CurrUser.LoginProjectId);
                ContactImg = 0;
                //string workContactId = Request.Params["WorkContactId"];
                //if (!string.IsNullOrEmpty(workContactId)) {
                //    //ContactImg = -1;
                //    //this.btnSave.Hidden = true;
                //    //txtCode.Enabled = false;
                //    //drpUnit.Enabled = false;
                //    //txtMainSendUnit.Enabled = false;
                //    //txtCCUnit.Enabled = false;
                //    //txtCompileDate.Enabled = false;
                //    //txtCause.Enabled = false;
                //    //lbfile.Enabled = false;
                //    Model.Unqualified_WorkContact workContact = WorkContactService.GetWorkContactByWorkContactId(workContactId);
                //    drpUnit.SelectedValue = workContact.ProposedUnitId;
                //    txtCode.Text = workContact.Code;
                //    if (!string.IsNullOrEmpty(workContact.MainSendUnitIds)) {
                //        txtMainSendUnit.Values = workContact.MainSendUnitIds.Split(',');
                //    }
                //    if (!string.IsNullOrEmpty(workContact.CCUnitIds))
                //    {
                //        txtCCUnit.Values = workContact.CCUnitIds.Split(',');
                //    }
                //    if (workContact.CompileDate != null) {
                //        txtCompileDate.Text = workContact.CompileDate.ToString();
                //    }
                //    txtCause.Text = workContact.Cause;
                //}
            }
        }

        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WorkContactId))
            {
                WorkContactId = SQLHelper.GetNewID(typeof(Model.Unqualified_WorkContact));
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/WorkContact&menuId={2}",
            ContactImg, WorkContactId, Const.WorkContactMenuId)));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Unqualified_WorkContact workContact = new Model.Unqualified_WorkContact();
            workContact.Code = txtCode.Text.Trim();
            workContact.ProjectId = CurrUser.LoginProjectId;
            if (drpUnit.SelectedValue != "0")
            {
                workContact.ProposedUnitId = drpUnit.SelectedValue;
            }
            workContact.MainSendUnitIds = string.Join(",", txtMainSendUnit.Values);
            workContact.CCUnitIds = string.Join(",", txtCCUnit.Values);
            workContact.State = Const.WorkContact_Complete;
            workContact.Cause = txtCause.Text.Trim();
            workContact.CompileMan = CurrUser.UserId;
            workContact.IsReply = rblIsReply.SelectedValue;
            if (!string.IsNullOrEmpty(txtCompileDate.Text.Trim()))
            {
                workContact.CompileDate = Convert.ToDateTime(txtCompileDate.Text.Trim());
            }
            workContact.IsFinal = true;  //定稿文件
            if (string.IsNullOrEmpty(WorkContactId))
            {
                WorkContactId = SQLHelper.GetNewID(typeof(Model.Unqualified_WorkContact));
            }
            workContact.WorkContactId = WorkContactId;
            WorkContactService.AddWorkContact(workContact);
            LogService.AddSys_Log(CurrUser, workContact.Code, workContact.WorkContactId, Const.WorkContactMenuId, "添加工作联系单定稿文件");
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //Alert.ShowInTop("保存成功！", MessageBoxIcon.Success);
        }
    }
}