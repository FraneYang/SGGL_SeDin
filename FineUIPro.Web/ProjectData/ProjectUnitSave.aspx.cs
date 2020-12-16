using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectUnitSave : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string ProjectUnitId
        {
            get
            {
                return (string)ViewState["ProjectUnitId"];
            }
            set
            {
                ViewState["ProjectUnitId"] = value;
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                BLL.ConstValue.InitConstValueDropDownList(this.drpUnitType, ConstValue.Group_ProjectUnitType, true);
                BasicDataService.InitBasicDataProjectUnitDropDownList(this.drpIdcardType, "ZHENGJIAN_TYPE", true);
                this.ProjectUnitId = Request.QueryString["ProjectUnitId"];
                if (!String.IsNullOrEmpty(this.ProjectUnitId))
                {
                    var projectUnit = BLL.ProjectUnitService.GetProjectUnitById(this.ProjectUnitId);
                    if (projectUnit != null)
                    {
                        var project = BLL.ProjectService.GetProjectByProjectId(projectUnit.ProjectId);
                        if (project != null)
                        {
                            this.lbProjectName.Text = project.ProjectName;
                        }
                        var unit = BLL.UnitService.GetUnitByUnitId(projectUnit.UnitId);
                        if (unit != null)
                        {
                            this.lbUnitName.Text = unit.UnitName;
                            this.txtCollCropCode.Text = unit.CollCropCode;
                            this.txtLinkName.Text = unit.LinkName;
                            if (!string.IsNullOrEmpty(unit.IdcardType))
                            {
                                this.drpIdcardType.SelectedValue = unit.IdcardType;
                            }
                            this.txtIdcardNumber.Text = unit.IdcardNumber;
                            this.txtLinkMobile.Text = unit.LinkMobile;
                            if (!string.IsNullOrEmpty(unit.IsChina))
                            {
                                this.rblIsChina.SelectedValue = unit.IsChina;
                            }
                            if (!string.IsNullOrEmpty(unit.CollCropStatus))
                            {
                                this.rblCollCropStatus.SelectedValue = unit.CollCropStatus;
                            }
                        }
                        this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", projectUnit.InTime);
                        this.txtOutTime.Text = string.Format("{0:yyyy-MM-dd}", projectUnit.OutTime);
                        if (!string.IsNullOrEmpty(projectUnit.UnitType))
                        {
                            this.drpUnitType.SelectedValue = projectUnit.UnitType;
                        }
                        if (projectUnit.PlanCostA.HasValue)
                        {
                            this.nbPlanCostA.Text = projectUnit.PlanCostA.ToString();
                        }
                        if (projectUnit.PlanCostB.HasValue)
                        {
                            this.nbPlanCostB.Text = projectUnit.PlanCostB.ToString();
                        }
                        this.txtContractRange.Text = projectUnit.ContractRange;
                    }
                }
            }
        }
       
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var newProjectUnit = BLL.ProjectUnitService.GetProjectUnitById(this.ProjectUnitId);
            if (newProjectUnit != null)
            {
                newProjectUnit.InTime = Funs.GetNewDateTime(this.txtInTime.Text.Trim());
                newProjectUnit.OutTime = Funs.GetNewDateTime(this.txtOutTime.Text.Trim());
                if (this.drpUnitType.SelectedValue != BLL.Const._Null)
                {
                    newProjectUnit.UnitType = this.drpUnitType.SelectedValue;
                }
                newProjectUnit.PlanCostA = Funs.GetNewDecimalOrZero(this.nbPlanCostA.Text.Trim());
                newProjectUnit.PlanCostB = Funs.GetNewDecimalOrZero(this.nbPlanCostB.Text.Trim());
                newProjectUnit.ContractRange = this.txtContractRange.Text.Trim();
                BLL.ProjectUnitService.UpdateProjectUnit(newProjectUnit);
                Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(newProjectUnit.UnitId);
                if (unit != null)
                {
                    unit.IsChina = this.rblIsChina.SelectedValue;
                    unit.CollCropCode = this.txtCollCropCode.Text.Trim();
                    unit.LinkName = this.txtLinkName.Text.Trim();
                    if (this.drpIdcardType.SelectedValue != BLL.Const._Null)
                    {
                        unit.IdcardType = this.drpIdcardType.SelectedValue;
                    }
                    unit.IdcardNumber = this.txtIdcardNumber.Text.Trim();
                    unit.LinkMobile = this.txtLinkMobile.Text.Trim();
                    unit.CollCropStatus = this.rblCollCropStatus.SelectedValue;
                    BLL.UnitService.UpdateUnit(unit);
                }
                BLL.LogService.AddSys_Log(this.CurrUser, null, newProjectUnit.ProjectUnitId, BLL.Const.ProjectUnitMenuId, BLL.Const.BtnModify);
                ShowNotify("保存数据成功!", MessageBoxIcon.Success);
                // 2. 关闭本窗体，然后回发父窗体
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }               
    }
}