using BLL;
using System;
using System.Collections.Generic;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class EquipmentOutEdit : PageBase
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
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.EquipmentOutId = Request.Params["EquipmentOutId"];
                if (!string.IsNullOrEmpty(this.EquipmentOutId))
                {
                    Model.InApproveManager_EquipmentOut equipmentOut = BLL.EquipmentOutService.GetEquipmentOutById(this.EquipmentOutId);
                    if (equipmentOut!=null)
                    {
                        this.ProjectId = equipmentOut.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtEquipmentOutCode.Text = CodeRecordsService.ReturnCodeByDataId(this.EquipmentOutId);
                        if (this.drpUnitId.SelectedValue != Const._Null)
                        {
                            this.drpUnitId.SelectedValue = equipmentOut.UnitId;
                        }
                        this.txtApplicationDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentOut.ApplicationDate);
                        this.txtCarNum.Text = equipmentOut.CarNum;
                        this.txtCarModel.Text = equipmentOut.CarModel;
                        this.txtDriverName.Text = equipmentOut.DriverName;
                        this.txtDriverNum.Text = equipmentOut.DriverNum;
                        this.txtTransPortStart.Text = equipmentOut.TransPortStart;
                        this.txtTransPortEnd.Text = equipmentOut.TransPortEnd;
                    }
                    BindGrid();
                }
                else
                {
                    this.txtApplicationDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtEquipmentOutCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.EquipmentOutMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.EquipmentOutMenuId;
                this.ctlAuditFlow.DataId = this.EquipmentOutId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            UnitService.InitUnitNameByProjectIdUnitTypeDropDownList(this.drpUnitId, this.ProjectId, Const.ProjectUnitType_2, true);
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

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentOutItemEdit.aspx?EquipmentOutItemId={0}", id, "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        BLL.EquipmentOutItemService.DeleteEquipmentOutItemById(rowID);
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
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
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择申请单位！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择申请单位！", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InApproveManager_EquipmentOut equipmentOut = new Model.InApproveManager_EquipmentOut
            {
                ProjectId = this.ProjectId,
                EquipmentOutCode = this.txtEquipmentOutCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                equipmentOut.UnitId = this.drpUnitId.SelectedValue;
            }
            equipmentOut.ApplicationDate = Funs.GetNewDateTime(this.txtApplicationDate.Text.Trim());
            equipmentOut.CarNum = this.txtCarNum.Text.Trim();
            equipmentOut.CarModel = this.txtCarModel.Text.Trim();
            equipmentOut.DriverName = this.txtDriverName.Text.Trim();
            equipmentOut.DriverNum = this.txtDriverNum.Text.Trim();
            equipmentOut.TransPortStart = this.txtTransPortStart.Text.Trim();
            equipmentOut.TransPortEnd = this.txtTransPortEnd.Text.Trim();
            equipmentOut.State = BLL.Const.State_0; 
            if (type == BLL.Const.BtnSubmit)
            {
                equipmentOut.State = this.ctlAuditFlow.NextStep;
            }
            equipmentOut.CompileMan = this.CurrUser.UserId;
            equipmentOut.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.EquipmentOutId))
            {
                equipmentOut.EquipmentOutId = this.EquipmentOutId;
                BLL.EquipmentOutService.UpdateEquipmentOut(equipmentOut);
                BLL.LogService.AddSys_Log(this.CurrUser, equipmentOut.EquipmentOutCode, equipmentOut.EquipmentOutId,BLL.Const.EquipmentOutMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.EquipmentOutId = SQLHelper.GetNewID(typeof(Model.InApproveManager_EquipmentOut));
                equipmentOut.EquipmentOutId = this.EquipmentOutId;
                BLL.EquipmentOutService.AddEquipmentOut(equipmentOut);
                BLL.LogService.AddSys_Log(this.CurrUser, equipmentOut.EquipmentOutCode, equipmentOut.EquipmentOutId, BLL.Const.EquipmentOutMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.EquipmentOutMenuId, this.EquipmentOutId, (type == BLL.Const.BtnSubmit ? true : false), (equipmentOut.DriverName + equipmentOut.CarNum), "../InApproveManager/EquipmentOutView.aspx?EquipmentOutId={0}");
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
            if (string.IsNullOrEmpty(this.EquipmentOutId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentOutAttachUrl&menuId={1}", this.EquipmentOutId, BLL.Const.EquipmentOutMenuId)));
        }
        #endregion

        #region 新增出场机具设备清单
        /// <summary>
        /// 添加出场机具设备清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择申请单位！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.EquipmentOutId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentOutItemEdit.aspx?EquipmentOutId={0}", this.EquipmentOutId, "编辑 - ")));
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