using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class HSSEStandardsListService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据id查询标准规范信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Law_HSSEStandardsList GetHSSEStandardsListByHSSEStandardsListId(string standardId)
        {
            return Funs.DB.Law_HSSEStandardsList.FirstOrDefault(e => e.StandardId == standardId);
        }

        /// <summary>
        /// 根据上传人获取标准规范
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Law_HSSEStandardsList> GetHSSEStandardsListByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Law_HSSEStandardsList where x.CompileMan == compileMan select x).ToList();
        }
        
        /// <summary>
        /// 添加标准规范信息
        /// </summary>
        /// <param name="?"></param>
        public static void AddHSSEStandardsList(Model.Law_HSSEStandardsList hSSEStandardsList)
        {
            Model.Law_HSSEStandardsList newHSSEStandardsList = new Model.Law_HSSEStandardsList
            {
                StandardId = hSSEStandardsList.StandardId,
                StandardGrade = hSSEStandardsList.StandardGrade,
                StandardNo = hSSEStandardsList.StandardNo,
                StandardName = hSSEStandardsList.StandardName,
                TypeId = hSSEStandardsList.TypeId,
                AttachUrl = hSSEStandardsList.AttachUrl,
                IsSelected1 = hSSEStandardsList.IsSelected1,
                IsSelected2 = hSSEStandardsList.IsSelected2,
                IsSelected3 = hSSEStandardsList.IsSelected3,
                IsSelected4 = hSSEStandardsList.IsSelected4,
                IsSelected5 = hSSEStandardsList.IsSelected5,
                IsSelected6 = hSSEStandardsList.IsSelected6,
                IsSelected7 = hSSEStandardsList.IsSelected7,
                IsSelected8 = hSSEStandardsList.IsSelected8,
                IsSelected9 = hSSEStandardsList.IsSelected9,
                IsSelected10 = hSSEStandardsList.IsSelected10,
                IsSelected11 = hSSEStandardsList.IsSelected11,
                IsSelected12 = hSSEStandardsList.IsSelected12,
                IsSelected13 = hSSEStandardsList.IsSelected13,
                IsSelected14 = hSSEStandardsList.IsSelected14,
                IsSelected15 = hSSEStandardsList.IsSelected15,
                IsSelected16 = hSSEStandardsList.IsSelected16,
                IsSelected17 = hSSEStandardsList.IsSelected17,
                IsSelected18 = hSSEStandardsList.IsSelected18,
                IsSelected19 = hSSEStandardsList.IsSelected19,
                IsSelected20 = hSSEStandardsList.IsSelected20,
                IsSelected21 = hSSEStandardsList.IsSelected21,
                IsSelected22 = hSSEStandardsList.IsSelected22,
                IsSelected23 = hSSEStandardsList.IsSelected23,
                IsSelected24 = hSSEStandardsList.IsSelected24,
                IsSelected25 = hSSEStandardsList.IsSelected25,
                IsSelected90 = hSSEStandardsList.IsSelected90,
                CompileMan = hSSEStandardsList.CompileMan,
                CompileDate = hSSEStandardsList.CompileDate,
                IsPass = hSSEStandardsList.IsPass,
                UnitId = hSSEStandardsList.UnitId,
                UpState = hSSEStandardsList.UpState,
                IsBuild = false,
                ApprovalDate = hSSEStandardsList.ApprovalDate,
                EffectiveDate = hSSEStandardsList.EffectiveDate,
                Description = hSSEStandardsList.Description,
                ReleaseStates = hSSEStandardsList.ReleaseStates,
                ReleaseUnit = hSSEStandardsList.ReleaseUnit,
                AbolitionDate = hSSEStandardsList.AbolitionDate,
                ReplaceInfo = hSSEStandardsList.ReplaceInfo,
                IndexesIds = hSSEStandardsList.IndexesIds,
            };
            Funs.DB.Law_HSSEStandardsList.InsertOnSubmit(newHSSEStandardsList);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改标准规范信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateHSSEStandardsList(Model.Law_HSSEStandardsList hSSEStandardsList)
        {            
            Model.Law_HSSEStandardsList newHSSEStandardsList = Funs.DB.Law_HSSEStandardsList.FirstOrDefault(e => e.StandardId == hSSEStandardsList.StandardId);
            if (newHSSEStandardsList != null)
            {
                newHSSEStandardsList.StandardGrade = hSSEStandardsList.StandardGrade;
                newHSSEStandardsList.StandardNo = hSSEStandardsList.StandardNo;
                newHSSEStandardsList.StandardName = hSSEStandardsList.StandardName;
                newHSSEStandardsList.TypeId = hSSEStandardsList.TypeId;
                newHSSEStandardsList.AttachUrl = hSSEStandardsList.AttachUrl;
                newHSSEStandardsList.IsSelected1 = hSSEStandardsList.IsSelected1;
                newHSSEStandardsList.IsSelected2 = hSSEStandardsList.IsSelected2;
                newHSSEStandardsList.IsSelected3 = hSSEStandardsList.IsSelected3;
                newHSSEStandardsList.IsSelected4 = hSSEStandardsList.IsSelected4;
                newHSSEStandardsList.IsSelected5 = hSSEStandardsList.IsSelected5;
                newHSSEStandardsList.IsSelected6 = hSSEStandardsList.IsSelected6;
                newHSSEStandardsList.IsSelected7 = hSSEStandardsList.IsSelected7;
                newHSSEStandardsList.IsSelected8 = hSSEStandardsList.IsSelected8;
                newHSSEStandardsList.IsSelected9 = hSSEStandardsList.IsSelected9;
                newHSSEStandardsList.IsSelected10 = hSSEStandardsList.IsSelected10;
                newHSSEStandardsList.IsSelected11 = hSSEStandardsList.IsSelected11;
                newHSSEStandardsList.IsSelected12 = hSSEStandardsList.IsSelected12;
                newHSSEStandardsList.IsSelected13 = hSSEStandardsList.IsSelected13;
                newHSSEStandardsList.IsSelected14 = hSSEStandardsList.IsSelected14;
                newHSSEStandardsList.IsSelected15 = hSSEStandardsList.IsSelected15;
                newHSSEStandardsList.IsSelected16 = hSSEStandardsList.IsSelected16;
                newHSSEStandardsList.IsSelected17 = hSSEStandardsList.IsSelected17;
                newHSSEStandardsList.IsSelected18 = hSSEStandardsList.IsSelected18;
                newHSSEStandardsList.IsSelected19 = hSSEStandardsList.IsSelected19;
                newHSSEStandardsList.IsSelected20 = hSSEStandardsList.IsSelected20;
                newHSSEStandardsList.IsSelected21 = hSSEStandardsList.IsSelected21;
                newHSSEStandardsList.IsSelected22 = hSSEStandardsList.IsSelected22;
                newHSSEStandardsList.IsSelected23 = hSSEStandardsList.IsSelected23;
                newHSSEStandardsList.IsSelected24 = hSSEStandardsList.IsSelected24;
                newHSSEStandardsList.IsSelected25 = hSSEStandardsList.IsSelected25;
                newHSSEStandardsList.IsSelected90 = hSSEStandardsList.IsSelected90;
                newHSSEStandardsList.UpState = hSSEStandardsList.UpState;

                newHSSEStandardsList.ApprovalDate = hSSEStandardsList.ApprovalDate;
                newHSSEStandardsList.EffectiveDate = hSSEStandardsList.EffectiveDate;
                newHSSEStandardsList.Description = hSSEStandardsList.Description;
                newHSSEStandardsList.ReleaseStates = hSSEStandardsList.ReleaseStates;
                newHSSEStandardsList.ReleaseUnit = hSSEStandardsList.ReleaseUnit;
                newHSSEStandardsList.AbolitionDate = hSSEStandardsList.AbolitionDate;
                newHSSEStandardsList.ReplaceInfo = hSSEStandardsList.ReplaceInfo;
                newHSSEStandardsList.IndexesIds = hSSEStandardsList.IndexesIds;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改标准规范 是否采用
        /// </summary>
        /// <param name="hSSEStandardsList"></param>
        public static void UpdateHSSEStandardsListIsPass(Model.Law_HSSEStandardsList standardsList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_HSSEStandardsList newHSSEStandardsList = db.Law_HSSEStandardsList.FirstOrDefault(e => e.StandardId == standardsList.StandardId);
            if (newHSSEStandardsList != null)
            {
                newHSSEStandardsList.AuditMan = standardsList.AuditMan;
                newHSSEStandardsList.AuditDate = standardsList.AuditDate;
                newHSSEStandardsList.IsPass = standardsList.IsPass;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在标准规范编号
        /// </summary>
        /// <param name="EDU_Code"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistStandardName(string standardName)
        {
            var q = from x in Funs.DB.Law_HSSEStandardsList where x.StandardName == standardName select x;
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
        /// 是否存在标准规范编号
        /// </summary>
        /// <param name="EDU_Code"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistStandardNo(string standardNo)
        {
            var q = from x in Funs.DB.Law_HSSEStandardsList where x.StandardNo == standardNo select x;
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
        /// 删除标准规范信息
        /// </summary>
        /// <param name="EDU_ID"></param>
        public static void DeleteHSSEStandardsList(string standardId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Law_HSSEStandardsList hSSEStandardsList = db.Law_HSSEStandardsList.FirstOrDefault(e => e.StandardId == standardId);
            if (hSSEStandardsList != null)
            {
                //if (!string.IsNullOrEmpty(hSSEStandardsList.AttachUrl))
                //{
                //    UploadFileService.DeleteFile(Funs.RootPath, hSSEStandardsList.AttachUrl);
                //}

                ////删除附件表
               // BLL.CommonService.DeleteAttachFileById(hSSEStandardsList.StandardId);

                db.Law_HSSEStandardsList.DeleteOnSubmit(hSSEStandardsList);
                db.SubmitChanges();
            }
        }
    }
}
