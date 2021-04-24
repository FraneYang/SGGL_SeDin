using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 10-1:施工人力投入计划表
    /// </summary>
    public class AttachUrl10_HumanInputService
    {
        /// <summary>
        /// 根据附件Id获取施工人力投入计划表列表
        /// </summary>
        /// <param name="attachUrlId"></param>
        /// <returns></returns>
        public static List<Model.PHTGL_AttachUrl10_HumanInput> GetHumanInputByAttachUrlId(string attachUrlId)
        {
            return (from x in Funs.DB.PHTGL_AttachUrl10_HumanInput where x.AttachUrlId == attachUrlId select x).ToList();
        }

        /// <summary>
        /// 添加施工人力投入计划表
        /// </summary>
        /// <param name="humanInput"></param>
        public static void AddHumanInput(Model.PHTGL_AttachUrl10_HumanInput humanInput)
        {
            Model.PHTGL_AttachUrl10_HumanInput newHumanInput = new Model.PHTGL_AttachUrl10_HumanInput();
            newHumanInput.AttachUrlItemId = humanInput.AttachUrlItemId;
            newHumanInput.AttachUrlId = humanInput.AttachUrlId;
            newHumanInput.Subject = humanInput.Subject;
            newHumanInput.WorkType = humanInput.WorkType;
            newHumanInput.PersonNumber = humanInput.PersonNumber;
            newHumanInput.LifeTime = humanInput.LifeTime;
            newHumanInput.Remarks = humanInput.Remarks;
            Funs.DB.PHTGL_AttachUrl10_HumanInput.InsertOnSubmit(newHumanInput);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 根据附件Id删除相关施工人力投入计划表
        /// </summary>
        /// <param name="attachUrlId"></param>
        public static void DeleteHumanInputByAttachUrlId(string attachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl10_HumanInput where x.AttachUrlId == attachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl10_HumanInput.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}