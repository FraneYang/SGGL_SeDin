using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Person
{
    public partial class PersonCheck : PageBase
    {
        #region 加载

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                BindGrid();
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnMenuDelete.Hidden = false;
                    this.BtnCreat.Hidden = false;

                    GridColumn columnexport = Grid1.FindColumn("export");
                    columnexport.Hidden = false;
                }
            }
        }
        private void BindGrid()
        {
            string strSql = @"select QuarterCheckId, QuarterCheckName, C.UserId, C.ProjectId, StartTime, EndTime,State,RoleName,U.UserName,P.ProjectName from [dbo].[Person_QuarterCheck] C left join (select S.RoleId, RoleName ,userId,UserName from Sys_Role S left join Sys_user U on S.RoleId=U.RoleId) U on C.UserId=U.UserId 
              left join Base_Project P on C.ProjectId=P.ProjectId where EndTime>CONVERT(varchar,GETDATE(),120) ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.CurrUser.UserId != BLL.Const.sysglyId)
            {
                strSql += " AND QuarterCheckId in (select QuarterCheckId from Person_QuarterCheckApprove where UserId=@UserId and ApproveDate is null) ";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected string ConvertApprove(object State)
        {
            if (!string.IsNullOrEmpty(State.ToString()))
            {
                return State.ToString() == "1" ? "考核结束" : "正在考核";
            }
            return "";
        }
        #endregion

        #region 数据操作

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuEdit_Click(null, null);
        }

        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonCheckingView.aspx?QuarterCheckId={0}", Id, "编辑 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonChecking.aspx?QuarterCheckId={0}", Id, "编辑 - ")));
            }

        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string rowID = Grid1.SelectedRowID;
            var QuarterCheck = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(rowID);
            if (QuarterCheck != null)
            {
                var CheckItem = BLL.Person_QuarterCheckItemService.GetCheckItemListById(rowID);
                if (CheckItem.Count > 0)
                {
                    foreach (var item in CheckItem)
                    {
                        BLL.Person_QuarterCheckItemService.DeleteCheckItem(item.QuarterCheckItemId);
                    }
                }
                var CheckApprove = BLL.Person_QuarterCheckApproveService.GetCheckApproveListById(rowID);
                if (CheckApprove.Count > 0)
                {
                    foreach (var item in CheckApprove)
                    {
                        BLL.Person_QuarterCheckApproveService.DeleteCheckApprove(item.ApproveId);
                    }
                }
                BLL.Person_QuarterCheckService.DeleteQuarterCheck(rowID);



                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion


        #region 生成季度考核表
        /// <summary>
        /// 生成季度考核表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCreat_Click(object sender, EventArgs e)
        {
            var QuarterCheck = new Model.SGGLDB(Funs.ConnString).Person_QuarterCheck.FirstOrDefault(x => x.EndTime > DateTime.Now);
            if (QuarterCheck != null)
            {
                ShowNotify("本季度已生成考核表");
                return;
            }
            var GetProjectList = BLL.ProjectService.GetProjectWorkList();
            if (GetProjectList.Count > 0)
            {
                List<string> projectIds = GetProjectList.Select(x => x.ProjectId).ToList();
                var ProjectUsers = ProjectUserService.GetProjectUsersByProjectIds(projectIds);
                foreach (var item in GetProjectList)
                {
                    ///施工经理工作任务书
                    Model.Project_ProjectUser ConstructUser = ProjectUsers.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.RoleId == BLL.Const.ConstructionManager);
                    if (ConstructUser != null)
                    {
                        Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                        {
                            QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                            QuarterCheckName = "施工经理工作任务书",
                            ProjectId = ConstructUser.ProjectId,
                            UserId = ConstructUser.UserId,
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddMonths(3),
                            State = "0",
                            CheckType = "1"
                        };
                        BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                        SaveConstructItem(Check.ProjectId, Check.QuarterCheckId);
                    }
                    ///安全经理工作任务书
                    Model.Project_ProjectUser HSSEUsers = ProjectUsers.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.RoleId == BLL.Const.HSSEManager);
                    if (HSSEUsers != null)
                    {
                        Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                        {
                            QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                            QuarterCheckName = "安全经理工作任务书",
                            ProjectId = HSSEUsers.ProjectId,
                            UserId = HSSEUsers.UserId,
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddMonths(3),
                            State = "0",
                            CheckType = "2"
                        };
                        BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                        SaveSecurityItem(Check.ProjectId, Check.QuarterCheckId);
                    }
                    ///质量经理工作任务书
                    Model.Project_ProjectUser QAUsers = ProjectUsers.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.RoleId == BLL.Const.QAManager);
                    if (QAUsers != null)
                    {
                        Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                        {
                            QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                            QuarterCheckName = "质量经理工作任务书",
                            ProjectId = QAUsers.ProjectId,
                            UserId = QAUsers.UserId,
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddMonths(3),
                            State = "0",
                            CheckType = "3"
                        };
                        BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                        SaveQAItem(Check.ProjectId, Check.QuarterCheckId);

                    }
                    ///试车经理工作任务书
                    Model.Project_ProjectUser TestUser = ProjectUsers.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.RoleId == BLL.Const.TestManager);
                    if (TestUser != null)
                    {
                        Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                        {
                            QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                            QuarterCheckName = "试车经理工作任务书",
                            ProjectId = TestUser.ProjectId,
                            UserId = TestUser.UserId,
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddMonths(3),
                            State = "0",
                            CheckType = "4"
                        };
                        BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                        SaveTestItem(Check.ProjectId, Check.QuarterCheckId);
                    }
                    ///施工专业工程师工作任务书
                    var ConstructEgUser = ProjectUsers.Where(x => x.ProjectId == item.ProjectId && (x.RoleId == BLL.Const.CVEngineer || x.RoleId == BLL.Const.FEEngineer || x.RoleId == BLL.Const.PDEngineer || x.RoleId == BLL.Const.EHEngineer || x.RoleId == BLL.Const.EAEngineer || x.RoleId == BLL.Const.HJEngineer)).ToList();
                    if (ConstructEgUser.Count > 0)
                    {
                        foreach (var seeUser in ConstructEgUser)
                        {
                            Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                            {
                                QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                                QuarterCheckName = "施工专业工程师工作任务书",
                                ProjectId = seeUser.ProjectId,
                                UserId = seeUser.UserId,
                                StartTime = DateTime.Now,
                                EndTime = DateTime.Now.AddMonths(3),
                                State = "0",
                                CheckType = "5"
                            };
                            BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                            SaveConstructEgItem(Check.ProjectId, Check.QuarterCheckId);
                        }
                    }
                    ///安全专业工程师工作任务书
                    Model.Project_ProjectUser SecurityEgUser = ProjectUsers.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.RoleId == BLL.Const.HSSEEngineer);
                    if (SecurityEgUser != null)
                    {
                        Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                        {
                            QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                            QuarterCheckName = "安全专业工程师工作任务书",
                            ProjectId = SecurityEgUser.ProjectId,
                            UserId = SecurityEgUser.UserId,
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddMonths(3),
                            State = "0",
                            CheckType = "6"
                        };
                        BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                        SaveSecurityEgItem(Check.ProjectId, Check.QuarterCheckId);
                    }
                    ///质量专业工程师工作任务书
                    Model.Project_ProjectUser QAEgUser = ProjectUsers.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.RoleId == BLL.Const.CQEngineer);
                    if (QAEgUser != null)
                    {
                        Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                        {
                            QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                            QuarterCheckName = "质量专业工程师工作任务书",
                            ProjectId = QAEgUser.ProjectId,
                            UserId = QAEgUser.UserId,
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddMonths(3),
                            State = "0",
                            CheckType = "7"
                        };
                        BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                        SaveQAEgItem(Check.ProjectId, Check.QuarterCheckId);
                    }
                    ///试车专业工程师工作任务书
                    Model.Project_ProjectUser TestEgUser = ProjectUsers.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.RoleId == BLL.Const.TestEngineer);
                    if (TestEgUser != null)
                    {
                        Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                        {
                            QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                            QuarterCheckName = "试车专业工程师工作任务书",
                            ProjectId = TestEgUser.ProjectId,
                            UserId = TestEgUser.UserId,
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddMonths(3),
                            State = "0",
                            CheckType = "8"
                        };
                        BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                        SaveTestEgItem(Check.ProjectId, Check.QuarterCheckId);
                    }
                }
            }


            #region 本部综合管理工程师工作任务书
            var SGAllEgUser = BLL.UserService.GetUserListByRole(BLL.Const.SGAllManageEngineer);
            if (SGAllEgUser.Count > 0)
            {
                List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                seeUsers.AddRange(SGAllEgUser);
                seeUsers = seeUsers.Distinct().ToList();
                foreach (var seeUser in seeUsers)
                {
                    Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                    {
                        QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                        QuarterCheckName = "本部综合管理工程师工作任务书",
                        ProjectId = seeUser.ProjectId,
                        UserId = seeUser.UserId,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMonths(3),
                        State = "0",
                        CheckType = "9"
                    };
                    BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                    SaveSGAllEgItem(Check.ProjectId, Check.QuarterCheckId);
                }
            }
            #endregion

            #region 本部合同管理工程师工作任务书
            var SGContractEgUser = BLL.UserService.GetUserListByRole(BLL.Const.SGContractManageEngineer);
            if (SGContractEgUser.Count > 0)
            {
                List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                seeUsers.AddRange(SGContractEgUser);
                seeUsers = seeUsers.Distinct().ToList();
                foreach (var seeUser in seeUsers)
                {
                    Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                    {
                        QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                        QuarterCheckName = "本部合同管理工程师工作任务书",
                        ProjectId = seeUser.ProjectId,
                        UserId = seeUser.UserId,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMonths(3),
                        State = "0",
                        CheckType = "10"
                    };
                    BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                    SaveSGContractEgItem(Check.ProjectId, Check.QuarterCheckId);
                }
            }
            #endregion

            #region 本部合同管理工程师工作任务书
            var SGSecurityQAEgUser = BLL.UserService.GetUserListByRole(BLL.Const.SGSecurityQAEngineer);
            if (SGSecurityQAEgUser.Count > 0)
            {
                List<Model.Sys_User> seeUsers = new List<Model.Sys_User>();
                seeUsers.AddRange(SGSecurityQAEgUser);
                seeUsers = seeUsers.Distinct().ToList();
                foreach (var seeUser in seeUsers)
                {
                    Model.Person_QuarterCheck Check = new Model.Person_QuarterCheck
                    {
                        QuarterCheckId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheck)),
                        QuarterCheckName = "本部合同管理工程师工作任务书",
                        ProjectId = seeUser.ProjectId,
                        UserId = seeUser.UserId,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMonths(3),
                        State = "0",
                        CheckType = "11"
                    };
                    BLL.Person_QuarterCheckService.AddPerson_QuarterCheck(Check);
                    SaveSGSecurityQAEgItem(Check.ProjectId, Check.QuarterCheckId);
                }
            }
            #endregion

            BindGrid();
        }

        #endregion

        #region 新增施工季度考核明细

        /// <summary>
        /// 新增施工季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveConstructItem(string ProjectId, string QuarterCheckId)
        {
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "施工管理",
                            CheckContent = "1.施工管理工作安排合理、问题协调解决；<br />2.及时组织施工例会、施工协调会；<br />3.对周、月工作计划进行工作部署；<br />4.对设备、材料等施工资源部署协调；<br />5.按时记录施工日志等。",
                            UserId = ProjectUser.UserId,
                            SortId = 1,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "文明施工",
                            CheckContent = "1.落实施工现场文明施工管理规定；<br />2.合理安排分包单位标志标牌、场容场貌、三防（防火、防爆、防毒）等。",
                            UserId = ProjectUser.UserId,
                            SortId = 4,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "进度管理",
                            CheckContent = "1. 无施工管理原因导致工期拖延；<br />2.施工进度部署合理；<br />3.对影响工程进度的施工资源（人材机）进行有效调配等。",
                            UserId = ProjectUser.UserId,
                            SortId = 5,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "技术管理",
                            CheckContent = "1.施工前组织图纸会审设计交底，及时组织编审施工方案；<br />2.严把危大工程施工；<br />3.对焊接技术等把关；<br />4.施工资料与工程进度同步。",
                            UserId = ProjectUser.UserId,
                            SortId = 6,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 4)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "成本管理",
                            CheckContent = "确认变更、把关签证工程量。",
                            UserId = ProjectUser.UserId,
                            SortId = 7,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 5)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "其他工作情况（30%）",
                            TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                            CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                            UserId = ProjectUser.UserId,
                            SortId = 8,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取质量经理
            var QAManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.QAManager);
            if (QAManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "质量管理",
                    CheckContent = "1. 执行质量管理各项制度；<br />2.不发生质量事故；<br />3.安排落实质量保证措施等。",
                    UserId = QAManager.UserId,
                    SortId = 3,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = QAManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取安全经理
            var SecurityManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.HSSEManager);
            if (SecurityManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "安全生产",
                    CheckContent = "1.执行安全管理各项制度；<br />2.现场不发生安全事故；<br />3.安排落实安全防护措施；<br />4.安排整改安全隐患等。",
                    UserId = SecurityManager.UserId,
                    SortId = 2,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SecurityManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 9,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 10,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增安全季度考核明细

        /// <summary>
        /// 新增安全季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveSecurityItem(string ProjectId, string QuarterCheckId)
        {
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "安全生产",
                            CheckContent = "1.制定安全管理计划；<br />2.组织编制HSE月报告；<br />3.严格贯彻执行国家、公司安全管理各项制度，组织安全教育、安全检查、安全交底等；<br />4.及时防范避免发生安全事故和人员伤亡；<br />5.检查督促现场做好安全防护措施；<br />6.及时对安全隐患问题督促采取有效措施整改；<br />7.召开安全例会；<br />8.组织并参与现场施工安全事故的处理，并记录；<br />9.审查并检查、督促施工单位执行施工安全保证程序；<br />10.参加重大施工方案的讨论与评审；<br />11.定期组织应急预案演练；<br />12.按时记录工作日志等。",
                            UserId = ProjectUser.UserId,
                            SortId = 2,
                            StandardGrade = 30,
                        };

                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {

                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "文明施工",
                            CheckContent = "1.督促落实施工现场文明施工管理规定；<br />2.督促统一建立标志标牌、场容场貌、三防（防火、防爆、防毒）等。",
                            UserId = ProjectUser.UserId,
                            SortId = 3,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "进度管理",
                            CheckContent = "营造良好项目安全氛围，为工程进度提供保障。",
                            UserId = ProjectUser.UserId,
                            SortId = 4,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "技术管理",
                            CheckContent = "1.及时编制安全计划，审批分包方施工安全计划和控制措施；<br />2.严格审批相关施工方案中涉及施工安全的内容；<br />3.学习并传授专业知识，提升业务能力；<br />4.预防安全事故发生等。",
                            UserId = ProjectUser.UserId,
                            SortId = 5,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 4)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "其他工作情况（30%）",
                            TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                            CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                            UserId = ProjectUser.UserId,
                            SortId = 6,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取施工经理
            var ConstructionManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ConstructionManager);
            if (ConstructionManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "施工管理",
                    CheckContent = "1.按时参加施工例会，了解整体部署；<br />2.为施工工作开展做好安全保障等；",
                    UserId = ConstructionManager.UserId,
                    SortId = 1,
                    StandardGrade = 10,

                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ConstructionManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 7,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 8,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增质量季度考核明细

        /// <summary>
        /// 新增质量季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveQAItem(string ProjectId, string QuarterCheckId)
        {
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "安全生产",
                            CheckContent = "执行安全管理各项制度，参加安全教育、安全检查、安全交底等。",
                            UserId = ProjectUser.UserId,
                            SortId = 2,
                            StandardGrade = 10,
                        };

                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {

                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "质量管理",
                            CheckContent = "1.编制工程质量实施计划和质量控制点及等级划分；<br />2.督促施工单位按照设计图纸和资料、技术规范、规定进行施工，保证施工质量；<br />3.组织质量例会；<br />4.审查并检查、督促施工单位执行施工质量保证程序；<br />5.参加重大施工方案的讨论；<br />6.按规定积累竣工验收需要的资料；<br />7.督促落实质量措施，避免质量事故；<br />8.组织并参与现场施工质量事故的处理，并记录；<br />9.按时记录工作日志；<br />10.组织质量检查等。",
                            UserId = ProjectUser.UserId,
                            SortId = 3,
                            StandardGrade = 30,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "技术管理",
                            CheckContent = "1.及时编制质量计划，审批分包方施工质量控制措施；<br />2.严格审批相关施工方案中涉及施工质量的内容；<br />3.学习并传授专业知识，提升业务能力等。",
                            UserId = ProjectUser.UserId,
                            SortId = 4,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "进度管理",
                            CheckContent = "营造良好项目安全氛围，为工程进度提供保障。",
                            UserId = ProjectUser.UserId,
                            SortId = 5,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 4)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "其他工作情况（30%）",
                            TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                            CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                            UserId = ProjectUser.UserId,
                            SortId = 6,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取施工经理
            var ConstructionManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ConstructionManager);
            if (ConstructionManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "施工管理",
                    CheckContent = "1.按时参加施工例会，了解整体部署；<br />2.为施工工作开展做好安全保障等；",
                    UserId = ConstructionManager.UserId,
                    SortId = 1,
                    StandardGrade = 10,

                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ConstructionManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 7,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 8,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增试车经理季度考核明细

        /// <summary>
        /// 新增试车经理季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveTestItem(string ProjectId, string QuarterCheckId)
        {
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "试车管理",
                            CheckContent = "1.严格执行试车相关管理制度、管理体系要求；<br />2.参加设计文件审查；根据项目试车需要，及时到达项目现场开展试车有关工作。<br />3.按照合同要求编制项目试车方案、培训教材、操作手册或规程等，组织开展有关人员培训。<br />4.参与预试车，审查预试车结果，参加工程中间交接。<br />5.根据项目需要，组织开展冷试车、热试车和性能考核等工作。<br />6.组织编写试车总结，完成试车有关资料的统一归档。",
                            UserId = ProjectUser.UserId,
                            SortId = 1,
                            StandardGrade = 40,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "质量管理",
                            CheckContent = "1.按照公司规定和合同要求编制试车有关技术文件，确保文件质量；<br />2.现场试车管理时，严格落实试车方案相关试车质量措施，不发生质量事故。",
                            UserId = ProjectUser.UserId,
                            SortId = 2,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "进度管理",
                            CheckContent = "1.按照总承包合同和业主要求编制试车工作进度计划；<br />2.施工进度部署合理；<br />3.对影响试车进度的资源（人材机）进行有效调配等。",
                            UserId = ProjectUser.UserId,
                            SortId = 3,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "安全环保",
                            CheckContent = "1.	试车期间执行安全管理各项制度，参加安全教育、安全检查、安全交底等。<br />2.严格遵守业主、公司试车安全管理相关规定，确保现场安全环保试车等，不发生安全环保事故。",
                            UserId = ProjectUser.UserId,
                            SortId = 4,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 4)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "其他工作情况（30%）",
                            TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                            CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                            UserId = ProjectUser.UserId,
                            SortId = 5,
                            StandardGrade = 10,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 6,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 7,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增施工专业工程师季度考核明细

        /// <summary>
        /// 新增施工专业工程师季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveConstructEgItem(string ProjectId, string QuarterCheckId)
        {
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                    CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                    UserId = ProjectUser.UserId,
                    SortId = 8,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取施工经理
            var ConstructionManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ConstructionManager);
            if (ConstructionManager != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "施工管理",
                            CheckContent = "1.施工管理工作安排合理、问题协调解决；<br />2.及时参加施工例会、施工协调会；<br />3.对负责片区周、月工作计划进行工作部署、评价；<br />4.对负责片区设备、材料等施工资源部署协调；<br />5.按时记录施工日志等。<br />",
                            UserId = ConstructionManager.UserId,
                            SortId = 1,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "文明施工",
                            CheckContent = "1.落实施工现场文明施工管理规定；<br />2.合理安排分包单位标志标牌、场容场貌、三防（防火、防爆、防毒）等。",
                            UserId = ConstructionManager.UserId,
                            SortId = 4,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "进度管理",
                            CheckContent = "1.负责片区无施工管理原因导致工期拖延；<br />2.负责片区施工进度部署合理；<br />3.对影响工程进度的施工资源（人材机）进行有效调配等。",
                            UserId = ConstructionManager.UserId,
                            SortId = 5,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "技术管理",
                            CheckContent = "1.施工前组织图纸会审设计交底，及时组织编审施工方案；<br />2.严把危大工程施工；<br />3.对焊接技术等把关；<br />4.施工资料与工程进度同步。",
                            UserId = ConstructionManager.UserId,
                            SortId = 6,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 4)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "成本管理",
                            CheckContent = "确认变更、把关签证工程量。",
                            UserId = ConstructionManager.UserId,
                            SortId = 7,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ConstructionManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取质量经理
            var QAManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.QAManager);
            if (QAManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "质量管理",
                    CheckContent = "1. 安排执行质量管理制度、管理体系；<br />2.负责片区不发生质量事故；<br />3.安排落实质量保证措施等。",
                    UserId = QAManager.UserId,
                    SortId = 3,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = QAManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取安全经理
            var SecurityManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.HSSEManager);
            if (SecurityManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "安全生产",
                    CheckContent = "1.执行安全管理各项制度；<br />2.负责片区现场不发生安全事故；<br />3.安排落实安全防护措施；<br />4.及时安排整改安全隐患等。",
                    UserId = SecurityManager.UserId,
                    SortId = 2,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SecurityManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 9,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 10,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增安全专业工程师季度考核明细

        /// <summary>
        /// 新增安全专业工程师季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveSecurityEgItem(string ProjectId, string QuarterCheckId)
        {
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                    CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                    UserId = ProjectUser.UserId,
                    SortId = 6,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取安全经理
            var SecurityManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.HSSEManager);
            if (SecurityManager != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "安全生产",
                            CheckContent = "1.落实安全管理计划；<br />2.编制HSE月报告；<br />3.严格贯彻执行国家、公司安全管理各项制度，协助安全经理组织安全教育、安全检查、安全交底等；<br />4.及时防范避免发生安全事故和人员伤亡；<br />5.检查督促现场做好安全防护措施；<br />6.及时对安全隐患问题督促采取有效措施整改；<br />7.参加安全例会；<br />8.参与现场施工安全事故的处理，并记录；<br />9.审查并检查、督促施工单位执行施工安全保证程序；<br />10.参加重大施工方案的讨论与评审；<br />11.协助定期组织应急预案演练；<br />12.按时记录工作日志等。",
                            UserId = SecurityManager.UserId,
                            SortId = 2,
                            StandardGrade = 30,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "文明施工",
                            CheckContent = "1.督促落实施工现场文明施工；<br />2.督促统一建立标志标牌、场容场貌、三防（防火、防爆、防毒）等。",
                            UserId = SecurityManager.UserId,
                            SortId = 3,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "工程进度",
                            CheckContent = "营造良好项目安全氛围，为工程进度提供保障。",
                            UserId = SecurityManager.UserId,
                            SortId = 4,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "技术管理",
                            CheckContent = "1.落实安全计划，督促分包方施工安全计划和控制措施落实；<br />2.参与审批相关施工方案中涉及施工安全的内容；<br />3.学习并传授专业知识，提升业务能力；<br />4.预防安全事故发生等。<br />",
                            UserId = SecurityManager.UserId,
                            SortId = 5,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SecurityManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取施工经理
            var ConstructionManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ConstructionManager);
            if (ConstructionManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "施工管理",
                    CheckContent = "1.按时参加施工例会，了解整体部署；<br />2.为施工工作开展做好安全保障等。",
                    UserId = ConstructionManager.UserId,
                    SortId = 1,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ConstructionManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 7,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 8,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增质量专业工程师季度考核明细

        /// <summary>
        /// 新增质量专业工程师季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveQAEgItem(string ProjectId, string QuarterCheckId)
        {
            ///获取施工经理
            var ConstructionManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ConstructionManager);
            if (ConstructionManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "岗位工作情况（70%）",
                    TargetClass2 = "施工管理",
                    CheckContent = "1.按时参加施工例会，了解整体部署；<br />2.为施工工作开展做好质量保障；<br />3.参加各项质量检验活动等。",
                    UserId = ConstructionManager.UserId,
                    SortId = 1,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ConstructionManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                    CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                    UserId = ProjectUser.UserId,
                    SortId = 6,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取质量经理
            var QAManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.QAManager);
            if (QAManager != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "安全生产",
                            CheckContent = "执行安全管理各项制度，参加安全教育、安全检查、安全交底等。",
                            UserId = QAManager.UserId,
                            SortId = 2,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "质量管理",
                            CheckContent = "1.参与编制工程质量实施计划和质量控制点及等级划分；<br />2.督促施工单位按照设计图纸和资料、技术规范、规定进行施工，保证施工质量；<br />3.参加质量例会；<br />4.审查并检查、督促施工单位执行施工质量保证程序；<br />5.参加重大施工方案的讨论；<br />6.按规定积累竣工验收需要的资料；<br />7.督促落实质量措施，避免质量事故；<br />8.参与现场施工质量事故的处理，并记录；<br />9.按时记录工作日志等。<br />10.组织质量检查等。",
                            UserId = QAManager.UserId,
                            SortId = 3,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "技术管理",
                            CheckContent = "1.协助编制质量计划，督促分包方施工落实质量控制措施；<br />2.参与审批相关施工方案中涉及施工质量的内容；<br />3.学习并传授专业知识，提升业务能力等。",
                            UserId = QAManager.UserId,
                            SortId = 4,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "进度管理",
                            CheckContent = "营造良好项目质量氛围，为工程进度提供保障。",
                            UserId = QAManager.UserId,
                            SortId = 5,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = QAManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }

            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 6,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 7,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增试车专业工程师季度考核明细

        /// <summary>
        /// 新增试车专业工程师季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveTestEgItem(string ProjectId, string QuarterCheckId)
        {
            ///获取试车经理
            var TestManager = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.TestManager);
            if (TestManager != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "试车管理",
                            CheckContent = "1.严格执行试车相关管理制度、管理体系要求；<br />2.参加设计文件审查；根据项目试车需要，及时到达项目现场开展试车有关工作。<br />3.按照合同要求编制项目试车方案、培训教材、操作手册或规程等，组织开展有关人员培训。<br />4.参与预试车，审查预试车结果，参加工程中间交接。<br />5.根据项目需要，参加开展冷试车、热试车和性能考核等工作。<br />6.编写试车总结，完成试车有关资料的统一归档。",
                            UserId = TestManager.UserId,
                            SortId = 1,
                            StandardGrade = 40,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 1)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "质量管理",
                            CheckContent = "1.按照公司规定和合同要求编制试车有关技术文件，确保文件质量；<br />2.现场试车管理时，严格落实试车方案相关试车质量措施，不发生质量事故。",
                            UserId = TestManager.UserId,
                            SortId = 2,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "进度管理",
                            CheckContent = "1.按照总承包合同和业主要求编制试车工作进度计划；<br />2.施工进度部署合理；<br />3.对影响试车进度的资源（人材机）进行有效调配等。",
                            UserId = TestManager.UserId,
                            SortId = 3,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 3)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "安全环保",
                            CheckContent = "1.	试车期间执行安全管理各项制度，参加安全教育、安全检查、安全交底等。<br />2.严格遵守业主、公司试车安全管理相关规定，确保现场安全环保试车等，不发生安全环保事故。",
                            UserId = TestManager.UserId,
                            SortId = 4,
                            StandardGrade = 10,

                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = TestManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
            ///获取项目经理
            var ProjectUser = BLL.ProjectUserService.GetProjectUserByProjectId(ProjectId, BLL.Const.ProjectManager);
            if (ProjectUser != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                    CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                    UserId = ProjectUser.UserId,
                    SortId = 5,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = ProjectUser.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }


            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "合规管理",
                    CheckContent = "按公司、部门发布的施工管理规定开展业务工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 6,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item);

                Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                {
                    QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                    QuarterCheckId = QuarterCheckId,
                    TargetClass1 = "其他工作情况（30%）",
                    TargetClass2 = "部门工作",
                    CheckContent = "及时完成、上报部门所需数据，及部门安排的工作。",
                    UserId = SGGLManager.UserId,
                    SortId = 7,
                    StandardGrade = 10,
                };
                BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增本部综合管理工程师季度考核明细

        /// <summary>
        /// 新增本部综合管理工程师季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveSGAllEgItem(string ProjectId, string QuarterCheckId)
        {
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {

                        Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "综合管理",
                            CheckContent = "1.综合管理工作安排得当、问题协调解决及时；<br />2.对项目施工管理综合状况的跟踪、记录、汇编和报告；<br />3.档案文件、施工技术标准、资料、报告、记录的管理工作和标准化工作；<br />4.审核规整各方移交的归档资料；<br />5.施工管理部员工的动态跟踪、联络和服务工作；<br />6.施工管理部的日常内务和文书管理工作；<br />7.部室行政事务管理、工作跟踪及预报；<br />8.完成部室主任安排的其他有关工作；<br />9.组织开展部门内审、外审工作；<br />10.策划、组织开展部门年度工作、对外学习对标、部门团队文化建设、部门活动、对外联络、员工培训等；<br />11.组织部门员工亲情关怀工作；<br />12.负责部门宣传工作等。",
                            UserId = SGGLManager.UserId,
                            SortId = 1,
                            StandardGrade = 40,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                    }
                    else if (i == 1)
                    {

                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "部门年度重点工作落实",
                            CheckContent = "1.坚决落实部门工作，特别是重点工作安排部署；<br />2.积极落实部署部门年度重点工作；<br />3.及时完成部门安排的临时性工作。",
                            UserId = SGGLManager.UserId,
                            SortId = 2,
                            StandardGrade = 30,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "其他工作情况（30%）",
                            TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                            CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                            UserId = SGGLManager.UserId,
                            SortId = 3,
                            StandardGrade = 30,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增本部合同管理工程师季度考核明细

        /// <summary>
        /// 新增本部合同管理工程师季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveSGContractEgItem(string ProjectId, string QuarterCheckId)
        {
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {

                        Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "综合管理",
                            CheckContent = "1.施工分包合同示范文本使用意见收集和反馈；<br />2.施工长名单管理；<br />3.施工分包招投标工作和施工合同管理工作；<br />4.公司投标等项目前期的施工管理方面工作；<br />5.参与总承包项目的计划、费用管理工作；<br />6.完成部室主任安排的其他有关工作。",
                            UserId = SGGLManager.UserId,
                            SortId = 1,
                            StandardGrade = 40,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                    }
                    else if (i == 1)
                    {

                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "部门年度重点工作落实",
                            CheckContent = "1.坚决落实部门工作，特别是重点工作安排部署；<br />2.积极落实部署部门年度重点工作；<br />3.及时完成部门安排的临时性工作。",
                            UserId = SGGLManager.UserId,
                            SortId = 2,
                            StandardGrade = 30,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "其他工作情况（30%）",
                            TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                            CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                            UserId = SGGLManager.UserId,
                            SortId = 3,
                            StandardGrade = 30,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }
        #endregion

        #region 新增本部安全质量工程师季度考核明细

        /// <summary>
        /// 新增本部合同管理工程师季度考核明细
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="QuarterCheckId"></param>
        private void SaveSGSecurityQAEgItem(string ProjectId, string QuarterCheckId)
        {
            ///获取部室
            var SGGLManager = BLL.UserService.GetUserByUserId(BLL.Const.SGGLB);
            if (SGGLManager != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {

                        Model.Person_QuarterCheckItem item1 = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "综合管理",
                            CheckContent = "1.对制度进行宣贯，监督制度的执行情况；<br />2.开展在建项目的质量、安全工作检查；<br />3.协助和监督新开工项目施工应急管理体系及QHSE管理体系的建设；<br />4.对项目的QHSE策划、月报告、危险性较大的方案等信息进行收集分析、跟踪；<br />5.宣传施工现场质量、安全的管理亮点；<br />6.更新、发布政府、行业、集团和公司QHSE方面的法律法规、标准规范、规定、通知；<br />7.完成三类人员资格证取证需求；<br />8.完成工作手册相关内容的编制工作等。",
                            UserId = SGGLManager.UserId,
                            SortId = 1,
                            StandardGrade = 40,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item1);
                    }
                    else if (i == 1)
                    {

                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "岗位工作情况（70%）",
                            TargetClass2 = "部门年度重点工作落实",
                            CheckContent = "1.坚决落实部门工作，特别是重点工作安排部署；<br />2.积极落实部署部门年度重点工作；<br />3.及时完成部门安排的临时性工作。",
                            UserId = SGGLManager.UserId,
                            SortId = 2,
                            StandardGrade = 30,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                    else if (i == 2)
                    {
                        Model.Person_QuarterCheckItem item = new Model.Person_QuarterCheckItem
                        {
                            QuarterCheckItemId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckItem)),
                            QuarterCheckId = QuarterCheckId,
                            TargetClass1 = "其他工作情况（30%）",
                            TargetClass2 = " 工作态度<br />遵守纪律<br />工作协调<br />团队精神",
                            CheckContent = "1.工作主动性、责任心；<br />2.遵守公司的考勤制度；<br />3.协调工作范围内的业主、监理、分包商关系；<br />4.团结一致，积极有效开展工作。",
                            UserId = SGGLManager.UserId,
                            SortId = 3,
                            StandardGrade = 30,
                        };
                        BLL.Person_QuarterCheckItemService.AddCheckItem(item);
                    }
                }
                Model.Person_QuarterCheckApprove approve = new Model.Person_QuarterCheckApprove
                {
                    ApproveId = SQLHelper.GetNewID(typeof(Model.Person_QuarterCheckApprove)),
                    QuarterCheckId = QuarterCheckId,
                    UserId = SGGLManager.UserId
                };
                BLL.Person_QuarterCheckApproveService.AddCheckApprove(approve);
            }
        }


        #endregion



        #region 导出

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            string fileId = string.Empty;
            if (keys == null)
            {
                return;
            }
            else
            {
                fileId = keys[0].ToString();
            }
            if (e.CommandName == "export")
            {
                string rootPath = Server.MapPath("~/");
                string initTemplatePath = string.Empty;
                string uploadfilepath = string.Empty;
                string newUrl = string.Empty;
                string filePath = string.Empty;


                string Id = Grid1.SelectedRowID;
                var GetQuarterCheck = BLL.Person_QuarterCheckService.GetPerson_QuarterCheckById(Id);
                if (GetQuarterCheck != null)
                {
                    if (GetQuarterCheck.CheckType == "1")
                    {
                        initTemplatePath = BLL.Const.ConsturctTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "2")
                    {
                        initTemplatePath = BLL.Const.HSSETemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "3")
                    {
                        initTemplatePath = BLL.Const.QATemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "4")
                    {
                        initTemplatePath = BLL.Const.TestTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "5")
                    {
                        initTemplatePath = BLL.Const.ConsturctEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "6")
                    {
                        initTemplatePath = BLL.Const.HSSEEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "7")
                    {
                        initTemplatePath = BLL.Const.QAEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "8")
                    {
                        initTemplatePath = BLL.Const.TestEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "9")
                    {
                        initTemplatePath = BLL.Const.SGALLEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "10")
                    {
                        initTemplatePath = BLL.Const.SGContractEgTemplateUrl;
                    }
                    else if (GetQuarterCheck.CheckType == "11")
                    {
                        initTemplatePath = BLL.Const.SGHSSEQAEgTemplateUrl;
                    }
                    uploadfilepath = rootPath + initTemplatePath;
                    newUrl = uploadfilepath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".doc");
                    filePath = initTemplatePath.Replace(".doc", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
                    File.Copy(uploadfilepath, newUrl);
                    //更新书签内容
                    Document doc = new Aspose.Words.Document(newUrl);
                    Bookmark bookmarkUserName = doc.Range.Bookmarks["UserName"];
                    if (bookmarkUserName != null)
                    {
                        var userName = BLL.UserService.GetUserByUserId(GetQuarterCheck.UserId).UserName;
                        if (userName != null)
                        {
                            bookmarkUserName.Text = userName;
                        }
                    }
                    Bookmark bookmarkProjectName = doc.Range.Bookmarks["ProjectName"];
                    if (bookmarkProjectName != null)
                    {
                        var ProjectName = BLL.ProjectService.GetProjectByProjectId(GetQuarterCheck.ProjectId).ProjectName;
                        if (ProjectName != null)
                        {
                            bookmarkProjectName.Text = ProjectName;
                        }
                    }
                    var GetCheckItemList = Person_QuarterCheckItemService.GetCheckItemListById(GetQuarterCheck.QuarterCheckId);
                    decimal totalValue = 0;
                    if (GetCheckItemList.Count > 0)
                    {
                        Bookmark bookmarkGrade1 = doc.Range.Bookmarks["Grade1"];
                        if (bookmarkGrade1 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 1);
                            if (item != null)
                            {

                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade1.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade1.Text);
                                }

                            }
                        }
                        Bookmark bookmarkGrade2 = doc.Range.Bookmarks["Grade2"];
                        if (bookmarkGrade2 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 2);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade2.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade2.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade3 = doc.Range.Bookmarks["Grade3"];
                        if (bookmarkGrade3 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 3);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade3.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade3.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade4 = doc.Range.Bookmarks["Grade4"];
                        if (bookmarkGrade4 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 4);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade4.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade4.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade5 = doc.Range.Bookmarks["Grade5"];
                        if (bookmarkGrade5 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 5);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade5.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade5.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade6 = doc.Range.Bookmarks["Grade6"];
                        if (bookmarkGrade6 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 6);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade6.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade6.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade7 = doc.Range.Bookmarks["Grade7"];
                        if (bookmarkGrade7 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 7);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade7.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade7.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade8 = doc.Range.Bookmarks["Grade8"];
                        if (bookmarkGrade1 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 8);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade8.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade8.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade9 = doc.Range.Bookmarks["Grade9"];
                        if (bookmarkGrade9 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 9);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade9.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade9.Text);
                                }
                            }

                        }
                        Bookmark bookmarkGrade10 = doc.Range.Bookmarks["Grade10"];
                        if (bookmarkGrade10 != null)
                        {
                            var item = GetCheckItemList.FirstOrDefault(x => x.SortId == 10);
                            if (item != null)
                            {
                                if (!string.IsNullOrEmpty(item.Grade.ToString()))
                                {
                                    var a = Convert.ToDouble(Convert.ToDouble(item.Grade) * (Convert.ToDouble(item.StandardGrade) / 100));
                                    bookmarkGrade10.Text = (decimal.Round(decimal.Parse(a.ToString()), 1)).ToString();
                                    totalValue += Convert.ToDecimal(bookmarkGrade10.Text);
                                }
                            }

                        }


                        Bookmark bookmarkGradeTotal = doc.Range.Bookmarks["GradeTotal"];
                        if (bookmarkGradeTotal != null)
                        {

                            bookmarkGradeTotal.Text = totalValue.ToString();

                        }
                        var approves = Person_QuarterCheckApproveService.GetCheckApproveListById(GetQuarterCheck.QuarterCheckId);
                        foreach (var approve in approves)
                        {
                            var projectUser = ProjectUserService.GetProjectUserByUserIdProjectId(GetQuarterCheck.ProjectId, approve.UserId);
                            if (projectUser != null)
                            {
                                if (projectUser.RoleId == BLL.Const.ConstructionManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {
                                        Bookmark ConstructUser = doc.Range.Bookmarks["ConstructUser"];
                                        if (ConstructUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("ConstructUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                ConstructUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark ConstructDate = doc.Range.Bookmarks["ConstructDate"];
                                        if (ConstructDate != null)
                                        {
                                            ConstructDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }


                                }
                                else if (projectUser.RoleId == BLL.Const.ProjectManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {
                                        Bookmark ProjectUser = doc.Range.Bookmarks["ProjectUser"];
                                        if (ProjectUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("ProjectUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                ProjectUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark ProjectDate = doc.Range.Bookmarks["ProjectDate"];
                                        if (ProjectDate != null)
                                        {
                                            ProjectDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                                else if (projectUser.RoleId == BLL.Const.HSSEManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {


                                        Bookmark HSSEUser = doc.Range.Bookmarks["HSSEUser"];
                                        if (HSSEUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("HSSEUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                HSSEUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark HSSEDate = doc.Range.Bookmarks["HSSEDate"];
                                        if (HSSEDate != null)
                                        {
                                            HSSEDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                                else if (projectUser.RoleId == BLL.Const.QAManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {


                                        Bookmark QAUser = doc.Range.Bookmarks["QAUser"];
                                        if (QAUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("QAUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                QAUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark QADate = doc.Range.Bookmarks["QADate"];
                                        if (QADate != null)
                                        {
                                            QADate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                                else if (projectUser.RoleId == BLL.Const.TestManager)
                                {
                                    if (approve.ApproveDate != null)
                                    {
                                        Bookmark TestUser = doc.Range.Bookmarks["TestUser"];
                                        if (TestUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("TestUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                TestUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark TestDate = doc.Range.Bookmarks["TestDate"];
                                        if (TestDate != null)
                                        {
                                            TestDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var SGUser = UserService.GetUserByUserId(BLL.Const.SGGLB);
                                if (approve.UserId == SGUser.UserId)
                                {
                                    if (approve.ApproveDate != null)
                                    {


                                        Bookmark SGGLUser = doc.Range.Bookmarks["SGUser"];
                                        if (SGGLUser != null)
                                        {
                                            var user = BLL.UserService.GetUserByUserId(approve.UserId);
                                            if (!string.IsNullOrEmpty(user.SignatureUrl))
                                            {
                                                var file = user.SignatureUrl;
                                                if (!string.IsNullOrWhiteSpace(file))
                                                {
                                                    string url = rootPath + file;
                                                    DocumentBuilder builders = new DocumentBuilder(doc);
                                                    builders.MoveToBookmark("SGUser");
                                                    builders.InsertImage(url, 100, 20);
                                                }
                                            }
                                            else
                                            {
                                                SGGLUser.Text = user.UserName;
                                            }
                                        }
                                        Bookmark SGDate = doc.Range.Bookmarks["SGDate"];
                                        if (SGDate != null)
                                        {
                                            SGDate.Text = string.Format("{0:yyyy-MM-dd}", approve.ApproveDate);
                                        }
                                    }
                                }
                            }
                        }
                        doc.Save(newUrl);
                        //生成PDF文件
                        string pdfUrl = newUrl.Replace(".doc", ".pdf");
                        Document doc1 = new Aspose.Words.Document(newUrl);
                        //验证参数
                        if (doc1 == null) { throw new Exception("Word文件无效"); }
                        doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Pdf);//还可以改成其它格式
                        string fileName = Path.GetFileName(filePath);
                        FileInfo info = new FileInfo(pdfUrl);
                        long fileSize = info.Length;
                        Response.Clear();
                        Response.ContentType = "application/x-zip-compressed";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                        Response.AddHeader("Content-Length", fileSize.ToString());
                        Response.TransmitFile(pdfUrl, 0, fileSize);
                        Response.Flush();
                        Response.Close();
                        File.Delete(newUrl);
                        File.Delete(pdfUrl);
                    }
                }
            }
        }
        #endregion
    }
}