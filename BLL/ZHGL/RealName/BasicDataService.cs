using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class BasicDataService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取字典信息
        /// </summary>
        /// <param name="basicDataId"></param>
        /// <returns></returns>
        public static Model.RealName_BasicData GetBasicDataById(string basicDataId)
        {
            return Funs.DB.RealName_BasicData.FirstOrDefault(e => e.BasicDataId == basicDataId);
        }

        /// <summary>
        /// 添加字典信息
        /// </summary>
        /// <param name="basicData"></param>
        public static void AddBasicData(Model.RealName_BasicData basicData)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_BasicData newBasicData = new Model.RealName_BasicData
            {
                BasicDataId = basicData.BasicDataId,
                DictTypeCode = basicData.DictTypeCode,
                DictCode = basicData.DictCode,
                DictName = basicData.DictName
            };
            db.RealName_BasicData.InsertOnSubmit(newBasicData);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改字典信息
        /// </summary>
        /// <param name="basicData"></param>
        public static void UpdateBasicData(Model.RealName_BasicData basicData)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_BasicData newBasicData = db.RealName_BasicData.FirstOrDefault(e => e.BasicDataId == basicData.BasicDataId);
            if (newBasicData != null)
            {
                newBasicData.DictTypeCode = basicData.DictTypeCode;
                newBasicData.DictCode = basicData.DictCode;
                newBasicData.DictName = basicData.DictName;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除字典信息
        /// </summary>
        /// <param name="basicDataId"></param>
        public static void DeleteBasicDataById(string basicDataId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_BasicData basicData = db.RealName_BasicData.FirstOrDefault(e => e.BasicDataId == basicDataId);
            if (basicData != null)
            {
                db.RealName_BasicData.DeleteOnSubmit(basicData);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据字典类型Id获取字典下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.RealName_BasicData> GetBasicDataListByDictTypeCode(string dictTypeCode)
        {
            return (from x in Funs.DB.RealName_BasicData where x.DictTypeCode == dictTypeCode orderby x.DictCode select x).ToList();
        }

        /// <summary>
        /// 根据字典类型Id获取字典下拉选择项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetBasicDataCodeAndNameListByDictTypeCode(string dictTypeCode)
        {
            var q= (from x in Funs.DB.RealName_BasicData where x.DictTypeCode == dictTypeCode orderby x.DictCode select x).ToList();
            ListItem[] lis = new ListItem[q.Count];
            for (int i = 0; i < q.Count; i++)
            {
                lis[i] = new ListItem(q[i].DictCode + "(" + q[i].DictName + ")", q[i].DictCode);
            }
            return lis;
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitBasicDataProjectUnitDropDownList(FineUIPro.DropDownList dropName, string dictTypeCode, bool isShowPlease)
        {
            dropName.DataValueField = "DictCode";
            dropName.DataTextField = "DictName";
            dropName.DataSource = GetBasicDataListByDictTypeCode(dictTypeCode);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitBasicDataCodeAndNameDropDownList(FineUIPro.DropDownList dropName, string dictTypeCode, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetBasicDataCodeAndNameListByDictTypeCode(dictTypeCode);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 获取字典名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static string GetDictNameByDictCode(string DictCode)
        {
            string name = string.Empty;
            var BasicData = Funs.DB.RealName_BasicData.FirstOrDefault(x => x.DictCode == DictCode);
            if (BasicData != null)
            {
                name = BasicData.DictName;
            }
            return name;
        }
    }
}
