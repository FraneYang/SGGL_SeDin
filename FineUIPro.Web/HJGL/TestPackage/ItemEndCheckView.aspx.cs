using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.TestPackage
{
    public partial class ItemEndCheckView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string PTP_ID
        {
            get
            {
                return (string)ViewState["PTP_ID"];
            }
            set
            {
                ViewState["PTP_ID"] = value;
            }
        }
        /// <summary>
        /// 记录主键
        /// </summary>
        public string ItemEndCheckListId
        {
            get
            {
                return (string)ViewState["ItemEndCheckListId"];
            }
            set
            {
                ViewState["ItemEndCheckListId"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PTP_ID = Request.Params["PTP_ID"];
                ItemEndCheckListId = Request.Params["ItemEndCheckListId"];
                if (!string.IsNullOrEmpty(PTP_ID))
                {
                    var getTestPakeage = TestPackageEditService.GetTestPackageByID(PTP_ID);
                    if (getTestPakeage != null)
                    {
                        this.txtTestPackageNo.Text = getTestPakeage.TestPackageNo;
                        this.txtTestPackageName.Text = getTestPakeage.TestPackageName;
                    }
                    BindGrid(); BindGrid1();
                }
            }
        }

        private void BindGrid()
        {
            string strSql = @"  select ItemCheckId, ItemEndCheckListId, PipelineId, Content,Remark, ItemType,(case when Content='/' then '/' else Result end)AS Result from PTP_ItemEndCheck WHERE ItemEndCheckListId =@ItemEndCheckListId Order By PipelineId";
            SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@ItemEndCheckListId",this.ItemEndCheckListId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }
        //办理记录
        public void BindGrid1()
        {
            string strSql = @"select ApproveId, ItemEndCheckListId, ApproveDate, Opinion, ApproveMan, ApproveType ,U.UserName from [dbo].[PTP_TestPackageApprove] P 
                              Left Join Sys_User U on p.ApproveMan=U.UserId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " where ItemEndCheckListId= @ItemEndCheckListId";
            listStr.Add(new SqlParameter("@ItemEndCheckListId", ItemEndCheckListId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
        protected string ConvertCarryPipeline(object PipelineId)
        {
            if (PipelineId != null)
            {
                var getPipeline = BLL.PipelineService.GetPipelineByPipelineId(PipelineId.ToString());
                if (getPipeline != null)
                {
                    return getPipeline.PipelineCode;
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

    
        protected string ConvertApproveType(object Type)
        {
            if (Type != null)
            {
                if (Type.ToString() == BLL.Const.TestPackage_Compile)
                {
                    return "总包专业工程师编制";
                }
                else if (Type.ToString() == Const.TestPackage_Audit1)
                {

                    return "施工分包商整改";
                }
                else if (Type.ToString() == Const.TestPackage_Audit2)
                {

                    return "总包确认";
                }
                else if (Type.ToString() == Const.TestPackage_Audit3)
                {
                    return "监理确认";
                }
                else if (Type.ToString() == Const.TestPackage_ReAudit2)//选否
                {
                    return "施工分包商重新整改";
                }
                else if (Type.ToString() == Const.TestPackage_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}