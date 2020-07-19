using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class Response : System.Web.UI.Page
    {
        #region 定义项
        /// <summary>
        /// 清单主键
        /// </summary>
        public string HazardListId
        {
            get
            {
                return (string)ViewState["HazardListId"];
            }
            set
            {
                ViewState["HazardListId"] = value;
            }
        }

        /// <summary>
        /// 检查项目内容ID
        /// </summary>
        public string HazardSortCode
        {
            get
            {
                return (string)ViewState["HazardSortCode"];
            }
            set
            {
                ViewState["HazardSortCode"] = value;
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get
            {
                return (string)ViewState["Type"];
            }
            set
            {
                ViewState["Type"] = value;
            }
        }

        /// <summary>
        /// 类别集合
        /// </summary>
        private static List<Model.Technique_HazardListType> hazardSorts = new List<Model.Technique_HazardListType>();

        /// <summary>
        /// 危险源集合
        /// </summary>
        public List<Model.Hazard_HazardSelectedItem> hazardSelectedItems = new List<Model.Hazard_HazardSelectedItem>();

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
                this.HazardListId = Request.Params["HazardListId"];
                this.Type = Request.Params["type"];
                hazardSorts.Clear(); 
                if (!string.IsNullOrEmpty(this.HazardListId))
                {
                    List<Model.Hazard_HazardSelectedItem> hazardSelectedItems = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(this.HazardListId);
                    HazardSortSetDataBind();
                    Grid1.DataSource = hazardSelectedItems;
                    Grid1.DataBind();  
                    for (int i = 0; i < this.Grid1.Rows.Count; i++)
                    {

                        System.Web.UI.WebControls.HiddenField hdWorkStage = (System.Web.UI.WebControls.HiddenField)(this.Grid1.Rows[i].FindControl("hdWorkStage"));
                        System.Web.UI.WebControls.DropDownList drpPromptTime = (System.Web.UI.WebControls.DropDownList)(this.Grid1.Rows[i].FindControl("drpPromptTime"));

                        var q = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemByHazardId(this.Grid1.Rows[i].DataKeys[0].ToString(), this.HazardListId, hdWorkStage.Value);
                        System.Web.UI.WebControls.LinkButton lbtnResponse = ((System.Web.UI.WebControls.LinkButton)(this.Grid1.Rows[i].FindControl("lbtnResponse")));
                        if (Convert.ToBoolean(q.IsResponse))
                        {
                            ((System.Web.UI.WebControls.LinkButton)(this.Grid1.Rows[i].FindControl("lbtnResponse"))).Text = "已响应";
                        }
                        else
                        {
                            ((System.Web.UI.WebControls.LinkButton)(this.Grid1.Rows[i].FindControl("lbtnResponse"))).Text = "未响应";
                        }
                    }
                }
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void HazardSortSetDataBind()
        {
            this.tvHazardTemplate.Nodes.Clear();

            TreeNode rootNode = new TreeNode
            {
                Text = "职业健康安全危险源辨识与评价",
                NodeID = "0"
            };//定义根节点

            this.tvHazardTemplate.Nodes.Add(rootNode);

            var q = (from x in Funs.DB.Hazard_HazardSelectedItem where x.HazardListId == HazardListId select x.WorkStage).Distinct();

            foreach (string work in q)
            {
                hazardSorts.Clear();
                TreeNode workStageTree = new TreeNode();
                var workStageName = BLL.WorkStageService.GetWorkStageById(work);
                if (workStageName != null)
                {
                    workStageTree.Text = workStageName.WorkStageName;
                    workStageTree.NodeID = workStageName.WorkStageId;
                    rootNode.Nodes.Add(workStageTree);
                }

                List<Model.Hazard_HazardSelectedItem> hazardSelecteds = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListIdAndWorkStage(HazardListId, work);
                foreach (Model.Hazard_HazardSelectedItem hazardSelectedItem in hazardSelecteds)
                {
                    AddHazardSortSet(hazardSelectedItem.HazardListTypeId);
                }
                this.GetNodes(workStageTree.Nodes, null);
            }

            this.tvHazardTemplate.ExpandAllNodes();
        }
        #endregion

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            List<Model.Technique_HazardListType> newHazardSorts = new List<Model.Technique_HazardListType>();
            if (parentId == null)
            {
                foreach (Model.Technique_HazardListType hazardSort in hazardSorts)
                {
                    if (hazardSort.SupHazardListTypeId == "0")
                    {
                        newHazardSorts.Add(hazardSort);
                    }
                }
            }
            else
            {
                foreach (Model.Technique_HazardListType hazardSort in hazardSorts)
                {
                    if (hazardSort.SupHazardListTypeId == parentId)
                    {
                        newHazardSorts.Add(hazardSort);
                    }
                }
            }
            foreach (var q in newHazardSorts)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = q.HazardListTypeName + "-" + q.HazardListTypeCode,
                    NodeID = q.HazardListTypeId
                };
                nodes.Add(newNode);
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].Nodes, nodes[i].NodeID);
            }
        }
        #endregion

        #region 添加节点
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="hazardSortCode"></param>
        private void AddHazardSortSet(string hazardListTypeId)
        {
            if (hazardListTypeId != "0")
            {
                Model.Technique_HazardListType hazardSortSet = BLL.HazardListTypeService.GetHazardListTypeById(hazardListTypeId);
                if (!hazardSorts.Contains(hazardSortSet))
                {
                    hazardSorts.Add(hazardSortSet);
                }
                AddHazardSortSet(hazardSortSet.SupHazardListTypeId);
            }
        }
        #endregion

        #region 节点选中事件
        /// <summary>
        /// 节点选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvHazardTemplate_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            hazardSelectedItems.Clear();
            HazardSortCode = this.tvHazardTemplate.SelectedNode.NodeID;

            var hazardList = BLL.Hazard_HazardListService.GetHazardList(this.HazardListId);
            if (hazardList != null)
            {
                string workStage = hazardList.WorkStage;

                Model.Technique_HazardListType hazardSort = BLL.HazardListTypeService.GetHazardListTypeById(HazardSortCode);

                if (hazardSort != null)
                {
                    if (Convert.ToBoolean(hazardSort.IsEndLevel))
                    {
                        this.Grid1.DataSource = BLL.Hazard_HazardSelectedItemService.getHazardSelectedItemByHazardListTypeId(HazardSortCode, HazardListId, workStage);
                        this.Grid1.DataBind();
                    }
                    else
                    {
                        GetTvHazardTemplateChecked(this.tvHazardTemplate.SelectedNode.Nodes, workStage);
                        this.Grid1.DataSource = hazardSelectedItems;
                        this.Grid1.DataBind();
                    }
                }
            }
        }
        #endregion

        #region 遍历选中节点
        /// <summary>
        /// 遍历选中的节点
        /// </summary>
        /// <param name="nodes"></param>
        private void GetTvHazardTemplateChecked(TreeNodeCollection nodes, string workStage)
        {
            foreach (TreeNode tnHazardTemplate in nodes)
            {
                if (tnHazardTemplate.NodeID != "0")
                {
                    Model.Technique_HazardListType hazardSortSet = (from x in Funs.DB.Technique_HazardListType where x.HazardListTypeId == tnHazardTemplate.NodeID orderby x.HazardListTypeCode select x).FirstOrDefault();
                    if (hazardSortSet != null)
                    {
                        if (Convert.ToBoolean(hazardSortSet.IsEndLevel))
                        {
                            hazardSelectedItems.AddRange(BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemBySortAndListIdAndWorkStage(tnHazardTemplate.NodeID.Trim(), HazardListId, workStage));
                        }
                        else
                        {
                            GetTvHazardTemplateChecked(tnHazardTemplate.Nodes, workStage);
                        }
                    }
                }
                else
                {
                    GetTvHazardTemplateChecked(tnHazardTemplate.Nodes, workStage);
                }
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取工作阶段
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertWorkStage(object WorkStage)
        {
            string name = string.Empty;
            if (WorkStage != null)
            {
                string workStage = WorkStage.ToString().Trim();
                Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(workStage);
                if (c != null)
                {
                    name = c.WorkStageName;
                }
            }
            return name;
        }

        /// <summary>
        /// 获取危险源编号
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertHazardCode(object HazardId)
        {
            string hazardCode = string.Empty;
            if (HazardId != null)
            {
                Model.Technique_HazardList hazardList = BLL.HazardListService.GetHazardListById(HazardId.ToString());
                if (hazardList != null)
                {
                    hazardCode = hazardList.HazardCode;
                }
            }
            return hazardCode;
        }
        #endregion

        #region 响应点击事件
        /// <summary>
        /// 响应按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnResponse_click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.LinkButton l = sender as System.Web.UI.WebControls.LinkButton;
            string hazardId = l.CommandArgument.ToString();
            int rowsCount = this.Grid1.Rows.Count;
            string workStage = string.Empty;
            for (int i = 0; i < rowsCount; i++)
            {
                System.Web.UI.WebControls.LinkButton l2 = ((System.Web.UI.WebControls.LinkButton)(this.Grid1.Rows[i].FindControl("lbtnResponse")));
                System.Web.UI.WebControls.HiddenField hdWorkStage = (System.Web.UI.WebControls.HiddenField)(this.Grid1.Rows[i].FindControl("hdWorkStage"));
                if (l.ClientID == l2.ClientID)
                {
                    workStage = hdWorkStage.Value.Trim();
                }
            }
            if (l.CommandName == "response")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ResponseItem.aspx?HazardId={0}&HazardListId={1}&workStage={2}", hazardId, HazardListId, workStage, "编辑 - ")));
            }
        }
        #endregion

        #region 弹出编辑窗口关闭事件
        /// <summary>
        /// 弹出编辑窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.Grid1.DataSource = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(HazardListId);
            this.Grid1.DataBind();

            ShowResponse();
        }

        private void ShowResponse()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                System.Web.UI.WebControls.LinkButton lbtnResponse = ((System.Web.UI.WebControls.LinkButton)(this.Grid1.Rows[i].FindControl("lbtnResponse")));
                System.Web.UI.WebControls.HiddenField hdWorkStage = (System.Web.UI.WebControls.HiddenField)(this.Grid1.Rows[i].FindControl("hdWorkStage"));
                Model.Hazard_HazardSelectedItem hazardSelectedItem = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemByHazardId(lbtnResponse.CommandArgument.ToString(), HazardListId, hdWorkStage.Value.Trim());

                if (Convert.ToBoolean(hazardSelectedItem.IsResponse))
                {
                    ((System.Web.UI.WebControls.LinkButton)(this.Grid1.Rows[i].FindControl("lbtnResponse"))).Text = "已响应";
                }
                else
                {
                    ((System.Web.UI.WebControls.LinkButton)(this.Grid1.Rows[i].FindControl("lbtnResponse"))).Text = "未响应";
                }
            }
        }
        #endregion
    }
}