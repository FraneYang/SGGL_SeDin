using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CheckEquipmentService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.Check_CheckEquipment> qq = from x in db.Check_CheckEquipment orderby x.CompileDate descending select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string userId, string roleId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Check_CheckEquipment> q = qq;
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
                       x.CheckEquipmentId,
                       x.ProjectId,
                       UserUnit = (from y in db.Base_Unit where y.UnitId == x.UserUnitId select y.UnitName).First(),
                       x.EquipmentName,
                       x.Format,
                       x.SetAccuracyGrade,
                       x.RealAccuracyGrade,
                       x.CheckCycle,
                       x.CheckDay,
                       IsIdentification = x.IsIdentification == true ? "有" : "没有",
                       IsCheckCertificate = x.IsCheckCertificate == true ? "有" : "没有",
                       x.AttachUrl,
                       CompileMan = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                       x.CompileDate,
                       x.State,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId, string userId, string roleId)
        {
            return count;
        }

        /// <summary>
        /// 根据检试验设备及测量器具信息Id获取一个检试验设备及测量器具信息
        /// </summary>
        /// <param name="CheckEquipmentCode">检试验设备及测量器具信息Id</param>
        /// <returns>一个检试验设备及测量器具信息实体</returns>
        public static Model.Check_CheckEquipment GetCheckEquipmentByCheckEquipmentId(string CheckEquipmentId)
        {
            Model.Check_CheckEquipment res = Funs.DB.Check_CheckEquipment.FirstOrDefault(x => x.CheckEquipmentId == CheckEquipmentId);
            //res.AttachUrl = AttachFileService.getFileUrl(res.CheckEquipmentId);
            return res;

        }
        public static Model.Check_CheckEquipment GetCheckEquipmentByCheckEquipmentIdForApi(string CheckEquipmentId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_CheckEquipment res = db.Check_CheckEquipment.FirstOrDefault(x => x.CheckEquipmentId == CheckEquipmentId);
                //res.AttachUrl = AttachFileService.getFileUrl(res.CheckEquipmentId);
                res.UserUnitId = res.UserUnitId + "$" + UnitService.getUnitNamesUnitIds(res.UserUnitId);
                return res;
            }


        }
        /// <summary>
        /// 增加检试验设备及测量器具信息信息
        /// </summary>
        /// <param name="CheckEquipment">检试验设备及测量器具信息实体</param>
        public static void AddCheckEquipment(Model.Check_CheckEquipment CheckEquipment)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckEquipment newCheckEquipment = new Model.Check_CheckEquipment();
            newCheckEquipment.CheckEquipmentId = CheckEquipment.CheckEquipmentId;
            newCheckEquipment.ProjectId = CheckEquipment.ProjectId;
            newCheckEquipment.UserUnitId = CheckEquipment.UserUnitId;
            newCheckEquipment.EquipmentName = CheckEquipment.EquipmentName;
            newCheckEquipment.Format = CheckEquipment.Format;
            newCheckEquipment.SetAccuracyGrade = CheckEquipment.SetAccuracyGrade;
            newCheckEquipment.RealAccuracyGrade = CheckEquipment.RealAccuracyGrade;
            newCheckEquipment.CheckCycle = CheckEquipment.CheckCycle;
            newCheckEquipment.CheckDay = CheckEquipment.CheckDay;
            newCheckEquipment.IsIdentification = CheckEquipment.IsIdentification;
            newCheckEquipment.IsCheckCertificate = CheckEquipment.IsCheckCertificate;
            newCheckEquipment.AttachUrl = CheckEquipment.AttachUrl;
            newCheckEquipment.CompileMan = CheckEquipment.CompileMan;
            newCheckEquipment.CompileDate = CheckEquipment.CompileDate;
            newCheckEquipment.State = CheckEquipment.State;
            newCheckEquipment.Isdamage = CheckEquipment.Isdamage;
            newCheckEquipment.SaveHandleMan = CheckEquipment.SaveHandleMan;
            db.Check_CheckEquipment.InsertOnSubmit(newCheckEquipment);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改检试验设备及测量器具信息信息
        /// </summary>
        /// <param name="CheckEquipment">检试验设备及测量器具信息实体</param>
        public static void UpdateCheckEquipment(Model.Check_CheckEquipment CheckEquipment)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckEquipment newCheckEquipment = db.Check_CheckEquipment.First(e => e.CheckEquipmentId == CheckEquipment.CheckEquipmentId);
            newCheckEquipment.UserUnitId = CheckEquipment.UserUnitId;
            newCheckEquipment.EquipmentName = CheckEquipment.EquipmentName;
            newCheckEquipment.Format = CheckEquipment.Format;
            newCheckEquipment.SetAccuracyGrade = CheckEquipment.SetAccuracyGrade;
            newCheckEquipment.RealAccuracyGrade = CheckEquipment.RealAccuracyGrade;
            newCheckEquipment.CheckCycle = CheckEquipment.CheckCycle;
            newCheckEquipment.CheckDay = CheckEquipment.CheckDay;
            newCheckEquipment.IsIdentification = CheckEquipment.IsIdentification;
            newCheckEquipment.IsCheckCertificate = CheckEquipment.IsCheckCertificate;
            newCheckEquipment.AttachUrl = CheckEquipment.AttachUrl;
            newCheckEquipment.State = CheckEquipment.State;
            newCheckEquipment.Isdamage = CheckEquipment.Isdamage;
            newCheckEquipment.SaveHandleMan = CheckEquipment.SaveHandleMan;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据检试验设备及测量器具信息Id删除一个检试验设备及测量器具信息信息
        /// </summary>
        /// <param name="CheckEquipmentCode">检试验设备及测量器具信息Id</param>
        public static void DeleteCheckEquipment(string CheckEquipmentId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckEquipment CheckEquipment = db.Check_CheckEquipment.First(e => e.CheckEquipmentId == CheckEquipmentId);

            db.Check_CheckEquipment.DeleteOnSubmit(CheckEquipment);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据项目主键获得检试验设备及测量器具信息的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <returns></returns>
        public static int GetCheckEquipmentCountByProjectId(string projectId)
        {
            var q = (from x in Funs.DB.Check_CheckEquipment where x.ProjectId == projectId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state)
        {
            if (state == Const.CheckEquipment_Compile || state == Const.CheckEquipment_ReCompile)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审核", Const.CheckEquipment_Approve);
                lis[1] = new ListItem("审批完成", Const.CheckEquipment_Complete);
                return lis;
            }
            else if (state == Const.CheckEquipment_Approve)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.CheckEquipment_Complete);
                lis[1] = new ListItem("重新编制", Const.CheckEquipment_ReCompile);
                return lis;
            }
            else
                return null;
        }

        /// <summary>
        /// 根据单位主键获得检试验设备及测量器具的数量
        /// </summary>
        /// <param name="unitId">单位主键</param>
        /// <returns></returns>
        public static int GetCheckEquipmentCountByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.Check_CheckEquipment where x.UserUnitId == unitId select x).ToList();
            return q.Count();
        }
        public static List<Model.Check_CheckEquipment> getListByProject(string name, string projectId, int startRowIndex, int maximumRows)
        {
            List<Model.Check_CheckEquipment> listRes = new List<Model.Check_CheckEquipment>();
            List<String> unitids = new List<string>();
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var qunit = from u in db.Base_Unit
                                where u.UnitName.Contains(name)
                                select u.UnitId;
                    unitids = qunit.ToList();
                }
                var q = from x in db.Check_CheckEquipment
                        orderby x.CheckDay descending
                        where x.ProjectId == projectId &&
                          (name == "" || x.EquipmentName.Contains(name) || unitids.Contains(x.UserUnitId))
                        select new
                        {
                            x.CheckEquipmentId,
                            x.ProjectId,
                            x.EquipmentName,
                            x.Format,
                            x.SetAccuracyGrade,
                            x.RealAccuracyGrade,
                            x.CheckCycle,
                            x.CheckDay,
                            x.IsIdentification,
                            x.IsCheckCertificate,
                            x.AttachUrl,
                            x.CompileDate,
                            x.State,
                            x.Isdamage,
                            x.UserUnitId,
                            UserUnitName = (from y in db.Base_Unit where y.UnitId == x.UserUnitId select y.UnitName).FirstOrDefault(),

                        };
                var list = q.Skip(startRowIndex * maximumRows).Take(maximumRows).ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    Model.Check_CheckEquipment x = new Model.Check_CheckEquipment();
                    x.CheckEquipmentId = list[i].CheckEquipmentId;
                    x.UserUnitId = list[i].UserUnitId + "$" + list[i].UserUnitName;
                    x.ProjectId = list[i].ProjectId;
                    x.EquipmentName = list[i].EquipmentName;
                    x.Format = list[i].Format;
                    x.SetAccuracyGrade = list[i].SetAccuracyGrade;
                    x.RealAccuracyGrade = list[i].RealAccuracyGrade;
                    x.CheckCycle = list[i].CheckCycle;
                    x.CheckDay = list[i].CheckDay;
                    x.IsIdentification = list[i].IsIdentification;
                    x.IsCheckCertificate = list[i].IsCheckCertificate;
                    x.CompileDate = list[i].CompileDate;
                    x.State = list[i].State;
                    x.Isdamage = list[i].Isdamage;
                    if (x.CheckCycle.HasValue && x.CheckDay.HasValue)
                    {
                        x.CompileMan = ConvertMan(x.CheckEquipmentId) + "$" + ConvertIsBeOverdue(x.CheckCycle.Value, x.CheckDay.Value);
                    }
                    else
                    {
                        x.CompileMan = ConvertMan(x.CheckEquipmentId) + "$";

                    }
                    x.AttachUrl = AttachFileService.getFileUrl(x.CheckEquipmentId);
                    listRes.Add(x);
                }
            }
            return listRes;
        }
        protected static string ConvertIsBeOverdue(object CheckCycle, object CheckDay)
        {
            if (CheckCycle != null && CheckDay != null)
            {
                if (!string.IsNullOrEmpty(CheckCycle.ToString()) && !string.IsNullOrEmpty(CheckDay.ToString()))
                {
                    var ResultData = Convert.ToDateTime(CheckDay).AddDays(Convert.ToDouble(CheckCycle) * 365);
                    if (ResultData >= DateTime.Now)
                    {
                        return "未过期";
                    }
                    else
                    {
                        return "过期";
                    }
                }
            }
            return "";

        }
        protected static string ConvertMan(string designId)
        {
            if (!string.IsNullOrEmpty(designId))
            {
                Model.Check_CheckEquipmentApprove a = BLL.CheckEquipmentApproveService.GetCheckEquipmentApproveByCheckEquipmentId(designId);
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        var item = BLL.UserService.GetUserByUserId(a.ApproveMan);
                        if (item != null)
                            return item.UserName;
                        else
                            return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
    }
}
