using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class OfficeCheck : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定明细
        /// </summary>
        public void BindGrid()
        {
            string strSql = @"SELECT Item.RectifyItemId,
                                     Item.RectifyId,
                                     Item.WrongContent,
                                     Item.Requirement,
                                     Item.LimitTime,
                                     Item.RectifyResults,
                                     Item.IsRectify,
                                     Rectify.States,
                                     Rectify.ProjectId "
                        + @" FROM ProjectSupervision_RectifyItem AS Item "
                        + @" LEFT JOIN ProjectSupervision_Rectify AS Rectify ON Rectify.RectifyId=Item.RectifyId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " WHERE Rectify.ProjectId = @ProjectId";
            strSql += " AND Rectify.States = @States";
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            listStr.Add(new SqlParameter("@States", this.rbStates.SelectedValue));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 状态筛选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "AttachUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Rectify&menuId={1}&type=0&strParam=1", itemId, BLL.Const.CheckInfoMenuId)));
            }
            if (e.CommandName == "ReAttachUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Rectify&menuId={1}&type=0&strParam=2", itemId, BLL.Const.CheckInfoMenuId)));
                var item = BLL.ProjectSupervision_RectifyItemService.GeRectifyItemById(itemId);
                if (item != null)
                {
                    item.States = "1";
                    Funs.DB.SubmitChanges();
                }
                int i = 0;
                var lists = BLL.ProjectSupervision_RectifyItemService.GetRectifyItemByRectifyId(item.RectifyId);
                foreach (var j in lists)
                {
                    if (j.States == "1")
                    {
                        i++;
                    }
                }
                if (i == lists.Count)
                {
                    var rectify = BLL.ProjectSupervision_RectifyService.GetRectifyById(item.RectifyId);
                    if (rectify != null)
                    {
                        rectify.States = BLL.Const.State_2;
                        BLL.ProjectSupervision_RectifyService.UpdateRectify(rectify);
                    }
                }
            }
        }
    }
}