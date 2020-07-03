using BLL;
using System;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class CheckItemEdit : PageBase
    {
        #region 定义项

        /// <summary>
        /// 上级检查项
        /// </summary>
        public string SupCheckItem
        {
            get
            {
                return (string)ViewState["SupCheckItem"];
            }
            set
            {
                ViewState["SupCheckItem"] = value;
            }
        }

        /// <summary>
        /// 检查项
        /// </summary>
        public string CheckItemSetId
        {
            get
            {
                return (string)ViewState["CheckItemSetId"];
            }
            set
            {
                ViewState["CheckItemSetId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 角色编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                this.CheckItemSetId = Request.Params["checkItemSetId"];
                this.SupCheckItem = Request.Params["supCheckItem"];

                if (!string.IsNullOrEmpty(this.CheckItemSetId))
                {
                    var checkItemSet = BLL.Technique_CheckItemSetService.GetCheckItemSetById(this.CheckItemSetId);
                    if (checkItemSet != null)
                    {
                        this.txtCheckItemName.Text = checkItemSet.CheckItemName;
                        if (checkItemSet.IsEndLever == true)
                        {
                            this.chkIsEndLevel.Checked = true;
                        }
                        else
                        {
                            chkIsEndLevel.Checked = false;
                        }
                        this.txtMapCode.Text = checkItemSet.MapCode;
                        this.txtSortIndex.Text = checkItemSet.SortIndex.ToString();
                    }
                    // 是末级存在明细 或者 不是末级存在下级 不修改是否末级菜单
                    this.chkIsEndLevel.Enabled = BLL.Technique_CheckItemSetService.IsDeleteCheckItemSet(this.CheckItemSetId);
                }
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string staName = this.txtCheckItemName.Text.Trim();
            if (!string.IsNullOrEmpty(staName))
            {
                if (!BLL.Technique_CheckItemSetService.IsExistCheckItemName(this.CheckItemSetId, this.SupCheckItem, staName))
                {
                    Model.Technique_CheckItemSet checkItemSet = new Model.Technique_CheckItemSet
                    {
                        CheckItemName = staName,
                        SupCheckItem = this.SupCheckItem,
                        MapCode = this.txtMapCode.Text.Trim(),
                        SortIndex = Funs.GetNewIntOrZero(this.txtSortIndex.Text.Trim()),
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked),
                        CheckType = Request.Params["checkType"]
                    };

                    if (string.IsNullOrEmpty(this.CheckItemSetId))
                    {
                        checkItemSet.CheckItemSetId = SQLHelper.GetNewID(typeof(Model.Technique_CheckItemSet));
                        BLL.Technique_CheckItemSetService.AddCheckItemSet(checkItemSet);
                        BLL.LogService.AddSys_Log(this.CurrUser, checkItemSet.MapCode, checkItemSet.CheckItemSetId, BLL.Const.TechniqueCheckItemSetMenuId, Const.BtnAdd);
                    }
                    else
                    {
                        checkItemSet.CheckItemSetId = this.CheckItemSetId;
                        BLL.Technique_CheckItemSetService.UpdateCheckItemSet(checkItemSet);
                        BLL.LogService.AddSys_Log(this.CurrUser, checkItemSet.MapCode, checkItemSet.CheckItemSetId, BLL.Const.TechniqueCheckItemSetMenuId, Const.BtnModify);
                    }
                }
                else
                {
                    Alert.ShowInParent("检查项目名称已存在！");
                }

                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("检查项目名称不能为空！");
            }
        }
    }
}