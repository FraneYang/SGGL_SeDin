using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class MonthReportDetailEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 工作月报明细主键
        /// </summary>
        public string MonthReportDetailId
        {
            get
            {
                return (string)ViewState["MonthReportDetailId"];
            }
            set
            {
                ViewState["MonthReportDetailId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MonthReportDetailId = Request.Params["monthReportDetailId"];
                this.txtWorkTime.Focus();
                if (!string.IsNullOrEmpty(Request.Params["type"]))
                {
                    this.btnSave.Hidden = true;
                    this.txtWorkTime.Readonly = true;
                    this.Grid1.AllowCellEditing = false;
                    this.txtRealPersonNum2.Readonly = true;
                    this.txtDayNum.Readonly = true;
                }
                var monthReportDetail = BLL.SitePerson_MonthReportDetailService.GetMonthReportDetailByMonthReportDetailId(this.MonthReportDetailId);
                if (monthReportDetail != null)
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(monthReportDetail.UnitId);
                    if (unit != null)
                    {
                        this.lbStaffData.Text = unit.UnitName + "人员情况";
                    }
                    //是否按平均数取值
                    var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_IsMonthReportGetAVG).FirstOrDefault();
                    if (sysSet != null)
                    {
                        if (sysSet.ConstValue == "1")
                        {
                            this.trAVG.Hidden = false;
                        }
                        else
                        {
                            this.trAVG.Hidden = true;
                        }
                    }
                    this.txtStaffData.Text = monthReportDetail.StaffData;
                    this.txtWorkTime.Text = monthReportDetail.WorkTime.ToString();
                    if (monthReportDetail.RealPersonNum != null)
                    {
                        this.txtRealPersonNum2.Text = monthReportDetail.RealPersonNum.ToString();
                    }
                    if (monthReportDetail.DayNum != null)
                    {
                        this.txtDayNum.Text = monthReportDetail.DayNum.ToString();
                    }
                    var viewMonthReportUnitDetail = from x in Funs.DB.View_SitePerson_MonthReportUnitDetail
                                                    where x.MonthReportDetailId == monthReportDetail.MonthReportDetailId 
                                                    orderby x.WorkPostCode
                                                    select x;
                    if (viewMonthReportUnitDetail.Count() > 0)
                    {
                        this.Grid1.DataSource = viewMonthReportUnitDetail;
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
            var monthReportDetail = BLL.SitePerson_MonthReportDetailService.GetMonthReportDetailByMonthReportDetailId(this.MonthReportDetailId);
            if (monthReportDetail != null)
            {
                monthReportDetail.WorkTime = Funs.GetNewDecimalOrZero(this.txtWorkTime.Text.Trim());
                monthReportDetail.DayNum = Funs.GetNewDecimalOrZero(this.txtDayNum.Text.Trim());
                int totalRealPersonNum = 0;
                decimal totalPersonWorkTime = 0;
                List<Model.SitePerson_MonthReportUnitDetail> monthReportUnitDetails = (from x in Funs.DB.SitePerson_MonthReportUnitDetail where x.MonthReportDetailId == this.MonthReportDetailId select x).ToList();
                
                JArray mergedData = Grid1.GetMergedData();
                foreach (JObject mergedRow in mergedData)
                {
                    string status = mergedRow.Value<string>("status");
                    JObject values = mergedRow.Value<JObject>("values");
                    Model.SitePerson_MonthReportUnitDetail monthReportUnitDetail = monthReportUnitDetails.FirstOrDefault(x => x.MonthReportUnitDetailId == values.Value<string>("MonthReportUnitDetailId").ToString());
                    if (monthReportUnitDetail != null)
                    {
                        monthReportUnitDetail.RealPersonNum = Funs.GetNewIntOrZero(values.Value<string>("RealPersonNum").ToString());
                        monthReportUnitDetail.PersonWorkTime = Funs.GetNewDecimalOrZero(values.Value<string>("PersonWorkTime").ToString()); ;
                        monthReportUnitDetail.Remark =  values.Value<string>("Remark").ToString();
                        
                        BLL.SitePerson_MonthReportUnitDetailService.UpdateMonthReportUnitDetail(monthReportUnitDetail);
                        totalPersonWorkTime += monthReportUnitDetail.PersonWorkTime ?? 0;
                        totalRealPersonNum += monthReportUnitDetail.RealPersonNum ?? 0;
                    }
                }

                monthReportDetail.RealPersonNum = totalRealPersonNum;
                monthReportDetail.PersonWorkTime = totalPersonWorkTime;
                //是否按平均数取值
                var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_IsMonthReportGetAVG).FirstOrDefault();
                if (sysSet != null)
                {
                    if (sysSet.ConstValue == "1")
                    {
                        monthReportDetail.RealPersonNum = Funs.GetNewDecimalOrZero(this.txtRealPersonNum2.Text.Trim());
                        monthReportDetail.DayNum = Funs.GetNewDecimalOrZero(this.txtDayNum.Text.Trim());
                        monthReportDetail.PersonWorkTime = (monthReportDetail.RealPersonNum ?? 0) * (monthReportDetail.DayNum ?? 0) * (monthReportDetail.WorkTime ?? 0);
                    }
                }
                BLL.SitePerson_MonthReportDetailService.UpdateReportDetail(monthReportDetail);
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
            int dayNum = Funs.GetNewIntOrZero(this.txtDayNum.Text.Trim());
            List<Model.View_SitePerson_MonthReportUnitDetail> viwList = new List<Model.View_SitePerson_MonthReportUnitDetail>();
            var viewDayReportUnitDetail = from x in Funs.DB.View_SitePerson_MonthReportUnitDetail where x.MonthReportDetailId == this.MonthReportDetailId orderby x.WorkPostCode select x;           

            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                JObject values = mergedRow.Value<JObject>("values");                
                var item = viewDayReportUnitDetail.FirstOrDefault(x => x.MonthReportUnitDetailId == values.Value<string>("MonthReportUnitDetailId").ToString());
                if (item != null)
                {
                    item.RealPersonNum = Funs.GetNewIntOrZero(values.Value<string>("RealPersonNum").ToString());
                    item.PersonWorkTime = item.RealPersonNum * time * dayNum;
                    item.Remark = values.Value<string>("Remark").ToString();
                    viwList.Add(item);
                }
            }

            this.Grid1.DataSource = viwList;
            this.Grid1.DataBind();
        } 
    }
}