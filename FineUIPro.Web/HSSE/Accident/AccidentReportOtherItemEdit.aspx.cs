using BLL;
using System;

namespace FineUIPro.Web.HSSE.Accident
{
    public partial class AccidentReportOtherItemEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string AccidentReportOtherId
        {
            get
            {
                return (string)ViewState["AccidentReportOtherId"];
            }
            set
            {
                ViewState["AccidentReportOtherId"] = value;
            }
        }

        public string AccidentReportOtherItemId
        {
            get
            {
                return (string)ViewState["AccidentReportOtherItemId"];
            }
            set
            {
                ViewState["AccidentReportOtherItemId"] = value;
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, true);
                PersonService.InitPersonByProjectUnitDropDownList(this.drpPersonId, this.CurrUser.LoginProjectId, string.Empty, true);
                this.AccidentReportOtherId = Request.Params["AccidentReportOtherId"];
                this.AccidentReportOtherItemId = Request.Params["AccidentReportOtherItemId"];
                if (!string.IsNullOrEmpty(this.AccidentReportOtherItemId))
                {
                    Model.Accident_AccidentReportOtherItem accidentReportOtherItem = BLL.AccidentReportOtherItemService.GetAccidentReportOtherItemById(this.AccidentReportOtherItemId);
                    if (accidentReportOtherItem != null)
                    {
                        this.AccidentReportOtherId = accidentReportOtherItem.AccidentReportOtherId;
                        if (!string.IsNullOrEmpty(accidentReportOtherItem.UnitId))
                        {
                            this.drpUnitId.SelectedValue = accidentReportOtherItem.UnitId;
                        }
                        if (!string.IsNullOrEmpty(accidentReportOtherItem.PersonId))
                        {
                            this.drpPersonId.SelectedValue = accidentReportOtherItem.PersonId;
                        }
                        if (!string.IsNullOrEmpty(accidentReportOtherItem.PositionId))
                        {
                            this.hdPositionId.Text = accidentReportOtherItem.PositionId;
                            var position = BLL.PositionService.GetPositionById(this.hdPositionId.Text);
                            if (position != null)
                            {
                                this.txtPositionName.Text = position.PositionName;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Accident_AccidentReportOtherItem accidentReportOtherItem = new Model.Accident_AccidentReportOtherItem
            {
                AccidentReportOtherId = this.AccidentReportOtherId
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                accidentReportOtherItem.UnitId = this.drpUnitId.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpPersonId.SelectedValue != BLL.Const._Null)
            {
                accidentReportOtherItem.PersonId = this.drpPersonId.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择姓名！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.hdPositionId.Text))
            {
                accidentReportOtherItem.PositionId = this.hdPositionId.Text;
            }
            if (!string.IsNullOrEmpty(this.AccidentReportOtherItemId))
            {
                accidentReportOtherItem.AccidentReportOtherItemId = this.AccidentReportOtherItemId;
                BLL.AccidentReportOtherItemService.UpdateAccidentReportOtherItem(accidentReportOtherItem);
            }
            else
            {
                this.AccidentReportOtherItemId = SQLHelper.GetNewID(typeof(Model.Accident_AccidentReportOtherItem));
                accidentReportOtherItem.AccidentReportOtherItemId = this.AccidentReportOtherItemId;
                BLL.AccidentReportOtherItemService.AddAccidentReportOtherItem(accidentReportOtherItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 人员下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpPersonId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpPersonId.SelectedValue != BLL.Const._Null)
            {
                var person = BLL.PersonService.GetPersonById(this.drpPersonId.SelectedValue);
                if (person != null)
                {
                    if (!string.IsNullOrEmpty(person.PositionId))
                    {
                        var position = BLL.PositionService.GetPositionById(person.PositionId);
                        if (position != null)
                        {
                            this.hdPositionId.Text = position.PositionId;
                            this.txtPositionName.Text = position.PositionName;
                        }
                    }
                }
            }
        }
    }
}