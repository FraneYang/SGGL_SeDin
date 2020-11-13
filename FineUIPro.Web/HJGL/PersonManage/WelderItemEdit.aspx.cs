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
                       
                        if (welderQualify.ThicknessMax.HasValue)
                        {
                            this.txtThicknessMax.Text = welderQualify.ThicknessMax.ToString();
                        }

                        if (welderQualify.SizesMin.HasValue)
                        {
                            this.txtSizesMin.Text = welderQualify.SizesMin.ToString();
                        }

                        this.txtWeldType.Text = welderQualify.WeldType;
                        this.ckbIsCanWeldG.Checked = welderQualify.IsCanWeldG.Value;
                        this.txtRemark.Text = welderQualify.Remark;
                        if (!string.IsNullOrEmpty(welderQualify.WelderMode)) {
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
            welderQualify.QualificationItem =txtQualificationItem.Text;
            welderQualify.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            welderQualify.LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text.Trim());
            //welderQualify.IsPrintShow = ckbIsPrintShow.Checked;
            welderQualify.WeldingMethod = txtWeldingMethod.Text.Trim();
            welderQualify.MaterialType = txtMaterialType.Text.Trim();
            welderQualify.WeldingLocation = txtWeldingLocation.Text.Trim();
            welderQualify.ThicknessMax = Funs.GetNewDecimal(this.txtThicknessMax.Text.Trim());
            welderQualify.SizesMin = Funs.GetNewDecimal(this.txtSizesMin.Text.Trim());
            welderQualify.WeldType = txtWeldType.Text.Trim();
            welderQualify.IsCanWeldG = ckbIsCanWeldG.Checked;
            if (this.drpWeldingMode.SelectedValue != BLL.Const._Null) {
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
            int thicknessMax = 0;  // 0表示不限
            int sizesMin = 0;      // 0表示不限

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
                    string[] thickSize = queProject[3].Split('/');
                    if (weldMethod != "OFW")
                    {
                        if (thickSize.Count() == 2)
                        {
                            int thick = Convert.ToInt32(thickSize[0]);
                            if (thick < 12)
                            {
                                //thicknessMax = "≤" + thick * 2;
                                thicknessMax = thick * 2;
                            }
                            else
                            {
                                thicknessMax = 0;
                            }

                            int size = Convert.ToInt32(thickSize[1]);
                            if (size < 25)
                            {
                                //sizesMin = "≥" + size;
                                sizesMin = size;
                            }
                            else if (size >= 25 && size < 76)
                            {
                                //sizesMin = "≥25";
                                sizesMin = 25;
                            }
                            else
                            {
                                //sizesMin = "≥76";
                                sizesMin = 76;
                            }
                        }
                        else if (thickSize.Count() == 1)
                        {
                            thicknessMax = 0;
                            //sizesMin = "≥76";
                            sizesMin = 76;
                        }
                    }
                    else
                    {
                        int thick = Convert.ToInt32(thickSize[0]);
                        thicknessMax = thick;
                        sizesMin = 0;
                    }
                }

                txtWeldingMethod.Text = weldMethod;
                txtMaterialType.Text = materialType;
                txtWeldingLocation.Text = location;
                txtThicknessMax.Text = thicknessMax.ToString();
                txtSizesMin.Text = sizesMin.ToString();
                txtWeldType.Text = weldType;
                ckbIsCanWeldG.Checked = isCanWeldG;
            }
            catch
            {
                ShowNotify("请录入规范数据！",MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}