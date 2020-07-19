using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 无损检测工管理
    /// </summary>
    public static class CheckerService
    {
        /// <summary>
        /// 根据主键获取焊工信息
        /// </summary>
        /// <param name="CheckerId"></param>
        /// <returns></returns>
        public static Model.SitePerson_Person GetCheckerById(string CheckerId)
        {
            return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == CheckerId);
        }
        /// <summary>
        /// 添加焊工
        /// </summary>
        /// <param name="Checker"></param>
        public static void AddChecker(Model.SitePerson_Person Checker)
        {
            Model.SitePerson_Person newChecker = new Model.SitePerson_Person();
            newChecker.PersonId = Checker.PersonId;
            newChecker.WelderCode = Checker.WelderCode;
            newChecker.PersonName = Checker.PersonName;
            newChecker.Sex = Checker.Sex;
            newChecker.Birthday = Checker.Birthday;
            newChecker.UnitId = Checker.UnitId;
            newChecker.IdentityCard = Checker.IdentityCard;
            newChecker.IsUsed = Checker.IsUsed;
            newChecker.ProjectId = Checker.ProjectId;
            newChecker.WorkPostId = Checker.WorkPostId;
            newChecker.Isprint = Checker.Isprint;
            Funs.DB.SitePerson_Person.InsertOnSubmit(newChecker);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改焊工
        /// </summary>
        /// <param name="welder"></param>
        public static void UpdateChecker(Model.SitePerson_Person checker)
        {
            Model.SitePerson_Person newChecker = Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == checker.PersonId);
            if (newChecker != null)
            {
                newChecker.PersonId = checker.PersonId;
                newChecker.WelderCode = checker.WelderCode;
                newChecker.PersonName = checker.PersonName;
                newChecker.Sex = checker.Sex;
                newChecker.Birthday = checker.Birthday;
                newChecker.UnitId = checker.UnitId;
                newChecker.IdentityCard = checker.IdentityCard;
                newChecker.CertificateCode = checker.CertificateCode;
                newChecker.IsUsed = checker.IsUsed;
                newChecker.ProjectId = checker.ProjectId;
                newChecker.WorkPostId = checker.WorkPostId;
                newChecker.Isprint = checker.Isprint;
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除焊工信息
        /// </summary>
        /// <param name="checkerId"></param>
        public static void DeleteCheckerById(string checkerId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_Person checker = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == checkerId);
            if (checker != null)
            {
                db.SitePerson_Person.DeleteOnSubmit(checker);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 是否存在焊工号
        /// </summary>
        /// <param name="checkerId"></param>
        /// <param name="checkerCode"></param>
        /// <returns></returns>
        public static bool IsExisCheckerCode(string checkerId, string checkerCode)
        {
            bool isExitCode = false;
            var q = from x in Funs.DB.SitePerson_Person where x.WelderCode == checkerCode && x.PersonId != checkerId select x;
            if (q.Count() > 0)
            {
                isExitCode = true;
            }
            return isExitCode;
        }
    }
}
