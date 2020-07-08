using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkPackageInitService
    {

        /// <summary>
        ///// 添加分部工程
        /// </summary>
        /// <param name="UnitWork"></param>
        public static void AddWorkPackageInit(Model.WBS_WorkPackageInit workPack)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_WorkPackageInit newPack = new Model.WBS_WorkPackageInit();
            newPack.WorkPackageCode = workPack.WorkPackageCode;
            newPack.SuperWorkPack = workPack.SuperWorkPack;
            newPack.PackageCode = workPack.PackageCode;
            newPack.PackageContent = workPack.PackageContent;
            newPack.IsChild = workPack.IsChild;
            newPack.ProjectType = workPack.ProjectType;
            db.WBS_WorkPackageInit.InsertOnSubmit(newPack);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改分部分项工程
        /// </summary>
        /// <param name="UnitWork"></param>
        public static void UpdateWorkPackageInit(Model.WBS_WorkPackageInit workPack)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_WorkPackageInit newPack = db.WBS_WorkPackageInit.First(e => e.WorkPackageCode == workPack.WorkPackageCode);
            newPack.IsChild = workPack.IsChild;
            newPack.PackageContent = workPack.PackageContent;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据分部分项工程编号删除一个分部工程信息
        /// </summary>
        /// <param name="UnitWorkId"></param>
        public static void DeleteWorkPackageInit(string workPackageCode)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.WBS_WorkPackageInit delWorkPack = db.WBS_WorkPackageInit.First(e => e.WorkPackageCode == workPackageCode);
            db.WBS_WorkPackageInit.DeleteOnSubmit(delWorkPack);
            db.SubmitChanges();
        }

        /// <summary>
        /// 是否存在分部分项工程
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistWorkPackageInitName(string supWorkPack, string packageContent, string workPackageCode)
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackageInit where x.SuperWorkPack == supWorkPack && x.PackageContent == packageContent && x.WorkPackageCode != workPackageCode select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据分部分项编号获取分部分项信息
        /// </summary>
        /// <param name="workPackageCode"></param>
        /// <returns></returns>
        public static Model.WBS_WorkPackageInit GetWorkPackageInitByWorkPackageCode(string workPackageCode)
        {
            return new Model.SGGLDB(Funs.ConnString).WBS_WorkPackageInit.FirstOrDefault(x => x.WorkPackageCode == workPackageCode);
        }

        /// <summary>
        /// 根据分部分项编号获取子级分部分项信息集合
        /// </summary>
        /// <param name="workPackageCode"></param>
        /// <returns></returns>
        public static List<Model.WBS_WorkPackageInit> GetWorkPackageInitsBySuperWorkPack(string workPackageCode)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).WBS_WorkPackageInit where x.SuperWorkPack == workPackageCode select x).ToList();
        }
    }
}
