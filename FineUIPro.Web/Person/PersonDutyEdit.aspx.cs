using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.Person
{
    public partial class PersonDutyEdit :PageBase
    {
        #region 定义项
        /// <summary>
        /// 员工责任主键
        /// </summary>
        public string DutyId
        {
            get
            {
                return (string)ViewState["DutyId"];
            }
            set
            {
                ViewState["DutyId"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UserService.InitUserUnitIdDropDownList(drpSEDINUser, Const.UnitId_SEDIN, true);
                BLL.WorkPostService.InitWorkPostDropDownList(drpWorkPost, true);
                BLL.UserService.InitUserUnitIdDepartIdDropDownList(drpHandleMan, Const.UnitId_SEDIN, Const.Depart_constructionId,true);
                this.DutyId = Request.Params["DutyId"];
                if (!string.IsNullOrEmpty(this.DutyId)) {
                    var PersonDuty = BLL.Person_DutyService.GetPersonDutyById(this.DutyId);
                    if (PersonDuty != null) {
                        if (!string.IsNullOrEmpty(PersonDuty.DutyPersonId)) {
                            this.drpSEDINUser.SelectedValue=PersonDuty.DutyPersonId;
                        }
                        if (!string.IsNullOrEmpty(PersonDuty.WorkPostId)) {
                            this.drpWorkPost.SelectedValue = PersonDuty.WorkPostId;
                        }
                        if (!string.IsNullOrEmpty(PersonDuty.Template))
                        {
                            this.txtTemplate.Text = HttpUtility.HtmlDecode(PersonDuty.Template);
                        }
                        if (PersonDuty.State == "2") {
                            this.drpHandleMan.Hidden = true;
                            this.IsAgree.Hidden = false;
                        }
                    }
                }
                
            }
        }
        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.BtnSubmit);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            
            var PersonDuty = BLL.Person_DutyService.GetPersonDutyById(this.DutyId);
            if (PersonDuty != null) {
                if (PersonDuty.State == "1")
                {
                    if (this.drpHandleMan.SelectedValue == BLL.Const._Null)
                    {
                        Alert.ShowInTop("请选择办理人", MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtTemplate.Text))
                    {
                        Alert.ShowInTop("模板内容不能为空", MessageBoxIcon.Warning);
                        return;
                    }
                    PersonDuty.Template = HttpUtility.HtmlEncode(this.txtTemplate.Text);
                    PersonDuty.ApprovePersonId = drpHandleMan.SelectedValue;
                    PersonDuty.DutyTime = DateTime.Now;
                    PersonDuty.State = "2";
                    Funs.DB.SubmitChanges();
                }
                else if (PersonDuty.State == "2") {
                    if (this.rdbIsAgree.SelectedValue.Contains("false"))
                    {
                        PersonDuty.State = "1";
                    }
                    else {
                        PersonDuty.ApproveTime = DateTime.Now;
                        PersonDuty.State = "3";
                    }
                    
                    Funs.DB.SubmitChanges();
                }
                
            }
            
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpHandleMan.Items.Clear();
            var PersonDuty = BLL.Person_DutyService.GetPersonDutyById(DutyId);
            if (this.rdbIsAgree.SelectedValue.Contains("false"))
            {
                this.drpHandleMan.Hidden = false;
                BLL.UserService.InitUserUnitIdDropDownList(drpHandleMan, Const.UnitId_SEDIN, true);
                this.drpHandleMan.SelectedValue = PersonDuty.DutyPersonId;
                this.drpHandleMan.Label = "打回责任人";
                this.drpHandleMan.Readonly = true;
            }
            else
            {

                this.drpHandleMan.Hidden = true;
            }
        }
    }
}