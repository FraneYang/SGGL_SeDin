using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReportQueryItem
    {
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode
        {
            get;
            set;
        }

        /// <summary>
        /// 单位工程
        /// </summary>
        public string UnitWork
        {
            get;
            set;
        }

        /// <summary>
        /// 施工单位
        /// </summary>
        public string CUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 材质
        /// </summary>
        public string Material
        {
            get;
            set;
        }

        /// <summary>
        /// 管线
        /// </summary>
        public string PipeLine
        {
            get;
            set;
        }

        /// <summary>
        /// 焊工
        /// </summary>
        public string WelderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 总焊口数/达因数
        /// </summary>
        public string TotalJotDin
        {
            get;
            set;
        }

        /// <summary>
        /// 已焊焊口数/达因数
        /// </summary>
        public string WeldedJotDin
        {
            get;
            set;
        }

        /// <summary>
        /// 焊接完成比例
        /// </summary>
        public string WeldedRate
        {
            get;
            set;
        }

        /// <summary>
        /// 焊接一次合格率
        /// </summary>
        public string OneOKRate
        {
            get;
            set;
        }

        /// <summary>
        /// 应检测焊口数
        /// </summary>
        public string MustCheckJotNum
        {
            get;
            set;
        }

        /// <summary>
        /// 已检测焊口数
        /// </summary>
        public string CheckedJotNum
        {
            get;
            set;
        }

        /// <summary>
        /// 检测完成比例
        /// </summary>
        public string CheckCompRate
        {
            get;
            set;
        }

        /// <summary>
        /// 返修焊口数
        /// </summary>
        public string RepairJotNum
        {
            get;
            set;
        }

        /// <summary>
        /// 扩探焊口数
        /// </summary>
        public string ExpandJotNum
        {
            get;
            set;
        }

        /// <summary>
        /// 应检测焊口数
        /// </summary>
        public string NeedCheckJotNum
        {
            get;
            set;
        }

        /// <summary>
        /// 检测完成比例
        /// </summary>
        public string CheckedRate
        {
            get;
            set;
        }

        /// <summary>
        /// 返修焊口数
        /// </summary>
        public string CheckRepairJotNum
        {
            get;
            set;
        }
    }
}
