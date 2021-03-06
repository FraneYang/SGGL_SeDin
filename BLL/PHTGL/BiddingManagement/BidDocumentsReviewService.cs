﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public static class PHTGL_BidDocumentsReviewService
    {

        public static Model.PHTGL_BidDocumentsReview GetPHTGL_BidDocumentsReviewById(string BidDocumentsReviewId)
        {
            return Funs.DB.PHTGL_BidDocumentsReview.FirstOrDefault(e => e.BidDocumentsReviewId == BidDocumentsReviewId);
        }


        public static void AddPHTGL_BidDocumentsReview(Model.PHTGL_BidDocumentsReview newtable)
        {
            Model.PHTGL_BidDocumentsReview table = new Model.PHTGL_BidDocumentsReview();
            table.BidDocumentsReviewId = newtable.BidDocumentsReviewId;
            table.ActionPlanReviewId = newtable.ActionPlanReviewId;
            table.ActionPlanID = newtable.ActionPlanID;
            table.Bidding_StartTime = newtable.Bidding_StartTime;
            table.Url = newtable.Url;
            table.CreateUser = newtable.CreateUser;
            table.CreatTime = newtable.CreatTime;
            table.ConstructionManager = newtable.ConstructionManager;
            table.ControlManager = newtable.ControlManager;
            table.PreliminaryMan = newtable.PreliminaryMan;
            table.ProjectManager = newtable.ProjectManager;
            table.ProjectId = newtable.ProjectId;
            table.Approval_Construction = newtable.Approval_Construction;
            table.State = newtable.State;
            table.BidContent = newtable.BidContent;
            table.BidType = newtable.BidType;
            table.BidDocumentsName = newtable.BidDocumentsName;
            table.BidDocumentsCode = newtable.BidDocumentsCode;
            table.Bidding_SendTime = newtable.Bidding_SendTime;
            Funs.DB.PHTGL_BidDocumentsReview.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_BidDocumentsReview(Model.PHTGL_BidDocumentsReview newtable)
        {
            Model.PHTGL_BidDocumentsReview table = Funs.DB.PHTGL_BidDocumentsReview.FirstOrDefault(e => e.BidDocumentsReviewId == newtable.BidDocumentsReviewId);

            if (table != null)
            {
                table.BidDocumentsReviewId = newtable.BidDocumentsReviewId;
                table.ActionPlanReviewId = newtable.ActionPlanReviewId;
                table.ActionPlanID = newtable.ActionPlanID;
                table.Bidding_StartTime = newtable.Bidding_StartTime;
                table.Url = newtable.Url;
                table.CreateUser = newtable.CreateUser;
                table.CreatTime = newtable.CreatTime;
                table.ConstructionManager = newtable.ConstructionManager;
                table.ControlManager = newtable.ControlManager;
                table.PreliminaryMan = newtable.PreliminaryMan;
                table.ProjectManager = newtable.ProjectManager;
                table.ProjectId = newtable.ProjectId;
                table.Approval_Construction = newtable.Approval_Construction;
                table.State = newtable.State;
                table.BidContent = newtable.BidContent;
                table.BidType = newtable.BidType;
                table.BidDocumentsName = newtable.BidDocumentsName;
                table.BidDocumentsCode = newtable.BidDocumentsCode;
                table.Bidding_SendTime = newtable.Bidding_SendTime;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_BidDocumentsReviewById(string BidDocumentsReviewId)
        {
            Model.PHTGL_BidDocumentsReview table = Funs.DB.PHTGL_BidDocumentsReview.FirstOrDefault(e => e.BidDocumentsReviewId == BidDocumentsReviewId);
            if (table != null)
            {
                Funs.DB.PHTGL_BidDocumentsReview.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }
        /// <summary>
        /// 获取审批人员
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public static Dictionary<int, string> Get_DicApproveman(string projectid, string BidDocumentsReviewId)
        {
            Dictionary<int, string> Dic_Approveman = new Dictionary<int, string>();

            Model.PHTGL_BidDocumentsReview table = GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);

            Dic_Approveman.Add(1, table.ConstructionManager);
            Dic_Approveman.Add(2,table.ControlManager);
            Dic_Approveman.Add(3, table.ProjectManager);
            Dic_Approveman.Add(4,table.PreliminaryMan );
            Dic_Approveman.Add(5, table.Approval_Construction);
            return Dic_Approveman;
        }
        public static List<ApproveManModel> GetApproveManModels(string projectid, string BidDocumentsReviewId)
        {

            Model.PHTGL_BidDocumentsReview table = GetPHTGL_BidDocumentsReviewById(BidDocumentsReviewId);

            List<ApproveManModel> approveManModels = new List<ApproveManModel>();
            approveManModels.Add(new ApproveManModel { Number = 1, userid = table.ConstructionManager, Rolename = "施工经理" });
            approveManModels.Add(new ApproveManModel { Number = 2, userid = table.ControlManager, Rolename = "控制经理" });
            approveManModels.Add(new ApproveManModel { Number = 3, userid = table.ProjectManager, Rolename = "项目经理" });
            approveManModels.Add(new ApproveManModel { Number = 4, userid = table.PreliminaryMan, Rolename = "预审人员" });
            approveManModels.Add(new ApproveManModel { Number = 5, userid = table.Approval_Construction, Rolename = "施工管理部" });
            return approveManModels;
        }
        /// <summary>
        /// 招标方式下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="RoleId">角色id</param>
        /// <param name="unitId">单位id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitGetBidTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Value";
            dropName.DataSource = BLL.DropListService.GetBidType();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 获取审批完成的招标文件审批
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        public static void InitGetBidCompleteDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "BidDocumentsReviewId";
            dropName.DataTextField = "BidDocumentsCode";
            dropName.DataSource = GetCompleteBidDocument();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        public  static  List<Model.PHTGL_BidDocumentsReview> GetCompleteBidDocument()
        { 
                var list = (from x in Funs.DB.PHTGL_BidDocumentsReview
                            where  x.State ==Const.ContractReview_Complete
                            select x).ToList();
                return list;
        }



    }
}