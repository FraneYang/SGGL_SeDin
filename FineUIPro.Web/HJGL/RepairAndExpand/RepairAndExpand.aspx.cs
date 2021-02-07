using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;
namespace FineUIPro.Web.HJGL.RepairAndExpand
{
    public partial class RepairAndExpand : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtRepairMonth.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.InitTreeMenu();//加载树
            }
        }

        private void PageInit()
        {
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            var jot = BLL.WeldJointService.GetViewWeldJointById(repairRecord.WeldJointId);
            var ndeItem = Batch_NDEItemService.GetNDEItemById(repairRecord.NDEItemID);
            //var pipe = BLL.Pipeline_PipelineService.GetPipelineByPipelineId(jot.PipelineId);
            if (!string.IsNullOrEmpty(repairRecordId))
            {
                this.txtPipeCode.Text = jot.PipelineCode;
                txtWeldJointCode.Text = jot.WeldJointCode;
                txtWelder.Text = jot.BackingWelderCode;
                txtRepairLocation.Text = repairRecord.RepairLocation;
                txtJudgeGrade.Text = ndeItem.JudgeGrade;
                txtCheckDefects.Text = repairRecord.CheckDefects;
                if (repairRecord.RepairDate.HasValue)
                {
                    txtRepairDate.Text = repairRecord.RepairDate.ToString();
                }
                else
                {
                    txtRepairDate.Text = DateTime.Now.Date.ToString();
                }
                if (!string.IsNullOrEmpty(repairRecord.PBackingWelderId))
                {
                    drpPBackingWelder.SelectedValue = repairRecord.PBackingWelderId;
                }
                else
                {
                    drpPBackingWelder.SelectedValue = repairRecord.WelderId;
                }
                if (!string.IsNullOrEmpty(repairRecord.PCoverWelderId))
                {
                    drpPCoverWelder.SelectedValue = repairRecord.PCoverWelderId;
                }
                else
                {
                    drpPCoverWelder.SelectedValue = repairRecord.WelderId;
                }
                //if (repairRecord.AuditDate.HasValue)
                //{
                //    lbIsAudit.Text = "已审核";
                //    this.btnPointAudit.Enabled = false;
                //}
                //else
                //{
                //    lbIsAudit.Text = "未审核";
                //}

                if (repairRecord.RepairMark == "R2")
                {
                    //ckbdaily.Hidden = true;
                    //ckbMat.Hidden = true;
                    //ckbPipe.Hidden = true;
                    //ckbRepairBefore.Hidden = true;
                    //ckbSpec.Hidden = true;
                    //ckbWelder.Hidden = true;
                    //lbdef.Hidden = false;
                    this.ckbBatch.Checked = false;
                    this.ckbPipe.Checked = false;
                    this.ckbdaily.Checked = false;
                    this.ckbRepairBefore.Checked = false;
                    this.ckbMat.Checked = false;
                    this.ckbSpec.Checked = false;
                    var repairRecordR1 = Funs.DB.HJGL_RepairRecord.FirstOrDefault(x => x.WeldJointId == repairRecord.WeldJointId && x.RepairMark == "R1");
                    if (repairRecordR1 != null)
                    {
                        if (repairRecordR1.Batch == true)
                        {
                            this.ckbBatch.Checked = true;
                        }
                        if (repairRecordR1.Pipe == true)
                        {
                            this.ckbPipe.Checked = true;
                        }
                        if (repairRecordR1.Daily == true)
                        {
                            this.ckbdaily.Checked = true;
                        }
                        if (repairRecordR1.RepairBefore == true)
                        {
                            this.ckbRepairBefore.Checked = true;
                        }
                        if (repairRecordR1.Mat == true)
                        {
                            this.ckbMat.Checked = true;
                        }
                        if (repairRecordR1.Spec == true)
                        {
                            this.ckbSpec.Checked = true;
                        }
                    }
                    ckbBatch.Enabled = false;
                    ckbdaily.Enabled = false;
                    ckbMat.Enabled = false;
                    ckbPipe.Enabled = false;
                    ckbRepairBefore.Enabled = false;
                    ckbSpec.Enabled = false;
                    lbdef.Hidden = true;
                }
                else
                {
                    this.ckbBatch.Checked = true;
                    this.ckbPipe.Checked = false;
                    this.ckbdaily.Checked = false;
                    this.ckbRepairBefore.Checked = false;
                    this.ckbMat.Checked = false;
                    this.ckbSpec.Checked = false;
                    ckbBatch.Enabled = true;
                    ckbdaily.Enabled = true;
                    ckbMat.Enabled = true;
                    ckbPipe.Enabled = true;
                    ckbRepairBefore.Enabled = true;
                    ckbSpec.Enabled = true;
                    ckbBatch.Hidden = false;
                    ckbdaily.Hidden = false;
                    ckbMat.Hidden = false;
                    ckbPipe.Hidden = false;
                    ckbRepairBefore.Hidden = false;
                    ckbSpec.Hidden = false;
                    ckbWelder.Hidden = false;
                    lbdef.Hidden = true;
                }
            }
        }

        #region 加载树
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
                    tn1.Text = q.UnitWorkName;
                    tn1.ToolTip = "施工单位：" + u.UnitName;
                    tn1.CommandName = "单位工程";
                    rootNode1.Nodes.Add(tn1);
                    BindNodes(tn1);
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
                    tn2.Text = q.UnitWorkName;
                    tn2.ToolTip = "施工单位：" + u.UnitName;
                    tn2.CommandName = "单位工程";
                    rootNode2.Nodes.Add(tn2);
                    BindNodes(tn2);
                }
            }
        }

        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode ChildNodes)
        {
            //单号
            var repairs = from x in Funs.DB.HJGL_RepairRecord
                          where x.NoticeDate < Convert.ToDateTime(this.txtRepairMonth.Text.Trim() + "-01").AddMonths(1)
                          && x.NoticeDate >= Convert.ToDateTime(this.txtRepairMonth.Text.Trim() + "-01")
                          && x.ProjectId == this.CurrUser.LoginProjectId
                          && x.RepairRecordCode.Contains(this.txtSearchCode.Text.Trim())
                          && x.UnitWorkId == ChildNodes.NodeID
                          orderby x.RepairRecordCode descending
                          select x;
            foreach (var r in repairs)
            {
                var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(r.WeldJointId);
                var iso = BLL.PipelineService.GetPipelineByPipelineId(jot.PipelineId);
                TreeNode newNode = new TreeNode();
                //if (!r.AuditDate.HasValue)
                //{
                //    newNode.Text = "<font color='#EE0000'>" + r.RepairRecordCode + "</font>";
                //}
                //else
                //{
                //    newNode.Text = r.RepairRecordCode;
                //}
                var trustItems = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.RepairRecordId == r.RepairRecordId select x;
                if (trustItems.Count() == 0)   //未生成返修委托单
                {
                    bool b = false;
                    var trustItem = (from x in Funs.DB.HJGL_Batch_BatchTrustItem
                                     join y in Funs.DB.HJGL_Batch_NDEItem on x.TrustBatchItemId equals y.TrustBatchItemId
                                     where y.NDEItemID == r.NDEItemID
                                     select x).FirstOrDefault();
                    if (trustItem != null)
                    {
                        var trustItems2 = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.TrustBatchId == trustItem.TrustBatchId select x;
                        foreach (var item in trustItems2)
                        {
                            var ndeItem = Funs.DB.HJGL_Batch_NDEItem.FirstOrDefault(x => x.TrustBatchItemId == item.TrustBatchItemId);
                            if (ndeItem != null)
                            {
                                var repairRecord = Funs.DB.HJGL_RepairRecord.FirstOrDefault(x => x.NDEItemID == ndeItem.NDEItemID);
                                if (repairRecord != null)
                                {
                                    var trustItem3s = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.RepairRecordId == repairRecord.RepairRecordId select x;
                                    if (trustItem3s.Count() > 0)
                                    {
                                        b = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (b)
                    {
                        newNode.Text = iso.PipelineCode + "-" + jot.WeldJointCode + r.RepairMark;
                    }
                    else
                    {
                        newNode.Text = "<font color='#EE0000'>" + iso.PipelineCode + "-" + jot.WeldJointCode + r.RepairMark + "</font>";
                    }
                }
                else
                {
                    newNode.Text = iso.PipelineCode + "-" + jot.WeldJointCode + r.RepairMark;
                }
                newNode.NodeID = r.RepairRecordId;
                newNode.ToolTip = "返修单";
                newNode.EnableClickEvent = true;
                ChildNodes.Nodes.Add(newNode);
            }
        }
        #endregion

        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID))
            {
                string repairRecordId = tvControlItem.SelectedNodeID;
                var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
                var jot = BLL.WeldJointService.GetViewWeldJointById(repairRecord.WeldJointId);
                var batchDetail = BLL.PointBatchDetailService.GetBatchDetailByJotId(repairRecord.WeldJointId);
                var day = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(jot.WeldingDailyId);

                string strSql = string.Empty;
                DataTable dt = null;
                if (repairRecord.RepairMark == "R1")
                {
                    strSql = @"SELECT PointBatchItemId,PointBatchId,PointBatchCode,WeldJointId,PointState,PointDate,WeldJointCode,
                                     JointArea,Size,WeldingDate,PipelineCode,PipingClassName,PointIsAudit,UnitId,
                                     (case when PBackingWelderId is null then BackingWelderId else PBackingWelderId end) BackingWelderId,
                                     (case when PCoverWelderId is null then CoverWelderId else PCoverWelderId end) CoverWelderId    
                              FROM dbo.View_HJGL_Batch_PointBatchItem
                              WHERE ProjectId=@ProjectId AND DetectionTypeId=@DetectionTypeId ";
                    List<SqlParameter> listStr = new List<SqlParameter>();
                    listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@DetectionTypeId", repairRecord.DetectionTypeId));
                    if (repairRecord.AuditDate.HasValue)
                    {
                        this.btnPointAudit.Enabled = false;
                        strSql += " AND RepairRecordId =@RepairRecordId";
                        listStr.Add(new SqlParameter("@RepairRecordId", repairRecordId));
                    }
                    else
                    {
                        strSql += "AND (PointDate IS NULL OR(PointDate IS NOT NULL AND RepairRecordId = @RepairRecordId))";
                        listStr.Add(new SqlParameter("@RepairRecordId", repairRecordId));
                    }
                    if (ckbWelder.Checked)
                    {
                        strSql += " AND WelderId =@WelderId";
                        listStr.Add(new SqlParameter("@WelderId", jot.BackingWelderId));
                    }
                    if (ckbBatch.Checked)
                    {
                        strSql += " AND PointBatchId =@PointBatchId";
                        listStr.Add(new SqlParameter("@PointBatchId", batchDetail.PointBatchId));
                    }
                    if (ckbPipe.Checked)
                    {
                        strSql += " AND PipelineId =@PipelineId";
                        listStr.Add(new SqlParameter("@PipelineId", jot.PipelineId));
                    }

                    if (ckbdaily.Checked)
                    {
                        strSql += " AND WeldingDate = @WeldingDate";
                        listStr.Add(new SqlParameter("@WeldingDate", day.WeldingDate));
                    }

                    if (ckbRepairBefore.Checked)
                    {
                        strSql += " AND WeldingDate <=@WeldingDate1";
                        listStr.Add(new SqlParameter("@WeldingDate1", day.WeldingDate));
                    }

                    if (ckbMat.Checked)
                    {
                        strSql += " AND Mat =@Mat";
                        listStr.Add(new SqlParameter("@Mat", jot.Material1Id));
                    }

                    if (ckbSpec.Checked)
                    {
                        strSql += " AND Specification =@Specification";
                        listStr.Add(new SqlParameter("@Specification", jot.Specification));
                    }

                    SqlParameter[] parameter = listStr.ToArray();
                    dt = SQLHelper.GetDataTableRunText(strSql, parameter);
                }
                else
                {
                    strSql = @"SELECT PointBatchItemId,PointBatchId,PointBatchCode,WeldJointId,PointState,PointDate,WeldJointCode,
                                     JointArea,Size,WeldingDate,PipelineCode,PipingClassName,PointIsAudit,UnitId,
                                     (case when PBackingWelderId is null then BackingWelderId else PBackingWelderId end) BackingWelderId,
                                     (case when PCoverWelderId is null then CoverWelderId else PCoverWelderId end) CoverWelderId  
                              FROM dbo.View_HJGL_Batch_PointBatchItem
                              WHERE ProjectId=@ProjectId AND DetectionTypeId=@DetectionTypeId 
                                      AND WelderId =@WelderId AND PointBatchId=@PointBatchId  ";
                    List<SqlParameter> listStr = new List<SqlParameter>();
                    if (repairRecord.AuditDate.HasValue)
                    {
                        this.btnPointAudit.Enabled = false;
                        strSql += " AND RepairRecordId =@RepairRecordId";
                        listStr.Add(new SqlParameter("@RepairRecordId", repairRecordId));
                    }
                    else
                    {
                        strSql += "AND (PointDate IS NULL OR(PointDate IS NOT NULL AND RepairRecordId = @RepairRecordId))";
                        listStr.Add(new SqlParameter("@RepairRecordId", repairRecordId));
                    }
                    listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@DetectionTypeId", repairRecord.DetectionTypeId));
                    listStr.Add(new SqlParameter("@WelderId", jot.BackingWelderId));
                    listStr.Add(new SqlParameter("@PointBatchId", jot.PointBatchId));

                    SqlParameter[] parameter = listStr.ToArray();
                    dt = SQLHelper.GetDataTableRunText(strSql, parameter);
                }

                Grid1.RecordCount = dt.Rows.Count;
                // tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, dt);
                Grid1.DataSource = table;
                Grid1.DataBind();

                string ids = string.Empty;
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    var pointItem = BLL.PointBatchDetailService.GetBatchDetailById(this.Grid1.Rows[i].DataKeys[0].ToString());
                    if (pointItem != null)
                    {
                        if (pointItem.PointState != null)
                        {
                            ids += pointItem.PointBatchItemId + ",";

                            this.Grid1.Rows[i].CellCssClasses[3] = "colorredBlue";
                            this.Grid1.Rows[i].CellCssClasses[6] = "colorredBlue";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.Substring(0, ids.Length - 1);
                    this.Grid1.SelectedRowIDArray = ids.Split(',');
                }
                if (Grid1.Rows.Count > 0)
                {
                    foreach (JObject mergedRow in Grid1.GetMergedData())
                    {
                        int i = mergedRow.Value<int>("index");
                        GridRow row = Grid1.Rows[i];
                        string UnitId = this.Grid1.Rows[i].DataKeys[1].ToString();
                        //打底焊工
                        //AspNet.DropDownList drpBackingWelderId = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpBackingWelderId");
                        //AspNet.HiddenField hdBackingWelderId = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdBackingWelderId");
                        //drpBackingWelderId.Items.AddRange(BLL.WelderService.GetWelderListItem(this.CurrUser.LoginProjectId, UnitId));
                        //Funs.PleaseSelect(drpBackingWelderId);
                        //if (!string.IsNullOrEmpty(hdBackingWelderId.Value))
                        //{
                        //    drpBackingWelderId.SelectedValue = hdBackingWelderId.Value;
                        //}
                        //盖面焊工
                        //AspNet.DropDownList drpCoverWelderId = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpCoverWelderId");
                        //AspNet.HiddenField hdCoverWelderId = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdCoverWelderId");
                        //drpCoverWelderId.Items.AddRange(BLL.WelderService.GetWelderListItem(this.CurrUser.LoginProjectId, UnitId));
                        //Funs.PleaseSelect(drpCoverWelderId);
                        //if (!string.IsNullOrEmpty(hdCoverWelderId.Value))
                        //{
                        //    drpCoverWelderId.SelectedValue = hdCoverWelderId.Value;
                        //}
                        //if (repairRecord.AuditDate.HasValue)//若已审核完毕，则不能修改
                        //{
                        //    drpBackingWelderId.Enabled = false;
                        //    drpCoverWelderId.Enabled = false;
                        //}
                    }
                }
            }
        }

        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //获取可焊焊工
            var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(repairRecord.WeldJointId);
            var iso = BLL.PipelineService.GetPipelineByPipelineId(jot.PipelineId);
            var joty = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(jot.WeldTypeId);
            string weldType = string.Empty;
            if (joty != null && joty.WeldTypeCode.Contains("B"))
            {
                weldType = "对接焊缝";
            }
            else
            {
                weldType = "角焊缝";
            }

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
            List<Model.SitePerson_Person> welders = new List<Model.SitePerson_Person>();
            string canWelderCode = string.Empty;
            string canWeldingRodName = string.Empty;
            string canWeldingWireName = string.Empty;
            var projectWelder = from x in Funs.DB.SitePerson_Person
                                where x.ProjectId == jot.ProjectId && x.IsUsed == true
                                      && x.UnitId == iso.UnitId && x.WorkPostId == Const.WorkPost_Welder
                                      && x.WelderCode != null && x.WelderCode != ""
                                select x;

            foreach (var welder in projectWelder)
            {
                bool canSave = false;
                List<Model.Welder_WelderQualify> welderQualifys = (from x in Funs.DB.Welder_WelderQualify
                                                                   where x.WelderId == welder.PersonId && x.WeldingMethod != null
                                                                                  && x.MaterialType != null && x.WeldType != null
                                                                                  && x.ThicknessMax != null && x.SizesMin != null
                                                                                  && x.LimitDate > DateTime.Now && x.IsAudit == true
                                                                   select x).ToList();
                if (welderQualifys != null)
                {
                    if (wmeCodes.Count() <= 1) // 一种焊接方法
                    {
                        canSave = OneWmeIsOK(welderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                    }
                    else  // 大于一种焊接方法，如氩电联焊
                    {
                        canSave = TwoWmeIsOK(welderQualifys, wmeCodes[0], wmeCodes[1], jointAttribute, weldType, ste, dia, sch);
                    }
                    if (canSave)
                    {
                        welders.Add(welder);
                    }
                }
            }
            BLL.WelderService.InitProjectWelderDropDownListByData(this.drpPBackingWelder, true, welders, "请选择");
            BLL.WelderService.InitProjectWelderDropDownListByData(this.drpPCoverWelder, true, welders, "请选择");

            PageInit();
            this.BindGrid();

            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
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
        private bool TwoWmeIsOK(List<Model.Welder_WelderQualify> welderQualifys, string wmeCode1, string wmeCode2, string jointAttribute, string weldType, string ste, decimal? dia, decimal? sch)
        {
            bool isok = false;

            decimal? fThicknessMax = 0;
            decimal? cThicknessMax = 0;

            var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(ste);
            var floorQ = from x in welderQualifys
                         where wmeCode1.Contains(x.WeldingMethod)
                         && (mat == null || x.MaterialType.Contains(mat.MetalType ?? ""))
                         && x.WeldType.Contains(weldType)
                         // && (dia == null || x.SizesMin<=dia)
                         select x;
            var cellQ = from x in welderQualifys
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

        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.SGGLDB db = Funs.DB;
            string repairRecordId = tvControlItem.SelectedNodeID;
            Model.HJGL_RepairRecord repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            var trustItem = (from x in Funs.DB.HJGL_Batch_BatchTrustItem
                             join y in Funs.DB.HJGL_Batch_NDEItem on x.TrustBatchItemId equals y.TrustBatchItemId
                             where y.NDEItemID == repairRecord.NDEItemID
                             select x).FirstOrDefault();
            if (trustItem != null)
            {
                var trustItems2 = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.TrustBatchId == trustItem.TrustBatchId select x;
                foreach (var item in trustItems2)
                {
                    var ndeItem = Funs.DB.HJGL_Batch_NDEItem.FirstOrDefault(x => x.TrustBatchItemId == item.TrustBatchItemId);
                    if (ndeItem != null)
                    {
                        var repair = Funs.DB.HJGL_RepairRecord.FirstOrDefault(x => x.NDEItemID == ndeItem.NDEItemID);
                        if (repair != null)
                        {
                            var trustItem3s = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.RepairRecordId == repair.RepairRecordId select x;
                            if (trustItem3s.Count() > 0)
                            {
                                Alert.ShowInTop("本次返修已扩透！", MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }
            if (!repairRecord.AuditDate.HasValue)
            {
                // 更新返修记录
                var repair = db.HJGL_RepairRecord.FirstOrDefault(x => x.RepairRecordId == repairRecordId);
                if (repair != null)
                {
                    repair.PBackingWelderId = drpPBackingWelder.SelectedValue;
                    repair.PCoverWelderId = drpPCoverWelder.SelectedValue;
                    repair.RepairDate = Convert.ToDateTime(this.txtRepairDate.Text);
                    repair.AuditDate = DateTime.Now;
                    repair.Batch = this.ckbBatch.Checked;
                    repair.Pipe = this.ckbPipe.Checked;
                    repair.Daily = this.ckbdaily.Checked;
                    repair.RepairBefore = this.ckbRepairBefore.Checked;
                    repair.Mat = this.ckbMat.Checked;
                    repair.Spec = this.ckbSpec.Checked;
                    //if (ckbIsCut.Checked)
                    //{
                    //    repair.IsCut = true;
                    //}
                }

                // 更新返修口
                var batchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.WeldJointId == repairRecord.WeldJointId);
                if (batchItem != null)
                {
                    batchItem.RepairDate = Convert.ToDateTime(this.txtRepairDate.Text);
                    //if (ckbIsCut.Checked)
                    //{
                    //    batchItem.CutDate = DateTime.Now.Date;
                    //}
                }
                db.SubmitChanges();

                var exp = BLL.RepairRecordService.GetExportItem(repairRecordId);
                if (exp != null)
                {
                    foreach (Model.HJGL_Batch_PointBatchItem item in exp)
                    {
                        Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == item.PointBatchItemId);
                        newPointBatchItem.PointState = null;
                        newPointBatchItem.PointDate = null;
                        newPointBatchItem.RepairRecordId = null;
                        db.SubmitChanges();
                    }
                }
                // 更新扩透口
                RandomExport(repairRecord.RepairMark);
                string[] checkedRow = Grid1.SelectedRowIDArray;
                if (checkedRow.Count() > 0)
                {
                    foreach (string item in checkedRow)
                    {
                        Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == item);
                        if (newPointBatchItem != null)
                        {
                            newPointBatchItem.PointState = "2";
                            newPointBatchItem.PointDate = DateTime.Now;
                            newPointBatchItem.RepairRecordId = repairRecordId;
                            db.SubmitChanges();
                        }
                    }
                }
                //更新返修打底/盖面焊工
                //JArray teamGroupData = Grid1.GetMergedData();
                //foreach (JObject mergedRow in Grid1.GetMergedData())
                //{
                //    int i = mergedRow.Value<int>("index");
                //    JObject values = mergedRow.Value<JObject>("values");
                //    string PointBatchItemId = Grid1.DataKeys[i][0].ToString();
                //    Model.HJGL_Batch_PointBatchItem newPointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == PointBatchItemId);
                //    if (newPointBatchItem != null)
                //    {
                //        System.Web.UI.WebControls.DropDownList drpBackingWelderId = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpBackingWelderId"));
                //        if (drpBackingWelderId.SelectedValue != BLL.Const._Null)
                //        {
                //            newPointBatchItem.PBackingWelderId = drpBackingWelderId.SelectedValue;
                //        }
                //        System.Web.UI.WebControls.DropDownList drpCoverWelderId = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpCoverWelderId"));
                //        if (drpCoverWelderId.SelectedValue != BLL.Const._Null)
                //        {
                //            newPointBatchItem.PCoverWelderId = drpCoverWelderId.SelectedValue;
                //        }
                //        db.SubmitChanges();
                //    }

                //}

                BindGrid();
                Alert.ShowInTop("保存成功！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("已审核，不能修改保存！", MessageBoxIcon.Warning);
            }
        }

        protected void btnPointAudit_Click(object sender, EventArgs e)
        {
            Model.SGGLDB db = Funs.DB;
            string repairRecordId = tvControlItem.SelectedNodeID;

            // 更新返修记录
            var repair = db.HJGL_RepairRecord.FirstOrDefault(x => x.RepairRecordId == repairRecordId);
            if (BLL.RepairRecordService.GetExportNum(repairRecordId) > 0)
            {
                if (!repair.AuditDate.HasValue)
                {
                    repair.AuditDate = DateTime.Now;
                    db.SubmitChanges();
                }
                else
                {
                    Alert.ShowInTop("已审核！");
                }
            }
            else
            {
                Alert.ShowInTop("请先保存已点的扩透口再审核！");
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            Model.SGGLDB db = Funs.DB;
            string repairRecordId = tvControlItem.SelectedNodeID;
            Model.HJGL_RepairRecord repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            var trustItem1 = (from x in Funs.DB.HJGL_Batch_BatchTrustItem
                              join y in Funs.DB.HJGL_Batch_NDEItem on x.TrustBatchItemId equals y.TrustBatchItemId
                              where y.NDEItemID == repairRecord.NDEItemID
                              select x).FirstOrDefault();
            if (trustItem1 != null)
            {
                var trustItems2 = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.TrustBatchId == trustItem1.TrustBatchId select x;
                foreach (var item in trustItems2)
                {
                    var ndeItem = Funs.DB.HJGL_Batch_NDEItem.FirstOrDefault(x => x.TrustBatchItemId == item.TrustBatchItemId);
                    if (ndeItem != null)
                    {
                        var repair = Funs.DB.HJGL_RepairRecord.FirstOrDefault(x => x.NDEItemID == ndeItem.NDEItemID);
                        if (repair != null)
                        {
                            var trustItem3s = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.RepairRecordId == repair.RepairRecordId select x;
                            if (trustItem3s.Count() > 0)
                            {
                                Alert.ShowInTop("本次返修已委托！", MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }
            var trustItem = from x in Funs.DB.HJGL_Batch_BatchTrustItem where x.RepairRecordId == repairRecordId select x;
            if (trustItem.Count() == 0)
            {
                if (!string.IsNullOrEmpty(repairRecordId) && repairRecord.AuditDate.HasValue)
                {
                    // 返修委托
                    Model.HJGL_Batch_BatchTrust newRepairTrust = new Model.HJGL_Batch_BatchTrust();
                    string trustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                    newRepairTrust.TrustBatchId = trustBatchId;
                    newRepairTrust.TrustBatchCode = repairRecord.RepairRecordCode;
                    newRepairTrust.TrustDate = DateTime.Now;
                    newRepairTrust.ProjectId = repairRecord.ProjectId;
                    newRepairTrust.UnitId = repairRecord.UnitId;
                    newRepairTrust.UnitWorkId = repairRecord.UnitWorkId;
                    newRepairTrust.DetectionTypeId = repairRecord.DetectionTypeId;
                    newRepairTrust.TrustType = "R";
                    BLL.Batch_BatchTrustService.AddBatchTrust(newRepairTrust);  // 新增返修委托单

                    Model.HJGL_Batch_BatchTrustItem newRepairTrustItem = new Model.HJGL_Batch_BatchTrustItem();
                    newRepairTrustItem.TrustBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrustItem));
                    newRepairTrustItem.TrustBatchId = trustBatchId;
                    newRepairTrustItem.RepairRecordId = repairRecordId;
                    newRepairTrustItem.WeldJointId = repairRecord.WeldJointId;
                    newRepairTrustItem.CreateDate = DateTime.Now;
                    Batch_BatchTrustItemService.AddBatchTrustItem(newRepairTrustItem);

                    // 扩透委托
                    var exp = BLL.RepairRecordService.GetExportItem(repairRecordId);
                    if (exp != null)
                    {
                        //string exportTrustCode = repairRecord.RepairRecordCode.Substring(0, repairRecord.RepairRecordCode.Length - 2) + "K1";
                        //Model.HJGL_Batch_BatchTrust newExportTrust = new Model.HJGL_Batch_BatchTrust();
                        //string exporttrustBatchId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrust));
                        //newExportTrust.TrustBatchId = exporttrustBatchId;
                        //newExportTrust.TrustBatchCode = exportTrustCode;
                        //newExportTrust.TrustDate = DateTime.Now;
                        //newExportTrust.ProjectId = repairRecord.ProjectId;
                        //newExportTrust.UnitId = repairRecord.UnitId;
                        //newExportTrust.UnitWorkId = repairRecord.UnitWorkId;
                        //newExportTrust.DetectionTypeId = repairRecord.DetectionTypeId;

                        //BLL.Batch_BatchTrustService.AddBatchTrust(newExportTrust);  // 新增扩透委托单
                        foreach (var q in exp)
                        {
                            Model.HJGL_Batch_BatchTrustItem newExportTrustItem = new Model.HJGL_Batch_BatchTrustItem();
                            newExportTrustItem.TrustBatchItemId = SQLHelper.GetNewID(typeof(Model.HJGL_Batch_BatchTrustItem));
                            newExportTrustItem.TrustBatchId = trustBatchId;
                            newExportTrustItem.PointBatchItemId = q.PointBatchItemId;
                            newExportTrustItem.WeldJointId = q.WeldJointId;
                            newExportTrustItem.CreateDate = DateTime.Now;
                            Batch_BatchTrustItemService.AddBatchTrustItem(newExportTrustItem);

                            Model.HJGL_Batch_PointBatchItem pointBatchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.PointBatchItemId == q.PointBatchItemId);
                            pointBatchItem.IsBuildTrust = true;
                            db.SubmitChanges();
                        }
                    }
                    Alert.ShowInTop("成功生成委托单！", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop("选中返修单并确认已审核！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                Alert.ShowInTop("已生成委托单！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSee_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID))
            {
                string window = String.Format("SeeFilm.aspx?repairRecordId={0}", tvControlItem.SelectedNodeID, "查看底片 - ");
                PageContext.RegisterStartupScript(Window1.GetShowReference(window));
            }
        }


        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ckbWelder_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            //string repairRecordId = tvControlItem.SelectedNodeID;
            //var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
        }

        protected void ckbBatch_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            //string repairRecordId = tvControlItem.SelectedNodeID;
            //var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
        }

        protected void ckbPipe_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            //string repairRecordId = tvControlItem.SelectedNodeID;
            //var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
        }

        protected void ckbDaily_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            //string repairRecordId = tvControlItem.SelectedNodeID;
            //var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
        }

        protected void ckbRepairBefore_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            //string repairRecordId = tvControlItem.SelectedNodeID;
            //var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
        }

        protected void ckbMat_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            //string repairRecordId = tvControlItem.SelectedNodeID;
            //var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
        }

        protected void ckbSpec_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            //string repairRecordId = tvControlItem.SelectedNodeID;
            //var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            //if (!repairRecord.AuditDate.HasValue)
            //{
            //    RandomExport(repairRecord.RepairMark);
            //}
        }

        private void RandomExport(string mark)
        {
            int num = Grid1.Rows.Count;
            if (mark == "R1")
            {
                if (num > 0 && num <= 2)
                {
                    if (num == 1)
                    {
                        Grid1.SelectedRowIndexArray = new int[] { 0 };
                    }
                    else
                    {
                        Grid1.SelectedRowIndexArray = new int[] { 0, 1 };
                    }
                }
                else
                {
                    int[] r = Funs.GetRandomNum(2, 0, num - 1);
                    Grid1.SelectedRowIndexArray = r;
                }
            }
            else
            {
                int[] groupNum = new int[num];
                for (int i = 0; i < num; i++)
                {
                    groupNum[i] = i;
                }

                Grid1.SelectedRowIndexArray = groupNum;
            }
        }
    }
}