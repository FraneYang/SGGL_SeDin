﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class WorkPackageInitEdit : PageBase
    {
        /// <summary>
        /// 分部分项编号
        /// </summary>
        public string WorkPackageCode
        {
            get
            {
                return (string)ViewState["WorkPackageCode"];
            }
            set
            {
                ViewState["WorkPackageCode"] = value;
            }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public string PackageCode
        {
            get
            {
                return (string)ViewState["PackageCode"];
            }
            set
            {
                ViewState["PackageCode"] = value;
            }
        }

        /// <summary>
        /// 工程类型
        /// </summary>
        public string ProjectType
        {
            get
            {
                return (string)ViewState["ProjectType"];
            }
            set
            {
                ViewState["ProjectType"] = value;
            }
        }

        /// <summary>
        /// 父级编号
        /// </summary>
        public string SuperWorkPack
        {
            get
            {
                return (string)ViewState["SuperWorkPack"];
            }
            set
            {
                ViewState["SuperWorkPack"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkPackageCode = Request.Params["Id"];
                if (Request.Params["type"] == "add")
                {
                    List<String> codelist = null;
                    codelist = (from x in Funs.DB.WBS_WorkPackageInit
                                where x.SuperWorkPack == WorkPackageCode
                                orderby x.WorkPackageCode
                                select x.WorkPackageCode).ToList();
                    SuperWorkPack = WorkPackageCode;
                    var workPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(WorkPackageCode);
                    if (workPackageInit != null)
                    {
                        ProjectType = workPackageInit.ProjectType;
                    }
                    string newCode = string.Empty;
                    string c = string.Empty;
                    string preCode = string.Empty;
                    if (codelist.Count() > 0)
                    {
                        string oldCode = codelist[codelist.Count - 1];
                        preCode = oldCode.Substring(0, oldCode.Length - 2);
                        string num = oldCode.Substring(oldCode.Length - 2, 2);
                        int a = Convert.ToInt32(num);
                        int b = a + 1;

                        if (b.ToString().Length == 1)
                        {
                            c = "0" + b.ToString();
                        }
                        else
                        {
                            c = b.ToString();
                        }
                    }
                    else
                    {
                        preCode = WorkPackageCode;
                        c = "01";
                    }
                    PackageCode = c;
                    newCode = preCode + c;
                    this.txtWorkPackageCode.Text = newCode;
                }
                if (Request.Params["type"] == "modify")
                {
                    this.txtWorkPackageCode.Text = WorkPackageCode;
                    Model.WBS_WorkPackageInit workPackageInit = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(WorkPackageCode);
                    if (workPackageInit != null)
                    {
                        this.txtWorkPackageName.Text = workPackageInit.PackageContent;
                        if (workPackageInit.IsChild == true)
                        {
                            this.drpIsChild.SelectedValue = "True";
                        }
                        else
                        {
                            this.drpIsChild.SelectedValue = "False";
                        }
                        SuperWorkPack = workPackageInit.SuperWorkPack;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemInitSetMenuId, BLL.Const.BtnSave))
            {
                if (!BLL.WorkPackageInitService.IsExistWorkPackageInitName(this.SuperWorkPack, this.txtWorkPackageName.Text.Trim(), this.txtWorkPackageCode.Text.Trim()))
                {
                    Model.WBS_WorkPackageInit newWorkPackage = new Model.WBS_WorkPackageInit();
                    newWorkPackage.WorkPackageCode = this.txtWorkPackageCode.Text.Trim();
                    newWorkPackage.PackageContent = this.txtWorkPackageName.Text.Trim();
                    newWorkPackage.SuperWorkPack = SuperWorkPack;
                    newWorkPackage.IsChild = Convert.ToBoolean(this.drpIsChild.SelectedValue.Trim());
                    newWorkPackage.ProjectType = ProjectType;
                    if (Request.Params["type"] == "add")
                    {
                        newWorkPackage.PackageCode = this.PackageCode;
                        BLL.WorkPackageInitService.AddWorkPackageInit(newWorkPackage);
                        BLL.LogService.AddSys_Log(this.CurrUser, newWorkPackage.WorkPackageCode, newWorkPackage.WorkPackageCode, BLL.Const.ControlItemInitSetMenuId, "增加分部分项信息！");
                        PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtWorkPackageCode.Text.Trim()) + ActiveWindow.GetHidePostBackReference());
                    }
                    if (Request.Params["type"] == "modify")
                    {
                        BLL.WorkPackageInitService.UpdateWorkPackageInit(newWorkPackage);
                        BLL.LogService.AddSys_Log(this.CurrUser, newWorkPackage.WorkPackageCode, newWorkPackage.WorkPackageCode, BLL.Const.ControlItemInitSetMenuId, "修改分部分项信息！");
                        PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtWorkPackageCode.Text.Trim()) + ActiveWindow.GetHidePostBackReference());
                    }
                }
                else
                {
                    ShowNotify("此分部分项名称已存在！", MessageBoxIcon.Warning);
                }
                //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
    }
}