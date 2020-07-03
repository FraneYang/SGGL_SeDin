using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CQMSConstructSolutionService
    {
        public static string IsAgree(Object type, Object res)
        {
            string result = string.Empty;
            if (type.ToString().Equals(Const.CQMSConstructSolution_ReCompile) || type.ToString().Equals(Const.CQMSConstructSolution_Compile))
            {
                res = null;
            }
            if (res != null)
            {
                if (Convert.ToBoolean(res))
                {
                    result = "是";
                }
                else
                {
                    result = "否";
                }
            }
            return result;
        }

        /// <summary>
        /// 根据方案审查Id删除一个方案审查信息
        /// </summary>
        /// <param name="constructSolutionCode">方案审查Id</param>
        public static void DeleteConstructSolution(string constructSolutionId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_CQMSConstructSolution constructSolution = db.Solution_CQMSConstructSolution.First(e => e.ConstructSolutionId == constructSolutionId);

            db.Solution_CQMSConstructSolution.DeleteOnSubmit(constructSolution);
            db.SubmitChanges();
        }

        /// <summary>
        /// 增加方案审查信息
        /// </summary>
        /// <param name="constructSolution">方案审查实体</param>
        public static void AddConstructSolution(Model.Solution_CQMSConstructSolution constructSolution)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_CQMSConstructSolution newConstructSolution = new Model.Solution_CQMSConstructSolution();
            newConstructSolution.ConstructSolutionId = constructSolution.ConstructSolutionId;
            newConstructSolution.Code = constructSolution.Code;
            newConstructSolution.ProjectId = constructSolution.ProjectId;
            newConstructSolution.UnitId = constructSolution.UnitId;
            newConstructSolution.SolutionName = constructSolution.SolutionName;
            newConstructSolution.SolutionType = constructSolution.SolutionType;
            newConstructSolution.UnitWorkIds = constructSolution.UnitWorkIds;
            newConstructSolution.CNProfessionalCodes = constructSolution.CNProfessionalCodes;
            newConstructSolution.AttachUrl = constructSolution.AttachUrl;
            newConstructSolution.CompileMan = constructSolution.CompileMan;
            newConstructSolution.CompileDate = constructSolution.CompileDate;
            newConstructSolution.State = constructSolution.State;
            newConstructSolution.Edition = constructSolution.Edition;
            db.Solution_CQMSConstructSolution.InsertOnSubmit(newConstructSolution);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改方案审查信息
        /// </summary>
        /// <param name="constructSolution">方案审查实体</param>
        public static void UpdateConstructSolution(Model.Solution_CQMSConstructSolution constructSolution)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Solution_CQMSConstructSolution newConstructSolution = db.Solution_CQMSConstructSolution.First(e => e.ConstructSolutionId == constructSolution.ConstructSolutionId);
            newConstructSolution.Code = constructSolution.Code;
            newConstructSolution.UnitId = constructSolution.UnitId;
            newConstructSolution.SolutionName = constructSolution.SolutionName;
            newConstructSolution.SolutionType = constructSolution.SolutionType;
            newConstructSolution.UnitWorkIds = constructSolution.UnitWorkIds;
            newConstructSolution.CNProfessionalCodes = constructSolution.CNProfessionalCodes;
            newConstructSolution.AttachUrl = constructSolution.AttachUrl;
            newConstructSolution.State = constructSolution.State;
            newConstructSolution.Edition = constructSolution.Edition;
            db.SubmitChanges();
        }


        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// 
        public static string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.CQMSConstructSolution_ReCompile)
                {
                    return "重报";
                }
                else if (state.ToString() == BLL.Const.CQMSConstructSolution_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.CQMSConstructSolution_Audit)
                {
                    return "会签";
                }
                else if (state.ToString() == BLL.Const.CQMSConstructSolution_Complete)
                {
                    return "审批完成";
                }
            }
            return "";
        }
        /// <summary>
        /// 获取单位信息中的常用工程类型信息
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetSolutionType()
        {
            ListItem[] list = new ListItem[3];
            list[0] = new ListItem("施工组织设计", "1");
            list[1] = new ListItem("专项施工方案", "2");
            list[2] = new ListItem("施工方案", "3");
            return list;
        }


        /// <summary>
        /// 根据方案审查Id获取一个方案审查信息
        /// </summary>
        /// <param name="constructSolutionCode">方案审查Id</param>
        /// <returns>一个方案审查实体</returns>
        public static Model.Solution_CQMSConstructSolution GetConstructSolutionByConstructSolutionId(string constructSolutionId)
        {
            return Funs.DB.Solution_CQMSConstructSolution.FirstOrDefault(x => x.ConstructSolutionId == constructSolutionId);
        }
        public static Model.Solution_CQMSConstructSolution GetConstructSolutionByConstructSolutionIdForApi(string constructSolutionId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Solution_CQMSConstructSolution solution = db.Solution_CQMSConstructSolution.FirstOrDefault(x => x.ConstructSolutionId == constructSolutionId);
                solution.AttachUrl = AttachFileService.getFileUrl(solution.ConstructSolutionId);
                solution.CNProfessionalCodes = solution.CNProfessionalCodes + "$" + GetProfessionalName(solution.CNProfessionalCodes);
                solution.UnitWorkIds = solution.UnitWorkIds + "$" + GetUnitWorkName(solution.UnitWorkIds);
                var unit = BLL.UnitService.GetUnitByUnitId(solution.UnitId);
                solution.UnitId = solution.UnitId + "$" + (unit != null ? unit.UnitName : "");
                solution.CompileMan = solution.CompileMan + "$" + UserService.GetUserNameByUserId(solution.CompileMan);
                return solution;
            }

        }
        /// <summary>
        /// 获取方案类别
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertSolutionType(object solutionType)
        {
            if (solutionType != null)
            {
                if (solutionType.ToString() == "1")
                {
                    return "施工组织设计";
                }
                else if (solutionType.ToString() == "2")
                {
                    return "专项施工方案";
                }
                else if (solutionType.ToString() == "3")
                {
                    return "施工方案";
                }
            }
            return "";
        }
        public static string GetProfessionalName(string cNProfessionalCodes)
        {
            string professionalName = string.Empty;
            if (!string.IsNullOrEmpty(cNProfessionalCodes))
            {
                string[] strs = cNProfessionalCodes.Split(',');
                foreach (var item in strs)
                {
                    var cn = BLL.CNProfessionalService.GetCNProfessional(item);
                    if (cn != null)
                    {
                        professionalName += cn.ProfessionalName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(professionalName))
                {
                    professionalName = professionalName.Substring(0, professionalName.LastIndexOf(","));
                }
            }
            return professionalName;
        }
        public static string GetUnitWorkName(string unitWorkIds)
        {
            string unitWorkName = string.Empty;
            if (!string.IsNullOrEmpty(unitWorkIds))
            {
                string[] strs = unitWorkIds.Split(',');
                foreach (var item in strs)
                {
                    var un = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(item);
                    if (un != null)
                    {
                        unitWorkName += un.UnitWorkName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(unitWorkName))
                {
                    unitWorkName = unitWorkName.Substring(0, unitWorkName.LastIndexOf(","));
                }
            }
            return unitWorkName;
        }
        public static List<Model.Solution_CQMSConstructSolution> getListDataForApi(string name, string unitId, string unitWork, string cNProfessionalCodes, string solutionType, string state, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Solution_CQMSConstructSolution> q = db.Solution_CQMSConstructSolution;

                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                if (!string.IsNullOrEmpty(unitId) && unitId != "undefined")
                {
                    q = q.Where(e => e.UnitId == unitId);
                }
                if (!string.IsNullOrEmpty(unitWork) && unitWork != "undefined")
                {
                    q = q.Where(e => e.UnitWorkIds.Contains(unitWork));
                }
                if (!string.IsNullOrEmpty(cNProfessionalCodes) && cNProfessionalCodes != "undefined")
                {
                    q = q.Where(e => e.CNProfessionalCodes.Contains(cNProfessionalCodes));
                }
                if (!string.IsNullOrEmpty(solutionType) && solutionType != "undefined")
                {
                    q = q.Where(e => e.SolutionType == solutionType);
                }
                if (!string.IsNullOrEmpty(state) && state != "undefined")
                {
                    if ("已闭合" == state)
                    {
                        q = q.Where(e => e.State == "3");
                    }
                    else if ("未闭合" == state)
                    {
                        q = q.Where(e => e.State != "3");

                    }
                }
                if (!string.IsNullOrEmpty(name))
                {
                    List<string> ids = new List<string>();
                    var qunit = from u in Funs.DB.Base_Unit
                                where u.UnitName.Contains(name)
                                select u.UnitId;
                    ids = qunit.ToList();
                    q = q.Where(e => e.SolutionName.Contains(name) || ids.Contains(e.UnitId));
                }


                var qlist = from x in q
                            orderby x.Code descending

                            select new
                            {
                                x.ConstructSolutionId,
                                x.Code,
                                x.ProjectId,
                                x.UnitId,
                                UnitName = (from y in db.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                                x.SolutionName,
                                x.SolutionType,
                                x.UnitWorkIds,
                                UnitWorkName = GetUnitWorkName(x.UnitWorkIds),
                                x.CNProfessionalCodes,
                                ProfessionalName = GetProfessionalName(x.CNProfessionalCodes),
                                x.AttachUrl,
                                x.CompileMan,
                                x.CompileDate,
                                x.State,
                                x.Edition,
                                CompileManName = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                            };

                List<Model.Solution_CQMSConstructSolution> res = new List<Model.Solution_CQMSConstructSolution>();
                var qres = qlist.Skip(startRowIndex * maximumRows).Take(maximumRows).ToList();
                foreach (var item in qres)
                {
                    Model.Solution_CQMSConstructSolution cs = new Model.Solution_CQMSConstructSolution();
                    cs.ConstructSolutionId = item.ConstructSolutionId;
                    cs.Code = item.Code;
                    cs.ProjectId = item.ProjectId;
                    cs.UnitId = item.UnitId + "$" + item.UnitName;
                    cs.SolutionName = item.SolutionName;
                    cs.SolutionType = item.SolutionType;
                    cs.UnitWorkIds = item.UnitWorkIds + "$" + item.UnitWorkName;
                    cs.CNProfessionalCodes = item.CNProfessionalCodes + "$" + item.ProfessionalName;
                    cs.AttachUrl = item.AttachUrl;
                    cs.CompileMan = item.CompileMan + "$" + item.CompileManName;
                    cs.CompileDate = item.CompileDate;
                    cs.State = item.State;
                    cs.Edition = item.Edition;

                    res.Add(cs);
                }
                return res;
            }
        }
        public static List<Model.Solution_CQMSConstructSolution> getListDataForApi(string name, string projectId, int startRowIndex, int maximumRows)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Solution_CQMSConstructSolution> q = db.Solution_CQMSConstructSolution;

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
                    q = q.Where(e => e.SolutionName.Contains(name) || ids.Contains(e.UnitId));
                }


                var qlist = from x in q
                            orderby x.Code descending

                            select new
                            {
                                x.ConstructSolutionId,
                                x.Code,
                                x.ProjectId,
                                x.UnitId,
                                UnitName = (from y in db.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                                x.SolutionName,
                                x.SolutionType,
                                x.UnitWorkIds,
                                UnitWorkName = GetUnitWorkName(x.UnitWorkIds),
                                x.CNProfessionalCodes,
                                ProfessionalName = GetProfessionalName(x.CNProfessionalCodes),
                                x.AttachUrl,
                                x.CompileMan,
                                x.CompileDate,
                                x.State,
                                x.Edition,
                                CompileManName = (from y in db.Sys_User where y.UserId == x.CompileMan select y.UserName).First(),
                            };

                List<Model.Solution_CQMSConstructSolution> res = new List<Model.Solution_CQMSConstructSolution>();
                var qres = qlist.Skip(startRowIndex * maximumRows).Take(maximumRows).ToList();
                foreach (var item in qres)
                {
                    Model.Solution_CQMSConstructSolution cs = new Model.Solution_CQMSConstructSolution();
                    cs.ConstructSolutionId = item.ConstructSolutionId;
                    cs.Code = item.Code;
                    cs.ProjectId = item.ProjectId;
                    cs.UnitId = item.UnitId + "$" + item.UnitName;
                    cs.SolutionName = item.SolutionName;
                    cs.SolutionType = item.SolutionType;
                    cs.UnitWorkIds = item.UnitWorkIds + "$" + item.UnitWorkName;
                    cs.CNProfessionalCodes = item.CNProfessionalCodes + "$" + item.ProfessionalName;
                    cs.AttachUrl = item.AttachUrl;
                    cs.CompileMan = item.CompileMan + "$" + item.CompileManName;
                    cs.CompileDate = item.CompileDate;
                    cs.State = item.State;
                    cs.Edition = item.Edition;

                    res.Add(cs);
                }
                return res;
            }
        }
        public static void UpdateConstructSolutionForApi(Model.Solution_CQMSConstructSolution constructSolution)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Solution_CQMSConstructSolution newConstructSolution = db.Solution_CQMSConstructSolution.First(e => e.ConstructSolutionId == constructSolution.ConstructSolutionId);
                if (!string.IsNullOrEmpty(constructSolution.Code))
                    newConstructSolution.Code = constructSolution.Code;
                if (!string.IsNullOrEmpty(constructSolution.UnitId))
                    newConstructSolution.UnitId = constructSolution.UnitId;
                if (!string.IsNullOrEmpty(constructSolution.SolutionName))
                    newConstructSolution.SolutionName = constructSolution.SolutionName;
                if (!string.IsNullOrEmpty(constructSolution.SolutionType))
                    newConstructSolution.SolutionType = constructSolution.SolutionType;
                if (!string.IsNullOrEmpty(constructSolution.UnitWorkIds))
                    newConstructSolution.UnitWorkIds = constructSolution.UnitWorkIds;
                if (!string.IsNullOrEmpty(constructSolution.CNProfessionalCodes))
                    newConstructSolution.CNProfessionalCodes = constructSolution.CNProfessionalCodes;
                if (!string.IsNullOrEmpty(constructSolution.AttachUrl))
                    newConstructSolution.AttachUrl = constructSolution.AttachUrl;
                if (!string.IsNullOrEmpty(constructSolution.State))
                    newConstructSolution.State = constructSolution.State;
                if (constructSolution.Edition.HasValue)
                    newConstructSolution.Edition = constructSolution.Edition;
                db.SubmitChanges();
            }
        }
    }
}
