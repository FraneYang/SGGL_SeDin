using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class TestingEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string DetectionTypeId
        {
            get
            {
                return (string)ViewState["DetectionTypeId"];
            }
            set
            {
                ViewState["DetectionTypeId"] = value;
            }
        }
        #endregion

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
                this.txtDetectionTypeCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                //this.drpSysType.DataTextField = "Text";
                //this.drpSysType.DataValueField = "Value";
                //this.drpSysType.DataSource = BLL.DropListService.HJGL_GetTestintTypeList();
                //this.drpSysType.DataBind();

                this.DetectionTypeId = Request.Params["DetectionTypeId"];
                if (!string.IsNullOrEmpty(this.DetectionTypeId))
                {
                    Model.Base_DetectionType DetectionType = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(this.DetectionTypeId);
                    if (DetectionType != null)
                    {
                        this.txtDetectionTypeCode.Text = DetectionType.DetectionTypeCode;
                        this.txtDetectionTypeName.Text = DetectionType.DetectionTypeName;                       
                        //this.drpSysType.SelectedValue = DetectionType.SysType;
                        this.txtSecuritySpace.Text = DetectionType.SecuritySpace.ToString();
                        this.txtInjuryDegree.Text = DetectionType.InjuryDegree;                        
                        this.txtRemark.Text = DetectionType.Remark;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_DetectionType.FirstOrDefault(x => x.DetectionTypeCode == this.txtDetectionTypeCode.Text.Trim() && (x.DetectionTypeId != this.DetectionTypeId || (this.DetectionTypeId == null && x.DetectionTypeId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此检测方法代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_DetectionType.FirstOrDefault(x => x.DetectionTypeName == this.txtDetectionTypeName.Text.Trim() && (x.DetectionTypeId != this.DetectionTypeId || (this.DetectionTypeId == null && x.DetectionTypeId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此检测方法名称已经存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_DetectionType newDetectionType = new Model.Base_DetectionType
            {
                DetectionTypeCode = this.txtDetectionTypeCode.Text.Trim(),
                DetectionTypeName = this.txtDetectionTypeName.Text.Trim(),
                //SysType=this.drpSysType.SelectedValue,
                SecuritySpace = Funs.GetNewDecimal(this.txtSecuritySpace.Text.Trim()),
                InjuryDegree=this.txtInjuryDegree.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };

            if (!string.IsNullOrEmpty(this.DetectionTypeId))
            {
                newDetectionType.DetectionTypeId = this.DetectionTypeId;
                BLL.Base_DetectionTypeService.UpdateDetectionType(newDetectionType);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DetectionTypeMenuId, Const.BtnModify, newDetectionType.DetectionTypeId);
            }
            else
            {
                this.DetectionTypeId = SQLHelper.GetNewID(typeof(Model.Base_DetectionType));
                newDetectionType.DetectionTypeId = this.DetectionTypeId;
                BLL.Base_DetectionTypeService.AddDetectionType(newDetectionType);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_DetectionTypeMenuId, Const.BtnAdd, newDetectionType.DetectionTypeId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}