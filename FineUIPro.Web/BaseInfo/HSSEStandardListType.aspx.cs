using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class HSSEStandardListType : PageBase
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).Base_HSSEStandardListType orderby x.TypeCode select x;
            Grid1.RecordCount = q.Count();
            // 2.获取当前分页数据
            var table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_HSSEStandardListType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_HSSEStandardListType> source = (from x in new Model.SGGLDB(Funs.ConnString).Base_HSSEStandardListType orderby x.TypeCode select x).ToList();
            List<Model.Base_HSSEStandardListType> paged = new List<Model.Base_HSSEStandardListType>();
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

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页选择事件
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
        // 删除数据
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (judgementDelete(hfFormID.Text, true))
            {
                var getType = BLL.HSSEStandardListTypeService.GetHSSEStandardListType(hfFormID.Text);
                if (getType != null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, getType.TypeCode, getType.TypeId, BLL.Const.LawsRegulationsTypeMenuId, BLL.Const.BtnDelete);
                    BLL.HSSEStandardListTypeService.DeleteHSSEStandardListType(hfFormID.Text);
                }
                // 重新绑定表格，并模拟点击[新增按钮]
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
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
                    if (judgementDelete(rowID, true))
                    {
                        var getType = BLL.HSSEStandardListTypeService.GetHSSEStandardListType(rowID);
                        if (getType != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getType.TypeCode, getType.TypeId, BLL.Const.LawsRegulationsTypeMenuId, BLL.Const.BtnDelete);
                            BLL.HSSEStandardListTypeService.DeleteHSSEStandardListType(rowID);
                        }
                    }
                }
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            //if (new Model.SGGLDB(Funs.ConnString).Law_HSSEStandardsList.FirstOrDefault(x => x.TypeId == id) != null)
            //{
            //    content = "该标准规范类型已在【安全标准规范】中使用，不能删除！";
            //}
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
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
                Alert.ShowInTop("请至少选择一条记录！",MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var standardListType = BLL.HSSEStandardListTypeService.GetHSSEStandardListType(Id);
            if (standardListType != null)
            {
                this.tbxTypeCode.Text = standardListType.TypeCode;
                this.tbxTypeName.Text = standardListType.TypeName;
                this.tbxRemark.Text = standardListType.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }
        #endregion

        #region 保存
        // 保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strRowID = hfFormID.Text;
            Model.Base_HSSEStandardListType hSSEStandardListType = new Base_HSSEStandardListType();

            if (String.IsNullOrEmpty(strRowID))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Base_HSSEStandardListType));
                hSSEStandardListType.TypeId = newKeyID;
                hSSEStandardListType.TypeCode = tbxTypeCode.Text.Trim();
                hSSEStandardListType.TypeName = tbxTypeName.Text.Trim();
                hSSEStandardListType.Remark = tbxRemark.Text.Trim();
                BLL.HSSEStandardListTypeService.AddHSSEStandardListType(hSSEStandardListType);
                BLL.LogService.AddSys_Log(this.CurrUser, hSSEStandardListType.TypeCode, hSSEStandardListType.TypeId, BLL.Const.HSSEStandardListTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                hSSEStandardListType.TypeId = strRowID;
                BLL.HSSEStandardListTypeService.UpdateHSSEStandardListType(strRowID, tbxTypeCode.Text.Trim(), tbxTypeName.Text.Trim(), tbxRemark.Text.Trim());
                BLL.LogService.AddSys_Log(this.CurrUser, hSSEStandardListType.TypeCode, hSSEStandardListType.TypeId, BLL.Const.HSSEStandardListTypeMenuId, BLL.Const.BtnModify);
            }

            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, hSSEStandardListType.TypeId));
        }
        #endregion

        #region 验证标准规范类型名称、编号是否存在
        /// <summary>
        /// 验证标准规范类型名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Base_HSSEStandardListType.FirstOrDefault(x => x.TypeCode == this.tbxTypeCode.Text.Trim() && (x.TypeId != hfFormID.Text || (hfFormID.Text == null && x.TypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_HSSEStandardListType.FirstOrDefault(x => x.TypeName == this.tbxTypeName.Text.Trim() && (x.TypeId != hfFormID.Text || (hfFormID.Text == null && x.TypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEStandardListTypeMenuId);
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
    }
}