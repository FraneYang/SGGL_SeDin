using BLL;
using FineUIPro.Web.PHTGL.ContractCompile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.PHTGL.Filing
{
    public partial class ContractFile : PageBase
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                 BindGrid();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Rev.ContractReviewId, 
                                    Con.ContractId, 
                                    Con.ProjectId, 
                                    Con.ContractName, 
                                    Con.ContractNum, 
                                    Con.Parties, 
                                    Con.Currency, 
                                    Con.ContractAmount, 
                                    Con.DepartId, 
                                    Con.Agent, 
                                    (CASE Con.ContractType WHEN '1' THEN '施工总承包分包合同'
                                     WHEN '2' THEN '施工专业分包合同'
                                     WHEN '3' THEN '施工劳务分包合同'
                                     WHEN '4' THEN '试车服务合同'
                                     WHEN '5' THEN 'ds' END) AS ContractType,
                                    ( CASE Rev.State
                                    WHEN  @ContractCreating     THEN '编制中'
                                     WHEN  @Contract_countersign     THEN '会签中'
                                    WHEN  @ContractReviewing        THEN '审批中'
                                    WHEN  @ContractReview_Complete  THEN '审批成功'
                                    WHEN  @ContractReview_Refuse    THEN '审批被拒'   END) AS State ,
                                      Con.Remarks,
                                    Con.EPCCode,
                                    Act.ProjectShortName,
                                    Pro.ProjectCode,
                                    Pro.ProjectName,
                                    Dep.DepartName,
                                    U.UserName AS AgentName"
                            + @" from PHTGL_ContractReview AS Rev"
                            + @"  LEFT JOIN PHTGL_Contract AS Con  ON Con.ContractId=Rev.ContractId"
                            + @"  left join PHTGL_ActionPlanFormation as Act on Act.EPCCode=Con.EPCCode"
                            + @"  LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Con.ProjectId"
                            + @"  LEFT JOIN Base_Depart AS Dep ON Dep.DepartId = Con.DepartId"
                            + @"  LEFT JOIN Sys_User AS U ON U.UserId = Con.Agent WHERE 1=1  and Rev.State=@State";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString()));
            listStr.Add(new SqlParameter("@Contract_countersign", Const.Contract_countersign));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));
            listStr.Add(new SqlParameter("@State", Const.ContractReview_Complete));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and Con.ProjectId =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtContractName.Text.Trim()))
            {
                strSql += " AND Con.ContractName LIKE @ContractName";
                listStr.Add(new SqlParameter("@ContractName", "%" + this.txtContractName.Text.Trim() + "%"));
            }
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            string fileId = string.Empty;
            if (keys == null)
            {
                return;
            }
            else
            {
                fileId = keys[0].ToString();
            }
            if (e.CommandName == "export")
            {
                ContractReview contractReview = new ContractReview();

                return;
            }
            if (e.CommandName == "download")
            {
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContractAttachUrl&menuId={1}", fileId, BLL.Const.ContractReview)));
                return;
            }
        }

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

        #region 打印
        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            ContractReview contractReview = new ContractReview();
            contractReview.Print(Id);

        }

        protected void btnPrinterWord_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var ContractId = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(Id).ContractId;
             var Con = BLL.ContractService.GetContractById(ContractId);
            if (Con.IsUseStandardtxt == 2)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContractAttachUrl&menuId={1}", ContractId, BLL.Const.ContractFormation)));
            }
            else
            {
                ContractReview contractReview = new ContractReview();
                contractReview.printContractAgreement(ContractId);
            }

           
        }
        #endregion
    }
}