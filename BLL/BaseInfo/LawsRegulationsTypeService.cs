﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 法律法规类型
    /// </summary>
    public static class LawsRegulationsTypeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据id获取法律法规类型信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Model.Base_LawsRegulationsType GetLawsRegulationsTypeById(string id)
        {
            return Funs.DB.Base_LawsRegulationsType.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// 添加法律法规类型
        /// </summary>
        /// <param name="lawsRegulationsType"></param>
        public static void AddLawsRegulationsType(Model.Base_LawsRegulationsType lawsRegulationsType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_LawsRegulationsType newLawsRegulationsType = new Model.Base_LawsRegulationsType
            {
                Id = lawsRegulationsType.Id,
                Code = lawsRegulationsType.Code,
                Name = lawsRegulationsType.Name,
                Remark = lawsRegulationsType.Remark
            };
            db.Base_LawsRegulationsType.InsertOnSubmit(newLawsRegulationsType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改法律法规类型
        /// </summary>
        /// <param name="lawsRegulationsType"></param>
        public static void UpdateLawsRegulationsType(Model.Base_LawsRegulationsType lawsRegulationsType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_LawsRegulationsType newLawsRegulationsType = db.Base_LawsRegulationsType.FirstOrDefault(e => e.Id == lawsRegulationsType.Id);
            if (newLawsRegulationsType != null)
            {
                newLawsRegulationsType.Code = lawsRegulationsType.Code;
                newLawsRegulationsType.Name = lawsRegulationsType.Name;
                newLawsRegulationsType.Remark = lawsRegulationsType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除法律法规信息
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteLawsRegulationsTypeById(string id)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_LawsRegulationsType lawsRegulationsType = db.Base_LawsRegulationsType.FirstOrDefault(e => e.Id == id);
            if (lawsRegulationsType != null)
            {
                db.Base_LawsRegulationsType.DeleteOnSubmit(lawsRegulationsType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 法律法规下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_LawsRegulationsType> GetLawsRegulationsTypeList()
        {
            var list = (from x in Funs.DB.Base_LawsRegulationsType orderby x.Code select x).ToList();           
            return list;
        }

        #region 类型表下拉框
        /// <summary>
        ///  单位类型表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitLawsRegulationsTypeDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Id";
            dropName.DataTextField = "Name";
            dropName.DataSource = GetLawsRegulationsTypeList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
