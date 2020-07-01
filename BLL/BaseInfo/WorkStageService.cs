using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 工作阶段
    /// </summary>
    public static class WorkStageService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取工作阶段
        /// </summary>
        /// <param name="workStageId"></param>
        /// <returns></returns>
        public static Model.Base_WorkStage GetWorkStageById(string workStageId)
        {
            return Funs.DB.Base_WorkStage.FirstOrDefault(e => e.WorkStageId == workStageId);
        }

        /// <summary>
        ///根据工作阶段名称获取工作阶段
        /// </summary>
        /// <param name="workStageName"></param>
        /// <returns></returns>
        public static Model.Base_WorkStage GetWorkStageByName(string workStageName)
        {
            return Funs.DB.Base_WorkStage.FirstOrDefault(e => e.WorkStageName == workStageName);
        }

        /// <summary>
        /// 获取工作阶段列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_WorkStage> GetWorkStageList()
        {
            return (from x in db.Base_WorkStage orderby x.WorkStageCode select x).ToList();
        }

        /// <summary>
        /// 添加工作阶段
        /// </summary>
        /// <param name="workStage"></param>
        public static void AddWorkStage(Model.Base_WorkStage workStage)
        {
            Model.Base_WorkStage newWorkStage = new Model.Base_WorkStage
            {
                WorkStageId = workStage.WorkStageId,
                WorkStageCode = workStage.WorkStageCode,
                WorkStageName = workStage.WorkStageName,
                Remarks = workStage.Remarks
            };
            db.Base_WorkStage.InsertOnSubmit(newWorkStage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作阶段
        /// </summary>
        /// <param name="workStage"></param>
        public static void UpdateWorkStage(Model.Base_WorkStage workStage)
        {
            Model.Base_WorkStage newWorkStage = db.Base_WorkStage.FirstOrDefault(e => e.WorkStageId == workStage.WorkStageId);
            if (newWorkStage != null)
            {
                newWorkStage.WorkStageCode = workStage.WorkStageCode;
                newWorkStage.WorkStageName = workStage.WorkStageName;
                newWorkStage.Remarks = workStage.Remarks;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除工作阶段
        /// </summary>
        /// <param name="workStageId"></param>
        public static void DeleteWorkStageById(string workStageId)
        {
            Model.Base_WorkStage workStage = db.Base_WorkStage.FirstOrDefault(e => e.WorkStageId == workStageId);
            if (workStage != null)
            {
                db.Base_WorkStage.DeleteOnSubmit(workStage);
                db.SubmitChanges();
            }
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkPostDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "WorkStageId";
            dropName.DataTextField = "WorkStageName";
            dropName.DataSource = GetWorkStageList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        // <summary>
        /// 得到角色名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getWorkStageNamesWorkStageIds(object workStageIds)
        {
            string workStageName = string.Empty;
            if (workStageIds != null)
            {
                string[] workStages = workStageIds.ToString().Split(',');
                foreach (string workStageId in workStages)
                {
                    var q = GetWorkStageById(workStageId);
                    if (q != null)
                    {
                        workStageName += q.WorkStageName + ",";
                    }
                }
                if (workStageName != string.Empty)
                {
                    workStageName = workStageName.Substring(0, workStageName.Length - 1); ;
                }
            }

            return workStageName;
        }
    }
}