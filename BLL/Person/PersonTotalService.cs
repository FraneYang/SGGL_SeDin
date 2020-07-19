using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class PersonTotalService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="PersonTotalId">人员Id</param>
        /// <returns>人员信息</returns>
        public static Model.PersonTotal GetPersonByPersonTotalId(string PersonTotalId)
        {
            return Funs.DB.PersonTotal.FirstOrDefault(e => e.PersonTotalId == PersonTotalId);
        }
        
        /// <summary>
        /// 增加人员总结信息
        /// </summary>
        /// <param name="user">人员实体</param>
        public static void AddPersonTotal(Model.PersonTotal total)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PersonTotal newTotla = new Model.PersonTotal
            {
                PersonTotalId = total.PersonTotalId,
                UserId = total.UserId,
                Content = total.Content,
                StartTime = total.StartTime,
                EndTime = total.EndTime,
                CompiledManId = total.CompiledManId,
                CompiledDate = total.CompiledDate,
            };
            db.PersonTotal.InsertOnSubmit(newTotla);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改人员总结信息
        /// </summary>
        /// <param name="user">实体</param>
        public static void UpdatePersonTotal(Model.PersonTotal total)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PersonTotal newTotal = db.PersonTotal.FirstOrDefault(e => e.PersonTotalId == total.PersonTotalId);
            if (newTotal != null)
            {
                newTotal.UserId = total.UserId;
                newTotal.Content = total.Content;
                newTotal.StartTime = total.StartTime;
                newTotal.EndTime = total.EndTime;
                newTotal.CompiledManId = total.CompiledManId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据人员Id删除一个人员信息
        /// </summary>
        /// <param name="PersonTotalId"></param>
        public static void DeletePersonTotal(string PersonTotalId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.PersonTotal user = db.PersonTotal.FirstOrDefault(e => e.PersonTotalId == PersonTotalId);
            if (user != null)
            {
                db.PersonTotal.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
        }
        
    }
}
