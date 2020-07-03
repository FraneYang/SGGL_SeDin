using Apache.NMS.ActiveMQ.Threads;
using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class SpotCheckView : PageBase
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SpotCheckCode = Request.Params["SpotCheckCode"];
                plApprove2.Hidden = true;
                if (!string.IsNullOrEmpty(SpotCheckCode))
                {
                    this.hdSpotCheckCode.Text = SpotCheckCode;
                    plApprove2.Hidden = false;
                    var dt = SpotCheckApproveService.getListData(SpotCheckCode);
                    gvApprove.DataSource = dt;
                    gvApprove.DataBind();
                    Model.Check_SpotCheck spotCheck = SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);
                    txtDocCode.Text = spotCheck.DocCode;
                    if (!string.IsNullOrEmpty(spotCheck.UnitId))
                    {
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(spotCheck.UnitId);
                        if (unit != null)
                        {
                            this.txtUnit.Text = unit.UnitName;
                        }
                    }
                    if (!string.IsNullOrEmpty(spotCheck.CNProfessionalCode))
                    {
                        Model.Base_CNProfessional cn = BLL.CNProfessionalService.GetCNProfessional(spotCheck.CNProfessionalCode);
                        if (cn != null)
                        {
                            this.txtCNProfessional.Text = cn.ProfessionalName;
                        }
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans))
                    {
                        this.txtJointCheckMans.Text = BLL.UserService.getUserNamesUserIds(spotCheck.JointCheckMans);
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans2))
                    {
                        this.txtJointCheckMans2.Text = BLL.UserService.getUserNamesUserIds(spotCheck.JointCheckMans2);
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans3))
                    {
                        this.txtJointCheckMans3.Text = BLL.UserService.getUserNamesUserIds(spotCheck.JointCheckMans3);
                    }
                    this.txtCheckDateType.Text = spotCheck.CheckDateType == "1" ? "点时间" : "段时间";
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
                    this.txtControlPointType.Text = spotCheck.ControlPointType == "C" ? "C级" : "非C级";
                    State = spotCheck.State;
                    //设置页面图片附件是否可以编辑
                    QuestionImg = -1;
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
                    if (!string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        Model.Check_SpotCheckApprove approve = BLL.SpotCheckApproveService.GetSee(SpotCheckCode, this.CurrUser.UserId);
                        if (approve != null)
                        {
                            approve.ApproveDate = DateTime.Now;
                            BLL.SpotCheckApproveService.UpdateSpotCheckApprove(approve);
                        }
                    }
                }
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                //是否同意触发
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

        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {

        }

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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CQMS/SpotCheck&menuId={1}&type=-1", itemId, BLL.Const.SpotCheckMenuId)));
            }
        }
    }
}