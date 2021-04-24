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
    public partial class AttachUrl4 : PageBase
    {

        public static DataTable Table = new DataTable();
        public static string AttachUrlId = "";
        private static List<Model.PHTGL_AttachUrl4> url4_model= new List<Model.PHTGL_AttachUrl4>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                AttachUrlId = Request.Params["AttachUrlId"];
                url4_model.Clear();
                var att = BLL.AttachUrl4Service.GetAttachurl4ById(AttachUrlId);
                if (att == null)
                {
                    att = BLL.AttachUrl4Service.GetAttachurl4ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 4).AttachUrlId);
                }
 
                 url4_model.Add(att);
                 DataGridAttachUrl();
                btnClose.OnClientClick= ActiveWindow.GetHideReference();

            }
            
        }
        #region 附件
        /// <summary>
        /// Grid绑定
        /// </summary>
        private void DataGridAttachUrl()
        {
            string strSql = @"  select 
                                      url4.AttachUrlItemId
                                      ,url4.AttachUrlId
                                      ,url4.OrderNumber
                                      ,url4.Describe
                                      ,url4.Duty_Gen
                                      ,url4.Duty_Sub
                                      ,url4.Remarks"
                            + @" from PHTGL_AttachUrl4 as url4"
                            + @"  where 1=1	order by url4.OrderNumber";
            List<SqlParameter> listStr = new List<SqlParameter>();

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(Grid1, tb);
            Table = table;
            Grid1.DataSource = table;
            Grid1.DataBind();
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
        void save()
        {
            Grid1.GetModifiedDict();
            Grid1Save(Grid1);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // SpecialTermsConditionsId = Guid.NewGuid().ToString ();

            save();

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            Model.PHTGL_AttachUrl4 newurl4 = new Model.PHTGL_AttachUrl4();

            newurl4.AttachUrlItemId = Guid.NewGuid().ToString();
            newurl4.AttachUrlId = AttachUrlId;
            url4_model.Add(newurl4);

            var table = this.GetPagedDataTable(Grid1, url4_model);
            Table = table;
            Grid1.DataSource = table;
            Grid1.DataBind();

 
        }

        private void UpdateDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
        {
            if (rowDict.ContainsKey(columnName))
            {
                rowData[columnName] = rowDict[columnName];
            }
        }

        private void UpdateDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            // 姓名
            UpdateDataRow("OrderNumber", rowDict, rowData);

            // 性别
            UpdateDataRow("Describe", rowDict, rowData);
            UpdateDataRow("Duty_Gen", rowDict, rowData);
            UpdateDataRow("Duty_Sub", rowDict, rowData);
            UpdateDataRow("Remarks", rowDict, rowData);

        }
        private DataRow FindRowByID(string rowID)
        {
            DataTable table = Table;
            foreach (DataRow row in table.Rows)
            {
                if (row["AttachUrlItemId"].ToString() == rowID)
                {
                    return row;
                }
            }
            return null;
        }
        private void Grid1Save(Grid grid)
        {
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            if (modifiedDict != null)
            {
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    DataRow row = FindRowByID(rowID);
                    UpdateDataRow(modifiedDict[rowIndex], row);
                }
            }
            Model.PHTGL_AttachUrl4  url4 = new Model.PHTGL_AttachUrl4();
            for (int i = 0; i < Table.Rows.Count; i++)
            {

                url4.AttachUrlId = Table.Rows[i]["AttachUrlId"].ToString(); 
                url4.AttachUrlItemId = Table.Rows[i]["AttachUrlItemId"].ToString(); 
                url4.Describe = Table.Rows[i]["Describe"].ToString(); 
                url4.Duty_Gen = Convert.ToBoolean( Table.Rows[i]["Duty_Gen"].ToString()); 
                url4.Duty_Sub = Convert.ToBoolean(Table.Rows[i]["Duty_Sub"].ToString()); 
                url4.Remarks = Table.Rows[i]["Remarks"].ToString(); 
                url4.OrderNumber = Table.Rows[i]["OrderNumber"].ToString(); 
                 

                Model.PHTGL_AttachUrl4 isExit_url4 = BLL.AttachUrl4Service.GetAttachurl4ByItemId(url4.AttachUrlItemId);

                if (isExit_url4 == null)
                {
                    BLL.AttachUrl4Service.AddAttachurl4(url4);

                }
                else
                {
                    BLL.AttachUrl4Service.UpdateAttachurl4(url4);
                }
            }


        }

        #endregion
    }

}