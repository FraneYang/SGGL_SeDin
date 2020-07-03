using System.Linq;

namespace BLL
{
    /// <summary>
    /// 公司培训明细表
    /// </summary>
    public class CompanyTrainingItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取公司培训明细信息
        /// </summary>
        /// <param name="companyTrainingItemId"></param>
        /// <returns></returns>
        public static Model.Training_CompanyTrainingItem GetCompanyTrainingItemById(string companyTrainingItemId)
        {
            return db.Training_CompanyTrainingItem.FirstOrDefault(e => e.CompanyTrainingItemId == companyTrainingItemId);
        }
        
        /// <summary>
        /// 添加公司培训明细
        /// </summary>
        /// <param name="companyTrainingItem"></param>
        public static void AddCompanyTrainingItem(Model.Training_CompanyTrainingItem companyTrainingItem)
        {
            Model.Training_CompanyTrainingItem newCompanyTrainingItem = new Model.Training_CompanyTrainingItem();
            newCompanyTrainingItem.CompanyTrainingItemId = companyTrainingItem.CompanyTrainingItemId;
            newCompanyTrainingItem.CompanyTrainingId = companyTrainingItem.CompanyTrainingId;
            newCompanyTrainingItem.CompanyTrainingItemCode = companyTrainingItem.CompanyTrainingItemCode;
            newCompanyTrainingItem.CompanyTrainingItemName = companyTrainingItem.CompanyTrainingItemName;
            newCompanyTrainingItem.AttachUrl = companyTrainingItem.AttachUrl;
            newCompanyTrainingItem.CompileMan = companyTrainingItem.CompileMan;
            newCompanyTrainingItem.CompileDate = companyTrainingItem.CompileDate;
            db.Training_CompanyTrainingItem.InsertOnSubmit(newCompanyTrainingItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改公司培训明细
        /// </summary>
        /// <param name="companyTrainingItem"></param>
        public static void UpdateCompanyTrainingItem(Model.Training_CompanyTrainingItem companyTrainingItem)
        {
            Model.Training_CompanyTrainingItem newCompanyTrainingItem = db.Training_CompanyTrainingItem.FirstOrDefault(e => e.CompanyTrainingItemId == companyTrainingItem.CompanyTrainingItemId);
            if (newCompanyTrainingItem != null)
            {
                newCompanyTrainingItem.CompanyTrainingItemCode = companyTrainingItem.CompanyTrainingItemCode;
                newCompanyTrainingItem.CompanyTrainingItemName = companyTrainingItem.CompanyTrainingItemName;
                newCompanyTrainingItem.AttachUrl = companyTrainingItem.AttachUrl;
                newCompanyTrainingItem.CompileMan = companyTrainingItem.CompileMan;
                newCompanyTrainingItem.CompileDate = companyTrainingItem.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除明细信息
        /// </summary>
        /// <param name="companyTrainItemId"></param>
        public static void DeleteCompanyTrainingItemById(string companyTrainItemId)
        {
            Model.Training_CompanyTrainingItem companyTrainingItem = db.Training_CompanyTrainingItem.FirstOrDefault(e => e.CompanyTrainingItemId == companyTrainItemId);
            if (companyTrainingItem != null)
            {
                db.Training_CompanyTrainingItem.DeleteOnSubmit(companyTrainingItem);
                db.SubmitChanges();
            }
        }
    }
}