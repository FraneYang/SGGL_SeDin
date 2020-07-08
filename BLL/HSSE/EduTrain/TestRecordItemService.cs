using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 考试记录明细
    /// </summary>
    public static class TestRecordItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据考试记录Id获取明细信息
        /// </summary>
        /// <param name="testRecordItemId"></param>
        /// <returns></returns>
        public static Model.Training_TestRecordItem GetTestRecordItemTestRecordItemId(string testRecordItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).Training_TestRecordItem.FirstOrDefault(e => e.TestRecordItemId == testRecordItemId);
        }

        /// <summary>
        /// 根据培训ID删除所有相关明细信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteTestRecordItemmByTestRecordItemId(string testRecordItemId)
        {
            var testRecordItem = db.Training_TestRecordItem.FirstOrDefault(x => x.TestRecordItemId == testRecordItemId);
            if (testRecordItem != null)
            {
                db.Training_TestRecordItem.DeleteOnSubmit(testRecordItem);
                db.SubmitChanges();
            }
        }
    }
}
