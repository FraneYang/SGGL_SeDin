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
                txtProject.Text = model.ProjectName.ToString();
                txtUnit.Text = model.Unit.ToString();
                txtConstructionSite.Text = model.ConstructionSite.ToString();
                txtBiddingProjectScope.Text = model.BiddingProjectScope.ToString();
                txtBiddingProjectContent.Text = model.BiddingProjectContent.ToString();
                txtTimeRequirements.Text = model.TimeRequirements.ToString();
                txtQualityRequirement.Text = model.QualityRequirement.ToString();
                txtHSERequirement.Text = model.HSERequirement.ToString();
                txtTechnicalRequirement.Text = model.TechnicalRequirement.ToString();
                txtCurrentRequirement.Text = model.CurrentRequirement.ToString();
                txtSub_Selection.Text = model.Sub_Selection.ToString();
                txtBid_Selection.Text = model.Bid_Selection.ToString();
                txtContractingMode_Select.Text = model.ContractingMode_Select.ToString();
                txtPriceMode_Select.Text = model.PriceMode_Select.ToString();
                txtMaterialsDifferentiate.Text = model.MaterialsDifferentiate.ToString();
                txtImportExplain.Text = model.ImportExplain.ToString();
                txtShortNameList.Text = model.ShortNameList.ToString();
                txtEvaluationMethods.Text = model.EvaluationMethods.ToString();
                txtEvaluationPlan.Text = model.EvaluationPlan.ToString();
                txtBiddingMethods_Select.Text = model.BiddingMethods_Select.ToString();
                txtSchedulePlan.Text = model.SchedulePlan.ToString();
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
            }
            else
            {
                this.txtProjectName.Text = string.Empty;
            }
        }
        #endregion
    
    void Save()
        {


            Model.PHTGL_ActionPlanFormation model = new Model.PHTGL_ActionPlanFormation();
           // model.ActionPlanID = ActionPlanID;
            model.CreateTime = DateTime.Now;
            model.CreatUser = this.CurrUser.UserId;
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

            if (this.drpProjectId.SelectedValue != BLL.Const._Null)
            {
                model.ProjectID = this.drpProjectId.SelectedValue;
            }
            else
            {
                ShowNotify("选择项目！", MessageBoxIcon.Warning);

                return;
            }
            if (string.IsNullOrEmpty(ActionPlanID))
            {
                ActionPlanID = Guid.NewGuid().ToString();
                model.ActionPlanID = ActionPlanID;
                BLL.PHTGL_ActionPlanFormationService.AddPHTGL_ActionPlanFormation(model);
            }
            else
            {
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

    }
}