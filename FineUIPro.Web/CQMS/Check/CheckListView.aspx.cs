using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class CheckListView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckControlCode
        {
            get
            {
                return (string)ViewState["CheckControlCode"];
            }
            set
            {
                ViewState["CheckControlCode"] = value;
            }
        }

        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
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
                if (state.ToString() == BLL.Const.CheckControl_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit1)
                {
                    return "总包负责人审批";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit2)
                {
                    return "分包专业工程师回复";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit3)
                {
                    return "分包负责人审批";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit4)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Audit5)
                {
                    return "总包负责人确认";
                }
                else if (state.ToString() == BLL.Const.CheckControl_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == BLL.Const.CheckControl_ReCompile2)
                {
                    return "分包专业工程师重新回复";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string checkControlCode = Request.Params["CheckControlCode"];
                CheckControlCode = checkControlCode;
                plApprove.Hidden = true;
                if (!string.IsNullOrEmpty(checkControlCode))
                {
                    plApprove.Hidden = false;
                    var dt = CheckControlApproveService.getListData(CheckControlCode);
                    gvApprove.DataSource = dt;
                    gvApprove.DataBind();
                    Model.Check_CheckControl checkControl = CheckControlService.GetCheckControl(CheckControlCode);
                    txtDocCode.Text = checkControl.DocCode;
                    if (!string.IsNullOrEmpty(checkControl.UnitId))
                    {
                        drpUnit.Text = UnitService.GetUnitNameByUnitId(checkControl.UnitId);
                    }
                    if (!string.IsNullOrEmpty(checkControl.ProposeUnitId))
                    {
                        drpProposeUnit.Text = UnitService.GetUnitNameByUnitId(checkControl.ProposeUnitId);
                    }
                    if (checkControl.UnitWorkId != null)
                    {
                        //checkControl.UnitWorkId.ToString();
                        drpUnitWork.Text = UnitWorkService.GetNameById(checkControl.UnitWorkId);
                    }
                    if (!string.IsNullOrEmpty(checkControl.CNProfessionalCode))
                    {
                        drpCNProfessional.Text = CNProfessionalService.GetCNProfessional(checkControl.CNProfessionalCode).ProfessionalName;
                    }
                    if (!string.IsNullOrEmpty(checkControl.QuestionType))
                    {
                        var questionType = QualityQuestionTypeService.GetQualityQuestionType(checkControl.QuestionType);
                        if (questionType != null)
                        {
                            drpQuestionType.Text = questionType.QualityQuestionType;
                        }
                    }
                    this.txtCheckSite.Text = checkControl.CheckSite;
                    if (checkControl.CheckDate != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkControl.CheckDate);
                    }
                    txtQuestionDef.Text = checkControl.QuestionDef;
                    txtRectifyOpinion.Text = checkControl.RectifyOpinion;
                    if (checkControl.LimitDate != null)
                    {
                        txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", checkControl.LimitDate);
                    }
                    txtHandleWay.Text = checkControl.HandleWay;
                    if (checkControl.RectifyDate != null)
                    {
                        txtRectifyDate.Text = string.Format("{0:yyyy-MM-dd}", checkControl.RectifyDate);
                    }
                    Model.Check_CheckControlApprove approve = BLL.CheckControlApproveService.GetSee(CheckControlCode, this.CurrUser.UserId);
                    if (approve != null)
                    {
                        approve.ApproveDate = DateTime.Now;
                        BLL.CheckControlApproveService.UpdateCheckControlApprove(approve);
                    }
                }
                else
                {
                    txtDocCode.Text = "";
                    State = Const.CheckControl_Compile;
                    CheckControlCode = SQLHelper.GetNewID(typeof(Model.Check_CheckControl));
                    //this.drpHandleType.Items.AddRange(BLL.CheckControlService.GetDHandleTypeByState(State));
                    //this.drpHandleMan.Items.AddRange(BLL.UserService.GetAllUserList(this.CurrUser.LoginProjectId));
                    txtHandleWay.Enabled = false;
                    txtRectifyDate.Enabled = false;
                    //this.imgfileRe.Visible = false;
                }

                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;


            }
        }


        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttach_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/CheckControl&menuId={1}", this.CheckControlCode + "r", BLL.Const.CheckListMenuId)));
        }
        #endregion

        protected void imgBtnFile_Click(object sender, EventArgs e)
        {

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/CheckControl&menuId={1}", CheckControlCode, BLL.Const.CheckListMenuId)));
        }
    }
}