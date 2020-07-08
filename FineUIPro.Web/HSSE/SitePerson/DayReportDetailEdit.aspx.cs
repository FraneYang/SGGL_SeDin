using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class DayReportDetailEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 工作月报明细主键
        /// </summary>
        public string DayReportDetailId
        {
            get
            {
                return (string)ViewState["DayReportDetailId"];
            }
            set
            {
                ViewState["DayReportDetailId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.DayReportDetailId = Request.Params["dayReportDetailId"];
                this.txtWorkTime.Focus();
                if (!string.IsNullOrEmpty(Request.Params["type"]))
                {
                    this.btnSave.Hidden = true;
                    this.txtWorkTime.Readonly = true;
                    this.Grid1.AllowCellEditing = false;
                }
                var dayReportDetail = BLL.SitePerson_DayReportDetailService.GetDayReportDetailByDayReportDetailId(this.DayReportDetailId);
                if (dayReportDetail != null)
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(dayReportDetail.UnitId);
                    if (unit != null)
                    {
                        this.lbStaffData.Text = unit.UnitName + "人员情况";
                    }

                    this.txtStaffData.Text = dayReportDetail.StaffData;
                    this.txtWorkTime.Text = dayReportDetail.WorkTime.ToString();

                    var viewDayReportUnitDetail = from x in new Model.SGGLDB(Funs.ConnString).View_SitePerson_DayReportUnitDetail where x.DayReportDetailId == dayReportDetail.DayReportDetailId orderby x.WorkPostCode select x;
                    if (viewDayReportUnitDetail.Count() > 0)
                    {
                        this.Grid1.DataSource = viewDayReportUnitDetail;
                        this.Grid1.DataBind();
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Grid1.CommitChanges();
            var dayReportDetail = BLL.SitePerson_DayReportDetailService.GetDayReportDetailByDayReportDetailId(this.DayReportDetailId);
            int glAllPerson = 0, hsseAllPerson = 0, zyAllPerson = 0;
            if (dayReportDetail != null)
            {
                dayReportDetail.WorkTime = Funs.GetNewDecimalOrZero(this.txtWorkTime.Text.Trim());
                int totalRealPersonNum = 0;
                decimal totalPersonWorkTime = 0;
                List<Model.SitePerson_DayReportUnitDetail> dayReportUnitDetails = (from x in new Model.SGGLDB(Funs.ConnString).SitePerson_DayReportUnitDetail where x.DayReportDetailId == this.DayReportDetailId select x).ToList();

                JArray mergedData = Grid1.GetMergedData();
                foreach (JObject mergedRow in mergedData)
                {
                    string status = mergedRow.Value<string>("status");
                    JObject values = mergedRow.Value<JObject>("values");
                    Model.SitePerson_DayReportUnitDetail dayReportUnitDetail = dayReportUnitDetails.FirstOrDefault(x => x.DayReportUnitDetailId == values.Value<string>("DayReportUnitDetailId").ToString());
                    if (dayReportUnitDetail != null)
                    {
                        string postType = values.Value<string>("WorkPostName");
                        string ishsse = values.Value<string>("CheckPersonNum");
                        dayReportUnitDetail.RealPersonNum = Funs.GetNewIntOrZero(values.Value<string>("RealPersonNum").ToString());
                        dayReportUnitDetail.PersonWorkTime = Funs.GetNewDecimalOrZero(values.Value<string>("PersonWorkTime").ToString()); ;
                        dayReportUnitDetail.Remark = values.Value<string>("Remark").ToString();
                        BLL.SitePerson_DayReportUnitDetailService.UpdateDayReportUnitDetail(dayReportUnitDetail);
                        totalPersonWorkTime += dayReportUnitDetail.PersonWorkTime ?? 0;
                        totalRealPersonNum += dayReportUnitDetail.RealPersonNum ?? 0;
                        if (ishsse == "1")     ///安全专职人员集合
                        {
                            hsseAllPerson += dayReportUnitDetail.RealPersonNum.Value;
                        }
                        else
                        {
                            if (postType == Const.PostType_1 || postType == Const.PostType_4)    //一般管理岗位和特种管理人员
                            {
                                glAllPerson += dayReportUnitDetail.RealPersonNum.Value;
                            }
                            if (postType == Const.PostType_2 || postType == Const.PostType_3)    //特种作业人员和一般作业岗位
                            {
                                zyAllPerson += dayReportUnitDetail.RealPersonNum.Value;
                            }
                        }
                    }
                }

                dayReportDetail.RealPersonNum = totalRealPersonNum;
                dayReportDetail.PersonWorkTime = totalPersonWorkTime;
                string staffData = string.Empty;
                var unit = (from x in new Model.SGGLDB(Funs.ConnString).Project_ProjectUnit
                            where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitId == dayReportDetail.UnitId
                            select x).FirstOrDefault();
                if (unit != null)
                {
                    if (unit.UnitType == "1")
                    {
                        staffData += "总人数：" + (glAllPerson + hsseAllPerson + zyAllPerson).ToString() + "，管理人员总数" + glAllPerson.ToString() + "人，专职安全人员共" + hsseAllPerson.ToString() + " 人。";
                    }
                    else
                    {
                        staffData += "总人数：" + (glAllPerson + hsseAllPerson + zyAllPerson).ToString() + "，管理人员总数" + glAllPerson.ToString() + "人，持证专职安全人员共" + hsseAllPerson.ToString() + " 人，施工单位作业人员总数" + zyAllPerson.ToString() + "人。";
                    }
                }
                dayReportDetail.StaffData = staffData;
                BLL.SitePerson_DayReportDetailService.UpdateReportDetail(dayReportDetail);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtWorkTime_TextChanged(object sender, EventArgs e)
        {
            decimal time = Funs.GetNewDecimalOrZero(this.txtWorkTime.Text.Trim());
            List<Model.View_SitePerson_DayReportUnitDetail> viwList = new List<Model.View_SitePerson_DayReportUnitDetail>();
            var viewDayReportUnitDetail = from x in new Model.SGGLDB(Funs.ConnString).View_SitePerson_DayReportUnitDetail where x.DayReportDetailId == this.DayReportDetailId orderby x.WorkPostCode select x;
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                JObject values = mergedRow.Value<JObject>("values");
                var item = viewDayReportUnitDetail.FirstOrDefault(x => x.DayReportUnitDetailId == values.Value<string>("DayReportUnitDetailId").ToString());
                if (item != null)
                {
                    item.RealPersonNum = Funs.GetNewIntOrZero(values.Value<string>("RealPersonNum").ToString());
                    item.PersonWorkTime = item.RealPersonNum * time;
                    item.Remark = values.Value<string>("Remark").ToString();
                    viwList.Add(item);
                }
            }

            this.Grid1.DataSource = viwList;
            this.Grid1.DataBind();
        }
    }
}