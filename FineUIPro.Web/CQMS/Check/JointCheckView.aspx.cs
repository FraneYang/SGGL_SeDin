using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class JointCheckView : PageBase
    {
        /// <summary>
        /// 质量共检记录主键
        /// </summary>
        public string JointCheckId
        {
            get
            {
                return (string)ViewState["JointCheckId"];
            }
            set
            {
                ViewState["JointCheckId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JointCheckId = Request.Params["JointCheckId"];
                if (!string.IsNullOrEmpty(JointCheckId))
                {
                    binData();

                }

            }
        }
        public void binData()
        {
            Model.Check_JointCheck jointCheck = BLL.JointCheckService.GetJointCheck(JointCheckId);
            txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
            txtJointCheckCode.Text = jointCheck.JointCheckCode;
            drpUnit.Text = UnitService.GetUnitNameByUnitId(jointCheck.UnitId);
            drpCheckType.Text = JointCheckService.GetCheckTypeList2().FirstOrDefault(p => p.Value.Equals(jointCheck.CheckType)).Text;
            txtCheckName.Text = jointCheck.CheckName;
            if (jointCheck.CheckDate != null)
            {
                txtCheckDate.Text = Convert.ToDateTime(jointCheck.CheckDate).ToString("yyyy-MM-dd");
            }
            drpProposeUnit.Text = UnitService.GetUnitNameByUnitId(jointCheck.ProposeUnitId);
            if (!string.IsNullOrEmpty(jointCheck.JointCheckMans1))
            {
                this.txtJointCheckMans1.Text = BLL.UserService.getUserNamesUserIds(jointCheck.JointCheckMans1);
            }
            if (!string.IsNullOrEmpty(jointCheck.JointCheckMans2))
            {
                this.txtJointCheckMans2.Text = BLL.UserService.getUserNamesUserIds(jointCheck.JointCheckMans2);
            }
            if (!string.IsNullOrEmpty(jointCheck.JointCheckMans3))
            {
                this.txtJointCheckMans3.Text = BLL.UserService.getUserNamesUserIds(jointCheck.JointCheckMans3);
            }
            if (!string.IsNullOrEmpty(jointCheck.JointCheckMans4))
            {
                this.txtJointCheckMans4.Text = BLL.UserService.getUserNamesUserIds(jointCheck.JointCheckMans4);
            }
            Model.Check_JointCheckApprove approve = BLL.JointCheckApproveService.GetJointCheckApproveByJointCheckId(JointCheckId, CurrUser.UserId);
            var list = JointCheckDetailService.GetViewLists(JointCheckId);
            gvJoinCheckDetail.DataSource = list;
            gvJoinCheckDetail.DataBind();
            var approves = JointCheckApproveService.getListData(JointCheckId);
            gvApprove.DataSource = approves;
            gvApprove.DataBind();
            if (jointCheck.State == BLL.Const.JointCheck_Complete || !string.IsNullOrEmpty(Request.Params["see"]))
            {
                //this.ImgBtnSubmit.Visible = false;
                //this.next.Visible = false;
                Model.Check_JointCheckApprove approveSee = BLL.JointCheckApproveService.GetSee(JointCheckId, this.CurrUser.UserId);
                if (approveSee != null)
                {
                    approveSee.ApproveDate = DateTime.Now;
                    BLL.JointCheckApproveService.UpdateJointCheckApprove(approveSee);
                }
            }
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.JointCheck_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit1)
                {
                    return "分包专工回复";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit2)
                {
                    return "分包负责人审批";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit3)
                {
                    return "总包专工回复";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit4)
                {
                    return "总包负责人审批";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Z)
                {
                    return "整改中";
                }
                else if (state.ToString() == BLL.Const.JointCheck_Audit1R)
                {
                    return "分包专工重新回复";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        protected string ConvertMan(object handleMan)
        {
            if (handleMan != null)
            {
                Model.Sys_User user = BLL.UserService.GetUserByUserId(handleMan.ToString());
                if (user != null)
                {
                    return user.UserName;
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        /// <summary>
        /// 明细集合
        /// </summary>
        private static List<Model.Check_JointCheckDetail> jointCheckDetails = new List<Model.Check_JointCheckDetail>();

        protected void gvJoinCheckDetail_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = gvJoinCheckDetail.DataKeys[e.RowIndex][0].ToString();

            if (e.CommandName == "ReAttachUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/JointCheck&menuId={2}&edit=1", "-1", itemId + "r", Const.JointCheckMenuId)));

            }
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type=-1&toKeyId={0}&path=FileUpload/JointCheck&menuId={1}&edit=1", itemId, BLL.Const.JointCheckMenuId)));
            }
        }
    }
}