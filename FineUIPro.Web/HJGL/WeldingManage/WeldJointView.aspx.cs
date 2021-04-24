using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class WeldJointView : PageBase
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
                            this.txtWpqId.Text = list.WPQId;
                        }
                        this.txtWeldJointCode.Text = joint.WeldJointCode;
                        if (!string.IsNullOrEmpty(joint.Material1Id))
                        {
                            this.txtMaterial1.Text = BLL.Base_MaterialService.GetMaterialByMaterialId(joint.Material1Id).MaterialCode;
                        }
                        if (!string.IsNullOrEmpty(joint.Material2Id))
                        {
                            this.txtMaterial2.Text = BLL.Base_MaterialService.GetMaterialByMaterialId(joint.Material2Id).MaterialCode;
                        }
                        if (joint.Size != null)
                        {
                            this.txtSize.Text = joint.Size.Value.ToString("N0");
                        }
                        if (joint.Dia != null)
                        {
                            this.txtDia.Text = joint.Dia.Value.ToString("N0");
                        }
                        if (joint.Thickness != null)
                        {
                            this.txtThickness.Text = joint.Thickness.Value.ToString("N0");
                        }
                        if (!string.IsNullOrEmpty(joint.WeldingMethodId))
                        {
                            txtWeldingMethod.Text = BLL.Base_WeldingMethodService.GetWeldingMethodByWeldingMethodId(joint.WeldingMethodId).WeldingMethodCode;
                            hdWeldingMethodId.Text = joint.WeldingMethodId;
                        }
                        if (!string.IsNullOrEmpty(joint.WeldingRod))
                        {
                            txtWeldingRod.Text = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(joint.WeldingRod).ConsumablesName;
                        }
                        if (!string.IsNullOrEmpty(joint.WeldingWire))
                        {
                            txtWeldingWire.Text = BLL.Base_ConsumablesService.GetConsumablesByConsumablesId(joint.WeldingWire).ConsumablesName;
                        }
                        if (!string.IsNullOrEmpty(joint.GrooveTypeId))
                        {
                            txtGrooveType.Text = BLL.Base_GrooveTypeService.GetGrooveTypeByGrooveTypeId(joint.GrooveTypeId).GrooveTypeCode;
                            hdGrooveType.Text = joint.GrooveTypeId;
                        }
                        if (!string.IsNullOrEmpty(joint.WeldTypeId))
                        {
                            txtWeldTypeCode.Text = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(joint.WeldTypeId).WeldTypeCode;
                        }
                        if (!string.IsNullOrEmpty(joint.DetectionTypeId))
                        {
                            this.txtDetectionType2.Text = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(joint.DetectionTypeId).DetectionTypeCode;
                        }
                        if (!string.IsNullOrEmpty(joint.Components1Id))
                        {
                            txtComponent1.Text = Base_ComponentsService.GetComponentsByComponentsId(joint.Components1Id).ComponentsCode;
                        }
                        if (!string.IsNullOrEmpty(joint.Components2Id))
                        {
                            txtComponent2.Text = Base_ComponentsService.GetComponentsByComponentsId(joint.Components2Id).ComponentsCode;
                        }
                        this.txtPreTemperature.Text = joint.PreTemperature;
                        this.txtSpecification.Text = joint.Specification;
                        txtRemark.Text = joint.Remark;
                        if (joint.IsHotProess == true)
                        {
                            txtIsHotProess.Text = "是";
                        }
                        else
                        {
                            txtIsHotProess.Text = "否";
                        }
                        if (joint.DesignIsHotProess == true)
                        {
                            txtDesignIsHotProess.Text = "是";
                        }
                        else
                        {
                            txtDesignIsHotProess.Text = "否";
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
                    if (!string.IsNullOrEmpty(pipeline.PipingClassCode))
                    {
                        this.txtPipingClass.Text = pipeline.PipingClassCode;
                    }

                    if (!string.IsNullOrEmpty(pipeline.DetectionRateCode))
                    {
                        this.txtDetectionRate.Text = pipeline.DetectionRateCode;
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
    }
}