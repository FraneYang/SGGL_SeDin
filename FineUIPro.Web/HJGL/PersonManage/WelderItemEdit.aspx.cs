using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.PersonManage
{
    public partial class WelderItemEdit : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///机动化程度
                this.drpWeldingMode.DataTextField = "Text";
                this.drpWeldingMode.DataValueField = "Value";
                this.drpWeldingMode.DataSource = BLL.DropListService.HJGL_WeldingMode();
                this.drpWeldingMode.DataBind();
                this.hdWelderId.Text = Request.Params["PersonId"];
                string welderQualifyId = Request.Params["WelderQualifyId"];
                if (!string.IsNullOrEmpty(welderQualifyId))
                {
                    Model.Welder_WelderQualify welderQualify = BLL.WelderQualifyService.GetWelderQualifyById(welderQualifyId);
                    if (welderQualify != null)
                    {
                        this.hdWelderId.Text = welderQualify.WelderId;
                        this.txtQualificationItem.Text = welderQualify.QualificationItem;
                        txtWeldingLocation.Text = welderQualify.WeldingLocation;
                        if (welderQualify.CheckDate.HasValue)
                        {
                            this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", welderQualify.CheckDate);
                        }
                        if (welderQualify.LimitDate.HasValue)
                        {
                            this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", welderQualify.LimitDate);
                        }

                        txtWeldingMethod.Text = welderQualify.WeldingMethod;
                        txtMaterialType.Text = welderQualify.MaterialType;
                        this.txtThicknessMin.Text = GetStrByDecimal(welderQualify.ThicknessMin);
                        this.txtThicknessMax.Text = GetStrByDecimal(welderQualify.ThicknessMax);
                        this.txtSizesMin.Text = GetStrByDecimal(welderQualify.SizesMin);
                        this.txtSizesMax.Text = GetStrByDecimal(welderQualify.SizesMax);
                        this.txtThicknessMin2.Text = GetStrByDecimal(welderQualify.ThicknessMin2);
                        this.txtThicknessMax2.Text = GetStrByDecimal(welderQualify.ThicknessMax2);
                        this.txtSizesMin2.Text = GetStrByDecimal(welderQualify.SizesMin2);
                        this.txtSizesMax2.Text = GetStrByDecimal(welderQualify.SizesMax2);

                        this.txtWeldType.Text = welderQualify.WeldType;
                        this.ckbIsCanWeldG.Checked = welderQualify.IsCanWeldG.Value;
                        this.txtRemark.Text = welderQualify.Remark;
                        if (!string.IsNullOrEmpty(welderQualify.WelderMode))
                        {
                            this.drpWeldingMode.SelectedValue = welderQualify.WelderMode;
                        }
                    }
                }
                var w = BLL.WelderService.GetWelderById(this.hdWelderId.Text);
                if (w != null)
                {
                    this.txtWelderCode.Text = w.WelderCode;
                }
            }
        }
        #endregion

        private string GetStrByDecimal(decimal? d)
        {
            if (d == null)
            {
                return "0";
            }
            else
            {
                if (d == 0)
                {
                    return "不限";
                }
                else
                {
                    decimal a = Convert.ToDecimal(d);
                    return a.ToString("0");
                }
            }
        }

        private decimal? GetDecimalByStr(string str)
        {
            if (str == "0")
            {
                return null;
            }
            else
            {
                if (str == "不限")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(str);
                }
            }
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string welderQualifyId = Request.Params["WelderQualifyId"];
            Model.Welder_WelderQualify welderQualify = new Model.Welder_WelderQualify();
            welderQualify.WelderId = this.hdWelderId.Text;
            welderQualify.QualificationItem = txtQualificationItem.Text;
            welderQualify.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            welderQualify.LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text.Trim());
            //welderQualify.IsPrintShow = ckbIsPrintShow.Checked;
            welderQualify.WeldingMethod = txtWeldingMethod.Text.Trim();
            welderQualify.MaterialType = txtMaterialType.Text.Trim();
            welderQualify.WeldingLocation = txtWeldingLocation.Text.Trim();
            welderQualify.ThicknessMin = GetDecimalByStr(this.txtThicknessMin.Text.Trim());
            welderQualify.ThicknessMax = GetDecimalByStr(this.txtThicknessMax.Text.Trim());
            welderQualify.SizesMin = GetDecimalByStr(this.txtSizesMin.Text.Trim());
            welderQualify.SizesMax = GetDecimalByStr(this.txtSizesMax.Text.Trim());
            welderQualify.ThicknessMin2 = GetDecimalByStr(this.txtThicknessMin2.Text.Trim());
            welderQualify.ThicknessMax2 = GetDecimalByStr(this.txtThicknessMax2.Text.Trim());
            welderQualify.SizesMin2 = GetDecimalByStr(this.txtSizesMin2.Text.Trim());
            welderQualify.SizesMax2 = GetDecimalByStr(this.txtSizesMax2.Text.Trim());
            welderQualify.WeldType = txtWeldType.Text.Trim();
            welderQualify.IsCanWeldG = ckbIsCanWeldG.Checked;
            if (this.drpWeldingMode.SelectedValue != BLL.Const._Null)
            {
                welderQualify.WelderMode = drpWeldingMode.SelectedValue;
            }
            if (!string.IsNullOrEmpty(welderQualifyId))
            {
                welderQualify.WelderQualifyId = welderQualifyId;
                BLL.WelderQualifyService.UpdateWelderQualify(welderQualify);
            }
            else
            {
                welderQualify.WelderQualifyId = SQLHelper.GetNewID(typeof(Model.Welder_WelderQualify));
                BLL.WelderQualifyService.AddWelderQualify(welderQualify);
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void txtQualificationItem_OnBlur(object sender, EventArgs e)
        {
            string weldMethod = string.Empty;
            string materialType = string.Empty;
            string location = string.Empty;
            string weldType = string.Empty;
            bool isCanWeldG = true;
            string thicknessMin = string.Empty;
            string thicknessMax = string.Empty;
            string sizesMin = string.Empty;
            string sizesMax = string.Empty;
            string thicknessMin2 = string.Empty;
            string thicknessMax2 = string.Empty;
            string sizesMin2 = string.Empty;
            string sizesMax2 = string.Empty;
            string[] queProject = txtQualificationItem.Text.Trim().Split('-');
            try
            {
                // 焊接方法和钢材类型
                weldMethod = queProject[0];
                if (queProject.Count() > 1)
                {
                    if (queProject[1].Trim() == ("FeI") || queProject[1].Trim() == ("FeⅠ"))
                    {
                        materialType = "FeⅠ";
                    }

                    else if (queProject[1].Trim() == ("FeII") || queProject[1].Trim() == ("FeⅡ"))
                    {
                        materialType = "FeⅡ,FeⅠ";

                    }
                    else if (queProject[1].Trim() == ("FeIII") || queProject[1].Trim() == ("FeⅢ"))
                    {
                        materialType = "FeⅢ,FeⅡ,FeⅠ";

                    }
                    else if (queProject[1].Trim() == ("FeIV") || queProject[1].Trim() == ("FeⅣ"))
                    {
                        materialType = "FeⅣ";
                    }
                    else
                    {
                        materialType = queProject[1].Trim();
                    }
                }

                if (queProject.Count() > 2)
                {
                    // 焊接位置
                    if (queProject[2].Contains("6G"))
                    {
                        location = "ALL";
                    }
                    else if (queProject[2].Contains("2G"))
                    {
                        location = "1G,2G";
                    }
                    else if (queProject[2].Contains("3G"))
                    {
                        location = "1G,2G,3G";
                    }
                    else if (queProject[2].Contains("4G"))
                    {
                        location = "1G,3G,4G";
                    }
                    else if (queProject[2].Contains("5G"))
                    {
                        location = "1G,2G,3G,5G";
                    }

                    else if (queProject[2].Contains("6FG"))
                    {
                        location = "ALL";
                    }
                    else if (queProject[2].Contains("5FG"))
                    {
                        location = "2FG,5FG";
                    }
                    else if (queProject[2].Contains("4FG"))
                    {
                        location = "2FG,4FG";
                    }
                    else
                    {
                        location = queProject[2];
                    }

                    if (queProject[2].Contains("1G") || queProject[2].Contains("1F") || queProject[2].Contains("2FR") || queProject[2].Contains("2FRG"))
                    {
                        isCanWeldG = false;
                    }

                    // 1-对接，2-角焊缝，3-支管连接焊缝
                    if (queProject[2].Contains("FG"))
                    {
                        weldType = "角焊缝,支管连接焊缝";
                    }
                    else
                    {
                        weldType = "对接焊缝,角焊缝";
                    }
                }
                if (queProject.Count() > 3)
                {
                    // 壁厚和外径
                    string[] strs = queProject[3].Split('/');
                    int thickness = Convert.ToInt32(strs[0]);
                    int sizes = 0;
                    if (strs.Count() == 2)
                    {
                        sizes = Convert.ToInt32(strs[1]);
                    }

                    if (queProject[2].Contains("1G") || queProject[2].Contains("2G") || queProject[2].Contains("5G") || queProject[2].Contains("5GX") || queProject[2].Contains("6G") || queProject[2].Contains("6GX"))
                    {
                        //壁厚算法
                        //对接焊缝算法   B类
                        if (weldMethod != "OFW")
                        {
                            if (thickness < 12)
                            {
                                thicknessMin = "不限";
                                thicknessMax = (thickness * 2).ToString();
                            }
                            else
                            {
                                thicknessMin = "不限";
                                thicknessMax = "不限";
                            }
                        }
                        else
                        {
                            thicknessMin = "不限";
                            thicknessMax = thickness.ToString();
                        }
                        //角焊缝算法   非B类
                        thicknessMin2 = "不限";
                        thicknessMax2 = "不限";

                        //管径算法
                        //对接焊缝算法   B类
                        if (sizes < 25)
                        {
                            sizesMin = sizes.ToString();
                            sizesMax = "不限";
                        }
                        else if (sizes >= 25 && sizes < 76)
                        {
                            sizesMin = "25";
                            sizesMax = "不限";
                        }
                        else
                        {
                            sizesMin = "76";
                            sizesMax = "不限";
                        }
                        //角焊缝算法   非B类
                        sizesMin2 = "不限";
                        sizesMax2 = "不限";

                    }
                    if (queProject[2].Contains("1F") || queProject[2].Contains("2F") || queProject[2].Contains("2FR") || queProject[2].Contains("4F") || queProject[2].Contains("5F")
                        || queProject[2].Contains("2FRG") || queProject[2].Contains("2FG") || queProject[2].Contains("4FG") || queProject[2].Contains("5FG") || queProject[2].Contains("6FG"))
                    {
                        //壁厚算法
                        //对接焊缝算法   B类
                        thicknessMin = "0";
                        thicknessMax = "0";
                        //角焊缝算法   非B类
                        if (strs.Length == 2)   //外径为：非none（管材），即外径和壁厚间有/
                        {
                            thicknessMin2 = "不限";
                            thicknessMax2 = "不限";
                        }
                        else    //外径为：none（管材），即外径和壁厚间没有/
                        {
                            if (thickness < 5)
                            {
                                thicknessMin2 = thickness.ToString();
                                thicknessMax2 = (thickness * 2).ToString();
                            }
                            else if (thickness >= 5 && thickness < 10)
                            {
                                thicknessMin2 = "不限";
                                thicknessMax2 = "不限";
                            }
                        }
                        //管径算法
                        //对接焊缝算法   B类
                        sizesMin = "0";
                        sizesMax = "0";
                        //角焊缝算法   非B类
                        if (strs.Length == 2)   //外径为：非none（管材），即外径和壁厚间有/
                        {
                            if (sizes < 25)
                            {
                                sizesMin2 = sizes.ToString();
                                sizesMax2 = "不限";
                            }
                            else if (sizes >= 25 && sizes < 76)
                            {
                                sizesMin2 = "25";
                                sizesMax2 = "不限";
                            }
                            else
                            {
                                sizesMin2 = "76";
                                sizesMax2 = "不限";
                            }
                        }
                        else    //外径为：none（管材），即外径和壁厚间没有/
                        {
                            sizesMin2 = "76";
                            sizesMax2 = "不限";
                        }
                    }
                    //string[] thickSize = queProject[3].Split('/');
                    //if (weldMethod != "OFW")
                    //{
                    //    if (thickSize.Count() == 2)
                    //    {
                    //        int thick = Convert.ToInt32(thickSize[0]);
                    //        if (thick < 12)
                    //        {
                    //            //thicknessMax = "≤" + thick * 2;
                    //            thicknessMax = thick * 2;
                    //        }
                    //        else
                    //        {
                    //            thicknessMax = 0;
                    //        }

                    //        int size = Convert.ToInt32(thickSize[1]);
                    //        if (size < 25)
                    //        {
                    //            //sizesMin = "≥" + size;
                    //            sizesMin = size;
                    //        }
                    //        else if (size >= 25 && size < 76)
                    //        {
                    //            //sizesMin = "≥25";
                    //            sizesMin = 25;
                    //        }
                    //        else
                    //        {
                    //            //sizesMin = "≥76";
                    //            sizesMin = 76;
                    //        }
                    //    }
                    //    else if (thickSize.Count() == 1)
                    //    {
                    //        thicknessMax = 0;
                    //        //sizesMin = "≥76";
                    //        sizesMin = 76;
                    //    }
                    //}
                    //else
                    //{
                    //    int thick = Convert.ToInt32(thickSize[0]);
                    //    thicknessMax = thick;
                    //    sizesMin = 0;
                    //}
                }

                txtWeldingMethod.Text = weldMethod;
                txtMaterialType.Text = materialType;
                txtWeldingLocation.Text = location;
                txtThicknessMin.Text = thicknessMin;
                txtThicknessMax.Text = thicknessMax;
                txtSizesMin.Text = sizesMin;
                txtSizesMax.Text = sizesMax;
                txtThicknessMin2.Text = thicknessMin2;
                txtThicknessMax2.Text = thicknessMax2;
                txtSizesMin2.Text = sizesMin2;
                txtSizesMax2.Text = sizesMax2;
                txtWeldType.Text = weldType;
                ckbIsCanWeldG.Checked = isCanWeldG;
            }
            catch
            {
                ShowNotify("请录入规范数据！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}