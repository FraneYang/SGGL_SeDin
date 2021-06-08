using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class ItemEndCheckListService
    {
        /// <summary>
        /// 根据Id获取尾项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.PTP_ItemEndCheckList GetItemEndCheckListByID(string ItemEndCheckListId)
        {
            var view = Funs.DB.PTP_ItemEndCheckList.FirstOrDefault(e => e.ItemEndCheckListId == ItemEndCheckListId);
            return view;
        }

        /// <summary>
        /// 根据试压Id获取尾项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.PTP_ItemEndCheckList> GetItemEndCheckListsByPTPID(string PTP_ID)
        {
            var view = from x in Funs.DB.PTP_ItemEndCheckList where x.PTP_ID == PTP_ID select x;
            return view.ToList();
        }

        /// <summary>
        /// 增加尾项信息
        /// </summary>
        /// <param name="ItemEndCheckList">试压实体</param>
        public static void AddItemEndCheckList(Model.PTP_ItemEndCheckList ItemEndCheckList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_ItemEndCheckList newItemEndCheckList = new Model.PTP_ItemEndCheckList();
            newItemEndCheckList.ItemEndCheckListId = ItemEndCheckList.ItemEndCheckListId;
            newItemEndCheckList.PTP_ID = ItemEndCheckList.PTP_ID;
            newItemEndCheckList.CompileMan = ItemEndCheckList.CompileMan;
            newItemEndCheckList.CompileDate = ItemEndCheckList.CompileDate;
            newItemEndCheckList.State = ItemEndCheckList.State;
            newItemEndCheckList.AIsOK = ItemEndCheckList.AIsOK;
            newItemEndCheckList.BIsOK = ItemEndCheckList.BIsOK;
            db.PTP_ItemEndCheckList.InsertOnSubmit(newItemEndCheckList);
            db.SubmitChanges();
        }

        public static void AddItemEndCheckListForApi(Model.PTP_ItemEndCheckList ItemEndCheckList)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.PTP_ItemEndCheckList newItemEndCheckList = new Model.PTP_ItemEndCheckList();
                newItemEndCheckList.ItemEndCheckListId = ItemEndCheckList.ItemEndCheckListId;
                newItemEndCheckList.PTP_ID = ItemEndCheckList.PTP_ID;
                newItemEndCheckList.CompileMan = ItemEndCheckList.CompileMan;
                newItemEndCheckList.CompileDate = ItemEndCheckList.CompileDate;
                newItemEndCheckList.State = ItemEndCheckList.State;
                newItemEndCheckList.AIsOK = ItemEndCheckList.AIsOK;
                newItemEndCheckList.BIsOK = ItemEndCheckList.BIsOK;

                db.PTP_ItemEndCheckList.InsertOnSubmit(newItemEndCheckList);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改尾项信息
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateItemEndCheckList(Model.PTP_ItemEndCheckList ItemEndCheckList)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_ItemEndCheckList newItemEndCheckList = db.PTP_ItemEndCheckList.First(e => e.ItemEndCheckListId == ItemEndCheckList.ItemEndCheckListId);
            newItemEndCheckList.State = ItemEndCheckList.State;
            newItemEndCheckList.AIsOK = ItemEndCheckList.AIsOK;
            newItemEndCheckList.BIsOK = ItemEndCheckList.BIsOK;
            newItemEndCheckList.AOKState = ItemEndCheckList.AOKState;
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改尾项信息
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateItemEndCheckListForApi(Model.PTP_ItemEndCheckList ItemEndCheckList)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.PTP_ItemEndCheckList newItemEndCheckList = db.PTP_ItemEndCheckList.First(e => e.ItemEndCheckListId == ItemEndCheckList.ItemEndCheckListId);
                if (!string.IsNullOrEmpty(ItemEndCheckList.State))
                    newItemEndCheckList.State = ItemEndCheckList.State;
                if (ItemEndCheckList.AIsOK != null)
                    newItemEndCheckList.AIsOK = ItemEndCheckList.AIsOK;
                if (ItemEndCheckList.BIsOK != null)
                    newItemEndCheckList.BIsOK = ItemEndCheckList.BIsOK;
                if (ItemEndCheckList.AOKState != null)
                    newItemEndCheckList.AOKState = ItemEndCheckList.AOKState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除尾项信息
        /// </summary>
        /// <param name="testPackageID">试压主键</param>
        public static void DeleteItemEndCheckList(string ItemEndCheckListId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PTP_ItemEndCheckList testPackage = db.PTP_ItemEndCheckList.First(e => e.ItemEndCheckListId == ItemEndCheckListId);
            if (testPackage != null)
            {
                db.PTP_ItemEndCheckList.DeleteOnSubmit(testPackage);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取A项整改状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertAState(string ItemEndCheckListId)
        {
            string rowID = ItemEndCheckListId.ToString();
            var items = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(rowID);
            var aItems = items.Where(x => x.ItemType == "A");
            var aOKItems = aItems.Where(x => x.Result == "合格");
            if (aItems.Count() > 0)   //存在A项
            {
                if (aItems.Count() == aOKItems.Count())   //A项完成
                {
                    return "已完成";
                }
                else
                {
                    return "未完成";
                }
            }
            else
            {
                return "无";
            }
        }

        /// <summary>
        /// 获取B项整改状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertBState(string ItemEndCheckListId)
        {
            string rowID = ItemEndCheckListId.ToString();
            var items = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(rowID);
            var bItems = items.Where(x => x.ItemType == "B");
            var bOKItems = bItems.Where(x => x.Result == "合格");
            if (bItems.Count() > 0)   //存在B项
            {
                if (bItems.Count() == bOKItems.Count())   //B项完成
                {
                    return "已完成";
                }
                else
                {
                    return "未完成";
                }
            }
            else
            {
                return "无";
            }
        }

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertMan(string ItemEndCheckListId)
        {
            if (ItemEndCheckListId != null)
            {
                var approve = BLL.TestPackageApproveService.GetTestPackageApproveById(ItemEndCheckListId.ToString());
                if (approve != null)
                {
                    if (approve.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(approve.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
    }
}
