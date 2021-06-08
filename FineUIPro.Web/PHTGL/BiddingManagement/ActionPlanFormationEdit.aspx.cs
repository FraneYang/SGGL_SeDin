using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.BiddingManagement
{
    public partial class ActionPlanFormationEdit : PageBase
    {
        public string ActionPlanID
          {
            get
            {
                return (string)ViewState["ActionPlanID"];
            }
            set
            {
                ViewState["ActionPlanID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();
                ActionPlanID = Request.Params["ActionPlanID"];
                BLL.ProjectService.InitAllProjectCodeDropDownList(this.drpProjectId, true);
                this.drpProjectId.SelectedValue = this.CurrUser.LoginProjectId;
                drpProjectId_SelectedIndexChanged(null, null);

                BindGrid();
                Bind();
                var newmodel = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(ActionPlanID);
                if (newmodel!=null)
                {
                    if (newmodel.State >= Const.ContractCreat_Complete)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                    }

                }

            }
        }


        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT           ActionPlanItemID
                                              ,ActionPlanID
                                              ,PlanningContent
                                              ,ActionPlan
                                              ,Remarks "
                            + @"   FROM PHTGL_ActionPlanFormation_Sch1  where  1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(ActionPlanID))
            {
                strSql += " AND ActionPlanID = @ActionPlanID";
                listStr.Add(new SqlParameter("@ActionPlanID", ActionPlanID));
            }
            else
            {
                strSql += " AND ActionPlanID = @ActionPlanID";
                listStr.Add(new SqlParameter("@ActionPlanID", "模板"));

            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }


        private void Bind()
        {
            if (!string.IsNullOrEmpty(ActionPlanID))
            {
                var model=BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(ActionPlanID);
                txtActionPlanCode.Text = Convert.ToString(model.ActionPlanCode);
                txtProject.Text = Convert.ToString(model.ProjectName);
                txtUnit.Text = Convert.ToString(model.Unit);
                txtConstructionSite.Text = Convert.ToString(model.ConstructionSite);
                txtBiddingProjectScope.Text = Convert.ToString(model.BiddingProjectScope);
                txtBiddingProjectContent.Text = Convert.ToString(model.BiddingProjectContent);
                txtTimeRequirements.Text = Convert.ToString(model.TimeRequirements);
                txtQualityRequirement.Text = Convert.ToString(model.QualityRequirement);
                txtHSERequirement.Text = Convert.ToString(model.HSERequirement);
                txtTechnicalRequirement.Text = Convert.ToString(model.TechnicalRequirement);
                txtCurrentRequirement.Text = Convert.ToString(model.CurrentRequirement);
                txtSub_Selection.Text = Convert.ToString(model.Sub_Selection);
                txtBid_Selection.Text = Convert.ToString(model.Bid_Selection);
                txtContractingMode_Select.Text = Convert.ToString(model.ContractingMode_Select);
                txtPriceMode_Select.Text = Convert.ToString(model.PriceMode_Select);
                txtMaterialsDifferentiate.Text = Convert.ToString(model.MaterialsDifferentiate);
                txtImportExplain.Text = Convert.ToString(model.ImportExplain);
                txtShortNameList.Text = Convert.ToString(model.ShortNameList);
                txtEvaluationMethods.Text = Convert.ToString(model.EvaluationMethods);
                txtEvaluationPlan.Text = Convert.ToString(model.EvaluationPlan);
                txtBiddingMethods_Select.Text = Convert.ToString(model.BiddingMethods_Select);
                txtSchedulePlan.Text = Convert.ToString(model.SchedulePlan);
                txtBidPrice.Text =   Convert.ToString(model.BidPrice);
                txtBidProject.Text = Convert.ToString(model.BidProject);
                txtBidType.Text =    Convert.ToString(model.BidType);
                txtPriceType.Text =  Convert.ToString(model.PriceType);
 
             }

        }
        #endregion



        #region DropDownList下拉选择事件
        /// <summary>
        /// 选择项目Id获取项目名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpProjectId.SelectedValue != BLL.Const._Null)
            {
                this.txtProjectName.Text = BLL.ProjectService.GetProjectNameByProjectId(this.drpProjectId.SelectedValue);
                if (string.IsNullOrEmpty(ActionPlanID))
                {
                    this.txtActionPlanCode.Text = this.drpProjectId.SelectedText + ".000.C01.92-";

                }
            }
            else
            {
                this.txtProjectName.Text = string.Empty;
                this.txtActionPlanCode.Text = string.Empty;
            }
        }
        #endregion
    
    void Save()
        {


            Model.PHTGL_ActionPlanFormation model = new Model.PHTGL_ActionPlanFormation();
            model.CreateTime = DateTime.Now;
            model.CreatUser = this.CurrUser.UserId;
            model.State = Const.ContractCreating;
            model.ProjectID = "";
            model.ProjectName = txtProject.Text;
            model.Unit = txtUnit.Text;
            model.ConstructionSite = txtConstructionSite.Text;
            model.BiddingProjectScope = txtBiddingProjectScope.Text;
            model.BiddingProjectContent = txtBiddingProjectContent.Text;
            model.TimeRequirements = txtTimeRequirements.Text;
            model.QualityRequirement = txtQualityRequirement.Text;
            model.HSERequirement = txtHSERequirement.Text;
            model.TechnicalRequirement = txtTechnicalRequirement.Text;
            model.CurrentRequirement = txtCurrentRequirement.Text;
            model.Sub_Selection = txtSub_Selection.Text;
            model.Bid_Selection = txtBid_Selection.Text;
            model.ContractingMode_Select = txtContractingMode_Select.Text;
            model.PriceMode_Select = txtPriceMode_Select.Text;
            model.MaterialsDifferentiate = txtMaterialsDifferentiate.Text;
            model.ImportExplain = txtImportExplain.Text;
            model.ShortNameList = txtShortNameList.Text;
            model.EvaluationMethods = txtEvaluationMethods.Text;
            model.EvaluationPlan = txtEvaluationPlan.Text;
            model.BiddingMethods_Select = txtBiddingMethods_Select.Text;
            model.SchedulePlan = txtSchedulePlan.Text;
            model.BidPrice= txtBidPrice.Text;
            model.BidProject=txtBidProject.Text;
            model.BidType= txtBidType.Text;
            model.PriceType= txtPriceType.Text;

            if (this.drpProjectId.SelectedValue != BLL.Const._Null)
            {
                model.ProjectID = this.drpProjectId.SelectedValue;
            }
            else
            {
                ShowNotify("选择项目！", MessageBoxIcon.Warning);

                return;
            }
            var IsExitCodemodel = PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationByCode(this.txtActionPlanCode.Text.Trim().ToString());

            if (string.IsNullOrEmpty(ActionPlanID))
            {
                if (IsExitCodemodel != null)
                {
                    ShowNotify("编号已经重复,请修改！", MessageBoxIcon.Warning);

                    return;
                }

                ActionPlanID = Guid.NewGuid().ToString();
                model.ActionPlanID = ActionPlanID;
                model.ActionPlanCode = this.txtActionPlanCode.Text.Trim().ToString();
                BLL.PHTGL_ActionPlanFormationService.AddPHTGL_ActionPlanFormation(model);
            }
            else
            {
                if (IsExitCodemodel != null&& IsExitCodemodel.ActionPlanID!=ActionPlanID)
                {
                    ShowNotify("编号已经重复,请修改！", MessageBoxIcon.Warning);

                    return;
                }
                model.ActionPlanCode = this.txtActionPlanCode.Text.Trim().ToString();
                model.ActionPlanID = ActionPlanID;
                BLL.PHTGL_ActionPlanFormationService.UpdatePHTGL_ActionPlanFormation(model);
            }

            BLL.PHTGL_ActionPlanFormation_Sch1Service.DeletePHTGL_ActionPlanFormation_Sch1ById(ActionPlanID);
            JArray EditorArr = Grid1.GetMergedData();
            if (EditorArr.Count > 0)
            {
                Model.PHTGL_ActionPlanFormation_Sch1 model_Sch1 = null;
                for (int i = 0; i < EditorArr.Count; i++)
                {
                    JObject objects = (JObject)EditorArr[i];
                    model_Sch1 = new Model.PHTGL_ActionPlanFormation_Sch1();
                    model_Sch1.ActionPlanItemID = SQLHelper.GetNewID(typeof(Model.PHTGL_ActionPlanFormation_Sch1));
                    model_Sch1.ActionPlanID = ActionPlanID;
                    model_Sch1.PlanningContent = objects["values"]["PlanningContent"].ToString();
                    model_Sch1.ActionPlan = objects["values"]["ActionPlan"].ToString();
                    model_Sch1.Remarks = objects["values"]["Remarks"].ToString();
                    BLL.PHTGL_ActionPlanFormation_Sch1Service.AddPHTGL_ActionPlanFormation_Sch1(model_Sch1);
                }
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
          
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();
            var newmodel=  PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(ActionPlanID);
            newmodel.State = Const.ContractCreat_Complete;
            PHTGL_ActionPlanFormationService.UpdatePHTGL_ActionPlanFormation(newmodel);

            var model = PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewByActionPlanID(ActionPlanID);
            if (model==null)
            {
                Model.PHTGL_ActionPlanReview _ActionPlanReview = new Model.PHTGL_ActionPlanReview();
                _ActionPlanReview.ActionPlanReviewId = SQLHelper.GetNewID(typeof(Model.PHTGL_ActionPlanReview));
                _ActionPlanReview.ActionPlanID = ActionPlanID;
                _ActionPlanReview.State = Const.ContractCreat_Complete;
                _ActionPlanReview.CreateUser = this.CurrUser.UserId;
                BLL.PHTGL_ActionPlanReviewService.AddPHTGL_ActionPlanReview(_ActionPlanReview);
            }
           
 
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        

    }
}