using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class WorkPostStatistic : PageBase
    {
        /// <summary>
        /// 项目id
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
                this.drpUnit.SelectedValue = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId;
                if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnit.Enabled = false;
                }
                WorkPostService.InitWorkPostDropDownList(this.drpWorkPost, true);
                this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
                this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.setData();
                GetPersonStatistic();               
            }
        }
        public static List<Model.SitePerson_PersonInOut> getAllPersonInOutList;

        /// <summary>
        /// 
        /// </summary>
        private void setData()
        {
            getAllPersonInOutList = null;
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                DateTime? startTime = Funs.GetNewDateTime(this.txtStartDate.Text);
                DateTime? endTime = Funs.GetNewDateTime(this.txtEndDate.Text);                
                var getAll = from x in Funs.DB.SitePerson_PersonInOut
                                            where x.ProjectId == this.ProjectId
                                            select x;
                if (startTime.HasValue)
                {
                    getAll = getAll.Where(x => x.ChangeTime >= startTime);
                }
                if (endTime.HasValue)
                {
                    getAll = getAll.Where(x => x.ChangeTime <= endTime);
                }
                if (this.drpUnit.SelectedValue != Const._Null)
                {
                    getAll = getAll.Where(x => x.UnitId == this.drpUnit.SelectedValue);
                }
                if (this.drpWorkPost.SelectedValue != Const._Null)
                {
                    getAll = getAll.Where(x => x.WorkPostId == this.drpWorkPost.SelectedValue);
                }
                getAllPersonInOutList = getAll.ToList();
            }
        }

        /// <summary>
        /// 获取数据，合并相同行
        /// </summary>
        private void GetPersonStatistic()
        {
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                DateTime? startTime = Funs.GetNewDateTime(this.txtStartDate.Text);
                DateTime? endTime = Funs.GetNewDateTime(this.txtEndDate.Text);
                string unitId = null;
                string workPostId = null;
                if (this.drpUnit.SelectedValue != Const._Null)
                {
                    unitId = this.drpUnit.SelectedValue;
                }
                if (this.drpWorkPost.SelectedValue != Const._Null)
                {
                    workPostId = this.drpWorkPost.SelectedValue;
                }

                var dayReports = PersonInOutService.getWorkPostStatistic(getAllPersonInOutList); 
                DataTable tb = this.LINQToDataTable(dayReports);
                // 2.获取当前分页数据
                //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.setData();
            GetPersonStatistic();             
        }

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetPersonStatistic();
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            GetPersonStatistic();
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("现场岗位人工时统计" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            GetPersonStatistic();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string ConvertWorkTime(object UnitWorkPostID)
        {
            int workTime = 0;
            if (UnitWorkPostID != null)
            {             
                List<string> getUWId = Funs.GetStrListByStr(UnitWorkPostID.ToString(), '|');
                if (getUWId.Count() > 1)
                {
                    string unitId = getUWId[0];
                    string workPostId = getUWId[1];
                    var getW = getAllPersonInOutList.Where(x => x.UnitId == unitId && x.WorkPostId == workPostId);
                    //// 出场记录 集合
                    var getUnitOutList = getW.Where(x => x.IsIn == false);
                    //// 进场记录 集合
                    var getUnitInList = getW.Where(x => x.IsIn == true);
                    int personWorkTime = 0;
                    List<string> personIdList = new List<string>();
                    foreach (var itemOut in getUnitOutList)
                    {
                        var getMaxInTime = getUnitInList.Where(x => x.ChangeTime < itemOut.ChangeTime
                                    && x.PersonId == itemOut.PersonId && x.ChangeTime.Value.AddDays(1) > itemOut.ChangeTime).Max(x => x.ChangeTime);
                        if (getMaxInTime.HasValue)
                        {
                            personWorkTime += Convert.ToInt32((itemOut.ChangeTime - getMaxInTime).Value.TotalMinutes);
                        }
                        else
                        {
                            personIdList.Add(itemOut.PersonId);
                        }
                    }
                    if (personIdList.Count() > 0)
                    {
                        personWorkTime += (personIdList.Distinct().Count() * 8 * 60);
                    }

                    workTime = Convert.ToInt32(personWorkTime * 1.0 / 60);
                }
            }
            return workTime.ToString();
        }
    }
}