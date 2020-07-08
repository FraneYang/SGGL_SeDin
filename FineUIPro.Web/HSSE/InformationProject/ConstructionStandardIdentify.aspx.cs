using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class ConstructionStandardIdentify : PageBase
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
                Funs.DropDownPageSize(this.ddlPageSize);
                ////权限按钮方法
                this.GetButtonPower();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string projectId = this.CurrUser.LoginProjectId;
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))
            {
                projectId = Request.Params["projectId"];
            }
            List<Model.View_InformationProject_ConstructionStandardIdentify> q = (from x in new Model.SGGLDB(Funs.ConnString).View_InformationProject_ConstructionStandardIdentify
                                                                                  where x.ProjectId == projectId && x.State == BLL.Const.State_2
                                                                                  orderby x.IdentifyDate descending
                                                                                  select x).ToList();
           

            if (!string.IsNullOrEmpty(txtConstructionStandardIdentifyCode.Text.Trim()))
            {
                q = q.Where(e => e.ConstructionStandardIdentifyCode.Contains(txtConstructionStandardIdentifyCode.Text.Trim())).ToList();
            }
            Grid1.RecordCount = q.Count();
            // 2.获取当前分页数据
            var table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.View_InformationProject_ConstructionStandardIdentify> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.View_InformationProject_ConstructionStandardIdentify> source = new List<Model.View_InformationProject_ConstructionStandardIdentify>();
            source = (from x in new Model.SGGLDB(Funs.ConnString).View_InformationProject_ConstructionStandardIdentify
                      where x.ProjectId == this.CurrUser.LoginProjectId
                      orderby x.IdentifyDate descending
                      select x).ToList();
            if (!string.IsNullOrEmpty(txtConstructionStandardIdentifyCode.Text.Trim()))
            {
                source = source.Where(e => e.ConstructionStandardIdentifyCode.Contains(txtConstructionStandardIdentifyCode.Text.Trim())).ToList();
            }
            List<Model.View_InformationProject_ConstructionStandardIdentify> paged = new List<Model.View_InformationProject_ConstructionStandardIdentify>();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Count())
            {
                rowend = source.Count();
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.Add(source[i]);
            }

            return paged;
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

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
            var constructionStandardIdentify = BLL.ConstructionStandardIdentifyService.GetConstructionStandardIdentifyById(id);
            if (constructionStandardIdentify != null)
            {
                if (constructionStandardIdentify.State == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ConstructionStandardIdentifyView.aspx?ConstructionStandardIdentifyId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ConstructionStandardIdentifyEdit.aspx?ConstructionStandardIdentifyId={0}", id, "编辑 - ")));
                }
            }
        }
        #endregion

        #region 增加、删除数据方法
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            int count = BLL.ConstructionStandardIdentifyService.GetConstructionStandardIdentifyByVersionIsNull(this.CurrUser.LoginProjectId);
            if (count == 0)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ConstructionStandardIdentifyEdit.aspx", "选择 - ")));
            }
            else
            {
                Alert.ShowInTop("标准规范版本号还未生成，不能编制！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string constructionStandardIdentifyId = Grid1.DataKeys[rowIndex][0].ToString();
                    var getD = BLL.ConstructionStandardIdentifyService.GetConstructionStandardIdentifyById(constructionStandardIdentifyId);
                    if (getD != null)
                    {
                        BLL.ConstructionStandardSelectedItemService.DeleteConstructionStandardSelectedItemByConstructionStandardIdentifyId(constructionStandardIdentifyId);
                        BLL.ConstructionStandardIdentifyService.DeleteConstructionStandardIdentifyById(constructionStandardIdentifyId);
                        BLL.LogService.AddSys_Log(this.CurrUser, getD.ConstructionStandardIdentifyCode, getD.ConstructionStandardIdentifyId, BLL.Const.ConstructionStandardIdentifyMenuId, BLL.Const.BtnDelete);
                    }
                }

                BindGrid();
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
            if (Request.Params["value"] == "0")
            {
                return;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ConstructionStandardIdentifyMenuId);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("标准规范清单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ConstructionStandardIdentify.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ConstructionStandardIdentify.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion        

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
    }
}