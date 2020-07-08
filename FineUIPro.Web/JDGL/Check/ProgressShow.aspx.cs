using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;
using System.Configuration;

namespace FineUIPro.Web.JDGL.Check
{
    public partial class ProgressShow : PageBase
    {
        #region  页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitTreeMenu();
            }
        }
        #endregion

        #region  加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trWBS.Nodes.Clear();
            this.trWBS.ShowBorder = false;
            this.trWBS.ShowHeader = false;
            this.trWBS.EnableIcons = true;
            this.trWBS.AutoScroll = true;
            this.trWBS.EnableSingleClickExpand = true;

            TreeNode rootNode1 = new TreeNode();
            rootNode1.Text = "建筑工程";
            rootNode1.NodeID = "1";
            rootNode1.CommandName = "ProjectType";
            rootNode1.EnableExpandEvent = true;
            rootNode1.EnableClickEvent = true;
            this.trWBS.Nodes.Add(rootNode1);
            TreeNode emptyNode = new TreeNode();
            emptyNode.Text = "";
            emptyNode.NodeID = "";
            rootNode1.Nodes.Add(emptyNode);
            //this.GetNodes(rootNode1.Nodes, rootNode1.NodeID);

            TreeNode rootNode2 = new TreeNode();
            rootNode2.Text = "安装工程";
            rootNode2.NodeID = "2";
            rootNode2.CommandName = "ProjectType";
            rootNode2.EnableExpandEvent = true;
            rootNode2.EnableClickEvent = true;
            this.trWBS.Nodes.Add(rootNode2);
            rootNode2.Nodes.Add(emptyNode);
            //this.GetNodes(rootNode2.Nodes, rootNode2.NodeID);
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            List<Model.WBS_WorkPackageProject> workPackages = new List<Model.WBS_WorkPackageProject>();
            if (parentId.Length == 1) //工程类型节点
            {
                workPackages = (from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackageProject
                                where x.SuperWorkPack == null && x.ProjectId == this.CurrUser.LoginProjectId && x.ProjectType == parentId
                                orderby x.PackageCode ascending
                                select x).ToList();
            }
            else
            {
                workPackages = (from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackageProject
                                where x.SuperWorkPack == parentId && x.ProjectId == this.CurrUser.LoginProjectId
                                orderby x.PackageCode ascending
                                select x).ToList();
            }
            foreach (var q in workPackages)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = q.PackageContent;
                newNode.NodeID = q.WorkPackageCode;
                newNode.CommandName = "WorkPackage";
                newNode.EnableClickEvent = true;
                nodes.Add(newNode);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].Nodes, nodes[i].NodeID);
            }
        }
        #endregion
        #endregion

        #region  展开树
        /// <summary>
        /// 展开树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trWBS_NodeExpand(object sender, TreeNodeEventArgs e)
        {
            e.Node.Nodes.Clear();
            if (e.Node.CommandName == "ProjectType")   //展开工程类型
            {
                var trUnitWork = from x in new Model.SGGLDB(Funs.ConnString).WBS_UnitWork
                                 where x.ProjectId == this.CurrUser.LoginProjectId && x.SuperUnitWork == null && x.ProjectType == e.Node.NodeID
                                 select x;
                trUnitWork = trUnitWork.OrderBy(x => x.UnitWorkCode);
                if (trUnitWork.Count() > 0)
                {
                    foreach (var trUnitWorkItem in trUnitWork)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = trUnitWorkItem.UnitWorkCode + "-" + trUnitWorkItem.UnitWorkName;
                        newNode.NodeID = trUnitWorkItem.UnitWorkId;
                        newNode.CommandName = "UnitWork";
                        newNode.EnableExpandEvent = true;
                        newNode.EnableClickEvent = true;
                        e.Node.Nodes.Add(newNode);
                        if (BLL.WorkPackageService.GetWorkPackages1ByUnitWorkId(trUnitWorkItem.UnitWorkId.ToString()) != null)
                        {
                            TreeNode temp = new TreeNode();
                            temp.Text = "temp";
                            temp.NodeID = "temp";
                            newNode.Nodes.Add(temp);
                        }
                    }
                }
            }
            else if (e.Node.CommandName == "UnitWork")   //展开单位工程节点
            {
                var workPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.UnitWorkId == e.NodeID && x.SuperWorkPack == null && x.IsApprove == true orderby x.WorkPackageCode select x;
                foreach (var workPackage in workPackages)
                {
                    TreeNode newNode = new TreeNode();
                    string weights = string.Empty;
                    if (workPackage.Weights != null)
                    {
                        weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                    }
                    newNode.Text = workPackage.PackageContent + weights;
                    newNode.NodeID = workPackage.WorkPackageId;
                    newNode.CommandName = "WorkPackage";
                    newNode.EnableExpandEvent = true;
                    newNode.EnableClickEvent = true;
                    e.Node.Nodes.Add(newNode);
                    var childWorkPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                    if (childWorkPackages.Count() > 0)
                    {
                        TreeNode emptyNode = new TreeNode();
                        emptyNode.Text = "";
                        emptyNode.NodeID = "";
                        newNode.Nodes.Add(emptyNode);
                    }
                }
            }
            else if (e.Node.CommandName == "WorkPackage")   //展开工作包节点
            {
                var workPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.SuperWorkPackageId == e.Node.NodeID && x.IsApprove == true orderby x.WorkPackageCode select x;
                if (workPackages.Count() > 0)   //存在子单位工程
                {
                    foreach (var workPackage in workPackages)
                    {
                        TreeNode newNode = new TreeNode();
                        string weights = string.Empty;
                        if (workPackage.Weights != null)
                        {
                            weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                        }
                        newNode.Text = workPackage.PackageContent + weights;
                        newNode.NodeID = workPackage.WorkPackageId;
                        newNode.CommandName = "WorkPackage";
                        newNode.EnableExpandEvent = true;
                        newNode.EnableClickEvent = true;
                        e.Node.Nodes.Add(newNode);
                        var childWorkPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                        if (childWorkPackages.Count() > 0)
                        {
                            TreeNode emptyNode = new TreeNode();
                            emptyNode.Text = "";
                            emptyNode.NodeID = "";
                            newNode.Nodes.Add(emptyNode);
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 展开全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuMore_Click(object sender, EventArgs e)
        {
            if (this.trWBS.SelectedNode != null)
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemAndCycleMenuId, BLL.Const.BtnAdd))
                {
                    if (this.trWBS.SelectedNode.CommandName != "ProjectType")   //非工程类型节点可以增加
                    {
                        Model.WBS_UnitWork unitWork = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(this.trWBS.SelectedNodeID);
                        if (unitWork != null)   //单位工程节点
                        {
                            this.trWBS.SelectedNode.Expanded = true;
                            this.trWBS.SelectedNode.Nodes.Clear();
                            var workPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.UnitWorkId == this.trWBS.SelectedNodeID && x.SuperWorkPack == null && x.IsApprove == true orderby x.WorkPackageCode select x;
                            foreach (var workPackage in workPackages)
                            {
                                TreeNode newNode = new TreeNode();
                                string weights = string.Empty;
                                if (workPackage.Weights != null)
                                {
                                    weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                                }
                                newNode.Text = workPackage.PackageContent + weights;
                                newNode.NodeID = workPackage.WorkPackageId;
                                newNode.CommandName = "WorkPackage";
                                newNode.EnableExpandEvent = true;
                                newNode.EnableClickEvent = true;
                                this.trWBS.SelectedNode.Nodes.Add(newNode);
                                var childWorkPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                                if (childWorkPackages.Count() > 0)
                                {
                                    newNode.Expanded = true;
                                    ExpandWorkPackage(newNode.Nodes, newNode.NodeID);
                                }
                            }
                        }
                        else
                        {
                            this.trWBS.SelectedNode.Expanded = true;
                            this.trWBS.SelectedNode.Nodes.Clear();
                            ExpandWorkPackage(this.trWBS.SelectedNode.Nodes, this.trWBS.SelectedNodeID);
                        }
                    }
                    else
                    {
                        ShowNotify("请选择单位工程节点展开！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 展开子级分部分项节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        private void ExpandWorkPackage(TreeNodeCollection nodes, string parentId)
        {
            var workPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.SuperWorkPackageId == parentId && x.IsApprove == true orderby x.WorkPackageCode select x;
            if (workPackages.Count() > 0)   //存在子单位工程
            {
                foreach (var workPackage in workPackages)
                {
                    TreeNode newNode = new TreeNode();
                    string weights = string.Empty;
                    if (workPackage.Weights != null)
                    {
                        weights = "(" + Convert.ToDouble(workPackage.Weights).ToString() + "%)";
                    }
                    newNode.Text = workPackage.PackageContent + weights;
                    newNode.NodeID = workPackage.WorkPackageId;
                    newNode.CommandName = "WorkPackage";
                    newNode.EnableExpandEvent = true;
                    newNode.EnableClickEvent = true;
                    nodes.Add(newNode);
                    var childWorkPackages = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackage where x.SuperWorkPackageId == workPackage.WorkPackageId && x.IsApprove == true select x;
                    if (childWorkPackages.Count() > 0)
                    {
                        newNode.Expanded = true;
                        ExpandWorkPackage(newNode.Nodes, newNode.NodeID);
                    }
                }
            }
        }

        #region  Tree点击事件
        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trWBS_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region  绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }


        private class SpotCheckInfo
        {
            private string controlItemAndCycleId;
            private string initControlItemCode;
            private string controlItemAndCycleCode;
            private string controlItemContent;
            private string controlPoint;
            private string weights;
            private string checkNum;
            private string spotCheckDetailId;
            private string isDataOK;
            private string spotCheckCode;
            private string spotCheckDate;
            public SpotCheckInfo parent;
            private string ok;
            private string id;
            private int isShow;
            private string planCompleteDate;
            public string ControlItemAndCycleId { get => controlItemAndCycleId; set => controlItemAndCycleId = value; }
            public string InitControlItemCode { get => initControlItemCode; set => initControlItemCode = value; }
            public string ControlItemAndCycleCode { get => controlItemAndCycleCode; set => controlItemAndCycleCode = value; }
            public string ControlItemContent { get => controlItemContent; set => controlItemContent = value; }
            public string ControlPoint { get => controlPoint; set => controlPoint = value; }
            public string Weights { get => weights; set => weights = value; }
            public string CheckNum { get => checkNum; set => checkNum = value; }
            public string SpotCheckCode { get => spotCheckCode; set => spotCheckCode = value; }
            public string SpotCheckDate { get => spotCheckDate; set => spotCheckDate = value; }
            public string SpotCheckDetailId { get => spotCheckDetailId; set => spotCheckDetailId = value; }
            public string IsDataOK { get => isDataOK; set => isDataOK = value; }

            public string Id { get => id; set => id = value; }
            public SpotCheckInfo Parent { get => parent; set => parent = value; }
            public string Ok { get => ok; set => ok = value; }
            public int IsShow { get => isShow; set => isShow = value; }//是否显示交工资料按钮
            public string PlanCompleteDate { get => planCompleteDate; set => planCompleteDate = value; }
        }
        /// <summary>
        /// 加载Grid
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT  ControlItemAndCycleId,ControlItemAndCycleCode,InitControlItemCode, ControlItemContent,ControlPoint,ControlItemDef,Weights,HGForms,SHForms,Standard,ClauseNo,CheckNum,PlanCompleteDate"
                            + @" FROM WBS_ControlItemAndCycle cycle";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where WorkPackageId = @WorkPackageId and IsApprove=1 ";
            listStr.Add(new SqlParameter("@WorkPackageId", this.trWBS.SelectedNodeID));
            strSql += " order by ControlItemAndCycleCode";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            List<string> listControlItemAndCycleId = new List<string>();
            if (tb.Rows.Count > 0)
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    listControlItemAndCycleId.Add(tb.Rows[i]["ControlItemAndCycleId"].ToString());
                }
            }
            if (listControlItemAndCycleId.Count > 0)
            {
                List<string> spotcode = new List<string>();//查找验收时间
                var detailList = SpotCheckDetailService.GetSpotCheckDetailsByControlItemAndCycleIds(listControlItemAndCycleId);// 明细
                foreach (var item in detailList)
                {
                    if (!spotcode.Contains(item.SpotCheckCode))
                    {
                        spotcode.Add(item.SpotCheckCode);
                    }
                }
                var spotcodeList = SpotCheckService.GetSpotChecks(spotcode);//主表数据
                List<SpotCheckInfo> listSpotCheckInfo = new List<SpotCheckInfo>();
                List<SpotCheckInfo> listSpotCheckInfos = new List<SpotCheckInfo>();
                List<SpotCheckInfo> listSpot = new List<SpotCheckInfo>();
                /// 组合数据
                if (tb.Rows.Count > 0)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        SpotCheckInfo info = new SpotCheckInfo();
                        info.ControlItemAndCycleId = tb.Rows[i]["ControlItemAndCycleId"].ToString();
                        info.ControlItemAndCycleCode = tb.Rows[i]["ControlItemAndCycleCode"].ToString();
                        info.InitControlItemCode = tb.Rows[i]["InitControlItemCode"].ToString();
                        info.ControlItemContent = tb.Rows[i]["ControlItemContent"].ToString();
                        info.ControlPoint = tb.Rows[i]["ControlPoint"].ToString();
                        info.Weights = tb.Rows[i]["Weights"].ToString();
                        info.CheckNum = tb.Rows[i]["CheckNum"].ToString();
                        info.Id = tb.Rows[i]["ControlItemAndCycleId"].ToString();
                        info.parent = null;
                        if (!Convert.IsDBNull(tb.Rows[i]["PlanCompleteDate"]))
                        {
                            info.PlanCompleteDate = Convert.ToDateTime(tb.Rows[i]["PlanCompleteDate"]).ToString("yyyy-MM-dd");
                        }
                        info.IsShow = 1;
                        info.IsDataOK = string.Empty;
                        listSpotCheckInfo.Add(info);
                        listSpotCheckInfos.Add(info);
                    }

                    if (listSpotCheckInfo.Count > 0)
                    {
                        for (int i = 0; i < listSpotCheckInfo.Count; i++)
                        {
                            var resList = detailList.Where(p => p.ControlItemAndCycleId == listSpotCheckInfo[i].ControlItemAndCycleId).OrderBy(p => p.ConfirmDate).ToList();

                            if (resList.Count > 0)
                            {
                                List<SpotCheckInfo> spDetailInfo = new List<SpotCheckInfo>();
                                foreach (var itemDetail in resList)
                                {
                                    SpotCheckInfo detailInfo = new SpotCheckInfo();
                                    var info = listSpotCheckInfos.FirstOrDefault(p => p.ControlItemAndCycleId == itemDetail.ControlItemAndCycleId);
                                    if (itemDetail.IsDataOK != null)
                                    {
                                        string str = string.Empty;
                                        if (itemDetail.IsDataOK == "0")
                                        {
                                            str = "不合格";
                                        }
                                        else if (itemDetail.IsDataOK == "1")
                                        {
                                            str = "合格";
                                        }
                                        else
                                        {
                                            str = "不需要";
                                        }
                                        detailInfo.IsDataOK = str;
                                    }
                                    detailInfo.Ok = "合格";
                                    var spot = spotcodeList.FirstOrDefault(p => p.SpotCheckCode == itemDetail.SpotCheckCode);
                                    if (spot != null)
                                    {
                                        detailInfo.SpotCheckDate = Convert.ToDateTime(spot.SpotCheckDate).ToString("yyyy-MM-dd");

                                    }
                                    if (info.PlanCompleteDate != null)
                                    {
                                        detailInfo.PlanCompleteDate = Convert.ToDateTime(info.PlanCompleteDate).ToString("yyyy-MM-dd");
                                    }
                                    detailInfo.ControlItemAndCycleId = itemDetail.ControlItemAndCycleId;
                                    detailInfo.parent = info;
                                    detailInfo.InitControlItemCode = info.InitControlItemCode;
                                    detailInfo.ControlItemContent = info.ControlItemContent;
                                    detailInfo.ControlPoint = info.ControlPoint;
                                    detailInfo.Weights = info.Weights;
                                    detailInfo.CheckNum = info.CheckNum;
                                    detailInfo.Id = itemDetail.SpotCheckDetailId;
                                    detailInfo.SpotCheckDetailId = itemDetail.SpotCheckDetailId;
                                    spDetailInfo.Add(detailInfo);
                                    //listSpot.Add(detailInfo);
                                }
                                if (spDetailInfo.Count > 1)
                                {
                                    spDetailInfo.OrderBy(p => p.SpotCheckDate);
                                    foreach (var item in spDetailInfo)
                                    {
                                        var index = spDetailInfo.IndexOf(item);
                                        index++;
                                        item.CheckNum = index + "/" + item.CheckNum;
                                        listSpot.Add(item);
                                    }
                                }
                                else if (spDetailInfo.Count == 1)
                                {
                                    foreach (var item in spDetailInfo)
                                    {
                                        var index = spDetailInfo.IndexOf(item);
                                        index++;
                                        item.CheckNum = index + "/" + item.CheckNum;
                                        listSpot.Add(item);
                                    }
                                }

                                //listSpot.Remove(listSpotCheckInfo[i]);
                                //listSpot.Remove(listSpotCheckInfo[i]);
                                //listSpot.RemoveAll(p => p.ControlItemAndCycleId == listSpotCheckInfo[i].ControlItemAndCycleId);
                            }
                            else
                            {
                                listSpot.Add(listSpotCheckInfo[i]);
                            }

                            //if (resList.Count > 0)
                            //{
                            //    listSpot.Remove(listSpotCheckInfo[i]);
                            //}

                        }

                    }

                }

                Grid1.RecordCount = listSpot.Count;
                Grid1.PageSize = listSpot.Count;
                Grid1.DataSource = listSpot;
                Grid1.DataBind();
                if (Grid1.Rows.Count > 0)
                {
                    for (int i = 0; i < Grid1.Rows.Count; i++)
                    {
                        System.Web.UI.WebControls.HiddenField show = (System.Web.UI.WebControls.HiddenField)Grid1.Rows[i].FindControl("isShow");
                        System.Web.UI.WebControls.LinkButton url = (System.Web.UI.WebControls.LinkButton)Grid1.Rows[i].FindControl("attchUrl");
                        if (Grid1.Rows[i].Values[7] != null)
                        {
                            if (!string.IsNullOrWhiteSpace(Grid1.Rows[i].Values[7].ToString()))
                            {
                                if (!Grid1.Rows[i].Values[7].ToString().Equals("合格"))
                                {
                                    url.Text = string.Empty;
                                }

                            }
                            else
                            {
                                url.Text = string.Empty;
                            }
                        }


                        if (show.Value.Equals("1"))
                        {
                            url.Visible = false;
                        }


                    }
                }
                Model.WBS_WorkPackage workPackage = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(this.trWBS.SelectedNodeID);

            }
        }
        #endregion

        protected void attchUrl_Click(object sender, EventArgs e)
        {

            AspNet.LinkButton btn = sender as AspNet.LinkButton;
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/uploader.aspx?toKeyId={0}&path=FileUpload/HJGL/SpotCheck&menuId={1}&type={2}&fname={3}", btn.CommandArgument, BLL.Const.SpotDataCheckMenuId, -1, string.IsNullOrWhiteSpace(btn.Text) ? "" : HttpUtility.UrlEncode(btn.Text))));

        }
    }
}