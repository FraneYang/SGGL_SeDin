namespace Web.Controls
{

    using System;
    using System.Globalization;
    using System.Web.UI.WebControls;

    /// <summary>
    /// 
    /// </summary>
    public partial class GridNavgator : System.Web.UI.UserControl
    {
        /// <summary>
        /// 页面GRIDVIEW
        /// </summary>
        public GridView GridView
        {
            get;
            set;
        }

        /// <summary>
        /// GRIDVIEWID
        /// </summary>
        public string GridViewId
        {
            get
            {
                return ViewState["GridViewId"] as string;
            }

            set
            {
                ViewState["GridViewId"] = value;
            }
        }

        /// <summary>
        /// PAGELOAD
        /// </summary>
        /// <param name="sender">SENDER</param>
        /// <param name="e">E</param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.PreRender += new System.EventHandler(this.GridNavagator_PreRender);
        }

        /// <summary>
        /// PRERENDER事件中主要是设置翻页相关的信息***
        /// </summary>
        /// <param name="sender">SENDER</param>
        /// <param name="e">E</param>
        protected void GridNavagator_PreRender(object sender, System.EventArgs e)
        {
            if (this.GridViewId == null)
            {
                this.GridViewId = this.GridView.ID;
            }

            if (this.GridView == null)
            {
                this.GridView = this.Page.FindControl(this.GridViewId) as GridView;
            }

            // 有可能是从母版页（default.Master）中调用此控件：
            if (this.GridView == null)
            {
                this.GridView = this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(this.GridViewId) as GridView;
            }

            this.Label3.Text = this.GridView.PageCount.ToString(CultureInfo.InvariantCulture);
            this.Label2.Text = (this.GridView.PageIndex + 1).ToString(CultureInfo.InvariantCulture);
            this.Label1.Text = this.GridView.PageSize.ToString(CultureInfo.InvariantCulture);

            this.LinkButton3.Enabled = !(this.GridView.PageIndex + 1 == this.GridView.PageCount);
            this.LinkButton4.Enabled = !(this.GridView.PageIndex + 1 == this.GridView.PageCount);

            this.LinkButton1.Enabled = !(this.GridView.PageIndex == 0);
            this.LinkButton2.Enabled = !(this.GridView.PageIndex == 0);
        }

        /// <summary>
        /// 跳转到某页

        /// </summary>
        /// <param name="sender">SENDER</param>
        /// <param name="e">E</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            int i = 1;
            if (Int32.TryParse(this.TextBox1.Text, out i))
            {
                if (i <= 0)
                {
                    i = 1;
                }

                if (i > Int32.Parse(this.Label3.Text, CultureInfo.InvariantCulture))
                {
                    i = Int32.Parse(this.Label3.Text, CultureInfo.InvariantCulture);
                }
            }

            this.Button1.CommandArgument = i.ToString(CultureInfo.InvariantCulture);
        }

    }
}