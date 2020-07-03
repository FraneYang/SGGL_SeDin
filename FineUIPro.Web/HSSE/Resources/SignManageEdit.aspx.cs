using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.HSSE.Resources
{
    public partial class SignManageEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 标牌主键
        /// </summary>
        public string SignManageId
        {
            get
            {
                return (string)ViewState["SignManageId"];
            }
            set
            {
                ViewState["SignManageId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 标牌编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SignManageId = Request.Params["SignManageId"];
                BLL.ConstValue.InitConstValueDropDownList(this.drpSignType, ConstValue.Group_SignType, true);
                if (!string.IsNullOrEmpty(this.SignManageId))
                {
                    var signManage = BLL.SignManageService.GetSignManageBySignManageId(this.SignManageId);
                    if (signManage != null)
                    {
                        this.txtSignCode.Text = signManage.SignCode;
                        this.txtSignName.Text = signManage.SignName;
                        this.txtSignLen.Text = signManage.SignLen;
                        this.txtSignWide.Text = signManage.SignWide;
                        this.txtSignHigh.Text = signManage.SignHigh;
                        this.txtSignThick.Text = signManage.SignThick;
                        this.txtMaterial.Text = signManage.Material;
                        this.txtSignArea.Text = signManage.SignArea;
                        if (!string.IsNullOrEmpty(signManage.SignType))
                        {
                            this.drpSignType.SelectedValue = signManage.SignType;
                        }
                        this.txtSignImage.Text = HttpUtility.HtmlDecode(signManage.SignImage);
                    }
                }
                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
                }
            }
        }


        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            Model.Resources_SignManage newSignManage = new Model.Resources_SignManage
            {
                SignCode = this.txtSignCode.Text.Trim(),
                SignName = this.txtSignName.Text.Trim(),
                SignLen = this.txtSignLen.Text.Trim(),
                SignWide = this.txtSignWide.Text.Trim(),
                SignHigh = this.txtSignHigh.Text.Trim(),
                SignThick = this.txtSignThick.Text.Trim(),
                Material = this.txtMaterial.Text.Trim(),
                SignArea = this.txtSignArea.Text.Trim()
            };
            if (this.drpSignType.SelectedValue != BLL.Const._Null)
            {
                newSignManage.SignType = this.drpSignType.SelectedValue;
            }
            newSignManage.SignImage = HttpUtility.HtmlEncode(this.txtSignImage.Text.Trim());
            if (string.IsNullOrEmpty(this.SignManageId))
            {
                this.SignManageId = SQLHelper.GetNewID(typeof(Model.Resources_SignManage));
                newSignManage.SignManageId = this.SignManageId;
                BLL.SignManageService.AddSignManage(newSignManage);
                BLL.LogService.AddSys_Log(this.CurrUser, newSignManage.SignCode, newSignManage.SignManageId,BLL.Const.SignManageMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                newSignManage.SignManageId = this.SignManageId;
                BLL.SignManageService.UpdateSignManage(newSignManage);
                BLL.LogService.AddSys_Log(this.CurrUser, newSignManage.SignCode, newSignManage.SignManageId, BLL.Const.SignManageMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SignManageAttachUrl&type=-1", this.SignManageId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.SignManageId))
                {
                    this.SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SignManageAttachUrl&menuId={1}", this.SignManageId, BLL.Const.SignManageMenuId)));
            }
        }
        #endregion
    }
}