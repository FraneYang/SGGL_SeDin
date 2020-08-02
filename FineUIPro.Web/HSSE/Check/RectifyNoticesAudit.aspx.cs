using BLL;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class RectifyNoticesAudit : PageBase
    {
        #region 定义项
        public string RectifyNoticesId
        {
            get
            {
                return (string)ViewState["RectifyNoticesId"];
            }
            set
            {
                ViewState["RectifyNoticesId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                RectifyNoticesId = Request.Params["RectifyNoticesId"];
                /// 专业工程师
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProfessionalEngineer, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                /// 施工经理
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                /// 项目经理
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProjectManager, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                if (getRectifyNotices != null)
                {
                    this.txtUnit.Text = UnitService.GetUnitNameByUnitId(getRectifyNotices.UnitId);
                    this.txtWorkArea.Text = UnitWorkService.GetUnitWorkName(getRectifyNotices.WorkAreaId);
                    this.txtCheckMan.Text = UserService.getUserNamesUserIds(getRectifyNotices.CheckManIds);
                    this.txtCheckPerson.Text = getRectifyNotices.CheckManNames;
                    this.txtRectifyNoticesCode.Text = getRectifyNotices.RectifyNoticesCode;
                    this.txtCheckedDate.Text = getRectifyNotices.CheckedDate.ToString();
                    this.txtHiddenHazardType.Text = "一般";
                    if (getRectifyNotices.HiddenHazardType == "2")
                    {
                        this.txtHiddenHazardType.Text = "较大";
                    }
                    else if (getRectifyNotices.HiddenHazardType == "3")
                    {
                        this.txtHiddenHazardType.Text = "重大";
                    }

                    getControlShow();                   
                }
                BindGrid();
                BindGrid1();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.RectifyNoticesId))
            {
                string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults
                            ,IsRectify,(CASE WHEN IsRectify=1 THEN '合格' WHEN IsRectify=0 THEN '不合格' ELSE '' END) AS IsRectifyName
                        from [dbo].[Check_RectifyNoticesItem] ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += "where RectifyNoticesId = @RectifyNoticesId";
                listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void BindGrid1()
        {
            if (!string.IsNullOrEmpty(this.RectifyNoticesId))
            {
                string strSql = @"select FlowOperateId, RectifyNoticesId, OperateName, OperateManId, OperateTime, IsAgree, Opinion,S.UserName
                ,(case when IsAgree=0 then '不同意' else '同意' end) as IsAgreeName
                from Check_RectifyNoticesFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += "where RectifyNoticesId= @RectifyNoticesId";
                listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                var table = this.GetPagedDataTable(gvFlowOperate, tb);
                gvFlowOperate.DataSource = table;
                gvFlowOperate.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
        }
        
        #region 提交方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveRectifyNotices();
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="saveType"></param>
        private void SaveRectifyNotices()
        {
            if (this.drpOperateMan.SelectedValue != Const._Null)
            {
                var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(this.RectifyNoticesId);
                if (getRectifyNotices != null)
                {
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(),
                        RectifyNoticesId = getRectifyNotices.RectifyNoticesId,
                        OperateManId = this.CurrUser.UserId,
                        OperateTime = DateTime.Now,
                        IsAgree = (this.rdbIsAgree.SelectedValue == "false" ? false : true),
                        Opinion = this.txtOpinion.Text.Trim(),
                    };
                    ////签发
                    if (getRectifyNotices.States == Const.State_1)
                    {
                        if (this.drpProfessionalEngineer.SelectedValue == Const._Null && newOItem.IsAgree ==true)
                        {
                            Alert.ShowInParent("请选择抄送专业工程师！", MessageBoxIcon.Warning);
                            return;
                        }
                        newOItem.OperateName = "施工单位项目安全经理签发";
                        getRectifyNotices.DutyPersonId = this.drpOperateMan.SelectedValue;
                        if (newOItem.IsAgree == true)
                        {
                            getRectifyNotices.States = Const.State_2;
                          
                            getRectifyNotices.SignDate = DateTime.Now;
                            if (this.drpProfessionalEngineer.SelectedValue != Const._Null)
                            {
                                getRectifyNotices.ProfessionalEngineerId = this.drpProfessionalEngineer.SelectedValue;
                            }
                            if (this.drpConstructionManager.SelectedValue != Const._Null)
                            {
                                getRectifyNotices.ConstructionManagerId = this.drpConstructionManager.SelectedValue;
                            }
                            if (this.drpProjectManager.SelectedValue != Const._Null)
                            {
                                getRectifyNotices.ProjectManagerId = this.drpProjectManager.SelectedValue;
                            }
                        }
                        else
                        {
                            getRectifyNotices.States = Const.State_0;
                        }
                    }
                    else if (getRectifyNotices.States == Const.State_3)
                    {
                        newOItem.OperateName = "施工单位项目负责人审核";
                        if (newOItem.IsAgree == true)
                        {
                            getRectifyNotices.States = Const.State_4;
                            getRectifyNotices.UnitHeadManDate = DateTime.Now;
                            getRectifyNotices.CheckPerson = this.drpOperateMan.SelectedValue;
                        }
                        else
                        {
                            getRectifyNotices.States = Const.State_2;
                            getRectifyNotices.CompleteDate = null;
                        }
                    }

                    Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    Funs.DB.SubmitChanges();

                    ShowNotify("提交成功！", MessageBoxIcon.Success);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
            else
            {
                Alert.ShowInParent("请选择" + this.drpOperateMan.Label + "！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 整改图片
        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object RectifyNoticesItemId)
        {
            string url = string.Empty;
            if (RectifyNoticesItemId != null)
            {
                var RectifyNoticesItem = BLL.AttachFileService.GetAttachFile(RectifyNoticesItemId.ToString() + "#1", BLL.Const.ProjectRectifyNoticesMenuId);
                if (RectifyNoticesItem != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../../", RectifyNoticesItem.AttachUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrlByImage(object RectifyNoticesItemId)
        {
            string url = string.Empty;
            if (RectifyNoticesItemId != null)
            {
                var RectifyNoticesItem = BLL.AttachFileService.GetAttachFile(RectifyNoticesItemId.ToString() + "#2", BLL.Const.ProjectRectifyNoticesMenuId);
                if (RectifyNoticesItem != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../../", RectifyNoticesItem.AttachUrl);
                }
            }
            return url;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            getControlShow();            
        }

        /// <summary>
        /// 
        /// </summary>
        private void getControlShow()
        {
            this.drpOperateMan.Items.Clear();
            this.step1_person2.Hidden = true;
            this.trOpinion.Hidden = true;
            var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
            if (getRectifyNotices != null)
            {
                if (getRectifyNotices.States == Const.State_1)
                {                  
                    this.Grid1.Columns[4].Hidden = true;
                    this.Grid1.Columns[5].Hidden = true;
                    this.Grid1.Columns[6].Hidden = true;
                }

                if (this.rdbIsAgree.SelectedValue == "true")
                {
                    this.drpOperateMan.Hidden = false;
                    this.drpOperateMan.Readonly = false;
                    if (getRectifyNotices.States == Const.State_1)
                    {
                        this.drpOperateMan.Label = "接收人";
                        UserService.InitUserProjectIdUnitIdDropDownList(this.drpOperateMan, this.CurrUser.LoginProjectId, getRectifyNotices.UnitId, true);
                        this.drpOperateMan.SelectedValue = getRectifyNotices.DutyPersonId;
                        this.step1_person2.Hidden = false;
                    }
                    else if (getRectifyNotices.States == Const.State_3)
                    {
                        this.drpOperateMan.Label = "安全经理/安全工程师";
                        UserService.InitUserProjectIdUnitIdDropDownList(this.drpOperateMan, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                        this.drpOperateMan.SelectedValue = getRectifyNotices.CompleteManId;
                    }
                }
                else
                {
                    this.trOpinion.Hidden = false;
                    this.drpOperateMan.Readonly = true;
                    if (getRectifyNotices.States == Const.State_1)
                    {
                        this.drpOperateMan.Label = "打回检查人";
                        UserService.InitFlowOperateControlUserDropDownList(this.drpOperateMan, this.CurrUser.LoginProjectId, null, true);
                        this.drpOperateMan.SelectedValue = getRectifyNotices.CompleteManId;                        
                    }
                    else if (getRectifyNotices.States == Const.State_3)
                    {
                        this.drpOperateMan.Label = "打回整改责任人";
                        UserService.InitUserProjectIdUnitIdDropDownList(this.drpOperateMan, this.CurrUser.LoginProjectId, getRectifyNotices.UnitId, true);
                        this.drpOperateMan.SelectedValue = getRectifyNotices.DutyPersonId;
                    }
                }
            }
        }
    }
}