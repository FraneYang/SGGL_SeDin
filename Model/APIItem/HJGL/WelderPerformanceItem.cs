using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WelderPerformanceItem
    {
        /// <summary>
        /// 焊工代号
        /// </summary>
        public string WelderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 焊工名称
        /// </summary>
        public string PersonName
        {
            get;
            set;
        }

        /// <summary>
        /// 施工单位
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }

        /// <summary>
        /// 资质等级
        /// </summary>
        public string WelderLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 资质有效期
        /// </summary>
        public string CertificateLimitTime
        {
            get;
            set;
        }

        /// <summary>
        /// 已焊焊口数/达因数
        /// </summary>
        public string TotalJotDin
        {
            get;
            set;
        }

        /// <summary>
        /// 一次检测焊口数
        /// </summary>
        public string OneCheckJotNum
        {
            get;
            set;
        }

        /// <summary>
        /// 焊工绩效（达因/天）
        /// </summary>
        public string WeldAvgNum
        {
            get;
            set;
        }

        /// <summary>
        ///一次检测合格焊口数
        /// </summary>
        public int? OneCheckPassJotNum
        {
            get;
            set;
        }

        /// <summary>
        ///焊接一次合格率（焊口） 
        /// </summary>
        public string OnePassRate
        {
            get;
            set;
        }

        /// <summary>
        ///一次检测总片子数
        /// </summary>
        public string OneCheckTotalFilm
        {
            get;
            set;
        }

        /// <summary>
        ///一次检测合格片子数
        /// </summary>
        public string OneCheckPassFilm
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
        /// 扩透焊口数
        /// </summary>
        public string ExpandJotNum
        {
            get;
            set;
        }

        /// <summary>
        ///RT合格率（片数）
        /// </summary>
        public string Passfilmrate
        {
            get;
            set;
        }
    }
}
