using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.Linq;
using System.Globalization;

namespace BLL
{
    public class SpotCheckService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 添加实体验收记录
        /// </summary>
        /// <param name="SpotCheck"></param>
        public static void AddSpotCheck(Model.Check_SpotCheck SpotCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_SpotCheck newSpotCheck = new Model.Check_SpotCheck();
            newSpotCheck.SpotCheckCode = SpotCheck.SpotCheckCode;
            newSpotCheck.DocCode = SpotCheck.DocCode;
            newSpotCheck.ProjectId = SpotCheck.ProjectId;
            newSpotCheck.UnitId = SpotCheck.UnitId;
            newSpotCheck.CheckDateType = SpotCheck.CheckDateType;
            newSpotCheck.SpotCheckDate = SpotCheck.SpotCheckDate;
            newSpotCheck.SpotCheckDate2 = SpotCheck.SpotCheckDate2;
            newSpotCheck.CheckArea = SpotCheck.CheckArea;
            newSpotCheck.CreateMan = SpotCheck.CreateMan;
            newSpotCheck.CreateDate = SpotCheck.CreateDate;
            newSpotCheck.JointCheckMans = SpotCheck.JointCheckMans;
            newSpotCheck.JointCheckMans2 = SpotCheck.JointCheckMans2;
            newSpotCheck.JointCheckMans3 = SpotCheck.JointCheckMans3;
            newSpotCheck.CNProfessionalCode = SpotCheck.CNProfessionalCode;
            newSpotCheck.AttachUrl = SpotCheck.AttachUrl;
            newSpotCheck.State = SpotCheck.State;
            newSpotCheck.ControlPointType = SpotCheck.ControlPointType;
            newSpotCheck.State2 = SpotCheck.State2;

            db.Check_SpotCheck.InsertOnSubmit(newSpotCheck);
            db.SubmitChanges();
        }
        public static void AddSpotCheckForApi(Model.Check_SpotCheck SpotCheck)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheck newSpotCheck = new Model.Check_SpotCheck();
                newSpotCheck.SpotCheckCode = SpotCheck.SpotCheckCode;
                newSpotCheck.DocCode = SpotCheck.DocCode;
                newSpotCheck.ProjectId = SpotCheck.ProjectId;
                newSpotCheck.UnitId = SpotCheck.UnitId;
                newSpotCheck.CheckDateType = SpotCheck.CheckDateType;
                newSpotCheck.SpotCheckDate = SpotCheck.SpotCheckDate;
                newSpotCheck.SpotCheckDate2 = SpotCheck.SpotCheckDate2;
                newSpotCheck.CheckArea = SpotCheck.CheckArea;
                newSpotCheck.CreateMan = SpotCheck.CreateMan;
                newSpotCheck.CreateDate = SpotCheck.CreateDate;
                newSpotCheck.JointCheckMans = SpotCheck.JointCheckMans;
                newSpotCheck.JointCheckMans2 = SpotCheck.JointCheckMans2;
                newSpotCheck.JointCheckMans3 = SpotCheck.JointCheckMans3;
                newSpotCheck.CNProfessionalCode = SpotCheck.CNProfessionalCode;
                newSpotCheck.AttachUrl = SpotCheck.AttachUrl;
                newSpotCheck.State = SpotCheck.State;
                newSpotCheck.ControlPointType = SpotCheck.ControlPointType;
                newSpotCheck.State2 = SpotCheck.State2;

                db.Check_SpotCheck.InsertOnSubmit(newSpotCheck);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改实体验收记录
        /// </summary>
        /// <param name="SpotCheck"></param>
        public static void UpdateSpotCheck(Model.Check_SpotCheck SpotCheck)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_SpotCheck newSpotCheck = db.Check_SpotCheck.First(e => e.SpotCheckCode == SpotCheck.SpotCheckCode);
            newSpotCheck.DocCode = SpotCheck.DocCode;
            newSpotCheck.UnitId = SpotCheck.UnitId;
            newSpotCheck.CheckDateType = SpotCheck.CheckDateType;
            newSpotCheck.SpotCheckDate = SpotCheck.SpotCheckDate;
            newSpotCheck.SpotCheckDate2 = SpotCheck.SpotCheckDate2;
            newSpotCheck.CheckArea = SpotCheck.CheckArea;
            newSpotCheck.IsOK = SpotCheck.IsOK;
            newSpotCheck.JointCheckMans = SpotCheck.JointCheckMans;
            newSpotCheck.JointCheckMans2 = SpotCheck.JointCheckMans2;
            newSpotCheck.JointCheckMans3 = SpotCheck.JointCheckMans3;
            newSpotCheck.CNProfessionalCode = SpotCheck.CNProfessionalCode;
            newSpotCheck.AttachUrl = SpotCheck.AttachUrl;
            newSpotCheck.State = SpotCheck.State;
            newSpotCheck.ControlPointType = SpotCheck.ControlPointType;
            newSpotCheck.State2 = SpotCheck.State2;
            newSpotCheck.IsShow = SpotCheck.IsShow;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据实体验收记录Id删除一个实体验收记录信息
        /// </summary>
        /// <param name="SpotCheckId"></param>
        public static void DeleteSpotCheck(string SpotCheckId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_SpotCheck SpotCheck = db.Check_SpotCheck.First(e => e.SpotCheckCode == SpotCheckId);
            db.Check_SpotCheck.DeleteOnSubmit(SpotCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据实体验收记录Id获取一个实体验收记录信息
        /// </summary>
        /// <param name="SpotCheckDetailId"></param>
        public static Model.Check_SpotCheck GetSpotCheckBySpotCheckCode(string SpotCheckCode)
        {
            return Funs.DB.Check_SpotCheck.FirstOrDefault(e => e.SpotCheckCode == SpotCheckCode);
        }
        public static Model.Check_SpotCheck GetSpotCheckBySpotCheckCodeForApi(string SpotCheckCode)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return db.Check_SpotCheck.FirstOrDefault(e => e.SpotCheckCode == SpotCheckCode);
            }
        }
        /// <summary>
        /// 根据用户Id获取一个实体验收记录信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        public static bool GetSpotCheckByUserId(string userId)
        {
            return (from x in Funs.DB.Check_SpotCheck where x.CreateMan == userId select x).Count() > 0;
        }

        /// <summary>
        /// 根据是否闭环获取实体验收记录集合信息
        /// </summary>
        /// <param name="SpotCheckDetailId"></param>
        public static List<Model.Check_SpotCheck> GetOKSpotChecks(string projectId)
        {
            return (from x in Funs.DB.Check_SpotCheck where x.ProjectId == projectId && x.IsOK == true select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取共检集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<Model.Check_SpotCheck> GetSpotCheckListByTime(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.Check_SpotCheck
                    where x.ProjectId == projectId && x.SpotCheckDate >= startTime && x.SpotCheckDate < endTime
                    select x).ToList();
        }

        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state, string controlPointType)
        {
            if (state == Const.SpotCheck_Compile || state == Const.SpotCheck_ReCompile)
            {
                if (controlPointType == "D")   //非C级
                {
                    ListItem[] lis = new ListItem[1];
                    lis[0] = new ListItem("总包专业工程师确认", Const.SpotCheck_Audit2);
                    return lis;
                }
                else    //C级
                {
                    ListItem[] lis = new ListItem[1];
                    lis[0] = new ListItem("分包负责人确认", Const.SpotCheck_Audit1);
                    return lis;
                }
            }
            else if (state == Const.SpotCheck_Audit1)
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("审批完成", Const.SpotCheck_Complete);
                return lis;
            }
            else if (state == Const.SpotCheck_Audit2)
            {
                ListItem[] lis = new ListItem[3];
                lis[0] = new ListItem("监理专业工程师确认", Const.SpotCheck_Audit3);
                lis[1] = new ListItem("建设单位确认", Const.SpotCheck_Audit4);
                lis[2] = new ListItem("重新编制", Const.SpotCheck_ReCompile);
                return lis;
            }
            else if (state == Const.SpotCheck_Audit3 || state == Const.SpotCheck_Audit4)
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("审批完成", Const.SpotCheck_Complete);
                return lis;
            }
            else if (state == Const.SpotCheck_Audit5 || state == Const.SpotCheck_Audit5R)
            {
                if (controlPointType == "D")   //非C级
                {
                    ListItem[] lis = new ListItem[1];
                    lis[0] = new ListItem("总包专业工程师确认", Const.SpotCheck_Audit6);
                    return lis;
                }
                else    //C级
                {
                    ListItem[] lis = new ListItem[1];
                    lis[0] = new ListItem("分包负责人确认", Const.SpotCheck_Audit7);
                    return lis;
                }
            }
            else if (state == Const.SpotCheck_Audit6 || state == Const.SpotCheck_Audit7)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.SpotCheck_Complete);
                lis[1] = new ListItem("分包专业工程师重新上传资料", Const.SpotCheck_Audit5R);
                return lis;
            }
            else
                return null;
        }

        public static void Init(FineUIPro.DropDownList dropName, string state, string controlPointType, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetDHandleTypeByState(state, controlPointType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
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
                if (state.ToString() == BLL.Const.SpotCheck_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit1)
                {
                    return "分包负责人确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit2)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit3)
                {
                    return "监理专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit4)
                {
                    return "建设单位确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit5)
                {
                    return "分包专业工程师上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit6)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit7)
                {
                    return "分包负责人确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit5R)
                {
                    return "分包专业工程师重新上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Complete)
                {
                    return "审批完成";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Z)
                {
                    return "资料验收中";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertMan(object SpotCheckCode)
        {
            if (SpotCheckCode != null)
            {
                Model.Check_SpotCheckApprove a = BLL.SpotCheckApproveService.GetSpotCheckApproveBySpotCheckCode(SpotCheckCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        public static string ConvertManAndId(object SpotCheckCode)
        {
            if (SpotCheckCode != null)
            {
                Model.Check_SpotCheckApprove a = BLL.SpotCheckApproveService.GetSpotCheckApproveBySpotCheckCode(SpotCheckCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        var user = BLL.UserService.GetUserByUserId(a.ApproveMan);
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
        public static List<Model.Check_SpotCheck> GetListDataForApi(string name, string unitId, string startTime, string endTime, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_SpotCheck> q = db.Check_SpotCheck;
                if (!string.IsNullOrEmpty(name) && "undefined" != name)
                {
                    var qunit = from u in db.Base_Unit where u.UnitName.Contains(name) select u.UnitId;
                    var ids = qunit.ToList();
                    q = q.Where(e => ids.Contains(e.UnitId) || e.DocCode.Contains(name));

                }
                if (!string.IsNullOrEmpty(unitId) && "undefined" != unitId)
                {
                    q = q.Where(e => e.UnitId == unitId);
                }
                if (!string.IsNullOrEmpty(startTime) && "undefined" != startTime)
                {
                    DateTime date = DateTime.ParseExact(startTime, "yyyy-MM-dd", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.SpotCheckDate >= date);
                }
                if (!string.IsNullOrEmpty(endTime) && "undefined" != endTime)
                {
                    DateTime date = DateTime.ParseExact(endTime + "23:59:59", "yyyy-MM-ddHH:mm:ss", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.SpotCheckDate <= date);
                }


                if (!string.IsNullOrEmpty(projectId) && "undefined" != projectId)
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }



                var qres = from x in q
                           orderby x.DocCode descending
                           select new
                           {
                               x.SpotCheckCode,
                               x.DocCode,
                               x.CheckDateType,
                               x.CNProfessionalCode,
                               x.UnitId,
                               x.SpotCheckDate,
                               x.ControlPointType,
                               x.CheckArea,
                               x.SpotCheckDate2,
                               x.State,
                               x.JointCheckMans,
                               x.JointCheckMans2,
                               x.JointCheckMans3,
                               x.CreateMan,
                               CreateManName = (from y in db.Sys_User where y.UserId == x.CreateMan select y.UserName).First(),
                               UnitName = (from y in db.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                               x.IsOK
                           };
                List<Model.Check_SpotCheck> res = new List<Model.Check_SpotCheck>();
                var list = qres.Skip(startRowIndex).Take(maximumRows).ToList();

                foreach (var item in list)
                {
                    Model.Check_SpotCheck jc = new Model.Check_SpotCheck();
                    jc.SpotCheckCode = item.SpotCheckCode;
                    jc.DocCode = item.DocCode;
                    jc.UnitId = item.UnitId + "$" + item.UnitName;
                    jc.SpotCheckDate = item.SpotCheckDate;
                    jc.CreateMan = item.CreateManName + "$" + ConvertManAndId(item.SpotCheckCode);
                    jc.State = item.State;
                    jc.ControlPointType = item.ControlPointType;
                    jc.SpotCheckDate2 = item.SpotCheckDate2;
                    jc.CheckArea = item.CheckArea;
                    jc.CNProfessionalCode = item.CNProfessionalCode + "$" + CNProfessionalService.GetCNProfessionalNameByCode(item.CNProfessionalCode);
                    jc.CheckDateType = item.CheckDateType;
                    jc.JointCheckMans = item.JointCheckMans + "$" + BLL.UserService.getUserNamesUserIds(item.JointCheckMans);
                    jc.JointCheckMans2 = item.JointCheckMans2 + "$" + BLL.UserService.getUserNamesUserIds(item.JointCheckMans2);
                    jc.JointCheckMans3 = item.JointCheckMans3 + "$" + BLL.UserService.getUserNamesUserIds(item.JointCheckMans3);
                    jc.AttachUrl = AttachFileService.getFileUrl(jc.SpotCheckCode);
                    res.Add(jc);
                }
                return res;

            }
        }
        public static Model.Check_SpotCheck GetSpotCheck(string SpotCheckCode)
        {
            return Funs.DB.Check_SpotCheck.FirstOrDefault(e => e.SpotCheckCode == SpotCheckCode);
        }
        public static Model.Check_SpotCheck GetSpotCheckForApi(string SpotCheckCode)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheck res = db.Check_SpotCheck.FirstOrDefault(e => e.SpotCheckCode == SpotCheckCode);
                res.JointCheckMans = res.JointCheckMans + "$" + BLL.UserService.getUserNamesUserIds(res.JointCheckMans);
                res.JointCheckMans2 = res.JointCheckMans2 + "$" + BLL.UserService.getUserNamesUserIds(res.JointCheckMans2);
                res.JointCheckMans3 = res.JointCheckMans3 + "$" + BLL.UserService.getUserNamesUserIds(res.JointCheckMans3);
                res.UnitId = res.UnitId + "$" + UnitService.getUnitNamesUnitIds(res.UnitId);
                res.CNProfessionalCode = res.CNProfessionalCode + "$" + CNProfessionalService.GetCNProfessionalNameByCode(res.CNProfessionalCode);
                res.AttachUrl = AttachFileService.getFileUrl(res.SpotCheckCode);
                return res;
            }
        }
        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="spotCheckCode"></param>
        /// <returns></returns>
        public static IList<Model.Check_SpotCheck> GetSpotChecks(List<string> spotCheckCode)
        {
            return Funs.DB.Check_SpotCheck.Where(p => spotCheckCode.Contains(p.SpotCheckCode)).ToList();
        }
        public static void UpdateSpotCheckForUpdateForApi(Model.Check_SpotCheck SpotCheck)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheck newSpotCheck = db.Check_SpotCheck.First(e => e.SpotCheckCode == SpotCheck.SpotCheckCode);
                if (!string.IsNullOrEmpty(SpotCheck.DocCode))
                    newSpotCheck.DocCode = SpotCheck.DocCode;
                if (!string.IsNullOrEmpty(SpotCheck.UnitId))
                    newSpotCheck.UnitId = SpotCheck.UnitId;
                if (!string.IsNullOrEmpty(SpotCheck.CheckDateType))
                    newSpotCheck.CheckDateType = SpotCheck.CheckDateType;
                if (SpotCheck.SpotCheckDate.HasValue)
                    newSpotCheck.SpotCheckDate = SpotCheck.SpotCheckDate;
                if (SpotCheck.SpotCheckDate2.HasValue)
                    newSpotCheck.SpotCheckDate2 = SpotCheck.SpotCheckDate2;
                if (!string.IsNullOrEmpty(SpotCheck.CheckArea))
                    newSpotCheck.CheckArea = SpotCheck.CheckArea;
                if (SpotCheck.IsOK.HasValue)
                    newSpotCheck.IsOK = SpotCheck.IsOK;
                //if (!string.IsNullOrEmpty(SpotCheck.JointCheckMans))
                newSpotCheck.JointCheckMans = SpotCheck.JointCheckMans;
                if (!string.IsNullOrEmpty(SpotCheck.AttachUrl))
                    newSpotCheck.AttachUrl = SpotCheck.AttachUrl;
                if (!string.IsNullOrEmpty(SpotCheck.State))
                    newSpotCheck.State = SpotCheck.State;
                if (!string.IsNullOrEmpty(SpotCheck.CreateMan))
                    newSpotCheck.CreateMan = SpotCheck.CreateMan;
                if (SpotCheck.IsShow.HasValue)
                    newSpotCheck.IsShow = SpotCheck.IsShow;
                if (!string.IsNullOrEmpty(SpotCheck.State2))
                    newSpotCheck.State2 = SpotCheck.State2;
                // if (!string.IsNullOrEmpty(SpotCheck.JointCheckMans2))
                newSpotCheck.JointCheckMans2 = SpotCheck.JointCheckMans2;
                // if (!string.IsNullOrEmpty(SpotCheck.JointCheckMans3))
                newSpotCheck.JointCheckMans3 = SpotCheck.JointCheckMans3;
                if (!string.IsNullOrEmpty(SpotCheck.CNProfessionalCode))
                    newSpotCheck.CNProfessionalCode = SpotCheck.CNProfessionalCode;
                db.SubmitChanges();
            }
        }
        public static void UpdateSpotCheckForApi(Model.Check_SpotCheck SpotCheck)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_SpotCheck newSpotCheck = db.Check_SpotCheck.First(e => e.SpotCheckCode == SpotCheck.SpotCheckCode);
                if (!string.IsNullOrEmpty(SpotCheck.DocCode))
                    newSpotCheck.DocCode = SpotCheck.DocCode;
                if (!string.IsNullOrEmpty(SpotCheck.UnitId))
                    newSpotCheck.UnitId = SpotCheck.UnitId;
                if (!string.IsNullOrEmpty(SpotCheck.CheckDateType))
                    newSpotCheck.CheckDateType = SpotCheck.CheckDateType;
                if (SpotCheck.SpotCheckDate.HasValue)
                    newSpotCheck.SpotCheckDate = SpotCheck.SpotCheckDate;
                if (SpotCheck.SpotCheckDate2.HasValue)
                    newSpotCheck.SpotCheckDate2 = SpotCheck.SpotCheckDate2;
                if (!string.IsNullOrEmpty(SpotCheck.CheckArea))
                    newSpotCheck.CheckArea = SpotCheck.CheckArea;
                if (SpotCheck.IsOK.HasValue)
                    newSpotCheck.IsOK = SpotCheck.IsOK;
                if (!string.IsNullOrEmpty(SpotCheck.JointCheckMans))
                    newSpotCheck.JointCheckMans = SpotCheck.JointCheckMans;
                if (!string.IsNullOrEmpty(SpotCheck.AttachUrl))
                    newSpotCheck.AttachUrl = SpotCheck.AttachUrl;
                if (!string.IsNullOrEmpty(SpotCheck.State))
                    newSpotCheck.State = SpotCheck.State;
                if (!string.IsNullOrEmpty(SpotCheck.CreateMan))
                    newSpotCheck.CreateMan = SpotCheck.CreateMan;
                if (SpotCheck.IsShow.HasValue)
                    newSpotCheck.IsShow = SpotCheck.IsShow;
                if (!string.IsNullOrEmpty(SpotCheck.State2))
                    newSpotCheck.State2 = SpotCheck.State2;
                if (!string.IsNullOrEmpty(SpotCheck.JointCheckMans2))
                    newSpotCheck.JointCheckMans2 = SpotCheck.JointCheckMans2;
                if (!string.IsNullOrEmpty(SpotCheck.JointCheckMans3))
                    newSpotCheck.JointCheckMans3 = SpotCheck.JointCheckMans3;
                if (!string.IsNullOrEmpty(SpotCheck.CNProfessionalCode))
                    newSpotCheck.CNProfessionalCode = SpotCheck.CNProfessionalCode;
                db.SubmitChanges();
            }
        }
    }
}
