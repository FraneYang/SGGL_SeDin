﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class CheckerManageEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnitId, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
                string CheckerId = Request.Params["CheckerId"];
                if (!string.IsNullOrEmpty(CheckerId))
                {
                    Model.SitePerson_Person Checker = BLL.CheckerService.GetCheckerById(CheckerId);
                    if (Checker != null)
                    {
                        //this.txtCheckerCode.Text = Checker.CheckerCode;
                        this.txtCheckerName.Text = Checker.PersonName;

                        if (!string.IsNullOrEmpty(Checker.UnitId))
                        {
                            this.drpUnitId.SelectedValue = Checker.UnitId;
                        }
                        this.rblSex.SelectedValue = Checker.Sex;

                        if (Checker.Birthday.HasValue)
                        {
                            this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", Checker.Birthday);
                        }
                        this.txtIdentityCard.Text = Checker.IdentityCard;
                        if (Checker.IsUsed == true)
                        {
                            cbIsOnDuty.Checked = true;
                        }
                        else
                        {
                            cbIsOnDuty.Checked = false;
                        }
                    }
                }
                else
                {
                    this.cbIsOnDuty.Checked = true;
                }
            }
        }

        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
             string checkerId = Request.Params["CheckerId"];
            var q = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.WelderCode == this.txtCheckerCode.Text.Trim()
           && (x.PersonId != checkerId || (checkerId == null && checkerId != null)));
            if (q != null)
            {
                Alert.ShowInTop("焊工号已经存在！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("选择单位", MessageBoxIcon.Warning);
                return;
            }
            
            checkerId = SaveData(checkerId);
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        private string SaveData(string checkerId)
        {
            Model.SitePerson_Person newChecker = new Model.SitePerson_Person();
            newChecker.WelderCode = this.txtCheckerCode.Text.Trim();
            newChecker.PersonName = this.txtCheckerName.Text.Trim();
            newChecker.WorkPostId = Const.WorkPost_Checker;
            newChecker.ProjectId = this.CurrUser.LoginProjectId;
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                newChecker.UnitId = this.drpUnitId.SelectedValue;
            }
            newChecker.Sex = this.rblSex.SelectedValue;
            newChecker.Birthday = Funs.GetNewDateTime(this.txtBirthday.Text.Trim());
            newChecker.IdentityCard = this.txtIdentityCard.Text.Trim();
            if (this.cbIsOnDuty.Checked)
            {
                newChecker.IsUsed = true;
            }
            else
            {
                newChecker.IsUsed = false;
            }
            newChecker.Isprint = "0";
            if (!string.IsNullOrEmpty(checkerId))
            {
                if (!BLL.CheckerService.IsExisCheckerCode(checkerId, this.txtCheckerCode.Text))
                {
                    newChecker.PersonId = checkerId;
                    BLL.CheckerService.UpdateChecker(newChecker);
                    //BLL.Sys_LogService.AddLog(Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnModify, checkerId);
                }
                else
                {
                    Alert.ShowInParent("焊工号已经存在！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                checkerId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                if (!BLL.CheckerService.IsExisCheckerCode(checkerId, this.txtCheckerCode.Text))
                {
                    newChecker.PersonId = checkerId;
                    BLL.CheckerService.AddChecker(newChecker);
                    //BLL.Sys_LogService.AddLog(Const.System_1, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.WelderManageMenuId, Const.BtnAdd, checkerId);
                }
                else
                {
                    Alert.ShowInParent("焊工号已经存在！", MessageBoxIcon.Warning);
                }
            }
            return checkerId;
        }

        protected void drpUnitId_SelectedIndexChanged1(object sender, EventArgs e)
        {

            if (this.drpUnitId.SelectedValue != Const._Null)
            {
                var u = BLL.UnitService.GetUnitByUnitId(drpUnitId.SelectedValue);
                string prefix = u.UnitCode + "-HG-";
                txtCheckerCode.Text = BLL.SQLHelper.RunProcNewId("SpGetThreeNumber", "SitePerson_Person", "WelderCode", prefix);
            }
        }
    }
}