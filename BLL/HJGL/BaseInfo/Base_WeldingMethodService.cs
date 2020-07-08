namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_WeldingMethodService
    {
        /// <summary>
        ///获取焊接方法信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_WeldingMethod GetWeldingMethodByWeldingMethodId(string weldingMethodId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_WeldingMethod.FirstOrDefault(e => e.WeldingMethodId == weldingMethodId);
        }

        /// <summary>
        /// 增加焊接方法信息
        /// </summary>
        /// <param name="weldingMethod"></param>
        public static void AddWeldingMethod(Model.Base_WeldingMethod weldingMethod)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldingMethod newWeldingMethod = new Base_WeldingMethod
            {
                WeldingMethodId = weldingMethod.WeldingMethodId,
                WeldingMethodCode = weldingMethod.WeldingMethodCode,
                WeldingMethodName = weldingMethod.WeldingMethodName,
                Remark = weldingMethod.Remark,
                ConsumablesType=weldingMethod.ConsumablesType,
            };
            db.Base_WeldingMethod.InsertOnSubmit(newWeldingMethod);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接方法信息 
        /// </summary>
        /// <param name="weldingMethod"></param>
        public static void UpdateWeldingMethod(Model.Base_WeldingMethod weldingMethod)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldingMethod newWeldingMethod = db.Base_WeldingMethod.FirstOrDefault(e => e.WeldingMethodId == weldingMethod.WeldingMethodId);
            if (newWeldingMethod != null)
            {
                newWeldingMethod.WeldingMethodCode = weldingMethod.WeldingMethodCode;
                newWeldingMethod.WeldingMethodName = weldingMethod.WeldingMethodName;
                newWeldingMethod.Remark = weldingMethod.Remark;
                newWeldingMethod.ConsumablesType = weldingMethod.ConsumablesType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据焊接方法Id删除一个焊接方法信息
        /// </summary>
        /// <param name="weldingMethodId"></param>
        public static void DeleteWeldingMethodByWeldingMethodId(string weldingMethodId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldingMethod delWeldingMethod = db.Base_WeldingMethod.FirstOrDefault(e => e.WeldingMethodId == weldingMethodId);
            if (delWeldingMethod != null)
            {
                db.Base_WeldingMethod.DeleteOnSubmit(delWeldingMethod);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取焊接方法项
        /// </summary>
        /// <param name="WeldingMethodType"></param>
        /// <returns></returns>
        public static List<Model.Base_WeldingMethod> GetWeldingMethodList()
        {
            var list = (from x in new Model.SGGLDB(Funs.ConnString).Base_WeldingMethod
                        orderby x.WeldingMethodCode
                        select x).ToList();

            return list;
        }

        #region 焊接方法下拉项
        /// <summary>
        /// 焊接方法下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="WeldingMethodType">耗材类型</param>
        public static void InitWeldingMethodDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease,string itemText)
        {
            dropName.DataValueField = "WeldingMethodId";
            dropName.DataTextField = "WeldingMethodCode";
            dropName.DataSource = GetWeldingMethodList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName,itemText);
            }
        }
        #endregion
    }
}
