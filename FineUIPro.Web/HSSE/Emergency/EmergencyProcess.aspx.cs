using BLL;
using System;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.HSSE.Emergency
{
    public partial class EmergencyProcess : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                // 绑定表格
                this.BindGrid();                
            }
        }
        #endregion

        #region GV绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
         void BindGrid()
        {
            var getEmergencys = (from x in Funs.DB.Emergency_EmergencyProcess
                                where x.ProjectId == this.ProjectId
                                select x).ToList();
            if (getEmergencys.Count() == 0)
            {
                var getEEmergencyProcessItems = from x in Funs.DB.Emergency_EmergencyProcess
                                                where x.ProjectId == null
                                                select x;
                foreach (var item in getEEmergencyProcessItems)
                {
                    Model.Emergency_EmergencyProcess newItem = new Model.Emergency_EmergencyProcess
                    {
                        EmergencyProcessId = SQLHelper.GetNewID(),
                        ProjectId = this.ProjectId,
                        ProcessSteps = item.ProcessSteps,
                        ProcessName = item.ProcessName,
                        StepOperator = item.StepOperator,
                        Remark = item.Remark,
                    };
                    Funs.DB.Emergency_EmergencyProcess.InsertOnSubmit(newItem);
                    Funs.DB.SubmitChanges();

                    getEmergencys.Add(item);
                }
            }

            Grid1.RecordCount = getEmergencys.Count();
            DataTable tb = this.GetPagedDataTable(Grid1, getEmergencys);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
         void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EmergencyProcessEdit.aspx?EmergencyProcessId={0}", Grid1.SelectedRowID, "详细 - ")));
        }
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
                    this.btnMenuEdit.Hidden = false;
                }
            }
        }
        #endregion        
    }
}