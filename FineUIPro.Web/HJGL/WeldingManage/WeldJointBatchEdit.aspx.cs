using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class WeldJointBatchEdit : PageBase
    {
        /// <summary>
        /// 管线主键
        /// </summary>
        public string PipelineId
        {
            get
            {
                return (string)ViewState["PipelineId"];
            }
            set
            {
                ViewState["PipelineId"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Base_MediumService.InitMediumDropDownList(this.drpMedium, true, "请选择");
                Base_PipingClassService.InitPipingClassDropDownList(this.drpPipingClass, this.CurrUser.LoginProjectId, true, "请选择");//管线
                Base_DetectionRateService.InitDetectionRateDropDownList(drpDetectionRate, true);
                BLL.Base_MaterialService.InitMaterialDropDownList(this.drpMaterial1, true, "请选择");//材质1
                BLL.Base_MaterialService.InitMaterialDropDownList(this.drpMaterial2, true, "请选择");//材质2
                BLL.Base_WeldTypeService.InitWeldTypeDropDownList(this.drpWeldTypeCode, "请选择", true);//焊缝类型

                BLL.Base_GrooveTypeService.InitGrooveTypeDropDownList(this.drpGrooveType, true, "请选择");//坡口类型
                BLL.Base_WeldingMethodService.InitWeldingMethodDropDownList(this.drpWeldingMethodId, true, "请选择");//焊接方法
                BLL.Base_ConsumablesService.InitConsumablesDropDownList(this.drpWeldingRod, true, "2", "请选择");//焊丝类型
                BLL.Base_ConsumablesService.InitConsumablesDropDownList(this.drpWeldingWire, true, "1", "请选择");//焊条类型
                BLL.Base_ComponentsService.InitComponentsDropDownList(this.drpComponent1, this.CurrUser.LoginProjectId, true, "请选择");//组件1
                BLL.Base_ComponentsService.InitComponentsDropDownList(this.drpComponent2, this.CurrUser.LoginProjectId, true, "请选择");//组件2

                ///焊接属性
                this.drpJointArea.DataTextField = "Text";
                this.drpJointArea.DataValueField = "Value";
                this.drpJointArea.DataSource = BLL.DropListService.HJGL_JointArea();
                this.drpJointArea.DataBind();
                ///焊口属性
                this.drpJointAttribute.DataTextField = "Text";
                this.drpJointAttribute.DataValueField = "Value";
                this.drpJointAttribute.DataSource = BLL.DropListService.HJGL_JointAttribute();
                this.drpJointAttribute.DataBind();
                string weldJointId = Request.Params["WeldJointId"];
                if (!string.IsNullOrEmpty(weldJointId))
                {
                    Model.HJGL_WeldJoint joint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
                    Model.WPQ_WPQList list = BLL.WPQListServiceService.GetWPQById(joint.WPQId);
                    if (joint != null)
                    {
                        this.PipelineId = joint.PipelineId;
                        if (list != null)
                        {
                            this.txtWPQCode.Text = list.WPQCode;
                        }

                        this.txtWeldJointCode.Text = joint.WeldJointCode;
                        if (!string.IsNullOrEmpty(joint.JointArea))
                        {
                            this.drpJointArea.SelectedValue = joint.JointArea;
                        }
                        if (!string.IsNullOrEmpty(joint.Material1Id))
                        {
                            this.drpMaterial1.SelectedValue = joint.Material1Id;
                        }
                        if (!string.IsNullOrEmpty(joint.Material2Id))
                        {
                            this.drpMaterial2.SelectedValue = joint.Material2Id;
                        }
                        this.txtSize.Text = Convert.ToString(joint.Size);
                        this.txtDia.Text = Convert.ToString(joint.Dia);
                        this.txtThickness.Text = Convert.ToString(joint.Thickness);
                        if (!string.IsNullOrEmpty(joint.WeldingMethodId))
                        {
                            drpWeldingMethodId.SelectedValue = joint.WeldingMethodId;
                        }
                        if (!string.IsNullOrEmpty(joint.WeldingRod))
                        {
                            drpWeldingRod.SelectedValue = joint.WeldingRod;
                        }
                        if (!string.IsNullOrEmpty(joint.WeldingWire))
                        {
                            drpWeldingWire.SelectedValue = joint.WeldingWire;
                        }
                        if (!string.IsNullOrEmpty(joint.GrooveTypeId))
                        {
                            drpGrooveType.SelectedValue = joint.GrooveTypeId;
                        }
                        if (!string.IsNullOrEmpty(joint.JointArea))
                        {
                            drpJointArea.SelectedValue = joint.JointArea;
                        }
                        if (!string.IsNullOrEmpty(joint.WeldTypeId))
                        {
                            drpWeldTypeCode.SelectedValue = joint.WeldTypeId;
                        }
                        if (!string.IsNullOrEmpty(joint.DetectionTypeId))
                        {
                            var DetectionTypeCode = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(joint.DetectionTypeId);
                            this.txtDetectionTypeId.Text = DetectionTypeCode.DetectionTypeCode;
                        }
                        if (!string.IsNullOrEmpty(joint.Components1Id))
                        {
                            drpComponent1.SelectedValue = joint.Components1Id;
                        }
                        if (!string.IsNullOrEmpty(joint.Components2Id))
                        {
                            drpComponent2.SelectedValue = joint.Components2Id;
                        }
                        if (!string.IsNullOrEmpty(joint.JointAttribute))
                        {
                            drpJointAttribute.SelectedValue = joint.JointAttribute;
                        }
                        this.txtHeartNo1.Text = joint.HeartNo1;
                        this.txtHeartNo2.Text = joint.HeartNo2;
                        this.txtPreTemperature.Text = joint.PreTemperature;
                        this.txtSpecification.Text = joint.Specification;

                        drpIsHotProess.SelectedValue = joint.IsHotProess.Value.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(Request.Params["PipelineId"]))
                {
                    this.PipelineId = Request.Params["PipelineId"];
                }

                Model.View_HJGL_Pipeline pipeline = BLL.PipelineService.GetViewPipelineByPipelineId(this.PipelineId);
                if (pipeline != null)
                {
                    if (!string.IsNullOrEmpty(pipeline.PipingClassId))
                    {
                        this.drpPipingClass.SelectedValue = pipeline.PipingClassId;
                    }

                    if (!string.IsNullOrEmpty(pipeline.DetectionRateId))
                    {
                        this.drpDetectionRate.SelectedValue = pipeline.DetectionRateId;
                    }
                    if (!string.IsNullOrEmpty(pipeline.DetectionType))
                    {
                        string[] dtype = pipeline.DetectionType.Split('|');
                        string DetectionTypestr = "";
                        for (int i = 0; i < dtype.Length; i++)
                        {
                            var DetectionType = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(dtype[i].ToString());
                            if (i == 0)
                            {
                                DetectionTypestr = DetectionType.DetectionTypeCode;
                            }
                            else
                            {
                                DetectionTypestr += "," + DetectionType.DetectionTypeCode;
                            }
                        }
                        this.txtDetectionType.Text = DetectionTypestr;
                    }
                    this.txtPipelineCode.Text = pipeline.PipelineCode;
                }

            }
        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_WeldJointMenuId, BLL.Const.BtnModify))
            {
                Model.HJGL_Pipeline pipeline = BLL.PipelineService.GetPipelineByPipelineId(this.PipelineId);
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txtWPQId.ClientID, txtWPQCode.ClientID, drpWeldingRod.ClientID, drpWeldingWire.ClientID, drpWeldingMethodId.ClientID, drpGrooveType.ClientID, txtPreTemperature.ClientID, drpMaterial1.ClientID, drpMaterial2.ClientID) 
                    + Window1.GetShowReference(String.Format("SelectWPS.aspx?Material1={0}&Material2={1}&Dia={2}&Thickness={3}&UnitId={4}&WeldingMethod={5}&WeldType={6}", this.drpMaterial1.SelectedValue, this.drpMaterial2.SelectedValue, this.txtDia.Text, this.txtThickness.Text,pipeline.UnitId, this.drpWeldingMethodId.SelectedText, this.drpWeldTypeCode.SelectedText, "维护 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();

        }
        /// <summary>
        /// 提交数据
        /// </summary>
        private void SaveData()
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_WeldJointMenuId, BLL.Const.BtnSave))
            {
                if (this.drpPipingClass.SelectedValue == BLL.Const._Null || this.drpMaterial1.SelectedValue == BLL.Const._Null || this.drpWeldTypeCode.SelectedValue == BLL.Const._Null || this.drpJointArea.SelectedValue == BLL.Const._Null || this.drpJointAttribute.SelectedValue==BLL.Const._Null)
                {
                    Alert.ShowInTop("页面必填项不能为空", MessageBoxIcon.Warning);
                    return;
                }
                int startInt = Funs.GetNewIntOrZero(this.txtWeldJointCode1.Text.Trim());
                int endInt = Funs.GetNewIntOrZero(this.txtWeldJointCode2.Text.Trim());
                if (endInt < startInt)
                {
                    Alert.ShowInTop("止数应大于等于起数", MessageBoxIcon.Warning);
                    return;
                }
                Model.HJGL_WeldJoint joint = new Model.HJGL_WeldJoint();
                joint.PipelineId = this.PipelineId;
                joint.PipelineCode = txtPipelineCode.Text.Trim();
                joint.ProjectId = this.CurrUser.LoginProjectId;
                joint.WeldJointCode = this.txtWeldJointCode.Text;
                if (this.drpMaterial1.SelectedValue != BLL.Const._Null)
                {
                    joint.Material1Id = this.drpMaterial1.SelectedValue;
                }
                if (this.drpMaterial2.SelectedValue != BLL.Const._Null)
                {
                    joint.Material2Id = this.drpMaterial2.SelectedValue;
                }
                joint.Size = Funs.GetNewDecimal(this.txtSize.Text.Trim());
                joint.Dia = Funs.GetNewDecimal(this.txtDia.Text.Trim());
                joint.Thickness = Funs.GetNewDecimal(this.txtThickness.Text.Trim());
                joint.HeartNo1 = this.txtHeartNo1.Text;
                joint.HeartNo2 = this.txtHeartNo2.Text;
                if (this.drpComponent1.SelectedValue != BLL.Const._Null)
                {
                    joint.Components1Id = this.drpComponent1.SelectedValue;
                }
                if (this.drpComponent2.SelectedValue != BLL.Const._Null)
                {
                    joint.Components2Id = this.drpComponent2.SelectedValue;
                }
                if (this.drpWeldingMethodId.SelectedValue != BLL.Const._Null)
                {
                    joint.WeldingMethodId = drpWeldingMethodId.SelectedValue;
                }
                if (this.drpWeldingRod.SelectedValue != BLL.Const._Null)
                {
                    joint.WeldingRod = drpWeldingRod.SelectedValue;
                }
                if (this.drpWeldingWire.SelectedValue != BLL.Const._Null)
                {
                    joint.WeldingWire = drpWeldingWire.SelectedValue;
                }
                if (this.drpGrooveType.SelectedValue != BLL.Const._Null)
                {
                    joint.GrooveTypeId = drpGrooveType.SelectedValue;
                }
                if (this.drpJointArea.SelectedValue != BLL.Const._Null)
                {
                    joint.JointArea = drpJointArea.SelectedValue;
                }
                if (this.drpWeldTypeCode.SelectedValue != BLL.Const._Null)
                {
                    joint.WeldTypeId = drpWeldTypeCode.SelectedValue;
                }
                if (this.drpJointAttribute.SelectedValue != BLL.Const._Null)
                {
                    joint.JointAttribute = drpJointAttribute.SelectedValue;
                }
                joint.PreTemperature = this.txtPreTemperature.Text;
                joint.Specification = this.txtSpecification.Text;
                var DetectionType = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode(this.txtDetectionTypeId.Text.Trim());
                joint.DetectionTypeId = DetectionType.DetectionTypeId;
                joint.IsHotProess = Convert.ToBoolean(drpIsHotProess.SelectedValue);

                if (this.txtWPQId.Text != "")
                {
                    joint.WPQId = this.txtWPQId.Text;
                }

                for (int i = startInt; i <= endInt; i++)
                {
                    if (i < 10)
                    {
                        joint.WeldJointCode = this.txtWeldJointCode.Text.Trim() + "0" + Convert.ToString(i);
                    }
                    else
                    {
                        joint.WeldJointCode = this.txtWeldJointCode.Text.Trim() + Convert.ToString(i);
                    }

                    if (!BLL.WeldJointService.IsExistWeldJointCode(this.txtWeldJointCode.Text.Trim(), this.PipelineId, string.Empty))
                    {
                        BLL.WeldJointService.AddWeldJoint(joint);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldJointMenuId, Const.BtnAdd, string.Empty);
                        ShowNotify("提交成功！", MessageBoxIcon.Success);
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }


        protected void drpWeldTypeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpWeldTypeCode.SelectedValue != Const._Null)
            {
                var weldType = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(drpWeldTypeCode.SelectedValue);
                List<string> WTDetectionTypetr = new List<string>();
                List<string> PiPDetectionTypestr = new List<string>();
                //根据焊缝类型获取探伤类型
                if (!string.IsNullOrEmpty(weldType.DetectionType))
                {
                    string[] dtype = weldType.DetectionType.Split('|');

                    for (int i = 0; i < dtype.Length; i++)
                    {
                        var DetectionTypeCode = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(dtype[i].ToString());
                        WTDetectionTypetr.Add(DetectionTypeCode.DetectionTypeCode);
                    }
                }
                //管线表探伤类型
                if (!string.IsNullOrEmpty(this.txtDetectionType.Text.Trim()))
                {
                    string[] dtype = this.txtDetectionType.Text.Trim().Split(',');
                    for (int i = 0; i < dtype.Length; i++)
                    {
                        PiPDetectionTypestr.Add(dtype[i].ToString());
                    }
                }

                var newlist = WTDetectionTypetr.Intersect(PiPDetectionTypestr).ToArray();
                if (newlist.Length > 0)
                {
                    this.txtDetectionTypeId.Text = newlist[0].ToString(); ;
                }

            }
        }

        /// <summary>
        /// 焊丝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWeldingRod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWPQId.Text))
            {
                var wps = BLL.WPQListServiceService.GetWPQById(txtWPQId.Text.Trim());

                if (drpMaterial1.SelectedValue != Const._Null)
                {
                    var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(drpMaterial1.SelectedValue);
                    string matClass = mat.MaterialClass;
                    if (matClass == "Fe-1" || matClass == "Fe-3")
                    {
                        var wpsRod = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(wps.WeldingRod);
                        var matRod = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(drpWeldingRod.SelectedValue);
                        if (IsCoverClass(wpsRod.SteelType, matRod.SteelType))
                        {

                        }
                        else
                        {
                            Alert.ShowInTop("焊口焊材强度需大于等于WPS焊材强度！", MessageBoxIcon.Warning);
                            drpWeldingWire.SelectedValue = wps.WeldingRod;
                        }
                    }


                }
                else
                {
                    drpWeldingWire.SelectedValue = wps.WeldingRod;
                }
            }
        }

        /// <summary>
        /// 焊条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWeldingWire_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWPQId.Text))
            {
                var wps = BLL.WPQListServiceService.GetWPQById(txtWPQId.Text.Trim());

                if (drpMaterial1.SelectedValue != Const._Null)
                {
                    var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(drpMaterial1.SelectedValue);
                    string matClass = mat.MaterialClass;
                    if (matClass == "Fe-1" || matClass == "Fe-3")
                    {
                        var wpsWire = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(wps.WeldingWire);
                        var matWire = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(drpWeldingWire.SelectedValue);
                        if (IsCoverClass(wpsWire.SteelType, matWire.SteelType))
                        {

                        }
                        else
                        {
                            Alert.ShowInTop("焊口焊材强度需大于等于WPS焊材强度！", MessageBoxIcon.Warning);
                            drpWeldingWire.SelectedValue = wps.WeldingWire;
                        }
                    }


                }
                else
                {
                    drpWeldingWire.SelectedValue = wps.WeldingWire;
                }
            }
        }

        /// <summary>
        /// 判断耗材强度是否大于WPS耗材强度，如是为true,否则为false
        /// </summary>
        /// <param name="wpsClass"></param>
        /// <param name="matClass"></param>
        /// <returns></returns>
        private bool IsCoverClass(string wpsClass, string matClass)
        {
            bool isCover = false;
            int wpsSn = 0;
            int matSn = 0;
            string wpsPre = wpsClass.Substring(0, wpsClass.Length - 2);
            string matPre = matClass.Substring(0, matClass.Length - 2);

            string wps = wpsClass.Substring(wpsClass.Length - 1, 1);
            wpsSn = Funs.GetNewInt(wps).HasValue ? Funs.GetNewInt(wps).Value : 0;

            string mat = matClass.Substring(matClass.Length - 1, 1);
            matSn = Funs.GetNewInt(mat).HasValue ? Funs.GetNewInt(mat).Value : 0;

            if (wpsPre == matPre && matSn >= wpsSn)
            {
                return true;
            }
            return isCover;
        }

        #region 外径、壁厚输入框事件
        /// <summary>
        /// 选择外径和壁厚自动获取规格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtText_TextChanged(object sender, EventArgs e)
        {
            string dn = string.Empty;
            string s = string.Empty;
            if (!string.IsNullOrEmpty(this.txtDia.Text.Trim()))
            {
                dn = this.txtDia.Text.Trim();
                decimal dia = Funs.GetNewDecimalOrZero(this.txtDia.Text.Trim());
                var inch = BLL.Base_DNCompareService.GetSizeByDia(dia);

                if (inch != null)
                {
                    this.txtSize.Text = Convert.ToString(inch);
                }

                if (!string.IsNullOrEmpty(this.txtThickness.Text.Trim()))
                {
                    this.txtSpecification.Text = "Φ" + dn + "*" + this.txtThickness.Text.Trim();
                }
            }
        }
        #endregion

    }
}