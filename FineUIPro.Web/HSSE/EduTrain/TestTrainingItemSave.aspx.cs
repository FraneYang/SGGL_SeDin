using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TestTrainingItemSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainingItemId
        {
            get
            {
                return (string)ViewState["TrainingItemId"];
            }
            set
            {
                ViewState["TrainingItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        public string TrainingId
        {
            get
            {
                return (string)ViewState["TrainingId"];
            }
            set
            {
                ViewState["TrainingId"] = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        private string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
            }
        }
        /// <summary>
        /// 单选题分值
        /// </summary>
        private int SValue
        {
            get
            {
                return (int)ViewState["SValue"];
            }
            set
            {
                ViewState["SValue"] = value;
            }
        }
        /// <summary>
        /// 多选题分值
        /// </summary>
        private int MValue
        {
            get
            {
                return (int)ViewState["MValue"];
            }
            set
            {
                ViewState["MValue"] = value;
            }
        } 

        /// <summary>
        /// 判断题分值
        /// </summary>
        private int JValue
        {
            get
            {
                return (int)ViewState["JValue"];
            }
            set
            {
                ViewState["JValue"] = value;
            }
        }
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SValue = 1;
                MValue = 2;
                JValue = 1;
                var testRule = Funs.DB.Sys_TestRule.FirstOrDefault();
                if (testRule != null)
                {
                    SValue = testRule.SValue ;
                    MValue = testRule.MValue;
                    JValue = testRule.JValue;
                }

                this.GetButtonPower();
                LoadData();
                BindEnumrableToDropDownList();
                this.TrainingItemId = Request.QueryString["TrainingItemId"];
                this.TrainingId = Request.QueryString["TrainingId"];
                if (!String.IsNullOrEmpty(this.TrainingItemId))
                {
                    var q = BLL.TestTrainingItemService.GetTestTrainingItemById(this.TrainingItemId);
                    if (q != null)
                    {
                        txtTrainingItemCode.Text = q.TrainingItemCode;
                        if (!string.IsNullOrEmpty(q.TestType))
                        {
                            this.rblTestType.SelectedValue = q.TestType;
                            if (q.TestType == "1")
                            {
                                this.lbScore.Text = SValue.ToString();
                                this.trE.Hidden = true;
                            }
                            else if (q.TestType == "2")
                            {
                                this.lbScore.Text = MValue.ToString();
                            }
                            else if (q.TestType == "3")
                            {
                                this.lbScore.Text = JValue.ToString();
                                this.trC.Hidden = true;
                                this.trD.Hidden = true;
                                this.trE.Hidden = true;
                            }

                            System.Web.UI.WebControls.ListItem[] myList2 = BLL.TestTrainingItemService.GetAnswerItemsList(this.rblTestType.SelectedValue);
                            RadioButtonList2.DataTextField = "Text";
                            RadioButtonList2.DataValueField = "Value";
                            RadioButtonList2.DataSource = myList2;
                            RadioButtonList2.DataBind();

                            if (!string.IsNullOrEmpty(q.AnswerItems))
                            {
                                string[] ids2 = q.AnswerItems.Split(',');
                                DropDownBox2.Values = ids2;
                            }
                        }
                        txtAbstracts.Text = q.Abstracts;
                        if (!string.IsNullOrEmpty(q.WorkPostIds))
                        {
                            string[] ids1 = q.WorkPostIds.Split(',');
                            DropDownBox1.Values = ids1;
                        }
                        this.txtAItem.Text = q.AItem;
                        this.txtBItem.Text = q.BItem;
                        this.txtCItem.Text = q.CItem;
                        this.txtDItem.Text = q.DItem;
                        this.txtEItem.Text = q.EItem;

                        this.AttachUrl = q.AttachUrl;
                        if (!string.IsNullOrEmpty(this.AttachUrl))
                        {
                            this.trImageUrl.Visible = true;
                            this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.AttachUrl);
                            this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../../", this.AttachUrl);
                        }
                    }
                }
                else
                {
                    this.lbScore.Text = SValue.ToString();
                    this.trE.Hidden = true;
                }
            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.trImageUrl.Visible = false;
            this.AttachUrl = string.Empty;
            this.divFile.InnerHtml = string.Empty;
            this.divBeImageUrl.InnerHtml = string.Empty; ;
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        private void BindEnumrableToDropDownList()
        {
            List<Model.Base_WorkPost> myList = BLL.WorkPostService.GetWorkPostList();
            RadioButtonList1.DataTextField = "WorkPostName";
            RadioButtonList1.DataValueField = "WorkPostId";
            RadioButtonList1.DataSource = myList;
            RadioButtonList1.DataBind();
            System.Web.UI.WebControls.ListItem[] myList2 = BLL.TestTrainingItemService.GetAnswerItemsList(this.rblTestType.SelectedValue);
            RadioButtonList2.DataTextField = "Text";
            RadioButtonList2.DataValueField = "Value";
            RadioButtonList2.DataSource = myList2;
            RadioButtonList2.DataBind();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(bool isClosed)
        {
            Model.Training_TestTrainingItem trainingItem = new Training_TestTrainingItem
            {
                TrainingItemCode = txtTrainingItemCode.Text.Trim(),
                Abstracts = txtAbstracts.Text.Trim(),
                TestType = this.rblTestType.SelectedValue,
                AItem = txtAItem.Text.Trim(),
                BItem = txtBItem.Text.Trim(),
                CItem = txtCItem.Text.Trim(),
                DItem = txtDItem.Text.Trim(),
                EItem = txtEItem.Text.Trim(),
                AttachUrl = this.AttachUrl,

            };
            if (!string.IsNullOrEmpty(DropDownBox1.Text))
            {
                trainingItem.WorkPostIds = String.Join(",", DropDownBox1.Values);
                trainingItem.WorkPostNames = String.Join(",", DropDownBox1.Texts);
            }
            if (!string.IsNullOrEmpty(DropDownBox2.Text))
            {
                trainingItem.AnswerItems = String.Join(",", DropDownBox2.Values);
            }
            else
            {
                trainingItem.AnswerItems = string.Empty;
            }
            if (String.IsNullOrEmpty(TrainingItemId))
            {
                trainingItem.TrainingItemId = SQLHelper.GetNewID(typeof(Model.Training_TestTrainingItem));
                trainingItem.TrainingId = this.TrainingId;
                this.TrainingItemId = trainingItem.TrainingItemId;
                BLL.TestTrainingItemService.AddTestTrainingItem(trainingItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainingItem.TrainingItemCode, trainingItem.TrainingItemId, BLL.Const.TestTrainingMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.Training_TestTrainingItem t = BLL.TestTrainingItemService.GetTestTrainingItemById(TrainingItemId);
                trainingItem.TrainingItemId = TrainingItemId;
                if (t != null)
                {
                    trainingItem.TrainingId = t.TrainingId;
                }
                BLL.TestTrainingItemService.UpdateTestTrainingItem(trainingItem);
                BLL.LogService.AddSys_Log(this.CurrUser, trainingItem.TrainingItemCode, trainingItem.TrainingItemId, BLL.Const.TestTrainingMenuId, BLL.Const.BtnModify);
            }
            if (isClosed)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFile_Click(object sender, EventArgs e)
        {
            if (btnFile.HasFile)
            {
                this.AttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnFile, this.AttachUrl, UploadFileService.TrainingFilePath);
                if (!string.IsNullOrEmpty(this.AttachUrl))
                {
                    this.trImageUrl.Visible = true;
                    this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../../", this.AttachUrl);
                    this.divBeImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowImage("../../", this.AttachUrl);
                }
            }
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TestTrainingMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证教材名称是否存在
        /// <summary>
        /// 验证教材名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            //var q = Funs.DB.Training_TrainingItem.FirstOrDefault(x => x.IsPass == true && x.TrainingId == this.TrainingId && x.TrainingItemName == this.txtTrainingItemName.Text.Trim() && (x.TrainingItemId != this.TrainingItemId || (this.TrainingItemId == null && x.TrainingItemId != null)));
            //if (q != null)
            //{
            //    ShowNotify("输入的教材名称已存在！", MessageBoxIcon.Warning);
            //}
        }
        #endregion

        /// <summary>
        /// 题型变换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.trC.Hidden = false;
            this.trD.Hidden = false;
            this.trE.Hidden = false;

            this.txtAItem.Text = string.Empty;
            this.txtBItem.Text = string.Empty;
            this.txtCItem.Text = string.Empty;
            this.txtDItem.Text = string.Empty;
            this.txtEItem.Text = string.Empty;

            string testType = this.rblTestType.SelectedValue;
            System.Web.UI.WebControls.ListItem[] myList2 = BLL.TestTrainingItemService.GetAnswerItemsList(testType);
            RadioButtonList2.DataTextField = "Text";
            RadioButtonList2.DataValueField = "Value";
            RadioButtonList2.DataSource = myList2;
            RadioButtonList2.DataBind();

            DropDownBox2.Text = string.Empty;
            DropDownBox2.Values = null;

            if (testType == "1")
            {
                this.lbScore.Text = SValue.ToString();
                this.trE.Hidden = true;
            }
            else if (testType == "2")
            {
                this.lbScore.Text = MValue.ToString();
            }
            else if (testType == "3")
            {
                this.lbScore.Text = JValue.ToString();
                this.trC.Hidden = true;
                this.trD.Hidden = true;
                this.trE.Hidden = true;
                this.txtAItem.Text = "对";
                this.txtBItem.Text = "错";
            }
        }
    }
}