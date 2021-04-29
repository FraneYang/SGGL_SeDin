using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractReviewDetail : PageBase
    {
        #region  定义属性
        /// <summary>
        /// 合同ID
        /// </summary>
        public string contractId
        {
            get
            {
                return (string)ViewState["contractId"];
            }
            set
            {
                ViewState["contractId"] = value;
            }
        }
        /// <summary>
        /// 最末的审批节点
        /// </summary>
        public int EndApproveType
        {
            get
            {
                return (int)ViewState["EndApproveType"];
            }
            set
            {
                ViewState["EndApproveType"] = value;
            }
        }
        /// <summary>
        /// 审批人字典
        /// </summary>
        public Dictionary<int, string> Dic_ApproveMan
        {
            get
            {
                return (Dictionary<int, string>)ViewState["Dic_ApproveMan"];
            }
            set
            {
                ViewState["Dic_ApproveMan"] = value;
            }
        }
        #endregion

        Model.PHTGL_Approve pHTGL_Approve;

         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();

                contractId = Request.Params["ContractId"];
                EndApproveType = 13;

                 //获取合同基本信息
                Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(contractId);
                Model.PHTGL_ContractReview _ContractReview = PHTGL_ContractReviewService.GetPHTGL_ContractReviewByContractId(contractId);

                //获取审批人字典
                Dic_ApproveMan = PHTGL_ContractReviewService.Get_DicApproveman(_Contract.ProjectId,contractId);

                 pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(contractId, this.CurrUser.UserId);

                GetEndApproveType();
                Tab1.Hidden = true;
                Tab3.Hidden = true;


                if (_ContractReview.State ==3)
                {
                    Tab3.Hidden = false;
                    BindApproveForm();

                }
                if (pHTGL_Approve != null )
                {
                    txtApproveType.Text = pHTGL_Approve.ApproveType;
                    BindGrid();
                    if (Convert.ToInt32(pHTGL_Approve.ApproveType) > 6)
                    {
                        Tab1.Hidden = false;
                        BindCountersignFrom();
                    }
                }
                else if ( this.CurrUser.UserId==Const.sysglyId)
                {
                    txtApproveType.Text = "管理员";
                     BindGrid();
                     btnSave.Hidden = true;
                }
                else if (_Contract.CreatUser == this.CurrUser.UserId)
                {
                    txtApproveType.Text = "创建者";
                    BindGrid();
                    btnSave.Hidden = true;
                } 
                else
                {
                    btnSave.Hidden = true;
                }
               
             }

        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(contractId);

            Tab2_CBContractType.DataTextField = "Text";
            Tab2_CBContractType.DataValueField = "Value";
            Tab2_CBContractType.DataSource = BLL.DropListService.GetContractType();
            Tab2_CBContractType.DataBind();


            if (_Contract != null)
            {
                 
                this.Tab2_txtContractName.Text = _Contract.ContractName;
                this.Tab2_txtContractNum.Text = _Contract.ContractNum;
                this.Tab2_txtParties.Text = _Contract.Parties;

                this.Tab2_txtContractAmount.Text = _Contract.ContractAmount.HasValue ? _Contract.ContractAmount.ToString() : "";
                if (!string.IsNullOrEmpty(_Contract.DepartId))
                {
                   
                    this.Tab2_txtDepartId.Text = DepartService.getDepartNameById(_Contract.DepartId);
                }
                if (!string.IsNullOrEmpty(_Contract.Agent))
                {
                   
                    this.Tab2_txtAgent.Text = UserService.GetUserNameByUserId(_Contract.Agent);
                }
                if (!string.IsNullOrEmpty(_Contract.ContractType))
                {
                    Tab2_CBContractType.SelectedValueArray = _Contract.ContractType.Split();
                }
            }
            string strSql = @"  select u.UserName as  ApproveMan,
                                       App.ApproveDate,
                                      (CASE App.IsAgree WHEN '1' THEN '不同意'
                                        WHEN '2' THEN '同意' END) AS IsAgree,
                                        App.ApproveIdea,
                                        App.ApproveId,
                                        App.ApproveType
                                       from PHTGL_Approve as App"
                              + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId";
            List<SqlParameter> listStr = new List<SqlParameter>();
                 listStr.Add(new SqlParameter("@ContractId", contractId));
     
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 绑定会签评审单
        /// </summary>
        void BindCountersignFrom()
        {
            Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(contractId);

            CBContractType.DataTextField = "Text";
            CBContractType.DataValueField = "Value";
            CBContractType.DataSource = BLL.DropListService.GetContractType();
            CBContractType.DataBind();


            if (_Contract != null)
            {
                if (!string.IsNullOrEmpty(_Contract.ProjectId))
                {
                    this.txtProjectid.Text = ProjectService.GetProjectCodeByProjectId(_Contract.ProjectId);
                }
                this.txtContractName.Text = _Contract.ContractName;
                this.txtContractNum.Text = _Contract.ContractNum;
                this.txtParties.Text = _Contract.Parties;

                this.txtContractAmount.Text = _Contract.ContractAmount.HasValue ? _Contract.ContractAmount.ToString() : "";
                if (!string.IsNullOrEmpty(_Contract.DepartId))
                {
                    this.txtDepartId.Text = DepartService.getDepartNameById(_Contract.DepartId);
                }
                if (!string.IsNullOrEmpty(_Contract.Agent))
                {
                    this.txtAgent.Text = UserService.GetUserNameByUserId(_Contract.Agent);
                }
                if (!string.IsNullOrEmpty(_Contract.ContractType))
                {
                    CBContractType.SelectedValueArray = _Contract.ContractType.Split();
                }
            }

            string strSql = @"  select u.UserName as  ApproveMan,
                                       App.ApproveDate,
                                      (CASE App.IsAgree WHEN '1' THEN '不同意'
                                        WHEN '2' THEN '同意' END) AS IsAgree,
                                        App.ApproveIdea,
                                        App.ApproveType
                                       from PHTGL_Approve as App"
                            + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", contractId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            foreach (DataRow dr in tb.Rows)
            {
                string ApproveType = dr["ApproveType"].ToString();
                string ApproveIdea = dr["ApproveIdea"].ToString();

                switch (ApproveType)
                {
                    case "1":
                        txtnode1.Text = ApproveIdea;
                        break;
                    case "2":
                        txtnode2.Text = ApproveIdea;
                        break;
                    case "3":
                        txtnode3.Text = ApproveIdea;
                        break;
                    case "4":
                        txtnode4.Text = ApproveIdea;
                        break;
                    case "5":
                        txtnode5.Text = ApproveIdea;
                        break;
                    case "6":
                        txtnode6.Text = ApproveIdea;
                        break;
                }
            }


        }

        /// <summary>
        /// 绑定施工合同签订审批表
        /// </summary>
        void BindApproveForm()
        {
            Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(contractId);

            Tab3_CBContractType.DataTextField = "Text";
            Tab3_CBContractType.DataValueField = "Value";
            Tab3_CBContractType.DataSource = BLL.DropListService.GetContractType();
            Tab3_CBContractType.DataBind();


            if (_Contract != null)
            {
                if (!string.IsNullOrEmpty(_Contract.ProjectId))
                {
                    this.Tab3_txtProjectid.Text = ProjectService.GetProjectCodeByProjectId(_Contract.ProjectId);
                }
                this.Tab3_txtContractName.Text = _Contract.ContractName;
                this.Tab3_txtContractNum.Text = _Contract.ContractNum;
                this.Tab3_txtParties.Text = _Contract.Parties;

                this.Tab3_txtContractAmount.Text = _Contract.ContractAmount.HasValue ? _Contract.ContractAmount.ToString() : "";
                if (!string.IsNullOrEmpty(_Contract.DepartId))
                {
                    this.Tab3_txtDepartId.Text = DepartService.getDepartNameById(_Contract.DepartId);
                }
                if (!string.IsNullOrEmpty(_Contract.Agent))
                {
                    this.Tab3_txtAgent.Text = UserService.GetUserNameByUserId(_Contract.Agent);
                }
                if (!string.IsNullOrEmpty(_Contract.ContractType))
                {
                    Tab3_CBContractType.SelectedValueArray = _Contract.ContractType.Split();
                }
            }

            string strSql = @"  select u.UserName as  ApproveMan,
                                       App.ApproveDate,
                                      (CASE App.IsAgree WHEN '1' THEN '不同意'
                                        WHEN '2' THEN '同意' END) AS IsAgree,
                                        App.ApproveIdea,
                                        App.ApproveType
                                       from PHTGL_Approve as App"
                            + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", contractId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            foreach (DataRow dr in tb.Rows)
            {
                string ApproveType = dr["ApproveType"].ToString();
                string ApproveIdea = dr["ApproveIdea"].ToString();

                switch (ApproveType)
                {
                    case "10":
                        txtnode1.Text = ApproveIdea;
                        break;
                    case "11":
                        txtnode2.Text = ApproveIdea;
                        break;
                    case "12":
                        txtnode3.Text = ApproveIdea;
                        break;
                    case "13":
                        txtnode4.Text = ApproveIdea;
                        break;
                    case "14":
                        txtnode5.Text = ApproveIdea;
                        break;
                    case "15":
                        txtnode6.Text = ApproveIdea;
                        break;
                    case "16":
                        txtnode6.Text = ApproveIdea;
                        break;
                }
            }


        }

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(pHTGL_Approve.ApproveType)<=6)
            {
                CountersignerSave();
            }
            else
            {
                ApprovemanSave();
            }

        }

        /// <summary>
        /// 会签人员保存
        /// </summary>
        void CountersignerSave()
        {
              pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
             if (CBIsAgree.SelectedValueArray.Length > 0)
            {
                pHTGL_Approve.State = 1;
                pHTGL_Approve.IsAgree = Convert.ToInt32(CBIsAgree.SelectedValueArray);
                pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
                BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);
                ChangeState(1);
                if (Convert.ToInt32(CBIsAgree.SelectedValueArray) == 2)  //同意时
                {
                    if (IsCountersignerAllAgree())
                    {
                         Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                        _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                        _Approve.ContractId = pHTGL_Approve.ContractId;
                        _Approve.ApproveMan = Dic_ApproveMan[7];
                        _Approve.ApproveDate = "";
                        _Approve.State = 0;
                        _Approve.IsAgree = 0;
                        _Approve.ApproveIdea = "";
                        _Approve.ApproveType = "7";

                        BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
                        ChangeState(2);
                    }
                }
                else
                {
                    ChangeState(4);
                }

            }
            else
            {
                ShowNotify("请确认是否同意！", MessageBoxIcon.Warning);

            }
        }

        /// <summary>
        /// 审批人员保存
        /// </summary>
        void ApprovemanSave()
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            if (CBIsAgree.SelectedValueArray.Length > 0)
            {
                pHTGL_Approve.State = 1;
                pHTGL_Approve.IsAgree = Convert.ToInt32(CBIsAgree.SelectedValueArray);
                pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
                BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);

                int nextApproveType = Convert.ToInt32(pHTGL_Approve.ApproveType) + 1;

                if (Convert.ToInt32(pHTGL_Approve.ApproveType) < EndApproveType) //判断当前审批节点是否结束
                {
                    if (Convert.ToInt32(CBIsAgree.SelectedValueArray) == 2)  //同意时
                    {
                        Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                        _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                        _Approve.ContractId = pHTGL_Approve.ContractId;
                        _Approve.ApproveMan = Dic_ApproveMan[nextApproveType];
                        _Approve.ApproveDate = "";
                        _Approve.State = 0;
                        _Approve.IsAgree = 0;
                        _Approve.ApproveIdea = "";
                        _Approve.ApproveType = nextApproveType.ToString();

                        BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
                        ChangeState(2);
                    }
                    else
                    {
                        ChangeState(4);
                    }

                }
                else
                {
                    ChangeState(3);
                }
               

            }
            else
            {
                ShowNotify("请确认是否同意！", MessageBoxIcon.Warning);

            }

        }
        /// <summary>
        /// 判断是否所以会签人员都已经同意
        /// </summary>
        /// <returns></returns>
        private bool  IsCountersignerAllAgree()
        {
            bool  IsNext=false;
            string strsql = "select  count(*) from PHTGL_Approve where  ApproveType<=6  and IsAgree='2' and ContractId='"+ pHTGL_Approve .ContractId+ "'";
            DataTable tb = SQLHelper.RunSqlGetTable(strsql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (tb.Rows[0][0].ToString()=="6")
                {
                    IsNext = true;
                }  

            }

            return IsNext;
              
        }
        /// <summary>
        /// 改变审批流状态1 是会签2 是审批 3是成功 4是失败
        /// </summary>
        /// <param name="state"></param>
        private void ChangeState(int state)
        {
            Model.PHTGL_ContractReview _ContractReview = PHTGL_ContractReviewService.GetPHTGL_ContractReviewByContractId(contractId);
            _ContractReview.State = state;
            PHTGL_ContractReviewService.UpdatePHTGL_ContractReview(_ContractReview);
            if (state ==3)
            {
                var _Att = BLL.ContractService.GetContractById(contractId);
                if (_Att != null)
                {
                    _Att.ApproveState = 3;
                    ContractService.UpdateContract(_Att);
                }

            }
            else if(state == 4)
            {
                var _Att = BLL.ContractService.GetContractById(contractId);
                if (_Att != null)
                {
                    _Att.ApproveState = 4;
                    ContractService.UpdateContract(_Att);
                }
            }
        }

       /// <summary>
       /// 获取最终的审批节点
       /// </summary>
       /// <returns></returns>
        private void   GetEndApproveType()
        {
            
          Model .PHTGL_Contract _Contract= ContractService.GetContractById(contractId);
            if (_Contract.ContractAmount>50000000)
            {
                EndApproveType = 16;
                return;
             }
            if (_Contract.ContractAmount > 20000000)
            {
                EndApproveType = 15;
                return;
            }


        }
 
            /// <summary>
            /// 排序
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}