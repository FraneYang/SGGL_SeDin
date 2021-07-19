using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WeldingDailyItem
    {
        /// <summary>
        /// 日报ID
        /// </summary>
        public string WeldingDailyId
        {
            get;
            set;
        }

        /// <summary>
        /// 选择项ID集合
        /// </summary>
        public string SelectIds
        {
            get;
            set;
        }

        /// <summary>
        /// 未选择项ID集合
        /// </summary>
        public string NotSelectIds
        {
            get;
            set;
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
    }
}
