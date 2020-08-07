using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class ShowMainItem : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitTree(Request.Params["id"], Request.Params["unitWorkId"]);
            }
        }

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="menuList">菜单集合</param>
        private void InitTree(string id, string unitWorkId)
        {
            this.tvMenu.Nodes.Clear();
            Model.SGGLDB db = Funs.DB;

            var mainItems = from x in db.ProjectData_MainItem where x.ProjectId == this.CurrUser.LoginProjectId select x;
            string otherIds = string.Empty;
            var otherUnitWorks = from x in db.WBS_UnitWork where x.ProjectId == this.CurrUser.LoginProjectId && x.UnitWorkId != unitWorkId select x;
            foreach (var otherUnitWork in otherUnitWorks)
            {
                if (!string.IsNullOrEmpty(otherUnitWork.MainItemAndDesignProfessionalIds))
                {
                    otherIds += otherUnitWork.MainItemAndDesignProfessionalIds;
                }
            }
            var allDesignProfessionals = from x in db.Base_DesignProfessional select x;
            foreach (var item in mainItems)
            {
                TreeNode rootNode = new TreeNode
                {
                    Text = item.MainItemCode + "-" + item.MainItemName,
                    NodeID = item.MainItemId,
                    EnableCheckBox = true,
                    EnableCheckEvent = true,
                    Expanded = true
                };
                if (!string.IsNullOrEmpty(id) && id.Contains(rootNode.NodeID))
                {
                    rootNode.Checked = true;
                }
                this.tvMenu.Nodes.Add(rootNode);
                var designProfessionals = from x in allDesignProfessionals where item.DesignProfessionalIds.Split(',').Contains(x.DesignProfessionalId) select x;
                foreach (var designProfessional in designProfessionals)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = designProfessional.ProfessionalName,
                        NodeID = item.MainItemId + "|" + designProfessional.DesignProfessionalId,
                        EnableCheckBox = true,
                        EnableCheckEvent = true,
                    };
                    if (!string.IsNullOrEmpty(id) && id.Contains(node.NodeID))
                    {
                        node.Checked = true;
                    }
                    if (!string.IsNullOrEmpty(otherIds) && otherIds.Contains(node.NodeID))
                    {
                        node.EnableCheckBox = false;
                    }
                    rootNode.Nodes.Add(node);
                }
            }
        }
        #endregion

        #region 保存事件
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ids = string.Empty;
            TreeNode[] nodes = this.tvMenu.GetCheckedNodes();
            if (nodes.Length > 0)
            {
                foreach (TreeNode tn in nodes)
                {
                    if (tn.NodeID.Contains("|"))
                    {
                        ids += tn.NodeID + ",";
                    }
                }
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.Substring(0, ids.Length - 1);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(ids) + ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 全选、全不选
        /// <summary>
        /// 全选、全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvMenu_NodeCheck(object sender, FineUIPro.TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                this.tvMenu.CheckAllNodes(e.Node.Nodes);
                SetCheckParentNode(e.Node);
            }
            else
            {
                this.tvMenu.UncheckAllNodes(e.Node.Nodes);
            }
        }

        /// <summary>
        /// 选中父节点
        /// </summary>
        /// <param name="node"></param>
        private void SetCheckParentNode(TreeNode node)
        {
            if (node.ParentNode != null && node.ParentNode.NodeID != "0")
            {
                node.ParentNode.Checked = true;
                if (node.ParentNode.ParentNode != null)
                {
                    SetCheckParentNode(node.ParentNode);
                }
            }
        }
        #endregion
    }
}