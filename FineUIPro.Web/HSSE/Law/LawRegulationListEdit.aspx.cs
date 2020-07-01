using BLL;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Law
{
    public partial class LawRegulationListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 法律法规主键
        /// </summary>
        public string LawRegulationId
        {
            get
            {
                return (string)ViewState["LawRegulationId"];
            }
            set
            {
                ViewState["LawRegulationId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //权限设置
                this.GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                //加载法律法规类别下拉选项
                this.ddlLawsRegulationsTypeId.DataTextField = "Name";
                this.ddlLawsRegulationsTypeId.DataValueField = "Id";
                this.ddlLawsRegulationsTypeId.DataSource = BLL.LawsRegulationsTypeService.GetLawsRegulationsTypeList();
                ddlLawsRegulationsTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlLawsRegulationsTypeId);

                this.LawRegulationId = Request.Params["LawRegulationId"];
                if (!string.IsNullOrEmpty(this.LawRegulationId))
                {
                    var lawRegulation = BLL.LawRegulationListService.GetViewLawRegulationListById(this.LawRegulationId);
                    if (lawRegulation != null)
                    {
                        this.txtLawRegulationCode.Text = lawRegulation.LawRegulationCode;
                        this.txtLawRegulationName.Text = lawRegulation.LawRegulationName;
                        if (lawRegulation.ApprovalDate.HasValue)
                        {
                            this.dpkApprovalDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulation.ApprovalDate);
                        }
                        if (lawRegulation.EffectiveDate.HasValue)
                        {
                            this.dpkEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulation.EffectiveDate);
                        }
                        this.txtDescription.Text = lawRegulation.Description;
                        if (!string.IsNullOrEmpty(lawRegulation.LawsRegulationsTypeId))
                        {
                            this.ddlLawsRegulationsTypeId.SelectedValue = lawRegulation.LawsRegulationsTypeId;
                        }
                        //if (!string.IsNullOrEmpty(lawRegulation.AttachUrl))
                        //{
                        //    this.FullAttachUrl = lawRegulation.AttachUrl;
                        //    this.lbAttachUrl.Text = lawRegulation.AttachUrl.Substring(lawRegulation.AttachUrl.IndexOf("~") + 1);
                        //}
                        this.txtCompileMan.Text = lawRegulation.CompileMan;
                        if (lawRegulation.CompileDate.HasValue)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulation.CompileDate);
                        }
                    }
                }
                else
                {
                    this.txtCompileMan.Text = this.CurrUser.UserName;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                }
            }
        }

        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData( bool isClose)
        {
            Model.Law_LawRegulationList lawRegulationList = new Model.Law_LawRegulationList
            {
                LawRegulationCode = this.txtLawRegulationCode.Text.Trim(),
                LawRegulationName = this.txtLawRegulationName.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkApprovalDate.Text.Trim()))
            {
                lawRegulationList.ApprovalDate = Convert.ToDateTime(this.dpkApprovalDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.dpkEffectiveDate.Text.Trim()))
            {
                lawRegulationList.EffectiveDate = Convert.ToDateTime(this.dpkEffectiveDate.Text.Trim());
            }
            lawRegulationList.Description = this.txtDescription.Text.Trim();
            //lawRegulationList.AttachUrl = this.FullAttachUrl;
            if (this.ddlLawsRegulationsTypeId.SelectedValue != BLL.Const._Null)
            {
                lawRegulationList.LawsRegulationsTypeId = this.ddlLawsRegulationsTypeId.SelectedValue;
            }
            if (string.IsNullOrEmpty(this.LawRegulationId))
            {
                lawRegulationList.IsPass = true;
                lawRegulationList.CompileMan = this.CurrUser.UserName;
                lawRegulationList.CompileDate = System.DateTime.Now;
                lawRegulationList.UnitId = this.CurrUser.UnitId;
                this.LawRegulationId = SQLHelper.GetNewID(typeof(Model.Law_LawRegulationList));
                lawRegulationList.LawRegulationId = this.LawRegulationId;
                BLL.LawRegulationListService.AddLawRegulationList(lawRegulationList);
                BLL.LogService.AddSys_Log(this.CurrUser, lawRegulationList.LawRegulationCode, lawRegulationList.LawRegulationId, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                lawRegulationList.LawRegulationId = this.LawRegulationId;
                BLL.LawRegulationListService.UpdateLawRegulationList(lawRegulationList);
                BLL.LogService.AddSys_Log(this.CurrUser, lawRegulationList.LawRegulationCode, lawRegulationList.LawRegulationId, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        #region 权限设置
        /// <summary>
        /// 权限按钮设置
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.LawRegulationListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证法律法规名称是否存在
        /// <summary>
        /// 验证法律法规名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var lawRegulation = Funs.DB.Law_LawRegulationList.FirstOrDefault(x => x.IsPass == true && x.LawRegulationName == this.txtLawRegulationName.Text.Trim() && (x.LawRegulationId != this.LawRegulationId || (this.LawRegulationId == null && x.LawRegulationId != null)));
            if (lawRegulation != null)
            {
                ShowNotify("输入的法律法规名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.LawRegulationId))
            {
                SaveData(false);
            }
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LawRegulation&type=-1", LawRegulationId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LawRegulation&menuId=F4B02718-0616-4623-ABCE-885698DDBEB1", LawRegulationId)));
            }
        }
        #endregion
    }
}