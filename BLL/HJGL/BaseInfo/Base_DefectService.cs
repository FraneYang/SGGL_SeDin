using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_DefectService
    {
        /// <summary>
        ///获取缺陷性质信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Defect GetDefectByDefectId(string defectId)
        {
            return Funs.DB.Base_Defect.FirstOrDefault(e => e.DefectId.ToString() == defectId);
        }

        /// <summary>
        ///根据缺陷名称字符串获取缺陷性质Id字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDefectIdStrByDefectNameStr(string defectNameStr)
        {
            string defectIdStr = string.Empty;
            var defects = from x in Funs.DB.Base_Defect select x;
            string[] strs = defectNameStr.Split(',');
            foreach (var str in strs)
            {
                var d = defects.FirstOrDefault(x=>x.DefectName==str);
                if (d != null)
                {
                    defectIdStr += d.DefectId.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(defectIdStr))
            {
                defectIdStr = defectIdStr.Substring(0, defectIdStr.LastIndexOf(","));
            }
            return defectIdStr;
        }

        /// <summary>
        ///根据缺陷名称(英文)字符串获取缺陷性质Id字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDefectIdStrByDefectEngNameStr(string defectEngNameStr)
        {
            string defectIdStr = string.Empty;
            var defects = from x in Funs.DB.Base_Defect select x;
            string[] strs = defectEngNameStr.Split(',');
            foreach (var str in strs)
            {
                var d = defects.FirstOrDefault(x => x.DefectEngName == str);
                if (d != null)
                {
                    defectIdStr += d.DefectId.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(defectIdStr))
            {
                defectIdStr = defectIdStr.Substring(0, defectIdStr.LastIndexOf(","));
            }
            return defectIdStr;
        }

        /// <summary>
        ///根据缺陷性质Id字符串获取缺陷名称字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDefectNameStrByDefectIdStr(string defectIdStr)
        {
            string defectNameStr = string.Empty;
            var defects = from x in Funs.DB.Base_Defect select x;
            string[] strs = defectIdStr.Split(',');
            foreach (var str in strs)
            {
                var d = defects.FirstOrDefault(x => x.DefectId.ToString() == str);
                if (d != null)
                {
                    defectNameStr += d.DefectName.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(defectNameStr))
            {
                defectNameStr = defectNameStr.Substring(0, defectNameStr.LastIndexOf(","));
            }
            return defectNameStr;
        }

        /// <summary>
        ///根据缺陷性质Id字符串获取缺陷名称(英文)字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDefectEngNameStrByDefectIdStr(string defectIdStr)
        {
            string defectEngNameStr = string.Empty;
            var defects = from x in Funs.DB.Base_Defect select x;
            string[] strs = defectIdStr.Split(',');
            foreach (var str in strs)
            {
                var d = defects.FirstOrDefault(x => x.DefectId.ToString() == str);
                if (d != null)
                {
                    defectEngNameStr += d.DefectEngName.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(defectEngNameStr))
            {
                defectEngNameStr = defectEngNameStr.Substring(0, defectEngNameStr.LastIndexOf(","));
            }
            return defectEngNameStr;
        }

        /// <summary>
        /// 增加缺陷性质信息
        /// </summary>
        /// <param name="Defect"></param>
        public static void AddDefect(Model.Base_Defect defect)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Defect newDefect = new Base_Defect
            {
                DefectId = defect.DefectId,
                DefectName = defect.DefectName,
                DefectEngName = defect.DefectEngName,
            };
            db.Base_Defect.InsertOnSubmit(newDefect);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改缺陷性质信息 
        /// </summary>
        /// <param name="Defect"></param>
        public static void UpdateDefect(Model.Base_Defect defect)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Defect newDefect = db.Base_Defect.FirstOrDefault(e => e.DefectId == defect.DefectId);
            if (newDefect != null)
            {
                newDefect.DefectName = defect.DefectName;
                newDefect.DefectEngName = defect.DefectEngName;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据缺陷性质Id删除一个缺陷性质信息
        /// </summary>
        /// <param name="DefectId"></param>
        public static void DeleteDefectByDefectId(string defectId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Defect delDefect = db.Base_Defect.FirstOrDefault(e => e.DefectId.ToString() == defectId);
            if (delDefect != null)
            {
                db.Base_Defect.DeleteOnSubmit(delDefect);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取缺陷性质项
        /// </summary>
        /// <param name="DefectType"></param>
        /// <returns></returns>
        public static List<Model.Base_Defect> GetDefectList()
        {
            var list = (from x in Funs.DB.Base_Defect
                        orderby x.DefectId
                        select x).ToList();

            return list;
        }

        #region 缺陷性质下拉项
        /// <summary>
        /// 缺陷性质下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="DefectType">耗材类型</param>
        public static void InitDefectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string itemText)
        {
            dropName.DataValueField = "DefectName";
            dropName.DataTextField = "DefectName";
            dropName.DataSource = GetDefectList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }

        /// <summary>
        /// 英文缺陷性质下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="DefectType">耗材类型</param>
        public static void InitEngDefectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string itemText)
        {
            dropName.DataValueField = "DefectEngName";
            dropName.DataTextField = "DefectEngName";
            dropName.DataSource = GetDefectList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }
        #endregion
    }
}
