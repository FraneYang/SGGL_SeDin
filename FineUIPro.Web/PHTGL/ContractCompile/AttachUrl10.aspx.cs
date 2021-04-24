using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl10 : PageBase
    {
        private bool AppendToEnd = false;

        private bool AppendToEnd2 = false;

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    BindGrid(attachUrlId);
                    BindGrid2(attachUrlId);

                    #region Grid1
                    // 删除选中单元格的客户端脚本
                    string deleteScript = GetDeleteScript();

                    JObject defaultObj = new JObject();
                    defaultObj.Add("Subject", "");
                    defaultObj.Add("WorkType", "");
                    defaultObj.Add("PersonNumber", "");
                    defaultObj.Add("LifeTime", "");
                    defaultObj.Add("Remarks", "");

                    // 在第一行新增一条数据
                    btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
                    // 删除选中行按钮
                    btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript;
                    #endregion

                    #region Grid2
                    // 删除选中单元格的客户端脚本
                    string deleteScript2 = GetDeleteScript2();

                    JObject defaultObj2 = new JObject();
                    defaultObj2.Add("MachineName", "");
                    defaultObj2.Add("MachineSpec", "");
                    defaultObj2.Add("number", "");
                    defaultObj2.Add("LeasedOrOwned", "");
                    defaultObj2.Add("Remarks", "");

                    // 在第一行新增一条数据
                    btnAdd.OnClientClick = Grid2.GetAddNewRecordReference(defaultObj2, AppendToEnd2);
                    // 删除选中行按钮
                    btnDel.OnClientClick = Grid2.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript2;
                    #endregion
                }
            }
        }
        #endregion

        #region 删除选中行脚本
        // 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("确定删除当前数据吗？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }

        private string GetDeleteScript2()
        {
            return Confirm.GetShowReference("确定删除当前数据吗？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedRowsReference(), String.Empty);
        }
        #endregion

        #region 绑定Grid
        private void BindGrid(string attachUrlId)
        {
            List<Model.PHTGL_AttachUrl10_HumanInput> lists = BLL.AttachUrl10_HumanInputService.GetHumanInputByAttachUrlId(attachUrlId);
            Grid1.DataSource = lists;
            Grid1.DataBind();
        }

        private void BindGrid2(string attachUrlId)
        {
            List<Model.PHTGL_AttachUrl10_MachineInput> lists = BLL.AttachUrl10_MachineInputService.GetMachineInputByAttachUrlId(attachUrlId);
            Grid2.DataSource = lists;
            Grid2.DataBind();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string attachUrlId = Request.Params["AttachUrlId"];
            if (!string.IsNullOrEmpty(attachUrlId))
            {
                #region 10-1：施工人力投入计划表
                BLL.AttachUrl10_HumanInputService.DeleteHumanInputByAttachUrlId(attachUrlId);
                List<Model.PHTGL_AttachUrl10_HumanInput> list = new List<Model.PHTGL_AttachUrl10_HumanInput>();
                JArray EditorArr = Grid1.GetMergedData();
                if (EditorArr.Count > 0)
                {
                    Model.PHTGL_AttachUrl10_HumanInput model = null;
                    for (int i = 0; i < EditorArr.Count; i++)
                    {
                        JObject objects = (JObject)EditorArr[i];
                        model = new Model.PHTGL_AttachUrl10_HumanInput();
                        model.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl10_HumanInput));
                        model.AttachUrlId = attachUrlId;
                        model.Subject = objects["values"]["Subject"].ToString();
                        model.WorkType = objects["values"]["WorkType"].ToString();
                        model.PersonNumber = Funs.GetNewInt(objects["values"]["PersonNumber"].ToString());
                        model.LifeTime = objects["values"]["LifeTime"].ToString();
                        model.Remarks = objects["values"]["Remarks"].ToString();
                        BLL.AttachUrl10_HumanInputService.AddHumanInput(model);
                    }
                }
                #endregion
                #region 主要机械设备投入计划表
                BLL.AttachUrl10_MachineInputService.DeleteMachineInputByAttachUrlId(attachUrlId);
                List<Model.PHTGL_AttachUrl10_MachineInput> list2 = new List<Model.PHTGL_AttachUrl10_MachineInput>();
                JArray EditorArr2 = Grid2.GetMergedData();
                if (EditorArr2.Count > 0)
                {
                    Model.PHTGL_AttachUrl10_MachineInput model = null;
                    for (int i = 0; i < EditorArr2.Count; i++)
                    {
                        JObject objects = (JObject)EditorArr2[i];
                        model = new Model.PHTGL_AttachUrl10_MachineInput();
                        model.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl10_MachineInput));
                        model.AttachUrlId = attachUrlId;
                        model.MachineName = objects["values"]["MachineName"].ToString();
                        model.MachineSpec = objects["values"]["MachineSpec"].ToString();
                        model.Number = objects["values"]["number"].ToString();
                        model.LeasedOrOwned = objects["values"]["LeasedOrOwned"].ToString();
                        model.Remarks = objects["values"]["Remarks"].ToString();
                        BLL.AttachUrl10_MachineInputService.AddMachineInput(model);
                    }
                }
                #endregion
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }
        #endregion
    }
}