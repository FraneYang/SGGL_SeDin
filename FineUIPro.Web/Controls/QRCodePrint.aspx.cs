using System;

namespace FineUIPro.Web.Controls
{
    public partial class QRCodePrint : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.getQRUrl();
            }
        }

        private void getQRUrl()
        {
            if (!string.IsNullOrEmpty(Request.Params["PersonId"]))
            {
                ////人员
                var person = BLL.PersonService.GetPersonById(Request.Params["PersonId"]);
                if (person != null && !string.IsNullOrEmpty(person.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + person.QRCodeAttachUrl;
                    this.lbWedCode.Text = "人员卡号：" + person.CardNo;
                    this.lbWedName.Text = "人员姓名：" + person.PersonName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(person.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "人员单位：" + unitName;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["EquipmentQualityId"]))
            {
                ////设备
                var equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(Request.Params["EquipmentQualityId"]);
                if (equipmentQuality != null && !string.IsNullOrEmpty(equipmentQuality.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + equipmentQuality.QRCodeAttachUrl;
                    this.lbWedCode.Text = "出厂编号：" + equipmentQuality.FactoryCode;
                    this.lbWedName.Text = "设备名称：" + equipmentQuality.EquipmentQualityName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(equipmentQuality.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "所属单位：" + unitName;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["GeneralEquipmentQualityId"]))
            {
                ///一般设备
                var generalEquipmentQuality = BLL.GeneralEquipmentQualityService.GetGeneralEquipmentQualityById(Request.Params["GeneralEquipmentQualityId"]);
                if (generalEquipmentQuality != null && !string.IsNullOrEmpty(generalEquipmentQuality.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + generalEquipmentQuality.QRCodeAttachUrl;
                    this.lbWedCode.Text = "编号：" + BLL.CodeRecordsService.ReturnCodeByDataId(generalEquipmentQuality.GeneralEquipmentQualityId);
                    string equipmentTypeName = string.Empty;
                    Model.Base_SpecialEquipment equipmentType = BLL.SpecialEquipmentService.GetSpecialEquipmentById(generalEquipmentQuality.SpecialEquipmentId);
                    if (equipmentType != null)
                    {
                        equipmentTypeName = equipmentType.SpecialEquipmentName;
                    }
                    this.lbWedName.Text = "设备名称：" + equipmentTypeName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(generalEquipmentQuality.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "所属单位：" + unitName;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["ConstructSolutionId"]))
            {
                ///施工方案
                var constructSolution = BLL.ConstructSolutionService.GetConstructSolutionById(Request.Params["ConstructSolutionId"]);
                if (constructSolution != null && !string.IsNullOrEmpty(constructSolution.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + constructSolution.QRCodeAttachUrl;
                    string code = string.Empty;
                    if (!string.IsNullOrEmpty(constructSolution.ConstructSolutionCode))
                    {
                        code = constructSolution.ConstructSolutionCode;
                    }
                    else
                    {
                        code = BLL.CodeRecordsService.ReturnCodeByDataId(constructSolution.ConstructSolutionId);
                    }
                    this.lbWedCode.Text = "编号：" + code;
                    this.lbWedName.Text = "方案名称：" + constructSolution.ConstructSolutionName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(constructSolution.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "所属单位：" + unitName;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["TrainingPlanId"]))
            {
                ///培训计划
                var trainingPlan = BLL.TrainingPlanService.GetPlanById(Request.Params["TrainingPlanId"]);
                if (trainingPlan != null && !string.IsNullOrEmpty(trainingPlan.QRCodeUrl))
                {
                    this.imgPhoto.Src = "../" + trainingPlan.QRCodeUrl;
                    string code = string.Empty;
                    if (!string.IsNullOrEmpty(trainingPlan.PlanCode))
                    {
                        code = trainingPlan.PlanCode;
                    }
                    else
                    {
                        code = BLL.CodeRecordsService.ReturnCodeByDataId(trainingPlan.PlanId);
                    }
                    this.lbWedCode.Text = "编号：" + code;
                    this.lbWedName.Text = "名称：" + trainingPlan.PlanName;
                    this.lbUnitName.Text = "培训时间：" + string.Format("{0:yyyy-MM-dd}",trainingPlan.TrainStartDate)+"；培训地点："+trainingPlan.TeachAddress;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["TestPlanId"]))
            {
                ///培训计划
                var testPlan = BLL.TestPlanService.GetTestPlanById(Request.Params["TestPlanId"]);
                if (testPlan != null && !string.IsNullOrEmpty(testPlan.QRCodeUrl))
                {
                    this.imgPhoto.Src = "../" + testPlan.QRCodeUrl;
                    string code = string.Empty;
                    if (!string.IsNullOrEmpty(testPlan.PlanCode))
                    {
                        code = testPlan.PlanCode;
                    }
                    else
                    {
                        code = BLL.CodeRecordsService.ReturnCodeByDataId(testPlan.PlanId);
                    }
                    this.lbWedCode.Text = "编号：" + code;
                    this.lbWedName.Text = "名称：" + testPlan.PlanName;
                    this.lbUnitName.Text = "考试扫码时间：" + string.Format("{0:yyyy-MM-dd}", testPlan.TestStartTime) + "；考试地点：" + testPlan.TestPalce;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["ServerTestPlanId"]))
            {
                ///知识竞赛
                var testPlan = BLL.ServerTestPlanService.GetTestPlanById(Request.Params["ServerTestPlanId"]);
                if (testPlan != null && !string.IsNullOrEmpty(testPlan.QRCodeUrl))
                {
                    this.imgPhoto.Src = "../" + testPlan.QRCodeUrl;                  
                    this.lbWedCode.Text = "编号：" + testPlan.PlanCode;
                    this.lbWedName.Text = "名称：" + testPlan.PlanName;
                    this.lbUnitName.Text = "扫码考试时间：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", testPlan.TestStartTime) + "；至：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", testPlan.TestEndTime);
                }
            }
        }
    }
}