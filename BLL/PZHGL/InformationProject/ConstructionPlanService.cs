using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 总承包商施工计划
    /// </summary>
    public static class ConstructionPlanService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取总承包商施工计划
        /// </summary>
        /// <param name="ConstructionPlanId"></param>
        /// <returns></returns>
        public static Model.ZHGL_ConstructionPlan GetConstructionPlanById(string ConstructionPlanId)
        {
            return Funs.DB.ZHGL_ConstructionPlan.FirstOrDefault(e => e.ConstructionPlanId == ConstructionPlanId);
        }

        /// <summary>
        /// 添加总承包商施工计划
        /// </summary>
        /// <param name="ConstructionPlan"></param>
        public static void AddConstructionPlan(Model.ZHGL_ConstructionPlan ConstructionPlan)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionPlan newConstructionPlan = new Model.ZHGL_ConstructionPlan
            {
                ConstructionPlanId = ConstructionPlan.ConstructionPlanId,
                ProjectId = ConstructionPlan.ProjectId,
                Code = ConstructionPlan.Code,
                Content = ConstructionPlan.Content,
                CompileMan = ConstructionPlan.CompileMan,
                CompileDate = ConstructionPlan.CompileDate,
                State = ConstructionPlan.State,
            };
            db.ZHGL_ConstructionPlan.InsertOnSubmit(newConstructionPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改总承包商施工计划
        /// </summary>
        /// <param name="ConstructionPlan"></param>
        public static void UpdateConstructionPlan(Model.ZHGL_ConstructionPlan ConstructionPlan)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionPlan newConstructionPlan = db.ZHGL_ConstructionPlan.FirstOrDefault(e => e.ConstructionPlanId == ConstructionPlan.ConstructionPlanId);
            if (newConstructionPlan != null)
            {
                newConstructionPlan.Code = ConstructionPlan.Code;
                newConstructionPlan.Content = ConstructionPlan.Content;
                newConstructionPlan.State = ConstructionPlan.State;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除总承包商施工计划
        /// </summary>
        /// <param name="ConstructionPlanId"></param>
        public static void DeleteConstructionPlanById(string ConstructionPlanId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionPlan ConstructionPlan = db.ZHGL_ConstructionPlan.FirstOrDefault(e => e.ConstructionPlanId == ConstructionPlanId);
            if (ConstructionPlan != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(ConstructionPlan.ConstructionPlanId);
                db.ZHGL_ConstructionPlan.DeleteOnSubmit(ConstructionPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.ConstructionPlan_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.ConstructionPlan_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.ConstructionPlan_Audit1)
                {
                    return "施工经理审核";
                }
                else if (state.ToString() == BLL.Const.ConstructionPlan_Audit2)
                {
                    return "施工管理部审核";
                }
                else if (state.ToString() == BLL.Const.ConstructionPlan_Audit3)
                {
                    return "项目经理批准";
                }
                else if (state.ToString() == BLL.Const.ConstructionPlan_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertMan(object ConstructionPlanId)
        {
            if (ConstructionPlanId != null)
            {
                Model.ZHGL_ConstructionPlanApprove a = ConstructionPlanApproveService.GetConstructionPlanApproveByConstructionPlanId(ConstructionPlanId.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        public static void InitHandleType(FineUIPro.DropDownList dropName, bool isShowPlease, string state)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetDHandleTypeByState(state);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state)
        {
            if (state == Const.ConstructionPlan_Compile || state == Const.ConstructionPlan_ReCompile)  //无是否同意
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("施工经理审核", Const.ConstructionPlan_Audit1);
                return lis;
            }
            else if (state == Const.ConstructionPlan_Audit1)   //有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("施工管理部审核", Const.ConstructionPlan_Audit2);//是 加载
                lis[1] = new ListItem("重新编制", Const.ConstructionPlan_ReCompile);//否加载
                return lis;
            }
            else if (state == Const.ConstructionPlan_Audit2)//有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("项目经理批准", Const.ConstructionPlan_Audit3);
                lis[1] = new ListItem("施工经理审核", Const.ConstructionPlan_Audit1);
                return lis;
            }
            else if (state == Const.ConstructionPlan_Audit3)//有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.ConstructionPlan_Complete);//是 加载
                lis[1] = new ListItem("施工管理部审核", Const.ConstructionPlan_Audit3);//否加载
                return lis;
            }
            else
                return null;
        }
    }
}
