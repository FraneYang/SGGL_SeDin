using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_TestMediumService
    {
        /// <summary>
        ///获取介质信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_TestMedium GetTestMediumById(string mediumId)
        {
            return Funs.DB.Base_TestMedium.FirstOrDefault(e => e.TestMediumId == mediumId);
        }

        /// <summary>
        /// 增加介质信息
        /// </summary>
        /// <param name="medium"></param>
        public static void AddTestMedium(Model.Base_TestMedium medium)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_TestMedium newMedium = new Base_TestMedium
            {
                TestMediumId = medium.TestMediumId,
                MediumCode = medium.MediumCode,
                MediumName = medium.MediumName,
                TestType=medium.TestType,
                Remark = medium.Remark,
            };
            db.Base_TestMedium.InsertOnSubmit(newMedium);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改介质信息 
        /// </summary>
        /// <param name="medium"></param>
        public static void UpdateTestMedium(Model.Base_TestMedium medium)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_TestMedium newMedium = db.Base_TestMedium.FirstOrDefault(e => e.TestMediumId == medium.TestMediumId);
            if (newMedium != null)
            {
                newMedium.MediumCode = medium.MediumCode;
                newMedium.MediumName = medium.MediumName;
                newMedium.TestType = medium.TestType;
                newMedium.Remark = medium.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据介质Id删除一个安装组件信息
        /// </summary>
        /// <param name="mediumId"></param>
        public static void DeleteTestMediumByMediumId(string mediumId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_TestMedium delMedium = db.Base_TestMedium.FirstOrDefault(e => e.TestMediumId == mediumId);
            if (delMedium != null)
            {
                db.Base_TestMedium.DeleteOnSubmit(delMedium);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取介质项
        /// </summary>
        /// <param name="testType">1-试压介质，2-泄漏性试验介质，3-真空试验介质，4-吹扫介质，5-清洗介质</param>
        /// <returns></returns>
        public static List<Model.Base_TestMedium> GetTestMediumList(string testType)
        {
            List<Model.Base_TestMedium> list = null;
            list = (from x in Funs.DB.Base_TestMedium
                    where x.TestType == testType
                    orderby x.MediumCode
                    select x).ToList();

            return list;
        }

        #region 介质下拉项
        /// <summary>
        /// 介质下拉项
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="testType">1-试压介质，2-泄漏性试验介质，3-真空试验介质，4-吹扫介质，5-清洗介质</param></param>
        /// <param name="isShowPlease"></param>
        public static void InitMediumDropDownList(FineUIPro.DropDownList dropName, string testType, bool isShowPlease)
        {
            dropName.DataValueField = "TestMediumId";
            dropName.DataTextField = "MediumName";
            dropName.DataSource = GetTestMediumList(testType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
