namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class RoleService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public static Model.Sys_Role GetRoleByRoleId(string roleId)
        {
            return Funs.DB.Sys_Role.FirstOrDefault(x => x.RoleId == roleId);
        }

        /// <summary>
        /// 获取角色名称是否存在
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否存在</returns>
        public static bool IsExistRoleName(string roleId,string roleName)
        {
            bool isExist = false;
            var role = Funs.DB.Sys_Role.FirstOrDefault(x => x.RoleName == roleName && x.RoleId != roleId);
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }
        
        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="role"></param>
        public static void AddRole(Model.Sys_Role role)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_Role newRole = new Model.Sys_Role
            {
                RoleId = role.RoleId,
                RoleCode = role.RoleCode,
                RoleName = role.RoleName,
                RoleType = role.RoleType,
                CNCodes=role.CNCodes,
                Def = role.Def,
                IsAuditFlow = role.IsAuditFlow,
                IsSystemBuilt = role.IsSystemBuilt,
            };
            db.Sys_Role.InsertOnSubmit(newRole);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="def"></param>
        public static void UpdateRole(Model.Sys_Role role)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_Role updateRole = db.Sys_Role.FirstOrDefault(e => e.RoleId == role.RoleId);
            if (updateRole != null)
            {
                updateRole.RoleCode = role.RoleCode;
                updateRole.RoleName = role.RoleName;
                updateRole.RoleType = role.RoleType;
                updateRole.CNCodes = role.CNCodes;
                updateRole.Def = role.Def;
                updateRole.IsAuditFlow = role.IsAuditFlow;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteRole(string roleId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_Role deleteRole = db.Sys_Role.FirstOrDefault(e => e.RoleId == roleId);
            if (deleteRole != null)
            {               
                ///删除对应权限表记录
                BLL.ButtonPowerService.DeleteButtonPower(roleId);
                var rolePower = from x in db.Sys_RolePower where x.RoleId == roleId select x;
                if (rolePower.Count() > 0)
                {
                    db.Sys_RolePower.DeleteAllOnSubmit(rolePower);
                    db.SubmitChanges();
                }

                db.Sys_Role.DeleteOnSubmit(deleteRole);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取角色下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_Role> GetRoleDropDownList(string roleId)
        {
            var list = (from x in Funs.DB.Sys_Role orderby x.RoleCode select x).ToList();
            if (!string.IsNullOrEmpty(roleId))
            {
                list = list.Where(x => x.RoleId != roleId).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取角色下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_Role> GetRoleListByRoelTypeId(string roleTypeId)
        {
            var list = (from x in Funs.DB.Sys_Role where x.RoleType == roleTypeId orderby x.RoleCode select x).ToList();
            return list;
        }

        /// <summary>
        /// 得到角色名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getRoleNamesRoleIds(object roleIds)
        {
            string roleName = string.Empty;
            if (roleIds != null)
            {
                string[] roles = roleIds.ToString().Split(',');
                foreach (string roleId in roles)
                {
                    var q = GetRoleByRoleId(roleId);
                    if (q != null && !roleName.Contains(q.RoleName))
                    {
                        roleName += q.RoleName + ",";
                    }
                }
                if (roleName != string.Empty)
                {
                    roleName = roleName.Substring(0, roleName.Length - 1); ;
                }
            }

            return roleName;
        }

        /// <summary>
        /// 角色下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitRoleDropDownList(FineUIPro.DropDownList dropName, string roleId, bool isShowPlease)
        {
            dropName.DataValueField = "RoleId";
            dropName.DataTextField = "RoleName";
            dropName.DataSource = BLL.RoleService.GetRoleDropDownList(roleId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
