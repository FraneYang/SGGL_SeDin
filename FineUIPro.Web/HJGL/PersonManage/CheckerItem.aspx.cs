using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class CheckerItem : PageBase
    {
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
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
                string PersonId = Request.Params["PersonId"];
                if (!string.IsNullOrEmpty(PersonId))
                {
                    this.lblWelderName.Text = BLL.WelderService.GetWelderById(PersonId).PersonName;
                }
            }
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT WelderQualifyId, WelderId, 
                                     QualificationItem, LimitDate, CheckDate 
                           FROM Welder_WelderQualify
                           LEFT JOIN SitePerson_Person AS Welder ON Welder.PersonId=Welder_WelderQualify.WelderId
                           WHERE WelderId=@WelderId";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@WelderId", Request.Params["PersonId"]));
            if (!string.IsNullOrEmpty(this.txtQualificationItem.Text))
            {
                strSql += " and QualificationItem LIKE  @QualificationItem";
                parms.Add(new SqlParameter("@QualificationItem", "%" + this.txtQualificationItem.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = parms.ToArray();
            DataTable dt = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = dt.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, dt);

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
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckerItemEdit.aspx?PersonId={0}", Request.Params["PersonId"], "新增 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
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
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }

            ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnSave))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemEdit.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemView.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "查看 - ")));
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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnAdd))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var welder = BLL.WelderQualifyService.GetWelderQualifyById(rowID);
                        if (welder != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.WelderQualifyService.DeleteWelderQualifyById(rowID);
                                //BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除安装组件信息");
                            }
                            //else
                            //{
                            //    strShowNotify += Resources.Lan.WelderQualification + "：" + welder.QualificationItem + cont;
                            //}
                        }
                    }

                    BindGrid();
                    if (!string.IsNullOrEmpty(strShowNotify))
                    {
                        Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            //if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id) != null)
            //{
            //    content += "已在【项目用户】中使用，不能删除！";
            //}            

            return content;
        }
        #endregion

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 查看按钮
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("WelderItemView.aspx?WelderQualifyId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        #endregion      
    }
}