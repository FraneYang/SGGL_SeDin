using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditSpotCheck : PageBase
    {
        #region 公共字段
        /// <summary>
        /// 主键
        /// </summary>
        public string SpotCheckCode
        {
            get
            {
                return (string)ViewState["SpotCheckCode"];
            }
            set
            {
                ViewState["SpotCheckCode"] = value;
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
        #endregion
        /// <summary>
        /// 图片是否可以编辑 -1查看 0编辑
        /// </summary>
        public int QuestionImg
        {
            get
            {
                return Convert.ToInt32(ViewState["QuestionImg"]);
            }
            set
            {
                ViewState["QuestionImg"] = value;
            }
        }
        /// <summary>
        /// 整改图片
        /// </summary>
        public int HandleImg
        {
            get
            {
                return Convert.ToInt32(ViewState["HandleImg"]);
            }
            set
            {
                ViewState["HandleImg"] = value;
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
                if (state.ToString() == BLL.Const.SpotCheck_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit1)
                {
                    return "分包负责人确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit2)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit3)
                {
                    return "监理专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit4)
                {
                    return "建设单位确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit5)
                {
                    return "分包专业工程师上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit6)
                {
                    return "总包专业工程师确认资料合格";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit7)
                {
                    return "分包负责人确认资料合格";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit5R)
                {
                    return "分包专业工程师重新上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Complete)
                {
                    return "审批完成";
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
        private static List<Model.Check_SpotCheckDetail> details = new List<Model.Check_SpotCheckDetail>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                details.Clear();
                this.Grid1.Columns[3].Hidden = true;
                this.Grid1.Columns[4].Hidden = true;
                this.Grid1.Columns[5].Hidden = true;
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, false);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, false);
                UserService.InitUserProjectIdUnitTypeDropDownList(drpJointCheckMans, this.CurrUser.LoginProjectId, Const.ProjectUnitType_1, false);
                UserService.InitUserProjectIdUnitTypeDropDownList(drpJointCheckMans2, this.CurrUser.LoginProjectId, Const.ProjectUnitType_3, false);
                UserService.InitUserProjectIdUnitTypeDropDownList(drpJointCheckMans3, this.CurrUser.LoginProjectId, Const.ProjectUnitType_4, false);
                drpHandleType.Readonly = true;
                SpotCheckCode = Request.Params["SpotCheckCode"];
                plApprove1.Hidden = true;
                plApprove2.Hidden = true;
                rblIsAgree.Hidden = true;
                rblIsAgree.SelectedValue = "true";
                if (!string.IsNullOrEmpty(SpotCheckCode))
                {
                    this.hdSpotCheckCode.Text = SpotCheckCode;
                    plApprove1.Hidden = false;
                    plApprove2.Hidden = false;
                    var dt = SpotCheckApproveService.getListData(SpotCheckCode);
                    gvApprove.DataSource = dt;
                    gvApprove.DataBind();
                    Model.Check_SpotCheck spotCheck = SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);
                    txtDocCode.Text = spotCheck.DocCode;
                    if (!string.IsNullOrEmpty(spotCheck.UnitId))
                    {
                        this.drpUnit.SelectedValue = spotCheck.UnitId;
                    }
                    if (!string.IsNullOrEmpty(spotCheck.CNProfessionalCode))
                    {
                        this.drpCNProfessional.SelectedValue = spotCheck.CNProfessionalCode;
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans))
                    {
                        this.drpJointCheckMans.SelectedValueArray = spotCheck.JointCheckMans.Split(',');
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans2))
                    {
                        this.drpJointCheckMans2.SelectedValueArray = spotCheck.JointCheckMans2.Split(',');
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans3))
                    {
                        this.drpJointCheckMans3.SelectedValueArray = spotCheck.JointCheckMans3.Split(',');
                    }
                    this.rblCheckDateType.SelectedValue = spotCheck.CheckDateType;
                    if (spotCheck.CheckDateType == "2")
                    {
                        this.txtSpotCheckDate.Label = "开始时间";
                        this.txtSpotCheckDate2.Hidden = false;
                    }
                    if (spotCheck.SpotCheckDate != null)
                    {
                        this.txtSpotCheckDate.Text = string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate);
                    }
                    if (spotCheck.SpotCheckDate2 != null)
                    {
                        this.txtSpotCheckDate2.Text = string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate2);
                    }
                    this.txtCheckArea.Text = spotCheck.CheckArea;
                    if (!string.IsNullOrEmpty(spotCheck.ControlPointType))
                    {
                        this.drpControlPointType.SelectedValue = spotCheck.ControlPointType;
                    }
                    if (!string.IsNullOrEmpty(spotCheck.State))
                    {
                        State = spotCheck.State;
                    }
                    else
                    {
                        State = BLL.Const.SpotCheck_Compile;
                        this.rblIsAgree.Hidden = true;
                    }
                    if (State != BLL.Const.SpotCheck_Complete)
                    {
                        SpotCheckService.Init(drpHandleType, State, spotCheck.ControlPointType, false);
                    }
                    if (State == BLL.Const.SpotCheck_Compile || State == BLL.Const.SpotCheck_ReCompile || State == BLL.Const.SpotCheck_Audit1 || State == BLL.Const.SpotCheck_Audit3 || State == BLL.Const.SpotCheck_Audit4)
                    {
                        this.rblIsAgree.Hidden = true;
                        this.plApprove1.Hidden = true;
                        UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                        this.drpHandleMan.SelectedIndex = 1;
                    }
                    else
                    {
                        UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                        this.rblIsAgree.Hidden = false;
                    }
                    if (State == BLL.Const.SpotCheck_Audit1 || State == BLL.Const.SpotCheck_Audit2 || State == BLL.Const.SpotCheck_Audit3 || State == BLL.Const.SpotCheck_Audit4)
                    {
                        this.drpUnit.Enabled = false;
                        this.drpCNProfessional.Enabled = false;
                        txtDocCode.Enabled = false;
                        txtProjectName.Enabled = false;
                        this.drpJointCheckMans.Enabled = false;
                        this.drpJointCheckMans2.Enabled = false;
                        this.drpJointCheckMans3.Enabled = false;
                        this.rblCheckDateType.Enabled = false;
                        this.txtSpotCheckDate.Enabled = false;
                        this.txtSpotCheckDate2.Enabled = false;
                        this.txtCheckArea.Enabled = false;
                        this.drpControlPointType.Enabled = false;
                    }
                    if (State == BLL.Const.SpotCheck_Audit1)
                    {
                        this.btnNew.Hidden = true;
                        this.Grid1.Columns[10].Hidden = true;
                        this.Grid1.Columns[3].Hidden = false;
                        this.Grid1.Columns[4].Hidden = false;
                        this.Grid1.Columns[5].Hidden = false;
                    }
                    else if (State == BLL.Const.SpotCheck_Audit2)
                    {
                        this.btnNew.Hidden = true;
                        this.Grid1.Columns[10].Hidden = true;
                        drpHandleType.Readonly = false;
                    }
                    else if (State == BLL.Const.SpotCheck_Audit3 || State == BLL.Const.SpotCheck_Audit4)
                    {
                        this.btnNew.Hidden = true;
                        this.Grid1.Columns[3].Hidden = false;
                        this.Grid1.Columns[4].Hidden = false;
                        this.Grid1.Columns[5].Hidden = false;
                        this.Grid1.Columns[10].Hidden = true;
                    }
                    this.Grid1.DataSource = BLL.SpotCheckDetailService.GetSpotCheckDetails(SpotCheckCode);
                    this.Grid1.DataBind();
                    for (int i = 0; i < Grid1.Rows.Count; i++)
                    {
                        string rowID = Grid1.Rows[i].RowID;
                        if (rowID.Count() > 0)
                        {
                            Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                            if (detail.IsOK == false || detail.IsDataOK == "0")
                            {
                                Grid1.Rows[i].RowCssClass = " Yellow ";
                            }
                        }
                    }
                    if (State == BLL.Const.SpotCheck_ReCompile)
                    {
                        //this.Grid1.Columns[3].Hidden = false;
                        //this.Grid1.Columns[5].Hidden = false;
                        this.Grid1.Columns[10].Hidden = false;
                        this.btnNew.Hidden = false;
                        for (int i = 0; i < this.Grid1.Rows.Count; i++)
                        {
                            Grid1.Rows[i].CellCssClasses[5] = "f-grid-cell-uneditable";
                        }
                    }
                    //设置页面图片附件是否可以编辑
                    if (!State.Equals(Const.SpotCheck_Complete))
                    {
                        if (State.Equals(Const.SpotCheck_ReCompile) || State.Equals(Const.SpotCheck_Compile))
                        {
                            QuestionImg = 0;
                        }
                        else
                        {
                            QuestionImg = -1;
                        }
                        if (State.Equals(Const.SpotCheck_Audit3) || State.Equals(Const.SpotCheck_Audit5R))
                        {
                            HandleImg = 0;
                        }
                        else
                        {
                            HandleImg = -1;
                        }
                    }
                }
                else
                {
                    State = Const.SpotCheck_Compile;
                    UserService.Init(drpHandleMan, CurrUser.LoginProjectId, false);
                    QuestionImg = 0;
                    //SpotCheckService.Init(drpHandleType, State, string.Empty, false);
                    string code = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectCode + "-06-CM03-XJ-";
                    txtDocCode.Text = BLL.SQLHelper.RunProcNewId("SpGetNewCode5", "dbo.Check_SpotCheck", "DocCode", code);
                }
                HandleMan();
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                //是否同意触发
                if (!rblIsAgree.Hidden)
                {
                    HandleType();
                }
                Model.Check_SpotCheck spotCheck1 = SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);
                if (spotCheck1 != null && !string.IsNullOrEmpty(spotCheck1.SaveHandleMan))
                {
                    this.drpHandleMan.SelectedValue = spotCheck1.SaveHandleMan;
                }
            }
        }

        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdSpotCheckCode.Text))   //新增记录
            {
                this.hdSpotCheckCode.Text = SQLHelper.GetNewID(typeof(Model.Check_SpotCheck));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/SpotCheck&menuId={2}", QuestionImg, this.hdSpotCheckCode.Text, BLL.Const.SpotCheckMenuId)));
        }

        #region 工序验收列表事件
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpControlPointType.SelectedValue))
            {
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdIds.ClientID)
                           + Window1.GetShowReference(String.Format("ShowWBS.aspx?ControlPointType=") + this.drpControlPointType.SelectedValue));
            }
            else
            {
                Alert.ShowInTop("请选择控制点级别！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region  关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            if (!string.IsNullOrEmpty(hdIds.Text))
            {
                string[] ids = hdIds.Text.Split(',');
                int i = 1;
                foreach (var id in ids)
                {
                    var oldDetail = details.FirstOrDefault(x => x.ControlItemAndCycleId == id);
                    if (oldDetail == null)   //添加集合没有的新纪录
                    {
                        Model.Check_SpotCheckDetail detail = new Model.Check_SpotCheckDetail();
                        detail.SpotCheckDetailId = SQLHelper.GetNewID(typeof(Model.Check_SpotCheckDetail));
                        detail.ControlItemAndCycleId = id;
                        detail.CreateDate = DateTime.Now.AddMinutes(i);
                        details.Add(detail);
                    }
                    i++;
                }
            }
            this.Grid1.DataSource = details;
            this.Grid1.DataBind();
        }
        #endregion

        /// <summary>
        /// 获取共检结果
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertIsOK(object IsOK)
        {
            string isOK = string.Empty;
            if (IsOK != null)
            {
                if (Convert.ToBoolean(IsOK))
                {
                    isOK = "合格";
                }
                else
                {
                    isOK = "不合格";
                }
            }
            return isOK;
        }

        /// <summary>
        /// 获取控制点级别
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertControlPoint(object ControlItemAndCycleId)
        {
            string controlPoint = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    controlPoint = c.ControlPoint;
                }
            }
            return controlPoint;
        }

        /// <summary>
        /// 获取共检内容
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertDetailName(object ControlItemAndCycleId)
        {
            string name = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    name = c.ControlItemContent;
                    Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(c.WorkPackageId);
                    if (w != null)
                    {
                        name = w.PackageContent + "/" + name;
                        Model.WBS_WorkPackage pw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(w.SuperWorkPackageId);
                        if (pw != null)
                        {
                            name = pw.PackageContent + "/" + name;
                            Model.WBS_WorkPackage ppw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(pw.SuperWorkPackageId);
                            if (ppw != null)
                            {
                                name = ppw.PackageContent + "/" + name;
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(ppw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                            else
                            {
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(pw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                        }
                        else
                        {
                            Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(w.UnitWorkId);
                            if (u != null)
                            {
                                name = u.UnitWorkName + "/" + name;
                            }
                        }
                    }
                }
            }
            return name;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            AspNet.Button btn = sender as AspNet.Button;
            Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(btn.CommandArgument);
            if (detail != null)
            {
                Model.Check_SpotCheckApprove approve = BLL.SpotCheckApproveService.GetReCompile(SpotCheckCode);
                if (approve == null)   //没有打回即为一次合格
                {
                    detail.IsOnesOK = true;
                }
                else
                {
                    detail.IsOnesOK = false;
                }
                detail.IsOK = true;
                detail.ConfirmDate = DateTime.Now;
                detail.RectifyDescription = string.Empty;
                BLL.SpotCheckDetailService.UpdateSpotCheckDetail(detail);
            }
            this.Grid1.DataSource = BLL.SpotCheckDetailService.GetSpotCheckDetails(SpotCheckCode);
            this.Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].RowID;
                if (rowID.Count() > 0)
                {
                    Model.Check_SpotCheckDetail detail1 = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                    if (detail1.IsOK == false)
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
        }

        protected void btnNotOK_Click(object sender, EventArgs e)
        {
            AspNet.Button btn = sender as AspNet.Button;
            Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(btn.CommandArgument);
            if (detail != null)
            {
                detail.IsOnesOK = false;
                detail.IsOK = false;
                detail.ConfirmDate = DateTime.Now;
                BLL.SpotCheckDetailService.UpdateSpotCheckDetail(detail);
            }
            this.Grid1.DataSource = BLL.SpotCheckDetailService.GetSpotCheckDetails(SpotCheckCode);
            this.Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].RowID;
                if (rowID.Count() > 0)
                {
                    Model.Check_SpotCheckDetail detail1 = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                    if (detail1.IsOK == false)
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
        }

        protected void btnDataOK_Click(object sender, EventArgs e)
        {
            AspNet.Button btn = sender as AspNet.Button;
            Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(btn.CommandArgument);
            if (detail != null)
            {
                detail.IsDataOK = "1";
                detail.DataConfirmDate = DateTime.Now;
                BLL.SpotCheckDetailService.UpdateSpotCheckDetail(detail);
            }
            this.Grid1.DataSource = BLL.SpotCheckDetailService.GetSpotCheckDetails(SpotCheckCode);
            this.Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].RowID;
                if (rowID.Count() > 0)
                {
                    Model.Check_SpotCheckDetail detail1 = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                    if (detail1.IsDataOK == "0")
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
        }

        protected void btnNotDataOK_Click(object sender, EventArgs e)
        {
            AspNet.Button btn = sender as AspNet.Button;
            Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(btn.CommandArgument);
            if (detail != null)
            {
                detail.IsDataOK = "0";
                detail.DataConfirmDate = DateTime.Now;
                BLL.SpotCheckDetailService.UpdateSpotCheckDetail(detail);
            }
            this.Grid1.DataSource = BLL.SpotCheckDetailService.GetSpotCheckDetails(SpotCheckCode);
            this.Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].RowID;
                if (rowID.Count() > 0)
                {
                    Model.Check_SpotCheckDetail detail1 = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                    if (detail1.IsDataOK == "0")
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "delete")
            {
                foreach (Model.Check_SpotCheckDetail detail in details)
                {
                    if (detail.SpotCheckDetailId == itemId)
                    {
                        details.Remove(detail);
                        break;
                    }
                }
                Grid1.DataSource = details;
                Grid1.DataBind();
            }
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGL/SpotCheck&menuId={1}&type={2}", itemId, BLL.Const.SpotCheckMenuId, HandleImg)));
            }
        }

        /// <summary>
        /// 检查并保存集合
        /// </summary>
        private void jerqueSaveList()
        {
            details.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Check_SpotCheckDetail detail = new Model.Check_SpotCheckDetail();
                detail.SpotCheckDetailId = this.Grid1.Rows[i].RowID;
                detail.ControlItemAndCycleId = this.Grid1.Rows[i].DataKeys[1].ToString();
                string isOnesOK = values.Value<string>("IsOnesOK");
                if (!string.IsNullOrEmpty(isOnesOK))
                {
                    detail.IsOnesOK = Convert.ToBoolean(isOnesOK);
                }
                string isOK = values.Value<string>("IsOK");
                if (!string.IsNullOrEmpty(isOK))
                {
                    detail.IsOK = Convert.ToBoolean(isOK);
                }
                string confirmDate = values.Value<string>("ConfirmDate");
                if (!string.IsNullOrEmpty(confirmDate))
                {
                    detail.ConfirmDate = Convert.ToDateTime(confirmDate);
                }
                string createDate = values.Value<string>("CreateDate");
                if (!string.IsNullOrEmpty(createDate))
                {
                    detail.CreateDate = Convert.ToDateTime(createDate);
                }
                detail.RectifyDescription = values.Value<string>("RectifyDescription");
                string isDataOK = values.Value<string>("IsDataOK");
                if (!string.IsNullOrEmpty(isDataOK))
                {
                    detail.IsDataOK = isDataOK;
                }
                string dataConfirmDate = values.Value<string>("DataConfirmDate");
                if (!string.IsNullOrEmpty(dataConfirmDate))
                {
                    detail.DataConfirmDate = Convert.ToDateTime(dataConfirmDate);
                }
                details.Add(detail);
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string projectId, string userId, string menuId, string buttonName)
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpotCheckMenuId, BLL.Const.BtnSave))
            {
                //if (this.rblCheckDateType.SelectedValue == "1")
                //{
                //    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()))
                //    {
                //        Alert.ShowInTop("请选择共检时间！", MessageBoxIcon.Warning);
                //        return;
                //    }
                //}
                //else
                //{
                //    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()) || string.IsNullOrEmpty(this.txtSpotCheckDate2.Text.Trim()))
                //    {
                //        Alert.ShowInTop("请选择开始时间和结束时间！", MessageBoxIcon.Warning);
                //        return;
                //    }
                //}
                //if (this.Grid1.Rows.Count == 0)
                //{
                //    Alert.ShowInTop("共检内容列表不能为空！", MessageBoxIcon.Warning);
                //    return;
                //}
                SavePauseNotice("save");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                //Response.Redirect("/check/CheckList.aspx");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpotCheckMenuId, BLL.Const.BtnSubmit))
            {
                if (this.rblCheckDateType.SelectedValue == "1")
                {
                    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()))
                    {
                        Alert.ShowInTop("请选择共检时间！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()) || string.IsNullOrEmpty(this.txtSpotCheckDate2.Text.Trim()))
                    {
                        Alert.ShowInTop("请选择开始时间和结束时间！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (this.Grid1.Rows.Count == 0)
                {
                    Alert.ShowInTop("共检内容列表不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpControlPointType.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择控制点级别！", MessageBoxIcon.Warning);
                    return;
                }
                SavePauseNotice("submit");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        private string GetStringByArray(string[] array)
        {
            string str = string.Empty;
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str += item + ",";
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        #region 保存处理
        /// <summary>
        /// 保存方法
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.Check_SpotCheck spotCheck = new Model.Check_SpotCheck();
            spotCheck.DocCode = this.txtDocCode.Text.Trim();
            spotCheck.ProjectId = this.CurrUser.LoginProjectId;
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                spotCheck.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpCNProfessional.SelectedValue != Const._Null)
            {
                spotCheck.CNProfessionalCode = this.drpCNProfessional.SelectedValue;
            }
            string jointCheckMans = GetStringByArray(this.drpJointCheckMans.SelectedValueArray);
            spotCheck.JointCheckMans = jointCheckMans;
            string jointCheckMans2 = GetStringByArray(this.drpJointCheckMans2.SelectedValueArray);
            spotCheck.JointCheckMans2 = jointCheckMans2;
            string jointCheckMans3 = GetStringByArray(this.drpJointCheckMans3.SelectedValueArray);
            spotCheck.JointCheckMans3 = jointCheckMans3;
            spotCheck.CheckDateType = this.rblCheckDateType.SelectedValue;
            if (this.rblCheckDateType.SelectedValue == "1")
            {
                if (!string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()))
                {
                    spotCheck.SpotCheckDate = Convert.ToDateTime(this.txtSpotCheckDate.Text.Trim());
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()))
                {
                    spotCheck.SpotCheckDate = Convert.ToDateTime(this.txtSpotCheckDate.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtSpotCheckDate2.Text.Trim()))
                {
                    spotCheck.SpotCheckDate2 = Convert.ToDateTime(this.txtSpotCheckDate2.Text.Trim());
                }
            }
            spotCheck.CheckArea = this.txtCheckArea.Text.Trim();
            if (this.drpControlPointType.SelectedValue != Const._Null)
            {
                spotCheck.ControlPointType = this.drpControlPointType.SelectedValue;
            }
            if (saveType == "submit")
            {
                spotCheck.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.Check_SpotCheck spotCheck1 = BLL.SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);

                if (spotCheck1 != null)
                {
                    if (string.IsNullOrEmpty(spotCheck1.State))
                    {
                        spotCheck.State = BLL.Const.SpotCheck_Compile;
                    }
                    else
                    {
                        spotCheck.State = spotCheck1.State;
                    }
                }
                else
                {
                    spotCheck.State = BLL.Const.SpotCheck_Compile;
                }
            }
            string initPath = "FileUpload\\" + BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectCode + "\\Check\\SpotCheck\\";
            if (!string.IsNullOrEmpty(SpotCheckCode) && BLL.SpotCheckService.GetSpotCheckBySpotCheckCode(Request.Params["SpotCheckCode"]) != null)
            {
                Model.Check_SpotCheckApprove approve1 = BLL.SpotCheckApproveService.GetSpotCheckApproveBySpotCheckCode(SpotCheckCode);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    approve1.IsAgree = Convert.ToBoolean(this.rblIsAgree.SelectedValue);
                    BLL.SpotCheckApproveService.UpdateSpotCheckApprove(approve1);
                }
                if (saveType == "submit")
                {
                    spotCheck.SaveHandleMan = null;
                    Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
                    approve.SpotCheckCode = SpotCheckCode;
                    if (this.drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;
                    if (this.drpHandleType.SelectedValue == BLL.Const.SpotCheck_Complete)
                    {
                        approve.ApproveDate = DateTime.Now.AddMinutes(1);
                    }
                    approve.Sign = "1";
                    BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "工序验收待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                    //总包专工确认时，通知相关人员
                    if (this.drpHandleType.SelectedValue == BLL.Const.SpotCheck_Audit3 || this.drpHandleType.SelectedValue == BLL.Const.SpotCheck_Audit4)
                    {
                        if (!string.IsNullOrEmpty(jointCheckMans))
                        {
                            string[] seeUsers = jointCheckMans.Split(',');
                            foreach (var seeUser in seeUsers)
                            {
                                if (!string.IsNullOrEmpty(seeUser))
                                {
                                    Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                                    approve2.SpotCheckCode = SpotCheckCode;
                                    approve2.ApproveMan = seeUser;
                                    approve2.ApproveType = "S";
                                    approve2.Sign = "1";
                                    BLL.SpotCheckApproveService.AddSpotCheckApprove(approve2);
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(jointCheckMans2))
                        {
                            string[] seeUsers = jointCheckMans2.Split(',');
                            foreach (var seeUser in seeUsers)
                            {
                                if (!string.IsNullOrEmpty(seeUser))
                                {
                                    Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                                    approve2.SpotCheckCode = SpotCheckCode;
                                    approve2.ApproveMan = seeUser;
                                    approve2.ApproveType = "S";
                                    approve2.Sign = "1";
                                    BLL.SpotCheckApproveService.AddSpotCheckApprove(approve2);
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(jointCheckMans3))
                        {
                            string[] seeUsers = jointCheckMans3.Split(',');
                            foreach (var seeUser in seeUsers)
                            {
                                if (!string.IsNullOrEmpty(seeUser))
                                {
                                    Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                                    approve2.SpotCheckCode = SpotCheckCode;
                                    approve2.ApproveMan = seeUser;
                                    approve2.ApproveType = "S";
                                    approve2.Sign = "1";
                                    BLL.SpotCheckApproveService.AddSpotCheckApprove(approve2);
                                }
                            }
                        }
                        Model.Check_SpotCheckApprove ap = BLL.SpotCheckApproveService.GetComplie(SpotCheckCode);
                        if (ap != null)
                        {
                            Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                            approve2.SpotCheckCode = SpotCheckCode;
                            approve2.ApproveMan = ap.ApproveMan;
                            approve2.ApproveType = "S";
                            approve2.Sign = "1";
                            BLL.SpotCheckApproveService.AddSpotCheckApprove(approve2);
                        }
                    }
                }
                spotCheck.SpotCheckCode = SpotCheckCode;
                jerqueSaveList();
                foreach (var detail in details)
                {
                    detail.SpotCheckCode = spotCheck.SpotCheckCode;
                    BLL.SpotCheckDetailService.UpdateSpotCheckDetail(detail);
                }
                if (saveType == "save")
                {
                    spotCheck.SaveHandleMan = this.drpHandleMan.SelectedValue;
                }
                if (saveType == "submit" && this.drpHandleType.SelectedValue == BLL.Const.SpotCheck_Complete)  //审批完成时，生成分包上传交工资料的办理记录
                {
                    spotCheck.State2 = BLL.Const.SpotCheck_Audit5;   //更新主表状态
                    bool isShow = true;   //判断主表是否需要上传资料
                    var list = BLL.SpotCheckDetailService.GetOKSpotCheckDetails(SpotCheckCode);
                    if (list.Count == 0)   //没有合格项，则在上传资料页面不显示该主表记录
                    {
                        isShow = false;
                    }
                    else
                    {
                        bool isExitForms = false;
                        foreach (var item in list)
                        {
                            Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(item.ControlItemAndCycleId);
                            if (c != null)
                            {
                                if (!string.IsNullOrEmpty(c.HGForms) || !string.IsNullOrEmpty(c.SHForms))
                                {
                                    isExitForms = true;
                                    break;
                                }
                            }
                        }
                        if (!isExitForms)   //不存在有表格需上传的明细记录
                        {
                            isShow = false;
                        }
                    }
                    spotCheck.IsShow = isShow;
                    foreach (var item in list)
                    {
                        //更新明细记录
                        Model.Check_SpotCheck spotCheck1 = BLL.SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);
                        //判断明细是否需要上传资料
                        Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(item.ControlItemAndCycleId);
                        if (c != null)
                        {
                            if (string.IsNullOrEmpty(c.HGForms) && string.IsNullOrEmpty(c.SHForms))
                            {
                                item.IsShow = false;
                                item.IsDataOK = "2";    //资料情况为不需要
                                item.State = BLL.Const.SpotCheck_Complete;
                                item.HandleMan = null;
                            }
                            else
                            {
                                item.IsShow = true;
                                item.State = BLL.Const.SpotCheck_Audit5;
                                item.HandleMan = spotCheck1.CreateMan;
                            }
                        }
                        BLL.SpotCheckDetailService.UpdateSpotCheckDetail(item);
                        if (item.IsShow == true)
                        {
                            //新增待办记录
                            Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
                            approve.SpotCheckCode = SpotCheckCode;
                            approve.ApproveMan = spotCheck1.CreateMan;
                            approve.ApproveType = BLL.Const.SpotCheck_Audit5;
                            approve.Sign = "2";
                            approve.SpotCheckDetailId = item.SpotCheckDetailId;
                            BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
                        }
                    }
                }
                BLL.SpotCheckService.UpdateSpotCheck(spotCheck);
            }
            else
            {
                spotCheck.CreateMan = this.CurrUser.UserId;
                spotCheck.CreateDate = DateTime.Now;
                if (!string.IsNullOrEmpty(this.hdSpotCheckCode.Text))
                {
                    spotCheck.SpotCheckCode = this.hdSpotCheckCode.Text;
                }
                else
                {
                    spotCheck.SpotCheckCode = SQLHelper.GetNewID(typeof(Model.Check_SpotCheck));
                }
                if (saveType == "save")
                {
                    spotCheck.SaveHandleMan = this.drpHandleMan.SelectedValue;
                }
                BLL.SpotCheckService.AddSpotCheck(spotCheck);
                if (saveType == "submit")
                {
                    Model.Check_SpotCheckApprove approve1 = new Model.Check_SpotCheckApprove();
                    approve1.SpotCheckCode = spotCheck.SpotCheckCode;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.SpotCheck_Compile;
                    approve1.Sign = "1";
                    BLL.SpotCheckApproveService.AddSpotCheckApprove(approve1);

                    Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
                    approve.SpotCheckCode = spotCheck.SpotCheckCode;
                    if (this.drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = this.drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = this.drpHandleType.SelectedValue;
                    approve.Sign = "1";
                    BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "工序验收待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                else
                {
                    Model.Check_SpotCheckApprove approve1 = new Model.Check_SpotCheckApprove();
                    approve1.SpotCheckCode = spotCheck.SpotCheckCode;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.SpotCheck_Compile;
                    approve1.Sign = "1";
                    BLL.SpotCheckApproveService.AddSpotCheckApprove(approve1);
                }
                jerqueSaveList();
                foreach (var detail in details)
                {
                    detail.SpotCheckCode = spotCheck.SpotCheckCode;
                    BLL.SpotCheckDetailService.AddSpotCheckDetail(detail);
                }
            }
            BLL.LogService.AddSys_Log(this.CurrUser, spotCheck.DocCode, SpotCheckCode, BLL.Const.SpotCheckMenuId, "编辑工序验收记录");
        }
        #endregion

        /// <summary>
        /// 办理人员的自动筛选
        /// </summary>
        protected void HandleMan()
        {
            drpHandleMan.Items.Clear();
            if (!string.IsNullOrEmpty(drpHandleType.SelectedText))
            {
                
                if (drpHandleType.SelectedText.Contains("总包"))
                {
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, string.Empty);
                }
                else if (drpHandleType.SelectedText.Contains("监理"))
                {
                    UserService.InitJLUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false);
                }
                else
                {
                    UserService.InitYZUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false);
                }
                if (drpHandleMan.Items.Count > 0)
                {
                    drpHandleMan.SelectedIndex = 0;
                }
                if (drpHandleType.SelectedText.Contains("重新编制") || drpHandleType.SelectedText.Contains("分包"))
                {
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, drpUnit.SelectedValue);
                    var HandleMan = BLL.SpotCheckApproveService.GetComplie(this.SpotCheckCode);
                    if (HandleMan != null)
                    {
                        this.drpHandleMan.SelectedValue = HandleMan.ApproveMan;
                    }
                }
                if (drpHandleType.SelectedValue == BLL.Const.SpotCheck_Complete)
                {
                    drpHandleMan.Items.Clear();
                    drpHandleMan.Enabled = false;
                    drpHandleMan.Required = false;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;
                }
            }
        }

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleMan();
        }

        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleType();
        }

        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {

        }

        /// <summary>
        /// 待办事项的下拉框的处理
        /// </summary>
        public void HandleType()
        {
            drpHandleType.Items.Clear();
            if (this.drpControlPointType.SelectedValue != BLL.Const._Null)
            {
                SpotCheckService.Init(drpHandleType, State, this.drpControlPointType.SelectedValue, false);
                string res = null;
                List<string> list = new List<string>();
                list.Add(Const.SpotCheck_ReCompile);
                var count = drpHandleType.Items.Count;
                List<ListItem> listitem = new List<ListItem>();
                if (rblIsAgree.SelectedValue.Equals("true"))
                {
                    //drpHandleType.SelectedIndex = 0;
                    for (int i = 0; i < count; i++)
                    {
                        res = drpHandleType.Items[i].Value;
                        if (list.Contains(res))
                        {
                            var item = (drpHandleType.Items[i]);
                            listitem.Add(item);
                        }
                    }
                    if (listitem.Count > 0)
                    {
                        for (int i = 0; i < listitem.Count; i++)
                        {
                            drpHandleType.Items.Remove(listitem[i]);
                        }
                    }
                }
                else
                {
                    //drpHandleType.SelectedIndex = 1;
                    for (int i = 0; i < count; i++)
                    {
                        res = drpHandleType.Items[i].Value;
                        if (!list.Contains(res))
                        {
                            var item = drpHandleType.Items[i];
                            listitem.Add(item);
                        }
                    }
                    if (listitem.Count > 0)
                    {
                        for (int i = 0; i < listitem.Count; i++)
                        {
                            drpHandleType.Items.Remove(listitem[i]);
                        }
                    }
                }
                if (count > 0)
                {
                    drpHandleType.SelectedIndex = 0;
                    HandleMan();
                }
                //HandleMan();
            }
        }

        protected void rblCheckDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCheckDateType.SelectedValue == "1")
            {
                this.txtSpotCheckDate.Label = "共检时间";
                this.txtSpotCheckDate2.Hidden = true;
            }
            else
            {
                this.txtSpotCheckDate.Label = "开始时间";
                this.txtSpotCheckDate2.Hidden = false;
            }
        }

        protected void drpControlPointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpControlPointType.SelectedValue == "D")
            {
                this.drpJointCheckMans.Enabled = true;
                this.drpJointCheckMans2.Enabled = true;
                this.drpJointCheckMans3.Enabled = true;
            }
            else
            {
                this.drpJointCheckMans.SelectedValue = BLL.Const._Null;
                this.drpJointCheckMans.Enabled = false;
                this.drpJointCheckMans2.SelectedValue = BLL.Const._Null;
                this.drpJointCheckMans2.Enabled = false;
                this.drpJointCheckMans3.SelectedValue = BLL.Const._Null;
                this.drpJointCheckMans3.Enabled = false;
            }
            HandleType();
        }

        protected void drpJointCheckMans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] array = this.drpJointCheckMans.SelectedValueArray;
            List<string> str = new List<string>();
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str.Add(item);
                }

            }
            this.drpJointCheckMans.SelectedValueArray = str.ToArray();
        }

        protected void drpJointCheckMans2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] array = this.drpJointCheckMans2.SelectedValueArray;
            List<string> str = new List<string>();
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str.Add(item);
                }

            }
            this.drpJointCheckMans2.SelectedValueArray = str.ToArray();
        }

        protected void drpJointCheckMans3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] array = this.drpJointCheckMans3.SelectedValueArray;
            List<string> str = new List<string>();
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str.Add(item);
                }

            }
            this.drpJointCheckMans3.SelectedValueArray = str.ToArray();
        }

        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpControlPointType.SelectedValue == "D")
            {
                this.drpJointCheckMans.Enabled = true;
                this.drpJointCheckMans2.Enabled = true;
                this.drpJointCheckMans3.Enabled = true;
            }
            else
            {
                this.drpJointCheckMans.SelectedValue = BLL.Const._Null;
                this.drpJointCheckMans.Enabled = false;
                this.drpJointCheckMans2.SelectedValue = BLL.Const._Null;
                this.drpJointCheckMans2.Enabled = false;
                this.drpJointCheckMans3.SelectedValue = BLL.Const._Null;
                this.drpJointCheckMans3.Enabled = false;
            }
            HandleType();
        }
    }
}