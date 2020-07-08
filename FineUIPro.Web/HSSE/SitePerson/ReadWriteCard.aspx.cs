using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class ReadWriteCard : PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员Id
        /// </summary>
        public string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.PersonId = Request.Params["PersonId"];
                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    var person = BLL.PersonService.GetPersonById(this.PersonId);
                    if (person != null)
                    {
                        this.txtPersonName.Text = person.PersonName;
                        this.txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(person.UnitId);
                        this.txtCardNo.Text = person.CardNo;
                    }
                }
            }
            //this.SetDefaltButton(this.txtCardNo, this.TextBox2);
        }

        /// <summary>
        /// 发卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendCard_Click(object sender, EventArgs e)
        {
            int newPersonIndex = 1;
            int? maxPersonIndex = BLL.PersonService.GetMaxPersonIndex(this.CurrUser.LoginProjectId);
            if (maxPersonIndex != null)
            {
                newPersonIndex = Convert.ToInt32(maxPersonIndex) + 1;
                List<int?> personIndexs = BLL.PersonService.GetPersonIndexs(this.CurrUser.LoginProjectId);
                for (int i = 1; i <= maxPersonIndex; i++)
                {
                    bool result = false;
                    foreach (int? personIndex in personIndexs)
                    {
                        if (i == personIndex)
                        {
                            result = true;
                        }
                    }

                    if (!result)
                    {
                        newPersonIndex = i;
                    }
                }
            }
            else
            {
                newPersonIndex = 1;
            }
            //string cardNo = this.txtCardNo.Text.Trim() + this.TextBox2.Text.Trim();
            string cardNo = this.txtCardNo.Text.Trim();
            try
            {
                if (!String.IsNullOrEmpty(cardNo))
                {
                    BLL.PersonService.SaveSendCard(PersonId, cardNo, newPersonIndex);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            catch
            {
                ShowNotify("发卡未成功！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 验证卡号是否存在
        /// <summary>
        /// 验证卡号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                var q = new Model.SGGLDB(Funs.ConnString).SitePerson_Person.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.CardNo == this.txtCardNo.Text.Trim() && (x.PersonId != this.PersonId || (this.PersonId == null && x.PersonId != null)));
                if (q != null)
                {
                    ShowNotify("输入的卡号已存在！", MessageBoxIcon.Warning);
                }
            }
        }
        #endregion
    }
}