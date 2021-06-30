using Aspose.Words;
using BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class ContractReview : PageBase
    {
        public string ContractId
        {
            get
            {
                return (string)ViewState["ContractId"];
            }
            set
            {
                ViewState["ContractId"] = value;
            }
        }
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                btnNew.OnClientClick = Window1.GetShowReference("ContractReviewEdit.aspx", "创建审批流") + "return false;";
                GetButtonPower();
                BindGrid();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Rev.ContractReviewId, 
                                    Con.ContractId, 
                                    Con.ProjectId, 
                                    Con.ContractName, 
                                    Con.ContractNum, 
                                    Con.Parties, 
                                    Con.Currency, 
                                    Con.ContractAmount, 
                                    Con.DepartId, 
                                    Con.Agent, 
                                    (CASE Con.ContractType WHEN '1' THEN '施工总承包分包合同'
                                     WHEN '2' THEN '施工专业分包合同'
                                     WHEN '3' THEN '施工劳务分包合同'
                                     WHEN '4' THEN '试车服务合同'
                                     WHEN '5' THEN 'ds' END) AS ContractType,
                                    ( CASE Rev.State
                                    WHEN  @ContractCreating     THEN '编制中'
                                    WHEN  @ContractCreat_Complete     THEN '编制完成'
                                    WHEN  @Contract_countersign     THEN '会签中'
                                    WHEN  @ContractReviewing        THEN '审批中'
                                    WHEN  @ContractReview_Complete  THEN '审批成功'
                                    WHEN  @ContractReview_Refuse    THEN '审批被拒'   END) AS State ,
                                    ApproveType =stuff((select ','+ ApproveType  from PHTGL_Approve app2 where app2.ContractId = Rev.ContractReviewId and app2 .state =0    for xml path('')), 1, 1, '') ,
                                     Con.Remarks,
                                    Con.EPCCode,
                                    Con.ProjectShortName,
                                    Pro.ProjectCode,
                                    Pro.ProjectName,
                                    Dep.DepartName,
                                    U.UserName AS AgentName"
                            + @" from PHTGL_ContractReview AS Rev"
                            + @"  LEFT JOIN PHTGL_Contract AS Con  ON Con.ContractId=Rev.ContractId"
                            //+ @"  left join (select * from PHTGL_ActionPlanFormation as a where  not  exists(select 1 from PHTGL_ActionPlanFormation where a.EPCCode = EPCCode and a.CreateTime< CreateTime )) as Act on Act.EPCCode=Con.EPCCode"
                            + @"  LEFT JOIN Base_Project AS Pro ON Pro.ProjectId = Con.ProjectId"
                            + @"  LEFT JOIN Base_Depart AS Dep ON Dep.DepartId = Con.DepartId"
                            + @"  LEFT JOIN Sys_User AS U ON U.UserId = Con.Agent WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ContractCreating", Const.ContractCreating.ToString ()));
            listStr.Add(new SqlParameter("@ContractCreat_Complete", Const.ContractCreat_Complete.ToString ()));
            listStr.Add(new SqlParameter("@Contract_countersign", Const.Contract_countersign));
            listStr.Add(new SqlParameter("@ContractReviewing", Const.ContractReviewing));
            listStr.Add(new SqlParameter("@ContractReview_Complete", Const.ContractReview_Complete));
            listStr.Add(new SqlParameter("@ContractReview_Refuse", Const.ContractReview_Refuse));

            if (!(this.CurrUser.UserId == Const.sysglyId))
            {
                strSql += " and Con.ProjectId =@ProjectId";

                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtContractName.Text.Trim()))
            {
                strSql += " AND Con.ContractName LIKE @ContractName";
                listStr.Add(new SqlParameter("@ContractName", "%" + this.txtContractName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

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

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
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

        #region 关闭弹出窗体
        /// <summary>
        /// 关闭弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            OAWebSevice.Pushoa();
            BindGrid();
        }
        #endregion

        #region 双击单机事件
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.ApproveData();
        }
        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            string id = Grid1.SelectedRowID;
            var actReview = PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(id);
            if (actReview.State == Const.ContractReview_Refuse)
            {
                btnSubmitAgain.Hidden = false;

            }
            else
            {
                btnSubmitAgain.Hidden = true;

            }

         }
        #endregion

        /// <summary>
        /// 重新提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuSubmit_Click(object sender, EventArgs e)
        {
            this.SubmitAgain();
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }

        private void SubmitAgain()
        {

            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            Model.PHTGL_ContractReview _ContractReview = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(id);

            Model.PHTGL_Contract _Contract = BLL.ContractService.GetContractById(_ContractReview.ContractId);
            if (_Contract.CreatUser!=this.CurrUser.UserId)
            {
                Alert.ShowInTop("您不是编制人无法重新提交！", MessageBoxIcon.Warning);
                return;
            }


            _ContractReview.State = Const.Contract_countersign;
            BLL.PHTGL_ContractReviewService.UpdatePHTGL_ContractReview(_ContractReview);

            PHTGL_ApproveService.DeletePHTGL_ApproveBycontractId(id);  //先删除

            var Countersignermodel = PHTGL_ContractReviewService.GetApproveManModels__Countersigner(id);
            for (int i = 0; i < Countersignermodel.Count; i++)
            {
                Model.PHTGL_Approve _Approve = new Model.PHTGL_Approve();
                _Approve.ApproveId = SQLHelper.GetNewID(typeof(Model.PHTGL_Approve));
                _Approve.ContractId = id;
                _Approve.ApproveMan = Countersignermodel[i].userid;
                _Approve.ApproveDate = "";
                _Approve.State = 0;
                _Approve.IsAgree = 0;
                _Approve.ApproveIdea = "";
                _Approve.ApproveType = Countersignermodel[i].Rolename;
                _Approve.IsPushOa = 0;
                _Approve.ApproveForm = PHTGL_ApproveService.ContractReview;

                BLL.PHTGL_ApproveService.AddPHTGL_Approve(_Approve);
            }

            OAWebSevice.Pushoa();
            ShowNotify("重新提交成功!", MessageBoxIcon.Success);

         }

        /// <summary>
        /// 审批  
        /// </summary>
        private void ApproveData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            var contract = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(id);
            if (contract != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ContractReviewDetail.aspx?ContractReviewId={0}", id, "编辑 - ")));
            }
        }
        protected void btnQueryApprove_Click(object sender, EventArgs e)
        {
            this.ApproveData();
        }

        private void EditData()
        {

            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
        
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ContractReviewEdit.aspx?ContractReviewId={0}", id, "编辑 - ")));
        
        }

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var p = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(rowID);
                        if (p != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, p.ContractId, p.ContractId, BLL.Const.ContractMenuId, BLL.Const.BtnDelete);
                            BLL.PHTGL_ContractReviewService.DeletePHTGL_ContractReviewById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = CommonService.GetAllButtonList(CurrUser.LoginProjectId, CurrUser.UserId, Const.ContractReview);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(Const.BtnAdd))
                {
                  //  btnNew.Hidden = false;
                }
                if (buttonList.Contains(Const.BtnModify))
                {
                    btnMenuEdit.Hidden = false;
                }
                //if (buttonList.Contains(Const.BtnDelete))
                //{
                //    btnMenuDelete.Hidden = false;
                //}
            }
        }
        #endregion

        protected void btnPrinter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            Print(Id);
 
         }

        protected void btnPrinterWord_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            btnPrinterWord.EnablePress = false;
             ContractId = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(Id).ContractId;
            var Con = BLL.ContractService.GetContractById(ContractId);
            if (Con.IsUseStandardtxt == 2)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContractAttachUrl&menuId={1}", ContractId, BLL.Const.ContractFormation)));
            }
            else
            {
                printContractAgreement(ContractId);
                btnPrinterWord.EnablePress = true;
            }
           

        }
        /// <summary>
        /// 合同评审、审批表
        /// </summary>
        /// <param name="contractId"></param>
        public void Print(string ContractReviewId)
         {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            var ReviewModel = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(ContractReviewId);
            var getFireWork = BLL.ContractService.GetContractById(ReviewModel.ContractId);
            initTemplatePath = "File\\Word\\PHTGL\\合同评审、审批表.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            Document doc = new Aspose.Words.Document(newUrl);
 
            Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            string txtProjectid = "";
            string txtContractName = "";
            string txtContractNum = "";
            string txtParties = "";
            string txtContractAmount = "";
            string txtDepartId = "";
            string txtAgent = "";
            string txtRemark = "";
            string TextArea2 = "";
            string type1 = "□";
            string type2 = "□";
            string type3 = "□";
            string type4 = "□";
            string type5 = "□";
            string node1Time = "";
            string node2Time = "";
            string node3Time = "";
            string node4Time = "";
            string node5Time = "";
            string node6Time = "";
            string node7Time = "";
            string node8Time = "";
            string node9Time = "";
            string node10Time = "";
            string node11Time = "";
            string node12Time = "";
            string node13Time = "";
            string node14Time = "";
            string node15Time = "";
            string node16Time = "";
            string node17Time = "";
            try
            {
                #region 评审单
                if (getFireWork != null)
                {
                    if (!string.IsNullOrEmpty(getFireWork.ProjectId))
                    {
                        txtProjectid = getFireWork.EPCCode;
                    }
                    txtContractName = getFireWork.ContractName;
                    txtContractNum = getFireWork.ContractNum;
                    txtParties = getFireWork.Parties;

                    txtContractAmount = getFireWork.ContractAmount.ToString();
                    if (!string.IsNullOrEmpty(getFireWork.DepartId))
                    {
                        txtDepartId = DepartService.getDepartNameById(getFireWork.DepartId);
                    }
                    if (!string.IsNullOrEmpty(getFireWork.Agent))
                    {
                        txtAgent = UserService.GetUserNameByUserId(getFireWork.Agent);
                    }
                    if (!string.IsNullOrEmpty(getFireWork.Remarks))
                    {
                        txtRemark = getFireWork.Remarks;
                    }
                    switch (getFireWork.ContractType)
                    {
                        case "1":
                            type1 = "√";
                            break;
                        case "2":
                            type2 = "√";

                            break;
                        case "3":
                            type3 = "√";

                            break;
                        case "4":
                            type4 = "√";

                            break;
                        case "5":
                            type5 = "√";
                            break;
                    }
                    //if (!string.IsNullOrEmpty(getFireWork.ContractType))
                    //{
                    //    CBContractType.SelectedValueArray = getFireWork.ContractType.Split();
                    //}
                }

                string strSql = @"  select  a.ApproveId ,a.ApproveMan,a.ApproveType,a.ApproveDate ,a.ApproveIdea,a.ApproveForm
                                from PHTGL_Approve a "
                                + @"  where not exists (select 1 from PHTGL_Approve b where a.ApproveType=b.ApproveType and a.ContractId=b.ContractId and a.ApproveDate<b.ApproveDate ) and ContractId=@ContractId";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@ContractId", ContractReviewId));

                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                var ApproveManModels = PHTGL_ContractReviewService.GetApproveManModels(ContractReviewId);
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
                    TextArea2 += ApproveIdea;
                    switch (ApproveType)
                    {
                        case "1":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode1", ApproveMan, ApproveIdea);
                            node1Time = ApproveDate;
                            break;
                        case "2":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode2", ApproveMan, ApproveIdea);
                            node2Time = ApproveDate;
                            break;
                        case "3":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode3", ApproveMan, ApproveIdea);
                            node3Time = ApproveDate;
                            break;
                        case "4":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode4", ApproveMan, ApproveIdea);
                            node4Time = ApproveDate;
                            break;
                        case "5":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode5", ApproveMan, ApproveIdea);
                            node5Time = ApproveDate;
                            break;
                        case "6":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode6", ApproveMan, ApproveIdea);
                            node6Time = ApproveDate;
                            break;
                        case "7":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode7", ApproveMan, ApproveIdea);
                            node7Time = ApproveDate;
                            break;
                        case "8":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode8", ApproveMan, ApproveIdea);
                            node8Time = ApproveDate;
                            break;
                        case "9":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode9", ApproveMan, ApproveIdea);
                            node9Time = ApproveDate;
                            break;
                        case "10":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode10", ApproveMan, ApproveIdea);
                            node10Time = ApproveDate;
                            break;
                        case "11":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode11", ApproveMan, ApproveIdea);
                            node11Time = ApproveDate;
                            break;
                        case "12":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode12", ApproveMan, ApproveIdea);
                            node12Time = ApproveDate;
                            break;
                        case "13":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode13", ApproveMan, ApproveIdea);
                            node13Time = ApproveDate;
                            break;
                        case "14":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode14", ApproveMan, ApproveIdea);
                            node14Time = ApproveDate;
                            break;
                        case "15":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode15", ApproveMan, ApproveIdea);
                            node15Time = ApproveDate;
                            break;
                        case "16":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode16", ApproveMan, ApproveIdea);
                            node16Time = ApproveDate;
                            break;
                        case "17":
                            AsposeWordHelper.InsertImg(doc, rootPath, "txtnode17", ApproveMan, ApproveIdea);
                            node17Time = ApproveDate;
                            break;
                    }
                }
                Dic_File.Add("txtProjectid", txtProjectid);
                Dic_File.Add("txtContractName", txtContractName);
                Dic_File.Add("txtContractNum", txtContractNum);
                Dic_File.Add("txtParties", txtParties);
                Dic_File.Add("txtContractAmount", txtContractAmount);
                Dic_File.Add("txtDepartId", txtDepartId);
                Dic_File.Add("txtAgent", txtAgent);
                Dic_File.Add("txtRemark", txtRemark);
                Dic_File.Add("TextArea2", TextArea2);
                Dic_File.Add("type1", type1);
                Dic_File.Add("type2", type2);
                Dic_File.Add("type3", type3);
                Dic_File.Add("type4", type4);
                Dic_File.Add("type5", type5);
                Dic_File.Add("node1Time", node1Time);
                Dic_File.Add("node2Time", node2Time);
                Dic_File.Add("node3Time", node3Time);
                Dic_File.Add("node4Time", node4Time);
                Dic_File.Add("node5Time", node5Time);
                Dic_File.Add("node6Time", node6Time);
                Dic_File.Add("node7Time", node7Time);
                Dic_File.Add("node8Time", node8Time);
                Dic_File.Add("node9Time", node9Time);
                Dic_File.Add("node10Time", node10Time);
                Dic_File.Add("node11Time", node11Time);
                Dic_File.Add("node12Time", node12Time);
                Dic_File.Add("node13Time", node13Time);
                Dic_File.Add("node14Time", node14Time);
                Dic_File.Add("node15Time", node15Time);
                Dic_File.Add("node16Time", node16Time);
                Dic_File.Add("node17Time", node17Time);
                #endregion
            }
            catch (Exception ex)
            {
                 ErrLogInfo.WriteLog(string.Empty, ex);
                  if (File.Exists(newUrl))
                  {
                    File.Delete(newUrl);
                  }
                return;
             }
         
            if (TextArea2=="")
            {
                TextArea2 = "已按评审意见修改完成，同意签订本合同。";

            }
             foreach (var item in Dic_File)
            {
                string[] key = { item.Key };
                object[] value = { item.Value };
                doc.MailMerge.Execute(key, value);
            }
            doc.Save(newUrl);
            //生成PDF文件
            string pdfUrl = newUrl.Replace(".doc", ".pdf");
            Document doc1 = new Aspose.Words.Document(newUrl);
            //Document srcDoc = new Document(newUrl);
            //doc1.AppendDocument(srcDoc, ImportFormatMode.UseDestinationStyles);
             //验证参数
            if (doc1 == null) { throw new Exception ("Word文件无效"); }
            doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Pdf);//还可以改成其它格式
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(pdfUrl);
            long fileSize = info.Length;
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
            System.Web.HttpContext.Current.Response.TransmitFile(pdfUrl, 0, fileSize);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Close();
            File.Delete(newUrl);
            File.Delete(pdfUrl);
        }

        /// <summary>
        /// 分包合同协议书
        /// </summary>
        /// <param name="AttachUrlId"></param>
        /// <returns></returns>
        public void    printContractAgreement(string ContractId)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\施工合同标准文本.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM-dd-HH-mm}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM-dd-HH-mm}", DateTime.Now) + ".docx");
            File.Copy(uploadfilepath, newUrl);
            Document doc = new Aspose.Words.Document(newUrl);
            var sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);
            var ContractModel= BLL.ContractService.GetContractById(ContractId);
            if (ContractModel.IsUseStandardtxt==2)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContractAttachUrl&menuId={1}", this.ContractId, BLL.Const.ContractFormation)));
                return;

            }
            if (sub==null)
            {
                Alert.ShowInTop("分包合同协议书未编制，无法导出！", MessageBoxIcon.Warning);
                File.Delete(newUrl);
                return;
            }
            Dictionary<string, object> Dic_File = new Dictionary<string, object>();

            #region  定义文本域
            string ContractNum = ContractModel.ContractNum;
            string BuildUnit = ContractModel.BuildUnit;
            string tab2_txtGeneralContractor = sub.GeneralContractor;
            string tab2_txtSubConstruction = sub.SubConstruction;
            string tab2_txtContents = sub.Contents;
            string tab2_txtContractProject = sub.ContractProject;
            string tab2_txtContractProjectOwner = sub.ContractProjectOwner;
            string tab2_txtSubProject = sub.SubProject;
            string tab2_txtSubProjectAddress = sub.SubProjectAddress;
            string tab2_txtFundingSources = sub.FundingSources;
            string tab2_txtSubProjectContractScope = sub.SubProjectContractScope;
            string tab2_txtSubProjectContent = sub.SubProjectContent;
            string tab2_txtPlanStartYear = sub.PlanStartYear.HasValue ? sub.PlanStartYear.ToString() : "";
            string tab2_txtPlanStartMonth = sub.PlanStartMonth.HasValue ? sub.PlanStartMonth.ToString() : "";
            string tab2_txtPlanStartDay = sub.PlanStartDay.HasValue ? sub.PlanStartDay.ToString() : "";
            string tab2_txtPlanEndYear = sub.PlanEndYear.HasValue ? sub.PlanEndYear.ToString() : "";
            string tab2_txtPlanEndMonth = sub.PlanEndMonth.HasValue ? sub.PlanEndMonth.ToString() : "";
            string tab2_txtPlanEndDay = sub.PlanEndDay.HasValue ? sub.PlanEndDay.ToString() : "";
            string tab2_txtLimit = sub.Limit.HasValue ? sub.Limit.ToString() : "";
            string tab2_txtQualityStandards = sub.QualityStandards;
            string tab2_txtHSEManageStandards = sub.HSEManageStandards;
            string tab2_txtSubcontractPriceForm = sub.SubcontractPriceForm;
            string tab2_txtContractPriceCapital = sub.ContractPriceCapital;
            string tab2_txtContractPriceCNY = sub.ContractPriceCNY.HasValue ? sub.ContractPriceCNY.ToString() : "";
            string tab2_txtContractPriceDesc = sub.ContractPriceDesc;
            string tab2_txtInvoice = sub.Invoice;
            string tab2_txtLaw = sub.Law;
            string tab2_txtSignedYear = sub.SignedYear.HasValue ? sub.SignedYear.ToString() : "";
            string tab2_txtSignedMonth = sub.SignedMonth.HasValue ? sub.SignedMonth.ToString() : "";
            string tab2_txtSignedAddress = sub.SignedAddress;
            string tab2_txtAgreementNum = sub.AgreementNum.HasValue ? sub.AgreementNum.ToString() : "";
            string tab2_txtGeneralContractorNum = sub.GeneralContractorNum.HasValue ? sub.GeneralContractorNum.ToString() : "";
            string tab2_txtSubContractorNum = sub.SubContractorNum.HasValue ? sub.SubContractorNum.ToString() : "";
            string tab2_txtSub = sub.SubConstruction;
            string tab2_txtSocialCreditCode1 = sub.SocialCreditCode1;
            string tab2_txtSocialCreditCode2 = sub.SocialCreditCode2;
            string tab2_txtAddress1 = sub.Address1;
            string tab2_txtAddress2 = sub.Address2;
            string tab2_txtZipCode1 = sub.ZipCode1;
            string tab2_txtZipCode2 = sub.ZipCode2;
            string tab2_txtLegalRepresentative1 = sub.LegalRepresentative1;
            string tab2_txtLegalRepresentative2 = sub.LegalRepresentative2;
            string tab2_txtEntrustedAgent1 = sub.EntrustedAgent1;
            string tab2_txtEntrustedAgent2 = sub.EntrustedAgent2;
            string tab2_txtTelephone1 = sub.Telephone1;
            string tab2_txtTelephone2 = sub.Telephone2;
            string tab2_txtFax1 = sub.Fax1;
            string tab2_txtFax2 = sub.Fax2;
            string tab2_txtEmail1 = sub.Email1;
            string tab2_txtEmail2 = sub.Email2;
            string tab2_txtBank1 = sub.Bank1;
            string tab2_txtBank2 = sub.Bank2;
            string tab2_txtAccount1 = sub.Account1;
            string tab2_txtAccount2 = sub.Account2;

            Dic_File.Add("ContractNum", ContractNum);
            Dic_File.Add("BuildUnit", BuildUnit);
            Dic_File.Add("tab2_txtGeneralContractor", tab2_txtGeneralContractor);
            Dic_File.Add("tab2_txtSubConstruction", tab2_txtSubConstruction);
            Dic_File.Add("tab2_txtContents", tab2_txtContents);
            Dic_File.Add("tab2_txtContractProject", tab2_txtContractProject);
            Dic_File.Add("tab2_txtContractProjectOwner", tab2_txtContractProjectOwner);
            Dic_File.Add("tab2_txtSubProject", tab2_txtSubProject);
            Dic_File.Add("tab2_txtSubProjectAddress", tab2_txtSubProjectAddress);
            Dic_File.Add("tab2_txtFundingSources", tab2_txtFundingSources);
            Dic_File.Add("tab2_txtSubProjectContractScope", tab2_txtSubProjectContractScope);
            Dic_File.Add("tab2_txtSubProjectContent", tab2_txtSubProjectContent);
            Dic_File.Add("tab2_txtPlanStartYear", tab2_txtPlanStartYear);
            Dic_File.Add("tab2_txtPlanStartMonth", tab2_txtPlanStartMonth);
            Dic_File.Add("tab2_txtPlanStartDay", tab2_txtPlanStartDay);
            Dic_File.Add("tab2_txtPlanEndYear", tab2_txtPlanEndYear);
            Dic_File.Add("tab2_txtPlanEndMonth", tab2_txtPlanEndMonth);
            Dic_File.Add("tab2_txtPlanEndDay", tab2_txtPlanEndDay);
            Dic_File.Add("tab2_txtLimit", tab2_txtLimit);
            Dic_File.Add("tab2_txtQualityStandards", tab2_txtQualityStandards);
            Dic_File.Add("tab2_txtHSEManageStandards", tab2_txtHSEManageStandards);
            Dic_File.Add("tab2_txtSubcontractPriceForm", tab2_txtSubcontractPriceForm);
            Dic_File.Add("tab2_txtContractPriceCapital", tab2_txtContractPriceCapital);
            Dic_File.Add("tab2_txtContractPriceCNY", tab2_txtContractPriceCNY);
            Dic_File.Add("tab2_txtContractPriceDesc", tab2_txtContractPriceDesc);
            Dic_File.Add("tab2_txtInvoice", tab2_txtInvoice);
            Dic_File.Add("tab2_txtLaw", tab2_txtLaw);
            Dic_File.Add("tab2_txtSignedYear", tab2_txtSignedYear);
            Dic_File.Add("tab2_txtSignedMonth", tab2_txtSignedMonth);
            Dic_File.Add("tab2_txtSignedAddress", tab2_txtSignedAddress);
            Dic_File.Add("tab2_txtAgreementNum", tab2_txtAgreementNum);
            Dic_File.Add("tab2_txtGeneralContractorNum", tab2_txtGeneralContractorNum);
            Dic_File.Add("tab2_txtSubContractorNum", tab2_txtSubContractorNum);
            Dic_File.Add("tab2_txtSub", tab2_txtSub);
            Dic_File.Add("tab2_txtSocialCreditCode1", tab2_txtSocialCreditCode1);
            Dic_File.Add("tab2_txtSocialCreditCode2", tab2_txtSocialCreditCode2);
            Dic_File.Add("tab2_txtAddress1", tab2_txtAddress1);
            Dic_File.Add("tab2_txtAddress2", tab2_txtAddress2);
            Dic_File.Add("tab2_txtZipCode1", tab2_txtZipCode1);
            Dic_File.Add("tab2_txtZipCode2", tab2_txtZipCode2);
            Dic_File.Add("tab2_txtLegalRepresentative1", tab2_txtLegalRepresentative1);
            Dic_File.Add("tab2_txtLegalRepresentative2", tab2_txtLegalRepresentative2);
            Dic_File.Add("tab2_txtEntrustedAgent1", tab2_txtEntrustedAgent1);
            Dic_File.Add("tab2_txtEntrustedAgent2", tab2_txtEntrustedAgent2);
            Dic_File.Add("tab2_txtTelephone1", tab2_txtTelephone1);
            Dic_File.Add("tab2_txtTelephone2", tab2_txtTelephone2);
            Dic_File.Add("tab2_txtFax1", tab2_txtFax1);
            Dic_File.Add("tab2_txtFax2", tab2_txtFax2);
            Dic_File.Add("tab2_txtEmail1", tab2_txtEmail1);
            Dic_File.Add("tab2_txtEmail2", tab2_txtEmail2);
            Dic_File.Add("tab2_txtBank1", tab2_txtBank1);
            Dic_File.Add("tab2_txtBank2", tab2_txtBank2);
            Dic_File.Add("tab2_txtAccount1", tab2_txtAccount1);
            Dic_File.Add("tab2_txtAccount2", tab2_txtAccount2);
            #endregion
            foreach (var item in Dic_File)
            {
                string[] key = { item.Key };
                object[] value = { item.Value };
                doc.MailMerge.Execute(key, value);
            }
            #region 拼接word 
            Document docGen = printGeneralTermsConditions(); //通用合同条款
            doc.AppendDocument(docGen, ImportFormatMode.UseDestinationStyles);
            Document docSpe = printSpecialTermsConditions(ContractId);
            doc.AppendDocument(docSpe, ImportFormatMode.UseDestinationStyles);

            var model = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsByContractId(ContractId);
            var SpecialTermsConditionsId = model.SpecialTermsConditionsId;
            string strSql = @"SELECT Att.AttachUrlId, 
                                    Att.AttachUrlCode, 
                                    Att.AttachUrlName, 
                                    Att.IsBuild,
                                    Att.IsSelected, 
                                    Att.SortIndex"
                          + @" FROM PHTGL_AttachUrl AS Att"
                          + @" WHERE 1=1  "
                          + @" and  Att.SpecialTermsConditionsId=@SpecialTermsConditionsId and Att.IsSelected=1  ORDER BY Att.SortIndex ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@SpecialTermsConditionsId", SpecialTermsConditionsId));
             SqlParameter[] parameter = listStr.ToArray();
             DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            int  schNumber = 0;
            foreach (DataRow dataRow   in tb.Rows)
            {
                schNumber += 1;
                string AttachUrlId = dataRow["AttachUrlId"].ToString();
                string AttachUrlName = dataRow["AttachUrlName"].ToString();
                string SortIndex= dataRow["SortIndex"].ToString();
                switch (SortIndex)
                {
                    case "1":
                        doc.AppendDocument(sch1(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);

                        break;
                    case "2":  
                        doc.AppendDocument(sch2(AttachUrlId,schNumber), ImportFormatMode.UseDestinationStyles);
                         break;
                   
                    case "3":
                        doc.AppendDocument(sch3(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "4":
                        doc.AppendDocument(sch4(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "5":
                        doc.AppendDocument(sch5(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "6":
                        doc.AppendDocument(sch6(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "7":
                        doc.AppendDocument(sch7(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "8":
                        doc.AppendDocument(sch8(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "9":
                        doc.AppendDocument(sch9(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "10":
                        doc.AppendDocument(sch10(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "11":
                        doc.AppendDocument(sch11(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "12":
                        doc.AppendDocument(sch12(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "13":
                        doc.AppendDocument(sch13(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "14":
                        doc.AppendDocument(sch14(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "15":
                        doc.AppendDocument(sch15(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "16":
                        doc.AppendDocument(sch16(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "17":
                        doc.AppendDocument(sch17(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "18":
                        doc.AppendDocument(sch18(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "19":
                        doc.AppendDocument(sch19(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;
                    case "20":
                        doc.AppendDocument(sch20(AttachUrlId, schNumber), ImportFormatMode.UseDestinationStyles);
                        break;

                }
 
            }
             #endregion
            doc.UpdateFields();
            doc.Save(newUrl);

            string pdfUrl = newUrl.Replace(".doc", ".doc");
            Document doc1 = new Aspose.Words.Document(newUrl);
            //Document srcDoc = new Document(newUrl);
            //doc1.AppendDocument(srcDoc, ImportFormatMode.UseDestinationStyles);
            //验证参数
            if (doc1 == null) { throw new Exception ("Word文件无效"); }
            doc1.Save(pdfUrl, Aspose.Words.SaveFormat.Docx);//还可以改成其它格式
            string fileName = Path.GetFileName(filePath).Replace("施工合同标准文本", tab2_txtContractProject+ tab2_txtSubProject);
            FileInfo info = new FileInfo(pdfUrl);
            long fileSize = info.Length;
            try
            {
 
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                System.Web.HttpContext.Current.Response.TransmitFile(pdfUrl, 0, fileSize);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.Close();
                File.Delete(newUrl);
                File.Delete(pdfUrl);
            }
            catch (Exception ex)
            {

                ErrLogInfo.WriteLog(string.Empty, ex);
                File.Delete(newUrl);
                File.Delete(pdfUrl);
            }
           
          
        }
        /// <summary>
        /// 导出通用合同条款
        /// </summary>
        /// <returns></returns>
        public Document printGeneralTermsConditions( )
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\第二部分 通用合同条款.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            File.Copy(uploadfilepath, newUrl);
            Document doc = new Aspose.Words.Document(newUrl);
            doc.Save(newUrl);
            //生成PDF文件
            File.Delete(newUrl);
            return doc;
        }

        /// <summary>
        /// 导出专用合同
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public Document printSpecialTermsConditions(string ContractId)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\第三部分 专用合同条款.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            File.Copy(uploadfilepath, newUrl);
            Document doc = new Aspose.Words.Document(newUrl);
             Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            var model = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsByContractId(ContractId);
            if (model != null)
            {
                string    SpecialTermsConditionsId = model.SpecialTermsConditionsId;
                Type type = model.GetType();
                PropertyInfo[] pArray = type.GetProperties();
                foreach (PropertyInfo p in pArray)
                {
                    string name= p.Name;
                    string value = Convert .ToString ( p.GetValue(model, null));
                    Dic_File.Add(name, value);
                }
                string strSql = @"SELECT Att.AttachUrlId, 
                                    Att.AttachUrlCode, 
                                    Att.AttachUrlName, 
                                    Att.IsBuild,
                                    Att.IsSelected, 
                                    Att.SortIndex"
                        + @" FROM PHTGL_AttachUrl AS Att"
                        + @" WHERE 1=1  "
                        + @" and  Att.SpecialTermsConditionsId=@SpecialTermsConditionsId and Att.IsSelected=1  ORDER BY Att.SortIndex ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@SpecialTermsConditionsId", SpecialTermsConditionsId));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                int schNumber = 0;
                string txtSch = "";
                foreach (DataRow dataRow in tb.Rows)
                {
                    schNumber += 1;
                    string AttachUrlId = dataRow["AttachUrlId"].ToString();
                    string AttachUrlName = dataRow["AttachUrlName"].ToString();
                    string SortIndex = dataRow["SortIndex"].ToString();
                    txtSch = txtSch + "附件" + schNumber + "   " + AttachUrlName + "\r\n";

                }
                Dic_File.Add("txtSch", txtSch);
                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }
              }

             doc.Save(newUrl);
            //生成PDF文件
            File.Delete(newUrl);
            return doc;
        }
      
 

        #region 附件
        public Document sch1(string AttachUrlId,int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件1    工作内容及要求.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {


                 File.Copy(uploadfilepath, newUrl);
                 Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                var model = AttachUrl1Service.GetAttachUrl1ById(AttachUrlId);
                if (model==null)
                {
                     Dic_File.Add("schNumber", schnumber.ToString());
                     Dic_File.Add("AttachUrlContent","");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;

                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("AttachUrlContent", model.AttachUrlContent);

            foreach (var item in Dic_File)
            {
                string[] key = { item.Key };
                object[] value = { item.Value };
                doc.MailMerge.Execute(key, value);
            }

            doc.Save(newUrl);
            //生成PDF文件
            Document doc1 = new Aspose.Words.Document(newUrl);
            File.Delete(newUrl);
            return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);


                Document doc1 = new Aspose.Words.Document(); 
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch2(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件2    合同价格及支付办法.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Document(newUrl);
                #region 旧
                //DocumentBuilder builder = new DocumentBuilder(doc);
                //Section section = doc.Sections[0];
                //Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                //Dic_File.Add("schNumber", schnumber.ToString());
                //var att = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(AttachUrlId);
                //if (att!=null)
                //{    string aaa = "";
                //    for (int i = 0; i < section.Body.ChildNodes.Count; i++)
                //    {
                //        aaa = aaa + "@" + i + "@" + section.Body.ChildNodes[i].Range.Text + "/r";
                //    }
                //    string b = aaa;
                //    ArrayList arraylist = new ArrayList();
                //    arraylist.Add("1");
                //    arraylist.Add("2");
                //    arraylist.Add("3");
                //    arraylist.Add("4");
                //    arraylist.Add("5");
                //    arraylist.Add("6");
                //    var aa = att.PayMethod.Split(',');
                //    foreach (var item in aa)
                //    {
                //        arraylist.Remove(item);

                //    }
                //     for (int i = 0; i < arraylist .Count; i++)
                //    {
                //        switch (arraylist[i])
                //        {
                //            case "1":
                //                 builder.MoveToBookmark("text1");
                //                 Paragraph paragraph = builder.CurrentParagraph;
                //                 paragraph.Remove();

                //              //  section.Body.ChildNodes[3].Remove();

                //                 break;
                //            case "2":
                //                builder.MoveToBookmark("text2");
                //                Paragraph paragraph2 = builder.CurrentParagraph;
                //                paragraph2.Remove();
                //            //    section.Body.ChildNodes[4].Remove();

                //                break;
                //            case "3":
                //                builder.MoveToBookmark("text3");
                //                Paragraph paragraph3 = builder.CurrentParagraph;
                //                paragraph3.NextSibling.Remove();
                //                paragraph3.NextSibling.Remove();
                //                paragraph3.NextSibling.Remove();
                //                paragraph3.NextSibling.Remove();
                //                paragraph3.NextSibling.Remove();
                //                paragraph3.Remove();

                //                //section.Body.ChildNodes[5].Remove();
                //                //section.Body.ChildNodes[6].Remove();
                //                //section.Body.ChildNodes[7].Remove();
                //                //section.Body.ChildNodes[8].Remove();
                //                //section.Body.ChildNodes[9].Remove();
                //                //section.Body.ChildNodes[10].Remove();

                //                break;
                //            case "4":
                //                builder.MoveToBookmark("text4");
                //                Paragraph paragraph4 = builder.CurrentParagraph;
                //                paragraph4.NextSibling.Remove();
                //                paragraph4.NextSibling.Remove();
                //                paragraph4.NextSibling.Remove();
                //                paragraph4.NextSibling.Remove();
                //                paragraph4.NextSibling.Remove();
                //                paragraph4.Remove();
                //                //section.Body.ChildNodes[11].Remove();
                //                //section.Body.ChildNodes[12].Remove();
                //                //section.Body.ChildNodes[13].Remove();
                //                //section.Body.ChildNodes[14].Remove();
                //                //section.Body.ChildNodes[15].Remove();
                //                //section.Body.ChildNodes[16].Remove();

                //                break;
                //            case "5":
                //                builder.MoveToBookmark("text5");
                //                Paragraph paragraph5 = builder.CurrentParagraph;
                //                paragraph5.NextSibling.Remove();
                //                paragraph5.NextSibling.Remove();
                //                paragraph5.NextSibling.Remove();
                //                paragraph5.NextSibling.Remove();
                //                paragraph5.NextSibling.Remove();
                //                paragraph5.NextSibling.Remove();
                //                paragraph5.Remove();

                //                //section.Body.ChildNodes[17].Remove();
                //                //section.Body.ChildNodes[18].Remove();
                //                //section.Body.ChildNodes[19].Remove();
                //                //section.Body.ChildNodes[20].Remove();
                //                //section.Body.ChildNodes[21].Remove();
                //                //section.Body.ChildNodes[22].Remove();
                //                //section.Body.ChildNodes[23].Remove();

                //                break;
                //            case "6":
                //                builder.MoveToBookmark("text6");
                //                Paragraph paragraph6 = builder.CurrentParagraph;
                //                paragraph6.NextSibling.Remove();
                //                paragraph6.NextSibling.Remove();
                //                paragraph6.NextSibling.Remove();
                //                paragraph6.NextSibling.Remove();
                //                paragraph6.Remove();

                //                //section.Body.ChildNodes[23].Remove();
                //                //section.Body.ChildNodes[24].Remove();
                //                //section.Body.ChildNodes[25].Remove();
                //                //section.Body.ChildNodes[26].Remove();
                //                //section.Body.ChildNodes[27].Remove();

                //                break;
                //        }
                //    }

                //    string strSql = @" SELECT  
                //                 OrderNumber = row_number() over(order by AttachUrlDetaild)
                //               , AttachUrlDetaild 
                //              , AttachUrlItemId 
                //              , DetailType 
                //              , Specifications 
                //              , MachineTeamPrice 
                //              , Remark  "
                //  + @"  from  PHTGL_AttachUrl2Detail "
                //  + @"   where 1=1 and  AttachUrlItemId=@AttachUrlItemId  and DetailType=@DetailType order by OrderNumber";
                //    List<SqlParameter> listStr = new List<SqlParameter>();
                //    listStr.Add(new SqlParameter("@AttachUrlItemId", att.AttachUrlItemId));
                //    listStr.Add(new SqlParameter("@DetailType", "1"));

                //    SqlParameter[] parameter = listStr.ToArray();
                //    DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                //    if (tb.Rows.Count ==0)
                //    {
                //        DataRow dataRow = tb.NewRow();
                //        tb.Rows.Add(dataRow);
                //    }
                //     tb.TableName = "Table";
                //    doc.MailMerge.ExecuteWithRegions(tb);
                //    string aaa2 = "";
                //    for (int i = 0; i < section.Body.ChildNodes.Count; i++)
                //    {
                //        aaa2 = aaa2 + "@" + i + "@" + section.Body.ChildNodes[i].Range.Text + "/r";
                //    }
                //    string b2 = aaa2;

                //    string strSql2 = @" SELECT  
                //                 OrderNumber = row_number() over(order by AttachUrlDetaild)
                //               , AttachUrlDetaild 
                //              , AttachUrlItemId 
                //              , DetailType 
                //              , Specifications 
                //              , MachineTeamPrice 
                //              , Remark  "
                //   + @" from  PHTGL_AttachUrl2Detail "
                //   + @"   where 1=1 and  AttachUrlItemId=@AttachUrlItemId  and DetailType=@DetailType order by OrderNumber";

                //    List<SqlParameter> listStr2 = new List<SqlParameter>();
                //    listStr2.Add(new SqlParameter("@AttachUrlItemId", att.AttachUrlItemId));
                //    listStr2.Add(new SqlParameter("@DetailType", "2"));
                //    SqlParameter[] parameter2 = listStr2.ToArray();
                //    DataTable tb2 = SQLHelper.GetDataTableRunText(strSql2, parameter2);
                //    if (tb2.Rows.Count == 0)
                //    {
                //        DataRow dataRow = tb2.NewRow();
                //        tb2.Rows.Add(dataRow);
                //    }
                //    tb2.TableName = "Table2";
                //    doc.MailMerge.ExecuteWithRegions(tb2);

                //    Dic_File.Add("txtContractPrice", att.ContractPrice);
                //    Dic_File.Add("txtComprehensiveUnitPrice", att.ComprehensiveUnitPrice);
                //    Dic_File.Add("txtComprehensiveRate1", att.ComprehensiveRate1);
                //    Dic_File.Add("txtComprehensiveRate2", att.ComprehensiveRate2);
                //    Dic_File.Add("txtComprehensiveRate3", att.ComprehensiveRate3);
                //    Dic_File.Add("txtComprehensiveRate4", att.ComprehensiveRate4);
                //    Dic_File.Add("txtComprehensiveRate5", att.ComprehensiveRate5);
                //    Dic_File.Add("txtTotalPriceDown1", att.TotalPriceDown1);
                //    Dic_File.Add("txtTotalPriceDown2", att.TotalPriceDown2);
                //    Dic_File.Add("txtTotalPriceDown3", att.TotalPriceDown3);
                //    Dic_File.Add("txtTotalPriceDown4", att.TotalPriceDown4);
                //    Dic_File.Add("txtTotalPriceDown5", att.TotalPriceDown5);
                //    Dic_File.Add("txtTechnicalWork", att.TechnicalWork.HasValue ? att.TechnicalWork.ToString() : "");
                //    Dic_File.Add("txtPhysicalLaborer", att.PhysicalLaborer.HasValue ? att.PhysicalLaborer.ToString() : "");
                //    Dic_File.Add("txtTestCar1", att.TestCar1.HasValue ? att.TestCar1.ToString() : "");
                //    Dic_File.Add("txtTestCar2", att.TestCar2.HasValue ? att.TestCar2.ToString() : "");
                //    Dic_File.Add("txtPayWay", "");
                //     var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                //     AsposeWordHelper.HtmlIntoWord(att.PayWay, Path);
                //     Document doc2 = new Document(Path);

                //    doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                //    doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                //      File.Delete(Path);

                //}

                //foreach (var item in Dic_File)
                //{
                //    string[] key = { item.Key };
                //    object[] value = { item.Value };
                //    doc.MailMerge.Execute(key, value);
                //}
                //doc.Save(newUrl);
                #endregion

                #region  复制表格版
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                Dic_File.Add("schNumber", schnumber.ToString());
                var att = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(AttachUrlId);
                if (att != null)
                {
                    var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                    AsposeWordHelper.HtmlIntoWord(att.AttachUrlContent, Path);
                    Document doc2 = new Document(Path);

                    doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                    doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                    File.Delete(Path);
                }
                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }
                doc.Save(newUrl);
                #endregion
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
            File.Delete(newUrl);
            return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                Document doc1 = new Aspose.Words.Document( );
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }

        }

        public Document sch3(string AttachUrlId, int schnumber)
        {
         
                string rootPath = Server.MapPath("~/");
                string initTemplatePath = string.Empty;
                string uploadfilepath = string.Empty;
                string newUrl = string.Empty;
                string filePath = string.Empty;
                initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件3    工程价格清单.docx";
                uploadfilepath = rootPath + initTemplatePath;
                newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
                filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
                try
                {
                    File.Copy(uploadfilepath, newUrl);
                    var model = AttachUrl3Service.GetAttachUrl3ByAttachUrlId(AttachUrlId);
                    Document doc = new Aspose.Words.Document(newUrl);
                    Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                if (model == null)
                {

                        Dic_File.Add("schNumber", schnumber.ToString());
                        Dic_File.Add("AttachUrlContent", "");

                        foreach (var item in Dic_File)
                        {
                            string[] key = { item.Key };
                            object[] value = { item.Value };
                            doc.MailMerge.Execute(key, value);
                        }
                        if (File.Exists(newUrl))
                        {
                            File.Delete(newUrl);
                        }
                        return doc;

                }
                    Dic_File.Add("schNumber", schnumber.ToString());
                   // Dic_File.Add("AttachUrlContent", model.AttachUrlContent);
                    Dic_File.Add("AttachUrlContent","");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                AsposeWordHelper.HtmlIntoWord(model.AttachUrlContent, Path);
                Document doc2 = new Document(Path);
                doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;
                doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                File.Delete(Path);

                doc.Save(newUrl);
                    //生成PDF文件
                    Document doc1 = new Aspose.Words.Document(newUrl);
                    File.Delete(newUrl);
                    return doc1;
                }
                catch (Exception ex)
                {
                ErrLogInfo.WriteLog(string.Empty, ex);

                if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                      Document doc = new Aspose.Words.Document( );
                     return doc;
 
                }
        }

        public Document sch4(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件4    工程设备及材料分交.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
             File.Copy(uploadfilepath, newUrl);
          
             Document doc = new Aspose.Words.Document(newUrl);
            // string strSql = @"  select 
            //                          url4.AttachUrlItemId
            //                          ,url4.AttachUrlId
            //                          ,url4.OrderNumber
            //                          ,url4.Describe
            //                          ,(Case  url4.Duty_Gen
            //                            when '0' Then  ''
            //                            when '1' Then  '√' end) as Duty_Gen
            //                          ,(Case  url4.Duty_Sub
            //                            when '0' Then  ''
            //                            when '1' Then  '√' end ) Duty_Sub
            //                          ,url4.Remarks"
            //               + @" from PHTGL_AttachUrl4 as url4"
            //               + @"  where 1=1 and url4.AttachUrlId =@AttachUrlId	order by url4.OrderNumber";
            // List<SqlParameter> listStr = new List<SqlParameter>();
            // listStr.Add(new SqlParameter("@AttachUrlId", AttachUrlId));
            // SqlParameter[] parameter = listStr.ToArray();
            // DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            //if (tb.Rows.Count == 0)
            //{
            //    DataRow dataRow = tb.NewRow();
            //    tb.Rows.Add(dataRow);
            //}
            //  tb.TableName = "Table";
            // doc.MailMerge.ExecuteWithRegions(tb);

            Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            Dic_File.Add("schNumber", schnumber.ToString());
 
            foreach (var item in Dic_File)
            {
                string[] key = { item.Key };
                object[] value = { item.Value };
                doc.MailMerge.Execute(key, value);
            }
            var model = AttachUrl4Service.GetAttachurl4ById(AttachUrlId);
            if (model!=null)
            {
                var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                AsposeWordHelper.HtmlIntoWord(model.AttachUrlContent, Path);
                Document doc2 = new Document(Path);

                doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                File.Delete(Path);
            }
               
                doc.Save(newUrl);
            //生成PDF文件
            Document doc1 = new Aspose.Words.Document(newUrl);
            File.Delete(newUrl);
            return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                Document doc1 = new Aspose.Words.Document(); 
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                 }
                return doc1;
            }
        }

        public Document sch5(string AttachUrlId , int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件5    暂估价材料.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {

             File.Copy(uploadfilepath, newUrl);
             Document doc = new Aspose.Words.Document(newUrl);

            // string strSql = @" select  mat.AttachUrlItemId,
            //                          mat.AttachUrlId,
            //                          mat.OrderNumber,
            //                          mat.Name,
            //                          mat.Spec,
            //                          mat.Material,
            //                          mat.Company,
            //                          mat.UnitPrice,
            //                          mat.Remarks "
            //                  + @" from  PHTGL_AttachUrl5_MaterialsPrice as mat"
            //                  + @"   where 1=1 and  mat.AttachUrlId=@AttachUrlId  order by mat.OrderNumber";
            //List<SqlParameter> listStr = new List<SqlParameter>();
            //listStr.Add(new SqlParameter("@AttachUrlId", AttachUrlId));
            //SqlParameter[] parameter = listStr.ToArray();
            //DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            //if (tb.Rows.Count == 0)
            //{
            //    DataRow dataRow = tb.NewRow();
            //    tb.Rows.Add(dataRow);
            //}
            //tb.TableName = "Table";
            //doc.MailMerge.ExecuteWithRegions(tb);


            //string strSql2 = @" SELECT  dev.AttachUrlItemId
            //                          ,dev.AttachUrlId
            //                          ,dev.OrderNumber
            //                          ,dev.Name
            //                          ,dev.Company
            //                          ,dev.amount
            //                          ,dev.UnitPrice
            //                          ,dev.Totalprice
            //                          ,dev.Remarks "
            //             + @"   FROM  PHTGL_AttachUrl5_DevicePrice as dev"
            //             + @"   where 1=1 and dev.AttachUrlId=@AttachUrlId order by dev.OrderNumber ";

            //List<SqlParameter> listStr2 = new List<SqlParameter>();
            //listStr2.Add(new SqlParameter("@AttachUrlId", AttachUrlId));
            //SqlParameter[] parameter2 = listStr2.ToArray();
            //DataTable tb2 = SQLHelper.GetDataTableRunText(strSql2, parameter2);
            //if (tb2.Rows.Count == 0)
            //{
            //    DataRow dataRow = tb2.NewRow();
            //    tb2.Rows.Add(dataRow);
            //}
            //tb2.TableName = "Table2";
            //doc.MailMerge.ExecuteWithRegions(tb2);

            Dictionary<string, object> Dic_File = new Dictionary<string, object>();
            Dic_File.Add("schNumber", schnumber.ToString());

            foreach (var item in Dic_File)
            {
                string[] key = { item.Key };
                object[] value = { item.Value };
                doc.MailMerge.Execute(key, value);
            }
            var model = AttachUrl5Service.GetAttachUrl5ByAttachUrlId(AttachUrlId);
            if (model != null)
            {
                var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                AsposeWordHelper.HtmlIntoWord(model.AttachUrlContent, Path);
                Document doc2 = new Document(Path);

                doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                File.Delete(Path);
            }
                doc.Save(newUrl);
            //生成PDF文件
            Document doc1 = new Aspose.Words.Document(newUrl);
            File.Delete(newUrl);
            return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                Document doc1 = new Aspose.Words.Document( );
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch6(string AttachUrlId, int schnumber)
        {

            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件6    关键材料框架协议.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                var model = AttachUrl6Service.GetAttachUrl6ById(AttachUrlId);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("AttachUrlContent", "");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;

                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("AttachUrlContent", model.AttachUrlContent);

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                Document doc = new Aspose.Words.Document();
                return doc;

            }
        }

        public Document sch7(string AttachUrlId, int schnumber)
        {

            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件7    合同工期关键里程碑时间节点.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                var model = AttachUrl7Service.GetAttachUrl7ById(AttachUrlId);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("AttachUrlContent", "");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;

                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("AttachUrlContent", model.AttachUrlContent);

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                Document doc = new Aspose.Words.Document();
                return doc;

            }
        }

        public Document sch8(string AttachUrlId, int schnumber)
        {

            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件8    总承包商关键人员一览表.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                var model = AttachUrl8Service.GetAttachurl8ById(AttachUrlId);
                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("txtProjectManager", "");
                    Dic_File.Add("txtProjectManager_deputy", "");
                    Dic_File.Add("txtSafetyDirector", "");
                    Dic_File.Add("txtControlManager", "");
                    Dic_File.Add("txtDesignManager", "");
                    Dic_File.Add("txtPurchasingManager", "");
                    Dic_File.Add("txtConstructionManager", "");
                    Dic_File.Add("txtConstructionManager_deputy", "");
                    Dic_File.Add("txtQualityManager", "");
                    Dic_File.Add("txtHSEManager", "");
                    Dic_File.Add("txtDrivingManager", "");
                    Dic_File.Add("txtFinancialManager", "");
                    Dic_File.Add("txtOfficeManager", "");
 
                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;

                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("txtProjectManager", model.ProjectManager);
                Dic_File.Add("txtProjectManager_deputy", model.ProjectManager_deputy);
                Dic_File.Add("txtSafetyDirector", model.SafetyDirector);
                Dic_File.Add("txtControlManager", model.ControlManager);
                Dic_File.Add("txtDesignManager", model.DesignManager);
                Dic_File.Add("txtPurchasingManager", model.PurchasingManager);
                Dic_File.Add("txtConstructionManager", model.ConstructionManager);
                Dic_File.Add("txtConstructionManager_deputy", model.ConstructionManager_deputy);
                Dic_File.Add("txtQualityManager", model.QualityManager);
                Dic_File.Add("txtHSEManager", model.HSEManager);
                Dic_File.Add("txtDrivingManager", model.DrivingManager);
                Dic_File.Add("txtFinancialManager", model.FinancialManager);
                Dic_File.Add("txtOfficeManager", model.OfficeManager);
                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }
                if (model != null)
                {
                    var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                    AsposeWordHelper.HtmlIntoWord(model.AttachUrlContent, Path);
                    Document doc2 = new Document(Path);

                    doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                    doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                    File.Delete(Path);
                }
                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                Document doc = new Aspose.Words.Document();
                return doc;

            }
        }

        public Document sch9(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件9    施工分包商组织机构人员配置表及关键人员名单.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                string strSql = @" SELECT  OrderNumber = row_number() over(order by AttachUrlItemId),
                                           AttachUrlItemId 
                                          , AttachUrlId 
                                          , WorkPostName 
                                          , Number 
                                          , Arrivaltime 
                                          , Remarks "
                               + @"  from PHTGL_AttachUrl9_SubStaffing "
                               + @"  where 1=1 and   AttachUrlId =@AttachUrlId	order by OrderNumber";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@AttachUrlId", AttachUrlId));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                if (tb.Rows.Count == 0)
                {
                    DataRow dataRow = tb.NewRow();
                    tb.Rows.Add(dataRow);
                }
                tb.TableName = "Table";
                doc.MailMerge.ExecuteWithRegions(tb);
                
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                var model = AttachUrl9_SubPersonService.GetSubPersonByAttachId(AttachUrlId);
                if (model!=null)
                {
                    Dic_File.Add("txtProjectManager", Convert.ToString(model.ProjectManager));
                    Dic_File.Add("txtProjectEngineer", Convert.ToString(model.ProjectEngineer));
                    Dic_File.Add("txtConstructionManager", Convert.ToString(model.ConstructionManager));
                    Dic_File.Add("txtQualityManager", Convert.ToString(model.QualityManager));
                    Dic_File.Add("txtHSEManager", Convert.ToString(model.HSEManager));
                    Dic_File.Add("txtPersonnel_Technician", Convert.ToString(model.Personnel_Technician));
                    Dic_File.Add("txtPersonnel_Civil_engineering", Convert.ToString(model.Personnel_Civil_engineering));
                    Dic_File.Add("txtPersonnel_Installation", Convert.ToString(model.Personnel_Installation));
                    Dic_File.Add("txtPersonnel_Electrical", Convert.ToString(model.Personnel_Electrical));
                    Dic_File.Add("txtPersonnel_meter", Convert.ToString(model.Personnel_meter));
                }
                else
                {
                    Dic_File.Add("txtProjectManager", "" );
                    Dic_File.Add("txtProjectEngineer","" );
                    Dic_File.Add("txtConstructionManager", "");
                    Dic_File.Add("txtQualityManager", "");
                    Dic_File.Add("txtHSEManager","");
                    Dic_File.Add("txtPersonnel_Technician", "");
                    Dic_File.Add("txtPersonnel_Civil_engineering","");
                    Dic_File.Add("txtPersonnel_Installation","");
                    Dic_File.Add("txtPersonnel_Electrical", "");
                    Dic_File.Add("txtPersonnel_meter", "");
                }
                Dic_File.Add("schNumber", schnumber.ToString());
                
                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex )
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                Document doc1 = new Aspose.Words.Document(); 
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch10(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件10   施工分包商人员机械投入计划一览表.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {

                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);

                //string strSql = @" SELECT    AttachUrlItemId 
                //                              , AttachUrlId 
                //                              , Subject 
                //                              , WorkType 
                //                              , PersonNumber 
                //                              , LifeTime 
                //                              , Remarks  "
                //                  + @" from  PHTGL_AttachUrl10_HumanInput "
                //                  + @"   where 1=1 and  AttachUrlId=@AttachUrlId  order by Subject";
                //List<SqlParameter> listStr = new List<SqlParameter>();
                //listStr.Add(new SqlParameter("@AttachUrlId", AttachUrlId));
                //SqlParameter[] parameter = listStr.ToArray();
                //DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                //if (tb.Rows.Count == 0)
                //{
                //    DataRow dataRow = tb.NewRow();
                //    tb.Rows.Add(dataRow);
                //}
                //tb.TableName = "Table";
                //doc.MailMerge.ExecuteWithRegions(tb);


                //string strSql2 = @" SELECT   
                //                        OrderNumber = row_number() over(order by AttachUrlItemId)
		              //                , AttachUrlItemId 
                //                      , AttachUrlId 
                //                      , MachineName 
                //                      , MachineSpec 
                //                      , number 
                //                      , LeasedOrOwned 
                //                      , Remarks  "
                //             + @"   FROM  PHTGL_AttachUrl10_MachineInput  "
                //             + @"   where 1=1 and  AttachUrlId=@AttachUrlId order by OrderNumber ";

                //List<SqlParameter> listStr2 = new List<SqlParameter>();
                //listStr2.Add(new SqlParameter("@AttachUrlId", AttachUrlId));
                //SqlParameter[] parameter2 = listStr2.ToArray();
                //DataTable tb2 = SQLHelper.GetDataTableRunText(strSql2, parameter2);
                //if (tb2.Rows.Count == 0)
                //{
                //    DataRow dataRow = tb2.NewRow();
                //    tb2.Rows.Add(dataRow);
                //}
                //tb2.TableName = "Table2";
                //doc.MailMerge.ExecuteWithRegions(tb2);

                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                Dic_File.Add("schNumber", schnumber.ToString());

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }
                var model = AttachUrl10Service.GetAttachUrl10ByAttachUrlId (AttachUrlId);
                if (model != null)
                {
                    var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                    AsposeWordHelper.HtmlIntoWord(model.AttachUrlContent, Path);
                    Document doc2 = new Document(Path);

                    doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                    doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                    File.Delete(Path);
                }
                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch11(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件11   履约担保格式.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                 File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                var model = AttachUrl11Service.GetPHTGL_AttachUrl11ById(AttachUrlId);
                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("AttachUrlContent", "");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;

                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("AttachUrlContent", model.AttachUrlContent);

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);


                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }
 
        public Document sch12(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件12   预付款担保格式.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                var model = AttachUrl12Service.GetPHTGL_AttachUrl12ById(AttachUrlId);
                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("AttachUrlContent", "");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;

                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("AttachUrlContent", model.AttachUrlContent);

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch13(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件13   工程质量保修书.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                 File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                var SpecialTermsConditionsId = BLL.AttachUrlService.GetAttachUrlById(AttachUrlId).SpecialTermsConditionsId;
                var ContractId = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsById(SpecialTermsConditionsId).ContractId;
                var sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);
                if (sub!=null)
                {
                    Dic_File.Add("GeneralContractor", sub.GeneralContractor);
                    Dic_File.Add("SubConstruction", sub.SubConstruction);


                }
                var model = BLL.AttachUrl13Service.GetPHTGL_AttachUrl13ById(AttachUrlId);
                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("txtGeneralContractorName", "");
                    Dic_File.Add("txtSubcontractorsName", "");
                    Dic_File.Add("txtProjectName", "");
                    Dic_File.Add("txtWarrantyContent", "");
                    Dic_File.Add("txtOtherWarrantyPeriod", "");
                    Dic_File.Add("txtWarrantyPeriodDate", "");
                    Dic_File.Add("txtDefectLiabilityDate", "");
                    Dic_File.Add("txtDefectLiabilityPeriod", "");
                    Dic_File.Add("txtOtherqualityWarranty", "");
                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;
                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("txtGeneralContractorName", Convert.ToString(model.GeneralContractorName));
                Dic_File.Add("txtSubcontractorsName", Convert.ToString(model.SubcontractorsName));
                Dic_File.Add("txtProjectName", Convert.ToString(model.ProjectName));
                Dic_File.Add("txtWarrantyContent", Convert.ToString(model.WarrantyContent));
                Dic_File.Add("txtOtherWarrantyPeriod", Convert.ToString(model.OtherWarrantyPeriod));
                Dic_File.Add("txtWarrantyPeriodDate", Convert.ToString(model.WarrantyPeriodDate));
                Dic_File.Add("txtDefectLiabilityDate", Convert.ToString(model.DefectLiabilityDate));
                Dic_File.Add("txtDefectLiabilityPeriod", Convert.ToString(model.DefectLiabilityPeriod));
                Dic_File.Add("txtOtherqualityWarranty", Convert.ToString(model.OtherqualityWarranty));

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch14(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件14   施工安全管理协议书.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                 File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                var SpecialTermsConditionsId = BLL.AttachUrlService.GetAttachUrlById(AttachUrlId).SpecialTermsConditionsId;
                var ContractId = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsById(SpecialTermsConditionsId).ContractId;
                var sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);
                if (sub != null)
                {
                    Dic_File.Add("GeneralContractor", sub.GeneralContractor);
                    Dic_File.Add("SubConstruction", sub.SubConstruction);


                }
                var model = AttachUrl14Service.GetPHTGL_AttachUrl14ById(AttachUrlId);
                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("txtProjectName", "");
                    Dic_File.Add("txtPersonAmount", "");
                    Dic_File.Add("txtSafetyManagerNumber", "");
                    Dic_File.Add("txtSystemManagerNumber", "");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;
                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("txtProjectName",Convert.ToString (model.ProjectName));
                Dic_File.Add("txtPersonAmount", Convert.ToString(model.PersonAmount));
                Dic_File.Add("txtSafetyManagerNumber", Convert.ToString(model.SafetyManagerNumber));
                Dic_File.Add("txtSystemManagerNumber", Convert.ToString(model.SystemManagerNumber));

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                 ErrLogInfo.WriteLog(string.Empty, ex);
                 Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch15(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件15   施工现场总图管理规定.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {

                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                #region 附表1
                string strSql = @" SELECT   Number = row_number() over(order by AttachUrlItemId)
		                                        ,AttachUrlItemId 
                                              , AttachUrlId 
                                              , AttachUrlContent 
                                              , ProjectName 
                                              , ContractId 
                                              , OrderNumber 
                                              , Type 
                                              , MainPoints 
                                              , Opinion   "
                                  + @" from  PHTGL_AttachUrl15_Sch1 "
                                  + @"   where 1=1 and  AttachUrlId=@AttachUrlId  order by Number";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@AttachUrlId", AttachUrlId));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                if (tb.Rows.Count == 0)
                {
                    DataRow dataRow = tb.NewRow();
                    tb.Rows.Add(dataRow);
                }
                tb.TableName = "Table";
                doc.MailMerge.ExecuteWithRegions(tb);
                 if (tb.Rows .Count >0)
                {
                    Dic_File.Add("Sch1_ProjectName", Convert.ToString(tb.Rows[0]["ProjectName"]));
                    Dic_File.Add("Sch1_ContractId", Convert.ToString(tb.Rows[0]["ContractId"]));
                    Dic_File.Add("txtAttachUrlContent", Convert.ToString(tb.Rows[0]["AttachUrlContent"]));
                    Dic_File.Add("Sch1_Opinion", Convert.ToString(tb.Rows[0]["Opinion"]));

                }
                else
                {
                    Dic_File.Add("Sch1_ProjectName", "");
                    Dic_File.Add("Sch1_ContractId", "");
                    Dic_File.Add("txtAttachUrlContent", "");
                    Dic_File.Add("Sch1_Opinion", "");
                }
                #endregion
                #region 附表2
                var Sch2 = BLL.PHTGL_AttachUrl15_Sch2Service.GetPHTGL_AttachUrl15_Sch2ById(AttachUrlId);
                if (Sch2 != null)
                {

                    Dic_File.Add("Sch2_ProjectName", Sch2.ProjectName.ToString());
                    Dic_File.Add("Sch2_ContractId", Sch2.ContractId.ToString());
                    Dic_File.Add("Sch2_Company", Sch2.Company.ToString());
                    Dic_File.Add("Sch2_ConstructionTask", Sch2.ConstructionTask.ToString());
                    Dic_File.Add("Sch2_Maxcapacitance", Sch2.Maxcapacitance.ToString());
                    Dic_File.Add("Sch2_MaxuseWtater", Sch2.MaxuseWtater.ToString());
                    Dic_File.Add("Sch2_elemeterPosition", Sch2.ElemeterPosition.ToString());
                    Dic_File.Add("Sch2_WatermeterPosition", Sch2.WatermeterPosition.ToString());
                    Dic_File.Add("Sch2_elemeterRead", Sch2.ElemeterRead.ToString());
                    Dic_File.Add("Sch2_WatermeterRead", Sch2.WatermeterRead.ToString());
                    if (Sch2.IsApproval.ToString()=="0")
                    {
                        Dic_File.Add("Sch2_IsApproval", "□");
                        Dic_File.Add("Sch2_IsApprovalNo", "√");
                    }
                    else
                    {
                        Dic_File.Add("Sch2_IsApprovalNo", "□");
                        Dic_File.Add("Sch2_IsApproval", "√ ");

                    }
                    if (Sch2.IsLineLayout.ToString()=="0")
                    {
                        Dic_File.Add("Sch2_IsLineLayout", "□");
                        Dic_File.Add("Sch2_IsLineLayoutNo", "√");
                    }
                    else
                    {
                        Dic_File.Add("Sch2_IsLineLayoutNo", "□");
                        Dic_File.Add("Sch2_IsLineLayout", "√ ");
                    }
                    if (Sch2.IsPowerBox.ToString()=="0")
                    {
                        Dic_File.Add("Sch2_IsPowerBox", "□");
                        Dic_File.Add("Sch2_IsPowerBoxNo", "√");
                    }
                    else
                    {
                        Dic_File.Add("Sch2_IsPowerBoxNo", "□");
                        Dic_File.Add("Sch2_IsPowerBox", "√");
                    }

                    if (Sch2.IsProfessional_ele.ToString() == "0")
                    {
                        Dic_File.Add("Sch2_IsProfessional_ele", "□");
                        Dic_File.Add("Sch2_IsProfessional_eleNo", "√");
                    }
                    else
                    {
                        Dic_File.Add("Sch2_IsProfessional_eleNo", "□");
                        Dic_File.Add("Sch2_IsProfessional_ele", "√");
                    }

                    if (Sch2.IsLineInstall.ToString() == "0")
                    {
                        Dic_File.Add("Sch2_IsLineInstall", "□");
                        Dic_File.Add("Sch2_IsLineInstallNo", "√");
                    }
                    else
                    {
                        Dic_File.Add("Sch2_IsLineInstallNo", "□");
                        Dic_File.Add("Sch2_IsLineInstall", "√");
                    }
                    if (Sch2.IsValve.ToString() == "0")
                    {
                        Dic_File.Add("Sch2_IsValve", "□");
                        Dic_File.Add("Sch2_IsValveNo", "√");
                    }
                    else
                    {
                        Dic_File.Add("Sch2_IsValveNo", "□");
                        Dic_File.Add("Sch2_IsValve", "√");
                    }
                    Dic_File.Add("Sch2_Terminalnumber", Sch2.Terminalnumber.ToString());
                    Dic_File.Add("Sch2_LineCabinetNumber", Sch2.LineCabinetNumber.ToString());
                    Dic_File.Add("Sch2_electricPrice", Sch2.ElectricPrice.ToString());
                    Dic_File.Add("Sch2_WaterPrice", Sch2.WaterPrice.ToString());
                }
                else
                {
                    Dic_File.Add("Sch2_ProjectName", "");
                    Dic_File.Add("Sch2_ContractId", "");
                    Dic_File.Add("Sch2_Company", "");
                    Dic_File.Add("Sch2_ConstructionTask", "");
                    Dic_File.Add("Sch2_Maxcapacitance", "");
                    Dic_File.Add("Sch2_MaxuseWtater", "");
                    Dic_File.Add("Sch2_elemeterPosition", "");
                    Dic_File.Add("Sch2_WatermeterPosition", "");
                    Dic_File.Add("Sch2_elemeterRead", "");
                    Dic_File.Add("Sch2_WatermeterRead", "");
                    Dic_File.Add("Sch2_IsApproval", "□");
                    Dic_File.Add("Sch2_IsApprovalNo", "□");
                    Dic_File.Add("Sch2_IsLineLayout", "□");
                    Dic_File.Add("Sch2_IsLineLayoutNo", "□");
                    Dic_File.Add("Sch2_IsPowerBox", "□");
                    Dic_File.Add("Sch2_IsPowerBoxNo", "□");
                    Dic_File.Add("Sch2_IsProfessional_ele", "□");
                    Dic_File.Add("Sch2_IsProfessional_eleNo", "□");
                    Dic_File.Add("Sch2_IsLineInstall", "□");
                    Dic_File.Add("Sch2_IsLineInstallNo", "□");
                    Dic_File.Add("Sch2_IsValve", "□");
                    Dic_File.Add("Sch2_IsValveNo", "□");
                    Dic_File.Add("Sch2_Terminalnumber", "");
                    Dic_File.Add("Sch2_LineCabinetNumber", "");
                    Dic_File.Add("Sch2_electricPrice", "");
                    Dic_File.Add("Sch2_WaterPrice", "");
                }
                #endregion
                #region 附表3
                List<Model.PHTGL_AttachUrl15_Sch3> lists = BLL.PHTGL_AttachUrl15_Sch3Service.GetPHTGL_AttachUrl15ByAttachUrlId(AttachUrlId);
                if (lists .Count ==0)
                {
                    lists.Add(new Model.PHTGL_AttachUrl15_Sch3());

                }
                 DataTable tb2 = LINQToDataTable(lists);
                if (tb2.Rows.Count == 0)
                {
                    DataRow dataRow = tb2.NewRow();
                    tb2.Rows.Add(dataRow);
                }
                tb2.TableName = "Table2";
                doc.MailMerge.ExecuteWithRegions(tb2);
                #endregion
                #region 附表4

                var Sch4 = BLL.PHTGL_AttachUrl15_Sch4Service.GetPHTGL_AttachUrl15_Sch4ById(AttachUrlId);
                if (Sch4 != null)
                {
                    Dic_File.Add("Sch4_ProjectName", Sch4.ProjectName.ToString());
                    Dic_File.Add("Sch4_ContractId", Sch4.ContractId.ToString());
                    Dic_File.Add("Sch4_SubcontractorsName", Sch4.SubcontractorsName.ToString());
                    Dic_File.Add("Sch4_Type",  Sch4.Type.ToString());
                    Dic_File.Add("Sch4_Time", string.Format("{0:D}", Sch4.Time));
                    Dic_File.Add("Sch4_Reason", Sch4.Reason.ToString());
                    Dic_File.Add("Sch4_Position", Sch4.Position.ToString());
                    Dic_File.Add("Sch4_ImpPlan", Sch4.ImpPlan.ToString());
                    Dic_File.Add("Sch4_Recoverymeasures", Sch4.Recoverymeasures.ToString());
                    Dic_File.Add("Sch4_Caption", Sch4.Caption.ToString());
                }
                else
                {
                    Dic_File.Add("Sch4_ProjectName", "");
                    Dic_File.Add("Sch4_ContractId", "");
                    Dic_File.Add("Sch4_SubcontractorsName", "");
                    Dic_File.Add("Sch4_Type", "");
                    Dic_File.Add("Sch4_Time", "");
                    Dic_File.Add("Sch4_Reason", "");
                    Dic_File.Add("Sch4_Position", "");
                    Dic_File.Add("Sch4_ImpPlan", "");
                    Dic_File.Add("Sch4_Recoverymeasures", "");
                    Dic_File.Add("Sch4_Caption", "");

                }
                #endregion
                Dic_File.Add("schNumber", schnumber.ToString());

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }
                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch16(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件16施工质量奖惩管理规定.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                 File.Copy(uploadfilepath, newUrl);
                 Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                Dic_File.Add("schNumber", schnumber.ToString());
 
                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                 ErrLogInfo.WriteLog(string.Empty, ex);
                 Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch17(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件17保密协议书.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                Dic_File.Add("schNumber", schnumber.ToString());
                var SpecialTermsConditionsId = BLL.AttachUrlService.GetAttachUrlById(AttachUrlId).SpecialTermsConditionsId;
                var ContractId = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsById(SpecialTermsConditionsId).ContractId;
                var sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);
                if (sub != null)
                {
                    Dic_File.Add("GeneralContractor", sub.GeneralContractor);
                    Dic_File.Add("SubConstruction", sub.SubConstruction);
 
                }
                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch18(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件18落实施工作业人员待遇承诺书.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                var SpecialTermsConditionsId = BLL.AttachUrlService.GetAttachUrlById(AttachUrlId).SpecialTermsConditionsId;
                var ContractId = BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsById(SpecialTermsConditionsId).ContractId;
                var sub = BLL.SubcontractAgreementService.GetSubcontractAgreementByContractId(ContractId);
                if (sub != null)
                {
                    Dic_File.Add("GeneralContractor", sub.GeneralContractor);
                    Dic_File.Add("SubConstruction", sub.SubConstruction);


                }
                var att = BLL.AttachUrl18Service.GetPHTGL_AttachUrl18ById(AttachUrlId);
                if (att == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("txtGeneralContractorName", "");
                    Dic_File.Add("txtSubcontractorsName", "");
                    Dic_File.Add("txtProjectName", "");
                    Dic_File.Add("txtContractId", "");
                    Dic_File.Add("txtStartDate", "");
                    Dic_File.Add("txtEndDate", "");
                    Dic_File.Add("txtPersonSum", "");
                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;
                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("txtGeneralContractorName", att.GeneralContractorName);
                Dic_File.Add("txtSubcontractorsName", att.SubcontractorsName);
                Dic_File.Add("txtProjectName", att.ProjectName);
                Dic_File.Add("txtContractId", att.ContractId);
                Dic_File.Add("txtStartDate", att.StartDate.ToString());
                Dic_File.Add("txtEndDate", att.EndDate.ToString());
                Dic_File.Add("txtPersonSum", att.PersonSum.ToString());
                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch19(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件19 联合体协议.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {
 
                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();

                var model = AttachUrl19Service.GetPHTGL_AttachUrl19ById(AttachUrlId);

                if (model == null)
                {
                    Dic_File.Add("schNumber", schnumber.ToString());
                    Dic_File.Add("AttachUrlContent", "");

                    foreach (var item in Dic_File)
                    {
                        string[] key = { item.Key };
                        object[] value = { item.Value };
                        doc.MailMerge.Execute(key, value);
                    }
                    if (File.Exists(newUrl))
                    {
                        File.Delete(newUrl);
                    }
                    return doc;
                }
                Dic_File.Add("schNumber", schnumber.ToString());
                Dic_File.Add("AttachUrlContent", model.AttachUrlContent);

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }

                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);


                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }

        public Document sch20(string AttachUrlId, int schnumber)
        {
            string rootPath = Server.MapPath("~/");
            string initTemplatePath = string.Empty;
            string uploadfilepath = string.Empty;
            string newUrl = string.Empty;
            string filePath = string.Empty;
            initTemplatePath = "File\\Word\\PHTGL\\施工分包合同\\附件20   其他.docx";
            uploadfilepath = rootPath + initTemplatePath;
            newUrl = uploadfilepath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".docx");
            filePath = initTemplatePath.Replace(".docx", string.Format("{0:yyyy-MM}", DateTime.Now) + ".pdf");
            try
            {

                File.Copy(uploadfilepath, newUrl);
                Document doc = new Aspose.Words.Document(newUrl);
  
                Dictionary<string, object> Dic_File = new Dictionary<string, object>();
                Dic_File.Add("schNumber", schnumber.ToString());

                foreach (var item in Dic_File)
                {
                    string[] key = { item.Key };
                    object[] value = { item.Value };
                    doc.MailMerge.Execute(key, value);
                }
                var model = AttachUrl20Service.GetAttachUrl20ByAttachUrlId(AttachUrlId);
                if (model != null)
                {
                    var Path = newUrl.Replace(".docx", "编辑栏.docx"); //word文件保存路径
                    AsposeWordHelper.HtmlIntoWord(model.AttachUrlContent, Path);
                    Document doc2 = new Document(Path);

                    doc2.FirstSection.PageSetup.SectionStart = SectionStart.OddPage;

                    doc.AppendDocument(doc2, ImportFormatMode.UseDestinationStyles);
                    File.Delete(Path);
                }
                doc.Save(newUrl);
                //生成PDF文件
                Document doc1 = new Aspose.Words.Document(newUrl);
                File.Delete(newUrl);
                return doc1;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);

                Document doc1 = new Aspose.Words.Document();
                if (File.Exists(newUrl))
                {
                    File.Delete(newUrl);
                }
                return doc1;
            }
        }
        #endregion
    }
}