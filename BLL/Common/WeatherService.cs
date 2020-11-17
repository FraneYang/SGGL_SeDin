using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 天气记录
    /// </summary>
    public class WeatherService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取事故类型
        /// </summary>
        /// <param name="weatherId"></param>
        /// <returns></returns>
        public static Model.Weather GetWeatherByDateAndCity(DateTime date, string city)
        {
            return Funs.DB.Weather.FirstOrDefault(e => e.Date == date && e.City == city);
        }

        /// <summary>
        /// 添加事故类型
        /// </summary>
        /// <param name="weather"></param>
        public static void AddWeather(Model.Weather weather)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Weather newWeather = new Model.Weather
            {
                WeatherId = weather.WeatherId,
                City = weather.City,
                Date = weather.Date,
                WeatherRef = weather.WeatherRef,
                CurrTem = weather.CurrTem,
                AllTem = weather.AllTem
            };
            db.Weather.InsertOnSubmit(newWeather);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除事故类型
        /// </summary>
        /// <param name="weatherId"></param>
        public static void DeleteWeatherById(string weatherId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Weather weather = db.Weather.FirstOrDefault(e => e.WeatherId == weatherId);
            if (weather != null)
            {
                db.Weather.DeleteOnSubmit(weather);
                db.SubmitChanges();
            }
        }
    }
}
