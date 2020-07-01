using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SolutionTempleteType : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var q = from x in Funs.DB.Base_SolutionTempleteType orderby x.SortIndex select x;
            Grid1.RecordCount = q.Count();
            // 2.获取当前分页数据
            var table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_SolutionTempleteType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_SolutionTempleteType> source = (from x in BLL.Funs.DB.Base_SolutionTempleteType orderby x.SortIndex select x).ToList();
            List<Model.Base_SolutionTempleteType> paged = new List<Model.Base_SolutionTempleteType>();

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
        #endregion

        #region 分页下拉选择
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
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL.LogService.AddSys_Log(this.CurrUser, this.txtSolutionTempleteTypeCode.Text, hfFormID.Text, BLL.Const.SolutionTempleteTypeMenuId, BLL.Const.BtnDelete);
            BLL.SolutionTempleteTypeService.DeleteSolutionTempleteTypeById(hfFormID.Text);
          
            // 重新绑定表格，并模拟点击[新增按钮]
            BindGrid();
            PageContext.RegisterStartupScript("onNewButtonClick();");
        }

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
                    var getV = BLL.SolutionTempleteTypeService.GetSolutionTempleteTypeById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.SolutionTempleteTypeCode, getV.SolutionTempleteTypeCode, BLL.Const.SolutionTempleteTypeMenuId, BLL.Const.BtnDelete);
                        BLL.SolutionTempleteTypeService.DeleteSolutionTempleteTypeById(rowID);
                    }
                }
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 右键编辑事件
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
            string Id = Grid1.SelectedRowID;
            var solutionTempleteType = BLL.SolutionTempleteTypeService.GetSolutionTempleteTypeById(Id);
            if (solutionTempleteType != null)
            {
                this.txtSolutionTempleteTypeCode.Text = solutionTempleteType.SolutionTempleteTypeCode;
                this.txtSolutionTempleteTypeName.Text = solutionTempleteType.SolutionTempleteTypeName;
                this.txtRemark.Text = solutionTempleteType.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }
        #endregion

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strRowID = hfFormID.Text;
            Model.Base_SolutionTempleteType solutionTempleteType = new Model.Base_SolutionTempleteType
            {
                SolutionTempleteTypeCode = this.txtSolutionTempleteTypeCode.Text.Trim(),
                SolutionTempleteTypeName = this.txtSolutionTempleteTypeName.Text.Trim(),
                Remark = txtRemark.Text.Trim(),
                SortIndex = Funs.GetNewIntOrZero(this.txtSolutionTempleteTypeCode.Text.Trim())
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                BLL.SolutionTempleteTypeService.AddSolutionTempleteType(solutionTempleteType);
                BLL.LogService.AddSys_Log(this.CurrUser, solutionTempleteType.SolutionTempleteTypeCode, solutionTempleteType.SolutionTempleteTypeCode, BLL.Const.SolutionTempleteTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                BLL.SolutionTempleteTypeService.UpdateSolutionTempleteType(solutionTempleteType);
                BLL.LogService.AddSys_Log(this.CurrUser, solutionTempleteType.SolutionTempleteTypeCode, solutionTempleteType.SolutionTempleteTypeCode, BLL.Const.SolutionTempleteTypeMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, solutionTempleteType.SolutionTempleteTypeCode));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SolutionTempleteTypeMenuId);
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
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证岗位名称、编号是否存在
        /// <summary>
        /// 验证岗位名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_SolutionTempleteType.FirstOrDefault(x => x.SolutionTempleteTypeCode == this.txtSolutionTempleteTypeCode.Text.Trim() && (x.SolutionTempleteTypeCode != hfFormID.Text || hfFormID.Text == null && x.SolutionTempleteTypeCode != null));
            if (q != null)
            {
                Alert.ShowInTop("输入的方案模板类型编号已存在！", MessageBoxIcon.Warning);
                return;
            }

            var q2 = Funs.DB.Base_SolutionTempleteType.FirstOrDefault(x => x.SolutionTempleteTypeName == this.txtSolutionTempleteTypeName.Text.Trim() && (x.SolutionTempleteTypeCode != hfFormID.Text || (hfFormID.Text == null && x.SolutionTempleteTypeCode != null)));
            if (q2 != null)
            {
                Alert.ShowInTop("输入的方案模板类型已存在！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion
    }
}