using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.WeldingReport
{
    public partial class JointShowColumn : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListItem[] list = new ListItem[17];
                list[0] = new ListItem("材质1", "2");
                list[1] = new ListItem("材质2", "3");
                list[2] = new ListItem("达因", "4");
                list[3] = new ListItem("外径", "5");
                list[4] = new ListItem("壁厚", "6");
                list[5] = new ListItem("规格", "7");
                list[6] = new ListItem("焊缝类型", "8");
                list[7] = new ListItem("组件1号", "9");
                list[8] = new ListItem("组件2号", "10");
                list[9] = new ListItem("对应WPS", "11");
                list[10] = new ListItem("坡口类型", "12");
                list[11] = new ListItem("焊接方法", "13");
                list[12] = new ListItem("焊丝", "14");
                list[13] = new ListItem("焊条", "15");
                list[14] = new ListItem("预热温度", "16");
                list[15] = new ListItem("是否热处理", "17");
                list[16] = new ListItem("备注", "18");

                this.cblColumn.DataSource = list;
                this.cblColumn.DataBind();

                Model.Sys_UserShowColumns c = BLL.Sys_UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "JointOut");
                if (c != null)
                {
                    if (!string.IsNullOrEmpty(c.Columns))
                    {
                        List<string> columns = c.Columns.Split(',').ToList();

                        foreach (var item in this.cblColumn.Items)
                        {
                            if (columns.Contains(item.Value))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 全选
        /// <summary>
        /// 全选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            int count = this.cblColumn.Items.Count;
            for (int i = 0; i < count; i++)
            {
                this.cblColumn.Items[i].Selected = ckAll.Checked;
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
            string cl = string.Empty;
            int count = this.cblColumn.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.cblColumn.Items[i].Selected)
                {
                    cl += this.cblColumn.Items[i].Value + ",";
                }
            }
            if (cl != "")
            {
                cl = cl.Substring(0, cl.LastIndexOf(","));
                Model.Sys_UserShowColumns columns = new Model.Sys_UserShowColumns();
                Model.Sys_UserShowColumns c = BLL.Sys_UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "JointOut");
                if (c == null)
                {
                    columns.UserId = this.CurrUser.UserId;
                    columns.Columns = cl;
                    columns.ShowType = "JointOut";
                    BLL.Sys_UserShowColumnsService.AddUserShowColumns(columns);
                }
                else
                {
                    c.Columns = cl;
                    BLL.Sys_UserShowColumnsService.UpdateUserShowColumns(c);
                }
            }
            else
            {
                Model.Sys_UserShowColumns c = BLL.Sys_UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "JointOut");
                BLL.Sys_UserShowColumnsService.DeleteUserShowColumns(c.ShowColumnId);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}