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
                Password =  this.txtword.Text.Trim(),
                Intervaltime=Funs.GetNewInt(this.txtintervaltime.Text.Trim()),
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
                ShowNotify("连接失败！", MessageBoxIcon.Warning);
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
                    //this.btnSave.Hidden = false;
                    this.btnConnect.Hidden = false;
                }
            }
        }
        #endregion       
     
    }
}