using Aspose.Words;
using Aspose.Words.Tables;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class FileCabinet : PageBase
    {
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.FileCabinetMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;

                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion
        /// <summary>
        /// 是否删除
        /// </summary>
        public string delId
        {
            get
            {
                return (string)ViewState["delId"];
            }
            set
            {
                ViewState["delId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("FileCabinetEdit.aspx") + "return false;";
            }

        }

        private void BindGrid()
        {
            var list = FileCabinetService.getList(CurrUser.LoginProjectId);
            gvFile.RecordCount = list.Count;
            DataTable tb = GetFilteredTable(gvFile.FilteredData, LINQToDataTable(list));
            var table = GetPagedDataTable(gvFile, tb);
            gvFile.DataSource = table;
            gvFile.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvFile.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }



        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (gvFile.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string fileId = gvFile.SelectedRowID.Split(',')[0];
            string url = "FileCabinetEdit.aspx?action=view&FileCabinetId=" + fileId;
            if (!string.IsNullOrEmpty(url))
            {
                PageContext.RegisterStartupScript(windows_tt.GetShowReference(url));
            }

        }

        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (gvFile.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string fileId = gvFile.SelectedRowID.Split(',')[0];
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("FileCabinetEdit.aspx?FileCabinetId={0}", fileId), "编辑 - "));
        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {

            if (gvFile.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string fileId = gvFile.SelectedRowID.Split(',')[0];
            var file = FileCabinetService.getInfo(fileId);
            FileCabinetService.DeleteFileCabinet(fileId);
            if (file != null)
            {
                LogService.AddSys_Log(CurrUser, file.FileCode, fileId, "删除文件柜-重要文件", CurrUser.LoginProjectId);
            }

            BindGrid();
            Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
        }
        #endregion

        protected void gvFile_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = gvFile.DataKeys[e.RowIndex];
            string fileId = string.Empty;
            if (keys == null)
            {
                return;
            }
            else
            {
                fileId = keys[0].ToString();
            }
            if (e.CommandName.Equals("download"))
            {
                PageContext.RegisterStartupScript(Windowtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/FileCabinet&menuId={2}",
                -1, fileId, Const.FileCabinetMenuId)));
            }

        }
    }
}