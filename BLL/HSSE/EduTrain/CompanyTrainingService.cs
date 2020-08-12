using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 公司培训主表
    /// </summary>
    public class CompanyTrainingService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取公司培训
        /// </summary>
        /// <param name="companyTrainingId"></param>
        /// <returns></returns>
        public static Model.Training_CompanyTraining GetCompanyTrainingById(string companyTrainingId)
        {
            return db.Training_CompanyTraining.FirstOrDefault(e => e.CompanyTrainingId == companyTrainingId);
        }

        /// <summary>
        /// 添加公司培训
        /// </summary>
        /// <param name="companyTraining"></param>
        public static void AddCompanyTraining(Model.Training_CompanyTraining companyTraining)
        {
            Model.Training_CompanyTraining newCompanyTraining = new Model.Training_CompanyTraining();
            newCompanyTraining.CompanyTrainingId = companyTraining.CompanyTrainingId;
            newCompanyTraining.CompanyTrainingCode = companyTraining.CompanyTrainingCode;
            newCompanyTraining.CompanyTrainingName = companyTraining.CompanyTrainingName;
            newCompanyTraining.SupCompanyTrainingId = companyTraining.SupCompanyTrainingId;
            newCompanyTraining.IsEndLever = companyTraining.IsEndLever;
            db.Training_CompanyTraining.InsertOnSubmit(newCompanyTraining);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改公司培训
        /// </summary>
        /// <param name="companyTraining"></param>
        public static void UpdateCompanyTraining(Model.Training_CompanyTraining companyTraining)
        {
            Model.Training_CompanyTraining newCompanyTraining = db.Training_CompanyTraining.FirstOrDefault(e => e.CompanyTrainingId == companyTraining.CompanyTrainingId);
            if (newCompanyTraining != null)
            {
                newCompanyTraining.CompanyTrainingCode = companyTraining.CompanyTrainingCode;
                newCompanyTraining.CompanyTrainingName = companyTraining.CompanyTrainingName;
                newCompanyTraining.SupCompanyTrainingId = companyTraining.SupCompanyTrainingId;
                newCompanyTraining.IsEndLever = companyTraining.IsEndLever;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除公司培训
        /// </summary>
        /// <param name="companyTrainingId"></param>
        public static void DeleteCompanyTraining(string companyTrainingId)
        {
            Model.Training_CompanyTraining companyTraining = db.Training_CompanyTraining.FirstOrDefault(e => e.CompanyTrainingId == companyTrainingId);
            if (companyTraining!=null)
            {
                db.Training_CompanyTraining.DeleteOnSubmit(companyTraining);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteCompanyTraining(string companyTrainingId)
        {
            bool isDelete = true;
            var Training = BLL.CompanyTrainingService.GetCompanyTrainingById(companyTrainingId);
            if (Training != null)
            {
                if (Training.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Training_CompanyTrainingItem.FirstOrDefault(x => x.CompanyTrainingId == companyTrainingId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.CompanyTrainingService.GetCompanyTrainingBySupItem(companyTrainingId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }

        /// <summary>
        /// 根据上级Id查询所有公司培训
        /// </summary>
        /// <param name="supItem">上级Id</param>
        /// <returns>公司培训的集合</returns>
        public static List<Model.Training_CompanyTraining> GetCompanyTrainingBySupItem(string supItem)
        {
            return (from x in Funs.DB.Training_CompanyTraining where x.SupCompanyTrainingId == supItem select x).ToList();
        }

        /// <summary>
        /// 根据上级Id查询所有公司培训
        /// </summary>
        /// <param name="supItem">上级Id</param>
        /// <returns>公司培训的集合</returns>
        public static List<Model.Training_CompanyTraining> GetCompanyTrainingIsEnd()
        {
            return (from x in Funs.DB.Training_CompanyTraining
                    where x.IsEndLever == true
                    select x).ToList();
        }

        /// <summary>
        /// 教材类型下拉框
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitCompanyTrainingDropDownList(FineUIPro.DropDownList dropName,string supItem, bool isShowPlease)
        {
            dropName.DataValueField = "CompanyTrainingId";
            dropName.DataTextField = "CompanyTrainingName";
            dropName.DataSource = GetCompanyTrainingBySupItem(supItem);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 教材类型下拉框（末级）
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitCompanyTrainingIsEndDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "CompanyTrainingId";
            dropName.DataTextField = "CompanyTrainingName";
            dropName.DataSource = GetCompanyTrainingIsEnd();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}