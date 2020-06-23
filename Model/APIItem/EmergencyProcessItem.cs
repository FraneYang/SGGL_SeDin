using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 应急流程项
    /// </summary>
    public class EmergencyProcessItem
    {
        public object EmergencyProcessId { get;  set; }
        public object ProjectId { get;  set; }
        public object ProcessSteps { get;  set; }
        public object ProcessName { get;  set; }
        public object StepOperator { get;  set; }
        public object Remark { get;  set; }

    }
}
