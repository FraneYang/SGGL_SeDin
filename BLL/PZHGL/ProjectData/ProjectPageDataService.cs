namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;
    using System;

    public static class ProjectPageDataService
    {
        public static SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        ///获取移动端首页
        /// </summary>
        /// <returns></returns>
        public static Wx_PageData GetPageDataByPageDataId(string PageDataId)
        {
            return new Model.SGGLDB(Funs.ConnString).Wx_PageData.FirstOrDefault(e => e.PageDataId == PageDataId);
        }

        /// <summary>
        /// 增加移动端首页
        /// </summary>
        /// <returns></returns>
        public static void AddPageData(Wx_PageData PageData)
        {
            SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Wx_PageData newPageData = new Wx_PageData
            {
                PageDataId = PageData.PageDataId,
                ProjectId = PageData.ProjectId,
                CreatDate = DateTime.Now,
                CreatManId = PageData.CreatManId,
                SafeHours = PageData.SafeHours,
                SitePersonNum = PageData.SitePersonNum,
                SpecialEquipmentNum = PageData.SpecialEquipmentNum,
                EntryTrainingNum = PageData.EntryTrainingNum,
                HiddenDangerNum = PageData.HiddenDangerNum,
                RectificationNum = PageData.RectificationNum,
                RiskI = PageData.RiskI,
                RiskII = PageData.RiskII,
                RiskIII = PageData.RiskIII,
                RiskIV = PageData.RiskIV,
                RiskV = PageData.RiskV,
            };
            db.Wx_PageData.InsertOnSubmit(newPageData);
            db.SubmitChanges();
        }

        /// <summary>
        ///修改移动端首页 
        /// </summary>
        /// <param name="PageData"></param>
        public static void UpdatePageData(Wx_PageData PageData)
        {
            SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Wx_PageData newPageData = db.Wx_PageData.FirstOrDefault(e => e.PageDataId == PageData.PageDataId);
            if (newPageData != null)
            {
                newPageData.SafeHours = PageData.SafeHours;
                newPageData.SitePersonNum = PageData.SitePersonNum;
                newPageData.SpecialEquipmentNum = PageData.SpecialEquipmentNum;
                newPageData.EntryTrainingNum = PageData.EntryTrainingNum;
                newPageData.HiddenDangerNum = PageData.HiddenDangerNum;
                newPageData.RectificationNum = PageData.RectificationNum;
                newPageData.RiskI = PageData.RiskI;
                newPageData.RiskII = PageData.RiskII;
                newPageData.RiskIII = PageData.RiskIII;
                newPageData.RiskIV = PageData.RiskIV;
                newPageData.RiskV = PageData.RiskV;
                db.SubmitChanges();                
            }
        }
    }
}
