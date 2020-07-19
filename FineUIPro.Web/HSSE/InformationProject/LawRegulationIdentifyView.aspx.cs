using BLL;
using System;
using System.Data;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class LawRegulationIdentifyView : PageBase
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.LawRegulationIdentifyId = Request.Params["lawRegulationIdentifyId"];
                Model.Law_LawRegulationIdentify lawRegulationIdentify = BLL.LawRegulationIdentifyService.GetLawRegulationIdentifyByLawRegulationIdentifyId(LawRegulationIdentifyId);
                if (lawRegulationIdentify != null)
                {
                    this.txtLawRegulationIdentifyCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.LawRegulationIdentifyId);
                    this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(lawRegulationIdentify.IdentifyPerson);
                    if (lawRegulationIdentify.IdentifyDate != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulationIdentify.IdentifyDate);
                    }
                    this.txtRemark.Text = lawRegulationIdentify.Remark;
                    this.Grid1.DataSource = from x in Funs.DB.View_Law_LawRegulationSelectedItem where x.LawRegulationIdentifyId == LawRegulationIdentifyId orderby x.LawRegulationCode select x;
                    this.Grid1.DataBind();
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.LawRegulationIdentifyMenuId;
                this.ctlAuditFlow.DataId = this.LawRegulationIdentifyId;
            }
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LawRegulationIdentify&menuId={1}&type=-1", this.LawRegulationIdentifyId, BLL.Const.LawRegulationIdentifyMenuId)));
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
            this.Grid1.DataSource = from x in Funs.DB.View_Law_LawRegulationSelectedItem where x.LawRegulationIdentifyId == LawRegulationIdentifyId orderby x.LawRegulationCode select x;
            this.Grid1.DataBind();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “LawRegulationIdentifyView.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “LawRegulationIdentifyView.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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