using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class MenuFlowOperateService
   {        
       /// <summary>
       ///  根据工作流主键获取工作流信息
       /// </summary>
       /// <param name="flowOperateId"></param>
       /// <returns></returns>
       public static Model.Sys_MenuFlowOperate GetMenuFlowOperateByFlowOperateId(string flowOperateId)
       {
           return  new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate.FirstOrDefault(x=> x.FlowOperateId == flowOperateId);
       }

        /// <summary>
        ///  根据工作流主键获取工作流信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static List<Model.Sys_MenuFlowOperate> GetMenuFlowOperateListByMenuId(string menuId)
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate where x.MenuId == menuId
                    orderby x.FlowStep select x).ToList();
        }

        /// <summary>
        /// 添加工作流信息
        /// </summary>
        /// <param name="flow"></param>
        public static void AddAuditFlow(Model.Sys_MenuFlowOperate flow)
        {
            Model.Sys_MenuFlowOperate newMenuFlowOperate = new Model.Sys_MenuFlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_MenuFlowOperate)),
                MenuId = flow.MenuId,
                FlowStep = flow.FlowStep,
                GroupNum = flow.GroupNum,
                OrderNum = flow.OrderNum,
                AuditFlowName = flow.AuditFlowName,
                RoleId = flow.RoleId,
                IsFlowEnd = flow.IsFlowEnd,                
            };
            new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate.InsertOnSubmit(newMenuFlowOperate);
            new Model.SGGLDB(Funs.ConnString).SubmitChanges();
        }

        /// <summary>
        /// 修改工作流信息
        /// </summary>
        /// <param name="flow"></param>
        public static void UpdateAuditFlow(Model.Sys_MenuFlowOperate flow)
        {
            Model.Sys_MenuFlowOperate newMenuFlow = new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate.FirstOrDefault(e => e.FlowOperateId == flow.FlowOperateId);
            if (newMenuFlow != null)
            {
                newMenuFlow.MenuId = flow.MenuId;
                newMenuFlow.FlowStep = flow.FlowStep;
                newMenuFlow.GroupNum = flow.GroupNum;
                newMenuFlow.OrderNum = flow.OrderNum;
                newMenuFlow.AuditFlowName = flow.AuditFlowName;
                newMenuFlow.RoleId = flow.RoleId;
                newMenuFlow.IsFlowEnd = flow.IsFlowEnd;
                new Model.SGGLDB(Funs.ConnString).SubmitChanges();
            }
        }

       /// <summary>
       /// 删除工作流信息
       /// </summary>
       /// <param name="FlowOperateId"></param>
       public static void DeleteAuditFlow(string FlowOperateId)
       {
           Model.Sys_MenuFlowOperate flow = new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate.First(e => e.FlowOperateId == FlowOperateId);
           if (flow != null)
           {
               new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate.DeleteOnSubmit(flow);
               new Model.SGGLDB(Funs.ConnString).SubmitChanges();
           }
       }
       
       /// <summary>
       ///  步骤更新顺序
       /// </summary>
       public static void SetSortIndex(string menuId)
       {
           var menuFlowOperate = from x in new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate where x.MenuId == menuId  select x;
           if (menuFlowOperate.Count() > 0)
           {
               var maxSortIndex = menuFlowOperate.Select(x => x.FlowStep).Max();
               if (menuFlowOperate.Count() < maxSortIndex)
               {
                   int sortIndex = 0;
                   foreach (var item in menuFlowOperate)
                   {
                       sortIndex++;
                       item.FlowStep = sortIndex;
                       new Model.SGGLDB(Funs.ConnString).SubmitChanges();
                   }
               }
           }
       }

       /// <summary>
       ///  步骤顺序说明
       /// </summary>
       public static string GetFlowOperateName(string menuId)
       {
           string returnValue = string.Empty;
           var menuFlowOperate = from x in new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate 
                                 where x.MenuId == menuId 
                                 orderby x.FlowStep
                                 select x;
           if (menuFlowOperate.Count() > 0)
           {
               foreach (var item in menuFlowOperate)
               {
                   returnValue += ("第" + item.FlowStep + "步[" + item.AuditFlowName+"]");
                   if(item.IsFlowEnd == false || !item.IsFlowEnd.HasValue)
                   {
                       returnValue += ("办理角色为{" + BLL.RoleService.getRoleNamesRoleIds(item.RoleId) + "}；");
                   }
               }
           }
           if (string.IsNullOrEmpty(returnValue))
           {
               returnValue = "未定义流程审批规则。";
           }

           return returnValue;
       }

        /// <summary>
        /// 获取分公司表下拉框
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        public static void InitMenuFlowOperateDropDownList(FineUIPro.DropDownList dropName,string menuId, string thisId, bool isShowPlease)
        {
            dropName.DataValueField = "FlowOperateId";
            dropName.DataTextField = "AuditFlowName";
            dropName.DataSource = (from x in new Model.SGGLDB(Funs.ConnString).Sys_MenuFlowOperate
                                   where x.MenuId == menuId && x.IsFlowEnd == false && x.FlowOperateId != thisId
                                   orderby x.FlowStep
                                   select x).ToList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
