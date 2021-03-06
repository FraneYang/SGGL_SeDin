﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.WeldingProcess.WeldingManage
{
    public partial class WeldReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 焊接日报主键
        /// </summary>
        public string WeldingDailyId
        {
            get
            {
                return (string)ViewState["WeldingDailyId"];
            }
            set
            {
                ViewState["WeldingDailyId"] = value;
            }
        }

        /// <summary>
        /// 单位工程
        /// </summary>
        public string UnitWorkId
        {
            get
            {
                return (string)ViewState["UnitWorkId"];
            }
            set
            {
                ViewState["UnitWorkId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.WeldingDailyId = Request.Params["WeldingDailyId"];
                ///焊口属性
                //this.drpJointAttribute.DataTextField = "Text";
                //this.drpJointAttribute.DataValueField = "Value";
                //this.drpJointAttribute.DataSource = BLL.DropListService.HJGL_JointAttribute();
                //this.drpJointAttribute.DataBind();
                this.PageInfoLoad(); // 加载页面 

                if (!string.IsNullOrEmpty(this.WeldingDailyId))
                {
                    List<Model.SpWeldingDailyItem> GetWeldingDailyItem = BLL.WeldingDailyService.GetWeldingDailyItem(this.WeldingDailyId);
                    this.BindGrid(GetWeldingDailyItem);  // 初始化页面
                }
                else
                {
                    this.BindGrid(null);
                }
                this.CalculationAmount();
            }
        }
        #endregion

        #region 超量焊工提示
        /// <summary>
        ///  超量焊工提示
        /// </summary>
        private void CalculationAmount()
        {
            this.lbAmount.Hidden = true;
            this.lbAmount.Text = string.Empty;
            DateTime? date = Funs.GetNewDateTime(this.txtWeldingDate.Text);
            string txtValue = string.Empty;
            if (date.HasValue)
            {
                //var weldJoints = BLL.WeldJointService.GetWeldlinesByWeldingDailyId(this.WeldingDailyId);
                //foreach (var item in weldJoints)
                //{
                //    bool cWelder = BLL.Pipeline_WeldJointService.GetWelderLimitDN(this.ProjectId, item.CoverWelderId, date.Value);
                //    bool bWelder = cWelder;
                //    if (item.BackingWelderId != item.CoverWelderId)
                //    {
                //        bWelder = BLL.Pipeline_WeldJointService.GetWelderLimitDN(this.ProjectId, item.BackingWelderId, date.Value);
                //    }
                //    if (cWelder || bWelder)
                //    {
                //        if (cWelder)
                //        {
                //            var coverWelder = BLL.WelderService.GetWelderById(item.CoverWelderId);
                //            if (coverWelder != null)
                //            {
                //                string txt = coverWelder.WelderCode + "；";
                //                if (!txtValue.Contains(txt))
                //                {
                //                    txtValue += txt;
                //                }
                //            }
                //        }
                //        if (bWelder)
                //        {
                //            var floorWelder = BLL.WelderService.GetWelderById(item.BackingWelderId);
                //            if (floorWelder != null)
                //            {
                //                string txt = floorWelder.WelderCode + "；";
                //                if (!txtValue.Contains(txt))
                //                {
                //                    txtValue += txt;
                //                }
                //            }
                //        }
                //    }
                //}
            }
            if (!string.IsNullOrEmpty(txtValue))
            {
                this.lbAmount.Text = txtValue;
                this.lbAmount.Hidden = false;
            }
        }
        #endregion

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);
            BLL.UnitWorkService.InitUnitWorkDropDownList(this.drpUnitWork, this.CurrUser.LoginProjectId, true);
            var report = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(this.WeldingDailyId);

            if (report != null)
            {
                this.txtWeldingDailyCode.Text = report.WeldingDailyCode;
                if (!string.IsNullOrEmpty(report.UnitId))
                {
                    this.drpUnit.SelectedValue = report.UnitId;
                }
                if (!string.IsNullOrEmpty(report.UnitWorkId))
                {
                    this.drpUnitWork.SelectedValue = report.UnitWorkId;
                }

                this.UnitWorkId = report.UnitWorkId;

                this.txtWeldingDate.Text = string.Format("{0:yyyy-MM-dd}", report.WeldingDate);
                this.hdTablerId.Text = report.Tabler;
                Model.Sys_User tabler = BLL.UserService.GetUserByUserId(report.Tabler);
                if (tabler != null)
                {
                    this.txtTabler.Text = tabler.UserName;
                }
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", report.TableDate);
                this.txtRemark.Text = report.Remark;
            }
            else
            {
                this.UnitWorkId = Request.Params["unitWorkId"];
                if (!string.IsNullOrEmpty(UnitWorkId))
                {
                    var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(UnitWorkId);
                    drpUnit.SelectedValue = w.UnitId;
                    this.drpUnitWork.SelectedValue = w.UnitWorkId;
                }

                this.SimpleForm1.Reset(); ///重置所有字段
                this.txtTabler.Text = this.CurrUser.UserName;
                this.hdTablerId.Text = this.CurrUser.UserId;

                //txtWeldingDate.MaxDate = DateTime.Now;
                this.txtWeldingDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                string perfix = string.Format("{0:yyyyMMdd}", System.DateTime.Now) + "-";
                this.txtWeldingDailyCode.Text = BLL.SQLHelper.RunProcNewId("SpGetThreeNumber", "dbo.HJGL_WeldingDaily", "WeldingDailyCode", this.CurrUser.LoginProjectId, perfix);
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid(List<Model.SpWeldingDailyItem> weldingDailyItem)
        {
            DataTable dt = null;
            //if (weldingDailyItem != null) 
            //{
            //    var task = from x in Funs.DB.View_HJGL_WeldingTask
            //               where (x.UnitWorkId == this.UnitWorkId && x.TaskDate.Value.Date == Convert.ToDateTime(txtWeldingDate.Text)
            //               && x.WeldingDailyId == null && x.CoverWelderId != null && x.BackingWelderId != null) || x.WeldingDailyId == this.WeldingDailyId
            //               select x;
            //    dt = this.LINQToDataTable(task);
            //}
            //else
            //{
            //    var task = from x in Funs.DB.View_HJGL_WeldingTask
            //               where x.UnitWorkId == this.UnitWorkId && x.TaskDate.Value.Date == Convert.ToDateTime(txtWeldingDate.Text)
            //               && x.WeldingDailyId == null && x.CoverWelderId != null && x.BackingWelderId != null
            //               select x;
            //    dt = this.LINQToDataTable(task);
            //}
            var list = from x in Funs.DB.View_HJGL_WeldingTask where x.ProjectId == this.CurrUser.LoginProjectId select x;
            if (weldingDailyItem != null)
            {
                var task = new List<Model.View_HJGL_WeldingTask>();
                var weldJointIds = (from x in list
                                    where (x.UnitWorkId == this.UnitWorkId && x.TaskDate.Value.Date <= Convert.ToDateTime(txtWeldingDate.Text)
                           && x.WeldingDailyId == null && x.CoverWelderId != null && x.BackingWelderId != null) || x.WeldingDailyId == this.WeldingDailyId
                                    select x.WeldJointId).Distinct().ToList();
                foreach (var weldJointId in weldJointIds)
                {
                    task.Add(list.FirstOrDefault(x => x.WeldJointId == weldJointId));
                }
                dt = this.LINQToDataTable(task);
            }
            else
            {
                var task = new List<Model.View_HJGL_WeldingTask>();
                var weldJointIds = (from x in list
                                    where x.UnitWorkId == this.UnitWorkId && x.TaskDate.Value.Date <= Convert.ToDateTime(txtWeldingDate.Text)
                           && x.WeldingDailyId == null && x.CoverWelderId != null && x.BackingWelderId != null
                                    select x.WeldJointId).Distinct().ToList();
                foreach (var weldJointId in weldJointIds)
                {
                    task.Add(list.FirstOrDefault(x => x.WeldJointId == weldJointId && x.CoverWelderId != null));
                }
                dt = this.LINQToDataTable(task);
            }
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            Grid1.RecordCount = dt.Rows.Count;
            // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, dt);

            Grid1.DataSource = table;
            Grid1.DataBind();
            if (weldingDailyItem != null)
            {
                var task = from x in Funs.DB.View_HJGL_WeldingTask
                           where x.WeldingDailyId == this.WeldingDailyId
                           select x;
                var weldTaskIds = task.Select(x => x.WeldTaskId).ToArray();
                this.Grid1.SelectedRowIDArray = weldTaskIds;
            }
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                var hotProesssItem = Funs.DB.HJGL_HotProess_TrustItem.FirstOrDefault(x => x.WeldJointId == this.Grid1.Rows[i].DataKeys[1].ToString());
                if (hotProesssItem != null)   //已生成热处理委托，不能编辑或删除
                {
                    Grid1.Rows[i].RowSelectable = false;
                }
                var pointBatchItem = Funs.DB.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.WeldJointId == this.Grid1.Rows[i].DataKeys[1].ToString() && x.PointState != null);
                if (pointBatchItem != null)   //已生成委托，不能编辑或删除
                {
                    Grid1.Rows[i].RowSelectable = false;
                }
            }
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            //List<Model.SpWeldingDailyItem> GetWeldingDailyItem = this.CollectGridJointInfo();
            this.BindGrid(null);
        }
        #endregion

        //private static bool canSave;  //是否可以保存

        #region 焊接日报 提交事件
        /// <summary>
        /// 编辑焊接日报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldTaskMenuId, Const.BtnSave))
            {
                if (BLL.WeldingDailyService.IsExistWeldingDailyCode(this.txtWeldingDailyCode.Text, !string.IsNullOrEmpty(this.WeldingDailyId) ? this.WeldingDailyId : "", CurrUser.LoginProjectId))
                {
                    ShowNotify("日报编号已存在，请重新录入", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.txtWeldingDate.Text) || string.IsNullOrEmpty(this.txtWeldingDailyCode.Text.Trim()))
                {
                    ShowNotify("日报告号、焊接日期不能为空", MessageBoxIcon.Warning);
                    return;
                }

                Model.HJGL_WeldingDaily newWeldingDaily = new Model.HJGL_WeldingDaily();
                newWeldingDaily.WeldingDailyCode = this.txtWeldingDailyCode.Text.Trim();
                newWeldingDaily.ProjectId = CurrUser.LoginProjectId;
                newWeldingDaily.UnitWorkId = this.UnitWorkId;
                if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
                {
                    newWeldingDaily.UnitWorkId = this.drpUnitWork.SelectedValue;
                }
                newWeldingDaily.UnitId = this.drpUnit.SelectedValue;

                DateTime? weldDate = Funs.GetNewDateTime(this.txtWeldingDate.Text);
                if (weldDate.HasValue)
                {
                    newWeldingDaily.WeldingDate = weldDate.Value;
                }
                else
                {
                    newWeldingDaily.WeldingDate = System.DateTime.Now;
                }
                newWeldingDaily.Tabler = this.hdTablerId.Text;
                newWeldingDaily.TableDate = Funs.GetNewDateTime(this.txtTableDate.Text);
                newWeldingDaily.Remark = this.txtRemark.Text.Trim();
                //List<Model.SpWeldingDailyItem> GetWeldingDailyItem = this.CollectGridJointInfo();
                string errlog = string.Empty;
                var weldJointView = (from x in BLL.Funs.DB.View_HJGL_WeldJoint where x.WeldingDailyId == this.WeldingDailyId orderby x.PipelineCode, x.WeldJointCode select x).ToList();

                string eventArg = string.Empty;

                #region 焊工资质判断这里取消
                //foreach (var item in GetWeldingDailyItem)
                //{
                //    bool canSave = false;
                //    var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(item.WeldJointId);
                //    var joty = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(jot.WeldTypeId);
                //    string weldType = string.Empty;
                //    if (joty != null && joty.WeldTypeCode.Contains("B"))
                //    {
                //        weldType = "对接焊缝";
                //    }
                //    else
                //    {
                //        weldType = "角焊缝";
                //    }
                //    string floorWelder = item.BackingWelderId;
                //    string cellWelder = item.CoverWelderId;
                //    decimal? dia = item.Dia;
                //    decimal? sch = Funs.GetNewDecimal(item.Thickness.HasValue ? item.Thickness.Value.ToString() : "");
                //    string wmeCode = string.Empty;
                //    var wm = BLL.Base_WeldingMethodService.GetWeldingMethodByWeldingMethodId(jot.WeldingMethodId);
                //    if (wm != null)
                //    {
                //        wmeCode = wm.WeldingMethodCode;
                //    }
                //    string[] wmeCodes = wmeCode.Split('+');
                //    //string location = item.JOT_Location;
                //    string ste = jot.Material1Id;
                //    string jointAttribute = jot.JointAttribute;

                //    List<Model.Welder_WelderQualify> floorWelderQualifys = (from x in Funs.DB.Welder_WelderQualify
                //                                                            where x.WelderId == floorWelder && x.WeldingMethod != null
                //                                                                           && x.MaterialType != null && x.WeldType != null
                //                                                                           && x.ThicknessMax != null && x.SizesMin != null
                //                                                            select x).ToList();

                //    List<Model.Welder_WelderQualify> cellWelderQualifys = (from x in Funs.DB.Welder_WelderQualify
                //                                                           where x.WelderId == cellWelder && x.WeldingMethod != null
                //                                                                 && x.MaterialType != null && x.WeldType != null
                //                                                                       && x.ThicknessMax != null && x.SizesMin != null
                //                                                           select x).ToList();
                //    // 打底和盖面同一焊工
                //    if (floorWelder == cellWelder)
                //    {
                //        if (floorWelderQualifys != null && floorWelderQualifys.Count() > 0)
                //        {
                //            if (wmeCodes.Count() <= 1) // 一种焊接方法
                //            {
                //                canSave = OneWmeIsOK(floorWelderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                //            }
                //            else  // 大于一种焊接方法，如氩电联焊
                //            {
                //                canSave = TwoWmeIsOK(floorWelderQualifys, cellWelderQualifys, wmeCodes[0], wmeCodes[1], jointAttribute, weldType, ste, dia, sch);
                //            }
                //        }
                //    }
                //    // 打底和盖面焊工不同
                //    else
                //    {
                //        bool isok1 = false;
                //        bool isok2 = false;

                //        if (wmeCodes.Count() <= 1) // 一种焊接方法
                //        {
                //            if (floorWelderQualifys != null && floorWelderQualifys.Count() > 0)
                //            {
                //                isok1 = OneWmeIsOK(floorWelderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                //            }
                //            if (cellWelderQualifys != null && cellWelderQualifys.Count() > 0)
                //            {
                //                isok2 = OneWmeIsOK(cellWelderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                //            }
                //            if (isok1 && isok2)
                //            {
                //                canSave = true;
                //            }
                //        }
                //        else
                //        {

                //            canSave = TwoWmeIsOK(floorWelderQualifys, cellWelderQualifys, wmeCodes[0], wmeCodes[1], jointAttribute, weldType, ste, dia, sch);
                //        }
                //    }

                //    if (canSave == false)
                //    {
                //        eventArg = eventArg + jot.WeldJointCode + ",";
                //    }
                //}
                #endregion

                if (eventArg == string.Empty)  //焊工焊接的所有焊口资质都符合要求）
                {
                    if (!string.IsNullOrEmpty(this.WeldingDailyId))
                    {
                        newWeldingDaily.WeldingDailyId = this.WeldingDailyId;
                        BLL.WeldingDailyService.UpdateWeldingDaily(newWeldingDaily);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnModify, this.WeldingDailyId);
                    }
                    else
                    {
                        this.WeldingDailyId = SQLHelper.GetNewID(typeof(Model.HJGL_WeldingDaily));
                        newWeldingDaily.WeldingDailyId = this.WeldingDailyId;
                        BLL.WeldingDailyService.AddWeldingDaily(newWeldingDaily);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnAdd, this.WeldingDailyId);
                    }

                    // 获取组批条件
                    var batchC = BLL.Project_SysSetService.GetSysSetBySetId("5", CurrUser.LoginProjectId);
                    if (batchC != null)
                    {
                        string batchCondition = batchC.SetValue;
                        // 新建日报
                        if (weldJointView.Count() == 0)
                        {
                            foreach (string row in Grid1.SelectedRowIDArray)
                            {

                                var t = BLL.WeldTaskService.GetWeldTaskById(row);
                                errlog += InsertWeldingDailyItem(t.WeldJointId, t.CoverWelderId, t.BackingWelderId, t.JointAttribute, newWeldingDaily.WeldingDate, batchCondition, true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Grid1.Rows.Count; i++)
                            {
                                if (Grid1.SelectedRowIDArray.Contains(Grid1.Rows[i].RowID))
                                {
                                    var t = BLL.WeldTaskService.GetWeldTaskById(Grid1.Rows[i].RowID);
                                    errlog += InsertWeldingDailyItem(t.WeldJointId, t.CoverWelderId, t.BackingWelderId, t.JointAttribute, newWeldingDaily.WeldingDate, batchCondition, true);
                                }
                                else
                                {
                                    var t = BLL.WeldTaskService.GetWeldTaskById(Grid1.Rows[i].RowID);
                                    errlog += DeleteWeldingDailyItem(t.WeldJointId, t.CoverWelderId, t.BackingWelderId, t.JointAttribute, newWeldingDaily.WeldingDate, batchCondition, true);
                                }
                            }
                        }
                        // 日报已存在的情况  暂时
                        //else
                        //{
                        //    var weldJoints = from x in weldJointView select x.WeldJointId;
                        //    foreach (var item in GetWeldingDailyItem)
                        //    {
                        //        // 如日报明细存在则只更新焊口信息，如进批条件改变，则只有删除后再重新增加
                        //        if (weldJoints.Contains(item.WeldJointId))
                        //        {
                        //            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(item.WeldJointId);
                        //            newWeldJoint.WeldingDailyId = this.WeldingDailyId;
                        //            newWeldJoint.WeldingDailyCode = this.txtWeldingDailyCode.Text.Trim();
                        //            newWeldJoint.CoverWelderId = item.CoverWelderId;
                        //            newWeldJoint.BackingWelderId = item.BackingWelderId;

                        //            if (!string.IsNullOrEmpty(item.JointAttribute))
                        //            {
                        //                newWeldJoint.JointAttribute = item.JointAttribute;

                        //            }
                        //            BLL.WeldJointService.UpdateWeldJoint(newWeldJoint);

                        //            //更新焊口号 修改固定焊口号后 +G
                        //            BLL.WeldJointService.UpdateWeldJointAddG(newWeldJoint.WeldJointId, newWeldJoint.JointAttribute, Const.BtnAdd);

                        //        }
                        //        else
                        //        {
                        //            errlog += InsertWeldingDailyItem(item, newWeldingDaily.WeldingDate, batchCondition, true);
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        errlog += "请设置项目的组批条件";
                    }

                    #region 焊工每天超过60达因的提示（暂不用）

                    // 焊工每天超过60达因的提示（暂不用）
                    //foreach (var item in GetWeldingDailyItem)
                    //{
                    //    if (!string.IsNullOrEmpty(item.CoverWelderId) && !string.IsNullOrEmpty(item.BackingWelderId))
                    //    {
                    //        bool cWelder = BLL.Pipeline_WeldJointService.GetWelderLimitDN(this.ProjectId, item.CoverWelderId, newWeldingDaily.WeldingDate.Value);
                    //        bool bWelder = cWelder;
                    //        if (item.CoverWelderId != item.BackingWelderId)
                    //        {
                    //            bWelder = BLL.Pipeline_WeldJointService.GetWelderLimitDN(this.ProjectId, item.BackingWelderId, newWeldingDaily.WeldingDate.Value);
                    //        }

                    //        if (cWelder || bWelder)
                    //        {
                    //            if (cWelder)
                    //            {
                    //                var coverWelder = BLL.WelderService.GetWelderById(item.CoverWelderId);
                    //                if (coverWelder != null)
                    //                {
                    //                    string txt = Resources.Lan.WelderCode + coverWelder.WelderCode + Resources.Lan.WeldingDiameter;
                    //                    if (!errlog.Contains(txt))
                    //                    {
                    //                        errlog += txt;
                    //                    }
                    //                }
                    //            }

                    //            if (bWelder)
                    //            {
                    //                var backingWelder = BLL.WelderService.GetWelderById(item.BackingWelderId);
                    //                if (backingWelder != null)
                    //                {
                    //                    string txt = Resources.Lan.WelderCode + backingWelder.WelderCode + Resources.Lan.WeldingDiameter;
                    //                    if (!errlog.Contains(txt))
                    //                    {
                    //                        errlog += txt;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    #endregion
                    //更新焊口属性
                    foreach (JObject mergedRow in Grid1.GetMergedData())
                    {
                        JObject values = mergedRow.Value<JObject>("values");
                        int i = mergedRow.Value<int>("index");
                        string rowId = Grid1.Rows[i].RowID;
                        if (this.Grid1.SelectedRowIDArray.Contains(rowId))
                        {
                            var t = BLL.WeldTaskService.GetWeldTaskById(rowId);
                            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(t.WeldJointId);
                            if (newWeldJoint != null)
                            {
                                //if (!string.IsNullOrEmpty(values.Value<string>("JointAttribute").ToString()))
                                //{
                                //    newWeldJoint.JointAttribute = values.Value<string>("JointAttribute").ToString();
                                //}
                                //BLL.WeldJointService.UpdateWeldJoint(newWeldJoint);
                                BLL.WeldJointService.UpdateWeldJointAddG(newWeldJoint.WeldJointId, newWeldJoint.JointAttribute, Const.BtnAdd);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(errlog))
                    {
                        ShowNotify("保存成功！", MessageBoxIcon.Success);
                        PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                    }
                    else
                    {
                        // string okj = ActiveWindow.GetWriteBackValueReference(newWeldReportMain.WeldingDailyId) + ActiveWindow.GetHidePostBackReference();
                        Alert.ShowInTop("保存成功！" + "焊接明细中" + errlog, "提交结果", MessageBoxIcon.Warning);
                        // ShowAlert("焊接明细中" + errlog, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Alert.ShowInTop("焊工无资质焊接的焊口：" + eventArg, "提交结果", MessageBoxIcon.Warning);
                }

            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 焊工资质判断
        /// <summary>
        /// 一种焊接方法资质判断
        /// </summary>
        /// <param name="welderQualifys"></param>
        /// <param name="wmeCode"></param>
        /// <param name="jointAttribute"></param>
        /// <param name="weldType"></param>
        /// <param name="ste"></param>
        /// <param name="dia"></param>
        /// <param name="sch"></param>
        /// <returns></returns>
        private bool OneWmeIsOK(List<Model.Welder_WelderQualify> welderQualifys, string wmeCode, string jointAttribute, string weldType, string ste, decimal? dia, decimal? sch)
        {
            bool isok = false;

            var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(ste);
            var welderQ = from x in welderQualifys
                          where wmeCode.Contains(x.WeldingMethod)
                          && (mat == null || x.MaterialType.Contains(mat.MetalType ?? ""))
                          && x.WeldType.Contains(weldType)
                          select x;

            if (welderQ.Count() > 0)
            {
                if (jointAttribute == "固定口")
                {
                    welderQ = welderQ.Where(x => x.IsCanWeldG == true);
                }
                if (welderQ.Count() > 0)
                {
                    if (weldType == "1") // 1-对接焊缝 2-表示角焊缝，当为角焊缝时，管径和壁厚不限制
                    {
                        var welderDiaQ = welderQ.Where(x => x.SizesMin <= dia || x.SizesMax == 0);

                        if (welderDiaQ.Count() > 0)
                        {
                            var welderThick = welderDiaQ.Where(x => x.ThicknessMax >= sch || x.ThicknessMax == 0);

                            // 只要有一个不限（为0）就通过
                            if (welderThick.Count() > 0)
                            {
                                isok = true;
                            }
                        }
                    }
                    else
                    {
                        isok = true;
                    }
                }
            }

            return isok;
        }
        /// <summary>
        /// 两种焊接方法资质判断
        /// </summary>
        /// <param name="floorWelderQualifys"></param>
        /// <param name="cellWelderQualifys"></param>
        /// <param name="wmeCode1"></param>
        /// <param name="wmeCode2"></param>
        /// <param name="jointAttribute"></param>
        /// <param name="weldType"></param>
        /// <param name="ste"></param>
        /// <param name="dia"></param>
        /// <param name="sch"></param>
        /// <returns></returns>
        private bool TwoWmeIsOK(List<Model.Welder_WelderQualify> floorWelderQualifys, List<Model.Welder_WelderQualify> cellWelderQualifys, string wmeCode1, string wmeCode2, string jointAttribute, string weldType, string ste, decimal? dia, decimal? sch)
        {
            bool isok = false;

            decimal? fThicknessMax = 0;
            decimal? cThicknessMax = 0;

            var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(ste);
            var floorQ = from x in floorWelderQualifys
                         where wmeCode1.Contains(x.WeldingMethod)
                         && (mat == null || x.MaterialType.Contains(mat.MetalType ?? ""))
                         && x.WeldType.Contains(weldType)
                         // && (dia == null || x.SizesMin<=dia)
                         select x;
            var cellQ = from x in cellWelderQualifys
                        where wmeCode2.Contains(x.WeldingMethod)
                         && (mat == null || x.MaterialType.Contains(mat.MetalType ?? ""))
                         && x.WeldType.Contains(weldType)
                        // && (dia == null || x.SizesMin <= dia)
                        select x;
            if (floorQ.Count() > 0 && cellQ.Count() > 0)
            {
                if (jointAttribute == "固定口")
                {
                    floorQ = floorQ.Where(x => x.IsCanWeldG == true);
                    cellQ = cellQ.Where(x => x.IsCanWeldG == true);
                }
                if (floorQ.Count() > 0 && cellQ.Count() > 0)
                {
                    if (weldType == "1") // 1-对接焊缝 2-表示角焊缝，当为角焊缝时，管径和壁厚不限制
                    {
                        var floorDiaQ = floorQ.Where(x => x.SizesMin <= dia || x.SizesMax == 0);
                        var cellDiaQ = cellQ.Where(x => x.SizesMin <= dia || x.SizesMax == 0);

                        if (floorDiaQ.Count() > 0 && cellDiaQ.Count() > 0)
                        {
                            var fThick = floorDiaQ.Where(x => x.ThicknessMax == 0);
                            var cThick = cellDiaQ.Where(x => x.ThicknessMax == 0);

                            // 只要有一个不限（为0）就通过
                            if (fThick.Count() > 0 || cThick.Count() > 0)
                            {
                                isok = true;
                            }

                            else
                            {
                                fThicknessMax = floorQ.Max(x => x.ThicknessMax);
                                cThicknessMax = cellQ.Max(x => x.ThicknessMax);

                                if ((fThicknessMax + cThicknessMax) >= sch)
                                {
                                    isok = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        isok = true;
                    }
                }
            }

            return isok;
        }
        #endregion

        #region 日报明细插入（更新焊口信息），组批等
        /// <summary>
        /// 日报明细插入（更新焊口信息），组批等
        /// </summary>
        /// <param name="item"></param>
        /// <param name="weldingDailyId"></param>
        /// <returns></returns>
        private string InsertWeldingDailyItem(string weldJointId, string coverWelderId, string backingWelderId, string jointAttribute, DateTime? weldingDate, string batchCondition, bool isSave)
        {
            string errlog = string.Empty;
            string[] condition = batchCondition.Split('|');
            var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
            var pipeline = BLL.PipelineService.GetPipelineByPipelineId(newWeldJoint.PipelineId);
            var unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(pipeline.UnitWorkId);
            var unit = BLL.UnitService.GetUnitByUnitId(pipeline.UnitId);
            var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(newWeldJoint.DetectionTypeId);
            var ndtr = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(pipeline.DetectionRateId);

            if (newWeldJoint != null && string.IsNullOrEmpty(newWeldJoint.WeldingDailyId))
            {
                if (!string.IsNullOrEmpty(coverWelderId) && !string.IsNullOrEmpty(backingWelderId))
                {
                    if (isSave)
                    {
                        newWeldJoint.WeldingDailyId = this.WeldingDailyId;
                        newWeldJoint.WeldingDailyCode = this.txtWeldingDailyCode.Text.Trim();
                        newWeldJoint.CoverWelderId = coverWelderId;
                        newWeldJoint.BackingWelderId = backingWelderId;
                        //if (item.WeldingLocationId != Const._Null)
                        //{
                        //    newWeldJoint.WeldingLocationId = item.WeldingLocationId;
                        //}
                        newWeldJoint.JointAttribute = jointAttribute;
                        BLL.WeldJointService.UpdateWeldJoint(newWeldJoint);

                        // 更新焊口号 修改固定焊口号后 +G
                        BLL.WeldJointService.UpdateWeldJointAddG(newWeldJoint.WeldJointId, newWeldJoint.JointAttribute, Const.BtnAdd);

                        // 进批
                        //BLL.Batch_PointBatchItemService.InsertPointBatch(this.ProjectId, this.drpUnit.SelectedValue, this.drpUnitWork.SelectedValue, item.CoverWelderId, item.WeldJointId, weldingDate);
                        bool isPass = true;
                        foreach (string c in condition)
                        {
                            if (c == "1")
                            {
                                if (string.IsNullOrEmpty(pipeline.UnitWorkId))
                                {
                                    isPass = false;
                                    break;

                                }
                            }
                            if (c == "2")
                            {
                                if (string.IsNullOrEmpty(pipeline.UnitId))
                                {
                                    isPass = false;
                                    break;

                                }
                            }
                            if (c == "3")
                            {
                                if (string.IsNullOrEmpty(newWeldJoint.DetectionTypeId))
                                {
                                    isPass = false;
                                    break;
                                }
                            }
                            if (c == "4")
                            {
                                if (string.IsNullOrEmpty(pipeline.DetectionRateId))
                                {
                                    isPass = false;
                                    break;
                                }
                            }
                            if (c == "5")
                            {
                                if (string.IsNullOrEmpty(pipeline.PipingClassId))
                                {
                                    isPass = false;
                                    break;
                                }
                            }
                            // 6是管线，7是焊工都不可能为空，这里就不判断了
                        }

                        if (isPass)
                        {
                            string strSql = @"SELECT PointBatchId FROM dbo.HJGL_Batch_PointBatch
                                                 WHERE (EndDate IS NULL OR EndDate ='')
                                                  AND ProjectId = @ProjectId 
                                                  AND UnitWorkId = @UnitWorkId AND UnitId =@UnitId 
                                                  AND DetectionTypeId =@DetectionTypeId
                                                  AND DetectionRateId =@DetectionRateId";
                            List<SqlParameter> listStr = new List<SqlParameter>();
                            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
                            listStr.Add(new SqlParameter("@UnitWorkId", pipeline.UnitWorkId));
                            listStr.Add(new SqlParameter("@UnitId", pipeline.UnitId));
                            listStr.Add(new SqlParameter("@DetectionTypeId", newWeldJoint.DetectionTypeId));
                            listStr.Add(new SqlParameter("@DetectionRateId", pipeline.DetectionRateId));

                            // 5,6,7项为可选项
                            if (condition.Contains("5"))
                            {
                                strSql += " AND PipingClassId =@PipingClassId";
                                listStr.Add(new SqlParameter("@PipingClassId", pipeline.PipingClassId));
                            }
                            if (condition.Contains("6"))
                            {
                                strSql += " AND PipelineId =@PipelineId";
                                listStr.Add(new SqlParameter("@PipelineId", newWeldJoint.PipelineId));
                            }
                            if (condition.Contains("7"))
                            {
                                strSql += " AND WelderId =@WelderId";
                                listStr.Add(new SqlParameter("@WelderId", newWeldJoint.CoverWelderId));
                            }

                            SqlParameter[] parameter = listStr.ToArray();
                            DataTable batchInfo = SQLHelper.GetDataTableRunText(strSql, parameter);

                            string batchId = string.Empty;
                            if (batchInfo.Rows.Count == 0)
                            {
                                Model.HJGL_Batch_PointBatch batch = new Model.HJGL_Batch_PointBatch();
                                batch.PointBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_PointBatch));
                                batchId = batch.PointBatchId;
                                string perfix = project.ProjectCode + "-" + unitWork.UnitWorkCode + "-GD-DK-" + ndt.DetectionTypeCode + "-" + ndtr.DetectionRateValue.ToString() + "%-";
                                batch.PointBatchCode = BLL.SQLHelper.RunProcNewIdByProjectId("SpGetNewCode4ByProjectId", "dbo.HJGL_Batch_PointBatch", "PointBatchCode", this.CurrUser.LoginProjectId, perfix);

                                batch.ProjectId = CurrUser.LoginProjectId;
                                batch.UnitWorkId = pipeline.UnitWorkId;
                                batch.BatchCondition = batchCondition;
                                batch.UnitId = pipeline.UnitId;
                                batch.DetectionTypeId = newWeldJoint.DetectionTypeId;
                                batch.DetectionRateId = pipeline.DetectionRateId;
                                if (condition.Contains("5"))
                                {
                                    batch.PipingClassId = pipeline.PipingClassId;
                                }
                                if (condition.Contains("6"))
                                {
                                    batch.PipelineId = newWeldJoint.PipelineId;
                                }
                                if (condition.Contains("7"))
                                {
                                    batch.WelderId = newWeldJoint.CoverWelderId;
                                }
                                batch.StartDate = DateTime.Now;
                                BLL.PointBatchService.AddPointBatch(batch);
                            }
                            else
                            {
                                batchId = batchInfo.Rows[0][0].ToString();
                            }

                            var b = BLL.PointBatchDetailService.GetBatchDetailByJotId(weldJointId);
                            if (b == null)
                            {
                                try
                                {
                                    Model.HJGL_Batch_PointBatchItem batchDetail = new Model.HJGL_Batch_PointBatchItem();
                                    string pointBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_PointBatchItem));
                                    batchDetail.PointBatchItemId = pointBatchItemId;
                                    batchDetail.PointBatchId = batchId;
                                    batchDetail.WeldJointId = weldJointId;
                                    batchDetail.WeldingDate = weldingDate;
                                    batchDetail.CreatDate = DateTime.Now;
                                    BLL.Funs.DB.HJGL_Batch_PointBatchItem.InsertOnSubmit(batchDetail);
                                    BLL.Funs.DB.SubmitChanges();

                                    // 焊工首道口RT必点 
                                    var joints = from x in Funs.DB.HJGL_Batch_PointBatchItem
                                                 join y in Funs.DB.HJGL_Batch_PointBatch on x.PointBatchId equals y.PointBatchId
                                                 join z in Funs.DB.Base_DetectionType on y.DetectionTypeId equals z.DetectionTypeId
                                                 join j in Funs.DB.HJGL_WeldJoint on x.WeldJointId equals j.WeldJointId
                                                 where z.DetectionTypeCode == "RT"
                                                 && j.CoverWelderId == newWeldJoint.CoverWelderId
                                                 select x;
                                    if (joints.Count() <= 1)
                                    {
                                        BLL.PointBatchDetailService.UpdatePointBatchDetail(pointBatchItemId, "1", System.DateTime.Now);
                                        BLL.PointBatchDetailService.UpdateWelderFirst(pointBatchItemId, true);
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                        else
                        {
                            errlog += "焊口【" + newWeldJoint.WeldJointCode + "】组批条件不能为空。";
                        }

                    }

                }
                else
                {
                    errlog = "焊口" + "【" + newWeldJoint.WeldJointCode + "】" + "未选择焊工";
                }
            }

            return errlog;
        }
        #endregion

        #region 日报明细删除（更新焊口信息），组批等
        /// <summary>
        /// 日报明细删除（更新焊口信息），组批等
        /// </summary>
        /// <param name="item"></param>
        /// <param name="weldingDailyId"></param>
        /// <returns></returns>
        private string DeleteWeldingDailyItem(string weldJointId, string coverWelderId, string backingWelderId, string jointAttribute, DateTime? weldingDate, string batchCondition, bool isSave)
        {
            string errlog = string.Empty;
            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
            var pipeline = BLL.PipelineService.GetPipelineByPipelineId(newWeldJoint.PipelineId);
            var unit = BLL.UnitService.GetUnitByUnitId(pipeline.UnitId);
            var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(newWeldJoint.DetectionTypeId);
            var ndtr = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(pipeline.DetectionRateId);

            if (newWeldJoint != null && !string.IsNullOrEmpty(newWeldJoint.WeldingDailyId))
            {
                var isTrust = from x in Funs.DB.HJGL_Batch_BatchTrustItem
                              where x.WeldJointId == weldJointId
                              select x; ;
                if (isTrust.Count() == 0)
                {
                    var updateWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
                    if (updateWeldJoint != null)
                    {
                        updateWeldJoint.WeldingDailyId = null;
                        updateWeldJoint.WeldingDailyCode = null;
                        updateWeldJoint.CoverWelderId = null;
                        updateWeldJoint.BackingWelderId = null;
                        BLL.WeldJointService.UpdateWeldJoint(updateWeldJoint);

                        var pointBatchItems = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.WeldJointId == weldJointId select x;
                        string pointBatchId = pointBatchItems.FirstOrDefault().PointBatchId;

                        // 删除焊口所在批明细信息
                        BLL.PointBatchDetailService.DeleteBatchDetail(weldJointId);

                        // 删除批信息
                        var batch = from x in Funs.DB.HJGL_Batch_PointBatchItem where x.PointBatchId == pointBatchId select x;
                        if (pointBatchId != null && batch.Count() == 0)
                        {
                            BLL.PointBatchService.DeleteBatch(pointBatchId);
                        }
                        //BLL.Batch_NDEItemService.DeleteAllNDEInfoToWeldJoint(item.WeldJointId);
                    }
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldReportMenuId, Const.BtnDelete, weldingDailyId);
                }
                else
                {
                    errlog += "焊口【" + newWeldJoint.WeldJointCode + "】已进委托单了，不能删除。";
                }
            }

            return errlog;
        }
        #endregion

        #region 收集Grid页面信息
        /// <summary>
        /// 收集Grid页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.SpWeldingDailyItem> CollectGridJointInfo()
        {
            List<Model.SpWeldingDailyItem> GetWeldingDailyItem = null;
            List<Model.SpWeldingDailyItem> getNewWeldReportItem = new List<Model.SpWeldingDailyItem>();
            if (!string.IsNullOrEmpty(this.hdItemsString.Text))
            {
                GetWeldingDailyItem = BLL.WeldingDailyService.GetWeldReportAddItem(this.hdItemsString.Text);
            }
            else if (string.IsNullOrEmpty(this.hdItemsString.Text) && this.WeldingDailyId != null)
            {
                GetWeldingDailyItem = BLL.WeldingDailyService.GetWeldingDailyItem(this.WeldingDailyId);
            }

            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");

                string rowID = values.Value<string>("WeldJointId").ToString();
                var item = GetWeldingDailyItem.FirstOrDefault(x => x.WeldJointId == rowID);
                if (item != null)
                {
                    var coverWelderCode = (from x in Funs.DB.SitePerson_Person
                                           where x.ProjectId == CurrUser.LoginProjectId && x.WelderCode == values.Value<string>("CoverWelderId")
                                           select x).FirstOrDefault();
                    if (coverWelderCode != null)
                    {
                        item.CoverWelderCode = coverWelderCode.WelderCode;
                        item.CoverWelderId = coverWelderCode.PersonId;
                    }
                    var backingWelderCode = (from x in Funs.DB.SitePerson_Person
                                             where x.ProjectId == CurrUser.LoginProjectId && x.WelderCode == values.Value<string>("BackingWelderId")
                                             select x).FirstOrDefault();
                    if (backingWelderCode != null)
                    {
                        item.BackingWelderCode = backingWelderCode.WelderCode;
                        item.BackingWelderId = backingWelderCode.PersonId;
                    }

                    if (!string.IsNullOrEmpty(values.Value<string>("JointAttribute")))
                    {
                        item.JointAttribute = values.Value<string>("JointAttribute").ToString();

                    }

                    getNewWeldReportItem.Add(item);
                }

            }
            return getNewWeldReportItem;
        }
        #endregion

        #region Grid 关闭弹出窗口事件
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            List<Model.SpWeldingDailyItem> GetWeldingDailyItem = BLL.WeldingDailyService.GetWeldReportAddItem(this.hdItemsString.Text);
            this.BindGrid(GetWeldingDailyItem);
            //SetDrpByDrpUnitChange();             
            //this.hdItemsString.Text = string.Empty;
        }
        #endregion

        #region 右键删除事件
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                List<Model.SpWeldingDailyItem> GetWeldingDailyItem = this.CollectGridJointInfo();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var item = GetWeldingDailyItem.FirstOrDefault(x => x.WeldJointId == rowID);
                    var p = BLL.PointBatchDetailService.GetBatchDetailByJotId(rowID);
                    if (item != null && (p == null || (p != null && p.PointState == null)))
                    {
                        GetWeldingDailyItem.Remove(item);
                        // 删除焊口所在批明细信息
                        if (p != null)
                        {
                            BLL.PointBatchDetailService.DeleteBatchDetail(item.WeldJointId);
                        }
                        //    // 更新焊口信息
                        var updateWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(item.WeldJointId);
                        if (updateWeldJoint != null)
                        {
                            updateWeldJoint.WeldingDailyId = null;
                            updateWeldJoint.WeldingDailyCode = null;
                            updateWeldJoint.CoverWelderId = null;
                            updateWeldJoint.BackingWelderId = null;
                            BLL.WeldJointService.UpdateWeldJoint(updateWeldJoint);
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("不能删除，已检测并审核！", MessageBoxIcon.Warning);
                    }
                }

                BindGrid(GetWeldingDailyItem);
                ShowNotify("删除成功！", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 查找
        protected void WeldingDateText_OnTextChanged(object sender, EventArgs e)
        {
            BindGrid(null);
        }
        /// <summary>
        /// 查找未焊接焊口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckSelect_Click(object sender, EventArgs e)
        {
            //string weldJointIds = string.Empty;

            //for (int i = 0; i < Grid1.Rows.Count; i++)
            //{
            //    string weldJointId = Grid1.DataKeys[i][0].ToString();
            //    weldJointIds = weldJointIds + weldJointId + "|";
            //}

            //if (weldJointIds != string.Empty)
            //{
            //    weldJointIds = weldJointIds.Substring(0, weldJointIds.Length - 1);
            //}
            string weldJointIds = hdItemsString.Text.Trim();
            if (!string.IsNullOrEmpty(this.drpUnit.SelectedValue) && this.drpUnit.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpUnitWork.SelectedValue) && this.drpUnitWork.SelectedValue != BLL.Const._Null)
            {
                string strList = this.drpUnitWork.SelectedValue + "|" + this.drpUnit.SelectedValue + "|" + this.WeldingDailyId;
                string window = String.Format("SelectDailyWeldJoint.aspx?strList={0}&weldJointIds={1}", strList, Server.UrlEncode(weldJointIds), "编辑 - ");
                //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdItemsString.ClientID) + Window1.GetShowReference(window));
            }
            else
            {
                Alert.ShowInTop("请选择单位和单位工程", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}