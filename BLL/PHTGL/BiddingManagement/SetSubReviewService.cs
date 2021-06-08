using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{

    public static class PHTGL_SetSubReviewService
    {
        /// <summary>
        /// 类型综合评估法
        /// </summary>
        public   const int Type_ConEvaluation = 2;
        /// <summary>
        /// 类型用于经评审的最低投标报价法
        /// </summary>
        public const int Type_MinPrice = 1;

        public static ListItem[] GetCreateType()
        {
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("综合评估法", Type_ConEvaluation.ToString ());
            list[1] = new ListItem("经评审的最低投标报价法", Type_MinPrice.ToString ());
            return list;
        }
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="SetSubReviewID"></param>
        /// <returns></returns>
        public static Model.PHTGL_SetSubReview GetPHTGL_SetSubReviewById(string SetSubReviewID)
        {
            return Funs.DB.PHTGL_SetSubReview.FirstOrDefault(e => e.SetSubReviewID == SetSubReviewID);
        }
        /// <summary>
        /// 根据编号获得实体
        /// </summary>
        /// <param name="setSubReviewCode"></param>
        /// <returns></returns>
        public static Model.PHTGL_SetSubReview GetPHTGL_SetSubReviewBySetSubReviewCode(string setSubReviewCode)
        {
            return Funs.DB.PHTGL_SetSubReview.FirstOrDefault(e => e.SetSubReviewCode == setSubReviewCode);
        }


        public static void AddPHTGL_SetSubReview(Model.PHTGL_SetSubReview newtable)
        {
            Model.PHTGL_SetSubReview table = new Model.PHTGL_SetSubReview();
            table.SetSubReviewID = newtable.SetSubReviewID;
            table.DeputyGeneralManager = newtable.DeputyGeneralManager;
            table.ApproveUserReviewID = newtable.ApproveUserReviewID;
            table.ActionPlanID = newtable.ActionPlanID;
            table.SetSubReviewCode = newtable.SetSubReviewCode;
            table.CreateUser = newtable.CreateUser;
            table.State = newtable.State;
            table.Type = newtable.Type;
            table.ConstructionManager = newtable.ConstructionManager;
            table.ProjectManager = newtable.ProjectManager;
            table.Approval_Construction = newtable.Approval_Construction;
            Funs.DB.PHTGL_SetSubReview.InsertOnSubmit(table);
            Funs.DB.SubmitChanges();
        }


        public static void UpdatePHTGL_SetSubReview(Model.PHTGL_SetSubReview newtable)
        {
            Model.PHTGL_SetSubReview table = Funs.DB.PHTGL_SetSubReview.FirstOrDefault(e => e.SetSubReviewID == newtable.SetSubReviewID
);

            if (table != null)
            {
                table.SetSubReviewID = newtable.SetSubReviewID;
                table.DeputyGeneralManager = newtable.DeputyGeneralManager;
                table.ApproveUserReviewID = newtable.ApproveUserReviewID;
                table.ActionPlanID = newtable.ActionPlanID;
                table.SetSubReviewCode = newtable.SetSubReviewCode;
                table.CreateUser = newtable.CreateUser;
                table.State = newtable.State;
                table.Type = newtable.Type;
                table.ConstructionManager = newtable.ConstructionManager;
                table.ProjectManager = newtable.ProjectManager;
                table.Approval_Construction = newtable.Approval_Construction;
                Funs.DB.SubmitChanges();
            }

        }
        public static void DeletePHTGL_SetSubReviewById(string SetSubReviewID
)
        {
            Model.PHTGL_SetSubReview table = Funs.DB.PHTGL_SetSubReview.FirstOrDefault(e => e.SetSubReviewID == SetSubReviewID
);
            if (table != null)
            {
                Funs.DB.PHTGL_SetSubReview.DeleteOnSubmit(table);
                Funs.DB.SubmitChanges();
            }

        }

        public static Dictionary<int, string> Get_DicApproveman(string SetSubReviewID)
        {
            Dictionary<int, string> Dic_Approveman = new Dictionary<int, string>();

            Model.PHTGL_SetSubReview table = GetPHTGL_SetSubReviewById(SetSubReviewID);

            Dic_Approveman.Add(1, table.ConstructionManager);
            Dic_Approveman.Add(2, table.Approval_Construction);
            Dic_Approveman.Add(3, table.ProjectManager);
            Dic_Approveman.Add(4, table.DeputyGeneralManager);

            return Dic_Approveman;
        }

        public static void InitGetSetSubCompleteDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "SetSubReviewCode";
            dropName.DataTextField = "SetSubReviewCode";
            dropName.DataSource = GetCompleteSetSubReview();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        public static object GetCompleteSetSubReview()
        { var list = (from x in Funs.DB.PHTGL_SetSubReview
                      where x.State==Const.ContractReview_Complete
                      select x).ToList();
          return list;
        }

    }
}