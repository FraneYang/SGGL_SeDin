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
    public partial class RectifyNoticesAdd : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
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
                //受检单位            
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, false);
                //区域
                BLL.UnitWorkService.InitUnitWorkDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);
                ///安全经理
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, Const.UnitId_SEDIN, true);
                ///检察人员
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpCheckMan, this.CurrUser.LoginProjectId, null, true);
                //Funs.FineUIPleaseSelect(drpIsRectify);
                RectifyNoticesId = Request.Params["RectifyNoticesId"];
                var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                if (getRectifyNotices != null)
                {
                    if (!string.IsNullOrEmpty(getRectifyNotices.UnitId))
                    {
                        this.drpUnitId.SelectedValue = getRectifyNotices.UnitId;
                    }

                    if (!string.IsNullOrEmpty(getRectifyNotices.WorkAreaId))
                    {
                        this.drpWorkAreaId.SelectedValueArray = getRectifyNotices.WorkAreaId.Split(',');
                    }
                    if (!string.IsNullOrEmpty(getRectifyNotices.CheckManIds))
                    {
                        this.drpCheckMan.SelectedValueArray = getRectifyNotices.CheckManIds.Split(',');
                    }
                    this.txtCheckPerson.Text = getRectifyNotices.CheckManNames;
                    this.txtRectifyNoticesCode.Text = getRectifyNotices.RectifyNoticesCode;
                    this.txtCheckedDate.Text = getRectifyNotices.CheckedDate.ToString();
                    if (!string.IsNullOrEmpty(getRectifyNotices.SignPerson))
                    {
                        this.drpSignPerson.SelectedValue = getRectifyNotices.SignPerson;
                    }
                    if (!string.IsNullOrEmpty(getRectifyNotices.HiddenHazardType))
                    {
                        this.drpHiddenHazardType.SelectedValue = getRectifyNotices.HiddenHazardType;
                    }
                }
                else
                {
                    this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtRectifyNoticesCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectRectifyNoticesMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                }

                BindGrid();
                BindGrid1();
            }
        }

        #region 绑定grid
        /// <summary>
        /// 
        /// </summary>
        public void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.RectifyNoticesId))
            {
                string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults, IsRectify  from [dbo].[Check_RectifyNoticesItem] ";
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
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<Model.Check_RectifyNoticesItem> view = getViewTestPlanTrainingList();
            Model.Check_RectifyNoticesItem notice = new Model.Check_RectifyNoticesItem
            {
                RectifyNoticesItemId = SQLHelper.GetNewID()
            };
            view.Add(notice);
            //将gd数据保存在list中
            Grid1.DataSource = view;
            Grid1.DataBind();
        }

        #region 收集页面明细信息
        /// <summary>
        /// 收集页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.Check_RectifyNoticesItem> getViewTestPlanTrainingList()
        {
            List<Model.Check_RectifyNoticesItem> view = new List<Check_RectifyNoticesItem>();
            var data = Grid1.GetMergedData();
            if (data != null)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    JObject values = mergedRow.Value<JObject>("values");
                    string wrongContent = values.Value<string>("WrongContent");
                    string rectifyNoticesItemId = values.Value<string>("RectifyNoticesItemId");
                    string requirement = values.Value<string>("Requirement");
                    string limitTime = values.Value<string>("LimitTime");
                    var item = new Check_RectifyNoticesItem
                    {
                        RectifyNoticesItemId = rectifyNoticesItemId,
                        RectifyNoticesId = this.RectifyNoticesId,
                        WrongContent = wrongContent,
                        Requirement = requirement,
                        LimitTime =Funs.GetNewDateTime( limitTime),
                    };
                    view.Add(item);
                }
            }
            return view;
        }
        #endregion

        #region grid 操作
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&strParam=1", itemId, BLL.Const.ProjectRectifyNoticesMenuId)));
            }
            if (e.CommandName == "delete")
            {
                var getView = getViewTestPlanTrainingList();
                var getfi = getView.FirstOrDefault(x => x.RectifyNoticesItemId == itemId);
                if (getfi != null)
                {
                    getView.Remove(getfi);
                }
                Grid1.DataSource = getView;
                Grid1.DataBind();
            }
        }

        #endregion

        #region 保存方法

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveRectifyNotices(Const.BtnSave);
        }

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
            Model.Check_RectifyNotices newRectifyNotices = new Check_RectifyNotices
            {
                RectifyNoticesId = this.RectifyNoticesId,
                RectifyNoticesCode = this.txtRectifyNoticesCode.Text.Trim(),
                ProjectId = this.CurrUser.LoginProjectId,
                CheckedDate = Funs.GetNewDateTime(this.txtCheckedDate.Text.Trim()),
                CompleteManId = this.CurrUser.UserId,
                Isprint = Const.State_0,
                Isprintf = Const.State_0,
            };
            newRectifyNotices.States = Const.State_0;
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                newRectifyNotices.SignPerson = this.drpSignPerson.SelectedValue;
            }

            if (saveType == Const.BtnSubmit)
            {
                newRectifyNotices.States = Const.State_1;
                if (string.IsNullOrEmpty(newRectifyNotices.SignPerson))
                {                   
                    Alert.ShowInParent("请选择项目安全经理！", MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                newRectifyNotices.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                newRectifyNotices.WorkAreaId = Funs.GetStringByArray(this.drpWorkAreaId.SelectedValueArray); 
            }
            if (this.drpCheckMan.SelectedValue != BLL.Const._Null)
            {
                newRectifyNotices.CheckManIds =Funs.GetStringByArray(this.drpCheckMan.SelectedValueArray);
            }
            if (!string.IsNullOrEmpty(txtCheckPerson.Text))
            {
                newRectifyNotices.CheckManNames = txtCheckPerson.Text;
            }
            if (this.drpHiddenHazardType.SelectedValue != BLL.Const._Null)
            {
                newRectifyNotices.HiddenHazardType = this.drpHiddenHazardType.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.RectifyNoticesId))
            {
                RectifyNoticesService.UpdateRectifyNotices(newRectifyNotices);
                RectifyNoticesService.DeleteRectifyNoticesRectifyNoticesItemById(this.RectifyNoticesId);
            }
            else
            {
                this.RectifyNoticesId = newRectifyNotices.RectifyNoticesId = SQLHelper.GetNewID();
                RectifyNoticesService.AddRectifyNotices(newRectifyNotices);
            }
            if (saveType == Const.BtnSubmit)
            {
                Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                {
                    FlowOperateId = SQLHelper.GetNewID(),
                    RectifyNoticesId = newRectifyNotices.RectifyNoticesId,
                    OperateName = "检查人员提交整改单",
                    OperateManId = this.CurrUser.UserId,
                    OperateTime = DateTime.Now,
                };
                Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                Funs.DB.SubmitChanges();
            }
            var getItem = getViewTestPlanTrainingList();
            if (getItem.Count() > 0)
            {
                Funs.DB.Check_RectifyNoticesItem.InsertAllOnSubmit(getItem);
                Funs.DB.SubmitChanges();
            }

            ShowNotify(saveType + "成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCheckMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpCheckMan.SelectedValueArray = Funs.RemoveDropDownListNull(this.drpCheckMan.SelectedValueArray);
        }

        protected void drpWorkAreaId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpWorkAreaId.SelectedValueArray = Funs.RemoveDropDownListNull(this.drpWorkAreaId.SelectedValueArray);
        }
    }
}