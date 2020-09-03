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

namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class WeldTask : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtTaskDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
                this.txtTaskDate.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
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
                }
            }

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
            if (!string.IsNullOrEmpty(tvControlItem.SelectedNodeID))
            {
                var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
                string UnitId = w.UnitId;
                string UnitWorkId = w.UnitWorkId;
                string strList = UnitWorkId + "|" + UnitId;
                string weldJointIds = hdItemsString.Text.Trim();
                string window = String.Format("SelectTaskWeldJoint.aspx?strList={0}&weldJointIds={1}", strList, Server.UrlEncode(weldJointIds), "编辑 - ");
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdItemsString.ClientID) + Window1.GetShowReference(window));
            }
            else
            {
                Alert.ShowInTop("请选择单位和单位工程", MessageBoxIcon.Warning);
            }
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            List<Model.SpWeldingDailyItem> GetWeldingDailyItem = BLL.WeldTaskService.GetWeldTaskListAddItem(this.hdItemsString.Text);
            this.BindGrid(GetWeldingDailyItem);
        }

        #endregion
        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_WeldTaskMenuId, Const.BtnSave))
            {
                List<Model.SpWeldingDailyItem> getNewWeldTaskItem = CollectGridJointInfo();
                var getUnit = BLL.UnitWorkService.getUnitWorkByUnitWorkId(tvControlItem.SelectedNodeID);
                foreach (var item in getNewWeldTaskItem)
                {
                    Model.HJGL_WeldTask NewTask = new Model.HJGL_WeldTask();
                    NewTask.ProjectId = this.CurrUser.LoginProjectId;
                    NewTask.UnitWorkId = this.tvControlItem.SelectedNodeID;
                    if (getUnit != null)
                    {
                        NewTask.UnitId = getUnit.UnitId;
                    }
                    NewTask.WeldJointId = item.WeldJointId;
                    NewTask.TaskDate = DateTime.Now.AddDays(1);
                    NewTask.CoverWelderId = item.CoverWelderId;
                    NewTask.BackingWelderId = item.BackingWelderId;
                    NewTask.JointAttribute = item.JointAttribute;
                    NewTask.WelderCodeStr = item.WelderCodeStr;
                    NewTask.Tabler = this.CurrUser.UserId;
                    NewTask.TableDate = DateTime.Now;
                    NewTask.WeldingMode = item.WeldingMode;
                    var task = Funs.DB.HJGL_WeldTask.FirstOrDefault(x => x.UnitWorkId == tvControlItem.SelectedNodeID && x.WeldJointId == item.WeldJointId);
                    if (task != null)
                    {
                        NewTask.WeldTaskId = task.WeldTaskId;
                        BLL.WeldTaskService.UpdateWeldTask(NewTask);
                    }
                    else
                    {
                        NewTask.WeldTaskId = SQLHelper.GetNewID(typeof(Model.HJGL_WeldTask));
                        BLL.WeldTaskService.AddWeldTask(NewTask);
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
                string rowID = values.Value<string>("WeldJointId").ToString();
                NewItem.WeldJointId = rowID;
                if (!string.IsNullOrEmpty(values.Value<string>("PipelineCode")))
                {
                    NewItem.PipelineCode = values.Value<string>("PipelineCode").ToString();

                }
                if (!string.IsNullOrEmpty(values.Value<string>("WeldJointCode")))
                {
                    NewItem.WeldJointCode = values.Value<string>("WeldJointCode").ToString();

                }
                if (!string.IsNullOrEmpty(values.Value<string>("WelderCodeStr")))
                {
                    NewItem.WelderCodeStr = values.Value<string>("WelderCodeStr").ToString();

                }
                var coverWelderCode = (from x in Funs.DB.SitePerson_Person
                                       where x.ProjectId == CurrUser.LoginProjectId && x.WelderCode == values.Value<string>("CoverWelderId")
                                       select x).FirstOrDefault();
                if (coverWelderCode != null)
                {
                    NewItem.CoverWelderCode = coverWelderCode.WelderCode;
                    NewItem.CoverWelderId = coverWelderCode.PersonId;
                }
                var backingWelderCode = (from x in Funs.DB.SitePerson_Person
                                         where x.ProjectId == CurrUser.LoginProjectId && x.WelderCode == values.Value<string>("BackingWelderId")
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
                        var item = getNewWeldTaskItem.FirstOrDefault(x => x.WeldJointId == rowID);
                        if (item != null)
                        {
                            getNewWeldTaskItem.Remove(item);
                            // 删除明细信息
                            var task = Funs.DB.HJGL_WeldTask.FirstOrDefault(x => x.UnitWorkId == tvControlItem.SelectedNodeID && x.WeldJointId == rowID);
                            if (task != null)
                            {
                                BLL.WeldTaskService.DeleteWeldingTask(task.WeldTaskId);
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
            string strSql = " select T.WeldJointId,T.CoverWelderId,T.BackingWelderId,T.JointAttribute,T.WelderCodeStr,T.WeldingMode,jot.WeldJointCode,jot.Dia," +
                "jot.Thickness,jot.Size,jot.WeldingLocationId,P.PipelineCode,B.WeldTypeCode,M.WeldingMethodCode,L.WeldingLocationCode from HJGL_WeldTask T" +
                " left join HJGL_WeldJoint jot on T.WeldJointId=jot.WeldJointId  left join HJGL_Pipeline P on jot.PipelineId=P.PipelineId " +
                " left join Base_WeldType B on jot.WeldTypeId=B.WeldTypeId left join Base_WeldingMethod M on jot.WeldingMethodId=M.WeldingMethodId " +
                " left join Base_WeldingLocation L on jot.WeldingLocationId=L.WeldingLocationId where T.ProjectId=@ProjectId And T.UnitWorkId = @UnitWorkId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            listStr.Add(new SqlParameter("@UnitWorkId", this.tvControlItem.SelectedNodeID));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #endregion
    }
}