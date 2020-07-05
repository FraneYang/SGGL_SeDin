using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.QualityAudit
{
    public partial class EquipmentPersonQuality : PageBase
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
            string strSql = @"SELECT DISTINCT EquipmentPersonQuality.EquipmentPersonQualityId,Person.PersonId,Person.ProjectId,Person.CardNo,Person.PersonName,Unit.UnitId,Unit.UnitCode,Unit.UnitName,WorkPost.WorkPostId,WorkPost.WorkPostName,WorkPost.WorkPostCode,"
                          + @" EquipmentPersonQuality.CertificateNo,EquipmentPersonQuality.CertificateName,EquipmentPersonQuality.Grade,EquipmentPersonQuality.SendUnit,EquipmentPersonQuality.SendDate,EquipmentPersonQuality.LimitDate,EquipmentPersonQuality.LateCheckDate,"
                          + @" EquipmentPersonQuality.ApprovalPerson,EquipmentPersonQuality.Remark,EquipmentPersonQuality.CompileMan,Users.UserName AS CompileManName,EquipmentPersonQuality.CompileDate,Auditor.UserName AS AuditorName,AuditDate"
                          + @" FROM  SitePerson_Person AS Person  "
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = Person.UnitId"
                          + @" LEFT JOIN QualityAudit_EquipmentPersonQuality AS EquipmentPersonQuality ON Person.PersonId = EquipmentPersonQuality.PersonId "
                          + @" LEFT JOIN Base_WorkPost AS WorkPost ON WorkPost.WorkPostId = Person.WorkPostId "
                          + @" LEFT JOIN Sys_User AS Users ON EquipmentPersonQuality.CompileMan = Users.UserId "
                          + @" LEFT JOIN Sys_User AS Auditor ON EquipmentPersonQuality.AuditorId = Auditor.UserId"
                          + @" WHERE WorkPost.PostType ='" + Const.PostType_5 + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND Person.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (this.drpUnitId.SelectedValue != Const._Null)
            {
                strSql += " AND Person.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                strSql += " AND Person.CardNo LIKE @CardNo";
                listStr.Add(new SqlParameter("@CardNo", "%" + this.txtCardNo.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND Person.PersonName LIKE @PersonName";
                listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWorkPostName.Text.Trim()))
            {
                strSql += " AND WorkPost.WorkPostName LIKE @WorkPostName";
                listStr.Add(new SqlParameter("@WorkPostName", "%" + this.txtWorkPostName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            this.Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            this.Grid1.DataSource = table;
            this.Grid1.DataBind();
            JObject summary = new JObject
            {
                { "UnitName", "合计人数：" },
                { "PersonName", this.Grid1.RecordCount }
            };

            Grid1.SummaryData = summary;
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            this.BindGrid();
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
            var person = BLL.PersonService.GetPersonById(id);
            if (person != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EquipmentPersonQualityEdit.aspx?PersonId={0}", id, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EquipmentPersonQualityMenuId);
            if (buttonList.Count() > 0)
            {                
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("特种设备作业人员资质" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}