using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class ShowEnvironmentalItem : PageBase
    {
        #region  定义项
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
        /// <summary>
        /// 环境危险源主键
        /// </summary>
        public string EnvironmentalRiskListId
        {
            get
            {
                return (string)ViewState["EnvironmentalRiskListId"];
            }
            set
            {
                ViewState["EnvironmentalRiskListId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                this.EnvironmentalRiskListId = Request.Params["EnvironmentalRiskListId"];
                this.ItemSelectedList = new List<string>();
                var riskItem = BLL.Hazard_EnvironmentalRiskItemService.GetEnvironmentalRiskItemListByRiskListId(this.EnvironmentalRiskListId);
                if (riskItem != null)
                {
                    foreach (var item in riskItem)
                    {
                        if (!string.IsNullOrEmpty(item.EnvironmentalId))
                        {
                            this.ItemSelectedList.Add(item.EnvironmentalId);
                        }
                    }
                }

                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();                        
                //BLL.ConstValue.InitConstValueDropDownList(this.drpEType, ConstValue.Group_EnvironmentalType, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpSmallType, ConstValue.Group_EnvironmentalSmallType, false);
                // 绑定表格
                this.BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    this.BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Environmental.EnvironmentalId,Environmental.EType,Environmental.ActivePoint,Environmental.EnvironmentalFactors,Environmental.AValue,Environmental.BValue,Environmental.CValue,Environmental.DValue,Environmental.EValue,Environmental.ZValue,Environmental.SmallType,Environmental.IsImportant,Environmental.Code,Environmental.ControlMeasures,Environmental.Remark"
                + @",(ISNULL(Environmental.AValue,0) + ISNULL(Environmental.BValue,0)+ ISNULL(Environmental.CValue,0)+ ISNULL(Environmental.DValue,0)+ ISNULL(Environmental.EValue,0)) AS ZValue1"
                + @",Environmental.FValue,Environmental.GValue,(ISNULL(Environmental.FValue,0) + ISNULL(Environmental.GValue,0)) AS ZValue2"
                + @" ,Sys_ConstEType.ConstText AS ETypeName,Sys_ConstESmallType.ConstText AS SmallTypeName "
                + @" FROM dbo.Technique_Environmental AS Environmental"
                + @" LEFT JOIN Sys_Const AS  Sys_ConstEType  ON Environmental.EType=Sys_ConstEType.ConstValue and Sys_ConstEType.GroupId='" + BLL.ConstValue.Group_EnvironmentalType + "'"
                + @" LEFT JOIN Sys_Const AS Sys_ConstESmallType ON Environmental.SmallType=Sys_ConstESmallType.ConstValue and Sys_ConstESmallType.GroupId='" + BLL.ConstValue.Group_EnvironmentalSmallType + "'"
                + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            //if (this.drpEType.SelectedValue != BLL.Const._Null)
            //{
            //    strSql += " AND Environmental.EType= @EType";
            //    listStr.Add(new SqlParameter("@EType", this.drpEType.SelectedValue));
            //}
            if (this.drpSmallType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Environmental.SmallType= @SmallType";
                listStr.Add(new SqlParameter("@SmallType", this.drpSmallType.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtActivePoint.Text.Trim()))
            {
                strSql += " AND Environmental.ActivePoint LIKE @ActivePoint";
                listStr.Add(new SqlParameter("@ActivePoint", "%" + this.txtActivePoint.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtEnvironmentalFactors.Text.Trim()))
            {
                strSql += " AND Environmental.EnvironmentalFactors LIKE @EnvironmentalFactors";
                listStr.Add(new SqlParameter("@EnvironmentalFactors", "%" + this.txtEnvironmentalFactors.Text.Trim() + "%"));
            }
            if (this.rblIsCompany.SelectedValue=="1")
            {
                strSql += " AND Environmental.IsCompany = 'True'";
            }
            else
            {
                strSql += " AND (Environmental.IsCompany = 'False' OR Environmental.IsCompany IS NULL)";
            }
            strSql += " order by Environmental.SmallType, Environmental.EType,Environmental.Code";

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            if (ItemSelectedList.Count > 0)
            {
                for (int j = 0; j < Grid1.Rows.Count; j++)
                {
                    if (ItemSelectedList.Contains(Grid1.DataKeys[j][0].ToString()))
                    {
                        CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                        checkField.SetCheckedState(j, true);
                        //Grid1.Rows[j].Values[0] = "True";
                    }
                }
            }
        }
        #endregion

        #region 确认按钮
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ItemSelectedList.Count == 0)
            {
                ShowNotify("请至少选择一项！", MessageBoxIcon.Warning);
                return;
            }
            
            foreach (var item in ItemSelectedList)
            {
                Model.Technique_Environmental environmental = BLL.EnvironmentalService.GetEnvironmental(item);
                if (environmental != null)
                {
                    var riskItem = BLL.Hazard_EnvironmentalRiskItemService.GetEnvironmentalRiskItemListByRiskListIdEnvironmentalId(this.EnvironmentalRiskListId, item);
                    if (riskItem == null)
                    {
                        Model.Hazard_EnvironmentalRiskItem detail = new Model.Hazard_EnvironmentalRiskItem
                        {
                            EnvironmentalRiskItemId = SQLHelper.GetNewID(typeof(Model.Hazard_EnvironmentalRiskItem)),
                            EnvironmentalRiskListId = this.EnvironmentalRiskListId,
                            EnvironmentalId = item,
                            EType = environmental.EType,
                            ActivePoint = environmental.ActivePoint,
                            EnvironmentalFactors = environmental.EnvironmentalFactors,
                            AValue = environmental.AValue,
                            BValue = environmental.BValue,
                            CValue = environmental.CValue,
                            DValue = environmental.DValue,
                            EValue = environmental.EValue,
                            FValue = environmental.FValue,
                            GValue = environmental.GValue,
                            SmallType = environmental.SmallType,
                            IsImportant = environmental.IsImportant,
                            Code = environmental.Code,
                            ControlMeasures = environmental.ControlMeasures,
                            Remark = environmental.Remark
                        };
                        BLL.Hazard_EnvironmentalRiskItemService.AddEnvironmentalRiskItem(detail);
                    }
                }
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        protected void all_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBoxField ckbIsSelected = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.DataKeys[i][0].ToString();
                bool istrue = ckbIsSelected.GetCheckedState(i);
                if (istrue)
                {
                    if (!ItemSelectedList.Contains(rowID))
                    {
                        ItemSelectedList.Add(rowID);
                    }
                }
                else
                {
                    if (ItemSelectedList.Contains(rowID))
                    {
                        ItemSelectedList.Remove(rowID);
                    }
                }
            }
        }

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
                    this.ckALL.Checked = false;
                    if (ItemSelectedList.Contains(rowID))
                    {
                        ItemSelectedList.Remove(rowID);
                    }
                }
            }
        }
        #endregion

        #region 根据表头信息过滤列表数据
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
            this.ckALL.Checked = false;
        }

        /// <summary>
        /// 分页显示条数下拉框
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 文本框查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 选择是否本公司危险源
        /// <summary>
        /// 选择是否本公司工作阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblIsCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion
    }
}