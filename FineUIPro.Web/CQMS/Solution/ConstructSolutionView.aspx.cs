using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Solution
{
    public partial class ConstructSolutionView : PageBase
    {
        public string ConstructSolutionId
        {
            get
            {
                return (string)ViewState["ConstructSolutionId"];
            }
            set
            {
                ViewState["ConstructSolutionId"] = value;
            }
        }
        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/Solution&menuId={2}",
            -1, ConstructSolutionId, Const.CQMSConstructSolutionMenuId)));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ConstructSolutionId = Request.Params["constructSolutionId"];
                if (!string.IsNullOrWhiteSpace(ConstructSolutionId))
                {
                    txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
                    Model.Solution_CQMSConstructSolution constructSolution = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(ConstructSolutionId);
                    txtCode.Text = constructSolution.Code;
                    if (!string.IsNullOrEmpty(constructSolution.UnitId))
                    {
                        drpUnit.Text = UnitService.GetUnitNameByUnitId(constructSolution.UnitId);
                    }
                    if (!string.IsNullOrEmpty(constructSolution.SolutionType))
                    {
                        drpModelType.Text = BLL.SolutionTempleteTypeService.GetSolutionTempleteTypeById(constructSolution.SolutionType).SolutionTempleteTypeName;
                    }
                    if (!string.IsNullOrEmpty(constructSolution.SpecialSchemeTypeId))
                    {
                        txtSpecialType.Text = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeById(constructSolution.SpecialSchemeTypeId).SpecialSchemeTypeName;
                    }
                    if (constructSolution.CompileDate != null)
                    {
                        txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructSolution.CompileDate);
                    }
                    txtSolutionName.Text = constructSolution.SolutionName;
                    if (!string.IsNullOrWhiteSpace(constructSolution.UnitWorkIds))
                    {
                        txtUnitWork.Text = UnitWorkService.GetUnitWorkName(constructSolution.UnitWorkIds);

                    }
                    if (!string.IsNullOrWhiteSpace(constructSolution.CNProfessionalCodes))
                    {
                        txtCNProfessional.Text = CQMSConstructSolutionService.GetProfessionalName(constructSolution.CNProfessionalCodes);
                    }
                    if (constructSolution.Edition != null)
                    {
                        txtEdition.Text = constructSolution.Edition.ToString();
                    }
                    bindApprove();
                    BindZYRole();
                    BindZLRole();
                    BindAQRole();
                    BindKZRole();
                    BindSGRole();
                    BindXMRole();
                    var zyUserIds = CQMSConstructSolutionApproveService.GetUserIdsApprovesBySignType(ConstructSolutionId, "ZY");
                    if (zyUserIds.Count > 0)
                    {
                        SetCheck(trOne, zyUserIds);
                    }
                    var zlUserIds = CQMSConstructSolutionApproveService.GetUserIdsApprovesBySignType(ConstructSolutionId, "ZL");
                    if (zlUserIds.Count > 0)
                    {
                        SetCheck(trTwo, zlUserIds);
                    }
                    var aqUserIds = CQMSConstructSolutionApproveService.GetUserIdsApprovesBySignType(ConstructSolutionId, "AQ");
                    if (aqUserIds.Count > 0)
                    {
                        SetCheck(trThree, aqUserIds);
                    }
                    var kzUserIds = CQMSConstructSolutionApproveService.GetUserIdsApprovesBySignType(ConstructSolutionId, "KZ");
                    if (kzUserIds.Count > 0)
                    {
                        SetCheck(trFour, kzUserIds);
                    }
                    var sgUserIds = CQMSConstructSolutionApproveService.GetUserIdsApprovesBySignType(ConstructSolutionId, "SG");
                    if (sgUserIds.Count > 0)
                    {
                        SetCheck(trFive, sgUserIds);
                    }
                    var xmUserIds = CQMSConstructSolutionApproveService.GetUserIdsApprovesBySignType(ConstructSolutionId, "XM");
                    if (xmUserIds.Count > 0)
                    {
                        SetCheck(trSixe, xmUserIds);
                    }
                    if (!string.IsNullOrEmpty(Request.Params["see"]))
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = BLL.CQMSConstructSolutionApproveService.GetSee(ConstructSolutionId, this.CurrUser.UserId);
                        if (approve != null)
                        {
                            approve.ApproveDate = DateTime.Now;
                            BLL.CQMSConstructSolutionApproveService.UpdateConstructSolutionApprove(approve);
                        }
                    }
                }
            }
        }

        #region 动态加载角色树

        /// <summary>
        /// 设置树的节点选择
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="role"></param>
        private void SetCheck(Tree tree, List<string> userIds)
        {
            foreach (TreeNode tn in tree.Nodes[0].Nodes)
            {
                if (userIds.Contains(tn.NodeID))
                {
                    tn.Checked = true;
                }
            }
        }

        /// 加载角色树：动态加载
        /// </summary>
        private void BindZYRole()
        {

            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "专业工程师";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            rootNode.EnableCheckEvent = true;
            trOne.Nodes.Add(rootNode);
            trOne.EnableCheckBox = true;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where p.RoleId.Contains(Const.ZBCNEngineer)
                               && y.UnitType == Const.ProjectUnitType_1 && p.ProjectId == CurrUser.LoginProjectId && y.ProjectId == CurrUser.LoginProjectId
                               select x;
                //var ss = LINQToDataTable(userList);
                foreach (var u in userList)
                {
                    TreeNode Node = new TreeNode();
                    Node.Text = u.UserName;
                    Node.NodeID = u.UserId;
                    Node.EnableCheckEvent = true;
                    rootNode.Nodes.Add(Node);
                }
            }

        }
        private void BindZLRole()
        {
            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "质量组";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            rootNode.EnableCheckEvent = true;
            trTwo.Nodes.Add(rootNode);
            trTwo.EnableCheckBox = true;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where (p.RoleId.Contains(Const.QAManager) || p.RoleId.Contains(Const.CQEngineer))
                               && y.UnitType == Const.ProjectUnitType_1 && p.ProjectId == CurrUser.LoginProjectId && y.ProjectId == CurrUser.LoginProjectId
                               orderby x.UserCode
                               select x;
                foreach (var u in userList)
                {
                    TreeNode roleNode = new TreeNode();
                    roleNode.Text = u.UserName;
                    roleNode.NodeID = u.UserId;
                    rootNode.Nodes.Add(roleNode);
                }
            }
        }
        /// <summary>
        /// 判断是否有选择
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Boolean nodesCheckd(Tree node)
        {
            bool res = false;
            if (node.Nodes[0].Nodes.Count > 0)
            {
                foreach (var item in node.Nodes[0].Nodes)
                {
                    if (item.Checked)
                    {
                        res = true;
                        break;
                    }
                }
            }
            return res;
        }

        private void BindAQRole()
        {
            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "HSE组";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            rootNode.EnableCheckEvent = true;
            trThree.Nodes.Add(rootNode);
            trThree.EnableCheckBox = true;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where (p.RoleId.Contains(Const.HSSEManager) || p.RoleId.Contains(Const.HSSEEngineer))
                               && y.UnitType == Const.ProjectUnitType_1 && p.ProjectId == CurrUser.LoginProjectId && y.ProjectId == CurrUser.LoginProjectId
                               orderby x.UserCode
                               select x;
                foreach (var u in userList)
                {
                    TreeNode roleNode = new TreeNode();
                    roleNode.Text = u.UserName;
                    roleNode.NodeID = u.UserId;
                    rootNode.Nodes.Add(roleNode);
                }
            }
        }

        private void BindKZRole()
        {
            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "控制组";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            rootNode.EnableCheckEvent = true;
            trFour.Nodes.Add(rootNode);
            trFour.EnableCheckBox = true;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where (p.RoleId.Contains(Const.ControlManager) || p.RoleId.Contains(Const.KZEngineer))
                               && y.UnitType == Const.ProjectUnitType_1 && p.ProjectId == CurrUser.LoginProjectId && y.ProjectId == CurrUser.LoginProjectId
                               orderby x.UserCode
                               select x;
                foreach (var u in userList)
                {
                    TreeNode roleNode = new TreeNode();
                    roleNode.Text = u.UserName;
                    roleNode.NodeID = u.UserId;
                    rootNode.Nodes.Add(roleNode);
                }
            }
        }

        private void BindSGRole()
        {

            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "施工经理";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            rootNode.EnableCheckEvent = true;
            trFive.Nodes.Add(rootNode);
            trFive.EnableCheckBox = true;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                              on x.UserId equals p.UserId
                               where (p.RoleId.Contains(Const.ConstructionManager) || p.RoleId.Contains(Const.ConstructionAssistantManager))
                               && y.UnitType == Const.ProjectUnitType_1 && p.ProjectId == CurrUser.LoginProjectId && y.ProjectId == CurrUser.LoginProjectId
                               orderby x.UserCode
                               select x;
                foreach (var u in userList)
                {
                    TreeNode roleNode = new TreeNode();
                    roleNode.Text = u.UserName;
                    roleNode.NodeID = u.UserId;
                    rootNode.Nodes.Add(roleNode);
                }
            }
        }

        private void BindXMRole()
        {

            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "项目经理";
            rootNode.NodeID = "0";
            rootNode.Expanded = true;
            rootNode.EnableCheckEvent = true;
            trSixe.Nodes.Add(rootNode);
            trSixe.EnableCheckBox = true;
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                              on x.UserId equals p.UserId
                               where p.RoleId.Contains(Const.ProjectManager)
                               && y.UnitType == Const.ProjectUnitType_1 && p.ProjectId == CurrUser.LoginProjectId && y.ProjectId == CurrUser.LoginProjectId
                               orderby x.UserCode
                               select x;
                foreach (var u in userList)
                {
                    TreeNode roleNode = new TreeNode();
                    roleNode.Text = u.UserName;
                    roleNode.NodeID = u.UserId;
                    rootNode.Nodes.Add(roleNode);
                }
            }
        }
        #endregion
        /// <summary>
        /// 审批列表
        /// </summary>
        private void bindApprove()
        {
            var list = CQMSConstructSolutionApproveService.getListData(ConstructSolutionId);
            //var user = UserService.GetAllUserList(CurrUser.LoginProjectId);
            gvApprove.DataSource = list;
            gvApprove.DataBind();
        }

        public string man(Object man)
        {
            string appman = string.Empty;
            if (UserService.GetUserByUserId(man.ToString()) != null)
            {
                appman = UserService.GetUserByUserId(man.ToString()).UserName;
            }
            return appman;
        }

        protected void gvApprove_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = gvApprove.DataKeys[e.RowIndex];
            string fileId = string.Empty;
            if (keys == null)
            {
                return;
            }
            else
            {
                fileId = keys[0].ToString();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
                 String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/Solution&menuId={2}",
                 -1, fileId, Const.CQMSConstructSolutionMenuId)));
        }
    }
}