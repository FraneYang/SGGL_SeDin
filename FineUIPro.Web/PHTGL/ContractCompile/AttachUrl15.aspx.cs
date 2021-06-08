using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl15 : PageBase
    {
        private bool AppendToEnd = false;

        private bool AppendToEnd2 = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    BindSch1(attachUrlId);
                    BindSch2(attachUrlId);
                    BindSch3(attachUrlId);
                    BindSch4(attachUrlId);

                    #region 附表1
                    // 删除选中单元格的客户端脚本
                    string deleteScript = GetDeleteScript();

                    JObject defaultObj = new JObject();
                    defaultObj.Add("Type", "");
                    defaultObj.Add("MainPoints", "");
 

                    // 在第一行新增一条数据
                    btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
                    // 删除选中行按钮
                    btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript;
                    #endregion

                    #region 附表3
                    // 删除选中单元格的客户端脚本
                    string deleteScript2 = GetDeleteScript2();

                    JObject defaultObj2 = new JObject();
                    defaultObj2.Add("StartTime", "");
                    defaultObj2.Add("Endtime", "");
                    defaultObj2.Add("Watermeter_Start", "");
                    defaultObj2.Add("Watermeter_End", "");
                    defaultObj2.Add("Watermeter_Read", "");
                    defaultObj2.Add("Elemeter_Start", "");
                    defaultObj2.Add("Elemeter_End", "");
                    defaultObj2.Add("Elemeter_Read", "");
                    defaultObj2.Add("SubcontractorsName", "");
                    defaultObj2.Add("GeneralContractorName", "");
                    defaultObj2.Add("Remarks", "");

                    // 在第一行新增一条数据
                    btnAdd.OnClientClick = Grid3.GetAddNewRecordReference(defaultObj2, AppendToEnd2);
                    // 删除选中行按钮
                    btnDel.OnClientClick = Grid3.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript2;
                    #endregion

                }
            }
        }

        #region 绑定
        void BindSch1(string attachUrlId)
        {
            List<Model.PHTGL_AttachUrl15_Sch1> lists = BLL.PHTGL_AttachUrl15_Sch1Service.GetPHTGL_AttachUrl15ByAttachUrlId(attachUrlId);
            if (lists .Count>0)
            {
                Sch1_ProjectName.Text = lists[0].ProjectName.ToString();
                Sch1_ContractId.Text = lists[0].ContractId.ToString();
                txtAttachUrlContent.Text = lists[0].AttachUrlContent.ToString();
                Sch1_Opinion.Text = lists[0].Opinion.ToString();
            }
            Grid1.DataSource = lists;
            Grid1.DataBind();
        }
        void BindSch2(string attachUrlId)
        {
            var Sch2 = BLL.PHTGL_AttachUrl15_Sch2Service.GetPHTGL_AttachUrl15_Sch2ById(attachUrlId);
            if (Sch2!=null)
            {
 
                Sch2_ProjectName.Text = Sch2.ProjectName.ToString() ;
                Sch2_ContractId.Text = Sch2.ContractId.ToString();
                Sch2_Company.Text = Sch2.Company.ToString();
                Sch2_ConstructionTask.Text = Sch2.ConstructionTask.ToString();
                Sch2_Maxcapacitance.Text = Sch2.Maxcapacitance.ToString();
                Sch2_MaxuseWtater.Text = Sch2.MaxuseWtater.ToString();
                Sch2_elemeterPosition.Text = Sch2.ElemeterPosition.ToString();
                Sch2_WatermeterPosition.Text = Sch2.WatermeterPosition.ToString();
                Sch2_elemeterRead.Text = Sch2.ElemeterRead.ToString();
                Sch2_WatermeterRead.Text = Sch2. WatermeterRead.ToString();
                Sch2_IsLineLayout.SelectedValue = Sch2. IsLineLayout.ToString();
                Sch2_IsPowerBox.SelectedValue = Sch2.IsPowerBox.ToString();
                Sch2_IsProfessional_ele.SelectedValue = Sch2.IsProfessional_ele.ToString();
                Sch2_IsLineInstall.SelectedValue = Sch2.IsLineInstall.ToString();
                Sch2_IsValve.SelectedValue = Sch2.IsValve.ToString();
                Sch2_Terminalnumber.Text = Sch2.Terminalnumber.ToString();
                Sch2_LineCabinetNumber.Text = Sch2.LineCabinetNumber.ToString();
                Sch2_electricPrice.Text = Sch2.ElectricPrice.ToString();
                Sch2_WaterPrice.Text = Sch2.WaterPrice.ToString();
            }
        }
        void BindSch3(string attachUrlId)
        {
            List<Model.PHTGL_AttachUrl15_Sch3> lists= BLL.PHTGL_AttachUrl15_Sch3Service.GetPHTGL_AttachUrl15ByAttachUrlId(attachUrlId);
            Grid3.DataSource = lists;
            Grid3.DataBind();
        }
        void BindSch4(string attachUrlId)
        {
            var Sch4 = BLL.PHTGL_AttachUrl15_Sch4Service.GetPHTGL_AttachUrl15_Sch4ById(attachUrlId);
            if (Sch4!=null)
            {
                Sch4_ProjectName.Text = Sch4.ProjectName.ToString();
                Sch4_ContractId.Text = Sch4.ContractId.ToString();
                Sch4_SubcontractorsName.Text = Sch4.SubcontractorsName.ToString();
                Sch4_Type.SelectedValue = Sch4.Type.ToString();
                Sch4_Time.Text = Sch4.Time.ToString();
                Sch4_Reason.Text = Sch4.Reason.ToString();
                Sch4_Position.Text = Sch4.Position.ToString();
                Sch4_ImpPlan.Text = Sch4.ImpPlan.ToString();
                Sch4_Recoverymeasures.Text = Sch4.Recoverymeasures.ToString();
                Sch4_Caption.Text = Sch4.Caption.ToString();
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
            return Confirm.GetShowReference("确定删除当前数据吗？", String.Empty, MessageBoxIcon.Question, Grid3.GetDeleteSelectedRowsReference(), String.Empty);
        }
        #endregion

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
                #region 附件1
                BLL.PHTGL_AttachUrl15_Sch1Service.DeleteAttachUrl15_Sch1ByAttachUrlId(attachUrlId);
                List<Model.PHTGL_AttachUrl15_Sch1> list = new List<Model.PHTGL_AttachUrl15_Sch1>();
                JArray EditorArr = Grid1.GetMergedData();
                if (EditorArr.Count > 0)
                {
                    Model.PHTGL_AttachUrl15_Sch1 model = null;
                    for (int i = 0; i < EditorArr.Count; i++)
                    {
                        JObject objects = (JObject)EditorArr[i];
                        model = new Model.PHTGL_AttachUrl15_Sch1();
                        model.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl15_Sch1));
                        model.AttachUrlId = attachUrlId;
                        model.AttachUrlContent = txtAttachUrlContent.Text;
                        model.ProjectName = Sch1_ProjectName.Text;
                        model.ContractId = Sch1_ContractId.Text;
                        //      model.OrderNumber = objects["values"]["Subject"].ToString();
                        model.Type = objects["values"]["Type"].ToString();
                        model.MainPoints = objects["values"]["MainPoints"].ToString();
                        model.Opinion = Sch1_Opinion.Text;
                        BLL.PHTGL_AttachUrl15_Sch1Service.AddPHTGL_AttachUrl15_Sch1(model);
                    }
                }
                #endregion
                #region 附件2
 
                var Sch2 = BLL.PHTGL_AttachUrl15_Sch2Service.GetPHTGL_AttachUrl15_Sch2ById(attachUrlId);
                if (Sch2 != null)
                {
                    Sch2.ProjectName = Sch2_ProjectName.Text;
                    Sch2.ContractId = Sch2_ContractId.Text;
                    Sch2.Company = Sch2_Company.Text;
                    Sch2.ConstructionTask = Sch2_ConstructionTask.Text;
                    Sch2.Maxcapacitance = Funs.GetNewInt(Sch2_Maxcapacitance.Text);
                    Sch2.MaxuseWtater = Funs.GetNewInt(Sch2_MaxuseWtater.Text);
                    Sch2.ElemeterPosition = Funs.GetNewInt(Sch2_elemeterPosition.Text);
                    Sch2.WatermeterPosition = Funs.GetNewInt(Sch2_WatermeterPosition.Text);
                    Sch2.ElemeterRead = Funs.GetNewInt(Sch2_elemeterRead.Text);
                    Sch2.WatermeterRead = Funs.GetNewInt(Sch2_WatermeterRead.Text);
                    Sch2.IsLineLayout = Convert.ToBoolean(Sch2_IsLineLayout.SelectedValue);
                    Sch2.IsPowerBox = Convert.ToBoolean(Sch2_IsPowerBox.SelectedValue);
                    Sch2.IsProfessional_ele = Convert.ToBoolean(Sch2_IsProfessional_ele.SelectedValue);
                    Sch2.IsLineInstall = Convert.ToBoolean(Sch2_IsLineInstall.SelectedValue);
                    Sch2.IsValve = Convert.ToBoolean(Sch2_IsValve.SelectedValue);
                    Sch2.Terminalnumber = Sch2_Terminalnumber.Text;
                    Sch2.LineCabinetNumber = Sch2_LineCabinetNumber.Text;
                    Sch2.ElectricPrice = Sch2_electricPrice.Text;
                    Sch2.WaterPrice = Sch2_WaterPrice.Text;

                    BLL.PHTGL_AttachUrl15_Sch2Service.UpdatePHTGL_AttachUrl15_Sch2(Sch2);
                }
                else
                {
                    Model.PHTGL_AttachUrl15_Sch2 newScch2 = new Model.PHTGL_AttachUrl15_Sch2();

                    newScch2.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl15_Sch2));
                    newScch2.AttachUrlId = attachUrlId;
                    newScch2.ProjectName = Sch2_ProjectName.Text;
                    newScch2.ContractId = Sch2_ContractId.Text;
                    newScch2.Company = Sch2_Company.Text;
                    newScch2.ConstructionTask = Sch2_ConstructionTask.Text;
                    newScch2.Maxcapacitance = Funs.GetNewInt(Sch2_Maxcapacitance.Text);
                    newScch2.MaxuseWtater = Funs.GetNewInt(Sch2_MaxuseWtater.Text);
                    newScch2.ElemeterPosition = Funs.GetNewInt(Sch2_elemeterPosition.Text);
                    newScch2.WatermeterPosition = Funs.GetNewInt(Sch2_WatermeterPosition.Text);
                    newScch2.ElemeterRead = Funs.GetNewInt(Sch2_elemeterRead.Text);
                    newScch2.WatermeterRead = Funs.GetNewInt(Sch2_WatermeterRead.Text);
                    newScch2.IsLineLayout = Convert.ToBoolean(Sch2_IsLineLayout.SelectedValue);
                    newScch2.IsPowerBox = Convert.ToBoolean(Sch2_IsPowerBox.SelectedValue);
                    newScch2.IsProfessional_ele = Convert.ToBoolean(Sch2_IsProfessional_ele.SelectedValue);
                    newScch2.IsLineInstall = Convert.ToBoolean(Sch2_IsLineInstall.SelectedValue);
                    newScch2.IsValve = Convert.ToBoolean(Sch2_IsValve.SelectedValue);
                    newScch2.Terminalnumber = Sch2_Terminalnumber.Text;
                    newScch2.LineCabinetNumber = Sch2_LineCabinetNumber.Text;
                    newScch2.ElectricPrice = Sch2_electricPrice.Text;
                    newScch2.WaterPrice = Sch2_WaterPrice.Text;
                    BLL.PHTGL_AttachUrl15_Sch2Service.AddPHTGL_AttachUrl15_Sch2(newScch2);
 
                }

                #endregion
                #region 附件3
                BLL.PHTGL_AttachUrl15_Sch3Service.DeleteAttachUrl15_Sch3ByAttachUrlId(attachUrlId);
                List<Model.PHTGL_AttachUrl15_Sch3> list3 = new List<Model.PHTGL_AttachUrl15_Sch3>();
                JArray EditorArr3 = Grid3.GetMergedData();
                if (EditorArr3.Count > 0)
                {
                    Model.PHTGL_AttachUrl15_Sch3 model = null;
                    for (int i = 0; i < EditorArr3.Count; i++)
                    {
                        JObject objects = (JObject)EditorArr3[i];
                        model = new Model.PHTGL_AttachUrl15_Sch3();

                        model.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl15_Sch3));
                        model.AttachUrlId = attachUrlId;
                        //  model.SerialNumber = objects["values"]["MainPoints"].ToString();
                        model.StartTime = Funs.GetNewDateTime(objects["values"]["StartTime"].ToString());
                        model.Endtime = Funs.GetNewDateTime(objects["values"]["Endtime"].ToString());
                        model.Watermeter_Start = Funs.GetNewInt(objects["values"]["Watermeter_Start"].ToString());
                        model.Watermeter_End = Funs.GetNewInt(objects["values"]["Watermeter_End"].ToString());
                        model.Watermeter_Read = Funs.GetNewInt(objects["values"]["Watermeter_Read"].ToString());
                        model.Elemeter_Start = Funs.GetNewInt(objects["values"]["Elemeter_Start"].ToString());
                        model.Elemeter_End = Funs.GetNewInt(objects["values"]["Elemeter_End"].ToString());
                        model.Elemeter_Read = Funs.GetNewInt(objects["values"]["Elemeter_Read"].ToString());
                        model.GeneralContractorName = objects["values"]["GeneralContractorName"].ToString();
                        model.SubcontractorsName = objects["values"]["SubcontractorsName"].ToString();
                        model.Remark = objects["values"]["Remark"].ToString();
                        BLL.PHTGL_AttachUrl15_Sch3Service.AddPHTGL_AttachUrl15_Sch3(model);
                    }
                }

                #endregion

                #region 附件4
                var Sch4 = BLL.PHTGL_AttachUrl15_Sch4Service.GetPHTGL_AttachUrl15_Sch4ById(attachUrlId);
                if (Sch4 !=null)
                {
                    //Sch4.AttachUrlContent]
                    Sch4.ProjectName = Sch4_ProjectName.Text;
                    Sch4.ContractId = Sch4_ContractId.Text;
                    Sch4.SubcontractorsName = Sch4_SubcontractorsName.Text;
                    Sch4.Type = Funs.GetNewInt ( Sch4_Type.SelectedValue);
                    Sch4.Time =Funs.GetNewDateTime( Sch4_Time.SelectedDate.ToString());
                    Sch4.Reason = Sch4_Reason.Text;
                    Sch4.Position = Sch4_Position.Text;
                    Sch4.ImpPlan = Sch4_ImpPlan.Text;
                    Sch4.Recoverymeasures = Sch4_Recoverymeasures.Text;
                    Sch4.Caption = Sch4_Caption.Text;
                    BLL.PHTGL_AttachUrl15_Sch4Service.UpdatePHTGL_AttachUrl15_Sch4(Sch4);
                }
                else
                {
                    Model.PHTGL_AttachUrl15_Sch4 newSch4 = new Model.PHTGL_AttachUrl15_Sch4();
                    newSch4.AttachUrlId = attachUrlId;
                    newSch4.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl15_Sch4));
                    newSch4.ProjectName = Sch4_ProjectName.Text;
                    newSch4.ContractId = Sch4_ContractId.Text;
                    newSch4.SubcontractorsName = Sch4_SubcontractorsName.Text;
                    newSch4.Type = Funs.GetNewInt(Sch4_Type.SelectedValue);
                    newSch4.Time = Funs.GetNewDateTime(Sch4_Time.SelectedDate.ToString());
                    newSch4.Reason = Sch4_Reason.Text;
                    newSch4.Position = Sch4_Position.Text;
                    newSch4.ImpPlan = Sch4_ImpPlan.Text;
                    newSch4.Recoverymeasures = Sch4_Recoverymeasures.Text;
                    newSch4.Caption = Sch4_Caption.Text;

                    BLL.PHTGL_AttachUrl15_Sch4Service.AddPHTGL_AttachUrl15_Sch4(newSch4);
                }

                #endregion
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}