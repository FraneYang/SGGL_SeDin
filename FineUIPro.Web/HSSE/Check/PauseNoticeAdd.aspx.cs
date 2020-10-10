using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class PauseNoticeAdd : PageBase
    {
        #region  定义项
        /// <summary>
        /// 工程暂停令主键
        /// </summary>
        public string PauseNoticeId
        {
            get
            {
                return (string)ViewState["PauseNoticeId"];
            }
            set
            {
                ViewState["PauseNoticeId"] = value;
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
        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.PauseNoticeId = Request.Params["PauseNoticeId"];
                this.InitDropDownList();
                BLL.UserService.InitUserProjectIdUnitIdRoleIdDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN,Const.ConstructionManager, true);

                if (!string.IsNullOrEmpty(PauseNoticeId))
                {
                    BindGrid();
                    Model.Check_PauseNotice pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
                    if (pauseNotice != null)
                    {
                        this.ProjectId = pauseNotice.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PauseNoticeId);
                        if (!string.IsNullOrEmpty(pauseNotice.UnitId))
                        {
                            this.drpUnit.SelectedValue = pauseNotice.UnitId;
                        }
                        //this.txtProjectPlace.Text = pauseNotice.ProjectPlace;
                        if (!string.IsNullOrEmpty(pauseNotice.UnitWorkId))
                        {
                            this.drpUnitWork.SelectedValue = pauseNotice.UnitWorkId;
                        }
                        this.txtWrongContent.Text = pauseNotice.WrongContent;
                        if (pauseNotice.PauseTime.HasValue)
                        {
                            this.txtPauseTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", pauseNotice.PauseTime);
                        }
                        this.drpSignPerson.SelectedValue = pauseNotice.SignManId;
                        this.AttachUrl = pauseNotice.AttachUrl;

                    }
                }
            }
        }
        public void BindGrid()
        {
            string strSql = @"select FlowOperateId, PauseNoticeId, OperateName, OperateManId, OperateTime, case when IsAgree='False' then '否' else '是' end  As IsAgree, Opinion,S.UserName from Check_PauseNoticeFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where PauseNoticeId= @PauseNoticeId";
            listStr.Add(new SqlParameter("@PauseNoticeId", PauseNoticeId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.ProjectId, Const.ProjectUnitType_2, true);
            UnitWorkService.InitUnitWorkDropDownList(this.drpUnitWork, this.ProjectId,  true);
        }

        #region  单位变化事件
        /// <summary>
        /// 单位变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPauseNoticeMenuId, this.ProjectId, this.drpUnit.SelectedValue);
            }
            else
            {
                this.txtPauseNoticeCode.Text = string.Empty;
            }
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SavePauseNotice(BLL.Const.BtnSubmit);
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {           
            this.SavePauseNotice(BLL.Const.BtnSave);
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SavePauseNotice(string type)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检单位！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitWork.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位工程！", MessageBoxIcon.Warning);
                return;
            }

            Model.Check_PauseNotice pauseNotice = new Model.Check_PauseNotice
            {
                PauseNoticeCode = this.txtPauseNoticeCode.Text.Trim(),
                ProjectId = this.ProjectId,

                UnitId = this.drpUnit.SelectedValue,
                UnitWorkId = this.drpUnitWork.SelectedValue,
                WrongContent = this.txtWrongContent.Text.Trim(),
                PauseTime = Funs.GetNewDateTime(this.txtPauseTime.Text.Trim())
            };
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                pauseNotice.SignManId = this.drpSignPerson.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("总包施工经理不能为空！", MessageBoxIcon.Warning);
                return;
            }
            pauseNotice.IsConfirm = false;
            pauseNotice.AttachUrl = this.AttachUrl;
            if (type == BLL.Const.BtnSubmit)
            {
                pauseNotice.PauseStates = "1";
            }
            else
            {
                pauseNotice.PauseStates = "0";
            }
            Model.Check_PauseNotice isUpdate = Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
            if (isUpdate != null)
            {
                isUpdate.UnitId = this.drpUnit.SelectedValue;
                isUpdate.UnitWorkId = this.drpUnitWork.SelectedValue;
                isUpdate.WrongContent = this.txtWrongContent.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtPauseTime.Text.Trim()))
                {
                    isUpdate.PauseTime = Funs.GetNewDateTimeOrNow(this.txtPauseTime.Text.Trim());
                }
                isUpdate.SignManId = pauseNotice.SignManId;
                isUpdate.PauseStates = pauseNotice.PauseStates;
                Funs.DB.SubmitChanges();
                if (isUpdate.PauseStates == "1")
                {
                    SaveData("总包安全工程师/安全经理下发暂停令");
                }
            }

            else
            {
                pauseNotice.States = "0";
                if (string.IsNullOrEmpty(this.PauseNoticeId))
                {
                    pauseNotice.PauseNoticeId = SQLHelper.GetNewID(typeof(Model.Check_PauseNotice));
                }
                else
                {
                    pauseNotice.PauseNoticeId = this.PauseNoticeId;
                }
                pauseNotice.CompileManId = this.CurrUser.UserId;
                pauseNotice.CompileDate = DateTime.Now;
                if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
                {
                    pauseNotice.SignManId = drpSignPerson.SelectedValue;
                }

                BLL.Check_PauseNoticeService.AddPauseNotice(pauseNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, pauseNotice.PauseNoticeCode, pauseNotice.PauseNoticeId, BLL.Const.ProjectPauseNoticeMenuId, BLL.Const.BtnAdd);
                this.PauseNoticeId = pauseNotice.PauseNoticeId;
                if (pauseNotice.PauseStates == "1")
                {
                    SaveData("总包安全工程师/安全经理下发暂停令");
                }
            }
            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoticeUrl_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.PauseNoticeId))
            {
                this.PauseNoticeId = SQLHelper.GetNewID(typeof(Model.Check_PauseNotice));
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPauseNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, PauseNoticeId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, PauseNoticeId)));
            }
        }

        #region 保存流程审核数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void SaveData(string OperateName)
        {
            Model.Check_PauseNoticeFlowOperate newFlowOperate = new Model.Check_PauseNoticeFlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_PauseNoticeFlowOperate)),
                PauseNoticeId = PauseNoticeId,
                OperateName = OperateName,
                OperateManId = CurrUser.UserId,
                OperateTime = DateTime.Now,
                IsAgree = true
            };
            Funs.DB.Check_PauseNoticeFlowOperate.InsertOnSubmit(newFlowOperate);
            Funs.DB.SubmitChanges();

        }
        #endregion

    }
}