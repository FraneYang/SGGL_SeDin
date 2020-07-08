using BLL;
using System;
using System.Collections.Generic;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InApproveManager
{
    public partial class GeneralEquipmentOutEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GeneralEquipmentOutId
        {
            get
            {
                return (string)ViewState["GeneralEquipmentOutId"];
            }
            set
            {
                ViewState["GeneralEquipmentOutId"] = value;
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
        public static List<Model.InApproveManager_GeneralEquipmentOutItem> generalEquipmentOutItems = new List<Model.InApproveManager_GeneralEquipmentOutItem>();
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
                this.GeneralEquipmentOutId = Request.Params["GeneralEquipmentOutId"];
                if (!string.IsNullOrEmpty(this.GeneralEquipmentOutId))
                {
                    Model.InApproveManager_GeneralEquipmentOut generalEquipmentOut = BLL.GeneralEquipmentOutService.GetGeneralEquipmentOutById(this.GeneralEquipmentOutId);
                    if (generalEquipmentOut!=null)
                    {
                        this.ProjectId = generalEquipmentOut.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtGeneralEquipmentOutCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GeneralEquipmentOutId);
                        if (!string.IsNullOrEmpty(generalEquipmentOut.UnitId))
                        {
                            this.drpUnitId.SelectedValue = generalEquipmentOut.UnitId;
                        }
                        this.txtApplicationDate.Text = string.Format("{0:yyyy-MM-dd}", generalEquipmentOut.ApplicationDate);
                        this.txtCarNum.Text = generalEquipmentOut.CarNum;
                        this.txtCarModel.Text = generalEquipmentOut.CarModel;
                        this.txtDriverName.Text = generalEquipmentOut.DriverName;
                        this.txtDriverNum.Text = generalEquipmentOut.DriverNum;
                        this.txtTransPortStart.Text = generalEquipmentOut.TransPortStart;
                        this.txtTransPortEnd.Text = generalEquipmentOut.TransPortEnd;
                    }
                    BindGrid();
                }
                else
                {
                    this.txtApplicationDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtGeneralEquipmentOutCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GeneralEquipmentOutMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GeneralEquipmentOutMenuId;
                this.ctlAuditFlow.DataId = this.GeneralEquipmentOutId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            this.drpUnitId.DataValueField = "UnitId";
            this.drpUnitId.DataTextField = "UnitName";
            this.drpUnitId.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnitId.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnitId);
        }

       /// <summary>
       /// 绑定数据
       /// </summary>
        private void BindGrid()
        {
            generalEquipmentOutItems = BLL.GeneralEquipmentOutItemService.GetGeneralEquipmentOutItemByGeneralEquipmentOutId(this.GeneralEquipmentOutId);
            this.Grid1.DataSource = generalEquipmentOutItems;
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GeneralEquipmentOutItemEdit.aspx?GeneralEquipmentOutItemId={0}", id, "编辑 - ")));
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
                        BLL.GeneralEquipmentOutItemService.DeleteGeneralEquipmentOutItemById(rowID);
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
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择申请单位！", MessageBoxIcon.Warning);
                return;
            }

            Model.InApproveManager_GeneralEquipmentOut generalEquipmentOut = new Model.InApproveManager_GeneralEquipmentOut
            {
                ProjectId = this.ProjectId,
                GeneralEquipmentOutCode = this.txtGeneralEquipmentOutCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                generalEquipmentOut.UnitId = this.drpUnitId.SelectedValue;
            }
            generalEquipmentOut.ApplicationDate = Funs.GetNewDateTime(this.txtApplicationDate.Text.Trim());
            generalEquipmentOut.CarNum = this.txtCarNum.Text.Trim();
            generalEquipmentOut.CarModel = this.txtCarModel.Text.Trim();
            generalEquipmentOut.DriverName = this.txtDriverName.Text.Trim();
            generalEquipmentOut.DriverNum = this.txtDriverNum.Text.Trim();
            generalEquipmentOut.TransPortStart = this.txtTransPortStart.Text.Trim();
            generalEquipmentOut.TransPortEnd = this.txtTransPortEnd.Text.Trim();
            generalEquipmentOut.State = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                generalEquipmentOut.State = this.ctlAuditFlow.NextStep;
            }
            generalEquipmentOut.CompileMan = this.CurrUser.UserId;
            generalEquipmentOut.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GeneralEquipmentOutId))
            {
                generalEquipmentOut.GeneralEquipmentOutId = this.GeneralEquipmentOutId;
                BLL.GeneralEquipmentOutService.UpdateGeneralEquipmentOut(generalEquipmentOut);
                BLL.LogService.AddSys_Log(this.CurrUser, generalEquipmentOut.GeneralEquipmentOutCode, generalEquipmentOut.GeneralEquipmentOutId, BLL.Const.GeneralEquipmentOutMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.GeneralEquipmentOutId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GeneralEquipmentOut));
                generalEquipmentOut.GeneralEquipmentOutId = this.GeneralEquipmentOutId;
                BLL.GeneralEquipmentOutService.AddGeneralEquipmentOut(generalEquipmentOut);
                BLL.LogService.AddSys_Log(this.CurrUser, generalEquipmentOut.GeneralEquipmentOutCode, generalEquipmentOut.GeneralEquipmentOutId,BLL.Const.GeneralEquipmentOutMenuId,BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GeneralEquipmentOutMenuId, this.GeneralEquipmentOutId, (type == BLL.Const.BtnSubmit ? true : false), (generalEquipmentOut.DriverName + generalEquipmentOut.CarNum), "../InApproveManager/GeneralEquipmentOutView.aspx?GeneralEquipmentOutId={0}");
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
            if (string.IsNullOrEmpty(this.GeneralEquipmentOutId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GeneralEquipmentOutAttachUrl&menuId={1}", this.GeneralEquipmentOutId, BLL.Const.GeneralEquipmentOutMenuId)));
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
            if (string.IsNullOrEmpty(this.GeneralEquipmentOutId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GeneralEquipmentOutItemEdit.aspx?GeneralEquipmentOutId={0}", this.GeneralEquipmentOutId, "编辑 - ")));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("一般设备机具出场报批明细" + filename, System.Text.Encoding.UTF8) + ".xls");
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