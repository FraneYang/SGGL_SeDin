using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.SitePerson
{
    public partial class PersonListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }
        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string QRCodeAttachUrl
        {
            get
            {
                return (string)ViewState["QRCodeAttachUrl"];
            }
            set
            {
                ViewState["QRCodeAttachUrl"] = value;
            }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
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
                ////权限按钮方法
                this.GetButtonPower();
                this.ProjectId = Request.Params["ProjectId"];
                this.UnitId = Request.Params["UnitId"];
                this.PersonId = Request.Params["PersonId"];
                this.InitDropDownList();
                this.drpIdcardType.SelectedValue = "SHENFEN_ZHENGJIAN";
                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    var person = BLL.PersonService.GetPersonById(this.PersonId);
                    if (person != null)
                    {
                        this.ProjectId = person.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        if (!string.IsNullOrEmpty(person.Sex))
                        {
                            this.rblSex.SelectedValue = person.Sex;
                        }
                        if (!string.IsNullOrEmpty(person.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(person.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                                this.UnitId = person.UnitId;
                            }
                        }
                        if (!string.IsNullOrEmpty(person.WorkAreaId))
                        {
                            txtWorkArea.Values = person.WorkAreaId.Split(',');
                        }
                        if (!string.IsNullOrEmpty(person.WorkPostId))
                        {
                            this.drpPost.SelectedValue = person.WorkPostId;
                        }
                        if (!string.IsNullOrEmpty(person.PositionId))
                        {
                            this.drpPosition.SelectedValue = person.PositionId;
                        }
                        if (!string.IsNullOrEmpty(person.PostTitleId))
                        {
                            this.drpTitle.SelectedValue = person.PostTitleId;
                        }
                        if (!string.IsNullOrEmpty(person.TeamGroupId))
                        {
                            this.drpTeamGroup.SelectedValue = person.TeamGroupId;
                        }
                        if (!string.IsNullOrEmpty(person.AuditorId))
                        {
                            this.drpAuditor.SelectedValue = person.AuditorId;
                        }
                        if (!string.IsNullOrEmpty(person.MainCNProfessionalId))
                        {
                            this.drpMainCNProfessional.SelectedValue = person.MainCNProfessionalId;
                        }
                        if (!string.IsNullOrEmpty(person.ViceCNProfessionalId))
                        {
                            this.drpViceCNProfessional.SelectedValueArray = person.ViceCNProfessionalId.Split(',');
                        }
                        if (!string.IsNullOrEmpty(person.EduLevel))
                        {
                            this.drpEduLevel.SelectedValue = person.EduLevel;
                        }
                        if (!string.IsNullOrEmpty(person.MaritalStatus))
                        {
                            this.drpMaritalStatus.SelectedValue = person.MaritalStatus;
                        }
                        this.txtIdcardAddress.Text = person.IdcardAddress;
                        if (person.IsUsed == true && person.InTime <= DateTime.Now && (!person.OutTime.HasValue || person.OutTime >= DateTime.Now))
                        {
                            this.rblIsUsed.SelectedValue = "True";
                        }
                        else
                        {
                            this.rblIsUsed.SelectedValue = "False";
                        }
                        this.rblIsCardUsed.SelectedValue = person.IsCardUsed ? "True" : "False";
                        this.txtCardNo.Text = person.CardNo;
                        this.txtPersonName.Text = person.PersonName;
                        this.txtIdentityCard.Text = person.IdentityCard;
                        this.txtAddress.Text = person.Address;
                        this.txtTelephone.Text = person.Telephone;
                        this.txtOutResult.Text = person.OutResult;
                        if (person.IsForeign.HasValue)
                        {
                            this.ckIsForeign.Checked = person.IsForeign.Value;
                        }
                        if (person.IsOutside.HasValue)
                        {
                            this.ckIsOutside.Checked = person.IsOutside.Value;
                        }
                        if (person.InTime.HasValue)
                        {
                            this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", person.InTime);
                        }
                        if (person.OutTime.HasValue)
                        {
                            this.txtOutTime.Text = string.Format("{0:yyyy-MM-dd}", person.OutTime);
                        }
                        if (person.AuditorDate.HasValue)
                        {
                            this.txtAuditorDate.Text = string.Format("{0:yyyy-MM-dd}", person.AuditorDate);
                            this.drpAuditor.Readonly = true;
                            this.txtAuditorDate.Readonly = true;
                        }
                        if (person.Birthday.HasValue)
                        {
                            this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", person.Birthday);
                        }
                        if (!string.IsNullOrEmpty(person.PhotoUrl))
                        {
                            imgPhoto.ImageUrl = ("~/" + person.PhotoUrl);
                        }
                        if (!string.IsNullOrEmpty(person.IdcardType))
                        {
                            this.drpIdcardType.SelectedValue = person.IdcardType;
                        }
                        if (!string.IsNullOrEmpty(person.PoliticsStatus))
                        {
                            this.drpPoliticsStatus.SelectedValue = person.PoliticsStatus;
                        }
                        if (!string.IsNullOrEmpty(person.Nation))
                        {
                            this.drpNation.SelectedValue = person.Nation;
                        }
                        if (!string.IsNullOrEmpty(person.CountryCode))
                        {
                            this.drpCountryCode.SelectedValue = person.CountryCode;
                            CityService.InitCityDropDownList(this.drpProvinceCode, person.CountryCode, false);
                        }
                        if (!string.IsNullOrEmpty(person.ProvinceCode))
                        {
                            this.drpProvinceCode.SelectedValue = person.ProvinceCode;
                        }
                        if (person.IdcardStartDate != null)
                        {
                            this.txtIdcardStartDate.Text = string.Format("{0:yyyy-MM-dd}", person.IdcardStartDate);
                        }
                        if (!string.IsNullOrEmpty(person.IdcardForever))
                        {
                            this.rblIdcardForever.SelectedValue = person.IdcardForever;
                            if (person.IdcardForever == "Y")
                            {
                                this.txtIdcardEndDate.Enabled = false;
                                this.txtIdcardEndDate.ShowRedStar = false;
                                this.txtIdcardEndDate.Required = false;
                            }
                        }
                        if (person.IdcardEndDate != null)
                        {
                            this.txtIdcardEndDate.Text = string.Format("{0:yyyy-MM-dd}", person.IdcardEndDate);
                        }
                    }

                    var personQuality = PersonQualityService.GetPersonQualityByPersonId(this.PersonId);
                    if (personQuality != null)
                    {
                        this.drpCertificate.SelectedValue = personQuality.CertificateId;
                        this.txtCertificateCode.Text = personQuality.CertificateNo;
                        this.txtCertificateLimitTime.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LimitDate);
                    }
                }
                else
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                        this.txtCardNo.Text = string.Empty;
                    }
                    this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.rblIsUsed.SelectedValue = "True";
                    this.rblIsCardUsed.SelectedValue = "True";
                }
            }
        }
        #endregion

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            gvWorkArea.DataSource = BLL.UnitWorkService.GetUnitWorkLists(this.CurrUser.LoginProjectId);
            gvWorkArea.DataBind();//单位工程 
            WorkPostService.InitWorkPostDropDownList(this.drpPost, true);
            PositionService.InitPositionDropDownList(this.drpPosition, true);
            PostTitleService.InitPostTitleDropDownList(this.drpTitle, true);
            TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.ProjectId, this.UnitId, true);
            CertificateService.InitCertificateDropDownList(this.drpCertificate, true);
            UserService.InitFlowOperateControlUserDropDownList(this.drpAuditor, this.ProjectId, Const.UnitId_SEDIN, true);
            CNProfessionalService.InitCNProfessionalDownList(this.drpMainCNProfessional, true);
            CNProfessionalService.InitCNProfessionalDownList(this.drpViceCNProfessional, true);
            BasicDataService.InitBasicDataProjectUnitDropDownList(this.drpEduLevel, "EDU_LEVEL", true);
            BasicDataService.InitBasicDataProjectUnitDropDownList(this.drpMaritalStatus, "MARITAL_STATUS", true);
            BasicDataService.InitBasicDataProjectUnitDropDownList(this.drpIdcardType, "ZHENGJIAN_TYPE", true);
            BasicDataService.InitBasicDataProjectUnitDropDownList(this.drpPoliticsStatus, "POLITICAL_LANDSCAPE", true);
            BasicDataService.InitBasicDataProjectUnitDropDownList(this.drpNation, "MINZU_TYPE", true);
            SynchroSetService.InitCountryDropDownList(this.drpCountryCode, true);
            Funs.FineUIPleaseSelect(this.drpProvinceCode);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPersonName.Text))
            {
                ShowNotify("人员姓名不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpIdcardType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择证件类型！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtIdentityCard.Text))
            {
                ShowNotify("证件号码不能为空！", MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrEmpty(this.txtIdcardStartDate.Text))
            //{
            //    ShowNotify("证件开始日期不能为空！", MessageBoxIcon.Warning);
            //    return;
            //}
            //if (this.txtIdcardEndDate.Enabled == true && string.IsNullOrEmpty(this.txtIdcardEndDate.Text.Trim()))
            //{
            //    ShowNotify("证件有效日期不能为空！", MessageBoxIcon.Warning);
            //    return;
            //}
            if (this.drpTeamGroup.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择所属班组！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpPost.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择所属岗位！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtInTime.Text))
            {
                ShowNotify("入场时间不能为空！", MessageBoxIcon.Warning);
                return;
            }
            SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void SaveData()
        {
            string pefx = string.Empty;
            Model.Base_Project project = ProjectService.GetProjectByProjectId(this.ProjectId);
            string projectCode = string.Empty;
            if (project != null)
            {
                projectCode = project.ProjectCode;
            }
            pefx = projectCode + "-";

            var unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
            if (unit != null && !string.IsNullOrEmpty(unit.UnitCode))
            {
                pefx = pefx + unit.UnitCode + "";
            }
            else
            {
                pefx = pefx + "000";
            }

            Model.SitePerson_Person person = new Model.SitePerson_Person
            {
                Sex = this.rblSex.SelectedValue,
                ProjectId = this.ProjectId
            };
            //if (this.drpUnit.SelectedValue != BLL.Const._Null)
            //{
            //    person.UnitId = this.drpUnit.SelectedValue;
            //}
            if (!string.IsNullOrEmpty(this.UnitId))
            {
                person.UnitId = this.UnitId;
            }
            if (this.drpIdcardType.SelectedValue != BLL.Const._Null)
            {
                person.IdcardType = this.drpIdcardType.SelectedValue;
            }
            if (this.drpTeamGroup.SelectedValue != BLL.Const._Null)
            {
                person.TeamGroupId = this.drpTeamGroup.SelectedValue;
            }
            if (!string.IsNullOrWhiteSpace(String.Join(",", this.txtWorkArea.Values)))
            {
                person.WorkAreaId = string.Join(",", txtWorkArea.Values);
            }
            if (this.drpPost.SelectedValue != BLL.Const._Null)
            {
                person.WorkPostId = this.drpPost.SelectedValue;
            }
            if (this.drpPosition.SelectedValue != BLL.Const._Null)
            {
                person.PositionId = this.drpPosition.SelectedValue;
            }
            if (this.drpTitle.SelectedValue != BLL.Const._Null)
            {
                person.PostTitleId = this.drpTitle.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.rblIsUsed.SelectedValue))
            {
                person.IsUsed = Convert.ToBoolean(this.rblIsUsed.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.rblIsCardUsed.SelectedValue))
            {
                person.IsCardUsed = Convert.ToBoolean(this.rblIsCardUsed.SelectedValue);
            }
            person.IdcardAddress = this.txtIdcardAddress.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                person.CardNo = this.txtCardNo.Text.Trim();
            }
            person.PersonName = this.txtPersonName.Text.Trim();

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text))
            {
                person.IdentityCard = this.txtIdentityCard.Text.Trim();
            }
            if (!string.IsNullOrEmpty(this.txtIdcardStartDate.Text.Trim()))
            {
                person.IdcardStartDate = Convert.ToDateTime(this.txtIdcardStartDate.Text.Trim());
            }
            person.IdcardForever = this.rblIdcardForever.SelectedValue;
            if (!string.IsNullOrEmpty(this.txtIdcardEndDate.Text.Trim()))
            {
                person.IdcardEndDate = Convert.ToDateTime(this.txtIdcardEndDate.Text.Trim());
            }
            if (this.drpEduLevel.SelectedValue != Const._Null)
            {
                person.EduLevel = this.drpEduLevel.SelectedValue;
            }
            if (this.drpPoliticsStatus.SelectedValue != Const._Null)
            {
                person.PoliticsStatus = this.drpPoliticsStatus.SelectedValue;
            }
            if (this.drpNation.SelectedValue != Const._Null)
            {
                person.Nation = this.drpNation.SelectedValue;
            }
            if (this.drpCountryCode.SelectedValue != Const._Null)
            {
                person.CountryCode = this.drpCountryCode.SelectedValue;
            }
            if (this.drpProvinceCode.SelectedValue != Const._Null)
            {
                person.ProvinceCode = this.drpProvinceCode.SelectedValue;
            }
            if (this.drpMaritalStatus.SelectedValue != Const._Null)
            {
                person.MaritalStatus = this.drpMaritalStatus.SelectedValue;
            }
            if (this.drpMainCNProfessional.SelectedValue != Const._Null)
            {
                person.MainCNProfessionalId = this.drpMainCNProfessional.SelectedValue;
            }
            foreach (var item in this.drpViceCNProfessional.SelectedValueArray)
            {
                var cn = BLL.CNProfessionalService.GetCNProfessional(item);
                if (cn != null)
                {
                    if (string.IsNullOrEmpty(person.ViceCNProfessionalId))
                    {
                        person.ViceCNProfessionalId = cn.CNProfessionalId;
                    }
                    else
                    {
                        person.ViceCNProfessionalId += "," + cn.CNProfessionalId;
                    }
                }
            }
            person.Address = this.txtAddress.Text.Trim();
            person.Telephone = this.txtTelephone.Text.Trim();
            person.OutResult = this.txtOutResult.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtInTime.Text.Trim()))
            {
                person.InTime = Convert.ToDateTime(this.txtInTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOutTime.Text.Trim()))
            {
                person.OutTime = Convert.ToDateTime(this.txtOutTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtBirthday.Text.Trim()))
            {
                person.Birthday = Convert.ToDateTime(this.txtBirthday.Text.Trim());
            }
            if (!string.IsNullOrEmpty(imgPhoto.ImageUrl) && imgPhoto.ImageUrl != "~/res/images/blank_150.png")
            {
                person.PhotoUrl = imgPhoto.ImageUrl.Replace("~/", "");
                person.HeadImage = AttachFileService.SetImageToByteArray(Funs.RootPath + person.PhotoUrl);
            }
            else
            {
                person.PhotoUrl = null;
                person.HeadImage = null;
            }

            if (!string.IsNullOrEmpty(this.txtAuditorDate.Text))
            {
                person.AuditorDate = Convert.ToDateTime(this.txtAuditorDate.Text);
            }
            if (this.drpAuditor.SelectedValue != BLL.Const._Null)
            {
                person.AuditorId = this.drpAuditor.SelectedValue;
            }
            person.IsForeign = this.ckIsForeign.Checked;
            person.IsOutside = this.ckIsOutside.Checked;
            if (string.IsNullOrEmpty(PersonId))
            {
                if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
                {
                    int cardNoCount = BLL.PersonService.GetPersonCountByCardNo(this.ProjectId, this.txtCardNo.Text.Trim());

                    if (cardNoCount > 0)
                    {
                        ShowNotify("此卡号已存在，不能重复！", MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(this.txtIdentityCard.Text))
                {
                    person.IdentityCard = this.txtIdentityCard.Text.Trim();
                    var identityCardCount = PersonService.GetPersonCountByIdentityCard(this.txtIdentityCard.Text.Trim(), this.CurrUser.LoginProjectId);
                    if (identityCardCount != null)
                    {
                        ShowNotify("此身份证号已存在，不能重复！", MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (!BLL.PersonService.IsExistPersonByUnit(this.UnitId, this.txtPersonName.Text.Trim(), this.ProjectId))
                {
                    this.PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                    person.PersonId = this.PersonId;
                    BLL.PersonService.AddPerson(person);
                }

                BLL.LogService.AddSys_Log(this.CurrUser, person.PersonName, person.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                var getPerson = BLL.PersonService.GetPersonById(PersonId);
                if (getPerson != null)
                { person.FromPersonId = getPerson.FromPersonId; }

                person.PersonId = PersonId;
                BLL.PersonService.UpdatePerson(person);
                //判断并更新项目用户的主副专业信息
                var projectUser = BLL.ProjectUserService.GetProjectUserByProjectIdAndIdentityCard(this.ProjectId, person.IdentityCard);
                if (projectUser != null)
                {
                    projectUser.MainCNProfessionalId = person.MainCNProfessionalId;
                    projectUser.ViceCNProfessionalId = person.ViceCNProfessionalId;
                    BLL.ProjectUserService.UpdateProjectUser(projectUser);
                }
                BLL.LogService.AddSys_Log(this.CurrUser, person.PersonName, person.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
            }

            if (!string.IsNullOrEmpty(person.PersonId))
            {
                ////更新特岗人员资质
                var personQuality = BLL.PersonQualityService.GetPersonQualityByPersonId(person.PersonId);
                if (personQuality != null)
                {
                    if (this.drpCertificate.SelectedValue != BLL.Const._Null)
                    {
                        personQuality.CertificateId = this.drpCertificate.SelectedValue;
                        personQuality.CertificateName = this.drpCertificate.SelectedItem.Text;
                    }
                    personQuality.CertificateNo = this.txtCertificateCode.Text.Trim();
                    personQuality.LimitDate = Funs.GetNewDateTime(this.txtCertificateLimitTime.Text);
                    personQuality.CompileMan = this.CurrUser.UserId;
                    personQuality.CompileDate = DateTime.Now;
                    BLL.PersonQualityService.UpdatePersonQuality(personQuality);
                }
                else
                {
                    Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
                    {
                        PersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_PersonQuality)),
                        PersonId = person.PersonId,
                        CompileMan = this.CurrUser.UserId,
                        CompileDate = DateTime.Now
                    };
                    if (this.drpCertificate.SelectedValue != BLL.Const._Null)
                    {
                        newPersonQuality.CertificateId = this.drpCertificate.SelectedValue;
                        newPersonQuality.CertificateName = this.drpCertificate.SelectedItem.Text;
                    }
                    newPersonQuality.CertificateNo = this.txtCertificateCode.Text.Trim();
                    newPersonQuality.LimitDate = Funs.GetNewDateTime(this.txtCertificateLimitTime.Text);
                    BLL.PersonQualityService.AddPersonQuality(newPersonQuality);
                }
            }
        }
        #endregion

        #region  上传照片
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;
                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！");
                    return;
                }
                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                string url = "~/FileUpload/PersonBaseInfo/" + DateTime.Now.Year + "-" + DateTime.Now.Month + "/";
                filePhoto.SaveAs(Server.MapPath(url + fileName));
                imgPhoto.ImageUrl = url + fileName;
                // 清空文件上传组件
                filePhoto.Reset();
            }
        }
        #endregion

        #region 验证身份证 卡号是否存在
        /// <summary>
        /// 验证身份证 卡号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                var q = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.CardNo == this.txtCardNo.Text.Trim() && (x.PersonId != this.PersonId || (this.PersonId == null && x.PersonId != null)));
                if (q != null)
                {
                    ShowNotify("输入的卡号已存在！", MessageBoxIcon.Warning);
                }
            }

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text))
            {
                var q2 = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.IdentityCard == this.txtIdentityCard.Text.Trim() && (x.PersonId != this.PersonId || (this.PersonId == null && x.PersonId != null)));
                if (q2 != null)
                {
                    ShowNotify("输入的身份证号码已存在！", MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 判断按钮权限
        /// <summary>
        /// 判断按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.filePhoto.Hidden = false;
                    this.btnReadIdentityCard.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdentityCard.Text.Trim()))
            {
                Alert.ShowInTop("身份证号码不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtPersonName.Text))
            {
                ShowNotify("人员姓名不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtInTime.Text))
            {
                ShowNotify("入场时间不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.PersonId))
            {
                this.SaveData();
            }
            string strCode = "person$" + this.txtIdentityCard.Text.Trim();
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("~/Controls/SeeQRImage.aspx?PersonId={0}&strCode={1}", this.PersonId, strCode), "二维码查看", 400, 400));
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonId))
            {
                SaveData();
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/IdCardAttachUrl&menuId={1}&strParam=1", this.PersonId, BLL.Const.PersonListMenuId)));
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonBaseInfo&menuId={1}&strParam=2", this.PersonId, BLL.Const.PersonListMenuId)));
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonBaseInfo&menuId={1}&strParam=3", this.PersonId, BLL.Const.PersonListMenuId)));
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonBaseInfo&menuId={1}&strParam=4", this.PersonId, BLL.Const.PersonListMenuId)));
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonId))
            {
                SaveData();
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/IdCardAttachUrl&menuId={1}&strParam=5", this.PersonId, BLL.Const.PersonListMenuId)));
        }
        #endregion

        protected void btnReadIdentityCard_Click(object sender, EventArgs e)
        {
            var getatt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == this.PersonId + "#1");
            if (getatt != null && !string.IsNullOrEmpty(getatt.AttachUrl))
            {
                string url = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + "/" + getatt.AttachUrl;
                string idInfo = APIIDCardInfoService.ReadIDCardInfo(url);
                if (!string.IsNullOrEmpty(idInfo))
                {
                    JObject obj = JObject.Parse(idInfo);
                    string errcode = obj["errcode"].ToString();
                    if (errcode == "0")
                    {
                        string name = obj["name"].ToString();
                        if (!string.IsNullOrEmpty(name))
                        {
                            this.txtPersonName.Text = name;
                        }
                        string id = obj["id"].ToString();
                        if (!string.IsNullOrEmpty(id))
                        {
                            this.txtIdentityCard.Text = id;
                        }
                        string addr = obj["addr"].ToString();
                        if (!string.IsNullOrEmpty(addr))
                        {
                            this.txtAddress.Text = addr;
                        }
                        string gender = obj["gender"].ToString();
                        if (!string.IsNullOrEmpty(gender))
                        {
                            this.rblSex.SelectedValue = gender == "女" ? "2" : "1";
                        }
                        // string nationality = obj["nationality"].ToString();
                    }
                }
            }
        }

        protected void rblIdcardForever_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblIdcardForever.SelectedValue == "Y")
            {
                this.txtIdcardEndDate.Text = string.Empty;
                this.txtIdcardEndDate.Enabled = false;
                this.txtIdcardEndDate.ShowRedStar = false;
                this.txtIdcardEndDate.Required = false;
            }
            else
            {
                this.txtIdcardEndDate.Enabled = true;
                this.txtIdcardEndDate.ShowRedStar = true;
                this.txtIdcardEndDate.Required = true;
            }
        }

        protected void drpCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpCountryCode.SelectedValue != BLL.Const._Null)
            {
                CityService.InitCityDropDownList(this.drpProvinceCode, this.drpCountryCode.SelectedValue, false);
                if (this.drpProvinceCode.Items.Count > 0)
                {
                    this.drpProvinceCode.SelectedIndex = 0;
                }
            }
            else
            {
                this.drpProvinceCode.Items.Clear();
                Funs.FineUIPleaseSelect(this.drpProvinceCode);
                this.drpProvinceCode.SelectedIndex = 0;
            }
        }
    }
}