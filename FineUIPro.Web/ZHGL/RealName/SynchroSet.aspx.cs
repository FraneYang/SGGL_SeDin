using BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace FineUIPro.Web.ZHGL.RealName
{
    public partial class SynchroSet : PageBase
    {
        /// <summary>
        /// 用户编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProjectService.InitAllProjectShortNameDropDownList(this.drpProject, this.CurrUser.UserId, true);
                ///权限
                this.GetButtonPower();
                var getSynchroSet = SynchroSetService.GetSynchroSetByUnitId(Const.UnitId_SEDIN);
                if (getSynchroSet != null)
                {
                    this.txtapiUrl.Text = getSynchroSet.ApiUrl;
                    this.txtclientId.Text = getSynchroSet.ClientId;
                    this.txt1.Text = getSynchroSet.UserName;
                    this.txtword.Text = getSynchroSet.Password;
                    this.txtintervaltime.Text = getSynchroSet.Intervaltime.ToString();
                }
                else
                {
                    this.txtintervaltime.Text = "120";
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.RealName_SynchroSet newSynchroSet = new Model.RealName_SynchroSet
            {
                UnitId = Const.UnitId_SEDIN,
                ApiUrl = this.txtapiUrl.Text.Trim(),
                ClientId = this.txtclientId.Text.Trim(),
                UserName = this.txt1.Text.Trim(),
                Password = this.txtword.Text.Trim(),
                Intervaltime = Funs.GetNewInt(this.txtintervaltime.Text.Trim()),
            };

            BLL.SynchroSetService.SaveSynchroSet(newSynchroSet);
            ShowNotify("保存成功!", MessageBoxIcon.Success);
        }

        /// <summary>
        /// 连接测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConnect_Click(object sender, EventArgs e)
        {
            Model.RealName_SynchroSet newSynchroSet = new Model.RealName_SynchroSet
            {
                UnitId = Const.UnitId_SEDIN,
                ApiUrl = this.txtapiUrl.Text.Trim(),
                ClientId = this.txtclientId.Text.Trim(),
                UserName = this.txt1.Text.Trim(),
                Password = this.txtword.Text.Trim(),
                Intervaltime = Funs.GetNewInt(this.txtintervaltime.Text.Trim()),
            };
            if (!string.IsNullOrEmpty(SynchroSetService.SaveToken(newSynchroSet)))
            {
                ShowNotify("连接成功！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInParent("连接失败！", MessageBoxIcon.Warning);
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RealNameSynchroSetMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnConnect.Hidden = false;
                    this.btnCompany.Hidden = false;

                    this.btnCompany.Hidden = false;
                    this.btnProCollCompany.Hidden = false;
                    this.btnCollTeam.Hidden = false;
                    this.btnPersons.Hidden = false;
                    this.btnAttendance.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 推送参建企业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompany_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.PushCollCompany(), MessageBoxIcon.Information);
        }

        /// <summary>
        /// 推送项目参建单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnProCollCompany_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.PushProCollCompany(null), MessageBoxIcon.Information);
        }

        /// <summary>
        /// 推送施工队
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCollTeam_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.PushCollTeam(null), MessageBoxIcon.Information);
        }

        /// <summary>
        /// 推送人员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPersons_Click(object sender, EventArgs e)
        {
            string add = "新增" + BLL.SynchroSetService.PushPersons(Const.BtnAdd, this.drpProject.SelectedValue == BLL.Const._Null ? null : BLL.ProjectService.GetProjectCodeByProjectId(this.drpProject.SelectedValue)) ?? "";
            string update = "更新" + BLL.SynchroSetService.PushPersons(Const.BtnModify, this.drpProject.SelectedValue == BLL.Const._Null ? null : BLL.ProjectService.GetProjectCodeByProjectId(this.drpProject.SelectedValue)) ?? "";
            ShowNotify(add + "|" + update, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 推送考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttendance_Click(object sender, EventArgs e)
        {
            ShowNotify(BLL.SynchroSetService.PushAttendance(null), MessageBoxIcon.Information);
        }
    }
}