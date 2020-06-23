using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// .net时间操作（星期-周）
    /// </summary>
    public class WeekDayService
    { 
             /// <summary> 
             /// 取指定日期是一年中的第几周 
             /// </summary> 
             /// <param name="dtime">给定的日期</param> 
             /// <returns>数字 一年中的第几周</returns> 
             public static int WeekOfYear(DateTime dtime)
             {
                 try
                 {
                    //确定此时间在一年中的位置
                   int dayOfYear = dtime.DayOfYear;
                    //当年第一天
                    DateTime tempDate = new DateTime(dtime.Year, 1, 1);
                   //确定当年第一天
                    int tempDayOfWeek = (int)tempDate.DayOfWeek;
                tempDayOfWeek = tempDayOfWeek == 0 ? 7 : tempDayOfWeek;
                ////确定星期几
                int index = (int)dtime.DayOfWeek;
                index = index == 0 ? 7 : index;
     
                    //当前周的范围
                    DateTime retStartDay = dtime.AddDays(-(index - 1));
                    DateTime retEndDay = dtime.AddDays( - index);
     
                    //确定当前是第几周
                   int weekIndex = (int)Math.Ceiling(((double)dayOfYear + tempDayOfWeek - 1) / 7);
    
                   if (retStartDay.Year<retEndDay.Year)
                   {
                       weekIndex = 1;
                   }
    
                   return weekIndex;
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message);
               }
    
          }
    
    
           /// <summary>
           /// 求某年有多少周
           /// </summary>
           /// <param name="dtime"></param>
           /// <returns></returns>
           public static int GetWeekOfYear(DateTime dtime)
           {
               int countDay = DateTime.Parse(dtime.Year + "--").DayOfYear;
              int countWeek = countDay / 7;
               return countWeek;
           }

        //根据年月日获得星期几
        public static int CaculateWeekDay(DateTime? date)
        {
            int week = 0;
            if (date.HasValue)
            {
                int year = date.Value.Year;
                int month = date.Value.Month;
                int day = date.Value.Day;
                //把一月和二月看成是上一年的十三月和十四月
                if (month == 1) { month = 13; year--; }
                if (month == 2) { month = 14; year--; }
                week = (day + 2 * month + 3 * (month + 1) / 5 + year + year / 4 - year / 100 + year / 400) % 7;
            }
            return week + 1;
        }

        //根据年月日获得星期几
        public static string CaculateWeekDayT(DateTime? date)
        {
            string weekStr = "星期";
            int week = CaculateWeekDay(date);
            if (week == 1)
            {
                weekStr += "一";
            }
            else if (week == 2)
            {
                weekStr += "二";
            }
            else if (week == 3)
            {
                weekStr += "三";
            }
            else if (week == 4)
            {
                weekStr += "四";
            }
            else if (week == 5)
            {
                weekStr += "五";
            }
            else if (week == 6)
            {
                weekStr += "六";
            }
            else if (week == 7)
            {
                weekStr += "日";
            }
            return weekStr;
        }

        /// <summary>
        /// 根据2个时间段获得相应的周数
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int WeekOfDate(DateTime beginDate, DateTime endDate)
          {
              TimeSpan ts1 = new TimeSpan(beginDate.Ticks);
              TimeSpan ts2 = new TimeSpan(endDate.Ticks);
              TimeSpan ts = ts2.Subtract(ts1).Duration();
              int weeks = ts.Days / 7;
   
             //确定此时间在一年中的位置
               int dayOfYear = beginDate.DayOfYear;
              //当年第一天
               DateTime tempDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);
             //最后一天
               DateTime tempendDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);
              int tempDayOfWeek = (int)tempDate.DayOfWeek;
               tempDayOfWeek = tempDayOfWeek == 0 ? 7: tempDayOfWeek;
    
              ////确定星期几
              int startindex = (int)beginDate.DayOfWeek;
              startindex = startindex == 0 ? 7 : startindex;
              //当前周的范围
              DateTime retStartDay = beginDate.AddDays(-(startindex - 1));
              DateTime retEndDay = beginDate.AddDays(7 - startindex);
              //确定当前是第几周
              int weekIndex = (int)Math.Ceiling(((double)dayOfYear + tempDayOfWeek - 1) / 7);
   
              return weeks;
           }
   
           /// <summary>
           /// 根据起始时间，获取第几周
          /// </summary>
        /// <param name="dtime">当前时间</param>
         /// <returns></returns>
         public static int WeekOfTermDate(DateTime dtime)
         {
             string datetime = "2011-3-1";
  
             TimeSpan ts1 = new TimeSpan(dtime.Ticks);
             TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(datetime).Ticks);
             TimeSpan ts = ts2.Subtract(ts1).Duration();
  
             //确定此时间在一年中的位置
             int dayOfYear = ts.Days;
             //当年第一天
              DateTime tempDate = new DateTime(Convert.ToDateTime(datetime).Year, Convert.ToDateTime(datetime).Month, Convert.ToDateTime(datetime).Day);
   
              int tempDayOfWeek = (int)tempDate.DayOfWeek;
              tempDayOfWeek = tempDayOfWeek == 0 ? 7 :tempDayOfWeek;
              ////确定星期几
              int index = (int)dtime.DayOfWeek;
              index = index == 0 ? 7 :index;
   
              //当前周的范围
             DateTime retStartDay = dtime.AddDays(-(index - 1));
              DateTime retEndDay = dtime.AddDays(7 - index);
   
              //确定当前是第几周
              int weekIndex = (int)Math.Ceiling(((double)dayOfYear + tempDayOfWeek) / 7);
              return weekIndex;
          }
   
          /// <summary>
          /// 根据周，星期获得具体年月日
          /// </summary>
          /// <param name="week">第几周</param>
          /// <param name="day">星期几</param>
          /// <returns></returns>
          public static DateTime DateTimeByWeekAndDay(int week, int day)
          {
              DateTime someTime = Convert.ToDateTime("2011-3-1");
   
              int i = someTime.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
   
              //获取第N周的星期一
              someTime = someTime.Subtract(ts).AddDays((week - 1) * 7);
              //获得星期几
              someTime = someTime.AddDays(day - 1);
             return someTime;
          }
   
          /// <summary>
          /// 获取一年中指定的一周的开始日期和结束日期。开始日期遵循ISO 8601即星期一。
          /// </summary>
          /// <param name="year">年（1 到 9999）</param>
          /// <param name="weeks">周（1 到 53）</param>
          /// <param name="weekrule">确定首周的规则</param>
          /// <param name="first">当此方法返回时，则包含参数 year 和 weeks 指定的周的开始日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。如果参数 year 或 weeks 超出有效范围，则操作失败。该参数未经初始化即被传递。</param>
          /// <param name="last">当此方法返回时，则包含参数 year 和 weeks 指定的周的结束日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。如果参数 year 或 weeks 超出有效范围，则操作失败。该参数未经初始化即被传递。</param>
          /// <returns>成功返回 true，否则为 false。</returns>
          public static bool GetDaysOfWeeks(int year, int weeks, CalendarWeekRule weekrule, out DateTime first, out DateTime last)
          {
              //初始化 out 参数
              first = DateTime.MinValue;
              last = DateTime.MinValue;
  
              //不用解释了吧...
              if (year< 1 | year> 9999)
                  return false;
   
              //一年最多53周地球人都知道...
              if (weeks< 1 | weeks> 53)
                  return false;
   
              //取当年首日为基准...为什么？容易得呗...
              DateTime firstCurr = new DateTime(year, 1, 1);
              //取下一年首日用于计算...
              DateTime firstNext = new DateTime(year + 1, 1, 1);
   
              //将当年首日星期几转换为数字...星期日特别处理...ISO 8601 标准...
              int dayOfWeekFirst = (int)firstCurr.DayOfWeek;
              if (dayOfWeekFirst == 0) dayOfWeekFirst = 7;
   
              //得到未经验证的周首日...
              first = firstCurr.AddDays((weeks - 1) * 7 - dayOfWeekFirst + 1);
   
              //周首日是上一年日期的情况...
              if (first.Year<year)
              {
                  switch (weekrule)
                  {
                      case CalendarWeekRule.FirstDay:
                          //不用解释了吧...
                          first = firstCurr;
                          break;
                       case CalendarWeekRule.FirstFullWeek:
                          //顺延一周...
                           first = first.AddDays(7);
                           break;
                      case CalendarWeekRule.FirstFourDayWeek:
                          //周首日距年首日不足4天则顺延一周...
                          if (firstCurr.Subtract(first).Days > 3)
                          {
                              first = first.AddDays(7);
                          }
                          break;
                      default:
                          break;
                  }
              }
              //得到未经验证的周末日...
              last = first.AddDays(7).AddSeconds(-1);
              switch (weekrule)
              {
                  case CalendarWeekRule.FirstDay:
                      //周末日是下一年日期的情况... 
                      if (last.Year > year)
                          last = firstNext.AddSeconds(-1);
                      else if (last.DayOfWeek != DayOfWeek.Monday)
                          last = first.AddDays(7 - (int) first.DayOfWeek).AddSeconds(-1);
                      break;
                  case CalendarWeekRule.FirstFourDayWeek:
                      //周末日距下一年首日不足4天则提前一周... 
                      if (last.Year > year && firstNext.Subtract(first).Days< 4)
                      {
                          first = first.AddDays(-7);
                          last = last.AddDays(-7);
                      }
                      break;
                  default:
                      break;
              }
              return true;
          }    
    }
}