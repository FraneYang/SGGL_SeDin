using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_MediumService
    {
        /// <summary>
        ///获取介质信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Medium GetMediumByMediumId(string mediumId)
        {
            return Funs.DB.Base_Medium.FirstOrDefault(e => e.MediumId == mediumId);
        }

        /// <summary>
        /// 增加介质信息
        /// </summary>
        /// <param name="medium"></param>
        public static void AddMedium(Model.Base_Medium medium)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Medium newMedium = new Base_Medium
            {
                MediumId = medium.MediumId,
                MediumCode = medium.MediumCode,
                MediumName = medium.MediumName,
                MediumAbbreviation = medium.MediumAbbreviation,
                IsTestMedium=medium.IsTestMedium,
                Remark = medium.Remark,
                ProjectId=medium.ProjectId
            };
            db.Base_Medium.InsertOnSubmit(newMedium);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改介质信息 
        /// </summary>
        /// <param name="medium"></param>
        public static void UpdateMedium(Model.Base_Medium medium)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Medium newMedium = db.Base_Medium.FirstOrDefault(e => e.MediumId == medium.MediumId);
            if (newMedium != null)
            {
                newMedium.MediumCode = medium.MediumCode;
                newMedium.MediumName = medium.MediumName;
                newMedium.MediumAbbreviation = medium.MediumAbbreviation;
                newMedium.IsTestMedium = medium.IsTestMedium;
                newMedium.Remark = medium.Remark;
                newMedium.ProjectId = medium.ProjectId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据介质Id删除一个安装组件信息
        /// </summary>
        /// <param name="mediumId"></param>
        public static void DeleteMediumByMediumId(string mediumId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Medium delMedium = db.Base_Medium.FirstOrDefault(e => e.MediumId == mediumId);
            if (delMedium != null)
            {
                db.Base_Medium.DeleteOnSubmit(delMedium);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取介质项
        /// </summary>
        /// <param name="MediumType"></param>
        /// <returns></returns>
        public static List<Model.Base_Medium> GetMediumList(bool? isTestMedium, string ProjectId)
        {
            List<Model.Base_Medium> list = null;
            if (isTestMedium == null)
            {
                list = (from x in Funs.DB.Base_Medium
                        where x.ProjectId==ProjectId
                        orderby x.MediumCode
                        select x).ToList();
            }
            else
            {
                list = (from x in Funs.DB.Base_Medium
                        where x.IsTestMedium == isTestMedium
                        orderby x.MediumCode
                        select x).ToList();
            }

            return list;
        }

        #region 介质下拉项
        /// <summary>
        /// 介质下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="MediumType">耗材类型</param>
        public static void InitMediumDropDownList(FineUIPro.DropDownList dropName,string ProjectId, bool? isTestMedium, bool isShowPlease)
        {
            dropName.DataValueField = "MediumId";
            dropName.DataTextField = "MediumName";
            dropName.DataSource = GetMediumList(isTestMedium, ProjectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
