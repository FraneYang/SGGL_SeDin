using BLL;
using System;
using System.Collections.Generic;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class EquipmentOutView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EquipmentOutId
        {
            get
            {
                return (string)ViewState["EquipmentOutId"];
            }
            set
            {
                ViewState["EquipmentOutId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        public static List<Model.InApproveManager_EquipmentOutItem> equipmentOutItems = new List<Model.InApproveManager_EquipmentOutItem>();
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
                Funs.DropDownPageSize(this.ddlPageSize);
                btnClose.OnClientClick = ActiveWindow.GetHideReference();              
                this.EquipmentOutId = Request.Params["EquipmentOutId"];
                if (!string.IsNullOrEmpty(this.EquipmentOutId))
                {
                    Model.InApproveManager_EquipmentOut equipmentOut = BLL.EquipmentOutService.GetEquipmentOutById(this.EquipmentOutId);
                    if (equipmentOut != null)
                    {
                        this.txtEquipmentOutCode.Text = CodeRecordsService.ReturnCodeByDataId(this.EquipmentOutId);
                        if (!string.IsNullOrEmpty(equipmentOut.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(equipmentOut.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (equipmentOut.ApplicationDate != null)
                        {
                            this.txtApplicationDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentOut.ApplicationDate);
                        }
                        this.txtCarNum.Text = equipmentOut.CarNum;
                        this.txtCarModel.Text = equipmentOut.CarModel;
                        this.txtDriverName.Text = equipmentOut.DriverName;
                        this.txtDriverNum.Text = equipmentOut.DriverNum;
                        this.txtTransPortStart.Text = equipmentOut.TransPortStart;
                        this.txtTransPortEnd.Text = equipmentOut.TransPortEnd;
                    }
                    BindGrid();
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.EquipmentOutMenuId;
                this.ctlAuditFlow.DataId = this.EquipmentOutId;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            equipmentOutItems = BLL.EquipmentOutItemService.GetEquipmentOutItemByEquipmentOutId(this.EquipmentOutId);
            this.Grid1.DataSource = equipmentOutItems;
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
            if (!string.IsNullOrEmpty(this.EquipmentOutId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentOutAttachUrl&menuId={1}&type=-1", this.EquipmentOutId, BLL.Const.EquipmentOutMenuId)));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("特种设备机具出场明细报批" + filename, System.Text.Encoding.UTF8) + ".xls");
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