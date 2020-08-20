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
            string material1 = Request.Params["Material1"];
            string material2 = Request.Params["Material2"];
            string diag = Request.Params["Dia"];
            string thickness = Request.Params["Thickness"];
            string unitId = Request.Params["UnitId"];
            string weldingMethod= Request.Params["WeldingMethod"];
            string weldType= Request.Params["WeldType"];

            decimal dia = Funs.GetNewDecimal(diag).HasValue ? Funs.GetNewDecimal(diag).Value : 0;
            decimal sch = Funs.GetNewDecimal(diag).HasValue ? Funs.GetNewDecimal(thickness).Value : 0;

            var mat1 = BLL.Base_MaterialService.GetMaterialByMaterialId(material1);
            var mat2 = BLL.Base_MaterialService.GetMaterialByMaterialId(material2);

            int m1 = SNClass(mat1.MaterialClass);
            int m2 = SNClass(mat2.MaterialClass);

            int g1= SNGroup(mat1.MaterialGroup);
            int g2 = SNGroup(mat2.MaterialGroup);

            string preGroup1 = PreGroup(mat1.MaterialGroup);
            string preGroup2 = PreGroup(mat2.MaterialGroup);

            // 单位、接头形式、材质的覆盖
            var wpq = from x in Funs.DB.View_HJGL_WPQ
                      where x.UnitId == unitId
                      && (x.JointType == "对接焊缝" || (x.JointType != "对接焊缝" && weldType != "B"))
                            && ((x.MaterialId1 == material1 && x.MaterialId2 == material2)
                            || (x.MaterialId1 == material2 || x.MaterialId2 == material1))
                      select x;

            // 根据接头形式判断外径和壁厚的覆盖
            if (weldType == "B")
            {
                wpq = from x in wpq
                      where dia >= x.MinImpactDia && dia <= x.MaxImpactDia
                       && sch >= x.MinImpactThickness && sch <= x.MaxImpactThickness
                      select x;
            }
            else
            {
                wpq = from x in wpq
                      where dia >= x.MinCImpactDia && dia <= x.MaxCImpactDia
                       && sch >= x.NoMinImpactThickness && sch <= x.NoMaxImpactThickness
                      select x;
            }

            // 材质类别相等
            var wpq1 = from x in wpq where x.Material1Class == x.Material2Class select x;

            // 材质类别不相等
            var wpq2 = from x in wpq where x.Material1Class != x.Material2Class select x;

            // 满足WPS第一个条件：焊接方法为“PAW、SMAW、SAW、GMAW、FCAW、GTAW”且材质类别属于Fe-1~Fe-5A
            var wpq3 = from x in wpq1
                       where (x.WeldingMethodCode == "PAW" || x.WeldingMethodCode == "SMAW" || x.WeldingMethodCode == "SAW" || x.WeldingMethodCode == "GMAW" || x.WeldingMethodCode == "FCAW" || x.WeldingMethodCode == "GTAW")
                             && (x.SNClass1 == "1" || x.SNClass1 == "2" || x.SNClass1 == "3" || x.SNClass1 == "4" || x.SNClass1 == "5"+"A")
                       select x;

            // 满足:焊口材质1”等于且“焊口材质2”小于WPS材质类别 或“材质2”等于且“材质1”小于WPS材质类别
            var wpq4 = from x in wpq3
                       where (x.Material1Class == mat1.MaterialClass && Convert.ToInt32(x.SNClass1) >= m2)
                             || (x.Material1Class == mat2.MaterialClass && Convert.ToInt32(x.SNClass1) >= m1)
                       select x;

            // 满足: 与WPS类别一致
            var wpq5 = from x in wpq2
                   where ((x.Material1Class == mat1.MaterialClass && x.Material2Class == mat2.MaterialClass)
                        || (x.Material1Class == mat2.MaterialClass && x.Material2Class == mat1.MaterialClass))
                   select x;

            // 以下是组别
            // 组别相等
            var wpq6 = from x in wpq1 where x.Material1Group == x.Material2Group select x;

            // 满足:焊口材质1”等于且“焊口材质2”小于WPS材质组别 或“材质2”等于且“材质1”小于WPS材质组别
            var wpq7 = from x in wpq6
                       where (x.Material1Group == mat1.MaterialGroup && x.PreGroup1== preGroup2 && Convert.ToInt32(x.SNGroup1) >= g2)
                             || (x.Material1Class == mat2.MaterialClass && x.PreGroup1 == preGroup1 && Convert.ToInt32(x.SNClass1) >= g1)
                       select x;

            // 满足: 当WPS组别等于Fe-1-2时
            var wpq8 = from x in wpq6
                       where x.Material1Group == "Fe-1-2" 
                             && (mat1.MaterialGroup== "Fe-1-2" || mat1.MaterialGroup== "Fe-1-1")
                             && (mat2.MaterialGroup == "Fe-1-2" || mat2.MaterialGroup == "Fe-1-1")
                       select x;

            // 满足: WPS材质1与材质2级别不相等时，焊口组别与WPS组别一致
            var wpq9 = from x in wpq1
                       where x.Material1Group != x.Material2Group
                       && ((x.Material1Group == mat1.MaterialGroup && x.Material2Group == mat2.MaterialGroup)
                        || (x.Material1Group == mat2.MaterialGroup && x.Material1Group == mat1.MaterialGroup))
                       select x;

            // 满足条件的集合连接后去除重复
            var q = wpq4.Concat(wpq5).Concat(wpq7).Concat(wpq8).Concat(wpq9).Distinct();
            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        /// <summary>
        /// 返回组别的序号
        /// </summary>
        /// <param name="matClass"></param>
        /// <returns></returns>
        private int SNClass(string matClass)
        {
            int sn = 0;
            if (!string.IsNullOrEmpty(matClass))
            {
                int m = matClass.IndexOf("-");
                string mat = matClass.Substring(m+1, 1);
                sn = Funs.GetNewInt(mat).HasValue ? Funs.GetNewInt(mat).Value : 0;
            }
            return sn;
        }

        // 
        private int SNGroup(string matGroup)
        {
            int sn = 0;
            if (!string.IsNullOrEmpty(matGroup))
            {
                string mat = matGroup.Substring(matGroup.Length-1, 1);
                sn = Funs.GetNewInt(mat).HasValue ? Funs.GetNewInt(mat).Value : 0;
            }
            return sn;
        }

        private string PreGroup(string matGroup)
        {
            string pre = string.Empty;
            if (!string.IsNullOrEmpty(matGroup))
            {
                pre = matGroup.Substring(0,matGroup.Length-2);
            }
            return pre;
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
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wpq.WPQId, wpq.WPQCode,wpq.WeldingRod,wpq.WeldingWire,wpq.WeldingMethodId,wpq.GrooveType,wpq.PreTemperature,wpq.MaterialId1,wpq.MaterialId2) + ActiveWindow.GetHideReference());
        }
    }
}