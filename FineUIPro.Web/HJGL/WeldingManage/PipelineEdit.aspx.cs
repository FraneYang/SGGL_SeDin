using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using BLL;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class PipelineEdit : PageBase
    {
        #region 定义项
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

        /// <summary>
        /// 项目主键
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

        /// <summary>
        /// 单位主键
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }
        

        /// <summary>
        /// 单位工程
        /// </summary>
        public string UnitWorkId
        {
            get
            {
                return (string)ViewState["UnitWorkId"];
            }
            set
            {
                ViewState["UnitWorkId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PipelineId = Request.Params["PipelineId"];

                Base_MediumService.InitMediumDropDownList(this.drpMedium, this.CurrUser.LoginProjectId, null, true);
                Base_MediumService.InitMediumDropDownList(this.drpTestMedium, this.CurrUser.LoginProjectId, true, true);
                Base_PipingClassService.InitPipingClassDropDownList(this.drpPipingClass, this.CurrUser.LoginProjectId, true,"请选择");
                Base_DetectionRateService.InitDetectionRateDropDownList(drpDetectionRate, true);
                Base_DetectionTypeService.InitDetectionTypeDropDownList(drpDetectionType, false, string.Empty);

                if (!string.IsNullOrEmpty(this.PipelineId))
                {
                    Model.View_HJGL_Pipeline pipeline = BLL.PipelineService.GetViewPipelineByPipelineId(this.PipelineId);
                    this.ProjectId = pipeline.ProjectId;
                    this.UnitId = pipeline.UnitId;
                    this.UnitWorkId = pipeline.UnitWorkId;
                    this.txtPipelineCode.Text = pipeline.PipelineCode;
                    this.txtUnitName.Text = pipeline.UnitName;
                    this.txtWorkAreaCode.Text = pipeline.UnitWorkCode;
                   
                    if (!string.IsNullOrEmpty(pipeline.MediumId))
                    {
                        this.drpMedium.SelectedValue = pipeline.MediumId;
                    }
                    if (!string.IsNullOrEmpty(pipeline.PipingClassId))
                    {
                        this.drpPipingClass.SelectedValue = pipeline.PipingClassId;
                    }
                    if (!string.IsNullOrEmpty(pipeline.DetectionRateId))
                    {
                        drpDetectionRate.SelectedValue = pipeline.DetectionRateId;
                    }
                    if (!string.IsNullOrEmpty(pipeline.DetectionType))
                    {
                        string[] dtype = pipeline.DetectionType.Split('|');
                        drpDetectionType.SelectedValueArray = dtype;
                    }
                    if (!string.IsNullOrEmpty(pipeline.TestMedium))
                    {
                        drpTestMedium.SelectedValue = pipeline.TestMedium;
                    }

                    this.txtSingleNumber.Text = pipeline.SingleNumber;
                    if (pipeline.TestPressure!=null)
                    {
                        numTestPressure.Text = pipeline.TestPressure.Value.ToString();
                    }
                    
                    this.txtRemark.Text = pipeline.Remark;

                    this.UnitWorkId = pipeline.UnitWorkId;
                }
                else
                {
                    this.UnitWorkId = Request.Params["UnitWorkId"];
                    Model.WBS_UnitWork workArea = BLL.UnitWorkService.getUnitWorkByUnitWorkId(this.UnitWorkId);
                    if (workArea != null)
                    {
                        this.ProjectId = workArea.ProjectId;
                        this.UnitId = workArea.UnitId;
                        //this.InstallationId = workArea.InstallationId;
                        this.txtWorkAreaCode.Text = workArea.UnitWorkCode;
                        this.txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(this.UnitId);
                    }
                }
            }
        }
        #endregion


        #region 管线信息保存事件
        /// <summary>
        /// 管线信息保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipelineMenuId, Const.BtnSave))
            {
                if (BLL.PipelineService.IsExistPipelineCode(this.txtPipelineCode.Text.Trim(), this.UnitWorkId, this.PipelineId))
                {
                    Alert.ShowInTop("该管线号已存在！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    SaveData(true);
                    // 关闭本窗体，然后回发父窗体
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }

            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        private void SaveData(bool b)
        {
            Model.HJGL_Pipeline pipeline = new Model.HJGL_Pipeline();
            pipeline.ProjectId = this.ProjectId;
            //pipeline.InstallationId = this.InstallationId;
            pipeline.UnitId = this.UnitId;
            pipeline.UnitWorkId = this.UnitWorkId;
            pipeline.PipelineCode = this.txtPipelineCode.Text.Trim();
            if (this.drpDetectionRate.SelectedValue != BLL.Const._Null)
            {
                pipeline.DetectionRateId = this.drpDetectionRate.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择探伤比例！");
                return;
            }

            if (this.drpMedium.SelectedValue != BLL.Const._Null)
            {
                pipeline.MediumId = this.drpMedium.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择介质！");
                return;
            }
            if (this.drpPipingClass.SelectedValue != BLL.Const._Null)
            {
                pipeline.PipingClassId = this.drpPipingClass.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择管线等级！");
                return;
            }

            if (this.drpTestMedium.SelectedValue != BLL.Const._Null)
            {
                pipeline.TestMedium = drpTestMedium.SelectedValue;
            }

            pipeline.SingleNumber = this.txtSingleNumber.Text.Trim();
            if (!string.IsNullOrEmpty(numTestPressure.Text))
            {
                pipeline.TestPressure = Convert.ToDecimal(numTestPressure.Text);
            }
            if (this.drpDetectionType.SelectedValue != BLL.Const._Null)
            {
                pipeline.DetectionType = String.Join("|", drpDetectionType.SelectedValueArray);
            }
            pipeline.Remark = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.PipelineId))
            {
                pipeline.PipelineId = this.PipelineId;
                BLL.PipelineService.UpdatePipeline(pipeline);
                if (b)
                {
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipelineMenuId, Const.BtnModify, this.PipelineId);
                }
            }
            else
            {
                this.PipelineId = SQLHelper.GetNewID(typeof(Model.HJGL_Pipeline));
                pipeline.PipelineId = this.PipelineId;
                BLL.PipelineService.AddPipeline(pipeline);
                if (b)
                {
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipelineMenuId, Const.BtnAdd, this.PipelineId);
                }
            }
        }
        #endregion

       
    }
}