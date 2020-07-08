using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 来文管理
    /// </summary>
    public class ReceiveFileManagerItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ReceiveFileManagerId
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
        /// 类型0-项目发文；1-公司来文
        /// </summary>
        public string FileType
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string ReceiveFileCode
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string ReceiveFileName
        {
            get;
            set;
        }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get;
            set;
        }
        /// <summary>
        /// 来文单位Id
        /// </summary>
        public string FileUnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 来文单位名称
        /// </summary>
        public string FileUnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 原文编号
        /// </summary>
        public string FileCode
        {
            get;
            set;
        }
        /// <summary>
        /// 原文页数
        /// </summary>
        public int? FilePageNum
        {
            get;
            set;
        }
        /// <summary>
        /// 收文日期
        /// </summary>
        public string GetFileDate
        {
            get;
            set;
        }
        /// <summary>
        /// 传送人ID
        /// </summary>
        public string SendPersonId
        {
            get;
            set;
        }
        /// <summary>
        /// 传送人姓名
        /// </summary>
        public string SendPersonName
        {
            get;
            set;
        }
        /// <summary>
        /// 来文内容
        /// </summary>
        public string MainContent
        {
            get;
            set;
        }
        /// <summary>
        /// 接收单位IDs
        /// </summary>
        public string UnitIds
        {
            get;
            set;
        }
        /// <summary>
        /// 接收单位名称s
        /// </summary>
        public string UnitNames
        {
            get;
            set;
        }
        /// <summary>
        /// 原文件路径
        /// </summary>
        public string FileAttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 回复文件路径
        /// </summary>
        public string ReplyFileAttachUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 是否下发
        /// </summary>
        public bool Issue
        {
            get;
            set;
        }
        /// <summary>
        /// 状态0待提交；1-已提交；2：已回复
        /// </summary>
        public string States
        {
            get;
            set;
        }
    }
}
