﻿namespace FineUIPro.Web.HSSE.License
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class LiftingWorkView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string LiftingWorkId
        {
            get
            {
                return (string)ViewState["LiftingWorkId"];
            }
            set
            {
                ViewState["LiftingWorkId"] = value;
            }
        }
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
                this.SimpleForm1.Title = UnitService.GetUnitNameByUnitId(Const.UnitId_SEDIN) + this.Title;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.LiftingWorkId = Request.Params["LiftingWorkId"];
                if (!string.IsNullOrEmpty(this.LiftingWorkId))
                {
                    var getLiftingWork = LicensePublicService.GetLiftingWorkById(this.LiftingWorkId);
                    if (getLiftingWork != null)
                    {
                        this.lbLicenseCode.Text = getLiftingWork.LicenseCode;
                        this.txtApplyUnit.Text = UnitService.GetUnitNameByUnitId(getLiftingWork.ApplyUnitId);
                        this.txtApplyManName.Text = UserService.GetUserNameByUserId(getLiftingWork.ApplyManId);
                        this.txtWorkPalce.Text = getLiftingWork.WorkPalce;
                        this.txtWorkLevel.Text = getLiftingWork.WorkLevel;
                        if (getLiftingWork.ValidityStartTime.HasValue)
                        {
                            this.txtWorkDate.Text = getLiftingWork.ValidityStartTime.Value.ToString("f") + " 至 ";
                            if (getLiftingWork.ValidityEndTime.HasValue)
                            {
                                this.txtWorkDate.Text += getLiftingWork.ValidityEndTime.Value.ToString("f");
                            }
                        }
                        this.txtWorkMeasures.Text = getLiftingWork.WorkMeasures;
                        this.txtCraneCapacity.Text = getLiftingWork.CraneCapacity;
                        if (!string.IsNullOrEmpty(getLiftingWork.CancelManId))
                        {
                            this.txtCance.Text = UserService.GetUserNameByUserId(getLiftingWork.CancelManId) + "；取消时间："
                                + string.Format("{0:yyyy-MM-dd HH:mm}", getLiftingWork.CancelTime) + "；原因：" + getLiftingWork.CancelReasons + "。";
                        }
                        if (!string.IsNullOrEmpty(getLiftingWork.CloseManId))
                        {
                            if (!string.IsNullOrEmpty(getLiftingWork.CloseReasons))
                            {
                                this.txtClose.Text = getLiftingWork.CloseReasons + " 关闭时间："
                                 + string.Format("{0:yyyy-MM-dd HH:mm}", getLiftingWork.CloseTime) + "。";
                            }
                            else
                            {
                                this.txtClose.Text = UserService.GetUserNameByUserId(getLiftingWork.CloseManId) + "；关闭时间："
                                + string.Format("{0:yyyy-MM-dd HH:mm}", getLiftingWork.CloseTime) + "。";
                            }
                        }
                    }
                }
                // 绑定表格
                this.BindGrid();
                this.SetFlow();
            }
        }
        #endregion

        #region 获取审核记录信息
        /// <summary>
        /// 
        /// </summary>
        private void SetFlow()
        {
            var getFlows = LicensePublicService.GetFlowOperateListByDataId(this.LiftingWorkId);
            if (getFlows.Count() > 0)
            {
                var getF1 = getFlows.FirstOrDefault(x => x.SortIndex == 1);
                if (getF1 != null)
                {
                    this.txtForm1.Title = getF1.AuditFlowName + "：";
                    if (getF1.OperaterTime.HasValue)
                    {
                        if (getF1.IsAgree == true)
                        {
                            this.txtOpinion1.Text = "同意。";
                        }
                        else
                        {
                            this.txtOpinion1.Text = getF1.Opinion;
                        }
                        this.txtName1.Text = UserService.GetUserNameByUserId(getF1.OperaterId);
                        this.txtTime1.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF1.OperaterTime);
                    }
                }
                var getF2 = getFlows.FirstOrDefault(x => x.SortIndex == 2);
                if (getF2 != null)
                {
                    this.txtForm2.Title = getF2.AuditFlowName + "：";
                    if (getF2.OperaterTime.HasValue)
                    {
                        if (getF2.IsAgree == true)
                        {
                            this.txtOpinion2.Text = "同意。";
                        }
                        else
                        {
                            this.txtOpinion2.Text = getF2.Opinion;
                        }
                        this.txtName2.Text = UserService.GetUserNameByUserId(getF2.OperaterId);
                        this.txtTime2.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF2.OperaterTime);
                    }
                }
                var getF3 = getFlows.FirstOrDefault(x => x.SortIndex == 3 && x.GroupNum == 1 && x.OrderNum == 1);
                if (getF3 != null)
                {
                    this.txtForm3.Title = getF3.AuditFlowName + "：";
                    if (getF3.OperaterTime.HasValue)
                    {
                        if (getF3.IsAgree == true)
                        {
                            this.txtOpinion3.Text = "同意。";
                        }
                        else
                        {
                            this.txtOpinion3.Text = getF3.Opinion;
                        }
                        this.txtName3.Text = UserService.GetUserNameByUserId(getF3.OperaterId);
                        this.txtTime3.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF3.OperaterTime);
                    }
                }
                var getF4 = getFlows.FirstOrDefault(x => x.SortIndex == 3 && x.GroupNum == 1 && x.OrderNum ==2);
                if (getF4 != null)
                {
                    this.txtForm4.Title = getF4.AuditFlowName + "：";
                    if (getF4.OperaterTime.HasValue)
                    {
                        if (getF4.IsAgree == true)
                        {
                            this.txtOpinion4.Text = "同意。";
                        }
                        else
                        {
                            this.txtOpinion4.Text = getF4.Opinion;
                        }
                        this.txtName4.Text = UserService.GetUserNameByUserId(getF4.OperaterId);
                        this.txtTime4.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF4.OperaterTime);
                    }
                }
                var getF5 = getFlows.FirstOrDefault(x => x.SortIndex == 3 && x.GroupNum == 2 && x.OrderNum == 1);
                if (getF5 != null)
                {
                    this.txtForm5.Title = getF5.AuditFlowName + "：";
                    if (getF5.OperaterTime.HasValue)
                    {
                        if (getF5.IsAgree == true)
                        {
                            this.txtOpinion5.Text = "同意。";
                        }
                        else
                        {
                            this.txtOpinion5.Text = getF5.Opinion;
                        }
                        this.txtName5.Text = UserService.GetUserNameByUserId(getF5.OperaterId);
                        this.txtTime5.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF5.OperaterTime);
                    }
                }
                var getF6 = getFlows.FirstOrDefault(x => x.SortIndex == 3 && x.GroupNum == 2 && x.OrderNum == 2);
                if (getF6 != null)
                {
                    this.txtForm6.Title = getF6.AuditFlowName + "：";
                    if (getF6.OperaterTime.HasValue)
                    {
                        if (getF6.IsAgree == true)
                        {
                            this.txtOpinion6.Text = "同意。";
                        }
                        else
                        {
                            this.txtOpinion6.Text = getF6.Opinion;
                        }
                        this.txtName6.Text = UserService.GetUserNameByUserId(getF6.OperaterId);
                        this.txtTime6.Text = string.Format("{0:yyyy-MM-dd HH:mm}", getF6.OperaterTime);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT LicenseItemId,DataId,SortIndex,SafetyMeasures,(CASE WHEN IsUsed = 1 THEN '适用' ELSE '不适用' END) AS NoUsedName,ConfirmManId,U.UserName AS ConfirmManName"
                         + @" FROM License_LicenseItem AS L "
                         + @" LEFT JOIN Sys_User AS U ON L.ConfirmManId =U.UserId"
                         + @" WHERE L.DataId ='" + this.LiftingWorkId +"'";
            List<SqlParameter> listStr = new List<SqlParameter>();            
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
    }
}