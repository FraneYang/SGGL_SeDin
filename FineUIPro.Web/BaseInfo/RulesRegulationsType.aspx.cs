using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class RulesRegulationsType : PageBase
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
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var q = from x in Funs.DB.Base_RulesRegulationsType orderby x.RulesRegulationsTypeCode select x;
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
        private List<Model.Base_RulesRegulationsType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_RulesRegulationsType> source = (from x in BLL.Funs.DB.Base_RulesRegulationsType orderby x.RulesRegulationsTypeCode select x).ToList();
            List<Model.Base_RulesRegulationsType> paged = new List<Model.Base_RulesRegulationsType>();

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
                var getRulesRegulationsType = BLL.RulesRegulationsTypeService.GetRulesRegulationsTypeById(hfFormID.Text);
                if (getRulesRegulationsType != null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, getRulesRegulationsType.RulesRegulationsTypeCode, getRulesRegulationsType.RulesRegulationsTypeId, BLL.Const.RulesRegulationsTypeMenuId, BLL.Const.BtnDelete);
                    BLL.RulesRegulationsTypeService.DeleteRulesRegulationsTypeById(hfFormID.Text);

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
                        var getRulesRegulationsType = BLL.RulesRegulationsTypeService.GetRulesRegulationsTypeById(rowID);
                        if (getRulesRegulationsType != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getRulesRegulationsType.RulesRegulationsTypeCode, getRulesRegulationsType.RulesRegulationsTypeId, BLL.Const.RulesRegulationsTypeMenuId, BLL.Const.BtnDelete);
                            BLL.RulesRegulationsTypeService.DeleteRulesRegulationsTypeById(rowID);
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
                Alert.ShowInTop("请至少选择一条记录！",MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var rulesRegulationsType = BLL.RulesRegulationsTypeService.GetRulesRegulationsTypeById(Id);
            if (rulesRegulationsType != null)
            {
                this.txtRulesRegulationsTypeCode.Text = rulesRegulationsType.RulesRegulationsTypeCode;
                this.txtRulesRegulationsTypeName.Text = rulesRegulationsType.RulesRegulationsTypeName;
                this.txtRemark.Text = rulesRegulationsType.Remark;
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
            Model.Base_RulesRegulationsType rulesRegulationsType = new Model.Base_RulesRegulationsType
            {
                RulesRegulationsTypeCode = this.txtRulesRegulationsTypeCode.Text.Trim(),
                RulesRegulationsTypeName = this.txtRulesRegulationsTypeName.Text.Trim(),
                Remark = txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                rulesRegulationsType.RulesRegulationsTypeId = SQLHelper.GetNewID(typeof(Model.Base_RulesRegulationsType));
                BLL.RulesRegulationsTypeService.AddRulesRegulationsType(rulesRegulationsType);
                BLL.LogService.AddSys_Log(this.CurrUser, rulesRegulationsType.RulesRegulationsTypeCode, rulesRegulationsType.RulesRegulationsTypeId, BLL.Const.RulesRegulationsTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                rulesRegulationsType.RulesRegulationsTypeId = strRowID;
                BLL.RulesRegulationsTypeService.UpdateRulesRegulationsType(rulesRegulationsType);
                BLL.LogService.AddSys_Log(this.CurrUser, rulesRegulationsType.RulesRegulationsTypeCode, rulesRegulationsType.RulesRegulationsTypeId, BLL.Const.RulesRegulationsTypeMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, rulesRegulationsType.RulesRegulationsTypeId));
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
            //if (Funs.DB.Law_RulesRegulations.FirstOrDefault(x => x.RulesRegulationsTypeId == id) != null)
            //{
            //    content = "该规章制度类别已在【安全生产规章制度】中使用，不能删除！";
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RulesRegulationsTypeMenuId);
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

        #region 验证政府部门安全规章类别名称称、编号是否存在
        /// <summary>
        /// 验证政府部门安全规章类别名称称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_RulesRegulationsType.FirstOrDefault(x => x.RulesRegulationsTypeCode == this.txtRulesRegulationsTypeCode.Text.Trim() && (x.RulesRegulationsTypeId != hfFormID.Text || (hfFormID.Text == null && x.RulesRegulationsTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_RulesRegulationsType.FirstOrDefault(x => x.RulesRegulationsTypeName == this.txtRulesRegulationsTypeName.Text.Trim() && (x.RulesRegulationsTypeId != hfFormID.Text || (hfFormID.Text == null && x.RulesRegulationsTypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}