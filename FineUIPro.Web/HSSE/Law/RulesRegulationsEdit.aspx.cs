using BLL;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Law
{
    public partial class RulesRegulationsEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string RulesRegulationsId
        {
            get
            {
                return (string)ViewState["RulesRegulationsId"];
            }
            set
            {
                ViewState["RulesRegulationsId"] = value;
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
                this.GetButtonPower();
                LoadData();

                //加载规章制度类别下拉选项
                this.ddlRulesRegulationsTypeId.DataTextField = "RulesRegulationsTypeName";
                this.ddlRulesRegulationsTypeId.DataValueField = "RulesRegulationsTypeId";
                this.ddlRulesRegulationsTypeId.DataSource = BLL.RulesRegulationsTypeService.GetRulesRegulationsTypeList();
                ddlRulesRegulationsTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlRulesRegulationsTypeId);

                this.RulesRegulationsId = Request.Params["RulesRegulationsId"];
                if (!string.IsNullOrEmpty(this.RulesRegulationsId))
                {
                    var rulesRegulation = BLL.RulesRegulationsService.GetRulesRegulationsById(this.RulesRegulationsId);
                    if (rulesRegulation != null)
                    {
                        this.txtRulesRegulationsCode.Text = rulesRegulation.RulesRegulationsCode;
                        this.txtRulesRegulationsName.Text = rulesRegulation.RulesRegulationsName;
                        if (!string.IsNullOrEmpty(rulesRegulation.RulesRegulationsTypeId))
                        {
                            this.ddlRulesRegulationsTypeId.SelectedValue = rulesRegulation.RulesRegulationsTypeId;
                        }
                        if (rulesRegulation.CustomDate != null)
                        {
                            this.dpkCustomDate.Text = string.Format("{0:yyyy-MM-dd}", rulesRegulation.CustomDate);
                        }
                        this.txtApplicableScope.Text = rulesRegulation.ApplicableScope;
                        //if (!string.IsNullOrEmpty(rulesRegulation.AttachUrl))
                        //{
                        //    this.FullAttachUrl = rulesRegulation.AttachUrl;
                        //    this.lbAttachUrl.Text = rulesRegulation.AttachUrl.Substring(rulesRegulation.AttachUrl.IndexOf("~") + 1);
                        //}
                        this.txtRemark.Text = rulesRegulation.Remark;
                    }
                }
               
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
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
        private void SaveData(bool isClose)
        {
            Model.Law_RulesRegulations rulesRegulations = new Model.Law_RulesRegulations
            {
                RulesRegulationsCode = this.txtRulesRegulationsCode.Text.Trim(),
                RulesRegulationsName = this.txtRulesRegulationsName.Text.Trim()
            };
            if (this.ddlRulesRegulationsTypeId.SelectedValue != BLL.Const._Null)
            {
                rulesRegulations.RulesRegulationsTypeId = this.ddlRulesRegulationsTypeId.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.dpkCustomDate.Text.Trim()))
            {
                rulesRegulations.CustomDate = Convert.ToDateTime(this.dpkCustomDate.Text.Trim());
            }
            rulesRegulations.ApplicableScope = this.txtApplicableScope.Text.Trim();
            rulesRegulations.Remark = this.txtRemark.Text.Trim();
            //rulesRegulations.AttachUrl = this.FullAttachUrl;
            if (string.IsNullOrEmpty(this.RulesRegulationsId))
            {
                rulesRegulations.IsPass = true;
                rulesRegulations.CompileMan = this.CurrUser.UserName;
                rulesRegulations.CompileDate = System.DateTime.Now;
                rulesRegulations.UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId;
                this.RulesRegulationsId = SQLHelper.GetNewID();
                rulesRegulations.RulesRegulationsId = this.RulesRegulationsId;
                BLL.RulesRegulationsService.AddRulesRegulations(rulesRegulations);
                BLL.LogService.AddSys_Log(this.CurrUser, rulesRegulations.RulesRegulationsCode, rulesRegulations.RulesRegulationsId, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                rulesRegulations.RulesRegulationsId = this.RulesRegulationsId;
                BLL.RulesRegulationsService.UpdateRulesRegulations(rulesRegulations);
                BLL.LogService.AddSys_Log(this.CurrUser, rulesRegulations.RulesRegulationsCode, rulesRegulations.RulesRegulationsId, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RulesRegulationsMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证规章制度名称是否存在
        /// <summary>
        /// 验证规章制度名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Law_RulesRegulations.FirstOrDefault(x => x.IsPass == true && x.RulesRegulationsName == this.txtRulesRegulationsName.Text.Trim() && (x.RulesRegulationsId != this.RulesRegulationsId || (this.RulesRegulationsId == null && x.RulesRegulationsId != null)));
            if (standard != null)
            {
                ShowNotify("输入的规章名称已存在！", MessageBoxIcon.Warning);
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RulesRegulations&type=-1", RulesRegulationsId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.RulesRegulationsId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RulesRegulations&menuId={1}", RulesRegulationsId, BLL.Const.RulesRegulationsMenuId)));
            }
        }
        #endregion
    }
}