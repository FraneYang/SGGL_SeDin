﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.UI.WebControls;

namespace BLL
{
    public class UnitWorkService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 添加单位工程信息
        /// </summary>
        /// <param name="WPQ"></param>
        public static void AddUnitWork(Model.WBS_UnitWork UnitWork)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_UnitWork newUnitWork = new Model.WBS_UnitWork();
            newUnitWork.UnitWorkId = UnitWork.UnitWorkId;
            newUnitWork.UnitWorkCode = UnitWork.UnitWorkCode;
            newUnitWork.UnitWorkName = UnitWork.UnitWorkName;
            newUnitWork.SuperUnitWork = UnitWork.SuperUnitWork;
            newUnitWork.IsChild = UnitWork.IsChild;
            newUnitWork.ProjectId = UnitWork.ProjectId;
            newUnitWork.ProjectType = UnitWork.ProjectType;
            newUnitWork.UnitId = UnitWork.UnitId;
            newUnitWork.SupervisorUnitId = UnitWork.SupervisorUnitId;
            newUnitWork.NDEUnit = UnitWork.NDEUnit;
            newUnitWork.Costs = UnitWork.Costs;
            newUnitWork.MainItemAndDesignProfessionalIds = UnitWork.MainItemAndDesignProfessionalIds;
            db.WBS_UnitWork.InsertOnSubmit(newUnitWork);
            db.SubmitChanges();
            GetWeights(UnitWork.ProjectId);
        }

        /// <summary>
        /// 修改单位工程信息
        /// </summary>
        /// <param name="WPQ"></param>
        public static void UpdateUnitWork(Model.WBS_UnitWork UnitWork)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_UnitWork newUnitWork = db.WBS_UnitWork.FirstOrDefault(e => e.UnitWorkId == UnitWork.UnitWorkId);
            if (newUnitWork != null)
            {
                newUnitWork.UnitWorkId = UnitWork.UnitWorkId;
                newUnitWork.UnitWorkCode = UnitWork.UnitWorkCode;
                newUnitWork.UnitWorkName = UnitWork.UnitWorkName;
                newUnitWork.SuperUnitWork = UnitWork.SuperUnitWork;
                newUnitWork.IsChild = UnitWork.IsChild;
                newUnitWork.ProjectId = UnitWork.ProjectId;
                newUnitWork.ProjectType = UnitWork.ProjectType;
                newUnitWork.UnitId = UnitWork.UnitId;
                newUnitWork.SupervisorUnitId = UnitWork.SupervisorUnitId;
                newUnitWork.NDEUnit = UnitWork.NDEUnit;
                newUnitWork.Costs = UnitWork.Costs;
                newUnitWork.MainItemAndDesignProfessionalIds = UnitWork.MainItemAndDesignProfessionalIds;
                db.SubmitChanges();
            }
            GetWeights(UnitWork.ProjectId);
            if (UnitWork.Costs != null)
            {
                UpdateWBSCosts(UnitWork.UnitWorkId, Convert.ToDecimal(UnitWork.Costs));
            }
        }

        private static void GetWeights(string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            decimal totalCosts = 0;
            var unitWorks = from x in db.WBS_UnitWork where x.ProjectId == projectId && x.Costs != null select x;
            foreach (var unitWork in unitWorks)
            {
                totalCosts += Convert.ToDecimal(unitWork.Costs);
            }
            foreach (var unitWork in unitWorks)
            {
                unitWork.Weights = unitWork.Costs / totalCosts * 100;
                db.SubmitChanges();
            }
            var noCostUnitWorks = from x in db.WBS_UnitWork where x.ProjectId == projectId && x.Costs == null select x;
            foreach (var noCostUnitWork in noCostUnitWorks)
            {
                noCostUnitWork.Weights = null;
                db.SubmitChanges();
            }
        }
        #region 更新计算单位工程WBS项的建安工程费
        /// <summary>
        /// 更新计算单位工程WBS项的建安工程费
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <param name="costs"></param>
        private static void UpdateWBSCosts(string unitWorkId, decimal costs)
        {
            Model.SGGLDB db = Funs.DB;
            var workPackages = from x in db.WBS_WorkPackage where x.UnitWorkId == unitWorkId && x.SuperWorkPackageId == null && x.IsApprove == true select x;
            foreach (var item in workPackages)
            {
                if (item.Weights != null)
                {
                    item.Costs = item.Weights / 100 * costs;
                    db.SubmitChanges();
                    UpdateWorkPackageCosts(item.WorkPackageId, Convert.ToDecimal(item.Costs));
                }
            }
        }

        private static void UpdateWorkPackageCosts(string workPackageId, decimal costs)
        {
            Model.SGGLDB db = Funs.DB;
            var childWorkPackages = from x in db.WBS_WorkPackage where x.SuperWorkPackageId == workPackageId && x.IsApprove == true select x;
            if (childWorkPackages.Count() > 0)   //存在子级
            {
                foreach (var item in childWorkPackages)
                {
                    if (item.Weights != null)
                    {
                        item.Costs = item.Weights / 100 * costs;
                        db.SubmitChanges();
                        UpdateWorkPackageCosts(item.WorkPackageId, Convert.ToDecimal(item.Costs));
                    }
                }
            }
            else
            {
                var controlItemAndCycles = from x in db.WBS_ControlItemAndCycle where x.WorkPackageId == workPackageId && x.IsApprove == true select x;
                foreach (var item in controlItemAndCycles)
                {
                    if (item.Weights != null)
                    {
                        item.Costs = item.Weights / 100 * costs;
                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 根据主键删除单位工程信息
        /// </summary>
        /// <param name="checkerId"></param>
        public static void DeleteUnitWorkById(string UnitWorkId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.WBS_UnitWork Unitwork = db.WBS_UnitWork.FirstOrDefault(e => e.UnitWorkId == UnitWorkId);
            if (Unitwork != null)
            {
                db.WBS_UnitWork.DeleteOnSubmit(Unitwork);
                db.SubmitChanges();
                GetWeights(Unitwork.ProjectId);
            }
        }

        /// <summary>
        /// 获取单位工程信息
        /// </summary>
        /// <param name="UnitWorkId"></param>
        /// <returns></returns>
        public static Model.WBS_UnitWork GetUnitWorkByUnitWorkId(string UnitWorkId)
        {
            return Funs.DB.WBS_UnitWork.FirstOrDefault(e => e.UnitWorkId == UnitWorkId);
        }

        /// <summary>
        /// 获取单位工程信息
        /// </summary>
        /// <param name="UnitWorkId"></param>
        /// <returns></returns>
        public static Model.WBS_UnitWork GetUnitWorkByMainItemAndDesignProfessionalIds(string mainItemAndDesignProfessionalIds)
        {
            return Funs.DB.WBS_UnitWork.FirstOrDefault(e => e.MainItemAndDesignProfessionalIds.Contains(mainItemAndDesignProfessionalIds));
        }

        /// <summary>
        /// 根据单位工程编号获取单位工程信息
        /// </summary>
        /// <param name="UnitWorkCode"></param>
        /// <returns></returns>
        public static List<Model.WBS_UnitWork> GetUnitWorkByUnitWorkCode(string UnitWorkCode)
        {
            return (from x in Funs.DB.WBS_UnitWork where x.UnitWorkCode == UnitWorkCode select x).ToList();
        }

        /// <summary>
        /// 下拉框选择(获取 text value)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextOrVal(string text, string val, string projectId)
        {
            string str = null;
            ListItem[] listitem = null;
            Cache cache = new Cache();
            if (cache.Get("UnitWorkList") == null)
            {
                listitem = GetUnitWork(projectId);
                if (listitem.Count() > 0)
                {
                    cache.Insert("UnitWorkList", listitem, null, DateTime.UtcNow.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
            }
            else
            {
                listitem = (ListItem[])cache.Get("UnitWorkList");
            }
            if (!string.IsNullOrWhiteSpace(text))
            {

                foreach (var item in listitem)
                {
                    if (text.Equals(item.Text))
                    {
                        str = item.Value;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(val))
            {
                foreach (var item in listitem)
                {
                    if (val.Equals(item.Value))
                    {
                        str = item.Text;
                    }
                }
            }

            return str;
        }
        /// <summary>
        /// 获取单位工程名称项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static ListItem[] GetUnitWorkList(string projectId)
        {
            List<Model.WBS_UnitWork> q = (from x in Funs.DB.WBS_UnitWork where x.ProjectId == projectId && x.SuperUnitWork == null orderby x.UnitWorkCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem((q[i].UnitWorkCode + "-" + q[i].UnitWorkName + GetProjectType(q[i].ProjectType)) ?? "", q[i].UnitWorkId.ToString());
            }
            return item;
        }
        /// <summary>
        /// 获取安装工程单位工程名称项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static ListItem[] GetAZUnitWorkList(string projectId)
        {
            List<Model.WBS_UnitWork> q = (from x in Funs.DB.WBS_UnitWork where x.ProjectId == projectId && x.SuperUnitWork == null && x.ProjectType == "2" orderby x.UnitWorkCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].UnitWorkCode + "-" + q[i].UnitWorkName, q[i].UnitWorkId.ToString());
            }
            return item;
        }
        /// <summary>
        /// 根据工程类型获取名称
        /// </summary>
        /// <param name="projectType"></param>
        /// <returns></returns>
        public static string GetProjectType(string projectType)
        {
            string name = string.Empty;
            if (projectType == "1")
            {
                name = "(建筑)";
            }
            else if (projectType == "2")
            {
                name = "(安装)";
            }
            return name;
        }
        /// <summary>
        /// 获取单位工程名称项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static List<Model.WBS_UnitWork> GetUnitWorkLists(string projectId)
        {
            return (from x in Funs.DB.WBS_UnitWork where x.ProjectId == projectId && x.SuperUnitWork == null orderby x.UnitWorkCode select x).ToList();
        }
        public static ListItem[] GetUnitWork(string projectId)
        {
            List<Model.WBS_UnitWork> q = (from x in Funs.DB.WBS_UnitWork where x.ProjectId == projectId && x.SuperUnitWork == null orderby x.UnitWorkCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem((q[i].UnitWorkCode + "-" + q[i].UnitWorkName + GetProjectType(q[i].ProjectType)) ?? "", q[i].UnitWorkId.ToString());
            }
            return item;
        }
        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static string GetNameById(string id)
        {
            string name = string.Empty;
            var UnitWork = Funs.DB.WBS_UnitWork.FirstOrDefault(x => x.UnitWorkId == id);
            if (UnitWork != null)
            {
                name = UnitWork.UnitWorkName + GetProjectType(UnitWork.ProjectType);
            }
            return name;
        }


        /// <summary>
        ///  单位工程表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitWorkDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = BLL.UnitWorkService.GetUnitWorkList(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        ///  安装工程单位工程表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitAZUnitWorkDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = BLL.UnitWorkService.GetAZUnitWorkList(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        ///  单位工程表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitWorkDownListByProjectType(FineUIPro.DropDownList dropName, string projectId, string projectType, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = BLL.UnitWorkService.GetUnitWorkListByProjectType(projectId, projectType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        ///  单位工程表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitWorkList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetUnitWork(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 根据项目id 获取单位工程
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.WBS_UnitWork> GetUnitWorkListByPid(string projectId)
        {
            Model.SGGLDB db = Funs.DB;
            List<Model.WBS_UnitWork> q = (from x in db.WBS_UnitWork where x.ProjectId == projectId && x.SuperUnitWork == null orderby x.UnitWorkCode select x).ToList();
            return q;
        }
        public static Model.WBS_UnitWork getUnitWorkByUnitWorkId(string UnitWorkId)
        {
            return Funs.DB.WBS_UnitWork.FirstOrDefault(e => e.UnitWorkId.ToString() == UnitWorkId);
        }
        public static List<Model.WBS_UnitWork> GetUnitWorkListByPidForApi(string projectId, string projectType)
        {
            string[] type = null;
            if (!string.IsNullOrEmpty(projectType))
                type = projectType.Split(',');
            var unitWorks = from x in Funs.DB.WBS_UnitWork where x.ProjectId == projectId && (type == null || type.Contains(x.ProjectType)) orderby x.UnitWorkCode select x;
            return unitWorks.ToList();
        }
        /// <summary>
        /// 获取单位工程名称项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static ListItem[] GetUnitWorkListByProjectType(string projectId, string projectType)
        {
            List<Model.WBS_UnitWork> q = (from x in Funs.DB.WBS_UnitWork where x.ProjectId == projectId && x.SuperUnitWork == null && x.ProjectType == projectType orderby x.UnitWorkCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem((q[i].UnitWorkCode + "-" + q[i].UnitWorkName + GetProjectType(q[i].ProjectType)) ?? "", q[i].UnitWorkId.ToString());
            }
            return item;
        }

        /// <summary>
        /// 根据单位工程Id获取对应code的所有单位工程Id集合
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static string GetUnitWorkIdsByUnitWorkId(string unitWorkId)
        {
            string unitWorkIds = string.Empty;
            Model.WBS_UnitWork unitWork = GetUnitWorkByUnitWorkId(unitWorkId);
            if (unitWork != null)
            {
                var q = from x in Funs.DB.WBS_UnitWork where x.UnitWorkCode == unitWork.UnitWorkCode select x;
                foreach (var item in q)
                {
                    unitWorkIds += item.UnitWorkId + ",";
                }
            }
            if (!string.IsNullOrEmpty(unitWorkIds))
            {
                unitWorkIds = unitWorkIds.Substring(0, unitWorkIds.LastIndexOf(","));
            }
            return unitWorkIds;
        }

        /// <summary>
        /// 根据单位工程类别获取单位工程
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<Model.WBS_UnitWork> GetUnitWorkDownList(string ProjectType, string ProjectId)
        {
            if (ProjectType == "1")
            {
                List<Model.WBS_UnitWork> lis = (from x in Funs.DB.WBS_UnitWork where x.ProjectType == "1" && x.ProjectId == ProjectId orderby x.UnitWorkCode select x).ToList();
                return lis;
            }
            else if (ProjectType == "2")
            {
                List<Model.WBS_UnitWork> lis = (from x in Funs.DB.WBS_UnitWork where x.ProjectType == "2" && x.ProjectId == ProjectId orderby x.UnitWorkCode select x).ToList();
                return lis;
            }
            else
                return null;
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
                        unitWorkName += un.UnitWorkName + GetProjectType(un.ProjectType) + ",";
                    }
                }
                if (!string.IsNullOrEmpty(unitWorkName))
                {
                    unitWorkName = unitWorkName.Substring(0, unitWorkName.LastIndexOf(","));
                }
            }
            return unitWorkName;
        }

        /// <summary>
        /// 获取单位工程下拉列表
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitUnitWorkDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            var unitWorks = from x in Funs.DB.WBS_UnitWork
                            where x.ProjectId == projectId && x.SuperUnitWork == null
                            orderby x.UnitWorkCode
                            select new
                            {
                                x.UnitWorkId,
                                UnitWorkName = GetUnitWorkALLName(x.UnitWorkId)
                            };

            dropName.DataValueField = "UnitWorkId";
            dropName.DataTextField = "UnitWorkName";
            dropName.DataSource = unitWorks;
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 获取单位工程下拉列表
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitUnitWorkNameDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            var unitWorks = from x in Funs.DB.WBS_UnitWork
                            where x.ProjectId == projectId && x.SuperUnitWork == null
                            orderby x.UnitWorkCode
                            select new
                            {
                                x.UnitWorkId,
                                UnitWorkName = GetUnitWorkALLName(x.UnitWorkId)
                            };

            dropName.DataValueField = "UnitWorkName";
            dropName.DataTextField = "UnitWorkName";
            dropName.DataSource = unitWorks;
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 获取单位工程名称
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static string GetUnitWorkALLName(string unitWorkId)
        {
            string name = string.Empty;
            var getu = Funs.DB.WBS_UnitWork.FirstOrDefault(x => x.UnitWorkId == unitWorkId && x.SuperUnitWork == null);
            if (getu != null)
            {
                if (!string.IsNullOrEmpty(getu.ProjectType))
                {
                    name = getu.UnitWorkName + "(" + Funs.GetUnitWorkType(getu.ProjectType) + ")";
                }
                else
                {
                    name = getu.UnitWorkName;
                }
            }
            return name;
        }

        /// <summary>
        /// 获取主项及设计专业名称
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static string GetMainItemAndDesignProfessionalName(string str, string projectId)
        {
            string name = string.Empty;
            Model.SGGLDB db = Funs.DB;
            var mainItems = from x in db.ProjectData_MainItem where x.ProjectId == projectId select x;
            var designProfessionals = from x in db.Base_DesignProfessional select x;
            if (!string.IsNullOrEmpty(str))
            {
                string[] ids = str.Split(',');
                string mainItemId = string.Empty;
                foreach (var id in ids)
                {
                    string[] strs = id.Split('|');
                    if (mainItemId != strs[0])   //新的主项内容
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            name = name.Substring(0, name.Length - 1) + "),";
                        }
                        var mainItem = mainItems.FirstOrDefault(x => x.MainItemId == strs[0]);
                        if (mainItem != null)
                        {
                            name += mainItem.MainItemName + "(";
                        }
                        var designProfessional = designProfessionals.FirstOrDefault(x => x.DesignProfessionalId == strs[1]);
                        if (designProfessional != null)
                        {
                            name += designProfessional.ProfessionalName + ",";
                        }
                    }
                    else
                    {
                        var designProfessional = designProfessionals.FirstOrDefault(x => x.DesignProfessionalId == strs[1]);
                        if (designProfessional != null)
                        {
                            name += designProfessional.ProfessionalName + ",";
                        }
                    }
                    mainItemId = strs[0];
                }
                if (!string.IsNullOrEmpty(name))
                {
                    name = name.Substring(0, name.Length - 1) + ")";
                }
            }
            return name;
        }
    }
}
