using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 焊接位置
    /// </summary>
    public static class Base_WeldingLocationServie
    {
        /// <summary>
        /// 根据主键获取焊接位置信息
        /// </summary>
        /// <param name="weldingLocationId"></param>
        /// <returns></returns>
        public static Model.Base_WeldingLocation GetWeldingLocationById(string weldingLocationId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_WeldingLocation.FirstOrDefault(e => e.WeldingLocationId == weldingLocationId);
        }

        /// <summary>
        /// 增加焊接位置
        /// </summary>
        /// <param name="weldingLocation"></param>
        public static void AddWeldingLocation(Model.Base_WeldingLocation weldingLocation)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldingLocation newWeldingLocation = new Model.Base_WeldingLocation();
            newWeldingLocation.WeldingLocationId = weldingLocation.WeldingLocationId;
            newWeldingLocation.WeldingLocationCode = weldingLocation.WeldingLocationCode;
            newWeldingLocation.WeldingLocationName = weldingLocation.WeldingLocationName;
            newWeldingLocation.Remark = weldingLocation.Remark;
            db.Base_WeldingLocation.InsertOnSubmit(weldingLocation);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊接位置
        /// </summary>
        /// <param name="weldingLocation"></param>
        public static void UpdateWeldingLocation(Model.Base_WeldingLocation weldingLocation)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldingLocation newWeldingLocation = db.Base_WeldingLocation.FirstOrDefault(e => e.WeldingLocationId == weldingLocation.WeldingLocationId);
            if (newWeldingLocation != null)
            {
                newWeldingLocation.WeldingLocationCode = weldingLocation.WeldingLocationCode;
                newWeldingLocation.WeldingLocationName = weldingLocation.WeldingLocationName;
                newWeldingLocation.Remark = weldingLocation.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除焊接位置
        /// </summary>
        /// <param name="weldingLocationId"></param>
        public static void DeleteWeldingLocationById(string weldingLocationId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_WeldingLocation weldingLocation = db.Base_WeldingLocation.FirstOrDefault(e => e.WeldingLocationId == weldingLocationId);
            if (weldingLocation != null)
            {
                db.Base_WeldingLocation.DeleteOnSubmit(weldingLocation);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取焊接位置列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_WeldingLocation> GetWeldingLocationList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_WeldingLocation orderby x.WeldingLocationCode select x).ToList();
        }

        /// <summary>
        /// 获取下拉列表选择项
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        /// <param name="itemText"></param>
        public static void InitWeldingLocationDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "WeldingLocationId";
            dropName.DataTextField = "WeldingLocationCode";
            dropName.DataSource = GetWeldingLocationList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
