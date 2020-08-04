using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SpecialSchemeType : PageBase
    {
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
                //SolutionTempleteTypeService.InitSolutionTempleteDropDownList(drpSolutionTempleteType, true);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"select SpecialSchemeTypeId, SpecialSchemeTypeCode, SpecialSchemeTypeName, S.Remark, S.SolutionTempleteTypeCode,T.SolutionTempleteTypeName from Base_SpecialSchemeType S
              left join Base_SolutionTempleteType T on S.SolutionTempleteTypeCode=T.SolutionTempleteTypeCode ";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_SpecialSchemeType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_SpecialSchemeType> source = (from x in Funs.DB.Base_SpecialSchemeType orderby x.SpecialSchemeTypeCode select x).ToList();
            List<Model.Base_SpecialSchemeType> paged = new List<Model.Base_SpecialSchemeType>();

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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (judgementDelete(hfFormID.Text, true))
            {
                var getSpecialSchemeType = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeById(hfFormID.Text);
                if (getSpecialSchemeType != null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, getSpecialSchemeType.SpecialSchemeTypeCode, getSpecialSchemeType.SpecialSchemeTypeId, BLL.Const.SpecialSchemeTypeMenuId, BLL.Const.BtnDelete);
                    BLL.SpecialSchemeTypeService.DeleteSpecialSchemeTypeById(hfFormID.Text);

                    // 重新绑定表格，并模拟点击[新增按钮]
                    BindGrid();
                    PageContext.RegisterStartupScript("onNewButtonClick();");
                }
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
                        var getSpecialSchemeType = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeById(rowID);
                        if (getSpecialSchemeType != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getSpecialSchemeType.SpecialSchemeTypeCode, getSpecialSchemeType.SpecialSchemeTypeId, BLL.Const.SpecialSchemeTypeMenuId, BLL.Const.BtnDelete);
                            BLL.SpecialSchemeTypeService.DeleteSpecialSchemeTypeById(rowID);
                        }
                    }
                }
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！");
                return;
            }
            string Id = Grid1.SelectedRowID;
            var specialSchemeType = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeById(Id);
            if (specialSchemeType != null)
            {
                this.txtSpecialSchemeTypeCode.Text = specialSchemeType.SpecialSchemeTypeCode;
                this.txtSpecialSchemeTypeName.Text = specialSchemeType.SpecialSchemeTypeName;
                this.txtRemark.Text = specialSchemeType.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strRowID = hfFormID.Text;
            Model.Base_SpecialSchemeType newSpecialSchemeType = new Model.Base_SpecialSchemeType
            {
                SpecialSchemeTypeCode = this.txtSpecialSchemeTypeCode.Text.Trim(),
                SpecialSchemeTypeName = this.txtSpecialSchemeTypeName.Text.Trim(),
                Remark = txtRemark.Text.Trim()
            };
            //if (drpSolutionTempleteType.SelectedValue != BLL.Const._Null) {
            //    newSpecialSchemeType.SolutionTempleteTypeCode = drpSolutionTempleteType.SelectedValue;
            //}
            if (string.IsNullOrEmpty(strRowID))
            {
                newSpecialSchemeType.SpecialSchemeTypeId = SQLHelper.GetNewID(typeof(Model.Base_SpecialSchemeType));
                BLL.SpecialSchemeTypeService.AddSpecialSchemeType(newSpecialSchemeType);
                BLL.LogService.AddSys_Log(this.CurrUser, newSpecialSchemeType.SpecialSchemeTypeCode, newSpecialSchemeType.SpecialSchemeTypeId, BLL.Const.SpecialSchemeTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newSpecialSchemeType.SpecialSchemeTypeId = strRowID;
                BLL.SpecialSchemeTypeService.UpdateSpecialSchemeType(newSpecialSchemeType);
                BLL.LogService.AddSys_Log(this.CurrUser, newSpecialSchemeType.SpecialSchemeTypeCode, newSpecialSchemeType.SpecialSchemeTypeId, BLL.Const.SpecialSchemeTypeMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, newSpecialSchemeType.SpecialSchemeTypeId));
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
            //if (Funs.DB.Technique_SpecialScheme.FirstOrDefault(x => x.SpecialSchemeTypeId == id) != null)
            //{
            //    content = "该专业方案类别已在【专业方案】中使用，不能删除！";
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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpecialSchemeTypeMenuId);
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

        #region 验证专项方案类型名称、编号是否存在
        /// <summary>
        /// 验证专项方案类型名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_SpecialSchemeType.FirstOrDefault(x => x.SpecialSchemeTypeCode == this.txtSpecialSchemeTypeCode.Text.Trim() && (x.SpecialSchemeTypeId != hfFormID.Text || (hfFormID.Text == null && x.SpecialSchemeTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的分部分项工程编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_SpecialSchemeType.FirstOrDefault(x => x.SpecialSchemeTypeName == this.txtSpecialSchemeTypeName.Text.Trim() && (x.SpecialSchemeTypeId != hfFormID.Text || (hfFormID.Text == null && x.SpecialSchemeTypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的分部分项工程名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}