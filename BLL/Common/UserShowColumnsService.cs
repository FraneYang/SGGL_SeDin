using System.Linq;

namespace BLL
{
    public static class UserShowColumnsService
    {
        /// <summary>
        /// 根据用户ID获取信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static Model.Sys_UserShowColumns GetColumnsByUserId(string userId, string type)
        {
            return Funs.DB.Sys_UserShowColumns.FirstOrDefault(x => x.UserId == userId && x.ShowType == type);
        }

        /// <summary>
        /// 添加用户对应显示列信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="opUserShowColumns"></param>
        public static void AddUserShowColumns(Model.Sys_UserShowColumns showColumns)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_UserShowColumns newShowColumns = new Model.Sys_UserShowColumns();

            newShowColumns.ShowColumnId = SQLHelper.GetNewID(typeof(Model.Sys_UserShowColumns));
            newShowColumns.UserId = showColumns.UserId;
            newShowColumns.Columns = showColumns.Columns;
            newShowColumns.ShowType = showColumns.ShowType;
            db.Sys_UserShowColumns.InsertOnSubmit(newShowColumns);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改用户对应显示列信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="def"></param>
        public static void UpdateUserShowColumns(Model.Sys_UserShowColumns showColumns)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_UserShowColumns newShowColumns = db.Sys_UserShowColumns.First(e => e.ShowColumnId == showColumns.ShowColumnId);
            newShowColumns.Columns = showColumns.Columns;
            newShowColumns.ShowType = showColumns.ShowType;
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除用户对应显示列信息
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteUserShowColumns(string showColumnId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Sys_UserShowColumns newShowColumns = db.Sys_UserShowColumns.First(e => e.ShowColumnId == showColumnId);
            db.Sys_UserShowColumns.DeleteOnSubmit(newShowColumns);
            db.SubmitChanges();
        }
    }
}
