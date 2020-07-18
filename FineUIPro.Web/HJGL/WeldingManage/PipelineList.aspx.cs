using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class PipelineList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.InitTreeMenu();//加载树
                //显示列
                //Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "Pipeline");
                //if (c != null)
                //{
                //    this.GetShowColumn(c.Columns);
                //}
            }
        }

        protected void drpProjectId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }

        #region 加载树装置-单位-工作区
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
                    tn1.Text = q.UnitWorkName + "【" + a.ToString() + "】" + "管线";
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
                    tn2.Text = q.UnitWorkName + "【" + a.ToString() + "】" + "管线";
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
            string strSql = @"SELECT ProjectId,UnitWorkId,PipelineId,PipelineCode,UnitName,MediumCode,PipingClassCode,UnitWorkCode,
                                     TestPressure,SingleNumber,DetectionRateCode,DetectionType,Remark,TestMediumCode,TotalDin,JointCount
                              FROM dbo.View_HJGL_Pipeline WHERE ProjectId= @ProjectId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));

            strSql += " AND UnitWorkId =@UnitWorkId";
            listStr.Add(new SqlParameter("@UnitWorkId", this.tvControlItem.SelectedNodeID));
            if (!string.IsNullOrEmpty(this.txtPipelineCode.Text.Trim()))
            {
                strSql += " AND PipelineCode LIKE @PipelineCode";
                listStr.Add(new SqlParameter("@PipelineCode", "%" + this.txtPipelineCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtSingleNumber.Text.Trim()))
            {
                strSql += " AND SingleNumber LIKE @SingleNumber";
                listStr.Add(new SqlParameter("@SingleNumber", "%" + this.txtSingleNumber.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWorkAreaCode.Text.Trim()))
            {
                strSql += " AND UnitWorkCode LIKE @UnitWorkCode";
                listStr.Add(new SqlParameter("@UnitWorkCode", "%" + this.txtWorkAreaCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            this.OutputSummaryData(tb); ///取合计值
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 计算合计
        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData(DataTable tb)
        {
            decimal count2 = 0;//总达因数
            int count3 = 0;//总焊口数          
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                count2 += Funs.GetNewDecimalOrZero(tb.Rows[i]["TotalDin"].ToString());
                count3 += Funs.GetNewIntOrZero(tb.Rows[i]["JointCount"].ToString());
            }
            JObject summary = new JObject();
            summary.Add("PipelineCode", "合计");
            summary.Add("TotalDin", count2);
            summary.Add("JointCount", count3);
            Grid1.SummaryData = summary;
        }
        #endregion

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

        #region 管线信息 维护事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_PipelineMenuId, BLL.Const.BtnModify))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PipelineEdit.aspx?PipelineId={0}", Grid1.SelectedRowID, "编辑 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 增加管线信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipelineMenuId, Const.BtnAdd))
            {
                var workArea = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
                if (workArea != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PipelineEdit.aspx?UnitWorkId={0}", this.tvControlItem.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    ShowNotify("请先选择区域", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 管线信息编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_PipelineMenuId, BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PipelineEdit.aspx?PipelineId={0}", Grid1.SelectedRowID, "维护 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipelineMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                    return;
                }

                bool isShow = true;
                if (Grid1.SelectedRowIndexArray.Length > 1)
                {
                    isShow = false;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, isShow))
                    {
                        BLL.PipelineService.DeletePipeline(rowID);
                        //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_PipelineMenuId, Const.BtnDelete, rowID);
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }

                this.BindGrid();
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 关闭弹出窗口及刷新页面
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
            this.BindGrid();
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

            string jotInfo = string.Empty;
            var q = from x in Funs.DB.HJGL_WeldJoint where x.PipelineId == id && x.WeldingDailyId != null select x;
            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    jotInfo += "焊口号" + "：" + item.WeldJointCode;
                    var dr = Funs.DB.HJGL_WeldingDaily.FirstOrDefault(x => x.WeldingDailyId == item.WeldingDailyId);
                    if (dr != null)
                    {
                        jotInfo += "；" + "焊接日报告号" + ":" + dr.WeldingDailyCode;
                    }
                }

                content = "该管线已焊焊口" + ":" + jotInfo;
            }
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content, MessageBoxIcon.Error);
                }
                return false;
            }
        }
        #endregion

        #region 选择要显示列
        /// <summary>
        /// 选择显示列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectColumn_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("PipelineShowColumn.aspx", "显示列 - ")));
        }
        #endregion

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            //this.BindGrid();
            ////显示列
            //Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "Pipeline");
            //if (c != null)
            //{
            //    this.GetShowColumn(c.Columns);
            //}
        }

        #region 显示的列
        /// <summary>
        /// 显示的列
        /// </summary>
        /// <param name="column"></param>
        private void GetShowColumn(string column)
        {
            if (!string.IsNullOrEmpty(column))
            {
                this.Grid1.Columns[1].Hidden = true;
                this.Grid1.Columns[2].Hidden = true;
                this.Grid1.Columns[3].Hidden = true;
                this.Grid1.Columns[4].Hidden = true;
                this.Grid1.Columns[5].Hidden = true;
                this.Grid1.Columns[6].Hidden = true;
                this.Grid1.Columns[7].Hidden = true;
                this.Grid1.Columns[8].Hidden = true;
                this.Grid1.Columns[9].Hidden = true;
                this.Grid1.Columns[10].Hidden = true;
                this.Grid1.Columns[11].Hidden = true;
                this.Grid1.Columns[12].Hidden = true;
                this.Grid1.Columns[13].Hidden = true;
                this.Grid1.Columns[14].Hidden = true;
                this.Grid1.Columns[15].Hidden = true;
                this.Grid1.Columns[16].Hidden = true;
                this.Grid1.Columns[17].Hidden = true;
                this.Grid1.Columns[18].Hidden = true;
                this.Grid1.Columns[19].Hidden = true;
                this.Grid1.Columns[20].Hidden = true;
                this.Grid1.Columns[21].Hidden = true;

                List<string> columns = column.Split(',').ToList();
                foreach (var item in columns)
                {
                    this.Grid1.Columns[Convert.ToInt32(item)].Hidden = false;
                }
            }
        }
        #endregion

        protected string ConvertDetectionType(object detectionType)
        {
            string detectionName = string.Empty;
            if (detectionType != null)
            {
                string[] types = detectionType.ToString().Split('|');
                foreach (string t in types)
                {
                    var type = BLL.Base_DetectionTypeService.GetDetectionTypeByDetectionTypeId(t);
                    if (type != null)
                    {
                        detectionName += type.DetectionTypeName + ",";
                    }
                }
            }
            if (detectionName != string.Empty)
            {
                return detectionName.Substring(0, detectionName.Length - 1);
            }
            else
            {
                return "";
            }
        }

        
    }
}