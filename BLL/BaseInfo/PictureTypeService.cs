using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目图片分类
    /// </summary>
    public static class PictureTypeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目图片分类
        /// </summary>
        /// <param name="PictureTypeId"></param>
        /// <returns></returns>
        public static Model.Base_PictureType GetPictureTypeById(string PictureTypeId)
        {
            return Funs.DB.Base_PictureType.FirstOrDefault(e => e.PictureTypeId == PictureTypeId);
        }

        /// <summary>
        /// 添加项目图片分类
        /// </summary>
        /// <param name="PictureType"></param>
        public static void AddPictureType(Model.Base_PictureType PictureType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PictureType newPictureType = new Model.Base_PictureType
            {
                PictureTypeId = PictureType.PictureTypeId,
                Code = PictureType.Code,
                Name = PictureType.Name,
                Remark = PictureType.Remark
            };
            db.Base_PictureType.InsertOnSubmit(newPictureType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改项目图片分类
        /// </summary>
        /// <param name="PictureType"></param>
        public static void UpdatePictureType(Model.Base_PictureType PictureType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PictureType newPictureType = db.Base_PictureType.FirstOrDefault(e => e.PictureTypeId == PictureType.PictureTypeId);
            if (newPictureType != null)
            {
                newPictureType.Code = PictureType.Code;
                newPictureType.Name = PictureType.Name;
                newPictureType.Remark = PictureType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目图片分类
        /// </summary>
        /// <param name="PictureTypeId"></param>
        public static void DeletePictureTypeById(string PictureTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PictureType PictureType = db.Base_PictureType.FirstOrDefault(e => e.PictureTypeId == PictureTypeId);
            if (PictureType != null)
            {
                db.Base_PictureType.DeleteOnSubmit(PictureType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取项目图片分类下拉列表项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_PictureType> GetPictureTypeList()
        {
            return (from x in Funs.DB.Base_PictureType orderby x.Code select x).ToList();
        }

        #region 项目图片分类下拉框
        /// <summary>
        ///  项目图片分类下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitPictureTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "PictureTypeId";
            dropName.DataTextField = "Name";
            dropName.DataSource = GetPictureTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
