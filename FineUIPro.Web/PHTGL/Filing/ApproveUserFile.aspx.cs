using Aspose.Words;
using BLL;
using FineUIPro.Web.PHTGL.BiddingManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.PHTGL.Filing
{
    public partial class ApproveUserFile : PageBase
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

        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT   Bid.ApproveUserReviewID
                                      ,BidDoc.BidDocumentsCode
                                      ,Acp.ActionPlanCode
                                      ,Acp.ProjectShortName
                                      ,Acp.EPCCode
                                      ,Bid.ProjectId
	                                  ,Pro.ProjectName
                                      ,Pro.ProjectCode
                                      ,Bid.BidProject
	                                  ,U.UserName AS CreateUser
                                      ,(CASE Bid.State 
                                        WHEN @ContractCreating THEN '编制中'
                                        WHEN @ContractCreat_Complete THEN '编制完成'
                                        WHEN @ContractReviewing THEN '审批中'
                                        WHEN @ContractReview_Complete THEN '审批成功'
                                        WHEN @ContractReview_Refuse THEN '审批被拒'END) AS State
                                       ,ApproveType =stuff((select ','+ ApproveType  from PHTGL_Approve app2 where app2.ContractId = Bid.ApproveUserReviewID and app2 .state =0    for xml path('')), 1, 1, '')
                                       ,Bid.ConstructionManager
                                      ,Bid.ProjectManager
                                      ,Bid.Approval_Construction
                                      ,Bid.DeputyGeneralManager"
                              + @" from  PHTGL_BidApproveUserReview AS Bid "
                              + @" LEFT JOIN PHTGL_BidDocumentsReview AS BidDoc ON BidDoc.BidDocumentsReviewId =Bid.BidDocumentsReviewId"
                              + @" LEFT JOIN PHTGL_ActionPlanFormation AS Acp ON Acp.ActionPlanID =Bid.ActionPlanID"
                              + @" LEFT JOIN Sys_User AS U ON U.UserId =Bid.CreateUser  "
                              + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Bid.ProjectId WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString()));
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));
            listStr.Add(new SqlParameter("@State", Const.ContractReview_Complete));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and  Bid.ProjectID =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(txtBidDocumentsCode.Text))
            {
                strSql += " and BidDoc.BidDocumentsCode like @BidDocumentsCode ";
                listStr.Add(new SqlParameter("@BidDocumentsCode", "%" + txtBidDocumentsCode.Text + "%"));

            }
            
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

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
                ApproveUserReview approveUserReview = new ApproveUserReview();
                approveUserReview.Print(fileId);
                return;
            }
            if (e.CommandName == "download")
            {
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ApproveUserAttachUrl&menuId={1}", fileId, BLL.Const.ApproveUserReview)));
                return;
            }

        }

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

        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtBidDocumentsCode.Text = "";
             BindGrid();
        }
        #endregion


    }
}