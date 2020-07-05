﻿using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class SpecialSchemeSelectCloumn : PageBase
    {
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
                ////权限按钮方法
                this.GetButtonPower();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
            }
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑                        
            // 2. 关闭本窗体，然后回发父窗体           
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference(String.Join("#", cblColumns.SelectedValueArray)));
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpecialSchemeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnOut))
                {
                    this.btnImport.Hidden = false;
                }
            }
        }
        #endregion
    }
}