using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.HJGL.WeldingManage
{
    public partial class SelectWPS : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                BindGrid();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string Material1 = Request.Params["Material1"];
            string Material2 = Request.Params["Material2"];
            string Dia = Request.Params["Dia"];
            string Thickness = Request.Params["Thickness"];
            string UnitId = Request.Params["UnitId"];
            string strSql = @"SELECT wpq.WPQId,wpq.WPQCode, c.ConsumablesName WeldingRod,c1.ConsumablesName WeldingWire, g.GrooveTypeName,wpq.MinImpactDia,wpq.MaxImpactDia, wpq.MinImpactThickness,wpq.MaxImpactThickness,(CASE wpq.IsHotProess WHEN 1 THEN '是' ELSE '否' END) AS IsHotProess, wmt.WeldingMethodCode,mat1.MaterialCode as MaterialCode1,mat2.MaterialCode as MaterialCode2 FROM WPQ_WPQList AS wpq
                                   LEFT JOIN Base_Material AS mat1 ON mat1.MaterialId = wpq.MaterialId1
                                   LEFT JOIN Base_Material AS mat2 ON mat2.MaterialId = wpq.MaterialId2
                                   LEFT JOIN Base_WeldingMethod AS wmt ON wmt.WeldingMethodId = wpq.WeldingMethodId
                                   LEFT JOIN dbo.Base_Consumables c ON c.ConsumablesId=wpq.WeldingRod 
								   LEFT JOIN dbo.Base_Consumables c1 ON c1.ConsumablesId=wpq.WeldingWire 
                                   LEFT JOIN dbo.Base_GrooveType g ON g.GrooveTypeId=wpq.GrooveType
                             WHERE 1 = 1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND  wpq.UnitId =@UnitId";
            listStr.Add(new SqlParameter("@UnitId", UnitId));
            if (Material1!=BLL.Const._Null)
            {
                strSql += " AND  wpq.MaterialId1 LIKE @MaterialId1";
                listStr.Add(new SqlParameter("@MaterialId1", "%" + Material1 + "%"));
            }
            if (Material2 != BLL.Const._Null)
            {
                strSql += " AND  wpq.MaterialId2 LIKE @MaterialId2";
                listStr.Add(new SqlParameter("@MaterialId2", "%" + Material2 + "%"));
            }
            if (!string.IsNullOrEmpty(Dia))
            {
                strSql += " AND  wpq.MinImpactDia <= @MinImpactDia and wpq.MaxImpactDia >=@MinImpactDia";
                listStr.Add(new SqlParameter("@MinImpactDia", "" + Dia + ""));
            }
            if (!string.IsNullOrEmpty(Thickness))
            {
                strSql += " AND  wpq.MinImpactThickness <= @Thickness and wpq.MaxImpactThickness >=@Thickness";
                listStr.Add(new SqlParameter("@Thickness", "" + Thickness + ""));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一行", MessageBoxIcon.Warning);
                return;
            }
            string wpqId = this.Grid1.SelectedRowID;
            Model.WPQ_WPQList wpq = BLL.WPQListServiceService.GetWPQById(wpqId);
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wpq.WPQId, wpq.WPQId, wpq.WPQCode,wpq.WeldingRod,wpq.WeldingWire,wpq.WeldingMethodId,wpq.GrooveType,wpq.PreTemperature,wpq.MaterialId1,wpq.MaterialId2) + ActiveWindow.GetHideReference());
        }
    }
}