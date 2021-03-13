using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HardReportItem : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HardTrustItemID
        {
            get
            {
                return (string)ViewState["HardTrustItemID"];
            }
            set
            {
                ViewState["HardTrustItemID"] = value;
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
        private List<Model.HJGL_View_HardReportItem> HardReportList = new List<Model.HJGL_View_HardReportItem>();
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
                HardTrustItemID = Request.Params["HardTrustItemID"];
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                var hardFeedback = BLL.Hard_TrustItemService.GetHardTrustItemById(HardTrustItemID);
                if (hardFeedback != null)
                {
                    if (hardFeedback.IsPass == true)
                    {
                        drpIsPass.SelectedValue = "1";
                    }
                    else if (hardFeedback.IsPass == false)
                    {
                        drpIsPass.SelectedValue = "0";
                    }
                    else
                    {
                        drpIsPass.SelectedValue = "2";
                    }
                    if (!string.IsNullOrEmpty(hardFeedback.WeldJointId))
                    {
                        var getWeldJoint = WeldJointService.GetViewWeldJointById(hardFeedback.WeldJointId);
                        if (getWeldJoint != null)
                        {
                            this.WeldJointId = getWeldJoint.WeldJointId;
                            this.lbWeldJointCode.Text = getWeldJoint.WeldJointCode;
                            var getPipeline = PipelineService.GetPipelineByPipelineId(getWeldJoint.PipelineId);
                            if (getPipeline != null)
                            {
                                this.lbPipeLineCode.Text = getPipeline.PipelineCode;
                                var getUnitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(getPipeline.UnitWorkId);
                                if (getUnitWork != null)
                                {
                                    this.lbUnitWork.Text = getUnitWork.UnitWorkName;
                                }
                            }
                        }
                    }
                }
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            //var getReports = Hard_ReportService.GetHardReportListById(HardTrustItemID);
            //foreach (var item in getReports)
            //{
            //    Model.HJGL_Hard_Report NewReport = new Model.HJGL_Hard_Report();
            //    NewReport.HardReportId = item.HardReportId;
            //    NewReport.HardTrustItemID = item.HardTrustItemID;
            //    NewReport.WeldJointId = item.WeldJointId;
            //    NewReport.TestingPointNo = item.TestingPointNo;
            //    NewReport.HardNessValue1 = item.HardNessValue1;
            //    NewReport.HardNessValue2 = item.HardNessValue2;
            //    NewReport.HardNessValue3 = item.HardNessValue3;
            //    NewReport.Remark = item.Remark;
            //    HardReportList.Add(NewReport);
            //}
            string strSql = @"select * from HJGL_View_HardReportItem where HardTrustItemID=@HardTrustItemID order by SortIndex";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@HardTrustItemID", HardTrustItemID));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotHardReportMenuId, Const.BtnAdd))
            {
                HardReportList = GetDetails();
                Model.HJGL_View_HardReportItem NewReport = new Model.HJGL_View_HardReportItem();
                NewReport.HardReportId = SQLHelper.GetNewID(typeof(Model.HJGL_Hard_Report));
                NewReport.HardTrustItemID = HardTrustItemID;
                NewReport.WeldJointId = WeldJointId;
                HardReportList.Add(NewReport);
                Grid1.DataSource = HardReportList;
                Grid1.DataBind();
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        private List<Model.HJGL_View_HardReportItem> GetDetails()
        {
            HardReportList.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                string TestingPointNo = values.Value<string>("TestingPointNo");
                string HardNessValue1 = values.Value<string>("HardNessValue1");
                string HardNessValue2 = values.Value<string>("HardNessValue2");
                string HardNessValue3 = values.Value<string>("HardNessValue3");
                string Remark = values.Value<string>("Remark");
                Model.HJGL_View_HardReportItem NewReport = new Model.HJGL_View_HardReportItem();
                NewReport.HardReportId = Grid1.Rows[i].DataKeys[0].ToString();
                NewReport.HardTrustItemID = HardTrustItemID;
                NewReport.WeldJointId = Grid1.Rows[i].DataKeys[1].ToString();
                NewReport.TestingPointNo = TestingPointNo;
                if (!string.IsNullOrEmpty(HardNessValue1))
                {
                    NewReport.HardNessValue1 = Convert.ToInt32(HardNessValue1);
                }
                if (!string.IsNullOrEmpty(HardNessValue2))
                {
                    NewReport.HardNessValue2 = Convert.ToInt32(HardNessValue2);
                }
                if (!string.IsNullOrEmpty(HardNessValue3))
                {
                    NewReport.HardNessValue3 = Convert.ToInt32(HardNessValue3);
                }
                NewReport.Remark = Remark;
                HardReportList.Add(NewReport);
            }
            return HardReportList;
        }
        #endregion
        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string hardTrustItemId = Request.Params["HardTrustItemID"];
            var hardFeedback = BLL.Hard_TrustItemService.GetHardTrustItemById(hardTrustItemId);
            if (hardFeedback != null)
            {
                var hotProessTrustItem = Funs.DB.HJGL_HotProess_TrustItem.FirstOrDefault(x => x.WeldJointId == hardFeedback.WeldJointId && x.HotProessTrustItemId == hardFeedback.HotProessTrustItemId);
                if (drpIsPass.SelectedValue == "1")
                {
                    hardFeedback.IsPass = true;
                    if (hotProessTrustItem != null)   //更新热处理委托硬度不合格记录id为空
                    {
                        hotProessTrustItem.HardTrustItemID = null;
                    }
                    hotProessTrustItem.IsPass = true;
                }
                else if (drpIsPass.SelectedValue == "0")
                {
                    hardFeedback.IsPass = false;
                    if (hotProessTrustItem != null)    //更新热处理委托硬度不合格记录id值
                    {
                        hotProessTrustItem.HardTrustItemID = hardFeedback.HardTrustItemID;
                    }
                    hotProessTrustItem.IsPass = false;
                }
                else  // 待检测
                {
                    hardFeedback.IsPass = null;
                    hotProessTrustItem.HardTrustItemID = null;
                    hotProessTrustItem.IsPass = null;
                }
                BLL.HotProessTrustItemService.UpdateHotProessTrustItem(hotProessTrustItem);
                BLL.Hard_TrustItemService.UpdateHardTrustItem(hardFeedback);
                ///保存硬度检测报告
                Hard_ReportService.DeleteHard_ReportsByHardTrustItemID(hardTrustItemId);
                HardReportList = GetDetails();
                var num = 1;
                foreach (var item in HardReportList)
                {
                    Model.HJGL_Hard_Report NewReport1 = new Model.HJGL_Hard_Report();
                    NewReport1.HardReportId = item.HardReportId;
                    NewReport1.HardTrustItemID = item.HardTrustItemID;
                    NewReport1.WeldJointId = item.WeldJointId;
                    NewReport1.TestingPointNo = item.TestingPointNo;
                    NewReport1.HardNessValue1 = item.HardNessValue1;
                    NewReport1.Remark = item.Remark;
                    NewReport1.SortIndex = num;
                    NewReport1.IsShow = true;
                    BLL.Hard_ReportService.AddHard_Report(NewReport1);
                    Model.HJGL_Hard_Report NewReport2 = new Model.HJGL_Hard_Report();
                    NewReport2.HardReportId = SQLHelper.GetNewID(typeof(Model.HJGL_Hard_Report));
                    NewReport2.HardTrustItemID = item.HardTrustItemID;
                    NewReport2.WeldJointId = item.WeldJointId;
                    NewReport2.TestingPointNo = item.TestingPointNo;
                    NewReport2.HardNessValue1 = item.HardNessValue2;
                    NewReport2.Remark = item.Remark;
                    NewReport2.SortIndex = num;
                    NewReport2.IsShow = false;
                    BLL.Hard_ReportService.AddHard_Report(NewReport2);
                    Model.HJGL_Hard_Report NewReport3 = new Model.HJGL_Hard_Report();
                    NewReport3.HardReportId = SQLHelper.GetNewID(typeof(Model.HJGL_Hard_Report));
                    NewReport3.HardTrustItemID = item.HardTrustItemID;
                    NewReport3.WeldJointId = item.WeldJointId;
                    NewReport3.TestingPointNo = item.TestingPointNo;
                    NewReport3.HardNessValue1 = item.HardNessValue3;
                    NewReport3.Remark = item.Remark;
                    NewReport3.SortIndex = num;
                    BLL.Hard_ReportService.AddHard_Report(NewReport3);
                    num++;
                }
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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotHardReportMenuId, Const.BtnAdd))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    string strShowNotify = string.Empty;
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var hardReport = BLL.Hard_ReportService.GetHardReportByHardReportId(rowID);
                        if (hardReport != null)
                        {
                            string cont = judgementDelete(rowID);
                            if (string.IsNullOrEmpty(cont))
                            {
                                BLL.AttachFileService.DeleteAttachFile(Funs.RootPath, rowID, Const.HJGL_HotHardReportMenuId);//删除附件
                                BLL.Hard_ReportService.DeleteHard_ReportByHardReportId(rowID);
                                //BLL.Sys_LogService.AddLog(BLL.Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Resources.Lan.DeleteHardnessReport);
                            }
                            else
                            {
                                strShowNotify += "硬度报告" + "：" + hardReport.TestingPointNo + cont;
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
            //if (Funs.DB.Project_HJGL_HotHardReport.FirstOrDefault(x => x.HardReportId == id) != null)
            //{
            //    content += "已在【硬度报告】中使用，不能删除！";
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
            string hardReportId = Grid1.DataKeys[e.RowIndex][0].ToString();
            HardReportList = GetDetails();
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGL/HotProcessHard&menuId={1}&edit=0", hardReportId, BLL.Const.HJGL_HotHardReportMenuId)));
            }
            if (e.CommandName == "del")//删除
            {
                var Report = HardReportList.FirstOrDefault(x => x.HardReportId == hardReportId);
                if (Report != null)
                {
                    HardReportList.Remove(Report);
                }
                this.Grid1.DataSource = HardReportList;
                this.Grid1.DataBind();
            }
        }
        #endregion
    }
}