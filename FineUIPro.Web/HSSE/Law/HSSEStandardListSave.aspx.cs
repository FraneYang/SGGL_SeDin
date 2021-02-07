using BLL;
using Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Law
{
    public partial class HSSEStandardListSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string StandardId
        {
            get
            {
                return (string)ViewState["StandardId"];
            }
            set
            {
                ViewState["StandardId"] = value;
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
                this.GetButtonPower();//权限设置
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.HSSEStandardListTypeService.InitStandardListTypeDropDownList(this.drpType, true);
                ConstValue.InitConstValueDropDownList(this.drpIndexesIds, ConstValue.Group_HSSE_Indexes, false);
                ConstValue.InitConstValueDropDownList(this.drpReleaseStates, ConstValue.Group_HSSE_ReleaseStates, false);
                StandardId = Request.QueryString["StandardId"];
                if (!String.IsNullOrEmpty(StandardId))
                {
                    var q = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(StandardId);
                    if (q != null)
                    {
                        this.txtStandardNo.Text = q.StandardNo;
                        this.txtStandardName.Text = q.StandardName;
                        this.drpType.SelectedValue = q.TypeId;
                        this.drpReleaseStates.SelectedValue = q.ReleaseStates;                    
                        this.txtReleaseUnit.Text = q.ReleaseUnit;
                        this.dpkApprovalDate.Text = string.Format("{0:yyyy-MM-dd}", q.ApprovalDate);
                        this.dpkEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", q.EffectiveDate);
                        this.txtAbolitionDate.Text = string.Format("{0:yyyy-MM-dd}", q.AbolitionDate);
                        this.txtReplaceInfo.Text = q.ReplaceInfo;
                        this.txtDescription.Text = q.Description;
                        if (!string.IsNullOrEmpty(q.IndexesIds))
                        {
                            this.drpIndexesIds.SelectedValueArray = q.IndexesIds.Split(',');
                        }
                        this.txtCompileMan.Text = q.CompileMan;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                    }
                }
                else
                {
                    txtCompileMan.Text = this.CurrUser.UserName;
                    txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="upState"></param>
        private void SaveData( bool isClose)
        {
            Model.Law_HSSEStandardsList hSSEStandardsList = new Law_HSSEStandardsList
            {
                StandardNo = txtStandardNo.Text.Trim(),
                StandardName = txtStandardName.Text.Trim(),
                ApprovalDate = Funs.GetNewDateTime(this.dpkApprovalDate.Text.Trim()),
                EffectiveDate = Funs.GetNewDateTime(this.dpkEffectiveDate.Text.Trim()),
                ReleaseUnit = this.txtReleaseUnit.Text.Trim(),
                AbolitionDate = Funs.GetNewDateTime(this.txtAbolitionDate.Text.Trim()),
                ReplaceInfo = this.txtReplaceInfo.Text.Trim(),
                Description = this.txtDescription.Text.Trim(),
            };
            if (drpType.SelectedValue != BLL.Const._Null)
            {
                hSSEStandardsList.TypeId = drpType.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.drpReleaseStates.SelectedValue))
            {
                hSSEStandardsList.ReleaseStates = this.drpReleaseStates.SelectedValue;
            }

            hSSEStandardsList.IndexesIds = Funs.GetStringByArray(this.drpIndexesIds.SelectedValueArray);
            if (string.IsNullOrEmpty(this.StandardId))
            {
                hSSEStandardsList.IsPass = true;
                hSSEStandardsList.CompileMan = this.CurrUser.UserName;
                hSSEStandardsList.UnitId = string.IsNullOrEmpty(this.CurrUser.UnitId) ? Const.UnitId_SEDIN : this.CurrUser.UnitId;
                hSSEStandardsList.CompileDate = System.DateTime.Now;
                this.StandardId = SQLHelper.GetNewID(typeof(Model.Law_HSSEStandardsList));
                hSSEStandardsList.StandardId = this.StandardId;
                BLL.HSSEStandardsListService.AddHSSEStandardsList(hSSEStandardsList);
                BLL.LogService.AddSys_Log(this.CurrUser, hSSEStandardsList.StandardNo, hSSEStandardsList.StandardId, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                hSSEStandardsList.StandardId = this.StandardId;
                BLL.HSSEStandardsListService.UpdateHSSEStandardsList(hSSEStandardsList);
                BLL.LogService.AddSys_Log(this.CurrUser, hSSEStandardsList.StandardNo, hSSEStandardsList.StandardId, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion
        
        #region 验证标准规范名称是否存在
        /// <summary>
        /// 验证标准规范名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Law_HSSEStandardsList.FirstOrDefault(x => x.IsPass == true && x.StandardName == this.txtStandardName.Text.Trim() && (x.StandardId != this.StandardId || (this.StandardId == null && x.StandardId != null)));
            if (standard != null)
            {
                ShowNotify("输入的标准名称已存在！", MessageBoxIcon.Warning);
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
            if (string.IsNullOrEmpty(this.StandardId))
            {
                SaveData(false);
            }
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSEStandardsList&type=-1", StandardId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSEStandardsList&menuId=EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL", StandardId)));
            }            
        }
        #endregion

        #region 权限设置
        /// <summary>
        /// 权限设置
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEStandardListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}