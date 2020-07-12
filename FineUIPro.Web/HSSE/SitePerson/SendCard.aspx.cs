using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class SendCard : PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员Id
        /// </summary>
        public string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
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
                Funs.DropDownPageSize(this.ddlPageSize);
                ////权限按钮方法
                this.GetButtonPower();
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT person.PersonId,person.CardNo,person.PersonName,person.IdentityCard,unit.UnitName,person.UnitId, post.WorkPostName, work.UnitWorkName AS WorkAreaName,person.ProjectId"
                + @" FROM dbo.SitePerson_Person person"
                + @" LEFT JOIN dbo.Base_Unit unit ON unit.UnitId=person.UnitId"
                + @" LEFT JOIN dbo.Base_WorkPost post ON post.WorkPostId=person.WorkPostId"
                + @" LEFT JOIN dbo.WBS_UnitWork work ON work.UnitWorkId=person.WorkAreaId  "
                + @" WHERE person.ProjectId=@ProjectId ";
            List<SqlParameter> listStr = new List<SqlParameter>();           
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));              
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND person.PersonName LIKE @PersonName";
                listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            }
            if (this.cbIsSend.SelectedValueArray.Length == 1)
            {
                ///是否发布
                string selectValue = String.Join(", ", this.cbIsSend.SelectedValueArray);
                if (selectValue == "1")
                {
                    strSql += " AND person.CardNo IS NOT NULL ";
                }
                else
                {
                    strSql += " AND person.CardNo IS NULL ";
                }
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region Gv事件
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
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
            BindGrid();
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
            BindGrid();
        }
        #endregion
        #endregion
        
        #region 关闭弹出窗事件
        /// <summary>
        /// 关闭弹出框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 发卡
        /// <summary>
        /// 右键发卡事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendCard_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            this.PersonId = this.Grid1.SelectedRowID;
            var person = PersonService.GetPersonById(PersonId);
            if (person != null)
            {
                if (!string.IsNullOrEmpty(person.CardNo))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SendCardView.aspx?PersonId={0}", this.PersonId, "发卡 - ")));
                }                
                else
                {                   
                    if (ConvertTrainResult(PersonId) == "通过")
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReadWriteCard.aspx?PersonId={0}", this.PersonId, "发卡 - ")));
                    }
                    else
                    {
                        Alert.ShowInTop("培训未通过，不能发卡！", MessageBoxIcon.Warning);
                        return;
                    }
                }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SendCardMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSendCard))
                {
                    this.btnSendCard.Hidden = false;
                }
            }
        }
        #endregion

        #region 获取培训结果
        /// <summary>
        /// 获取培训结果
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertTrainResult(object PersonId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string result = string.Empty;
                if (PersonId != null)
                {
                    string personId = PersonId.ToString().Trim();
                    List<Model.Base_TrainType> trainTypeList = BLL.TrainTypeService.GetIsAboutSendCardTrainTypeList();
                    int i = 0;   //培训合格次数
                    foreach (var item in trainTypeList)
                    {
                        var q = (from x in db.EduTrain_TrainRecord
                                 join y in db.EduTrain_TrainRecordDetail
                                 on x.TrainingId equals y.TrainingId
                                 where x.TrainTypeId == item.TrainTypeId && y.PersonId == PersonId.ToString() && y.CheckResult == true
                                 select y);
                        i += q.Count();
                    }

                    if (i >= trainTypeList.Count)
                    {
                        result = "通过";
                    }
                    else
                    {
                        result = "未通过";
                    }
                }
                return result;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("发卡信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
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
    }
}