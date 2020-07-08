using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class CheckColligation : PageBase
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
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("CheckColligationEdit.aspx") + "return false;";

                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                    new SqlParameter("@ProjectId", this.ProjectId),
                    new SqlParameter("@StartTime", !string.IsNullOrEmpty(this.txtStartTime.Text)?this.txtStartTime.Text:null),
                    new SqlParameter("@EndTime", !string.IsNullOrEmpty(this.txtEndTime.Text)?this.txtEndTime.Text:null),
                    new SqlParameter("@States", !string.IsNullOrEmpty(Request.Params["projectId"])?BLL.Const.State_2:null),
                    new SqlParameter("@UnitName", !string.IsNullOrEmpty(this.txtUnitName.Text)?this.txtUnitName.Text:null),
                    new SqlParameter("@WorkAreaName",  !string.IsNullOrEmpty(this.txtWorkAreaName.Text)?this.txtWorkAreaName.Text:null),
                    };
            DataTable tb = SQLHelper.GetDataTableRunProc("SpCheckColligationStatistic", parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string[] rowID = Grid1.Rows[i].DataKeys[0].ToString().Split(',');
                if (rowID.Count() > 0)
                {
                    var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(rowID[0]);
                    if (checkColligation != null)
                    {
                        if (checkColligation.States == BLL.Const.State_2)
                        {
                            if (rowID.Count() > 1)
                            {
                                Model.Check_CheckColligationDetail detail = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(rowID[1]);
                                if (detail != null)
                                {
                                    if (!detail.CompletedDate.HasValue)
                                    {
                                        Grid1.Rows[i].RowCssClass = "Yellow";
                                    }
                                }
                                //else
                                //{
                                //    Grid1.Rows[i].RowCssClass = "Red";
                                //}
                            }
                        }
                        else
                        {
                            Grid1.Rows[i].RowCssClass = "Green";
                        }
                    }
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 表排序、分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string CheckColligationId = Grid1.SelectedRowID.Split(',')[0];
            var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(CheckColligationId);
            if (checkColligation != null)
            {
                if (this.btnMenuModify.Hidden || checkColligation.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckColligationView.aspx?CheckColligationId={0}", CheckColligationId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckColligationEdit.aspx?CheckColligationId={0}", CheckColligationId, "编辑 - ")));
                }
            }            
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString().Split(',')[0];
                    var getV = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.CheckColligationCode, getV.CheckColligationId, BLL.Const.ProjectCheckColligationMenuId, BLL.Const.BtnDelete);
                        BLL.Check_CheckColligationDetailService.DeleteCheckColligationDetails(rowID);
                        
                        BLL.Check_CheckColligationService.DeleteCheckColligation(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
        }
        #endregion

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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectCheckColligationMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnCompletedDate.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion

        //#region 生成隐患整改单
        ///// <summary>
        ///// 生成隐患整改单
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnMenuRectify_Click(object sender, EventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length == 0)
        //    {
        //        Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
        //        return;
        //    }
        //    string rectifyNoticeCode = string.Empty;
        //    string CheckColligationId = Grid1.SelectedRowID.Split(',')[0];
        //    var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(CheckColligationId);
        //    if (checkColligation.States == BLL.Const.State_2)
        //    {
        //        string CheckColligationDetailId = Grid1.SelectedRowID.Split(',')[1];
        //        Model.Check_CheckColligationDetail detail = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(CheckColligationDetailId);
        //        if (string.IsNullOrEmpty(detail.RectifyNoticeId))
        //        {
        //            Model.Check_RectifyNotices rectifyNotice = new Model.Check_RectifyNotices
        //            {
        //                RectifyNoticesId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotice)),
        //                ProjectId = checkColligation.ProjectId,
        //                UnitId = detail.UnitId,
        //                CheckedDate = checkColligation.CheckTime,
        //                RectifyNoticesCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticeMenuId, checkColligation.ProjectId, detail.UnitId),
        //                WrongContent = "开展了综合检查,发现问题及隐患:" + detail.Unqualified + "\n" + detail.Suggestions,
        //                SignPerson = this.CurrUser.UserId,
        //                SignDate = DateTime.Now,
        //            };

        //            var workArea = new Model.SGGLDB(Funs.ConnString).ProjectData_WorkArea.FirstOrDefault(x => x.ProjectId == checkColligation.ProjectId && x.WorkAreaName == detail.WorkArea);
        //            if (workArea != null)
        //            {
        //                rectifyNotice.WorkAreaId = workArea.WorkAreaId;
        //            }                  
                 
        //            BLL.RectifyNoticesService.AddRectifyNotices(rectifyNotice);
        //            rectifyNoticeCode = rectifyNotice.RectifyNoticesCode;
        //            detail.RectifyNoticeId = rectifyNotice.RectifyNoticesId;
        //            BLL.Check_CheckColligationDetailService.UpdateCheckColligationDetail(detail);
        //            ///写入工程师日志
        //            BLL.HSSELogService.CollectHSSELog(rectifyNotice.ProjectId, rectifyNotice.SignPerson, rectifyNotice.SignDate, "22", rectifyNotice.WrongContent, Const.BtnAdd, 1);
        //        }
        //        if (!string.IsNullOrEmpty(rectifyNoticeCode))
        //        {
        //            Alert.ShowInTop("已生成隐患整改通知单：" + rectifyNoticeCode + "！", MessageBoxIcon.Success);
        //        }
        //        else
        //        {
        //            Alert.ShowInTop("隐患整改通知单已存在，请到对应模块进行处理！", MessageBoxIcon.Warning);
        //        }
        //    }
        //    else
        //    {
        //        Alert.ShowInTop("该记录尚未审批完成，无法进行操作！", MessageBoxIcon.Warning);
        //        return;
        //    }
        //}
        //#endregion

        #region 闭环
        /// <summary>
        /// 闭环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompletedDate_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string CheckColligationId = Grid1.SelectedRowID.Split(',')[0];
            var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(CheckColligationId);
            if (checkColligation.States == BLL.Const.State_2)
            {               
                string CheckColligationDetailId = Grid1.SelectedRowID.Split(',')[1];
                Model.Check_CheckColligationDetail detail = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(CheckColligationDetailId);
                if (detail != null && !detail.CompletedDate.HasValue)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowCompletedDate.aspx?CheckColligationDetailId={0}", CheckColligationDetailId), "编辑闭环时间", 400, 250));
                }
                else
                {
                    Alert.ShowInTop("该记录已闭环或不存在明细项！", MessageBoxIcon.Warning);
                    return;
                }
                
            }
            else
            {
                Alert.ShowInTop("该记录尚未审批完成，无法进行闭环操作！", MessageBoxIcon.Warning);
                return;
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("综合检查" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}