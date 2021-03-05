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
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class ItemEndCheckEdit : PageBase
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
        /// <summary>
        /// 明细集合
        /// </summary>
        private List<Model.PTP_ItemEndCheck> ItemEndCheckLists = new List<Model.PTP_ItemEndCheck>();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PTP_ID = Request.Params["PTP_ID"];
                ItemEndCheckListId= Request.Params["ItemEndCheckListId"];
                if (!string.IsNullOrEmpty(PTP_ID))
                {
                    var getTestPakeage = TestPackageEditService.GetTestPackageByID(PTP_ID);
                    if (getTestPakeage != null)
                    {
                        this.txtTestPackageNo.Text = getTestPakeage.TestPackageNo;
                        this.txtTestPackageName.Text = getTestPakeage.TestPackageName;
                    }
                    var getItemEndCheck = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(this.ItemEndCheckListId);
                    if (getItemEndCheck.Count == 0)
                    {
                        var getPipeLineList = TestPackageEditService.GetPipeLineListByPTP_ID(PTP_ID);
                        foreach (var TestPackage in getPipeLineList)
                        {
                            Model.PTP_ItemEndCheck newPipelineList = new Model.PTP_ItemEndCheck();
                            newPipelineList.ItemCheckId = SQLHelper.GetNewID(typeof(Model.PTP_ItemEndCheck));
                            newPipelineList.ItemEndCheckListId = this.ItemEndCheckListId;
                            newPipelineList.PipelineId = TestPackage.PipelineId;
                            ItemEndCheckLists.Add(newPipelineList);
                        }
                    }
                    foreach (var item in getItemEndCheck)
                    {
                        Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                        newItemEndCheck.ItemCheckId = SQLHelper.GetNewID(typeof(Model.PTP_ItemEndCheck));
                        newItemEndCheck.ItemEndCheckListId = this.ItemEndCheckListId;
                        newItemEndCheck.PipelineId = item.PipelineId;
                        newItemEndCheck.Content = item.Content;
                        newItemEndCheck.ItemType = item.ItemType;
                        newItemEndCheck.Remark = item.Remark;
                        ItemEndCheckLists.Add(newItemEndCheck);
                    }
                    ItemEndCheckLists = ItemEndCheckLists.OrderBy(x => x.PipelineId).ToList();
                    this.Grid1.DataSource = ItemEndCheckLists;
                    this.Grid1.DataBind();
                    if (Grid1.Rows.Count > 0)
                    {
                        foreach (JObject mergedRow in Grid1.GetMergedData())
                        {
                            int i = mergedRow.Value<int>("index");
                            GridRow row = Grid1.Rows[i];
                            AspNet.DropDownList drpItemType = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpItemType");
                            AspNet.HiddenField ItemType = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdItemType");
                            if (!string.IsNullOrEmpty(ItemType.Value))
                            {
                                if (ItemType.Value == "/")
                                {
                                    drpItemType.Text = "/";
                                }
                                else
                                {
                                    drpItemType.SelectedValue = ItemType.Value;
                                }
                            }
                        }
                    }
                    State = "1";
                    TestPackageEditService.Init(drpHandleType, State, false);
                    UserService.InitUserProjectIdUnitTypeDropDownList(drpHandleMan, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, false);
                    if (!string.IsNullOrEmpty(ItemEndCheckListId))
                    {
                        BindGrid1();
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string id = this.Grid1.SelectedRow.RowID;
            ItemEndCheckLists = GetDetails();
            if (e.CommandName == "add")//增加
            {
                Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                newItemEndCheck.ItemCheckId = SQLHelper.GetNewID(typeof(Model.PTP_ItemEndCheck));
                newItemEndCheck.ItemEndCheckListId = this.ItemEndCheckListId;
                newItemEndCheck.PipelineId = this.Grid1.SelectedRow.DataKeys[0].ToString();
                ItemEndCheckLists.Add(newItemEndCheck);
                ItemEndCheckLists = ItemEndCheckLists.OrderBy(x => x.PipelineId).ToList();
                this.Grid1.DataSource = ItemEndCheckLists;
                this.Grid1.DataBind();
                if (Grid1.Rows.Count > 0)
                {
                    foreach (JObject mergedRow in Grid1.GetMergedData())
                    {
                        int i = mergedRow.Value<int>("index");
                        GridRow row = Grid1.Rows[i];
                        AspNet.DropDownList drpItemType = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpItemType");
                        AspNet.HiddenField ItemType = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdItemType");
                        if (!string.IsNullOrEmpty(ItemType.Value))
                        {
                            drpItemType.SelectedValue = ItemType.Value;
                        }
                    }
                }
            }
            if (e.CommandName == "del")//删除
            {
                var itemCheck = ItemEndCheckLists.FirstOrDefault(x => x.ItemCheckId == id);
                if (itemCheck != null)
                {
                    var thisPipelineItems = ItemEndCheckLists.Where(x=>x.PipelineId== itemCheck.PipelineId);
                    if (thisPipelineItems.Count() > 1)   //当前管线记录大于1时可以删除
                    {
                        ItemEndCheckLists.Remove(itemCheck);
                        var olditemCheck = BLL.AItemEndCheckService.GetAItemEndCheckByID(id);
                        if (olditemCheck != null)
                        {
                            AItemEndCheckService.DeleteAItemEndCheckByID(id);
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("管线信息无法删除！", MessageBoxIcon.Warning);
                    }
                }
                ItemEndCheckLists = ItemEndCheckLists.OrderBy(x => x.PipelineId).ToList();
                this.Grid1.DataSource = ItemEndCheckLists;
                this.Grid1.DataBind();
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    GridRow row = Grid1.Rows[i];
                    AspNet.DropDownList drpItemType = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpItemType");
                    AspNet.HiddenField ItemType = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdItemType");
                    if (!string.IsNullOrEmpty(ItemType.Value))
                    {
                        drpItemType.SelectedValue = ItemType.Value;
                    }
                }
            }
        }
        private List<Model.PTP_ItemEndCheck> GetDetails()
        {
            ItemEndCheckLists.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string Content = values.Value<string>("Content");
                string Remark = values.Value<string>("Remark");
                System.Web.UI.WebControls.DropDownList ItemType = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpItemType"));
                Model.PTP_ItemEndCheck newAddItemEndCheck = new Model.PTP_ItemEndCheck();
                newAddItemEndCheck.ItemEndCheckListId = this.ItemEndCheckListId;
                newAddItemEndCheck.PipelineId = Grid1.Rows[i].DataKeys[0].ToString();
                newAddItemEndCheck.ItemCheckId = Grid1.Rows[i].DataKeys[1].ToString();
                newAddItemEndCheck.Content = Content;
                if (ItemType.SelectedValue != BLL.Const._Null)
                {
                    newAddItemEndCheck.ItemType = ItemType.SelectedValue;
                }
                newAddItemEndCheck.Remark = Remark;
                ItemEndCheckLists.Add(newAddItemEndCheck);
            }
            return ItemEndCheckLists;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnSave))
            {
                SaveData(Const.BtnSave);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AItemEndCheckMenuId, Const.BtnSave))
            {
                SaveData(Const.BtnSubmit);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        private void SaveData(string saveType)
        {
            if (string.IsNullOrEmpty(this.ItemEndCheckListId))
            {
                Model.PTP_ItemEndCheckList list = new Model.PTP_ItemEndCheckList();
                list.ItemEndCheckListId = SQLHelper.GetNewID();
                list.PTP_ID = this.PTP_ID;
                list.CompileMan = this.CurrUser.UserId;
                list.CompileDate = DateTime.Now;
                list.State = BLL.Const.TestPackage_Compile;
                BLL.ItemEndCheckListService.AddItemEndCheckList(list);
                ItemEndCheckListId = list.ItemEndCheckListId;
            }
            ///保存明细
            if (saveType == Const.BtnSubmit)
            {
                this.State = Const.TestPackage_Audit1;
            }
            var getItemEndCheck = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(this.ItemEndCheckListId);
            if (getItemEndCheck.Count>0)
            {
                BLL.AItemEndCheckService.DeleteAllItemEndCheckByID(this.PTP_ID);
                ItemEndCheckLists = GetDetails();
                foreach (var item in ItemEndCheckLists)
                {
                    if (item.Content!="/")
                    {
                        Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                        newItemEndCheck.ItemCheckId = item.ItemCheckId;
                        newItemEndCheck.ItemEndCheckListId = item.ItemEndCheckListId;
                        newItemEndCheck.PipelineId = item.PipelineId;
                        newItemEndCheck.Content = item.Content;
                        newItemEndCheck.ItemType = item.ItemType;
                        newItemEndCheck.Remark = item.Remark;
                        AItemEndCheckService.AddAItemEndCheck(newItemEndCheck);
                    }
                    else
                    {
                        Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                        newItemEndCheck.ItemCheckId = item.ItemCheckId;
                        newItemEndCheck.ItemEndCheckListId = item.ItemEndCheckListId;
                        newItemEndCheck.PipelineId = item.PipelineId;
                        newItemEndCheck.Content = "/";
                        newItemEndCheck.ItemType = "/";
                        newItemEndCheck.Remark = item.Remark;
                        AItemEndCheckService.AddAItemEndCheck(newItemEndCheck);
                    }
                }
                Model.PTP_TestPackageApprove approve1 = BLL.TestPackageApproveService.GetTestPackageApproveById(this.ItemEndCheckListId);
                if (approve1 != null && saveType == Const.BtnSubmit)
                {
                    approve1.ApproveDate = DateTime.Now;

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
                    if (ItemEndCheckList != null) {
                        ItemEndCheckList.State = this.State;
                        ItemEndCheckListService.UpdateItemEndCheckList(ItemEndCheckList);
                    }
                }
            }
            else
            {
                ItemEndCheckLists = GetDetails();
                foreach (var item in ItemEndCheckLists)
                {
                    if (!string.IsNullOrEmpty(item.Content) && !string.IsNullOrEmpty(item.ItemCheckId))
                    {
                        Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                        newItemEndCheck.ItemCheckId = item.ItemCheckId;
                        newItemEndCheck.ItemEndCheckListId = item.ItemEndCheckListId;
                        newItemEndCheck.PipelineId = item.PipelineId;
                        newItemEndCheck.Content = item.Content;
                        newItemEndCheck.ItemType = item.ItemType;
                        newItemEndCheck.Remark = item.Remark;
                        AItemEndCheckService.AddAItemEndCheck(newItemEndCheck);
                    }
                    else
                    {
                        Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                        newItemEndCheck.ItemCheckId = item.ItemCheckId;
                        newItemEndCheck.ItemEndCheckListId = item.ItemEndCheckListId;
                        newItemEndCheck.PipelineId = item.PipelineId;
                        newItemEndCheck.Content = "/";
                        newItemEndCheck.ItemType = "/";
                        newItemEndCheck.Remark = item.Remark;
                        AItemEndCheckService.AddAItemEndCheck(newItemEndCheck);
                    }
                }
                if (saveType == Const.BtnSubmit)
                {
                    Model.PTP_TestPackageApprove approve1 = new Model.PTP_TestPackageApprove();
                    approve1.ApproveId = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.TestPackage_Compile;
                    approve1.ItemEndCheckListId = this.ItemEndCheckListId;
                    BLL.TestPackageApproveService.AddTestPackageApprove(approve1);
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
                        ItemEndCheckList.State = this.State;
                        ItemEndCheckListService.UpdateItemEndCheckList(ItemEndCheckList);
                    }
                }
                else
                {
                    Model.PTP_TestPackageApprove approve1 = new Model.PTP_TestPackageApprove();
                    approve1.ApproveId = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
                    approve1.ApproveMan = this.CurrUser.UserId;
                    approve1.ApproveType = BLL.Const.TestPackage_Compile;
                    approve1.ItemEndCheckListId = this.ItemEndCheckListId;
                    BLL.TestPackageApproveService.AddTestPackageApprove(approve1);
                }
            }

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