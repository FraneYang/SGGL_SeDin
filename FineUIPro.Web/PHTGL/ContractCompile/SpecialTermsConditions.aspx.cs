using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.PHTGL.ContractCompile
{
    public partial class SpecialTermsConditions : PageBase
    {
        public Dictionary<string, string> myDictionary = new Dictionary<string, string>();
        public string SpecialTermsConditionsId = "专用条款模板";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                RederDatabase(SimpleForm1); //从数据库读取数据填充
                DataGridAttachUrl();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // SpecialTermsConditionsId = Guid.NewGuid().ToString ();
   
            Save();

        }

        void Save()
        {
            myDictionary.Clear();
            myDictionary.Add("SpecialTermsConditionsId", SpecialTermsConditionsId);
            myDictionary.Add("ContractId", "模板");


            SaveTextEmpty(SimpleForm1);  //得到键值对

           DataTable table= GetDataTable(myDictionary);//键值对转DATatable;

            List<PHTGL_SpecialTermsConditions> List_pHTGL_SpecialTermsConditions = TableToEntity<PHTGL_SpecialTermsConditions>(table);

            Model.PHTGL_SpecialTermsConditions pHTGL_SpecialTermsConditions = List_pHTGL_SpecialTermsConditions[0];
            Model.PHTGL_SpecialTermsConditions  model= BLL.PHTGL_SpecialTermsConditionsService.GetSpecialTermsConditionsById(SpecialTermsConditionsId);
            if (model !=null)
            {
                BLL.PHTGL_SpecialTermsConditionsService.UpdateSpecialTermsConditions(pHTGL_SpecialTermsConditions);
            }
            else
            {
                BLL.PHTGL_SpecialTermsConditionsService.AddSpecialTermsConditions(pHTGL_SpecialTermsConditions);
            }
    }

    /// <summary>
    ///  根据主键获取要查询字段的值
    /// </summary>
    /// <param name="SpecialTermsConditionsId">主键</param>
    /// <param name="field"></param>
    /// <returns></returns>
        public string  getvalue(string SpecialTermsConditionsId, string field)
        {
            string values = "";
             
            string sql = "select " + field + " from PHTGL_SpecialTermsConditions where SpecialTermsConditionsId='" + SpecialTermsConditionsId + "'";

            DataTable tb = SQLHelper.RunSqlGetTable(sql);
            if (tb!=null &&tb.Rows.Count >0)
            {
                values= tb.Rows[0][field].ToString();

            }
            return values;
        }

        /// <summary>
        /// 集合转DataTable
        /// </summary>
        /// <param name="Dictionary"></param>
        /// <returns></returns>
        private DataTable GetDataTable(Dictionary<string, string> Dictionary)
        {
            
            DataTable dt = new DataTable();
            foreach (KeyValuePair<string, string> kvp in Dictionary)
            {
                //dt.Rows.Add(kvp.Key, kvp.Value);
                DataColumn dc = new DataColumn(kvp.Key.ToString());
                dt.Columns.Add(dc);
                
            }
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            foreach (KeyValuePair<string, string> kvp in Dictionary)
            {
                dt.Rows[0][kvp.Key.ToString()] = kvp.Value;
            }

            return dt;
        }

        /// <summary>
        /// data转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<T> TableToEntity<T>(DataTable dt) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {
                    if (!row.Table.Columns.Contains(p.Name))
                    {
                        continue;

                    }

                    if (row[p.Name].ToString () == "" )
                    {
                        p.SetValue(entity, null, null);
                        continue;
                    }
                    switch (Gettype(p.PropertyType.FullName))
                    {
                        case "String":
                            p.SetValue(entity, row[p.Name].ToString (), null);
                            break;
                        case "Int32":
                            p.SetValue(entity,Int32.Parse( row[p.Name].ToString ()), null);
                            break;
                        case "Decimal":
                            p.SetValue(entity, Decimal.Parse( row[p.Name].ToString()), null);
                            break;
                        case "DateTime":
                            p.SetValue(entity,DateTime.Parse(row[p.Name].ToString()), null);
                            break;
                        default:
                            p.SetValue(entity, row[p.Name], null);
                            break;
                    }

                    
                }
                list.Add(entity);
            }
            return list;
        }

         public  static  string Gettype(string name)
        {
            if (name.Contains ("System.String"))
            {
                return "String";
            }
            if (name .Contains("System.Int32"))
            {
                return "Int32";
            }
            if (name.Contains("System.Decimal"))
            {
                return "Decimal";
            }
            if (name.Contains("System.DateTime"))
            {
                return "DateTime";
            }
            return "";
        }

        private void SaveTextEmpty(Control c)
        {
            //遍历控件
            //myDictionary.Clear();
            foreach (Control childControl in c.Controls)
           {
                if (childControl is TextBox)
                {
                    TextBox tb = (TextBox)childControl;
                    if (!tb.ID.StartsWith("TextBox"))
                    {
                        myDictionary.Add(tb.ID, tb.Text.ToString());
                    }
                       
                  //  tb.Text = "";
                    if (tb.Text.Length > 7)
                    {
                        tb.Width = tb.Text.Length * 16;
                    }
                }
                else if(childControl is TextArea)
                {
                    TextArea textArea = (TextArea)childControl;
                    if (!textArea.ID.StartsWith("TextArea"))
                    {
                        myDictionary.Add(textArea.ID, textArea.Text.ToString());
                    }
                       
                }
                else
                {
                    SaveTextEmpty(childControl);
                }
            }
        }

        private void RederDatabase(Control c)
        {
            //遍历控件给控件赋值
            myDictionary.Clear();
            foreach (Control childControl in c.Controls)
            {
                if (childControl is TextBox)
                {
                    TextBox tb = (TextBox)childControl;
                    if (!tb.ID.StartsWith("TextBox"))
                    {
                        tb.Text = getvalue(SpecialTermsConditionsId, tb.ID);
                    }
                    if (tb.Text.Length > 7)
                    {
                        tb.Width = tb.Text.Length * 16;
                    }
                }
                else if (childControl is TextArea)
                {
                    TextArea textArea = (TextArea)childControl;
                    if (!textArea.ID.StartsWith("TextArea"))
                    {
                        textArea.Text = getvalue(SpecialTermsConditionsId, textArea.ID);
                    }
                }
                else
                {
                    RederDatabase(childControl);
                }
            }
        }

        protected void TextBoxChanged(object sender, EventArgs e)
        {
             
            TextBox textBox = (TextBox)sender;
            if (textBox .Text.Length >7)
            {
                textBox.Width = textBox.Text.Length*16;

            }

              Save();


        }

        #region 附件
        /// <summary>
        /// Grid绑定
        /// </summary>
        private void DataGridAttachUrl()
        {
            string strSql = @"SELECT Att.AttachUrlId, 
                                    Att.AttachUrlCode, 
                                    Att.AttachUrlName, 
                                    Att.IsBuild,
                                    Att.IsSelected, 
                                    Att.SortIndex"
                            + @" FROM PHTGL_AttachUrl AS Att"
                            + @" WHERE 1=1 and Att.SpecialTermsConditionsId='专用条款模板' ORDER BY Att.SortIndex ";
            List<SqlParameter> listStr = new List<SqlParameter>();

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            //Grid1.RecordCount = tb.Rows.Count;
            //var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }

        /// <summary>
        /// grid行绑定前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
            CheckBoxField cbIsSelected = Grid1.FindColumn("cbIsSelected") as CheckBoxField;
            bool isSelected = Convert.ToBoolean(row["IsBuild"]);
            if (isSelected == true)
            {
                cbIsSelected.Enabled = false;
            }
            else
            {
                cbIsSelected.Enabled = true;
            }
        }

        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                CheckBoxField cbIsSelected = Grid1.FindColumn("cbIsSelected") as CheckBoxField;
                if (cbIsSelected.GetCheckedState(e.RowIndex))
                {
                    string id = Grid1.SelectedRowID;
                    var att = BLL.AttachUrlService.GetAttachUrlById(id);
                    if (att != null)
                    {
                        att.IsSelected = true;
                        BLL.AttachUrlService.UpdateAttachUrl(att);
                    }
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AttachUrl{0}.aspx?AttachUrlId={1}", att.SortIndex, id, "编辑 - ")));
                }
                else
                {
                    Alert.ShowInTop("未选中的项！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion
    }
}