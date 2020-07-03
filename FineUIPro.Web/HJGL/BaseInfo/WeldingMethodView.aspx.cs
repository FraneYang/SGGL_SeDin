using System;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class WeldingMethodView : PageBase
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string WeldingMethodId = Request.Params["WeldingMethodId"];
                if (!string.IsNullOrEmpty(WeldingMethodId))
                {
                    Model.Base_WeldingMethod getWeldingMethod = BLL.Base_WeldingMethodService.GetWeldingMethodByWeldingMethodId(WeldingMethodId);
                    if (getWeldingMethod != null)
                    {
                        this.txtWeldingMethodCode.Text = getWeldingMethod.WeldingMethodCode;
                        this.txtWeldingMethodName.Text = getWeldingMethod.WeldingMethodName;
                        this.txtRemark.Text = getWeldingMethod.Remark;
                        if (!string.IsNullOrEmpty(getWeldingMethod.ConsumablesType))
                        {
                            string name = string.Empty;
                            var lists = getWeldingMethod.ConsumablesType.Split(',');
                            foreach (var item in lists)
                            {
                                if (item == "1")
                                {
                                    name += "焊丝" + ",";
                                }
                                if (item == "2")
                                {
                                    name += "焊条" + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(name))
                            {
                                name = name.Substring(0, name.LastIndexOf(','));
                            }
                            this.txtConsumablesType.Text = name;

                        }
                    }
                }
            }
        }
        #endregion
    }
}