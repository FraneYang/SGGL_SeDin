using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.HSSE.Check
{
    public partial class CheckSpecialEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckSpecialId
        {
            get
            {
                return (string)ViewState["CheckSpecialId"];
            }
            set
            {
                ViewState["CheckSpecialId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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
        /// 定义集合
        /// </summary>
        //private static List<Model.View_CheckSpecialDetail> checkSpecialDetails = new List<Model.View_CheckSpecialDetail>();
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.InitCheckItemSetDropDownList();
                this.CheckSpecialId = Request.Params["CheckSpecialId"];
                var checkSpecial = Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(this.CheckSpecialId);
                if (checkSpecial != null)
                {
                    this.ProjectId = checkSpecial.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }

                    if (!String.IsNullOrEmpty(checkSpecial.CheckType))
                    {
                        this.rbType.SelectedValue = checkSpecial.CheckType;
                    }
                    this.InitCheckItemSetDropDownList();
                    this.txtCheckSpecialCode.Text = CodeRecordsService.ReturnCodeByDataId(this.CheckSpecialId);
                    if (checkSpecial.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkSpecial.CheckTime);
                    }
                    this.txtPartInPersonNames.Text = checkSpecial.PartInPersonNames;
                    if (!string.IsNullOrEmpty(checkSpecial.CheckItemSetId))
                    {
                        this.drpSupCheckItemSet.SelectedValue = checkSpecial.CheckItemSetId;
                    }
                    if (!string.IsNullOrEmpty(checkSpecial.PartInPersonIds))
                    {
                        this.drpPartInPersons.SelectedValueArray = checkSpecial.PartInPersonIds.Split(',');
                    }

                    var checkSpecialDetails = (from x in Funs.DB.View_CheckSpecialDetail
                                               where x.CheckSpecialId == this.CheckSpecialId
                                               orderby x.SortIndex
                                               select x).ToList();
                    if (checkSpecialDetails.Count() > 0)
                    {
                        this.drpSupCheckItemSet.Readonly = true;
                        this.rbType.Readonly = true;
                    }

                    Grid1.DataSource = checkSpecialDetails;
                    Grid1.DataBind();
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckSpecialCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckSpecialMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.drpSupCheckItemSet.SelectedIndex = 0;
                    this.drpSupCheckItemSet.SelectedIndex = 0;
                }

                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();
                // 新增数据初始值
                JObject defaultObj = new JObject
                {
                    { "CheckAreaName", "" },
                    { "UnitName", "" },
                    { "Unqualified", "" },
                    { "CheckItemName", "" },
                    { "CompleteStatusName", "" },
                    { "HandleStepStr", "" },
                    { "HiddenHazardTypeName", "" },
                    { "Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)) }
                };
                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, true);            
            }
        }
        #endregion
        
        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            //检查组成员
            UserService.InitUserDropDownList(this.drpPartInPersons, this.ProjectId, true);           
            ConstValue.InitConstNameDropDownList(this.drpHandleStep, ConstValue.Group_HandleStep, true);      
            ///责任单位
            UnitService.InitUnitNameByProjectIdUnitTypeDropDownList(this.drpWorkUnit, this.ProjectId, Const.ProjectUnitType_2, false);
            ///单位工程
            UnitWorkService.InitUnitWorkNameDropDownList(this.drpCheckArea, this.ProjectId, false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveNew()
        {
            if (string.IsNullOrEmpty(this.CheckSpecialId))
            {
                Model.Check_CheckSpecial checkSpecial = new Model.Check_CheckSpecial
                {
                    CheckSpecialId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecial)),
                    CheckSpecialCode = this.txtCheckSpecialCode.Text.Trim(),
                    ProjectId = this.ProjectId,
                    CompileMan=this.CurrUser.UserId,
                };

                ///组成员
                string partInPersonIds = string.Empty;
                string partInPersons = string.Empty;
                foreach (var item in this.drpPartInPersons.SelectedValueArray)
                {
                    var user = BLL.UserService.GetUserByUserId(item);
                    if (user != null)
                    {
                        partInPersonIds += user.UserId + ",";
                        partInPersons += user.UserName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(partInPersonIds))
                {
                    checkSpecial.PartInPersonIds = partInPersonIds.Substring(0, partInPersonIds.LastIndexOf(","));
                    checkSpecial.PartInPersons = partInPersons.Substring(0, partInPersons.LastIndexOf(","));
                }
                checkSpecial.PartInPersonNames = this.txtPartInPersonNames.Text.Trim();
                checkSpecial.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());

                ////单据状态
                checkSpecial.States = Const.State_0;
                this.CheckSpecialId = checkSpecial.CheckSpecialId;
                checkSpecial.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckSpecialService.AddCheckSpecial(checkSpecial);
                BLL.LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId, BLL.Const.BtnAdd);
            }
        }

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.rbType.SelectedValue == "0" && (string.IsNullOrEmpty(this.drpSupCheckItemSet.SelectedValue) || this.drpSupCheckItemSet.SelectedValue == Const._Null))
            {
                ShowNotify("请选择检查类别！", MessageBoxIcon.Warning);
                return;
            }

            this.SaveData(Const.BtnSubmit);
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.rbType.SelectedValue == "0" && (string.IsNullOrEmpty(this.drpSupCheckItemSet.SelectedValue) || this.drpSupCheckItemSet.SelectedValue == Const._Null))
            {
                ShowNotify("请选择检查类别！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(Const.BtnSave);
        }
        #endregion

        #region 保存方法
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Check_CheckSpecial checkSpecial = new Model.Check_CheckSpecial
            {
                CheckSpecialCode = this.txtCheckSpecialCode.Text.Trim(),
                ProjectId = this.ProjectId,
                PartInPersonNames = this.txtPartInPersonNames.Text.Trim(),
                CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()),
                CompileMan=this.CurrUser.UserId,
                CheckType=this.rbType.SelectedValue,
                ////单据状态
                States = Const.State_0,
            };
            ///组成员
            string partInPersonIds = string.Empty;
            string partInPersons = string.Empty;
            foreach (var item in this.drpPartInPersons.SelectedValueArray)
            {
                var user = BLL.UserService.GetUserByUserId(item);
                if (user != null)
                {
                    partInPersonIds += user.UserId + ",";
                    partInPersons += user.UserName + ",";
                }
            }
            if (!string.IsNullOrEmpty(partInPersonIds))
            {
                checkSpecial.PartInPersonIds = partInPersonIds.Substring(0, partInPersonIds.LastIndexOf(","));
                checkSpecial.PartInPersons = partInPersons.Substring(0, partInPersons.LastIndexOf(","));
            }
            
            if (!string.IsNullOrEmpty(this.drpSupCheckItemSet.SelectedValue) && this.drpSupCheckItemSet.SelectedValue != Const._Null)
            {
                checkSpecial.CheckItemSetId = this.drpSupCheckItemSet.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.CheckSpecialId))
            {
                checkSpecial.CheckSpecialId = this.CheckSpecialId;
                Check_CheckSpecialService.UpdateCheckSpecial(checkSpecial);
                LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId, BLL.Const.BtnModify);
                Check_CheckSpecialDetailService.DeleteCheckSpecialDetails(this.CheckSpecialId);
            }
            else
            {
                checkSpecial.CheckSpecialId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecial));
                this.CheckSpecialId = checkSpecial.CheckSpecialId;
                checkSpecial.CompileMan = this.CurrUser.UserId;
                Check_CheckSpecialService.AddCheckSpecial(checkSpecial);
                LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId, BLL.Const.BtnAdd);
            }

            ShowNotify(this.SaveDetail(type, checkSpecial), MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        ///  保存明细项
        /// </summary>
        private string SaveDetail(string type, Model.Check_CheckSpecial checkSpecial)
        {
            string info = string.Empty;
            List<Model.Check_CheckSpecialDetail> detailLists = new List<Model.Check_CheckSpecialDetail>();
            JArray teamGroupData = Grid1.GetMergedData();
            foreach (JObject teamGroupRow in teamGroupData)
            {
                JObject values = teamGroupRow.Value<JObject>("values");
                int rowIndex = teamGroupRow.Value<int>("index");
                Model.Check_CheckSpecialDetail newDetail = new Model.Check_CheckSpecialDetail
                {
                    CheckSpecialDetailId = SQLHelper.GetNewID(),
                    CheckSpecialId = this.CheckSpecialId,
                    CheckContent = values.Value<string>("CheckItemName"),
                    Unqualified = values.Value<string>("Unqualified"),
                   // WorkArea = values.Value<string>("WorkArea"),
                };
                var getUnit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitName == values.Value<string>("UnitName"));
                if (getUnit != null)
                {
                    newDetail.UnitId = getUnit.UnitId;
                }

                var getUnitWork = Funs.DB.WBS_UnitWork.FirstOrDefault(x => x.UnitWorkName == values.Value<string>("CheckAreaName") && x.ProjectId == this.ProjectId);
                if (getUnitWork != null)
                {
                    newDetail.CheckArea = getUnitWork.UnitWorkId;                    
                }
                string[] strs = values.Value<string>("HandleStepStr").Split(',');
                string handleStep = string.Empty;
                foreach (var item in strs)
                {
                    var getHandleStep = Funs.DB.Sys_Const.FirstOrDefault(x => x.GroupId == ConstValue.Group_HandleStep && x.ConstText == item);
                    if (getHandleStep != null)
                    {
                        handleStep += getHandleStep.ConstValue + ",";
                    }
                }
                if (!string.IsNullOrEmpty(handleStep))
                {
                    handleStep = handleStep.Substring(0, handleStep.LastIndexOf(","));
                }
                newDetail.HandleStep = handleStep;
                if (newDetail.HandleStep.Contains("1"))
                {
                     if (values.Value<string>("HiddenHazardTypeName") == "较大")
                    {
                        newDetail.HiddenHazardType = "2";
                    }
                    else if (values.Value<string>("HiddenHazardTypeName") == "重大")
                    {
                        newDetail.HiddenHazardType = "3";
                    }
                    else
                    {
                        newDetail.HiddenHazardType = "1";
                    }
                }
                if (values.Value<string>("CompleteStatusName") == "已整改")
                {
                    newDetail.CompleteStatus = true;
                    newDetail.CompletedDate = DateTime.Now;
                }
                else
                {
                    newDetail.CompleteStatus = false;
                }
                var getCheckItem = Funs.DB.Technique_CheckItemSet.FirstOrDefault(x => x.SupCheckItem == this.drpSupCheckItemSet.SelectedValue && x.CheckItemName == newDetail.CheckContent);
                if (getCheckItem != null)
                {
                    newDetail.CheckItem = getCheckItem.CheckItemSetId;
                }
                newDetail.SortIndex = rowIndex;
                Check_CheckSpecialDetailService.AddCheckSpecialDetail(newDetail);
                detailLists.Add(newDetail);
            }
            if (type == Const.BtnSubmit)
            {
                var getDails = detailLists.Where(x => x.CompleteStatus == false);
                if (getDails.Count() > 0)
                {
                    if (getDails.FirstOrDefault(x => x.HandleStep == null) != null)
                    {
                        info = "存在待整改问题，没有处理措施！";
                    }
                    else
                    {
                        info = Check_CheckSpecialService.IssueRectification(getDails.ToList(), checkSpecial);
                    }
                }
                else
                {
                    checkSpecial.States = Const.State_2;
                    Check_CheckSpecialService.UpdateCheckSpecial(checkSpecial);
                    info = "提交成功！";
                }
            }
            else
            {
                info = "保存成功！";
            }

            return info;
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CheckSpecialId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckSpecial&menuId={1}", this.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId)));
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript();
        }
        /// <summary>
        /// 删除提示
        /// </summary>
        /// <returns></returns>
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSupCheckItemSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpCheckItem.Items.Clear();
            Technique_CheckItemSetService.InitCheckItemSetNameDropDownList(this.drpCheckItem, "2", this.drpSupCheckItemSet.SelectedValue, false);
        }
        
        protected void drpPartInPersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpPartInPersons.SelectedValueArray = Funs.RemoveDropDownListNull(this.drpPartInPersons.SelectedValueArray);

        }

        protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCheckItemSetDropDownList();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitCheckItemSetDropDownList()
        {
            this.drpSupCheckItemSet.Items.Clear();
            this.drpCheckItem.Items.Clear();
            if (this.rbType.SelectedValue == "1")
            {
                this.drpSupCheckItemSet.SelectedIndex = 0;
                this.drpSupCheckItemSet.Readonly = true;
                Technique_CheckItemSetService.InitCheckItemSetNameDropDownList(this.drpCheckItem, "6", "0", false);                
            }
            else
            {
                this.drpSupCheckItemSet.Readonly = false;
                Technique_CheckItemSetService.InitCheckItemSetDropDownList(this.drpSupCheckItemSet, "2", "0", false);
                this.drpSupCheckItemSet.SelectedIndex = 0;
                Technique_CheckItemSetService.InitCheckItemSetNameDropDownList(this.drpCheckItem, "2", this.drpSupCheckItemSet.SelectedValue, false);
            }

            this.drpCheckItem.SelectedValue = null;
        }

        protected void drpCheckItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpCheckItem.SelectedValueArray = Funs.RemoveDropDownListNull(this.drpCheckItem.SelectedValueArray);
        }
    }
}