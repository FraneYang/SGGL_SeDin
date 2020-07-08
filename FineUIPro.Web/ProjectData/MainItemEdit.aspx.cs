using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FineUIPro.Web.ProjectData
{
    public partial class MainItemEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string MainItemId
        {
            get
            {
                return (string)ViewState["MainItemId"];
            }
            set
            {
                ViewState["MainItemId"] = value;
            }
        }
        /// <summary>
        /// 项目id
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                {
                    this.ProjectId = this.CurrUser.LoginProjectId;
                }
                else
                {
                    this.ProjectId = this.CurrUser.LoginProjectId;
                }
                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.ProjectId).ProjectName;
                MainItemId = Request.Params["MainItemId"];
                gvCarryUnit.DataSource = (from x in new Model.SGGLDB(Funs.ConnString).WBS_UnitWork where x.SuperUnitWork == null && x.ProjectId == this.CurrUser.LoginProjectId orderby x.UnitWorkCode select x);
                gvCarryUnit.DataBind();
                if (!string.IsNullOrEmpty(MainItemId))
                {
                    Model.ProjectData_MainItem MaineItem = BLL.MainItemService.GetMainItemByMainItemId(MainItemId);
                    this.txtMainItemCode.Text = MaineItem.MainItemCode;
                    this.txtMainItemName.Text = MaineItem.MainItemName;
                    this.txtRemark.Text = MaineItem.Remark;
                    if (!string.IsNullOrEmpty(MaineItem.UnitWorkIds))
                    {
                        txtCarryUnit.Values = MaineItem.UnitWorkIds.Split(',');
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.ProjectData_MainItem maiItem = new Model.ProjectData_MainItem();
            maiItem.MainItemCode = this.txtMainItemCode.Text;
            maiItem.MainItemName = this.txtMainItemName.Text;
            maiItem.ProjectId = this.ProjectId;
            maiItem.UnitWorkIds = string.Join(",", txtCarryUnit.Values);
            maiItem.Remark = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(MainItemId))
            {
                maiItem.MainItemId = MainItemId;
                BLL.MainItemService.UpdateMainItem(maiItem);
            }
            else
            {
                maiItem.MainItemId = SQLHelper.GetNewID(typeof(Model.ProjectData_MainItem));
                BLL.MainItemService.AddMainItem(maiItem);
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void txtMainItemCode_TextChanged(object sender, EventArgs e)
        {
            if (BLL.MainItemService.IsExistMainItem(this.txtMainItemCode.Text.Trim(), this.ProjectId))
            {
                Alert.ShowInTop("此主项和单位工程对应关系编号已存在！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}