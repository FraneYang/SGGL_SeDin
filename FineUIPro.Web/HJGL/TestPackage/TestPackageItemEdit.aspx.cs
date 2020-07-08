using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

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
        
        /// <summary>
        /// 选择字符串
        /// </summary>
        public List<string> listSelects
        {
            get
            {
                return (List<string>)ViewState["listSelects"];
            }
            set
            {
                ViewState["listSelects"] = value;
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
                //    units = (from x in new Model.SGGLDB(Funs.ConnString).Base_Unit
                //             join y in new Model.SGGLDB(Funs.ConnString).Project_ProjectUnit on x.UnitId equals y.UnitId
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

                // 修改人
                BLL.UserService.InitUserDropDownList(drpModifier, this.CurrUser.LoginProjectId, true);
                // 建档人
                BLL.UserService.InitUserDropDownList(drpTabler, this.CurrUser.LoginProjectId, true);

                // 试压类型
                BLL.Base_PressureService.InitPressureDropDownList(drpTestType, true);

                // 试验介质
                BLL.Base_MediumService.InitMediumDropDownList(drpTestMedium,this.CurrUser.LoginProjectId, true, true);
                // 管线等级
                BLL.Base_PipingClassService.InitPipingClassDropDownList(drpPipingClass, this.CurrUser.LoginProjectId, true, "请选择");

                listSelects = new List<string>();
                var list = (from x in new Model.SGGLDB(Funs.ConnString).PTP_PipelineList
                            where x.PTP_ID == this.PTP_ID
                            select x).ToList();
                if (list.Count() > 0)
                {
                    foreach (var infoRow in list)
                    {
                        listSelects.Add(infoRow.PipelineId);
                    }
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
                //this.txtTestPackageCode.Text = testPackageManage.TestPackageCode;
                if (!string.IsNullOrEmpty(testPackageManage.TestType))
                {
                    this.drpTestType.SelectedValue = testPackageManage.TestType;
                }
                this.txtTestService.Text = testPackageManage.TestService;
                this.txtTestHeat.Text = testPackageManage.TestHeat;
                this.txtTestAmbientTemp.Text = testPackageManage.TestAmbientTemp;
                this.txtTestMediumTemp.Text = testPackageManage.TestMediumTemp;
                this.txtVacuumTestService.Text = testPackageManage.VacuumTestService;
                this.txtVacuumTestPressure.Text = testPackageManage.VacuumTestPressure;
                this.txtTightnessTestTime.Text = testPackageManage.TightnessTestTime;
                this.txtTightnessTestTemp.Text = testPackageManage.TightnessTestTemp;
                this.txtTightnessTest.Text = testPackageManage.TightnessTest;
                this.txtTestPressure.Text = testPackageManage.TestPressure;
                this.txtTestPressureTemp.Text = testPackageManage.TestPressureTemp;
                this.txtTestPressureTime.Text = testPackageManage.TestPressureTime;
                this.txtOperationMedium.Text = testPackageManage.OperationMedium;
                this.txtPurgingMedium.Text = testPackageManage.PurgingMedium;
                this.txtCleaningMedium.Text = testPackageManage.CleaningMedium;
                this.txtLeakageTestService.Text = testPackageManage.LeakageTestService;
                this.txtLeakageTestPressure.Text = testPackageManage.LeakageTestPressure;
                this.txtAllowSeepage.Text = testPackageManage.AllowSeepage;
                this.txtFactSeepage.Text = testPackageManage.FactSeepage;
                this.txtModifyDate.Text = string.Format("{0:yyyy-MM-dd}", testPackageManage.ModifyDate);
                if (!string.IsNullOrEmpty(testPackageManage.Modifier))
                {
                    this.drpModifier.SelectedValue = testPackageManage.Modifier;
                }
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", testPackageManage.TableDate);
                if (!string.IsNullOrEmpty(testPackageManage.Tabler))
                {
                    this.drpTabler.SelectedValue = testPackageManage.Tabler;
                }
                this.txtRemark.Text = testPackageManage.Remark;

                this.BindGrid(); ////初始化页面
                this.ShowGridItem();
            }
            else
            {
                this.txtModifyDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
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
                    this.drpModifier.SelectedValue = this.CurrUser.UserId;
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
		                             bs.MediumName,testMedium.MediumName AS TestMediumName,IsoInfo.SingleNumber,
		                             IsoInfo.PipingClassId,class.PipingClassCode,IsoList.PT_PipeId,IsoList.PTP_ID
                                FROM dbo.HJGL_Pipeline AS IsoInfo
                                LEFT JOIN WBS_UnitWork AS UnitWork ON IsoInfo.UnitWorkId=UnitWork.UnitWorkId
                                LEFT JOIN dbo.Base_Medium  AS bs ON  bs.MediumId = IsoInfo.MediumId
								LEFT JOIN dbo.Base_Medium  AS testMedium ON testMedium.MediumId = IsoInfo.TestMedium
								LEFT JOIN dbo.Base_PipingClass class ON class.PipingClassId = IsoInfo.PipingClassId
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
            
            
            if (this.drpPipingClass.SelectedValue != Const._Null)
            {
                strSql += " AND IsoInfo.PipingClassId = @PipingClassId";
                listStr.Add(new SqlParameter("@PipingClassId", this.drpPipingClass.SelectedValue));
            }
            if (this.drpTestMedium.SelectedValue != Const._Null)
            {
                strSql += " AND IsoInfo.MediumId = @MediumId";
                listStr.Add(new SqlParameter("@MediumId", this.drpTestMedium.SelectedValue));
            }

            if (!string.IsNullOrEmpty(numTestPressure.Text) && !string.IsNullOrEmpty(numTo.Text))
            {
                strSql += " AND IsoInfo.TestPressure >=@MinTestPressure AND IsoInfo.TestPressure <=@MaxTestPressure";
                listStr.Add(new SqlParameter("@MinTestPressure", Convert.ToDecimal(numTestPressure.Text)));
                listStr.Add(new SqlParameter("@MaxTestPressure", Convert.ToDecimal(numTo.Text)));
            }
           
            //if (this.drpWorkArea.SelectedValueArray.Count() > 1 || (this.drpWorkArea.SelectedValueArray.Count() == 1 && this.drpWorkArea.SelectedValueArray[0] != BLL.Const._Null))
            //{
            //    strSql += " AND (IsoInfo.WorkAreaId = '' ";
            //    int i = 0;
            //    foreach (var item in this.drpWorkArea.SelectedValueArray)
            //    {
            //        if (item != BLL.Const._Null)
            //        {
            //            strSql += " OR IsoInfo.WorkAreaId = @WorkAreaId" + i.ToString();
            //            listStr.Add(new SqlParameter("@WorkAreaId" + i.ToString(), item));
            //            i++;
            //        }
            //    }
            //    strSql += ")";
            //}

            //if (!string.IsNullOrEmpty(this.txtIsono.Text.Trim()))
            //{
            //    strSql += " AND IsoInfo.PipelineCode LIKE @PipelineCode";
            //    listStr.Add(new SqlParameter("@PipelineCode", "%" + this.txtIsono.Text.Trim() + "%"));
            //}

            //if (this.ckSelect.Checked) ///只显示选中项
            //{
            //    if (this.listSelects.Count() > 0)
            //    {
            //        strSql += " AND (ISO_IsoNo.ISO_ID = @ISO_ID ";
            //        listStr.Add(new SqlParameter("@ISO_ID", ""));
            //        int i = 0;
            //        foreach (var items in this.listSelects)
            //        {
            //            List<string> item = Funs.GetStrListByStr(items, '|');
            //            strSql += " OR ISO_IsoNo.ISO_ID = @ISO_ID" + i.ToString();
            //            listStr.Add(new SqlParameter("@ISO_ID" + i.ToString(), item[0].ToString()));
            //            i++;
            //        }

            //        strSql += ")";
            //    }
            //}
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

        /// <summary>
        /// 对GV 赋值
        /// </summary>
        /// <param name="jointInfosSelectList"></param>
        private void ShowGridItem()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                Grid1.Rows[i].Values[0] = BLL.Const._False;              
                ////操作焊接焊口信息
                if (listSelects.Count() > 0)
                {
                    foreach (var item in listSelects)
                    {
                        if (item == Grid1.DataKeys[i][0].ToString())
                        {
                            Grid1.Rows[i].Values[0] = BLL.Const._True;
                        }
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
            this.CollectGridJointInfo();
            this.BindGrid();
            this.ShowGridItem();
        }

        #endregion

        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.CollectGridJointInfo();
            this.BindGrid();
            this.ShowGridItem();
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
            this.CollectGridJointInfo();
            this.BindGrid();
            this.ShowGridItem();
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
            this.CollectGridJointInfo();
            this.BindGrid();
            this.ShowGridItem();
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
            this.CollectGridJointInfo();
            this.BindGrid();
            this.ShowGridItem();
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
                if (string.IsNullOrEmpty(this.txtTestPackageNo.Text) || this.drpUnit.SelectedValue == BLL.Const._Null || this.drpTabler.SelectedValue == BLL.Const._Null
                    || this.drpUnitWork.SelectedValue == BLL.Const._Null || string.IsNullOrEmpty(this.txtTableDate.Text))
                {
                    ShowNotify("必填项不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                this.CollectGridJointInfo();
                if (this.listSelects.Count() == 0)
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
                testPackage.TestHeat = this.txtTestHeat.Text.Trim();
                testPackage.TestService = this.txtTestService.Text.Trim();
                if (this.drpTestType.SelectedValue != BLL.Const._Null)
                {
                    testPackage.TestType = this.drpTestType.SelectedValue;
                }
                if (this.drpTabler.SelectedValue != BLL.Const._Null)
                {
                    testPackage.Tabler = this.drpTabler.SelectedValue;
                }
                testPackage.TableDate = Funs.GetNewDateTime(this.txtTableDate.Text);
                if (this.drpModifier.SelectedValue != BLL.Const._Null)
                {
                    testPackage.Modifier = this.drpModifier.SelectedValue;
                }
                testPackage.ModifyDate = Funs.GetNewDateTime(this.txtModifyDate.Text);
                testPackage.Remark = this.txtRemark.Text.Trim();
                //testPackage.TestPackageCode = this.txtTestPackageCode.Text.Trim();
                testPackage.TestAmbientTemp = this.txtTestAmbientTemp.Text.Trim();
                testPackage.TestMediumTemp = this.txtTestMediumTemp.Text.Trim();
                testPackage.TestPressure = this.txtTestPressure.Text.Trim();
                testPackage.TestPressureTemp = this.txtTestPressureTemp.Text.Trim();
                testPackage.TestPressureTime = this.txtTestPressureTime.Text.Trim();
                testPackage.TightnessTest = this.txtTightnessTest.Text.Trim();
                testPackage.TightnessTestTemp = this.txtTightnessTestTemp.Text.Trim();
                testPackage.TightnessTestTime = this.txtTightnessTestTime.Text.Trim();
                testPackage.LeakageTestService = this.txtLeakageTestService.Text.Trim();
                testPackage.LeakageTestPressure = this.txtLeakageTestPressure.Text.Trim();
                testPackage.VacuumTestService = this.txtVacuumTestService.Text.Trim();
                testPackage.VacuumTestPressure = this.txtVacuumTestPressure.Text.Trim();
                testPackage.OperationMedium = this.txtOperationMedium.Text.Trim();
                testPackage.PurgingMedium = this.txtPurgingMedium.Text.Trim();
                testPackage.CleaningMedium = this.txtCleaningMedium.Text.Trim();
                testPackage.AllowSeepage = this.txtAllowSeepage.Text.Trim();
                testPackage.FactSeepage = this.txtFactSeepage.Text.Trim();               
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

                foreach (var item in listSelects)
                {
                    Model.PTP_PipelineList newitem = new Model.PTP_PipelineList();
                    newitem.PTP_ID = this.PTP_ID;
                    newitem.PipelineId = item;
                    BLL.TestPackageEditService.AddPipelineList(newitem);
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
        #endregion

        #region 收集Grid页面信息
        /// <summary>
        /// 收集Grid页面信息
        /// </summary>
        /// <returns></returns>
        private void CollectGridJointInfo()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.DataKeys[i][0].ToString();
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (listSelects.Contains(rowID))
                {
                    listSelects.Remove(rowID);
                }
                if (checkField.GetCheckedState(i))
                {
                    listSelects.Add(rowID);
                }
            }
        }
        #endregion

        #region Grid 明细操作事件
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.DataKeys[i][0].ToString();
                if (!listSelects.Contains(rowID))
                {
                    listSelects.Add(rowID);
                }
            }
            this.ShowGridItem();
        }

        /// <summary>
        /// 全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.DataKeys[i][0].ToString();
                if (listSelects.Contains(rowID))
                {
                    listSelects.Remove(rowID);
                }
            }
            this.ShowGridItem();
        }
        #endregion

        #region 管线查询
        /// <summary>
        /// 管线查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtIsono_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.CollectGridJointInfo();
            this.BindGrid();
            this.ShowGridItem();
        }
        #endregion

        #region 只显示选中项
        /// <summary>
        /// 只显示选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckSelect_OnCheckedChanged(object sender, CheckedEventArgs e)
        {
            this.CollectGridJointInfo();
            this.BindGrid();
            this.ShowGridItem();
        }
        #endregion
    }
}