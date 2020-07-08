using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class LawRegulationIdentifyEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 法律法规辨识主键
        /// </summary>
        public string LawRegulationIdentifyId
        {
            get
            {
                return (string)ViewState["LawRegulationIdentifyId"];
            }
            set
            {
                ViewState["LawRegulationIdentifyId"] = value;
            }
        }
        /// <summary>
        /// 项目id
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                this.LawRegulationIdentifyId = Request.Params["lawRegulationIdentifyId"];
                drpLawsRegulationsType.DataTextField = "Name";
                drpLawsRegulationsType.DataValueField = "Name";
                drpLawsRegulationsType.DataSource = BLL.LawsRegulationsTypeService.GetLawsRegulationsTypeList();
                drpLawsRegulationsType.DataBind();
                this.hdLawRegulationId.Text = string.Empty;

                Model.Law_LawRegulationIdentify lawRegulationIdentify = BLL.LawRegulationIdentifyService.GetLawRegulationIdentifyByLawRegulationIdentifyId(LawRegulationIdentifyId);
                if (lawRegulationIdentify != null)
                {
                    this.ProjectId = lawRegulationIdentify.ProjectId;
                    this.txtLawRegulationIdentifyCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.LawRegulationIdentifyId);
                    this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(lawRegulationIdentify.IdentifyPerson);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulationIdentify.IdentifyDate);
                    this.txtRemark.Text = lawRegulationIdentify.Remark;
                    var viewLaw = from x in new Model.SGGLDB(Funs.ConnString).View_Law_LawRegulationSelectedItem where x.LawRegulationIdentifyId == LawRegulationIdentifyId orderby x.LawRegulationCode select x;
                    if (viewLaw.Count() > 0)
                    {
                        this.Grid1.DataSource = viewLaw;
                        this.Grid1.DataBind();
                    }
                }
                else
                {
                    this.txtCompileMan.Text = this.CurrUser.UserName;
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    ////自动生成编码
                    this.txtLawRegulationIdentifyCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.LawRegulationIdentifyMenuId, this.ProjectId, this.CurrUser.UnitId);

                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.LawRegulationIdentifyMenuId;
                this.ctlAuditFlow.DataId = this.LawRegulationIdentifyId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Law_LawRegulationIdentify lawRegulationIdentify = new Model.Law_LawRegulationIdentify
            {
                LawRegulationIdentifyCode = this.txtLawRegulationIdentifyCode.Text.Trim(),
                ProjectId = this.ProjectId,
                IdentifyPerson = this.CurrUser.UserId,
                IdentifyDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()),
                UpdateDate = DateTime.Now,
                Remark = this.txtRemark.Text.Trim(),
                ////单据状态
                States = BLL.Const.State_0
            };
            if (type == BLL.Const.BtnSubmit)
            {
                lawRegulationIdentify.States = this.ctlAuditFlow.NextStep;
                if (lawRegulationIdentify.States == BLL.Const.State_2)
                {
                    lawRegulationIdentify.VersionNumber = BLL.SQLHelper.RunProcNewId2("SpGetVersionNumber", "Law_LawRegulationIdentify", "VersionNumber", this.ProjectId);
                }
            }
            if (!string.IsNullOrEmpty(this.LawRegulationIdentifyId))
            {
                lawRegulationIdentify.LawRegulationIdentifyId = this.LawRegulationIdentifyId;
                BLL.LawRegulationIdentifyService.UpdateLawRegulationIdentify(lawRegulationIdentify);
                BLL.LogService.AddSys_Log(this.CurrUser, lawRegulationIdentify.LawRegulationIdentifyCode, this.LawRegulationIdentifyId, BLL.Const.LawRegulationIdentifyMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.LawRegulationIdentifyId = SQLHelper.GetNewID(typeof(Model.Law_LawRegulationIdentify));
                lawRegulationIdentify.LawRegulationIdentifyId =  this.LawRegulationIdentifyId;
                BLL.LawRegulationIdentifyService.AddLawRegulationIdentify(lawRegulationIdentify);
                BLL.LogService.AddSys_Log(this.CurrUser, lawRegulationIdentify.LawRegulationIdentifyCode, this.LawRegulationIdentifyId, BLL.Const.LawRegulationIdentifyMenuId, BLL.Const.BtnAdd);
            }
            
            if (lawRegulationIdentify != null)
            {
                List<Model.Base_LawsRegulationsType> lawsRegulationsTypes = BLL.LawsRegulationsTypeService.GetLawsRegulationsTypeList();
                BLL.LawRegulationSelectedItemService.DeleteLawRegulationSelectedItemByLawRegulationIdentifyId(LawRegulationIdentifyId);               
                JArray mergedData = Grid1.GetMergedData();
                foreach (JObject mergedRow in mergedData)
                {
                    string status = mergedRow.Value<string>("status");
                    JObject values = mergedRow.Value<JObject>("values");

                    Model.Law_LawRegulationSelectedItem lawRegulationSelectedItem = new Model.Law_LawRegulationSelectedItem
                    {
                        LawRegulationIdentifyId = this.LawRegulationIdentifyId,
                        LawRegulationId = values.Value<string>("LawRegulationId").ToString()
                    };

                    Model.Base_LawsRegulationsType lawsRegulationsType = lawsRegulationsTypes.FirstOrDefault(x => x.Name == values.Value<string>("LawsRegulationsTypeId").ToString());
                    if (lawsRegulationsType != null)
                    {
                        lawRegulationSelectedItem.LawRegulationGrade = lawsRegulationsType.Id;
                    }
                    lawRegulationSelectedItem.FitPerfomance = values.Value<string>("FitPerfomance").ToString();
                    BLL.LawRegulationSelectedItemService.AddLawRegulationSelectedItem(lawRegulationSelectedItem);
                }
            }

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId,BLL.Const.LawRegulationIdentifyMenuId, this.LawRegulationIdentifyId, (type == BLL.Const.BtnSubmit ? true : false), string.Format("{0:yyyy-MM-dd}", lawRegulationIdentify.IdentifyDate), "../InformationProject/LawRegulationIdentifyView.aspx?LawRegulationIdentifyId={0}");
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
            if (string.IsNullOrEmpty(this.LawRegulationIdentifyId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LawRegulationIdentify&menuId={1}", this.LawRegulationIdentifyId, BLL.Const.LawRegulationIdentifyMenuId)));
        }
        #endregion

        #region 重选事件
        private static List<Model.View_Law_LawRegulationSelectedItem> pageItem = new List<Model.View_Law_LawRegulationSelectedItem>();
        /// <summary>
        /// 重选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReSet_Click(object sender, EventArgs e)
        {
            pageItem.Clear();
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");

                Model.View_Law_LawRegulationSelectedItem newItem = new Model.View_Law_LawRegulationSelectedItem
                {
                    LawRegulationIdentifyId = this.LawRegulationIdentifyId,
                    LawRegulationId = values.Value<string>("LawRegulationId").ToString()
                };
                Model.Base_LawsRegulationsType lawsRegulationsType = new Model.SGGLDB(Funs.ConnString).Base_LawsRegulationsType.FirstOrDefault(x => x.Name == values.Value<string>("LawsRegulationsTypeId").ToString());
                if (lawsRegulationsType != null)
                {
                    newItem.LawsRegulationsTypeId = lawsRegulationsType.Id;
                    newItem.LawsRegulationsTypeName = lawsRegulationsType.Name;
                }
                newItem.FitPerfomance = values.Value<string>("FitPerfomance").ToString();
                pageItem.Add(newItem);
                this.hdLawRegulationId.Text += newItem.LawRegulationId + ",";
            }

            string window = String.Format("CreateLawRegulationIdentify.aspx?LawRegulationIds={0}", this.hdLawRegulationId.Text, "选择 - ");
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(this.hdLawRegulationId.ClientID) + Window1.GetShowReference(window));
        }

        /// <summary>
        /// 页面关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            string LawRegulationIds = this.hdLawRegulationId.Text;
            var viewLaw = new List<Model.View_Law_LawRegulationSelectedItem>();
            if (!string.IsNullOrEmpty(LawRegulationIds))
            {
                List<string> LawRegulationIdLists = Funs.GetStrListByStr(LawRegulationIds, ',');
                foreach (var id in LawRegulationIdLists)
                {
                    var law = BLL.LawRegulationListService.GetViewLawRegulationListById(id);
                    if (law != null)
                    {
                        Model.View_Law_LawRegulationSelectedItem newLawitem = new Model.View_Law_LawRegulationSelectedItem
                        {
                            LawRegulationSelectedItemId = SQLHelper.GetNewID(typeof(Model.Law_LawRegulationSelectedItem)),
                            LawRegulationIdentifyId = this.LawRegulationIdentifyId,
                            LawRegulationId = law.LawRegulationId,
                            LawRegulationCode = law.LawRegulationCode,
                            LawsRegulationsTypeId = law.LawsRegulationsTypeId,
                            LawsRegulationsTypeName = law.LawsRegulationsTypeName,
                            LawRegulationName = law.LawRegulationName,
                            ApprovalDate = law.ApprovalDate,
                            EffectiveDate = law.EffectiveDate,
                            Description = law.Description
                        };
                        if (law.Description.Length > 41)
                        {
                            newLawitem.ShortDescription = law.Description.Substring(0, 40);
                        }
                        else
                        {
                            newLawitem.ShortDescription = law.Description;
                        }

                        if (pageItem.Count() > 0)
                        {
                            var viewSelectItem = pageItem.FirstOrDefault(x => x.LawRegulationIdentifyId == this.LawRegulationIdentifyId && x.LawRegulationId == law.LawRegulationId);
                            if (viewSelectItem != null)
                            {
                                newLawitem.LawsRegulationsTypeId = viewSelectItem.LawsRegulationsTypeId;
                                newLawitem.LawsRegulationsTypeName = viewSelectItem.LawsRegulationsTypeName;
                                newLawitem.FitPerfomance = viewSelectItem.FitPerfomance;
                            }
                        }
                        viewLaw.Add(newLawitem);
                    }
                }
            }

            this.Grid1.DataSource = viewLaw;
            this.Grid1.DataBind();
            this.hdLawRegulationId.Text = string.Empty;
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("法律法规辨识" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.Grid1.DataSource = from x in new Model.SGGLDB(Funs.ConnString).View_Law_LawRegulationSelectedItem where x.LawRegulationIdentifyId == LawRegulationIdentifyId orderby x.LawRegulationCode select x;
            this.Grid1.DataBind();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “LawRegulationIdentifyEdit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “LawRegulationIdentifyEdit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {            
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                grid.Columns[0].Visible = false;
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfDescription")
                    {
                        html = (row.FindControl("lblDescription") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion
    }
}