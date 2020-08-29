using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class PreWeldReportAudit :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitTreeMenu();
                txtWeldingDate.Text = DateTime.Now.Date.ToString();
            }
        }

        #region 加载树装置-单位
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();

            TreeNode rootNode1 = new TreeNode();
            rootNode1.NodeID = "1";
            rootNode1.Text = "建筑工程";
            rootNode1.CommandName = "建筑工程";
            this.tvControlItem.Nodes.Add(rootNode1);

            TreeNode rootNode2 = new TreeNode();
            rootNode2.NodeID = "2";
            rootNode2.Text = "安装工程";
            rootNode2.CommandName = "安装工程";
            rootNode2.Expanded = true;
            this.tvControlItem.Nodes.Add(rootNode2);

            var pUnits = (from x in Funs.DB.Project_ProjectUnit where x.ProjectId == this.CurrUser.LoginProjectId select x).ToList();
            // 获取当前用户所在单位
            var currUnit = pUnits.FirstOrDefault(x => x.UnitId == this.CurrUser.UnitId);

            var unitWorkList = (from x in Funs.DB.WBS_UnitWork
                                where x.ProjectId == this.CurrUser.LoginProjectId
                                      && x.SuperUnitWork == null && x.UnitId != null && x.ProjectType != null
                                select x).ToList();

            List<Model.WBS_UnitWork> unitWork1 = null;
            List<Model.WBS_UnitWork> unitWork2 = null;

            // 当前为施工单位，只能操作本单位的数据
            if (currUnit != null && currUnit.UnitType == Const.ProjectUnitType_2)
            {
                unitWork1 = (from x in unitWorkList
                             where x.UnitId == this.CurrUser.UnitId && x.ProjectType == "1"
                             select x).ToList();
                unitWork2 = (from x in unitWorkList
                             where x.UnitId == this.CurrUser.UnitId && x.ProjectType == "2"
                             select x).ToList();
            }
            else
            {
                unitWork1 = (from x in unitWorkList where x.ProjectType == "1" select x).ToList();
                unitWork2 = (from x in unitWorkList where x.ProjectType == "2" select x).ToList();
            }

            if (unitWork1.Count() > 0)
            {
                foreach (var q in unitWork1)
                {
                    int a = (from x in Funs.DB.HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn1 = new TreeNode();
                    tn1.NodeID = q.UnitWorkId;
                    tn1.Text = q.UnitWorkName ;
                    tn1.ToolTip = "施工单位：" + u.UnitName;
                    tn1.EnableClickEvent = true;
                    rootNode1.Nodes.Add(tn1);
                }
            }
            if (unitWork2.Count() > 0)
            {
                foreach (var q in unitWork2)
                {
                    int a = (from x in Funs.DB.HJGL_Pipeline where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId == q.UnitWorkId select x).Count();
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn2 = new TreeNode();
                    tn2.NodeID = q.UnitWorkId;
                    tn2.Text = q.UnitWorkName ;
                    tn2.ToolTip = "施工单位：" + u.UnitName;
                    tn2.EnableClickEvent = true;
                    rootNode2.Nodes.Add(tn2);
                }
            }
        }

        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion


        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT preWeld.PreWeldingDailyId, 
                                     preWeld.ProjectId, 
                                     preWeld.WeldJointId, 
                                     preWeld.WeldingDate, 
									 jot.WeldJointCode,
									 jot.PipelineCode,
                                     jot.JointArea,
                                     preWeld.JointAttribute, 
                                     jot.Size, 
                                     jot.Dia, 
                                     jot.Thickness,                            
                                     preWeld.AttachUrl, 
                                     cellWelder.WelderCode AS CellWelderCode,
                                     backingWelder.WelderCode AS BackingWelderCode,
                                     method.WeldingMethodCode AS WeldMethod,
                                     preWeld.AuditDate,
                                     users.UserName AS AuditManName 
                                  FROM dbo.HJGL_PreWeldingDaily AS preWeld
                                  LEFT JOIN dbo.HJGL_WeldJoint AS jot ON jot.WeldJointId = preWeld.WeldJointId
                                  LEFT JOIN dbo.SitePerson_Person AS cellWelder ON cellWelder.PersonId=preWeld.CoverWelderId
                                  LEFT JOIN dbo.SitePerson_Person AS backingWelder ON backingWelder.PersonId=preWeld.BackingWelderId
                                  LEFT JOIN dbo.Base_WeldingMethod method ON method.WeldingMethodId=jot.WeldingMethodId
                                  LEFT JOIN dbo.Sys_User AS users ON users.UserId = preWeld.AuditMan
                                  WHERE preWeld.ProjectId=@ProjectId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID))
            {
                strSql += " AND preWeld.UnitWorkId =@UnitWorkId";
                listStr.Add(new SqlParameter("@UnitWorkId", tvControlItem.SelectedNode.NodeID));
            }

            if (!string.IsNullOrEmpty(txtWeldingDate.Text.Trim()))
            {
                strSql += " AND CONVERT(VARCHAR(100), preWeld.WeldingDate,23) =@WeldingDate";
                listStr.Add(new SqlParameter("@WeldingDate", Convert.ToDateTime(txtWeldingDate.Text.Trim())));
            }

            if (IsAudit.SelectedValue == "0")
            {
                strSql += " AND preWeld.AuditDate IS NULL ";
            }
            else
            {
                strSql += " AND preWeld.AuditDate IS NOT NULL ";
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        protected void IsAudit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void WeldingDate_OnTextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region 分页排序
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        /// <summary>
        /// 预提交日报审核并提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeldingDate.Text))
            {
                string perfix = string.Format("{0:yyyyMMdd}", Convert.ToDateTime(txtWeldingDate.Text)) + "-";
                string weldingDailyCode = BLL.SQLHelper.RunProcNewId("SpGetThreeNumber", "dbo.HJGL_WeldingDaily", "WeldingDailyCode", this.CurrUser.LoginProjectId, perfix);

                Model.HJGL_WeldingDaily newWeldingDaily = new Model.HJGL_WeldingDaily();
                newWeldingDaily.WeldingDailyCode = weldingDailyCode;
                newWeldingDaily.ProjectId = CurrUser.LoginProjectId;
                if (tvControlItem.SelectedNodeID != BLL.Const._Null)
                {
                    newWeldingDaily.UnitWorkId = tvControlItem.SelectedNodeID;
                    var unitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
                    newWeldingDaily.UnitId = unitWork.UnitId;
                }
               
                DateTime? weldDate = Funs.GetNewDateTime(this.txtWeldingDate.Text);
                if (weldDate.HasValue)
                {
                    newWeldingDaily.WeldingDate = weldDate.Value;
                }
                else
                {
                    newWeldingDaily.WeldingDate = Convert.ToDateTime(txtWeldingDate.Text);
                }
                newWeldingDaily.Tabler = this.CurrUser.UserId;
                newWeldingDaily.TableDate = Funs.GetNewDateTime(this.txtWeldingDate.Text);
                newWeldingDaily.Remark = "移动端提交";
               
                string errlog = string.Empty;
                string eventArg = string.Empty;

                int[] selections = Grid1.SelectedRowIndexArray;
                foreach (int rowIndex in selections)
                {
                    string weldJointId = Grid1.DataKeys[rowIndex][1].ToString();
                    string preWeldingDailyId = Grid1.DataKeys[rowIndex][0].ToString();

                    bool canSave = false;
                    var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
                    var joty = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(jot.WeldTypeId);
                    var preWeldingDaily = Funs.DB.HJGL_PreWeldingDaily.FirstOrDefault(x => x.PreWeldingDailyId == preWeldingDailyId);

                    string weldType = string.Empty;
                    if (joty != null && joty.WeldTypeCode.Contains("B"))

                    {
                        weldType = "对接焊缝";
                    }
                    else
                    {
                        weldType = "角焊缝";
                    }
                    string floorWelder = preWeldingDaily.BackingWelderId;
                    string cellWelder = preWeldingDaily.CoverWelderId;
                    decimal? dia = jot.Dia;
                    decimal? sch = Funs.GetNewDecimal(jot.Thickness.HasValue ? jot.Thickness.Value.ToString() : "");
                    string wmeCode = string.Empty;
                    var wm = BLL.Base_WeldingMethodService.GetWeldingMethodByWeldingMethodId(jot.WeldingMethodId);
                    if (wm != null)
                    {
                        wmeCode = wm.WeldingMethodCode;
                    }
                    string[] wmeCodes = wmeCode.Split('+');
                    //string location = item.JOT_Location;
                    string ste = jot.Material1Id;
                    string jointAttribute = jot.JointAttribute;

                    List<Model.Welder_WelderQualify> floorWelderQualifys = (from x in Funs.DB.Welder_WelderQualify
                                                                            where x.WelderId == floorWelder && x.WeldingMethod != null
                                                                                           && x.MaterialType != null && x.WeldType != null
                                                                                           && x.ThicknessMax != null && x.SizesMin != null
                                                                            select x).ToList();

                    List<Model.Welder_WelderQualify> cellWelderQualifys = (from x in Funs.DB.Welder_WelderQualify
                                                                           where x.WelderId == cellWelder && x.WeldingMethod != null
                                                                                 && x.MaterialType != null && x.WeldType != null
                                                                                       && x.ThicknessMax != null && x.SizesMin != null
                                                                           select x).ToList();
                    // 打底和盖面同一焊工
                    if (floorWelder == cellWelder)
                    {
                        if (floorWelderQualifys != null && floorWelderQualifys.Count() > 0)
                        {
                            if (wmeCodes.Count() <= 1) // 一种焊接方法
                            {
                                canSave = OneWmeIsOK(floorWelderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                            }
                            else  // 大于一种焊接方法，如氩电联焊
                            {
                                canSave = TwoWmeIsOK(floorWelderQualifys, cellWelderQualifys, wmeCodes[0], wmeCodes[1], jointAttribute, weldType, ste, dia, sch);
                            }
                        }
                    }
                    // 打底和盖面焊工不同
                    else
                    {
                        bool isok1 = false;
                        bool isok2 = false;

                        if (wmeCodes.Count() <= 1) // 一种焊接方法
                        {
                            if (floorWelderQualifys != null && floorWelderQualifys.Count() > 0)
                            {
                                isok1 = OneWmeIsOK(floorWelderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                            }
                            if (cellWelderQualifys != null && cellWelderQualifys.Count() > 0)
                            {
                                isok2 = OneWmeIsOK(cellWelderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                            }
                            if (isok1 && isok2)
                            {
                                canSave = true;
                            }
                        }
                        else
                        {

                            canSave = TwoWmeIsOK(floorWelderQualifys, cellWelderQualifys, wmeCodes[0], wmeCodes[1], jointAttribute, weldType, ste, dia, sch);
                        }
                    }

                    if (canSave == false)
                    {
                        eventArg = eventArg + jot.WeldJointCode + ",";
                    }
                }

                if (eventArg == string.Empty)  //焊工焊接的所有焊口资质都符合要求）
                {
                    string weldingDailyId = SQLHelper.GetNewID(typeof(Model.HJGL_WeldingDaily));
                    newWeldingDaily.WeldingDailyId = weldingDailyId;
                    BLL.WeldingDailyService.AddWeldingDaily(newWeldingDaily);

                    // 获取组批条件
                    var batchC = BLL.Project_SysSetService.GetSysSetBySetId("5", CurrUser.LoginProjectId);
                    if (batchC != null)
                    {
                        string batchCondition = batchC.SetValue;
                        // 组批

                        foreach (int rowIndex in selections)
                        {
                            string[] condition = batchCondition.Split('|');
                            string weldJointId = Grid1.DataKeys[rowIndex][1].ToString();
                            string preWeldingDailyId = Grid1.DataKeys[rowIndex][0].ToString();
                            var preWeldingDaily = Funs.DB.HJGL_PreWeldingDaily.FirstOrDefault(x => x.PreWeldingDailyId == preWeldingDailyId);


                            var newWeldJoint = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
                            var pipeline = BLL.PipelineService.GetPipelineByPipelineId(newWeldJoint.PipelineId);
                            var unit = BLL.UnitService.GetUnitByUnitId(pipeline.UnitId);
                            var ndt = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(newWeldJoint.DetectionTypeId);
                            var ndtr = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(pipeline.DetectionRateId);

                            if (newWeldJoint != null && string.IsNullOrEmpty(newWeldJoint.WeldingDailyId))
                            {
                                newWeldJoint.WeldingDailyId = weldingDailyId;
                                newWeldJoint.WeldingDailyCode = weldingDailyCode;
                                newWeldJoint.CoverWelderId = preWeldingDaily.CoverWelderId;
                                newWeldJoint.BackingWelderId = preWeldingDaily.BackingWelderId;
                                newWeldJoint.JointAttribute = preWeldingDaily.JointAttribute;
                                BLL.WeldJointService.UpdateWeldJoint(newWeldJoint);

                                // 更新焊口号 修改固定焊口号后 +G
                                BLL.WeldJointService.UpdateWeldJointAddG(newWeldJoint.WeldJointId, newWeldJoint.JointAttribute, Const.BtnAdd);

                                // 更新预提交日报审核日期
                                preWeldingDaily.AuditDate = DateTime.Now;
                                Funs.DB.SubmitChanges();

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

                                    // 添加批次主表
                                    if (batchInfo.Rows.Count == 0)
                                    {
                                        Model.HJGL_Batch_PointBatch batch = new Model.HJGL_Batch_PointBatch();
                                        batch.PointBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_PointBatch));
                                        batchId = batch.PointBatchId;
                                        string perfix1 = unit.UnitCode + "-" + ndt.DetectionTypeCode + "-" + ndtr.DetectionRateValue.ToString() + "-";
                                        batch.PointBatchCode = BLL.SQLHelper.RunProcNewIdByProjectId("SpGetNewCode5ByProjectId", "dbo.HJGL_Batch_PointBatch", "PointBatchCode", this.CurrUser.LoginProjectId, perfix1);

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

                                    // 插入批次明细表
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
                                            batchDetail.WeldingDate = Convert.ToDateTime(txtWeldingDate.Text.Trim());
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
                    }
                    else
                    {
                        errlog += "请设置项目的组批条件";
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
                ShowNotify("请选择焊接日期！", MessageBoxIcon.Warning);
            }
        }


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
    }
}