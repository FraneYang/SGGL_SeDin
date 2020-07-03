using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Hazard
{
    public partial class HazardListView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 类别Code
        /// </summary>
        public string HazardSortCode
        {
            get
            {
                return (string)ViewState["HazardSortCode"];
            }
            set
            {
                ViewState["HazardSortCode"] = value;
            }
        }

        /// <summary>
        /// 危险源辨识与评价清单Id
        /// </summary>
        public string HazardListId
        {
            get
            {
                return (string)ViewState["HazardListId"];
            }
            set
            {
                ViewState["HazardListId"] = value;
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
        /// 判断是否完全选
        /// </summary>
        //private bool result = true;

        /// <summary>
        /// 危险源集合
        /// </summary>
        public static List<Model.HSSE_HazardTemplate> hazardTemplates = new List<Model.HSSE_HazardTemplate>();

        /// <summary>
        /// 新增危险源集合
        /// </summary>
        public static List<Model.HSSE_HazardTemplate> newHazardTemplates = new List<Model.HSSE_HazardTemplate>();

        /// <summary>
        /// 类别集合
        /// </summary>
        //private static List<Model.Technique_HazardListType> hazardSorts = new List<Model.Technique_HazardListType>();

        /// <summary>
        /// 工作阶段ID集合
        /// </summary>
        private static List<string> workStageIds = new List<string>();

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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                hazardTemplates.Clear();
                newHazardTemplates.Clear();

                this.HazardListId = Request.Params["HazardListId"];
                if (!string.IsNullOrEmpty(this.HazardListId))
                {
                    Model.Hazard_HazardList hazardList = Hazard_HazardListService.GetHazardList(this.HazardListId);
                    if (hazardList != null)
                    {
                        this.ProjectId = hazardList.ProjectId;
                        this.txtHazardListCode.Text = CodeRecordsService.ReturnCodeByDataId(this.HazardListId);
                        if (!string.IsNullOrEmpty(hazardList.WorkStage))
                        {
                            List<string> workStages = hazardList.WorkStage.Split(',').ToList();
                            string workStageId = string.Empty;
                            string workStage = string.Empty;
                            foreach (string str in workStages)
                            {
                                workStageId += str + ",";
                                Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(str);
                                if (c != null)
                                {
                                    workStage += c.WorkStageName + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(workStage))
                            {
                                this.hdWorkStage.Text = workStageId.Substring(0, workStageId.LastIndexOf(","));
                                workStage = workStage.Substring(0, workStage.LastIndexOf(","));
                            }
                            this.txtWorkStage.Text = workStage;
                        }
                        this.hdWorkStage.Text = hazardList.WorkStage;
                        Model.Sys_User compileMan = BLL.UserService.GetUserByUserId(hazardList.CompileMan);
                        if (compileMan!=null)
                        {
                            this.txtCompileMan.Text = compileMan.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", hazardList.CompileDate);
                        Model.Sys_User controllingPerson = BLL.UserService.GetUserByUserId(hazardList.ControllingPerson);
                        if (controllingPerson!=null)
                        {
                            this.txtControllingPerson.Text = controllingPerson.UserName;
                        }
                        this.txtIdentificationDate.Text = string.Format("{0:yyyy-MM-dd}", hazardList.IdentificationDate);
                        this.txtWorkArea.Text = hazardList.WorkAreaName;
                        this.txtContents.Text = HttpUtility.HtmlDecode(hazardList.Contents);
                        //this.HazardSortSetDataBind();
                        this.SelectedCheckedHazardItem();
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHazardListMenuId;
                this.ctlAuditFlow.DataId = this.HazardListId;
            }
        }
        #endregion

        #region 筛选风险评价清单列表
        /// <summary>
        /// 筛选风险评价清单列表
        /// </summary>
        private void GetHazardTemplates()
        {
            List<Model.HSSE_HazardTemplate> newHazardTemplates1 = new List<Model.HSSE_HazardTemplate>();
            List<Model.HSSE_HazardTemplate> newHazardTemplates2 = new List<Model.HSSE_HazardTemplate>();
            if (this.drpHelperMethods.SelectedValue != null)
            {
                foreach (Model.HSSE_HazardTemplate hazardTemplate in hazardTemplates)
                {
                    for (int i = 0; i < this.drpHelperMethods.Items.Count; i++)
                    {
                        if (this.drpHelperMethods.Items[i].Selected)
                        {
                            if (hazardTemplate.HelperMethod == this.drpHelperMethods.Items[i].Value)
                            {
                                newHazardTemplates1.Add(hazardTemplate);
                            }
                        }
                    }
                }
            }
            else
            {
                newHazardTemplates1.AddRange(hazardTemplates);
            }

            if (this.drpHazardLevel.SelectedValue != null)
            {
                foreach (Model.HSSE_HazardTemplate hazardTemplate in newHazardTemplates1)
                {
                    for (int i = 0; i < this.drpHazardLevel.Items.Count; i++)
                    {
                        if (this.drpHazardLevel.Items[i].Selected)
                        {
                            if (hazardTemplate.HazardLevel.ToString() == this.drpHazardLevel.Items[i].Value)
                            {
                                newHazardTemplates2.Add(hazardTemplate);
                            }
                        }
                    }
                }
            }
            else
            {
                newHazardTemplates2.AddRange(newHazardTemplates1);
            }
            this.Grid1.DataSource = newHazardTemplates2;
            this.Grid1.DataBind();
        }
        #endregion

        #region 选中选择项
        /// <summary>
        /// 选中选择项
        /// </summary>
        private void SelectedCheckedHazardItem()
        {
            List<Model.HSSE_HazardTemplate> newHazardTemplates = new List<Model.HSSE_HazardTemplate>();
            List<Model.Hazard_HazardSelectedItem> hazardSelectedItems = Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(HazardListId);           
            foreach (Model.Hazard_HazardSelectedItem hazardSelectedItem in hazardSelectedItems)
            {
                Model.HSSE_HazardTemplate newHazardTemplate = new Model.HSSE_HazardTemplate
                {
                    HazardId = hazardSelectedItem.HazardId,
                    HazardListTypeId = hazardSelectedItem.HazardListTypeId,
                    HazardItems = hazardSelectedItem.HazardItems,
                    DefectsType = hazardSelectedItem.DefectsType,
                    MayLeadAccidents = hazardSelectedItem.MayLeadAccidents,
                    HelperMethod = hazardSelectedItem.HelperMethod,
                    HazardJudge_L = hazardSelectedItem.HazardJudge_L,
                    HazardJudge_E = hazardSelectedItem.HazardJudge_E,
                    HazardJudge_C = hazardSelectedItem.HazardJudge_C,
                    HazardJudge_D = hazardSelectedItem.HazardJudge_D,
                    HazardLevel = hazardSelectedItem.HazardLevel,
                    ControlMeasures = hazardSelectedItem.ControlMeasures,
                    WorkStage = hazardSelectedItem.WorkStage.Trim()
                };
                newHazardTemplates.Add(newHazardTemplate);
            }
            hazardTemplates = newHazardTemplates;
            this.Grid1.DataSource = hazardTemplates;
            this.Grid1.DataBind();
        }
        #endregion
        
        #region 依次展开此节点的所以上级节点
        /// <summary>
        /// 依次展开此节点的所以上级节点
        /// </summary>
        private void ExpandParents(TreeNode node)
        {
            if (node.NodeID != "0")
            {
                node.Expanded = true;
                node.Checked = true;
                ExpandParents(node.ParentNode);
            }
        }
        #endregion               

        #region 查找工作阶段及危险源清单
        /// <summary>
        /// 查找工作阶段及危险源清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWorkStage_OnClick(object sender, EventArgs e)
        {
            Session["workStages"] = null;
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("HazardTemplate.aspx?HazardListId={0}&WorkStageIds={1}", this.HazardListId, this.hdWorkStage.Text, "工作阶段和危险源清单 - ")));
        }

        /// <summary>
        /// 关闭查找工作阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["workStages"] != null)
            {
                string newWorkStages = string.Empty;
                string workStages = Session["workStages"].ToString();
                List<string> templates = workStages.Split('|').ToList();
                foreach (var item in templates)
                {
                    Model.Technique_HazardList template = BLL.HazardListService.GetHazardListById(item.Split(',').ToList()[0]);
                    Model.HSSE_HazardTemplate hazardTemplate = new Model.HSSE_HazardTemplate
                    {
                        HazardId = template.HazardId,
                        HazardListTypeId = template.HazardListTypeId,
                        WorkStage = item.Split(',').ToList()[1],
                        HazardItems = template.HazardItems,
                        DefectsType = template.DefectsType,
                        MayLeadAccidents = template.MayLeadAccidents,
                        HelperMethod = template.HelperMethod,
                        HazardJudge_L = template.HazardJudge_L,
                        HazardJudge_E = template.HazardJudge_E,
                        HazardJudge_C = template.HazardJudge_C,
                        HazardJudge_D = template.HazardJudge_D,
                        HazardLevel = template.HazardLevel,
                        ControlMeasures = template.ControlMeasures
                    };
                    hazardTemplates.Add(hazardTemplate);
                    workStageIds.Add(item.Split(',').ToList()[1]);//把工作阶段放入集合里    

                }
                //获取工作阶段加载到文本中
                foreach (var i in workStageIds.Distinct().ToList())
                {
                    this.hdWorkStage.Text = i;
                    newWorkStages += WorkStageService.GetWorkStageById(this.hdWorkStage.Text).WorkStageName + ",";
                }
                if (!string.IsNullOrEmpty(newWorkStages))
                {
                    this.txtWorkStage.Text = newWorkStages.Substring(0, newWorkStages.LastIndexOf(","));
                }

                //循环集合是否存在相同项，如果存在，移除重复项
                for (int i = 0; i < hazardTemplates.Count(); i++)
                {
                    for (int j = hazardTemplates.Count() - 1; j > i; j--)
                    {
                        if (hazardTemplates[j].HazardId.Equals(hazardTemplates[i].HazardId))
                        {
                            hazardTemplates.Remove(hazardTemplates[j]);
                        }
                    }
                }
                this.Grid1.DataSource = hazardTemplates;
                this.Grid1.DataBind();
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取工作阶段
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertWorkStage(object WorkStage)
        {
            string name = string.Empty;
            if (WorkStage != null)
            {
                string workStage = WorkStage.ToString().Trim();
                Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(workStage);
                if (c != null)
                {
                    name = c.WorkStageName;
                }
            }
            return name;
        }

        /// <summary>
        /// 获取危险源编号
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertHazardCode(object HazardId)
        {
            string hazardCode = string.Empty;
            if (HazardId != null)
            {
                Model.Technique_HazardList hazardList = BLL.HazardListService.GetHazardListById(HazardId.ToString());
                if (hazardList != null)
                {
                    hazardCode = hazardList.HazardCode;
                }
            }
            return hazardCode;
        }

        /// <summary>
        /// 获取危险源类别
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        protected string ConvertSupHazardListTypeId(object hazardListTypeId)
        {
            if (hazardListTypeId != null)
            {
                Model.Technique_HazardListType hazardListType = HazardListTypeService.GetHazardListTypeById(hazardListTypeId.ToString());
                if (hazardListType != null)
                {
                    var hazard = HazardListTypeService.GetHazardListTypeById(hazardListType.SupHazardListTypeId);
                    if (hazard != null)
                    {
                        return hazard.HazardListTypeName;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取危险源项
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        protected string ConvertHazardListTypeId(object hazardListTypeId)
        {
            if (hazardListTypeId != null)
            {
                Model.Technique_HazardListType hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(hazardListTypeId.ToString());
                if (hazardListType != null)
                {
                    return hazardListType.HazardListTypeName;
                }
            }
            return null;
        }
        #endregion

        #region 选择辅助方法
        /// <summary>
        /// 选择辅助方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpHelperMethods_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string assistWays = "";
            for (int i = 0; i < this.drpHelperMethods.Items.Count; i++)
            {
                if (this.drpHelperMethods.Items[i].Selected)
                {
                    if (assistWays == "")
                    {
                        assistWays = this.drpHelperMethods.Items[i].Text;
                    }
                    else
                    {
                        assistWays += "," + this.drpHelperMethods.Items[i].Text;
                    }
                }
            }
            this.drpHelperMethods.Text = assistWays;
            GetHazardTemplates();
        }
        #endregion

        #region 选择危险级别
        /// <summary>
        /// 选择危险级别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpHazardLevel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string hazardGrades = "";
            for (int i = 0; i < this.drpHazardLevel.Items.Count; i++)
            {
                if (this.drpHazardLevel.Items[i].Selected)
                {
                    if (hazardGrades == "")
                    {
                        hazardGrades = this.drpHazardLevel.Items[i].Text;
                    }
                    else
                    {
                        hazardGrades += "," + this.drpHazardLevel.Items[i].Text;
                    }
                }
            }
            this.drpHazardLevel.Text = hazardGrades;
            GetHazardTemplates();
        }
        #endregion      

        #region 格式化字符串
        /// <summary>
        /// 转换工作阶段
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertWorkStage(string workStage)
        {
            if (workStage != null)
            {
                string workStages = string.Empty;
                string[] strList = workStage.ToString().Split(',');
                foreach (string str in strList)
                {
                    Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(str);
                    if (c != null)
                    {
                        workStages += c.WorkStageName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(workStages))
                {
                    workStages = workStages.Substring(0, workStages.LastIndexOf(","));
                }
                return workStages;
            }
            return "";
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("风险源辨识与评价清单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “HazardListView.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “HazardListView.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardId")
                    {
                        html = (row.FindControl("lblHazardId") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHazardListTypeId")
                    {
                        html = (row.FindControl("lblHazardListTypeId") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkStage")
                    {
                        html = (row.FindControl("lblWorkStage") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkStages")
                    {
                        html = (row.FindControl("lblWorkStages") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
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
            if (!string.IsNullOrEmpty(this.HazardListId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HazardListAttachUrl&menuId={1}&type=-1", this.HazardListId, BLL.Const.ProjectHazardListMenuId)));
            }
        }
        #endregion
    }
}