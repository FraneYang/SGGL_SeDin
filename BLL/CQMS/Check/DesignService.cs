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
    public class DesignService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

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
        private static IQueryable<Model.Check_Design> qq = from x in db.Check_Design orderby x.DesignDate descending select x;


        public static string CovBool(bool? b)
        {
            string str = string.Empty;
            if (b == true)
            {
                str = "是";
            }
            else if (b == false)
            {
                str = "否";
            }
            return str;
        }


        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId, string userId, string roleId, string designType, string startTime, string endTime, string unitId, string mainItemId, string cNProfessionalCode)
        {
            return count;
        }

        /// <summary>
        /// 根据设计变更信息Id获取一个设计变更信息
        /// </summary>
        /// <param name="DesignCode">设计变更信息Id</param>
        /// <returns>一个设计变更信息实体</returns>
        public static Model.Check_Design GetDesignByDesignId(string DesignId)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_Design.FirstOrDefault(x => x.DesignId == DesignId);
        }
        public static Model.Check_Design GetDesignByDesignIdForApi(string DesignId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {

                Model.Check_Design res = db.Check_Design.FirstOrDefault(x => x.DesignId == DesignId);
                res.AttachUrl = AttachFileService.getFileUrl(res.DesignId);
                res.CarryUnitIds = res.CarryUnitIds + "$" + UnitService.getUnitNamesUnitIds(res.CarryUnitIds);
                var resItem = (from y in db.ProjectData_MainItem where y.MainItemId == res.MainItemId select y.MainItemName).FirstOrDefault();
                res.MainItemId = res.MainItemId + "$" + resItem;
                res.BuyMaterialUnitIds = res.BuyMaterialUnitIds + "$" + UnitService.getUnitNamesUnitIds(res.BuyMaterialUnitIds);
                res.CNProfessionalCode = res.CNProfessionalCode + "$" + DesignProfessionalService.GetDesignProfessional(res.CNProfessionalCode).ProfessionalName;
                res.CompileMan = res.CompileMan + "$" + ConvertManAndId(res.DesignId);
                return res;
            }
        }
        /// <summary>
        /// 增加设计变更信息信息
        /// </summary>
        /// <param name="Design">设计变更信息实体</param>
        public static void AddDesign(Model.Check_Design Design)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_Design newDesign = new Model.Check_Design();
            newDesign.DesignId = Design.DesignId;
            newDesign.ProjectId = Design.ProjectId;
            newDesign.DesignCode = Design.DesignCode;
            newDesign.MainItemId = Design.MainItemId;
            newDesign.CNProfessionalCode = Design.CNProfessionalCode;
            newDesign.DesignType = Design.DesignType;
            newDesign.DesignContents = Design.DesignContents;
            newDesign.DesignDate = Design.DesignDate;
            newDesign.CarryUnitIds = Design.CarryUnitIds;
            newDesign.IsNoChange = Design.IsNoChange;
            newDesign.IsNeedMaterial = Design.IsNeedMaterial;
            newDesign.BuyMaterialUnitIds = Design.BuyMaterialUnitIds;
            newDesign.MaterialPlanReachDate = Design.MaterialPlanReachDate;
            newDesign.PlanDay = Design.PlanDay;
            newDesign.PlanCompleteDate = Design.PlanCompleteDate;
            newDesign.MaterialRealReachDate = Design.MaterialRealReachDate;
            newDesign.RealCompleteDate = Design.RealCompleteDate;
            newDesign.AttachUrl = Design.AttachUrl;
            newDesign.CompileMan = Design.CompileMan;
            newDesign.CompileDate = Design.CompileDate;
            newDesign.State = Design.State;

            db.Check_Design.InsertOnSubmit(newDesign);
            db.SubmitChanges();
        }
        public static void AddDesignForApi(Model.Check_Design Design)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_Design newDesign = new Model.Check_Design();
                newDesign.DesignId = Design.DesignId;
                newDesign.ProjectId = Design.ProjectId;
                newDesign.DesignCode = Design.DesignCode;
                newDesign.MainItemId = Design.MainItemId;
                newDesign.CNProfessionalCode = Design.CNProfessionalCode;
                newDesign.DesignType = Design.DesignType;
                newDesign.DesignContents = Design.DesignContents;
                newDesign.DesignDate = Design.DesignDate;
                newDesign.CarryUnitIds = Design.CarryUnitIds;
                newDesign.IsNoChange = Design.IsNoChange;
                newDesign.IsNeedMaterial = Design.IsNeedMaterial;
                newDesign.BuyMaterialUnitIds = Design.BuyMaterialUnitIds;
                newDesign.MaterialPlanReachDate = Design.MaterialPlanReachDate;
                newDesign.PlanDay = Design.PlanDay;
                newDesign.PlanCompleteDate = Design.PlanCompleteDate;
                newDesign.MaterialRealReachDate = Design.MaterialRealReachDate;
                newDesign.RealCompleteDate = Design.RealCompleteDate;
                newDesign.AttachUrl = Design.AttachUrl;
                newDesign.CompileMan = Design.CompileMan;
                newDesign.CompileDate = Design.CompileDate;
                newDesign.State = Design.State;
                db.Check_Design.InsertOnSubmit(newDesign);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改设计变更信息信息
        /// </summary>
        /// <param name="Design">设计变更信息实体</param>
        public static void UpdateDesign(Model.Check_Design Design)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_Design newDesign = db.Check_Design.First(e => e.DesignId == Design.DesignId);
            newDesign.DesignCode = Design.DesignCode;
            newDesign.MainItemId = Design.MainItemId;
            newDesign.CNProfessionalCode = Design.CNProfessionalCode;
            newDesign.DesignType = Design.DesignType;
            newDesign.DesignContents = Design.DesignContents;
            newDesign.DesignDate = Design.DesignDate;
            newDesign.CarryUnitIds = Design.CarryUnitIds;
            newDesign.IsNoChange = Design.IsNoChange;
            newDesign.IsNeedMaterial = Design.IsNeedMaterial;
            newDesign.BuyMaterialUnitIds = Design.BuyMaterialUnitIds;
            newDesign.MaterialPlanReachDate = Design.MaterialPlanReachDate;
            newDesign.PlanDay = Design.PlanDay;
            newDesign.PlanCompleteDate = Design.PlanCompleteDate;
            newDesign.MaterialRealReachDate = Design.MaterialRealReachDate;
            newDesign.RealCompleteDate = Design.RealCompleteDate;
            newDesign.AttachUrl = Design.AttachUrl;
            newDesign.State = Design.State;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据设计变更信息Id删除一个设计变更信息信息
        /// </summary>
        /// <param name="DesignCode">设计变更信息Id</param>
        public static void DeleteDesign(string DesignId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_Design Design = db.Check_Design.First(e => e.DesignId == DesignId);

            db.Check_Design.DeleteOnSubmit(Design);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据项目主键获得设计变更信息的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <returns></returns>
        public static int GetDesignCountByProjectId(string projectId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Check_Design where x.ProjectId == projectId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据主项主键获得设计变更信息的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <returns></returns>
        public static int GetDesignCountByMainItemId(string mainItemId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Check_Design where x.MainItemId == mainItemId select x).ToList();
            return q.Count();
        }
        private static string GetUnitNames(string unitIds)
        {
            string unitNames = string.Empty;
            if (!string.IsNullOrEmpty(unitIds))
            {
                string[] ids = unitIds.Split(',');
                foreach (var id in ids)
                {
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(id);
                    if (unit != null)
                    {
                        unitNames += unit.UnitName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(unitNames))
                {
                    unitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
                }
            }
            return unitNames;
        }
        /// <summary>
        /// 根据时间段获取设计变更集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static IEnumerable GetDesignListByTime(string projectId, DateTime startTime, DateTime endTime)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return from x in db.Check_Design
                   where x.ProjectId == projectId && x.DesignDate >= startTime && x.DesignDate < endTime
                   select new
                   {
                       x.DesignId,
                       x.ProjectId,
                       x.DesignCode,
                       MainItemName = (from y in db.ProjectData_MainItem where y.MainItemId == x.MainItemId select y.MainItemName).First(),
                       CNProfessional = (from y in db.Base_DesignProfessional where y.DesignProfessionalId == x.CNProfessionalCode select y.ProfessionalName).First(),
                       x.DesignType,
                       x.DesignContents,
                       x.DesignDate,
                       CarryUnit = GetUnitNames(x.CarryUnitIds),
                       IsNoChange = x.IsNoChange == true ? "是" : "否",
                       IsNeedMaterial = x.IsNeedMaterial == true ? "是" : "否",
                       BuyMaterialUnit = GetUnitNames(x.BuyMaterialUnitIds),
                       x.MaterialPlanReachDate,
                       x.PlanDay,
                       x.PlanCompleteDate,
                       x.MaterialRealReachDate,
                       x.RealCompleteDate,
                       x.CompileMan,
                       x.CompileDate,
                       x.State,
                   };
        }
        /// <summary>
        /// 获取变更类型项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static ListItem[] GetDesignTypeList()
        {
            ListItem[] lis = new ListItem[10];
            lis[0] = new ListItem("标准规范升版", "标准规范升版");
            lis[1] = new ListItem("设计优化", "设计优化");
            lis[2] = new ListItem("设计不当", "设计不当");
            lis[3] = new ListItem("设计漏项", "设计漏项");
            lis[4] = new ListItem("材料代用", "材料代用");
            lis[5] = new ListItem("业主要求", "业主要求");
            lis[6] = new ListItem("供货偏离", "供货偏离");
            lis[7] = new ListItem("施工偏离", "施工偏离");
            lis[8] = new ListItem("外部条件改变", "外部条件改变");
            lis[9] = new ListItem("其它", "其它");
            return lis;
        }

        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state)
        {
            if (state == Const.Design_Compile || state == Const.Design_ReCompile)
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("变更分析", Const.Design_Audit1);
                return lis;
            }
            else if (state == Const.Design_Audit1)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("变更分析审核", Const.Design_Audit2);
                lis[1] = new ListItem("重新整理", Const.Design_ReCompile);
                return lis;
            }
            else if (state == Const.Design_Audit2)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("变更实施", Const.Design_Audit3);
                lis[1] = new ListItem("变更分析", Const.Design_Audit1);
                return lis;
            }
            else if (state == Const.Design_Audit3)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("变更实施审核", Const.Design_Audit4);
                lis[1] = new ListItem("变更分析审核", Const.Design_Audit2);
                return lis;
            }
            else if (state == Const.Design_Audit4)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.Design_Complete);
                lis[1] = new ListItem("变更实施", Const.Design_Audit3);
                return lis;
            }
            else
                return null;
        }
        public static List<Model.Check_Design> getListDataForApi(string projectId, string unitName, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_Design> q = db.Check_Design;
                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }

                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                if (!string.IsNullOrEmpty(unitName))
                {

                    q = q.Where(e => e.CarryUnitIds != null);
                }


                var qres = from x in q
                           orderby x.DesignCode descending
                           select new
                           {
                               x.DesignId,
                               x.ProjectId,
                               x.DesignCode,
                               x.MainItemId,
                               x.CNProfessionalCode,
                               MainItemName = (from y in db.ProjectData_MainItem where y.MainItemId == x.MainItemId select y.MainItemName).First(),
                               CNProfessional = (from y in db.Base_DesignProfessional where y.DesignProfessionalId == x.CNProfessionalCode select y.ProfessionalName).First(),
                               CompileManName = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                               x.DesignType,
                               x.DesignContents,
                               x.DesignDate,
                               x.CarryUnitIds,
                               CarryUnit = GetUnitNames(x.CarryUnitIds),
                               x.IsNoChange,
                               x.IsNeedMaterial,
                               x.BuyMaterialUnitIds,
                               BuyMaterialUnit = GetUnitNames(x.BuyMaterialUnitIds),
                               x.MaterialPlanReachDate,
                               x.PlanDay,
                               x.PlanCompleteDate,
                               x.MaterialRealReachDate,
                               x.RealCompleteDate,
                               x.CompileMan,
                               x.CompileDate,
                               x.State
                           };
                List<Model.Check_Design> res = new List<Model.Check_Design>();

                if (!string.IsNullOrEmpty(unitName))
                {
                    List<string> ids;
                    var qids = from x in new Model.SGGLDB(Funs.ConnString).Base_Unit where x.UnitName.Contains(unitName) select x.UnitId;
                    ids = qids.ToList();


                    var list = qres.ToList();
                    foreach (var item in list)
                    {
                        string[] carryIds = item.CarryUnitIds.Split(',');
                        foreach (string id in carryIds)
                        {
                            if (ids.Contains(id))
                            {
                                Model.Check_Design cd = new Model.Check_Design();
                                cd.DesignId = item.DesignId;
                                cd.ProjectId = item.ProjectId;
                                cd.DesignCode = item.DesignCode;
                                cd.CNProfessionalCode = item.CNProfessionalCode + "$" + item.CNProfessional;
                                cd.DesignType = item.DesignType;
                                cd.DesignContents = item.DesignContents;
                                cd.DesignDate = item.DesignDate;
                                cd.CarryUnitIds = item.CarryUnitIds + "$" + item.CarryUnit;
                                cd.IsNoChange = item.IsNoChange;
                                cd.IsNeedMaterial = item.IsNeedMaterial;
                                cd.MainItemId = item.MainItemId + "$" + item.MainItemName;
                                cd.BuyMaterialUnitIds = item.BuyMaterialUnitIds + "$" + item.BuyMaterialUnit;
                                cd.MaterialPlanReachDate = item.MaterialPlanReachDate;
                                cd.PlanDay = item.PlanDay;
                                cd.PlanCompleteDate = item.PlanCompleteDate;
                                cd.MaterialRealReachDate = item.MaterialRealReachDate;
                                cd.RealCompleteDate = item.RealCompleteDate;
                                cd.CompileMan = item.CompileManName + "$" + ConvertMan(item.DesignId);
                                cd.CompileDate = item.CompileDate;
                                cd.State = item.State;
                                cd.AttachUrl = AttachFileService.getFileUrl(item.DesignId);
                                res.Add(cd);
                                break;
                            }
                        }

                    }
                    res = res.Skip(startRowIndex).Take(maximumRows).ToList();
                }
                else
                {
                    var list = qres.Skip(startRowIndex).Take(maximumRows).ToList();
                    foreach (var item in list)
                    {
                        Model.Check_Design cd = new Model.Check_Design();
                        cd.DesignId = item.DesignId;
                        cd.ProjectId = item.ProjectId;
                        cd.DesignCode = item.DesignCode;
                        cd.CNProfessionalCode = item.CNProfessionalCode + "$" + item.CNProfessional;
                        cd.DesignType = item.DesignType;
                        cd.DesignContents = item.DesignContents;
                        cd.DesignDate = item.DesignDate;
                        cd.CarryUnitIds = item.CarryUnitIds + "$" + item.CarryUnit;
                        cd.IsNoChange = item.IsNoChange;
                        cd.IsNeedMaterial = item.IsNeedMaterial;
                        cd.MainItemId = item.MainItemId + "$" + item.MainItemName;
                        cd.BuyMaterialUnitIds = item.BuyMaterialUnitIds + "$" + item.BuyMaterialUnit;
                        cd.MaterialPlanReachDate = item.MaterialPlanReachDate;
                        cd.PlanDay = item.PlanDay;
                        cd.PlanCompleteDate = item.PlanCompleteDate;
                        cd.MaterialRealReachDate = item.MaterialRealReachDate;
                        cd.RealCompleteDate = item.RealCompleteDate;
                        cd.CompileMan = item.CompileManName + "$" + ConvertManAndId(item.DesignId);
                        cd.CompileDate = item.CompileDate;
                        cd.State = item.State;
                        cd.AttachUrl = AttachFileService.getFileUrl(item.DesignId);
                        res.Add(cd);
                    }
                }
                return res;
            }

        }

        public static List<Model.Check_Design> getListDataForApi(string carryUnitIds, string state, string mainItemId, string cNProfessionalCode, string designType, string designDateA, string designDateZ, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_Design> q = db.Check_Design;
                if (!string.IsNullOrEmpty(projectId) && "undefined" != projectId)
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                if (!string.IsNullOrEmpty(carryUnitIds) && "undefined" != carryUnitIds)
                {
                    q = q.Where(e => e.CarryUnitIds.Contains(carryUnitIds));
                }
                if (!string.IsNullOrEmpty(state) && "undefined" != state)
                {
                    if ("已闭合" == state)
                    {
                        q = q.Where(e => e.State == "6");
                    }
                    else
                    {
                        q = q.Where(e => e.State != "6");

                    }
                }
                if (!string.IsNullOrEmpty(mainItemId) && "undefined" != mainItemId)
                {
                    q = q.Where(e => e.MainItemId == mainItemId);
                }

                if (!string.IsNullOrEmpty(cNProfessionalCode) && "undefined" != cNProfessionalCode)
                {
                    q = q.Where(e => e.CNProfessionalCode.Contains(cNProfessionalCode));
                }
                if (!string.IsNullOrEmpty(designType) && "undefined" != designType)
                {
                    q = q.Where(e => e.DesignType == designType);
                }
                if (!string.IsNullOrEmpty(designDateA) && "undefined" != designDateA)
                {
                    DateTime date = DateTime.ParseExact(designDateA, "yyyy-MM-dd", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.DesignDate >= date);
                }
                if (!string.IsNullOrEmpty(designDateZ) && "undefined" != designDateZ)
                {
                    DateTime date = DateTime.ParseExact(designDateZ + "23:59:59", "yyyy-MM-ddHH:mm:ss", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.DesignDate <= date);
                }

                var qres = from x in q.Skip(startRowIndex).Take(maximumRows)
                           select new
                           {
                               x.DesignId,
                               x.ProjectId,
                               x.DesignCode,
                               x.MainItemId,
                               x.CNProfessionalCode,
                               MainItemName = (from y in db.ProjectData_MainItem where y.MainItemId == x.MainItemId select y.MainItemName).First(),
                               CNProfessional = (from y in db.Base_DesignProfessional where y.DesignProfessionalId == x.CNProfessionalCode select y.ProfessionalName).First(),
                               CompileManName = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                               x.DesignType,
                               x.DesignContents,
                               x.DesignDate,
                               CarryUnit = GetUnitNames(x.CarryUnitIds),
                               x.IsNoChange,
                               x.IsNeedMaterial,
                               x.BuyMaterialUnitIds,
                               BuyMaterialUnit = GetUnitNames(x.BuyMaterialUnitIds),
                               x.MaterialPlanReachDate,
                               x.PlanDay,
                               x.PlanCompleteDate,
                               x.MaterialRealReachDate,
                               x.RealCompleteDate,
                               x.CompileMan,
                               x.CompileDate,
                               x.State

                           };
                List<Model.Check_Design> res = new List<Model.Check_Design>();
                var list = qres.ToList();
                foreach (var item in list)
                {
                    Model.Check_Design cd = new Model.Check_Design();
                    cd.DesignId = item.DesignId;
                    cd.ProjectId = item.ProjectId;
                    cd.DesignCode = item.DesignCode;
                    cd.CNProfessionalCode = cd.CNProfessionalCode + "$" + item.CNProfessional;
                    cd.DesignType = item.DesignType;
                    cd.DesignContents = item.DesignContents;
                    cd.DesignDate = item.DesignDate;
                    cd.CarryUnitIds = cd.CarryUnitIds + "$" + item.CarryUnit;
                    cd.IsNoChange = item.IsNoChange;
                    cd.IsNeedMaterial = item.IsNeedMaterial;
                    cd.MainItemId = item.MainItemId + "$" + item.MainItemName;
                    cd.BuyMaterialUnitIds = item.BuyMaterialUnitIds + "$" + item.BuyMaterialUnit;
                    cd.MaterialPlanReachDate = item.MaterialPlanReachDate;
                    cd.PlanDay = item.PlanDay;
                    cd.PlanCompleteDate = item.PlanCompleteDate;
                    cd.MaterialRealReachDate = item.MaterialRealReachDate;
                    cd.RealCompleteDate = item.RealCompleteDate;
                    cd.CompileMan = item.CompileManName + "$" + ConvertManAndId(item.DesignId);
                    cd.CompileDate = item.CompileDate;
                    cd.State = item.State;
                    cd.AttachUrl = AttachFileService.getFileUrl(item.DesignId);
                    res.Add(cd);
                }
                return res;
            }
        }
        public static string ConvertMan(string id)
        {
            if (id != null)
            {
                Model.Check_DesignApprove a = BLL.DesignApproveService.getCurrApproveForApi(id);
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
        public static string ConvertManAndId(string id)
        {
            if (id != null)
            {
                Model.Check_DesignApprove a = BLL.DesignApproveService.getCurrApproveForApi(id);
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

        public static void UpdateDesignForApi(Model.Check_Design Design)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_Design newDesign = db.Check_Design.FirstOrDefault(e => e.DesignId == Design.DesignId);
                if (newDesign != null)
                {
                    if (!string.IsNullOrEmpty(Design.DesignCode))
                        newDesign.DesignCode = Design.DesignCode;
                    if (!string.IsNullOrEmpty(Design.MainItemId))
                        newDesign.MainItemId = Design.MainItemId;
                    if (!string.IsNullOrEmpty(Design.CNProfessionalCode))
                        newDesign.CNProfessionalCode = Design.CNProfessionalCode;
                    if (!string.IsNullOrEmpty(Design.DesignType))
                        newDesign.DesignType = Design.DesignType;
                    if (!string.IsNullOrEmpty(Design.DesignContents))
                        newDesign.DesignContents = Design.DesignContents;
                    if (Design.DesignDate.HasValue)
                        newDesign.DesignDate = Design.DesignDate;
                    if (!string.IsNullOrEmpty(Design.CarryUnitIds))
                        newDesign.CarryUnitIds = Design.CarryUnitIds;
                    if (Design.IsNoChange.HasValue)
                        newDesign.IsNoChange = Design.IsNoChange;
                    if (Design.IsNeedMaterial.HasValue)
                        newDesign.IsNeedMaterial = Design.IsNeedMaterial;

                    if (!string.IsNullOrEmpty(Design.BuyMaterialUnitIds))
                        newDesign.BuyMaterialUnitIds = Design.BuyMaterialUnitIds;
                    if (Design.MaterialPlanReachDate.HasValue)
                        newDesign.MaterialPlanReachDate = Design.MaterialPlanReachDate;
                    if (newDesign.IsNeedMaterial.HasValue && !newDesign.IsNeedMaterial.Value)
                    {
                        newDesign.BuyMaterialUnitIds = null;
                        newDesign.MaterialPlanReachDate = null;
                    }
                    if (Design.PlanDay.HasValue)
                        newDesign.PlanDay = Design.PlanDay;
                    if (Design.PlanCompleteDate.HasValue)
                        newDesign.PlanCompleteDate = Design.PlanCompleteDate;
                    if (Design.MaterialRealReachDate.HasValue)
                        newDesign.MaterialRealReachDate = Design.MaterialRealReachDate;
                    if (Design.RealCompleteDate.HasValue)
                        newDesign.RealCompleteDate = Design.RealCompleteDate;
                    if (!string.IsNullOrEmpty(Design.AttachUrl))
                        newDesign.AttachUrl = Design.AttachUrl;
                    if (!string.IsNullOrEmpty(Design.State))
                        newDesign.State = Design.State;

                    db.SubmitChanges();
                }
            }
        }
    }
}
