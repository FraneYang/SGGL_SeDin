using Aspose.Words;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
    public partial class ActionPlanReview : PageBase
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
                this.DropState.DataValueField = "Value";
                DropState.DataTextField = "Text";
                DropState.DataSource = BLL.DropListService.GetState();
                DropState.DataBind();
                Funs.FineUIPleaseSelect(DropState);

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
                                          , (CASE APR.State 
                                                     WHEN  @ContractCreat_Complete THEN '编制完成'
                                                     WHEN  @ContractReviewing THEN '审批中'
                                                     WHEN  @ContractReview_Complete THEN '审批完成'
                                                     WHEN  @ContractReview_Refuse THEN '审批被拒'END) AS State
                                          ,APR.Approval_Construction
                                          ,Act.CreateTime
                                          ,U.UserName AS CreateUser "
                            + @" FROM PHTGL_ActionPlanReview  AS APR "
                            + @" LEFT JOIN Sys_User AS U ON U.UserId = APR.CreateUser  "
                            + @" LEFT JOIN PHTGL_ActionPlanFormation AS Act ON Act.ActionPlanID=APR.ActionPlanID"
                            + @" LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Act.ProjectID  WHERE 1=1";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreat_Complete",  Const.ContractCreat_Complete));
            listStr.Add(new SqlParameter("@ContractReviewing",  Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));
 
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
            if (DropState.SelectedValue != Const._Null)
            {
                strSql += " and APR.State =@State  ";
                listStr.Add(new SqlParameter("@State", DropState.SelectedValue));
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
            if (e.CommandName == "LooK")
            {
                string id = fileId;
                var Act = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(id);
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ActionPlanFormationEdit.aspx?ActionPlanID={0}", Act.ActionPlanID, "编辑 - ")));
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
            DropState.SelectedValue = "null";
            BindGrid();
        }
        protected void btnQueryApprove_Click(object sender, EventArgs e)
        {
            this.EditData();
        }
        #endregion

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
        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
             string id = Grid1.SelectedRowID;
             var actReview = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(id);
            if (actReview.State==Const.ContractReview_Refuse)
            {
                MenuButton1.Hidden = false;

            }
            else
            {
                MenuButton1.Hidden = true;

            }


        }

        /// <summary>
        /// 重新提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
             string id = Grid1.SelectedRowID;

            var actReview = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(id);
            if (actReview.State< Const.ContractReviewing)
            {
                Alert.ShowInTop("还未创建审批流无法重新提交！", MessageBoxIcon.Warning);
                return;
            }
            actReview.State = Const.ContractReviewing;

            if (actReview.CreateUser!=this.CurrUser.UserId)
            {
               string name= UserService.GetUserNameByUserId(actReview.CreateUser);
                Alert.ShowInTop("！此审批不是您创建，无法重新提交【创建者："+name+"】", MessageBoxIcon.Warning);
                return;

            }
            BLL.PHTGL_ActionPlanReviewService.UpdatePHTGL_ActionPlanReview(actReview);

            /////改变实施计划表状态
            //var model = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(actReview.ActionPlanID);
            //model.State = Const.ContractReviewing;
            //PHTGL_ActionPlanFormationService.UpdatePHTGL_ActionPlanFormation(model);

              ///删除历史审批记录
             PHTGL_ApproveService.DeletePHTGL_ApproveBycontractId(id);
             var _ActFormation = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(actReview.ActionPlanID);
              //创建第一节点审批信息
            Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
            _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
            _Approve.ContractId = actReview.ActionPlanReviewId;
            _Approve.ApproveMan = PHTGL_ActionPlanReviewService.Get_DicApproveman(_ActFormation.ProjectID, actReview.ActionPlanReviewId)[1];
            _Approve.ApproveDate = "";
            _Approve.State = 0;
            _Approve.IsAgree = 0;
            _Approve.ApproveIdea = "";
            _Approve.ApproveType = "1";
            _Approve.ApproveForm = Request.Path;

            BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
            ShowNotify("重新提交成功!", MessageBoxIcon.Success);
            BindGrid();

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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ActionPlanReviewDetail.aspx?ActionPlanReviewId={0}",id, "编辑 - ")));

        }
        #endregion

        #region 添加
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ActionPlanReviewEdit.aspx?ActionPlanReviewId={0}", id, "编辑 - ")));

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
                        var p = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(rowID);
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.CreateUser, p.ActionPlanID, BLL.Const.ActionPlanReview, BLL.Const.BtnDelete);
                            PHTGL_ApproveService.DeletePHTGL_ApproveBycontractId(rowID);
                            BLL.PHTGL_ActionPlanReviewService.DeletePHTGL_ActionPlanReviewById(rowID);
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
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ActionPlanReview);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                    btnNew.Hidden = false;
                }
                 
                if (buttonList.Contains(Const.BtnDelete))
                {
                    btnDelete.Hidden = false;
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
            if (Act==null)
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