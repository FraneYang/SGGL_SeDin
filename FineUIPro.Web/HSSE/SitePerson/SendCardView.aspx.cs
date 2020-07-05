using BLL;
using System;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class SendCardView : PageBase
    {
        /// <summary>
        /// 主键
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lbThisUnit.Text = UnitService.GetUnitNameByUnitId(Const.UnitId_SEDIN);
                this.lbProject.Text = ProjectService.GetProjectNameByProjectId(this.CurrUser.LoginProjectId);

                this.PersonId = Request.Params["PersonId"];
                var getPerson = PersonService.GetPersonById(this.PersonId);
                if (getPerson != null)
                {
                    this.txtUnit.Text = UnitService.GetUnitNameByUnitId(getPerson.UnitId);
                    this.txtWorkPost.Text = WorkPostService.getWorkPostNamesWorkPostIds(getPerson.WorkPostId);
                    this.txtName.Text = getPerson.PersonName;
                    this.txtCardNo.Text = getPerson.CardNo;
                    if (!string.IsNullOrEmpty(getPerson.QRCodeAttachUrl) && CreateQRCodeService.isHaveImage(getPerson.QRCodeAttachUrl))
                    {
                        this.Img5.Src = "~/" + getPerson.QRCodeAttachUrl;
                    }
                    else
                    {
                        string url = CreateQRCodeService.CreateCode_Simple(getPerson.IdentityCard);
                        getPerson.QRCodeAttachUrl = url;
                        BLL.PersonService.UpdatePerson(getPerson);
                        this.Img5.Src = "~/" + url;
                    }
                    if (!string.IsNullOrEmpty(getPerson.PhotoUrl))
                    {
                        this.Img6.Src = "~/" + getPerson.PhotoUrl;
                    }
                }
            }
        }
    }
}