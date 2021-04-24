using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 10-2：主要机械设备投入计划表
    /// </summary>
    public class AttachUrl10_MachineInputService
    {
        /// <summary>
        /// 根据附件Id获取主要机械设备投入计划表列表
        /// </summary>
        /// <param name="attachUrlId"></param>
        /// <returns></returns>
        public static List<Model.PHTGL_AttachUrl10_MachineInput> GetMachineInputByAttachUrlId(string attachUrlId)
        {
            return (from x in Funs.DB.PHTGL_AttachUrl10_MachineInput where x.AttachUrlId == attachUrlId select x).ToList();
        }

        /// <summary>
        /// 添加主要机械设备投入计划表
        /// </summary>
        /// <param name="machineInput"></param>
        public static void AddMachineInput(Model.PHTGL_AttachUrl10_MachineInput machineInput)
        {
            Model.PHTGL_AttachUrl10_MachineInput newMachineInput = new Model.PHTGL_AttachUrl10_MachineInput();
            newMachineInput.AttachUrlItemId = machineInput.AttachUrlItemId;
            newMachineInput.AttachUrlId = machineInput.AttachUrlId;
            newMachineInput.MachineName = machineInput.MachineName;
            newMachineInput.MachineSpec = machineInput.MachineSpec;
            newMachineInput.Number = machineInput.Number;
            newMachineInput.LeasedOrOwned = machineInput.LeasedOrOwned;
            newMachineInput.Remarks = machineInput.Remarks;
            Funs.DB.PHTGL_AttachUrl10_MachineInput.InsertOnSubmit(newMachineInput);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 根据附件Id删除相关主要机械设备投入计划表
        /// </summary>
        /// <param name="attachUrlId"></param>
        public static void DeleteMachineInputByAttachUrlId(string attachUrlId)
        {
            var q = (from x in Funs.DB.PHTGL_AttachUrl10_MachineInput where x.AttachUrlId == attachUrlId select x).ToList();
            if (q != null)
            {
                Funs.DB.PHTGL_AttachUrl10_MachineInput.DeleteAllOnSubmit(q);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
