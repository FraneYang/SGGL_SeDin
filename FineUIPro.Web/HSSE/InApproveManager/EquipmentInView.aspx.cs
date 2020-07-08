using BLL;
using System;
using System.Collections.Generic;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class EquipmentInView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EquipmentInId
        {
            get
            {
                return (string)ViewState["EquipmentInId"];
            }
            set
            {
                ViewState["EquipmentInId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        public static List<Model.InApproveManager_EquipmentInItem> equipmentInItems = new List<Model.InApproveManager_EquipmentInItem>();
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference(); 
                this.EquipmentInId = Request.Params["EquipmentInId"];
                if (!string.IsNullOrEmpty(this.EquipmentInId))
                {
                    Model.InApproveManager_EquipmentIn equipmentIn = BLL.EquipmentInService.GetEquipmentInById(this.EquipmentInId);
                    if (equipmentIn != null)
                    {
                        this.txtEquipmentInCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.EquipmentInId);
                        if (!string.IsNullOrEmpty(equipmentIn.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(equipmentIn.UnitId);
                            if (unit !=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        this.txtCarNumber.Text = equipmentIn.CarNumber;
                        this.txtSubProjectName.Text = equipmentIn.SubProjectName;
                        this.txtContentDef.Text = equipmentIn.ContentDef;
                        this.txtOtherDef.Text = equipmentIn.OtherDef;
                    }
                    BindGrid();
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.EquipmentInMenuId;
                this.ctlAuditFlow.DataId = this.EquipmentInId;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            equipmentInItems = BLL.EquipmentInItemService.GetEquipmentInItemByEquipmentInId(this.EquipmentInId);
            this.Grid1.DataSource = equipmentInItems;
            this.Grid1.PageIndex = 0;
            this.Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.EquipmentInId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentInAttachUrl&menuId={1}&type=-1", this.EquipmentInId, BLL.Const.EquipmentInMenuId)));
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        protected string ConvertEqiupment(object equipmentId)
        {
            string equipmentName = string.Empty;
            if (equipmentId != null)
            {
                var specialEquipment = BLL.SpecialEquipmentService.GetSpecialEquipmentById(equipmentId.ToString());
                if (specialEquipment != null)
                {
                    equipmentName = specialEquipment.SpecialEquipmentName;
                }
            }
            return equipmentName;
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("特种设备机具入场明细报批" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}