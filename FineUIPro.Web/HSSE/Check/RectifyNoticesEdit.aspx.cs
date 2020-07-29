using BLL;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class RectifyNoticesEdit : PageBase
    {
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
        public string RectifyNoticesId
        {
            get
            {
                return (string)ViewState["RectifyNoticesId"];
            }
            set
            {
                ViewState["RectifyNoticesId"] = value;
            }
        }
        /// <summary>
        /// 当前状态
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
        public static List<Model.Check_RectifyNoticesItem> viewTestPlanTrainingList = new List<Model.Check_RectifyNoticesItem>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                
                ////自动生成编码
                this.txtRectifyNoticesCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticesMenuId, this.ProjectId, this.CurrUser.UnitId);
                //受检单位            
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, false);
                //区域
                BLL.UnitWorkService.InitUnitWorkDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);
                ///安全经理
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);

                ///检察人员
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpCheckMan, this.CurrUser.LoginProjectId, null, true);
                //Funs.FineUIPleaseSelect(drpIsRectify);
                RectifyNoticesId = Request.Params["RectifyNoticesId"];

                if (!string.IsNullOrEmpty(RectifyNoticesId))
                {
                    BindGrid();
                    BindGrid1();
                    this.hdRectifyNoticesId.Text = RectifyNoticesId;
                    Model.Check_RectifyNotices RectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                    if (!string.IsNullOrEmpty(RectifyNotices.UnitId))
                    {
                        this.drpUnitId.SelectedValue = RectifyNotices.UnitId;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.WorkAreaId))
                    {
                        this.drpWorkAreaId.SelectedValue = RectifyNotices.WorkAreaId;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.CheckManIds))
                    {
                        this.drpCheckMan.SelectedValueArray = RectifyNotices.CheckManIds.Split(',');
                    }
                    this.txtCheckPerson.Text = RectifyNotices.CheckManNames;
                    this.txtRectifyNoticesCode.Text = RectifyNotices.RectifyNoticesCode;
                    this.txtCheckedDate.Text = RectifyNotices.CheckedDate.ToString();
                    if (!string.IsNullOrEmpty(RectifyNotices.HiddenHazardType))
                    {
                        this.drpHiddenHazardType.SelectedValue = RectifyNotices.HiddenHazardType;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.States))
                    {
                        State = RectifyNotices.States;
                    }
                    else
                    {
                        State = "0";
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                    }
                    if (State == "0")
                    {
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                        this.drpUnitId.Readonly = false;
                        this.drpWorkAreaId.Readonly = false;
                        this.drpCheckMan.Readonly = false;
                        this.txtCheckPerson.Readonly = false;
                        this.txtCheckedDate.Readonly = false;
                        this.drpHiddenHazardType.Readonly = false;
                        this.drpSignPerson.SelectedValue = RectifyNotices.SignPerson;
                        this.btnSave.Hidden = false;
                        GridColumn columnReAttachUrl = Grid1.FindColumn("ReAttachUrl");
                        columnReAttachUrl.Hidden = true;
                        GridColumn columnIsRectify = Grid1.FindColumn("IsRectify");
                        columnIsRectify.Hidden = true;
                        GridColumn columnRectifyResults = Grid1.FindColumn("RectifyResults");
                        columnRectifyResults.Hidden = true;

                    }
                    if (State == "1")
                    {
                        this.next.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                        //this.Itemcontent.Hidden = true;
                        //this.btnSure.Hidden = true;
                        BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpDutyPerson, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//接收人
                        BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProfessionalEngineer, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//专业工程师
                        BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//施工经理
                        BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProjectManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);//项目经理
                       
                        var datas = Grid1.GetMergedData();
                        if (datas != null)
                        {
                            foreach (JObject mergedRow in Grid1.GetMergedData())
                            {
                                int i = mergedRow.Value<int>("index");
                                foreach (GridColumn column in Grid1.AllColumns)
                                {
                                    //int i = Grid1.AllColumns.IndexOf(column);
                                    Grid1.Rows[i].CellCssClasses[1] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[2] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[3] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                                    txtlimitTim.Enabled = false;
                                }
                            }
                        }
                        GridColumn columnReAttachUrl = Grid1.FindColumn("ReAttachUrl");
                        columnReAttachUrl.Hidden = true;
                        GridColumn columnIsRectify = Grid1.FindColumn("IsRectify");
                        columnIsRectify.Hidden = true;
                        GridColumn columnRectifyResults = Grid1.FindColumn("RectifyResults");
                        columnRectifyResults.Hidden = true;

                    }
                    else if (State == "2")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                        var datas = Grid1.GetMergedData();
                        if (datas != null)
                        {
                            foreach (JObject mergedRow in Grid1.GetMergedData())
                            {
                                int i = mergedRow.Value<int>("index");
                                foreach (GridColumn column in Grid1.AllColumns)
                                {
                                    //int i = Grid1.AllColumns.IndexOf(column);
                                    Grid1.Rows[i].CellCssClasses[1] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[2] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[3] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                                    txtlimitTim.Enabled = false;
                                    GridColumn columnIsRectify = Grid1.FindColumn("IsRectify");
                                    columnIsRectify.Hidden = true;
                                }
                            }
                        }
                        
                        //this.after.Hidden = false;
                        //this.txtWrongContent.Readonly = true;
                        //this.txtRequirement.Readonly = true;
                        //this.txtLimitTime.Readonly = true;
                        BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadManId, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//施工单位负责人
                    }
                    else if (State == "3")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next4.Hidden = true;
                        var datas = Grid1.GetMergedData();
                        if (datas != null)
                        {
                            foreach (JObject mergedRow in Grid1.GetMergedData())
                            {
                                int i = mergedRow.Value<int>("index");
                                foreach (GridColumn column in Grid1.AllColumns)
                                {
                                    //int i = Grid1.AllColumns.IndexOf(column);
                                    Grid1.Rows[i].CellCssClasses[1] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[2] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[3] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[5] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[6] = "f-grid-cell-uneditable";
                                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                                    txtlimitTim.Enabled = false;

                                }
                            }
                        }
                        GridColumn columnIsRectify = Grid1.FindColumn("IsRectify");
                        columnIsRectify.Hidden = true;

                        //this.Itemcontent.Hidden = true;
                        //this.btnSure.Hidden = true;
                        //复查人
                        BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpCheckPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                        this.drpCheckPerson.SelectedValue = RectifyNotices.CompleteManId;
                    }
                    else if (State == "4")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        //this.end.Hidden = false;
                        //this.after.Hidden = false;
                        //this.txtWrongContent.Readonly = true;
                        //this.txtRequirement.Readonly = true;
                        //this.txtLimitTime.Readonly = true;
                        //this.txtRectifyResults.Readonly = true;
                        var datas = Grid1.GetMergedData();
                        if (datas != null)
                        {
                            foreach (JObject mergedRow in Grid1.GetMergedData())
                            {
                                int i = mergedRow.Value<int>("index");
                                foreach (GridColumn column in Grid1.AllColumns)
                                {
                                    //int i = Grid1.AllColumns.IndexOf(column);
                                    Grid1.Rows[i].CellCssClasses[1] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[2] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[3] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[5] = "f-grid-cell-uneditable";
                                    Grid1.Rows[i].CellCssClasses[6] = "f-grid-cell-uneditable";
                                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                                    txtlimitTim.Enabled = false;
                                    
                                }
                            }
                        }

                    }
                    else if (State == "5")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        //this.Itemcontent.Hidden = true;
                    }
                }
                else
                {
                    this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    State = "0";
                    this.btnSave.Hidden = false;
                    this.next1.Hidden = true;
                    this.next2.Hidden = true;
                    this.next3.Hidden = true;
                    this.next3.Hidden = true;
                    this.next4.Hidden = true;
                    this.drpUnitId.Readonly = false;
                    this.drpWorkAreaId.Readonly = false;
                    this.drpCheckMan.Readonly = false;
                    this.txtCheckPerson.Readonly = false;
                    this.txtCheckedDate.Readonly = false;
                    this.drpHiddenHazardType.Readonly = false;
                    GridColumn columnReAttachUrl = Grid1.FindColumn("ReAttachUrl");
                    columnReAttachUrl.Hidden = true;
                    GridColumn columnIsRectify = Grid1.FindColumn("IsRectify");
                    columnIsRectify.Hidden = true;
                    GridColumn columnRectifyResults = Grid1.FindColumn("RectifyResults");
                    columnRectifyResults.Hidden = true;
                }
                if (State.Equals("0"))
                {
                    toolAdd.Hidden = false;
                    GridColumn columndel = Grid1.FindColumn("del");
                    columndel.Hidden = false;
                }
                else
                {
                    toolAdd.Hidden = true;
                    GridColumn columndel = Grid1.FindColumn("del");
                    columndel.Hidden = true;
                }
                viewTestPlanTrainingList.Clear();

            }
        }
        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ConvertDate(object date)
        {
            if (!Convert.IsDBNull(date))
            {
                return string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(date));
            }
            else
            {
                return null;
            }
        }
        public void BindGrid()
        {
            string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults, IsRectify  from [dbo].[Check_RectifyNoticesItem] ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where RectifyNoticesId = @RectifyNoticesId";
            listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

        }
        public void BindGrid1()
        {
            string strSql = @"select FlowOperateId, RectifyNoticesId, OperateName, OperateManId, OperateTime, IsAgree, Opinion,S.UserName from Check_RectifyNoticesFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where RectifyNoticesId= @RectifyNoticesId";
            listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addViewTestPlanTrainingList();
            Model.Check_RectifyNoticesItem notice = new Model.Check_RectifyNoticesItem();
            notice.RectifyNoticesItemId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem));
            viewTestPlanTrainingList.Add(notice);
            //将gd数据保存在list中
            Grid1.DataSource = viewTestPlanTrainingList;
            Grid1.DataBind();

        }
        private void addViewTestPlanTrainingList()
        {
            viewTestPlanTrainingList.Clear();
            var data = Grid1.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string wrongContent = values.Value<string>("WrongContent");
                    string rectifyNoticesItemId = values.Value<string>("RectifyNoticesItemId");
                    string requirement = values.Value<string>("Requirement");
                    string rectifyResults = values.Value<string>("RectifyResults");
                    //txtLimitTimes
                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                    System.Web.UI.WebControls.DropDownList drpIsRect = (System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpIsRectify");
                    var item = new Check_RectifyNoticesItem();
                    item.RectifyNoticesItemId = rectifyNoticesItemId;
                    item.RectifyNoticesId = RectifyNoticesId;
                    item.WrongContent = wrongContent;
                    item.Requirement = requirement;
                    item.LimitTime = Funs.GetNewDateTime(txtlimitTim.Text);
                    item.RectifyResults = rectifyResults;
                    viewTestPlanTrainingList.Add(item);
                }
                //item.RectifyResults = Grid1.Rows[i].Values[3].ToString()

            }


        }
        /// <summary>
        /// 保存整改单明细
        /// </summary>
        public void saveNoticesItemDetail()
        {
            var data = Grid1.GetMergedData();
            if (data != null)
            {
               
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string wrongContent = values.Value<string>("WrongContent");
                    string rectifyNoticesItemId = values.Value<string>("RectifyNoticesItemId");
                    string requirement = values.Value<string>("Requirement");
                    string rectifyResults = values.Value<string>("RectifyResults");
                    //txtLimitTimes
                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                    System.Web.UI.WebControls.DropDownList drpIsRect = (System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpIsRectify");
                    string limitTime = txtlimitTim.Text.Trim();
                        Model.Check_RectifyNoticesItem rectifyNoticesItem = Funs.DB.Check_RectifyNoticesItem.FirstOrDefault(e => e.RectifyNoticesItemId == rectifyNoticesItemId);
                        if (rectifyNoticesItem != null)
                        {
                            rectifyNoticesItem.RectifyNoticesItemId = rectifyNoticesItemId;
                            rectifyNoticesItem.RectifyNoticesId = RectifyNoticesId;
                            rectifyNoticesItem.WrongContent = wrongContent;
                            rectifyNoticesItem.Requirement = requirement;
                            rectifyNoticesItem.LimitTime = Funs.GetNewDateTime(limitTime);
                            rectifyNoticesItem.RectifyResults = rectifyResults;
                            if (State.Equals("4"))
                            {
                                    rectifyNoticesItem.IsRectify = Convert.ToBoolean(drpIsRect.SelectedValue);
                            }
                        Funs.DB.SubmitChanges();
                    }
                    
                    else
                    {
                        
                        var item = new Check_RectifyNoticesItem();
                        item.RectifyNoticesItemId = rectifyNoticesItemId;
                        item.RectifyNoticesId = RectifyNoticesId;
                        item.WrongContent = wrongContent;
                        item.Requirement = requirement;
                        item.LimitTime = Funs.GetNewDateTime(limitTime);
                        item.RectifyResults = rectifyResults;
                        if (State.Equals("4"))
                        {
                            
                                item.IsRectify = Convert.ToBoolean(drpIsRect.SelectedValue);
                        }

                        Funs.DB.Check_RectifyNoticesItem.InsertOnSubmit(item);
                        Funs.DB.SubmitChanges();
                    }
                    //item.RectifyResults = Grid1.Rows[i].Values[3].ToString()
                }
                
            }
        }


        /// <summary>
        /// 整改单明细数据验证
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {
            bool res = false;
            string err = string.Empty;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                string WrongContent = values.Value<string>("WrongContent");
                string Requirement = values.Value<string>("Requirement");
                if (string.IsNullOrWhiteSpace(WrongContent) || string.IsNullOrWhiteSpace(Requirement))
                {
                    err += "第" + (i + 1).ToString() + "行：";

                    if (string.IsNullOrWhiteSpace(WrongContent))
                    {
                        err += "请输入具体位置及隐患内容,";
                    }
                    if (string.IsNullOrWhiteSpace(Requirement))
                    {
                        err += "请输入整改要求,";
                    }
                    err = err.Substring(0, err.LastIndexOf(","));
                    err += "!";
                }

            }
            if (Grid1.Rows.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(err))
                {
                    Alert.ShowInTop(err, MessageBoxIcon.Warning);
                }
                else
                {
                    res = true;
                }
            }
            else
            {
                Alert.ShowInTop("请整改单内容！", MessageBoxIcon.Warning);
            }

            return res;
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "AttachUrl")
            {
                
                if (State=="1" || State == "2" || State == "3" || State == "4")
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=-1&strParam=1", itemId, BLL.Const.ProjectRectifyNoticesMenuId)));
                }
                else
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=0&strParam=1", itemId, BLL.Const.ProjectRectifyNoticesMenuId)));
                }
                }
            if (e.CommandName == "delete")
            {
                viewTestPlanTrainingList.Remove(viewTestPlanTrainingList.FirstOrDefault(p => p.RectifyNoticesItemId == itemId));
                Grid1.DataSource = viewTestPlanTrainingList;
                Grid1.DataBind();
            }
            if (e.CommandName == "ReAttachUrl")
            {
                if (State == "1" || State == "4")
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=-1&strParam=2", itemId, BLL.Const.ProjectRectifyNoticesMenuId)));
                }
                else
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=0&strParam=2", itemId, BLL.Const.ProjectRectifyNoticesMenuId)));
                }

                }
        }
        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;

        }
        #region


        //public void BindGrid()
        //{
        //    if (!string.IsNullOrEmpty(RectifyNoticesId))
        //    {
        //        string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults, IsRectify  from [dbo].[Check_RectifyNoticesItem] ";
        //        List<SqlParameter> listStr = new List<SqlParameter>();
        //        strSql += "where RectifyNoticesId = @RectifyNoticesId";
        //        listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
        //        SqlParameter[] parameter = listStr.ToArray();
        //        DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
        //        var table = this.GetPagedDataTable(Grid1, tb);
        //        Grid1.DataSource = table;
        //        Grid1.DataBind();
        //    }
        //    else
        //    {
        //        Grid1.DataSource = null;
        //        Grid1.DataBind();
        //    }
        //}
        //#region 保存整改信息
        ///// <summary>
        ///// 按钮确定事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSure_Click(object sender, EventArgs e)
        //{
        //    var getViewList = this.CollectGridInfo();
        //    getViewList = getViewList.Where(x => x.RectifyNoticesItemId != this.hdTestPlanTrainingId.Text).ToList();
        //    bool IsRectify = true;
        //    if (this.drpIsRectify.SelectedValue != BLL.Const._Null)
        //    {
        //        if (this.drpIsRectify.SelectedValue == "true")
        //        {
        //            IsRectify = true;
        //            Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
        //            {
        //                RectifyNoticesItemId = this.hdTestPlanTrainingId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem)) : this.hdTestPlanTrainingId.Text,
        //                RectifyNoticesId = this.hdRectifyNoticesId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices)) : this.hdRectifyNoticesId.Text.ToString(),
        //                WrongContent = txtWrongContent.Text,
        //                Requirement = txtRequirement.Text,
        //                LimitTime = Convert.ToDateTime(txtLimitTime.Text.Trim()),
        //                RectifyResults = txtRectifyResults.Text,
        //                IsRectify = IsRectify
        //            };
        //            getViewList.Add(newView);
        //            this.hdRectifyNoticesId.Text = newView.RectifyNoticesId;
        //        }
        //        else
        //        {
        //            IsRectify = false;
        //            Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
        //            {
        //                RectifyNoticesItemId = this.hdTestPlanTrainingId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem)) : this.hdTestPlanTrainingId.Text,
        //                RectifyNoticesId = this.hdRectifyNoticesId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices)) : this.hdRectifyNoticesId.Text.ToString(),
        //                WrongContent = txtWrongContent.Text,
        //                Requirement = txtRequirement.Text,
        //                LimitTime = Convert.ToDateTime(txtLimitTime.Text.Trim()),
        //                RectifyResults = txtRectifyResults.Text,
        //                IsRectify = IsRectify
        //            };
        //            getViewList.Add(newView);
        //            this.hdRectifyNoticesId.Text = newView.RectifyNoticesId;
        //        }
        //    }
        //    else {
        //        Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
        //        {
        //            RectifyNoticesItemId = this.hdTestPlanTrainingId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem)) : this.hdTestPlanTrainingId.Text,
        //            RectifyNoticesId = this.hdRectifyNoticesId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices)) : this.hdRectifyNoticesId.Text.ToString(),
        //            WrongContent = txtWrongContent.Text,
        //            Requirement = txtRequirement.Text,
        //            LimitTime = Convert.ToDateTime(txtLimitTime.Text.Trim()),
        //            RectifyResults = txtRectifyResults.Text
        //        };
        //        getViewList.Add(newView);
        //        this.hdRectifyNoticesId.Text = newView.RectifyNoticesId;
        //    }

        //    this.Grid1.DataSource = getViewList;
        //    this.Grid1.DataBind();
        //    this.InitText();

        //}
        ///// <summary>
        ///// 整改前照片
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnAttachUrl_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.hdTestPlanTrainingId.Text))   //新增记录
        //    {
        //        this.hdTestPlanTrainingId.Text = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem));
        //    }
        //    if (State == "2" || State == "4")
        //    {
        //        PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=-1&strParam=1", this.hdTestPlanTrainingId.Text, BLL.Const.ProjectRectifyNoticesMenuId)));
        //    }
        //    else
        //    {
        //        PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=0&strParam=1", this.hdTestPlanTrainingId.Text, BLL.Const.ProjectRectifyNoticesMenuId)));
        //    }
        //}
        ///// <summary>
        ///// 整改后照片
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnAttachUrlafter_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.hdTestPlanTrainingId.Text))   //新增记录
        //    {
        //        this.hdTestPlanTrainingId.Text = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem));
        //    }
        //    if (State == "4")
        //    {
        //        PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=-1&strParam=2", this.hdTestPlanTrainingId.Text, BLL.Const.ProjectRectifyNoticesMenuId)));
        //    }
        //    else {
        //        PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=0&strParam=2", this.hdTestPlanTrainingId.Text, BLL.Const.ProjectRectifyNoticesMenuId)));
        //    }

        //}
        //#region 收集页面信息
        ///// <summary>
        /////  收集页面信息
        ///// </summary>
        ///// <returns></returns>
        //private List<Model.Check_RectifyNoticesItem> CollectGridInfo()
        //{
        //    List<Model.Check_RectifyNoticesItem> getViewList = new List<Model.Check_RectifyNoticesItem>();
        //    for (int i = 0; i < Grid1.Rows.Count; i++)
        //    {
        //        var str = Grid1.Rows[i].DataKeys[1] + "";
        //        if (str.IndexOf("True") > -1)
        //        {
        //            Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
        //            {
        //                RectifyNoticesItemId = Grid1.Rows[i].DataKeys[0].ToString(),
        //                RectifyNoticesId = this.RectifyNoticesId,
        //                WrongContent = Grid1.Rows[i].Values[0].ToString(),
        //                Requirement = Grid1.Rows[i].Values[1].ToString(),
        //                LimitTime = Convert.ToDateTime(Grid1.Rows[i].Values[2].ToString()),
        //                RectifyResults = Grid1.Rows[i].Values[4].ToString(),
        //                IsRectify = true
        //            };

        //            getViewList.Add(newView);
        //        }
        //        else if (str.IndexOf("False") > -1)
        //        {
        //            Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
        //            {
        //                RectifyNoticesItemId = Grid1.Rows[i].DataKeys[0].ToString(),
        //                RectifyNoticesId = this.RectifyNoticesId,
        //                WrongContent = Grid1.Rows[i].Values[0].ToString(),
        //                Requirement = Grid1.Rows[i].Values[1].ToString(),
        //                LimitTime = Convert.ToDateTime(Grid1.Rows[i].Values[2].ToString()),
        //                RectifyResults = Grid1.Rows[i].Values[4].ToString(),
        //                IsRectify = false
        //            };

        //            getViewList.Add(newView);
        //        }
        //        else
        //        {
        //            Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
        //            {
        //                RectifyNoticesItemId = Grid1.Rows[i].DataKeys[0].ToString(),
        //                RectifyNoticesId = this.RectifyNoticesId,
        //                WrongContent = Grid1.Rows[i].Values[0].ToString(),
        //                Requirement = Grid1.Rows[i].Values[1].ToString(),
        //                LimitTime = Funs.GetNewDateTime(Grid1.Rows[i].Values[2].ToString()),
        //                RectifyResults = Grid1.Rows[i].Values[4].ToString()
        //            };

        //            getViewList.Add(newView);
        //        }
        //    }

        //    return getViewList;
        //}
        //#endregion

        //#region 页面清空
        ///// <summary>
        ///// 页面清空
        ///// </summary>
        //private void InitText()
        //{
        //    this.hdTestPlanTrainingId.Text = string.Empty;
        //    this.txtWrongContent.Text = string.Empty;
        //    this.txtRequirement.Text = string.Empty;
        //    this.txtLimitTime.Text = string.Empty;
        //    txtRectifyResults.Text = string.Empty;
        //    this.drpIsRectify.SelectedIndex = 0;
        //}
        //#endregion
        //#endregion

        //#region 修改
        //protected void btnMenuEdit_Click(object sender, EventArgs e)
        //{
        //    EditData();
        //}
        //protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        //{
        //    EditData();
        //}
        //private void EditData()
        //{
        //    if (Grid1.SelectedRowIndexArray.Length == 0)
        //    {
        //        Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
        //        return;
        //    }
        //    var getViewList = this.CollectGridInfo();
        //    var item = getViewList.FirstOrDefault(x => x.RectifyNoticesItemId == Grid1.SelectedRowID);
        //    if (item != null)
        //    {
        //        this.hdTestPlanTrainingId.Text = item.RectifyNoticesItemId;
        //        this.txtRequirement.Text = item.Requirement.ToString();
        //        this.txtWrongContent.Text = item.WrongContent.ToString();
        //        this.txtLimitTime.Text = item.LimitTime.ToString();
        //        this.txtRectifyResults.Text = item.RectifyResults.ToString();
        //        if (item.IsRectify != null) {
        //            if(item.IsRectify==true)
        //            this.drpIsRectify.SelectedValue ="true";
        //            else
        //                this.drpIsRectify.SelectedValue = "false";
        //        }
        //    }
        //}
        //#endregion

        //#region 删除
        //protected void btnMenuDelete_Click(object sender, EventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length > 0)
        //    {
        //        var getViewList = this.CollectGridInfo();
        //        foreach (int rowIndex in Grid1.SelectedRowIndexArray)
        //        {
        //            string rowID = Grid1.DataKeys[rowIndex][0].ToString();
        //            var item = getViewList.FirstOrDefault(x => x.RectifyNoticesItemId == rowID);
        //            if (item != null)
        //            {
        //                getViewList.Remove(item);
        //            }
        //        }

        //        this.Grid1.DataSource = getViewList;
        //        this.Grid1.DataBind();
        //    }
        //}




        //#endregion
        #endregion


        #region 保存方法

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRectifyNotices("save");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveRectifyNotices("submit");
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="saveType"></param>
        private void SaveRectifyNotices(string saveType)
        {
            Model.Check_RectifyNotices Notices = new Model.Check_RectifyNotices();
            Notices.RectifyNoticesCode = this.txtRectifyNoticesCode.Text.Trim();
            Notices.ProjectId = this.CurrUser.LoginProjectId;
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                Notices.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                Notices.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }
            if (this.drpCheckMan.SelectedValue != BLL.Const._Null)
            {
                string str = GetStringByArray(this.drpCheckMan.SelectedValueArray);
                Notices.CheckManIds = str;
            }
            if (!string.IsNullOrEmpty(txtCheckPerson.Text))
            {
                Notices.CheckManNames = txtCheckPerson.Text;
            }
            if (!string.IsNullOrEmpty(this.txtCheckedDate.Text.Trim()))
            {
                Notices.CheckedDate = Convert.ToDateTime(this.txtCheckedDate.Text.Trim());
            }
            if (this.drpHiddenHazardType.SelectedValue != BLL.Const._Null)
            {
                Notices.HiddenHazardType = this.drpHiddenHazardType.SelectedValue;
            }
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                Notices.SignPerson = this.drpSignPerson.SelectedValue;
            }
            if (saveType == "submit")
            {
                Notices.States = Convert.ToInt32(Convert.ToInt32(State) + 1).ToString();
            }
            else
            {
                var isUpdate = Funs.DB.Check_RectifyNotices.FirstOrDefault(x => x.RectifyNoticesId == RectifyNoticesId);
                if (isUpdate != null)
                {
                    if (string.IsNullOrEmpty(isUpdate.States))
                    {
                        Notices.States = "0";
                    }
                    else
                    {
                        Notices.States = isUpdate.States;
                    }
                }
                else
                {
                    Notices.States = "0";
                }
            }
            if (!string.IsNullOrEmpty(RectifyNoticesId))
            {
                Model.Check_RectifyNotices isUpdate = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                if (Notices.States == "0" || Notices.States == "1")  ////编制人 修改或提交
                {
                    isUpdate.UnitId = this.drpUnitId.SelectedValue;
                    if (this.drpWorkAreaId.SelectedValue != Const._Null)
                    {
                        isUpdate.WorkAreaId = this.drpWorkAreaId.SelectedValue;
                    }
                    isUpdate.CheckManIds = GetStringByArray(this.drpCheckMan.SelectedValueArray);
                    isUpdate.CheckManNames = txtCheckPerson.Text;
                    isUpdate.CheckedDate =Funs.GetNewDateTimeOrNow(this.txtCheckedDate.Text.Trim());
                    isUpdate.HiddenHazardType = this.drpHiddenHazardType.SelectedValue;
                    if (Notices.States == "1" && !string.IsNullOrEmpty(Notices.SignPerson))
                    {
                        isUpdate.SignPerson = Notices.SignPerson;
                        isUpdate.States = "1";
                        
                    }
                    else {
                        Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                        {
                            FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                            RectifyNoticesId = isUpdate.RectifyNoticesId,
                            OperateName = "检查人员重新下发整改单",
                            OperateManId = this.CurrUser.UserId,
                            OperateTime = DateTime.Now,

                        };
                        Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                        Funs.DB.SubmitChanges();
                    }

                    
                    saveNoticesItemDetail();
                    Funs.DB.SubmitChanges();
                }
                else if (Notices.States == "2") ////总包单位项目安全经理 审核
                {
                    /// 不同意 打回 同意抄送专业工程师、施工经理、项目经理 并下发分包接收人（也就是施工单位项目安全经理）
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        Notices.States = isUpdate.States = "0";

                    }
                    else
                    {

                        if (this.drpProfessionalEngineer.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.ProfessionalEngineerId = this.drpProfessionalEngineer.SelectedValue;
                        }
                        else
                        {
                            Alert.ShowInTop("专业工程师不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        if (this.drpDutyPerson.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.DutyPersonId = this.drpDutyPerson.SelectedValue;
                            isUpdate.DutyPerson = this.drpDutyPerson.SelectedValue;
                            isUpdate.SignDate = DateTime.Now;
                            isUpdate.DutyPersonTime = DateTime.Now;
                        }
                        else
                        {
                            Alert.ShowInTop("接收人不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        Notices.States = isUpdate.States = "2";
                        Funs.DB.SubmitChanges();

                    }
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "总包单位项目安全经理签发",
                        OperateManId = CurrUser.UserId,
                        Opinion = reason.Text,
                        IsAgree = Convert.ToBoolean(this.rdbIsAgree.SelectedValue),
                        OperateTime = DateTime.Now,

                    };
                    Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    Funs.DB.SubmitChanges();

                }
                else if (Notices.States == "3") /// 施工单位项目安全经理 整改 提交施工单位项目负责人
                {
                    //// 整改明细反馈
                    //var getViewList = this.CollectGridInfo();
                    //if (getViewList != null && getViewList.Count() > 0)
                    //{
                    //    foreach (var rItem in getViewList)
                    //    {
                    //        var getUpdateItem = Funs.DB.Check_RectifyNoticesItem.FirstOrDefault(x => x.RectifyNoticesItemId == rItem.RectifyNoticesItemId);
                    //        if (getUpdateItem != null)
                    //        {
                    //            getUpdateItem.RectifyResults = rItem.RectifyResults;
                    //            Funs.DB.SubmitChanges();
                    //        }
                    //    }
                    //}
                    saveNoticesItemDetail();
                    if (this.drpUnitHeadManId.SelectedValue != BLL.Const._Null)
                    {
                        isUpdate.UnitHeadManId = this.drpUnitHeadManId.SelectedValue;
                        isUpdate.CompleteDate = DateTime.Now;
                        Notices.States = isUpdate.States = "3";
                    }
                    else
                    {
                        Alert.ShowInTop("施工单位负责人不能为空！", MessageBoxIcon.Warning);
                        return;
                    }
                    Funs.DB.SubmitChanges();
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "责任人整改",
                        OperateManId = CurrUser.UserId,
                        OperateTime = DateTime.Now,

                    };
                    Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    Funs.DB.SubmitChanges();

                }
                else if (Notices.States == "4")
                { /// 施工单位项目负责人不同意 打回施工单位项目安全经理,同意提交安全经理/安全工程师复查
                    if (this.rdbUnitHeadManAgree.SelectedValue.Equals("false"))
                    {
                        Notices.States = isUpdate.States = "2";
                        isUpdate.CompleteDate = null;
                    }
                    else
                    {
                        if (drpCheckPerson.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.UnitHeadManDate = DateTime.Now;
                            isUpdate.CheckPerson = drpCheckPerson.SelectedValue;
                            Notices.States = isUpdate.States = "4";
                        }
                        else
                        {
                            Alert.ShowInTop("安全经理/安全工程师不能为空！", MessageBoxIcon.Warning);
                            return;
                        }
                        Funs.DB.SubmitChanges();

                    }
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "施工单位项目负责人审核",
                        OperateManId = CurrUser.UserId,
                        Opinion = this.reason1.Text,
                        IsAgree = Convert.ToBoolean(this.rdbUnitHeadManAgree.SelectedValue),
                        OperateTime = DateTime.Now,

                    };
                    Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    Funs.DB.SubmitChanges();
                    
                }

                else if (Notices.States == "5")
                {  ////安全经理/安全工程师 同意关闭，不同意打回施工单位项目安全经理
                    isUpdate.ReCheckOpinion = drpReCheckOpinion.SelectedText;
                    if (this.drpReCheckOpinion.SelectedValue.Equals("False"))
                    {
                        Notices.States = isUpdate.States = "2";
                        isUpdate.UnitHeadManDate = null;
                        isUpdate.CompleteDate = null;
                        isUpdate.ProfessionalEngineerTime2 = null;
                        isUpdate.ConstructionManagerTime2 = null;
                        isUpdate.ProjectManagerTime2 = null;
                    }
                    else
                    {
                        isUpdate.ReCheckDate = DateTime.Now;
                        Notices.States = isUpdate.States = "5";
                    }
                    Funs.DB.SubmitChanges();
                    //// 整改明细反馈
                    // var getViewList = this.CollectGridInfo();
                    //if (getViewList != null && getViewList.Count() > 0)
                    //{
                    //    foreach (var rItem in getViewList)
                    //    {
                    //        var getUpdateItem = Funs.DB.Check_RectifyNoticesItem.FirstOrDefault(x => x.RectifyNoticesItemId == rItem.RectifyNoticesItemId);
                    //        if (getUpdateItem != null)
                    //        {
                    //            if (this.drpReCheckOpinion.SelectedValue.Equals("false"))
                    //            {
                    //                getUpdateItem.IsRectify = false;
                    //            }
                    //            else
                    //            {
                    //                getUpdateItem.IsRectify = true;
                    //            }

                    //            Funs.DB.SubmitChanges();
                    //        }
                    //    }
                    //}
                    //// 回写专项检查明细表                            
                    var getcheck = from x in Funs.DB.Check_CheckSpecialDetail where x.RectifyNoticeId == isUpdate.RectifyNoticesId select x;
                    if (getcheck.Count() > 0)
                    {
                        foreach (var item in getcheck)
                        {
                            item.CompleteStatus = true;
                            item.CompletedDate = DateTime.Now;
                            Funs.DB.SubmitChanges();
                        }
                    }
                    bool flag = false;
                    if (this.drpReCheckOpinion.SelectedValue.Equals("True"))
                    {
                        flag = true;
                    }
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "总包单位安全经理/安全工程师复查",
                        OperateManId = CurrUser.UserId,
                        Opinion = this.drpReCheckOpinion.SelectedText+this.opinion.Text,
                        IsAgree = flag,
                        OperateTime = DateTime.Now,

                    };
                    Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    Funs.DB.SubmitChanges();
                    saveNoticesItemDetail();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.hdRectifyNoticesId.Text))
                {
                    Notices.RectifyNoticesId = this.hdRectifyNoticesId.Text;
                }
                else
                {
                    Notices.RectifyNoticesId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
                }
                Notices.CompleteManId = this.CurrUser.UserId;
                Notices.CompleteDate = DateTime.Now;
                Notices.Isprint = "0";
                Notices.Isprintf = "0";
                if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
                {
                    Notices.SignPerson = this.drpSignPerson.SelectedValue;
                }
                else
                {
                    Alert.ShowInTop("项目安全经理不能为空！", MessageBoxIcon.Warning);
                    return;
                }

                RectifyNoticesService.AddRectifyNotices(Notices);
                RectifyNoticesId = Notices.RectifyNoticesId;
                Model.Check_RectifyNotices Notices1 = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);

                Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                {
                    FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                    RectifyNoticesId = Notices1.RectifyNoticesId,
                    OperateName = "检查人员下发整改单",
                    OperateManId = this.CurrUser.UserId,
                    OperateTime = DateTime.Now,

                };
                Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                Funs.DB.SubmitChanges();
                saveNoticesItemDetail();

                //var getViewList = this.CollectGridInfo();
                //var getRectifyNoticesItem = from x in getViewList
                //                            select new Model.Check_RectifyNoticesItem
                //                            {
                //                                RectifyNoticesItemId = x.RectifyNoticesItemId,
                //                                RectifyNoticesId = Notices1.RectifyNoticesId,
                //                                WrongContent = x.WrongContent,
                //                                Requirement = x.Requirement,
                //                                LimitTime = x.LimitTime,
                //                            };
                //if (getRectifyNoticesItem.Count() > 0)
                //{
                //    Funs.DB.Check_RectifyNoticesItem.InsertAllOnSubmit(getRectifyNoticesItem);
                //    Funs.DB.SubmitChanges();
                //}
            }

            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion


        #region 单选按钮切换事件
        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                this.reason.Hidden = false;
                this.step1_person.Hidden = true;
                this.step1_person2.Hidden = true;
                this.txtHandleMan.Hidden = false;
                Model.Check_RectifyNotices RectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                this.txtHandleMan.Text = BLL.UserService.GetUserNameByUserId(RectifyNotices.CompleteManId);
            }
            else
            {
                this.txtHandleMan.Hidden = true;
                this.reason.Hidden = true;
                this.step1_person.Hidden = false;
                this.step1_person2.Hidden = false;
            }
        }

        protected void rdbUnitHeadManAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdbUnitHeadManAgree.SelectedValue.Contains("false"))
            {
                this.reason1.Hidden = false;
                this.step3_CheckPerson.Hidden = true;
                this.txtHandleMan1.Hidden = false;
                Model.Check_RectifyNotices RectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                this.txtHandleMan1.Text = BLL.UserService.GetUserNameByUserId(RectifyNotices.DutyPersonId);
            }
            else
            {
                this.txtHandleMan1.Hidden = true;
                this.reason1.Hidden = true;
                this.step3_CheckPerson.Hidden = false;
            }
        }

        
        #endregion

        
        private string GetStringByArray(string[] array)
        {
            string str = string.Empty;
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str += item + ",";
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }
        protected string ConvertIsRectify(object flag)
        {

            if (flag != null)
            {
                if (flag.ToString() == "True")
                {
                    return "合格";
                }
                else if (flag.ToString() == "False")
                {

                    return "不合格";
                }
            }
            return "";
        }


        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object RectifyNoticesItemId)
        {
            string url = string.Empty;
            if (RectifyNoticesItemId != null)
            {
                var RectifyNoticesItem = BLL.AttachFileService.GetAttachFile(RectifyNoticesItemId.ToString() + "#1", BLL.Const.ProjectRectifyNoticesMenuId);
                if (RectifyNoticesItem != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("~/", RectifyNoticesItem.AttachUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrlByImage(object RectifyNoticesItemId)
        {
            string url = string.Empty;
            if (RectifyNoticesItemId != null)
            {
                var RectifyNoticesItem = BLL.AttachFileService.GetAttachFile(RectifyNoticesItemId.ToString() + "#2", BLL.Const.ProjectRectifyNoticesMenuId);
                if (RectifyNoticesItem != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("~/", RectifyNoticesItem.AttachUrl);
                }
            }
            return url;
        }
        /// <summary>
        /// 明细整改状态判断
        /// </summary>
        /// <returns></returns>
        private Boolean noticeItem()
        {
            bool res = true;
            var data = Grid1.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    System.Web.UI.WebControls.DropDownList drpIsRect = (System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpIsRectify");
                    if (drpIsRect.SelectedValue.Equals("false"))
                    {
                        res = false;
                        break;
                    }

                }
            }
            return res;
        }

        protected void drpIsRectify_SelectedIndexChanged(object sender, EventArgs e)
        {
            string res = noticeItem().ToString();
            drpReCheckOpinion.SelectedValue = res;
            if (drpReCheckOpinion.SelectedValue == "True")
            {
                this.opinion.Hidden = true;
            }
            else {
                this.opinion.Hidden = false;
            }
            
        }

        protected void drpCheckMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] array = this.drpCheckMan.SelectedValueArray;
            List<string> str = new List<string>();
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str.Add(item);
                }

            }
            this.drpCheckMan.SelectedValueArray = str.ToArray();
        }
    }
}