using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class ConstructionStandardIdentifyEdit : PageBase
    {
        #region  定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ConstructionStandardIdentifyId
        {
            get
            {
                return (string)ViewState["ConstructionStandardIdentifyId"];
            }
            set
            {
                ViewState["ConstructionStandardIdentifyId"] = value;
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        private string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        /// <summary>
        /// 选中项
        /// </summary>
        public string[] arr
        {
            get
            {
                return (string[])ViewState["arr"];
            }
            set
            {
                ViewState["arr"] = value;
            }
        }

        /// <summary>
        /// GV被选择项列表
        /// </summary>
        public List<string> ItemSelectedList
        {
            get
            {
                return (List<string>)ViewState["ItemSelectedList"];
            }
            set
            {
                ViewState["ItemSelectedList"] = value;
            }
        }

        public List<string> ItemSelectedList2
        {
            get
            {
                return (List<string>)ViewState["ItemSelectedList2"];
            }
            set
            {
                ViewState["ItemSelectedList2"] = value;
            }
        }
        #endregion

        #region  页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                BLL.ConstValue.InitConstValueDropDownList(this.drpCNProfessional,ConstValue.Group_CNProfessional,true);
                
                this.ItemSelectedList = new List<string>();
                this.ItemSelectedList2 = new List<string>();
                this.ConstructionStandardIdentifyId = Request.Params["ConstructionStandardIdentifyId"];               
               
                if (!string.IsNullOrEmpty(this.ConstructionStandardIdentifyId))
                {
                    Model.InformationProject_ConstructionStandardIdentify constructionStandardIdentify = BLL.ConstructionStandardIdentifyService.GetConstructionStandardIdentifyById(this.ConstructionStandardIdentifyId);
                    if (constructionStandardIdentify!=null)
                    {
                        this.ProjectId = constructionStandardIdentify.ProjectId;
                        this.txtConstructionStandardIdentifyCode.Text = CodeRecordsService.ReturnCodeByDataId(this.ConstructionStandardIdentifyId);
                        this.txtRemark.Text = constructionStandardIdentify.Remark;
                    }
                    BindGridById(this.ConstructionStandardIdentifyId);//显示选中的项            
                }
                else
                { 
                    // 绑定显示所有项
                    BindGrid();
                    this.ckbAll.Checked = true;
                    ////自动生成编码
                    this.txtConstructionStandardIdentifyCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ConstructionStandardIdentifyMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ConstructionStandardIdentifyMenuId;
                this.ctlAuditFlow.DataId = this.ConstructionStandardIdentifyId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }


        /// <summary>
        /// 显示所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbAll_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (this.ckbAll.Checked == true)
            {
                BindGrid();
            }
            else
            {
                BindGridById(this.ConstructionStandardIdentifyId);
            }
        }

        /// <summary>
        /// 显示勾选的项
        /// </summary>
        private void BindGridById(string constructionStandardIdentifyId)
        {
            var q = (from x in Funs.DB.View_InformationProject_ConstructionStandardSelectedItem
                     where x.ConstructionStandardIdentifyId == constructionStandardIdentifyId
                     orderby x.StandardNo
                     select x).ToList();
            if (!string.IsNullOrEmpty(this.txtStandardGrade.Text.Trim()))
            {
                q = q.Where(e => e.StandardGrade.Contains(this.txtStandardGrade.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtStandardNo.Text.Trim()))
            {
                q = q.Where(e => e.StandardNo.Contains(this.txtStandardNo.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtStandardName.Text.Trim()))
            {
                q = q.Where(e => e.StandardName.Contains(this.txtStandardName.Text.Trim())).ToList();
            }
            if (this.drpCNProfessional.SelectedValue != BLL.Const._Null)
            {
                string code = this.drpCNProfessional.SelectedValue;
                if (code == "1")
                {
                    q = q.Where(e => e.IsSelected1 == true).ToList();
                }
                else if (code == "2")
                {
                    q = q.Where(e => e.IsSelected2 == true).ToList();
                }
                else if (code == "3")
                {
                    q = q.Where(e => e.IsSelected3 == true).ToList();
                }
                else if (code == "4")
                {
                    q = q.Where(e => e.IsSelected4 == true).ToList();
                }
                else if (code == "5")
                {
                    q = q.Where(e => e.IsSelected5 == true).ToList();
                }
                else if (code == "6")
                {
                    q = q.Where(e => e.IsSelected6 == true).ToList();
                }
                else if (code == "7")
                {
                    q = q.Where(e => e.IsSelected7 == true).ToList();
                }
                else if (code == "8")
                {
                    q = q.Where(e => e.IsSelected8 == true).ToList();
                }
                else if (code == "9")
                {
                    q = q.Where(e => e.IsSelected9 == true).ToList();
                }
                else if (code == "10")
                {
                    q = q.Where(e => e.IsSelected10 == true).ToList();
                }
                else if (code == "11")
                {
                    q = q.Where(e => e.IsSelected11 == true).ToList();
                }
                else if (code == "12")
                {
                    q = q.Where(e => e.IsSelected12 == true).ToList();
                }
                else if (code == "13")
                {
                    q = q.Where(e => e.IsSelected13 == true).ToList();
                }
                else if (code == "14")
                {
                    q = q.Where(e => e.IsSelected14 == true).ToList();
                }
                else if (code == "15")
                {
                    q = q.Where(e => e.IsSelected15 == true).ToList();
                }
                else if (code == "16")
                {
                    q = q.Where(e => e.IsSelected16 == true).ToList();
                }
                else if (code == "17")
                {
                    q = q.Where(e => e.IsSelected17 == true).ToList();
                }
                else if (code == "18")
                {
                    q = q.Where(e => e.IsSelected18 == true).ToList();
                }
                else if (code == "19")
                {
                    q = q.Where(e => e.IsSelected19 == true).ToList();
                }
                else if (code == "20")
                {
                    q = q.Where(e => e.IsSelected10 == true).ToList();
                }
                else if (code == "10")
                {
                    q = q.Where(e => e.IsSelected20 == true).ToList();
                }
                else if (code == "21")
                {
                    q = q.Where(e => e.IsSelected21 == true).ToList();
                }
                else if (code == "22")
                {
                    q = q.Where(e => e.IsSelected22 == true).ToList();
                }
                else if (code == "23")
                {
                    q = q.Where(e => e.IsSelected23 == true).ToList();
                }
                else if (code == "90")
                {
                    q = q.Where(e => e.IsSelected90 == true).ToList();
                }
            }

            DataTable tb = this.LINQToDataTable(q);

            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            List<Model.InformationProject_ConstructionStandardSelectedItem> lists = BLL.ConstructionStandardSelectedItemService.GetConstructionStandardSelectedItemsByConstructionStandardIdentifyId(this.ConstructionStandardIdentifyId);
            if (lists.Count() > 0)
            {
                foreach (var item in lists)
                {
                    ItemSelectedList2.Add(item.StandardId.ToString());
                }
            }
            if (ItemSelectedList2.Count > 0)
            {
                for (int j = 0; j < Grid1.Rows.Count; j++)
                {
                    if (ItemSelectedList2.Contains(Grid1.DataKeys[j][0].ToString()))
                    {
                        Grid1.Rows[j].Values[0] = "True";
                    }
                }
            }            
        }

        /// <summary>
        /// 绑定显示所有项数据
        /// </summary>
        private void BindGrid()
        {
            var q = (from x in Funs.DB.Law_HSSEStandardsList
                     orderby x.StandardNo
                     select x).ToList();
            if (!string.IsNullOrEmpty(this.txtStandardGrade.Text.Trim()))
            {
                q = q.Where(e => e.StandardGrade.Contains(this.txtStandardGrade.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtStandardNo.Text.Trim()))
            {
                q = q.Where(e => e.StandardNo.Contains(this.txtStandardNo.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtStandardName.Text.Trim()))
            {
                q = q.Where(e => e.StandardName.Contains(this.txtStandardName.Text.Trim())).ToList();
            }
            if (this.drpCNProfessional.SelectedValue != BLL.Const._Null)
            {
                string code = this.drpCNProfessional.SelectedValue;
                if (code == "1")
                {
                    q = q.Where(e => e.IsSelected1 == true).ToList();
                }
                else if (code == "2")
                {
                    q = q.Where(e => e.IsSelected2 == true).ToList();
                }
                else if (code == "3")
                {
                    q = q.Where(e => e.IsSelected3 == true).ToList();
                }
                else if (code == "4")
                {
                    q = q.Where(e => e.IsSelected4 == true).ToList();
                }
                else if (code == "5")
                {
                    q = q.Where(e => e.IsSelected5 == true).ToList();
                }
                else if (code == "6")
                {
                    q = q.Where(e => e.IsSelected6 == true).ToList();
                }
                else if (code == "7")
                {
                    q = q.Where(e => e.IsSelected7 == true).ToList();
                }
                else if (code == "8")
                {
                    q = q.Where(e => e.IsSelected8 == true).ToList();
                }
                else if (code == "9")
                {
                    q = q.Where(e => e.IsSelected9 == true).ToList();
                }
                else if (code == "10")
                {
                    q = q.Where(e => e.IsSelected10 == true).ToList();
                }
                else if (code == "11")
                {
                    q = q.Where(e => e.IsSelected11 == true).ToList();
                }
                else if (code == "12")
                {
                    q = q.Where(e => e.IsSelected12 == true).ToList();
                }
                else if (code == "13")
                {
                    q = q.Where(e => e.IsSelected13 == true).ToList();
                }
                else if (code == "14")
                {
                    q = q.Where(e => e.IsSelected14 == true).ToList();
                }
                else if (code == "15")
                {
                    q = q.Where(e => e.IsSelected15 == true).ToList();
                }
                else if (code == "16")
                {
                    q = q.Where(e => e.IsSelected16 == true).ToList();
                }
                else if (code == "17")
                {
                    q = q.Where(e => e.IsSelected17 == true).ToList();
                }
                else if (code == "18")
                {
                    q = q.Where(e => e.IsSelected18 == true).ToList();
                }
                else if (code == "19")
                {
                    q = q.Where(e => e.IsSelected19 == true).ToList();
                }
                else if (code == "20")
                {
                    q = q.Where(e => e.IsSelected10 == true).ToList();
                }
                else if (code == "10")
                {
                    q = q.Where(e => e.IsSelected20 == true).ToList();
                }
                else if (code == "21")
                {
                    q = q.Where(e => e.IsSelected21 == true).ToList();
                }
                else if (code == "22")
                {
                    q = q.Where(e => e.IsSelected22 == true).ToList();
                }
                else if (code == "23")
                {
                    q = q.Where(e => e.IsSelected23 == true).ToList();
                }
                else if (code == "90")
                {
                    q = q.Where(e => e.IsSelected90 == true).ToList();
                }
            }

            DataTable tb = this.LINQToDataTable(q);

            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            List<Model.InformationProject_ConstructionStandardSelectedItem> lists = BLL.ConstructionStandardSelectedItemService.GetConstructionStandardSelectedItemsByConstructionStandardIdentifyId(this.ConstructionStandardIdentifyId);
            if (lists.Count() > 0)
            {
                foreach (var item in lists)
                {
                    ItemSelectedList.Add(item.StandardId.ToString());
                }
            }
            if (ItemSelectedList.Count > 0)
            {
                for (int j = 0; j < Grid1.Rows.Count; j++)
                {
                    if (ItemSelectedList.Contains(Grid1.DataKeys[j][0].ToString()))
                    {
                        Grid1.Rows[j].Values[0] = "True";
                    }
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.ckbAll.Checked == true)
            {
                this.BindGrid();
            }
            else
            {
                this.BindGridById(this.ConstructionStandardIdentifyId);
            }
        }

        /// <summary>
        /// 分页索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            if (this.ckbAll.Checked == true)
            {
                this.BindGrid();
            }
            else
            {
                this.BindGridById(this.ConstructionStandardIdentifyId);
            }
        }

        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            if (this.ckbAll.Checked == true)
            {
                this.BindGrid();
            }
            else
            {
                this.BindGridById(this.ConstructionStandardIdentifyId);
            }
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            if (this.ckbAll.Checked == true)
            {
                this.BindGrid();
            }
            else
            {
                this.BindGridById(this.ConstructionStandardIdentifyId);
            }
        }       
        #endregion

        #region  保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ckbAll.Checked == true)
            {
                if (ItemSelectedList.Count() == 0)
                {
                    Alert.ShowInParent("请至少选择一条记录！");
                    return;
                }
            }
            else
            {
                if (ItemSelectedList2.Count() == 0)
                {
                    Alert.ShowInParent("请至少选择一条记录！");
                    return;
                }
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ckbAll.Checked == true)
            {
                if (ItemSelectedList.Count() == 0)
                {
                    Alert.ShowInParent("请至少选择一条记录！");
                    return;
                }
            }
            else
            {
                if (ItemSelectedList2.Count() == 0)
                {
                    Alert.ShowInParent("请至少选择一条记录！");
                    return;
                }
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {

            Model.InformationProject_ConstructionStandardIdentify constructionStandardIdentify = new Model.InformationProject_ConstructionStandardIdentify
            {
                ConstructionStandardIdentifyCode = this.txtConstructionStandardIdentifyCode.Text.Trim(),
                ProjectId = this.ProjectId,
                IdentifyPerson = this.CurrUser.UserId,
                IdentifyDate = DateTime.Now.Date,
                UpdateDate = DateTime.Now,
                State = BLL.Const.State_0
            };
            if (type == BLL.Const.BtnSubmit)
                {
                    constructionStandardIdentify.State = this.ctlAuditFlow.NextStep;
                    if (this.ctlAuditFlow.NextStep == BLL.Const.State_2)
                    {
                        constructionStandardIdentify.VersionNumber = BLL.SQLHelper.RunProcNewId2("SpGetVersionNumber", "InformationProject_ConstructionStandardIdentify", "VersionNumber", this.ProjectId);
                    }
                }
                constructionStandardIdentify.Remark = this.txtRemark.Text.Trim();
                if (string.IsNullOrEmpty(ConstructionStandardIdentifyId))  //新增
                {
                    this.ConstructionStandardIdentifyId = SQLHelper.GetNewID(typeof(Model.InformationProject_ConstructionStandardIdentify));
                    constructionStandardIdentify.ConstructionStandardIdentifyId = this.ConstructionStandardIdentifyId;
                    BLL.ConstructionStandardIdentifyService.AddConstructionStandardIdentify(constructionStandardIdentify);
                    BLL.LogService.AddSys_Log(this.CurrUser, constructionStandardIdentify.ConstructionStandardIdentifyCode, constructionStandardIdentify.ConstructionStandardIdentifyId, BLL.Const.ConstructionStandardIdentifyMenuId, BLL.Const.BtnAdd);
                }
                else   //重新选择
                {
                    constructionStandardIdentify.ConstructionStandardIdentifyId = this.ConstructionStandardIdentifyId;
                    BLL.ConstructionStandardIdentifyService.UpdateConstructionStandardIdentify(constructionStandardIdentify);
                BLL.LogService.AddSys_Log(this.CurrUser, constructionStandardIdentify.ConstructionStandardIdentifyCode, constructionStandardIdentify.ConstructionStandardIdentifyId, BLL.Const.ConstructionStandardIdentifyMenuId, BLL.Const.BtnModify);
                BLL.ConstructionStandardSelectedItemService.DeleteConstructionStandardSelectedItemByConstructionStandardIdentifyId(ConstructionStandardIdentifyId);
                }
                if (this.ckbAll.Checked == true)
                {
                    foreach (string item in ItemSelectedList)
                    {
                    Model.InformationProject_ConstructionStandardSelectedItem constructionStandardSelectedItem = new Model.InformationProject_ConstructionStandardSelectedItem
                    {
                        ConstructionStandardIdentifyId = ConstructionStandardIdentifyId,
                        StandardId = item
                    };
                    BLL.ConstructionStandardSelectedItemService.AddConstructionStandardSelectedItem(constructionStandardSelectedItem);
                    }
                }
                else
                {
                    foreach (string item in ItemSelectedList2)
                    {
                    Model.InformationProject_ConstructionStandardSelectedItem constructionStandardSelectedItem = new Model.InformationProject_ConstructionStandardSelectedItem
                    {
                        ConstructionStandardIdentifyId = ConstructionStandardIdentifyId,
                        StandardId = item
                    };
                    BLL.ConstructionStandardSelectedItemService.AddConstructionStandardSelectedItem(constructionStandardSelectedItem);
                    }
                }
                ////保存流程审核数据         
                this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ConstructionStandardIdentifyMenuId, this.ConstructionStandardIdentifyId, (type == BLL.Const.BtnSubmit ? true : false), constructionStandardIdentify.ConstructionStandardIdentifyCode, "../InformationProject/ConstructionStandardIdentifyView.aspx?ConstructionStandardIdentifyId={0}");
        }
        #endregion        

        #region Grid行点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    if (!ItemSelectedList.Contains(rowID))
                    {
                        ItemSelectedList.Add(rowID);
                    }
                }
                else
                {
                    if (this.ckbAll.Checked == true)
                    {
                        if (ItemSelectedList.Contains(rowID))
                        {
                            ItemSelectedList.Remove(rowID);
                        }
                    }
                    else
                    {
                        if (ItemSelectedList2.Contains(rowID))
                        {
                            ItemSelectedList2.Remove(rowID);
                        }
                    }
                }
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ConstructionStandardIdentifyId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ConstructionStandardIdentifyAttachUrl&menuId={1}", ConstructionStandardIdentifyId, BLL.Const.ConstructionStandardIdentifyMenuId)));
        }
        #endregion

    }
}