using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class BItemEndCheckService
    {
        /// <summary>
        /// 根据主键Id获取用于A项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.PTP_BItemEndCheck GetAItemEndCheckByID(string id)
        {
            return new Model.SGGLDB(Funs.ConnString).PTP_BItemEndCheck.FirstOrDefault(x => x.BItemCheckId == id);
        }

        /// <summary>
        /// 根据管线Id获取用于A项信息
        /// </summary>
        /// <param name="pipelineId"></param>
        /// <returns></returns>
        public static List<Model.PTP_BItemEndCheck> GetBItemEndCheckBypipelineId(string pipelineId)
        {
            var view = from x in new Model.SGGLDB(Funs.ConnString).PTP_BItemEndCheck
                       where x.PipelineId == pipelineId
                       orderby x.CheckDate
                       select x;
            return view.ToList();
        }

        /// <summary>
        /// 增加业务_A项尾工检查表
        /// </summary>
        /// <param name="bItemEndCheck">试压实体</param>
        public static void AddBItemEndCheck(Model.PTP_BItemEndCheck bItemEndCheck)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_BItemEndCheck newBItemEndCheck = new Model.PTP_BItemEndCheck();
            newBItemEndCheck.BItemCheckId = SQLHelper.GetNewID(typeof(Model.PTP_BItemEndCheck));
            newBItemEndCheck.PipelineId = bItemEndCheck.PipelineId;
            newBItemEndCheck.CheckMan = bItemEndCheck.CheckMan;
            newBItemEndCheck.CheckDate = bItemEndCheck.CheckDate;
            newBItemEndCheck.DealMan = bItemEndCheck.DealMan;
            newBItemEndCheck.DealDate = bItemEndCheck.DealDate;
            newBItemEndCheck.Remark = bItemEndCheck.Remark;
            db.PTP_BItemEndCheck.InsertOnSubmit(newBItemEndCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改业务_A项尾工检查表
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateBItemEndCheck(Model.PTP_BItemEndCheck bItemEndCheck)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_BItemEndCheck newBItemEndCheck = db.PTP_BItemEndCheck.First(e => e.BItemCheckId == bItemEndCheck.BItemCheckId);
            newBItemEndCheck.PipelineId = bItemEndCheck.PipelineId;
            newBItemEndCheck.CheckMan = bItemEndCheck.CheckMan;
            newBItemEndCheck.CheckDate = bItemEndCheck.CheckDate;
            newBItemEndCheck.DealMan = bItemEndCheck.DealMan;
            newBItemEndCheck.DealDate = bItemEndCheck.DealDate;
            newBItemEndCheck.Remark = bItemEndCheck.Remark;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除业务_A项尾工检查表
        /// </summary>
        /// <param name="id">业务_A项尾工检查表主键</param>
        public static void DeleteBItemEndCheckByID(string bItemCheckId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_BItemEndCheck newBItemEndCheck = db.PTP_BItemEndCheck.First(e => e.BItemCheckId == bItemCheckId);
            db.PTP_BItemEndCheck.DeleteOnSubmit(newBItemEndCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据管线Id判断是否存在A项尾工
        /// </summary>
        /// <param name="isono"></param>
        /// <returns></returns>
        public static bool IsExistBItemEndCheck(string pipelineId)
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).PTP_BItemEndCheck where x.PipelineId == pipelineId select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}