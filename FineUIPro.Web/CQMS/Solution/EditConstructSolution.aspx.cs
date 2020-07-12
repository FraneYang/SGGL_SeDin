using BLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Solution
{
    public partial class EditConstructSolution : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 方案审查主键
        /// </summary>
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
        #endregion

        #region 定义集合
        /// <summary>
        /// 定义会签意见集合
        /// </summary>
        public static List<Model.Solution_CQMSConstructSolutionApprove> approves = new List<Model.Solution_CQMSConstructSolutionApprove>();
        #endregion

        public int ContactImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ContactImg"]);
            }
            set
            {
                ViewState["ContactImg"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var unitWork = UnitWorkService.GetUnitWorkLists(CurrUser.LoginProjectId);
                gvUnitWork.DataSource = unitWork;
                gvUnitWork.DataBind();
                var gvCNProfessional = CNProfessionalService.GetList();
                gvCNPro.DataSource = gvCNProfessional;
                gvCNPro.DataBind();
                drpModelType.DataSource = CQMSConstructSolutionService.GetSolutionType();
                drpModelType.DataTextField = "Text";
                drpModelType.DataValueField = "Value";
                drpModelType.DataBind();
                Funs.FineUIPleaseSelect(drpModelType);
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, false);
                BindZYRole();
                BindZLRole();
                BindAQRole();
                BindKZRole();
                BindSGRole();
                BindXMRole();
                ContactImg = 0;
                //CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.CQMSConstructSolutionMenuId);
                txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                ConstructSolutionId = Request.Params["constructSolutionId"];
                if (!string.IsNullOrWhiteSpace(ConstructSolutionId))
                {
                    HFConstructSolutionId.Text = ConstructSolutionId;
                    Model.Solution_CQMSConstructSolution constructSolution = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(ConstructSolutionId);
                    txtCode.Text = constructSolution.Code;
                    if (!string.IsNullOrEmpty(constructSolution.UnitId))
                    {
                        drpUnit.SelectedValue = constructSolution.UnitId;
                    }
                    if (!string.IsNullOrEmpty(constructSolution.SolutionType))
                    {
                        drpModelType.SelectedValue = constructSolution.SolutionType;
                    }
                    if (constructSolution.CompileDate != null)
                    {
                        txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", constructSolution.CompileDate);
                    }
                    txtSolutionName.Text = constructSolution.SolutionName;
                    if (constructSolution.UnitWorkIds.Length > 0)
                    {
                        txtUnitWork.Values = constructSolution.UnitWorkIds.Split(',');
                    }
                    if (constructSolution.CNProfessionalCodes.Length > 0)
                    {
                        txtCNProfessional.Values = constructSolution.CNProfessionalCodes.Split(',');
                    }

                    txtEdition.Text = constructSolution.Edition.ToString();
                    bindApprove();

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
                    if (constructSolution.State == Const.CQMSConstructSolution_ReCompile)
                    {
                        agree.Hidden = true;
                        options.Hidden = true;
                        optio.Hidden = true;
                    }
                    if (constructSolution.State == Const.CQMSConstructSolution_Audit)
                    {

                        txtProjectName.Enabled = false;
                        txtCode.Enabled = false;
                        drpUnit.Enabled = false;
                        drpModelType.Enabled = false;
                        txtCompileDate.Enabled = false;
                        txtSolutionName.Enabled = false;
                        txtCNProfessional.Enabled = false;
                        txtUnitWork.Enabled = false;
                        ContactImg = -1;
                        Panel2.Enabled = false;
                    }
                    //提交版本人多次修改
                    if (constructSolution.CompileMan.Equals(CurrUser.UserId))
                    {
                        txtProjectName.Enabled = true;
                        txtCode.Enabled = true;
                        drpUnit.Enabled = true;
                        drpModelType.Enabled = true;
                        txtCompileDate.Enabled = true;
                        txtSolutionName.Enabled = true;
                        txtCNProfessional.Enabled = true;
                        txtUnitWork.Enabled = true;
                        ContactImg = 0;
                        Panel2.Enabled = true;
                        rblIsAgree.Hidden = true;
                        rblIsAgree.Required = false;
                        options.Hidden = true;
                        txtOptions.Required = false;
                        optio.Hidden = true;
                    }
                    //if (!string.IsNullOrEmpty(countersign.CVRole))
                    //{
                    //    GetCheck(tvCV.Nodes[0].ChildNodes, countersign.CVRole);
                    //}

                }
                else
                {
                    agree.Hidden = true;
                    txtEdition.Text = "0";
                    options.Hidden = true;
                    optio.Hidden = true;
                    plApprove2.Hidden = true;
                    txtCode.Text = SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Solution_CQMSConstructSolution", "Code", CurrUser.LoginProjectId);

                }

                txtProjectName.Text = ProjectService.GetProjectByProjectId(CurrUser.LoginProjectId).ProjectName;
            }
        }
        /// <summary>
        /// 审批列表
        /// </summary>
        private void bindApprove()
        {
            var list = CQMSConstructSolutionApproveService.getListData(ConstructSolutionId);
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

        #region 保存/提交
        protected void btnSave_Click(object sender, EventArgs e)
        {
            validate(Const.BtnSave, "save");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            validate(Const.BtnSubmit, "submmit");
        }

        /// <summary>
        /// 保存验证
        /// </summary>
        /// <param name="buttonName"></param>
        /// <param name="tip"></param>
        public void validate(string buttonName, string tip)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.CQMSConstructSolutionMenuId, buttonName))
            {
                string err = string.Empty;
                if (!AttachFileService.Getfile(HFConstructSolutionId.Text, Const.CQMSConstructSolutionMenuId))
                {
                    err += "请上传附件,";
                }
                //if (string.IsNullOrEmpty(hdFilePath.Value))
                //{
                //    err += "请上传附件,";
                //}
                //if (trOne.Nodes[0].Nodes.Count == 0 && trTwo.Nodes.Count == 0 && trThree.Nodes.Count == 0 &&
                //    trFour.Nodes.Count == 0 && trFive.Nodes.Count == 0 && trSixe.Nodes.Count == 0)
                //{
                //    err += "请选择总包会签人员,";
                //}
                List<FineUIPro.Tree> list = new List<FineUIPro.Tree>();
                list.Add(trOne);
                list.Add(trTwo);
                list.Add(trThree);
                list.Add(trFour);
                list.Add(trFive);
                list.Add(trSixe);
                var res = false;
                foreach (var item in list)
                {
                    if (nodesCheckd(item))
                    {
                        res = true;
                        break;
                    }
                }
                if (!res)
                {
                    err += "请选择总包会签人员,";
                }
                if (!string.IsNullOrWhiteSpace(err))
                {
                    err = err.Substring(0, err.LastIndexOf(","));
                    err += "!";
                }
                if (!string.IsNullOrWhiteSpace(err))
                {
                    Alert.ShowInTop(err, MessageBoxIcon.Warning);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(ConstructSolutionId))
                {//更新时操作
                    if (tip == "save")
                    {
                        EditConstructSol("save");
                    }
                    else
                    {
                        EditConstructSol("submit");
                    }

                }
                else
                {
                    if (tip == "save")
                    {
                        SaveCQMSConstructSolution("save");
                    }
                    else
                    {
                        SaveCQMSConstructSolution("submit");
                    }
                    //添加时操作           
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                if (tip == "save")
                {
                    tip = "保存成功!";
                }
                else
                {
                    tip = "提交成功!";
                }
                Alert.ShowInTop(tip, MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系!", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑时候保存
        /// </summary>
        private void EditConstructSol(string saveType)
        {
            Model.Solution_CQMSConstructSolution constructSolution = new Model.Solution_CQMSConstructSolution();
            constructSolution.Code = txtCode.Text.Trim();
            constructSolution.ProjectId = CurrUser.LoginProjectId;
            if (drpUnit.SelectedValue != "0")
            {
                constructSolution.UnitId = drpUnit.SelectedValue;
            }
            if (drpUnit.SelectedValue != "0")
            {
                constructSolution.SolutionType = drpModelType.SelectedValue;
            }
            constructSolution.SolutionName = txtSolutionName.Text.Trim();
            int edtion = Convert.ToInt32(txtEdition.Text);
            constructSolution.Edition = edtion;
            if (!string.IsNullOrEmpty(txtCompileDate.Text.Trim()))
            {
                constructSolution.CompileDate = Convert.ToDateTime(txtCompileDate.Text.Trim());
            }

            if (txtUnitWork.Values.Length > 0)
            {
                constructSolution.UnitWorkIds = string.Join(",", txtUnitWork.Values);
            }
            if (txtCNProfessional.Values.Length > 0)
            {
                constructSolution.CNProfessionalCodes = string.Join(",", txtCNProfessional.Values);
            }
            if (!string.IsNullOrEmpty(ConstructSolutionId))
            {
                constructSolution.ConstructSolutionId = ConstructSolutionId;

                Model.Solution_CQMSConstructSolution constructSolution1 = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(ConstructSolutionId);
                if (saveType == "submit")
                {
                    if (CurrUser.UserId != constructSolution1.CompileMan)   //办理人不是编制人，提示查看审批信息
                    {
                        Model.Solution_CQMSConstructSolutionApprove sApprove = new Model.Solution_CQMSConstructSolutionApprove();
                        sApprove.ConstructSolutionId = constructSolution.ConstructSolutionId;
                        sApprove.ApproveMan = constructSolution1.CompileMan;
                        sApprove.ApproveType = "S";
                        CQMSConstructSolutionApproveService.AddConstructSolutionApprove(sApprove);

                        if (constructSolution1.State == Const.CQMSConstructSolution_Audit)
                        {
                            constructSolution.State = constructSolution1.State;

                        }
                        else
                        {
                            constructSolution.State = Const.CQMSConstructSolution_Audit;
                        }
                    }
                    else
                    {
                        countersign(constructSolution.ConstructSolutionId);
                        constructSolution.State = constructSolution1.State;
                    }

                    if (!CurrUser.UserId.Equals(constructSolution1.CompileMan))
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = CQMSConstructSolutionApproveService.GetConstructSoluAppByApproveMan(ConstructSolutionId, CurrUser.UserId, Convert.ToInt32(constructSolution1.Edition));
                        if (saveType == "submit")
                        {
                            approve.ApproveDate = DateTime.Now;
                        }
                        approve.Edition = Convert.ToInt32(edtion);
                        approve.IsAgree = Convert.ToBoolean(rblIsAgree.SelectedValue);
                        approve.ApproveIdea = txtOptions.Text.Trim();
                        CQMSConstructSolutionApproveService.UpdateConstructSolutionApprove(approve);
                    }
                    else
                    {
                        if (saveType == "submit")
                        {
                            if (constructSolution1.State == Const.CQMSConstructSolution_Audit)//==会签状态升级版本
                            {
                                Model.Solution_CQMSConstructSolutionApprove reApprove = new Model.Solution_CQMSConstructSolutionApprove();
                                reApprove.ConstructSolutionId = constructSolution.ConstructSolutionId;
                                reApprove.ApproveDate = DateTime.Now;
                                reApprove.ApproveMan = constructSolution1.CompileMan;
                                reApprove.ApproveType = Const.CQMSConstructSolution_ReCompile;
                                edtion++;
                                reApprove.Edition = edtion;
                                CQMSConstructSolutionApproveService.AddConstructSolutionApprove(reApprove);
                            }
                            else
                            {
                                Model.Solution_CQMSConstructSolutionApprove approves = CQMSConstructSolutionApproveService.GetConstructSolApproveByApproveMan(ConstructSolutionId, constructSolution1.CompileMan);
                                approves.ApproveDate = DateTime.Now;
                                CQMSConstructSolutionApproveService.UpdateConstructSolutionApprove(approves);
                            }
                        }

                    }
                }
                else
                {
                    constructSolution.State = constructSolution1.State;
                }
                //提交时候，更新提交版本
                if (CurrUser.UserId.Equals(constructSolution1.CompileMan))
                {
                    if (constructSolution1.State != Const.CQMSConstructSolution_Audit)
                    {
                        edtion++;
                    }
                    constructSolution.Edition = Convert.ToInt32(edtion);
                    constructSolution.State = Const.CQMSConstructSolution_Audit;
                }
                CQMSConstructSolutionService.UpdateConstructSolution(constructSolution);
                //判断状态，全部会签同意，则审批完成
                if (saveType == "submit")
                {
                    List<Model.Solution_CQMSConstructSolutionApprove> allApproves = CQMSConstructSolutionApproveService.GetHandleConstructSolutionApprovesByConstructSolutionId(ConstructSolutionId, constructSolution.Edition == null ? 0 : Convert.ToInt32(constructSolution.Edition));
                    var count = allApproves.Where(p => p.ApproveDate != null && p.IsAgree != null && Convert.ToBoolean(p.IsAgree)).Count();//查询会签同意的
                    var fcount = allApproves.Where(p => p.ApproveDate != null && p.IsAgree != null && !Convert.ToBoolean(p.IsAgree)).Count();//查询会签不同意的
                    if ((count + fcount) == allApproves.Count)
                    {
                        if (count == allApproves.Count)
                        {
                            var cons = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(ConstructSolutionId);
                            cons.State = Const.CQMSConstructSolution_Complete;
                            cons.CompileDate = DateTime.Now;
                            CQMSConstructSolutionService.UpdateConstructSolution(cons);
                        }
                        //有不同意意见，打回重新编制
                        if (fcount > 0)
                        {
                            var cons = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(ConstructSolutionId);
                            Model.Solution_CQMSConstructSolutionApprove reApprove = new Model.Solution_CQMSConstructSolutionApprove();
                            reApprove.ConstructSolutionId = constructSolution.ConstructSolutionId;
                            reApprove.ApproveMan = cons.CompileMan;
                            reApprove.ApproveType = Const.CQMSConstructSolution_ReCompile;
                            edtion++;
                            reApprove.Edition = edtion;
                            CQMSConstructSolutionApproveService.AddConstructSolutionApprove(reApprove);

                            cons.State = Const.CQMSConstructSolution_ReCompile;
                            cons.CompileDate = DateTime.Now;
                            constructSolution.State = Const.CQMSConstructSolution_ReCompile;
                            CQMSConstructSolutionService.UpdateConstructSolution(cons);
                        }
                    }

                }
                LogService.AddSys_Log(CurrUser, constructSolution.Code, ConstructSolutionId, Const.CQMSConstructSolutionMenuId, "修改施工方案");

            }
        }
        #endregion

        #region 添加时候的保存

        /// <summary>
        /// 保存方案审查
        /// </summary>
        /// <param name="saveType">保存类型</param>
        private void SaveCQMSConstructSolution(string saveType)
        {
            //if (tvHSE.CheckedNodes.Count == 0 || (tvHSE.CheckedNodes.Count > 0 && tvHSE.CheckedNodes[0].Value == "0"))
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择HSE会签人员！')", true);
            //    return;
            //}
            Model.Solution_CQMSConstructSolution constructSolution = new Model.Solution_CQMSConstructSolution();
            constructSolution.Code = txtCode.Text.Trim();
            constructSolution.ProjectId = CurrUser.LoginProjectId;
            if (drpUnit.SelectedValue != "0")
            {
                constructSolution.UnitId = drpUnit.SelectedValue;
            }
            if (drpModelType.SelectedValue != "0")
            {
                constructSolution.SolutionType = drpModelType.SelectedValue;
            }
            constructSolution.SolutionName = txtSolutionName.Text.Trim();
            if (!string.IsNullOrEmpty(txtCompileDate.Text.Trim()))
            {
                constructSolution.CompileDate = Convert.ToDateTime(txtCompileDate.Text.Trim());
            }
            if (txtUnitWork.Values.Length > 0)
            {
                constructSolution.UnitWorkIds = string.Join(",", txtUnitWork.Values);
            }
            if (txtCNProfessional.Values.Length > 0)
            {
                constructSolution.CNProfessionalCodes = string.Join(",", txtCNProfessional.Values);
            }

            if (saveType == "submit")
            {
                constructSolution.State = Const.CQMSConstructSolution_Audit;
            }
            else
            {
                constructSolution.State = Const.CQMSConstructSolution_Compile;
            }
            if (!string.IsNullOrEmpty(HFConstructSolutionId.Text))
            {
                constructSolution.ConstructSolutionId = HFConstructSolutionId.Text;
            }
            else
            {
                constructSolution.ConstructSolutionId = SQLHelper.GetNewID(typeof(Model.Solution_CQMSConstructSolution));
            }
            constructSolution.CompileMan = CurrUser.UserId;
            constructSolution.Edition = Convert.ToInt32(txtEdition.Text);

            CQMSConstructSolutionService.AddConstructSolution(constructSolution);
            if (saveType == "submit")
            {
                Model.Solution_CQMSConstructSolutionApprove approve1 = new Model.Solution_CQMSConstructSolutionApprove();
                approve1.ConstructSolutionId = constructSolution.ConstructSolutionId;
                approve1.ApproveDate = DateTime.Now;
                approve1.ApproveMan = this.CurrUser.UserId;
                approve1.ApproveType = Const.CQMSConstructSolution_Compile;
                approve1.Edition = Convert.ToInt32(txtEdition.Text);
                CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve1);
            }



            LogService.AddSys_Log(CurrUser, constructSolution.Code, ConstructSolutionId, Const.CQMSConstructSolutionMenuId, "添加施工方案");

            //}

            //提交
            if (saveType == "submit")
            {
                countersign(constructSolution.ConstructSolutionId);
            }
            LogService.AddSys_Log(CurrUser, constructSolution.Code, ConstructSolutionId, Const.CQMSConstructSolutionMenuId, "编制方案审查");

        }
        #endregion

        /// <summary>
        /// 删除未选择的代办记录
        /// </summary>
        /// <param name="constructSolutionId"></param>
        private void delSolutionApprove(string constructSolutionId, string man)
        {
            var count = CQMSConstructSolutionApproveService.getListSolutionApproveCount(constructSolutionId, man);
            if (count > 0)
            {
                CQMSConstructSolutionApproveService.delSolutionApprove(constructSolutionId, man);
            }
        }



        /// <summary>
        /// 会签
        /// </summary>
        private void countersign(string constructSolutionId)
        {
            var solution = CQMSConstructSolutionService.GetConstructSolutionByConstructSolutionId(ConstructSolutionId);
            if (trOne.Nodes[0].Nodes.Count > 0)
            {
                foreach (TreeNode tn in trOne.Nodes[0].Nodes)
                {
                    if (tn.Checked)
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = new Model.Solution_CQMSConstructSolutionApprove();
                        approve.ConstructSolutionId = constructSolutionId;
                        approve.ApproveMan = tn.NodeID;
                        approve.ApproveType = Const.CQMSConstructSolution_Audit;
                        approve.SignType = "ZY";
                        int edtion = Convert.ToInt32(txtEdition.Text);
                        if (solution != null)
                        {
                            edtion++;
                        }
                        approve.Edition = edtion;
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                        CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve);
                    }
                    else
                    {
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                    }

                }
            }
            if (trTwo.Nodes[0].Nodes.Count > 0)
            {

                foreach (TreeNode tn in trTwo.Nodes[0].Nodes)
                {
                    if (tn.Checked)
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = new Model.Solution_CQMSConstructSolutionApprove();
                        approve.ConstructSolutionId = constructSolutionId;
                        approve.ApproveMan = tn.NodeID;
                        approve.ApproveType = Const.CQMSConstructSolution_Audit;
                        approve.SignType = "ZL";
                        int edtion = Convert.ToInt32(txtEdition.Text);
                        if (solution != null)
                        {
                            edtion++;
                        }
                        approve.Edition = edtion;
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                        CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve);
                    }
                    else
                    {
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                    }

                }
            }
            if (trThree.Nodes[0].Nodes.Count > 0)
            {

                foreach (TreeNode tn in trThree.Nodes[0].Nodes)
                {
                    if (tn.Checked)
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = new Model.Solution_CQMSConstructSolutionApprove();
                        approve.ConstructSolutionId = constructSolutionId;
                        approve.ApproveMan = tn.NodeID;
                        approve.ApproveType = Const.CQMSConstructSolution_Audit;
                        approve.SignType = "AQ";
                        int edtion = Convert.ToInt32(txtEdition.Text);
                        if (solution != null)
                        {
                            edtion++;
                        }
                        approve.Edition = edtion;
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                        CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve);
                    }
                    else
                    {
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                    }

                }
            }
            if (trFour.Nodes[0].Nodes.Count > 0)
            {

                foreach (TreeNode tn in trFour.Nodes[0].Nodes)
                {
                    if (tn.Checked)
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = new Model.Solution_CQMSConstructSolutionApprove();
                        approve.ConstructSolutionId = constructSolutionId;
                        approve.ApproveMan = tn.NodeID;
                        approve.ApproveType = Const.CQMSConstructSolution_Audit;
                        approve.SignType = "KZ";
                        int edtion = Convert.ToInt32(txtEdition.Text);
                        if (solution != null)
                        {
                            edtion++;
                        }
                        approve.Edition = edtion;
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                        CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve);

                    }
                    else
                    {
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                    }

                }
            }
            if (trFive.Nodes[0].Nodes.Count > 0)
            {

                foreach (TreeNode tn in trFive.Nodes[0].Nodes)
                {
                    if (tn.Checked)
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = new Model.Solution_CQMSConstructSolutionApprove();
                        approve.ConstructSolutionId = constructSolutionId;
                        approve.ApproveMan = tn.NodeID;
                        approve.ApproveType = Const.CQMSConstructSolution_Audit;
                        approve.SignType = "SG";
                        int edtion = Convert.ToInt32(txtEdition.Text);
                        if (solution != null)
                        {
                            edtion++;
                        }
                        approve.Edition = edtion;
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                        CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve);
                    }
                    else
                    {
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                    }
                }
            }
            if (trSixe.Nodes[0].Nodes.Count > 0)
            {

                foreach (TreeNode tn in trSixe.Nodes[0].Nodes)
                {
                    if (tn.Checked)
                    {
                        Model.Solution_CQMSConstructSolutionApprove approve = new Model.Solution_CQMSConstructSolutionApprove();
                        approve.ConstructSolutionId = constructSolutionId;
                        approve.ApproveMan = tn.NodeID;
                        approve.ApproveType = Const.CQMSConstructSolution_Audit;
                        approve.SignType = "XM";
                        int edtion = Convert.ToInt32(txtEdition.Text);
                        if (solution != null)
                        {
                            edtion++;
                        }
                        approve.Edition = edtion;
                        delSolutionApprove(constructSolutionId, tn.NodeID);
                        CQMSConstructSolutionApproveService.AddConstructSolutionApprove(approve);
                    }
                    else
                    {
                        delSolutionApprove(constructSolutionId, tn.NodeID);
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where (x.RoleId.Contains(Const.CVEngineer) || x.RoleId.Contains(Const.FEEngineer) || x.RoleId.Contains(Const.PDEngineer)
                               || x.RoleId.Contains(Const.EHEngineer) || x.RoleId.Contains(Const.EAEngineer) || x.RoleId.Contains(Const.HJEngineer))
                               && y.UnitType == Const.ProjectUnitType_1 && p.ProjectId == CurrUser.LoginProjectId && y.ProjectId== CurrUser.LoginProjectId
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where (x.RoleId.Contains(Const.QAManager) || x.RoleId.Contains(Const.CQEngineer))
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where (x.RoleId.Contains(Const.HSSEManager) || x.RoleId.Contains(Const.HSSEEngineer))
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                               on x.UserId equals p.UserId
                               where (x.RoleId.Contains(Const.ControlManager) || x.RoleId.Contains(Const.KZEngineer))
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                              on x.UserId equals p.UserId
                               where (x.RoleId.Contains(Const.ConstructionManager) || x.RoleId.Contains(Const.ConstructionAssistantManager))
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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))            {
                var userList = from x in db.Sys_User
                               join y in db.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               join p in db.Project_ProjectUser
                              on x.UserId equals p.UserId
                               where x.RoleId.Contains(Const.ProjectManager)
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

        #region 树结构的全选
        protected void trOne_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                trOne.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                trOne.UncheckAllNodes(e.Node.Nodes);
            }
        }

        protected void trTwo_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                trTwo.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                trTwo.UncheckAllNodes(e.Node.Nodes);
            }
        }

        protected void trThree_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                trThree.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                trThree.UncheckAllNodes(e.Node.Nodes);
            }
        }

        protected void trFour_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                trFour.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                trFour.UncheckAllNodes(e.Node.Nodes);
            }
        }

        protected void trFive_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                trFive.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                trFive.UncheckAllNodes(e.Node.Nodes);
            }

        }

        protected void trSixe_NodeCheck(object sender, TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                trSixe.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                trSixe.UncheckAllNodes(e.Node.Nodes);
            }
        }


        #endregion

        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFConstructSolutionId.Text))   //新增记录
            {
                HFConstructSolutionId.Text = SQLHelper.GetNewID(typeof(Model.Solution_CQMSConstructSolution));
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/Solution&menuId={2}",
            ContactImg, HFConstructSolutionId.Text, Const.CQMSConstructSolutionMenuId)));
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            //HFConstructSolutionId.Text
            var approve = CQMSConstructSolutionApproveService.GetConstructSolutionApproveByApproveMan(HFConstructSolutionId.Text, CurrUser.UserId);
            if (approve != null)
            {
                var approveId = approve.ConstructSolutionApproveId;
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
                String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/Solution&menuId={2}",
                0, approveId, Const.CQMSConstructSolutionMenuId)));
            }

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