using BLL;
using System;
using System.Web;
using System.Linq;
using System.Data;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ExpertArgumentListEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string LargerHazardListId
        {
            get
            {
                return (string)ViewState["LargerHazardListId"];
            }
            set
            {
                ViewState["LargerHazardListId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();          
                this.LargerHazardListId = Request.Params["LargerHazardListId"];
                btnNew.OnClientClick = Window1.GetShowReference(String.Format("ExpertArgumentListEditItem.aspx?LargerHazardListItemId={0}", this.LargerHazardListId, "编辑 - ")) + "return false;";
                var getRecord = BLL.ExpertArgumentService.GetLargerHazardListById(this.LargerHazardListId);
                if (getRecord != null)
                {
                    this.txtHazardCode.Text = getRecord.HazardCode;
                    this.txtRecordTime.Text = string.Format("{0:yyyy-MM-dd}", getRecord.RecordTime);
                    this.txtVersionNo.Text = getRecord.VersionNo;
                    ExpertArgumentService.getViewLargerHazardListItem = (from x in Funs.DB.View_Solution_LargerHazardListItem
                                                                         where x.LargerHazardListId == this.LargerHazardListId
                                                                        select x).ToList();
                 }
                else
                {
                    ////自动生成编码
                    this.txtHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectExpertArgumentMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                    this.txtRecordTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtVersionNo.Text = "V1.0";
                }
                // 绑定表格
                BindGrid();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            if (ExpertArgumentService.getViewLargerHazardListItem != null)
            {
                Grid1.RecordCount = ExpertArgumentService.getViewLargerHazardListItem.Count();
                DataTable tb = this.GetPagedDataTable(Grid1, ExpertArgumentService.getViewLargerHazardListItem);
                Grid1.DataSource = tb;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
        }

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Solution_LargerHazardList newRecord = new Model.Solution_LargerHazardList
            {
                LargerHazardListId = this.LargerHazardListId,
                HazardCode = this.txtHazardCode.Text,
                ProjectId = this.CurrUser.LoginProjectId,
                RecordTime = Funs.GetNewDateTime(this.txtRecordTime.Text),
                VersionNo = this.txtVersionNo.Text.Trim(),
                RecardManId = this.CurrUser.UserId,
                ////单据状态
                States = BLL.Const.State_0,
            };

            if (type == BLL.Const.BtnSubmit)
            {
                newRecord.States = BLL.Const.State_1;
            }
            if (!string.IsNullOrEmpty(this.LargerHazardListId))
            {
                BLL.ExpertArgumentService.UpdateLargerHazardList(newRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, newRecord.HazardCode, newRecord.LargerHazardListId, BLL.Const.ProjectExpertArgumentMenuId, BLL.Const.BtnModify);

                BLL.ExpertArgumentService.DeleteLargerHazardListItemByLargerHazardListId(this.LargerHazardListId);
            }
            else
            {                
                this.LargerHazardListId = newRecord.LargerHazardListId = SQLHelper.GetNewID();
                BLL.ExpertArgumentService.AddLargerHazardList(newRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, newRecord.HazardCode, newRecord.LargerHazardListId, BLL.Const.ProjectExpertArgumentMenuId, BLL.Const.BtnAdd);
            }
            var newListItems = from x in ExpertArgumentService.getViewLargerHazardListItem
                               select new Model.Solution_LargerHazardListItem
                               {
                                   LargerHazardListItemId = x.LargerHazardListItemId,
                                   SortIndex = x.SortIndex,
                                   LargerHazardListId = this.LargerHazardListId,
                                   UnitWorkId = x.UnitWorkId,
                                   WorkPackageId = x.WorkPackageId,
                                   WorkPackageSize = x.WorkPackageSize,
                                   ExpectedStartTime = x.ExpectedStartTime,
                                   ExpectedEndTime = x.ExpectedEndTime,
                                   IsArgument = x.IsArgument,
                                   UnitId = x.UnitId,
                               };
            if (newListItems != null && newListItems.Count() > 0)
            {
                Funs.DB.Solution_LargerHazardListItem.InsertAllOnSubmit(newListItems);
                Funs.DB.SubmitChanges();
            }
        }
        #endregion        

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ExpertArgumentListEditItem.aspx?LargerHazardListItemId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    ExpertArgumentService.getViewLargerHazardListItem = ExpertArgumentService.getViewLargerHazardListItem.Where(x => x.LargerHazardListItemId != rowID).ToList();
                    //BLL.LargerHazardService.DeleteLargerHazard(rowID);
                }

                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid();
        }
    }
}