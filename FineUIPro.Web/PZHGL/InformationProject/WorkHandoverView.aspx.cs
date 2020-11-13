using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class WorkHandoverView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string WorkHandoverId
        {
            get
            {
                return (string)ViewState["WorkHandoverId"];
            }
            set
            {
                ViewState["WorkHandoverId"] = value;
            }
        }
        public int ContactImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ContactImg"]);
            }
            set
            {
                ViewState["ContactImg"] = value;
            }
        }


        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        private List<Model.ZHGL_WorkHandoverDetail> details;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkHandoverId = Request.Params["WorkHandoverId"];
                BindData();
                if (!string.IsNullOrEmpty(WorkHandoverId))
                {
                    details = BLL.WorkHandoverDetailService.GetWorkHandoverDetailsByWorkHandoverId(WorkHandoverId);
                    this.Grid2.DataSource = details;
                    this.Grid2.DataBind();
                    HFWorkHandoverId.Text = WorkHandoverId;
                    Model.ZHGL_WorkHandover workHandover = WorkHandoverService.GetWorkHandoverById(WorkHandoverId);
                    Model.Sys_User transferMan = BLL.UserService.GetUserByUserId(workHandover.TransferMan);
                    if (transferMan != null)
                    {
                        this.txtTransferMan.Text = transferMan.UserName;
                    }
                    this.txtTransferManDepart.Text = workHandover.TransferManDepart;
                    Model.Sys_User receiveMan = BLL.UserService.GetUserByUserId(workHandover.ReceiveMan);
                    if (receiveMan != null)
                    {
                        this.txtReceiveMan.Text = receiveMan.UserName;
                    }
                    this.txtReceiveManDepart.Text = workHandover.ReceiveManDepart;
                    Model.Base_WorkPost workPost = BLL.WorkPostService.GetWorkPostById(workHandover.WorkPostId);
                    if (workPost != null)
                    {
                        this.txtWorkPost.Text = workPost.WorkPostName;
                    }
                    if (workHandover.TransferDate != null)
                    {
                        this.txtTransferDate.Text = string.Format("{0:yyyy-MM-dd}", workHandover.TransferDate);
                    }
                }
            }
        }

        private void BindData()
        {
            var table = WorkHandoverApproveService.getListData(WorkHandoverId);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid2.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ZHGL/WorkHandover&menuId={1}&type={2}", itemId, BLL.Const.WorkHandoverMenuId, -1)));
            }
        }
    }
}