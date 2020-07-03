using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class ControlItemProjectEdit : PageBase
    {
        /// <summary>
        /// 分部分项编号
        /// </summary>
        public string WorkPackageCode
        {
            get
            {
                return (string)ViewState["WorkPackageCode"];
            }
            set
            {
                ViewState["WorkPackageCode"] = value;
            }
        }

        /// <summary>
        /// 工作包编号
        /// </summary>
        public string ControlItemCode
        {
            get
            {
                return (string)ViewState["ControlItemCode"];
            }
            set
            {
                ViewState["ControlItemCode"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.FineUIPleaseSelect(this.drpControlPoint);
                WorkPackageCode = Request.Params["WorkPackageCode"];
                var workPackage = BLL.WorkPackageProjectService.GetWorkPackageProjectByWorkPackageCode(WorkPackageCode, this.CurrUser.LoginProjectId);
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
                if (Request.Params["type"] == "add")
                {
                    string newCode = string.Empty;
                    List<Model.WBS_ControlItemProject> list = BLL.ControlItemProjectService.GetItemsByWorkPackageCode(WorkPackageCode, this.CurrUser.LoginProjectId);
                    if (list != null)
                    {
                        string oldCode = list[list.Count - 1].ControlItemCode;
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
                        newCode = WorkPackageCode + "01";
                    }
                    this.txtControlItemCode.Text = newCode;
                }
                if (Request.Params["type"] == "modify")
                {
                    ControlItemCode = Request.Params["ControlItemCode"];
                    Model.WBS_ControlItemProject controlItem = BLL.ControlItemProjectService.GetControlItemProjectByCode(ControlItemCode, this.CurrUser.LoginProjectId);
                    WorkPackageCode = controlItem.WorkPackageCode;
                    this.txtControlItemCode.Text = controlItem.ControlItemCode;
                    this.txtControlItemContent.Text = controlItem.ControlItemContent;
                    this.drpControlPoint.SelectedValue = controlItem.ControlPoint;
                    this.txtControlItemDef.Text = controlItem.ControlItemDef;
                    if (controlItem.Weights != null)
                    {
                        this.txtWeights.Text = controlItem.Weights.ToString();
                    }
                    this.txtHGForms.Text = controlItem.HGForms;
                    this.txtSHForms.Text = controlItem.SHForms;
                    this.txtStandard.Text = controlItem.Standard;
                    this.txtClauseNo.Text = controlItem.ClauseNo;
                    if (controlItem.CheckNum != null)
                    {
                        this.txtCheckNum.Text = controlItem.CheckNum.ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemProjectSetMenuId, BLL.Const.BtnSave))
            {
                if (!BLL.ControlItemProjectService.IsExistControlItemProjectName(this.WorkPackageCode, this.txtControlItemContent.Text.Trim(), this.txtControlItemCode.Text.Trim(), this.CurrUser.LoginProjectId))
                {
                    Model.WBS_ControlItemProject newControlItemProject = new Model.WBS_ControlItemProject();
                    newControlItemProject.ControlItemCode = this.txtControlItemCode.Text.Trim();
                    newControlItemProject.ProjectId = this.CurrUser.LoginProjectId;
                    newControlItemProject.WorkPackageCode = this.WorkPackageCode;
                    newControlItemProject.ControlItemContent = this.txtControlItemContent.Text.Trim();
                    newControlItemProject.ControlPoint = this.drpControlPoint.SelectedValue;
                    newControlItemProject.ControlItemDef = this.txtControlItemDef.Text.Trim();
                    if (!string.IsNullOrEmpty(this.txtWeights.Text.Trim()))
                    {
                        newControlItemProject.Weights = Convert.ToDecimal(this.txtWeights.Text.Trim());
                    }
                    newControlItemProject.HGForms = this.txtHGForms.Text.Trim();
                    newControlItemProject.SHForms = this.txtSHForms.Text.Trim();
                    newControlItemProject.Standard = this.txtStandard.Text.Trim();
                    newControlItemProject.ClauseNo = this.txtClauseNo.Text.Trim();
                    if (!string.IsNullOrEmpty(this.txtCheckNum.Text.Trim()))
                    {
                        newControlItemProject.CheckNum = Convert.ToInt32(this.txtCheckNum.Text.Trim());
                    }
                    if (Request.Params["type"] == "add")
                    {
                        BLL.ControlItemProjectService.AddControlItemProject(newControlItemProject);
                        BLL.LogService.AddSys_Log(this.CurrUser, newControlItemProject.ControlItemCode, newControlItemProject.ControlItemCode, BLL.Const.ControlItemProjectSetMenuId, "增加工作包信息！");
                        //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtControlItemCode.Text.Trim()) + ActiveWindow.GetHidePostBackReference());
                    }
                    if (Request.Params["type"] == "modify")
                    {
                        BLL.ControlItemProjectService.UpdateControlItemProject(newControlItemProject);
                        BLL.LogService.AddSys_Log(this.CurrUser, newControlItemProject.ControlItemCode, newControlItemProject.ControlItemCode, BLL.Const.ControlItemProjectSetMenuId, "修改工作包信息！");
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