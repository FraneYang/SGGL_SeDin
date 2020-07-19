using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_PressureService
    {
        /// <summary>
        ///获取试压类型信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Pressure GetPressureByPressureId(string pressureId)
        {
            return Funs.DB.Base_Pressure.FirstOrDefault(e => e.PressureId == pressureId);
        }

        /// <summary>
        /// 增加试压类型信息
        /// </summary>
        /// <param name="pressure"></param>
        public static void AddPressure(Model.Base_Pressure pressure)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Pressure newPressure = new Base_Pressure
            {
                PressureId = pressure.PressureId,
                PressureCode = pressure.PressureCode,
                PressureName = pressure.PressureName,
                Remark = pressure.Remark,
            };
            db.Base_Pressure.InsertOnSubmit(newPressure);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试压类型信息 
        /// </summary>
        /// <param name="pressure"></param>
        public static void UpdatePressure(Model.Base_Pressure pressure)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Pressure newPressure = db.Base_Pressure.FirstOrDefault(e => e.PressureId == pressure.PressureId);
            if (newPressure != null)
            {
                newPressure.PressureCode = pressure.PressureCode;
                newPressure.PressureName = pressure.PressureName;
                newPressure.Remark = pressure.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据试压类型Id删除一个试压类型信息
        /// </summary>
        /// <param name="pressureId"></param>
        public static void DeletePressureByPressureId(string pressureId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_Pressure delPressure = db.Base_Pressure.FirstOrDefault(e => e.PressureId == pressureId);
            if (delPressure != null)
            {
                db.Base_Pressure.DeleteOnSubmit(delPressure);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取试压类型项
        /// </summary>
        /// <param name="PressureType"></param>
        /// <returns></returns>
        public static List<Model.Base_Pressure> GetPressureList()
        {
            var list = (from x in Funs.DB.Base_Pressure
                        orderby x.PressureCode
                        select x).ToList();

            return list;
        }

        #region 试压类型下拉项
        /// <summary>
        /// 试压类型下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="PressureType">耗材类型</param>
        public static void InitPressureDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "PressureId";
            dropName.DataTextField = "PressureCode";
            dropName.DataSource = GetPressureList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
