using System;
using System.Linq;
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
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
                BLL.Base_MaterialService.InitMaterialDropDownList(this.drpSteel1, true, "请选择");//材质1
                BLL.Base_MaterialService.InitMaterialDropDownList(this.drpSteel2, true, "请选择");//材质2
                BLL.Base_WeldingMethodService.InitWeldingMethodDropDownList(this.drpWeldingMethodId, true, "请选择");//焊接方法
                BLL.Base_ConsumablesService.InitConsumablesDropDownList(this.drpWeldingRod, true, "2", "请选择");//焊材类型
                BLL.Base_ConsumablesService.InitConsumablesDropDownList(this.drpWeldingWire, true, "1", "请选择");//焊材类型
                BLL.Base_GrooveTypeService.InitGrooveTypeDropDownList(this.drpGrooveType, true, "请选择");//焊材类型
                BLL.UserService.InitUsersDropDownList(this.drpPerson, this.CurrUser.LoginProjectId, false, null);//审批人
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
                        }
                        if (!string.IsNullOrEmpty(wpq.MaterialId2))
                        {
                            this.drpSteel2.SelectedValue = wpq.MaterialId2;
                        }

                        if (!string.IsNullOrEmpty(wpq.Material1Class))
                        {
                            this.txtMaterialClass1.Text = wpq.Material1Class;
                        }
                        if (!string.IsNullOrEmpty(wpq.Material1Group))
                        {
                            this.txtMaterialGroup1.Text = wpq.Material1Group;
                        }

                        if (!string.IsNullOrEmpty(wpq.Material2Class))
                        {
                            this.txtMaterialClass2.Text = wpq.Material2Class;
                        }
                        if (!string.IsNullOrEmpty(wpq.Material2Group))
                        {
                            this.txtMaterialGroup2.Text = wpq.Material2Group;
                        }

                        if (!string.IsNullOrEmpty(wpq.UnitId))
                        {
                            drpUnit.SelectedValue = wpq.UnitId;
                        }
                        this.txtSpecifications.Text = wpq.Specifications;
                        if (!string.IsNullOrEmpty(wpq.WeldingRod))
                        {
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
                        if (wpq.MinCImpactDia != null)
                        {
                            this.txtMinCImpactDia.Text = Convert.ToString(wpq.MinCImpactDia);
                        }
                        if (wpq.MaxImpactDia != null)
                        {
                            this.txtMaxCImpactDia.Text = Convert.ToString(wpq.MaxCImpactDia);
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
                        if (wpq.State == "1")
                        {
                            rblFlowOperate.Hidden = false;
                            drpPerson.Hidden = true;
                        }
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
            if (this.drpWeldingWire.SelectedValue == BLL.Const._Null && this.drpWeldingRod.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("焊丝、焊条不能同时为空！", MessageBoxIcon.Warning);
                return;
            }
            string id = SaveData(BLL.Const.BtnSave);
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
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
            if (this.drpWeldingWire.SelectedValue == BLL.Const._Null && this.drpWeldingRod.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("焊丝、焊条不能同时为空！", MessageBoxIcon.Warning);
                return;
            }
            string id = SaveData(BLL.Const.BtnSubmit);
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        /// <summary>
        /// 提交数据
        /// </summary>
        private string SaveData(string type)
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

            wpq.Material1Class = this.txtMaterialClass1.Text.Trim();
            wpq.Material1Group = this.txtMaterialGroup1.Text.Trim();
            wpq.Material2Class = this.txtMaterialClass2.Text.Trim();
            wpq.Material2Group = this.txtMaterialGroup2.Text.Trim();
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
            wpq.MinCImpactDia = Funs.GetNewDecimal(this.txtMinCImpactDia.Text.Trim());
            wpq.MaxCImpactDia = Funs.GetNewDecimal(this.txtMaxCImpactDia.Text.Trim());
            wpq.WPQStandard = this.txtWPQStandard.Text.Trim();
            wpq.JointType = this.drpWeldType.SelectedValue;

            wpq.ProtectiveGas = this.txtProtectiveGas.Text.Trim();

            string wpqId = Request.Params["WPQId"];
            var GetWpq = BLL.WPQListServiceService.GetWPQById(wpqId);
            if (GetWpq != null)
            {
                if (type == BLL.Const.BtnSubmit)
                {
                    if (GetWpq.State == BLL.Const.State_0 || string.IsNullOrEmpty(GetWpq.State))
                    {
                        if (this.drpPerson.SelectedValue == BLL.Const._Null)
                        {
                            Alert.ShowInTop("请选择下一步办理人", MessageBoxIcon.Warning);
                            return "";
                        }
                        wpq.State = BLL.Const.State_1;
                        wpq.ApproveManId = this.drpPerson.SelectedValue;
                        SaveFlowOperate(GetWpq.WPQId, "施工单位编制");
                    }
                    else if (GetWpq.State == BLL.Const.State_1)
                    {
                        wpq.State = BLL.Const.State_2;
                        wpq.ApproveTime = DateTime.Now;
                        wpq.ApproveManId = null;
                        SaveFlowOperate(GetWpq.WPQId, "总包用户审核");
                    }
                }
                else
                {
                    wpq.State = GetWpq.State;
                    wpq.ApproveManId = GetWpq.ApproveManId;
                }
                wpq.WPQId = wpqId;
                BLL.WPQListServiceService.UpdateWPQ(wpq);
                //BLL.Sys_LogService.AddLog(Const.System_2, this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改焊接工艺评定台账");
            }
            else
            {
                string newId = SQLHelper.GetNewID(typeof(Model.WPQ_WPQList));
                wpq.WPQId = newId;
                if (type == BLL.Const.BtnSubmit)
                {
                    if (this.drpPerson.SelectedValue == BLL.Const._Null)
                    {
                        Alert.ShowInTop("请选择下一步办理人", MessageBoxIcon.Warning);
                        return "";
                    }
                    else
                    {
                        wpq.ApproveManId = drpPerson.SelectedValue;
                        wpq.State = BLL.Const.State_1;
                        SaveFlowOperate(wpq.WPQId, "施工单位编制");
                    }
                }
                else
                {
                    wpq.ApproveManId = this.CurrUser.UserId;
                    wpq.State = BLL.Const.State_0;
                }
                BLL.WPQListServiceService.AddWPQ(wpq);
                //BLL.Sys_LogService.AddLog(Const.System_2, this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加焊接工艺评定台账");
            }
            return wpq.WPQId;
        }
        #region 保存流程审核数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void SaveFlowOperate(string WpqId, string OperateName)
        {
            Model.WPQ_WPQListFlowOperate newFlowOperate = new Model.WPQ_WPQListFlowOperate();
            newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.WPQ_WPQListFlowOperate));
            newFlowOperate.WPQId = WpqId;
            newFlowOperate.OperateName = OperateName;
            newFlowOperate.OperateManId = CurrUser.UserId;
            newFlowOperate.OperateTime = DateTime.Now;
            Funs.DB.WPQ_WPQListFlowOperate.InsertOnSubmit(newFlowOperate);
            Funs.DB.SubmitChanges();

        }
        #endregion
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
                    wpqId = SaveData(BLL.Const.BtnSave);
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
                        this.txtMaterialClass2.Text = mat2.MaterialClass;
                    }
                    if (!string.IsNullOrEmpty(mat2.MaterialGroup))
                    {
                        this.txtMaterialGroup2.Text = mat2.MaterialGroup;
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