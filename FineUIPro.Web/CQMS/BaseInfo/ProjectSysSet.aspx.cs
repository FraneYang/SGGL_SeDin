using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CQMS.BaseInfo
{
    public partial class ProjectSysSet : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetButtonPower();
                Model.Project_Sys_Set CheckEquipmentDay = BLL.Project_SysSetService.GetSysSetBySetName("检试验设备到期提醒天数", this.CurrUser.LoginProjectId);
                if (CheckEquipmentDay != null)
                {
                    this.txtRemindDay.Text = CheckEquipmentDay.SetValue;
                }
                Model.Project_Sys_Set CheckMonthStartDay = BLL.Project_SysSetService.GetSysSetBySetName("月报开始日期", this.CurrUser.LoginProjectId);
                if (CheckMonthStartDay != null)
                {
                    this.txtStarTime.Text = CheckMonthStartDay.SetValue;
                }
                else
                {
                    this.txtStarTime.Text = "25";
                }
                Model.Project_Sys_Set CheckMonthEndDay = BLL.Project_SysSetService.GetSysSetBySetName("月报结束日期", this.CurrUser.LoginProjectId);
                if (CheckMonthEndDay != null)
                {
                    this.txtEndTime.Text = CheckMonthEndDay.SetValue;
                }
                else
                {
                    this.txtEndTime.Text = "24";
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Project_Sys_Set CheckEquipmentDay = BLL.Project_SysSetService.GetSysSetBySetName("检试验设备到期提醒天数", this.CurrUser.LoginProjectId);
            if (CheckEquipmentDay != null)
            {

                if (!string.IsNullOrEmpty(this.txtRemindDay.Text.Trim()))
                {
                    CheckEquipmentDay.SetValue = this.txtRemindDay.Text.Trim();
                    BLL.Project_SysSetService.UpdateSet(CheckEquipmentDay);
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(this.txtRemindDay.Text.Trim()))
                {
                    Model.Project_Sys_Set newCheckEquipmentDay = new Model.Project_Sys_Set();
                    newCheckEquipmentDay.SetId = SQLHelper.GetNewID(typeof(Model.Project_Sys_Set));

                    newCheckEquipmentDay.ProjectId = this.CurrUser.LoginProjectId;
                    newCheckEquipmentDay.SetName = "检试验设备到期提醒天数";
                    newCheckEquipmentDay.SetValue = this.txtRemindDay.Text.Trim();
                    BLL.Project_SysSetService.AddSet(newCheckEquipmentDay);
                }
            }
            Model.Project_Sys_Set CheckMonthStartDay = BLL.Project_SysSetService.GetSysSetBySetName("月报开始日期", this.CurrUser.LoginProjectId);
            if (CheckMonthStartDay != null)
            {
                if (!string.IsNullOrEmpty(this.txtStarTime.Text.Trim()))
                {
                    CheckMonthStartDay.SetValue = this.txtStarTime.Text.Trim();
                    BLL.Project_SysSetService.UpdateSet(CheckMonthStartDay);
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(this.txtStarTime.Text.Trim()))
                {
                    Model.Project_Sys_Set newCheckEquipmentDay = new Model.Project_Sys_Set();
                    newCheckEquipmentDay.SetId = SQLHelper.GetNewID(typeof(Model.Project_Sys_Set));
                    newCheckEquipmentDay.ProjectId = this.CurrUser.LoginProjectId;
                    newCheckEquipmentDay.SetName = "月报开始日期";
                    newCheckEquipmentDay.SetValue = this.txtStarTime.Text.Trim();
                    BLL.Project_SysSetService.AddSet(newCheckEquipmentDay);
                }
            }
            Model.Project_Sys_Set CheckMonthEndDay = BLL.Project_SysSetService.GetSysSetBySetName("月报结束日期", this.CurrUser.LoginProjectId);
            if (CheckMonthEndDay != null)
            {
                if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
                {
                    CheckMonthStartDay.SetValue = this.txtEndTime.Text.Trim();
                    BLL.Project_SysSetService.UpdateSet(CheckMonthStartDay);
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
                {
                    Model.Project_Sys_Set newCheckEquipmentDay = new Model.Project_Sys_Set();
                    newCheckEquipmentDay.SetId = SQLHelper.GetNewID(typeof(Model.Project_Sys_Set));
                    newCheckEquipmentDay.ProjectId = this.CurrUser.LoginProjectId;
                    newCheckEquipmentDay.SetName = "月报结束日期";
                    newCheckEquipmentDay.SetValue = this.txtEndTime.Text.Trim();
                    BLL.Project_SysSetService.AddSet(newCheckEquipmentDay);
                }
            }

            Alert.ShowInTop("保存成功！", MessageBoxIcon.Success);
        }

        #region 判断按钮权限
        /// <summary>
        /// 判断按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.CQMSSysSetMenuId, Const.BtnSave))
            {
                this.btnSave.Hidden = false;
            }
        }
        #endregion
    }
}