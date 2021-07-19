using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class HotProcessHardItem
    {
        /// <summary>
        /// 管线编号
        /// </summary>
        public string PipelineCode
        {
            get;
            set;
        }

        /// <summary>
        /// 焊口号
        /// </summary>
        public string WeldJointCode
        {
            get;
            set;
        }

        /// <summary>
        /// 焊工号
        /// </summary>
        public string WelderCode
        {
            get;
            set;
        }

        /// <summary>
        /// 材质1
        /// </summary>
        public string Material
        {
            get;
            set;
        }

        /// <summary>
        /// 焊口规格
        /// </summary>
        public string Specification
        {
            get;
            set;
        }

        /// <summary>
        /// 委托日期
        /// </summary>
        public DateTime? TrustDate
        {
            get;
            set;
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        public string IsCompleted
        {
            get;
            set;
        }

        /// <summary>
        /// 是否合格
        /// </summary>
        public string IsPass
        {
            get;
            set;
        }
    }
}
