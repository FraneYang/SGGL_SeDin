using BLL;
using System;
using System.Linq;
using System.Web;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ExpertArgumentListEditItem : PageBase
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
        /// <summary>
        /// 明细主键
        /// </summary>
        public string LargerHazardListItemId
        {
            get
            {
                return (string)ViewState["LargerHazardListItemId"];
            }
            set
            {
                ViewState["LargerHazardListItemId"] = value;
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
                //单位工程             
                BLL.UnitWorkService.InitUnitWorkDropDownList(this.drpUnitWorkId, this.CurrUser.LoginProjectId, true);
                //是否需要专家论证              
                BLL.ConstValue.InitConstValueRadioButtonList(this.rblIsArgument, ConstValue.Group_0001, "False");
                //施工单位             
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, true);
                this.LargerHazardListId = Request.Params["LargerHazardListId"];
                this.LargerHazardListItemId = Request.Params["LargerHazardListItemId"];
              
                var getItem = BLL.ExpertArgumentService.getViewLargerHazardListItem.FirstOrDefault(x => x.LargerHazardListItemId == this.LargerHazardListItemId);
                if (getItem != null)
                {
                    this.txtSortIndex.Text = getItem.SortIndex.ToString();
                    if (!string.IsNullOrEmpty(getItem.UnitWorkId))
                    {
                        this.drpUnitWorkId.SelectedValue = getItem.UnitWorkId;
                        BLL.WorkPackageService.InitWorkPackagesDropDownListByUnitWorkId(this.drpWorkPackageId, this.drpUnitWorkId.SelectedValue, true);
                        if (!string.IsNullOrEmpty(getItem.WorkPackageId))
                        {
                            this.drpWorkPackageId.SelectedValue = getItem.WorkPackageId;
                        }
                    }
                    this.txtWorkPackageSize.Text = getItem.WorkPackageSize;
                    this.txtExpectedStartTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getItem.ExpectedStartTime);
                    this.txtExpectedEndTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", getItem.ExpectedEndTime);
                    if (getItem.IsArgument == true)
                    {
                        this.rblIsArgument.SelectedValue = "True";
                    }
                    else
                    {
                        this.rblIsArgument.SelectedValue = "False";
                    }
                    this.drpUnitId.SelectedValue = getItem.UnitId;
                }
                else
                {
                    var getMax = BLL.ExpertArgumentService.getViewLargerHazardListItem.Where(x => x.LargerHazardListId == this.LargerHazardListId).Max(x => x.SortIndex);
                    this.txtSortIndex.Text = ((getMax ?? 0) + 1).ToString();
                }
            }
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
            var getItem = BLL.ExpertArgumentService.getViewLargerHazardListItem.FirstOrDefault(x => x.LargerHazardListItemId == this.LargerHazardListItemId);
            if (getItem != null)
            {
                BLL.ExpertArgumentService.getViewLargerHazardListItem.Remove(getItem);
            }
            Model.View_Solution_LargerHazardListItem newItem = new Model.View_Solution_LargerHazardListItem
            {
                LargerHazardListItemId = SQLHelper.GetNewID(),
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text.Trim()),
                LargerHazardListId = this.LargerHazardListId,
                UnitWorkId = this.drpUnitWorkId.SelectedValue,
                UnitWorkName = this.drpUnitWorkId.SelectedText,
                WorkPackageId = this.drpWorkPackageId.SelectedValue,
                PackageContent = this.drpWorkPackageId.SelectedText,
                WorkPackageSize = this.txtWorkPackageSize.Text.Trim(),
                ExpectedStartTime = Funs.GetNewDateTime(this.txtExpectedStartTime.Text),
                ExpectedEndTime = Funs.GetNewDateTime(this.txtExpectedEndTime.Text),
                ExpectedTime= this.txtExpectedStartTime.Text+"至"+ Funs.GetNewDateTime(this.txtExpectedEndTime.Text),
                IsArgument = Convert.ToBoolean(this.rblIsArgument.SelectedValue),
                IsArgumentName = this.rblIsArgument.SelectedItem.Text,
                UnitId = this.drpUnitId.SelectedValue,
                UnitName = this.drpUnitId.SelectedText,
            };
            BLL.ExpertArgumentService.getViewLargerHazardListItem.Add(newItem);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        protected void drpUnitWorkId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpWorkPackageId.Items.Clear();
            BLL.WorkPackageService.InitWorkPackagesDropDownListByUnitWorkId(this.drpWorkPackageId, this.drpUnitWorkId.SelectedValue, true);
        }
    }
}