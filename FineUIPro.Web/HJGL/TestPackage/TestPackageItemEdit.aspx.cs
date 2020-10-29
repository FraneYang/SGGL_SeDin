using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class TestPackageItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string PTP_ID
        {
            get
            {
                return (string)ViewState["PTP_ID"];
            }
            set
            {
                ViewState["PTP_ID"] = value;
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
                this.PTP_ID = Request.Params["PTP_ID"];
                //List<Model.Base_Unit> units = new List<Model.Base_Unit>();
                //var pUnit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId);

                //if (pUnit == null || pUnit.UnitType == BLL.Const.ProjectUnitType_1 || pUnit.UnitType == BLL.Const.ProjectUnitType_2
                //  || pUnit.UnitType == BLL.Const.ProjectUnitType_5)
                //{
                //    units = (from x in Funs.DB.Base_Unit
                //             join y in Funs.DB.Project_ProjectUnit on x.UnitId equals y.UnitId
                //             where y.ProjectId == this.CurrUser.LoginProjectId && y.UnitType.Contains(BLL.Const.ProjectUnitType_2)
                //             select x).ToList();
                //}
                //else
                //{
                //    units.Add(BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId));
                //}
                /////单位
                //this.drpUnit.DataTextField = "UnitName";
                //this.drpUnit.DataValueField = "UnitId";
                //this.drpUnit.DataSource = units;
                //this.drpUnit.DataBind();
                //Funs.FineUIPleaseSelect(this.drpUnit);
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);//单位
                BLL.UnitWorkService.InitUnitWorkDropDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);

                var list = (from x in Funs.DB.PTP_PipelineList
                            where x.PTP_ID == this.PTP_ID
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
                if (!string.IsNullOrEmpty(Request.Params["type"]))
                {
                    this.btnFind.Hidden = true;
                    this.btnSave.Hidden = true;
                }
            }
        }
        #endregion

        #region 加载页面输入保存信息
        /// <summary>
        /// 加载页面输入保存信息
        /// </summary>
        private void PageInfoLoad()
        {
            var testPackageManage = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
            if (testPackageManage != null)
            {
                this.txtTestPackageNo.Text = testPackageManage.TestPackageNo;
                if (!string.IsNullOrEmpty(testPackageManage.UnitId))
                {
                    this.drpUnit.SelectedValue = testPackageManage.UnitId;
                }

                if (!string.IsNullOrEmpty(testPackageManage.UnitWorkId))
                {
                    //BLL.UnitWorkService.InitUnitWorkDropDownList(drpUnitWork, this.CurrUser.LoginProjectId,true);
                    drpUnitWork.SelectedValue = testPackageManage.UnitWorkId;
                    //var UnitWork = this.drpUnitWork.Items.FirstOrDefault(x => x.Value == testPackageManage.UnitWorkId);
                }
                this.txtTestPackageName.Text = testPackageManage.TestPackageName;
                this.txtRemark.Text = testPackageManage.Remark;
                this.txtadjustTestPressure.Text = testPackageManage.AdjustTestPressure;
                this.BindGrid(); ////初始化页面
            }
            else
            {
                string unitWorkId = Request.Params["unitWorkId"];
                if (!string.IsNullOrEmpty(unitWorkId))
                {
                    var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(unitWorkId);
                    drpUnit.SelectedValue = w.UnitId;
                    this.drpUnitWork.SelectedValue = w.UnitWorkId;
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
            string strSql = @"SELECT IsoInfo.ProjectId,IsoInfo.UnitWorkId,UnitWork.UnitWorkCode,IsoInfo.PipelineId,IsoInfo.PipelineCode,
                                     IsoInfo.UnitId,IsoInfo.DesignPress,IsoInfo.DesignTemperature,IsoInfo.TestPressure,IsoInfo.TestMedium,
		                             testMedium.MediumName ,IsoInfo.SingleNumber,IsoInfo.PipingClassId,IsoList.PT_PipeId,
		                             IsoList.PTP_ID
                                     FROM dbo.HJGL_Pipeline AS IsoInfo
                                     LEFT JOIN WBS_UnitWork AS UnitWork ON IsoInfo.UnitWorkId=UnitWork.UnitWorkId
								     LEFT JOIN dbo.Base_TestMedium  AS testMedium ON testMedium.TestMediumId = IsoInfo.TestMedium
                                     LEFT JOIN dbo.PTP_PipelineList AS IsoList ON  IsoList.PipelineId = IsoInfo.PipelineId
                                     WHERE IsoInfo.ProjectId= @ProjectId 
				                    AND UnitWork.UnitWorkId= @UnitWorkId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.PTP_ID))
            {
                strSql += " AND (IsoList.PTP_ID IS NULL OR IsoList.PTP_ID = @PTP_ID)";
                listStr.Add(new SqlParameter("@PTP_ID", this.PTP_ID));
            }
            else
            {
                strSql += " AND IsoList.PTP_ID IS NULL";
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
                + Window1.GetShowReference(String.Format("SelectPipeline.aspx?PTP_ID={0}&UnitWorkId={1}&Pipelines={2}", this.PTP_ID, this.drpUnitWork.SelectedValue, hdPipelinesId.Text.Trim(), "维护 - ")));
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
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnSave))
            {
                if (BLL.TestPackageEditService.IsExistTestPackageCode(this.txtTestPackageNo.Text, this.PTP_ID, this.CurrUser.LoginProjectId))
                {
                    ShowNotify("试压包编号已存在，请重新录入！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.txtTestPackageNo.Text) || this.drpUnit.SelectedValue == BLL.Const._Null ||  this.drpUnitWork.SelectedValue == BLL.Const._Null )
                {
                    ShowNotify("必填项不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(hdPipelinesId.Text))
                {
                    ShowNotify("请选择管线号！", MessageBoxIcon.Warning);
                    return;
                }
                var updatetrust = BLL.TestPackageEditService.GetTestPackageByID(this.PTP_ID);
                if (updatetrust != null && updatetrust.AduditDate.HasValue)
                {
                    ShowNotify("此施压包已审核不能修改！", MessageBoxIcon.Warning);
                    return;
                }

                Model.PTP_TestPackage testPackage = new Model.PTP_TestPackage();
                testPackage.ProjectId = this.CurrUser.LoginProjectId;
                if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
                {
                    testPackage.UnitWorkId = this.drpUnitWork.SelectedValue;
                }
                if (this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    testPackage.UnitId = this.drpUnit.SelectedValue;
                }

                testPackage.TestPackageNo = this.txtTestPackageNo.Text.Trim();
                testPackage.TestPackageName = this.txtTestPackageName.Text.Trim();
                testPackage.Tabler = this.CurrUser.UserId;
                testPackage.TableDate = DateTime.Now;
                testPackage.Remark = this.txtRemark.Text.Trim();
                testPackage.AdjustTestPressure = this.txtadjustTestPressure.Text.Trim();
                if (!string.IsNullOrEmpty(this.PTP_ID))
                {
                    testPackage.PTP_ID = this.PTP_ID;
                    BLL.TestPackageEditService.UpdateTestPackage(testPackage);
                    BLL.TestPackageEditService.DeletePipelineListByPTP_ID(PTP_ID);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnModify, this.PTP_ID);
                }
                else

                {
                    testPackage.PTP_ID = SQLHelper.GetNewID(typeof(Model.PTP_TestPackage));
                    this.PTP_ID = testPackage.PTP_ID;
                    BLL.TestPackageEditService.AddTestPackage(testPackage);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_6, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TestPackageEditMenuId, Const.BtnAdd, this.PTP_ID);
                }
                ///保存明细
                var getViewList = this.CollectGridInfo();
                foreach (var item in getViewList)
                {
                    Model.PTP_PipelineList newitem = new Model.PTP_PipelineList();
                    newitem.PTP_ID = this.PTP_ID;
                    newitem.PipelineId = item.PipelineId;
                    newitem.DesignPress = item.DesignPress;
                    newitem.DesignTemperature = item.DesignTemperature;
                    newitem.TestMedium = item.TestMedium;
                    newitem.TestPressure = item.TestPressure;
                    var PipelineList = Funs.DB.PTP_PipelineList.FirstOrDefault(x => x.PTP_ID == item.PTP_ID && x.PipelineId == item.PipelineId);
                    if (PipelineList != null)
                    {
                        newitem.PT_PipeId = PipelineList.PT_PipeId;
                        BLL.TestPackageEditService.UpdatePipelineList(newitem);
                    }
                    else
                    {
                        BLL.TestPackageEditService.AddPipelineList(newitem);
                    }
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.PTP_ID)
                  + ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        private List<Model.PTP_PipelineList> CollectGridInfo()
        {
            List<Model.PTP_PipelineList> getViewList = new List<Model.PTP_PipelineList>();
            JArray teamGroupData = Grid1.GetMergedData();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                Model.PTP_PipelineList newView = new Model.PTP_PipelineList();
                newView.PTP_ID = this.PTP_ID;
                newView.PipelineId = Grid1.DataKeys[i][0].ToString();
                newView.TestMedium = Grid1.DataKeys[i][1].ToString();
                if (!string.IsNullOrEmpty(values.Value<string>("DesignPress").ToString()))
                {
                    newView.DesignPress = Funs.GetNewDecimal(values.Value<string>("DesignPress").ToString());
                }
                if (!string.IsNullOrEmpty(values.Value<string>("DesignTemperature").ToString()))
                {
                    newView.DesignTemperature = Funs.GetNewDecimal(values.Value<string>("DesignTemperature").ToString());
                }
                if (!string.IsNullOrEmpty(values.Value<string>("TestPressure").ToString()))
                {
                    newView.TestPressure = Funs.GetNewDecimal(values.Value<string>("TestPressure").ToString());
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
                if (!string.IsNullOrEmpty(this.PTP_ID))
                {
                    var getPipelineList = Funs.DB.PTP_PipelineList.FirstOrDefault(x => x.PTP_ID == this.PTP_ID && x.PipelineId == rowID);
                    if (getPipelineList != null)
                    {
                        Funs.DB.PTP_PipelineList.DeleteOnSubmit(getPipelineList);
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