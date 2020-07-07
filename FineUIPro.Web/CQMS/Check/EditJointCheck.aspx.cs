using Apache.NMS.ActiveMQ.Threads;
using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditJointCheck : PageBase
    {
        /// <summary>
        /// 主键
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, false);
                UnitService.InitUnitNotsub(drpProposeUnit, CurrUser.LoginProjectId, false);
                JointCheckService.Init(drpCheckType, false);
                txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
                JointCheckId = Request.Params["JointCheckId"];
                //this.HideOptions.Visible = false;
                //this.Url.Visible = false;
                //this.rblIsAgree.Visible = false;
                if (string.IsNullOrEmpty(JointCheckId))
                {
                    string prefix = "T18006-JC-";
                    txtJointCheckCode.Text = BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.Check_JointCheck", "JointCheckCode", prefix);
                    State = Const.JointCheck_Compile;

                    //drpHandleType.Items.AddRange(listitem);
                    //this.drpHandleType.Items.AddRange(BLL.JointCheckService.GetDHandleTypeByState(State));
                    //this.drp_HandleMan.Items.AddRange(BLL.UserService.GetAllUserList(this.CurrUser.LoginProjectId));
                    //UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                    //drpHandleMan.SelectedIndex = 0;
                    //drpHandleType.SelectedIndex = 0;
                    string questionImg = "0";
                    var str = String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/CheckControl&menuId={2}", questionImg, JointCheckId, Const.JointCheckMenuId);
                    //imgAttachUrl.OnClientClick=Window1.GetShowReference(str) + "return false;";
                    var mainUnit = BLL.UnitService.GetUnitByProjectIdUnitTypeList(this.CurrUser.LoginProjectId,Const.ProjectUnitType_1)[0];
                    if (mainUnit != null)
                    {
                        this.drpProposeUnit.SelectedValue = mainUnit.UnitId;
                    }
                    //txtCheckName.Text = CheckControlService.GetValByText("总包负责人审核", State);
                }
                //this.drpHandleType.Enabled = true;

            }
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {

        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {

        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {

        }



        protected void addList()
        {
            jointCheckDetails.Clear();

            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                Model.Check_JointCheckDetail jointCheckDetail = new Model.Check_JointCheckDetail();
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                string JointCheckDetailId = values.Value<string>("JointCheckDetailId");
                AspNet.DropDownList drpUnitWork = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpUnitWork");
                AspNet.DropDownList drpCNProfessional = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpCNProfessional");
                string CheckSite = values.Value<string>("CheckSite");
                string QuestionDef = values.Value<string>("QuestionDef");
                AspNet.DropDownList drpQuestionType = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpQuestionType");
                string RectifyOpinion = values.Value<string>("RectifyOpinion");
                string AttachUrl = values.Value<string>("AttachUrl");
                AspNet.TextBox LimitDate = (AspNet.TextBox)Grid1.Rows[i].FindControl("LimitDate");
                string state = values.Value<string>("State");
                AspNet.DropDownList drpHandleMan = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpHandleMan");
                string createDate = values.Value<string>("CreateDate");
                jointCheckDetail.JointCheckDetailId = JointCheckDetailId;
                jointCheckDetail.UnitWorkId = drpUnitWork.SelectedValue;
                jointCheckDetail.CNProfessionalCode = drpCNProfessional.SelectedValue;
                jointCheckDetail.CheckSite = CheckSite;
                jointCheckDetail.QuestionDef = QuestionDef;
                jointCheckDetail.QuestionType = drpQuestionType.SelectedValue;
                if (!string.IsNullOrEmpty(createDate))
                {
                    jointCheckDetail.CreateDate = Convert.ToDateTime(createDate);
                }
                //jointCheckDetail.Standard = Standard;
                jointCheckDetail.RectifyOpinion = RectifyOpinion;
                if (!string.IsNullOrWhiteSpace(LimitDate.Text.Trim()))
                {
                    jointCheckDetail.LimitDate = Convert.ToDateTime(LimitDate.Text.Trim());
                }
                jointCheckDetail.AttachUrl = AttachUrl;
                //jointCheckDetail.HandleWay = HandleWay;
                //jointCheckDetail.ReAttachUrl = ReUrl;
                jointCheckDetail.State = Const.JointCheck_Audit1;
                jointCheckDetail.HandleMan = drpHandleMan.SelectedValue;
                jointCheckDetails.Add(jointCheckDetail);
            }

        }

        /// <summary>
        /// 绑定表格保存到数据使用
        /// </summary>
        protected void addGvList()
        {
            jointCheckDetails.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                Model.Check_JointCheckDetail jointCheckDetail = new Model.Check_JointCheckDetail();
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                string JointCheckDetailId = values.Value<string>("JointCheckDetailId");
                AspNet.DropDownList drpUnitWork = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpUnitWork");
                AspNet.DropDownList drpCNProfessional = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpCNProfessional");
                string CheckSite = values.Value<string>("CheckSite");
                string QuestionDef = values.Value<string>("QuestionDef");
                AspNet.DropDownList drpQuestionType = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpQuestionType");
                string RectifyOpinion = values.Value<string>("RectifyOpinion");
                string AttachUrl = values.Value<string>("AttachUrl");
                AspNet.TextBox LimitDate = (AspNet.TextBox)Grid1.Rows[i].FindControl("LimitDate");
                string state = values.Value<string>("State");
                AspNet.DropDownList drpHandleMan = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpHandleMan");
                string createDate = values.Value<string>("CreateDate");
                if (createDate != null)
                {
                    jointCheckDetail.CreateDate = Convert.ToDateTime(createDate);
                }
                jointCheckDetail.JointCheckDetailId = JointCheckDetailId;
                //jointCheckDetail.JointCheckId = handleMan;
                if (drpUnitWork.SelectedValue != "0")
                {
                    jointCheckDetail.UnitWorkId = drpUnitWork.SelectedValue;
                }
                if (drpCNProfessional.SelectedValue != "0")
                {
                    jointCheckDetail.CNProfessionalCode = drpCNProfessional.SelectedValue;
                }
                jointCheckDetail.CheckSite = CheckSite;
                jointCheckDetail.QuestionDef = QuestionDef;
                if (drpQuestionType.SelectedValue != "0")
                {
                    jointCheckDetail.QuestionType = drpQuestionType.SelectedValue;
                }
                //jointCheckDetail.Standard = Standard;
                jointCheckDetail.RectifyOpinion = RectifyOpinion;
                if (!string.IsNullOrWhiteSpace(LimitDate.Text.Trim()))
                {
                    jointCheckDetail.LimitDate = Convert.ToDateTime(LimitDate.Text.Trim());
                }
                jointCheckDetail.AttachUrl = AttachUrl;
                //jointCheckDetail.HandleWay = HandleWay;
                //jointCheckDetail.ReAttachUrl = ReUrl;
                jointCheckDetail.State = Const.JointCheck_Audit1;
                if (drpHandleMan.SelectedValue != "0")
                {
                    jointCheckDetail.HandleMan = drpHandleMan.SelectedValue;
                }
                jointCheckDetails.Add(jointCheckDetail);
            }
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检施工单位！", MessageBoxIcon.Warning);
                return;
            }
            addList();
            Model.Check_JointCheckDetail jointCheckDetail = new Model.Check_JointCheckDetail();
            jointCheckDetail.JointCheckDetailId = SQLHelper.GetNewID(typeof(Model.Check_JointCheckDetail));
            jointCheckDetail.CreateDate = DateTime.Now;
            jointCheckDetails.Add(jointCheckDetail);
            Grid1.DataSource = jointCheckDetails;
            Grid1.DataBind();
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                AspNet.DropDownList drpUnitWork = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpUnitWork");
                AspNet.HiddenField hdUnitWork = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdUnitWork");
                drpUnitWork.Items.AddRange(BLL.UnitWorkService.GetUnitWork(this.CurrUser.LoginProjectId));
                Funs.PleaseSelect(drpUnitWork);
                if (!string.IsNullOrEmpty(hdUnitWork.Value))
                {
                    drpUnitWork.SelectedValue = hdUnitWork.Value;
                }
                AspNet.HiddenField hdCNProfessional = (AspNet.HiddenField)(this.Grid1.Rows[i].FindControl("hdCNProfessional"));
                AspNet.DropDownList drpCNProfessional = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpCNProfessional"));
                drpCNProfessional.Items.AddRange(BLL.CNProfessionalService.GetCNProfessionalItem());
                Funs.PleaseSelect(drpCNProfessional);
                if (!string.IsNullOrEmpty(hdCNProfessional.Value))
                {
                    drpCNProfessional.SelectedValue = hdCNProfessional.Value;
                }
                AspNet.HiddenField hdQuestionType = (AspNet.HiddenField)(this.Grid1.Rows[i].FindControl("hdQuestionType"));
                AspNet.DropDownList drpQuestionType = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpQuestionType"));
                drpQuestionType.Items.AddRange(BLL.QualityQuestionTypeService.GetQualityQuestionTypeItem());
                Funs.PleaseSelect(drpQuestionType);
                if (!string.IsNullOrEmpty(hdQuestionType.Value))
                {
                    drpQuestionType.SelectedValue = hdQuestionType.Value;
                }

                AspNet.HiddenField hdHandleMan = (AspNet.HiddenField)(this.Grid1.Rows[i].FindControl("hdHandleMan"));
                AspNet.DropDownList drpHandleMan = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpHandleMan"));
                Funs.PleaseSelect(drpHandleMan);
                drpHandleMan.Items.AddRange(BLL.UserService.GetUserByUnitId(CurrUser.LoginProjectId, drpUnit.SelectedValue));
                if (!string.IsNullOrEmpty(hdHandleMan.Value))
                {
                    drpHandleMan.SelectedValue = hdHandleMan.Value;
                }
            }
        }
        /// <summary>
        /// 明细集合
        /// </summary>
        private static List<Model.Check_JointCheckDetail> jointCheckDetails = new List<Model.Check_JointCheckDetail>();

        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {

        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ConvertDate(object date)
        {
            if (date != null)
            {
                return string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(date));
            }
            else
            {
                return null;
            }
        }




        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                SaveJointCheck("save");

            }

        }






        /// <summary>
        /// 保存质量不合格整改通知单
        /// </summary>
        public void SaveJointCheck(string saveType)
        {
            Model.Check_JointCheck jointCheck = new Model.Check_JointCheck();
            jointCheck.JointCheckCode = txtJointCheckCode.Text.Trim();
            jointCheck.ProjectId = CurrUser.LoginProjectId;

            if (drpCheckType.SelectedValue != Const._Null)
            {
                jointCheck.CheckType = drpCheckType.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("必填项不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (drpUnit.SelectedValue != Const._Null)
            {
                jointCheck.UnitId = drpUnit.SelectedValue;
            }
            if (drpProposeUnit.SelectedValue != Const._Null)
            {
                jointCheck.ProposeUnitId = drpProposeUnit.SelectedValue;
            }
            if (!string.IsNullOrEmpty(txtCheckDate.Text.Trim()))
            {
                jointCheck.CheckDate = Convert.ToDateTime(txtCheckDate.Text.Trim());
            }
            jointCheck.CheckName = this.txtCheckName.Text.Trim();
            jointCheck.State = "1";  //整改中

            if (saveType == "submit")
            {
                jointCheck.State = "Z";
            }
            else
            {
                Model.Check_JointCheck jointCheck1 = JointCheckService.GetJointCheck(JointCheckId);
                if (jointCheck1 != null)
                {
                    if (string.IsNullOrEmpty(jointCheck1.State))
                    {
                        jointCheck.State = BLL.Const.JointCheck_Compile;
                    }
                    else
                    {
                        jointCheck.State = jointCheck1.State;
                    }
                }
                else
                {
                    jointCheck.State = BLL.Const.JointCheck_Compile;
                }
            }

            if (!string.IsNullOrEmpty(JointCheckId))
            {
                jointCheck.JointCheckId = JointCheckId;
                jointCheck.CheckMan = CurrUser.UserId;
                JointCheckService.UpdateJointCheck(jointCheck);
                if (saveType == "submit")
                {
                    Model.Check_JointCheckApprove approve1 = BLL.JointCheckApproveService.GetJointCheckApproveByJointCheckId(JointCheckId, this.CurrUser.UserId);
                    approve1.ApproveDate = DateTime.Now;
                    BLL.JointCheckApproveService.UpdateJointCheckApprove(approve1);
                    foreach (JObject mergedRow in Grid1.GetMergedData())
                    {
                        JObject values = mergedRow.Value<JObject>("values");
                        int i = mergedRow.Value<int>("index");
                        AspNet.DropDownList drpHandleMan = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpHandleMan"));
                        if (drpHandleMan.SelectedValue != "0")
                        {
                            Model.Check_JointCheckApprove approve = new Model.Check_JointCheckApprove();
                            approve.JointCheckId = jointCheck.JointCheckId;
                            approve.ApproveMan = drpHandleMan.SelectedValue;
                            approve.ApproveType = Const.JointCheck_Audit1;
                            approve.JointCheckDetailId = SQLHelper.GetNewID(typeof(Model.Check_JointCheck));
                            JointCheckApproveService.AddJointCheckApprove(approve);
                        }
                    }
                }
            }
            else
            {
                jointCheck.JointCheckId = SQLHelper.GetNewID(typeof(Model.Check_JointCheck)); ;
                jointCheck.CheckMan = CurrUser.UserId;
                JointCheckService.AddJointCheck(jointCheck);
                if (saveType == "submit")
                {
                    Model.Check_JointCheckApprove approve1 = new Model.Check_JointCheckApprove();
                    approve1.JointCheckId = jointCheck.JointCheckId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.JointCheck_Compile;
                    BLL.JointCheckApproveService.AddJointCheckApprove(approve1);
                    foreach (JObject mergedRow in Grid1.GetMergedData())
                    {
                        JObject values = mergedRow.Value<JObject>("values");
                        int i = mergedRow.Value<int>("index");
                        AspNet.DropDownList drpHandleMan = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpHandleMan"));
                        if (drpHandleMan.SelectedValue != "0")
                        {
                            Model.Check_JointCheckApprove approve = new Model.Check_JointCheckApprove();
                            approve.JointCheckId = jointCheck.JointCheckId;
                            approve.ApproveMan = drpHandleMan.SelectedValue;
                            approve.ApproveType = Const.JointCheck_Audit1;
                            approve.JointCheckDetailId = Grid1.Rows[i].RowID;
                            JointCheckApproveService.AddJointCheckApprove(approve);
                        }
                    }
                }
                else
                {
                    Model.Check_JointCheckApprove approve = new Model.Check_JointCheckApprove();
                    approve.JointCheckId = jointCheck.JointCheckId;
                    approve.ApproveMan = CurrUser.UserId;
                    approve.ApproveType = Const.JointCheck_Compile;
                    BLL.JointCheckApproveService.AddJointCheckApprove(approve);
                }
            }
            addGvList();
            List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
            foreach (Model.Check_JointCheckDetail a in jointCheckDetails)
            {
                a.JointCheckId = jointCheck.JointCheckId;
                BLL.JointCheckDetailService.AddJointCheckDetail(a);
                if (string.IsNullOrEmpty(JointCheckId))
                {
                    seeUsers.AddRange(BLL.UserService.GetSeeUserList2(this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue, a.CNProfessionalCode, a.UnitWorkId.ToString(), this.CurrUser.UserId));
                }
            }
            if (string.IsNullOrEmpty(JointCheckId))
            {
                seeUsers = seeUsers.Distinct().ToList();
                foreach (var seeUser in seeUsers)
                {
                    Model.Check_JointCheckApprove approve = new Model.Check_JointCheckApprove();
                    approve.JointCheckId = jointCheck.JointCheckId;
                    approve.ApproveMan = seeUser.UserId;
                    approve.ApproveType = "S";
                    BLL.JointCheckApproveService.AddJointCheckApprove(approve);
                }
            }
            LogService.AddSys_Log(CurrUser, jointCheck.JointCheckCode, jointCheck.JointCheckId, Const.JointCheckMenuId, "添加共检记录");
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }
        /// <summary>
        /// 表格数据验证
        /// </summary>
        private bool validate()
        {
            bool res = false;
            string err = string.Empty;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                AspNet.DropDownList drpUnitWork = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpUnitWork");
                AspNet.DropDownList drpCNProfessional = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpCNProfessional");
                string CheckSite = values.Value<string>("CheckSite");
                string QuestionDef = values.Value<string>("QuestionDef");
                AspNet.DropDownList drpQuestionType = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpQuestionType"));
                AspNet.TextBox LimitDate = (AspNet.TextBox)Grid1.Rows[i].FindControl("LimitDate");
                AspNet.DropDownList drpHandleMan = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpHandleMan"));
                if (drpUnitWork.SelectedValue == "0" ||
                      drpCNProfessional.SelectedValue == "0" ||
                    string.IsNullOrWhiteSpace(CheckSite) ||
                    string.IsNullOrWhiteSpace(QuestionDef) ||
                     drpQuestionType.SelectedValue == "0" ||
                     drpHandleMan.SelectedValue == "0" ||
                    string.IsNullOrWhiteSpace(LimitDate.Text.Trim()))
                {
                    err += "第" + (i + 1).ToString() + "行：";
                    if (drpUnitWork.SelectedValue == "0")
                    {
                        err += "请选择单位工程,";
                    }
                    if (drpCNProfessional.SelectedValue == "0")
                    {
                        err += "请选择专业,";
                    }
                    if (string.IsNullOrWhiteSpace(CheckSite))
                    {
                        err += "请输入部位,";
                    }
                    if (string.IsNullOrWhiteSpace(QuestionDef))
                    {
                        err += "请输入问题描述,";
                    }
                    if (drpQuestionType.SelectedValue == "0")
                    {
                        err += "请选择问题类别,";
                    }
                    if (string.IsNullOrWhiteSpace(LimitDate.Text.Trim()))
                    {
                        err += "请输入整改时间,";
                    }
                    if (drpHandleMan.SelectedValue == "0")
                    {
                        err += "请选择办理人,";
                    }
                    err = err.Substring(0, err.LastIndexOf(","));
                    err += "!";
                }

            }
            if (Grid1.Rows.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(err))
                {
                    Alert.ShowInTop(err, MessageBoxIcon.Warning);
                }
                else
                {
                    res = true;
                }
            }
            else
            {
                Alert.ShowInTop("请完善共检问题内容！", MessageBoxIcon.Warning);
            }

            return res;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                SaveJointCheck("submit");

            }

        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            addList();
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/JointCheck&menuId={1}&edit=1", itemId, BLL.Const.JointCheckMenuId)));
            }
            if (e.CommandName == "delete")
            {
                foreach (Model.Check_JointCheckDetail jointCheckDetail in jointCheckDetails)
                {
                    if (jointCheckDetail.JointCheckDetailId == itemId)
                    {
                        jointCheckDetails.Remove(jointCheckDetail);
                        break;
                    }
                }
                Grid1.DataSource = jointCheckDetails;
                Grid1.DataBind();
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    AspNet.DropDownList drpUnitWork = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpUnitWork");
                    AspNet.HiddenField hdUnitWork = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdUnitWork");
                    drpUnitWork.Items.AddRange(BLL.UnitWorkService.GetUnitWork(this.CurrUser.LoginProjectId));
                    Funs.PleaseSelect(drpUnitWork);
                    if (!string.IsNullOrEmpty(hdUnitWork.Value))
                    {
                        drpUnitWork.SelectedValue = hdUnitWork.Value;
                    }
                    AspNet.HiddenField hdCNProfessional = (AspNet.HiddenField)(this.Grid1.Rows[i].FindControl("hdCNProfessional"));
                    AspNet.DropDownList drpCNProfessional = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpCNProfessional"));
                    drpCNProfessional.Items.AddRange(BLL.CNProfessionalService.GetCNProfessionalItem());
                    Funs.PleaseSelect(drpCNProfessional);
                    if (!string.IsNullOrEmpty(hdCNProfessional.Value))
                    {
                        drpCNProfessional.SelectedValue = hdCNProfessional.Value;
                    }
                    AspNet.HiddenField hdQuestionType = (AspNet.HiddenField)(this.Grid1.Rows[i].FindControl("hdQuestionType"));
                    AspNet.DropDownList drpQuestionType = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpQuestionType"));
                    drpQuestionType.Items.AddRange(BLL.QualityQuestionTypeService.GetQualityQuestionTypeItem());
                    Funs.PleaseSelect(drpQuestionType);
                    if (!string.IsNullOrEmpty(hdQuestionType.Value))
                    {
                        drpQuestionType.SelectedValue = hdQuestionType.Value;
                    }

                    AspNet.HiddenField hdHandleMan = (AspNet.HiddenField)(this.Grid1.Rows[i].FindControl("hdHandleMan"));
                    AspNet.DropDownList drpHandleMan = (AspNet.DropDownList)(this.Grid1.Rows[i].FindControl("drpHandleMan"));
                    Funs.PleaseSelect(drpHandleMan);
                    drpHandleMan.Items.AddRange(BLL.UserService.GetUserByUnitId(CurrUser.LoginProjectId, drpUnit.SelectedValue));
                    if (!string.IsNullOrEmpty(hdHandleMan.Value))
                    {
                        drpHandleMan.SelectedValue = hdHandleMan.Value;
                    }
                }
            }
        }
    }
}