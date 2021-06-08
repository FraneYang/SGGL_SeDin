using Aspose.Words;
using BLL;
using FineUIPro.Web.PHTGL.BiddingManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.PHTGL.Filing
{
    public partial class SetSubFile : PageBase
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
               

                this.DropType.DataValueField = "Value";
                DropType.DataTextField = "Text";
                DropType.DataSource = BLL.PHTGL_SetSubReviewService.GetCreateType();
                DropType.DataBind();
                Funs.FineUIPleaseSelect(DropType);
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
            string strSql = @"  select Sub.SetSubReviewID
                                      ,Sub.SetSubReviewCode
                                      ,BidDoc.BidDocumentsCode
                                      ,BidDoc.BidContent
                                      ,BidDoc.Bidding_StartTime
                                      ,(CASE Sub.State 
                                        WHEN @ContractCreating THEN '编制中'
                                        WHEN @ContractReviewing THEN '审批中'
                                        WHEN @ContractReview_Complete THEN '审批成功'
                                        WHEN @ContractReview_Refuse THEN '审批被拒'END) AS State
                                     ,(Case Sub.Type  
				                          WHEN @Type_MinPrice THEN '用于经评审的最低投标报价法'
                                          WHEN @Type_ConEvaluation THEN '综合评估法' END) AS Type
                                       ,U.UserName AS CreateUser
                                       ,Pro.ProjectName
                                       ,Pro.ProjectCode"
                               + @"  from  PHTGL_SetSubReview as  Sub "
                               + @" LEFT JOIN  PHTGL_BidApproveUserReview as BidUser on BidUser.ApproveUserReviewID = Sub.ApproveUserReviewID   "
                               + @" LEFT JOIN PHTGL_BidDocumentsReview as BidDoc  on BidDoc.BidDocumentsReviewId = BidUser.BidDocumentsReviewId "
                               + @" LEFT JOIN Sys_User AS U ON U.UserId = Sub.CreateUser    "
                               + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = BidUser.ProjectId WHERE 1 = 1  and Sub.State =@State  ";

            List<SqlParameter> listStr = new List<SqlParameter>();

            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString()));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));
            listStr.Add(new SqlParameter("@Type_MinPrice", BLL.PHTGL_SetSubReviewService.Type_MinPrice));
            listStr.Add(new SqlParameter("@Type_ConEvaluation", PHTGL_SetSubReviewService.Type_ConEvaluation));
            listStr.Add(new SqlParameter("@State", Const.ContractReview_Complete));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and  BidUser.ProjectId  =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(txtSetSubReviewCode.Text))
            {
                strSql += " and  Sub.SetSubReviewCode like @SetSubReviewCode";
                listStr.Add(new SqlParameter("@SetSubReviewCode", "%" + txtSetSubReviewCode.Text + "%"));

            }
            
            if (DropType.SelectedValue != Const._Null)
            {
                strSql += " and  Sub.Type =@Type";
                listStr.Add(new SqlParameter("@Type", DropType.SelectedValue));
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
                SetSubReview setSubReview = new SetSubReview();
                setSubReview.Print(fileId);
                return;
            }
            if (e.CommandName == "download")
            {
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SetSubAttachUrl&menuId={1}", fileId, BLL.Const.SetSubReview)));
                return;
            }

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
        
        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtSetSubReviewCode.Text = "";
             DropType.SelectedValue = "null";
            BindGrid();
        }
        #endregion
 
  
    }
}