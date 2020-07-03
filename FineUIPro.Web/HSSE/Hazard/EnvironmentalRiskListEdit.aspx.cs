using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class EnvironmentalRiskListEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string EnvironmentalRiskListId
        {
            get
            {
                return (string)ViewState["EnvironmentalRiskListId"];
            }
            set
            {
                ViewState["EnvironmentalRiskListId"] = value;
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
        private static List<Model.View_Hazard_EnvironmentalRiskItem> environmentalRiskItems = new List<Model.View_Hazard_EnvironmentalRiskItem>();
        #endregion

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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                
                environmentalRiskItems.Clear();

                this.EnvironmentalRiskListId = Request.Params["EnvironmentalRiskListId"];
                var environmentalRiskList = BLL.Hazard_EnvironmentalRiskListService.GetEnvironmentalRiskList(this.EnvironmentalRiskListId);
                if (environmentalRiskList != null)
                {
                    this.ProjectId = environmentalRiskList.ProjectId;
                    this.InitDropDownList();

                    this.txtRiskCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.EnvironmentalRiskListId);
                    this.txtWorkArea.Text = environmentalRiskList.WorkAreaName;
                    if (!string.IsNullOrEmpty(environmentalRiskList.CompileMan))
                    {
                        this.drpCompileMan.SelectedValue = environmentalRiskList.CompileMan;
                    }
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", environmentalRiskList.CompileDate);
                    if (!string.IsNullOrEmpty(environmentalRiskList.ControllingPerson))
                    {
                        this.drpControllingPerson.SelectedValue = environmentalRiskList.ControllingPerson;
                    }
                    this.txtIdentificationDate.Text = string.Format("{0:yyyy-MM-dd}", environmentalRiskList.IdentificationDate);
                    this.txtContents.Text = HttpUtility.HtmlDecode(environmentalRiskList.Contents);
                    environmentalRiskItems = (from x in Funs.DB.View_Hazard_EnvironmentalRiskItem where x.EnvironmentalRiskListId == this.EnvironmentalRiskListId orderby x.EType, x.Code select x).ToList();
                }
                else
                {
                    ////自动生成编码
                    this.txtRiskCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectEnvironmentalRiskListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtIdentificationDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                Grid1.DataSource = environmentalRiskItems;
                Grid1.DataBind();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEnvironmentalRiskListMenuId;
                this.ctlAuditFlow.DataId = this.EnvironmentalRiskListId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            ///区域下拉框
            BLL.UnitWorkService.InitUnitWorkDownList(this.drpWorkArea, this.ProjectId, true);
            ///编制人
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
            ///控制责任人
            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpControllingPerson, this.ProjectId, string.Empty, true);
        }

        #region 选择按钮
        /// <summary>
        /// 选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.EnvironmentalRiskListId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ShowEnvironmentalItem.aspx?EnvironmentalRiskListId={0}", this.EnvironmentalRiskListId, "编辑 - ")));
        }
        #endregion
                  
        #region 提交按钮
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
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Hazard_EnvironmentalRiskList newEnvironmentalRiskList = new Model.Hazard_EnvironmentalRiskList
            {
                ProjectId = this.ProjectId,
                RiskCode = this.txtRiskCode.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newEnvironmentalRiskList.CompileMan = this.drpCompileMan.SelectedValue;
            }
            newEnvironmentalRiskList.WorkAreaName = this.txtWorkArea.Text.Trim();
            newEnvironmentalRiskList.IdentificationDate = Funs.GetNewDateTime(this.txtIdentificationDate.Text.Trim());
            if (this.drpControllingPerson.SelectedValue != BLL.Const._Null)
            {
                newEnvironmentalRiskList.ControllingPerson = this.drpControllingPerson.SelectedValue;
            }
            newEnvironmentalRiskList.Contents = HttpUtility.HtmlEncode(this.txtContents.Text);
            ////单据状态
            newEnvironmentalRiskList.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                newEnvironmentalRiskList.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.EnvironmentalRiskListId))
            {
                newEnvironmentalRiskList.EnvironmentalRiskListId = this.EnvironmentalRiskListId;
                BLL.Hazard_EnvironmentalRiskListService.UpdateEnvironmentalRiskList(newEnvironmentalRiskList);
                BLL.LogService.AddSys_Log(this.CurrUser, newEnvironmentalRiskList.RiskCode, newEnvironmentalRiskList.EnvironmentalRiskListId,BLL.Const.ProjectEnvironmentalRiskListMenuId,BLL.Const.BtnModify);
            }
            else
            {
                newEnvironmentalRiskList.EnvironmentalRiskListId = SQLHelper.GetNewID(typeof(Model.Hazard_EnvironmentalRiskList));
                this.EnvironmentalRiskListId = newEnvironmentalRiskList.EnvironmentalRiskListId;
                newEnvironmentalRiskList.CompileMan = this.CurrUser.UserId;
                BLL.Hazard_EnvironmentalRiskListService.AddEnvironmentalRiskList(newEnvironmentalRiskList);
                BLL.LogService.AddSys_Log(this.CurrUser, newEnvironmentalRiskList.RiskCode, newEnvironmentalRiskList.EnvironmentalRiskListId, BLL.Const.ProjectEnvironmentalRiskListMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectEnvironmentalRiskListMenuId, this.EnvironmentalRiskListId, (type == BLL.Const.BtnSubmit ? true : false), this.txtCompileDate.Text.Trim(), "../Hazard/EnvironmentalRiskListView.aspx?EnvironmentalRiskListId={0}");
        }

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            environmentalRiskItems = (from x in Funs.DB.View_Hazard_EnvironmentalRiskItem where x.EnvironmentalRiskListId == this.EnvironmentalRiskListId orderby x.SmallType, x.Code select x).ToList();
            Grid1.DataSource = environmentalRiskItems;
            Grid1.DataBind();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuEdit_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string checkDayDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EnvironmentalRiskItemEdit.aspx?EnvironmentalRiskItemId={0}", checkDayDetailId, "编辑 - ")));

        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.Hazard_EnvironmentalRiskItemService.DeleteEnvironmentalRiskItemById(rowID);
                }
                environmentalRiskItems = (from x in Funs.DB.View_Hazard_EnvironmentalRiskItem where x.EnvironmentalRiskListId == this.EnvironmentalRiskListId orderby x.EType, x.Code select x).ToList();
                Grid1.DataSource = environmentalRiskItems;
                Grid1.DataBind();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
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
            if (string.IsNullOrEmpty(this.EnvironmentalRiskListId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId={1}", this.EnvironmentalRiskListId, BLL.Const.ProjectEnvironmentalRiskListMenuId)));
        }
        #endregion

        #region 区域选择框事件
        /// <summary>
        /// 区域选择框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWorkArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpWorkArea.SelectedValue != BLL.Const._Null)
            {
                this.txtWorkArea.Text = this.drpWorkArea.SelectedText;
            }
            else
            {
                this.txtWorkArea.Text = string.Empty;
            }
        }
        #endregion
    }
}