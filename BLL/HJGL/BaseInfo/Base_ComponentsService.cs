namespace BLL
{
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public static class Base_ComponentsService
    {
        /// <summary>
        ///获取安装组件信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Components GetComponentsByComponentsId(string componentsId)
        {
            return Funs.DB.Base_Components.FirstOrDefault(e => e.ComponentsId == componentsId);
        }

        /// <summary>
        /// 增加安装组件信息
        /// </summary>
        /// <param name="Components"></param>
        public static void AddComponents(Model.Base_Components components)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Components newComponents = new Base_Components
            {
                ComponentsId = components.ComponentsId,
                ComponentsCode = components.ComponentsCode,
                ComponentsName = components.ComponentsName,
                Remark = components.Remark,
            };
            db.Base_Components.InsertOnSubmit(newComponents);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安装组件信息 
        /// </summary>
        /// <param name="Components"></param>
        public static void UpdateComponents(Model.Base_Components components)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Components newComponents = db.Base_Components.FirstOrDefault(e => e.ComponentsId == components.ComponentsId);
            if (newComponents != null)
            {
                newComponents.ComponentsCode = components.ComponentsCode;
                newComponents.ComponentsName = components.ComponentsName;
                newComponents.Remark = components.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安装组件Id删除一个安装组件信息
        /// </summary>
        /// <param name="ComponentsId"></param>
        public static void DeleteComponentsByComponentsId(string componentsId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Components delComponents = db.Base_Components.FirstOrDefault(e => e.ComponentsId == componentsId);
            if (delComponents != null)
            {
                db.Base_Components.DeleteOnSubmit(delComponents);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取安装组件项
        /// </summary>
        /// <param name="ComponentsType"></param>
        /// <returns></returns>
        public static List<Model.Base_Components> GetComponentsList()
        {
            var list = (from x in Funs.DB.Base_Components
                        orderby x.ComponentsCode
                        select x).ToList();

            return list;
        }

        #region 安装组件下拉项
        /// <summary>
        /// 安装组件下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="ComponentsType">耗材类型</param>
        public static void InitComponentsDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease,string itemText)
        {
            dropName.DataValueField = "ComponentsId";
            dropName.DataTextField = "ComponentsCode";
            dropName.DataSource = GetComponentsList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName,itemText);
            }
        }
        #endregion
    }
}
