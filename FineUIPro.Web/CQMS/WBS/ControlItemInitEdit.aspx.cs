using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CQMS.WBS
{
    public partial class ControlItemInitEdit : PageBase
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
        /// 工作包编号
        /// </summary>
        public string ControlItemCode
        {
            get
            {
                return (string)ViewState["ControlItemCode"];
            }
            set
            {
                ViewState["ControlItemCode"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.FineUIPleaseSelect(this.drpControlPoint);
                WorkPackageCode = Request.Params["WorkPackageCode"];
                var workPackage = BLL.WorkPackageInitService.GetWorkPackageInitByWorkPackageCode(WorkPackageCode);
                if (workPackage.ProjectType == "1") //建筑工程
                {
                    txtHGForms.Label = "对应的资料表格";
                    tr6.Hidden = true;
                }
                else
                {
                    txtHGForms.Text = "对应的化工资料表格";
                    tr6.Hidden = false;
                }
                if (Request.Params["type"] == "add")
                {
                    string newCode = string.Empty;
                    List<Model.WBS_ControlItemInit> list = BLL.ControlItemInitService.GetItemsByWorkPackageCode(WorkPackageCode);
                    if (list != null)
                    {
                        string oldCode = list[list.Count - 1].ControlItemCode;
                        string num = oldCode.Substring(oldCode.Length - 2, 2);
                        int a = Convert.ToInt32(num);
                        int b = a + 1;
                        string c;
                        if (b.ToString().Length == 1)
                        {
                            c = "0" + b.ToString();
                        }
                        else
                        {
                            c = b.ToString();
                        }
                        newCode = oldCode.Substring(0, oldCode.Length - 2) + c;
                    }
                    else
                    {
                        newCode = WorkPackageCode + "01";
                    }
                    this.txtControlItemCode.Text = newCode;
                }
                if (Request.Params["type"] == "modify")
                {
                    ControlItemCode = Request.Params["ControlItemCode"];
                    Model.WBS_ControlItemInit controlItem = BLL.ControlItemInitService.GetControlItemInitByCode(ControlItemCode);
                    WorkPackageCode = controlItem.WorkPackageCode;
                    this.txtControlItemCode.Text = controlItem.ControlItemCode;
                    this.txtControlItemContent.Text = controlItem.ControlItemContent;
                    this.drpControlPoint.SelectedValue = controlItem.ControlPoint;
                    this.txtControlItemDef.Text = controlItem.ControlItemDef;
                    if (controlItem.Weights != null)
                    {
                        this.txtWeights.Text = controlItem.Weights.ToString();
                    }
                    this.txtHGForms.Text = controlItem.HGForms;
                    this.txtSHForms.Text = controlItem.SHForms;
                    this.txtStandard.Text = controlItem.Standard;
                    this.txtClauseNo.Text = controlItem.ClauseNo;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ControlItemInitSetMenuId, BLL.Const.BtnSave))
            {
                if (!BLL.ControlItemInitService.IsExistControlItemInitName(this.WorkPackageCode, this.txtControlItemContent.Text.Trim(), this.txtControlItemCode.Text.Trim()))
                {
                    Model.WBS_ControlItemInit newControlItem = new Model.WBS_ControlItemInit();
                    newControlItem.ControlItemCode = this.txtControlItemCode.Text.Trim();
                    newControlItem.WorkPackageCode = this.WorkPackageCode;
                    newControlItem.ControlItemContent = this.txtControlItemContent.Text.Trim();
                    newControlItem.ControlPoint = this.drpControlPoint.SelectedValue;
                    newControlItem.ControlItemDef = this.txtControlItemDef.Text.Trim();
                    if (!string.IsNullOrEmpty(this.txtWeights.Text.Trim()))
                    {
                        newControlItem.Weights = Convert.ToDecimal(this.txtWeights.Text.Trim());
                    }
                    newControlItem.HGForms = this.txtHGForms.Text.Trim();
                    newControlItem.SHForms = this.txtSHForms.Text.Trim();
                    newControlItem.Standard = this.txtStandard.Text.Trim();
                    newControlItem.ClauseNo = this.txtClauseNo.Text.Trim();
                    if (Request.Params["type"] == "add")
                    {
                        BLL.ControlItemInitService.AddControlItemInit(newControlItem);
                        BLL.LogService.AddSys_Log(this.CurrUser, newControlItem.ControlItemCode, newControlItem.ControlItemCode, BLL.Const.ControlItemInitSetMenuId, "增加工作包信息！");
                        //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtControlItemCode.Text.Trim()) + ActiveWindow.GetHidePostBackReference());
                    }
                    if (Request.Params["type"] == "modify")
                    {
                        BLL.ControlItemInitService.UpdateControlItemInit(newControlItem);
                        BLL.LogService.AddSys_Log(this.CurrUser, newControlItem.ControlItemCode, newControlItem.ControlItemCode, BLL.Const.ControlItemInitSetMenuId, "修改工作包信息！");
                        //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(txtControlItemCode.Text.Trim()) + ActiveWindow.GetHidePostBackReference());
                    }
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    ShowNotify("此工作包已存在！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
    }
}