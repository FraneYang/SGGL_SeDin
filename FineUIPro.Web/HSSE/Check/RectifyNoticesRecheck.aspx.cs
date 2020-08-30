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
    public partial class RectifyNoticesRecheck : PageBase
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
                var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                if (getRectifyNotices != null)
                {
                    this.txtUnit.Text = UnitService.GetUnitNameByUnitId(getRectifyNotices.UnitId);
                    this.txtWorkArea.Text = UnitWorkService.GetUnitWorkName(getRectifyNotices.WorkAreaId);
                    this.txtCheckMan.Text = UserService.getUserNamesUserIds(getRectifyNotices.CheckManIds);
                    this.txtCheckPerson.Text = getRectifyNotices.CheckManNames;
                    this.txtRectifyNoticesCode.Text = getRectifyNotices.RectifyNoticesCode;
                    this.txtCheckedDate.Text = getRectifyNotices.CheckedDate.ToString();
                    this.txtReCheckOpinion.Text = getRectifyNotices.ReCheckOpinion;
                    this.txtHiddenHazardType.Text = "一般";
                    if (getRectifyNotices.HiddenHazardType == "2")
                    {
                        this.txtHiddenHazardType.Text = "较大";
                    }
                    else if (getRectifyNotices.HiddenHazardType == "3")
                    {
                        this.txtHiddenHazardType.Text = "重大";
                    }

                    if (!string.IsNullOrEmpty(getRectifyNotices.ProfessionalEngineerId))
                    {
                        this.drpProfessionalEngineer.Text = UserService.GetUserNameByUserId(getRectifyNotices.ProfessionalEngineerId);
                    }
                    if (!string.IsNullOrEmpty(getRectifyNotices.ConstructionManagerId))
                    {
                        this.drpConstructionManager.Text = UserService.GetUserNameByUserId(getRectifyNotices.ConstructionManagerId);
                    }
                    if (!string.IsNullOrEmpty(getRectifyNotices.ProjectManagerId))
                    {
                        this.drpProjectManager.Text = UserService.GetUserNameByUserId(getRectifyNotices.ProjectManagerId);
                    }
                    this.lbDutyPerson.Text = UserService.GetUserNameByUserId(getRectifyNotices.DutyPersonId);
                    if (Funs.DB.Check_RectifyNoticesItem.FirstOrDefault(x => !x.IsRectify.HasValue || x.IsRectify == false) == null)
                    {
                        this.rdbIsAgree.SelectedValue = "true";
                    }
                    else
                    {
                        this.rdbIsAgree.SelectedValue = "false";
                    }
                    getControlShow();
                }
                BindGrid();
                BindGrid1();
            }
            else
            {
                string arg = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                if (arg == "UPDATE_SUMMARY")
                {
                    getControlShow();
                }
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
                
        #region 数据保存提交方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRectifyNotices(Const.BtnSave);
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveRectifyNotices(Const.BtnSubmit);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="saveType"></param>
        private void SaveRectifyNotices(string saveType)
        {
            var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(this.RectifyNoticesId);
            if (getRectifyNotices != null)
            {
                var getAllItem = from x in Funs.DB.Check_RectifyNoticesItem
                                 where x.RectifyNoticesId == getRectifyNotices.RectifyNoticesId
                                 select x;
                if (getAllItem.Count() > 0)
                {
                    foreach (JObject mergedRow in Grid1.GetModifiedData())
                    {
                        JObject values = mergedRow.Value<JObject>("values");
                        string rectifyNoticesItemId = mergedRow.Value<string>("id");
                        string isRectify = values.Value<string>("IsRectify");
                        var getUpdateItem = getAllItem.FirstOrDefault(x => x.RectifyNoticesItemId == rectifyNoticesItemId);
                        if (getUpdateItem != null)
                        {
                            if (isRectify == "合格")
                            {
                                getUpdateItem.IsRectify = true;
                            }
                            else
                            {
                                getUpdateItem.IsRectify = false;
                            }
                            Funs.DB.SubmitChanges();
                        }
                    }
                }

                getRectifyNotices.ReCheckOpinion = this.txtReCheckOpinion.Text;
                getRectifyNotices.ReCheckDate = DateTime.Now;
                if (saveType == Const.BtnSubmit)
                {
                    bool isAgree = false;
                    if (getAllItem.FirstOrDefault(x => !x.IsRectify.HasValue || x.IsRectify == false) == null)
                    {
                        isAgree = true;
                    }
                    if (isAgree)
                    {
                        getRectifyNotices.States = Const.State_5;
                        getRectifyNotices.ReCheckDate = DateTime.Now;                        
                        //// 回写专项检查明细表                            
                        var getcheck = Funs.DB.Check_CheckSpecialDetail.Where(x => x.DataId.Contains(getRectifyNotices.RectifyNoticesId));
                        if (getcheck.Count() > 0)
                        {
                            foreach (var item in getcheck)
                            {
                                item.CompleteStatus = true;
                                item.CompletedDate = DateTime.Now;
                                Funs.DB.SubmitChanges();
                            }
                            //// 根据明细ID判断是否全部整改完成 并更新专项检查状态
                            Check_CheckSpecialService.UpdateCheckSpecialStates(getcheck.FirstOrDefault().CheckSpecialId);
                        }
                    }
                    else
                    {
                        getRectifyNotices.States = Const.State_2;
                        getRectifyNotices.UnitHeadManDate = null;
                        getRectifyNotices.CompleteDate = null;
                        getRectifyNotices.ProfessionalEngineerTime2 = null;
                        getRectifyNotices.ConstructionManagerTime2 = null;
                        getRectifyNotices.ProjectManagerTime2 = null;
                    }

                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(),
                        RectifyNoticesId = getRectifyNotices.RectifyNoticesId,
                        OperateManId = this.CurrUser.UserId,
                        OperateTime = DateTime.Now,
                        OperateName = "安全经理/安全工程师复查",
                        IsAgree = isAgree,
                        Opinion = this.txtReCheckOpinion.Text.Trim(),
                    };
                    Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);                  
                }

                Funs.DB.SubmitChanges();

                ShowNotify(saveType + "成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
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
            if (this.rdbIsAgree.SelectedValue == "true")
            {
                this.step1_person2.Hidden = false;
                this.lbDutyPerson.Hidden = true;
            }
            else
            {
                this.step1_person2.Hidden = true;
                this.lbDutyPerson.Hidden = false;
            }
            this.lbCom.Hidden = !this.lbDutyPerson.Hidden;
        }
    }
}