using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class CountryService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取国家信息
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static Model.RealName_Country GetCountryById(string id)
        {
            return Funs.DB.RealName_Country.FirstOrDefault(e => e.ID == id);
        }

        /// <summary>
        /// 根据国家类型Id获取国家下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.RealName_Country> GetCountryList()
        {
            return (from x in Funs.DB.RealName_Country orderby x.Cname select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitCountryDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ID";
            dropName.DataTextField = "Cname";
            dropName.DataSource = GetCountryList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 获取国家名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static string GetCNameByCountryId(string countryId)
        {
            string name = string.Empty;
            var Country = Funs.DB.RealName_Country.FirstOrDefault(x => x.CountryId == countryId);
            if (Country != null)
            {
                name = Country.Cname;
            }
            return name;
        }
    }
}
