using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class WorkContactService
    {
        #region 把状态转换代号为文字形式
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.WorkContact_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Audit1)
                {
                    return "分包负责人审核";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Audit2)
                {
                    return "总包专工回复";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Audit3)
                {
                    return "总包负责人审核";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Audit1R)
                {
                    return "分包专工重新回复";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Audit2R)
                {
                    return "总包专工重新回复";
                }
                else if (state.ToString() == BLL.Const.WorkContact_Audit4)
                {
                    return "分包专工回复";
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
        #endregion
        /// <summary>
        /// 根据工作联系单信息Id删除一个工作联系单信息信息
        /// </summary>
        /// <param name="workContactId">工作联系单信息Id</param>
        public static void DeleteWorkContact(string workContactId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Unqualified_WorkContact workContact = db.Unqualified_WorkContact.First(e => e.WorkContactId == workContactId);

            db.Unqualified_WorkContact.DeleteOnSubmit(workContact);
            db.SubmitChanges();
        }
        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state, string unitType, string isReply)
        {
            ListItem[] lis = null;
            if (state == Const.WorkContact_Compile || state == Const.WorkContact_ReCompile)
            {
                if (unitType == Const.ProjectUnitType_2)   //分包商
                {
                    lis = new ListItem[1];
                    lis[0] = new ListItem("分包负责人审核", Const.WorkContact_Audit1);
                    //lis[1] = new ListItem("审核完结", Const.WorkContact_Complete);
                }
                else
                {
                    lis = new ListItem[1];
                    lis[0] = new ListItem("总包负责人审核", Const.WorkContact_Audit3);
                    //lis[1] = new ListItem("审核完结", Const.WorkContact_Complete);
                }
            }
            else if (state == Const.WorkContact_Audit1)
            {
                if (unitType == Const.ProjectUnitType_2)   //分包商
                {
                    if (isReply == "1")  //需要回复
                    {
                        lis = new ListItem[2];
                        lis[0] = new ListItem("总包专工回复", Const.WorkContact_Audit2);
                        lis[1] = new ListItem("重新编制", Const.WorkContact_ReCompile);
                    }
                    else   //不需回复
                    {
                        lis = new ListItem[2];
                        lis[0] = new ListItem("审批完成", Const.WorkContact_Complete);
                        lis[1] = new ListItem("重新编制", Const.WorkContact_ReCompile);
                    }
                }
                else  //总包
                {
                    if (isReply == "1")  //需要回复
                    {
                        lis = new ListItem[2];
                        lis[0] = new ListItem("审批完成", Const.WorkContact_Complete);
                        lis[1] = new ListItem("分包专工重新回复", Const.WorkContact_Audit1R);
                    }
                }
            }
            else if (state == Const.WorkContact_Audit2 || state == Const.WorkContact_Audit2R)
            {
                lis = new ListItem[2];
                lis[0] = new ListItem("总包负责人审核", Const.WorkContact_Audit3);
                lis[1] = new ListItem("重新编制", Const.WorkContact_ReCompile);
            }
            else if (state == Const.WorkContact_Audit3)
            {
                if (unitType == Const.ProjectUnitType_2)   //分包商
                {
                    lis = new ListItem[3];
                    lis[0] = new ListItem("审批完成", Const.WorkContact_Complete);
                    lis[1] = new ListItem("重新编制", Const.WorkContact_ReCompile);
                    lis[2] = new ListItem("总包专工重新回复", Const.WorkContact_Audit2R);
                }
                else    //总包
                {
                    if (isReply == "1")   //需要回复
                    {
                        lis = new ListItem[2];
                        lis[0] = new ListItem("分包专工回复", Const.WorkContact_Audit4);
                        lis[1] = new ListItem("重新编制", Const.WorkContact_ReCompile);
                    }
                    else
                    {
                        lis = new ListItem[2];
                        lis[0] = new ListItem("审批完成", Const.WorkContact_Complete);
                        lis[1] = new ListItem("重新编制", Const.WorkContact_ReCompile);
                    }
                }
            }
            else if (state == Const.WorkContact_Audit4 || state == Const.WorkContact_Audit1R)
            {
                if (isReply == "1")   //需要回复
                {
                    lis = new ListItem[1];
                    lis[0] = new ListItem("分包负责人审核", Const.WorkContact_Audit1);
                    //lis[1] = new ListItem("重新编制", Const.WorkContact_ReCompile);
                }
            }
            return lis;
        }
        public static void InitHandleType(FineUIPro.DropDownList dropName, bool isShowPlease, string state, string unitType, string isReply)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetDHandleTypeByState(state, unitType, isReply);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 根据工作联系单信息Id获取一个工作联系单信息信息
        /// </summary>
        /// <param name="workContactId">工作联系单信息Id</param>
        /// <returns>一个工作联系单信息实体</returns>
        public static Model.Unqualified_WorkContact GetWorkContactByWorkContactId(string workContactId)
        {
            return Funs.DB.Unqualified_WorkContact.FirstOrDefault(x => x.WorkContactId == workContactId);

        }
        public static Model.Unqualified_WorkContact GetWorkContactByWorkContactIdForApi(string workContactId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.Unqualified_WorkContact.FirstOrDefault(x => x.WorkContactId == workContactId);
                //Model.Base_Unit unit = db.Base_Unit.FirstOrDefault(e => e.UnitId == res.ProposedUnitId);
                var unit = db.Project_ProjectUnit.FirstOrDefault(e => e.UnitId == res.ProposedUnitId && e.ProjectId == res.ProjectId);
                if (unit != null)
                {
                    res.ProposedUnitId = res.ProposedUnitId + "$" + unit.Base_Unit.UnitName + "$" + unit.UnitType;
                }
                res.MainSendUnitIds = res.MainSendUnitIds + "$" + UnitService.getUnitNamesUnitIds(res.MainSendUnitIds);
                res.CCUnitIds = res.CCUnitIds + "$" + UnitService.getUnitNamesUnitIds(res.CCUnitIds);
                res.AttachUrl = AttachFileService.getFileUrl(res.WorkContactId);
                res.ReturnAttachUrl = AttachFileService.getFileUrl(res.WorkContactId + "r");
                return res;
            }
        }

        #region 获取办理人姓名
        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertMan(object WorkContactId)
        {
            if (WorkContactId != null)
            {
                Model.Unqualified_WorkContactApprove a = WorkContactApproveService.GetWorkContactApproveByWorkContactId(WorkContactId.ToString());
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
        public static string ConvertManAndId(object WorkContactId)
        {
            if (WorkContactId != null)
            {
                Model.Unqualified_WorkContactApprove a = WorkContactApproveService.GetWorkContactApproveByWorkContactId(WorkContactId.ToString());
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
        #endregion
        /// <summary>
        /// 增加工作联系单信息信息
        /// </summary>
        /// <param name="workContact">工作联系单信息实体</param>
        public static void AddWorkContact(Model.Unqualified_WorkContact workContact)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Unqualified_WorkContact newWorkContact = new Model.Unqualified_WorkContact();
            newWorkContact.WorkContactId = workContact.WorkContactId;
            newWorkContact.ProjectId = workContact.ProjectId;
            newWorkContact.Code = workContact.Code;
            newWorkContact.ProposedUnitId = workContact.ProposedUnitId;
            newWorkContact.MainSendUnitIds = workContact.MainSendUnitIds;
            newWorkContact.CCUnitIds = workContact.CCUnitIds;
            newWorkContact.Cause = workContact.Cause;
            newWorkContact.Contents = workContact.Contents;
            newWorkContact.IsReply = workContact.IsReply;
            newWorkContact.AttachUrl = workContact.AttachUrl;
            newWorkContact.CompileMan = workContact.CompileMan;
            newWorkContact.CompileDate = workContact.CompileDate;
            newWorkContact.State = workContact.State;
            newWorkContact.IsFinal = workContact.IsFinal;
            newWorkContact.ReOpinion = workContact.ReOpinion;
            db.Unqualified_WorkContact.InsertOnSubmit(newWorkContact);
            db.SubmitChanges();
        }
        public static void AddWorkContactForApi(Model.Unqualified_WorkContact workContact)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Unqualified_WorkContact newWorkContact = new Model.Unqualified_WorkContact();
                newWorkContact.WorkContactId = workContact.WorkContactId;
                newWorkContact.ProjectId = workContact.ProjectId;
                newWorkContact.Code = workContact.Code;
                newWorkContact.ProposedUnitId = workContact.ProposedUnitId;
                newWorkContact.MainSendUnitIds = workContact.MainSendUnitIds;
                newWorkContact.CCUnitIds = workContact.CCUnitIds;
                newWorkContact.Cause = workContact.Cause;
                newWorkContact.Contents = workContact.Contents;
                newWorkContact.IsReply = workContact.IsReply;
                newWorkContact.AttachUrl = workContact.AttachUrl;
                newWorkContact.CompileMan = workContact.CompileMan;
                newWorkContact.CompileDate = workContact.CompileDate;
                newWorkContact.State = workContact.State;
                newWorkContact.IsFinal = workContact.IsFinal;
                newWorkContact.ReOpinion = workContact.ReOpinion;
                db.Unqualified_WorkContact.InsertOnSubmit(newWorkContact);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改工作联系单信息信息
        /// </summary>
        /// <param name="workContact">工作联系单信息实体</param>
        public static void UpdateWorkContact(Model.Unqualified_WorkContact workContact)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Unqualified_WorkContact newWorkContact = db.Unqualified_WorkContact.First(e => e.WorkContactId == workContact.WorkContactId);
            newWorkContact.Code = workContact.Code;
            newWorkContact.ProposedUnitId = workContact.ProposedUnitId;
            newWorkContact.MainSendUnitIds = workContact.MainSendUnitIds;
            newWorkContact.CCUnitIds = workContact.CCUnitIds;
            newWorkContact.Cause = workContact.Cause;
            newWorkContact.Contents = workContact.Contents;
            newWorkContact.IsReply = workContact.IsReply;
            newWorkContact.AttachUrl = workContact.AttachUrl;
            newWorkContact.State = workContact.State;
            newWorkContact.ReOpinion = workContact.ReOpinion;
            db.SubmitChanges();
        }
        public static List<Model.Unqualified_WorkContact> getListDataForApi(string projectId, string name, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Unqualified_WorkContact> q = db.Unqualified_WorkContact;
                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    List<string> ids = new List<string>();
                    var qunit = from u in Funs.DB.Base_Unit
                                where u.UnitName.Contains(name)
                                select u.UnitId;
                    ids = qunit.ToList();
                    q = q.Where(e => ids.Contains(e.ProposedUnitId));
                }

                var qres = from x in q
                           orderby x.Code descending
                           select new
                           {
                               x.WorkContactId,
                               x.Code,
                               x.ProjectId,
                               x.CompileMan,
                               x.CompileDate,
                               x.Cause,
                               x.ProposedUnitId,
                               x.State,
                               x.Contents,
                               MainSendUnitName = UnitService.getUnitNamesUnitIds(x.MainSendUnitIds),
                               CCUnitName = UnitService.getUnitNamesUnitIds(x.CCUnitIds),
                               x.IsReply
                           };

                List<Model.Unqualified_WorkContact> res = new List<Model.Unqualified_WorkContact>();
                var list = qres.Skip(startRowIndex * maximumRows).Take(maximumRows).ToList();
                foreach (var item in list)
                {
                    Model.Unqualified_WorkContact wc = new Model.Unqualified_WorkContact();
                    wc.WorkContactId = item.WorkContactId;
                    wc.Code = item.Code;
                    wc.ProjectId = item.ProjectId;
                    wc.CompileMan = item.CompileMan;
                    wc.CompileDate = item.CompileDate;
                    wc.Cause = item.Cause;
                    wc.State = item.State;
                    var unit = db.Project_ProjectUnit.FirstOrDefault(u => u.ProjectId == wc.ProjectId && u.UnitId == item.ProposedUnitId);
                    if (unit != null)
                    {
                        wc.ProposedUnitId = item.ProposedUnitId + "$" + unit.Base_Unit.UnitName + "$" + unit.UnitType;
                    }
                    else
                    {
                        wc.ProposedUnitId = item.ProposedUnitId + "$$";
                    }
                    wc.MainSendUnitIds = item.MainSendUnitName;
                    wc.CCUnitIds = item.CCUnitName;
                    wc.CompileMan = ConvertManAndId(item.WorkContactId);
                    wc.IsReply = item.IsReply;
                    wc.Contents = item.Contents;
                    wc.AttachUrl = AttachFileService.getFileUrl(item.WorkContactId);
                    wc.ReturnAttachUrl = AttachFileService.getFileUrl(item.WorkContactId + "r");
                    res.Add(wc);

                }
                return res;
            }
        }


        public static List<Model.Unqualified_WorkContact> getListDataForApi(string code, string proposedUnitId, string mainSendUnitId, string cCUnitId, string cause, string contents, string dateA, string dateZ, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Unqualified_WorkContact> q = db.Unqualified_WorkContact;
                if (!string.IsNullOrEmpty(code) && "undefined" != code)
                {
                    q = q.Where(e => e.Code.Contains(code));
                }
                if (!string.IsNullOrEmpty(proposedUnitId) && "undefined" != proposedUnitId)
                {
                    q = q.Where(e => e.ProposedUnitId.Contains(proposedUnitId));
                }
                if (!string.IsNullOrEmpty(mainSendUnitId) && "undefined" != mainSendUnitId)
                {
                    q = q.Where(e => e.MainSendUnitIds.Contains(mainSendUnitId));
                }
                if (!string.IsNullOrEmpty(cCUnitId) && "undefined" != cCUnitId)
                {
                    q = q.Where(e => e.CCUnitIds.Contains(cCUnitId));
                }
                if (!string.IsNullOrEmpty(cause) && "undefined" != cause)
                {
                    q = q.Where(e => e.Cause.Contains(cause));
                }

                if (!string.IsNullOrEmpty(contents) && "undefined" != contents)
                {
                    q = q.Where(e => e.Contents.Contains(contents));
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




                var qres = from x in q
                           orderby x.Code descending
                           select new
                           {
                               x.WorkContactId,
                               x.Code,
                               x.ProjectId,
                               x.CompileMan,
                               x.CompileDate,
                               x.Cause,
                               x.ProposedUnitId,
                               x.State,
                               x.Contents,
                               x.AttachUrl,
                               MainSendUnitName = UnitService.getUnitNamesUnitIds(x.MainSendUnitIds),
                               CCUnitName = UnitService.getUnitNamesUnitIds(x.CCUnitIds),
                               x.IsReply
                           };

                List<Model.Unqualified_WorkContact> res = new List<Model.Unqualified_WorkContact>();
                var list = qres.Skip(startRowIndex * maximumRows).Take(maximumRows).ToList();
                foreach (var item in list)
                {
                    Model.Unqualified_WorkContact wc = new Model.Unqualified_WorkContact();
                    wc.WorkContactId = item.WorkContactId;
                    wc.Code = item.Code;
                    wc.ProjectId = item.ProjectId;
                    wc.CompileMan = item.CompileMan;
                    wc.CompileDate = item.CompileDate;
                    wc.Cause = item.Cause;
                    wc.State = item.State;
                    var unit = db.Project_ProjectUnit.FirstOrDefault(u => u.ProjectId == wc.ProjectId && u.UnitId == item.ProposedUnitId);
                    if (unit != null)
                    {
                        wc.ProposedUnitId = item.ProposedUnitId + "$" + unit.Base_Unit.UnitName + "$" + unit.UnitType;
                    }
                    else
                    {
                        wc.ProposedUnitId = item.ProposedUnitId + "$$";
                    }
                    wc.MainSendUnitIds = item.MainSendUnitName;
                    wc.CCUnitIds = item.CCUnitName;
                    wc.CompileMan = ConvertManAndId(item.WorkContactId);
                    wc.IsReply = item.IsReply;
                    wc.Contents = item.Contents;
                    wc.AttachUrl = item.AttachUrl;
                    wc.IsReply = item.IsReply;
                    wc.AttachUrl = AttachFileService.getFileUrl(item.WorkContactId);
                    wc.ReturnAttachUrl = AttachFileService.getFileUrl(item.WorkContactId + "r");
                    res.Add(wc);

                }
                return res;
            }
        }
        public static void UpdateWorkContactForApi(Model.Unqualified_WorkContact workContact)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Unqualified_WorkContact newWorkContact = db.Unqualified_WorkContact.FirstOrDefault(e => e.WorkContactId == workContact.WorkContactId);
                if (newWorkContact != null)
                {
                    if (!string.IsNullOrEmpty(workContact.Code))
                        newWorkContact.Code = workContact.Code;
                    if (!string.IsNullOrEmpty(workContact.ProposedUnitId))
                        newWorkContact.ProposedUnitId = workContact.ProposedUnitId;
                    if (!string.IsNullOrEmpty(workContact.MainSendUnitIds))
                        newWorkContact.MainSendUnitIds = workContact.MainSendUnitIds;
                    if (!string.IsNullOrEmpty(workContact.CCUnitIds))
                        newWorkContact.CCUnitIds = workContact.CCUnitIds;
                    if (!string.IsNullOrEmpty(workContact.Cause))
                        newWorkContact.Cause = workContact.Cause;
                    if (!string.IsNullOrEmpty(workContact.Contents))
                        newWorkContact.Contents = workContact.Contents;
                    if (!string.IsNullOrEmpty(workContact.IsReply))
                        newWorkContact.IsReply = workContact.IsReply;
                    if (!string.IsNullOrEmpty(workContact.AttachUrl))
                        newWorkContact.AttachUrl = workContact.AttachUrl;
                    if (workContact.CompileDate.HasValue)
                        newWorkContact.CompileDate = workContact.CompileDate;
                    if (!string.IsNullOrEmpty(workContact.State))
                        newWorkContact.State = workContact.State;
                    if (!string.IsNullOrEmpty(workContact.ReOpinion))
                        newWorkContact.ReOpinion = workContact.ReOpinion;
                    db.SubmitChanges();
                }
            }
        }
        public static int getListCount(string projectId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Unqualified_WorkContact> q = db.Unqualified_WorkContact;
                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                return q.Count();
            }
        }
    }
}
