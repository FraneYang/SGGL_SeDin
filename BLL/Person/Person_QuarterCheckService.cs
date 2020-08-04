using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class Person_QuarterCheckService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="QuarterCheckId">人员Id</param>
        /// <returns>人员信息</returns>
        public static Model.Person_QuarterCheck GetPerson_QuarterCheckById(string QuarterCheckId)
        {
            return Funs.DB.Person_QuarterCheck.FirstOrDefault(e => e.QuarterCheckId == QuarterCheckId);
        }
        public static Model.Person_QuarterCheck GetQuarterCheckByDateTime(DateTime startTime, DateTime endTime)
        {
            return Funs.DB.Person_QuarterCheck.FirstOrDefault(e => e.StartTime == startTime && e.EndTime == endTime);
        }

        /// <summary>
        /// 增加人员总结信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddPerson_QuarterCheck(Model.Person_QuarterCheck check)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheck newcheck = new Model.Person_QuarterCheck
            {
                QuarterCheckId = check.QuarterCheckId,
                QuarterCheckName = check.QuarterCheckName,
                UserId = check.UserId,
                RoleId = check.RoleId,
                ProjectId = check.ProjectId,
                StartTime = check.StartTime,
                EndTime = check.EndTime,
                State = check.State,
                CheckType = check.CheckType
            };
            db.Person_QuarterCheck.InsertOnSubmit(newcheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员总结信息
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdatePerson_QuarterCheck(Model.Person_QuarterCheck total)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheck newTotal = db.Person_QuarterCheck.FirstOrDefault(e => e.QuarterCheckId == total.QuarterCheckId);
            if (newTotal != null)
            {
                newTotal.State = total.State;
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 根据人员Id删除
        /// </summary>
        /// <param name="PersonTotalId"></param>
        public static void DeleteQuarterCheck(string QuarterCheckId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheck check = db.Person_QuarterCheck.FirstOrDefault(e => e.QuarterCheckId == QuarterCheckId);
            if (check != null)
            {
                db.Person_QuarterCheck.DeleteOnSubmit(check);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetQuarterCheckList()
        {
            ListItem[] list = new ListItem[11];
            list[0] = new ListItem("施工经理工作任务书", "1");
            list[1] = new ListItem("安全经理工作任务书", "2");
            list[2] = new ListItem("质量经理工作任务书", "3");
            list[3] = new ListItem("试车经理工作任务书", "4");
            list[4] = new ListItem("施工专业工程师工作任务书", "5");
            list[5] = new ListItem("安全专业工程是工作任务书", "6");
            list[6] = new ListItem("质量专业工程师工作任务书", "7");
            list[7] = new ListItem("试车专业工程师工作任务书", "8");
            list[8] = new ListItem("员工综合管理工作任务书", "9");
            list[9] = new ListItem("员工合同管理工作任务书", "10");
            list[10] = new ListItem("员工安全质量工作任务书", "11");
            return list;
        }

        public static List<Model.Person_QuarterCheck> GetListDataForApi(string userId, int index, int page)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Person_QuarterCheck> q = db.Person_QuarterCheck;
                List<string> ids = new List<string>();
                if (!string.IsNullOrEmpty(userId))
                {
                    q = from x in q
                        join y in db.Person_QuarterCheckApprove
                        on x.QuarterCheckId equals y.QuarterCheckId
                        where y.UserId == userId && y.ApproveDate == null
                        select x;
                }

                var qq1 = from x in q
                          orderby x.UserId descending
                          select new
                          {
                              x.QuarterCheckId,
                              x.QuarterCheckName,
                              x.UserId,
                              x.ProjectId,
                              x.StartTime,
                              x.EndTime,
                              x.State,
                              x.CheckType,
                              x.RoleId,

                              UserName = (from y in db.Sys_User where y.UserId == x.UserId select y.UserName).First(),
                              ProjectName = (from y in db.Base_Project where y.ProjectId == x.ProjectId select y.ProjectName).First(),
                              RoleName = (from y in db.Sys_Role where y.RoleId == x.RoleId select y.RoleName).First()

                          };
                var list = qq1.Skip(index * page).Take(page).ToList();

                List<Model.Person_QuarterCheck> listRes = new List<Model.Person_QuarterCheck>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.Person_QuarterCheck x = new Model.Person_QuarterCheck();
                    x.QuarterCheckId = list[i].QuarterCheckId;
                    x.QuarterCheckName = list[i].QuarterCheckName;
                    x.UserId = list[i].UserId + "$" + list[i].UserName;
                    x.ProjectId = list[i].ProjectId+"$"+list[i].ProjectName;
                    x.StartTime = list[i].StartTime;
                    x.EndTime = list[i].EndTime;
                    x.State = list[i].State;
                    x.CheckType = list[i].CheckType;
                    x.RoleId = list[i].RoleId+"$"+list[i].RoleName;
                    listRes.Add(x);
                }
                return listRes;
            }
        }

        public static Model.Person_QuarterCheck GetPersonCheckForApi(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Person_QuarterCheck x = db.Person_QuarterCheck.FirstOrDefault(e => e.QuarterCheckId == id);
                x.QuarterCheckId = x.QuarterCheckId;
                x.QuarterCheckName = x.QuarterCheckName;
                x.UserId = x.UserId + "$" + BLL.UserService.GetUserNameByUserId(x.UserId);
                x.ProjectId = x.ProjectId + "$" + BLL.ProjectService.GetProjectNameByProjectId(x.ProjectId);
                x.StartTime = x.StartTime;
                x.EndTime = x.EndTime;
                x.State = x.State;
                x.CheckType = x.CheckType;
                string roleName = string.Empty;
                var role = BLL.RoleService.GetRoleByRoleId(x.RoleId);
                if (role != null)
                {
                    roleName = role.RoleName;
                }
                x.RoleId = x.RoleId + "$" + roleName;
                return x;
            }
        }
    }
}
