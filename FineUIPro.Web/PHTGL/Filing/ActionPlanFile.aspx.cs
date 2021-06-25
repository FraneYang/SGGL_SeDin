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
    public partial class ActionPlanFile : PageBase
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
                GetButtonPower();
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
            string strSql = @"SELECT       APR.ActionPlanReviewId
                                          ,APR.ActionPlanID
                                          ,Act.ActionPlanCode
                                          ,Pro.ProjectName
                                          ,Pro.ProjectCode
                                          ,Act.ProjectShortName as Name
                                          ,Act.EPCCode
                                          , (CASE APR.State 
                                                     WHEN  @ContractCreat_Complete THEN '编制完成'
                                                     WHEN  @ContractReviewing THEN '审批中'
                                                     WHEN  @ContractReview_Complete THEN '审批完成'
                                                     WHEN  @ContractReview_Refuse THEN '审批被拒'END) AS State
                                          ,ApproveType =stuff((select ','+ ApproveType  from PHTGL_Approve app2 where app2.ContractId = APR.ActionPlanReviewId and app2 .state =0    for xml path('')), 1, 1, '')
                                          ,APR.Approval_Construction
                                          ,Act.CreateTime
                                          ,U.UserName AS CreateUser "
                                + @" FROM  PHTGL_ActionPlanReview  AS APR "
                                + @" LEFT JOIN Sys_User AS U ON U.UserId = APR.CreateUser  "
                                + @" LEFT JOIN PHTGL_ActionPlanFormation AS Act ON Act.ActionPlanID=APR.ActionPlanID"
                                + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Act.ProjectID  WHERE 1=1";


            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));
            listStr.Add(new SqlParameter("@State", Const.ContractReview_Complete));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and  Act.ProjectID =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(txtActionPlanCode.Text))
            {
                strSql += " and Act.ActionPlanCode like @ActionPlanCode ";
                listStr.Add(new SqlParameter("@ActionPlanCode", "%" + txtActionPlanCode.Text + "%"));

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
                ActionPlanFormation actionPlanFormation = new ActionPlanFormation();
                var Act = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(fileId);
                actionPlanFormation.Print(Act.ActionPlanID);
                return;
            }
            if (e.CommandName == "download")
            {
                 PageContext.RegisterStartupScript(Windowtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanAttachUrl&menuId={1}", fileId, BLL.Const.ActionPlanReview)));
                 return;
            }
        }
        #endregion

     

        #region 查询 重置
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
            txtActionPlanCode.Text = "";
             BindGrid();
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ActionPlanReview);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                 }

                if (buttonList.Contains(Const.BtnDelete))
                {
                 }
            }
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
            var Act = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(Id);
            if (Act == null)
            {
                Alert.ShowInTop("还未创建审批流无法导出！", MessageBoxIcon.Warning);
                return;
            }
            string ActionPlanID = Act.ActionPlanID;
            ActionPlanFormation actionPlanFormation = new ActionPlanFormation();
            actionPlanFormation.Print(ActionPlanID);
        }
        #endregion


    }
}