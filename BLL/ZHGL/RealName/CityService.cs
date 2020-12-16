using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class CityService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取国家信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static Model.RealName_City GetCityById(string cityId)
        {
            return Funs.DB.RealName_City.FirstOrDefault(e => e.ID == cityId);
        }

        /// <summary>
        /// 添加国家信息
        /// </summary>
        /// <param name="city"></param>
        public static void AddCity(Model.RealName_City city)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_City newCity = new Model.RealName_City
            {
                ID = city.ID,
                ProvinceCode = city.ProvinceCode,
                CityCode = city.CityCode,
                Cname = city.Cname,
                CnShortName = city.CnShortName,
                Name = city.Name,
                CountryId = city.CountryId
            };
            db.RealName_City.InsertOnSubmit(newCity);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改国家信息
        /// </summary>
        /// <param name="city"></param>
        public static void UpdateCity(Model.RealName_City city)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_City newCity = db.RealName_City.FirstOrDefault(e => e.ID == city.ID);
            if (newCity != null)
            {
                newCity.ProvinceCode = city.ProvinceCode;
                newCity.CityCode = city.CityCode;
                newCity.Cname = city.Cname;
                newCity.CnShortName = city.CnShortName;
                newCity.Name = city.Name;
                newCity.CountryId = city.CountryId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除国家信息
        /// </summary>
        /// <param name="cityId"></param>
        public static void DeleteCityById(string cityId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_City city = db.RealName_City.FirstOrDefault(e => e.ID == cityId);
            if (city != null)
            {
                db.RealName_City.DeleteOnSubmit(city);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据国家类型Id获取国家下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.RealName_City> GetCityList(string countryId)
        {
            return (from x in Funs.DB.RealName_City where x.CountryId == countryId orderby x.Cname select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitCityDropDownList(FineUIPro.DropDownList dropName, string countryId, bool isShowPlease)
        {
            dropName.DataValueField = "ProvinceCode";
            dropName.DataTextField = "Cname";
            dropName.DataSource = GetCityList(countryId);
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
        public static string GetCNameByProvinceCode(string ProvinceCode)
        {
            string name = string.Empty;
            var City = Funs.DB.RealName_City.FirstOrDefault(x => x.ProvinceCode == ProvinceCode);
            if (City != null)
            {
                name = City.Cname;
            }
            return name;
        }
    }
}
