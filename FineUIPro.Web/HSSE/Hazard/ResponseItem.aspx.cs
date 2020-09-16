using System;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class ResponseItem : PageBase
    {
        #region 定义项
        /// <summary>
        /// 危险源辨识与评价清单编号
        /// </summary>
        public string HazardId
        {
            get
            {
                return (string)ViewState["HazardId"];
            }
            set
            {
                ViewState["HazardId"] = value;
            }
        }

        /// <summary>
        /// 危险源辨识与评价清单编号
        /// </summary>
        public string HazardListId
        {
            get
            {
                return (string)ViewState["HazardListId"];
            }
            set
            {
                ViewState["HazardListId"] = value;
            }
        }

        public string WorkStage
        {
            get
            {
                return (string)ViewState["WorkStage"];
            }
            set
            {
                ViewState["WorkStage"] = value;
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
                this.HazardId = Request.Params["HazardId"];
                this.HazardListId = Request.Params["HazardListId"];
                this.WorkStage = Request.Params["workStage"];
                var hazardSelectedItem = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemByHazardId(HazardId, HazardListId, this.WorkStage);
                if (hazardSelectedItem != null)
                {
                    if (Convert.ToBoolean(hazardSelectedItem.IsResponse))
                    {
                        this.rbtnIsResponse.SelectedValue = "True";
                        this.txtResponseRecode.Enabled = true;
                        this.btnSave.Enabled = true;
                        this.txtResponseRecode.Text = hazardSelectedItem.ResponseRecode;
                    }
                    else
                    {
                        this.rbtnIsResponse.SelectedValue = "False";
                        this.txtResponseRecode.Enabled = false;
                        this.btnSave.Enabled = false;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Hazard_HazardSelectedItem hazardSelectedItem = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemByHazardId(HazardId, HazardListId, this.WorkStage);
            if (hazardSelectedItem != null)
            {
                hazardSelectedItem.IsResponse = Convert.ToBoolean(this.rbtnIsResponse.SelectedValue);
                hazardSelectedItem.ResponseRecode = this.txtResponseRecode.Text.Trim();
                BLL.Hazard_HazardSelectedItemService.UpdateHazardSelectedItem(hazardSelectedItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 是否响应选择事件
        /// <summary>
        /// 是否响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbtnIsResponse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rbtnIsResponse.SelectedValue == "True")
            {
                this.txtResponseRecode.Enabled = true;
                this.btnSave.Enabled = true;
            }
            else
            {
                this.txtResponseRecode.Enabled = false;
                this.txtResponseRecode.Text = string.Empty;
                this.btnSave.Enabled = false;
            }
        }
        #endregion
    }
}