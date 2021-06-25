using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl2 : PageBase
    {
        
  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 旧
                //GroupPanel1.Expanded = true;
                //GroupPanel2.Expanded = true;
                //GroupPanel3.Expanded = true;
                //GroupPanel4.Expanded = true;
                //GroupPanel5.Expanded = true;
                //GroupPanel6.Expanded = true;
                //////GroupPanel7.Expanded = true;

                //this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //string attachUrlId = Request.Params["AttachUrlId"];
                //if (!string.IsNullOrEmpty(attachUrlId))
                //{
                //    var att = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(attachUrlId);
                //    if (att == null)
                //    {
                //        att = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 2).AttachUrlId);
                //    }
                //    if (att != null)
                //    {
                //        this.txtContractPrice.Text = att.ContractPrice;
                //        this.txtComprehensiveUnitPrice.Text = att.ComprehensiveUnitPrice;
                //        this.txtComprehensiveRate1.Text = att.ComprehensiveRate1;
                //        this.txtComprehensiveRate2.Text = att.ComprehensiveRate2;
                //        this.txtComprehensiveRate3.Text = att.ComprehensiveRate3;
                //        this.txtComprehensiveRate4.Text = att.ComprehensiveRate4;
                //        this.txtComprehensiveRate5.Text = att.ComprehensiveRate5;
                //        this.txtTotalPriceDown1.Text = att.TotalPriceDown1;
                //        this.txtTotalPriceDown2.Text = att.TotalPriceDown2;
                //        this.txtTotalPriceDown3.Text = att.TotalPriceDown3;
                //        this.txtTotalPriceDown4.Text = att.TotalPriceDown4;
                //        this.txtTotalPriceDown5.Text = att.TotalPriceDown5;
                //        this.txtTechnicalWork.Text = att.TechnicalWork.HasValue ? att.TechnicalWork.ToString() : "";
                //        this.txtPhysicalLaborer.Text = att.PhysicalLaborer.HasValue ? att.PhysicalLaborer.ToString() : "";
                //        this.txtTestCar1.Text = att.TestCar1.HasValue ? att.TestCar1.ToString() : "";
                //        this.txtTestCar2.Text = att.TestCar2.HasValue ? att.TestCar2.ToString() : "";
                //        this.txtPayWay.Text = HttpUtility.HtmlDecode(att.PayWay);
                //        if (!string.IsNullOrEmpty( att.PayMethod))
                //        {
                //            CheckBoxList1.SelectedValueArray = att.PayMethod.Split(',');
                //            BindingPanal(att.PayMethod);
                //        }
                //        else
                //        {
                //            BindingPanal("");
                //        }

                //        BindGrid(att.AttachUrlItemId, "1");
                //        BindGrid2(att.AttachUrlItemId, "2");
                //    }
                //    #region Grid1
                //    // 删除选中单元格的客户端脚本
                //    string deleteScript = GetDeleteScript();

                //    JObject defaultObj = new JObject();
                //    defaultObj.Add("Specifications", "");
                //    defaultObj.Add("MachineTeamPrice", "");
                //    defaultObj.Add("Remark", "");

                //    // 在末尾新增一条数据
                //    btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, true);
                //    // 删除选中行按钮
                //    btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript;
                //    #endregion

                //    #region Grid2
                //    // 删除选中单元格的客户端脚本
                //    string deleteScript2 = GetDeleteScript2();

                //    JObject defaultObj2 = new JObject();
                //    defaultObj2.Add("Specifications", "");
                //    defaultObj2.Add("MachineTeamPrice", "");
                //    defaultObj2.Add("Remark", "");

                //    // 在末尾新增一条数据
                //    btnAdd.OnClientClick = Grid2.GetAddNewRecordReference(defaultObj2, true);
                //    // 删除选中行按钮
                //    btnDel.OnClientClick = Grid2.GetNoSelectionAlertReference("请选择一条记录!") + deleteScript2;
                //    #endregion
                //}

                #endregion
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string attachUrlId = Request.Params["AttachUrlId"];
                if (!string.IsNullOrEmpty(attachUrlId))
                {
                    var att = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(attachUrlId);
                    if (att == null)
                    {
                        att = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 2).AttachUrlId);
                    }
                    if (att != null)
                    {
                        this.txtAttachUrlContent.Text = HttpUtility.HtmlDecode(att.AttachUrlContent);
                    }
                }
            }
        }
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
                var attItem = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(attachUrlId);
                if (attItem != null)
                {
                    attItem.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    attItem.AttachUrlId = attachUrlId;
                    BLL.AttachUrl2Service.UpdateAttachUrl2(attItem);
                }
                else
                {
                    Model.PHTGL_AttachUrl2 newUrl = new Model.PHTGL_AttachUrl2();
                    newUrl.AttachUrlContent = this.txtAttachUrlContent.Text.Trim();
                    newUrl.AttachUrlId = attachUrlId;
                    newUrl.AttachUrlItemId = BLL.SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl2));
                    BLL.AttachUrl2Service.AddAttachUrl2(newUrl);
                }
                var att = BLL.AttachUrlService.GetAttachUrlById(attachUrlId);
                if (att != null)
                {
                    att.IsSelected = true;
                    BLL.AttachUrlService.UpdateAttachUrl(att);
                }
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        }

        #region 旧
        //#region 绑定Grid
        //private void BindGrid(string attachUrlItemId, string type)
        //{
        //    List<Model.PHTGL_AttachUrl2Detail> lists = BLL.AttachUrl2DetailService.GetDetailListByItemId(attachUrlItemId, type);
        //    Grid1.DataSource = lists;
        //    Grid1.DataBind();
        //}

        //private void BindGrid2(string attachUrlItemId, string type)
        //{
        //    List<Model.PHTGL_AttachUrl2Detail> lists = BLL.AttachUrl2DetailService.GetDetailListByItemId(attachUrlItemId, type);
        //    Grid2.DataSource = lists;
        //    Grid2.DataBind();
        //}
        //#endregion

        //#region 删除选中行脚本
        //// 删除选中行的脚本
        //private string GetDeleteScript()
        //{
        //    return Confirm.GetShowReference("确定删除当前数据吗？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        //}

        //private string GetDeleteScript2()
        //{
        //    return Confirm.GetShowReference("确定删除当前数据吗？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedRowsReference(), String.Empty);
        //}
        //#endregion

        ///// <summary>
        ///// 保存
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    string attachUrlId = Request.Params["AttachUrlId"];
        //    if (!string.IsNullOrEmpty(attachUrlId))
        //    {
        //        Model.PHTGL_AttachUrl2 att = BLL.AttachUrl2Service.GetAttachUrlByAttachUrlId(attachUrlId);
        //        if (att != null)
        //        {
        //            this.hdAttachUrlItemId.Text = att.AttachUrlItemId;
        //            att.ContractPrice = this.txtContractPrice.Text.Trim();
        //            att.ComprehensiveUnitPrice = this.txtComprehensiveUnitPrice.Text.Trim();
        //            att.ComprehensiveRate1 = this.txtComprehensiveRate1.Text.Trim();
        //            att.ComprehensiveRate2 = this.txtComprehensiveRate2.Text.Trim();
        //            att.ComprehensiveRate3 = this.txtComprehensiveRate3.Text.Trim();
        //            att.ComprehensiveRate4 = this.txtComprehensiveRate4.Text.Trim();
        //            att.ComprehensiveRate5 = this.txtComprehensiveRate5.Text.Trim();
        //            att.TotalPriceDown1 = this.txtTotalPriceDown1.Text.Trim();
        //            att.TotalPriceDown2 = this.txtTotalPriceDown2.Text.Trim();
        //            att.TotalPriceDown3 = this.txtTotalPriceDown3.Text.Trim();
        //            att.TotalPriceDown4 = this.txtTotalPriceDown4.Text.Trim();
        //            att.TotalPriceDown5 = this.txtTotalPriceDown5.Text.Trim();
        //            att.TechnicalWork = Funs.GetNewDecimal(this.txtTechnicalWork.Text.Trim());
        //            att.PhysicalLaborer = Funs.GetNewDecimal(this.txtPhysicalLaborer.Text.Trim());
        //            att.TestCar1 = Funs.GetNewDecimal(this.txtTestCar1.Text.Trim());
        //            att.TestCar2 = Funs.GetNewDecimal(this.txtTestCar2.Text.Trim());
        //             //att.PayWay = HttpUtility.HtmlEncode(this.txtPayWay.Text);
        //            att.PayWay =  this.txtPayWay.Text;
        //            if (CheckBoxList1.SelectedItemArray.Length > 0)
        //            {
        //                att.PayMethod = string.Join(",", CheckBoxList1.SelectedValueArray);
        //            }
        //            BLL.AttachUrl2Service.UpdateAttachUrl2(att);
        //        }
        //        else
        //        {
        //            Model.PHTGL_AttachUrl2 newAtt = new Model.PHTGL_AttachUrl2();
        //            newAtt.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl2));
        //            this.hdAttachUrlItemId.Text = newAtt.AttachUrlItemId;
        //            newAtt.AttachUrlId = attachUrlId;
        //            newAtt.ContractPrice = this.txtContractPrice.Text.Trim();
        //            newAtt.ComprehensiveUnitPrice = this.txtComprehensiveUnitPrice.Text.Trim();
        //            newAtt.ComprehensiveRate1 = this.txtComprehensiveRate1.Text.Trim();
        //            newAtt.ComprehensiveRate2 = this.txtComprehensiveRate2.Text.Trim();
        //            newAtt.ComprehensiveRate3 = this.txtComprehensiveRate3.Text.Trim();
        //            newAtt.ComprehensiveRate4 = this.txtComprehensiveRate4.Text.Trim();
        //            newAtt.ComprehensiveRate5 = this.txtComprehensiveRate5.Text.Trim();
        //            newAtt.TotalPriceDown1 = this.txtTotalPriceDown1.Text.Trim();
        //            newAtt.TotalPriceDown2 = this.txtTotalPriceDown2.Text.Trim();
        //            newAtt.TotalPriceDown3 = this.txtTotalPriceDown3.Text.Trim();
        //            newAtt.TotalPriceDown4 = this.txtTotalPriceDown4.Text.Trim();
        //            newAtt.TotalPriceDown5 = this.txtTotalPriceDown5.Text.Trim();
        //            newAtt.TechnicalWork = Funs.GetNewDecimal(this.txtTechnicalWork.Text.Trim());
        //            newAtt.PhysicalLaborer = Funs.GetNewDecimal(this.txtPhysicalLaborer.Text.Trim());
        //            newAtt.TestCar1 = Funs.GetNewDecimal(this.txtTestCar1.Text.Trim());
        //            newAtt.TestCar2 = Funs.GetNewDecimal(this.txtTestCar2.Text.Trim());
        //            newAtt.PayWay = HttpUtility.HtmlEncode(this.txtPayWay.Text);
        //            if (CheckBoxList1.SelectedItemArray.Length >0 )
        //            {
        //                newAtt.PayMethod = string.Join(",", CheckBoxList1.SelectedValueArray);
        //            }
                    
        //            BLL.AttachUrl2Service.AddAttachUrl2(newAtt);
        //        }
        //        //先删除再保存
        //        BLL.AttachUrl2DetailService.DeleteAttachUrl2DetailByItemId(this.hdAttachUrlItemId.Text, "1");
        //        List<Model.PHTGL_AttachUrl2Detail> list = new List<Model.PHTGL_AttachUrl2Detail>();
        //        JArray EditorTDCArr = Grid1.GetMergedData();
        //        if (EditorTDCArr.Count > 0)
        //        {
        //            Model.PHTGL_AttachUrl2Detail model = null;
        //            for (int i = 0; i < EditorTDCArr.Count; i++)
        //            {
        //                JObject objects = (JObject)EditorTDCArr[i];
        //                model = new Model.PHTGL_AttachUrl2Detail();
        //                model.AttachUrlDetaild = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl2Detail));
        //                model.AttachUrlItemId = hdAttachUrlItemId.Text.Trim();
        //                model.DetailType = "1";
        //                model.Specifications = objects["values"]["Specifications"].ToString();
        //                model.MachineTeamPrice = Funs.GetNewDecimal(objects["values"]["MachineTeamPrice"].ToString());
        //                model.Remark = objects["values"]["Remark"].ToString();
        //                BLL.AttachUrl2DetailService.AddAttachUrl2Detail(model);
        //            }
        //        }
        //        BindGrid(this.hdAttachUrlItemId.Text.Trim(), "1");

        //        //先删除再保存
        //        BLL.AttachUrl2DetailService.DeleteAttachUrl2DetailByItemId(this.hdAttachUrlItemId.Text, "2");
        //        List<Model.PHTGL_AttachUrl2Detail> list2 = new List<Model.PHTGL_AttachUrl2Detail>();
        //        JArray EditorTDCArr2 = Grid2.GetMergedData();
        //        if (EditorTDCArr2.Count > 0)
        //        {
        //            Model.PHTGL_AttachUrl2Detail model = null;
        //            for (int i = 0; i < EditorTDCArr2.Count; i++)
        //            {
        //                JObject objects = (JObject)EditorTDCArr2[i];
        //                model = new Model.PHTGL_AttachUrl2Detail();
        //                model.AttachUrlDetaild = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl2Detail));
        //                model.AttachUrlItemId = hdAttachUrlItemId.Text.Trim();
        //                model.DetailType = "2";
        //                model.Specifications = objects["values"]["Specifications"].ToString();
        //                model.MachineTeamPrice = Funs.GetNewDecimal(objects["values"]["MachineTeamPrice"].ToString());
        //                model.Remark = objects["values"]["Remark"].ToString();
        //                BLL.AttachUrl2DetailService.AddAttachUrl2Detail(model);
        //            }
        //        }
        //        BindGrid(this.hdAttachUrlItemId.Text.Trim(), "2");

        //        ShowNotify("保存成功！", MessageBoxIcon.Success);
        //        PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
        //    }
        //}
        //protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string  a =string .Join (",", CheckBoxList1.SelectedValueArray);
        //    BindingPanal(a);
        //}
        //void BindingPanal(string a)
        //{
        //  var b=  a.Split(',');
        //    GroupPanel1.Hidden = true;
        //    GroupPanel2.Hidden = true;
        //    GroupPanel3.Hidden = true;
        //    GroupPanel4.Hidden = true;
        //    GroupPanel5.Hidden = true;
        //    GroupPanel6.Hidden = true;
        //    foreach (var item in b)
        //    {
        //        switch (item)
        //        {
        //            case  "1":
        //                GroupPanel1.Hidden = false;
        //                GroupPanel1.Expanded = true;
        //                break;
        //            case "2":
        //                GroupPanel2.Hidden = false;
        //                GroupPanel2.Expanded = true;
        //                break;
        //            case "3":
        //                GroupPanel3.Hidden = false;
        //                GroupPanel3.Expanded = true;
        //                break;
        //            case "4":
        //                GroupPanel4.Hidden = false;
        //                GroupPanel4.Expanded = true;
        //                break;
        //            case "5":
        //                GroupPanel5.Hidden = false;
        //                GroupPanel5.Expanded = true;
        //                break;
        //            case "6":
        //                GroupPanel6.Hidden = false;
        //                GroupPanel6.Expanded = true;
        //                break;
        //        }
                
        //    }
        //}
        #endregion
    }
}