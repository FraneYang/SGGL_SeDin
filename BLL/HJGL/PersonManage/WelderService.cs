using System.Linq;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 焊工管理
    /// </summary>
    public static class WelderService
    {
        /// <summary>
        /// 根据主键获取焊工信息
        /// </summary>
        /// <param name="welderId"></param>
        /// <returns></returns>
        public static Model.SitePerson_Person GetWelderById(string welderId)
        {
            return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == welderId);
        }

        ///// <summary>
        ///// 添加焊工
        ///// </summary>
        ///// <param name="welder"></param>
        //public static void AddWelder(Model.SitePerson_Person welder)
        //{
        //    Model.SitePerson_Person newWelder = new Model.SitePerson_Person();
        //    newWelder.PersonId = welder.PersonId;
        //    newWelder.WelderCode = welder.WelderCode;
        //    newWelder.PersonName = welder.PersonName;
        //    newWelder.ProjectId = welder.ProjectId;
        //    newWelder.UnitId = welder.UnitId;
        //    newWelder.Sex = welder.Sex;
        //    newWelder.Birthday = welder.Birthday;
        //    newWelder.IdentityCard = welder.IdentityCard;
        //    newWelder.CertificateCode = welder.CertificateCode;
        //    newWelder.CertificateLimitTime = welder.CertificateLimitTime;
        //    newWelder.WelderLevel = welder.WelderLevel;
        //    newWelder.IsUsed = welder.IsUsed ;
        //    newWelder.QualificationCertificateUrl = welder.QualificationCertificateUrl;
        //    newWelder.Remark = welder.Remark;
        //    newWelder.PhotoUrl = welder.PhotoUrl;
        //    newWelder.WorkPostId = welder.WorkPostId;
        //    newWelder.Isprint = welder.Isprint;
        //    Funs.DB.SitePerson_Person.InsertOnSubmit(newWelder);
        //    Funs.DB.SubmitChanges();
        //}

        /// <summary>
        /// 修改焊工
        /// </summary>
        /// <param name="welder"></param>
        public static void UpdateWelder(Model.SitePerson_Person welder)
        {
            Model.SitePerson_Person newWelder = Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == welder.PersonId);
            if (newWelder != null)
            {
                newWelder.WelderCode = welder.WelderCode;
                newWelder.PersonName = welder.PersonName;
                newWelder.ProjectId = welder.ProjectId;
                newWelder.UnitId = welder.UnitId;
                newWelder.Sex = welder.Sex;
                newWelder.Birthday = welder.Birthday;
                newWelder.IdentityCard = welder.IdentityCard;
                newWelder.CertificateCode = welder.CertificateCode;
                newWelder.CertificateLimitTime = welder.CertificateLimitTime;
                newWelder.WelderLevel = welder.WelderLevel;
                newWelder.IsUsed = welder.IsUsed;
                newWelder.QualificationCertificateUrl = welder.QualificationCertificateUrl;
                newWelder.Remark = welder.Remark;
                newWelder.PhotoUrl = welder.PhotoUrl;
                newWelder.WorkPostId = welder.WorkPostId;
                newWelder.Isprint = welder.Isprint;
                Funs.DB.SubmitChanges();
            }
        }

        public static void UpdateQRCode(string welderId,string qRCode)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_Person newWelder = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == welderId);
            if (newWelder != null)
            {
                newWelder.QRCodeAttachUrl = qRCode;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除焊工信息
        /// </summary>
        /// <param name="welderId"></param>
        public static void DeleteWelderById(string welderId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.SitePerson_Person welder = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == welderId);
            if (welder != null)
            {
                db.SitePerson_Person.DeleteOnSubmit(welder);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在焊工号
        /// </summary>
        /// <param name="welderId"></param>
        /// <param name="welderCode"></param>
        /// <returns></returns>
        public static bool IsExisWelderCode(string welderId, string welderCode)
        {
            bool isExitCode = false;
            var q = from x in Funs.DB.SitePerson_Person where x.WelderCode == welderCode && x.PersonId != welderId select x;
            if (q.Count() > 0)
            {
                isExitCode = true;
            }
            return isExitCode;
        }

        /// <summary>
        /// 根据项目ID、单位ID获取焊工信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetWelderByProjectIdAndUnitId(string projectId, string unitId)
        {
            var users = from x in Funs.DB.SitePerson_Person
                        join y in Funs.DB.Base_Project on x.ProjectId equals y.ProjectId
                        where y.ProjectId == projectId && x.UnitId == unitId
                        select x;
            return users.ToList();
        }

        #region 项目焊工下拉项
        /// <summary>
        /// 项目焊工下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="InstallationType">耗材类型</param>
        public static void InitProjectWelderDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string projectId, string unitId,string itemText)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                dropName.DataValueField = "PersonId";
                dropName.DataTextField = "WelderCode";
                dropName.DataSource = from x in db.SitePerson_Person
                                      join y in db.Base_Project on x.ProjectId equals y.ProjectId
                                      where y.ProjectId == projectId && x.UnitId == unitId
                                      && x.WorkPostId == Const.WorkPost_Welder && (x.WelderCode != null || x.WelderCode != "")
                                      orderby x.WelderCode
                                      select x;
                dropName.DataBind();
                if (isShowPlease)
                {
                    Funs.FineUIPleaseSelect(dropName, itemText);
                }
            }
        }
        #endregion

        #region 项目焊工下拉项
        /// <summary>
        /// 项目焊工下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="InstallationType">耗材类型</param>
        public static void InitProjectWelderCodeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string projectId, string unitId)
        {
            dropName.DataValueField = "WelderCode";
            dropName.DataTextField = "WelderCode";
            dropName.DataSource = from x in Funs.DB.SitePerson_Person
                                  join y in Funs.DB.Base_Project on x.ProjectId equals y.ProjectId
                                  where y.ProjectId == projectId && x.UnitId == unitId
                                  && x.WorkPostId == Const.WorkPost_Welder && (x.WelderCode != null || x.WelderCode != "")
                                  select x; ;
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
            else
            {
                dropName.SelectedIndex = 0;
            }
        }
        #endregion

    }
}
