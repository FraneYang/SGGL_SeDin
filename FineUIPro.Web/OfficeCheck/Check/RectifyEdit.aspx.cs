using BLL;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.OfficeCheck.Check
{
    public partial class RectifyEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 检查Id
        /// </summary>
        public string CheckNoticeId
        {
            get
            {
                return (string)ViewState["CheckNoticeId"];
            }
            set
            {
                ViewState["CheckNoticeId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        public static List<Model.ProjectSupervision_RectifyItem> rectifyItemLists = new List<Model.ProjectSupervision_RectifyItem>();
        #endregion

        #region 加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //受检单位            
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, true);
                //区域
                BLL.UnitWorkService.InitUnitWorkDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);
                ///安全经理
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                ///检察人员
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpCheckMan, this.CurrUser.LoginProjectId, null, true);
                string type = Request.Params["type"];
                if (type == "1")
                {
                    this.btnSave.Hidden = true;
                    this.btnAdd.Hidden = true;
                    this.Grid1.Columns[8].Hidden = true;
                }
                this.CheckNoticeId = Request.Params["CheckNoticeId"];
                if (!string.IsNullOrEmpty(this.CheckNoticeId))
                {
                    Model.ProjectSupervision_Rectify RectifyNotices = ProjectSupervision_RectifyService.GetRectifyByCheckNoticeId(this.CheckNoticeId);
                    if (RectifyNotices != null)
                    {
                        this.hdRectifyNoticesId.Text = RectifyNotices.RectifyId;
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
                        this.txtRectifyNoticesCode.Text = RectifyNotices.RectifyCode;
                        this.txtCheckedDate.Text = RectifyNotices.CheckedDate.ToString();
                        if (!string.IsNullOrEmpty(RectifyNotices.HiddenHazardType))
                        {
                            this.drpHiddenHazardType.SelectedValue = RectifyNotices.HiddenHazardType;
                        }
                        if (!string.IsNullOrEmpty(RectifyNotices.SignPerson))
                        {
                            this.drpSignPerson.SelectedValue = RectifyNotices.SignPerson;
                        }
                        BindGrid();
                    }
                    else
                    {
                        this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    }
                    rectifyItemLists.Clear();
                }
            }
        }

        /// <summary>
        /// 绑定明细
        /// </summary>
        public void BindGrid()
        {
            string strSql = @"select RectifyItemId, RectifyId, WrongContent, Requirement, LimitTime, RectifyResults, IsRectify  from ProjectSupervision_RectifyItem ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where RectifyId = @RectifyId";
            listStr.Add(new SqlParameter("@RectifyId", this.hdRectifyNoticesId.Text));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 添加按钮
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addViewTestPlanTrainingList();
            Model.ProjectSupervision_RectifyItem notice = new Model.ProjectSupervision_RectifyItem();
            notice.RectifyItemId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_RectifyItem));
            rectifyItemLists.Add(notice);
            //将gd数据保存在list中
            Grid1.DataSource = rectifyItemLists;
            Grid1.DataBind();

        }

        private void addViewTestPlanTrainingList()
        {
            rectifyItemLists.Clear();
            var data = Grid1.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    JObject values = mergedRow.Value<JObject>("values");
                    string wrongContent = values.Value<string>("WrongContent");
                    string rectifyNoticesItemId = values.Value<string>("RectifyItemId");
                    string requirement = values.Value<string>("Requirement");
                    string rectifyResults = values.Value<string>("RectifyResults");
                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                    System.Web.UI.WebControls.DropDownList drpIsRect = (System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpIsRectify");
                    var item = new ProjectSupervision_RectifyItem();
                    item.RectifyItemId = rectifyNoticesItemId;
                    item.RectifyId = hdRectifyNoticesId.Text.Trim();
                    item.WrongContent = wrongContent;
                    item.Requirement = requirement;
                    item.LimitTime = Funs.GetNewDateTime(txtlimitTim.Text);
                    item.RectifyResults = rectifyResults;
                    rectifyItemLists.Add(item);
                }
            }
        }
        #endregion

        #region 整改单明细数据验证
        ///// <summary>
        ///// 整改单明细数据验证
        ///// </summary>
        ///// <returns></returns>
        //private bool validate()
        //{
        //    bool res = false;
        //    string err = string.Empty;
        //    foreach (JObject mergedRow in Grid1.GetMergedData())
        //    {
        //        int i = mergedRow.Value<int>("index");
        //        JObject values = mergedRow.Value<JObject>("values");
        //        string WrongContent = values.Value<string>("WrongContent");
        //        string Requirement = values.Value<string>("Requirement");
        //        if (string.IsNullOrWhiteSpace(WrongContent) || string.IsNullOrWhiteSpace(Requirement))
        //        {
        //            err += "第" + (i + 1).ToString() + "行：";

        //            if (string.IsNullOrWhiteSpace(WrongContent))
        //            {
        //                err += "请输入具体位置及隐患内容,";
        //            }
        //            if (string.IsNullOrWhiteSpace(Requirement))
        //            {
        //                err += "请输入整改要求,";
        //            }
        //            err = err.Substring(0, err.LastIndexOf(","));
        //            err += "!";
        //        }
        //    }
        //    if (Grid1.Rows.Count > 0)
        //    {
        //        if (!string.IsNullOrWhiteSpace(err))
        //        {
        //            Alert.ShowInTop(err, MessageBoxIcon.Warning);
        //        }
        //        else
        //        {
        //            res = true;
        //        }
        //    }
        //    else
        //    {
        //        Alert.ShowInTop("请整改单内容！", MessageBoxIcon.Warning);
        //    }
        //    return res;
        //}
        #endregion

        #region Grid行点击事件
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "AttachUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Rectify&menuId={1}&type=0&strParam=1", itemId, BLL.Const.CheckInfoMenuId)));
            }
            if (e.CommandName == "delete")
            {
                rectifyItemLists.Remove(rectifyItemLists.FirstOrDefault(p => p.RectifyItemId == itemId));
                Grid1.DataSource = rectifyItemLists;
                Grid1.DataBind();
            }
            if (e.CommandName == "ReAttachUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Rectify&menuId={1}&type=0&strParam=2", itemId, BLL.Const.CheckInfoMenuId)));
            }
        }
        #endregion

        #region Grid行绑定事件
        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            //DataRowView row = e.DataItem as DataRowView;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                System.Web.UI.WebControls.DropDownList drpIsRectify = (System.Web.UI.WebControls.DropDownList)(this.Grid1.Rows[i].FindControl("drpIsRectify"));
                HiddenField hdIsRectify = (HiddenField)(this.Grid1.Rows[i].FindControl("hdIsRectify"));
                if (!string.IsNullOrEmpty(hdIsRectify.Text))
                {
                    if (hdIsRectify.Text == "True")
                    {
                        drpIsRectify.SelectedValue = "true";
                    }
                    else
                    {
                        drpIsRectify.SelectedValue = "false";
                    }
                }
            }
        }
        #endregion

        #region 时间转换
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
        #endregion

        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRectifyNotices("save");
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="saveType"></param>
        private void SaveRectifyNotices(string saveType)
        {
            if (this.drpSignPerson.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("项目安全经理不能为空！", MessageBoxIcon.Warning);
                return;
            }
            Model.ProjectSupervision_Rectify Notices = new Model.ProjectSupervision_Rectify();
            Notices.RectifyCode = this.txtRectifyNoticesCode.Text.Trim();
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
            if (!string.IsNullOrEmpty(this.hdRectifyNoticesId.Text))
            {
                Notices.RectifyId = this.hdRectifyNoticesId.Text;
                ProjectSupervision_RectifyService.UpdateRectify(Notices);
            }
            else
            {
                Notices.CheckNoticeId = this.CheckNoticeId;
                Notices.RectifyId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_Rectify));
                ProjectSupervision_RectifyService.AddRectify(Notices);
                this.hdRectifyNoticesId.Text = Notices.RectifyId;
            }
            saveNoticesItemDetail();//增加明细
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存明细
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
                    string rectifyNoticesItemId = values.Value<string>("RectifyItemId");
                    string requirement = values.Value<string>("Requirement");
                    string rectifyResults = values.Value<string>("RectifyResults");
                    System.Web.UI.WebControls.TextBox txtlimitTim = (System.Web.UI.WebControls.TextBox)Grid1.Rows[i].FindControl("txtLimitTimes");
                    System.Web.UI.WebControls.DropDownList drpIsRect = (System.Web.UI.WebControls.DropDownList)Grid1.Rows[i].FindControl("drpIsRectify");
                    string limitTime = txtlimitTim.Text.Trim();
                    Model.ProjectSupervision_RectifyItem rectifyNoticesItem = Funs.DB.ProjectSupervision_RectifyItem.FirstOrDefault(e => e.RectifyItemId == rectifyNoticesItemId);
                    if (rectifyNoticesItem != null)
                    {
                        rectifyNoticesItem.RectifyItemId = rectifyNoticesItemId;
                        rectifyNoticesItem.RectifyId = this.hdRectifyNoticesId.Text.Trim();
                        rectifyNoticesItem.WrongContent = wrongContent;
                        rectifyNoticesItem.Requirement = requirement;
                        rectifyNoticesItem.LimitTime = Funs.GetNewDateTime(limitTime);
                        rectifyNoticesItem.RectifyResults = rectifyResults;
                        rectifyNoticesItem.IsRectify = Convert.ToBoolean(drpIsRect.SelectedValue);
                        Funs.DB.SubmitChanges();
                    }
                    else
                    {

                        var item = new ProjectSupervision_RectifyItem();
                        item.RectifyItemId = rectifyNoticesItemId;
                        item.RectifyId = this.hdRectifyNoticesId.Text.Trim();
                        item.WrongContent = wrongContent;
                        item.Requirement = requirement;
                        item.LimitTime = Funs.GetNewDateTime(limitTime);
                        item.RectifyResults = rectifyResults;
                        item.IsRectify = Convert.ToBoolean(drpIsRect.SelectedValue);
                        Funs.DB.ProjectSupervision_RectifyItem.InsertOnSubmit(item);
                        Funs.DB.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 格式化字符串
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
        #endregion

        #region DropDownList下拉选择
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
        #endregion
    }
}