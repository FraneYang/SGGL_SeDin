using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static  class OAWebSevice
    {
        public static string geturl(string projectid, string formname,string ID)
        {
            string SGGLUrl = Funs.SGGLUrl.Substring(Funs.SGGLUrl.IndexOf("//")+2);
            string url = string.Format("{0}/Login.aspx?projectId={1}&", SGGLUrl, projectid);
            string PHTUrl = "";
            switch (formname)
            {
                case PHTGL_ApproveService.ActionPlanReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}Detail.aspx?ActionPlanReviewId={1}", formname, ID);
                    break;
                case PHTGL_ApproveService.BidDocumentsReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}Detail.aspx?BidDocumentsReviewId={1}", formname, ID);
                     break;
                case PHTGL_ApproveService.ApproveUserReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}Detail.aspx?ApproveUserReviewID={1}", formname, ID);
                     break;
                case PHTGL_ApproveService.SetSubReview :
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}Detail.aspx?SetSubReviewID={1}", formname, ID);
                     break;
                case PHTGL_ApproveService.ContractReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/ContractCompile/{0}Detail.aspx?ContractReviewId={1}", formname, ID);
                     break;
            }
            url = url + PHTUrl;
            return url;
       }
        /// <summary>
        /// 获取审批表单主界面
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="formname"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string geturl_Form(string projectid, string formname)
        {
            string SGGLUrl = Funs.SGGLUrl.Substring(Funs.SGGLUrl.IndexOf("//") + 2);
            string url = string.Format("{0}/Login.aspx?projectId={1}&", SGGLUrl, projectid);
            string PHTUrl = "";
            switch (formname)
            {
                case PHTGL_ApproveService.ActionPlanReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}.aspx", formname);
                    break;
                case PHTGL_ApproveService.BidDocumentsReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}.aspx", formname);
                    break;
                case PHTGL_ApproveService.ApproveUserReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}.aspx", formname);
                    break;
                case PHTGL_ApproveService.SetSubReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/BiddingManagement/{0}.aspx", formname);
                    break;
                case PHTGL_ApproveService.ContractReview:
                    PHTUrl = string.Format("PHTUrl=PHTGL/ContractCompile/{0}.aspx", formname);
                    break;
            }
            url = url + PHTUrl;
            return url;
        }


        public static void Pushoa_Creater(string id)
        {

            OAWebService.OfsTodoDataWebServicePortTypeClient OAWeb = new OAWebService.OfsTodoDataWebServicePortTypeClient();
            OAWebJson webJson = new OAWebJson();
            webJson.syscode = "shigong";
            webJson.workflowname = "审批";
            webJson.appurl = "";
            webJson.receivedatetime = DateTime.Now.ToString();
            webJson.createdatetime = DateTime.Now.ToString();

            var users = PHTGL_ApproveService.GetPHTGL_ApproveById(id);
            try
            {
     
                  switch (users.ApproveForm)
                        {
                            case PHTGL_ApproveService.ActionPlanReview:
                                var Acp = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(users.ContractId);
                                if (Acp != null)
                                {
                                    var gereceiver = BLL.UserService.GetUserByUserId(Acp.CreateUser);
                                    var geCreatUser = BLL.UserService.GetUserByUserId(users.ApproveMan);
                                    var Act = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(Acp.ActionPlanID);
                                    webJson.flowid = users.ApproveId;
                                    webJson.requestname = "施工分包实施计划审批";
                                    webJson.nodename = users.ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl_Form(Act.ProjectID, PHTGL_ApproveService.ActionPlanReview);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users.ApproveId);
                                        Approve.IsPushOa = 3;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.ApproveUserReview:
                                var BidApp = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(users.ContractId);
                                if (BidApp != null)
                                {
                                    var gereceiver = BLL.UserService.GetUserByUserId(BidApp.CreateUser);
                                    var geCreatUser = BLL.UserService.GetUserByUserId(users.ApproveMan);
                                    webJson.flowid = users.ApproveId;
                                    webJson.requestname = "评标小组名单审批";
                                    webJson.nodename = users.ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl_Form(BidApp.ProjectId, PHTGL_ApproveService.ApproveUserReview);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users.ApproveId);
                                        Approve.IsPushOa = 3;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.BidDocumentsReview:
                                var Biddoc = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(users.ContractId);
                                if (Biddoc != null)
                                {
                                    var gereceiver = BLL.UserService.GetUserByUserId(Biddoc.CreateUser);
                                    var geCreatUser = BLL.UserService.GetUserByUserId(users.ApproveMan);
                                    webJson.flowid = users.ApproveId;
                                    webJson.requestname = "招标文件审批";
                                    webJson.nodename = users.ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl_Form(Biddoc.ProjectId, PHTGL_ApproveService.BidDocumentsReview);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users.ApproveId);
                                        Approve.IsPushOa = 3;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.SetSubReview:
                                var Sub = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(users.ContractId);
                                var BidApp2 = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(Sub.ApproveUserReviewID);
                                if (Sub != null)
                                {
                                    var gereceiver = BLL.UserService.GetUserByUserId(Sub.CreateUser);
                                    var geCreatUser = BLL.UserService.GetUserByUserId(users.ApproveMan);
                                    webJson.flowid = users.ApproveId;
                                    webJson.requestname = "确定分包商审批审批";
                                    webJson.nodename = users.ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl_Form(BidApp2.ProjectId, PHTGL_ApproveService.SetSubReview);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users.ApproveId);
                                        Approve.IsPushOa = 3;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.ContractReview:
                                var Ctr = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(users.ContractId);
                                var Con = BLL.ContractService.GetContractById(Ctr.ContractId);
                                if (Ctr != null)
                                {
                                    var gereceiver = BLL.UserService.GetUserByUserId(Ctr.CreateUser);
                                    var geCreatUser = BLL.UserService.GetUserByUserId(users.ApproveMan);
                                    webJson.flowid = users.ApproveId;
                                    webJson.requestname = "合同审批";
                                    webJson.nodename = users.ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl_Form(Con.ProjectId, PHTGL_ApproveService.ContractReview);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users.ApproveId);
                                        Approve.IsPushOa = 3;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                        }

             }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(ex.ToString());

            }

        }


        public static void Pushoa()
        {
 
            OAWebService.OfsTodoDataWebServicePortTypeClient OAWeb = new OAWebService.OfsTodoDataWebServicePortTypeClient();
            OAWebJson webJson = new OAWebJson();
            webJson.syscode = "shigong";
            webJson.workflowname = "审批";
            webJson.appurl = "";
            webJson.receivedatetime = DateTime.Now.ToString();
            webJson.createdatetime = DateTime.Now.ToString();

            var users =  PHTGL_ApproveService.GetApproves_NopushOa();
            try
            {
                if (users.Count > 0)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        switch (users[i].ApproveForm)
                        {
                            case PHTGL_ApproveService.ActionPlanReview:
                                var Acp = BLL.PHTGL_ActionPlanReviewService.GetPHTGL_ActionPlanReviewById(users[i].ContractId);
                                if (Acp != null)
                                {
                                    var geCreatUser = BLL.UserService.GetUserByUserId(Acp.CreateUser);
                                    var gereceiver = BLL.UserService.GetUserByUserId(users[i].ApproveMan);
                                    var Act = BLL.PHTGL_ActionPlanFormationService.GetPHTGL_ActionPlanFormationById(Acp.ActionPlanID);
                                    webJson.flowid = users[i].ApproveId;
                                    webJson.requestname = "施工分包实施计划审批";
                                    webJson.nodename = users[i].ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl(Act.ProjectID, PHTGL_ApproveService.ActionPlanReview, Acp.ActionPlanReviewId);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users[i].ApproveId);
                                        Approve.IsPushOa = 1;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.ApproveUserReview:
                                var BidApp = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(users[i].ContractId);
                                if (BidApp != null)
                                {
                                    var geCreatUser = BLL.UserService.GetUserByUserId(BidApp.CreateUser);
                                    var gereceiver = BLL.UserService.GetUserByUserId(users[i].ApproveMan);
                                    webJson.flowid = users[i].ApproveId;
                                    webJson.requestname = "评标小组名单审批";
                                    webJson.nodename = users[i].ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                     webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl(BidApp.ProjectId, PHTGL_ApproveService.ApproveUserReview, BidApp.ApproveUserReviewID);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users[i].ApproveId);
                                        Approve.IsPushOa = 1;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.BidDocumentsReview:
                                var Biddoc = BLL.PHTGL_BidDocumentsReviewService.GetPHTGL_BidDocumentsReviewById(users[i].ContractId);
                                if (Biddoc != null)
                                {
                                    var geCreatUser = BLL.UserService.GetUserByUserId(Biddoc.CreateUser);
                                    var gereceiver = BLL.UserService.GetUserByUserId(users[i].ApproveMan);
                                    webJson.flowid = users[i].ApproveId;
                                    webJson.requestname = "招标文件审批";
                                    webJson.nodename = users[i].ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl(Biddoc.ProjectId, PHTGL_ApproveService.BidDocumentsReview, Biddoc.BidDocumentsReviewId);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users[i].ApproveId);
                                        Approve.IsPushOa = 1;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.SetSubReview:
                                var Sub = BLL.PHTGL_SetSubReviewService.GetPHTGL_SetSubReviewById(users[i].ContractId);
                                var BidApp2 = BLL.PHTGL_BidApproveUserReviewService.GetPHTGL_BidApproveUserReviewById(Sub.ApproveUserReviewID);
                                if (Sub != null)
                                {
                                    var geCreatUser = BLL.UserService.GetUserByUserId(Sub.CreateUser);
                                    var gereceiver = BLL.UserService.GetUserByUserId(users[i].ApproveMan);
                                    webJson.flowid = users[i].ApproveId;
                                    webJson.requestname = "确定分包商审批审批";
                                    webJson.nodename = users[i].ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl(BidApp2.ProjectId, PHTGL_ApproveService.SetSubReview, Sub.SetSubReviewID);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users[i].ApproveId);
                                        Approve.IsPushOa = 1;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                            case PHTGL_ApproveService.ContractReview:
                                var Ctr = BLL.PHTGL_ContractReviewService.GetPHTGL_ContractReviewById(users[i].ContractId);
                                var Con = BLL.ContractService.GetContractById(Ctr.ContractId);
                                if (Ctr != null)
                                {
                                    var geCreatUser = BLL.UserService.GetUserByUserId(Ctr.CreateUser);
                                    var gereceiver = BLL.UserService.GetUserByUserId(users[i].ApproveMan);
                                    webJson.flowid = users[i].ApproveId;
                                    webJson.requestname = "合同审批";
                                    webJson.nodename = users[i].ApproveType;
                                    webJson.creator = geCreatUser.UserCode;
                                    webJson.receiver = gereceiver.UserCode;
                                    webJson.pcurl = geturl(Con.ProjectId, PHTGL_ApproveService.ContractReview, Ctr.ContractReviewId);
                                    string strjson = JsonConvert.SerializeObject(webJson);
                                    var returnjson = OAWeb.receiveTodoRequestByJson(strjson);
                                    ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);

                                    if (reviceReturn.operResult == "1")
                                    {
                                        var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(users[i].ApproveId);
                                        Approve.IsPushOa = 1;
                                        PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
                                    }
                                }
                                break;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog( ex.ToString());

             }
             
        }

        /// <summary>
        /// 处理待办流程（变为已办）
        /// </summary>
        /// <param name="id"></param>
        public static void DoneRequest(string id)
        {
         
            OAWebService.OfsTodoDataWebServicePortTypeClient OAWeb = new OAWebService.OfsTodoDataWebServicePortTypeClient();
             var Approve = PHTGL_ApproveService.GetPHTGL_ApproveById(id);
             var gereceiver = BLL.UserService.GetUserByUserId(Approve.ApproveMan);

            OaDoneJson webJson = new OaDoneJson();
            webJson.syscode = "shigong";
            webJson.workflowname = "审批";
            webJson.flowid = Approve.ApproveId;
            webJson.nodename = Approve.ApproveType;
            webJson.receiver = gereceiver.UserCode;
            switch (Approve.ApproveForm)
            {
                case PHTGL_ApproveService.ActionPlanReview:
                     webJson.requestname = "施工分包实施计划审批";
                     break;
                case PHTGL_ApproveService.ApproveUserReview:
                    webJson.requestname = "评标小组名单审批";

                    break;
                case PHTGL_ApproveService.BidDocumentsReview:
                    webJson.requestname = "招标文件审批";


                    break;
                case PHTGL_ApproveService.SetSubReview:
                    webJson.requestname = "确定分包商审批";


                    break;
                case PHTGL_ApproveService.ContractReview:
                    webJson.requestname = "合同审批";

                    break;
            }
            string strjson = JsonConvert.SerializeObject(webJson);
            var returnjson = OAWeb.processDoneRequestByJson (strjson);

            ReviceReturn reviceReturn = JsonConvert.DeserializeObject<ReviceReturn>(returnjson);
            if (reviceReturn.operResult == "1")
            {
                 Approve.IsPushOa = 2;
                PHTGL_ApproveService.UpdatePHTGL_Approve(Approve);
            }
         }

    }
    public class OAWebJson
    {
        /// <summary>
        /// 异构系统标识
        /// </summary>
        public string syscode { get; set; }
        /// <summary>
        /// 流程任务id
        /// </summary>
        public string flowid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string requestname { get; set; }
        /// <summary>
        /// 流程类型名称
        /// </summary>
        public string workflowname { get; set; }
        /// <summary>
        /// 步骤名称（节点名称）
        /// </summary>
        public string nodename { get; set; }
        /// <summary>
        /// PC地址
        /// </summary>
        public string pcurl { get; set; }
        /// <summary>
        /// APP地址
        /// </summary>
        public string appurl { get; set; }
        /// <summary>
        /// 创建人（原值)
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 创建日期时间
        /// </summary>
        public string createdatetime { get; set; }
        /// <summary>
        /// 接收人（原值）
        /// </summary>
        public string receiver { get; set; }
        /// <summary>
        /// 接收日期时间
        /// </summary>
        public string receivedatetime { get; set; }
    }

    public class ReviceReturn
    {
        /// <summary>
        /// 
        /// </summary>
        public string syscode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dateType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
    }

    public class OaDoneJson {
        /// <summary>
        /// 异构系统标识
        /// </summary>
        public string syscode { get; set; }
        /// <summary>
        /// 流程任务id
        /// </summary>
        public string flowid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string requestname { get; set; }
        /// <summary>
        /// 流程类型名称
        /// </summary>
        public string workflowname { get; set; }
        /// <summary>
        /// 步骤名称（节点名称）
        /// </summary>
        public string nodename { get; set; }
        /// <summary>
        /// 接收人（原值）
        /// </summary>
        public string receiver { get; set; }
    }
}
