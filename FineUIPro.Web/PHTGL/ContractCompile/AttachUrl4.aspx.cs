using BLL;
using Newtonsoft.Json;
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
        public string AttachUrlId
        {
            get
            {
                return (string)ViewState["AttachUrlId"];
            }
            set
            {
                ViewState["AttachUrlId"] = value;
            }
        }
        public DataTable Table
        {
            get
            {
                return (DataTable)ViewState["Table"];
            }
            set
            {
                ViewState["Table"] = value;
            }
        }
        public List<Model.PHTGL_AttachUrl4> url4_model
        {
            get
            {
                return (List<Model.PHTGL_AttachUrl4>)Session["url4_model"];
            }
            set
            {
                Session["url4_model"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                AttachUrlId = Request.Params["AttachUrlId"];
                url4_model = new List<Model.PHTGL_AttachUrl4>();
                var att = BLL.AttachUrl4Service.GetListAttachurl4ById(AttachUrlId);
                if (att.Count ==0)
                {
                    DataGridAttachUrl(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 4).AttachUrlId);
                    url4_model = BLL.AttachUrl4Service.GetListAttachurl4ById(BLL.AttachUrlService.GetAttachUrlByAttachUrlCode("", 4).AttachUrlId);

                }
                else
                {
                    DataGridAttachUrl(AttachUrlId);
                    url4_model = att;

                }

            //    
                btnClose.OnClientClick= ActiveWindow.GetHideReference();

            }
            
        }
        #region 附件
        /// <summary>
        /// Grid绑定
        /// </summary>
        private void DataGridAttachUrl(string AttachUrlId)
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
                           + @"  where 1=1	 and url4.AttachUrlId =@AttachUrlId order by url4.OrderNumber";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@AttachUrlId", AttachUrlId));

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
             Table = tb;
            Grid1.DataSource = tb;
            Grid1.DataBind();
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
            newurl4.Duty_Gen = false;
            newurl4.Duty_Sub = false;
            url4_model.Add(newurl4);
            DataTable table = this.LINQToDataTable(url4_model);

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
            BLL.AttachUrl4Service.deletePHTGL_AttachUrl14byAttachUrlId(AttachUrlId);
            for (int i = 0; i < Table.Rows.Count; i++)
            {

                url4.AttachUrlId = AttachUrlId;
                url4.AttachUrlItemId = SQLHelper.GetNewID(typeof(Model.PHTGL_AttachUrl4));
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