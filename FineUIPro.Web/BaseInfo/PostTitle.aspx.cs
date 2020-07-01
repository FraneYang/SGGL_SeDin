using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class PostTitle : PageBase
    {
        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
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
            var q = from x in Funs.DB.Base_PostTitle orderby x.PostTitleCode select x;
            Grid1.RecordCount = q.Count();
            // 2.获取当前分页数据
            var table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_PostTitle> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_PostTitle> source = (from x in BLL.Funs.DB.Base_PostTitle orderby x.PostTitleCode select x).ToList();
            List<Model.Base_PostTitle> paged = new List<Model.Base_PostTitle>();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Count())
            {
                rowend = source.Count();
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.Add(source[i]);
            }

            return paged;
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (judgementDelete(hfFormID.Text, true))
            {
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtPostTitleCode.Text, hfFormID.Text, BLL.Const.PostTitleMenuId, BLL.Const.BtnDelete);
                BLL.PostTitleService.DeletePostTitleById(hfFormID.Text);
               
                // 重新绑定表格，并模拟点击[新增按钮]
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, true))
                    {
                        var getV = BLL.PostTitleService.GetPostTitleById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.PostTitleCode, getV.PostTitleId, BLL.Const.PostTitleMenuId, BLL.Const.BtnDelete);
                            BLL.PostTitleService.DeletePostTitleById(rowID);
                        }
                    }
                }

                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            if (Funs.DB.SitePerson_Person.FirstOrDefault(x => x.PostTitleId == id) != null)
            {
                content = "该职称已在【人员信息】中使用，不能删除！";
            }
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！");
                return;
            }
            string Id = Grid1.SelectedRowID;
            var postTitle = BLL.PostTitleService.GetPostTitleById(Id);
            if (postTitle != null)
            {
                this.txtPostTitleCode.Text = postTitle.PostTitleCode;
                this.txtPostTitleName.Text = postTitle.PostTitleName;
                this.txtRemark.Text = postTitle.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
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
            string strRowID = hfFormID.Text;
            Model.Base_PostTitle newPostTitle = new Model.Base_PostTitle
            {
                PostTitleCode = this.txtPostTitleCode.Text.Trim(),
                PostTitleName = this.txtPostTitleName.Text.Trim(),
                Remark = txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                newPostTitle.PostTitleId = SQLHelper.GetNewID(typeof(Model.Base_PostTitle));
                BLL.PostTitleService.AddPostTitle(newPostTitle);
                BLL.LogService.AddSys_Log(this.CurrUser, newPostTitle.PostTitleCode, newPostTitle.PostTitleId, BLL.Const.PostTitleMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newPostTitle.PostTitleId = strRowID;
                BLL.PostTitleService.UpdatePostTitle(newPostTitle);
                BLL.LogService.AddSys_Log(this.CurrUser, newPostTitle.PostTitleCode, newPostTitle.PostTitleId, BLL.Const.PostTitleMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, newPostTitle.PostTitleId));
        }
        #endregion

        #region 验证职称信息名称、编号是否存在
        /// <summary>
        /// 验证职称信息名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_PostTitle.FirstOrDefault(x => x.PostTitleCode == this.txtPostTitleCode.Text.Trim() && (x.PostTitleId != hfFormID.Text || (hfFormID.Text == null && x.PostTitleId != null)));
            if (q != null)
            {
                ShowNotify("输入的职称编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_PostTitle.FirstOrDefault(x => x.PostTitleName == this.txtPostTitleName.Text.Trim() && (x.PostTitleId != hfFormID.Text || (hfFormID.Text == null && x.PostTitleId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的职称名称已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PostTitleMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}