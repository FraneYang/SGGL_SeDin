using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class ControlItemAndCycleEdit : PageBase
    {
        /// <summary>
        /// 分部分项Id
        /// </summary>
        public string WorkPackageId
        {
            get
            {
                return (string)ViewState["WorkPackageId"];
            }
            set
            {
                ViewState["WorkPackageId"] = value;
            }
        }

        /// <summary>
        /// 工作包编号
        /// </summary>
        public string ControlItemAndCycleId
        {
            get
            {
                return (string)ViewState["ControlItemAndCycleId"];
            }
            set
            {
                ViewState["ControlItemAndCycleId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Funs.FineUIPleaseSelect(this.drpControlPoint);
                WorkPackageId = Request.Params["WorkPackageId"];
                var workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId);
                if (workPackage.ProjectType == "1") //建筑工程
                {
                    txtHGForms.Label = "对应的资料表格";
                    tr6.Hidden = true;
                }
                else
                {
                    txtHGForms.Text = "对应的化工资料表格";
                    tr6.Hidden = false;
                }
                List<Model.WBS_ControlItemAndCycle> list = BLL.ControlItemAndCycleService.GetListByWorkPackageId(WorkPackageId);
                if (Request.Params["type"] == "add")
                {
                    string newCode = string.Empty;
                    if (list.Count > 0)
                    {
                        string oldCode = list[list.Count - 1].ControlItemAndCycleCode;
                        string num = oldCode.Substring(oldCode.Length - 2, 2);
                        int a = Convert.ToInt32(num);
                        int b = a + 1;
                        string c;
                        if (b.ToString().Length == 1)
                        {
                            c = "0" + b.ToString();
                        }
                        else
                        {
                            c = b.ToString();
                        }
                        newCode = oldCode.Substring(0, oldCode.Length - 2) + c;
                    }
                    else
                    {
                        newCode = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(WorkPackageId).WorkPackageCode + "01";
                    }
                    this.txtControlItemAndCycleCode.Text = newCode;
                }
                if (Request.Params["type"] == "modify")
                {
                    ControlItemAndCycleId = Request.Params["ControlItemAndCycleId"];
                    Model.WBS_ControlItemAndCycle controlItemAndCycle = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId);
                    WorkPackageId = controlItemAndCycle.WorkPackageId;
                    this.txtControlItemAndCycleCode.Text = controlItemAndCycle.ControlItemAndCycleCode;
                    this.txtControlItemContent.Text = controlItemAndCycle.ControlItemContent;
                    this.drpControlPoint.SelectedValue = controlItemAndCycle.ControlPoint;
                    this.txtControlItemDef.Text = controlItemAndCycle.ControlItemDef;
                    if (controlItemAndCycle.Weights != null)
                    {
                        this.txtWeights.Text = controlItemAndCycle.Weights.ToString();
                    }
                    this.txtHGForms.Text = controlItemAndCycle.HGForms;
                    this.txtSHForms.Text = controlItemAndCycle.SHForms;
                    this.txtStandard.Text = controlItemAndCycle.Standard;
                    this.txtClauseNo.Text = controlItemAndCycle.ClauseNo;
                    if (controlItemAndCycle.CheckNum != null)
                    {
                        this.txtCheckNum.Text = controlItemAndCycle.CheckNum.ToString();
                    }
                    if (controlItemAndCycle.PlanCompleteDate != null)
                    {
                        this.txtPlanCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", controlItemAndCycle.PlanCompleteDate);
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemAndCycleMenuId, BLL.Const.BtnSave))
            {
                if (!BLL.ControlItemAndCycleService.IsExistControlItemAndCycleName(this.WorkPackageId, this.txtControlItemContent.Text.Trim(), this.txtControlItemAndCycleCode.Text.Trim(), this.CurrUser.LoginProjectId))
                {
                    Model.WBS_ControlItemAndCycle newControlItemAndCycle = new Model.WBS_ControlItemAndCycle();
                    newControlItemAndCycle.ControlItemAndCycleCode = this.txtControlItemAndCycleCode.Text.Trim();
                    newControlItemAndCycle.ProjectId = this.CurrUser.LoginProjectId;
                    newControlItemAndCycle.WorkPackageId = this.WorkPackageId;
                    newControlItemAndCycle.ControlItemContent = this.txtControlItemContent.Text.Trim();
                    newControlItemAndCycle.ControlPoint = this.drpControlPoint.SelectedValue;
                    newControlItemAndCycle.ControlItemDef = this.txtControlItemDef.Text.Trim();
                    newControlItemAndCycle.IsApprove = true;
                    if (!string.IsNullOrEmpty(this.txtWeights.Text.Trim()))
                    {
                        newControlItemAndCycle.Weights = Convert.ToDecimal(this.txtWeights.Text.Trim());
                    }
                    newControlItemAndCycle.HGForms = this.txtHGForms.Text.Trim();
                    newControlItemAndCycle.SHForms = this.txtSHForms.Text.Trim();
                    newControlItemAndCycle.Standard = this.txtStandard.Text.Trim();
                    newControlItemAndCycle.ClauseNo = this.txtClauseNo.Text.Trim();
                    if (!string.IsNullOrEmpty(this.txtCheckNum.Text.Trim()))
                    {
                        newControlItemAndCycle.CheckNum = Convert.ToInt32(this.txtCheckNum.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(this.txtPlanCompleteDate.Text.Trim()))
                    {
                        newControlItemAndCycle.PlanCompleteDate = Convert.ToDateTime(this.txtPlanCompleteDate.Text.Trim());
                    }
                    if (Request.Params["type"] == "add")
                    {
                        newControlItemAndCycle.ControlItemAndCycleId = SQLHelper.GetNewID(typeof(Model.WBS_ControlItemAndCycle));
                        BLL.ControlItemAndCycleService.AddControlItemAndCycle(newControlItemAndCycle);
                        BLL.LogService.AddSys_Log(this.CurrUser, newControlItemAndCycle.ControlItemAndCycleCode, newControlItemAndCycle.ControlItemAndCycleCode, BLL.Const.ControlItemAndCycleMenuId, "增加工作包信息！");
                        //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtControlItemCode.Text.Trim()) + ActiveWindow.GetHidePostBackReference());
                    }
                    if (Request.Params["type"] == "modify")
                    {
                        newControlItemAndCycle.ControlItemAndCycleId = ControlItemAndCycleId;
                        BLL.ControlItemAndCycleService.UpdateControlItemAndCycle(newControlItemAndCycle);
                        BLL.LogService.AddSys_Log(this.CurrUser, newControlItemAndCycle.ControlItemAndCycleCode, newControlItemAndCycle.ControlItemAndCycleCode, BLL.Const.ControlItemAndCycleMenuId, "修改工作包信息！");
                        //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtControlItemCode.Text.Trim()) + ActiveWindow.GetHidePostBackReference());
                    }
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    ShowNotify("此工作包已存在！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
    }
}