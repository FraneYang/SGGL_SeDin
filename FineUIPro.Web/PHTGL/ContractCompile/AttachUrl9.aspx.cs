using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl9 : PageBase
    {
        private bool AppendToEnd = false;

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
                    Model.PHTGL_AttachUrl9_SubPerson subPerson = BLL.AttachUrl9_SubPersonService.GetSubPersonByAttachId(attachUrlId);
                    if (subPerson == null)
                    {
                        subPerson = BLL.AttachUrl9_SubPersonService.GetSubPersonByAttachId(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 9).AttachUrlId);
                    }
                    if (subPerson != null)
                    {
                        this.txtProjectManager.Text = subPerson.ProjectManager;
                        this.txtProjectEngineer.Text = subPerson.ProjectEngineer;
                        this.txtConstructionManager.Text = subPerson.ConstructionManager;
                        this.txtQualityManager.Text = subPerson.QualityManager;
                        this.txtHSEManager.Text = subPerson.HSEManager;
                        this.txtPersonnel_Technician.Text = subPerson.Personnel_Technician;
                        this.txtPersonnel_Civil_engineering.Text = subPerson.Personnel_Civil_engineering;
                        this.txtPersonnel_Installation.Text = subPerson.Personnel_Installation;
                        this.txtPersonnel_Electrical.Text = subPerson.Personnel_Electrical;
                        this.txtPersonnel_meter.Text = subPerson.Personnel_meter;

                        BindGrid(attachUrlId);
                    }
                    #region Grid1
                    // 删除选中单元格的客户端脚本
                    string deleteScript = GetDeleteScript();

                    JObject defaultObj = new JObject();
                    defaultObj.Add("WorkPostName", "");
                    defaultObj.Add("Number", "");
                    defaultObj.Add("Arrivaltime", "");
                    defaultObj.Add("Remarks", "");

                    // 在第一行新增一条数据
                    btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
                    // 删除选中行按钮
                    btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript;
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
        #endregion

        #region 绑定Grid
        private void BindGrid(string attachUrlId)
        {
            List<Model.PHTGL_AttachUrl9_SubStaffing> lists = BLL.AttachUrl9_SubStaffingService.GetSubStaffingByAttachUrlId(attachUrlId);
            Grid1.DataSource = lists;
            Grid1.DataBind();
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
                Model.PHTGL_AttachUrl9_SubPerson newSubPerson = new Model.PHTGL_AttachUrl9_SubPerson();
                newSubPerson.ProjectManager = this.txtProjectManager.Text.Trim();
                newSubPerson.ProjectEngineer = this.txtProjectEngineer.Text.Trim();
                newSubPerson.ConstructionManager = this.txtConstructionManager.Text.Trim();
                newSubPerson.QualityManager = this.txtQualityManager.Text.Trim();
                newSubPerson.HSEManager = this.txtHSEManager.Text.Trim();
                newSubPerson.Personnel_Technician = this.txtPersonnel_Technician.Text.Trim();
                newSubPerson.Personnel_Civil_engineering = this.txtPersonnel_Civil_engineering.Text.Trim();
                newSubPerson.Personnel_Installation = this.txtPersonnel_Installation.Text.Trim();
                newSubPerson.Personnel_Electrical = this.txtPersonnel_Electrical.Text.Trim();
                newSubPerson.Personnel_meter = this.txtPersonnel_meter.Text.Trim();
                Model.PHTGL_AttachUrl9_SubPerson subPerson = BLL.AttachUrl9_SubPersonService.GetSubPersonByAttachId(attachUrlId);
                if (subPerson != null)
                {
                    newSubPerson.AttachUrlId = attachUrlId;
                    newSubPerson.AttachUrlItemId = subPerson.AttachUrlItemId;
                    BLL.AttachUrl9_SubPersonService.UpdateSubPerson(newSubPerson);
                }
                else
                {
                    newSubPerson.AttachUrlId = attachUrlId;
                    newSubPerson.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl9_SubPerson));
                    BLL.AttachUrl9_SubPersonService.AddSubPerson(newSubPerson);
                }
                //施工分包商组织机构关键人员名单
                BLL.AttachUrl9_SubStaffingService.DeleteSubStaffingByAttachUrlId(attachUrlId);
                List<Model.PHTGL_AttachUrl9_SubStaffing> list = new List<Model.PHTGL_AttachUrl9_SubStaffing>();
                JArray EditorArr = Grid1.GetMergedData();
                if (EditorArr.Count > 0)
                {
                    Model.PHTGL_AttachUrl9_SubStaffing model = null;
                    for (int i = 0; i < EditorArr.Count; i++)
                    {
                        JObject objects = (JObject)EditorArr[i];
                        model = new Model.PHTGL_AttachUrl9_SubStaffing();
                        model.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl9_SubStaffing));
                        model.AttachUrlId = attachUrlId;
                        model.WorkPostName = objects["values"]["WorkPostName"].ToString();
                        model.Number = Funs.GetNewInt(objects["values"]["Number"].ToString());
                        model.Arrivaltime = Funs.GetNewDateTime(objects["values"]["Arrivaltime"].ToString());
                        model.Remarks = objects["values"]["Remarks"].ToString();
                        BLL.AttachUrl9_SubStaffingService.AddSubStaffing(model);
                    }
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }
        #endregion
    }
}