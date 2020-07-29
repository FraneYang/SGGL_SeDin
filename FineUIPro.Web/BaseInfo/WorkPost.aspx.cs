using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;


namespace FineUIPro.Web.BaseInfo
{
    public partial class WorkPost : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                Funs.DropDownPageSize(this.ddlPageSize);
                //岗位类型            
                BLL.ConstValue.InitConstValueDropDownList(this.drpPostType, ConstValue.Group_PostType, true);
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT wp.WorkPostId,wp.WorkPostName, wp.WorkPostCode, wp.Remark,wp.PostType,wp.IsHsse,
                                            case wp.IsHsse when 1 then '是' else '否' end as IsHsseStr,const.ConstText as PostTypeName
                                            FROM dbo.Base_WorkPost AS wp
                                            LEFT JOIN Sys_Const AS const ON const.ConstValue = wp.PostType and const.GroupId = '"+ConstValue.Group_PostType+"' ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            //strSql += " AND HazardList.ProjectId = @ProjectId";
            //listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_WorkPost> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_WorkPost> source = (from x in Funs.DB.Base_WorkPost orderby x.PostType, x.WorkPostCode select x).ToList();
            List<Model.Base_WorkPost> paged = new List<Model.Base_WorkPost>();

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
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL.LogService.AddSys_Log(this.CurrUser, this.txtWorkPostCode.Text, hfFormID.Text, BLL.Const.WorkPostMenuId, BLL.Const.BtnDelete);
            BLL.WorkPostService.DeleteWorkPostById(hfFormID.Text);
            
            // 重新绑定表格，并模拟点击[新增按钮]
            BindGrid();
            PageContext.RegisterStartupScript("onNewButtonClick();");
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
                    var getV = BLL.WorkPostService.GetWorkPostById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.WorkPostCode, getV.WorkPostId, BLL.Const.WorkPostMenuId, BLL.Const.BtnDelete);
                        BLL.WorkPostService.DeleteWorkPostById(rowID);
                    }
                }

                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var workPost = BLL.WorkPostService.GetWorkPostById(Id);
            if (workPost != null)
            {
                this.txtWorkPostCode.Text = workPost.WorkPostCode;
                this.txtWorkPostName.Text = workPost.WorkPostName;
                if (!string.IsNullOrEmpty(workPost.PostType))
                {
                    this.drpPostType.SelectedValue = workPost.PostType;
                }
                if (workPost.IsHsse == true)
                {
                    this.ckbIsHsse.Checked = true;
                }
                this.txtRemark.Text = workPost.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpPostType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择类型！", MessageBoxIcon.Warning);
                return;
            }
            string strRowID = hfFormID.Text;
            Model.Base_WorkPost newWorkPost = new Model.Base_WorkPost
            {
                WorkPostCode = this.txtWorkPostCode.Text.Trim(),
                WorkPostName = this.txtWorkPostName.Text.Trim(),
                PostType = this.drpPostType.SelectedValue,
                IsHsse = Convert.ToBoolean(this.ckbIsHsse.Checked),
                Remark = txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                newWorkPost.WorkPostId = SQLHelper.GetNewID(typeof(Model.Base_WorkPost));
                BLL.WorkPostService.AddWorkPost(newWorkPost);
                BLL.LogService.AddSys_Log(this.CurrUser, newWorkPost.WorkPostCode, newWorkPost.WorkPostId, BLL.Const.WorkPostMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newWorkPost.WorkPostId = strRowID;
                BLL.WorkPostService.UpdateWorkPost(newWorkPost);
                BLL.LogService.AddSys_Log(this.CurrUser, newWorkPost.WorkPostCode, newWorkPost.WorkPostId, BLL.Const.WorkPostMenuId, BLL.Const.BtnDelete);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, newWorkPost.WorkPostId));
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.WorkPostMenuId);
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

        #region 验证岗位名称、编号是否存在
        /// <summary>
        /// 验证岗位名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_WorkPost.FirstOrDefault(x => x.WorkPostCode == this.txtWorkPostCode.Text.Trim() && (x.WorkPostId != hfFormID.Text || (hfFormID.Text == null && x.WorkPostId != null)));
            if (q != null)
            {
                ShowNotify("输入的岗位编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_WorkPost.FirstOrDefault(x => x.WorkPostName == this.txtWorkPostName.Text.Trim() && (x.WorkPostId != hfFormID.Text || (hfFormID.Text == null && x.WorkPostId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的岗位名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertPostType(object PostType)
        {
            string name = string.Empty;
            if (PostType != null)
            {
                string postType = PostType.ToString().Trim();
                Model.Sys_Const c = ConstValue.drpConstItemList(ConstValue.Group_PostType).FirstOrDefault(x => x.ConstValue == postType);
                if (c != null)
                {
                    name = c.ConstText;
                }
            }
            return name;
        }
        #endregion
    }
}