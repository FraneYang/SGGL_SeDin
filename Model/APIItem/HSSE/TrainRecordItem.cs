﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class TrainRecordItem
    {
        /// <summary>
        /// 培训记录ID
        /// </summary>
        public string TrainRecordId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string TrainingCode
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string TrainTitle
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
        /// <summary>
        /// 培训类型ID
        /// </summary>
        public string TrainTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训类型名称
        /// </summary>
        public string TrainTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 培训级别ID
        /// </summary>
        public string TrainLevelId
        {
            get;
            set;
        }
        /// <summary>
        /// 培训级别名称
        /// </summary>
        public string TrainLevelName
        {
            get;
            set;
        }

        /// <summary>
        /// 学时
        /// </summary>
        public decimal TeachHour
        {
            get;
            set;
        }
        /// <summary>
        /// 培训地点
        /// </summary>
        public string TeachAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 培训时间
        /// </summary>
        public string TrainStartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 授课人
        /// </summary>
        public string TeachMan
        {
            get;
            set;
        }
        /// <summary>
        /// 培训状态
        /// </summary>
        public string TrainStates
        {
            get;
            set;
        }
        /// <summary>
        /// 培训单位ID
        /// </summary>
        public string UnitIds
        {
            get;
            set;
        }
        /// <summary>
        /// 培训单位名称
        /// </summary>
        public string UnitNames
        {
            get;
            set;
        }
        /// <summary>
        /// 培训岗位ID
        /// </summary>
        public string WorkPostIds
        {
            get;
            set;
        }
        /// <summary>
        /// 培训岗位名称
        /// </summary>
        public string WorkPostNames
        {
            get;
            set;
        }
        /// <summary>
        /// 培训内容
        /// </summary>
        public string TrainContent
        {
            get;
            set;
        }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 编制人
        /// </summary>
        public string CompileMan
        {
            get;
            set;
        }
    }
}