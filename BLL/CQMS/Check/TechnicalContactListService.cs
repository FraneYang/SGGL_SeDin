using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class TechnicalContactListService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据工程联络单信息Id删除一个工程联络单信息信息
        /// </summary>
        /// <param name="TechnicalContactListCode">工程联络单信息Id</param>
        public static void DeleteTechnicalContactList(string TechnicalContactListId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_TechnicalContactList TechnicalContactList = db.Check_TechnicalContactList.First(e => e.TechnicalContactListId == TechnicalContactListId);

            db.Check_TechnicalContactList.DeleteOnSubmit(TechnicalContactList);
            db.SubmitChanges();
        }


        /// <summary>
        /// 增加工程联络单信息信息
        /// </summary>
        /// <param name="TechnicalContactList">工程联络单信息实体</param>
        public static void AddTechnicalContactListForApi(Model.Check_TechnicalContactList TechnicalContactList)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_TechnicalContactList newTechnicalContactList = new Model.Check_TechnicalContactList();
                newTechnicalContactList.TechnicalContactListId = TechnicalContactList.TechnicalContactListId;
                newTechnicalContactList.ProjectId = TechnicalContactList.ProjectId;
                newTechnicalContactList.Code = TechnicalContactList.Code;
                newTechnicalContactList.ProposedUnitId = TechnicalContactList.ProposedUnitId;
                newTechnicalContactList.MainSendUnitId = TechnicalContactList.MainSendUnitId;
                newTechnicalContactList.CCUnitIds = TechnicalContactList.CCUnitIds;
                newTechnicalContactList.UnitWorkId = TechnicalContactList.UnitWorkId;
                newTechnicalContactList.CNProfessionalCode = TechnicalContactList.CNProfessionalCode;
                newTechnicalContactList.ContactListType = TechnicalContactList.ContactListType;
                newTechnicalContactList.IsReply = TechnicalContactList.IsReply;
                newTechnicalContactList.Cause = TechnicalContactList.Cause;
                newTechnicalContactList.Contents = TechnicalContactList.Contents;
                newTechnicalContactList.AttachUrl = TechnicalContactList.AttachUrl;
                newTechnicalContactList.CompileMan = TechnicalContactList.CompileMan;
                newTechnicalContactList.CompileDate = TechnicalContactList.CompileDate;
                newTechnicalContactList.State = TechnicalContactList.State;
                newTechnicalContactList.ReOpinion = TechnicalContactList.ReOpinion;
                db.Check_TechnicalContactList.InsertOnSubmit(newTechnicalContactList);
                db.SubmitChanges();
            }
        }
        public static void AddTechnicalContactList(Model.Check_TechnicalContactList TechnicalContactList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_TechnicalContactList newTechnicalContactList = new Model.Check_TechnicalContactList();
            newTechnicalContactList.TechnicalContactListId = TechnicalContactList.TechnicalContactListId;
            newTechnicalContactList.ProjectId = TechnicalContactList.ProjectId;
            newTechnicalContactList.Code = TechnicalContactList.Code;
            newTechnicalContactList.ProposedUnitId = TechnicalContactList.ProposedUnitId;
            newTechnicalContactList.MainSendUnitId = TechnicalContactList.MainSendUnitId;
            newTechnicalContactList.CCUnitIds = TechnicalContactList.CCUnitIds;
            newTechnicalContactList.UnitWorkId = TechnicalContactList.UnitWorkId;
            newTechnicalContactList.CNProfessionalCode = TechnicalContactList.CNProfessionalCode;
            newTechnicalContactList.ContactListType = TechnicalContactList.ContactListType;
            newTechnicalContactList.IsReply = TechnicalContactList.IsReply;
            newTechnicalContactList.Cause = TechnicalContactList.Cause;
            newTechnicalContactList.Contents = TechnicalContactList.Contents;
            newTechnicalContactList.AttachUrl = TechnicalContactList.AttachUrl;
            newTechnicalContactList.CompileMan = TechnicalContactList.CompileMan;
            newTechnicalContactList.CompileDate = TechnicalContactList.CompileDate;
            newTechnicalContactList.State = TechnicalContactList.State;
            newTechnicalContactList.ReOpinion = TechnicalContactList.ReOpinion;
            db.Check_TechnicalContactList.InsertOnSubmit(newTechnicalContactList);
            db.SubmitChanges();
        }
        /// <summary>
        /// 增加工程联络单审批信息
        /// </summary>
        /// <param name="managerRuleApprove">工程联络单审批实体</param>
        public static void AddTechnicalContactListApprove(Model.Check_TechnicalContactListApprove approve)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Check_TechnicalContactListApprove));
            Model.Check_TechnicalContactListApprove newApprove = new Model.Check_TechnicalContactListApprove();
            newApprove.TechnicalContactListApproveId = newKeyID;
            newApprove.TechnicalContactListId = approve.TechnicalContactListId;
            newApprove.ApproveMan = approve.ApproveMan;
            newApprove.ApproveDate = approve.ApproveDate;
            newApprove.ApproveIdea = approve.ApproveIdea;
            newApprove.IsAgree = approve.IsAgree;
            newApprove.ApproveType = approve.ApproveType;

            db.Check_TechnicalContactListApprove.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工程联络单信息信息
        /// </summary>
        /// <param name="TechnicalContactList">工程联络单信息实体</param>
        public static void UpdateTechnicalContactList(Model.Check_TechnicalContactList TechnicalContactList)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_TechnicalContactList newTechnicalContactList = db.Check_TechnicalContactList.First(e => e.TechnicalContactListId == TechnicalContactList.TechnicalContactListId);
            newTechnicalContactList.Code = TechnicalContactList.Code;
            newTechnicalContactList.ProposedUnitId = TechnicalContactList.ProposedUnitId;
            newTechnicalContactList.MainSendUnitId = TechnicalContactList.MainSendUnitId;
            newTechnicalContactList.CCUnitIds = TechnicalContactList.CCUnitIds;
            newTechnicalContactList.UnitWorkId = TechnicalContactList.UnitWorkId;
            newTechnicalContactList.CNProfessionalCode = TechnicalContactList.CNProfessionalCode;
            newTechnicalContactList.ContactListType = TechnicalContactList.ContactListType;
            newTechnicalContactList.IsReply = TechnicalContactList.IsReply;
            newTechnicalContactList.Cause = TechnicalContactList.Cause;
            newTechnicalContactList.Contents = TechnicalContactList.Contents;
            newTechnicalContactList.AttachUrl = TechnicalContactList.AttachUrl;
            newTechnicalContactList.ReAttachUrl = TechnicalContactList.ReAttachUrl;
            newTechnicalContactList.State = TechnicalContactList.State;
            newTechnicalContactList.ReOpinion = TechnicalContactList.ReOpinion;
            db.SubmitChanges();
        }


        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }
        public static void InitHandleType(FineUIPro.DropDownList dropName, bool isShowPlease, string state, string unitType, string contactListType, string isReply)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetDHandleTypeByState(state, unitType, contactListType, isReply);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }


        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state, string unitType, string contactListType, string isReply)
        {
            if (state == Const.TechnicalContactList_Compile || state == Const.TechnicalContactList_ReCompile)
            {
                if (unitType == Const.ProjectUnitType_2)  //施工分包商
                {
                    if (contactListType == "1")  //图纸类
                    {
                        ListItem[] lis = new ListItem[1];
                        lis[0] = new ListItem("分包负责人审批", Const.TechnicalContactList_Audit1);
                        //lis[1] = new ListItem("总包专工确认", Const.TechnicalContactList_Audit2);
                        //lis[2] = new ListItem("总包设计人员回复", Const.TechnicalContactList_Audit4);
                        //lis[3] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[4] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        return lis;
                    }
                    else  //非图纸类
                    {
                        ListItem[] lis = new ListItem[1];
                        lis[0] = new ListItem("分包负责人审批", Const.TechnicalContactList_Audit1);
                        //lis[1] = new ListItem("总包专工回复", Const.TechnicalContactList_Audit2H);
                        //lis[2] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[3] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        return lis;
                    }
                }
                else   //总包
                {
                    if (isReply == "1")  //需要回复
                    {
                        ListItem[] lis = new ListItem[1];
                        lis[0] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[1] = new ListItem("分包专工回复", Const.TechnicalContactList_Audit6);
                        //lis[2] = new ListItem("分包负责人审批", Const.TechnicalContactList_Audit1);
                        //lis[3] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        return lis;
                    }
                    else  //不需回复
                    {
                        ListItem[] lis = new ListItem[1];
                        lis[0] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[1] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        return lis;
                    }
                }
            }
            else if (state == Const.TechnicalContactList_Audit1)
            {
                if (unitType == Const.ProjectUnitType_2)  //施工分包商
                {
                    if (isReply == "1")  //需要回复
                    {
                        if (contactListType == "1")  //图纸类
                        {
                            ListItem[] lis = new ListItem[2];
                            lis[0] = new ListItem("总包专工确认", Const.TechnicalContactList_Audit2);
                            //lis[1] = new ListItem("总包设计人员回复", Const.TechnicalContactList_Audit4);
                            //lis[2] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                            //lis[3] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                            lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                            return lis;
                        }
                        else  //非图纸类
                        {
                            ListItem[] lis = new ListItem[2];
                            lis[0] = new ListItem("总包专工回复", Const.TechnicalContactList_Audit2H);
                            //lis[1] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                            //lis[2] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                            lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                            return lis;
                        }
                    }
                    else
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                }
                else   //总包
                {
                    if (isReply == "1")  //需要回复
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("分包专工重新回复", Const.TechnicalContactList_Audit6R);
                        return lis;
                    }
                    else  //不需回复
                    {
                        return null;
                    }
                }
            }
            else if (state == Const.TechnicalContactList_Audit2 || state == Const.TechnicalContactList_Audit2R || state == Const.TechnicalContactList_Audit2H)
            {
                if (unitType == Const.ProjectUnitType_2)  //施工分包商
                {
                    if (contactListType == "1")  //图纸类
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("总包设计人员回复", Const.TechnicalContactList_Audit4);
                        //lis[1] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        //lis[2] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        return lis;
                    }
                    else  //非图纸类
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[1] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                }
                else   //总包
                {
                    if (isReply == "1")  //需要回复
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[1] = new ListItem("分包专工回复", Const.TechnicalContactList_Audit6);
                        //lis[2] = new ListItem("分包负责人审批", Const.TechnicalContactList_Audit1);
                        //lis[3] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                    else  //不需回复
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[1] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                }
            }
            else if (state == Const.TechnicalContactList_Audit3)
            {
                if (unitType == Const.ProjectUnitType_2)  //施工分包商
                {
                    if (contactListType == "1")  //图纸类
                    {
                        ListItem[] lis = new ListItem[3];
                        lis[0] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        lis[2] = new ListItem("总包设计人员重新回复", Const.TechnicalContactList_Audit4R);
                        return lis;
                    }
                    else  //非图纸类
                    {
                        ListItem[] lis = new ListItem[3];
                        lis[0] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        lis[2] = new ListItem("总包专工重新回复", Const.TechnicalContactList_Audit2R);
                        return lis;
                    }
                }
                else   //总包
                {
                    if (isReply == "1")  //需要回复
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("分包专工回复", Const.TechnicalContactList_Audit6);
                        //lis[1] = new ListItem("分包负责人审批", Const.TechnicalContactList_Audit1);
                        //lis[2] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                    else  //不需回复
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                }
            }
            else if (state == Const.TechnicalContactList_Audit4 || state == Const.TechnicalContactList_Audit4R)
            {
                if (unitType == Const.ProjectUnitType_2)  //施工分包商
                {
                    if (contactListType == "1")  //图纸类
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("总包负责人审批", Const.TechnicalContactList_Audit3);
                        //lis[1] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                    else  //非图纸类
                    {
                        return null;
                    }
                }
                else   //总包
                {
                    if (isReply == "1")  //需要回复
                    {
                        return null;
                    }
                    else  //不需回复
                    {
                        return null;
                    }
                }
            }
            else if (state == Const.TechnicalContactList_Audit6 || state == Const.TechnicalContactList_Audit6R)
            {
                if (unitType == Const.ProjectUnitType_2)  //施工分包商
                {
                    if (contactListType == "1")  //图纸类
                    {
                        return null;
                    }
                    else  //非图纸类
                    {
                        return null;
                    }
                }
                else   //总包
                {
                    if (isReply == "1")  //需要回复
                    {
                        ListItem[] lis = new ListItem[2];
                        lis[0] = new ListItem("分包负责人审批", Const.TechnicalContactList_Audit1);
                        //lis[1] = new ListItem("审批完成", Const.TechnicalContactList_Complete);
                        lis[1] = new ListItem("重新编制", Const.TechnicalContactList_ReCompile);
                        return lis;
                    }
                    else  //不需回复
                    {
                        return null;
                    }
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.Check_TechnicalContactList> qq = from x in db.Check_TechnicalContactList orderby x.CompileDate descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_TechnicalContactList> q = db.Check_TechnicalContactList;
                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                //if (!string.IsNullOrEmpty(userId))
                //{
                //    var roleList = BLL.CommonService.GetUserRoleEntrustedRole(userId);
                //    if (roleId != "admin" && !roleList.Contains(BLL.Const.CNPrincipalRole) && !roleList.Contains(BLL.Const.CQPrincipalRole)
                //        && !roleList.Contains(BLL.Const.INPrincipalRole))
                //    {
                //        string entrusteUserId = BLL.EntrustDetailService.GetEntrusteUserId(userId);
                //        if (!string.IsNullOrEmpty(entrusteUserId))
                //        {
                //            q = q.Where(e => e.CompileMan == userId || e.CompileMan == entrusteUserId);
                //        }
                //        else
                //        {
                //            q = q.Where(e => e.CompileMan == userId);
                //        }
                //    }
                //}
                count = q.Count();
                if (count == 0)
                {
                    return new object[] { "" };
                }

                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.TechnicalContactListId,
                           x.ProjectId,
                           x.Code,
                           ProposedUnit = (from y in db.Base_Unit where y.UnitId == x.ProposedUnitId select y.UnitName).First(),
                           MainSendUnit = (from y in db.Base_Unit where y.UnitId == x.MainSendUnitId select y.UnitName).First(),
                           UnitWorkName = UnitWorkService.GetUnitWorkName(x.UnitWorkId),
                           CNProfessional = CNProfessionalService.GetCNProfessionalNameByCode(x.CNProfessionalCode),
                           x.CCUnitIds,
                           ContactListType = x.ContactListType == "1" ? "图纸类" : "非图纸类",
                           IsReply = x.IsReply == "1" ? "需要回复" : "不需回复",
                           x.Cause,
                           x.AttachUrl,
                           CompileMan = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                           x.CompileDate,
                           x.State,
                       };
            }
        }
        /// <summary>
        /// 根据工程联络单信息Id获取一个工程联络单信息
        /// </summary>
        /// <param name="TechnicalContactListCode">工程联络单信息Id</param>
        /// <returns>一个工程联络单信息实体</returns>
        public static Model.Check_TechnicalContactList GetTechnicalContactListByTechnicalContactListId(string TechnicalContactListId)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_TechnicalContactList.FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId);
        }
        public static Model.Check_TechnicalContactList GetTechnicalContactListByTechnicalContactListIdForApi(string TechnicalContactListId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_TechnicalContactList res = db.Check_TechnicalContactList.FirstOrDefault(x => x.TechnicalContactListId == TechnicalContactListId);
                res.UnitWorkId = res.UnitWorkId + "$" + UnitWorkService.GetUnitWorkName(res.UnitWorkId);
                res.CNProfessionalCode = res.CNProfessionalCode + "$" + CNProfessionalService.GetCNProfessionalNameByCode(res.CNProfessionalCode);
                // Model.Base_Unit unit = BLL.UnitService.GetUnit(res.ProposedUnitId);
                var unit = db.Project_ProjectUnit.FirstOrDefault(e => e.ProjectId == res.ProjectId && e.UnitId == res.ProposedUnitId);
                if (unit != null)
                {
                    res.ProposedUnitId = res.ProposedUnitId + "$" + unit.Base_Unit.UnitName + "$" + unit.UnitType;
                }
                else
                {
                    res.ProposedUnitId = res.ProposedUnitId + "$$";

                }
                res.MainSendUnitId = res.MainSendUnitId + "$" + UnitService.getUnitNamesUnitIds(res.MainSendUnitId);
                res.CCUnitIds = res.CCUnitIds + "$" + UnitService.getUnitNamesUnitIds(res.CCUnitIds);
                res.AttachUrl = AttachFileService.getFileUrl(res.TechnicalContactListId);
                res.ReturnAttachUrl = AttachFileService.getFileUrl(res.TechnicalContactListId + "r");
                return res;
            }
        }
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertMan(object centerHandoverCode)
        {
            if (centerHandoverCode != null)
            {
                Model.Check_TechnicalContactListApprove a = TechnicalContactListApproveService.GetTechnicalContactListApproveByTechnicalContactListId(centerHandoverCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        public static string ConvertManAndId(object centerHandoverCode)
        {
            if (centerHandoverCode != null)
            {
                Model.Check_TechnicalContactListApprove a = TechnicalContactListApproveService.GetTechnicalContactListApproveByTechnicalContactListId(centerHandoverCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        var user = UserService.GetUserByUserId(a.ApproveMan);
                        return user.UserName + "$" + user.UserId;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == Const.TechnicalContactList_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == Const.TechnicalContactList_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit1)
                {
                    return "分包负责人审批";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit2)
                {
                    return "总包专工确认";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit3)
                {
                    return "总包负责人审批";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit4)
                {
                    return "总包设计人员回复";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit6)
                {
                    return "分包专工回复";
                }
                else if (state.ToString() == Const.TechnicalContactList_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit2R)
                {
                    return "总包专工重新回复";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit4R)
                {
                    return "总包设计人员重新回复";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit6R)
                {
                    return "分包专工重新回复";
                }
                else if (state.ToString() == Const.TechnicalContactList_Audit2H)
                {
                    return "总包专工回复";
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
        public static int getListCount(string projectId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_TechnicalContactList> q = db.Check_TechnicalContactList;
                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                return q.Count();
            }
        }
        public static List<Model.Check_TechnicalContactList> getListDataForApi(string name, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_TechnicalContactList> q = db.Check_TechnicalContactList;
                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    List<string> ids = new List<string>();
                    var qunit = from u in new Model.SGGLDB(Funs.ConnString).Base_Unit
                                where u.UnitName.Contains(name)
                                select u.UnitId;
                    ids = qunit.ToList();
                    q = q.Where(e => ids.Contains(e.ProposedUnitId));
                }

                var qres = from x in q
                           orderby x.Code descending
                           select new
                           {
                               x.TechnicalContactListId,
                               x.ProjectId,
                               x.Code,
                               x.ProposedUnitId,
                               x.CNProfessionalCode,
                               x.UnitWorkId,
                               x.MainSendUnitId,
                               x.CCUnitIds,
                               MainSendUnit1 = UnitService.getUnitNamesUnitIds(x.MainSendUnitId),
                               UnitWorkName = BLL.UnitWorkService.GetUnitWorkName(x.UnitWorkId),
                               CNProfessional = BLL.CNProfessionalService.GetCNProfessionalNameByCode(x.CNProfessionalCode),
                               CCUnitIdName = UnitService.getUnitNamesUnitIds(x.CCUnitIds),
                               x.Contents,
                               x.ContactListType,
                               x.IsReply,
                               x.Cause,
                               CompileMan = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                               x.CompileDate,
                               x.State,
                           };
                List<Model.Check_TechnicalContactList> res = new List<Model.Check_TechnicalContactList>();

                var list = qres.Skip(startRowIndex).Take(maximumRows).ToList();
                foreach (var item in list)
                {
                    Model.Check_TechnicalContactList tc = new Model.Check_TechnicalContactList();
                    tc.TechnicalContactListId = item.TechnicalContactListId;
                    tc.ProjectId = item.ProjectId;
                    tc.Code = item.Code;
                    var unit = db.Project_ProjectUnit.FirstOrDefault(u => u.ProjectId == tc.ProjectId && u.UnitId == item.ProposedUnitId);
                    if (unit != null)
                    {
                        tc.ProposedUnitId = item.ProposedUnitId + "$" + unit.Base_Unit.UnitName + "$" + unit.UnitType;
                    }
                    else
                    {
                        tc.ProposedUnitId = item.ProposedUnitId + "$$";

                    }
                    tc.MainSendUnitId = item.MainSendUnitId + "$" + item.MainSendUnit1;
                    tc.CCUnitIds = item.CCUnitIds + "$" + item.CCUnitIdName;
                    tc.Contents = item.Contents;
                    tc.CNProfessionalCode = item.CNProfessionalCode + "$" + item.CNProfessional;
                    tc.CCUnitIds = item.CCUnitIds + "$" + item.CCUnitIdName;
                    tc.ContactListType = item.ContactListType;
                    tc.IsReply = item.IsReply;
                    tc.Cause = item.Cause;
                    tc.AttachUrl = AttachFileService.getFileUrl(item.TechnicalContactListId);
                    tc.ReturnAttachUrl = AttachFileService.getFileUrl(item.TechnicalContactListId + "r");
                    tc.CompileMan = item.CompileMan + "$" + ConvertManAndId(item.TechnicalContactListId);
                    tc.CompileDate = item.CompileDate;
                    tc.State = item.State;
                    tc.UnitWorkId = item.UnitWorkId + "$" + item.UnitWorkName;
                    res.Add(tc);
                }
                return res;
            }
        }
        public static List<Model.Check_TechnicalContactList> getListDataForApi(string state, string contactListType, string isReply, string dateA, string dateZ, string proposedUnitId, string unitWorkId, string mainSendUnit, string cCUnitIds, string professional, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_TechnicalContactList> q = db.Check_TechnicalContactList;
                if (!string.IsNullOrEmpty(state) && "undefined" != state)
                {
                    if ("8" == state)
                        q = q.Where(e => e.State == "8");
                    else
                    {
                        q = q.Where(e => e.State != "8");

                    }
                }
                if (!string.IsNullOrEmpty(contactListType) && "undefined" != contactListType)
                {
                    q = q.Where(e => e.ContactListType == contactListType);
                }
                if (!string.IsNullOrEmpty(isReply) && "undefined" != isReply)
                {
                    q = q.Where(e => e.IsReply == isReply);
                }

                if (!string.IsNullOrEmpty(dateA) && "undefined" != dateA)
                {
                    DateTime date = DateTime.ParseExact(dateA, "yyyy-MM-dd", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.CompileDate >= date);
                }
                if (!string.IsNullOrEmpty(dateZ) && "undefined" != dateZ)
                {
                    DateTime date = DateTime.ParseExact(dateZ + "23:59:59", "yyyy-MM-ddHH:mm:ss", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.CompileDate <= date);
                }

                if (!string.IsNullOrEmpty(projectId) && "undefined" != projectId)
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }

                if (!string.IsNullOrEmpty(proposedUnitId) && "undefined" != proposedUnitId)
                {
                    q = q.Where(e => proposedUnitId == e.ProposedUnitId);
                }
                if (!string.IsNullOrEmpty(unitWorkId) && "undefined" != unitWorkId)
                {
                    q = q.Where(e => e.UnitWorkId.Contains(unitWorkId));
                }
                if (!string.IsNullOrEmpty(mainSendUnit) && "undefined" != mainSendUnit)
                {
                    q = q.Where(e => e.MainSendUnitId.Contains(mainSendUnit));
                }
                if (!string.IsNullOrEmpty(cCUnitIds) && "undefined" != cCUnitIds)
                {
                    q = q.Where(e => e.CCUnitIds.Contains(cCUnitIds));
                }
                if (!string.IsNullOrEmpty(professional) && "undefined" != professional)
                {
                    q = q.Where(e => e.CNProfessionalCode.Contains(professional));
                }
                var qres = from x in q
                           orderby x.Code descending
                           select new
                           {
                               x.TechnicalContactListId,
                               x.ProjectId,
                               x.Code,
                               x.ProposedUnitId,
                               x.CNProfessionalCode,
                               x.UnitWorkId,
                               x.MainSendUnitId,
                               x.CCUnitIds,
                               MainSendUnit1 = UnitService.getUnitNamesUnitIds(x.MainSendUnitId),
                               UnitWorkName = BLL.UnitWorkService.GetUnitWorkName(x.UnitWorkId),
                               CNProfessional = BLL.CNProfessionalService.GetCNProfessionalNameByCode(x.CNProfessionalCode),
                               CCUnitIdName = UnitService.getUnitNamesUnitIds(x.CCUnitIds),
                               x.Contents,
                               x.ContactListType,
                               x.IsReply,
                               x.Cause,
                               x.AttachUrl,
                               CompileMan = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                               x.CompileDate,
                               x.State,
                           };
                List<Model.Check_TechnicalContactList> res = new List<Model.Check_TechnicalContactList>();

                var list = qres.Skip(startRowIndex).Take(maximumRows).ToList();
                foreach (var item in list)
                {
                    Model.Check_TechnicalContactList tc = new Model.Check_TechnicalContactList();
                    tc.TechnicalContactListId = item.TechnicalContactListId;
                    tc.ProjectId = item.ProjectId;
                    tc.Code = item.Code;

                    var unit = db.Project_ProjectUnit.FirstOrDefault(u => u.ProjectId == tc.ProjectId && u.UnitId == item.ProposedUnitId);
                    if (unit != null)
                    {
                        tc.ProposedUnitId = item.ProposedUnitId + "$" + unit.Base_Unit.UnitName + "$" + unit.UnitType;
                    }
                    else
                    {
                        tc.ProposedUnitId = item.ProposedUnitId + "$$";
                    }
                    tc.MainSendUnitId = item.MainSendUnitId + "$" + item.MainSendUnit1;
                    tc.CCUnitIds = item.CCUnitIds + "$" + item.CCUnitIdName;
                    tc.Contents = item.Contents;
                    tc.CNProfessionalCode = item.CNProfessionalCode + "$" + item.CNProfessional;
                    tc.CCUnitIds = item.CCUnitIds + "$" + item.CCUnitIdName;
                    tc.ContactListType = item.ContactListType;
                    tc.IsReply = item.IsReply;
                    tc.Cause = item.Cause;
                    tc.AttachUrl = item.AttachUrl;
                    tc.CompileMan = item.CompileMan + "$" + ConvertManAndId(item.TechnicalContactListId); ;
                    tc.CompileDate = item.CompileDate;
                    tc.State = item.State;
                    tc.UnitWorkId = item.UnitWorkId + "$" + item.UnitWorkName;
                    tc.AttachUrl = AttachFileService.getFileUrl(item.TechnicalContactListId);
                    tc.ReturnAttachUrl = AttachFileService.getFileUrl(item.TechnicalContactListId + "r");
                    res.Add(tc);
                }
                return res;
            }

        }
        public static void UpdateTechnicalContactListForApi(Model.Check_TechnicalContactList TechnicalContactList)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.Check_TechnicalContactList newTechnicalContactList = db.Check_TechnicalContactList.FirstOrDefault(e => e.TechnicalContactListId == TechnicalContactList.TechnicalContactListId);
                if (newTechnicalContactList != null)
                {
                    if (!string.IsNullOrEmpty(TechnicalContactList.Code))
                        newTechnicalContactList.Code = TechnicalContactList.Code;
                    if (!string.IsNullOrEmpty(TechnicalContactList.ProposedUnitId))
                        newTechnicalContactList.ProposedUnitId = TechnicalContactList.ProposedUnitId;
                    if (!string.IsNullOrEmpty(TechnicalContactList.MainSendUnitId))
                        newTechnicalContactList.MainSendUnitId = TechnicalContactList.MainSendUnitId;
                    if (!string.IsNullOrEmpty(TechnicalContactList.CCUnitIds))
                        newTechnicalContactList.CCUnitIds = TechnicalContactList.CCUnitIds;
                    if (!string.IsNullOrEmpty(TechnicalContactList.UnitWorkId))
                        newTechnicalContactList.UnitWorkId = TechnicalContactList.UnitWorkId;
                    if (!string.IsNullOrEmpty(TechnicalContactList.CNProfessionalCode))
                        newTechnicalContactList.CNProfessionalCode = TechnicalContactList.CNProfessionalCode;
                    if (!string.IsNullOrEmpty(TechnicalContactList.ContactListType))
                        newTechnicalContactList.ContactListType = TechnicalContactList.ContactListType;
                    if (!string.IsNullOrEmpty(TechnicalContactList.IsReply))
                        newTechnicalContactList.IsReply = TechnicalContactList.IsReply;
                    if (!string.IsNullOrEmpty(TechnicalContactList.Cause))
                        newTechnicalContactList.Cause = TechnicalContactList.Cause;
                    if (!string.IsNullOrEmpty(TechnicalContactList.Contents))
                        newTechnicalContactList.Contents = TechnicalContactList.Contents;
                    if (!string.IsNullOrEmpty(TechnicalContactList.AttachUrl))
                        newTechnicalContactList.AttachUrl = TechnicalContactList.AttachUrl;
                    if (!string.IsNullOrEmpty(TechnicalContactList.State))
                        newTechnicalContactList.State = TechnicalContactList.State;
                    if (!string.IsNullOrEmpty(TechnicalContactList.ReAttachUrl))
                        newTechnicalContactList.ReAttachUrl = TechnicalContactList.ReAttachUrl;
                    if (!string.IsNullOrEmpty(TechnicalContactList.ReturnAttachUrl))
                        newTechnicalContactList.ReturnAttachUrl = TechnicalContactList.ReturnAttachUrl;
                    if (!string.IsNullOrEmpty(TechnicalContactList.ReOpinion))
                        newTechnicalContactList.ReOpinion = TechnicalContactList.ReOpinion;
                    db.SubmitChanges();
                }
            }
        }
    }
}
