using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class JointCheckService
    {
        /// <summary>
        /// 下拉框选择(根据text获取value)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetValByText(string text)
        {
            string str = null;
            var listemItem = GetCheckTypeList();
            foreach (var item in listemItem)
            {
                if (text.Equals(item.Key))
                {
                    str = item.Value;
                }
            }
            return str;
        }
        /// <summary>
        /// 根据质量共检Id删除一个质量共检信息
        /// </summary>
        /// <param name="JointCheckId"></param>
        public static void DeleteJointCheck(string JointCheckId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_JointCheck JointCheck = db.Check_JointCheck.First(e => e.JointCheckId == JointCheckId);
            db.Check_JointCheck.DeleteOnSubmit(JointCheck);
            db.SubmitChanges();
        }
        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state)
        {
            if (state == Const.JointCheck_Compile || state == Const.JointCheck_ReCompile)
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("分包专工回复", Const.JointCheck_Audit1);
                return lis;
            }
            else if (state == Const.JointCheck_Audit1 || state == Const.JointCheck_Audit1R)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("分包负责人审批", Const.JointCheck_Audit2);
                lis[1] = new ListItem("总包专工回复", Const.JointCheck_Audit3);
                return lis;
            }
            else if (state == Const.JointCheck_Audit2)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("总包专工回复", Const.JointCheck_Audit3);
                lis[1] = new ListItem("分包专工重新回复", Const.JointCheck_Audit1R);
                return lis;
            }
            else if (state == Const.JointCheck_Audit3)
            {
                ListItem[] lis = new ListItem[3];
                lis[0] = new ListItem("总包负责人审批", Const.JointCheck_Audit4);
                lis[1] = new ListItem("审批完成", Const.JointCheck_Complete);
                lis[2] = new ListItem("分包专工重新回复", Const.JointCheck_Audit1R);
                return lis;
            }
            else if (state == Const.JointCheck_Audit4 || state == Const.JointCheck_Complete)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.JointCheck_Complete);
                lis[1] = new ListItem("分包专工重新回复", Const.JointCheck_Audit1R);
                return lis;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加质量共检
        /// </summary>
        /// <param name="JointCheck"></param>
        public static void AddJointCheck(Model.Check_JointCheck JointCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_JointCheck newJointCheck = new Model.Check_JointCheck();
            newJointCheck.JointCheckId = JointCheck.JointCheckId;
            newJointCheck.JointCheckCode = JointCheck.JointCheckCode;
            newJointCheck.ProjectId = JointCheck.ProjectId;
            newJointCheck.CheckType = JointCheck.CheckType;
            newJointCheck.CheckName = JointCheck.CheckName;
            newJointCheck.ProposeUnitId = JointCheck.ProposeUnitId;
            newJointCheck.UnitId = JointCheck.UnitId;
            newJointCheck.CheckDate = JointCheck.CheckDate;
            newJointCheck.CheckMan = JointCheck.CheckMan;
            newJointCheck.State = JointCheck.State;
            newJointCheck.JointCheckMans1 = JointCheck.JointCheckMans1;
            newJointCheck.JointCheckMans2 = JointCheck.JointCheckMans2;
            newJointCheck.JointCheckMans3 = JointCheck.JointCheckMans3;
            newJointCheck.JointCheckMans4 = JointCheck.JointCheckMans4;

            db.Check_JointCheck.InsertOnSubmit(newJointCheck);
            db.SubmitChanges();
        }
        public static void AddJointCheckForApi(Model.Check_JointCheck JointCheck)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_JointCheck newJointCheck = new Model.Check_JointCheck();
                newJointCheck.JointCheckId = JointCheck.JointCheckId;
                newJointCheck.JointCheckCode = JointCheck.JointCheckCode;
                newJointCheck.ProjectId = JointCheck.ProjectId;
                newJointCheck.CheckType = JointCheck.CheckType;
                newJointCheck.CheckName = JointCheck.CheckName;
                if (!string.IsNullOrEmpty(JointCheck.ProposeUnitId))
                {
                    newJointCheck.ProposeUnitId = JointCheck.ProposeUnitId;
                }
                if (!string.IsNullOrEmpty(JointCheck.UnitId))
                {
                    newJointCheck.UnitId = JointCheck.UnitId;
                }
                newJointCheck.CheckDate = JointCheck.CheckDate;
                newJointCheck.CheckMan = JointCheck.CheckMan;
                newJointCheck.State = JointCheck.State;
                newJointCheck.JointCheckMans1 = JointCheck.JointCheckMans1;
                newJointCheck.JointCheckMans2 = JointCheck.JointCheckMans2;
                newJointCheck.JointCheckMans3 = JointCheck.JointCheckMans3;
                newJointCheck.JointCheckMans4 = JointCheck.JointCheckMans4;

                db.Check_JointCheck.InsertOnSubmit(newJointCheck);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改质量共检
        /// </summary>
        /// <param name="JointCheck"></param>
        public static void UpdateJointCheck(Model.Check_JointCheck JointCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_JointCheck newJointCheck = db.Check_JointCheck.First(e => e.JointCheckId == JointCheck.JointCheckId);
            newJointCheck.JointCheckCode = JointCheck.JointCheckCode;
            newJointCheck.ProjectId = JointCheck.ProjectId;
            newJointCheck.CheckType = JointCheck.CheckType;
            newJointCheck.CheckName = JointCheck.CheckName;
            newJointCheck.ProposeUnitId = JointCheck.ProposeUnitId;
            newJointCheck.UnitId = JointCheck.UnitId;
            newJointCheck.CheckDate = JointCheck.CheckDate;
            newJointCheck.State = JointCheck.State;
            newJointCheck.JointCheckMans1 = JointCheck.JointCheckMans1;
            newJointCheck.JointCheckMans2 = JointCheck.JointCheckMans2;
            newJointCheck.JointCheckMans3 = JointCheck.JointCheckMans3;
            newJointCheck.JointCheckMans4 = JointCheck.JointCheckMans4;

            db.SubmitChanges();
        }

        /// <summary>
        /// 获取检查类别项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static Dictionary<int, string> GetCheckTypeList()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "周检查");
            dic.Add(2, "月检查");
            dic.Add(3, "不定期检查");
            dic.Add(4, "专业检查");
            return dic;
        }
        /// <summary>
        /// 获取审批状态项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetStateList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(BLL.Const.JointCheck_Compile, "编制");
            dic.Add(BLL.Const.JointCheck_Z, "整改中");
            dic.Add(BLL.Const.JointCheck_Complete, "审批完成");
            return dic;
        }
        /// <summary>
        ///  检查类别表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void Init(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataTextField = "Value";
            dropName.DataValueField = "Key";

            dropName.DataSource = GetCheckTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        ///  审批状态表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitState(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataTextField = "Value";
            dropName.DataValueField = "Key";

            dropName.DataSource = GetStateList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 根据质量共检Id获取一个质量共检信息
        /// </summary>
        /// <param name="JointCheckDetailId"></param>
        public static Model.Check_JointCheck GetJointCheck(string JointCheckId)
        {
            return Funs.DB.Check_JointCheck.FirstOrDefault(e => e.JointCheckId == JointCheckId);
        }
        public static Model.Check_JointCheck GetJointCheckForApi(string JointCheckId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.Check_JointCheck.FirstOrDefault(e => e.JointCheckId == JointCheckId);
                res.UnitId = res.UnitId + "$" + UnitService.getUnitNamesUnitIds(res.UnitId);
                res.ProposeUnitId = res.ProposeUnitId + "$" + UnitService.getUnitNamesUnitIds(res.ProposeUnitId);
                var user = UserService.GetUserByUserId(res.CheckMan);
                res.CheckMan = res.CheckMan + "$" + (user == null ? "" : user.UserName);
                res.JointCheckMans1= res.JointCheckMans1 + "$" + BLL.UserService.getUserNamesUserIds(res.JointCheckMans1);
                res.JointCheckMans2 = res.JointCheckMans2 + "$" + BLL.UserService.getUserNamesUserIds(res.JointCheckMans2);
                res.JointCheckMans3 = res.JointCheckMans3 + "$" + BLL.UserService.getUserNamesUserIds(res.JointCheckMans3);
                res.JointCheckMans4 = res.JointCheckMans4 + "$" + BLL.UserService.getUserNamesUserIds(res.JointCheckMans4);
                return res;
            }
        }

        /// <summary>
        /// 获取检查类别项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetCheckTypeList2()
        {
            ListItem[] lis = new ListItem[6];
            lis[0] = new ListItem("-请选择-", "");
            lis[1] = new ListItem("周检查", "1");
            lis[2] = new ListItem("月检查", "2");
            lis[3] = new ListItem("不定期检查", "3");
            lis[4] = new ListItem("专业检查", "4");
            lis[5] = new ListItem("质量巡检", "5");
            return lis;
        }
        public static int GetListCount(string projectId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {

                IQueryable<Model.Check_JointCheck> q = db.Check_JointCheck;

                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                return q.Count();
            }
        }
        public static List<Model.Check_JointCheck> GetListDataForApi(string name, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_JointCheck> q = db.Check_JointCheck;
                if (!string.IsNullOrEmpty(name))
                {
                    List<string> ids = new List<string>();
                    var qunit = from u in Funs.DB.Base_Unit
                                where u.UnitName.Contains(name)
                                select u.UnitId;
                    ids = qunit.ToList();
                    q = q.Where(e => ids.Contains(e.UnitId));
                }


                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }

                var qres = from x in q
                           orderby x.JointCheckCode descending
                           select new
                           {
                               x.JointCheckId,
                               x.JointCheckCode,
                               x.UnitId,
                               x.ProposeUnitId,
                               x.CheckDate,
                               x.CheckMan,
                               x.CheckType,
                               x.CheckName,
                               x.State,
                               x.JointCheckMans1,
                               x.JointCheckMans2,
                               x.JointCheckMans3,
                               x.JointCheckMans4,
                               JointCheckMans1_Name = BLL.UserService.getUserNamesUserIds(x.JointCheckMans1),
                               JointCheckMans2_Name = BLL.UserService.getUserNamesUserIds(x.JointCheckMans2),
                               JointCheckMans3_Name = BLL.UserService.getUserNamesUserIds(x.JointCheckMans3),
                               JointCheckMans4_Name = BLL.UserService.getUserNamesUserIds(x.JointCheckMans4),
                               CheckManName = (from y in db.Sys_User where y.UserId == x.CheckMan select y.UserName).First(),
                               UnitName = UnitService.getUnitNamesUnitIds(x.UnitId),
                               ProposeUnitName = UnitService.getUnitNamesUnitIds(x.ProposeUnitId)
                           };
                List<Model.Check_JointCheck> res = new List<Model.Check_JointCheck>();
                var list = qres.Skip(startRowIndex* maximumRows).Take(maximumRows).ToList();
                foreach (var item in list)
                {
                    Model.Check_JointCheck jc = new Model.Check_JointCheck();
                    jc.JointCheckId = item.JointCheckId;
                    jc.JointCheckCode = item.JointCheckCode;
                    jc.UnitId = item.UnitId + "$" + item.UnitName;
                    jc.CheckDate = item.CheckDate;
                    jc.CheckType = item.CheckType;
                    jc.CheckName = item.CheckName;
                    jc.State = item.State;
                    jc.JointCheckMans1 = item.JointCheckMans1 + "$" + item.JointCheckMans1_Name;
                    jc.JointCheckMans2 = item.JointCheckMans2 + "$" + item.JointCheckMans2_Name;
                    jc.JointCheckMans3 = item.JointCheckMans3 + "$" + item.JointCheckMans3_Name;
                    jc.JointCheckMans4 = item.JointCheckMans4 + "$" + item.JointCheckMans4_Name;
                    jc.CheckMan = item.CheckMan + "$" + item.CheckManName + "$" + ConvertManAndID(jc.JointCheckId);
                    jc.ProposeUnitId = item.ProposeUnitId + "$" + item.ProposeUnitName;

                    res.Add(jc);
                }
                return res;
            }
        }
        public static List<Model.Check_JointCheck> GetListDataForApi(string name, string code, string unitId, string proposeUnitId, string type, string dateA, string dateZ, string projectId, string state, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_JointCheck> q = db.Check_JointCheck;
                if (!string.IsNullOrEmpty(name) && "undefined" != name)
                {
                    q = q.Where(e => e.CheckName.Contains(name));
                }
                if (!string.IsNullOrEmpty(code) && "undefined" != code)
                {

                    q = q.Where(e => e.JointCheckCode.Contains(code));
                }
                if (!string.IsNullOrEmpty(unitId) && "undefined" != unitId)
                {
                    q = q.Where(e => e.UnitId == unitId);
                }
                if (!string.IsNullOrEmpty(proposeUnitId) && "undefined" != proposeUnitId)
                {
                    q = q.Where(e => e.ProposeUnitId == proposeUnitId);
                }
                if (!string.IsNullOrEmpty(type) && "undefined" != type)
                {
                    q = q.Where(e => e.CheckType == type);
                }
                if (!string.IsNullOrEmpty(dateA) && "undefined" != dateA)
                {
                    DateTime date = DateTime.ParseExact(dateA, "yyyy-MM-dd", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.CheckDate >= date);
                }
                if (!string.IsNullOrEmpty(dateZ) && "undefined" != dateZ)
                {
                    DateTime date = DateTime.ParseExact(dateZ + "23:59:59", "yyyy-MM-ddHH:mm:ss", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.CheckDate <= date);
                }
                if (!string.IsNullOrEmpty(projectId) && "undefined" != projectId)
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                if (!string.IsNullOrEmpty(state) && "undefined" != state)
                {
                    if ("Z" == state)
                    {
                        List<string> states = new List<string>();
                        states.Add("2");
                        states.Add("3");
                        states.Add("4");
                        states.Add("5");
                        states.Add("7");
                        states.Add("Z");
                        q = q.Where(e => states.Contains(e.State));
                    }
                    else
                    {
                        q = q.Where(e => e.State == state);
                    }
                }
                var qres = from x in q
                           orderby x.JointCheckCode descending
                           select new
                           {
                               x.JointCheckId,
                               x.JointCheckCode,
                               x.UnitId,
                               x.ProposeUnitId,
                               x.CheckDate,
                               x.CheckType,
                               x.CheckName,
                               x.CheckMan,
                               x.State,
                               CheckManName = (from y in db.Sys_User where y.UserId == x.CheckMan select y.UserName).First(),
                               UnitName = UnitService.getUnitNamesUnitIds(x.UnitId),
                               ProposeUnitName = UnitService.getUnitNamesUnitIds(x.ProposeUnitId)
                           };
                List<Model.Check_JointCheck> res = new List<Model.Check_JointCheck>();
                var list = qres.Skip(startRowIndex* maximumRows).Take(maximumRows).ToList();
                foreach (var item in list)
                {
                    Model.Check_JointCheck jc = new Model.Check_JointCheck();
                    jc.JointCheckId = item.JointCheckId;
                    jc.JointCheckCode = item.JointCheckCode;
                    jc.UnitId = item.UnitId + "$" + item.UnitName;
                    jc.CheckDate = item.CheckDate;
                    jc.CheckType = item.CheckType;
                    jc.CheckName = item.CheckName;
                    jc.State = item.State;
                    jc.CheckMan = item.CheckMan + "$" + item.CheckManName + "$" + ConvertManAndID(jc.JointCheckId);
                    jc.ProposeUnitId = item.ProposeUnitId + "$" + item.ProposeUnitName;

                    res.Add(jc);
                }
                return res;

            }
        }
        public static string ConvertManAndID(string id)
        {
            if (id != null)
            {

                List<Model.Check_JointCheckApprove> apporves = BLL.JointCheckApproveService.getCurrApproveForApi(id);
                if (apporves != null)
                {
                    string names = "";
                    string ids = "";
                    foreach (var item in apporves)
                    {
                        names += item.ApproveIdea + ",";
                        ids += item.ApproveMan + ",";
                    }
                    if (!string.IsNullOrEmpty(names))
                    {
                        names = names.TrimEnd(',');
                        ids = ids.TrimEnd(',');
                    }
                    return names + "$" + ids;
                }

                //Model.Check_JointCheckApprove a = BLL.JointCheckApproveService.getCurrApproveByJoinCheckIdForApi(id);
                //if (a != null)
                //{
                //    if (a.ApproveMan != null)
                //    {
                //        var user = BLL.UserService.GetUserName(a.ApproveMan);
                //        return user.UserName + "$" + user.UserId;
                //    }
                //}
                //else
                //{
                //    return "";
                //}
            }
            return "";
        }
        public static void UpdateJointCheckForApi(Model.Check_JointCheck JointCheck)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_JointCheck newJointCheck = db.Check_JointCheck.First(e => e.JointCheckId == JointCheck.JointCheckId);
                if (!string.IsNullOrEmpty(JointCheck.JointCheckCode))
                    newJointCheck.JointCheckCode = JointCheck.JointCheckCode;
                if (!string.IsNullOrEmpty(JointCheck.ProjectId))
                    newJointCheck.ProjectId = JointCheck.ProjectId;
                if (!string.IsNullOrEmpty(JointCheck.CheckType))
                    newJointCheck.CheckType = JointCheck.CheckType;
                if (!string.IsNullOrEmpty(JointCheck.CheckName))
                    newJointCheck.CheckName = JointCheck.CheckName;
                if (!string.IsNullOrEmpty(JointCheck.UnitId))
                    newJointCheck.UnitId = JointCheck.UnitId;
                if (!string.IsNullOrEmpty(JointCheck.ProposeUnitId))
                    newJointCheck.ProposeUnitId = JointCheck.ProposeUnitId;
                if (JointCheck.CheckDate.HasValue)
                    newJointCheck.CheckDate = JointCheck.CheckDate;
                if (!string.IsNullOrEmpty(JointCheck.State))
                    newJointCheck.State = JointCheck.State;
                if (!string.IsNullOrEmpty(JointCheck.JointCheckMans1))
                    newJointCheck.JointCheckMans1 = JointCheck.JointCheckMans1;
                if (!string.IsNullOrEmpty(JointCheck.JointCheckMans2))
                    newJointCheck.JointCheckMans2 = JointCheck.JointCheckMans2;
                if (!string.IsNullOrEmpty(JointCheck.JointCheckMans3))
                    newJointCheck.JointCheckMans3 = JointCheck.JointCheckMans3;
                if (!string.IsNullOrEmpty(JointCheck.JointCheckMans4))
                    newJointCheck.JointCheckMans4 = JointCheck.JointCheckMans4;

                db.SubmitChanges();
            }
        }
    }
}
