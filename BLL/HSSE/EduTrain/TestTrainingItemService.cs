using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class TestTrainingItemService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Training_TestTrainingItem GetTestTrainingItemById(string TrainingItemId)
        {
            return Funs.DB.Training_TestTrainingItem.FirstOrDefault(e => e.TrainingItemId == TrainingItemId);
        }

        /// <summary>
        /// 添加试题信息
        /// </summary>
        /// <param name="TrainingItem"></param>
        public static void AddTestTrainingItem(Model.Training_TestTrainingItem TestTrainingItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_TestTrainingItem newTestTrainingItem = new Model.Training_TestTrainingItem
            {
                TrainingItemId = TestTrainingItem.TrainingItemId,
                TrainingId = TestTrainingItem.TrainingId,
                TrainingItemCode = TestTrainingItem.TrainingItemCode,
                TrainingItemName = TestTrainingItem.TrainingItemName,
                Abstracts = TestTrainingItem.Abstracts,
                AttachUrl = TestTrainingItem.AttachUrl,
                VersionNum = TestTrainingItem.VersionNum,
                TestType = TestTrainingItem.TestType,
                WorkPostIds = TestTrainingItem.WorkPostIds,
                WorkPostNames = TestTrainingItem.WorkPostNames,
                AItem = TestTrainingItem.AItem,
                BItem = TestTrainingItem.BItem,
                CItem = TestTrainingItem.CItem,
                DItem = TestTrainingItem.DItem,
                EItem = TestTrainingItem.EItem,
                AnswerItems = TestTrainingItem.AnswerItems,
            };
            db.Training_TestTrainingItem.InsertOnSubmit(newTestTrainingItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试题信息
        /// </summary>
        /// <param name="TrainingItem"></param>
        public static void UpdateTestTrainingItem(Model.Training_TestTrainingItem TestTrainingItem)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_TestTrainingItem newTestTrainingItem = db.Training_TestTrainingItem.FirstOrDefault(e => e.TrainingItemId == TestTrainingItem.TrainingItemId);
            if (newTestTrainingItem != null)
            {
                newTestTrainingItem.TrainingItemCode = TestTrainingItem.TrainingItemCode;
                newTestTrainingItem.TrainingItemName = TestTrainingItem.TrainingItemName;
                newTestTrainingItem.Abstracts = TestTrainingItem.Abstracts;
                newTestTrainingItem.AttachUrl = TestTrainingItem.AttachUrl;
                newTestTrainingItem.VersionNum = TestTrainingItem.VersionNum;
                newTestTrainingItem.TestType = TestTrainingItem.TestType;
                newTestTrainingItem.WorkPostIds = TestTrainingItem.WorkPostIds;
                newTestTrainingItem.WorkPostNames = TestTrainingItem.WorkPostNames;
                newTestTrainingItem.AItem = TestTrainingItem.AItem;
                newTestTrainingItem.BItem = TestTrainingItem.BItem;
                newTestTrainingItem.CItem = TestTrainingItem.CItem;
                newTestTrainingItem.DItem = TestTrainingItem.DItem;
                newTestTrainingItem.EItem = TestTrainingItem.EItem;
                newTestTrainingItem.AnswerItems = TestTrainingItem.AnswerItems;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="TrainingItemId"></param>
        public static void DeleteTestTrainingItemById(string TrainingItemId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Training_TestTrainingItem TestTrainingItem = db.Training_TestTrainingItem.FirstOrDefault(e => e.TrainingItemId == TrainingItemId);
            if (TestTrainingItem != null)
            {
                db.Training_TestTrainingItem.DeleteOnSubmit(TestTrainingItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据教材类型获取正确答案项
        /// </summary>
        /// <param name="testType"></param>
        /// <returns></returns>
        public static ListItem[] GetAnswerItemsList(string testType)
        {
            if (testType == "1")   //单选题
            {
                ListItem[] item = new ListItem[4];
                item[0] = new ListItem("A", "A");
                item[1] = new ListItem("B", "B");
                item[2] = new ListItem("C", "C");
                item[3] = new ListItem("D", "D");
                return item;
            }
            else if (testType == "2")   //多选题
            {
                ListItem[] item = new ListItem[5];
                item[0] = new ListItem("A", "A");
                item[1] = new ListItem("B", "B");
                item[2] = new ListItem("C", "C");
                item[3] = new ListItem("D", "D");
                item[4] = new ListItem("E", "E");
                return item;
            }
            else    //判断题
            {
                ListItem[] item = new ListItem[2];
                item[0] = new ListItem("A", "A");
                item[1] = new ListItem("B", "B");
                return item;
            }
        }
    }
}
