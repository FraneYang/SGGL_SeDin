using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectInformation : PageBase
    {
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
                this.InitTreeMenu();//加载树
            }
        }
        #endregion

        #region 加载树项目
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();
            TreeNode rootNode = new TreeNode();
            rootNode.Text = "项目";
            rootNode.ToolTip = "项目";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            this.tvControlItem.Nodes.Add(rootNode);

            List<Model.Base_Project> projects = BLL.ProjectService.GetProjectByUserIdDropDownList(this.CurrUser.UserId);
            foreach (var item in projects)
            {
                TreeNode rootProjectNode = new TreeNode();//定义根节点
                rootProjectNode.Text = item.ProjectName;
                rootProjectNode.NodeID = item.ProjectId;
                rootProjectNode.EnableClickEvent = true;
                rootProjectNode.Expanded = true;
                rootProjectNode.ToolTip = "项目名称";
                rootNode.Nodes.Add(rootProjectNode);
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
           
        }
        #endregion
        
        protected void IntoProject_Click(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null )
            {
                string url = "~/indexProject.aspx?projectId=" + this.tvControlItem.SelectedNode.NodeID;
                UserService.UpdateLastUserInfo(this.CurrUser.UserId, null, false, this.tvControlItem.SelectedNode.NodeID);
                PageContext.Redirect(url, "_top");
            }
            else
            {
                ShowNotify("请选择项目进入！", MessageBoxIcon.Warning);
            }
        }
    }
}