using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Person_ShuntDetailService
    {
        /// <summary>
        /// 增加分流管理明细信息
        /// </summary>
        /// <param name="pauseNotice">分流管理明细实体</param>
        public static void AddShuntDetail(Model.Person_ShuntDetail a)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_ShuntDetail newShuntDetail = new Model.Person_ShuntDetail();
            newShuntDetail.ShuntDetailId = a.ShuntDetailId;
            newShuntDetail.ShuntId = a.ShuntId;
            newShuntDetail.UserId = a.UserId;
            newShuntDetail.WorkPostId = a.WorkPostId;
            newShuntDetail.SortIndex = a.SortIndex;
            db.Person_ShuntDetail.InsertOnSubmit(newShuntDetail);
            db.SubmitChanges();
        }
        /// <summary>
        /// 修改分流管理明细信息
        /// </summary>
        /// <param name="pauseNotice">分流管理明细实体</param>
        public static void UpdateShuntDetail(Model.Person_ShuntDetail a)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_ShuntDetail newShuntDetail = db.Person_ShuntDetail.First(e => e.ShuntDetailId == a.ShuntDetailId);
            newShuntDetail.ShuntId = a.ShuntId;
            newShuntDetail.UserId = a.UserId;
            newShuntDetail.WorkPostId = a.WorkPostId;
            db.SubmitChanges();
        }
        /// <summary>
        /// 根据分流管理明细编号获取分流管理明细
        /// </summary>
        /// <param name="costCode"></param>
        public static Model.Person_ShuntDetail GetShuntDetailByShuntDetailId(string ShuntDetailId)
        {
            return Funs.DB.Person_ShuntDetail.FirstOrDefault(e => e.ShuntDetailId == ShuntDetailId);
        }
        /// <summary>
        /// 根据分流管理编号获取分流管理明细集合
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<Model.Person_ShuntDetail> GetLists(string shuntId)
        {
            return (from x in Funs.DB.Person_ShuntDetail where x.ShuntId == shuntId select x).ToList();
        }

        public static List<Model.Person_ShuntDetail> GetListsForApi(string shuntId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Person_ShuntDetail where x.ShuntId == shuntId select x).ToList();

            }
        }
        /// <summary>
        /// 根据分流管理明细主键删除所有分流管理明细信息
        /// </summary>
        /// <param name="pauseNoticeCode">分流管理明细主键</param>
        public static void DeleteShuntDetailByShuntId(string ShuntId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Person_ShuntDetail where x.ShuntId == ShuntId select x).ToList();
            db.Person_ShuntDetail.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
        public static void DeleteShuntDetailById(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = (from x in db.Person_ShuntDetail where x.ShuntDetailId == id select x).ToList();
                db.Person_ShuntDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
