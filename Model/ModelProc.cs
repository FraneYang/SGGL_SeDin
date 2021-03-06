namespace Model
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Reflection;

    public partial class SGGLDB : DataContext
    {

        /// <summary>
        /// 获取当前用户在移动端待办事项
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[Sp_APP_GetToDoItems]")]
        public IEnumerable<ToDoItem> Sp_APP_GetToDoItems([Parameter(DbType = "nvarchar(50)")] string projectId, [Parameter(DbType = "nvarchar(50)")] string userId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), projectId, userId);
            return (ISingleResult<ToDoItem>)result.ReturnValue;
        }

        /// <summary>
        /// 获取当前用户在本部待办事项
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[Sp_Main_GetToDoItems]")]
        public IEnumerable<ToDoItem> Sp_Main_GetToDoItems([Parameter(DbType = "nvarchar(50)")] string userId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), userId);
            return (ISingleResult<ToDoItem>)result.ReturnValue;
        }

        /// <summary>
        /// 获取人员培训教材
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[Sp_GetTraining_TaskItemTraining]")]
        public IEnumerable<TrainingTaskItemItem> Sp_GetTraining_TaskItemTraining([Parameter(DbType = "nvarchar(50)")] string planId, [Parameter(DbType = "nvarchar(200)")] string workPostId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), planId, workPostId);
            return (ISingleResult<TrainingTaskItemItem>)result.ReturnValue;
        }

        /// <summary>
        /// 获取隐患整改单
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[SP_RectifyNoticesListByProjectStates]")]
        public IEnumerable<RectifyNoticesItem> SP_RectifyNoticesListByProjectStates([Parameter(DbType = "nvarchar(50)")] string projectId, [Parameter(DbType = "nvarchar(50)")] string states, [Parameter(DbType = "INT")] int pageIndex, [Parameter(DbType = "INT")] int pageSize)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), projectId, states, pageIndex, pageSize);
            return (ISingleResult<RectifyNoticesItem>)result.ReturnValue;
        }

        /// <summary>
        /// 获取安全数据汇总
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[Sp_DigData_HSEDataCollect]")]
        public IEnumerable<DigDataHSEDataCollectItem> Sp_DigData_HSEDataCollect( [Parameter(DbType = "INT")] int year)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), year);
            return (ISingleResult<DigDataHSEDataCollectItem>)result.ReturnValue;
        }
    }
}
