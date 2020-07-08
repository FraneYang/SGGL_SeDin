using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class WeldEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string WeldTypeId
        {
            get
            {
                return (string)ViewState["WeldTypeId"];
            }
            set
            {
                ViewState["WeldTypeId"] = value;
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
                this.txtWeldTypeCode.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                Base_DetectionTypeService.InitDetectionTypeDropDownList(drpDetectionType, true, string.Empty);
                this.WeldTypeId = Request.Params["WeldTypeId"];
                if (!string.IsNullOrEmpty(this.WeldTypeId))
                {
                    Model.Base_WeldType WeldType = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(this.WeldTypeId);
                    if (WeldType != null)
                    {
                        this.txtWeldTypeCode.Text = WeldType.WeldTypeCode;
                        this.txtWeldTypeName.Text = WeldType.WeldTypeName;
                        this.txtRemark.Text = WeldType.Remark;
                        if (!string.IsNullOrEmpty(WeldType.DetectionType))
                        {
                            string[] dtype = WeldType.DetectionType.Split('|');
                            drpDetectionType.SelectedValueArray = dtype;
                        }
                        this.txtThickness.Text =Convert.ToString(WeldType.Thickness);
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
            var q = new Model.SGGLDB(Funs.ConnString).Base_WeldType.FirstOrDefault(x => x.WeldTypeCode == this.txtWeldTypeCode.Text.Trim() && (x.WeldTypeId != this.WeldTypeId || (this.WeldTypeId == null && x.WeldTypeId != null)));
            if (q != null)
            {
                Alert.ShowInTop("此焊缝类型代号已经存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_WeldType.FirstOrDefault(x => x.WeldTypeName == this.txtWeldTypeName.Text.Trim() && (x.WeldTypeId != this.WeldTypeId || (this.WeldTypeId == null && x.WeldTypeId != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("此焊缝类型名称已经存在！", MessageBoxIcon.Warning);
                return;
            }
            string DetectionTypeId= String.Join("|", drpDetectionType.SelectedValueArray);
            if (this.drpDetectionType.SelectedValue != BLL.Const._Null)
            {
                DetectionTypeId = String.Join("|", drpDetectionType.SelectedValueArray);
            }
            Model.Base_WeldType newWeldType = new Model.Base_WeldType
            {
                WeldTypeCode = this.txtWeldTypeCode.Text.Trim(),
                WeldTypeName = this.txtWeldTypeName.Text.Trim(),
                DetectionType = DetectionTypeId,
                Thickness =Convert.ToDecimal(this.txtThickness.Text.Trim()),
                Remark = this.txtRemark.Text.Trim()
            };

            if (!string.IsNullOrEmpty(this.WeldTypeId))
            {
                newWeldType.WeldTypeId = this.WeldTypeId;
                BLL.Base_WeldTypeService.UpdateWeldType(newWeldType);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldTypeMenuId, Const.BtnModify, newWeldType.WeldTypeId);
            }
            else
            {
                this.WeldTypeId = SQLHelper.GetNewID(typeof(Model.Base_WeldType));
                newWeldType.WeldTypeId = this.WeldTypeId;
                BLL.Base_WeldTypeService.AddWeldType(newWeldType);
                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldTypeMenuId, Const.BtnAdd, newWeldType.WeldTypeId);
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}