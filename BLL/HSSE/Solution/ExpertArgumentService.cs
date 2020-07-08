using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 专家论证清单
    /// </summary>
    public static class ExpertArgumentService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取专家论证清单
        /// </summary>
        /// <param name="expertArgumentId"></param>
        /// <returns></returns>
        public static Model.Solution_ExpertArgument GetExpertArgumentById(string expertArgumentId)
        {
            return new Model.SGGLDB(Funs.ConnString).Solution_ExpertArgument.FirstOrDefault(e => e.ExpertArgumentId == expertArgumentId);
        }

        /// <summary>
        /// 添加专家论证清单
        /// </summary>
        /// <param name="expertArgument"></param>
        public static void AddExpertArgument(Model.Solution_ExpertArgument expertArgument)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_ExpertArgument newExpertArgument = new Model.Solution_ExpertArgument
            {
                ExpertArgumentId = expertArgument.ExpertArgumentId,
                ExpertArgumentCode = expertArgument.ExpertArgumentCode,
                HazardType = expertArgument.HazardType,
                ProjectId = expertArgument.ProjectId,
                Address = expertArgument.Address,
                ExpectedTime = expertArgument.ExpectedTime,
                IsArgument = expertArgument.IsArgument,
                RecardMan = expertArgument.RecardMan,
                RecordTime = expertArgument.RecordTime,
                Remark = expertArgument.Remark,
                States = expertArgument.States,
                Descriptions = expertArgument.Descriptions
            };
            db.Solution_ExpertArgument.InsertOnSubmit(newExpertArgument);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectExpertArgumentMenuId, expertArgument.ProjectId, null, expertArgument.ExpertArgumentId, expertArgument.ExpectedTime);
        }

        /// <summary>
        /// 修改专家论证清单
        /// </summary>
        /// <param name="expertArgument"></param>
        public static void UpdateExpertArgument(Model.Solution_ExpertArgument expertArgument)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_ExpertArgument newExpertArgument = db.Solution_ExpertArgument.FirstOrDefault(e => e.ExpertArgumentId == expertArgument.ExpertArgumentId);
            if (newExpertArgument != null)
            {
                newExpertArgument.ExpertArgumentCode = expertArgument.ExpertArgumentCode;
                newExpertArgument.HazardType = expertArgument.HazardType;
                newExpertArgument.ProjectId = expertArgument.ProjectId;
                newExpertArgument.Address = expertArgument.Address;
                newExpertArgument.ExpectedTime = expertArgument.ExpectedTime;
                newExpertArgument.IsArgument = expertArgument.IsArgument;
                newExpertArgument.RecardMan = expertArgument.RecardMan;
                newExpertArgument.RecordTime = expertArgument.RecordTime;
                newExpertArgument.Remark = expertArgument.Remark;
                newExpertArgument.States = expertArgument.States;
                newExpertArgument.Descriptions = expertArgument.Descriptions;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除专家论证清单
        /// </summary>
        /// <param name="expertArgumentId"></param>
        public static void DeleteExpertArgumentById(string expertArgumentId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_ExpertArgument expertArgument = db.Solution_ExpertArgument.FirstOrDefault(e => e.ExpertArgumentId == expertArgumentId);
            if (expertArgument != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(expertArgumentId);
                db.Solution_ExpertArgument.DeleteOnSubmit(expertArgument);
                db.SubmitChanges();
            }
        }
    }
}
