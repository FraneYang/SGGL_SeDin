using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Person
{
    public partial class Shunt : PageBase
    {
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
                this.btnNew.OnClientClick = Window1.GetShowReference("ShuntEdit.aspx") + "return false;";
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
            string strSql = @"SELECT Shunt.ShuntId,Shunt.Code,Shunt.ProjectId,Shunt.State,Shunt.CompileMan,Shunt.CompileDate,project.ProjectName"
                                    + @" From dbo.Person_Shunt AS Shunt"
                                    + @" LEFT JOIN Base_Project AS project ON project.projectId=Shunt.ProjectId"
                                    + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            //if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            //{
            //    strSql += " AND Users.UserName LIKE @UserName";
            //    listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
            //}
            //if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是企业单位或者管理员
            //{
            //    strSql += " AND Users.UnitId = @UnitId";
            //    listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            //}

            //if (!string.IsNullOrEmpty(this.txtRoleName.Text.Trim()))
            //{
            //    strSql += " AND Roles.RoleName LIKE @RoleName";
            //    listStr.Add(new SqlParameter("@RoleName", "%" + this.txtRoleName.Text.Trim() + "%"));
            //}
            //if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
            //{
            //    strSql += " AND project.ProjectName LIKE @ProjectName";
            //    listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
            //}
            //if (this.ckbAll.Checked == false)
            //{
            //    strSql += " AND Users.IsPost =1 ";
            //}

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonShuntMenuId);
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

        #region  删除数据
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
                string strShowNotify = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.Person_ShuntDetailService.DeleteShuntDetailByShuntId(rowID);
                    BLL.Person_ShuntApproveService.DeleteShuntApprovesByShuntId(rowID);
                    BLL.Person_ShuntService.DeleteShunt(rowID);
                }
                BindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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
            BindGrid();
        }

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        /// <summary>
        /// Grid行双击事件
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
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var shunt = BLL.Person_ShuntService.GetShunt(Id);

            if (shunt != null)
            {
                if (shunt.State.Equals(Const.Shunt_Complete))
                {
                    Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                    return;
                }
                Model.Person_ShuntApprove approve = BLL.Person_ShuntApproveService.GetShuntApproveByShuntId(Id);
                if (approve != null)
                {
                    if (!string.IsNullOrEmpty(approve.ApproveMan))
                    {
                        if (this.CurrUser.UserId == approve.ApproveMan || CurrUser.UserId == Const.sysglyId)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntEdit.aspx?ShuntId={0}", Id, "编辑 - ")));
                            return;
                        }
                        else if (shunt.State == BLL.Const.Shunt_Complete)
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntView.aspx?ShuntId={0}", Id, "查看 - ")));
                            return;
                        }
                        else
                        {
                            Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                            return;
                        }

                    }
                    //if (this.btnMenuModify.Hidden || checks.State == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListView.aspx?CheckControlCode={0}", codes, "查看 - ")));
                    //    return;
                    //}
                    //else
                    //{
                    //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckListEdit.aspx?CheckControlCode={0}", codes, "编辑 - ")));
                    //    return;
                    //}

                }
                else
                {
                    Alert.ShowInTop("您不是当前办理人，无法编辑,请右键查看！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShuntView.aspx?ShuntId={0}", Id, "查看 - ")));
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id) != null)
            {
                content += "已在【项目员工】中使用，不能删除！";
            }

            return content;
        }
        #endregion

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void ckbAll_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.Shunt_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.Shunt_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.Shunt_Audit)
                {
                    return "审核";
                }
                else if (state.ToString() == BLL.Const.Shunt_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object ShuntId)
        {
            if (ShuntId != null)
            {
                Model.Person_ShuntApprove a = BLL.Person_ShuntApproveService.GetShuntApproveByShuntId(ShuntId.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
    }
}