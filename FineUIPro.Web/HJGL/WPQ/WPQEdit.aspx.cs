﻿using System;
using BLL;

namespace FineUIPro.Web.HJGL.WPQ
{
    public partial class WPQEdit : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit,this.CurrUser.LoginProjectId,Const.ProjectUnitType_2,true);
                BLL.Base_MaterialService.InitMaterialDropDownList(this.drpSteel1, true, "请选择");//材质1
                BLL.Base_MaterialService.InitMaterialDropDownList(this.drpSteel2, true, "请选择");//材质2
                BLL.Base_WeldingMethodService.InitWeldingMethodDropDownList(this.drpWeldingMethodId, true,"请选择");//焊接方法
                BLL.Base_ConsumablesService.InitConsumablesDropDownList(this.drpWeldingRod, true,"2", "请选择");//焊材类型
                BLL.Base_ConsumablesService.InitConsumablesDropDownList(this.drpWeldingWire, true, "1", "请选择");//焊材类型
                BLL.Base_GrooveTypeService.InitGrooveTypeDropDownList(this.drpGrooveType, true, "请选择");//焊材类型
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string wpqId = Request.Params["WPQId"];
                if (!string.IsNullOrEmpty(wpqId))
                {
                    Model.WPQ_WPQList wpq = BLL.WPQListServiceService.GetWPQById(wpqId);
                    if (wpq != null)
                    {
                        this.txtWeldingProcedureCode.Text = wpq.WPQCode;
                        if (wpq.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", wpq.CompileDate);
                        }
                        if (!string.IsNullOrEmpty(wpq.MaterialId1))
                        {
                            this.drpSteel1.SelectedValue = wpq.MaterialId1;
                            var mat1 = BLL.Base_MaterialService.GetMaterialByMaterialId(wpq.MaterialId1);
                            if (mat1 != null)
                            {
                                if (!string.IsNullOrEmpty(mat1.MaterialClass))
                                {
                                    this.txtMaterialClass1.Text = mat1.MaterialClass;
                                }
                                if (!string.IsNullOrEmpty(mat1.MaterialGroup))
                                {
                                    this.txtMaterialGroup1.Text = mat1.MaterialGroup;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(wpq.MaterialId2))
                        {
                            this.drpSteel2.SelectedValue = wpq.MaterialId2;
                            var mat2 = BLL.Base_MaterialService.GetMaterialByMaterialId(wpq.MaterialId2);
                            if (mat2 != null)
                            {
                                if (!string.IsNullOrEmpty(mat2.MaterialClass))
                                {
                                    this.txtMaterialClass2.Text = mat2.MaterialClass;
                                }
                                if (!string.IsNullOrEmpty(mat2.MaterialGroup))
                                {
                                    this.txtMaterialGroup2.Text = mat2.MaterialGroup;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(wpq.UnitId))
                        {
                            drpUnit.SelectedValue = wpq.UnitId;
                        }
                        this.txtSpecifications.Text = wpq.Specifications;
                        if (!string.IsNullOrEmpty(wpq.WeldingRod)) {
                            drpWeldingRod.SelectedValue = wpq.WeldingRod;
                        }
                        if (!string.IsNullOrEmpty(wpq.WeldingWire))
                        {
                            drpWeldingWire.SelectedValue = wpq.WeldingWire;
                        }
                        if (!string.IsNullOrEmpty(wpq.GrooveType))
                        {
                            drpGrooveType.SelectedValue = wpq.GrooveType;
                        }
                        //this.txtWeldingModel.Text = wpq.WeldingRod;
                        //this.txtWeldingGrade.Text = wpq.WeldingWire;
                        //this.txtWeldingSpecifications.Text = wpq.GrooveType;
                        this.txtWeldingPosition.Text = wpq.WeldingPosition;
                        if (!string.IsNullOrEmpty(wpq.WeldingMethodId))
                        {
                            this.drpWeldingMethodId.SelectedValue = wpq.WeldingMethodId;
                        }
                        if (wpq.MinImpactDia != null)
                        {
                            this.txtMinImpactDia.Text = Convert.ToString(wpq.MinImpactDia);
                        }
                        if (wpq.MaxImpactDia != null)
                        {
                            this.txtMaxImpactDia.Text = Convert.ToString(wpq.MaxImpactDia);
                        }
                        if (wpq.MinImpactThickness != null)
                        {
                            this.txtMinImpactThickness.Text = Convert.ToString(wpq.MinImpactThickness);
                        }
                        if (wpq.MaxImpactThickness != null)
                        {
                            this.txtMaxImpactThickness.Text = Convert.ToString(wpq.MaxImpactThickness);
                        }
                        if (wpq.NoMinImpactThickness != null)
                        {
                            this.txtNoMinImpactThickness.Text = Convert.ToString(wpq.NoMinImpactThickness);
                        }
                        if (wpq.NoMaxImpactThickness != null)
                        {
                            this.txtNoMaxImpactThickness.Text = Convert.ToString(wpq.NoMaxImpactThickness);
                        }
                        this.txtWPQStandard.Text = wpq.WPQStandard;
                        if (wpq.IsHotProess == true)
                        {
                            this.cbkIsHotTreatment.Checked = true;
                        }
                        else
                        {
                            this.cbkIsHotTreatment.Checked = false;
                        }
                        this.txtPreTemperature.Text = wpq.PreTemperature;
                        this.txtRemark.Text = wpq.Remark;
                        if (!string.IsNullOrEmpty(wpq.JointType))
                        {
                            drpWeldType.SelectedValue = wpq.JointType;
                        }
                        this.txtProtectiveGas.Text = wpq.ProtectiveGas;
                    }
                }
                else
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }
        #endregion

        #region 提交
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("编制单位不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpSteel1.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("材质1不能为空！", MessageBoxIcon.Warning);
                return;
            }
            string id = SaveData();
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        private string SaveData()
        {
            Model.WPQ_WPQList wpq = new Model.WPQ_WPQList();
            wpq.WPQCode = this.txtWeldingProcedureCode.Text.Trim();
            wpq.UnitId = drpUnit.SelectedValue;
            wpq.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            if (this.drpSteel1.SelectedValue != BLL.Const._Null)
            {
                wpq.MaterialId1 = this.drpSteel1.SelectedValue;
            }
            if (this.drpSteel2.SelectedValue != BLL.Const._Null)
            {
                wpq.MaterialId2 = this.drpSteel2.SelectedValue;
            }
            wpq.Specifications = this.txtSpecifications.Text.Trim();
            if (this.drpWeldingRod.SelectedValue != BLL.Const._Null)
            {
                wpq.WeldingRod = this.drpWeldingRod.SelectedValue;
            }
            if (this.drpWeldingWire.SelectedValue != BLL.Const._Null)
            {
                wpq.WeldingWire = this.drpWeldingWire.SelectedValue;
            }
            if (this.drpGrooveType.SelectedValue != BLL.Const._Null)
            {
                wpq.GrooveType = this.drpGrooveType.SelectedValue;
            }
            //wpq.WeldingModel = this.txtWeldingModel.Text.Trim();
            //wpq.WeldingGrade = this.txtWeldingGrade.Text.Trim();
            //wpq.WeldingSpecifications = this.txtWeldingSpecifications.Text.Trim();
            wpq.WeldingPosition = this.txtWeldingPosition.Text.Trim();
            if (this.drpWeldingMethodId.SelectedValue != BLL.Const._Null)
            {
                wpq.WeldingMethodId = this.drpWeldingMethodId.SelectedValue;
            }
            wpq.PreTemperature = this.txtPreTemperature.Text;
            wpq.Remark = this.txtRemark.Text.Trim();
            wpq.MinImpactThickness = Funs.GetNewDecimal(this.txtMinImpactThickness.Text.Trim());
            wpq.MaxImpactThickness = Funs.GetNewDecimal(this.txtMaxImpactThickness.Text.Trim());
            wpq.NoMinImpactThickness = Funs.GetNewDecimal(this.txtNoMinImpactThickness.Text.Trim());
            wpq.NoMaxImpactThickness = Funs.GetNewDecimal(this.txtNoMaxImpactThickness.Text.Trim());
            if (this.cbkIsHotTreatment.Checked == true)
            {
                wpq.IsHotProess = true;
            }
            else
            {
                wpq.IsHotProess = false;
            }
            wpq.MinImpactDia = Funs.GetNewDecimal(this.txtMinImpactDia.Text.Trim());
            wpq.MaxImpactDia = Funs.GetNewDecimal(this.txtMaxImpactDia.Text.Trim());
            wpq.WPQStandard = this.txtWPQStandard.Text.Trim();
            wpq.JointType = this.drpWeldType.SelectedValue;
           
            wpq.ProtectiveGas = this.txtProtectiveGas.Text.Trim();
         
            string wpqId = Request.Params["WPQId"];
            if (!string.IsNullOrEmpty(wpqId))
            {
                wpq.WPQId = wpqId;
                BLL.WPQListServiceService.UpdateWPQ(wpq);
                //BLL.Sys_LogService.AddLog(Const.System_2, this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改焊接工艺评定台账");
            }
            else
            {
                string newId = SQLHelper.GetNewID(typeof(Model.WPQ_WPQList));
                wpq.WPQId = newId;
                BLL.WPQListServiceService.AddWPQ(wpq);
                //BLL.Sys_LogService.AddLog(Const.System_2, this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加焊接工艺评定台账");
            }
            return wpq.WPQId;
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            string wpqId = Request.Params["WPQId"];
            string edit = "0"; // 表示能打开附件上传窗口，但不能上传附件
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.WPQListMenuId, BLL.Const.BtnSave))
            {
                if (string.IsNullOrEmpty(wpqId))
                {
                    wpqId = SaveData();
                }
                edit = "1";
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGLServer/WeldingManage&menuId={1}&edit={2}", wpqId, Const.WPQListMenuId, edit)));
            }
        }
        #endregion

        #region DropDownList下拉选择事件
        /// <summary>
        /// 材质1下拉列表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSteel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpSteel1.SelectedValue != BLL.Const._Null)
            {
                var mat1 = BLL.Base_MaterialService.GetMaterialByMaterialId(this.drpSteel1.SelectedValue);
                if (mat1 != null)
                {
                    if (!string.IsNullOrEmpty(mat1.MaterialClass))
                    {
                        this.txtMaterialClass1.Text = mat1.MaterialClass;
                    }
                    if (!string.IsNullOrEmpty(mat1.MaterialGroup))
                    {
                        this.txtMaterialGroup1.Text = mat1.MaterialGroup;
                    }
                }
            }
        }

        /// <summary>
        /// 材质2下拉列表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSteel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpSteel2.SelectedValue != BLL.Const._Null)
            {
                var mat2 = BLL.Base_MaterialService.GetMaterialByMaterialId(this.drpSteel2.SelectedValue);
                if (mat2 != null)
                {
                    if (!string.IsNullOrEmpty(mat2.MaterialClass))
                    {
                        this.txtMaterialClass1.Text = mat2.MaterialClass;
                    }
                    if (!string.IsNullOrEmpty(mat2.MaterialGroup))
                    {
                        this.txtMaterialGroup1.Text = mat2.MaterialGroup;
                    }
                }
            }
        }
        #endregion

        protected void drpWeldingRod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpWeldingWire_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpGrooveType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}