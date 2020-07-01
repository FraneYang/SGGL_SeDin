using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class TrainType : PageBase
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
            var q = from x in Funs.DB.Base_TrainType orderby x.TrainTypeCode select x;
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
        private List<Model.Base_TrainType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_TrainType> source = (from x in BLL.Funs.DB.Base_TrainType orderby x.TrainTypeCode select x).ToList();
            List<Model.Base_TrainType> paged = new List<Model.Base_TrainType>();

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
            string rowID = hfFormID.Text;
            if (this.judgementDelete(rowID, true))
            {
                var newTrainType = BLL.TrainTypeService.GetTrainTypeById(rowID);
                if (newTrainType != null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, newTrainType.TrainTypeCode, newTrainType.TrainTypeId, BLL.Const.TrainTypeMenuId, BLL.Const.BtnDelete);
                    BLL.TrainTypeService.DeleteTrainTypeById(rowID);
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
                    if (this.judgementDelete(rowID, true))
                    {
                        var newTrainType = BLL.TrainTypeService.GetTrainTypeById(rowID);
                        if (newTrainType != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, newTrainType.TrainTypeCode, newTrainType.TrainTypeId, BLL.Const.TrainTypeMenuId, BLL.Const.BtnDelete);
                            BLL.TrainTypeService.DeleteTrainTypeById(rowID);
                        }
                    }
                }

                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
                Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
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
            var trainType = BLL.TrainTypeService.GetTrainTypeById(Id);
            if (trainType != null)
            {
                this.txtTrainTypeCode.Text = trainType.TrainTypeCode;
                this.txtTrainTypeName.Text = trainType.TrainTypeName;
                if (trainType.IsAboutSendCard == true)
                {
                    this.ckbIsAboutSendCard.Checked = true;
                }
                else
                {
                    this.ckbIsAboutSendCard.Checked = false;
                }
                if (trainType.IsRepeat == true)
                {
                    this.ckbIsRepeat.Checked = true;
                }
                else
                {
                    this.ckbIsRepeat.Checked = false;
                }
                this.txtRemark.Text = trainType.Remark;
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
            Model.Base_TrainType newTrainType = new Model.Base_TrainType
            {
                TrainTypeCode = this.txtTrainTypeCode.Text.Trim(),
                TrainTypeName = this.txtTrainTypeName.Text.Trim()
            };
            if (this.ckbIsAboutSendCard.Checked == true)
            {
                newTrainType.IsAboutSendCard = true;
            }
            else
            {
                newTrainType.IsAboutSendCard = false;
            }
            if (this.ckbIsRepeat.Checked == true)
            {
                newTrainType.IsRepeat = true;
            }
            else
            {
                newTrainType.IsRepeat = false;
            }
            newTrainType.Remark = txtRemark.Text.Trim();
            if (string.IsNullOrEmpty(strRowID))
            {
                newTrainType.TrainTypeId = SQLHelper.GetNewID(typeof(Model.Base_TrainType));
                BLL.TrainTypeService.AddTrainType(newTrainType);
                BLL.LogService.AddSys_Log(this.CurrUser, newTrainType.TrainTypeCode, newTrainType.TrainTypeId, BLL.Const.TrainTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newTrainType.TrainTypeId = strRowID;
                BLL.TrainTypeService.UpdateTrainType(newTrainType);
                BLL.LogService.AddSys_Log(this.CurrUser, newTrainType.TrainTypeCode, newTrainType.TrainTypeId, BLL.Const.TrainTypeMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, newTrainType.TrainTypeId));
            Alert.ShowInTop("保存成功！", MessageBoxIcon.Success);
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
            var buttonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainTypeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnMenuSet.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证培训类型名称、编号是否存在
        /// <summary>
        /// 验证培训类型名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_TrainType.FirstOrDefault(x => x.TrainTypeCode == this.txtTrainTypeCode.Text.Trim() && (x.TrainTypeId != hfFormID.Text || (hfFormID.Text == null && x.TrainTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的类型编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_TrainType.FirstOrDefault(x => x.TrainTypeName == this.txtTrainTypeName.Text.Trim() && (x.TrainTypeId != hfFormID.Text || (hfFormID.Text == null && x.TrainTypeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的类型名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            //if (Funs.DB.EduTrain_TrainRecord.FirstOrDefault(x => x.TrainTypeId == id) != null)
            //{
            //    content = "该培训类别已在【教育培训】中使用，不能删除！";
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

        /// <summary>
        /// 设置培训题型设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuSet_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TrainTypeEdit.aspx?TrainTypeId={0}", Grid1.SelectedRowID, "设置 - ")));
        }
    }
}