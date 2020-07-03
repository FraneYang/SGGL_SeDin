using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class SpotDataCheck : PageBase
    {
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.SpotDataCheckMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnSave) || buttonList.Contains(Const.BtnSubmit))
                {
                    btnMenuModify.Hidden = false;
                }
            }
        }
        #endregion
        /// <summary>
        /// 工序验收记录主键
        /// </summary>
        public string SpotCheckCode
        {
            get
            {
                return (string)ViewState["SpotCheckCode"];
            }
            set
            {
                ViewState["SpotCheckCode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitService.GetUnit(drpUnit, CurrUser.LoginProjectId, true);
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                GetButtonPower();
                BindData();

            }
        }


        protected DataTable ChecklistData()
        {
            string strSql = @"SELECT chec.SpotCheckCode,chec.ProjectId,chec.UnitId,"
                          + @" chec.DocCode,chec.State2,chec.SpotCheckDate,cn.ProfessionalName,case chec.ControlPointType when 'D' then '非C级' when 'C' then 'C级' else '' end as ControlPointTypeStr,"
                          + @" unit.UnitName,u.userName as CreateMan "
                          + @" FROM Check_SpotCheck chec "
                          + @" left join Base_Unit unit on unit.unitId=chec.UnitId "
                          + @" left join sys_User u on u.userId = chec.CreateMan"
                          + @" left join Base_CNProfessional cn on cn.CNProfessionalId = chec.CNProfessionalCode"
                          + @" where chec.ProjectId=@ProjectId";

            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", CurrUser.LoginProjectId));
            strSql += " AND (chec.SpotCheckDate>=@startTime or @startTime='') and (chec.SpotCheckDate<=@endTime or @endTime='') ";
            listStr.Add(new SqlParameter("@startTime", !string.IsNullOrEmpty(txtStartTime.Text.Trim()) ? txtStartTime.Text.Trim() : ""));
            listStr.Add(new SqlParameter("@endTime", !string.IsNullOrEmpty(txtEndTime.Text.Trim()) ? txtEndTime.Text.Trim() : ""));
            if (drpUnit.SelectedValue != Const._Null)
            {
                strSql += " AND chec.UnitId=@unitId";
                listStr.Add(new SqlParameter("@unitId", drpUnit.SelectedValue));
            }
            strSql += " AND chec.state=@state and IsShow=1";
            listStr.Add(new SqlParameter("@state", Const.SpotCheck_Complete));
            SqlParameter[] parameter = listStr.ToArray();

            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            return tb;
        }


        private void BindData()
        {
            var list = ChecklistData();
            Grid1.RecordCount = list.Rows.Count;
            list = GetFilteredTable(Grid1.FilteredData, list);
            var table = GetPagedDataTable(Grid1, list);
            Grid1.DataSource = table;
            Grid1.DataBind();


        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            //Grid1.PageIndex = e.NewPageIndex;
            BindData();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindData();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        protected string ConvertMan(object JointCheckId)
        {
            string userNames = string.Empty;
            if (JointCheckId != null)
            {
                List<Model.Check_SpotCheckApprove> list = BLL.SpotCheckApproveService.GetSpotApprovesBySpotCheckCode(JointCheckId.ToString());
                foreach (var a in list)
                {
                    if (a != null)
                    {
                        if (a.ApproveMan != null)
                        {
                            if (!userNames.Contains(BLL.UserService.GetUserByUserId(a.ApproveMan).UserName))
                            {
                                userNames += UserService.GetUserByUserId(a.ApproveMan).UserName + ",";
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(userNames))
                {
                    userNames = userNames.Substring(0, userNames.LastIndexOf(","));
                }
            }
            return userNames;
        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindData();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            SpotCheckCode = Grid1.SelectedRowID.Split(',')[0];
            Model.Check_SpotCheck spotCheck = SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);
            var approves = SpotCheckApproveService.GetSpotCheck2ApproveBySpotCheckCode(SpotCheckCode);

            //if (spotCheck.State == Const.SpotCheck_Complete)
            //{
            //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditSportDataCheck.aspx?SpotCheckCode={0}", SpotCheckCode, "编辑 - ")));
            //}
            if (approves != null)
            {
                bool res = false;
                foreach (var item in approves)
                {
                    if (CurrUser.UserId == item.ApproveMan || CurrUser.UserId == Const.sysglyId)
                    {
                        res = true;
                        break;
                    }
                }
                if (res)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EditSportDataCheck.aspx?SpotCheckCode={0}", SpotCheckCode, "编辑 - ")));

                }
                else
                {

                    Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("您不是当前办理人，无法操作！", MessageBoxIcon.Warning);
                return;
            }
        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            Grid1_RowDoubleClick(null, null);
        }

        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            SpotCheckCode = Grid1.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SpotDataCheckView.aspx?SpotCheckCode={0}", SpotCheckCode, "查看 - ")));
        }

        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            SpotCheckCode = Grid1.SelectedRowID.Split(',')[0];
            var code = SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode).DocCode;
            SpotCheckApproveService.DeleteSpotCheckApprovesBySpotCheckCode(SpotCheckCode);
            SpotCheckDetailService.DeleteAllSpotCheckDetail(SpotCheckCode);
            SpotCheckService.DeleteSpotCheck(SpotCheckCode);
            LogService.AddSys_Log(CurrUser, code, SpotCheckCode, Const.SpotDataCheckMenuId, "删除工序验收记录");
            BindData();
            Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
        }

        protected void btnRset_Click(object sender, EventArgs e)
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            drpUnit.SelectedIndex = 0;
            BindData();
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
    }
}