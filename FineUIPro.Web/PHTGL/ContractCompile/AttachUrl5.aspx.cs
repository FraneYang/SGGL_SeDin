using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class AttachUrl5 : PageBase
    {
        public static DataTable Grid1Table = new DataTable();
        public static DataTable Grid2Table = new DataTable();
        private static List<Model.PHTGL_AttachUrl5_DevicePrice> url5_dev = new List<Model.PHTGL_AttachUrl5_DevicePrice>();
        private static List<Model.PHTGL_AttachUrl5_MaterialsPrice> url5_mat = new List<Model.PHTGL_AttachUrl5_MaterialsPrice>();
        public static string AttachUrlId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AttachUrlId = Request.Params["AttachUrlId"];
                url5_dev.Clear();
                url5_mat.Clear();
                var att = BLL.AttachUrl5_DevicePriceService.GetAttachurl5ById(AttachUrlId);
                var att2 = BLL.AttachUrl5_MaterialsPriceService.GetAttachurl5ById(AttachUrlId);
                if (att == null)
                {
                    att = BLL.AttachUrl5_DevicePriceService.GetAttachurl5ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 5).AttachUrlId);
                }
                if (att2 == null)
                {
                    att2 = BLL.AttachUrl5_MaterialsPriceService.GetAttachurl5ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 5).AttachUrlId);
                }
                url5_dev.Add(att);
                url5_mat.Add(att2);



                DataGridAttachUrl();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

            }

        }
        #region 附件
        /// <summary>
        /// Grid绑定
        /// </summary>
        private void DataGridAttachUrl()
        {
            Grid1Binding();
            Grid2Binding();
        }

        void Grid1Binding()
        {
            string strSql = @" select  mat.AttachUrlItemId,
                                      mat.AttachUrlId,
                                      mat.OrderNumber,
                                      mat.Name,
                                      mat.Spec,
                                      mat.Material,
                                      mat.Company,
                                      mat.UnitPrice,
                                      mat.Remarks "
                             + @" from  PHTGL_AttachUrl5_MaterialsPrice as mat"
                             + @"   where 1=1  order by mat.OrderNumber";
            List<SqlParameter> listStr = new List<SqlParameter>();

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1Table = table;
            Grid1.DataSource = table;
            Grid1.DataBind();

        }
        void Grid2Binding()
        {
            string strSql = @" SELECT  dev.AttachUrlItemId
                                      ,dev.AttachUrlId
                                      ,dev.OrderNumber
                                      ,dev.Name
                                      ,dev.Company
                                      ,dev.amount
                                      ,dev.UnitPrice
                                      ,dev.Totalprice
                                      ,dev.Remarks "
                           + @"   FROM  PHTGL_AttachUrl5_DevicePrice as dev"
                           + @"   where 1=1 order by dev.OrderNumber ";
            List<SqlParameter> listStr = new List<SqlParameter>();

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid2.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2Table = table;
            Grid2.DataSource = table;
            Grid2.DataBind();

        }

        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            
            Model.PHTGL_AttachUrl5_MaterialsPrice url5 = new Model.PHTGL_AttachUrl5_MaterialsPrice();

            url5.AttachUrlItemId = Guid.NewGuid().ToString ();
            url5.AttachUrlId = AttachUrlId;
            url5_mat.Add(url5);

            var table = this.GetPagedDataTable(Grid1, url5_mat);
            Grid1Table = table;
            Grid1.DataSource = table;
            Grid1.DataBind();


        }
        protected void btnAddDevice_Click(object sender, EventArgs e)
        {

            Model.PHTGL_AttachUrl5_DevicePrice url5 = new Model.PHTGL_AttachUrl5_DevicePrice();
            url5.AttachUrlItemId = Guid.NewGuid().ToString();
            url5.AttachUrlId = AttachUrlId;

            url5_dev.Add(url5);
            var table = this.GetPagedDataTable(Grid2, url5_dev);
            Grid2Table = table;
            Grid2.DataSource = table;
            Grid2.DataBind();


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Grid1.GetModifiedDict();
            Grid1Save_material(Grid1);

            Grid2.GetModifiedDict();
            Grid2Save__device(Grid2);
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }



        private void UpdateDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
        {
            if (rowDict.ContainsKey(columnName))
            {
                rowData[columnName] = rowDict[columnName];
            }
        }

        private void UpdateDataRow_device(Dictionary<string, object> rowDict, DataRow rowData)
        {
             UpdateDataRow("OrderNumber", rowDict, rowData);
             UpdateDataRow("Name", rowDict, rowData);
             UpdateDataRow("Company", rowDict, rowData);
             UpdateDataRow("amount", rowDict, rowData);
             UpdateDataRow("UnitPrice", rowDict, rowData);
            UpdateDataRow("Totalprice", rowDict, rowData);
            UpdateDataRow("Remarks", rowDict, rowData);

        }

        private void UpdateDataRow_material(Dictionary<string, object> rowDict, DataRow rowData)
        {
            UpdateDataRow("OrderNumber", rowDict, rowData);
            UpdateDataRow("Name", rowDict, rowData);
            UpdateDataRow("Spec", rowDict, rowData);
            UpdateDataRow("Material", rowDict, rowData);
            UpdateDataRow("Company", rowDict, rowData);
            UpdateDataRow("UnitPrice", rowDict, rowData);
            UpdateDataRow("Remarks", rowDict, rowData);

        }

        private DataRow FindRowByID(string rowID, DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["AttachUrlItemId"].ToString() == rowID)
                {
                    return row;
                }
            }
            return null;
        }

        private void Grid1Save_material(Grid grid)
        {
            
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            if (modifiedDict != null)
            {
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    DataRow row = FindRowByID(rowID,Grid1Table);
                    UpdateDataRow_material(modifiedDict[rowIndex], row);
                }
            }
            Model.PHTGL_AttachUrl5_MaterialsPrice url5 = new Model.PHTGL_AttachUrl5_MaterialsPrice();
            for (int i = 0; i < Grid1Table.Rows.Count; i++)
            {
                url5.AttachUrlItemId = Grid1Table.Rows[i]["AttachUrlItemId"].ToString();
                url5.AttachUrlId = Grid1Table.Rows[i]["AttachUrlId"].ToString();
                url5.OrderNumber = Grid1Table.Rows[i]["OrderNumber"].ToString();
                url5.Name = Grid1Table.Rows[i]["Name"].ToString();
                url5.Spec = Grid1Table.Rows[i]["Spec"].ToString();
                url5.Material = Grid1Table.Rows[i]["Material"].ToString();
                url5.Company = Grid1Table.Rows[i]["Company"].ToString();
                url5.UnitPrice = Convert.ToDecimal(Grid1Table.Rows[i]["UnitPrice"].ToString());
                url5.Remarks = Grid1Table.Rows[i]["Remarks"].ToString();
 

                Model.PHTGL_AttachUrl5_MaterialsPrice isExit_url4 = BLL.AttachUrl5_MaterialsPriceService.GetAttachurl5ById (url5.AttachUrlItemId);

                if (isExit_url4 == null)
                {
                    BLL.AttachUrl5_MaterialsPriceService.AddAttachurl5(url5);

                }
                else
                {
                    BLL.AttachUrl5_MaterialsPriceService.UpdateAttachurl5(url5);
                }
            }
  
        }

        private void Grid2Save__device(Grid grid)
        {

            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid2.GetModifiedDict();
            if (modifiedDict != null)
            {
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    string rowID = Grid2.DataKeys[rowIndex][0].ToString();
                    DataRow row = FindRowByID(rowID,Grid2Table);
                    UpdateDataRow_device(modifiedDict[rowIndex], row);
                }
            }
            Model.PHTGL_AttachUrl5_DevicePrice url5 = new Model.PHTGL_AttachUrl5_DevicePrice();
            for (int i = 0; i < Grid2Table.Rows.Count; i++)
            {

                url5.AttachUrlItemId = Grid2Table.Rows[i]["AttachUrlItemId"].ToString();
                url5.AttachUrlId = Grid2Table.Rows[i]["AttachUrlId"].ToString();
                url5.OrderNumber = Grid2Table.Rows[i]["OrderNumber"].ToString();
                url5.Name = Grid2Table.Rows[i]["Name"].ToString();
                url5.Company = Grid2Table.Rows[i]["Company"].ToString();
                url5.Amount = Convert.ToInt32(Grid2Table.Rows[i]["Amount"].ToString());
                url5.UnitPrice = Convert.ToDecimal(Grid2Table.Rows[i]["UnitPrice"].ToString());
                url5.Totalprice = Convert.ToDecimal(Grid2Table.Rows[i]["Totalprice"].ToString());
                url5.Remarks = Grid2Table.Rows[i]["Remarks"].ToString();


                Model.PHTGL_AttachUrl5_DevicePrice isExit_url5 = BLL.AttachUrl5_DevicePriceService.GetAttachurl5ById(url5.AttachUrlItemId);

                if (isExit_url5 == null)
                {
                    BLL.AttachUrl5_DevicePriceService.AddAttachurl5(url5);

                }
                else
                {
                    BLL.AttachUrl5_DevicePriceService.UpdateAttachurl5(url5);
                }
            }

        }




        /// <summary>
        /// grid行绑定前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            //DataRowView row = e.DataItem as DataRowView;
            //CheckBoxField cbIsSelected = Grid1.FindColumn("cbIsSelected") as CheckBoxField;
            //bool isSelected = Convert.ToBoolean(row["IsBuild"]);
            //if (isSelected == true)
            //{
            //    cbIsSelected.Enabled = false;
            //}
            //else
            //{
            //    cbIsSelected.Enabled = true;
            //}
        }

        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.CommandName == "edit")
            //{
            //    CheckBoxField cbIsSelected = Grid1.FindColumn("cbIsSelected") as CheckBoxField;
            //    if (cbIsSelected.GetCheckedState(e.RowIndex))
            //    {
            //        string id = Grid1.SelectedRowID;

            //    }
            //}

        }
        #endregion
    }
}