using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class SolutionTemplate : PageBase
    {
        #region 定义集合
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.Technique_ProjectSolutionTemplete> projectSolutionTempletes = new List<Model.Technique_ProjectSolutionTemplete>();
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
                this.btnAdd.OnClientClick = Window1.GetShowReference("SolutionTemplateEdit.aspx") + "return false;";
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
            string strSql = @"SELECT SolutionTemplate.SolutionTemplateId,"
                            + @"SolutionTemplate.ProjectId,"
                            + @"SolutionTemplate.SolutionTemplateCode,"
                            + @"SolutionTemplate.SolutionTemplateName,"
                            + @"Const.ConstText AS SolutionTemplateType,"
                            + @"SolutionTemplate.CompileMan,"
                            + @"SolutionTemplate.CompileDate,"
                            + @"Users.UserName AS CompileManName "
                          + @" FROM Solution_SolutionTemplate AS SolutionTemplate "
                          + @" LEFT JOIN Sys_User AS Users ON SolutionTemplate.CompileMan=Users.UserId "
                          + @" LEFT JOIN Sys_Const AS Const ON Const.ConstValue = SolutionTemplate.SolutionTemplateType WHERE Const.GroupId='CNProfessional' ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND SolutionTemplate.ProjectId = @ProjectId ";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtSolutionTemplateCode.Text.Trim()))
            {
                strSql += " AND SolutionTemplateCode LIKE @SolutionTemplateCode";
                listStr.Add(new SqlParameter("@SolutionTemplateCode", "%" + this.txtSolutionTemplateCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtSolutionTemplateName.Text.Trim()))
            {
                strSql += " AND SolutionTemplateName LIKE @SolutionTemplateName";
                listStr.Add(new SqlParameter("@SolutionTemplateName", "%" + this.txtSolutionTemplateName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 分页 排序
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SolutionTemplateEdit.aspx?SolutionTemplateId={0}", id, "编辑 - ")));
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
                    var getV = BLL.SolutionTemplateService.GetSolutionTemplateById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.SolutionTemplateCode, getV.SolutionTemplateId, BLL.Const.SolutionTemplateMenuId, BLL.Const.BtnDelete);
                        BLL.SolutionTemplateService.DeleteSolutionTemplateById(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SolutionTemplateMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnAdd.Hidden = false;
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

        #region 提取方案模板
        /// <summary>
        /// 提取模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            projectSolutionTempletes = (from x in Funs.DB.Technique_ProjectSolutionTemplete orderby x.TempleteCode select x).ToList();
            if (projectSolutionTempletes.Count > 0)
            {
                foreach (var item in projectSolutionTempletes)
                {
                    Model.Solution_SolutionTemplate slt = Funs.DB.Solution_SolutionTemplate.FirstOrDefault(x => x.SolutionTemplateId == item.TempleteId);
                    if (slt == null)
                    {
                        Model.Solution_SolutionTemplate solutionTemplate = new Model.Solution_SolutionTemplate
                        {
                            SolutionTemplateId = item.TempleteId,
                            ProjectId = this.CurrUser.LoginProjectId,
                            SolutionTemplateCode = item.TempleteCode,
                            SolutionTemplateName = item.TempleteName,
                            SolutionTemplateType = item.TempleteType,
                            FileContents = item.FileContents,
                            CompileMan = item.CompileMan,
                            CompileDate = item.CompileDate
                        };
                        BLL.SolutionTemplateService.AddSolutionTemplate(solutionTemplate);
                    }
                }
                
                BLL.LogService.AddSys_Log(this.CurrUser, "提取方案模板！", null, BLL.Const.SolutionTemplateMenuId, BLL.Const.BtnDownload);
                Alert.Show("提取成功！", MessageBoxIcon.Success);
                this.BindGrid();
            }
        }
        #endregion
    }
}