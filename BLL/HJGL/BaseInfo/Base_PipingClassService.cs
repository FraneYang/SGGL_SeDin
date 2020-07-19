namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_PipingClassService
    {
        /// <summary>
        ///获取管道等级信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_PipingClass GetPipingClassByPipingClassId(string pipingClassId)
        {
            return Funs.DB.Base_PipingClass.FirstOrDefault(e => e.PipingClassId == pipingClassId);
        }

        /// <summary>
        /// 增加管道等级信息
        /// </summary>
        /// <param name="PipingClass"></param>
        public static void AddPipingClass(Model.Base_PipingClass pipingClass)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PipingClass newPipingClass = new Base_PipingClass
            {
                PipingClassId = pipingClass.PipingClassId,
                PipingClassCode = pipingClass.PipingClassCode,
                PipingClassName = pipingClass.PipingClassName,
                Remark = pipingClass.Remark,
                PNO = pipingClass.PNO,
                ProjectId=pipingClass.ProjectId
            };
            db.Base_PipingClass.InsertOnSubmit(newPipingClass);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改管道等级信息 
        /// </summary>
        /// <param name="pipingClass"></param>
        public static void UpdatePipingClass(Model.Base_PipingClass pipingClass)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PipingClass newPipingClass = db.Base_PipingClass.FirstOrDefault(e => e.PipingClassId == pipingClass.PipingClassId);
            if (newPipingClass != null)
            {
                newPipingClass.PipingClassCode = pipingClass.PipingClassCode;
                newPipingClass.PipingClassName = pipingClass.PipingClassName;
                newPipingClass.Remark = pipingClass.Remark;
                newPipingClass.PNO = pipingClass.PNO;
                newPipingClass.ProjectId = pipingClass.ProjectId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据管道等级Id删除一个管道等级信息
        /// </summary>
        /// <param name="pipingClassId"></param>
        public static void DeletePipingClassByPipingClassId(string pipingClassId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PipingClass delPipingClass = db.Base_PipingClass.FirstOrDefault(e => e.PipingClassId == pipingClassId);
            if (delPipingClass != null)
            {
                db.Base_PipingClass.DeleteOnSubmit(delPipingClass);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取管道等级项
        /// </summary>
        /// <param name="PipingClassType"></param>
        /// <returns></returns>
        public static List<Model.Base_PipingClass> GetPipingClassList(string ProjectId)
        {
            var list = (from x in Funs.DB.Base_PipingClass where x.ProjectId==ProjectId 
                        orderby x.PipingClassCode
                        select x).ToList();

            return list;
        }

        #region 管道等级下拉项
        /// <summary>
        /// 管道等级下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="PipingClassType">耗材类型</param>
        public static void InitPipingClassDropDownList(FineUIPro.DropDownList dropName,string ProjectId, bool isShowPlease,string itemText)
        {
            dropName.DataValueField = "PipingClassId";
            dropName.DataTextField = "PipingClassCode";
            dropName.DataSource = GetPipingClassList(ProjectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName,itemText);
            }
        }
        #endregion
    }
}
