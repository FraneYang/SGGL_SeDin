using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 施工分包商组织机构关键人员名单
    /// </summary>
    public class AttachUrl9_SubPersonService
    {
        /// <summary>
        /// 根据附件Id获取施工分包商组织机构关键人员名单
        /// </summary>
        /// <param name="attachId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl9_SubPerson GetSubPersonByAttachId(string attachId)
        {
            return Funs.DB.PHTGL_AttachUrl9_SubPerson.FirstOrDefault(e => e.AttachUrlId == attachId);
        }

        /// <summary>
        /// 添加施工分包商组织机构关键人员名单
        /// </summary>
        /// <param name="subPerson"></param>
        public static void AddSubPerson(Model.PHTGL_AttachUrl9_SubPerson subPerson)
        {
            Model.PHTGL_AttachUrl9_SubPerson newSubPerson = new Model.PHTGL_AttachUrl9_SubPerson();
            newSubPerson.AttachUrlItemId = subPerson.AttachUrlItemId;
            newSubPerson.AttachUrlId = subPerson.AttachUrlId;
            newSubPerson.ProjectManager = subPerson.ProjectManager;
            newSubPerson.ProjectEngineer = subPerson.ProjectEngineer;
            newSubPerson.ConstructionManager = subPerson.ConstructionManager;
            newSubPerson.QualityManager = subPerson.QualityManager;
            newSubPerson.HSEManager = subPerson.HSEManager;
            newSubPerson.Personnel_Technician = subPerson.Personnel_Technician;
            newSubPerson.Personnel_Civil_engineering = subPerson.Personnel_Civil_engineering;
            newSubPerson.Personnel_Installation = subPerson.Personnel_Installation;
            newSubPerson.Personnel_Electrical = subPerson.Personnel_Electrical;
            newSubPerson.Personnel_meter = subPerson.Personnel_meter;
            Funs.DB.PHTGL_AttachUrl9_SubPerson.InsertOnSubmit(newSubPerson);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改施工分包商组织机构关键人员名单
        /// </summary>
        /// <param name="subPerson"></param>
        public static void UpdateSubPerson(Model.PHTGL_AttachUrl9_SubPerson subPerson)
        {
            Model.PHTGL_AttachUrl9_SubPerson newSubPerson = Funs.DB.PHTGL_AttachUrl9_SubPerson.FirstOrDefault(e => e.AttachUrlId == subPerson.AttachUrlId);
            if (newSubPerson != null)
            {
                newSubPerson.ProjectManager = subPerson.ProjectManager;
                newSubPerson.ProjectEngineer = subPerson.ProjectEngineer;
                newSubPerson.ConstructionManager = subPerson.ConstructionManager;
                newSubPerson.QualityManager = subPerson.QualityManager;
                newSubPerson.HSEManager = subPerson.HSEManager;
                newSubPerson.Personnel_Technician = subPerson.Personnel_Technician;
                newSubPerson.Personnel_Civil_engineering = subPerson.Personnel_Civil_engineering;
                newSubPerson.Personnel_Installation = subPerson.Personnel_Installation;
                newSubPerson.Personnel_Electrical = subPerson.Personnel_Electrical;
                newSubPerson.Personnel_meter = subPerson.Personnel_meter;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
