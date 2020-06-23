namespace BLL
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Linq;
    using Model;

    /// <summary>
    /// 错误日志
    /// </summary>
    public static class ErrLogInfo
    {
        /// <summary>
        /// 错误日志默认路径
        /// </summary>
        public static string DefaultErrLogFullPath
        {
            get;
            set;
        }

        #region 写入错误日志
        /// <summary>
        /// 用默认错误日志路径写入错误日志
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="appendErrLog">异常后追加的说明信息</param>
        /// <returns>最终写入的错误信息</returns>
        public static string WriteLog(string ErrId, Exception ex, params string[] appendErrLog)
        {
            StringBuilder errLog = new StringBuilder();
            if (ex != null)
            {
                using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                {
                    Exception innerEx = ex.InnerException;
                    errLog.Append("\r\n");
                    errLog.Append("错误信息开始=====>\r\n");
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "错误类型:{0}\r\n", ex.GetType().Name));
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "错误信息:{0}\r\n", ex.Message));
                    errLog.Append("错误堆栈:\r\n");
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "{0}\r\n", ex.StackTrace));
                    Sys_ErrLogInfo newErr = new Sys_ErrLogInfo
                    {
                        ErrType = ex.GetType().Name,
                        ErrMessage = ex.Message,
                        ErrStackTrace = ex.StackTrace
                    };

                    int level = 1;
                    while (innerEx != null)
                    {
                        string preString = new string('-', level * 4);
                        errLog.Append(preString);
                        if (innerEx.GetType() != null)
                        {
                            errLog.Append(String.Format(CultureInfo.InvariantCulture, "错误类型:{0}\r\n", innerEx.GetType().Name));
                        }
                        errLog.Append(preString);
                        errLog.Append("错误信息:\r\n");
                        errLog.Append(preString);
                        errLog.Append(innerEx.Message + "\r\n");
                        errLog.Append(preString);
                        errLog.Append("错误堆栈:\r\n");
                        errLog.Append(new string(' ', level * 4));
                        if (innerEx.StackTrace != null)
                        {
                            errLog.Append(String.Format(CultureInfo.InvariantCulture, "{0}\r\n", innerEx.StackTrace.Replace("\r\n", "\r\n" + new string(' ', level * 4))));
                        }
                        newErr.ErrType += innerEx.GetType().Name;
                        newErr.ErrMessage += innerEx.Message;
                        newErr.ErrStackTrace += innerEx.StackTrace;

                        innerEx = innerEx.InnerException;
                        level++;
                    }

                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "出错时间:{0}\r\n", DateTime.Now));
                    newErr.ErrTime = DateTime.Now;

                    var errlogInfo = db.Sys_ErrLogInfo.FirstOrDefault(x => x.ErrLogId == ErrId);
                    if (errlogInfo != null)
                    {
                        errlogInfo.ErrType = newErr.ErrType;
                        errlogInfo.ErrMessage = newErr.ErrMessage;
                        errlogInfo.ErrStackTrace = newErr.ErrStackTrace;
                        errlogInfo.ErrTime = newErr.ErrTime;
                       db.SubmitChanges();
                    }
                    else
                    {
                        newErr.ErrLogId = SQLHelper.GetNewID(typeof(Sys_ErrLogInfo));
                        db.Sys_ErrLogInfo.InsertOnSubmit(newErr);
                        db.SubmitChanges();
                    }
                }

                if (appendErrLog != null)
                {
                    foreach (string log in appendErrLog)
                    {
                        if (log != null)
                        {
                            errLog.Append(log);
                            errLog.Append("\r\n");
                        }
                    }

                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "出错时间:{0}\r\n", DateTime.Now));
                }
            }
            return WriteLog(errLog.ToString());
        }

        /// <summary>
        /// 用默认错误日志路径写入错误日志
        /// </summary>
        /// <param name="errLog">错误日志</param>
        /// <returns>最终写入的错误信息</returns>
        public static string WriteLog(string errLog)
        {
            return WriteLog(DefaultErrLogFullPath, errLog);
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="writeFullPath">日志写入完整路径</param>
        /// <param name="errLog">错误日志</param>
        /// <returns>最终写入的错误信息</returns>
        public static string WriteLog(string writeFullPath, string errLog)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(writeFullPath, true);                
                sw.WriteLine(errLog);
                sw.Close();
                return errLog;
            }
            catch
            {
                try
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }
                }
                catch
                {
                }
            }

            return String.Empty;
        }
        #endregion
    }
}