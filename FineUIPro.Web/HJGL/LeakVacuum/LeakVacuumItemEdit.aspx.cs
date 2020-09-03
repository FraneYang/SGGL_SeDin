using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;
namespace FineUIPro.Web.HJGL.LeakVacuum
{
    public partial class LeakVacuumItemEdit :PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string LeakVacuumId
        {
            get
            {
                return (string)ViewState["LeakVacuumId"];
            }
            set
            {
                ViewState["LeakVacuumId"] = value;
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
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.LeakVacuumId = Request.Params["LeakVacuumId"];
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);//单位
                BLL.UnitWorkService.InitUnitWorkDropDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);

                // 建档人
                BLL.UserService.InitUserDropDownList(drpTabler, this.CurrUser.LoginProjectId, true);

                var list = (from x in Funs.DB.HJGL_LV_Pipeline
                            where x.LeakVacuumId == this.LeakVacuumId
                            select x).ToList();
                if (list.Count() > 0)
                {
                    foreach (var infoRow in list)
                    {
                        this.hdPipelinesId.Text += infoRow.PipelineId + ",";
                    }
                }
                if (!string.IsNullOrEmpty(hdPipelinesId.Text))
                {
                    hdPipelinesId.Text = hdPipelinesId.Text.Substring(0, hdPipelinesId.Text.Length - 1);
                }
                this.PageInfoLoad(); ///加载页面 
            }
        }
        #endregion

        #region 加载页面输入保存信息
        /// <summary>
        /// 加载页面输入保存信息
        /// </summary>
        private void PageInfoLoad()
        {
            var leakVacuunManage = BLL.LeakVacuumEditService.GetLeakVacuumByID(this.LeakVacuumId);
            if (leakVacuunManage != null)
            {
                this.txtsysNo.Text = leakVacuunManage.SysNo;
                if (!string.IsNullOrEmpty(leakVacuunManage.UnitId))
                {
                    this.drpUnit.SelectedValue = leakVacuunManage.UnitId;
                }

                if (!string.IsNullOrEmpty(leakVacuunManage.UnitWorkId))
                {
                    drpUnitWork.SelectedValue = leakVacuunManage.UnitWorkId;
                }
                this.txtsysName.Text = leakVacuunManage.SysName;
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", leakVacuunManage.TableDate);
                if (!string.IsNullOrEmpty(leakVacuunManage.Tabler))
                {
                    this.drpTabler.SelectedValue = leakVacuunManage.Tabler;
                }
                this.txtRemark.Text = leakVacuunManage.Remark;
                drpInstallationSpecification.SelectedValue = leakVacuunManage.Check1;
                drpPressureTest.SelectedValue = leakVacuunManage.Check2;
                drpWorkRecord.SelectedValue = leakVacuunManage.Check3;
                drpNDTConform.SelectedValue = leakVacuunManage.Check4;
                drpHotConform.SelectedValue = leakVacuunManage.Check5;
                this.BindGrid(); ////初始化页面
            }
            else
            {

                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

                string unitWorkId = Request.Params["unitWorkId"];
                if (!string.IsNullOrEmpty(unitWorkId))
                {
                    var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(unitWorkId);
                    drpUnit.SelectedValue = w.UnitId;
                    this.drpUnitWork.SelectedValue = w.UnitWorkId;
                }

                if (this.CurrUser.UserId != BLL.Const.sysglyId)
                {
                    this.drpTabler.SelectedValue = this.CurrUser.UserId;
                }
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT  IsoInfo.ProjectId,IsoInfo.UnitWorkId,UnitWork.UnitWorkCode,IsoInfo.PipelineId,IsoInfo.PipelineCode,
                              IsoInfo.UnitId,IsoInfo.DesignPress,IsoInfo.DesignTemperature,IsoInfo.TestPressure,IsoInfo.TestMedium,
                              bs.MediumName,testMedium.MediumName AS TestMediumName,IsoInfo.SingleNumber,
                              IsoInfo.PipingClassId,class.PipingClassCode,IsoList.LV_PipeId,IsoList.LeakVacuumId,IsoList.AmbientTemperature,
                              IsoList.TestMediumTemperature,IsoList.VacuumMedium,
                              (case when IsoList.LeakPressure is null then IsoInfo.LeakPressure else IsoList.LeakPressure end) LeakPressure,
                              (case when IsoList.LeakMedium is null then IsoInfo.LeakMedium else IsoList.LeakMedium end) LeakMedium,
                              (case when IsoList.VacuumPressure is null then IsoInfo.VacuumPressure else IsoList.VacuumPressure end) VacuumPressure
                              FROM dbo.HJGL_Pipeline AS IsoInfo
                              LEFT JOIN WBS_UnitWork AS UnitWork ON IsoInfo.UnitWorkId=UnitWork.UnitWorkId
                              LEFT JOIN dbo.Base_Medium  AS bs ON  bs.MediumId = IsoInfo.MediumId
                              LEFT JOIN dbo.Base_TestMedium  AS testMedium ON testMedium.TestMediumId = IsoInfo.TestMedium
                              LEFT JOIN dbo.Base_PipingClass class ON class.PipingClassId = IsoInfo.PipingClassId
                              LEFT JOIN dbo.HJGL_LV_Pipeline AS IsoList ON  IsoList.PipelineId = IsoInfo.PipelineId
                              WHERE IsoInfo.ProjectId= @ProjectId AND UnitWork.UnitWorkId= @UnitWorkId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.LeakVacuumId))
            {
                strSql += " AND (IsoList.LeakVacuumId IS NULL OR IsoList.LeakVacuumId = @LeakVacuumId)";
                listStr.Add(new SqlParameter("@LeakVacuumId", this.LeakVacuumId));
            }
            else {
                strSql += " AND IsoList.LeakMedium IS NULL";
            }
            listStr.Add(new SqlParameter("@UnitWorkId", this.drpUnitWork.SelectedValue));


            if (!string.IsNullOrEmpty(hdPipelinesId.Text))
            {
                strSql += " And CHARINDEX(IsoInfo.PipelineId,@PipelineId)>0";
                listStr.Add(new SqlParameter("@PipelineId", hdPipelinesId.Text.Trim()));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            // tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            if (Grid1.Rows.Count > 0)
            {
                foreach (JObject mergedRow in Grid1.GetMergedData())
                {
                    int i = mergedRow.Value<int>("index");
                    GridRow row = Grid1.Rows[i];
                    AspNet.DropDownList drpVacuumMedium = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpVacuumMedium");
                    AspNet.HiddenField VacuumMedium = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdVacuumMedium");
                    drpVacuumMedium.Items.AddRange(BLL.Base_TestMediumService.GetTestMediumListItem("3"));
                    Funs.PleaseSelect(drpVacuumMedium);
                    if (!string.IsNullOrEmpty(VacuumMedium.Value))
                    {
                        drpVacuumMedium.SelectedValue = VacuumMedium.Value;
                    }
                    AspNet.DropDownList drpLeakMedium = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpLeakMedium");
                    AspNet.HiddenField LeakMedium = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdLeakMedium");
                    drpLeakMedium.Items.AddRange(BLL.Base_TestMediumService.GetTestMediumListItem("2"));
                    Funs.PleaseSelect(drpLeakMedium);
                    if (!string.IsNullOrEmpty(LeakMedium.Value))
                    {
                        drpLeakMedium.SelectedValue = LeakMedium.Value;
                    }
                }
            }
        }
        #endregion

        #region 下拉框联动事件


        /// <summary>
        /// 单位、单位工程下拉框变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpInstallation_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //BLL.Project_WorkAreaService.InitWorkAreaDropDownList(drpWorkArea, true, this.CurrUser.LoginProjectId, drpInstallation.SelectedValue, drpUnit.SelectedValue, null);
            this.BindGrid();
        }

        #endregion

        protected void btnFind_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdPipelinesId.ClientID)
                + Window1.GetShowReference(String.Format("SelectPipeline.aspx?LeakVacuumId={0}&UnitWorkId={1}&Pipelines={2}", this.LeakVacuumId, this.drpUnitWork.SelectedValue, hdPipelinesId.Text.Trim(), "维护 - ")));
        }

        #region 分页排序
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            this.BindGrid();
        }
        #endregion
        #endregion

        #region 试压包 保存事件
        /// <summary>
        /// 编辑试压包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.LeakVacuumEditMenuId, Const.BtnSave))
            {
                if (BLL.LeakVacuumEditService.IsExistLeakVacuumCode(this.txtsysNo.Text, this.LeakVacuumId, this.CurrUser.LoginProjectId))
                {
                    ShowNotify("试压包编号已存在，请重新录入！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.txtsysName.Text) || this.drpUnit.SelectedValue == BLL.Const._Null || this.drpTabler.SelectedValue == BLL.Const._Null
                    || this.drpUnitWork.SelectedValue == BLL.Const._Null || string.IsNullOrEmpty(this.txtTableDate.Text))
                {
                    ShowNotify("必填项不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(hdPipelinesId.Text))
                {
                    ShowNotify("请选择管线号！", MessageBoxIcon.Warning);
                    return;
                }
                var updateleakVacuun = BLL.LeakVacuumEditService.GetLeakVacuumByID(this.LeakVacuumId);
                if (updateleakVacuun != null && !string.IsNullOrEmpty( updateleakVacuun.AduditDate))
                {
                    ShowNotify("此泄露性/真空试验包已审核不能修改！", MessageBoxIcon.Warning);
                    return;
                }

                Model.HJGL_LV_LeakVacuum LeakVacuum = new Model.HJGL_LV_LeakVacuum();
                LeakVacuum.ProjectId = this.CurrUser.LoginProjectId;
                if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
                {
                    LeakVacuum.UnitWorkId = this.drpUnitWork.SelectedValue;
                }
                if (this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    LeakVacuum.UnitId = this.drpUnit.SelectedValue;
                }

                LeakVacuum.SysNo = this.txtsysNo.Text.Trim();
                LeakVacuum.SysName = this.txtsysName.Text.Trim();

                if (this.drpTabler.SelectedValue != BLL.Const._Null)
                {
                    LeakVacuum.Tabler = this.drpTabler.SelectedValue;
                }
                LeakVacuum.TableDate = Funs.GetNewDateTime(this.txtTableDate.Text);
                LeakVacuum.Check1 = drpInstallationSpecification.SelectedValue;
                LeakVacuum.Check2 = drpPressureTest.SelectedValue;
                LeakVacuum.Check3 = drpWorkRecord.SelectedValue;
                LeakVacuum.Check4 = drpNDTConform.SelectedValue;
                LeakVacuum.Check5 = drpHotConform.SelectedValue;
                if (!string.IsNullOrEmpty(this.LeakVacuumId))
                {
                    LeakVacuum.LeakVacuumId = this.LeakVacuumId;
                    BLL.LeakVacuumEditService.UpdateLeakVacuum(LeakVacuum);
                    BLL.LeakVacuumEditService.DeletePipelineListByLeakVacuumId(LeakVacuumId);
                }
                else
                {
                    LeakVacuum.LeakVacuumId = SQLHelper.GetNewID(typeof(Model.HJGL_LV_LeakVacuum));
                    this.LeakVacuumId = LeakVacuum.LeakVacuumId;
                    BLL.LeakVacuumEditService.AddLeakVacuum(LeakVacuum);
                }
                ///保存明细
                var getViewList = this.CollectGridInfo();
                foreach (var item in getViewList)
                {
                    Model.HJGL_LV_Pipeline newitem = new Model.HJGL_LV_Pipeline();
                    newitem.LeakVacuumId = this.LeakVacuumId;
                    newitem.PipelineId = item.PipelineId;
                    newitem.DesignPress = item.DesignPress;
                    newitem.DesignTemperature = item.DesignTemperature;
                    newitem.AmbientTemperature = item.AmbientTemperature;
                    newitem.LeakPressure = item.LeakPressure;
                    if (item.LeakMedium != "0")
                    {
                        newitem.LeakMedium = item.LeakMedium;
                    }
                    newitem.VacuumPressure = item.VacuumPressure;
                    if (item.VacuumMedium != "0")
                    {
                        newitem.VacuumMedium = item.VacuumMedium;
                    }
                    newitem.TestMediumTemperature = item.TestMediumTemperature;
                    var PipelineList = Funs.DB.HJGL_LV_Pipeline.FirstOrDefault(x => x.LeakVacuumId == item.LeakVacuumId && x.PipelineId == item.PipelineId);
                    if (PipelineList != null)
                    {
                        newitem.LV_PipeId = PipelineList.LV_PipeId;
                        BLL.LeakVacuumEditService.UpdatePipelineList(newitem);
                    }
                    else
                    {
                        BLL.LeakVacuumEditService.AddPipelineList(newitem);
                    }
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.LeakVacuumId)
                  + ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        private List<Model.HJGL_LV_Pipeline> CollectGridInfo()
        {
            List<Model.HJGL_LV_Pipeline> getViewList = new List<Model.HJGL_LV_Pipeline>();
            JArray teamGroupData = Grid1.GetMergedData();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                Model.HJGL_LV_Pipeline newView = new Model.HJGL_LV_Pipeline();
                newView.LeakVacuumId = this.LeakVacuumId;
                newView.PipelineId = Grid1.DataKeys[i][0].ToString();
                if (!string.IsNullOrEmpty(values.Value<string>("DesignPress").ToString()))
                {
                    newView.DesignPress = Funs.GetNewDecimal(values.Value<string>("DesignPress").ToString());
                }
                if (!string.IsNullOrEmpty(values.Value<string>("DesignTemperature").ToString()))
                {
                    newView.DesignTemperature = Funs.GetNewDecimal(values.Value<string>("DesignTemperature").ToString());
                }
                if (!string.IsNullOrEmpty((values.Value<string>("AmbientTemperature").ToString())))
                {
                    newView.AmbientTemperature = Funs.GetNewDecimal(values.Value<string>("AmbientTemperature").ToString());
                }
                if (!string.IsNullOrEmpty((values.Value<string>("LeakPressure").ToString())))
                {
                    newView.LeakPressure = Funs.GetNewDecimal(values.Value<string>("LeakPressure").ToString());
                }
                System.Web.UI.WebControls.DropDownList LeakMedium = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpLeakMedium"));
                if (LeakMedium.SelectedValue!=BLL.Const._Null)
                {
                    newView.LeakMedium = LeakMedium.SelectedValue;
                }
                if (!string.IsNullOrEmpty((values.Value<string>("VacuumPressure").ToString())))
                {
                    newView.VacuumPressure = Funs.GetNewDecimal(values.Value<string>("VacuumPressure").ToString());
                }
                System.Web.UI.WebControls.DropDownList VacuumMedium = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpVacuumMedium"));
                if (VacuumMedium.SelectedValue != BLL.Const._Null)
                {
                    newView.VacuumMedium = VacuumMedium.SelectedValue;
                }
                if (!string.IsNullOrEmpty(values.Value<string>("TestMediumTemperature")))
                {
                    newView.TestMediumTemperature = Funs.GetNewDecimal(values.Value<string>("TestMediumTemperature"));
                }
                getViewList.Add(newView);
            }

            return getViewList;
        }
        #endregion
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
        }

        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录", MessageBoxIcon.Warning);
                return;
            }
            string Pipelines = string.Empty;
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                if (!string.IsNullOrEmpty(this.LeakVacuumId))
                {
                    var getPipelineList = Funs.DB.HJGL_LV_Pipeline.FirstOrDefault(x => x.LeakVacuumId == this.LeakVacuumId && x.PipelineId == rowID);
                    if (getPipelineList != null)
                    {
                        Funs.DB.HJGL_LV_Pipeline.DeleteOnSubmit(getPipelineList);
                        Funs.DB.SubmitChanges();
                    }
                }
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string ID = Grid1.DataKeys[i][0].ToString();
                    if (rowID != ID)
                    {
                        Pipelines += ID + ",";
                    }
                }
            }
            if (Pipelines != string.Empty)
            {
                this.hdPipelinesId.Text = string.Empty;
                Pipelines = Pipelines.Substring(0, Pipelines.Length - 1);
                this.hdPipelinesId.Text = Pipelines;
            }
            this.BindGrid();
        }
    }
}