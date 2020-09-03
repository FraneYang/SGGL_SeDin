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
namespace FineUIPro.Web.HJGL.PurgingCleaning
{
    public partial class PurgingCleaningItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PurgingCleaningId
        {
            get
            {
                return (string)ViewState["PurgingCleaningId"];
            }
            set
            {
                ViewState["PurgingCleaningId"] = value;
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
                this.PurgingCleaningId = Request.Params["PurgingCleaningId"];
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);//单位
                BLL.UnitWorkService.InitUnitWorkDropDownList(drpUnitWork, this.CurrUser.LoginProjectId, true);

                // 建档人
                BLL.UserService.InitUserDropDownList(drpTabler, this.CurrUser.LoginProjectId, true);

                var list = (from x in Funs.DB.HJGL_PC_Pipeline
                            where x.PurgingCleaningId == this.PurgingCleaningId
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
            var PurgingCleaningManage = BLL.PurgingCleaningEditService.GetPurgingCleaningByID(this.PurgingCleaningId);
            if (PurgingCleaningManage != null)
            {
                this.txtsysNo.Text = PurgingCleaningManage.SysNo;
                if (!string.IsNullOrEmpty(PurgingCleaningManage.UnitId))
                {
                    this.drpUnit.SelectedValue = PurgingCleaningManage.UnitId;
                }

                if (!string.IsNullOrEmpty(PurgingCleaningManage.UnitWorkId))
                {
                    drpUnitWork.SelectedValue = PurgingCleaningManage.UnitWorkId;
                }
                this.txtsysName.Text = PurgingCleaningManage.SysName;
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", PurgingCleaningManage.TableDate);
                if (!string.IsNullOrEmpty(PurgingCleaningManage.Tabler))
                {
                    this.drpTabler.SelectedValue = PurgingCleaningManage.Tabler;
                }
                this.txtRemark.Text = PurgingCleaningManage.Remark;
                drpInstallationSpecification.SelectedValue = PurgingCleaningManage.Check1;
                drpPressureTest.SelectedValue = PurgingCleaningManage.Check2;
                drpWorkRecord.SelectedValue = PurgingCleaningManage.Check3;
                drpNDTConform.SelectedValue = PurgingCleaningManage.Check4;
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
            string strSql = @"SELECT IsoInfo.ProjectId,IsoInfo.UnitWorkId,UnitWork.UnitWorkCode,IsoInfo.PipelineId,IsoInfo.PipelineCode,
                                     IsoInfo.UnitId,IsoInfo.TestPressure,IsoInfo.TestMedium,
		                             bs.MediumName,testMedium.MediumName AS TestMediumName,IsoInfo.SingleNumber,IsoInfo.PipingClassId,
		                             class.PipingClassCode,IsoList.PC_PipeId,IsoList.PurgingCleaningId,IsoList.MaterialId,IsoList.MediumId,
                                     (case when IsoList.PurgingMedium is null then (case when IsoInfo.PCtype='1' then IsoInfo.PCMedium else null end)  else IsoList.PurgingMedium end) PurgingMedium,
                                     (case when IsoList.CleaningMedium is null then (case when IsoInfo.PCtype='2' then IsoInfo.PCMedium else null end) else   IsoList.CleaningMedium end) CleaningMedium
                                     FROM dbo.HJGL_Pipeline AS IsoInfo
                                     LEFT JOIN WBS_UnitWork AS UnitWork ON IsoInfo.UnitWorkId=UnitWork.UnitWorkId
                                     LEFT JOIN dbo.Base_Medium  AS bs ON  bs.MediumId = IsoInfo.MediumId
								     LEFT JOIN dbo.Base_TestMedium  AS testMedium ON testMedium.TestMediumId = IsoInfo.TestMedium
								     LEFT JOIN dbo.Base_PipingClass class ON class.PipingClassId = IsoInfo.PipingClassId
                                     LEFT JOIN dbo.HJGL_PC_Pipeline AS IsoList ON  IsoList.PipelineId = IsoInfo.PipelineId
                                     WHERE IsoInfo.ProjectId= @ProjectId AND UnitWork.UnitWorkId= @UnitWorkId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.PurgingCleaningId))
            {
                strSql += " AND (IsoList.PurgingCleaningId IS NULL OR IsoList.PurgingCleaningId = @PurgingCleaningId)";
                listStr.Add(new SqlParameter("@PurgingCleaningId", this.PurgingCleaningId));
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
                    //材质
                    AspNet.DropDownList drpMaterialId = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpMaterialId");
                    AspNet.HiddenField MaterialId = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdMaterialId");
                    drpMaterialId.Items.AddRange(BLL.Base_MaterialService.GetMaterialListItem());
                    Funs.PleaseSelect(drpMaterialId);
                    if (!string.IsNullOrEmpty(MaterialId.Value))
                    {
                        drpMaterialId.SelectedValue = MaterialId.Value;
                    }
                    //操作介质
                    AspNet.DropDownList drpMediumId = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpMediumId");
                    AspNet.HiddenField MediumId = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdMediumId");
                    drpMediumId.Items.AddRange(BLL.Base_MediumService.GetMediumListItem(this.CurrUser.LoginProjectId));
                    Funs.PleaseSelect(drpMediumId);
                    if (!string.IsNullOrEmpty(MediumId.Value))
                    {
                        drpMediumId.SelectedValue = MediumId.Value;
                    }
                    //吹扫
                    AspNet.DropDownList drpPurgingMedium = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpPurgingMedium");
                    AspNet.HiddenField PurgingMedium = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdPurgingMedium");
                    drpPurgingMedium.Items.AddRange(BLL.Base_TestMediumService.GetTestMediumListItem("4"));
                    Funs.PleaseSelect(drpPurgingMedium);
                    if (!string.IsNullOrEmpty(PurgingMedium.Value))
                    {
                        drpPurgingMedium.SelectedValue = PurgingMedium.Value;
                    }
                    //清洗
                    AspNet.DropDownList drpCleaningMedium = (AspNet.DropDownList)Grid1.Rows[i].FindControl("drpCleaningMedium");
                    AspNet.HiddenField CleaningMedium = (AspNet.HiddenField)Grid1.Rows[i].FindControl("hdCleaningMedium");
                    drpCleaningMedium.Items.AddRange(BLL.Base_TestMediumService.GetTestMediumListItem("5"));
                    Funs.PleaseSelect(drpCleaningMedium);
                    if (!string.IsNullOrEmpty(CleaningMedium.Value))
                    {
                        drpCleaningMedium.SelectedValue = CleaningMedium.Value;
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
                + Window1.GetShowReference(String.Format("SelectPipeline.aspx?PurgingCleaningId={0}&UnitWorkId={1}&Pipelines={2}", this.PurgingCleaningId, this.drpUnitWork.SelectedValue, hdPipelinesId.Text.Trim(), "维护 - ")));
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

        #region 吹扫清洗试验包 保存事件
        /// <summary>
        /// 编辑吹扫清洗试验包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.PurgingCleaningEditMenuId, Const.BtnSave))
            {
                if (BLL.PurgingCleaningEditService.IsExistPurgingCleaningCode(this.txtsysNo.Text, this.PurgingCleaningId, this.CurrUser.LoginProjectId))
                {
                    ShowNotify("吹扫清洗试验包编号已存在，请重新录入！", MessageBoxIcon.Warning);
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
                var updatePurgingCleaning = BLL.PurgingCleaningEditService.GetPurgingCleaningByID(this.PurgingCleaningId);
                if (updatePurgingCleaning != null && !string.IsNullOrEmpty( updatePurgingCleaning.AduditDate))
                {
                    ShowNotify("此泄露性/真空试验包已审核不能修改！", MessageBoxIcon.Warning);
                    return;
                }

                Model.HJGL_PC_PurgingCleaning PurgingCleaning = new Model.HJGL_PC_PurgingCleaning();
                PurgingCleaning.ProjectId = this.CurrUser.LoginProjectId;
                if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
                {
                    PurgingCleaning.UnitWorkId = this.drpUnitWork.SelectedValue;
                }
                if (this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    PurgingCleaning.UnitId = this.drpUnit.SelectedValue;
                }

                PurgingCleaning.SysNo = this.txtsysNo.Text.Trim();
                PurgingCleaning.SysName = this.txtsysName.Text.Trim();

                if (this.drpTabler.SelectedValue != BLL.Const._Null)
                {
                    PurgingCleaning.Tabler = this.drpTabler.SelectedValue;
                }
                PurgingCleaning.TableDate = Funs.GetNewDateTime(this.txtTableDate.Text);
                PurgingCleaning.Check1 = drpInstallationSpecification.SelectedValue;
                PurgingCleaning.Check2 = drpPressureTest.SelectedValue;
                PurgingCleaning.Check3 = drpWorkRecord.SelectedValue;
                PurgingCleaning.Check4 = drpNDTConform.SelectedValue;
                if (!string.IsNullOrEmpty(this.PurgingCleaningId))
                {
                    PurgingCleaning.PurgingCleaningId = this.PurgingCleaningId;
                    BLL.PurgingCleaningEditService.UpdatePurgingCleaning(PurgingCleaning);
                    BLL.PurgingCleaningEditService.DeletePipelineListByPurgingCleaningId(PurgingCleaningId);
                }
                else
                {
                    PurgingCleaning.PurgingCleaningId = SQLHelper.GetNewID(typeof(Model.HJGL_PC_PurgingCleaning));
                    this.PurgingCleaningId = PurgingCleaning.PurgingCleaningId;
                    BLL.PurgingCleaningEditService.AddPurgingCleaning(PurgingCleaning);
                }
                ///保存明细
                var getViewList = this.CollectGridInfo();
                foreach (var item in getViewList)
                {
                    Model.HJGL_PC_Pipeline newitem = new Model.HJGL_PC_Pipeline();
                    newitem.PurgingCleaningId = this.PurgingCleaningId;
                    newitem.PipelineId = item.PipelineId;
                    if (item.MaterialId != "0")
                    {
                        newitem.MaterialId = item.MaterialId;
                    }
                    if (item.MediumId != "0")
                    {
                        newitem.MediumId = item.MediumId;
                    }
                    if (item.PurgingMedium != "0")
                    {
                        newitem.PurgingMedium = item.PurgingMedium;
                    }
                    if (item.CleaningMedium != "0")
                    {
                        newitem.CleaningMedium = item.CleaningMedium;
                    }
                    var PipelineList = Funs.DB.HJGL_PC_Pipeline.FirstOrDefault(x => x.PurgingCleaningId == item.PurgingCleaningId && x.PipelineId == item.PipelineId);
                    if (PipelineList != null)
                    {
                        newitem.PC_PipeId = PipelineList.PC_PipeId;
                        BLL.PurgingCleaningEditService.UpdatePipelineList(newitem);
                    }
                    else
                    {
                        BLL.PurgingCleaningEditService.AddPipelineList(newitem);
                    }
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.PurgingCleaningId)
                  + ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        private List<Model.HJGL_PC_Pipeline> CollectGridInfo()
        {
            List<Model.HJGL_PC_Pipeline> getViewList = new List<Model.HJGL_PC_Pipeline>();
            JArray teamGroupData = Grid1.GetMergedData();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                JObject values = mergedRow.Value<JObject>("values");
                Model.HJGL_PC_Pipeline newView = new Model.HJGL_PC_Pipeline();
                newView.PurgingCleaningId = this.PurgingCleaningId;
                newView.PipelineId = Grid1.DataKeys[i][0].ToString();
                System.Web.UI.WebControls.DropDownList drpMaterialId = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpMaterialId"));
                if (drpMaterialId.SelectedValue != BLL.Const._Null)
                {
                    newView.MaterialId = drpMaterialId.SelectedValue;
                }
                System.Web.UI.WebControls.DropDownList drpMediumId = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpMediumId"));
                if (drpMediumId.SelectedValue != BLL.Const._Null)
                {
                    newView.MediumId = drpMediumId.SelectedValue;
                }
                System.Web.UI.WebControls.DropDownList drpPurgingMedium = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpPurgingMedium"));
                if (drpPurgingMedium.SelectedValue != BLL.Const._Null)
                {
                    newView.PurgingMedium = drpPurgingMedium.SelectedValue;
                }
                System.Web.UI.WebControls.DropDownList drpCleaningMedium = (System.Web.UI.WebControls.DropDownList)(Grid1.Rows[i].FindControl("drpCleaningMedium"));
                if (drpCleaningMedium.SelectedValue != BLL.Const._Null)
                {
                    newView.CleaningMedium = drpCleaningMedium.SelectedValue;
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
                if (!string.IsNullOrEmpty(this.PurgingCleaningId))
                {
                    var getPipelineList = Funs.DB.HJGL_PC_Pipeline.FirstOrDefault(x => x.PurgingCleaningId == this.PurgingCleaningId && x.PipelineId == rowID);
                    if (getPipelineList != null)
                    {
                        Funs.DB.HJGL_PC_Pipeline.DeleteOnSubmit(getPipelineList);
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