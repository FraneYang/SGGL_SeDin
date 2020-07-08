using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SpecialEquipmentDetailService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取月报特种设备信息模板列表
        /// </summary>
        /// <param name="satartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string CheckMonthId)
        {
            return from x in db.Check_SpecialEquipmentDetail
                   where x.CheckMonthId == CheckMonthId
                   select new
                   {
                       x.SpecialEquipmentDetailId,
                       x.CheckMonthId,
                       x.SpecialEquipmentName,
                       x.TotalNum,
                       x.ThisCompleteNum1,
                       x.TotalCompleteNum1,
                       x.TotalRate1,
                       x.ThisCompleteNum2,
                       x.TotalCompleteNum2,
                       x.TotalRate2,
                   };
        }

        /// <summary>
        /// 增加月报特种设备信息
        /// </summary>
        /// <param name="managerRuleApprove">月报特种设备信息实体</param>
        public static void AddSpecialEquipmentDetail(Model.Check_SpecialEquipmentDetail specialEquipmentDetail)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_SpecialEquipmentDetail newApprove = new Model.Check_SpecialEquipmentDetail();
            newApprove.SpecialEquipmentDetailId = specialEquipmentDetail.SpecialEquipmentDetailId;
            newApprove.CheckMonthId = specialEquipmentDetail.CheckMonthId;
            newApprove.SpecialEquipmentName = specialEquipmentDetail.SpecialEquipmentName;
            newApprove.TotalNum = specialEquipmentDetail.TotalNum;
            newApprove.ThisCompleteNum1 = specialEquipmentDetail.ThisCompleteNum1;
            newApprove.TotalCompleteNum1 = specialEquipmentDetail.TotalCompleteNum1;
            newApprove.TotalRate1 = specialEquipmentDetail.TotalRate1;
            newApprove.ThisCompleteNum2 = specialEquipmentDetail.ThisCompleteNum2;
            newApprove.TotalCompleteNum2 = specialEquipmentDetail.TotalCompleteNum2;
            newApprove.TotalRate2 = specialEquipmentDetail.TotalRate2;

            db.Check_SpecialEquipmentDetail.InsertOnSubmit(newApprove);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报特种设备信息
        /// </summary>
        /// <param name="CheckMonthId">月报特种设备信息编号</param>
        public static void DeleteSpecialEquipmentDetailsByCheckMonthId(string CheckMonthId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var q = (from x in db.Check_SpecialEquipmentDetail where x.CheckMonthId == CheckMonthId select x).ToList();
            if (q.Count() > 0)
            {
                db.Check_SpecialEquipmentDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据月报id删除对应的所有月报特种设备信息
        /// </summary>
        /// <param name="CheckMonthId">月报特种设备信息编号</param>
        public static List<Model.Check_SpecialEquipmentDetail> GetList(string CheckMonthId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return (from x in db.Check_SpecialEquipmentDetail where x.CheckMonthId == CheckMonthId select x).ToList();
        }
    }
}
