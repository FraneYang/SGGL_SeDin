using BLL;
using Model;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectSetSave : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.ConstValue.InitConstValueDropDownList(this.drpProjectType, ConstValue.Group_ProjectType, false);       
                this.ProjectId = Request.QueryString["ProjectId"];
                //// this.txtProjectState.Text = "施工中";
                //if (CommonService.GetIsThisUnit(Const.UnitId_7))
                //{
                //    this.drpProjectState.Readonly = false;
                //}
                /////项目经理
                //UserService.InitUserDropDownList(this.drpProjectManager, string.Empty, false);
                /////施工经理
                //UserService.InitUserDropDownList(this.drpConstructionManager, string.Empty, true);
                /////安全经理
                //UserService.InitUserDropDownList(this.drpHSSEManager, string.Empty, true);
                UnitService.InitUnitDropDownList(this.drpUnit, string.Empty, true);
                this.drpUnit.SelectedValue = Const.UnitId_SEDIN;
                //var thisUnit = BLL.CommonService.GetIsThisUnit();
                //if (thisUnit != null)
                //{
                //    this.drpUnit.SelectedValue = thisUnit.UnitId;
                //    if (thisUnit.UnitId == Const.UnitId_6)
                //    {
                //        this.drpUnit.Label = "所属分公司";
                //    }
                //    if (thisUnit.UnitId == BLL.Const.UnitId_ECEC)
                //    {
                //        this.ckIsUpTotalMonth.Hidden = false;
                //    }
                //    if (thisUnit.UnitId == BLL.Const.UnitId_TCC_)
                //    {
                //        this.trAPP.Hidden = false;
                //        this.trIsForeign.Hidden = false;
                //    }
                //}

                if (!String.IsNullOrEmpty(this.ProjectId))
                {
                    var project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                    if (project != null)
                    {
                        this.txtProjectCode.Text = project.ProjectCode;
                        this.txtProjectName.Text = project.ProjectName;
                        this.txtProjectAddress.Text = project.ProjectAddress;
                        this.txtWorkRange.Text = project.WorkRange;
                        this.txtContractNo.Text = project.ContractNo;
                        if (project.Duration != null)
                        {
                            this.txtDuration.Text = project.Duration.ToString();
                        }
                        if (project.StartDate.HasValue)
                        {
                            this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                        }
                        if (project.EndDate.HasValue)
                        {
                            this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                        }

                        this.txtShortName.Text = project.ShortName;
                        if (!string.IsNullOrEmpty(project.ProjectType))
                        {
                            this.drpProjectType.SelectedValue = project.ProjectType;
                        }
                        this.txtPostCode.Text = project.PostCode;
                        ///项目经理
                        var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.RoleId.Contains(BLL.Const.ProjectManager));
                        if (m != null)
                        {
                            this.drpProjectManager.SelectedValue = m.UserId;
                        }
                        ///施工经理 
                        var c = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.RoleId.Contains(BLL.Const.ConstructionManager));
                        if (c != null)
                        {
                            this.drpConstructionManager.SelectedValue = c.UserId;
                        }
                        ////安全经理
                        var h = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.RoleId.Contains(BLL.Const.HSSEManager));
                        if (h != null)
                        {
                            this.drpHSSEManager.SelectedValue = h.UserId;
                        }

                        this.drpProjectState.SelectedValue = project.ProjectState;
                        //if (project.ProjectState == BLL.Const.ProjectState_2)
                        //{
                        //    this.txtProjectState.Text = "暂停中";
                        //}
                        //else if (project.ProjectState == BLL.Const.ProjectState_3)
                        //{
                        //    this.txtProjectState.Text = "已完工";
                        //}
                        //else
                        //{
                        //    this.txtProjectState.Text = "施工中";
                        //}
                        if (!string.IsNullOrEmpty(project.UnitId))
                        {
                            this.drpUnit.SelectedValue = project.UnitId;
                        }
                       
                        this.ckIsUpTotalMonth.Checked = project.IsUpTotalMonth.Value;
                        if (project.IsForeign == true)
                        {
                            this.ckbIsForeign.Checked = true;
                        }
                        this.txtMapCoordinates.Text = project.MapCoordinates;
                    }
                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Base_Project project = new Base_Project
            {
                ProjectCode = this.txtProjectCode.Text.Trim(),
                ProjectName = Regex.Replace(this.txtProjectName.Text, @"\s", ""),
                ProjectAddress = this.txtProjectAddress.Text.Trim(),
                WorkRange = this.txtWorkRange.Text.Trim(),
                ContractNo = this.txtContractNo.Text.Trim(),
                Duration = Funs.GetNewIntOrZero(this.txtDuration.Text.Trim()),
                MapCoordinates = this.txtMapCoordinates.Text.Trim(),
                ProjectState = this.drpProjectState.SelectedValue,
            };

            if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()))
            {
                project.StartDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                project.EndDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim());
            }

            project.ShortName = this.txtShortName.Text.Trim();
            if (this.drpProjectType.SelectedValue != BLL.Const._Null)
            {
                project.ProjectType = this.drpProjectType.SelectedValue;
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                project.UnitId = this.drpUnit.SelectedValue;
            }
            project.PostCode = this.txtPostCode.Text.Trim();
            project.IsUpTotalMonth = Convert.ToBoolean(this.ckIsUpTotalMonth.Checked);
            project.IsForeign = Convert.ToBoolean(this.ckbIsForeign.Checked);
            if (String.IsNullOrEmpty(this.ProjectId))
            {
                project.ProjectId = SQLHelper.GetNewID(typeof(Model.Base_Project));
                project.ProjectState = BLL.Const.ProjectState_1;
                this.ProjectId = project.ProjectId;
                ProjectService.AddProject(project);
                LogService.AddSys_Log(this.CurrUser, project.ProjectCode, project.ProjectId, BLL.Const.ProjectSetMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                var getProject = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                if (getProject != null)
                {
                    project.FromProjectId = getProject.FromProjectId;
                }
                project.ProjectId = this.ProjectId;
                ProjectService.UpdateProject(project);
                LogService.AddSys_Log(this.CurrUser, project.ProjectCode, project.ProjectId, BLL.Const.ProjectSetMenuId, BLL.Const.BtnModify);
            }

            this.SetProjectManager(project.ProjectId);/// 设置项目、施工、安全经理
            ShowNotify("保存数据成功!", MessageBoxIcon.Success);
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wedId) + ActiveWindow.GetHideReference());

        }

        #region 验证项目名称、项目编号是否存在
        /// <summary>
        /// 验证项目名称、项目编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_Project.FirstOrDefault(x => x.ProjectCode == this.txtProjectCode.Text.Trim() && (x.ProjectId != this.ProjectId || (this.ProjectId == null && x.ProjectId != null)));
            if (q != null)
            {
                ShowNotify("输入的项目编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_Project.FirstOrDefault(x => x.ProjectName == this.txtProjectName.Text.Trim() && (x.ProjectId != this.ProjectId || (this.ProjectId == null && x.ProjectId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的项目名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 设置项目、施工、安全经理
        /// <summary>
        /// 设置项目、施工、安全经理
        /// </summary>
        /// <param name="projectId"></param>
        private void SetProjectManager(string projectId)
        {
            string newProjectManager = this.drpProjectManager.SelectedValue;
            string newConstructionManager = this.drpConstructionManager.SelectedValue;
            string newHSSEManager = this.drpHSSEManager.SelectedValue;
            var project = BLL.ProjectService.GetProjectByProjectId(projectId);
            if (project != null)
            {
                //string OldProjectManager = string.Empty; ////项目经理
                //var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == projectId && x.RoleId.Contains(BLL.Const.ProjectManager));
                //if (m != null)
                //{
                //    OldProjectManager = m.UserId;
                //}
                //////此人不在项目中
                //if (!string.IsNullOrEmpty(newProjectManager) && newProjectManager != OldProjectManager)
                //{
                //    BLL.ProjectUserService.DeleteProjectUserByProjectIdUserId(projectId, newProjectManager);
                //    BLL.ProjectUserService.DeleteProjectUserByProjectIdUserId(projectId, OldProjectManager);
                //    var user = BLL.UserService.GetUserByUserId(newProjectManager);
                //    if (user != null && !string.IsNullOrEmpty(user.UnitId))
                //    {
                //        var punit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(projectId, user.UnitId);
                //        if (punit == null) ///项目单位为空时要增加项目单位
                //        {
                //            Model.Project_ProjectUnit newProjectUnit = new Project_ProjectUnit
                //            {
                //                ProjectId = projectId,
                //                UnitId = user.UnitId,
                //                InTime = System.DateTime.Now
                //            };
                //            BLL.ProjectUnitService.AddProjectUnit(newProjectUnit);
                //        }

                //        Model.Project_ProjectUser newProjectUser = new Model.Project_ProjectUser
                //        {
                //            ProjectId = projectId,
                //            UserId = newProjectManager,
                //            UnitId = user.UnitId,
                //            RoleId = BLL.Const.ProjectManager,
                //            IsPost = true
                //        };
                //        BLL.ProjectUserService.AddProjectUser(newProjectUser);
                //    }
                //}
                //////施工经理
                //string OldConstructionManager = string.Empty;
                //var c = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == projectId && x.RoleId.Contains(BLL.Const.ConstructionManager));
                //if (c != null)
                //{
                //    OldConstructionManager = c.UserId;
                //}
                //////此人不在项目中
                //if (!string.IsNullOrEmpty(newConstructionManager) && newConstructionManager != BLL.Const._Null && newConstructionManager != OldConstructionManager)
                //{
                //    BLL.ProjectUserService.DeleteProjectUserByProjectIdUserId(projectId, newConstructionManager);
                //    BLL.ProjectUserService.DeleteProjectUserByProjectIdUserId(projectId, OldConstructionManager);
                //    var user = BLL.UserService.GetUserByUserId(newConstructionManager);
                //    if (user != null && !string.IsNullOrEmpty(user.UnitId))
                //    {
                //        var punit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(projectId, user.UnitId);
                //        if (punit == null) ///项目单位为空时要增加项目单位
                //        {
                //            Model.Project_ProjectUnit newProjectUnit = new Project_ProjectUnit
                //            {
                //                ProjectId = projectId,
                //                UnitId = user.UnitId,
                //                InTime = System.DateTime.Now
                //            };
                //            BLL.ProjectUnitService.AddProjectUnit(newProjectUnit);
                //        }

                //        Model.Project_ProjectUser newProjectUser = new Model.Project_ProjectUser
                //        {
                //            ProjectId = projectId,
                //            UserId = newConstructionManager,
                //            UnitId = user.UnitId,
                //            RoleId = BLL.Const.ConstructionManager,
                //            IsPost = true
                //        };
                //        BLL.ProjectUserService.AddProjectUser(newProjectUser);
                //    }
                //}
                /////安全经理
                //string OldHSSEManager = string.Empty;
                //var h = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == projectId && x.RoleId.Contains(BLL.Const.HSSEManager));
                //if (h != null)
                //{
                //    OldHSSEManager = h.UserId;
                //}
                //////此人不在项目中
                //if (!string.IsNullOrEmpty(newHSSEManager) && newHSSEManager != BLL.Const._Null && newHSSEManager != OldHSSEManager)
                //{
                //    BLL.ProjectUserService.DeleteProjectUserByProjectIdUserId(projectId, newHSSEManager);
                //    BLL.ProjectUserService.DeleteProjectUserByProjectIdUserId(projectId, OldHSSEManager);
                //    var user = BLL.UserService.GetUserByUserId(newHSSEManager);
                //    if (user != null && !string.IsNullOrEmpty(user.UnitId))
                //    {
                //        var punit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(projectId, user.UnitId);
                //        if (punit == null) ///项目单位为空时要增加项目单位
                //        {
                //            Model.Project_ProjectUnit newProjectUnit = new Project_ProjectUnit
                //            {
                //                ProjectId = projectId,
                //                UnitId = user.UnitId,
                //                InTime = System.DateTime.Now
                //            };
                //            BLL.ProjectUnitService.AddProjectUnit(newProjectUnit);
                //        }

                //        Model.Project_ProjectUser newProjectUser = new Model.Project_ProjectUser
                //        {
                //            ProjectId = projectId,
                //            UserId = newHSSEManager,
                //            UnitId = user.UnitId,
                //            RoleId = BLL.Const.HSSEManager,
                //            IsPost = true
                //        };
                //        BLL.ProjectUserService.AddProjectUser(newProjectUser);
                //    }
                //}
            }
        }
        #endregion
    }
}