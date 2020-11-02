using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Person_DutyTemplateService
    {
        public static Model.SGGLDB db = Funs.DB;
        /// <summary>
        /// 获取员工责任书模板信息
        /// </summary>
        /// <param name="dutyTemplateId">模板Id</param>
        /// <returns>员工责任书模板</returns>
        public static Model.Person_DutyTemplate GetPersondutyTemplateById(string dutyTemplateId)
        {
            return Funs.DB.Person_DutyTemplate.FirstOrDefault(e => e.DutyTemplateId == dutyTemplateId);
        }

        /// <summary>
        /// 获取员工责任书模板信息
        /// </summary>
        /// <param name="dutyTemplateId">模板Id</param>
        /// <returns>员工责任书模板</returns>
        public static Model.Person_DutyTemplate GetPersondutyTemplateByWorkPostId(string workPostId)
        {
            return Funs.DB.Person_DutyTemplate.FirstOrDefault(e => e.WorkPostId == workPostId);
        }

        /// <summary>
        /// 增加员工责任书模板信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddPersondutyTemplate(Model.Person_DutyTemplate dutyTemplate)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_DutyTemplate newdutyTemplate = new Model.Person_DutyTemplate
            {
                DutyTemplateId = dutyTemplate.DutyTemplateId,
                WorkPostId = dutyTemplate.WorkPostId,
                Template = dutyTemplate.Template
            };
            db.Person_DutyTemplate.InsertOnSubmit(newdutyTemplate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改员工责任书模板信息
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdatePersondutyTemplate(Model.Person_DutyTemplate dutyTemplate)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_DutyTemplate newdutyTemplate = db.Person_DutyTemplate.FirstOrDefault(e => e.DutyTemplateId == dutyTemplate.DutyTemplateId);
            if (newdutyTemplate != null)
            {
                newdutyTemplate.Template = dutyTemplate.Template;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据Id删除一个员工责任书模板信息
        /// </summary>
        /// <param name="Person_DutyTemplateId"></param>
        public static void DeletePersondutyTemplate(string dutyTemplateId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_DutyTemplate user = db.Person_DutyTemplate.FirstOrDefault(e => e.DutyTemplateId == dutyTemplateId);
            if (user != null)
            {
                db.Person_DutyTemplate.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
        }
    }
}
