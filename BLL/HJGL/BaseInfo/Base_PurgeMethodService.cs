﻿namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;

    public static class Base_PurgeMethodService
    {
        /// <summary>
        ///获取吹洗方法信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_PurgeMethod GetPurgeMethod(string purgeMethodId)
        {
            return Funs.DB.Base_PurgeMethod.FirstOrDefault(e => e.PurgeMethodId == purgeMethodId);
        }

        /// <summary>
        /// 增加吹洗方法
        /// </summary>
        /// <param name="grooveType"></param>
        public static void AddPurgeMethod(Model.Base_PurgeMethod purgeMethod)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PurgeMethod newPurgeMethod = new Base_PurgeMethod
            {
                PurgeMethodId = purgeMethod.PurgeMethodId,
                PurgeMethodCode = purgeMethod.PurgeMethodCode,
                PurgeMethodName = purgeMethod.PurgeMethodName,
                Remark = purgeMethod.Remark,
            };

            db.Base_PurgeMethod.InsertOnSubmit(newPurgeMethod);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改吹洗方法信息 
        /// </summary>
        /// <param name="grooveType"></param>
        public static void UpdatePurgeMethod(Model.Base_PurgeMethod purgeMethod)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PurgeMethod newPurgeMethod = db.Base_PurgeMethod.FirstOrDefault(e => e.PurgeMethodId == purgeMethod.PurgeMethodId);
            if (newPurgeMethod != null)
            {
                newPurgeMethod.PurgeMethodCode = purgeMethod.PurgeMethodCode;
                newPurgeMethod.PurgeMethodName = purgeMethod.PurgeMethodName;
                newPurgeMethod.Remark = purgeMethod.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据吹洗方法Id删除信息
        /// </summary>
        /// <param name="grooveTypeId"></param>
        public static void DeletePurgeMethod(string purgeMethodId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PurgeMethod del = db.Base_PurgeMethod.FirstOrDefault(e => e.PurgeMethodId == purgeMethodId);
            if (del != null)
            {
                db.Base_PurgeMethod.DeleteOnSubmit(del);
                db.SubmitChanges();
            }
        }
        public static ListItem[] GetMediumListItem(string ProjectId)
        {
            var list = (from x in Funs.DB.Base_Medium where x.ProjectId == ProjectId orderby x.MediumCode select x).ToList();
            ListItem[] item = new ListItem[list.Count()];
            for (int i = 0; i < list.Count(); i++)
            {
                item[i] = new ListItem(list[i].MediumName ?? "", list[i].MediumId);
            }
            return item;
        }
        /// <summary>
        /// 获取吹洗方法列表
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetPurgeMethodList()
        {
            var list = (from x in Funs.DB.Base_PurgeMethod
                        orderby x.PurgeMethodCode
                        select x).ToList();
            ListItem[] item = new ListItem[list.Count()];
            for (int i = 0; i < list.Count(); i++)
            {
                item[i] = new ListItem(list[i].PurgeMethodName+(list[i].PurgeMethodCode) ?? "", list[i].PurgeMethodId);
            }
            return item;
        }

        #region 吹洗方法下拉项
        /// <summary>
        /// 吹洗方法下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="GrooveTypeType">耗材类型</param>
        public static void InitPurgeMethodDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string itemText)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetPurgeMethodList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }
        #endregion
    }
}
