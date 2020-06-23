using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace BLL
{
    public class MethodRefelect
    {
        /// <summary>
        /// 搜索方法，并执行
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="publicMethodName"></param>
        public static void InvokeMethod(object obj, string assemblyName, string className, string publicMethodName)
        {
            string operate = publicMethodName;
            if (string.IsNullOrEmpty(operate)) { return; }
            Assembly ass = Assembly.Load(assemblyName);
            Type tp = ass.GetType(assemblyName + "." + className);
            MethodInfo methodInfo = tp.GetMethod(operate);
            if (methodInfo == null) { return; }
            methodInfo.Invoke(obj, null);
        }
    }
}
