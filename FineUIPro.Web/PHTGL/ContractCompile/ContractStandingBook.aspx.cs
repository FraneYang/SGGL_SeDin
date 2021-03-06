﻿using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractStandingBook : PageBase
    {
        #region 项目主键
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
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
        #endregion
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
               // btnNew.OnClientClick = Window1.GetShowReference("ContractFormationEdit.aspx", "基本信息") + "return false;";
                GetButtonPower();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                //this.drpStates.DataValueField = "Value";
                //drpStates.DataTextField = "Text";
                //drpStates.DataSource = BLL.DropListService.GetState();
                //drpStates.DataBind();
                //Funs.FineUIPleaseSelect(drpStates);

                BindGrid();

            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
             string strSql = @" select 
                                    con.ContractId,
                                    SUBSTRING(Con.ContractNum, 1, CHARINDEX('.', Con.ContractNum) - 1) as ProjectCode,
                                    Pro.ShortName,
                                    Con.ContractName,
                                    Con.ContractNum,
                                    Sub.SubConstruction,
                                    Con.Currency,
                                    Con.ContractAmount,
                                    Con.EPCCode,
                                    con.ProjectShortName,
                                   (convert(varchar(20), Sub.SignedYear) + '年' + convert(varchar(20), Sub.SignedMonth) + '月') as DepartName,
                                    Sub.Bank1,
                                    Sub.SubcontractPriceForm,
                                    Sub.Account1"
                            + @"  from PHTGL_Contract as Con "
                            + @"  left join PHTGL_SubcontractAgreement as Sub on Sub.ContractId=Con.ContractId "
                            + @"  left join Base_Project as Pro on con.ProjectId=Pro.ProjectId "
                            + @"  WHERE 1=1   and  Con.ApproveState=@ContractReview_Complete";
            List<SqlParameter> listStr = new List<SqlParameter>();
 
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
 
            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and Con.ProjectId =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtProjectCode.Text.Trim()))
            {
                strSql += " AND Pro.ProjectCode LIKE @ProjectCode";
                listStr.Add(new SqlParameter("@ProjectCode", "%" + this.txtProjectCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtShortName.Text.Trim()))
            {
                strSql += " AND Pro.ShortName LIKE @ShortName";
                listStr.Add(new SqlParameter("@ShortName", "%" + this.txtShortName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtContractName.Text.Trim()))
            {
                strSql += " AND Con.ContractName LIKE @ContractName";
                listStr.Add(new SqlParameter("@ContractName", "%" + this.txtContractName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtContractNum.Text.Trim()))
            {
                strSql += " AND Con.ContractNum LIKE @ContractNum";
                listStr.Add(new SqlParameter("@ContractNum", "%" + this.txtContractNum.Text.Trim() + "%"));
            }
            //if (!string.IsNullOrEmpty(this.txtProjectCode2.Text.Trim()))
            //{
            //    strSql += " AND Pro.ProjectCode LIKE @ProjectCode";
            //    listStr.Add(new SqlParameter("@ProjectCode", "%" + this.txtContractName.Text.Trim() + "%"));
            //}
            if (!string.IsNullOrEmpty(this.txtSubConstruction.Text.Trim()))
            {
                strSql += " AND Sub.SubConstruction LIKE @SubConstruction";
                listStr.Add(new SqlParameter("@SubConstruction", "%" + this.txtSubConstruction.Text.Trim() + "%"));
            }
            //if (drpStates.SelectedValue != Const._Null)
            //{
            //    strSql += " AND Con.ApproveState LIKE @ApproveState";
            //    listStr.Add(new SqlParameter("@ApproveState", drpStates.SelectedValue));

            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
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
            BindGrid();
        }
        #endregion

        #region 关闭弹出窗体
        /// <summary>
        /// 关闭弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtProjectCode.Text = string.Empty;
            txtShortName.Text = string.Empty;
            txtContractNum.Text = string.Empty;
            txtContractName.Text = string.Empty;
            txtSubConstruction.Text = string.Empty;
        }
         
        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
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
            string id = Grid1.SelectedRowID;
            var contract = BLL.ContractService.GetContractById(id);
            Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(id);
            _Contract.ApproveState = 0;
            ContractService.UpdateContract(_Contract);
            if (contract != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ContractFormationEdit.aspx?ContractId={0}", id, "编辑 - ")));
            }
        }
        #endregion 

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var p = BLL.ContractService.GetContractById(rowID);
                        var Sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(rowID);
                        var Spe = PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsByContractId(rowID);
                        if (Sub != null)
                        {
                            SubcontractAgreementService.DeleteSubcontractAgreementBycontractId(rowID);
                        }
                        if (Spe != null)
                        {
                            PHTGL_SpecialTermsConditionsService.DeleteSpecialTermsConditionsBycontractId(rowID);
                        }
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.ContractName, p.ContractId, BLL.Const.ContractMenuId, BLL.Const.BtnDelete);
                            BLL.ContractService.DeleteContractById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ContractFormation);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    //btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                //    btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                 //   btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion        
        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("施工分包合同管理台账" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //this.Grid1.PageSize = this.;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

//#pragma warning disable CS0108 // “PersonList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
//        /// <summary>
//        /// 导出方法
//        /// </summary>
//        /// <param name="grid"></param>
//        /// <returns></returns>
//        private string GetGridTableHtml(Grid grid)
//#pragma warning restore CS0108 // “PersonList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
//            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
//            sb.Append("<tr>");
//            foreach (GridColumn column in grid.Columns)
//            {
//                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
//            }
//            sb.Append("</tr>");
//            foreach (GridRow row in grid.Rows)
//            {
//                sb.Append("<tr>");
//                foreach (GridColumn column in grid.Columns)
//                {
//                    string html = row.Values[column.ColumnIndex].ToString();
//                    if (column.ColumnID == "tfNumber")
//                    {
//                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
//                    }
//                    if (column.ColumnID == "tfI")
//                    {
//                        html = (row.FindControl("lbI") as AspNet.Label).Text;
//                    }
//                    //sb.AppendFormat("<td>{0}</td>", html);
//                    sb.AppendFormat("<td style='vnd.ms-excel.numberformat:@;width:140px;'>{0}</td>", html);
//                }

//                sb.Append("</tr>");
//            }

//            sb.Append("</table>");

//            return sb.ToString();
//        }
        #endregion
    }
}