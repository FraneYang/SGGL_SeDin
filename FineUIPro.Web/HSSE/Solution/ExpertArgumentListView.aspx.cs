using BLL;
using System;
using System.Web;
using System.Linq;
using System.Data;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ExpertArgumentListView : PageBase
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
                var getRecord = BLL.ExpertArgumentService.GetLargerHazardListById(LargerHazardListId);
                if (getRecord != null)
                {
                    this.txtHazardCode.Text = getRecord.HazardCode;
                    this.txtRecordTime.Text = string.Format("{0:yyyy-MM-dd}", getRecord.RecordTime);
                    this.txtVersionNo.Text = getRecord.VersionNo;
                    ExpertArgumentService.getViewLargerHazardListItem = (from x in Funs.DB.View_Solution_LargerHazardListItem
                                                                         where x.LargerHazardListId == this.LargerHazardListId
                                                                         select x).ToList();
                    if (getRecord.States == Const.State_1 && this.CurrUser.UserId == Const.sysglyId)
                    {
                        var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectExpertArgumentMenuId);
                        if (buttonList.Count() > 0)
                        {
                            if (buttonList.Contains(BLL.Const.BtnDelete))
                            {
                                this.btnCancel.Hidden = false;
                            }
                        }
                    }
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
               

        #region 作废按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            var getData = BLL.ExpertArgumentService.GetLargerHazardListById(this.LargerHazardListId);
            if (getData != null)
            {
                getData.States = "-1";
                BLL.ExpertArgumentService.UpdateLargerHazardList(getData);
            }
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
    }
}