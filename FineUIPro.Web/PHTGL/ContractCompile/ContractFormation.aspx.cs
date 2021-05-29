using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractFormation : PageBase
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
                btnNew.OnClientClick = Window1.GetShowReference("ContractFormationEdit.aspx", "基本信息") + "return false;";
                GetButtonPower();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                this.drpStates.DataValueField = "Value";
                drpStates.DataTextField = "Text";
                drpStates.DataSource = BLL.DropListService.GetState();
                drpStates.DataBind();
                Funs.FineUIPleaseSelect(drpStates);
               
                BindGrid();
   
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Con.ContractId, 
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
                                     WHEN '3' THEN '上官红劳务分包合同'
                                     WHEN '4' THEN '试车服务合同'
                                     WHEN '5' THEN '租赁合同' END) AS ContractType,
                                    (CASE Con.ApproveState 
                                      WHEN  @ContractCreating         THEN '编制中'
                                      WHEN  @ContractCreat_Complete   THEN '编制完成' 
                                      WHEN  @Contract_countersign     THEN '会签中'
                                      WHEN  @ContractReviewing        THEN '审批中'
                                      WHEN  @ContractReview_Complete  THEN '审批成功' 
                                      WHEN  @ContractReview_Refuse    THEN '审批被拒'END) AS ApproveState,
                                    Con.Remarks,
                                    Pro.ProjectCode,
                                    Pro.ProjectName,
                                    Dep.DepartName,
                                    U.UserName AS AgentName"
                            + @"  FROM PHTGL_Contract AS Con"
                            + @"  LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Con.ProjectId"
                            + @"  LEFT JOIN Base_Depart AS Dep ON Dep.DepartId = Con.DepartId"
                            + @"  LEFT JOIN Sys_User AS U ON U.UserId = Con.Agent WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString ()));
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete));
            listStr.Add(new SqlParameter("@Contract_countersign", Const.Contract_countersign));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));

            if (!(this.CurrUser.UserId==Const.sysglyId))
            {
                strSql += " and Con.ProjectId =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtContractName.Text.Trim()))
            {
                strSql += " AND Con.ContractName LIKE @ContractName";
                listStr.Add(new SqlParameter("@ContractName", "%" + this.txtContractName.Text.Trim() + "%"));
            }
            if (drpStates.SelectedValue!=Const._Null)
            {
                strSql += " AND Con.ApproveState LIKE @ApproveState";
                listStr.Add(new SqlParameter("@ApproveState", drpStates.SelectedValue));

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
                        if (Sub!=null)
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
                    btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnDelete))
                {
                    btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion        
    }
}