using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class EmergencyType : PageBase
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
            var q = from x in new Model.SGGLDB(Funs.ConnString).Base_EmergencyType orderby x.EmergencyTypeCode select x;
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
        private List<Model.Base_EmergencyType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_EmergencyType> source = (from x in new Model.SGGLDB(Funs.ConnString).Base_EmergencyType orderby x.EmergencyTypeCode select x).ToList();
            List<Model.Base_EmergencyType> paged = new List<Model.Base_EmergencyType>();

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
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
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
            if (judgementDelete(hfFormID.Text, true))
            {
                var getType = BLL.EmergencyTypeService.GetEmergencyTypeById(hfFormID.Text);
                if (getType != null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, getType.EmergencyTypeCode, getType.EmergencyTypeId, BLL.Const.EmergencyTypeMenuId, BLL.Const.BtnDelete);
                    BLL.EmergencyTypeService.DeleteEmergencyTypeById(hfFormID.Text);
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
                    var getType = BLL.EmergencyTypeService.GetEmergencyTypeById(rowID);
                    if (getType != null)
                    {
                        if (judgementDelete(rowID, true))
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getType.EmergencyTypeCode, getType.EmergencyTypeId, BLL.Const.EmergencyTypeMenuId, BLL.Const.BtnDelete);
                            BLL.EmergencyTypeService.DeleteEmergencyTypeById(rowID);
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
            //if (new Model.SGGLDB(Funs.ConnString).Technique_Emergency.FirstOrDefault(x => x.EmergencyTypeId == id) != null)
            //{
            //    content = "该应急预案类型已在【应急预案】中使用，不能删除！";
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
                Alert.ShowInTop("请至少选择一条记录！");
                return;
            }
            string Id = Grid1.SelectedRowID;
            var emergencyType = BLL.EmergencyTypeService.GetEmergencyTypeById(Id);
            if (emergencyType != null)
            {
                this.txtEmergencyTypeCode.Text = emergencyType.EmergencyTypeCode;
                this.txtEmergencyTypeName.Text = emergencyType.EmergencyTypeName;
                this.txtRemark.Text = emergencyType.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }
        #endregion        

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strRowID = hfFormID.Text;
            Model.Base_EmergencyType newEmergencyType = new Model.Base_EmergencyType
            {
                EmergencyTypeCode = this.txtEmergencyTypeCode.Text.Trim(),
                EmergencyTypeName = this.txtEmergencyTypeName.Text.Trim(),
                Remark = txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                newEmergencyType.EmergencyTypeId = SQLHelper.GetNewID(typeof(Model.Base_EmergencyType));
                BLL.EmergencyTypeService.AddEmergencyType(newEmergencyType);
                BLL.LogService.AddSys_Log(this.CurrUser, newEmergencyType.EmergencyTypeCode, newEmergencyType.EmergencyTypeId, BLL.Const.EmergencyTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newEmergencyType.EmergencyTypeId = strRowID;
                BLL.EmergencyTypeService.UpdateEmergencyType(newEmergencyType);
                BLL.LogService.AddSys_Log(this.CurrUser, newEmergencyType.EmergencyTypeCode, newEmergencyType.EmergencyTypeId, BLL.Const.EmergencyTypeMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, newEmergencyType.EmergencyTypeId));
        }
        #endregion

        #region 验证应急预案类型名称、编号是否存在
        /// <summary>
        /// 验证应急预案类型名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Base_EmergencyType.FirstOrDefault(x => x.EmergencyTypeCode == this.txtEmergencyTypeCode.Text.Trim() && (x.EmergencyTypeId != hfFormID.Text || (hfFormID.Text == null && x.EmergencyTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_EmergencyType.FirstOrDefault(x => x.EmergencyTypeName == this.txtEmergencyTypeName.Text.Trim() && (x.EmergencyTypeId != hfFormID.Text || (hfFormID.Text == null && x.EmergencyTypeId != null)));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EmergencyTypeMenuId);
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