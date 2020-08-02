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
    public partial class RectifyNoticesRectify : PageBase
    {
        #region 定义项
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
                    this.txtHiddenHazardType.Text = "一般";
                    if (getRectifyNotices.HiddenHazardType == "2")
                    {
                        this.txtHiddenHazardType.Text = "较大";
                    }
                    else if (getRectifyNotices.HiddenHazardType == "3")
                    {
                        this.txtHiddenHazardType.Text = "重大";
                    }

                    UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadMan, this.CurrUser.LoginProjectId, getRectifyNotices.UnitId, true);
                    this.drpUnitHeadMan.SelectedValue = getRectifyNotices.UnitHeadManId;
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

        #region grid 操作 -附件上传
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "AttachUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&strParam=2", itemId, BLL.Const.ProjectRectifyNoticesMenuId)));
            }
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
                if (this.drpUnitHeadMan.SelectedValue != Const._Null)
                {
                    getRectifyNotices.UnitHeadManId = this.drpUnitHeadMan.SelectedValue;                   
                }
                if (saveType == Const.BtnSubmit)
                {
                    if (string.IsNullOrEmpty(getRectifyNotices.UnitHeadManId))
                    {
                        Alert.ShowInParent("请选择单位项目负责人", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        getRectifyNotices.CompleteDate = DateTime.Now;
                        getRectifyNotices.States = Const.State_3;
                        Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                        {
                            FlowOperateId = SQLHelper.GetNewID(),
                            RectifyNoticesId = getRectifyNotices.RectifyNoticesId,
                            OperateManId = this.CurrUser.UserId,
                            OperateTime = DateTime.Now,
                            OperateName = "责任人整改",
                        };
                        Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                        Funs.DB.SubmitChanges();
                    }
                }
                else
                {
                    Funs.DB.SubmitChanges();
                }

                foreach (JObject mergedRow in Grid1.GetModifiedData())
                {
                    JObject values = mergedRow.Value<JObject>("values");
                    string rectifyNoticesItemId = mergedRow.Value<string>("id");
                    string rectifyResults = values.Value<string>("RectifyResults");
                    var getUpdateItem = Funs.DB.Check_RectifyNoticesItem.FirstOrDefault(x => x.RectifyNoticesItemId == rectifyNoticesItemId);
                    if (getUpdateItem != null)
                    {
                        getUpdateItem.RectifyResults = rectifyResults;
                        getUpdateItem.IsRectify = null;
                        Funs.DB.SubmitChanges();
                    }
                }

                ShowNotify(saveType + "成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion
    }
}