using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class EquipmentQuality : PageBase
    {
        #region 项目主键
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
                ////权限按钮方法
                this.GetButtonPower();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                this.btnNew.OnClientClick = Window1.GetShowReference("EquipmentQualityEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT EquipmentQuality.EquipmentQualityId,"
                          + @"EquipmentQuality.ProjectId,"
                          + @"CodeRecords.Code AS EquipmentQualityCode,"
                          + @"EquipmentQuality.UnitId,"
                          + @"EquipmentQuality.SpecialEquipmentId,"
                          + @"EquipmentQuality.EquipmentQualityName,"
                          + @"EquipmentQuality.SizeModel,"
                          + @"EquipmentQuality.FactoryCode,"
                          + @"EquipmentQuality.CertificateCode,"
                          + @"EquipmentQuality.CheckDate,"
                          + @"EquipmentQuality.LimitDate,"
                          + @"EquipmentQuality.InDate,"
                          + @"EquipmentQuality.OutDate,"
                          + @"EquipmentQuality.ApprovalPerson,"
                          + @"EquipmentQuality.CarNum,"
                          + @"EquipmentQuality.Remark,"
                          + @"EquipmentQuality.CompileMan,"
                          + @"EquipmentQuality.CompileDate,"
                          + @"Unit.UnitName,"
                          + @"SpecialEquipment.SpecialEquipmentName"
                          + @" FROM QualityAudit_EquipmentQuality AS EquipmentQuality "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON EquipmentQuality.EquipmentQualityId = CodeRecords.DataId "
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = EquipmentQuality.UnitId "
                          + @" LEFT JOIN Base_SpecialEquipment AS SpecialEquipment ON SpecialEquipment.SpecialEquipmentId = EquipmentQuality.SpecialEquipmentId "
                          + @" LEFT JOIN Sys_User AS Users ON EquipmentQuality.CompileMan = Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND EquipmentQuality.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtEquipmentQualityCode.Text.Trim()))
            {
                strSql += " AND EquipmentQualityCode LIKE @EquipmentQualityCode";
                listStr.Add(new SqlParameter("@EquipmentQualityCode", "%" + this.txtEquipmentQualityCode.Text.Trim() + "%"));
            }
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND EquipmentQuality.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtSpecialEquipmentName.Text.Trim()))
            {
                strSql += " AND SpecialEquipmentName LIKE @SpecialEquipmentName";
                listStr.Add(new SqlParameter("@SpecialEquipmentName", "%" + this.txtSpecialEquipmentName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;           
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
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
            var equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(id);
            if (equipmentQuality != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentQualityEdit.aspx?EquipmentQualityId={0}", id, "编辑 - ")));
            }
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(rowID);
                    if (equipmentQuality != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, equipmentQuality.EquipmentQualityCode, equipmentQuality.EquipmentQualityId, BLL.Const.EquipmentQualityMenuId, BLL.Const.BtnDelete);
                        BLL.EquipmentQualityService.DeleteEquipmentQualityById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("特殊机具设备资质" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
           this.Grid1.PageSize = this.Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region Grid点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string id = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "auditDetail")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentQualityAudit.aspx?EquipmentQualityId={0}", id, "审查记录 - ")));
            }
        }
        #endregion
    }
}