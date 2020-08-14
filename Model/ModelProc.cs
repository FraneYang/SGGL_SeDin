namespace Model
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Reflection;

    public partial class SGGLDB : DataContext
    {

        /// <summary>
        /// ��ȡ��ǰ�û����ƶ��˴�������
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
        /// ��ȡ��Ա��ѵ�̲�
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
        /// ��ȡ�������ĵ�
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[SP_RectifyNoticesListByProjectStates]")]
        public IEnumerable<RectifyNoticesItem> SP_RectifyNoticesListByProjectStates([Parameter(DbType = "nvarchar(50)")] string projectId, [Parameter(DbType = "nvarchar(50)")] string states)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), projectId, states);
            return (ISingleResult<RectifyNoticesItem>)result.ReturnValue;
        }
    }
}
