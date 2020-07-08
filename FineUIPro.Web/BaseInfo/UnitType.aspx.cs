using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class UnitType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                Funs.DropDownPageSize(this.ddlPageSize);
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var q = UnitTypeService.GetUnitTypeList();
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
        private List<Model.Base_UnitType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_UnitType> source = (from x in new Model.SGGLDB(Funs.ConnString).Base_UnitType orderby x.UnitTypeCode select x).ToList();
            List<Model.Base_UnitType> paged = new List<Model.Base_UnitType>();

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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.UnitTypeMenuId);
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
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtUnitTypeCode.Text, hfFormID.Text, BLL.Const.UnitTypeMenuId, BLL.Const.BtnDelete);
                BLL.UnitTypeService.DeleteUnitTypeById(hfFormID.Text);
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
                    if (this.judgementDelete(rowID, true))
                    {
                        var getV = BLL.UnitTypeService.GetUnitTypeById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.UnitTypeCode, getV.UnitTypeId, BLL.Const.UnitTypeMenuId, BLL.Const.BtnDelete);
                            BLL.UnitTypeService.DeleteUnitTypeById(rowID);
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
            var unitType = BLL.UnitTypeService.GetUnitTypeById(Id);
            if (unitType != null)
            {
                this.txtUnitTypeCode.Text = unitType.UnitTypeCode;
                this.txtUnitTypeName.Text = unitType.UnitTypeName;
                this.txtRemark.Text = unitType.Remark;
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
            Model.Base_UnitType newUnitType = new Model.Base_UnitType
            {
                UnitTypeCode = this.txtUnitTypeCode.Text.Trim(),
                UnitTypeName = this.txtUnitTypeName.Text.Trim(),
                Remark = txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                newUnitType.UnitTypeId = SQLHelper.GetNewID(typeof(Model.Base_UnitType));
                BLL.UnitTypeService.AddUnitType(newUnitType);
                BLL.LogService.AddSys_Log(this.CurrUser, newUnitType.UnitTypeCode, newUnitType.UnitTypeId, BLL.Const.UnitTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newUnitType.UnitTypeId = strRowID;
                BLL.UnitTypeService.UpdateUnitType(newUnitType);
                BLL.LogService.AddSys_Log(this.CurrUser, newUnitType.UnitTypeCode, newUnitType.UnitTypeId,BLL.Const.UnitTypeMenuId,BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, newUnitType.UnitTypeId));
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
            if (new Model.SGGLDB(Funs.ConnString).Base_Unit.FirstOrDefault(x => x.UnitTypeId == id) != null)
            {
                content = "该单位类型已在【单位设置】中使用，不能删除！";
            }
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

        #region 验证单位类别名称、编号是否存在
        /// <summary>
        /// 验证单位类别名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Base_UnitType.FirstOrDefault(x => x.UnitTypeCode == this.txtUnitTypeCode.Text.Trim() && (x.UnitTypeId != hfFormID.Text || (hfFormID.Text == null && x.UnitTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_UnitType.FirstOrDefault(x => x.UnitTypeName == this.txtUnitTypeName.Text.Trim() && (x.UnitTypeId != hfFormID.Text || (hfFormID.Text == null && x.UnitTypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}