using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Personal
{
    public partial class PersonalInfo : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 照片附件路径
        /// </summary>
        public string PhotoAttachUrl
        {
            get
            {
                return (string)ViewState["PhotoAttachUrl"];
            }
            set
            {
                ViewState["PhotoAttachUrl"] = value;
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
                /// Tab1加载页面方法
                this.Tab1LoadData();
            }
        }

        #region Tab1
        /// <summary>
        /// Tab1加载页面方法
        /// </summary>
        private void Tab1LoadData()
        {
            var user = Funs.DB.Sys_User.FirstOrDefault(x=>x.UserId == this.CurrUser.UserId);
            if (user != null)
            {
                this.txtUserName.Text = user.UserName;
                this.txtUserCode.Text = user.UserCode;
                var sexVules = BLL.ConstValue.drpConstItemList(ConstValue.Group_0002).FirstOrDefault(x => x.ConstValue == user.Sex);
                if (sexVules != null)
                {
                    this.drpSex.Text = sexVules.ConstText;
                }

                this.dpBirthDay.Text = string.Format("{0:yyyy-MM-dd}", user.BirthDay);
                var MarriageVules = BLL.ConstValue.drpConstItemList(ConstValue.Group_0003).FirstOrDefault(x => x.ConstValue == user.Marriage);
                if (MarriageVules != null)
                {
                    this.drpMarriage.Text = MarriageVules.ConstText;
                }

                var NationVules = BLL.ConstValue.drpConstItemList(ConstValue.Group_0005).FirstOrDefault(x => x.ConstValue == user.Nation);
                if (NationVules != null)
                {
                    this.drpNation.Text = NationVules.ConstText;
                }
                var units = Funs.DB.Base_Unit.FirstOrDefault(x=>x.UnitId == user.UnitId);
                if (units != null)
                {
                    this.drpUnit.Text = units.UnitName;
                }
                this.txtAccount.Text = user.Account;
                this.txtIdentityCard.Text = user.IdentityCard;
                this.txtEmail.Text = user.Email;
                this.txtTelephone.Text = user.Telephone;
                var EducationVules = BLL.ConstValue.drpConstItemList(ConstValue.Group_0004).FirstOrDefault(x => x.ConstValue == user.Education);
                if (EducationVules != null)
                {
                    this.drpEducation.Text = EducationVules.ConstText;
                }
                this.txtHometown.Text = user.Hometown;
                //var position = BLL.PositionService.GetPositionById(user.PositionId);
                //if (position != null)
                //{
                //    this.drpPosition.Text = position.PositionName;
                //}
                this.txtPerformance.Text = user.Performance;
                if (!string.IsNullOrEmpty(user.PhotoUrl))
                {
                    this.PhotoAttachUrl = user.PhotoUrl;
                    this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
                }
            }
        }
        #endregion              
    }
}