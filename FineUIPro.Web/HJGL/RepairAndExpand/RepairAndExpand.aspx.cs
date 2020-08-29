using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

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
                txtWeldJointCode.Text = jot.BackingWelderCode;
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
                if (!string.IsNullOrEmpty(repairRecord.RepairWelder))
                {
                    drpRepairWelder.SelectedValue = repairRecord.RepairWelder;
                }
                else
                {
                    drpRepairWelder.SelectedValue = repairRecord.WelderId;
                }

                if (repairRecord.AuditDate.HasValue)
                {
                    lbIsAudit.Text = "已审核";
                }
                else
                {
                    lbIsAudit.Text = "未审核";
                }

                if (repairRecord.RepairMark == "R2")
                {
                    ckbdaily.Hidden = true;
                    ckbMat.Hidden = true;
                    ckbPipe.Hidden = true;
                    ckbRepairBefore.Hidden = true;
                    ckbSpec.Hidden = true;
                    ckbWelder.Hidden = true;
                    lbdef.Hidden = false;

                }
                else
                {
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
                TreeNode newNode = new TreeNode();
                if (!r.AuditDate.HasValue)
                {
                    newNode.Text = "<font color='#EE0000'>" + r.RepairRecordCode + "</font>";
                }
                else
                {
                    newNode.Text = r.RepairRecordCode;
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
                var day = BLL.WeldingDailyService.GetPipeline_WeldingDailyByWeldingDailyId(jot.WeldingDailyId);

                string strSql = string.Empty;
                DataTable dt = null;
                if (repairRecord.RepairMark == "R1")
                {
                    strSql = @"SELECT PointBatchItemId,PointBatchId,PointBatchCode,WeldJointId,PointState,PointDate,WeldJointCode,
                                     JointArea,Size,WeldingDate,PipelineCode,PipingClassName,PointIsAudit
                              FROM dbo.View_HJGL_Batch_PointBatchItem
                              WHERE ProjectId=@ProjectId AND DetectionTypeId=@DetectionTypeId AND
                                    (PointDate IS NULL OR (PointDate IS NOT NULL AND RepairRecordId=@RepairRecordId))";
                    List<SqlParameter> listStr = new List<SqlParameter>();
                    listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@DetectionTypeId", repairRecord.DetectionTypeId));
                    listStr.Add(new SqlParameter("@RepairRecordId", repairRecordId));
                    if (ckbWelder.Checked)
                    {
                        strSql += " AND WelderId =@WelderId";
                        listStr.Add(new SqlParameter("@WelderId", jot.BackingWelderId));
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
                                     JointArea,Size,WeldingDate,PipelineCode,PipingClassName,PointIsAudit
                              FROM dbo.View_HJGL_Batch_PointBatchItem
                              WHERE ProjectId=@ProjectId AND DetectionTypeId=@DetectionTypeId AND
                                    (PointDate IS NULL OR (PointDate IS NOT NULL AND RepairRecordId=@RepairRecordId))
                                      AND WelderId =@WelderId AND PointBatchId=@PointBatchId";
                    List<SqlParameter> listStr = new List<SqlParameter>();
                    listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
                    listStr.Add(new SqlParameter("@DetectionTypeId", repairRecord.DetectionTypeId));
                    listStr.Add(new SqlParameter("@RepairRecordId", repairRecordId));
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
            }
        }

        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            BLL.WelderService.InitProjectWelderDropDownList(this.drpRepairWelder, true, this.CurrUser.LoginProjectId, repairRecord.UnitId, "请选择");
            PageInit();
            this.BindGrid();

            if (!repairRecord.AuditDate.HasValue)
            {
                RandomExport(repairRecord.RepairMark);
            }
        }

        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.SGGLDB db = Funs.DB;
            string repairRecordId = tvControlItem.SelectedNodeID;
            Model.HJGL_RepairRecord repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);

            if (!repairRecord.AuditDate.HasValue)
            {
                // 更新返修记录
                var repair = db.HJGL_RepairRecord.FirstOrDefault(x => x.RepairRecordId == repairRecordId);
                if (repair != null)
                {
                    repair.RepairWelder = drpRepairWelder.SelectedValue;
                    repair.RepairDate = Convert.ToDateTime(this.txtRepairDate.Text);
                    if (ckbIsCut.Checked)
                    {
                        repair.IsCut = true;
                    }
                }

                // 更新返修口
                var batchItem = db.HJGL_Batch_PointBatchItem.FirstOrDefault(x => x.WeldJointId == repairRecord.WeldJointId);
                if (batchItem != null)
                {
                    batchItem.RepairDate = Convert.ToDateTime(this.txtRepairDate.Text);
                    if (ckbIsCut.Checked)
                    {
                        batchItem.CutDate = DateTime.Now.Date;
                    }
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
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            if (!repairRecord.AuditDate.HasValue)
            {
                RandomExport(repairRecord.RepairMark);
            }
        }

        protected void ckbPipe_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            if (!repairRecord.AuditDate.HasValue)
            {
                RandomExport(repairRecord.RepairMark);
            }
        }

        protected void ckbDaily_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            if (!repairRecord.AuditDate.HasValue)
            {
                RandomExport(repairRecord.RepairMark);
            }
        }

        protected void ckbRepairBefore_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            if (!repairRecord.AuditDate.HasValue)
            {
                RandomExport(repairRecord.RepairMark);
            }
        }

        protected void ckbMat_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            if (!repairRecord.AuditDate.HasValue)
            {
                RandomExport(repairRecord.RepairMark);
            }
        }

        protected void ckbSpec_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid();
            string repairRecordId = tvControlItem.SelectedNodeID;
            var repairRecord = BLL.RepairRecordService.GetRepairRecordById(repairRecordId);
            if (!repairRecord.AuditDate.HasValue)
            {
                RandomExport(repairRecord.RepairMark);
            }
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
                for(int i=0;i<num; i++)
                {
                    groupNum[i] = i;
                }

                Grid1.SelectedRowIndexArray = groupNum;
            }
        }
    }
}