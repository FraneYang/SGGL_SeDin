using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditJointCheckTwo : PageBase
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
        /// 图片处理
        /// </summary>
        public int ImgId
        {
            get
            {
                return (int)ViewState["ImgId"];
            }
            set
            {
                ViewState["ImgId"] = value;
            }
        }
        /// <summary>
        /// 明细集合
        /// </summary>
        private static List<Model.Check_JointCheckDetail> jointCheckDetails = new List<Model.Check_JointCheckDetail>();

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
            //绑定表格---表单
            if (!IsPostBack)
            {
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, false);
                UnitService.InitUnitNotsub(drpProposeUnit, CurrUser.LoginProjectId, false);
                JointCheckService.Init(drpCheckType, true);
                txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
                JointCheckId = Request.Params["JointCheckId"];
                if (!string.IsNullOrEmpty(JointCheckId))
                {
                    Model.Check_JointCheck jointCheck = BLL.JointCheckService.GetJointCheck(JointCheckId);
                    txtJointCheckCode.Text = jointCheck.JointCheckCode;
                    if (!string.IsNullOrEmpty(jointCheck.UnitId))
                    {
                        drpUnit.SelectedValue = jointCheck.UnitId;
                    }
                    if (!string.IsNullOrEmpty(jointCheck.ProposeUnitId))
                    {
                        drpProposeUnit.SelectedValue = jointCheck.ProposeUnitId;
                    }
                    if (!string.IsNullOrEmpty(jointCheck.CheckType))
                    {
                        drpCheckType.SelectedValue = jointCheck.CheckType;
                    }
                    if (jointCheck.CheckDate != null)
                    {
                        txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", jointCheck.CheckDate);
                    }
                    txtCheckName.Text = jointCheck.CheckName;
                    if (!string.IsNullOrEmpty(jointCheck.State))
                    {
                        State = jointCheck.State;
                    }
                    else
                    {
                        State = BLL.Const.JointCheck_Compile;
                    }
                    jointCheckDetails.Clear();
                    ImgId = 0;
                    BindData();
                }


            }
        }
        private void BindData()
        {
            jointCheckDetails = JointCheckDetailService.GetLists(JointCheckId);
            var unitWorks = BLL.UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
            var cNProfessionals = BLL.CNProfessionalService.GetCNProfessionalItem();
            var questionTypes = BLL.QualityQuestionTypeService.GetQualityQuestionTypeItem();
            var users = BLL.UserService.GetProjectUserListByProjectId(CurrUser.LoginProjectId);
            if (jointCheckDetails.Count > 0)
            {
                for (int i = 0; i < jointCheckDetails.Count; i++)
                {
                    var cn = cNProfessionals.FirstOrDefault(x => x.Value == jointCheckDetails[i].CNProfessionalCode);
                    if (cn != null)
                    {
                        jointCheckDetails[i].CNProfessionalCode = cn.Text;
                    }
                    var unitWork = unitWorks.FirstOrDefault(x => x.UnitWorkId == jointCheckDetails[i].UnitWorkId);
                    if (unitWork != null)
                    {
                        jointCheckDetails[i].UnitWorkId = unitWork.UnitWorkName;
                    }
                    var questionType = questionTypes.FirstOrDefault(x => x.Value == jointCheckDetails[i].QuestionType);
                    if (questionType != null)
                    {
                        jointCheckDetails[i].QuestionType = questionType.Text;
                    }
                }
            }
            Grid1.DataSource = jointCheckDetails;
            Grid1.DataBind();
            if (Grid1.Rows.Count > 0)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    GridRow row = Grid1.Rows[i];
                    JObject values = mergedRow.Value<JObject>("values");
                    System.Web.UI.WebControls.DropDownList handtype = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleType"));
                    System.Web.UI.WebControls.DropDownList handman = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleMan"));
                    System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdHandleMan"));
                    System.Web.UI.WebControls.HiddenField lblsite = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdState"));
                    handtype.Items.AddRange(JointCheckService.GetDHandleTypeByState(lblsite.Value));
                    //System.Web.UI.WebControls.TextBox handleWays = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtHandleWay"));
                    //System.Web.UI.WebControls.TextBox rectifyDate = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtRectifyDate"));
                    if (!handtype.SelectedValue.Equals(Const.JointCheck_Complete))
                    {
                        if (handtype.SelectedValue.Equals(Const.JointCheck_Audit3) || handtype.SelectedValue.Equals(Const.JointCheck_Audit4))
                        {
                            handman.Items.AddRange(UserService.GetMainUserList(CurrUser.LoginProjectId));
                        }
                        else
                        {
                            handman.Items.AddRange(UserService.GetUserByUnitId(CurrUser.LoginProjectId, drpUnit.SelectedValue));
                        }

                    }
                    Funs.PleaseSelect(handman);
                    if (lblHandleMan.Value != CurrUser.UserId)
                    {
                        foreach (GridColumn column in Grid1.AllColumns)
                        {
                            Grid1.Rows[i].CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                        }
                        handtype.Enabled = false;
                        handman.Enabled = false;

                    }
                    else
                    {
                        //handleWays.ReadOnly = false;
                        //rectifyDate.Enabled = true;
                        handtype.Enabled = true;
                        handman.Enabled = true;
                        if (handtype.SelectedValue == Const.JointCheck_Complete)
                        {
                            handman.Enabled = false;
                        }
                    }


                    if (lblsite.Value.Equals(Const.JointCheck_Audit3) || lblsite.Value.Equals(Const.JointCheck_Audit4))
                    {
                        foreach (GridColumn column in Grid1.AllColumns)
                        {
                            Grid1.Rows[i].CellCssClasses[11] = "f-grid-cell-uneditable";
                            Grid1.Rows[i].CellCssClasses[12] = "f-grid-cell-uneditable";
                        }
                        ImgId = -1;
                    }

                }
                //setHandelMan();
            }
            var list = JointCheckApproveService.getListData(JointCheckId);
            gvApprove.DataSource = list;
            gvApprove.DataBind();

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

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.DropDownList handleType = sender as System.Web.UI.WebControls.DropDownList;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                string handleWay = values.Value<string>("HandleWay");
                AspNet.TextBox RectifyDate = (AspNet.TextBox)Grid1.Rows[i].FindControl("RectifyDate");
                string id = this.Grid1.Rows[i].RowID;
                var detail = jointCheckDetails.FirstOrDefault(x => x.JointCheckDetailId == id);
                detail.HandleWay = handleWay;
                if (!string.IsNullOrWhiteSpace(RectifyDate.Text.Trim()))
                {
                    detail.RectifyDate = Convert.ToDateTime(RectifyDate.Text.Trim());
                }
                System.Web.UI.WebControls.DropDownList handleType1 = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpHandleType"));
                if (handleType.ClientID == handleType1.ClientID)
                {
                    detail.State = handleType.SelectedValue;
                }


            }
            BindData2();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                SaveJointCheck("save");
            }
        }

        /// <summary>
        /// 检查并保存集合
        /// </summary>
        private void jerqueSaveList()
        {
            jointCheckDetails.Clear();

            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                string jointCheckDetailId = values.Value<string>("JointCheckDetailId");
                //System.Web.UI.WebControls.TextBox thandleWay = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtHandleWay"));
                //System.Web.UI.WebControls.TextBox trectifyDate = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtRectifyDate"));
                System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(Grid1.Rows[i].FindControl("hdHandleMan"));
                AspNet.TextBox RectifyDate = (AspNet.TextBox)Grid1.Rows[i].FindControl("RectifyDate");
                string handleWay = values.Value<string>("HandleWay");
                string state = ((System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpHandleType")).SelectedValue;
                string handleMan = ((System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpHandleMan")).SelectedValue;
                Model.Check_JointCheckDetail jointCheckDetail = JointCheckDetailService.GetJointCheckDetailByJointCheckDetailId(jointCheckDetailId);
                jointCheckDetail.JointCheckDetailId = jointCheckDetailId;
                jointCheckDetail.JointCheckId = JointCheckId;
                jointCheckDetail.HandleWay = handleWay;
                if (!string.IsNullOrEmpty(RectifyDate.Text.Trim()))
                {
                    jointCheckDetail.RectifyDate = Convert.ToDateTime(RectifyDate.Text.Trim());
                }
                if (handleMan == "0" && state != Const.JointCheck_Complete)   //非审批完成步骤，未选择办理人，不更新办理状态
                {
                    var oldDetail = BLL.JointCheckDetailService.GetJointCheckDetailByJointCheckDetailId(jointCheckDetailId);
                    if (oldDetail != null)
                    {
                        jointCheckDetail.State = oldDetail.State;
                        jointCheckDetail.HandleMan = oldDetail.HandleMan;
                    }
                }
                else
                {
                    jointCheckDetail.State = state;
                    jointCheckDetail.HandleMan = handleMan;
                }
                jointCheckDetails.Add(jointCheckDetail);

            }


        }

        /// <summary>
        /// 保存质量不合格整改通知单
        /// </summary>
        private void SaveJointCheck(string saveType)
        {

            if (!string.IsNullOrEmpty(JointCheckId))
            {

                if (saveType == "submit")
                {
                    foreach (JObject mergedRow in Grid1.GetMergedData())
                    {
                        //先更新明细再添加审批信息
                        int i = mergedRow.Value<int>("index");
                        JObject values = mergedRow.Value<JObject>("values");
                        GridRow row = Grid1.Rows[i];
                        string state = values.Value<string>("HandleType");

                        string jointCheckDetailId = values.Value<string>("JointCheckDetailId");
                        string handleWay = values.Value<string>("HandleWay");

                        var man = (System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpHandleMan");
                        var type = (System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpHandleType");
                        var rectifyDate = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("RectifyDate");

                        var handleMan = (System.Web.UI.WebControls.HiddenField)Grid1.Rows[i].FindControl("hdHandleMan");
                        if (handleMan.Value == CurrUser.UserId)
                        {
                            Model.Check_JointCheckDetail spdetail = JointCheckDetailService.GetJointCheckDetailByJointCheckDetailId(jointCheckDetailId);
                            spdetail.HandleWay = handleWay;
                            if (rectifyDate.Text != null)
                            {
                                spdetail.RectifyDate = Convert.ToDateTime(rectifyDate.Text);
                            }
                            if (type.SelectedValue == Const.JointCheck_Complete)
                            {
                                spdetail.State = type.SelectedValue;
                                spdetail.HandleMan = string.Empty;
                                JointCheckDetailService.UpdateJointCheckDetail(spdetail);
                                if (saveType == "submit")
                                {
                                    var appro = JointCheckApproveService.GetJointCheckApproveByJointCheckDetailId(jointCheckDetailId, CurrUser.UserId);
                                    if (appro != null && saveType == "submit")
                                    {
                                        appro.ApproveDate = DateTime.Now;
                                        appro.ApproveIdea = txtOpinions.Text.Trim();
                                        JointCheckApproveService.UpdateJointCheckApprove(appro);
                                    }
                                }
                            }
                            else
                            {
                                if (saveType == "submit")
                                {
                                    if (man.SelectedValue != "0")
                                    {

                                        spdetail.State = type.SelectedValue;
                                        spdetail.HandleMan = man.SelectedValue;
                                        JointCheckDetailService.UpdateJointCheckDetail(spdetail);

                                        var appro = JointCheckApproveService.GetJointCheckApproveByJointCheckDetailId(row.RowID, CurrUser.UserId);
                                        if (appro != null && saveType == "submit")
                                        {
                                            appro.ApproveDate = DateTime.Now;
                                            appro.ApproveIdea = txtOpinions.Text.Trim();
                                            JointCheckApproveService.UpdateJointCheckApprove(appro);
                                        }

                                        Model.Check_JointCheckApprove approve = new Model.Check_JointCheckApprove();
                                        approve.JointCheckId = JointCheckId;
                                        approve.ApproveMan = man.SelectedValue;
                                        approve.ApproveType = type.SelectedValue;
                                        approve.JointCheckDetailId = jointCheckDetailId;
                                        JointCheckApproveService.AddJointCheckApprove(approve);
                                        APICommonService.SendSubscribeMessage(approve.ApproveMan, "质量共检问题待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                                    }
                                }
                            }

                        }
                    }

                }

            }

            var joinCheck = JointCheckService.GetJointCheck(JointCheckId);
            //循环判断明细是否都是已经完成是则处理主表状态已完成
            var details = JointCheckDetailService.GetLists(JointCheckId);
            if (details.Count > 0)
            {
                var stat = true;
                foreach (var item in details)
                {
                    if (item.State != Const.JointCheck_Complete)
                    {
                        stat = false;
                        break;
                    }
                }
                if (stat)
                {
                    joinCheck.State = Const.JointCheck_Complete;
                    JointCheckService.UpdateJointCheck(joinCheck);
                }
            }
            LogService.AddSys_Log(this.CurrUser, joinCheck.JointCheckCode, JointCheckId, Const.JointCheckMenuId, "质量共检");
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private Boolean validate()
        {
            string err = string.Empty;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                string HandleWay = values.Value<string>("HandleWay");
                AspNet.TextBox RectifyDate = (AspNet.TextBox)Grid1.Rows[i].FindControl("RectifyDate");

                //System.Web.UI.WebControls.TextBox thandleWay = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtHandleWay"));
                //System.Web.UI.WebControls.TextBox trectifyDate = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtRectifyDate"));
                //if (thandleWay.Enabled && trectifyDate.Enabled) 
                //{
                System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(Grid1.Rows[i].FindControl("hdHandleMan"));
                if (lblHandleMan.Value == CurrUser.UserId)//如果不是此用户的记录
                {
                    if (string.IsNullOrEmpty(HandleWay.Trim()) || string.IsNullOrEmpty(RectifyDate.Text.Trim()))
                    {
                        err += "第" + (i + 1).ToString() + "行：";
                        if (string.IsNullOrEmpty(HandleWay.Trim()))
                        {
                            err += "请输入整改方案,";
                        }
                        if (string.IsNullOrEmpty(RectifyDate.Text.Trim()))
                        {
                            err += "请输入实际整改时间,";
                        }
                    }
                }


            }
            if (!string.IsNullOrEmpty(err))
            {
                err = err.Substring(0, err.LastIndexOf(","));
                err += "!";
                PageContext.RegisterStartupScript("alert('" + err + "');");
                return false;
            }
            else
            {
                return true;
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                SaveJointCheck("submit");
            }
        }
        private void BindData2()
        {
            Grid1.DataSource = jointCheckDetails;
            Grid1.DataBind();
            if (Grid1.Rows.Count > 0)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    GridRow row = Grid1.Rows[i];
                    JObject values = mergedRow.Value<JObject>("values");
                    System.Web.UI.WebControls.DropDownList handtype = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleType"));
                    System.Web.UI.WebControls.DropDownList handman = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleMan"));
                    System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdHandleMan"));
                    System.Web.UI.WebControls.HiddenField lblsite = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdState"));
                    Model.Check_JointCheckDetail detail = JointCheckDetailService.GetJointCheckDetailByJointCheckDetailId(Grid1.Rows[i].RowID);
                    handtype.Items.AddRange(JointCheckService.GetDHandleTypeByState(detail.State));
                    if (lblsite.Value != detail.State)
                    {
                        handtype.SelectedValue = lblsite.Value;
                    }
                    //System.Web.UI.WebControls.TextBox handleWays = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtHandleWay"));
                    //System.Web.UI.WebControls.TextBox rectifyDate = (System.Web.UI.WebControls.TextBox)(Grid1.Rows[i].FindControl("txtRectifyDate"));
                    if (!handtype.SelectedValue.Equals(Const.JointCheck_Complete))
                    {
                        if (handtype.SelectedValue.Equals(Const.JointCheck_Audit3) || handtype.SelectedValue.Equals(Const.JointCheck_Audit4))
                        {
                            handman.Items.AddRange(UserService.GetMainUserList(CurrUser.LoginProjectId));
                        }
                        else
                        {
                            handman.Items.AddRange(UserService.GetUserByUnitId(CurrUser.LoginProjectId, drpUnit.SelectedValue));
                        }
                    }
                    Funs.PleaseSelect(handman);
                    if (lblHandleMan.Value != CurrUser.UserId)
                    {
                        foreach (GridColumn column in Grid1.AllColumns)
                        {
                            this.Grid1.Rows[i].CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                        }
                        //handleWays.ReadOnly = true;
                        //rectifyDate.Enabled = false;
                        handtype.Enabled = false;
                        handman.Enabled = false;

                    }
                    else
                    {
                        //handleWays.ReadOnly = false;
                        //rectifyDate.Enabled = true;
                        handtype.Enabled = true;
                        handman.Enabled = true;
                        if (handtype.SelectedValue == Const.JointCheck_Complete)
                        {
                            handman.Enabled = false;
                        }
                    }


                    if (detail.State.Equals(Const.JointCheck_Audit3) || detail.State.Equals(Const.JointCheck_Audit4))
                    {
                        foreach (GridColumn column in Grid1.AllColumns)
                        {
                            Grid1.Rows[i].CellCssClasses[11] = "f-grid-cell-uneditable";
                            Grid1.Rows[i].CellCssClasses[12] = "f-grid-cell-uneditable";
                        }
                        ImgId = -1;
                    }
                }
                //setHandelMan();
            }
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            var detail = jointCheckDetails.FirstOrDefault(x => x.JointCheckDetailId == itemId);
            if (detail.HandleMan == CurrUser.UserId)
            {
                if (e.CommandName == "ReAttachUrl")
                {
                    if (detail.State != Const.JointCheck_Audit3 && detail.State != Const.JointCheck_Audit4)
                    {
                        ImgId = 0;
                    }
                    else
                    {
                        ImgId = -1;
                    }
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/JointCheck&menuId={2}&edit=1", ImgId, itemId + "r", Const.JointCheckMenuId)));

                }
            }
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type=-1&toKeyId={0}&path=FileUpload/JointCheck&menuId={1}&edit=1", itemId, BLL.Const.JointCheckMenuId)));
            }
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
    }
}