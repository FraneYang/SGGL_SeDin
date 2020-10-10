using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using System.Data;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.HJGL.HotProcessHard
{
    public partial class HotProessTrustEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 热处理委托主键
        /// </summary>
        public string HotProessTrustId
        {
            get
            {
                return (string)ViewState["HotProessTrustId"];
            }
            set
            {
                ViewState["HotProessTrustId"] = value;
            }
        }

        ///// <summary>
        ///// 项目主键
        ///// </summary>
        //public string ProjectId
        //{
        //    get
        //    {
        //        return (string)ViewState["ProjectId"];
        //    }
        //    set
        //    {
        //        ViewState["ProjectId"] = value;
        //    }
        //}
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
                this.HotProessTrustId = Request.Params["HotProessTrustId"];

                //this.ProjectId = Request.Params["ProjectId"];
                //var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
                //if (trust != null)
                //{
                //    this.ProjectId = trust.ProjectId;
                //}
                BLL.UnitWorkService.InitUnitWorkDownList(this.drpUnitWork, this.CurrUser.LoginProjectId, true);//单位工程
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, true);//单位

                this.PageInfoLoad(); ///加载页面 

                List<Model.View_HJGL_HotProess_TrustItem> lists = BLL.HotProessTrustItemService.GetViewHotProessTrustItem(this.CurrUser.LoginProjectId, this.HotProessTrustId);
                this.BindGrid(lists); ////初始化页面
            }
        }
        #endregion

        #region 加载页面输入提交信息
        /// <summary>
        /// 加载页面输入提交信息
        /// </summary>
        private void PageInfoLoad()
        {
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
            if (trust != null)
            {
                //this.ProjectId = trust.ProjectId;
                this.txtHotProessTrustNo.Text = trust.HotProessTrustNo;
                if (trust.ProessDate.HasValue)
                {
                    this.txtProessDate.Text = string.Format("{0:yyyy-MM-dd}", trust.ProessDate);
                }
                if (!string.IsNullOrEmpty(trust.UnitWorkId))
                {
                    this.drpUnitWork.SelectedValue = trust.UnitWorkId;
                }
                if (!string.IsNullOrEmpty(trust.UnitId))
                {
                    this.drpUnitId.SelectedValue = trust.UnitId;
                }
                if (!string.IsNullOrEmpty(trust.Tabler))
                {
                    this.txtTabler.Text = BLL.UserService.GetUserNameByUserId(trust.Tabler);
                }
                this.txtRemark.Text = trust.Remark;
            }
            else
            {
                string unitWorkId = Request.Params["unitWorkId"];

                if (!string.IsNullOrEmpty(unitWorkId))
                {
                    var w = BLL.UnitWorkService.getUnitWorkByUnitWorkId(unitWorkId);
                    drpUnitId.SelectedValue = w.UnitId;
                    this.drpUnitWork.SelectedValue = w.UnitWorkId;
                }
                
                    
                this.txtTabler.Text = this.CurrUser.UserName;
                this.SimpleForm1.Reset(); //重置所有字段
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid(List<Model.View_HJGL_HotProess_TrustItem> lists)
        {
            DataTable tb = this.LINQToDataTable(lists);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 查找需要热处理的焊口
        /// <summary>
        /// 查找需要热处理的焊口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckSelect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpUnitId.SelectedValue) && this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                string weldJointIds = string.Empty;
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string jotId = Grid1.DataKeys[i][0].ToString();
                    weldJointIds += jotId + "|";
                }
                if (weldJointIds != string.Empty)
                {
                    weldJointIds = weldJointIds.Substring(0, weldJointIds.Length - 1);
                }
                string strList = this.drpUnitId.SelectedValue + "|" + this.HotProessTrustId;
                string window = String.Format("HotProessTrustItemEdit.aspx?strList={0}&weldJointIds={1}", strList, weldJointIds, "编辑 - ");
                PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdItemsString.ClientID) + Window1.GetShowReference(window));
            }
            else
            {
                Alert.ShowInTop("请选择单位！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Grid 关闭弹出窗口事件
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            string weldJointIds = string.Empty;
            if (!string.IsNullOrEmpty(hdItemsString.Text))
            {
                weldJointIds = hdItemsString.Text.Substring(0, hdItemsString.Text.LastIndexOf('|'));

                List<Model.View_HJGL_HotProess_TrustItem> lists = BLL.HotProess_TrustService.GetHotProessTrustAddItem(weldJointIds);
                this.BindGrid(lists);
            }
        }
        #endregion

        #region 热处理委托 提交事件
        /// <summary>
        /// 编辑热处理委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HJGL_HotProessTrustMenuId, Const.BtnSave))
            {
                if (BLL.HotProess_TrustService.IsExistTrustCode(this.txtHotProessTrustNo.Text, this.HotProessTrustId, this.CurrUser.LoginProjectId))
                {
                    ShowNotify("委托单号已存在，请重新录入!", MessageBoxIcon.Warning);
                    return;
                }
                string unitWorkId = Request.Params["unitWorkId"];
                Model.HJGL_HotProess_Trust newHotProessTrust = new Model.HJGL_HotProess_Trust();
                newHotProessTrust.HotProessTrustNo = this.txtHotProessTrustNo.Text.Trim();
                newHotProessTrust.ProessDate = Funs.GetNewDateTime(this.txtProessDate.Text.Trim());
                if (this.drpUnitWork.SelectedValue != BLL.Const._Null)
                {
                    newHotProessTrust.UnitWorkId = this.drpUnitWork.SelectedValue;
                }
                newHotProessTrust.ProjectId = this.CurrUser.LoginProjectId;

                if (this.drpUnitId.SelectedValue != BLL.Const._Null)
                {
                    newHotProessTrust.UnitId = this.drpUnitId.SelectedValue;
                }
                newHotProessTrust.Tabler = this.CurrUser.UserId;
                newHotProessTrust.Remark = this.txtRemark.Text.Trim();
                if (!string.IsNullOrEmpty(this.HotProessTrustId))
                {
                    newHotProessTrust.HotProessTrustId = this.HotProessTrustId;
                    BLL.HotProess_TrustService.UpdateHotProessTrust(newHotProessTrust);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_3, this.CurrUser.LoginProjectId, this.CurrUser.UserId, Resources.Lan.ModifyPWHT);
                }
                else
                {
                    this.HotProessTrustId = SQLHelper.GetNewID(typeof(Model.HJGL_HotProess_Trust));
                    newHotProessTrust.HotProessTrustId = this.HotProessTrustId;
                    BLL.HotProess_TrustService.AddHotProessTrust(newHotProessTrust);
                    //BLL.Sys_LogService.AddLog(BLL.Const.System_3,this.CurrUser.LoginProjectId, this.CurrUser.UserId, Resources.Lan.AddPWHT);
                }
                BLL.HotProessTrustItemService.DeleteHotProessTrustItemById(this.HotProessTrustId);
                this.CollectGridJointInfo();//收集Grid页面信息,增加明细
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(newHotProessTrust.HotProessTrustId)
                  + ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 收集Grid页面信息,提交明细
        /// <summary>
        /// 收集Grid页面信息,提交明细
        /// </summary>
        /// <returns></returns>
        private void CollectGridJointInfo()
        {
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                JObject values = mergedRow.Value<JObject>("values");

                Model.HJGL_HotProess_TrustItem newTrustItem = new Model.HJGL_HotProess_TrustItem();
                newTrustItem.HotProessTrustId = this.HotProessTrustId;
                newTrustItem.WeldJointId = values.Value<string>("WeldJointId").ToString();
                string hotProessTrustItemId = values.Value<string>("HotProessTrustItemId").ToString();
                if (!string.IsNullOrEmpty(hotProessTrustItemId))
                {
                    newTrustItem.HotProessTrustItemId = hotProessTrustItemId;
                }
                BLL.HotProessTrustItemService.AddHotProessTrustItem(newTrustItem);
            }
        }
        #endregion     

        #region 右键删除事件
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.hdItemsString.Text))
            {
                this.hdItemsString.Text = this.hdItemsString.Text.Substring(0, this.hdItemsString.Text.LastIndexOf('|'));
            }
            var trust = BLL.HotProess_TrustService.GetHotProessTrustById(this.HotProessTrustId);
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                List<Model.View_HJGL_HotProess_TrustItem> GetHotProessTrustItem = new List<Model.View_HJGL_HotProess_TrustItem>();
                if (!string.IsNullOrEmpty(this.hdItemsString.Text))
                {
                    GetHotProessTrustItem = BLL.HotProess_TrustService.GetHotProessTrustAddItem(this.hdItemsString.Text);
                }
                else if (string.IsNullOrEmpty(this.hdItemsString.Text) && this.HotProessTrustId != null)
                {
                    GetHotProessTrustItem = BLL.HotProess_TrustService.GetHotProessTrustItem(this.CurrUser.LoginProjectId, this.HotProessTrustId);
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var item = GetHotProessTrustItem.FirstOrDefault(x => x.WeldJointId == rowID);
                    if (item != null)
                    {
                        if (string.IsNullOrEmpty(this.HotProessTrustId))   //新增记录可直接删除
                        {
                            GetHotProessTrustItem.Remove(item);
                        }
                    }
                }
                BindGrid(GetHotProessTrustItem);
                ShowNotify("删除成功！", MessageBoxIcon.Success);
            }
        }
        #endregion        
    }
}