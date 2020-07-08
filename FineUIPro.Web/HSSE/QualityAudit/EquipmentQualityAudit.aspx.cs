using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class EquipmentQualityAudit : PageBase
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
                GetButtonPower();
                string equipmentQualityId = Request.Params["EquipmentQualityId"];
                btnNew.OnClientClick = Window1.GetShowReference("EquipmentQualityAuditEdit.aspx?EquipmentQualityId=" + equipmentQualityId) + "return false;";
                var details = (from x in new Model.SGGLDB(Funs.ConnString).View_QualityAudit_EquipmentQualityAuditDetail
                                                                                     where x.ProjectId == this.CurrUser.LoginProjectId && x.EquipmentQualityId == equipmentQualityId
                                                                                   orderby x.AuditDate descending
                                                                                   select x).ToList();
                Grid1.DataSource = details;
                Grid1.DataBind();
            }
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
            string AuditDetailId = Grid1.SelectedRowID;
            var auditDetail = BLL.EquipmentQualityAuditDetailService.GetEquipmentQualityAuditDetailById(AuditDetailId);
            if (auditDetail != null)
            {
                if (this.btnMenuEdit.Hidden)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentQualityAuditView.aspx?AuditDetailId={0}", AuditDetailId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentQualityAuditEdit.aspx?AuditDetailId={0}", AuditDetailId, "编辑 - ")));
                }
            }
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
                    var detail = BLL.EquipmentQualityAuditDetailService.GetEquipmentQualityAuditDetailById(rowID);
                    if (detail != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, null, detail.AuditDetailId, BLL.Const.EquipmentQualityMenuId, BLL.Const.BtnModify);
                        BLL.EquipmentQualityAuditDetailService.DeleteEquipmentQualityAuditDetailById(rowID);
                    }
                }

                var details = (from x in new Model.SGGLDB(Funs.ConnString).View_QualityAudit_EquipmentQualityAuditDetail
                                                                                     where x.ProjectId == this.CurrUser.LoginProjectId && x.EquipmentQualityId == Request.Params["EquipmentQualityId"]
                                                                                   orderby x.AuditDate descending
                                                                                   select x).ToList();
                Grid1.DataSource = details;
                Grid1.DataBind();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
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
            //if (Request.Params["value"] == "0")
            //{
            //    return;
            //}
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EquipmentQualityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            var details = (from x in new Model.SGGLDB(Funs.ConnString).View_QualityAudit_EquipmentQualityAuditDetail
                                                                                 where x.ProjectId == this.CurrUser.LoginProjectId && x.EquipmentQualityId == Request.Params["EquipmentQualityId"]
                                                                               orderby x.AuditDate descending
                                                                               select x).ToList();
            Grid1.DataSource = details;
            Grid1.DataBind();
        }
    }
}