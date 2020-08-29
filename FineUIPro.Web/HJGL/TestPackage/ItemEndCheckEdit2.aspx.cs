using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;
namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class ItemEndCheckEdit2 : PageBase
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string PTP_ID
        {
            get
            {
                return (string)ViewState["PTP_ID"];
            }
            set
            {
                ViewState["PTP_ID"] = value;
            }
        }
        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PTP_ID = Request.Params["PTP_ID"];
                if (!string.IsNullOrEmpty(PTP_ID))
                {
                    var getTestPakeage = TestPackageEditService.GetTestPackageByID(PTP_ID);
                    if (getTestPakeage != null)
                    {
                        this.txtTestPackageNo.Text = getTestPakeage.TestPackageNo;
                        this.txtTestPackageName.Text = getTestPakeage.TestPackageName;
                        State = getTestPakeage.State;
                    }
                    TestPackageEditService.Init(drpHandleType, State, false);
                    BindGrid(); BindGrid1();
                    if (State == Const.TestPackage_Audit1 || State==Const.TestPackage_ReAudit2)
                    {
                        this.ckA.Hidden = false;
                        this.Grid1.Columns[4].Hidden = true;
                        UserService.InitUserProjectIdUnitTypeDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_1, false);
                    }
                    if (State == Const.TestPackage_Audit2)
                    {
                        this.Grid1.Columns[3].Hidden = false;
                        this.IsAgree.Hidden = false;
                        this.Opinion.Hidden = false;
                        UserService.InitJLUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId , false);
                    }
                    if (State == Const.TestPackage_Audit3)
                    {
                        this.Opinion.Hidden = false;
                        this.drpHandleMan.Enabled = false;
                    }
                }
            }
        }

        private void BindGrid() {
            string strSql = @"  select ItemCheckId, PTP_ID, PipelineId, Content, ItemType,(case when Content='/' then '/' else Result end)AS Result from PTP_ItemEndCheck WHERE PTP_ID =@PTP_ID Order By PipelineId";
            SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@PTP_ID",this.PTP_ID),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
            if (Grid1.Rows.Count > 0)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    JObject values = mergedRow.Value<JObject>("values");
                    int i = mergedRow.Value<int>("index");
                    string Content = values.Value<string>("Content");
                    if (Content == "/")
                    {
                        AspNet.Button btnOK = (AspNet.Button)Grid1.Rows[i].FindControl("btnOK");
                        AspNet.Button btnNotOK = (AspNet.Button)Grid1.Rows[i].FindControl("btnNotOK");
                        btnOK.Visible =false;
                        btnNotOK.Visible = false;
                    }
                }
            }
        }
        //办理记录
        public void BindGrid1()
        {
            string strSql = @"select ApproveId, PTP_ID, ApproveDate, Opinion, ApproveMan, ApproveType ,U.UserName from [dbo].[PTP_TestPackageApprove] P 
                              Left Join Sys_User U on p.ApproveMan=U.UserId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where PTP_ID= @PTP_ID";
            listStr.Add(new SqlParameter("@PTP_ID", PTP_ID));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
        protected string ConvertCarryPipeline(object PipelineId)
        {
            if (PipelineId != null)
            {
                var getPipeline = BLL.PipelineService.GetPipelineByPipelineId(PipelineId.ToString());
                if (getPipeline != null)
                {
                    return getPipeline.PipelineCode;
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnSave))
            {
                SaveData(Const.BtnSave);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnSave))
            {
                SaveData(Const.BtnSubmit);
            }
        }

        private void SaveData(string saveType)
        {

            bool flag = true;
            ///保存明细
            if (saveType == Const.BtnSubmit)
            {
                State = drpHandleType.SelectedValue.Trim();
            }
            var getItemEndCheck = BLL.AItemEndCheckService.GetItemEndCheckByPTPID(this.PTP_ID);
            if (getItemEndCheck.Count > 0)
            {
                foreach (var item in getItemEndCheck)
                {
                    if (item.Result == "不合格") {
                        flag = false;
                    }
                }
                if (saveType != Const.BtnSave) {
                    if (State == Const.TestPackage_Audit2)
                    {
                        if (!this.ckIsOK.Checked)
                        {
                            Alert.ShowInTop("请勾选【A项已全部整改完毕】!", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (State == Const.TestPackage_Complete || State == Const.TestPackage_Audit3)
                    {
                        if (!flag)
                        {
                            Alert.ShowInTop("尾项中有【不合格】选项！", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.PTP_TestPackageApprove approve1 = BLL.TestPackageApproveService.GetTestPackageApproveById(this.PTP_ID);
                    if (approve1 != null && saveType == Const.BtnSubmit)
                    {
                        approve1.ApproveDate = DateTime.Now;
                        approve1.Opinion = txtOpinion.Text;
                        BLL.TestPackageApproveService.UpdateTestPackageApprove(approve1);
                    }
                    if (saveType == Const.BtnSubmit)
                    {
                        Model.PTP_TestPackageApprove approve = new Model.PTP_TestPackageApprove();
                        approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
                        if (this.drpHandleMan.SelectedValue != "0")
                        {
                            approve.ApproveMan = this.drpHandleMan.SelectedValue;
                        }
                        approve.ApproveType = this.drpHandleType.SelectedValue;
                        approve.PTP_ID = this.PTP_ID;
                        BLL.TestPackageApproveService.AddTestPackageApprove(approve);
                        var TestPackage = TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                        if (TestPackage != null)
                        {
                            TestPackage.State = this.State;
                            TestPackageEditService.UpdateTestPackage(TestPackage);
                        }
                    }
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            AspNet.Button btn = sender as AspNet.Button;
            var ItemEndCheck = BLL.AItemEndCheckService.GetAItemEndCheckByID(btn.CommandArgument);
            if (ItemEndCheck != null)
            {
                ItemEndCheck.Result = "合格";
                BLL.AItemEndCheckService.UpdateAItemEndCheck(ItemEndCheck);
            }
            BindGrid();
        }

        protected void btnNotOK_Click(object sender, EventArgs e)
        {
            AspNet.Button btn = sender as AspNet.Button;
            var ItemEndCheck = BLL.AItemEndCheckService.GetAItemEndCheckByID(btn.CommandArgument);
            if (ItemEndCheck != null)
            {
                ItemEndCheck.Result = "不合格";
                BLL.AItemEndCheckService.UpdateAItemEndCheck(ItemEndCheck);
            }
            BindGrid();
        }

        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpHandleType.Items.Clear();
            this.drpHandleMan.Items.Clear();
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                TestPackageEditService.Init(drpHandleType, State, false);
                UserService.InitJLUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
            }
            else {
                TestPackageEditService.Init(drpHandleType, "F", false);
                UserService.InitUserProjectIdUnitTypeDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, false);
            }
            this.drpHandleType.SelectedIndex = 0;
            this.drpHandleMan.SelectedIndex = 0;
        }

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpHandleMan.Items.Clear();
            if (drpHandleType.SelectedValue == BLL.Const.TestPackage_Complete)
            {
                drpHandleMan.Enabled = false;
            }
            else if (drpHandleType.SelectedValue == BLL.Const.TestPackage_Audit3) {
                drpHandleMan.Enabled = true;
                UserService.InitJLUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
            }
            this.drpHandleMan.SelectedIndex = 0;
        }
        protected string ConvertApproveType(object Type)
        {
            if (Type != null)
            {
                if (Type.ToString() == BLL.Const.TestPackage_Compile)
                {
                    return "总包专业工程师编制";
                }
                else if (Type.ToString() == Const.TestPackage_Audit1)
                {

                    return "施工分包商整改";
                } 
                else if (Type.ToString() == Const.TestPackage_Audit2)
                {
                   
                    return "总包确认";
                }
                else if (Type.ToString() == Const.TestPackage_Audit3)
                {
                    return "监理确认";
                }
                else if (Type.ToString() == Const.TestPackage_ReAudit2)
                {
                    return "施工分包商重新整改";
                }
                else if (Type.ToString() == Const.TestPackage_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}