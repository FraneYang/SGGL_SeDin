using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.WPQ
{
    public partial class WPQView : PageBase
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
                        BindGrid(wpq.WPQId);
                    }
                }
                else
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }

        public void BindGrid(string WPQId)
        {
            string strSql = @"select FlowOperateId, WPQId, OperateName, OperateManId, OperateTime,U.UserName   
                              from WPQ_WPQListFlowOperate Operate left join Sys_User U on Operate.OperateManId=U.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where WPQId= @WPQId";
            listStr.Add(new SqlParameter("@WPQId", WPQId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGLServer/WeldingManage&menuId={1}&edit={2}", wpqId, Const.WPQListMenuId, edit)));
            }
        }
        #endregion
        


    }
}