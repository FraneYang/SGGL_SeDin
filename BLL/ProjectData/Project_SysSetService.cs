using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Data.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace BLL
{
    public static class Project_SysSetService
    {
        public static bool? IsAuto(string setId, string projectId)
        {
            var q = from x in Funs.DB.Project_Sys_Set where x.SetId == setId && x.ProjectId == projectId select x;
            return q.First().IsAuto;
        }

        public static Model.Project_Sys_Set GetSysSetBySetId(string setId, string projectId)
        {
            return Funs.DB.Project_Sys_Set.Where(x => x.SetId == setId && x.ProjectId == projectId).FirstOrDefault();
        }
        public static Model.Project_Sys_Set GetSysSetBySetName(string setName, string projectId)
        {
            return Funs.DB.Project_Sys_Set.Where(x => x.SetName == setName && x.ProjectId == projectId).FirstOrDefault();
        }
        /// <summary>
        /// 增加系统变量
        /// </summary>
        /// <param name="user">系统变量</param>
        public static void AddSet(Model.Project_Sys_Set set)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Project_Sys_Set newSet = new Model.Project_Sys_Set();
            newSet.SetId = set.SetId;
            newSet.ProjectId = set.ProjectId;
            newSet.SetName = set.SetName;
            newSet.IsAuto = set.IsAuto;
            newSet.SetValue = set.SetValue;

            db.Project_Sys_Set.InsertOnSubmit(newSet);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改系统变量
        /// </summary>
        /// <param name="user">系统变量</param>
        public static void UpdateSet(Model.Project_Sys_Set set)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Project_Sys_Set newSet = db.Project_Sys_Set.First(e => e.SetId == set.SetId && e.ProjectId == set.ProjectId);

            newSet.IsAuto = set.IsAuto;
            newSet.SetValue = set.SetValue;
            db.SubmitChanges();
        }
    }
}
