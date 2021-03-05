using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.common.ProjectSet
{
    public partial class ProjectSysSet : PageBase
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetButtonPower();
                Show(this.CurrUser.LoginProjectId);
            }
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

        #region   提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string projectId = this.CurrUser.LoginProjectId;
            if (string.IsNullOrEmpty(projectId))
            {
                ShowNotify("请选择项目！", MessageBoxIcon.Warning);
                return;
            }
            #region 焊接
            // 焊接
            Model.Project_Sys_Set dayReport = BLL.Project_SysSetService.GetSysSetBySetId("1", projectId);
            Model.Project_Sys_Set point = BLL.Project_SysSetService.GetSysSetBySetId("2", projectId);
            Model.Project_Sys_Set trust = BLL.Project_SysSetService.GetSysSetBySetId("3", projectId);
            Model.Project_Sys_Set pdms = BLL.Project_SysSetService.GetSysSetBySetId("4", projectId);
            Model.Project_Sys_Set batch = BLL.Project_SysSetService.GetSysSetBySetId("5", projectId);
            Model.Project_Sys_Set jointB = BLL.Project_SysSetService.GetSysSetBySetId("6", projectId);
            if (jointB != null)
            {
                if (this.ckbJointB.Checked)
                {
                    jointB.IsAuto = true;
                }
                else
                {
                    jointB.IsAuto = false;
                }
                BLL.Project_SysSetService.UpdateSet(jointB);
            }
            else
            {
                Model.Project_Sys_Set newJointB = new Model.Project_Sys_Set();
                newJointB.SetId = "6";
                newJointB.ProjectId = projectId;
                if (this.ckbJointB.Checked)
                {
                    newJointB.IsAuto = true;
                }
                else
                {
                    newJointB.IsAuto = false;
                }
                BLL.Project_SysSetService.AddSet(newJointB);
            }
            if (this.ckbPdms.Checked)
            {
                pdms.IsAuto = true;
            }
            else
            {
                pdms.IsAuto = false;
            }

            if (ckbDayReport.Checked)
            {
                dayReport.IsAuto = true;
            }
            else
            {
                dayReport.IsAuto = false;
            }

            if (ckbPoint.Checked)
            {
                point.IsAuto = true;
            }
            else
            {
                point.IsAuto = false;
            }

            if (robStandard.SelectedValue == "1")
            {
                trust.IsAuto = true;
                trust.SetValue = null;
            }
            else if (robStandard.SelectedValue == "2")
            {
                trust.IsAuto = false;
                trust.SetValue = null;
            }
            else
            {
                trust.IsAuto = null;
                trust.SetValue = robStandard.SelectedValue;
            }
            string lists = string.Empty;
            if (cb1.Checked)
            {
                lists += "1|";
            }
            if (cb2.Checked)
            {
                lists += "2|";
            }
            if (cb3.Checked)
            {
                lists += "3|";
            }
            if (cb4.Checked)
            {
                lists += "4|";
            }
            if (cb5.Checked)
            {
                lists += "5|";
            }
            if (cb6.Checked)
            {
                lists += "6|";
            }
            if (cb7.Checked)
            {
                lists += "7|";
            }
            if (!string.IsNullOrEmpty(lists))
            {
                lists = lists.Substring(0, lists.LastIndexOf('|'));
                batch.IsAuto = true;
                batch.SetValue = lists;
            }

            BLL.Project_SysSetService.UpdateSet(dayReport);
            BLL.Project_SysSetService.UpdateSet(point);
            BLL.Project_SysSetService.UpdateSet(pdms);
            BLL.Project_SysSetService.UpdateSet(trust);
            BLL.Project_SysSetService.UpdateSet(batch);
            this.Show(projectId);
            #endregion

            #region 质量

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

            #endregion

            //BLL.Sys_LogService.AddLog(BLL.Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, "提交项目环境设置");
            Alert.ShowInTop("提交成功！", MessageBoxIcon.Success);
        }
        #endregion

        #region 页面呈现
        /// <summary>
        /// 
        /// </summary>
        private void Show(string projectId)
        {
            var q = from x in Funs.DB.Project_Sys_Set where x.ProjectId == projectId select x;
            if (q.Count() > 0)
            {
                foreach (var s in q)
                {
                    if (s.SetId == "1")
                    {
                        if (s.IsAuto == true)
                        {
                            this.ckbDayReport.Checked = true;
                        }
                        else
                        {
                            this.ckbDayReport.Checked = false;
                        }
                    }
                    else if (s.SetId == "2")
                    {
                        if (s.IsAuto == true)
                        {
                            this.ckbPoint.Checked = true;
                        }
                        else
                        {
                            this.ckbPoint.Checked = false;
                        }
                    }
                    else if (s.SetId == "3")
                    {
                        if (s.IsAuto == true)
                        {
                            this.robStandard.SelectedValue = "1";
                        }
                        if (s.IsAuto == false)
                        {
                            this.robStandard.SelectedValue = "2";
                        }
                        if (s.SetValue == "3")
                        {
                            this.robStandard.SelectedValue = "3";
                        }
                        if (s.SetValue == "4")
                        {
                            this.robStandard.SelectedValue = "4";
                        }
                    }
                    else if (s.SetId == "4")
                    {
                        if (s.IsAuto == true)
                        {
                            this.ckbPdms.Checked = true;
                        }
                        else
                        {
                            this.ckbPdms.Checked = false;
                        }
                    }
                    else if (s.SetId == "5")
                    {
                        cb4.Checked = false;
                        cb5.Checked = false;
                        cb6.Checked = false;
                        cb7.Checked = false;
                        var lists = s.SetValue.Split('|');
                        foreach (var item in lists)
                        {
                            if (item == "1")
                            {
                                cb1.Checked = true;
                            }
                            else if (item == "2")
                            {
                                cb2.Checked = true;
                            }
                            else if (item == "3")
                            {
                                cb3.Checked = true;
                            }
                            else if (item == "4")
                            {
                                cb4.Checked = true;
                            }
                            else if (item == "5")
                            {
                                cb5.Checked = true;
                            }
                            else if (item == "6")
                            {
                                cb6.Checked = true;
                            }
                            else if (item == "7")
                            {
                                cb7.Checked = true;
                            }
                        }
                    }
                    else if (s.SetId == "6")
                    {
                        if (s.IsAuto == true)
                        {
                            this.ckbJointB.Checked = true;
                        }
                        else
                        {
                            this.ckbJointB.Checked = false;
                        }
                    }
                }
            }

            ///质量页面呈现
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
        #endregion
    }
}