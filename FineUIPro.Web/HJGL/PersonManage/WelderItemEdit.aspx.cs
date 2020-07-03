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
                        this.txtRemark.Text = welderQualify.Remark;
                        
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
            int thicknessMax = 0;  // 0表示不限
            int sizesMin = 0;      // 0表示不限

            string[] queProject = txtQualificationItem.Text.Trim().Split('-');
            try
            {
                // 焊接方法和钢材类型
                weldMethod = queProject[0];
                if (weldMethod.Contains("GTAW"))
                {
                    if (queProject[1] == "FeⅡ" || queProject[1] == "FeII")
                    {
                        materialType = "FeⅡ,FeⅠ";
                    }
                    else if (queProject[1] == "FeⅢ" || queProject[1] == "FeIII")
                    {
                        materialType = "FeⅢ";
                    }
                    else if (queProject[1] == "FeⅣ" || queProject[1] == "FeIV")
                    {
                        materialType = "FeⅣ";
                    }
                    else if (queProject[1] == "FeⅠ" || queProject[1] == "FeI")
                    {
                        materialType = "FeⅠ";
                    }
                    else
                    {
                        materialType = queProject[1];
                    }
                }
                else if (weldMethod.Contains("SMAW"))
                {
                    if (queProject[1] == "FeⅡ" || queProject[1] == "FeII")
                    {
                        materialType = "FeⅡ,FeⅠ";
                    }
                    else if (queProject[1] == "FeⅢ" || queProject[1] == "FeIII")
                    {
                        materialType = "FeⅢ,FeⅡ,FeⅠ";
                    }
                    else if (queProject[1] == "FeⅣ" || queProject[1] == "FeIV")
                    {
                        materialType = "FeⅣ";
                    }
                    else if (queProject[1] == "FeⅠ" || queProject[1] == "FeI")
                    {
                        materialType = "FeⅠ";
                    }
                    else
                    {
                        materialType = queProject[1];
                    }
                }
                else
                {
                    materialType = queProject[1];
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
                }
                if (queProject.Count() > 3)
                {
                    // 壁厚和外径
                    string[] thickSize = queProject[3].Split('/');
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

                txtWeldingMethod.Text = weldMethod;
                txtMaterialType.Text = materialType;
                txtWeldingLocation.Text = location;
                txtThicknessMax.Text = thicknessMax.ToString();
                txtSizesMin.Text = sizesMin.ToString();

            }
            catch
            {
                ShowNotify("请录入规范数据！",MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}