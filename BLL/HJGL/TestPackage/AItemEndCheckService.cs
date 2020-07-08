using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class AItemEndCheckService
    {
        /// <summary>
        /// 根据主键Id获取用于A项信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.PTP_AItemEndCheck GetAItemEndCheckByID(string id)
        {
            return new Model.SGGLDB(Funs.ConnString).PTP_AItemEndCheck.FirstOrDefault(x=>x.AItemCheckId == id);
        }

        /// <summary>
        /// 根据管线Id获取用于A项信息
        /// </summary>
        /// <param name="pipelineId"></param>
        /// <returns></returns>
        public static List<Model.PTP_AItemEndCheck> GetAItemEndCheckBypipelineId(string pipelineId)
        {
            var view = from x in new Model.SGGLDB(Funs.ConnString).PTP_AItemEndCheck
                       where x.PipelineId == pipelineId
                       orderby x.CheckDate
                       select x;
            return view.ToList();
        } 
        
          /// <summary>
        /// 增加业务_A项尾工检查表
        /// </summary>
        /// <param name="aItemEndCheck">试压实体</param>
        public static void AddAItemEndCheck(Model.PTP_AItemEndCheck aItemEndCheck)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_AItemEndCheck newAItemEndCheck = new Model.PTP_AItemEndCheck();
            newAItemEndCheck.AItemCheckId = SQLHelper.GetNewID(typeof(Model.PTP_AItemEndCheck));
            newAItemEndCheck.PipelineId = aItemEndCheck.PipelineId;
            newAItemEndCheck.CheckMan = aItemEndCheck.CheckMan;
            newAItemEndCheck.CheckDate = aItemEndCheck.CheckDate;
            newAItemEndCheck.DealMan = aItemEndCheck.DealMan;
            newAItemEndCheck.DealDate = aItemEndCheck.DealDate;
            newAItemEndCheck.Remark = aItemEndCheck.Remark;
            db.PTP_AItemEndCheck.InsertOnSubmit(newAItemEndCheck);
            db.SubmitChanges();
        }

          /// <summary>
        /// 修改业务_A项尾工检查表
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateAItemEndCheck(Model.PTP_AItemEndCheck aItemEndCheck)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_AItemEndCheck newAItemEndCheck = db.PTP_AItemEndCheck.First(e => e.AItemCheckId == aItemEndCheck.AItemCheckId);
            newAItemEndCheck.PipelineId = aItemEndCheck.PipelineId;
            newAItemEndCheck.CheckMan = aItemEndCheck.CheckMan;
            newAItemEndCheck.CheckDate = aItemEndCheck.CheckDate;
            newAItemEndCheck.DealMan = aItemEndCheck.DealMan;
            newAItemEndCheck.DealDate = aItemEndCheck.DealDate;
            newAItemEndCheck.Remark = aItemEndCheck.Remark;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除业务_A项尾工检查表
        /// </summary>
        /// <param name="id">业务_A项尾工检查表主键</param>
        public static void DeleteAItemEndCheckByID(string aItemCheckId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.PTP_AItemEndCheck newAItemEndCheck = db.PTP_AItemEndCheck.First(e => e.AItemCheckId == aItemCheckId);
            db.PTP_AItemEndCheck.DeleteOnSubmit(newAItemEndCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据管线Id判断是否存在A项尾工
        /// </summary>
        /// <param name="isono"></param>
        /// <returns></returns>
        public static bool IsExistAItemEndCheck(string pipelineId)
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).PTP_AItemEndCheck where x.PipelineId == pipelineId select x;
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
