namespace BLL
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Linq;
    using System.Globalization;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// 通用方法类。
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// 维护一个DB集合
        /// </summary>
        private static Dictionary<int, Model.SGGLDB> dataBaseLinkList = new System.Collections.Generic.Dictionary<int, Model.SGGLDB>();


        /// <summary>
        /// 维护一个DB集合
        /// </summary>
        public static System.Collections.Generic.Dictionary<int, Model.SGGLDB> DBList
        {
            get
            {
                return dataBaseLinkList;
            }
        }


        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string connString;

        /// <summary>
        /// 数据库连结字符串。
        /// </summary>
        public static string ConnString
        {
            get
            {
                if (connString == null)
                {
                    throw new NotSupportedException("请设置连接字符串！");
                }

                return connString;
            }

            set
            {
                if (connString != null)
                {
                    throw new NotSupportedException("连接已设置！");
                }

                connString = value;
            }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SystemName
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器路径
        /// </summary>
        public static string RootPath
        {
            get;
            set;
        }

        /// <summary>
        /// 集团服务器路径
        /// </summary>
        public static string CNCECPath
        {
            get;
            set;
        }

        /// <summary>
        /// 软件版本
        /// </summary>
        public static string SystemVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 每页数量
        /// </summary>
        public static int PageSize
        {
            get;
            set;
        } = 15;

        /// <summary>
        /// 数据库上下文。
        /// </summary>
        public static SGGLDB DB
        {
            get
            {
                if (!DBList.ContainsKey(System.Threading.Thread.CurrentThread.ManagedThreadId))
                {
                    DBList.Add(System.Threading.Thread.CurrentThread.ManagedThreadId, new SGGLDB(connString));
                }

                // DBList[System.Threading.Thread.CurrentThread.ManagedThreadId].CommandTimeout = 1200;
                return DBList[System.Threading.Thread.CurrentThread.ManagedThreadId];
            }
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">加密前的密码</param>
        /// <returns>加密后的密码</returns>
        public static string EncryptionPassword(string password)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", null);

            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        /// <summary>
        /// 为目标下拉框加上 "请选择" 项
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void PleaseSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- 请选择 -", "0"));
            return;
        }

        /// <summary>
        /// 为目标下拉框加上 "请选择" 项
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem("- 请选择 -", BLL.Const._Null));
            return;
        }
              
        /// <summary>
        /// 为目标下拉框加上选择内容
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL, string text)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem(text, BLL.Const._Null));
            return;
        }

        /// <summary>
        /// 为目标下拉框加上 "重新编制" 项
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void ReCompileSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("重新编制", "0"));
            return;
        }

        /// <summary>
        /// 页码下拉框
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void DropDownPageSize(FineUIPro.DropDownList DDL)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem("10", "10"));
            DDL.Items.Insert(1, new FineUIPro.ListItem("20", "20", true));
            DDL.Items.Insert(2, new FineUIPro.ListItem("30", "30"));
            DDL.Items.Insert(3, new FineUIPro.ListItem("50", "50"));
            DDL.Items.Insert(4, new FineUIPro.ListItem("所有行", "1000000"));
            return;
        }

        /// <summary>
        /// 字符串是否为浮点数
        /// </summary>
        /// <param name="decimalStr">要检查的字符串</param>
        /// <returns>返回是或否</returns>
        public static bool IsDecimal(string decimalStr)
        {
            if (String.IsNullOrEmpty(decimalStr))
            {
                return false;
            }

            try
            {
                Convert.ToDecimal(decimalStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// 判断一个字符串是否是整数
        /// </summary>
        /// <param name="integerStr">要检查的字符串</param>
        /// <returns>返回是或否</returns>
        public static bool IsInteger(string integerStr)
        {
            if (String.IsNullOrEmpty(integerStr))
            {
                return false;
            }

            try
            {
                Convert.ToInt32(integerStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// 获取新的数字
        /// </summary>
        /// <param name="number">要转换的数字</param>
        /// <returns>新的数字</returns>
        public static string InterceptDecimal(object number)
        {
            if (number == null)
            {
                return null;
            }
            decimal newNumber = 0;
            string newNumberStr = "";
            int an = -1;
            string numberStr = number.ToString();
            int n = numberStr.IndexOf(".");
            if (n == -1)
            {
                return numberStr;
            }
            for (int i = n + 1; i < numberStr.Length; i++)
            {
                string str = numberStr.Substring(i, 1);
                if (str == "0")
                {
                    if (GetStr(numberStr, i))
                    {
                        an = i;
                        break;
                    }
                }
            }
            if (an == -1)
            {
                newNumber = Convert.ToDecimal(numberStr);
            }
            else if (an == n + 1)
            {

                newNumberStr = numberStr.Substring(0, an - 1);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            else
            {
                newNumberStr = numberStr.Substring(0, an);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            return newNumber.ToString();
        }

        /// <summary>
        /// 判断字符串从第n位开始以后是否都为0
        /// </summary>
        /// <param name="number">要判断的字符串</param>
        /// <param name="n">开始的位数</param>
        /// <returns>false不都为0，true都为0</returns>
        public static bool GetStr(string number, int n)
        {
            for (int i = n; i < number.Length; i++)
            {
                if (number.Substring(i, 1) != "0")
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="n">长度</param>
        /// <returns>截取后字符串</returns>
        public static string GetSubStr(object str, object n)
        {
            if (str != null)
            {
                if (str.ToString().Length > Convert.ToInt32(n))
                {
                    return str.ToString().Substring(0, Convert.ToInt32(n)) + "....";
                }
                else
                {
                    return str.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 根据标识返回字符串list
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<string> GetStrListByStr(string str, char n)
        {
            List<string> strList = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                strList.AddRange(str.Split(n));
            }

            return strList;
        }

        #region 数字转换
        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewDecimalOrZero(string value)
        {
            decimal revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static decimal? GetNewDecimal(string value)
        {
            decimal? revalue = null;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static int? GetNewInt(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    return Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static int GetNewIntOrZero(string value)
        {
            int revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }
        #endregion

        /// <summary>
        /// 指定上传文件的名称
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            Random rm = new Random(Environment.TickCount);
            return DateTime.Now.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
        }

        /// <summary>
        /// 指定上传文件的名称
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName(DateTime? dateTime)
        {
            string str = string.Empty;
            Random rm = new Random(System.Environment.TickCount);
            if (dateTime.HasValue)
            {
                str= dateTime.Value.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
            }
            return str;
        }

        #region 时间转换
        /// <summary>
        /// 输入文本转换时间类型
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetNewDateTime(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return null;
            }
        }

        /// <summary>
        /// 输入文本转换时间类型（空时：默认当前时间）
        /// </summary>
        /// <returns></returns>
        public static DateTime GetNewDateTimeOrNow(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return System.DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return System.DateTime.Now;
            }
        }

        /// <summary>
        /// 根据时间获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static string GetQuarterlyByTime(DateTime time)
        {
            string quarterly = string.Empty;
            string yearName = time.Year.ToString();
            int month = time.Month;
            string name = string.Empty;
            if (month >= 1 && month <= 3)
            {
                name = "第一季度";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "第二季度";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "第三季度";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "第四季度";
            }

            quarterly = yearName + "年" + name;
            return quarterly;
        }

        /// <summary>
        /// 根据时间获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static int GetNowQuarterlyByTime(DateTime time)
        {
            int quarterly = 0;
            int month = time.Month;
            if (month >= 1 && month <= 3)
            {
                quarterly = 1;
            }
            else if (month >= 4 && month <= 6)
            {
                quarterly = 2;
            }
            else if (month >= 7 && month <= 9)
            {
                quarterly = 3;
            }
            else if (month >= 10 && month <= 12)
            {
                quarterly = 4;
            }

            return quarterly;
        }

        /// <summary>
        /// 根据月获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static int GetNowQuarterlyByMonth(int month)
        {
            int quarterly = 0;
            if (month >= 1 && month <= 3)
            {
                quarterly = 1;
            }
            else if (month >= 4 && month <= 6)
            {
                quarterly = 2;
            }
            else if (month >= 7 && month <= 9)
            {
                quarterly = 3;
            }
            else if (month >= 10 && month <= 12)
            {
                quarterly = 4;
            }

            return quarterly;
        }
        /// <summary>
        /// 根据时间获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static string GetQuarterlyNameByMonth(int month)
        {
            string name = string.Empty;
            if (month >= 1 && month <= 3)
            {
                name = "第一季度";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "第二季度";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "第三季度";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "第四季度";
            }

            return name;
        }
        /// <summary>
        /// 根据时间获取是上、下半年
        /// </summary>
        /// <returns></returns>
        public static int GetNowHalfYearByTime(DateTime time)
        {
            int quarterly = 1;
            int month = time.Month;
            if (month >= 1 && month <= 6)
            {
                quarterly = 1;
            }
            else
            {
                quarterly = 2;
            }

            return quarterly;
        }

        /// <summary>
        /// 根据时间获取是上、下半年
        /// </summary>
        /// <returns></returns>
        public static int GetNowHalfYearByMonth(int month)
        {
            int halfYear = 1;
            if (month >= 1 && month <= 6)
            {
                halfYear = 1;
            }
            else
            {
                halfYear = 2;
            }

            return halfYear;
        }

        /// <summary>
        /// 根据时间获取是上、下半年
        /// </summary>
        /// <returns></returns>
        public static string GetNowHalfYearNameByTime(DateTime time)
        {
            string quarterly = "上半年";
            int month = time.Month;
            if (month >= 1 && month <= 6)
            {
                quarterly = "上半年";
            }
            else
            {
                quarterly = "下半年";
            }

            return quarterly;
        }
        #endregion

        /// <summary>
        /// 相差月份
        /// </summary>
        /// <param name="datetime2"></param>
        /// <param name="datetime2"></param>
        /// <returns></returns>
        public static int CompareMonths(DateTime datetime1, DateTime datetime2)
        {
            DateTime dt = datetime1;
            DateTime dt2 = datetime2;
            if (DateTime.Compare(dt, dt2) < 0)
            {
                dt2 = dt;
                dt = datetime2;
            }
            int year = dt.Year - dt2.Year;
            int month = dt.Month - dt2.Month;
            month = year * 12 + month;
            if (dt.Day - dt2.Day < -15)
            {
                month--;
            }
            else if (dt.Day - dt2.Day > 14)
            {
                month++;
            }
            return month;
        }


        public static DateTime GetQuarterlyMonths(string year, string quarterly)
        {
            string startMonth = string.Empty;
            if (quarterly == "1")
            {
                startMonth = "1";
            }
            else if (quarterly == "2")
            {
                startMonth = "4";
            }
            else if (quarterly == "3")
            {
                startMonth = "7";
            }
            else if (quarterly == "4")
            {
                startMonth = "10";
            }
            return Funs.GetNewDateTimeOrNow(year + "-" + startMonth + "-01");
        }

        /// <summary>
        /// 获取单位工程类型
        /// </summary>
        /// <param name="projectType"></param>
        /// <returns></returns>
        public static string GetUnitWorkType(string projectType)
        {
            string type = string.Empty;
            if (projectType == "1")
            {
                type = "建筑";
            }
            if (projectType == "2")
            {
                type = "安装";
            }
            return type;
        }

        #region  获取大写金额事件
            public static string NumericCapitalization(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以tr2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public static void SubmitChanges()
        {
            try
            {
                DB.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (ChangeConflictException ex)
            {
                foreach (ObjectChangeConflict occ in DB.ChangeConflicts)
                {
                    //以下是解决冲突的三种方法，选一种即可
                    //// 使用当前数据库中的值，覆盖Linq缓存中实体对象的值
                    //occ.Resolve(RefreshMode.OverwriteCurrentValues);
                    //// 使用Linq缓存中实体对象的值，覆盖当前数据库中的值
                    //occ.Resolve(RefreshMode.KeepCurrentValues);
                    // 只更新实体对象中改变的字段的值，其他的保留不变
                    occ.Resolve(RefreshMode.KeepChanges);
                }
                // 这个地方要注意，Catch方法中，我们前面只是指明了怎样来解决冲突，这个地方还需要再次提交更新，这样的话，值    //才会提交到数据库。
                DB.SubmitChanges();                
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
        }

        /// <summary>
        /// 将IEnumerable<T>类型的集合转换为DataTable类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            //定义要返回的DataTable对象
            DataTable dtReturn = new DataTable();
            // 保存列集合的属性信息数组
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;//安全性检查
                                                 //循环遍历集合，使用反射获取类型的属性信息
            foreach (T rec in varlist)
            {
                //使用反射获取T类型的属性信息，返回一个PropertyInfo类型的集合
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    //循环PropertyInfo数组
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;//得到属性的类型
                                                       //如果属性为泛型类型
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {   //获取泛型类型的参数
                            colType = colType.GetGenericArguments()[0];
                        }
                        //将类型的属性名称与属性类型作为DataTable的列数据
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                //新建一个用于添加到DataTable中的DataRow对象
                DataRow dr = dtReturn.NewRow();
                //循环遍历属性集合
                foreach (PropertyInfo pi in oProps)
                {   //为DataRow中的指定列赋值
                    dr[pi.Name] = pi.GetValue(rec, null) == null ?
                        DBNull.Value : pi.GetValue(rec, null);
                }
                //将具有结果值的DataRow添加到DataTable集合中
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;//返回DataTable对象
        }


        /// <summary>
        /// 在设定的数值内产生随机数的数量
        /// </summary>
        /// <param name="num">随机数的数量</param>
        /// <param name="minValue">随机数的最小值</param>
        /// <param name="maxValue">随机数的最大值</param>
        /// <returns></returns>
        public static int[] GetRandomNum(int num, int minValue, int maxValue)
        {
            if ((maxValue + 1 - minValue - num < 0))
                maxValue += num - (maxValue + 1 - minValue);
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            int[] arrNum = new int[num];
            int tmp = 0;
            StringBuilder sb = new StringBuilder(num * maxValue.ToString().Trim().Length);

            for (int i = 0; i <= num - 1; i++)
            {
                tmp = ra.Next(minValue, maxValue);
                while (sb.ToString().Contains("#" + tmp.ToString().Trim() + "#"))
                    tmp = ra.Next(minValue, maxValue + 1);
                arrNum[i] = tmp;
                sb.Append("#" + tmp.ToString().Trim() + "#");
            }
            return arrNum;
        }
    }
}

