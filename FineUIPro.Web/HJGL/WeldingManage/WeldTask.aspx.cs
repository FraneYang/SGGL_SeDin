﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class WeldTask : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTaskDate.MinDate = DateTime.Now;
                this.txtTaskDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
                this.txtTaskDateMonth.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                ///焊接属性
                this.drpJointAttribute.DataTextField = "Text";
                this.drpJointAttribute.DataValueField = "Value";
                this.drpJointAttribute.DataSource = BLL.DropListService.HJGL_JointAttribute();
                this.drpJointAttribute.DataBind();
                ///机动化程度
                this.drpWeldingMode.DataTextField = "Text";
                this.drpWeldingMode.DataValueField = "Value";
                this.drpWeldingMode.DataSource = BLL.DropListService.HJGL_WeldingMode();
                this.drpWeldingMode.DataBind();
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.InitTreeMenu();//加载树
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
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn1 = new TreeNode();
                    tn1.NodeID = q.UnitWorkId;
                    tn1.Text = q.UnitWorkName;
                    tn1.ToolTip = "施工单位：" + u.UnitName;
                    tn1.EnableClickEvent = true;
                    rootNode1.Nodes.Add(tn1);
                    BindNodes(tn1);
                }
            }
            if (unitWork2.Count() > 0)
            {
                foreach (var q in unitWork2)
                {
                    var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                    TreeNode tn2 = new TreeNode();
                    tn2.NodeID = q.UnitWorkId;
                    tn2.Text = q.UnitWorkName;
                    tn2.ToolTip = "施工单位：" + u.UnitName;
                    tn2.EnableClickEvent = true;
                    rootNode2.Nodes.Add(tn2);
                    BindNodes(tn2);
                }
            }

        }

        private void BindNodes(TreeNode node)
        {
            var p = (from x in Funs.DB.HJGL_WeldTask
                    where x.UnitWorkId == node.NodeID
                         && x.TaskDate < Convert.ToDateTime(this.txtTaskDateMonth.Text.Trim() + "-01").AddMonths(1)
                         && x.TaskDate >= Convert.ToDateTime(this.txtTaskDateMonth.Text.Trim() + "-01")
                    select x.TaskDate.Value.Date).Distinct();
            if (p.Count() > 0)
            {
                foreach (var item in p)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = string.Format("{0:yyyy-MM-dd}", item);
                    newNode.NodeID = string.Format("{0:yyyy-MM-dd}", item);
                    newNode.EnableClickEvent = true;
                    node.Nodes.Add(newNode);
                }
            }

        }

        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
        }

        private void BindGrid(List<Model.SpWeldingDailyItem> GetWeldingDailyItem)
        {

            DataTable tb = this.LINQToDataTable(GetWeldingDailyItem);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            List<Model.SpWeldingDailyItem> GetWeldingTaskItem = this.CollectGridJointInfo();
            this.BindGrid(GetWeldingTaskItem);
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
            List<Model.SpWeldingDailyItem> GetWeldingTaskItem = this.CollectGridJointInfo();
            this.BindGrid(GetWeldingTaskItem);
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
            List<Model.SpWeldingDailyItem> GetWeldingTaskItem = this.CollectGridJointInfo();
            this.BindGrid(GetWeldingTaskItem);
        }
        #endregion

        #endregion

        #region 查找
        protected void ckSelect_Click(object sender, EventArgs e)
        {
            var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
            if (w != null)
            {
                string UnitId = w.UnitId;
                string UnitWorkId = w.UnitWorkId;
                string strList = UnitWorkId + "|" + UnitId;
                string weldJointIds = hdItemsString.Text.Trim();

                var task = from x in Funs.DB.HJGL_WeldTask where x.UnitWorkId == w.UnitWorkId && x.TaskDate.Value.Date.ToString() == Convert.ToDateTime(txtTaskDate.Text.Trim()).ToString("yyyy-MM-dd") select x;
                this.hdTaskWeldJoint.Text = string.Empty;
                if (task.Count() > 0)
                {
                    foreach (var item in task)
                    {
                        this.hdTaskWeldJoint.Text += item.WeldJointId + "|";
                    }
                }
                if (!string.IsNullOrEmpty(hdTaskWeldJoint.Text))
                {
                    hdTaskWeldJoint.Text = hdTaskWeldJoint.Text.Substring(0, hdTaskWeldJoint.Text.Length - 1);
                }
                string TaskWeldJoints = hdTaskWeldJoint.Text.Trim();//任务表已存在的焊口
                string window = String.Format("SelectTaskWeldJoint.aspx?strList={0}&weldJointIds={1}&TaskWeldJoints={2}", strList, Server.UrlEncode(weldJointIds), Server.UrlEncode(TaskWeldJoints), "编辑 - ");
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdItemsString.ClientID, hdTaskWeldJoint.ClientID) + Window1.GetShowReference(window));
            }
            else
            {
                Alert.ShowInTop("请选择单位和单位工程", MessageBoxIcon.Warning);
            }
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            List<Model.SpWeldingDailyItem> GetWeldingTaskList = BLL.WeldTaskService.GetWeldingTaskList(this.CurrUser.LoginProjectId, tvControlItem.SelectedNodeID, Convert.ToDateTime(txtTaskDate.Text.Trim()));//任务表已存在的焊口
            List<Model.SpWeldingDailyItem> GetWeldingDailyItem = BLL.WeldTaskService.GetWeldTaskListAddItem(this.hdItemsString.Text);
            List<Model.SpWeldingDailyItem> Result = GetWeldingTaskList.Union(GetWeldingDailyItem).ToList();
            this.BindGrid(Result);
        }

        #endregion
        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldTaskMenuId, Const.BtnSave))
            {
                List<Model.SpWeldingDailyItem> getNewWeldTaskItem = CollectGridJointInfo();
                var getUnit = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
                if (getUnit == null)
                {
                    getUnit = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNode.ParentNode.NodeID);
                }

                if (getUnit != null)
                {
                    foreach (var item in getNewWeldTaskItem)
                    {
                        Model.HJGL_WeldTask NewTask = new Model.HJGL_WeldTask();
                        NewTask.ProjectId = this.CurrUser.LoginProjectId;
                        NewTask.UnitWorkId = getUnit.UnitWorkId;
                        if (getUnit != null)
                        {
                            NewTask.UnitId = getUnit.UnitId;
                        }

                        NewTask.WeldTaskId = item.WeldTaskId;
                        NewTask.WeldJointId = item.WeldJointId;
                        NewTask.CoverWelderId = item.CoverWelderId;
                        NewTask.BackingWelderId = item.BackingWelderId;
                        NewTask.JointAttribute = item.JointAttribute;
                        NewTask.WeldingMode = item.WeldingMode;
                        var task = Funs.DB.HJGL_WeldTask.FirstOrDefault(x => x.WeldTaskId == item.WeldTaskId);
                        if (task == null)
                        {
                            NewTask.TaskDate = Convert.ToDateTime(txtTaskDate.Text);
                            NewTask.Tabler = this.CurrUser.UserId;
                            NewTask.TableDate = DateTime.Now;
                            BLL.WeldTaskService.AddWeldTask(NewTask);
                        }
                        else
                        {
                            NewTask.TaskDate = task.TaskDate;
                            NewTask.Tabler = task.Tabler;
                            NewTask.TableDate = task.TaskDate;
                            BLL.WeldTaskService.UpdateWeldTask(NewTask);
                        }
                    }
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        protected void CreatWeldableWeldJoint_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid1.Rows.Count(); i++)
            {
                string weldTaskId = Grid1.DataKeys[i][0].ToString();
                string jotId = Grid1.DataKeys[i][1].ToString();
                var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(jotId);
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
                string canWelderId = string.Empty;
                string canWelderCode = string.Empty;

                var projectWelder = from x in Funs.DB.SitePerson_Person
                                    where x.ProjectId == jot.ProjectId
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
                            canWelderId = canWelderId + welder.PersonId + ",";
                            canWelderCode= canWelderCode+welder.WelderCode + ",";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(canWelderId))
                {
                    BLL.WeldTaskService.UpdateCanWelderTask(weldTaskId, canWelderId.Substring(0, canWelderId.Length - 1), canWelderCode.Substring(0, canWelderCode.Length - 1));
                }

                DateTime? taskTime = Funs.GetNewDateTime(tvControlItem.SelectedNodeID);
                if (taskTime != null)
                {
                    List<Model.SpWeldingDailyItem> GetWeldingTaskList = BLL.WeldTaskService.GetWeldingTaskList(this.CurrUser.LoginProjectId, tvControlItem.SelectedNode.ParentNode.NodeID, Convert.ToDateTime(tvControlItem.SelectedNodeID));
                    this.BindGrid(GetWeldingTaskList);
                    GetCanWelderDropDownList(GetWeldingTaskList);
                }
                else
                {
                    List<Model.SpWeldingDailyItem> GetWeldingTaskList = BLL.WeldTaskService.GetWeldingTaskList(this.CurrUser.LoginProjectId, tvControlItem.SelectedNodeID, Convert.ToDateTime(txtTaskDate.Text));
                    this.BindGrid(GetWeldingTaskList);
                    GetCanWelderDropDownList(GetWeldingTaskList);
                }
            }

            Alert.ShowInTop("已生成可焊焊工！", MessageBoxIcon.Success);

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

        #region 收集Grid页面信息
        /// <summary>
        /// 收集Grid页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.SpWeldingDailyItem> CollectGridJointInfo()
        {
            List<Model.SpWeldingDailyItem> getNewWeldTaskItem = new List<Model.SpWeldingDailyItem>();
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {

                Model.SpWeldingDailyItem NewItem=new Model.SpWeldingDailyItem ();
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");

                NewItem.WeldTaskId= values.Value<string>("WeldTaskId").ToString();
                NewItem.WeldJointId = values.Value<string>("WeldJointId").ToString();

                if (!string.IsNullOrEmpty(values.Value<string>("PipelineCode")))
                {
                    NewItem.PipelineCode = values.Value<string>("PipelineCode").ToString();

                }
                if (!string.IsNullOrEmpty(values.Value<string>("WeldJointCode")))
                {
                    NewItem.WeldJointCode = values.Value<string>("WeldJointCode").ToString();
                }
                if (!string.IsNullOrEmpty(values.Value<string>("CanWelderCode")))
                {
                    NewItem.CanWelderCode = values.Value<string>("CanWelderCode").ToString();
                }
                if (!string.IsNullOrEmpty(values.Value<string>("CanWelderId")))
                {
                    NewItem.CanWelderId = values.Value<string>("CanWelderId").ToString();
                }
                if (!string.IsNullOrEmpty(values.Value<string>("IsWelding")))
                {
                    NewItem.IsWelding = values.Value<string>("IsWelding").ToString();
                }
                var coverWelderCode = (from x in Funs.DB.SitePerson_Person
                                       where x.ProjectId == CurrUser.LoginProjectId && x.WelderCode == values.Value<string>("CoverWelderCode")
                                       select x).FirstOrDefault();
                if (coverWelderCode != null)
                {
                    NewItem.CoverWelderCode = coverWelderCode.WelderCode;
                    NewItem.CoverWelderId = coverWelderCode.PersonId;
                }
                var backingWelderCode = (from x in Funs.DB.SitePerson_Person
                                         where x.ProjectId == CurrUser.LoginProjectId && x.WelderCode == values.Value<string>("BackingWelderCode")
                                         select x).FirstOrDefault();
                if (backingWelderCode != null)
                {
                    NewItem.BackingWelderCode = backingWelderCode.WelderCode;
                    NewItem.BackingWelderId = backingWelderCode.PersonId;
                }
                if (!string.IsNullOrEmpty(values.Value<string>("JointAttribute").ToString()))
                {
                    NewItem.JointAttribute = values.Value<string>("JointAttribute").ToString();
                }
                if (!string.IsNullOrEmpty(values.Value<string>("WeldingMode")))
                {
                    NewItem.WeldingMode = values.Value<string>("WeldingMode").ToString();
                }
                if(!string.IsNullOrEmpty(values.Value<string>("WeldTypeCode")))
                {
                    NewItem.WeldTypeCode = values.Value<string>("WeldTypeCode").ToString();
                }
                if (!string.IsNullOrEmpty(values.Value<int>("Size").ToString()))
                {
                    NewItem.Size = values.Value<int>("Size");
                }
                if (!string.IsNullOrEmpty(values.Value<int>("Dia").ToString()))
                {
                    NewItem.Dia = values.Value<int>("Dia");
                }
                if (!string.IsNullOrEmpty(values.Value<int>("Thickness").ToString()))
                {
                    NewItem.Thickness = values.Value<int>("Thickness");
                }
                if (!string.IsNullOrEmpty(values.Value<string>("WeldingMethodCode").ToString()))
                {
                    NewItem.WeldingMethodCode = values.Value<string>("WeldingMethodCode").ToString();
                }
                getNewWeldTaskItem.Add(NewItem);

            }
            return getNewWeldTaskItem;
        }
        #endregion

        #region 删除
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldTaskMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    List<Model.SpWeldingDailyItem> getNewWeldTaskItem = this.CollectGridJointInfo();
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var item = getNewWeldTaskItem.FirstOrDefault(x => x.WeldTaskId == rowID);
                        if (item != null)
                        {
                            getNewWeldTaskItem.Remove(item);
                            // 删除明细信息
                            var task = Funs.DB.HJGL_WeldTask.FirstOrDefault(x => x.WeldTaskId == rowID);
                            if (task != null)
                            {
                                BLL.WeldTaskService.DeleteWeldingTask(task.WeldTaskId);
                            }
                        }
                    }
                    BindGrid(getNewWeldTaskItem);
                    ShowNotify("删除成功！", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        #region 点击TreeView
        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
            if (w == null)
            {
                DateTime? taskTime = Funs.GetNewDateTime(tvControlItem.SelectedNodeID);
                if (taskTime != null)
                {
                    List<Model.SpWeldingDailyItem> GetWeldingTaskList = BLL.WeldTaskService.GetWeldingTaskList(this.CurrUser.LoginProjectId, tvControlItem.SelectedNode.ParentNode.NodeID, Convert.ToDateTime(tvControlItem.SelectedNodeID));
                    this.BindGrid(GetWeldingTaskList);
                    GetCanWelderDropDownList(GetWeldingTaskList);

                    if (taskTime.Value.Date < DateTime.Now.Date)
                    {
                        ckSelect.Hidden = true;
                        btnSave.Hidden = true;
                        CreatWeldableWeldJoint.Hidden = true;
                        btnSaveWelder.Hidden = true;
                        txtTaskDate.Hidden = true;
                    }
                    else
                    {
                        ckSelect.Hidden = false;
                        btnSave.Hidden = false;
                        CreatWeldableWeldJoint.Hidden = false;
                        btnSaveWelder.Hidden = false;
                        txtTaskDate.Hidden = false;
                    }
                }
                
            }
            else
            {
                ckSelect.Hidden = false;
                btnSave.Hidden = false;
                CreatWeldableWeldJoint.Hidden = false;
                btnSaveWelder.Hidden = false;
                txtTaskDate.Hidden = false;

                this.BindGrid(null);
               
            }

        }

        #endregion

        #region 批量填充焊工
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveWelder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(drpCanWelder.SelectedValue))
            {
                for (int i = 0; i < Grid1.Rows.Count(); i++)
                {
                    string weldTaskId = Grid1.DataKeys[i][0].ToString();
                    BLL.WeldTaskService.UpdateWelderTask(weldTaskId, drpCanWelder.SelectedValue);
                }

                DateTime? taskTime = Funs.GetNewDateTime(tvControlItem.SelectedNodeID);
                if (taskTime != null)
                {
                    List<Model.SpWeldingDailyItem> GetWeldingTaskList = BLL.WeldTaskService.GetWeldingTaskList(this.CurrUser.LoginProjectId, tvControlItem.SelectedNode.ParentNode.NodeID, Convert.ToDateTime(tvControlItem.SelectedNodeID));
                    this.BindGrid(GetWeldingTaskList);
                    GetCanWelderDropDownList(GetWeldingTaskList);
                }
                else
                {
                    List<Model.SpWeldingDailyItem> GetWeldingTaskList = BLL.WeldTaskService.GetWeldingTaskList(this.CurrUser.LoginProjectId, tvControlItem.SelectedNodeID, Convert.ToDateTime(txtTaskDate.Text));
                    this.BindGrid(GetWeldingTaskList);
                    GetCanWelderDropDownList(GetWeldingTaskList);
                }
            }
        }

        /// <summary>
        /// 获取能焊焊工下拉列表
        /// </summary>
        /// <param name="getWeldingTaskList"></param>
        private void GetCanWelderDropDownList(List<Model.SpWeldingDailyItem> getWeldingTaskList)
        {
            if (getWeldingTaskList.Count() > 0)
            {
                List<string> canWelder = null;
                int i = 0;
                foreach (var item in getWeldingTaskList)
                {
                    if (!string.IsNullOrEmpty(item.CanWelderId))
                    {
                        List<string> jotCanWelder = item.CanWelderId.Split(',').ToList();
                        if (i == 0)
                        {
                            canWelder = jotCanWelder;
                        }
                        else
                        {
                            canWelder = canWelder.Intersect(jotCanWelder).ToList();
                        }
                    }
                    else
                    {
                        canWelder = null;
                        break;
                    }
                    i++;
                }
                if (canWelder != null)
                {
                    var welder = from x in canWelder
                                 join y in Funs.DB.SitePerson_Person on x equals y.PersonId
                                 select new { WelderId = x, y.WelderCode };
                    drpCanWelder.DataValueField = "WelderId";
                    drpCanWelder.DataTextField = "WelderCode";
                    drpCanWelder.DataSource = welder;
                    drpCanWelder.DataBind();
                }
            }
        }
        #endregion
    }
}