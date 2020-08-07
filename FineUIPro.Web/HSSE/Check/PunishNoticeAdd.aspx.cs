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
namespace FineUIPro.Web.HSSE.Check
{
    public partial class PunishNoticeAdd : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PunishNoticeId
        {
            get
            {
                return (string)ViewState["PunishNoticeId"];
            }
            set
            {
                ViewState["PunishNoticeId"] = value;
            }
        }
        /// <summary>
        /// 项目Id
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
        public static List<Model.Check_PunishNoticeItem> viewPunishNoticeList = new List<Model.Check_PunishNoticeItem>();
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.InitDropDownList();
                this.PunishNoticeId = Request.Params["PunishNoticeId"];
                this.txtCurrency.Text = "人民币";
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpPunishPersonId, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                if (!string.IsNullOrEmpty(this.PunishNoticeId))
                {
                    BindGrid();
                    BindGrid1();
                    Model.Check_PunishNotice punishNotice = BLL.PunishNoticeService.GetPunishNoticeById(this.PunishNoticeId);
                    if (punishNotice != null)
                    {
                        this.ProjectId = punishNotice.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPunishNoticeCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PunishNoticeId);
                        this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", punishNotice.PunishNoticeDate);
                        if (!string.IsNullOrEmpty(punishNotice.UnitId))
                        {
                            this.drpUnitId.SelectedValue = punishNotice.UnitId;
                        }
                        if (!string.IsNullOrEmpty(punishNotice.PunishPersonId))
                        {
                            this.drpPunishPersonId.SelectedValue = punishNotice.PunishPersonId;
                        }
                        if (punishNotice.PunishMoney.HasValue)
                        {
                            this.txtPunishMoney.Text = Convert.ToString(punishNotice.PunishMoney);
                            this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));
                        }
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(punishNotice.FileContents);

                        if (!string.IsNullOrEmpty(punishNotice.Currency))
                        {
                            this.txtCurrency.Text = punishNotice.Currency;
                        }
                        this.drpSignPerson.SelectedValue = punishNotice.SignMan;
                    }
                }
                else
                {
                    this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtPunishNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPunishNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
            }
            else
            {
                if (GetRequestEventArgument() == "UPDATE_SUMMARY")
                {
                    // 页面要求计算总金额的值
                    OutputSummaryData();
                }
            }
        }
        public void BindGrid()
        {
            string strSql = @"select PunishNoticeItemId, PunishNoticeId, PunishContent,PunishBasicItem, PunishMoney, SortIndex from Check_PunishNoticeItem ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where PunishNoticeId= @PunishNoticeId";
            listStr.Add(new SqlParameter("@PunishNoticeId", PunishNoticeId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

        }
        //办理记录
        public void BindGrid1()
        {
            string strSql = @"select FlowOperateId, PunishNoticeId, OperateName, OperateManId, OperateTime, case when IsAgree='False' then '否' else '是' end  As IsAgree, Opinion,S.UserName from Check_PunishNoticeFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where PunishNoticeId= @PunishNoticeId";
            listStr.Add(new SqlParameter("@PunishNoticeId", PunishNoticeId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
        #endregion

        #region 合计金额
        private void OutputSummaryData()
        {
            decimal TotalMoney = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (!string.IsNullOrEmpty(values["PunishMoney"].ToString()))
                {
                    TotalMoney += values.Value<decimal>("PunishMoney");
                }
            }
            this.txtPunishMoney.Text = TotalMoney.ToString();
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, false);
        }

        #region  获取大写金额事件
        /// <summary>
        /// 获取大写金额事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPunishMoney_Blur(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPunishMoney.Text))
            {
                this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));
            }
            else
            {
                this.txtBig.Text = string.Empty;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 回执单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PunishNoticeId))
            {
                this.PunishNoticeId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNotice&menuId=" + Const.ProjectPunishNoticeMenuId, this.PunishNoticeId)));
        }

        /// <summary>
        /// 通知单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPunishNoticeUrl_Click(object sender, EventArgs e)
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPunishNoticeMenuId);
            if (string.IsNullOrEmpty(this.PunishNoticeId))
            {
                this.PunishNoticeId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
            }
            if (buttonList.Count() > 0)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {

            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受罚单位", MessageBoxIcon.Warning);
                return;
            }

            Model.Check_PunishNotice punishNotice = new Model.Check_PunishNotice
            {
                ProjectId = this.ProjectId,
                PunishNoticeCode = this.txtPunishNoticeCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                punishNotice.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpPunishPersonId.SelectedValue != BLL.Const._Null)
            {
                punishNotice.PunishPersonId = this.drpPunishPersonId.SelectedValue;
            }
            punishNotice.PunishNoticeDate = Funs.GetNewDateTime(this.txtPunishNoticeDate.Text.Trim());
            punishNotice.PunishMoney = Funs.GetNewDecimalOrZero(this.txtPunishMoney.Text.Trim());
            punishNotice.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            punishNotice.CompileMan = this.CurrUser.UserId;
            punishNotice.CompileDate = DateTime.Now;
            punishNotice.States = Const.State_0;
            punishNotice.Currency = this.txtCurrency.Text.Trim();
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                punishNotice.SignMan = this.drpSignPerson.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("总包安全工程师/安全经理不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (type == BLL.Const.BtnSubmit)
            {
                punishNotice.PunishStates = "1";
            }
            else
            {
                punishNotice.PunishStates = "0";
            }
            var getUpdate = BLL.PunishNoticeService.GetPunishNoticeById(PunishNoticeId);
            //没有就新增一条处罚单
            if (getUpdate == null)
            {
                if (string.IsNullOrEmpty(this.PunishNoticeId))
                {
                    punishNotice.PunishNoticeId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
                }
                else
                {
                    punishNotice.PunishNoticeId = this.PunishNoticeId;
                }
                BLL.PunishNoticeService.AddPunishNotice(punishNotice);
                
                
                
                CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectPunishNoticeMenuId, punishNotice.ProjectId, punishNotice.UnitId, punishNotice.PunishNoticeId, punishNotice.CompileDate);
                this.PunishNoticeId = punishNotice.PunishNoticeId;
                saveNoticesItemDetail();
            }
            else
            {
                ////编制人 修改或提交
                if (this.drpUnitId.SelectedValue != BLL.Const._Null)
                {
                    getUpdate.UnitId = this.drpUnitId.SelectedValue;
                }
                getUpdate.PunishNoticeDate = Funs.GetNewDateTime(this.txtPunishNoticeDate.Text.Trim());
                getUpdate.PunishMoney = Funs.GetNewDecimalOrZero(this.txtPunishMoney.Text.Trim());
                getUpdate.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
                getUpdate.CompileMan = this.CurrUser.UserId;
                getUpdate.CompileDate = DateTime.Now;
                getUpdate.SignMan = this.drpSignPerson.SelectedValue;
                getUpdate.Currency = this.txtCurrency.Text.Trim();
                if (punishNotice.PunishStates == BLL.Const.State_1)
                {
                    getUpdate.PunishStates = "1";
                }
                Funs.DB.SubmitChanges();
                saveNoticesItemDetail();
            }
            if (punishNotice.PunishStates == BLL.Const.State_1)
            {
                SaveOperate("总包安全工程师下发处罚单");
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #region 保存流程审核数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void SaveOperate(string OperateName)
        {
            Model.Check_PunishNoticeFlowOperate newFlowOperate = new Model.Check_PunishNoticeFlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PunishNoticeFlowOperate)),
                PunishNoticeId = PunishNoticeId,
                OperateName = OperateName,
                OperateManId = CurrUser.UserId,
                OperateTime = DateTime.Now,
                IsAgree = true
            };
            Funs.DB.Check_PunishNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
            Funs.DB.SubmitChanges();

        }
        #endregion

        #endregion

        #region 处罚单明细

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addPunishNoticeList();
            Model.Check_PunishNoticeItem notice = new Model.Check_PunishNoticeItem();
            notice.PunishNoticeItemId = SQLHelper.GetNewID(typeof(Model.Check_PunishNoticeItem));
            viewPunishNoticeList.Add(notice);
            //将gd数据保存在list中
            Grid1.DataSource = viewPunishNoticeList;
            Grid1.DataBind();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "delete")
            {
                viewPunishNoticeList.Remove(viewPunishNoticeList.FirstOrDefault(p => p.PunishNoticeItemId == itemId));
                Grid1.DataSource = viewPunishNoticeList;
                Grid1.DataBind();
            }
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
        }
        private void addPunishNoticeList()
        {
            viewPunishNoticeList.Clear();
            var data = Grid1.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string PunishNoticeItemId = values.Value<string>("PunishNoticeItemId");
                    string PunishContent = values.Value<string>("PunishContent");
                    string PunishMoney = values.Value<string>("PunishMoney");
                    string PunishBasicItem = values.Value<string>("PunishBasicItem");
                    var item = new Model.Check_PunishNoticeItem();
                    item.PunishNoticeItemId = PunishNoticeItemId;
                    item.PunishNoticeId = PunishNoticeId;
                    item.PunishContent = PunishContent;
                    item.PunishMoney = Funs.GetNewDecimal(PunishMoney);
                    item.PunishBasicItem = PunishBasicItem;
                    viewPunishNoticeList.Add(item);
                }
                //item.RectifyResults = Grid1.Rows[i].Values[3].ToString()

            }


        }
        /// <summary>
        /// 保存处罚单明细
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
                    string PunishNoticeItemId = values.Value<string>("PunishNoticeItemId");
                    string PunishContent = values.Value<string>("PunishContent");
                    string PunishMoney = values.Value<string>("PunishMoney");
                    string PunishBasicItem = values.Value<string>("PunishBasicItem");
                    AspNet.Label lblNumber = (AspNet.Label)Grid1.Rows[i].FindControl("lblNumber");
                    string SortIndex = lblNumber.Text.Trim();
                    Model.Check_PunishNoticeItem PunishNoticeItem = Funs.DB.Check_PunishNoticeItem.FirstOrDefault(e => e.PunishNoticeItemId == PunishNoticeItemId);
                    if (PunishNoticeItem != null)
                    {
                        PunishNoticeItem.PunishNoticeItemId = PunishNoticeItemId;
                        PunishNoticeItem.PunishNoticeId = PunishNoticeId;
                        PunishNoticeItem.PunishContent = PunishContent;
                        PunishNoticeItem.PunishMoney = decimal.Round(Funs.GetNewDecimalOrZero(PunishMoney), 2);
                        PunishNoticeItem.SortIndex = Funs.GetNewInt(SortIndex);
                        PunishNoticeItem.PunishBasicItem = PunishBasicItem;
                        Funs.DB.SubmitChanges();
                    }
                    else
                    {

                        var item = new Model.Check_PunishNoticeItem();
                        item.PunishNoticeItemId = PunishNoticeItemId;
                        item.PunishNoticeId = PunishNoticeId;
                        item.PunishContent = PunishContent;
                        item.PunishMoney = decimal.Round(Funs.GetNewDecimalOrZero(PunishMoney), 2);
                        item.SortIndex = Funs.GetNewInt(SortIndex);
                        item.PunishBasicItem = PunishBasicItem;
                        Funs.DB.Check_PunishNoticeItem.InsertOnSubmit(item);
                        Funs.DB.SubmitChanges();
                    }
                }

            }
        }
        #endregion
        /// <summary>
        /// 根据受罚单位定位受罚人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpPunishPersonId, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//分包单位
                this.drpPunishPersonId.SelectedIndex = 0;
            }
            else
            {
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpPunishPersonId, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
            }
        }
    }
}