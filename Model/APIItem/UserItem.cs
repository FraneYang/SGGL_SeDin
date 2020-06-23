using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserItem
    {
       /// <summary>
       /// 用户ID
       /// </summary>
       public string UserId
        {
           get;
           set;
       }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get;
            set;
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode
        {
            get;
            set;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
           get;
           set;
       }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId
        {
            get;
            set;
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get;
            set;
        }
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName
        {
            get;
            set;
        }
        /// <summary>
        /// 当前项目ID
        /// </summary>
        public string LoginProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 当前项目名称
        /// </summary>
        public string LoginProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string WorkPostId
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string WorkPostName
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdentityCard
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone
        {
            get;
            set;
        }
        /// <summary>
        /// 是否本部
        /// </summary>
        public bool? IsOffice
        {
            get;
            set;
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string SignatureUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 用户类型（1用户；2临时用户；3现场人员）
        /// </summary>
        public string UserType
        {
            get;
            set;
        }
    }
}
