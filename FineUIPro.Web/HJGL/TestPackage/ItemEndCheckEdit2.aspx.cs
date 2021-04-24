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
        /// 记录主键
        /// </summary>
        public string ItemEndCheckListId
        {
            get
            {
                return (string)ViewState["ItemEndCheckListId"];
            }
            set
            {
                ViewState["ItemEndCheckListId"] = value;
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
                ItemEndCheckListId = Request.Params["ItemEndCheckListId"];
                if (!string.IsNullOrEmpty(PTP_ID))
                {
                    var getTestPakeage = TestPackageEditService.GetTestPackageByID(PTP_ID);
                    if (getTestPakeage != null)
                    {
                        this.txtTestPackageNo.Text = getTestPakeage.TestPackageNo;
                        this.txtTestPackageName.Text = getTestPakeage.TestPackageName;
                    }
                    var itemEndCheckList = ItemEndCheckListService.GetItemEndCheckListByID(ItemEndCheckListId);
                    if (itemEndCheckList != null)
                    {
                        State = itemEndCheckList.State;
                        if (itemEndCheckList.AIsOK == true)
                        {
                            this.ckAIsOK.Checked = true;
                        }
                        if (itemEndCheckList.BIsOK == true)
                        {
                            this.ckBIsOK.Checked = true;
                        }
                    }
                    TestPackageEditService.Init(drpHandleType, State, false);
                    BindGrid(); BindGrid1();
                    if (State == Const.TestPackage_Audit1 || State == Const.TestPackage_ReAudit2)
                    {
                        //this.ckA.Hidden = false;
                        this.Grid1.Columns[4].Hidden = true;
                        UserService.InitUserProjectIdUnitTypeDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_1, false);
                    }
                    if (State == Const.TestPackage_Audit2)
                    {
                        this.Grid1.Columns[3].Hidden = false;
                        this.IsAgree.Hidden = false;
                        this.Opinion.Hidden = false;
                        UserService.InitJLUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
                        for (int i = 0; i < this.Grid1.Rows.Count; i++)
                        {
                            string itemCheckId = this.Grid1.Rows[i].DataKeys[1].ToString();
                            Model.PTP_ItemEndCheck itemCheck = BLL.AItemEndCheckService.GetAItemEndCheckByID(itemCheckId);
                            if (itemEndCheckList.BIsOK != true)   //未勾选B项全部整改完成，则B项内容不能操作是否合格
                            {
                                if (itemCheck.ItemType == "B")
                                {
                                    AspNet.Button btnOK = this.Grid1.Rows[i].FindControl("btnOK") as AspNet.Button;
                                    AspNet.Button btnNotOK = this.Grid1.Rows[i].FindControl("btnNotOK") as AspNet.Button;
                                    btnOK.Visible = false;
                                    btnNotOK.Visible = false;
                                }
                            }
                        }
                    }
                    if (State == Const.TestPackage_Audit3)
                    {
                        this.Opinion.Hidden = false;
                        this.drpHandleMan.Enabled = false;
                    }
                }
            }
        }

        private void BindGrid()
        {
            string strSql = @"  select ItemCheckId, ItemEndCheckListId, PipelineId, Content,Remark, ItemType,(case when Content='/' then '/' else Result end)AS Result from PTP_ItemEndCheck WHERE ItemEndCheckListId =@ItemEndCheckListId Order By PipelineId";
            SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@ItemEndCheckListId",this.ItemEndCheckListId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
            if (Grid1.Rows.Count > 0)
            {
                var itemEndCheckList = ItemEndCheckListService.GetItemEndCheckListByID(ItemEndCheckListId);
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    JObject values = mergedRow.Value<JObject>("values");
                    int i = mergedRow.Value<int>("index");
                    string Content = values.Value<string>("Content");
                    if (Content == "/")
                    {
                        AspNet.Button btnOK = (AspNet.Button)Grid1.Rows[i].FindControl("btnOK");
                        AspNet.Button btnNotOK = (AspNet.Button)Grid1.Rows[i].FindControl("btnNotOK");
                        btnOK.Visible = false;
                        btnNotOK.Visible = false;
                    }
                    string itemCheckId = this.Grid1.Rows[i].DataKeys[1].ToString();
                    Model.PTP_ItemEndCheck itemCheck = BLL.AItemEndCheckService.GetAItemEndCheckByID(itemCheckId);
                    if (itemEndCheckList.BIsOK != true)   //未勾选B项全部整改完成，则B项内容不能操作是否合格
                    {
                        if (itemCheck.ItemType == "B")
                        {
                            AspNet.Button btnOK = this.Grid1.Rows[i].FindControl("btnOK") as AspNet.Button;
                            AspNet.Button btnNotOK = this.Grid1.Rows[i].FindControl("btnNotOK") as AspNet.Button;
                            btnOK.Visible = false;
                            btnNotOK.Visible = false;
                        }
                    }
                }
            }
        }
        //办理记录
        public void BindGrid1()
        {
            string strSql = @"select ApproveId, ItemEndCheckListId, ApproveDate, Opinion, ApproveMan, ApproveType ,U.UserName from [dbo].[PTP_TestPackageApprove] P 
                              Left Join Sys_User U on p.ApproveMan=U.UserId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where ItemEndCheckListId= @ItemEndCheckListId";
            listStr.Add(new SqlParameter("@ItemEndCheckListId", ItemEndCheckListId));
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
            var getItemEndCheck = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(this.ItemEndCheckListId);
            if (getItemEndCheck.Count > 0)
            {
                foreach (var item in getItemEndCheck)
                {
                    if (item.ItemType == "A" && item.Result != "合格")
                    {
                        flag = false;
                    }
                }
                if (saveType != Const.BtnSave)
                {
                    if (State == Const.TestPackage_Audit2)
                    {
                        if (!this.ckAIsOK.Checked)
                        {
                            Alert.ShowInTop("请勾选【A项已全部整改完毕】!", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (State == Const.TestPackage_Complete || State == Const.TestPackage_Audit3)
                    {
                        if (!flag)
                        {
                            Alert.ShowInTop("A项尾项尚未全部合格，请打回施工单位重新整改！", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Model.PTP_TestPackageApprove approve1 = BLL.TestPackageApproveService.GetTestPackageApproveById(this.ItemEndCheckListId);
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
                        approve.ItemEndCheckListId = this.ItemEndCheckListId;
                        BLL.TestPackageApproveService.AddTestPackageApprove(approve);
                        var ItemEndCheckList = ItemEndCheckListService.GetItemEndCheckListByID(this.ItemEndCheckListId);
                        if (ItemEndCheckList != null)
                        {
                            if (flag)
                            {
                                ItemEndCheckList.AOKState = true;
                            }
                            else
                            {
                                ItemEndCheckList.AOKState = null;
                            }
                            if (State == Const.TestPackage_Complete)
                            {
                                bool b = true;   //B项是否全部整改完成
                                var BItems = getItemEndCheck.Where(x => x.ItemType == "B");
                                foreach (var item in BItems)
                                {
                                    if (item.Result != "合格")
                                    {
                                        b = false;
                                    }
                                }
                                if (b)
                                {
                                    ItemEndCheckList.State = this.State;
                                }
                                else
                                {
                                    ItemEndCheckList.State = BLL.Const.TestPackage_ReAudit2;
                                    var approve2 = Funs.DB.PTP_TestPackageApprove.FirstOrDefault(x=>x.ItemEndCheckListId== this.ItemEndCheckListId && x.ApproveType== BLL.Const.TestPackage_Audit1);
                                    Model.PTP_TestPackageApprove approveR = new Model.PTP_TestPackageApprove();
                                    approveR.ApproveId = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
                                    if (approve2!=null)
                                    {
                                        approveR.ApproveMan = approve2.ApproveMan;
                                    }
                                    approveR.ApproveType = BLL.Const.TestPackage_ReAudit2;
                                    approveR.ItemEndCheckListId = this.ItemEndCheckListId;
                                    BLL.TestPackageApproveService.AddTestPackageApprove(approveR);
                                    BLL.TestPackageApproveService.DeleteAllTestPackageApproveByApproveID(approve.ApproveId);
                                }
                            }
                            else
                            {
                                ItemEndCheckList.State = this.State;
                            }
                            ItemEndCheckList.AIsOK = this.ckAIsOK.Checked;
                            ItemEndCheckList.BIsOK = this.ckBIsOK.Checked;
                            ItemEndCheckListService.UpdateItemEndCheckList(ItemEndCheckList);
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
            string state = BLL.ItemEndCheckListService.GetItemEndCheckListByID(this.ItemEndCheckListId).State;
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                TestPackageEditService.Init(drpHandleType, state, false);
                UserService.InitJLUserDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, false);
            }
            else
            {
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
            else if (drpHandleType.SelectedValue == BLL.Const.TestPackage_Audit3)
            {
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
                    return "施工分包商继续整改";
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