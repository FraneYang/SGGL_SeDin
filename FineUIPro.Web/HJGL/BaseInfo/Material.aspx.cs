using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HJGL.BaseInfo
{
    public partial class Material : PageBase
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
                this.drpSteType.DataTextField = "Text";
                this.drpSteType.DataValueField = "Value";
                this.drpSteType.DataSource = BLL.DropListService.HJGL_GetSteTypeList();
                this.drpSteType.DataBind();
                Funs.FineUIPleaseSelect(this.drpSteType);
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        #region 类别变化事件
        /// <summary>
        /// 类别变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void drpMaterialClass_TextChanged(object sender, EventArgs e)
        //{
        //    this.drpMaterialGroup.Items.Clear();
           
        //    Funs.FineUIPleaseSelect(this.drpMaterialGroup);
        //    this.drpMaterialGroup.SelectedValue = BLL.Const._Null;
        //}
        #endregion       

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT MaterialId,MaterialCode,MaterialType,SteelType,Remark,MaterialClass,MaterialGroup,MetalType"
                         + @" FROM dbo.Base_Material WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtMaterialCode.Text.Trim()))
            {
                strSql += " AND MaterialCode LIKE @MaterialCode";
                listStr.Add(new SqlParameter("@MaterialCode", "%" + this.txtMaterialCode.Text.Trim() + "%"));
            }
            
            if (this.drpSteType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND SteelType = @SteelType";
                listStr.Add(new SqlParameter("@SteelType", this.drpSteType.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
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

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (GetButtonPower(Const.BtnAdd))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MaterialEdit.aspx", "新增 - ")));
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
            if (GetButtonPower(Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MaterialEdit.aspx?MaterialId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else if (GetButtonPower(Const.BtnSee))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MaterialView.aspx?MaterialId={0}", Grid1.SelectedRowID, "查看 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
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
            if (GetButtonPower(Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var getMaterial = BLL.Base_MaterialService.GetMaterialByMaterialId(rowID);
                        if (getMaterial != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.Base_MaterialService.DeleteMaterialByMaterialId(rowID);
                                //BLL.Sys_LogService.AddLog(Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_MaterialMenuId, Const.BtnDelete, rowID);
                            }
                            else
                            {
                                strShowNotify += "材质定义"+"：" + getMaterial.MaterialCode + cont;
                            }
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
            //if (Funs.DB.Pipeline_Pipeline.FirstOrDefault(x => x.MainMaterialId == id) != null || Funs.DB.Pipeline_WeldJoint.FirstOrDefault(x => x.Material1Id == id) != null)
            //{
            //    content += "已在【管线焊口信息】中使用，不能删除！";
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MaterialView.aspx?MaterialId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertMaterialClassCode(object MaterialClassId)
        {
            string name = string.Empty;
          
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertMaterialGroupCode(object MaterialGroupId)
        {
            string name = string.Empty;
           
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertSteelType(object SteelType)
        {
            string name = string.Empty;
            if (SteelType != null)
            {
                var dropValue = BLL.DropListService.HJGL_GetSteTypeList().FirstOrDefault(x => x.Value == SteelType.ToString());
                if (dropValue != null)
                {
                    name = dropValue.Text;
                }
            }

            return name;
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_MaterialMenuId, button);
        }
        #endregion
    }
}