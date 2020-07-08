using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HSSE.Technique
{
    public partial class EnvironmentalEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string EnvironmentalId
        {
            get
            {
                return (string)ViewState["EnvironmentalId"];
            }
            set
            {
                ViewState["EnvironmentalId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                BLL.ConstValue.InitConstValueDropDownList(this.drpEType, ConstValue.Group_EnvironmentalType, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpSmallType, ConstValue.Group_EnvironmentalSmallType, false);
                BLL.ConstValue.InitConstValueRadioButtonList(this.rblIsImportant, ConstValue.Group_0001, "False");
                
                this.EnvironmentalId = Request.Params["EnvironmentalId"];
                if (!string.IsNullOrEmpty(this.EnvironmentalId))
                {
                    var q = BLL.EnvironmentalService.GetEnvironmental(this.EnvironmentalId);
                    if (q != null)
                    {
                        this.txtCode.Text = q.Code;
                        if (!string.IsNullOrEmpty(q.EType))
                        {
                            this.drpEType.SelectedValue = q.EType;
                        }
                        this.txtActivePoint.Text = q.ActivePoint;
                        this.txtEnvironmentalFactors.Text = q.EnvironmentalFactors;
                        this.txtControlMeasures.Text = q.ControlMeasures;
                        this.txtRemark.Text = q.Remark;
                        if (!string.IsNullOrEmpty(q.SmallType))
                        {
                            this.drpSmallType.SelectedValue = q.SmallType;
                        }
                        if (q.IsImportant == true)
                        {
                            this.rblIsImportant.SelectedValue = "True";
                        }
                        else
                        {
                            this.rblIsImportant.SelectedValue = "False";
                        }
                        if (q.AValue.HasValue)
                        {
                            this.txtAValue.Text = q.AValue.ToString();
                        }
                        if (q.BValue.HasValue)
                        {
                            this.txtBValue.Text = q.BValue.ToString();
                        }
                        if (q.CValue.HasValue)
                        {
                            this.txtCValue.Text = q.CValue.ToString();
                        }
                        if (q.DValue.HasValue)
                        {
                            this.txtDValue.Text = q.DValue.ToString();
                        }
                        if (q.EValue.HasValue)
                        {
                            this.txtEValue.Text = q.EValue.ToString();
                        }
                        if (q.FValue.HasValue)
                        {
                            this.txtFValue.Text = q.FValue.ToString();
                        }
                        if (q.GValue.HasValue)
                        {
                            this.txtGValue.Text = q.GValue.ToString();
                        } 
                    }
                }

                this.ShowRow();
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.Technique_Environmental environmental = new Model.Technique_Environmental
            {
                Code = this.txtCode.Text.Trim()
            };
            if (this.drpEType.SelectedValue != BLL.Const._Null)
            {
                environmental.EType = this.drpEType.SelectedValue;
            }
            environmental.ActivePoint = this.txtActivePoint.Text.Trim();
            environmental.EnvironmentalFactors = this.txtEnvironmentalFactors.Text.Trim();
            environmental.ControlMeasures = this.txtControlMeasures.Text.Trim();
            environmental.Remark = this.txtRemark.Text.Trim();
            if (this.drpSmallType.SelectedValue != BLL.Const._Null)
            {
                environmental.SmallType = this.drpSmallType.SelectedValue;
            }
            if (this.rblIsImportant.SelectedValue == "True")
            {
                environmental.IsImportant = true;
            }
            else
            {
                environmental.IsImportant = false;
            }
            int? aValue = Funs.GetNewInt(this.txtAValue.Text.Trim());
            if (aValue == 2 || aValue == 4)
            {
                environmental.AValue = aValue - 1;
            }
            else
            {
                environmental.AValue = aValue;
            }
            int? bValue = Funs.GetNewInt(this.txtBValue.Text.Trim());
            if (bValue == 2 || bValue == 4)
            {
                environmental.BValue = bValue - 1;
            }
            else
            {
                environmental.BValue = bValue;
            }
            int? cValue = Funs.GetNewInt(this.txtCValue.Text.Trim());
            if (cValue == 2 || cValue == 4)
            {
                environmental.CValue = cValue - 1;
            }
            else
            {
                environmental.CValue = cValue;
            }
            int? dValue = Funs.GetNewInt(this.txtDValue.Text.Trim());
            if (dValue == 2 || dValue == 4)
            {
                environmental.DValue = dValue - 1;
            }
            else
            {
                environmental.DValue = dValue;
            }
            int? eValue = Funs.GetNewInt(this.txtEValue.Text.Trim());
            if (eValue == 2 || eValue == 4)
            {
                environmental.EValue = eValue - 1;
            }
            else
            {
                environmental.EValue = eValue;
            }
            int? fValue = Funs.GetNewInt(this.txtFValue.Text.Trim());
            if (fValue == 2 || fValue == 4)
            {
                environmental.FValue = fValue - 1;
            }
            else
            {
                environmental.FValue = fValue;
            }
            int? gValue = Funs.GetNewInt(this.txtGValue.Text.Trim());
            if (gValue == 2 || gValue == 4)
            {
                environmental.GValue = gValue - 1;
            }
            else
            {
                environmental.GValue = gValue;
            }
            //environmental.ZValue = Funs.GetNewInt(this.txtZValue.Text.Trim());
            if (string.IsNullOrEmpty(this.EnvironmentalId))
            {
                environmental.IsCompany = Convert.ToBoolean(Request.Params["IsCompany"]);
                environmental.EnvironmentalId = SQLHelper.GetNewID(typeof(Model.Technique_Environmental));
                EnvironmentalId = environmental.EnvironmentalId;
                BLL.EnvironmentalService.AddEnvironmental(environmental);
                BLL.LogService.AddSys_Log(this.CurrUser, environmental.Code, environmental.EnvironmentalId, BLL.Const.EnvironmentalMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                environmental.EnvironmentalId = this.EnvironmentalId;
                BLL.EnvironmentalService.UpdateEnvironmental(environmental);
                BLL.LogService.AddSys_Log(this.CurrUser, environmental.Code, environmental.EnvironmentalId,BLL.Const.EnvironmentalMenuId,BLL.Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region 验证危险源代码是否存在
        /// <summary>
        /// 验证危险源名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Technique_Environmental.FirstOrDefault(x => x.Code == this.txtCode.Text.Trim() && (x.EnvironmentalId != this.EnvironmentalId || (this.EnvironmentalId == null && x.EnvironmentalId != null)));
            if (q != null)
            {
                ShowNotify("输入的危险源代码已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 文本框改变事件
        /// <summary>
        /// 文本框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            this.ShowRow();
        }
        
        /// <summary>
        /// 危险源类型改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSmallType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtAValue.Text = string.Empty;
            this.txtBValue.Text = string.Empty;
            this.txtCValue.Text = string.Empty;
            this.txtDValue.Text = string.Empty;
            this.txtEValue.Text = string.Empty;
            this.txtFValue.Text = string.Empty;
            this.txtGValue.Text = string.Empty;
            this.txtZValue.Text = string.Empty;
            this.ShowRow();
        }

        /// <summary>
        ///  危险源类型事件
        /// </summary>
        private void ShowRow()
        {
            this.rblIsImportant.SelectedValue = "False";
            if (this.drpSmallType.SelectedValue == "1")
            {
                this.fRow1.Hidden = true;
                this.fRow2.Hidden = false;
                int f = Funs.GetNewIntOrZero(this.txtFValue.Text);
                int g = Funs.GetNewIntOrZero(this.txtGValue.Text);
                this.txtZValue.Text = (f + g).ToString();
                if (f >= 5 || g >= 5 || (f + g) > 7)
                {
                    this.rblIsImportant.SelectedValue = "True";
                }               
            }
            else
            {
                this.fRow2.Hidden = true;
                this.fRow1.Hidden = false;
                int a = Funs.GetNewIntOrZero(this.txtAValue.Text);
                int b = Funs.GetNewIntOrZero(this.txtBValue.Text);
                int c = Funs.GetNewIntOrZero(this.txtCValue.Text);
                int d = Funs.GetNewIntOrZero(this.txtDValue.Text);
                int e = Funs.GetNewIntOrZero(this.txtEValue.Text);
                this.txtZValue.Text = (a+b+c+d+e).ToString();
                if (a >= 5 || b >= 5 || d >= 5 || (a + b + c + d + e) >= 15)
                {
                    this.rblIsImportant.SelectedValue = "True";
                }
            }
        }
        #endregion
    }
}