﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Person_QuarterCheckItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="QuarterCheckId">人员Id</param>
        /// <returns>人员信息</returns>
        public static Model.Person_QuarterCheckItem GetCheckItemById(string QuarterCheckItemId)
        {
            return Funs.DB.Person_QuarterCheckItem.FirstOrDefault(e => e.QuarterCheckItemId == QuarterCheckItemId);
        }
        public static List<Model.Person_QuarterCheckItem> GetCheckItemListById(string QuarterCheckId)
        {
            return (from x in Funs.DB.Person_QuarterCheckItem where x.QuarterCheckId == QuarterCheckId orderby x.SortId select x).ToList();

        }
        public static Decimal GetCheckItemSumById(string QuarterCheckId)
        {
            return decimal.Parse((from x in Funs.DB.Person_QuarterCheckItem where x.QuarterCheckId == QuarterCheckId select x.Grade).Sum().ToString());

        }
        /// <summary>
        /// 增加人员总结信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddCheckItem(Model.Person_QuarterCheckItem contruct)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheckItem newcontruct = new Model.Person_QuarterCheckItem
            {
                QuarterCheckItemId = contruct.QuarterCheckItemId,
                QuarterCheckId = contruct.QuarterCheckId,
                UserId = contruct.UserId,
                TargetClass1 = contruct.TargetClass1,
                TargetClass2 = contruct.TargetClass2,
                CheckContent = contruct.CheckContent,
                SortId = contruct.SortId,
                StandardGrade = contruct.StandardGrade,
            };
            db.Person_QuarterCheckItem.InsertOnSubmit(newcontruct);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdateCheckItem(Model.Person_QuarterCheckItem item)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheckItem newItem = db.Person_QuarterCheckItem.FirstOrDefault(e => e.QuarterCheckItemId == item.QuarterCheckItemId);
            if (newItem != null)
            {
                newItem.Grade = item.Grade;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员Id删除
        /// </summary>
        /// <param name="PersonTotalId"></param>
        public static void DeleteCheckItem(string QuarterCheckItemId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_QuarterCheckItem check = db.Person_QuarterCheckItem.FirstOrDefault(e => e.QuarterCheckItemId == QuarterCheckItemId);
            if (check != null)
            {
                db.Person_QuarterCheckItem.DeleteOnSubmit(check);
                db.SubmitChanges();
            }
        }

        public static List<Model.Person_QuarterCheckItem> GetListDataForApi(string id, string userId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Person_QuarterCheckItem> q = db.Person_QuarterCheckItem;
                List<string> ids = new List<string>();
                if (!string.IsNullOrEmpty(userId))
                {
                    q = q.Where(e => e.QuarterCheckId == id && e.UserId == userId);
                }

                var qq1 = from x in q
                          orderby x.SortId
                          select new
                          {
                              x.QuarterCheckItemId,
                              x.QuarterCheckId,
                              x.UserId,
                              x.TargetClass1,
                              x.TargetClass2,
                              x.CheckContent,
                              x.Grade,
                              x.SortId,
                              x.StandardGrade,

                          };
                var list = qq1.ToList();

                List<Model.Person_QuarterCheckItem> listRes = new List<Model.Person_QuarterCheckItem>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.Person_QuarterCheckItem x = new Model.Person_QuarterCheckItem();
                    x.QuarterCheckItemId = list[i].QuarterCheckItemId;
                    x.QuarterCheckId = list[i].QuarterCheckId;
                    x.UserId = list[i].UserId;
                    x.TargetClass1 = list[i].TargetClass1;
                    x.TargetClass2 = list[i].TargetClass2;
                    x.CheckContent = list[i].CheckContent;
                    x.Grade = list[i].Grade;
                    x.SortId = list[i].SortId;
                    x.StandardGrade = list[i].StandardGrade;
                    listRes.Add(x);
                }
                return listRes;
            }
        }
    }
}
