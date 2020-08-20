namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_PressurePipingClassService
    {
        /// <summary>
        ///获取压力管道级别信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_PressurePipingClass GetPressurePipingClass(string pressurePipingClassId)
        {
            return Funs.DB.Base_PressurePipingClass.FirstOrDefault(e => e.PressurePipingClassId == pressurePipingClassId);
        }

        /// <summary>
        /// 增加压力管道级别
        /// </summary>
        /// <param name="grooveType"></param>
        public static void AddPressurePipingClass(Model.Base_PressurePipingClass pipingClass)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PressurePipingClass newPipingClass = new Base_PressurePipingClass
            {
                PressurePipingClassId = pipingClass.PressurePipingClassId,
                PressurePipingClassCode = pipingClass.PressurePipingClassCode,
                PressurePipingType = pipingClass.PressurePipingType,
                Remark = pipingClass.Remark,
            };

            db.Base_PressurePipingClass.InsertOnSubmit(newPipingClass);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改压力管道级别信息 
        /// </summary>
        /// <param name="grooveType"></param>
        public static void UpdatePressurePipingClass(Model.Base_PressurePipingClass pipingClass)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PressurePipingClass newPipingClass = db.Base_PressurePipingClass.FirstOrDefault(e => e.PressurePipingClassId == pipingClass.PressurePipingClassId);
            if (newPipingClass != null)
            {
                newPipingClass.PressurePipingClassCode = pipingClass.PressurePipingClassCode;
                newPipingClass.PressurePipingType = pipingClass.PressurePipingType;
                newPipingClass.Remark = pipingClass.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据压力管道级别Id删除信息
        /// </summary>
        /// <param name="grooveTypeId"></param>
        public static void DeletePressurePipingClass(string pipingClassId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PressurePipingClass del = db.Base_PressurePipingClass.FirstOrDefault(e => e.PressurePipingClassId == pipingClassId);
            if (del != null)
            {
                db.Base_PressurePipingClass.DeleteOnSubmit(del);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取压力管道级别
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_PressurePipingClass> GetPressurePipingClassList()
        {
            var list = (from x in Funs.DB.Base_PressurePipingClass
                        orderby x.PressurePipingClassCode
                        select x).ToList();

            return list;
        }

        #region 压力管道级别下拉项
        /// <summary>
        /// 吹洗方法下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="GrooveTypeType">耗材类型</param>
        public static void InitPressurePipingClassDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease, string itemText)
        {
            dropName.DataValueField = "PressurePipingClassId";
            dropName.DataTextField = "PressurePipingClassCode";
            dropName.DataSource = GetPressurePipingClassList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName, itemText);
            }
        }
        #endregion
    }
}
