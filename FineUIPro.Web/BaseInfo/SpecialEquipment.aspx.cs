using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SpecialEquipment : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                SpecialEquipmentService.InitSpecialEquipmentTypeDropDownList(this.drpType, true);
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).Base_SpecialEquipment                        
                        orderby x.SpecialEquipmentCode select  new
                        {
                            x.SpecialEquipmentId,
                            x.SpecialEquipmentCode,
                            x.SpecialEquipmentName,
                            TypeName=SpecialEquipmentService.getTypeName(x.SpecialEquipmentType),
                            x.SpecialEquipmentType,
                            x.Remark,
                            x.IsSpecial,
                        };

            Grid1.RecordCount =q.Count();
            DataTable tb = this.GetPagedDataTable(Grid1, q);
            Grid1.DataSource = tb;
            Grid1.DataBind();
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
            var getD = BLL.SpecialEquipmentService.GetSpecialEquipmentById(hfFormID.Text);
            if (getD != null)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, getD.SpecialEquipmentCode, getD.SpecialEquipmentId, BLL.Const.SpecialEquipmentMenuId, BLL.Const.BtnDelete);
                BLL.SpecialEquipmentService.DeleteSpecialEquipmentById(hfFormID.Text);                
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
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getD = BLL.SpecialEquipmentService.GetSpecialEquipmentById(rowID);
                    if (getD != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getD.SpecialEquipmentCode, getD.SpecialEquipmentId, BLL.Const.SpecialEquipmentMenuId, BLL.Const.BtnDelete);
                        BLL.SpecialEquipmentService.DeleteSpecialEquipmentById(rowID);
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
            string Id = Grid1.SelectedRowID;
            var specialEquipment = BLL.SpecialEquipmentService.GetSpecialEquipmentById(Id);
            if (specialEquipment != null)
            {
                this.txtSpecialEquipmentCode.Text = specialEquipment.SpecialEquipmentCode;
                this.txtSpecialEquipmentName.Text = specialEquipment.SpecialEquipmentName;
                if (specialEquipment.IsSpecial == true)
                {
                    this.ckbIsSpecial.Checked = true;
                }
                else
                {
                    this.ckbIsSpecial.Checked = false;
                }
                this.txtRemark.Text = specialEquipment.Remark;
                if (!string.IsNullOrEmpty(specialEquipment.SpecialEquipmentType))
                {
                    this.drpType.SelectedValue = specialEquipment.SpecialEquipmentType;
                }
                else
                {
                    this.drpType.SelectedIndex = 0;
                }
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strRowID = hfFormID.Text;
            Model.Base_SpecialEquipment specialEquipment = new Model.Base_SpecialEquipment
            {
                SpecialEquipmentCode = this.txtSpecialEquipmentCode.Text.Trim(),
                SpecialEquipmentName = this.txtSpecialEquipmentName.Text.Trim()
            };
            if (this.ckbIsSpecial.Checked == true)
            {
                specialEquipment.IsSpecial = true;
            }
            else
            {
                specialEquipment.IsSpecial = false;
            }
            if (this.drpType.SelectedValue != Const._Null)
            {
                specialEquipment.SpecialEquipmentType = this.drpType.SelectedValue;
            }
            else
            {
                specialEquipment.SpecialEquipmentType = null;
            }
            specialEquipment.Remark = txtRemark.Text.Trim();
            if (string.IsNullOrEmpty(strRowID))
            {
                specialEquipment.SpecialEquipmentId = SQLHelper.GetNewID(typeof(Model.Base_SpecialEquipment));
                BLL.SpecialEquipmentService.AddSpecialEquipment(specialEquipment);
                BLL.LogService.AddSys_Log(this.CurrUser, specialEquipment.SpecialEquipmentCode, specialEquipment.SpecialEquipmentId, BLL.Const.SpecialEquipmentMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                specialEquipment.SpecialEquipmentId = strRowID;
                BLL.SpecialEquipmentService.UpdateSpecialEquipment(specialEquipment);
                BLL.LogService.AddSys_Log(this.CurrUser, specialEquipment.SpecialEquipmentCode, specialEquipment.SpecialEquipmentId, BLL.Const.SpecialEquipmentMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, specialEquipment.SpecialEquipmentId));
        }

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpecialEquipmentMenuId);
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

        #region 验证机具设备名称、编号是否存在
        /// <summary>
        /// 验证机具设备名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Base_SpecialEquipment.FirstOrDefault(x => x.SpecialEquipmentCode == this.txtSpecialEquipmentCode.Text.Trim() && (x.SpecialEquipmentId != hfFormID.Text || (hfFormID.Text == null && x.SpecialEquipmentId != null)));
            if (q != null)
            {
                ShowNotify("输入的设备编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = new Model.SGGLDB(Funs.ConnString).Base_SpecialEquipment.FirstOrDefault(x => x.SpecialEquipmentName == this.txtSpecialEquipmentName.Text.Trim() && (x.SpecialEquipmentId != hfFormID.Text || (hfFormID.Text == null && x.SpecialEquipmentId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的设备名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}