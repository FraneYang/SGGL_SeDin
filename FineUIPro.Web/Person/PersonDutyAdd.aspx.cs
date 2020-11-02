using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.Person
{
    public partial class PersonDutyAdd :PageBase
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
                WorkPostService.InitMainWorkPostDropDownList(drpWorkPost, true);
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
                    }
                }
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.BtnSave);
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

            if (this.drpSEDINUser.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择员工", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpWorkPost.SelectedValue == BLL.Const._Null) {
                Alert.ShowInTop("岗位不能为空", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtTemplate.Text))
            {
                Alert.ShowInTop("模板内容不能为空", MessageBoxIcon.Warning);
                return;
            }
            Model.Person_Duty PersonDuty = new Model.Person_Duty
            {
                DutyPersonId = this.drpSEDINUser.SelectedValue,
                CompilePersonId = CurrUser.UserId,
                CompileTime = DateTime.Now,
                WorkPostId=this.drpWorkPost.SelectedValue,
                Template= HttpUtility.HtmlEncode(this.txtTemplate.Text),
        };
            if (type == BLL.Const.BtnSubmit)
            {
                
                PersonDuty.State = "1";
            }
            else
            {
                PersonDuty.State = "0";
            }
            var getDuty = BLL.Person_DutyService.GetPersonDutyById(DutyId);
            if (getDuty == null)
            {
                if (string.IsNullOrEmpty(this.DutyId))
                {
                    PersonDuty.DutyId = SQLHelper.GetNewID(typeof(Model.Person_Duty));
                }
                else
                {
                    PersonDuty.DutyId = this.DutyId;
                }
                BLL.Person_DutyService.AddPersonDuty(PersonDuty);


            }
            else
            {
                PersonDuty.DutyId = this.DutyId;
                BLL.Person_DutyService.UpdatePersonDuty(PersonDuty);
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void drpSEDINUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSEDINUser.SelectedValue != BLL.Const._Null) {
                var getUser = BLL.UserService.GetUserByUserId(drpSEDINUser.SelectedValue);
                if (getUser != null) {
                    this.drpWorkPost.SelectedValue = getUser.WorkPostId;
                    if (this.drpWorkPost.SelectedValue != null) {
                        var getWorkPost = BLL.WorkPostService.GetWorkPostById(this.drpWorkPost.SelectedValue);
                        if (!string.IsNullOrEmpty(getWorkPost.Template)) {
                            this.txtTemplate.Text = HttpUtility.HtmlDecode(getWorkPost.Template);
                        }
                        
                    }
                    
                    
                }
            }
        }

        protected void drpWorkPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtTemplate.Text = HttpUtility.HtmlDecode(string.Empty);
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
            {
                var dutyTemplate = BLL.Person_DutyTemplateService.GetPersondutyTemplateByWorkPostId(this.drpWorkPost.SelectedValue);
                if (dutyTemplate != null)
                {
                    if (!string.IsNullOrEmpty(dutyTemplate.Template))
                    {
                        this.txtTemplate.Text = HttpUtility.HtmlDecode(dutyTemplate.Template);
                    }
                }
            }
        }
    }
}