using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.InformationProject
{
    public partial class CreateLawRegulationIdentify : PageBase
    {
        #region  定义项
        /// <summary>
        /// 选中项
        /// </summary>
        public string[] arr
        {
            get
            {
                return (string[])ViewState["arr"];
            }
            set
            {
                ViewState["arr"] = value;
            }
        }

        /// <summary>
        /// 法律法规ID
        /// </summary>
        public string LawRegulationIds
        {
            get
            {
                return (string)ViewState["LawRegulationIds"];
            }
            set
            {
                ViewState["LawRegulationIds"] = value;
            }
        }
        #endregion

        #region  页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LawRegulationIds = Request.Params["LawRegulationIds"];
              
                // 绑定表格
                this.BindGrid();
                List<string> lists = Funs.GetStrListByStr(this.LawRegulationIds, ',');
                if (lists.Count() > 0)
                {
                    arr = new string[lists.Count()];
                    int i = 0;
                    foreach (var q in lists)
                    {
                        arr[i] = q;
                        i++;
                    }

                    Grid1.SelectedRowIDArray = arr;
                } 
            }
        }
        #endregion

        #region  保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {          
            if (Grid1.SelectedRowIDArray.Count() > 0)
            {               
                int[] selections = Grid1.SelectedRowIndexArray;
                string lawIds= string.Empty;
                foreach (int rowIndex in selections)
                {
                    lawIds += Grid1.DataKeys[rowIndex][0].ToString() + ",";
                }

                PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(lawIds)
                    + ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }
        }
        #endregion

        #region  绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var laws = from x in new Model.SGGLDB(Funs.ConnString).View_Law_LawRegulationList select x;
            if (!string.IsNullOrEmpty(this.txtLawRegulationCode.Text.Trim()))
            {
                laws = laws.Where(x => x.LawRegulationCode.Contains(this.txtLawRegulationCode.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtLawsRegulationsTypeName.Text.Trim()))
            {
                laws = laws.Where(x => x.LawsRegulationsTypeName.Contains(this.txtLawsRegulationsTypeName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtLawRegulationName.Text.Trim()))
            {
                laws = laws.Where(x => x.LawRegulationName.Contains(this.txtLawRegulationName.Text.Trim()));
            }
            var q = from x in laws
                    orderby x.LawRegulationCode
                    select new
                    {
                        x.LawRegulationId,
                        x.LawRegulationCode,
                        x.LawRegulationName,
                        x.ApprovalDate,
                        x.EffectiveDate,
                        x.Description,
                        x.AttachUrl,
                        ShortDescription = Funs.GetSubStr(x.Description, 45),
                        x.LawsRegulationsTypeId,
                        x.LawsRegulationsTypeName,
                    };

            Grid1.DataSource = q;
            Grid1.DataBind();
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}