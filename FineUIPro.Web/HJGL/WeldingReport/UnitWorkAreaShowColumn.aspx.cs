using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HJGL.WeldingReport
{
    public partial class UnitWorkAreaShowColumn : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListItem[] list = new ListItem[35];
                list[0] = new ListItem("序号", "0");
                list[1] = new ListItem("单位名称", "1");
                list[2] = new ListItem("单位工程名称", "2");
                list[4] = new ListItem("总焊口", "3");
                list[5] = new ListItem("预制总焊口", "4");
                list[6] = new ListItem("安装总焊口", "5");
                list[7] = new ListItem("切除焊口", "6");
                list[8] = new ListItem("总达因数", "7");
                list[9] = new ListItem("预制总达因", "8");
                list[10] = new ListItem("安装总达因", "9");
                list[11] = new ListItem("本期完成焊口数", "10");
                list[12] = new ListItem("本期完成预制焊口数", "11");
                list[13] = new ListItem("本期完成安装焊口数", "12");
                list[14] = new ListItem("本期完成比例", "13");
                list[15] = new ListItem("本期预制完成比例", "14");
                list[16] = new ListItem("本期安装完成比例", "15");
                list[17] = new ListItem("本期完成达因", "16");
                list[18] = new ListItem("本期完成预制达因", "17");
                list[19] = new ListItem("本期完成安装达因", "18");
                list[20] = new ListItem("本期完成达因比例", "19");
                list[21] = new ListItem("本期完成预制达因比例", "20");
                list[22] = new ListItem("本期完成安装达因比例", "21");
                list[23] = new ListItem("完成焊口", "22");
                list[24] = new ListItem("完成预制焊口", "23");
                list[25] = new ListItem("完成安装焊口", "24");
                list[26] = new ListItem("完成比例", "25");
                list[27] = new ListItem("安装完成比例", "26");
                list[28] = new ListItem("预制完成比例", "27");

                list[29] = new ListItem("完成达因", "28");
                list[30] = new ListItem("完成预制达因", "29");
                list[31] = new ListItem("完成安装达因", "30");
                list[32] = new ListItem("完成达因比例", "31");
                list[33] = new ListItem("完成预制达因比例", "32");
                list[34] = new ListItem("完成安装达因比例", "33");

                this.cblColumn.DataSource = list;
                this.cblColumn.DataBind();

                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "UnitWorkAreaAnalyze");
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

        #region 提交
        /// <summary>
        /// 提交按钮
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
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "UnitWorkAreaAnalyze");
                if (c == null)
                {
                    columns.UserId = this.CurrUser.UserId;
                    columns.Columns = cl;
                    columns.ShowType = "UnitWorkAreaAnalyze";
                    BLL.UserShowColumnsService.AddUserShowColumns(columns);
                }
                else
                {
                    c.Columns = cl;
                    BLL.UserShowColumnsService.UpdateUserShowColumns(c);
                }
            }
            else
            {
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "UnitWorkAreaAnalyze");
                BLL.UserShowColumnsService.DeleteUserShowColumns(c.ShowColumnId);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}