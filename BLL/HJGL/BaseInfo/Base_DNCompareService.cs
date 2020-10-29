using Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Base_DNCompareService
    {
        /// <summary>
        ///获取直径寸径对照信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_DNCompare GetDNCompareByDNCompareId(string dNCompareId)
        {
            return Funs.DB.Base_DNCompare.FirstOrDefault(e => e.DNCompareId == dNCompareId);
        }

        /// <summary>
        /// 增加直径寸径对照信息
        /// </summary>
        /// <param name="dNCompare"></param>
        public static void AddDNCompare(Model.Base_DNCompare dNCompare)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_DNCompare newDNCompare = new Base_DNCompare
            {
                DNCompareId = dNCompare.DNCompareId,
                DN = dNCompare.DN,
                PipeSize = dNCompare.PipeSize,
                OutSizeDia = dNCompare.OutSizeDia,
                SCH10 = dNCompare.SCH10,
                SCH20 = dNCompare.SCH20,
                SCH30 = dNCompare.SCH30,
                STD = dNCompare.STD,
                SCH40 = dNCompare.SCH40,
                SCH60 = dNCompare.SCH60,
                XS = dNCompare.XS,
                SCH80 = dNCompare.SCH80,
                SCH100 = dNCompare.SCH100,
                SCH120 = dNCompare.SCH120,
                SCH140 = dNCompare.SCH140,
                SCH160 = dNCompare.SCH160,
                XXS = dNCompare.XXS,
                Size = dNCompare.Size,
                Thickness=dNCompare.Thickness
            };

            db.Base_DNCompare.InsertOnSubmit(newDNCompare);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改直径寸径对照信息 
        /// </summary>
        /// <param name="dNCompare"></param>
        public static void UpdateDNCompare(Model.Base_DNCompare dNCompare)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_DNCompare newDNCompare = db.Base_DNCompare.FirstOrDefault(e => e.DNCompareId == dNCompare.DNCompareId);
            if (newDNCompare != null)
            {
                newDNCompare.DN = dNCompare.DN;
                newDNCompare.PipeSize = dNCompare.PipeSize;
                newDNCompare.OutSizeDia = dNCompare.OutSizeDia;
                newDNCompare.SCH10 = dNCompare.SCH10;
                newDNCompare.SCH20 = dNCompare.SCH20;
                newDNCompare.SCH30 = dNCompare.SCH30;
                newDNCompare.STD = dNCompare.STD;
                newDNCompare.SCH40 = dNCompare.SCH40;
                newDNCompare.SCH60 = dNCompare.SCH60;
                newDNCompare.XS = dNCompare.XS;
                newDNCompare.SCH80 = dNCompare.SCH80;
                newDNCompare.SCH100 = dNCompare.SCH100;
                newDNCompare.SCH120 = dNCompare.SCH120;
                newDNCompare.SCH140 = dNCompare.SCH140;
                newDNCompare.SCH160 = dNCompare.SCH160;
                newDNCompare.XXS = dNCompare.XXS;
                newDNCompare.Size = dNCompare.Size;
                newDNCompare.Thickness = dNCompare.Thickness;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据直径寸径对照Id删除一个直径寸径对照信息
        /// </summary>
        /// <param name="dNCompareId"></param>
        public static void DeleteDNCompareByDNCompareId(string dNCompareId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_DNCompare delDNCompare = db.Base_DNCompare.FirstOrDefault(e => e.DNCompareId == dNCompareId);
            if (delDNCompare != null)
            {
                db.Base_DNCompare.DeleteOnSubmit(delDNCompare);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 按类型获取直径寸径对照项
        /// </summary>
        /// <param name="DNCompareType"></param>
        /// <returns></returns>
        public static List<Model.Base_DNCompare> GetdNCompareList()
        {
            var list = (from x in Funs.DB.Base_DNCompare
                        orderby x.PipeSize,x.OutSizeDia
                        select x).ToList();

            return list;
        }

        public static decimal? GetSizeByDia(decimal dia)
        {
            var q = Funs.DB.Base_DNCompare.FirstOrDefault(x => x.OutSizeDia == dia);
            if (q != null)
            {
                return q.PipeSize;
            }
            else
            {
                return null;
            }
        }

        #region 直径寸径对照下拉项
        /// <summary>
        /// 直径寸径对照下拉项
        /// </summary>
        /// <param name="dropName">下拉框名称</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        /// <param name="DNCompareType">耗材类型</param>
        public static void InitDNCompareDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "DNCompareId";
            dropName.DataTextField = "PipeSize";
            dropName.DataSource = GetdNCompareList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
