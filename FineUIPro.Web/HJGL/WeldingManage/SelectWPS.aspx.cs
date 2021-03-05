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
            if (!IsPostBack)
            {
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
            string weldingMethod = Request.Params["WeldingMethod"];
            string weldType = Request.Params["WeldType"];

            decimal dia = Funs.GetNewDecimal(diag).HasValue ? Funs.GetNewDecimal(diag).Value : 0;
            decimal sch = Funs.GetNewDecimal(diag).HasValue ? Funs.GetNewDecimal(thickness).Value : 0;

            var mat1 = BLL.Base_MaterialService.GetMaterialByMaterialId(material1);
            var mat2 = BLL.Base_MaterialService.GetMaterialByMaterialId(material2);

            int m1 = SNClass(mat1.MaterialClass);
            int m2 = SNClass(mat2.MaterialClass);

            int g1 = SNGroup(mat1.MaterialGroup);
            int g2 = SNGroup(mat2.MaterialGroup);

            string preGroup1 = PreGroup(mat1.MaterialGroup);
            string preGroup2 = PreGroup(mat2.MaterialGroup);
            List<Model.View_HJGL_WPQ> list = new List<Model.View_HJGL_WPQ>();
            // 单位、接头形式、材质的覆盖
            var wpq = from x in Funs.DB.View_HJGL_WPQ
                      where x.UnitId == unitId && x.State == BLL.Const.State_2
                      //&& (x.JointType == "对接焊缝" || (x.JointType != "对接焊缝" && weldType != "B"))
                      //&& ((x.Material1Group == mat1.MaterialGroup && x.Material2Group == mat2.MaterialGroup)
                      //|| (x.Material1Group == mat2.MaterialGroup && x.Material2Group == mat1.MaterialGroup))
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

            // 一、材质类别相等
            var wpq1 = from x in wpq where x.Material1Class == x.Material2Class select x;
            foreach (var item in wpq1)
            {
                // 满足WPS第一个条件：焊接方法为“PAW、SMAW、SAW、GMAW、FCAW、GTAW”且材质类别属于Fe-1~Fe-5A
                if ((item.WeldingMethodCode == "PAW" || item.WeldingMethodCode == "SMAW" || item.WeldingMethodCode == "SAW" || item.WeldingMethodCode == "GMAW" || item.WeldingMethodCode == "FCAW" || item.WeldingMethodCode == "GTAW")
                             && (item.SNClass1 == "1" || item.SNClass1 == "2" || item.SNClass1 == "3" || item.SNClass1 == "4" || item.SNClass1 == "5A"))
                {
                    // 满足:焊口材质1”等于且“焊口材质2”小于WPS材质类别
                    if (item.Material1Class == mat1.MaterialClass && Convert.ToInt32(item.SNClass2) > m2)
                    {
                        list.Add(item);
                    }
                    // 满足:焊口“材质2”等于且“焊口材质1”小于WPS材质类别
                    else if (item.Material2Class == mat2.MaterialClass && Convert.ToInt32(item.SNClass1) > m1)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        // WPS“材质1”与“材质2”“组别”相等
                        if (item.Material1Group == item.Material2Group)
                        {
                            //≠“Fe-1-2”
                            if (item.Material1Group != "Fe-1-2")
                            {
                                //焊口“材质1”与“材质2”组别关系
                                //相等且等于WPS材质组别
                                if (mat1.MaterialGroup == mat2.MaterialGroup && mat1.MaterialGroup == item.Material1Group)
                                {
                                    list.Add(item);
                                }
                                //“材质1”等于且“材质2”小于WPS材质组别
                                else if (mat1.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup2 && Convert.ToInt32(item.SNGroup1) > g2)
                                {
                                    list.Add(item);
                                }
                                //“材质2”等于且“材质1”小于WPS材质组别
                                else if (mat2.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup1 && Convert.ToInt32(item.SNGroup1) > g1)
                                {
                                    list.Add(item);
                                }
                            }
                            //＝“Fe-1-2”
                            else
                            {
                                //=“Fe-1-2”
                                if (mat1.MaterialGroup == "Fe-1-2" && mat2.MaterialGroup == "Fe-1-2")
                                {
                                    list.Add(item);
                                }
                                //=“Fe-1-1”
                                else if (mat1.MaterialGroup == "Fe-1-1" && mat2.MaterialGroup == "Fe-1-1")
                                {
                                    list.Add(item);
                                }
                                //“材质1”等于且“材质2”小于WPS材质组别
                                else if (mat1.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup2 && Convert.ToInt32(item.SNGroup1) > g2)
                                {
                                    list.Add(item);
                                }
                                //“材质2”等于且“材质1”小于WPS材质组别
                                else if (mat2.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup1 && Convert.ToInt32(item.SNGroup1) > g1)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                        //WPS“材质1”与“材质2”“组别”不相等
                        else
                        {
                            //"与WPS一致（材1 = WPS材1，且材2 = WPS材2或材2 = WPS材1，且材1 = WPS材2）"
                            if ((mat1.MaterialGroup == item.Material1Group && mat2.MaterialGroup == item.Material2Group) || (mat2.MaterialGroup == item.Material1Group && mat1.MaterialGroup == item.Material2Group))
                            {
                                list.Add(item);
                            }
                        }
                    }
                }
                //其他
                else
                {
                    // WPS“材质1”与“材质2”“组别”相等
                    if (item.Material1Group == item.Material2Group)
                    {
                        //≠“Fe-1-2”
                        if (item.Material1Group != "Fe-1-2")
                        {
                            //焊口“材质1”与“材质2”组别关系
                            //相等且等于WPS材质组别
                            if (mat1.MaterialGroup == mat2.MaterialGroup && mat1.MaterialGroup == item.Material1Group)
                            {
                                list.Add(item);
                            }
                            //“材质1”等于且“材质2”小于WPS材质组别
                            else if (mat1.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup2 && Convert.ToInt32(item.SNGroup1) > g2)
                            {
                                list.Add(item);
                            }
                            //“材质2”等于且“材质1”小于WPS材质组别
                            else if (mat2.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup1 && Convert.ToInt32(item.SNGroup1) > g1)
                            {
                                list.Add(item);
                            }
                        }
                        //＝“Fe-1-2”
                        else
                        {
                            //=“Fe-1-2”
                            if (mat1.MaterialGroup == "Fe-1-2" && mat2.MaterialGroup == "Fe-1-2")
                            {
                                list.Add(item);
                            }
                            //=“Fe-1-1”
                            else if (mat1.MaterialGroup == "Fe-1-1" && mat2.MaterialGroup == "Fe-1-1")
                            {
                                list.Add(item);
                            }
                            //“材质1”等于且“材质2”小于WPS材质组别
                            else if (mat1.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup2 && Convert.ToInt32(item.SNGroup1) > g2)
                            {
                                list.Add(item);
                            }
                            //“材质2”等于且“材质1”小于WPS材质组别
                            else if (mat2.MaterialGroup == item.Material1Group && item.PreGroup1 == preGroup1 && Convert.ToInt32(item.SNGroup1) > g1)
                            {
                                list.Add(item);
                            }
                        }
                    }
                    //WPS“材质1”与“材质2”“组别”不相等
                    else
                    {
                        //"与WPS一致（材1 = WPS材1，且材2 = WPS材2或材2 = WPS材1，且材1 = WPS材2）"
                        if ((mat1.MaterialGroup == item.Material1Group && mat2.MaterialGroup == item.Material2Group) || (mat2.MaterialGroup == item.Material1Group && mat1.MaterialGroup == item.Material2Group))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            // 一、材质类别不相等
            var wpq2 = from x in wpq where x.Material1Class != x.Material2Class select x;
            foreach (var item in wpq2)
            {
                //与WPS一致（材1 = WPS材1，且材2 = WPS材2或材2 = WPS材1，且材1 = WPS材2）
                if ((mat1.MaterialClass == item.Material1Class && mat2.MaterialClass == item.Material2Class) || (mat2.MaterialClass == item.Material1Class && mat1.MaterialClass == item.Material2Class))
                {
                    list.Add(item);
                }
            }
            list = list.Distinct().ToList();
            Grid1.DataSource = list;
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
                string mat = matClass.Substring(m + 1, 1);
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
                string mat = matGroup.Substring(matGroup.Length - 1, 1);
                sn = Funs.GetNewInt(mat).HasValue ? Funs.GetNewInt(mat).Value : 0;
            }
            return sn;
        }

        private string PreGroup(string matGroup)
        {
            string pre = string.Empty;
            if (!string.IsNullOrEmpty(matGroup))
            {
                pre = matGroup.Substring(0, matGroup.Length - 2);
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
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wpq.WPQId, wpq.WPQCode,wpq.WeldingRod,wpq.WeldingWire,wpq.WeldingMethodId,wpq.GrooveType,wpq.PreTemperature,wpq.MaterialId1,wpq.MaterialId2,wpq.IsHotProess.Value.ToString()) + ActiveWindow.GetHidePostBackReference());
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wpq.WPQId, wpq.WPQCode, wpq.WeldingRod, wpq.WeldingWire, wpq.WeldingMethodId, wpq.WeldingMethodId, wpq.GrooveType, wpq.GrooveType, wpq.PreTemperature, wpq.IsHotProess.Value.ToString()) + ActiveWindow.GetHidePostBackReference());
        }
    }
}