using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class CheckerItemEdit : System.Web.UI.Page
    {
        #region 加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.hdWelderId.Text = Request.Params["PersonId"];
                string welderQualifyId = Request.Params["WelderQualifyId"];
                if (!string.IsNullOrEmpty(welderQualifyId))
                {
                    Model.Welder_WelderQualify welderQualify = BLL.WelderQualifyService.GetWelderQualifyById(welderQualifyId);
                    if (welderQualify != null)
                    {
                        this.hdWelderId.Text = welderQualify.WelderId;
                        this.txtQualificationItem.Text = welderQualify.QualificationItem;
                        if (welderQualify.CheckDate.HasValue)
                        {
                            this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", welderQualify.CheckDate);
                        }
                        if (welderQualify.LimitDate.HasValue)
                        {
                            this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", welderQualify.LimitDate);
                        }

                    }
                }
                var w = BLL.WelderService.GetWelderById(this.hdWelderId.Text);
                if (w != null)
                {
                    this.txtWelderCode.Text = w.WelderCode;
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
            string welderQualifyId = Request.Params["WelderQualifyId"];
            Model.Welder_WelderQualify welderQualify = new Model.Welder_WelderQualify();
            welderQualify.WelderId = this.hdWelderId.Text;
            welderQualify.QualificationItem = txtQualificationItem.Text;
            welderQualify.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            welderQualify.LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text.Trim());
            
            if (!string.IsNullOrEmpty(welderQualifyId))
            {
                welderQualify.WelderQualifyId = welderQualifyId;
                BLL.WelderQualifyService.UpdateWelderQualify(welderQualify);
            }
            else
            {
                welderQualify.WelderQualifyId = SQLHelper.GetNewID(typeof(Model.Welder_WelderQualify));
                BLL.WelderQualifyService.AddWelderQualify(welderQualify);
            }
            Alert.ShowInTop("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        
        #endregion
    }
}