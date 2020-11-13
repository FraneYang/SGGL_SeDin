using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 工作交接
    /// </summary>
    public static class WorkHandoverService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取工作交接
        /// </summary>
        /// <param name="WorkHandoverId"></param>
        /// <returns></returns>
        public static Model.ZHGL_WorkHandover GetWorkHandoverById(string WorkHandoverId)
        {
            return Funs.DB.ZHGL_WorkHandover.FirstOrDefault(e => e.WorkHandoverId == WorkHandoverId);
        }

        /// <summary>
        /// 添加工作交接
        /// </summary>
        /// <param name="WorkHandover"></param>
        public static void AddWorkHandover(Model.ZHGL_WorkHandover WorkHandover)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_WorkHandover newWorkHandover = new Model.ZHGL_WorkHandover
            {
                WorkHandoverId = WorkHandover.WorkHandoverId,
                ProjectId = WorkHandover.ProjectId,
                TransferMan = WorkHandover.TransferMan,
                TransferManDepart = WorkHandover.TransferManDepart,
                ReceiveMan = WorkHandover.ReceiveMan,
                ReceiveManDepart = WorkHandover.ReceiveManDepart,
                WorkPostId = WorkHandover.WorkPostId,
                TransferDate = WorkHandover.TransferDate,
                State = WorkHandover.State,
            };
            db.ZHGL_WorkHandover.InsertOnSubmit(newWorkHandover);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工作交接
        /// </summary>
        /// <param name="WorkHandover"></param>
        public static void UpdateWorkHandover(Model.ZHGL_WorkHandover WorkHandover)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_WorkHandover newWorkHandover = db.ZHGL_WorkHandover.FirstOrDefault(e => e.WorkHandoverId == WorkHandover.WorkHandoverId);
            if (newWorkHandover != null)
            {
                newWorkHandover.TransferMan = WorkHandover.TransferMan;
                newWorkHandover.TransferManDepart = WorkHandover.TransferManDepart;
                newWorkHandover.ReceiveMan = WorkHandover.ReceiveMan;
                newWorkHandover.ReceiveManDepart = WorkHandover.ReceiveManDepart;
                newWorkHandover.WorkPostId = WorkHandover.WorkPostId;
                newWorkHandover.TransferDate = WorkHandover.TransferDate;
                newWorkHandover.State = WorkHandover.State;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除工作交接
        /// </summary>
        /// <param name="WorkHandoverId"></param>
        public static void DeleteWorkHandoverById(string WorkHandoverId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_WorkHandover WorkHandover = db.ZHGL_WorkHandover.FirstOrDefault(e => e.WorkHandoverId == WorkHandoverId);
            if (WorkHandover != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(WorkHandover.WorkHandoverId);
                db.ZHGL_WorkHandover.DeleteOnSubmit(WorkHandover);
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
                if (state.ToString() == BLL.Const.WorkHandover_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.WorkHandover_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.WorkHandover_Audit1)
                {
                    return "接收人确认";
                }
                else if (state.ToString() == BLL.Const.WorkHandover_Audit2)
                {
                    return "移交人主管确认";
                }
                else if (state.ToString() == BLL.Const.WorkHandover_Complete)
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
        public static string ConvertMan(object WorkHandoverId)
        {
            if (WorkHandoverId != null)
            {
                Model.ZHGL_WorkHandoverApprove a = WorkHandoverApproveService.GetWorkHandoverApproveByWorkHandoverId(WorkHandoverId.ToString());
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
            if (state == Const.WorkHandover_Compile || state == Const.WorkHandover_ReCompile)  //无是否同意
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("接收人确认", Const.WorkHandover_Audit1);
                return lis;
            }
            else if (state == Const.WorkHandover_Audit1)   //有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("移交人主管确认", Const.WorkHandover_Audit2);//是 加载
                lis[1] = new ListItem("重新编制", Const.WorkHandover_ReCompile);//否加载
                return lis;
            }
            else if (state == Const.WorkHandover_Audit2)//有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.WorkHandover_Complete);//是 加载
                lis[1] = new ListItem("接收人确认", Const.WorkHandover_Audit1);//否加载
                return lis;
            }
            else
                return null;
        }
    }
}
