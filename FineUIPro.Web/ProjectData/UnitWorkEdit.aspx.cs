using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.ProjectData
{
    public partial class UnitWorkEdit : PageBase
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);//施工
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpSupervisorUnit, this.CurrUser.LoginProjectId, Const.ProjectUnitType_3, true);//监理
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpNDEUnit, this.CurrUser.LoginProjectId, Const.ProjectUnitType_5, true);//检测
                string UnitWorkId = Request.Params["UnitWorkId"];
                if (!string.IsNullOrEmpty(UnitWorkId))
                {

                    Model.WBS_UnitWork UnitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(UnitWorkId);
                    if (UnitWork != null)
                    {
                        this.ProjectId = UnitWork.ProjectId;
                        this.txtUnitWorkCode.Text = UnitWork.UnitWorkCode;
                        this.txtUnitWorkName.Text = UnitWork.UnitWorkName;
                        if (!string.IsNullOrEmpty(UnitWork.ProjectType))
                        {
                            this.drpProjectType.SelectedValue = UnitWork.ProjectType;
                        }
                        if (UnitWork.Weights != null)
                        {
                            this.txtWeights.Text = UnitWork.Weights.ToString();
                        }
                        if (!string.IsNullOrEmpty(UnitWork.UnitId)) {
                            this.drpUnit.SelectedValue = UnitWork.UnitId;
                        }
                        if (!string.IsNullOrEmpty(UnitWork.SupervisorUnitId))
                        {
                            this.drpSupervisorUnit.SelectedValue = UnitWork.SupervisorUnitId;
                        }
                        if (!string.IsNullOrEmpty(UnitWork.NDEUnit))
                        {
                            this.drpNDEUnit.SelectedValue = UnitWork.NDEUnit;
                        }
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        private void SaveData(bool b)
        {
            //if (this.drpProjectType.SelectedValue == BLL.Const._Null)
            //{
            //    Alert.ShowInTop("所属工程不能为空！", MessageBoxIcon.Warning);
            //    return;
            //}
            string UnitWorkId = Request.Params["UnitWorkId"];
            Model.WBS_UnitWork UnitWork = new Model.WBS_UnitWork();
            UnitWork.ProjectId = this.CurrUser.LoginProjectId;
            UnitWork.UnitWorkCode = this.txtUnitWorkCode.Text.Trim();
            UnitWork.UnitWorkName = this.txtUnitWorkName.Text.Trim();
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                UnitWork.UnitId = this.drpUnit.SelectedValue;
            }
            else {
                Alert.ShowInTop("请选择施工单位！");
                return;
            }
            if (this.drpSupervisorUnit.SelectedValue != BLL.Const._Null)
            {
                UnitWork.SupervisorUnitId = this.drpSupervisorUnit.SelectedValue;
            }
            if (this.drpNDEUnit.SelectedValue != BLL.Const._Null)
            {
                UnitWork.NDEUnit = this.drpNDEUnit.SelectedValue;
            }
            if (this.drpProjectType.SelectedValue != BLL.Const._Null)
            {
                UnitWork.ProjectType = drpProjectType.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtWeights.Text.Trim()))
            {
                UnitWork.Weights = Convert.ToDecimal(this.txtWeights.Text.Trim());
            }
            if (!string.IsNullOrEmpty(UnitWorkId))
            {
                UnitWork.UnitWorkId = UnitWorkId;
                BLL.UnitWorkService.UpdateUnitWork(UnitWork);
            }
            else
            {
                UnitWork.UnitWorkId = SQLHelper.GetNewID(typeof(Model.WBS_UnitWork));
                BLL.UnitWorkService.AddUnitWork(UnitWork);

            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}