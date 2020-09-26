using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencyProcessEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EmergencyProcessId
        {
            get
            {
                return (string)ViewState["EmergencyProcessId"];
            }
            set
            {
                ViewState["EmergencyProcessId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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
                this.GetButtonPower();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.EmergencyProcessId = Request.Params["EmergencyProcessId"];
                if (!string.IsNullOrEmpty(this.EmergencyProcessId))
                {
                    var EmergencyProcess = Funs.DB.Emergency_EmergencyProcess.FirstOrDefault(x => x.EmergencyProcessId == this.EmergencyProcessId);
                    if (EmergencyProcess != null)
                    {
                        this.txtProcessSteps.Text = EmergencyProcess.ProcessSteps;
                        this.txtProcessName.Text = EmergencyProcess.ProcessName;
                        this.txtStepOperator.Text = EmergencyProcess.StepOperator;
                        //this.txtRemark.Text = EmergencyProcess.Remark;
                        if (EmergencyProcess.ProcessSteps == "0")
                        {
                            this.State1.Hidden = false;
                            this.State2.Hidden = true;
                            this.State2Person.Hidden = true;
                            BindGrid();
                        }
                        else
                        {
                            EmergencyTeamAndTrainService.InitTeamDropDownList(drpTeam, this.CurrUser.LoginProjectId, true);
                            if (!string.IsNullOrEmpty(EmergencyProcess.ProcessTeam))
                            {
                                this.drpTeam.SelectedValueArray = EmergencyProcess.ProcessTeam.Split(',');
                                string Users = string.Empty;
                                string[] array = this.drpTeam.SelectedValueArray;
                                List<string> str = new List<string>();
                                foreach (var item in array)
                                {
                                    if (item != BLL.Const._Null)
                                    {
                                        var TeamItem = (from x in Funs.DB.Emergency_EmergencyTeamItem where x.FileId == item select x).ToList();
                                        foreach (var teams in TeamItem)
                                        {
                                            Users += PersonService.GetPersonNameById(teams.PersonId) + ",";
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(Users))
                                {
                                    txtUser.Text = Users.Substring(0, Users.LastIndexOf(","));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void BindGrid()
        {
            string strSql = @"select * from Emergency_EmergencyProcessItem where EmergencyProcessId=@EmergencyProcessId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@EmergencyProcessId", this.EmergencyProcessId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        void SaveData()
        {
            var EmergencyProcess = Funs.DB.Emergency_EmergencyProcess.FirstOrDefault(x => x.EmergencyProcessId == this.EmergencyProcessId);
            if (EmergencyProcess != null)
            {

                EmergencyProcess.ProcessName = this.txtProcessName.Text.Trim();
                EmergencyProcess.StepOperator = this.txtStepOperator.Text.Trim();
                //EmergencyProcess.Remark = this.txtRemark.Text.Trim();
                //队伍
                EmergencyProcess.ProcessTeam = Funs.GetStringByArray(this.drpTeam.SelectedValueArray);
                Funs.DB.SubmitChanges();
                if (EmergencyProcess.ProcessSteps == "0")
                {
                    var getViewList = this.CollectGridInfo();
                    foreach (var item in getViewList)
                    {
                        var ProcessItem = Funs.DB.Emergency_EmergencyProcessItem.FirstOrDefault(x => x.EmergencyProcessItemId == item.EmergencyProcessItemId);
                        if (ProcessItem != null)
                        {
                            ProcessItem.Content = item.Content;
                            ProcessItem.SortId = item.SortId;
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            Model.Emergency_EmergencyProcessItem newItem = new Model.Emergency_EmergencyProcessItem
                            {
                                EmergencyProcessItemId = item.EmergencyProcessItemId,
                                EmergencyProcessId = this.EmergencyProcessId,
                                Content = item.Content,
                                SortId = item.SortId,
                            };
                            Funs.DB.Emergency_EmergencyProcessItem.InsertOnSubmit(newItem);
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

        }

        //private string GetStringByArray(string[] array)
        //{
        //    string str = string.Empty;
        //    foreach (var item in array)
        //    {
        //        if (item != BLL.Const._Null)
        //        {
        //            str += item + ",";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(str))
        //    {
        //        str = str.Substring(0, str.LastIndexOf(","));
        //    }
        //    return str;
        //}
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectEmergencyProcessMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 确定按钮
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            var getViewList = this.CollectGridInfo();
            getViewList = getViewList.Where(x => x.EmergencyProcessItemId != this.hdEmergencyProcessId.Text).ToList();
            Model.Emergency_EmergencyProcessItem newView = new Model.Emergency_EmergencyProcessItem
            {
                EmergencyProcessItemId = SQLHelper.GetNewID(),
                EmergencyProcessId = this.EmergencyProcessId,
                Content = this.txtContent.Text,
                SortId=this.txtSortId.Text,
            };
            getViewList.Add(newView);
            
            this.Grid1.DataSource = getViewList;
            this.Grid1.DataBind();
            this.InitText();
        }
        #endregion

        #region 收集页面信息
        /// <summary>
        ///  收集页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.Emergency_EmergencyProcessItem> CollectGridInfo()
        {
            List<Model.Emergency_EmergencyProcessItem> getViewList = new List<Model.Emergency_EmergencyProcessItem>();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                Model.Emergency_EmergencyProcessItem newView = new Model.Emergency_EmergencyProcessItem
                {
                    EmergencyProcessItemId = Grid1.Rows[i].DataKeys[0].ToString(),
                    EmergencyProcessId = this.EmergencyProcessId,
                    Content = Grid1.Rows[i].Values[1].ToString(),
                    SortId = Grid1.Rows[i].Values[0].ToString(),
                };
                getViewList.Add(newView);
            }

            return getViewList;
        }
        #endregion

        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            this.hdEmergencyProcessId.Text = string.Empty;
            this.txtContent.Text = string.Empty;
            this.txtSortId.Text = string.Empty;
        }
        #endregion

        #region Grid 操作事件
        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectGridInfo();
            var item = getViewList.FirstOrDefault(x => x.EmergencyProcessItemId == Grid1.SelectedRowID);
            if (item != null)
            {
                this.hdEmergencyProcessId.Text = item.EmergencyProcessItemId;
                this.txtContent.Text = item.Content;
                this.txtSortId.Text = item.SortId;
            }
            
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                var getViewList = this.CollectGridInfo();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var item = getViewList.FirstOrDefault(x => x.EmergencyProcessItemId == rowID);
                    if (item != null)
                    {
                        getViewList.Remove(item);
                    }
                    var ProcessItem = Funs.DB.Emergency_EmergencyProcessItem.First(x => x.EmergencyProcessItemId == rowID);
                    if (ProcessItem != null) {
                        Funs.DB.Emergency_EmergencyProcessItem.DeleteOnSubmit(ProcessItem);
                        Funs.DB.SubmitChanges();
                    }
                }

                this.Grid1.DataSource = getViewList;
                this.Grid1.DataBind();
            }
        }
        #endregion

        protected void drpTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUser.Text = string.Empty; ;
            string Users = string.Empty;
            string[] array = this.drpTeam.SelectedValueArray;
            List<string> str = new List<string>();
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    var TeamItem = (from x in Funs.DB.Emergency_EmergencyTeamItem where x.FileId == item select x).ToList();
                    foreach (var teams in TeamItem)
                    {
                        Users += PersonService.GetPersonNameById(teams.PersonId)+ ",";
                    }
                }
            }
            if (!string.IsNullOrEmpty(Users))
            {
                txtUser.Text = Users.Substring(0, Users.LastIndexOf(","));
            }
        }
    }
}