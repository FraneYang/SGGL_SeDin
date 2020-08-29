using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class JointCheckStatistics : PageBase
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.FineUIPleaseSelect(drpState);
                Funs.FineUIPleaseSelect(drpQuestionType);
                QualityQuestionTypeService.InitQualityQuestionTypeDownList(drpQuestionType, true);
                UnitService.InitUnitDropDownList(drpSponsorUnit, this.CurrUser.LoginProjectId, true);//施工单位
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);//单位工程
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);//专业
                this.drpCheckType.DataTextField = "Text";
                this.drpCheckType.DataValueField = "Value";
                this.drpCheckType.DataSource = BLL.JointCheckService.GetCheckTypeList2();
                this.drpCheckType.DataBind();
                BindGrid();
            }

        }
        public void BindGrid()
        {
            this.ProjectId = this.CurrUser.LoginProjectId;
            string strSql = @"select * from  View_Check_JointCheckDetail where 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (drpSponsorUnit.SelectedValue != BLL.Const._Null && drpSponsorUnit.SelectedValue != null)
            {
                strSql += " AND UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpSponsorUnit.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                strSql += " AND CheckDate >= @CheckDate";
                listStr.Add(new SqlParameter("@CheckDate", txtStartTime.Text.Trim() + " 00:00:00"));
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                strSql += " AND CheckDate <= @CheckDateE";
                listStr.Add(new SqlParameter("@CheckDateE", txtEndTime.Text.Trim() + " 23:59:59"));
            }
            if (drpUnitWork.SelectedValue != BLL.Const._Null && drpUnitWork.SelectedValue != null)
            {
                strSql += " AND UnitWorkId = @UnitWorkId";
                listStr.Add(new SqlParameter("@UnitWorkId", this.drpUnitWork.SelectedValue));
            }
            if (drpCNProfessional.SelectedValue != BLL.Const._Null && drpCNProfessional.SelectedValue != null)
            {
                strSql += " AND CNProfessionalCode = @CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", this.drpCNProfessional.SelectedValue));
            }
            if (drpCheckType.SelectedValue != BLL.Const._Null && drpCheckType.SelectedValue != null && drpCheckType.SelectedValue != "")
            {
                strSql += " AND CheckType = @CheckType";
                listStr.Add(new SqlParameter("@CheckType", this.drpCheckType.SelectedValue));
            }
            if (drpState.SelectedValue != Const._Null)
            {
                if (drpState.SelectedValue == "1")   //已闭合
                {
                    strSql += " AND OK='1' ";
                }
                else   //未闭合
                {
                    strSql += " AND OK='0'";
                }
            }
            if (drpQuestionType.SelectedValue != Const._Null)
            {
                strSql += " AND QuestionType=@QuestionType";
                listStr.Add(new SqlParameter("@QuestionType", drpQuestionType.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object JointCheckDetailId)
        {
            Model.Check_JointCheckDetail jointCheckDetail = BLL.JointCheckDetailService.GetJointCheckDetailByJointCheckDetailId(JointCheckDetailId.ToString());
            if (jointCheckDetail != null)
            {
                if (!string.IsNullOrEmpty(jointCheckDetail.State))
                {
                    string state = jointCheckDetail.State;
                    if (state == BLL.Const.JointCheck_ReCompile)
                    {
                        return "重新整理";
                    }
                    else if (state == BLL.Const.JointCheck_Compile)
                    {
                        return "编制";
                    }
                    else if (state == BLL.Const.JointCheck_Audit1)
                    {
                        return "分包专业工程师回复";
                    }
                    else if (state == BLL.Const.JointCheck_Audit2)
                    {
                        return "分包负责人确认";
                    }
                    else if (state == BLL.Const.JointCheck_Audit3)
                    {
                        return "总包专业工程师确认";
                    }
                    else if (state == BLL.Const.JointCheck_Audit4)
                    {
                        return "总包质量经理确认";
                    }
                    else if (state == BLL.Const.JointCheck_Complete)
                    {
                        return "审批完成";
                    }
                    else if (state == BLL.Const.JointCheck_Audit1R)
                    {
                        return "分包专工重新回复";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
                //string state = BLL.JointCheckService.GetJointCheck(jointCheckDetail.JointCheckId).State;

            }
            else
            {
                string state = BLL.CheckControlService.GetCheckControl(JointCheckDetailId.ToString()).State;
                if (state == BLL.Const.CheckControl_ReCompile)
                {
                    return "重新整理";
                }
                else if (state == BLL.Const.CheckControl_Compile)
                {
                    return "编制";
                }
                else if (state == BLL.Const.CheckControl_Audit1)
                {
                    return "总包负责人审批";
                }
                else if (state == BLL.Const.CheckControl_Audit2)
                {
                    return "分包专业工程师回复";
                }
                else if (state == BLL.Const.CheckControl_Audit3)
                {
                    return "分包负责人审批";
                }
                else if (state == BLL.Const.CheckControl_Audit4)
                {
                    return "总包专业工程师确认";
                }
                else if (state == BLL.Const.CheckControl_Audit5)
                {
                    return "总包负责人审批";
                }
                else if (state == BLL.Const.CheckControl_Complete)
                {
                    return "审批完成";
                }
                else if (state == BLL.Const.CheckControl_ReCompile2)
                {
                    return "分包专业工程师重新回复";
                }
                else
                {
                    return "";
                }
            }
        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.Check_JointCheck jointCheck1 = JointCheckService.GetJointCheck(itemId);
            if (e.CommandName == "ReAttachUrl")
            {

                if (jointCheck1 != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/JointCheck&type=-1", itemId + "r", "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckControl&type=-1", itemId + "r", "查看 - ")));
                }


            }
            if (e.CommandName == "attchUrl")
            {
                if (jointCheck1 != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/JointCheck&type=-1", itemId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckControl&type=-1", itemId, "查看 - ")));
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpSponsorUnit.SelectedIndex = 0;
            drpCheckType.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpUnitWork.SelectedIndex = 0;
            drpState.SelectedIndex = 0;
            drpQuestionType.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("质量问题统计" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //this.Grid1.PageSize = this.;
            this.Grid1.PageSize = 10000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “PersonList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “PersonList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (column.ColumnID != "AttchUrl" && column.ColumnID != "ReAttachUrl")
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    if (column.ColumnID != "AttchUrl" && column.ColumnID != "ReAttachUrl")
                    {
                        string html = row.Values[column.ColumnIndex].ToString();
                        if (column.ColumnID == "tfPageIndex")
                        {
                            html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                        }
                        if (column.ColumnID == "lbState")
                        {
                            html = (row.FindControl("lbState") as AspNet.Label).Text;
                        }
                        //sb.AppendFormat("<td>{0}</td>", html);
                        sb.AppendFormat("<td style='vnd.ms-excel.numberformat:@;width:140px;'>{0}</td>", html);
                    }
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion
    }
}