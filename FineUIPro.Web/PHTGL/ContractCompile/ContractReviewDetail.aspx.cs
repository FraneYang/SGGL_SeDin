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
        public string ContractReviewId
        {
            get
            {
                return (string)ViewState["ContractReviewId"];
            }
            set
            {
                ViewState["ContractReviewId"] = value;
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
        public List<ApproveManModel> ApproveManModels
        {
            get
            {
                return (List<ApproveManModel>)Session["ApproveManModels"];
            }
            set
            {
                Session["ApproveManModels"] = value;
            }
        }
        public Model.PHTGL_Approve pHTGL_Approve
        {
            get
            {
                return (Model.PHTGL_Approve)Session["pHTGL_Approve"];
            }
            set
            {
                Session["pHTGL_Approve"] = value;
            }
        }
        #endregion

 
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideRefreshReference();

                ContractReviewId = Request.Params["ContractReviewId"];
                Model.PHTGL_ContractReview _ContractReview = PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(ContractReviewId);
                contractId = _ContractReview.ContractId;
                //获取合同基本信息
                Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(contractId);
                 //获取审批人字典
                Dic_ApproveMan = PHTGL_ContractReviewService.Get_DicApproveman(ContractReviewId);
                ApproveManModels = PHTGL_ContractReviewService.GetApproveManModels(ContractReviewId);

                var ApproveManModels__Countersigner = PHTGL_ContractReviewService.GetApproveManModels__Countersigner(ContractReviewId);
                var allApproveMan = ApproveManModels__Countersigner.Concat(ApproveManModels).ToList();
                pHTGL_Approve = BLL.PHTGL_ApproveService.GetPHTGL_ApproveByUserId(ContractReviewId, this.CurrUser.UserId);

                GetEndApproveType();
                Tab1.Hidden = true;
                Tab3.Hidden = true;
                if (PHTGL_ApproveService.IsApproveMan(ContractReviewId, this.CurrUser.UserId))
                {
                     BindGrid();
                     var ApproveManList = PHTGL_ApproveService.GetListPHTGL_ApproveByUserId(ContractReviewId, this.CurrUser.UserId);
                    int Number = 0;
                    if (ApproveManList.Count >0)
                    {
                        for (int i = 0; i < ApproveManList.Count; i++)
                        {
                            var number = allApproveMan.Find(x => x.Rolename == ApproveManList[i].ApproveType).Number;
                            if (number>Number)
                            {
                                Number = number;
                            }
                        }

                    }
                    // if (Convert.ToInt32(Number) > 6)
                    //{
                    //    Tab1.Hidden = false;
                    //    BindCountersignFrom();
                    //    if (_ContractReview.State == Const.ContractReview_Complete)
                    //    {
                    //        Tab3.Hidden = false;
                    //        BindApproveForm();

                    //    }
                    //}
                }
                else if (this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId == Const.hfnbdId)
                {
                    BindGrid();

                }
                else if (_Contract.CreatUser == this.CurrUser.UserId)
                {
                    BindGrid();
                }

                if (pHTGL_Approve != null)
                {
                    btnAgree.Enabled = true;
                    btnDisgree.Enabled = true;
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
                this.Tab2_txtProjectShortName.Text = _Contract.ProjectShortName;
                this.Tab2_txtContractName.Text = _Contract.ContractName;
                this.Tab2_txtContractNum.Text = _Contract.ContractNum;
                this.Tab2_txtParties.Text = _Contract.Parties;
                if (_Contract.ContractAttribute==1) //补充合同
                {
                    this.NoUseStandardtxtRemark.Hidden = false;

                    this.NoUseStandardtxtRemark.Text = _Contract.ContractAttributeRemark;

                }
                else if(_Contract.IsUseStandardtxt==2) //不使用标准文本
                {
                    this.NoUseStandardtxtRemark.Hidden = false;

                    this.NoUseStandardtxtRemark.Text = _Contract.NoUseStandardtxtRemark;

                }
 
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
            //string strSql = @"  select u.UserName as  ApproveMan,
            //                           App.ApproveDate,
            //                          (CASE App.IsAgree WHEN '1' THEN '不同意'
            //                            WHEN '2' THEN '同意' END) AS IsAgree,
            //                            App.ApproveIdea,
            //                            App.ApproveId,
            //                            App.ApproveType
            //                           from PHTGL_Approve as App"
            //                  + @"   left join Sys_User AS U ON U.UserId = App.ApproveMan WHERE 1=1   and App.IsAgree <>0 and app.ContractId= @ContractId order by convert(datetime ,App.ApproveDate)  ";
            //List<SqlParameter> listStr = new List<SqlParameter>();
            //     listStr.Add(new SqlParameter("@ContractId", ContractReviewId));

            //SqlParameter[] parameter = listStr.ToArray();
            //DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            DataTable tb = BLL.PHTGL_ApproveService.GetAllApproveData(ContractReviewId);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void btnLooK_Click(object sender, EventArgs e)
        {
            var model = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(ContractReviewId);
            var Con = BLL.ContractService.GetContractById(model.ContractId);
            if (Con.IsUseStandardtxt==2 || Con.ContractAttribute == 1)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContractAttachUrl&menuId={1}&type=-1", model.ContractId, BLL.Const.ContractFormation)));
              }
            else
            {
                ContractReview contractReview = new ContractReview();
                contractReview.printContractAgreement(model.ContractId);
            }

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

            string strSql = @"  select  a.ApproveId ,a.ApproveMan,a.ApproveType,a.ApproveDate ,a.ApproveIdea,a.ApproveForm
                                from PHTGL_Approve a "
                                + @"  where not exists (select 1 from PHTGL_Approve b where a.ApproveType=b.ApproveType and a.ContractId=b.ContractId and a.ApproveDate<b.ApproveDate ) and ContractId=@ContractId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", ContractReviewId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var ApproveManModels__Countersigner = PHTGL_ContractReviewService.GetApproveManModels__Countersigner(ContractReviewId);
            var allApproveMan = ApproveManModels__Countersigner.Concat(ApproveManModels).ToList();

            foreach (DataRow dr in tb.Rows)
            {
                string ApproveMan = dr["ApproveMan"].ToString();
                var model = allApproveMan.Find(e => e.Rolename == dr["ApproveType"].ToString());
                string ApproveType = "";
                string ApproveDate = "";
                if (model != null)
                {
                    ApproveType = model.Number.ToString();

                }
                string ApproveIdea = dr["ApproveIdea"].ToString();
                try
                {
                    ApproveDate = string.Format("{0:D}", DateTime.Parse(dr["ApproveDate"].ToString()));


                }
                catch (Exception)
                {
                    ApproveDate = "";
                }
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
                    case "7":
                        txtnode7.Text = ApproveIdea;
                        break;
                    case "8":
                        txtnode8.Text = ApproveIdea;
                        break;
                    case "9":
                        txtnode9.Text = ApproveIdea;
                        break;
                    case "10":
                        txtnode10.Text = ApproveIdea;
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
                if (!string .IsNullOrEmpty(_Contract.Remarks))
                {
                    txtRemark.Text = _Contract.Remarks;
                }
            }

            string strSql = @"  select  a.ApproveId ,a.ApproveMan,a.ApproveType,a.ApproveDate ,a.ApproveIdea,a.ApproveForm
                                from PHTGL_Approve a "
                               + @"  where not exists (select 1 from PHTGL_Approve b where a.ApproveType=b.ApproveType and a.ContractId=b.ContractId and a.ApproveDate<b.ApproveDate ) and ContractId=@ContractId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractId", ContractReviewId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var ApproveManModels__Countersigner = PHTGL_ContractReviewService.GetApproveManModels__Countersigner(ContractReviewId);
            var allApproveMan = ApproveManModels__Countersigner.Concat(ApproveManModels).ToList();
            foreach (DataRow dr in tb.Rows)
            {

                string ApproveMan = dr["ApproveMan"].ToString();
                var model = allApproveMan.Find(e => e.Rolename == dr["ApproveType"].ToString());
                string ApproveType = "";
                string ApproveDate = "";
                if (model != null)
                {
                    ApproveType = model.Number.ToString();

                }
                string ApproveIdea = dr["ApproveIdea"].ToString();
                try
                {
                    ApproveDate = string.Format("{0:D}", DateTime.Parse(dr["ApproveDate"].ToString()));


                }
                catch (Exception)
                {
                    ApproveDate = "";
                }

                switch (ApproveType)
                {
                    
                    case "11":
                        txtnode11.Text = ApproveIdea;
                        break;
                    case "12":
                        txtnode12.Text = ApproveIdea;
                        break;
                    case "13":
                        txtnode13.Text = ApproveIdea;
                        break;
                    case "14":
                        txtnode14.Text = ApproveIdea;
                        break;
                    case "15":
                        txtnode15.Text = ApproveIdea;
                        break;
                    case "16":
                        txtnode16.Text = ApproveIdea;
                        break;
                    case "17":
                        txtnode17.Text = ApproveIdea;
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

 
        protected void btnAgree_Click(object sender, EventArgs e)
        {
            var ApproveManModels__Countersigner = PHTGL_ContractReviewService.GetApproveManModels__Countersigner(ContractReviewId);
            var allApproveMan = ApproveManModels__Countersigner.Concat(ApproveManModels).ToList();
            var number = allApproveMan.Find(x => x.Rolename == pHTGL_Approve.ApproveType).Number;
            if (number <= 6)
            {
                CountersignerSave(true);
            }
            else
            {
                ApprovemanSave(true);
            }
            BindGrid();
            OAWebSevice.Pushoa();
            OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
        }

        protected void btnDisgree_Click(object sender, EventArgs e)
        {
            var ApproveManModels__Countersigner = PHTGL_ContractReviewService.GetApproveManModels__Countersigner(ContractReviewId);
            var allApproveMan = ApproveManModels__Countersigner.Concat(ApproveManModels).ToList();
            var number = allApproveMan.Find(x => x.Rolename == pHTGL_Approve.ApproveType).Number;
            if (number <= 6)
            {
                CountersignerSave(false);
            }
            else
            {
                ApprovemanSave(false);
            }
            BindGrid();
            OAWebSevice.DoneRequest(pHTGL_Approve.ApproveId);
            OAWebSevice.Pushoa_Creater(pHTGL_Approve.ApproveId);

        }

        void CountersignerSave(bool IsAgree)
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            pHTGL_Approve.State = 1;
            pHTGL_Approve.IsAgree = IsAgree ? 2 : 1;
            pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
            BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);
            ChangeState(Const.ContractReviewing);

            if (IsAgree)
            {
                if (IsCountersignerAllAgree())
                {
                    Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                    //_Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                    _Approve.ContractId = pHTGL_Approve.ContractId;
                    _Approve.ApproveMan = ApproveManModels.Find(e => e.Number == 7).userid;
                    _Approve.ApproveDate = "";
                    _Approve.State = 0;
                    _Approve.IsAgree = 0;
                    _Approve.ApproveIdea = "";
                    _Approve.ApproveType = ApproveManModels.Find(e => e.Number == 7).Rolename;
                    _Approve.IsPushOa = 0;
                    _Approve.ApproveForm = PHTGL_ApproveService.ContractReview;

                    var IsExitmodel = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdAndType(_Approve.ContractId, _Approve.ApproveType);
                    if (IsExitmodel == null)
                    {
                        _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                        BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);

                    }
                    else
                    {
                        _Approve.ApproveId = IsExitmodel.ApproveId;
                        BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(_Approve);

                    }

                    ChangeState(Const.ContractReviewing);
                }

            }
            else
            {
                ChangeState(Const.ContractReview_Refuse);
                var model = PHTGL_ApproveService.GetPHTGL_ApproveByContractId(pHTGL_Approve.ContractId);
                if (model != null)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        Model.PHTGL_Approve _Approve = model[i];
                        _Approve.State = 1;
                        _Approve.IsAgree = 1;
                        _Approve.ApproveIdea = "其他会签人员已经拒绝";
                        PHTGL_ApproveService.UpdatePHTGL_Approve(_Approve);

                        OAWebSevice.DoneRequest(_Approve.ApproveId);

                    }

                }

            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());


        }

        void ApprovemanSave(bool IsAgree)
        {
            pHTGL_Approve.ApproveDate = Funs.GetNewDateTimeOrNow("").ToString();
            pHTGL_Approve.State = 1;
            //pHTGL_Approve.IsAgree = Convert.ToInt32(CBIsAgree.SelectedValueArray[0]);
            pHTGL_Approve.IsAgree = IsAgree ? 2 : 1;
            pHTGL_Approve.ApproveIdea = txtApproveIdea.Text;
            BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(pHTGL_Approve);

            int thisApproveTypeNumber = ApproveManModels.Find(e => e.Rolename == pHTGL_Approve.ApproveType).Number;
            int nextApproveType = thisApproveTypeNumber + 1;

            if (IsAgree)
            {
                if (thisApproveTypeNumber < EndApproveType)
                {

                    Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                    _Approve.ContractId = pHTGL_Approve.ContractId;
                    _Approve.ApproveMan = ApproveManModels.Find(e => e.Number == nextApproveType).userid;
                    _Approve.ApproveDate = "";
                    _Approve.State = 0;
                    _Approve.IsAgree = 0;
                    _Approve.ApproveIdea = "";
                    _Approve.ApproveType = ApproveManModels.Find(e => e.Number == nextApproveType).Rolename;
                    _Approve.IsPushOa = 0;
                    _Approve.ApproveForm = PHTGL_ApproveService.ContractReview;

                    var IsExitmodel = PHTGL_ApproveService.GetPHTGL_ApproveByContractIdAndType(_Approve.ContractId, _Approve.ApproveType);
                    if (IsExitmodel == null)
                    {
                        _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                        BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);

                    }
                    else
                    {
                        _Approve.ApproveId = IsExitmodel.ApproveId;
                        BLL.PHTGL_ApproveService.UpdatePHTGL_Approve(_Approve);

                    }
                    ChangeState(Const.ContractReviewing);
                }
                else
                {
                     ChangeState(Const.ContractReview_Complete);
                }
            }
            else
            {
                ChangeState(Const.ContractReview_Refuse);
            }


            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());


        }

 
 
         /// <summary>
        /// 判断是否所以会签人员都已经同意
        /// </summary>
        /// <returns></returns>
        private bool  IsCountersignerAllAgree()
        {
            bool  IsNext=false;
            string strsql = "select  count(*) from PHTGL_Approve where    State='0'  and ApproveMan !='' and ContractId='" + pHTGL_Approve .ContractId+ "'";
            DataTable tb = SQLHelper.RunSqlGetTable(strsql);
            if (tb != null && tb.Rows.Count > 0)
            {
                if (tb.Rows[0][0].ToString()=="0")
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

            var _Att = BLL.ContractService.GetContractById(contractId);
            if (_Att != null)
            {
                _Att.ApproveState = state;
                ContractService.UpdateContract(_Att);
            }
           
        }

       /// <summary>
       /// 获取最终的审批节点
       /// </summary>
       /// <returns></returns>
        private void   GetEndApproveType()
        {
            EndApproveType = ApproveManModels.Find(x => x.Rolename == "分管副总经理").Number;
 
            Model.PHTGL_Contract _Contract= ContractService.GetContractById(contractId);
            if (_Contract.ContractAmount>=50000000)
            {
                EndApproveType = ApproveManModels.Find(x => x.Rolename == "董事长").Number;
                return;
             }
            if (_Contract.ContractAmount >=20000000)
            {
                EndApproveType = ApproveManModels.Find(x => x.Rolename == "总经理").Number;
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