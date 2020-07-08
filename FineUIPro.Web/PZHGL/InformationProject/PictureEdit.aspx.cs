using BLL;
using System;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class PictureEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PictureId
        {
            get
            {
                return (string)ViewState["PictureId"];
            }
            set
            {
                ViewState["PictureId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;              
                this.PictureId = Request.Params["PictureId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                if (!string.IsNullOrEmpty(this.PictureId))
                {
                    Model.InformationProject_Picture picture = BLL.PictureService.GetPictureById(this.PictureId);
                    if (picture != null)
                    {
                        this.ProjectId = picture.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PictureId);
                        this.txtTitle.Text = picture.Title;
                        this.txtContentDef.Text = picture.ContentDef;
                        this.drpPictureType.SelectedValue = picture.PictureType;
                        this.txtUploadDate.Text = string.Format("{0:yyyy-MM-dd}", picture.UploadDate);
                        if (!string.IsNullOrEmpty(picture.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = picture.CompileMan;
                        }
                    }                  
                }
                else
                {
                    ////自动生成编码
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPictureMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtTitle.Text = this.SimpleForm1.Title;
                    this.txtUploadDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectPictureMenuId;
                this.ctlAuditFlow.DataId = this.PictureId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
            PictureTypeService.InitPictureTypeDropDownList(this.drpPictureType, false);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InformationProject_Picture picture = new Model.InformationProject_Picture
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                ContentDef = this.txtContentDef.Text.Trim()
            };
            if (this.drpPictureType.SelectedValue != Const._Null)
            {
                picture.PictureType = this.drpPictureType.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                picture.CompileMan = this.drpCompileMan.SelectedValue;
            }
            picture.UploadDate = Funs.GetNewDateTime(this.txtUploadDate.Text.Trim());
            picture.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                picture.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.PictureId))
            {
                picture.PictureId = this.PictureId;
                BLL.PictureService.UpdatePicture(picture);
                BLL.LogService.AddSys_Log(this.CurrUser, picture.Title, picture.PictureId, BLL.Const.ProjectPictureMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.PictureId = SQLHelper.GetNewID(typeof(Model.InformationProject_Picture));
                picture.PictureId = this.PictureId;
                BLL.PictureService.AddPicture(picture);
                BLL.LogService.AddSys_Log(this.CurrUser, picture.Title, picture.PictureId, BLL.Const.ProjectPictureMenuId, BLL.Const.BtnAdd);
            }

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectPictureMenuId, this.PictureId, (type == Const.BtnSubmit ? true : false), picture.Title, "../InformationProject/PictureView.aspx?PictureId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PictureId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PictureAttachUrl&menuId={1}", this.PictureId, BLL.Const.ProjectPictureMenuId)));
        }
        #endregion
    }
}