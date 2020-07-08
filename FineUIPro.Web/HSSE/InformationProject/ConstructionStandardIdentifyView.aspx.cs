using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class ConstructionStandardIdentifyView : PageBase
    {
        #region  定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ConstructionStandardIdentifyId
        {
            get
            {
                return (string)ViewState["ConstructionStandardIdentifyId"];
            }
            set
            {
                ViewState["ConstructionStandardIdentifyId"] = value;
            }
        }
        ///// <summary>
        ///// 选中项
        ///// </summary>
        //public string[] arr
        //{
        //    get
        //    {
        //        return (string[])ViewState["arr"];
        //    }
        //    set
        //    {
        //        ViewState["arr"] = value;
        //    }
        //}

        public List<string> ItemSelectedList2
        {
            get
            {
                return (List<string>)ViewState["ItemSelectedList2"];
            }
            set
            {
                ViewState["ItemSelectedList2"] = value;
            }
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();              
                BLL.ConstValue.InitConstValueDropDownList(this.drpCNProfessional, BLL.ConstValue.Group_CNProfessional, true);

                //this.ItemSelectedList = new List<string>();
                this.ConstructionStandardIdentifyId = Request.Params["ConstructionStandardIdentifyId"];
                if (!string.IsNullOrEmpty(this.ConstructionStandardIdentifyId))
                {
                    Model.InformationProject_ConstructionStandardIdentify constructionStandardIdentify = BLL.ConstructionStandardIdentifyService.GetConstructionStandardIdentifyById(this.ConstructionStandardIdentifyId);
                    if (constructionStandardIdentify != null)
                    {
                        this.txtConstructionStandardIdentifyCode.Text = CodeRecordsService.ReturnCodeByDataId(this.ConstructionStandardIdentifyId);
                        this.txtRemark.Text = constructionStandardIdentify.Remark;
                    }
                    BindGridById(this.ConstructionStandardIdentifyId);//显示选中的项            
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ConstructionStandardIdentifyMenuId;
                this.ctlAuditFlow.DataId = this.ConstructionStandardIdentifyId;
            }
        }

        /// <summary>
        /// 显示勾选的项
        /// </summary>
        private void BindGridById(string constructionStandardIdentifyId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).View_InformationProject_ConstructionStandardSelectedItem
                     where x.ConstructionStandardIdentifyId == constructionStandardIdentifyId
                     orderby x.StandardNo
                     select x).ToList();
            if (!string.IsNullOrEmpty(this.txtStandardGrade.Text.Trim()))
            {
                q = q.Where(e => e.StandardGrade.Contains(this.txtStandardGrade.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtStandardNo.Text.Trim()))
            {
                q = q.Where(e => e.StandardNo.Contains(this.txtStandardNo.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtStandardName.Text.Trim()))
            {
                q = q.Where(e => e.StandardName.Contains(this.txtStandardName.Text.Trim())).ToList();
            }
            if (this.drpCNProfessional.SelectedValue != BLL.Const._Null)
            {
                string code = this.drpCNProfessional.SelectedValue;
                if (code == "1")
                {
                    q = q.Where(e => e.IsSelected1 == true).ToList();
                }
                else if (code == "2")
                {
                    q = q.Where(e => e.IsSelected2 == true).ToList();
                }
                else if (code == "3")
                {
                    q = q.Where(e => e.IsSelected3 == true).ToList();
                }
                else if (code == "4")
                {
                    q = q.Where(e => e.IsSelected4 == true).ToList();
                }
                else if (code == "5")
                {
                    q = q.Where(e => e.IsSelected5 == true).ToList();
                }
                else if (code == "6")
                {
                    q = q.Where(e => e.IsSelected6 == true).ToList();
                }
                else if (code == "7")
                {
                    q = q.Where(e => e.IsSelected7 == true).ToList();
                }
                else if (code == "8")
                {
                    q = q.Where(e => e.IsSelected8 == true).ToList();
                }
                else if (code == "9")
                {
                    q = q.Where(e => e.IsSelected9 == true).ToList();
                }
                else if (code == "10")
                {
                    q = q.Where(e => e.IsSelected10 == true).ToList();
                }
                else if (code == "11")
                {
                    q = q.Where(e => e.IsSelected11 == true).ToList();
                }
                else if (code == "12")
                {
                    q = q.Where(e => e.IsSelected12 == true).ToList();
                }
                else if (code == "13")
                {
                    q = q.Where(e => e.IsSelected13 == true).ToList();
                }
                else if (code == "14")
                {
                    q = q.Where(e => e.IsSelected14 == true).ToList();
                }
                else if (code == "15")
                {
                    q = q.Where(e => e.IsSelected15 == true).ToList();
                }
                else if (code == "16")
                {
                    q = q.Where(e => e.IsSelected16 == true).ToList();
                }
                else if (code == "17")
                {
                    q = q.Where(e => e.IsSelected17 == true).ToList();
                }
                else if (code == "18")
                {
                    q = q.Where(e => e.IsSelected18 == true).ToList();
                }
                else if (code == "19")
                {
                    q = q.Where(e => e.IsSelected19 == true).ToList();
                }
                else if (code == "20")
                {
                    q = q.Where(e => e.IsSelected10 == true).ToList();
                }
                else if (code == "10")
                {
                    q = q.Where(e => e.IsSelected20 == true).ToList();
                }
                else if (code == "21")
                {
                    q = q.Where(e => e.IsSelected21 == true).ToList();
                }
                else if (code == "22")
                {
                    q = q.Where(e => e.IsSelected22 == true).ToList();
                }
                else if (code == "23")
                {
                    q = q.Where(e => e.IsSelected23 == true).ToList();
                }
                else if (code == "90")
                {
                    q = q.Where(e => e.IsSelected90 == true).ToList();
                }
            }

            DataTable tb = this.LINQToDataTable(q);

            // 2.获取当前分页数据
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGridById(this.ConstructionStandardIdentifyId);
        }

        /// <summary>
        /// 分页索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            this.BindGridById(this.ConstructionStandardIdentifyId);
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ConstructionStandardIdentifyId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ConstructionStandardIdentifyAttachUrl&menuId={1}", ConstructionStandardIdentifyId, BLL.Const.ConstructionStandardIdentifyMenuId)));
            }
        }
        #endregion
    }
}