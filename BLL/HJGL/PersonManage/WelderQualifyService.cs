using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 焊工资质
    /// </summary>
    public static class WelderQualifyService
    {
        /// <summary>
        /// 根据主键获取焊工资质信息
        /// </summary>
        /// <param name="welderQualifyId"></param>
        /// <returns></returns>
        public static Model.Welder_WelderQualify GetWelderQualifyById(string welderQualifyId)
        {
            return new Model.SGGLDB(Funs.ConnString).Welder_WelderQualify.FirstOrDefault(e => e.WelderQualifyId == welderQualifyId);
        }

        /// <summary>
        /// 根据主键获取焊工资质视图
        /// </summary>
        /// <param name="welderQualifyId"></param>
        /// <returns></returns>
        public static Model.View_Welder_WelderQualify GetViewWelderQualifyById(string welderQualifyId)
        {
            return new Model.SGGLDB(Funs.ConnString).View_Welder_WelderQualify.FirstOrDefault(e => e.WelderQualifyId == welderQualifyId);
        }

        /// <summary>
        /// 根据焊工主键获取焊工资质信息
        /// </summary>
        /// <param name="welderQualifyId"></param>
        /// <returns></returns>
        public static List<Model.Welder_WelderQualify> GetWelderQualifysByWelderId(string welderId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Welder_WelderQualify where x.WelderId == welderId select x).ToList();
        }

        public static List<Model.Welder_WelderQualify> GetShowWelderQualifysByWelderId(string welderId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Welder_WelderQualify where x.WelderId == welderId && x.IsPrintShow == true select x).ToList();
        }

        /// <summary>
        /// 添加焊工资质信息
        /// </summary>
        /// <param name="welderQualify"></param>
        public static void AddWelderQualify(Model.Welder_WelderQualify welderQualify)
        {
            Model.Welder_WelderQualify newWelderQualify = new Model.Welder_WelderQualify();
            newWelderQualify.WelderQualifyId = welderQualify.WelderQualifyId;
            newWelderQualify.WelderId = welderQualify.WelderId;
            newWelderQualify.QualificationItem = welderQualify.QualificationItem;
            newWelderQualify.CheckDate = welderQualify.CheckDate;
            newWelderQualify.LimitDate = welderQualify.LimitDate;
            newWelderQualify.WeldingMethod = welderQualify.WeldingMethod;
            newWelderQualify.MaterialType = welderQualify.MaterialType;
            newWelderQualify.WeldingLocation = welderQualify.WeldingLocation;
            newWelderQualify.ThicknessMin = welderQualify.ThicknessMin;
            newWelderQualify.ThicknessMax = welderQualify.ThicknessMax;
            newWelderQualify.SizesMin = welderQualify.SizesMin;
            newWelderQualify.SizesMax = welderQualify.SizesMax;
            newWelderQualify.Remark = welderQualify.Remark;
            newWelderQualify.IsPrintShow = welderQualify.IsPrintShow;
            new Model.SGGLDB(Funs.ConnString).Welder_WelderQualify.InsertOnSubmit(newWelderQualify);
            new Model.SGGLDB(Funs.ConnString).SubmitChanges();
        }

        /// <summary>
        /// 修改焊工资质
        /// </summary>
        /// <param name="welderQualify"></param>
        public static void UpdateWelderQualify(Model.Welder_WelderQualify welderQualify)
        {
            Model.Welder_WelderQualify newWelderQualify = new Model.SGGLDB(Funs.ConnString).Welder_WelderQualify.FirstOrDefault(e => e.WelderQualifyId == welderQualify.WelderQualifyId);
            if (newWelderQualify != null)
            {
                newWelderQualify.QualificationItem = welderQualify.QualificationItem;
                newWelderQualify.CheckDate = welderQualify.CheckDate;
                newWelderQualify.LimitDate = welderQualify.LimitDate;
                newWelderQualify.WeldingMethod = welderQualify.WeldingMethod;
                newWelderQualify.MaterialType = welderQualify.MaterialType;
                newWelderQualify.WeldingLocation = welderQualify.WeldingLocation;
                newWelderQualify.ThicknessMin = welderQualify.ThicknessMin;
                newWelderQualify.ThicknessMax = welderQualify.ThicknessMax;
                newWelderQualify.SizesMin = welderQualify.SizesMin;
                newWelderQualify.SizesMax = welderQualify.SizesMax;
                newWelderQualify.Remark = welderQualify.Remark;
                newWelderQualify.IsPrintShow = welderQualify.IsPrintShow;
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除焊工资质
        /// </summary>
        /// <param name="welderQualifyId"></param>
        public static void DeleteWelderQualifyById(string welderQualifyId)
        {
            Model.Welder_WelderQualify welderQualify = new Model.SGGLDB(Funs.ConnString).Welder_WelderQualify.FirstOrDefault(e => e.WelderQualifyId == welderQualifyId);
            if (welderQualify != null)
            {
                new Model.SGGLDB(Funs.ConnString).Welder_WelderQualify.DeleteOnSubmit(welderQualify);
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }
    }
}
