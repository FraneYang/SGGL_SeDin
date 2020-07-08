namespace BLL
{
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// 项目单据流程通用类
    /// </summary>
    public static class ProjectDataFlowSetService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string dataId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_ProjectData_FlowOperate> q = from x in db.View_ProjectData_FlowOperate
                                                               where x.DataId == dataId && x.IsClosed == true
                                                               orderby x.SortIndex descending
                                                               select x;

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }

            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.FlowOperateId,
                       x.MenuId,
                       x.DataId,
                       x.SortIndex,
                       x.OperaterId,
                       x.OperaterTime,
                       x.State,
                       x.Opinion,
                       x.IsClosed,
                       x.MenuName,
                       x.OperaterName,
                       x.UnitId,
                       x.UnitName,
                       x.StateName,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string dataId)
        {
            return count;
        }

        /// <summary>
        /// 根据流程id得到流程信息
        /// </summary>
        /// <param name="flowOperateId">流程id</param>
        /// <returns></returns>
        public static Model.ProjectData_FlowOperate getProjectDataFlowOperateByFlowOperateId(string flowOperateId)
        {
            return new Model.SGGLDB(Funs.ConnString).ProjectData_FlowOperate.FirstOrDefault(x => x.FlowOperateId == flowOperateId);
        }

        #region 项目单据流程 增删改
        /// <summary>
        /// 增加项目单据流程
        /// </summary>
        /// <param name="FlowSetName"></param>
        /// <param name="def"></param>
        public static void AddProjectData_FlowOperate(Model.ProjectData_FlowOperate flowSet)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.ProjectData_FlowOperate newFlowOperate =new Model.ProjectData_FlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(typeof(Model.ProjectData_FlowOperate)),
                MenuId = flowSet.MenuId,
                DataId = flowSet.DataId
            };
            int newSortIndex = getFlowSetMaxSortIndexByMenuId(flowSet.MenuId, flowSet.DataId);
            newFlowOperate.SortIndex = newSortIndex;
            newFlowOperate.OperaterId = flowSet.OperaterId;
            newFlowOperate.OperaterTime = flowSet.OperaterTime;
            newFlowOperate.State = flowSet.State;
            newFlowOperate.Opinion = flowSet.Opinion;
            newFlowOperate.IsClosed = flowSet.IsClosed;
            db.ProjectData_FlowOperate.InsertOnSubmit(newFlowOperate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除项目单据流程
        /// </summary>
        /// <param name="flowSetId"></param>
        public static void DeleteFlowSetByFlowSetId(string flowSetId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.ProjectData_FlowOperate flowSet = db.ProjectData_FlowOperate.FirstOrDefault(e => e.FlowOperateId == flowSetId);
            if (flowSet != null)
            {
                db.ProjectData_FlowOperate.DeleteOnSubmit(flowSet);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除流程根据单据主键ID
        /// </summary>
        /// <param name="flowSetId"></param>
        public static void DeleteFlowSetByDataId(string dataId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var flowSets = from x in db.ProjectData_FlowOperate where x.DataId == dataId select x;
            if (flowSets.Count() > 0)
            {
                db.ProjectData_FlowOperate.DeleteAllOnSubmit(flowSets);
                db.SubmitChanges();
            }
        }
        #endregion

        /// <summary>
        /// 根据ID获取未处理流程
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">数据主键id</param>
        /// <returns></returns>
        public static Model.ProjectData_FlowOperate getUnFlowOperate(string dataId)
        {
            var unFlowOperate = (from x in new Model.SGGLDB(Funs.ConnString).ProjectData_FlowOperate
                                 where x.DataId == dataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                 orderby x.SortIndex descending
                                 select x).FirstOrDefault();
            return unFlowOperate;
        }

        /// <summary>
        /// 根据ID获取编制人的信息
        /// </summary>
        /// <param name="dataId">主表id</param>
        /// <returns></returns>
        public static Model.ProjectData_FlowOperate getCompileFlowOperate(string dataId)
        {
            var unFlowOperate = (from x in new Model.SGGLDB(Funs.ConnString).ProjectData_FlowOperate
                                 where x.DataId == dataId && x.SortIndex == 1
                                 select x).FirstOrDefault();
            return unFlowOperate;
        }

        /// <summary>
        /// 更新处理意见
        /// </summary>
        /// <param name="upFlowOperate"></param>
        public static void UpdateFlowOperateOpinion(Model.ProjectData_FlowOperate flowOperate)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            var upFlowOperate = from x in db.ProjectData_FlowOperate
                                where x.MenuId == flowOperate.MenuId && x.DataId == flowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                select x;
            if (upFlowOperate.Count() > 0)
            {
                foreach (var item in upFlowOperate)
                {
                    item.OperaterId = flowOperate.OperaterId;
                    item.OperaterTime = flowOperate.OperaterTime;
                    item.State = flowOperate.State;
                    item.Opinion = flowOperate.Opinion;
                    item.IsClosed = flowOperate.IsClosed;
                    new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                }
            }
            else
            {
                Model.ProjectData_FlowOperate newFlowOperate = new Model.ProjectData_FlowOperate
                {
                    MenuId = flowOperate.MenuId,
                    DataId = flowOperate.DataId,
                    OperaterId = flowOperate.OperaterId,
                    OperaterTime = flowOperate.OperaterTime,
                    State = flowOperate.State,
                    Opinion = flowOperate.Opinion,
                    IsClosed = flowOperate.IsClosed
                };
                AddProjectData_FlowOperate(newFlowOperate);
            }
        }

        /// <summary>
        /// 获取流程最大排序号
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">数据主键id</param>
        /// <returns></returns>
        public static int getFlowSetMaxSortIndexByMenuId(string menuId, string dataId)
        {
            int maxSortIndex = 1;
            var flowSet = new Model.SGGLDB(Funs.ConnString).ProjectData_FlowOperate.Where(x => x.MenuId == menuId && x.DataId == dataId);
            var sortIndex = flowSet.Select(x => x.SortIndex).Max();
            if (sortIndex.HasValue)
            {
                maxSortIndex = sortIndex.Value + 1;
            }
            return maxSortIndex;
        }

        /// <summary>
        /// 根据单据id得到下步办理步骤和办理人
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public static string GetNextProjectDataFlowOperaterName(string dataId, string state)
        {
            string operaterName = string.Empty;

            if (state == Const.State_2)
            {
                operaterName = "已完成";
            }
            else if (state == Const.State_R)
            {
                operaterName = "重申请";
            }
            else if (state == Const.State_1)
            {
                var flowSet = getUnFlowOperate(dataId);
                if (flowSet != null)
                {
                    var user = BLL.UserService.GetUserByUserId(flowSet.OperaterId);
                    if (user != null)
                    {
                        operaterName = "下步处理人：" + user.UserName;
                    }
                    else
                    {
                        operaterName = "待处理：管理员";
                    }
                }
                else
                {
                    operaterName = "待处理：管理员";
                }
            }
            else
            {
                operaterName = "待提交：单据编制人";
            }
            return operaterName;
        }

        /// <summary>
        /// 根据菜单id和步骤序号得到下一个流程
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="sortIndex"></param>
        /// <returns></returns>
        public static Model.ProjectData_FlowOperate GetNextProjectData_FlowOperate(string dataId)
        {
            Model.ProjectData_FlowOperate flowSet = new Model.ProjectData_FlowOperate();
            var flowSets = from x in new Model.SGGLDB(Funs.ConnString).ProjectData_FlowOperate
                           where x.DataId == dataId && (!x.IsClosed.HasValue || x.IsClosed == false)
                           orderby x.SortIndex
                           select x;
            if (flowSets.Count() > 0)
            {
                int? index = flowSets.Select(x => x.SortIndex).Min();
                if (index.HasValue)
                {
                    flowSet = flowSets.FirstOrDefault(x => x.SortIndex == index);
                }
            }

            return flowSet;
        }

        /// <summary>
        /// 判断主表是否存在审批记录
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="dataId">主表主键Id</param>
        public static bool IsExitOperate(string menuId, string dataId)
        {
            return (from x in db.ProjectData_FlowOperate
                    where x.MenuId == menuId && x.DataId == dataId
                    select x).Count() > 0;
        }

        /// <summary>
        /// 获取最后一条未办理的意见
        /// </summary>
        /// <param name="upFlowOperate"></param>
        public static Model.ProjectData_FlowOperate GetFlowOperateOpinion(string menuId, string dataId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            return (from x in db.ProjectData_FlowOperate
                    where x.MenuId == menuId && x.DataId == dataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                    select x).FirstOrDefault();
        }
    }
}
