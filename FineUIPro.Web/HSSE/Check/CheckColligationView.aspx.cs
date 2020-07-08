using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class CheckColligationView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckColligationId
        {
            get
            {
                return (string)ViewState["CheckColligationId"];
            }
            set
            {
                ViewState["CheckColligationId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Check_CheckColligationDetail> checkColligationDetails = new List<Model.View_Check_CheckColligationDetail>();
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
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
          
                this.CheckColligationId = Request.Params["CheckColligationId"];
                var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(this.CheckColligationId);
                if (checkColligation != null)
                {
                    this.txtCheckColligationCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckColligationId);
                    if (checkColligation.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkColligation.CheckTime);
                    }
                    if (!string.IsNullOrEmpty(checkColligation.CheckType))
                    {
                        if (checkColligation.CheckType == "0")
                        {
                            this.txtCheckType.Text = "周检";
                        }
                        else if(checkColligation.CheckType == "1")
                        {
                            this.txtCheckType.Text = "月检";
                        }
                        else if (checkColligation.CheckType == "2")
                        {
                            this.txtCheckType.Text = "其它";
                        }
                    }
                    if (!string.IsNullOrEmpty(checkColligation.PartInUnits))
                    {
                        string unitNames = string.Empty;
                        string[] unitIds = checkColligation.PartInUnits.Split(',');
                        foreach (var item in unitIds)
                        {
                            string name = BLL.UnitService.GetUnitNameByUnitId(item);
                            if (!string.IsNullOrEmpty(name))
                            {
                                unitNames += name + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(unitNames))
                        {
                            unitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
                        }
                        this.txtUnit.Text = unitNames;
                    }
                    this.txtCheckPerson.Text = BLL.UserService.GetUserNameByUserId(checkColligation.CheckPerson);
                    //if (!string.IsNullOrEmpty(checkColligation.CheckAreas))
                    //{
                    //    string areas = string.Empty;
                    //    string[] unitIds = checkColligation.CheckAreas.Split(',');
                    //    foreach (var item in unitIds)
                    //    {
                    //        Model.ProjectData_WorkArea area = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(item);
                    //        if (area != null)
                    //        {
                    //            areas += area.WorkAreaName + ",";
                    //        }
                    //    }
                    //    if (!string.IsNullOrEmpty(areas))
                    //    {
                    //        areas = areas.Substring(0, areas.LastIndexOf(","));
                    //    }
                    //    this.txtCheckAreas.Text = areas;
                    //}
                    this.txtPartInPersons.Text = checkColligation.PartInPersons;
                    this.txtPartInPersonNames.Text = checkColligation.PartInPersonNames;
                    this.txtDaySummary.Text = HttpUtility.HtmlDecode(checkColligation.DaySummary);
                    checkColligationDetails = (from x in new Model.SGGLDB(Funs.ConnString).View_Check_CheckColligationDetail where x.CheckColligationId == this.CheckColligationId orderby x.CheckItem select x).ToList();
                }
                Grid1.DataSource = checkColligationDetails;
                Grid1.DataBind();

                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckColligationMenuId;
                this.ctlAuditFlow.DataId = this.CheckColligationId;
            }
        }
        #endregion

        #region 获取检查类型
        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckItemType(object CheckItem)
        {
            return BLL.Technique_CheckItemDetailService .ConvertCheckItemType(CheckItem);
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
            if (!string.IsNullOrEmpty(this.CheckColligationId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckColligation&menuId={1}&type=-1", this.CheckColligationId, BLL.Const.ProjectCheckColligationMenuId)));
            }
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 转换整改完成情况
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertCompleteStatus(object CompleteStatus)
        {
            if (CompleteStatus != null)
            {
                if (!string.IsNullOrEmpty(CompleteStatus.ToString()))
                {
                    bool completeStatus = Convert.ToBoolean(CompleteStatus.ToString());
                    if (completeStatus)
                    {
                        return "是";
                    }
                    else
                    {
                        return "否";
                    }
                }
            }
            return "";
        }
        #endregion
    }
}