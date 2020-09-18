using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessReportItem : PageBase
    {
        #region 定义项
        public string hotProessTrustItemId
        {
            get
            {
                return (string)ViewState["hotProessTrustItemId"];
            }
            set
            {
                ViewState["hotProessTrustItemId"] = value;
            }
        }
        public string WeldJointId
        {
            get
            {
                return (string)ViewState["WeldJointId"];
            }
            set
            {
                ViewState["WeldJointId"] = value;
            }
        }
        /// <summary>
        /// 报告集合
        /// </summary>
        private List<Model.HJGL_HotProess_Report> HotProessReportList = new List<Model.HJGL_HotProess_Report>();
        #endregion
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
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.hotProessTrustItemId = Request.Params["HotProessTrustItemId"];
                var hotProessFeedback = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hotProessTrustItemId);
                if (hotProessFeedback != null) {
                    if (!string.IsNullOrEmpty(hotProessFeedback.IsCompleted.ToString())) {
                        drpIsCompleted.SelectedValue = hotProessFeedback.IsCompleted.Value.ToString();
                    }
                    if (!string.IsNullOrEmpty(hotProessFeedback.WeldJointId)) {
                        var getWeldJoint = WeldJointService.GetViewWeldJointById(hotProessFeedback.WeldJointId);
                        if (getWeldJoint != null)
                        {
                            this.WeldJointId = getWeldJoint.WeldJointId;
                            this.lbWeldJointCode.Text = getWeldJoint.WeldJointCode;
                            var getPipeline = PipelineService.GetPipelineByPipelineId(getWeldJoint.PipelineId);
                            if (getPipeline != null)
                            {
                                this.lbPipeLineCode.Text = getPipeline.PipelineCode;
                            }
                        }
                    }
                }
                

                //// 绑定表格
                this.BindGrid();
            }
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var getReports = HotProessReportService.GetHotProessReportListById(hotProessTrustItemId);
            foreach (var item in getReports)
            {
                Model.HJGL_HotProess_Report NewReport = new Model.HJGL_HotProess_Report();
                NewReport.HotProessReportId = item.HotProessReportId;
                NewReport.HotProessTrustItemId = item.HotProessTrustItemId;
                NewReport.WeldJointId = item.WeldJointId;
                NewReport.PointCount = item.PointCount;
                NewReport.RequiredT = item.RequiredT;
                NewReport.ActualT = item.ActualT;
                NewReport.RequestTime = item.RequestTime;
                NewReport.ActualTime = item.ActualTime;
                NewReport.RecordChartNo = item.RecordChartNo;
                HotProessReportList.Add(NewReport);
            }
            Grid1.DataSource = HotProessReportList;
            Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
        
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessReportMenuId, Const.BtnAdd))
            {
                HotProessReportList = GetDetails();
                Model.HJGL_HotProess_Report NewReport = new Model.HJGL_HotProess_Report();
                NewReport.HotProessReportId = SQLHelper.GetNewID(typeof(Model.HJGL_HotProess_Report));
                NewReport.HotProessTrustItemId = hotProessTrustItemId;
                NewReport.WeldJointId =WeldJointId;
                HotProessReportList.Add(NewReport);
                Grid1.DataSource = HotProessReportList;
                Grid1.DataBind();
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        private List<Model.HJGL_HotProess_Report> GetDetails()
        {
            HotProessReportList.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string PointCount = values.Value<string>("PointCount");
                string RequiredT = values.Value<string>("RequiredT");
                string ActualT = values.Value<string>("ActualT");
                string RequestTime = values.Value<string>("RequestTime");
                string ActualTime = values.Value<string>("ActualTime");
                string RecordChartNo = values.Value<string>("RecordChartNo");
                Model.HJGL_HotProess_Report NewReport = new Model.HJGL_HotProess_Report();
                NewReport.HotProessReportId = Grid1.Rows[i].DataKeys[0].ToString(); 
                NewReport.HotProessTrustItemId = hotProessTrustItemId;
                NewReport.WeldJointId = Grid1.Rows[i].DataKeys[1].ToString();
                if (!string.IsNullOrEmpty(PointCount))
                {
                    NewReport.PointCount = Convert.ToInt32(PointCount);
                }
                NewReport.RequiredT = RequiredT;
                NewReport.ActualT = ActualT;
                NewReport.RequestTime = RequestTime;
                NewReport.ActualTime = ActualTime;
                NewReport.RecordChartNo = RecordChartNo;
                HotProessReportList.Add(NewReport);
            }
            return HotProessReportList;
        }
        #endregion
        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string hotProessTrustItemId = Request.Params["HotProessTrustItemId"];
            var hotProessFeedback = BLL.HotProessTrustItemService.GetHotProessTrustItemById(hotProessTrustItemId);

            hotProessFeedback.IsCompleted = Convert.ToBoolean(drpIsCompleted.SelectedValue);
            hotProessFeedback.IsHardness = Convert.ToBoolean(drpIsCompleted.SelectedValue);
            
            BLL.HotProessTrustItemService.UpdateHotProessFeedback(hotProessFeedback);
            ///保存热处理报告
            HotProessReportService.DeleteAllHotProessReportById(hotProessTrustItemId);
            HotProessReportList = GetDetails();
            foreach (var item in HotProessReportList)
            {
                Model.HJGL_HotProess_Report NewReport = new Model.HJGL_HotProess_Report();
                NewReport.HotProessReportId = item.HotProessReportId;
                NewReport.HotProessTrustItemId = item.HotProessTrustItemId;
                NewReport.WeldJointId = item.WeldJointId;
                NewReport.PointCount = item.PointCount;
                NewReport.RequiredT = item.RequiredT;
                NewReport.ActualT = item.ActualT;
                NewReport.RequestTime = item.RequestTime;
                NewReport.ActualTime = item.ActualTime;
                NewReport.RecordChartNo = item.RecordChartNo;
                BLL.HotProessReportService.AddHotProessReport(NewReport);
            }
            Alert.ShowInTop("保存成功", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion
        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessReportMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var hotProessReport = BLL.HotProessReportService.GetHotProessReportById(rowID);
                        if (hotProessReport != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.AttachFileService.DeleteAttachFile(Funs.RootPath, rowID, Const.HJGL_HotProessReportMenuId);//删除附件
                                BLL.HotProessReportService.DeleteHotProessReportById(rowID);
                            }
                        }
                    }

                    BindGrid();
                    if (!string.IsNullOrEmpty(strShowNotify))
                    {
                        Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            //if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id) != null)
            //{
            //    content += "已在【项目用户】中使用，不能删除！";
            //}            

            return content;
        }
        #endregion
        #endregion

        #region 行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string hotProessReportId = Grid1.DataKeys[e.RowIndex][0].ToString();
            HotProessReportList = GetDetails();
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGL/HotProcessHard&menuId={1}&edit=0", hotProessReportId, BLL.Const.HJGL_HotProessReportMenuId)));
            }
            if (e.CommandName == "del")//删除
            {
               var Report = HotProessReportList.FirstOrDefault(x => x.HotProessReportId == hotProessReportId);
                if (Report != null)
                {
                    HotProessReportList.Remove(Report);
                }
                this.Grid1.DataSource = HotProessReportList;
                this.Grid1.DataBind();
            }
        }
        #endregion
    }
}