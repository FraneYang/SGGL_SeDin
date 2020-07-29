using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.BaseInfo
{
    public partial class PracticeCertificate : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            var q = from x in Funs.DB.Base_PracticeCertificate orderby x.PracticeCertificateCode select x;
            Grid1.RecordCount = q.Count();
            // 2.获取当前分页数据
            var table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);
            Grid1.DataSource = table;
            Grid1.DataBind();
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

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_PracticeCertificate> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_PracticeCertificate> source = (from x in Funs.DB.Base_PracticeCertificate orderby x.PracticeCertificateCode select x).ToList();
            List<Model.Base_PracticeCertificate> paged = new List<Model.Base_PracticeCertificate>();

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
        #endregion

        #region 分页下拉选择
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
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var getV = BLL.PracticeCertificateService.GetPracticeCertificateById(hfFormID.Text);
            if (getV != null)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, getV.PracticeCertificateCode, getV.PracticeCertificateId, BLL.Const.PracticeCertificateMenuId, BLL.Const.BtnDelete);
                BLL.PracticeCertificateService.DeletePracticeCertificateById(getV.PracticeCertificateId);

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
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();

                    var getV = BLL.PracticeCertificateService.GetPracticeCertificateById(hfFormID.Text);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.PracticeCertificateCode, getV.PracticeCertificateId, BLL.Const.PracticeCertificateMenuId, BLL.Const.BtnDelete);
                        BLL.PracticeCertificateService.DeletePracticeCertificateById(getV.PracticeCertificateId);
                    }
                }
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
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
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var PracticeCertificate = BLL.PracticeCertificateService.GetPracticeCertificateById(Id);
            if (PracticeCertificate != null)
            {
                this.txtPracticeCertificateCode.Text = PracticeCertificate.PracticeCertificateCode;
                this.txtPracticeCertificateName.Text = PracticeCertificate.PracticeCertificateName;
                this.txtRemark.Text = PracticeCertificate.Remark;
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
            Model.Base_PracticeCertificate PracticeCertificate = new Model.Base_PracticeCertificate
            {
                PracticeCertificateCode = this.txtPracticeCertificateCode.Text.Trim(),
                PracticeCertificateName = this.txtPracticeCertificateName.Text.Trim(),
                Remark = txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                PracticeCertificate.PracticeCertificateId = SQLHelper.GetNewID(typeof(Model.Base_PracticeCertificate));
                BLL.PracticeCertificateService.AddPracticeCertificate(PracticeCertificate);
                BLL.LogService.AddSys_Log(this.CurrUser, PracticeCertificate.PracticeCertificateCode, PracticeCertificate.PracticeCertificateId, BLL.Const.PracticeCertificateMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                PracticeCertificate.PracticeCertificateId = strRowID;
                BLL.PracticeCertificateService.UpdatePracticeCertificate(PracticeCertificate);
                BLL.LogService.AddSys_Log(this.CurrUser, PracticeCertificate.PracticeCertificateCode, PracticeCertificate.PracticeCertificateId, BLL.Const.PracticeCertificateMenuId, BLL.Const.BtnModify);
            }

            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, PracticeCertificate.PracticeCertificateId));
        }
        #endregion

        #region 验证证书名称、编号是否存在
        /// <summary>
        /// 验证证书名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_PracticeCertificate.FirstOrDefault(x => x.PracticeCertificateCode == this.txtPracticeCertificateCode.Text.Trim() && (x.PracticeCertificateId != hfFormID.Text || (hfFormID.Text == null && x.PracticeCertificateId != null)));
            if (q != null)
            {
                ShowNotify("输入的证书编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_PracticeCertificate.FirstOrDefault(x => x.PracticeCertificateName == this.txtPracticeCertificateName.Text.Trim() && (x.PracticeCertificateId != hfFormID.Text || (hfFormID.Text == null && x.PracticeCertificateId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的证书名称已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PracticeCertificateMenuId);
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