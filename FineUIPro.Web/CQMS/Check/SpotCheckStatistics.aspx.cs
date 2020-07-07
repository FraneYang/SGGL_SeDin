using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class SpotCheckStatistics : PageBase
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
                UnitService.InitUnitByProjectIdUnitTypeDropDownList(drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);//施工单位
                UnitWorkService.InitUnitWorkDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);//单位工程
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, true);//专业
                Funs.FineUIPleaseSelect(this.drpControlPoint);//控制点等级
                Funs.FineUIPleaseSelect(drpIsOK);
                Funs.FineUIPleaseSelect(drpIsDataOK);
                BindGrid();
            }
        }
        public void BindGrid()
        {
            this.ProjectId = this.CurrUser.LoginProjectId;
            string strSql = @"select * from  View_Check_SoptCheckDetail where 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (drpUnit.SelectedValue != BLL.Const._Null && drpUnit.SelectedValue != null)
            {
                strSql += " AND UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                strSql += " AND SpotCheckDate >= @SpotCheckDate";
                listStr.Add(new SqlParameter("@SpotCheckDate", txtStartTime.Text.Trim() + " 00:00:00"));
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                strSql += " AND SpotCheckDate <= @SpotCheckDateE";
                listStr.Add(new SqlParameter("@SpotCheckDateE", txtEndTime.Text.Trim() + " 23:59:59"));
            }
            string controlPoint = string.Empty;
            string[] strs = this.drpControlPoint.SelectedValueArray;
            foreach (var item in strs)
            {
                controlPoint += item + ",";
            }
            if (!string.IsNullOrEmpty(controlPoint))
            {
                controlPoint = controlPoint.Substring(0, controlPoint.LastIndexOf(","));
            }
            if (controlPoint != BLL.Const._Null)
            {
                strSql += " AND CHARINDEX(ControlPoint,@ControlPoint)>0";
                listStr.Add(new SqlParameter("@ControlPoint", controlPoint));
            }
            if (drpUnitWork.SelectedValue != BLL.Const._Null && drpUnitWork.SelectedValue != null)
            {
                string unitWorkIds = BLL.UnitWorkService.GetUnitWorkIdsByUnitWorkId(this.drpUnitWork.SelectedValue);
                strSql += " AND CHARINDEX(UnitWorkId,@UnitWorkId)>0";
                listStr.Add(new SqlParameter("@UnitWorkId", unitWorkIds));
            }
            if (drpCNProfessional.SelectedValue != BLL.Const._Null && drpCNProfessional.SelectedValue != null)
            {
                strSql += " AND CNProfessionalCode = @CNProfessionalCode";
                listStr.Add(new SqlParameter("@CNProfessionalCode", this.drpCNProfessional.SelectedValue));
            }
            if (drpIsOK.SelectedValue != BLL.Const._Null && drpIsOK.SelectedValue != null)
            {
                strSql += " AND IsOK = @IsOK";
                listStr.Add(new SqlParameter("@IsOK", this.drpIsOK.SelectedValue));
            }
            if (drpIsDataOK.SelectedValue != BLL.Const._Null && drpIsDataOK.SelectedValue != null)
            {
                strSql += " AND IsDataOK = @IsDataOK";
                listStr.Add(new SqlParameter("@IsDataOK", this.drpIsDataOK.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].RowID;
                if (rowID.Count() > 0)
                {
                    Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                    if (detail.IsOK == false || detail.IsDataOK == "0")
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.SpotCheck_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit1)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit2)
                {
                    return "监理专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit3)
                {
                    return "分包专业工程师上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit5R)
                {
                    return "分包专业工程师重新上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit4)
                {
                    return "总包专业工程师确认资料合格";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        /// <summary>
        /// 获取控制点级别
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertControlPoint(object ControlItemAndCycleId)
        {
            string controlPoint = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    controlPoint = c.ControlPoint;
                }
            }
            return controlPoint;
        }
        /// <summary>
        /// 根据主键返回共检日期
        /// </summary>
        /// <param name="SpotCheckCode"></param>
        /// <returns></returns>
        protected string ConvertSpotCheckDate(object SpotCheckCode)
        {
            if (SpotCheckCode != null)
            {
                Model.Check_SpotCheck spotCheck = BLL.SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode.ToString());
                if (spotCheck != null)
                {
                    if (spotCheck.CheckDateType == "1")
                    {
                        return string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate);
                    }
                    else
                    {
                        return string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate) + "—" + string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate2);
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// 获取共检内容
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertDetailName(object ControlItemAndCycleId)
        {
            string name = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    name = c.ControlItemContent;
                    Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(c.WorkPackageId);
                    if (w != null)
                    {
                        name = w.PackageContent + "/" + name;
                        Model.WBS_WorkPackage pw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(w.SuperWorkPackageId);
                        if (pw != null)
                        {
                            name = pw.PackageContent + "/" + name;
                            Model.WBS_WorkPackage ppw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(pw.SuperWorkPackageId);
                            if (ppw != null)
                            {
                                name = ppw.PackageContent + "/" + name;
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(ppw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                            else
                            {
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(pw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                        }
                        else
                        {
                            Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(w.UnitWorkId);
                            if (u != null)
                            {
                                name = u.UnitWorkName + "/" + name;
                            }
                        }
                    }
                }
            }
            return name;
        }
        /// <summary>
        /// 获取共检结果
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertIsOK(object IsOK)
        {
            string isOK = string.Empty;
            if (IsOK != null)
            {
                if (IsOK.ToString() != "")
                {
                    if (Convert.ToBoolean(IsOK))
                    {
                        isOK = "合格";
                    }
                    else
                    {
                        isOK = "不合格";
                    }
                }
            }
            return isOK;
        }
        /// <summary>
        /// 获取资料结果
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertIsDataOK(object IsDataOK)
        {
            string isDataOK = string.Empty;
            if (IsDataOK != null)
            {
                if (IsDataOK.ToString() != "")
                {
                    if (IsDataOK.ToString() == "1")
                    {
                        isDataOK = "合格";
                    }
                    else if (IsDataOK.ToString() == "0")
                    {
                        isDataOK = "不合格";
                    }
                    else
                    {
                        isDataOK = "不需要";
                    }
                }
            }
            return isDataOK;
        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CQMS/SpotCheck&menuId={1}&type=-1", itemId, BLL.Const.SpotCheckMenuId)));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            drpUnit.SelectedIndex = 0;
            drpCNProfessional.SelectedIndex = 0;
            drpUnitWork.SelectedIndex = 0;
            drpControlPoint.SelectedIndex = 0;
            drpIsOK.SelectedIndex = 0;
            drpIsDataOK.SelectedIndex = 0;
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
    }
}