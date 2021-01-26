using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class WeldJointEdit : PageBase
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
                Base_PipingClassService.InitPipingClassDropDownList(this.drpPipingClass, this.CurrUser.LoginProjectId, true, "请选择");//管线等级
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
                this.drpDetectionTypeId.DataValueField = "DetectionTypeId";
                this.drpDetectionTypeId.DataTextField = "DetectionTypeCode";
                //Funs.FineUIPleaseSelect(this.drpDetectionTypeId);
                string weldJointId = Request.Params["WeldJointId"];
                if (!string.IsNullOrEmpty(weldJointId))
                {
                    Model.HJGL_WeldJoint joint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
                    Model.WPQ_WPQList list = BLL.WPQListServiceService.GetWPQById(joint.WPQId);
                    this.txtWpqId.Text = joint.WPQId;

                    if (joint != null)
                    {
                        this.PipelineId = joint.PipelineId;
                        if (list != null)
                        {
                            this.txtWPQCode.Text = list.WPQCode;
                        }

                        this.txtWeldJointCode.Text = joint.WeldJointCode;
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
                        if (!string.IsNullOrEmpty(joint.WeldTypeId))
                        {
                            drpWeldTypeCode.SelectedValue = joint.WeldTypeId;
                        }
                        if (!string.IsNullOrEmpty(joint.DetectionTypeId))
                        {
                            List<Model.Base_DetectionType> dList = new List<Model.Base_DetectionType>();
                            if (drpWeldTypeCode.SelectedItem.Text == "B")
                            {
                                Model.Base_DetectionType rt = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("RT");
                                Model.Base_DetectionType ut = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("UT");
                                dList.Add(rt);
                                dList.Add(ut);
                            }
                            else
                            {
                                Model.Base_DetectionType mt = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("MT");
                                Model.Base_DetectionType pt = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("PT");
                                dList.Add(mt);
                                dList.Add(pt);
                            }
                            this.drpDetectionTypeId.DataSource = dList;
                            this.drpDetectionTypeId.DataBind();
                            this.drpDetectionTypeId.SelectedValue = joint.DetectionTypeId;
                        }
                        if (!string.IsNullOrEmpty(joint.Components1Id))
                        {
                            drpComponent1.SelectedValue = joint.Components1Id;
                        }
                        if (!string.IsNullOrEmpty(joint.Components2Id))
                        {
                            drpComponent2.SelectedValue = joint.Components2Id;
                        }
                        this.txtPreTemperature.Text = joint.PreTemperature;
                        this.txtSpecification.Text = joint.Specification;
                        txtRemark.Text= joint.Remark;
                        drpIsHotProess.SelectedValue = joint.IsHotProess.Value.ToString();
                        if (!string.IsNullOrEmpty(joint.DesignIsHotProess.ToString()))
                        {
                            drpDesignIsHotProess.SelectedValue = joint.DesignIsHotProess.Value.ToString();
                        }
                        
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
                if (this.drpPipingClass.SelectedValue == BLL.Const._Null || this.drpMaterial1.SelectedValue == BLL.Const._Null || this.drpMaterial2.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("材质1、材质2不能为空", MessageBoxIcon.Warning);
                    return;
                }
                Model.HJGL_Pipeline pipeline = BLL.PipelineService.GetPipelineByPipelineId(this.PipelineId);
                //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txtWpqId.ClientID, txtWPQCode.ClientID, drpWeldingRod.ClientID, drpWeldingWire.ClientID, drpWeldingMethodId.ClientID, drpGrooveType.ClientID, txtPreTemperature.ClientID, drpMaterial1.ClientID,drpMaterial2.ClientID,txtIsHotProess.ClientID)
                //    + Window1.GetShowReference(String.Format("SelectWPS.aspx?Material1={0}&Material2={1}&Dia={2}&Thickness={3}&UnitId={4}&WeldingMethod={5}&WeldType={6}", this.drpMaterial1.SelectedValue, this.drpMaterial2.SelectedValue, this.txtDia.Text, this.txtThickness.Text,pipeline.UnitId,this.drpWeldingMethodId.SelectedText,this.drpWeldTypeCode.SelectedText, "维护 - ")));
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txtWpqId.ClientID, txtWPQCode.ClientID, drpWeldingRod.ClientID, drpWeldingWire.ClientID, drpWeldingMethodId.ClientID, drpGrooveType.ClientID, txtPreTemperature.ClientID, txtIsHotProess.ClientID)
                   + Window1.GetShowReference(String.Format("SelectWPS.aspx?Material1={0}&Material2={1}&Dia={2}&Thickness={3}&UnitId={4}&WeldingMethod={5}&WeldType={6}", this.drpMaterial1.SelectedValue, this.drpMaterial2.SelectedValue, this.txtDia.Text, this.txtThickness.Text, pipeline.UnitId, this.drpWeldingMethodId.SelectedText, this.drpWeldTypeCode.SelectedText, "维护 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldJointMenuId, Const.BtnSave))
            {
                int joint = Funs.GetNewIntOrZero(this.txtWeldJointCode.Text.Trim());
                string weldJointCode = string.Empty;
                if (joint != 0)
                {
                    if (joint < 10)
                    {
                        weldJointCode = "0" + Convert.ToString(joint);
                    }
                    else
                    {
                        weldJointCode = Convert.ToString(joint);
                    }
                }
                else
                {
                    weldJointCode = this.txtWeldJointCode.Text;
                }
                if (BLL.WeldJointService.IsExistWeldJointCode(weldJointCode, this.PipelineId, Request.Params["WeldJointId"]))
                {
                    Alert.ShowInTop("该管线焊口已存在！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    SaveData();
                }

            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }

        }
        /// <summary>
        /// 提交数据
        /// </summary>
        private void SaveData()
        {
            if (string.IsNullOrEmpty(this.txtWeldJointCode.Text.Trim()) || this.drpPipingClass.SelectedValue == BLL.Const._Null || this.drpMaterial1.SelectedValue == BLL.Const._Null || this.drpMaterial2.SelectedValue == BLL.Const._Null || this.drpWeldTypeCode.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("页面必填项不能为空", MessageBoxIcon.Warning);
                Alert.ShowInTop("请完善必填项！", MessageBoxIcon.Warning);
                return;
            }
            Model.HJGL_WeldJoint joint = new Model.HJGL_WeldJoint();
            string weldJointId = Request.Params["WeldJointId"];
            joint.PipelineId = this.PipelineId;
            joint.PipelineCode = txtPipelineCode.Text.Trim();
            joint.ProjectId = this.CurrUser.LoginProjectId;

            int jointCode = Funs.GetNewIntOrZero(this.txtWeldJointCode.Text.Trim());
            if (jointCode != 0)
            {
                if (jointCode < 10)
                {
                    joint.WeldJointCode = "0" + Convert.ToString(jointCode);
                }
                else
                {
                    joint.WeldJointCode =  Convert.ToString(jointCode);
                }
            }
            else
            {
                if (this.txtWeldJointCode.Text.Contains("G")) {
                    Alert.ShowInTop("焊口编号不能包含G！", MessageBoxIcon.Warning);
                    return;
                }
                joint.WeldJointCode = this.txtWeldJointCode.Text;
            }

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
            if (this.drpWeldTypeCode.SelectedValue != BLL.Const._Null)
            {
                joint.WeldTypeId = drpWeldTypeCode.SelectedValue;
            }
            joint.PreTemperature = this.txtPreTemperature.Text;
            joint.Specification = this.txtSpecification.Text;
            //var DetectionType = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode(this.txtDetectionTypeId.Text.Trim());
            if (this.drpDetectionTypeId.SelectedValue != null)
            {
                joint.DetectionTypeId = this.drpDetectionTypeId.SelectedValue;
            }
            bool flag = false;
            if (Convert.ToBoolean(drpDesignIsHotProess.SelectedValue)) {
                flag = true;
            }
            if (flag)
            {
                joint.IsHotProess = true;
            }
            else {
                joint.IsHotProess = false;
            }
            joint.IsHotProess = Convert.ToBoolean(drpIsHotProess.SelectedValue);
            joint.DesignIsHotProess = Convert.ToBoolean(drpDesignIsHotProess.SelectedValue);
            joint.Remark = txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(weldJointId))
            {
                if (this.txtWpqId.Text != "")
                {
                    joint.WPQId = this.txtWpqId.Text;
                }
               
                joint.WeldJointId = weldJointId;
                BLL.WeldJointService.UpdateWeldJoint(joint);
            }
            else
            {
                if (this.txtWpqId.Text != "")
                {
                    joint.WPQId = this.txtWpqId.Text;
                }
                string newId = SQLHelper.GetNewID(typeof(Model.HJGL_WeldJoint));
                joint.WeldJointId = newId;
                BLL.WeldJointService.AddWeldJoint(joint);
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 根据焊缝类型获取探伤类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWeldTypeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpDetectionTypeId.Items.Clear();
            if (this.drpWeldTypeCode.SelectedValue != Const._Null)
            {
                //var DetectionType = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(drpWeldTypeCode.SelectedValue);
                //List<string> WTDetectionTypetr = new List<string>();
                //List<string> PiPDetectionTypestr = new List<string>();
                ////根据焊缝类型获取探伤类型
                //if (!string.IsNullOrEmpty(DetectionType.DetectionType))
                //{
                //    string[] dtype = DetectionType.DetectionType.Split('|');

                //    for (int i = 0; i < dtype.Length; i++)
                //    {
                //        var DetectionTypeCode = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(dtype[i].ToString());
                //        WTDetectionTypetr.Add(DetectionTypeCode.DetectionTypeCode);
                //    }
                //}
                ////管线表探伤类型
                //if (!string.IsNullOrEmpty(this.txtDetectionType.Text.Trim()))
                //{
                //    string[] dtype = this.txtDetectionType.Text.Trim().Split(',');
                //    for (int i = 0; i < dtype.Length; i++)
                //    {
                //        PiPDetectionTypestr.Add(dtype[i].ToString());
                //    }
                //}

                //var newlist = WTDetectionTypetr.Intersect(PiPDetectionTypestr).ToArray();
                //if (newlist.Length > 0)
                //{
                //    this.txtDetectionTypeId.Text = newlist[0].ToString();
                //}
                //else
                //{
                //    this.txtDetectionTypeId.Text = string.Empty;
                //}
                List<Model.Base_DetectionType> dList = new List<Model.Base_DetectionType>();
                if (drpWeldTypeCode.SelectedItem.Text == "B")
                {
                    Model.Base_DetectionType rt = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("RT");
                    Model.Base_DetectionType ut = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("UT");
                    dList.Add(rt);
                    dList.Add(ut);
                    this.drpDetectionTypeId.DataSource = dList;
                    this.drpDetectionTypeId.DataBind();
                    this.drpDetectionTypeId.SelectedValue = rt.DetectionTypeId;
                }
                else
                {
                    Model.Base_DetectionType mt = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("MT");
                    Model.Base_DetectionType pt = BLL.Base_DetectionTypeService.GetDetectionTypeIdByDetectionTypeCode("PT");
                    dList.Add(mt);
                    dList.Add(pt);
                    this.drpDetectionTypeId.DataSource = dList;
                    this.drpDetectionTypeId.DataBind();
                    this.drpDetectionTypeId.SelectedValue = pt.DetectionTypeId;
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
            if (!string.IsNullOrEmpty(txtWpqId.Text))
            {
                var wps = BLL.WPQListServiceService.GetWPQById(txtWpqId.Text.Trim());

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
            if (!string.IsNullOrEmpty(txtWpqId.Text))
            {
                var wps = BLL.WPQListServiceService.GetWPQById(txtWpqId.Text.Trim());

                if (drpMaterial1.SelectedValue != Const._Null)
                {
                    var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(drpMaterial1.SelectedValue);
                    string matClass = mat.MaterialClass;
                    if (matClass == "Fe-1" || matClass == "Fe-3")
                    {
                        var wpsWire = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(wps.WeldingWire);
                        var matWire= BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(drpWeldingWire.SelectedValue);
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

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            var IsHotProess = this.txtIsHotProess.Text;
            if (IsHotProess == "True")
            {
                this.drpIsHotProess.SelectedValue = "True";
            }
            else {
                if (!Convert.ToBoolean(drpDesignIsHotProess.SelectedValue )) {
                    this.drpIsHotProess.SelectedValue = "False";
                }
            }
        }

        protected void drpDesignIsHotProess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpDesignIsHotProess.SelectedValue != BLL.Const._Null) {
                if (Convert.ToBoolean(drpDesignIsHotProess.SelectedValue))
                {
                    this.drpIsHotProess.SelectedValue = "True";
                }
                else {
                    if (this.txtIsHotProess.Text == "False") {
                        this.drpIsHotProess.SelectedValue = "False";
                    }
                }
            }
        }
    }
}