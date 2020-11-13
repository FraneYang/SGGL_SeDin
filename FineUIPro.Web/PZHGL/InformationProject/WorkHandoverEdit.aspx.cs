using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FineUIPro.Web.PZHGL.InformationProject
{
    public partial class WorkHandoverEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string WorkHandoverId
        {
            get
            {
                return (string)ViewState["WorkHandoverId"];
            }
            set
            {
                ViewState["WorkHandoverId"] = value;
            }
        }
        public int ContactImg
        {
            get
            {
                return Convert.ToInt32(ViewState["ContactImg"]);
            }
            set
            {
                ViewState["ContactImg"] = value;
            }
        }


        /// <summary>
        /// 办理类型
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int HandleImg
        {
            get
            {
                return Convert.ToInt32(ViewState["HandleImg"]);
            }
            set
            {
                ViewState["HandleImg"] = value;
            }
        }

        private List<Model.ZHGL_WorkHandoverDetail> details;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkHandoverId = Request.Params["WorkHandoverId"];
                HideOptions.Hidden = true;
                rblIsAgree.Hidden = true;
                BindData();
                UserService.InitUserDropDownList(drpTransferMan, CurrUser.LoginProjectId, true, Const.UnitId_SEDIN);
                UserService.InitUserDropDownList(drpReceiveMan, CurrUser.LoginProjectId, true, Const.UnitId_SEDIN);
                WorkPostService.InitWorkPostNameByTypeDropDownList2(this.drpWorkPost, "1", true);  //加载管理岗位
                HandleImg = 0;
                if (!string.IsNullOrEmpty(WorkHandoverId))
                {
                    details = BLL.WorkHandoverDetailService.GetWorkHandoverDetailsByWorkHandoverId(WorkHandoverId);
                    this.Grid2.DataSource = details;
                    this.Grid2.DataBind();
                    HFWorkHandoverId.Text = WorkHandoverId;
                    Model.ZHGL_WorkHandover workHandover = WorkHandoverService.GetWorkHandoverById(WorkHandoverId);
                    if (!string.IsNullOrEmpty(workHandover.TransferMan))
                    {
                        this.drpTransferMan.SelectedValue = workHandover.TransferMan;
                    }
                    this.txtTransferManDepart.Text = workHandover.TransferManDepart;
                    if (!string.IsNullOrEmpty(workHandover.ReceiveMan))
                    {
                        this.drpReceiveMan.SelectedValue = workHandover.ReceiveMan;
                    }
                    this.txtReceiveManDepart.Text = workHandover.ReceiveManDepart;
                    if (!string.IsNullOrEmpty(workHandover.WorkPostId))
                    {
                        this.drpWorkPost.SelectedValue = workHandover.WorkPostId;
                    }
                    if (workHandover.TransferDate != null)
                    {
                        this.txtTransferDate.Text = string.Format("{0:yyyy-MM-dd}", workHandover.TransferDate);
                    }
                    if (!string.IsNullOrEmpty(workHandover.State))
                    {
                        State = workHandover.State;
                    }
                    else
                    {
                        State = Const.WorkHandover_Compile;
                        HideOptions.Hidden = true;
                        //Url.Visible = false;//附件查看权限-1
                        ContactImg = -1;
                        rblIsAgree.Hidden = true;
                    }
                    if (State != Const.WorkHandover_Complete)
                    {
                        WorkHandoverService.InitHandleType(drpHandleType, false, State);
                    }
                    if (State == Const.WorkHandover_Compile || State == Const.WorkHandover_ReCompile)
                    {
                        HideOptions.Hidden = true;
                        ContactImg = 0;
                        rblIsAgree.Hidden = true;
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                        drpHandleMan.SelectedIndex = 0;
                    }
                    else
                    {
                        //------------
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                        //drpHandleMan.Items.AddRange(UserService.GetAllUserList(CurrUser.LoginProjectId));
                        HideOptions.Hidden = false;
                        //Url.Visible = true; 附件查看权限 - 1
                        ContactImg = -1;
                        rblIsAgree.Hidden = false;
                    }
                    if (drpHandleType.SelectedValue == Const.WorkHandover_Complete)
                    {
                        rblIsAgree.Hidden = false;
                        drpHandleMan.Enabled = false;
                        drpHandleMan.Required = false;

                    }
                    else
                    {
                        drpHandleMan.Items.Clear();
                        UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                        drpHandleMan.Enabled = true;
                        drpHandleMan.Required = true;
                    }
                    if (rblIsAgree.Hidden == false)
                    {
                        Agree();
                    }
                    if (State == Const.WorkHandover_Compile || State == Const.WorkHandover_ReCompile)
                    {
                        HideOptions.Hidden = true;
                    }
                    //设置回复审批场景下的操作
                }
                else
                {
                    State = Const.WorkHandover_Compile;
                    WorkHandoverService.InitHandleType(drpHandleType, false, State);
                    this.txtTransferDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                    drpHandleMan.SelectedValue = this.CurrUser.UserId;
                    Model.SitePerson_Person person = BLL.PersonService.GetPersonByIdentityCard(this.CurrUser.LoginProjectId, this.CurrUser.IdentityCard);
                    if (person != null)
                    {
                        this.drpWorkPost.SelectedValue = person.WorkPostId;
                    }
                    plApprove2.Hidden = true;
                    string unitId = string.Empty;
                }
            }
        }

        private void BindData()
        {
            var table = WorkHandoverApproveService.getListData(WorkHandoverId);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        public void DoEabled()
        {
            txtTransferDate.Enabled = false;
            ContactImg = -1;
        }

        public void DoEdit()
        {
            txtTransferDate.Enabled = true;
            ContactImg = 0;
        }
        /// <summary>
        /// 附件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFWorkHandoverId.Text))   //新增记录
            {
                HFWorkHandoverId.Text = SQLHelper.GetNewID(typeof(Model.ZHGL_WorkHandover));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(
            String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/WorkHandover&menuId={2}",
            ContactImg, HFWorkHandoverId.Text, Const.WorkHandoverMenuId)));
        }

        #region  保存
        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            Model.ZHGL_WorkHandover workHandover = new Model.ZHGL_WorkHandover();
            workHandover.ProjectId = CurrUser.LoginProjectId;
            if (this.drpTransferMan.SelectedValue != BLL.Const._Null)
            {
                workHandover.TransferMan = this.drpTransferMan.SelectedValue;
            }
            workHandover.TransferManDepart = this.txtTransferManDepart.Text.Trim();
            if (this.drpReceiveMan.SelectedValue != BLL.Const._Null)
            {
                workHandover.ReceiveMan = this.drpReceiveMan.SelectedValue;
            }
            workHandover.ReceiveManDepart = this.txtReceiveManDepart.Text.Trim();
            if (this.drpWorkPost.SelectedValue != BLL.Const._Null)
            {
                workHandover.WorkPostId = this.drpWorkPost.SelectedValue;
            }
            workHandover.TransferDate = Funs.GetNewDateTime(this.txtTransferDate.Text.Trim());
            if (saveType == "submit")
            {
                workHandover.State = drpHandleType.SelectedValue.Trim();
            }
            else
            {
                Model.ZHGL_WorkHandover workHandover1 = WorkHandoverService.GetWorkHandoverById(WorkHandoverId);
                if (workHandover1 != null)
                {
                    if (string.IsNullOrEmpty(workHandover1.State))
                    {
                        workHandover.State = Const.WorkHandover_Compile;
                    }
                    else
                    {
                        workHandover.State = workHandover1.State;
                    }
                }
                else
                {
                    workHandover.State = Const.WorkHandover_Compile;
                }
            }
            if (!string.IsNullOrEmpty(WorkHandoverId) && WorkHandoverService.GetWorkHandoverById(Request.Params["WorkHandoverId"]) != null)
            {
                Model.ZHGL_WorkHandover workHandover1 = WorkHandoverService.GetWorkHandoverById(WorkHandoverId);
                Model.ZHGL_WorkHandoverApprove approve1 = WorkHandoverApproveService.GetWorkHandoverApproveByWorkHandoverId(WorkHandoverId);
                if (approve1 != null && saveType == "submit")
                {
                    approve1.IsAgree = Convert.ToBoolean(rblIsAgree.SelectedValue);
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveIdea = txtOpinions.Text.Trim();
                    WorkHandoverApproveService.UpdateWorkHandoverApprove(approve1);
                }
                if (saveType == "submit")
                {
                    Model.ZHGL_WorkHandoverApprove approve = new Model.ZHGL_WorkHandoverApprove();
                    approve.WorkHandoverId = workHandover1.WorkHandoverId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;
                    if (this.drpHandleType.SelectedValue == BLL.Const.WorkHandover_Complete)
                    {
                        approve.ApproveDate = DateTime.Now.AddMinutes(1);
                    }
                    WorkHandoverApproveService.AddWorkHandoverApprove(approve);
                    //APICommonService.SendSubscribeMessage(approve.ApproveMan, "工作交接待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                workHandover.WorkHandoverId = WorkHandoverId;
                WorkHandoverService.UpdateWorkHandover(workHandover);
            }
            else
            {
                if (!string.IsNullOrEmpty(HFWorkHandoverId.Text))
                {
                    workHandover.WorkHandoverId = HFWorkHandoverId.Text;
                }
                else
                {
                    workHandover.WorkHandoverId = SQLHelper.GetNewID(typeof(Model.ZHGL_WorkHandover));
                }
                WorkHandoverService.AddWorkHandover(workHandover);
                if (saveType == "submit")
                {
                    Model.ZHGL_WorkHandoverApprove approve1 = new Model.ZHGL_WorkHandoverApprove();
                    approve1.WorkHandoverId = workHandover.WorkHandoverId;
                    approve1.ApproveDate = DateTime.Now;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.WorkHandover_Compile;
                    WorkHandoverApproveService.AddWorkHandoverApprove(approve1);

                    Model.ZHGL_WorkHandoverApprove approve = new Model.ZHGL_WorkHandoverApprove();
                    approve.WorkHandoverId = workHandover.WorkHandoverId;
                    if (drpHandleMan.SelectedValue != "0")
                    {
                        approve.ApproveMan = drpHandleMan.SelectedValue;
                    }
                    approve.ApproveType = drpHandleType.SelectedValue;

                    WorkHandoverApproveService.AddWorkHandoverApprove(approve);
                    APICommonService.SendSubscribeMessage(approve.ApproveMan, "工作交接待办理", this.CurrUser.UserName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                }
                else
                {
                    Model.ZHGL_WorkHandoverApprove approve1 = new Model.ZHGL_WorkHandoverApprove();
                    approve1.WorkHandoverId = workHandover.WorkHandoverId;
                    approve1.ApproveMan = CurrUser.UserId;
                    approve1.ApproveType = Const.WorkHandover_Compile;
                    WorkHandoverApproveService.AddWorkHandoverApprove(approve1);
                }
            }
            BLL.WorkHandoverDetailService.DeleteMonthSpotCheckDetailsByWorkHandoverId(workHandover.WorkHandoverId);
            jerqueSaveList();
            foreach (var item in details)
            {
                item.WorkHandoverId = workHandover.WorkHandoverId;
                BLL.WorkHandoverDetailService.AddMonthSpotCheckDetail(item);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            LogService.AddSys_Log(CurrUser, this.drpTransferMan.SelectedItem.Text, WorkHandoverId, Const.WorkHandoverMenuId, "工作交接");
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.WorkHandoverMenuId, Const.BtnSubmit))
            {
                if (this.drpTransferMan.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择移交人！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpReceiveMan.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择接收人！", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpWorkPost.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择交接岗位！", MessageBoxIcon.Warning);
                    return;
                }
                SavePauseNotice("submit");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.WorkHandoverMenuId, Const.BtnSave))
            {

                SavePauseNotice("save");
                //Response.Redirect("/check/CheckList.aspx");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void rblIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            Agree();
        }
        /// <summary>
        /// 是否同意的逻辑处理
        /// </summary>
        public void Agree()
        {
            drpHandleType.Items.Clear();
            string State = WorkHandoverService.GetWorkHandoverById(WorkHandoverId).State;
            WorkHandoverService.InitHandleType(drpHandleType, false, State);
            if (rblIsAgree.SelectedValue.Equals("true"))
            {
                drpHandleType.SelectedIndex = 0;
                UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                drpHandleMan.SelectedIndex = 0;
                if (drpHandleType.SelectedValue == Const.WorkHandover_Complete)
                {
                    drpHandleMan.Items.Clear();
                    drpHandleMan.Enabled = false;
                    drpHandleMan.Required = false;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;
                }
            }
            else
            {
                drpHandleMan.Items.Clear();
                drpHandleType.SelectedIndex = 1;
                UserService.InitUserDropDownList(drpHandleMan, CurrUser.LoginProjectId, false, Const.UnitId_SEDIN);
                drpHandleMan.SelectedIndex = 0;
                if (drpHandleType.SelectedValue == Const.WorkHandover_ReCompile)
                {
                    drpHandleMan.Enabled = true;
                    var HandleMan = BLL.WorkHandoverApproveService.GetComplie(this.WorkHandoverId);                    if (HandleMan != null)                    {                        this.drpHandleMan.SelectedValue = HandleMan.ApproveMan;                    }
                    drpHandleMan.Required = true;
                }
                else
                {
                    drpHandleMan.Enabled = true;
                    drpHandleMan.Required = true;

                }
            }
        }

        protected void drpReceiveMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpReceiveMan.SelectedValue != BLL.Const._Null)
            {
                this.drpHandleMan.SelectedValue = this.drpReceiveMan.SelectedValue;
            }
        }

        protected void drpTransferMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpTransferMan.SelectedValue != BLL.Const._Null)
            {
                Model.Sys_User user = BLL.UserService.GetUserByUserId(this.drpTransferMan.SelectedValue);
                if (user != null)
                {
                    Model.SitePerson_Person person = BLL.PersonService.GetPersonByIdentityCard(this.CurrUser.LoginProjectId, user.IdentityCard);
                    if (person != null)
                    {
                        this.drpWorkPost.SelectedValue = person.WorkPostId;
                    }
                }
            }
        }

        #region 明细操作事件
        private void jerqueSaveList()
        {
            details = new List<Model.ZHGL_WorkHandoverDetail>();
            foreach (JObject mergedRow in Grid2.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.ZHGL_WorkHandoverDetail detail = new Model.ZHGL_WorkHandoverDetail();
                detail.WorkHandoverDetailId = this.Grid2.Rows[i].RowID;
                detail.SortIndex = i;
                detail.HandoverContent = values.Value<string>("HandoverContent");
                string num = values.Value<string>("Num");
                if (!string.IsNullOrEmpty(num))
                {
                    detail.Num = Convert.ToInt32(num);
                }
                details.Add(detail);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            jerqueSaveList();
            Model.ZHGL_WorkHandoverDetail detail = new Model.ZHGL_WorkHandoverDetail();
            detail.WorkHandoverDetailId = SQLHelper.GetNewID();
            detail.SortIndex = this.Grid2.Rows.Count;
            details.Add(detail);
            this.Grid2.DataSource = details;
            this.Grid2.DataBind();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid2.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "delete")
            {
                jerqueSaveList();
                foreach (Model.ZHGL_WorkHandoverDetail detail in details)
                {
                    if (detail.WorkHandoverDetailId == itemId)
                    {
                        details.Remove(detail);
                        ////删除附件表
                        BLL.CommonService.DeleteAttachFileById(itemId);
                        break;
                    }
                }
                Grid2.DataSource = details;
                Grid2.DataBind();
            }
            if (e.CommandName == "attchUrl")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ZHGL/WorkHandover&menuId={1}&type={2}", itemId, BLL.Const.WorkHandoverMenuId, HandleImg)));
            }
        }

        #endregion
    }
}