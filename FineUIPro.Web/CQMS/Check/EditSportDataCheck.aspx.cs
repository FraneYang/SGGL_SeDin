using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class EditSportDataCheck : PageBase
    {
        #region 公共字段
        /// <summary>
        /// 主键
        /// </summary>
        public string SpotCheckCode
        {
            get
            {
                return (string)ViewState["SpotCheckCode"];
            }
            set
            {
                ViewState["SpotCheckCode"] = value;
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
        /// 图片是否可以编辑 -1查看 0编辑
        /// </summary>
        public int QuestionImg
        {
            get
            {
                return Convert.ToInt32(ViewState["QuestionImg"]);
            }
            set
            {
                ViewState["QuestionImg"] = value;
            }
        }
        /// <summary>
        /// 整改图片
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
        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.SpotCheck_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit1)
                {
                    return "分包负责人确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit2)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit3)
                {
                    return "监理专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit4)
                {
                    return "建设单位确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit5)
                {
                    return "分包专业工程师上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit6)
                {
                    return "总包专业工程师确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit7)
                {
                    return "分包负责人确认";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Audit5R)
                {
                    return "分包专业工程师重新上传资料";
                }
                else if (state.ToString() == BLL.Const.SpotCheck_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        /// <summary>
        /// 明细集合
        /// </summary>
        private static List<Model.Check_SpotCheckDetail> details = new List<Model.Check_SpotCheckDetail>();
        #endregion

        /// <summary>
        /// 处理代办事项
        /// </summary>
        /// <param name="isok"></param>
        /// <param name="handtype"></param>
        protected void setHandType(int op)
        {
            var st = false;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                int i = mergedRow.Value<int>("index");
                GridRow row = Grid1.Rows[i];
                //JObject values = mergedRow.Value<JObject>("values");
                System.Web.UI.WebControls.DropDownList handtype = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleType"));
                System.Web.UI.WebControls.DropDownList handman = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleMan"));
                System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdHandleMan"));
                System.Web.UI.WebControls.HiddenField lblsite = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdState"));
                System.Web.UI.WebControls.Label url = (System.Web.UI.WebControls.Label)(row.FindControl("lblattchUrl"));
                System.Web.UI.WebControls.Button btnOk = (System.Web.UI.WebControls.Button)(row.FindControl("btnDataOK"));
                System.Web.UI.WebControls.Button btnNotOK = (System.Web.UI.WebControls.Button)(row.FindControl("btnNotDataOK"));
                System.Web.UI.WebControls.HiddenField isok = (System.Web.UI.WebControls.HiddenField)(row.FindControl("IsDataOk"));
                System.Web.UI.WebControls.Label lblData = (System.Web.UI.WebControls.Label)(row.FindControl("lblDataOk"));
                System.Web.UI.WebControls.Label lblhandtyp = (System.Web.UI.WebControls.Label)(row.FindControl("lblhandtype"));
                if (lblHandleMan.Value != CurrUser.UserId)
                {
                    //row.RowSelectable = false;
                    row.RowCssClass = "f-grid-cell-uneditable";
                    handtype.Enabled = false;
                    handman.Enabled = false;
                    btnOk.Enabled = false;
                    btnNotOK.Enabled = false;
                    foreach (GridColumn column in Grid1.AllColumns)
                    {
                        Grid1.Rows[i].CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                    }
                    handtype.Visible = false;
                    handman.Visible = false;
                    url.Text = "-1";
                }
                else
                {
                    handtype.Visible = false;
                    lblhandtyp.Visible = true;
                    if (lblsite.Value.Equals(Const.SpotCheck_Audit6) || lblsite.Value.Equals(Const.SpotCheck_Audit7))
                    {

                        btnOk.Visible = true;
                        btnNotOK.Visible = true;
                        url.Text = "-1";
                        lblhandtyp.Visible = false;
                        handman.Items.Clear();
                        handman.Visible = false;
                    }
                    else
                    {
                        btnOk.Visible = false;
                        btnNotOK.Visible = false;
                        url.Text = "0";
                        lblhandtyp.Visible = true;
                        var itemlist = SpotCheckService.GetDHandleTypeByState(lblsite.Value, drpControlPointType.SelectedValue);
                        handtype.Items.AddRange(itemlist);
                        if (drpControlPointType.SelectedValue == "D")
                        {
                            handman.Items.AddRange(UserService.GetMainUserList(CurrUser.LoginProjectId));
                        }
                        else
                        {
                            handman.Items.AddRange(UserService.GetUserByUnitId(CurrUser.LoginProjectId, drpUnit.SelectedValue));
                        }
                    }

                    //if (op == 0) {

                    //        if (State.Equals("true"))
                    //        {
                    //            handtype.Items.Add(new AspNet.ListItem("审批完成", Const.SpotCheck_Complete, false));
                    //            if (handtype.Items.Count == 3)
                    //            {
                    //                handtype.SelectedIndex = 1;

                    //            }
                    //            lblhandtyp.Text = handtype.SelectedItem.Text;
                    //            handman.Items.Clear();
                    //            lblhandtyp.Visible = true;
                    //            handman.Visible = false;
                    //            url.Text = "-1";
                    //        }
                    //        else
                    //        {
                    //            //handtype.Items.Clear();
                    //            //handtype.Items.Add(new AspNet.ListItem("分包专业工程师重新上传资料", Const.SpotCheck_Audit5R, true));
                    //            //handman.Visible = true;
                    //            //handtype.Visible = false;

                    //            handtype.Items.Add(new AspNet.ListItem("分包专业工程师重新上传资料", Const.SpotCheck_Audit5R, true));
                    //            if (handtype.Items.Count == 2)
                    //            {
                    //                handtype.SelectedIndex = 1;

                    //            }
                    //            lblhandtyp.Visible = true;
                    //            lblhandtyp.Text = handtype.SelectedItem.Text;
                    //            handman.Visible = true;
                    //            url.Text = "0";
                    //        }


                    //}
                    if (op == 1)
                    {
                        if ((!string.IsNullOrWhiteSpace(isok.Value)))
                        {
                            if (isok.Value.ToString().Equals("1"))
                            {
                                handtype.Items.Add(new AspNet.ListItem("审批完成", Const.SpotCheck_Complete, false));
                                if (handtype.Items.Count == 3)
                                {
                                    handtype.SelectedIndex = 1;

                                }
                                lblhandtyp.Text = handtype.SelectedItem.Text;
                                handman.Items.Clear();
                                lblhandtyp.Visible = true;
                                handman.Visible = false;
                                url.Text = "-1";
                            }
                            else
                            {
                                //handtype.Items.Clear();
                                //handtype.Items.Add(new AspNet.ListItem("分包专业工程师重新上传资料", Const.SpotCheck_Audit5R, true));
                                //handman.Visible = true;
                                //handtype.Visible = false;

                                handtype.Items.Add(new AspNet.ListItem("分包专业工程师重新上传资料", Const.SpotCheck_Audit5R, true));
                                if (handtype.Items.Count == 2)
                                {
                                    handtype.SelectedIndex = 1;

                                }
                                lblhandtyp.Visible = true;
                                lblhandtyp.Text = handtype.SelectedItem.Text;
                                handman.Visible = true;
                                url.Text = "0";
                            }

                        }
                    }



                    if (handtype.Items.Count > 0)
                    {
                        handman.Items.Clear();
                        if (handtype.SelectedItem.Text.Contains("分包"))
                        {
                            handman.Items.AddRange(UserService.GetUserByUnitId(CurrUser.LoginProjectId, drpUnit.SelectedValue));
                        }
                        else
                        {
                            handman.Items.AddRange(UserService.GetMainUserList(CurrUser.LoginProjectId));
                        }
                    }


                    if (lblsite.Value.Equals(Const.SpotCheck_Complete))
                    {
                        btnOk.Visible = false;
                        btnNotOK.Visible = false;
                        url.Text = "-1";
                        handman.Items.Clear();
                        handman.Enabled = false;
                        row.RowSelectable = false;
                    }
                    //办理步骤到审批完成这一步
                    if (handtype.SelectedValue == Const.SpotCheck_Complete)
                    {
                        handman.Items.Clear();
                        handman.Visible = false;
                    }
                    if (handtype.Items.Count == 1)
                    {
                        handtype.SelectedIndex = 0;
                        lblhandtyp.Text = handtype.SelectedItem.Text;
                    }



                    if (lblsite.Value == Const.SpotCheck_Audit6 || lblsite.Value == Const.SpotCheck_Audit7)
                    {
                        st = true;
                    }




                    Funs.PleaseSelect(handman);
                    Funs.PleaseSelect(handtype);
                    if (lblsite.Value.Equals(Const.SpotCheck_Audit5R) || lblsite.Value.Equals(Const.SpotCheck_Audit5))
                    {
                        if (handtype.Items.Count > 1)
                        {
                            handtype.SelectedIndex = 1;
                        }
                    }



                }
            }
            if (!st)
            {
                this.Grid1.Columns[5].Hidden = true;
            }


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QuestionImg = -1;
                UnitService.InitUnitDropDownList(drpUnit, this.CurrUser.LoginProjectId, false);
                CNProfessionalService.InitCNProfessionalDownList(drpCNProfessional, false);
                UserService.InitUserProjectIdUnitTypeDropDownList(drpJointCheckMans, this.CurrUser.LoginProjectId, Const.ProjectUnitType_1, false);
                UserService.InitUserProjectIdUnitTypeDropDownList(drpJointCheckMans2, this.CurrUser.LoginProjectId, Const.ProjectUnitType_3, false);
                UserService.InitUserProjectIdUnitTypeDropDownList(drpJointCheckMans3, this.CurrUser.LoginProjectId, Const.ProjectUnitType_4, false);
                //dpHandleMan.Readonly = true;
                SpotCheckCode = Request.Params["SpotCheckCode"];
                plApprove1.Hidden = true;
                plApprove2.Hidden = true;
                if (!string.IsNullOrEmpty(SpotCheckCode))
                {
                    this.hdSpotCheckCode.Text = SpotCheckCode;
                    plApprove1.Hidden = false;
                    plApprove2.Hidden = false;

                    var dt = SpotCheckApproveService.getList(SpotCheckCode);
                    if (dt != null)
                    {
                        gvApprove.DataSource = dt;
                        gvApprove.DataBind();
                    }

                    Model.Check_SpotCheck spotCheck = SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);
                    txtDocCode.Text = spotCheck.DocCode;
                    if (!string.IsNullOrEmpty(spotCheck.UnitId))
                    {
                        this.drpUnit.SelectedValue = spotCheck.UnitId;
                    }
                    if (!string.IsNullOrEmpty(spotCheck.CNProfessionalCode))
                    {
                        this.drpCNProfessional.SelectedValue = spotCheck.CNProfessionalCode;
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans))
                    {
                        this.drpJointCheckMans.SelectedValueArray = spotCheck.JointCheckMans.Split(',');
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans2))
                    {
                        this.drpJointCheckMans2.SelectedValueArray = spotCheck.JointCheckMans2.Split(',');
                    }
                    if (!string.IsNullOrEmpty(spotCheck.JointCheckMans3))
                    {
                        this.drpJointCheckMans3.SelectedValueArray = spotCheck.JointCheckMans3.Split(',');
                    }
                    drpControlPointType.SelectedValue = spotCheck.ControlPointType;
                    this.rblCheckDateType.SelectedValue = spotCheck.CheckDateType;
                    if (spotCheck.CheckDateType == "2")
                    {
                        this.txtSpotCheckDate.Label = "开始时间";
                        this.txtSpotCheckDate2.Hidden = false;
                    }
                    if (spotCheck.SpotCheckDate != null)
                    {
                        this.txtSpotCheckDate.Text = string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate);
                    }
                    if (spotCheck.SpotCheckDate2 != null)
                    {
                        this.txtSpotCheckDate2.Text = string.Format("{0:yyyy-MM-dd HH:mm}", spotCheck.SpotCheckDate2);
                    }
                    this.txtCheckArea.Text = spotCheck.CheckArea;
                    if (!string.IsNullOrEmpty(spotCheck.State))
                    {
                        State = spotCheck.State;
                    }
                    else
                    {
                        State = BLL.Const.SpotCheck_Compile;

                    }

                    if (State == BLL.Const.SpotCheck_Audit1 || State == BLL.Const.SpotCheck_Audit2 || State == BLL.Const.SpotCheck_Audit3 || State == BLL.Const.SpotCheck_Audit5R || State == BLL.Const.SpotCheck_Audit4)
                    {
                        this.drpUnit.Enabled = false;
                        this.drpCNProfessional.Enabled = false;
                        txtDocCode.Enabled = false;
                        txtProjectName.Enabled = false;
                        this.drpJointCheckMans.Enabled = false;
                        this.drpJointCheckMans2.Enabled = false;
                        this.drpJointCheckMans3.Enabled = false;
                        this.rblCheckDateType.Enabled = false;
                        this.txtSpotCheckDate.Enabled = false;
                        this.txtSpotCheckDate2.Enabled = false;
                        this.txtCheckArea.Enabled = false;
                    }
                    var list = SpotCheckDetailService.GetShowSpotCheckDetails(SpotCheckCode);
                    this.Grid1.DataSource = list;
                    this.Grid1.DataBind();


                    if (Grid1.Rows.Count > 0)
                    {
                        for (int i = 0; i < Grid1.Rows.Count; i++)
                        {
                            string rowID = Grid1.Rows[i].RowID;
                            if (rowID.Count() > 0)
                            {
                                Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                                if (detail.IsOK == false || detail.IsDataOK == "0")
                                {
                                    Grid1.Rows[i].RowCssClass = " Yellow ";
                                }
                            }

                        }
                        //for (int i = 0; i < Grid1.Rows.Count; i++)
                        //{

                        //}
                        //    System.Web.UI.WebControls.HiddenField lblsite = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdState"));
                        setHandType(0);


                        //setHandelMan();
                    }
                    if (State == BLL.Const.SpotCheck_ReCompile)
                    {
                        for (int i = 0; i < this.Grid1.Rows.Count; i++)
                        {
                            Grid1.Rows[i].CellCssClasses[5] = "f-grid-cell-uneditable";
                        }
                    }

                }

                txtProjectName.Text = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;

                this.drpUnit.Enabled = false;
                this.drpCNProfessional.Enabled = false;
                txtDocCode.Enabled = false;
                txtProjectName.Enabled = false;
                this.drpJointCheckMans.Enabled = false;
                this.drpJointCheckMans2.Enabled = false;
                this.drpJointCheckMans3.Enabled = false;
                this.rblCheckDateType.Enabled = false;
                this.txtSpotCheckDate.Enabled = false;
                this.txtSpotCheckDate2.Enabled = false;
                this.txtCheckArea.Enabled = false;
                drpControlPointType.Enabled = false;

            }
        }
        /// <summary>
        /// 获取共检内容
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertDetailName(object ControlItemAndCycleId)
        {
            string name = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    name = c.ControlItemContent;
                    Model.WBS_WorkPackage w = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(c.WorkPackageId);
                    if (w != null)
                    {
                        name = w.PackageContent + "/" + name;
                        Model.WBS_WorkPackage pw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(w.SuperWorkPackageId);
                        if (pw != null)
                        {
                            name = pw.PackageContent + "/" + name;
                            Model.WBS_WorkPackage ppw = BLL.WorkPackageService.GetWorkPackageByWorkPackageId(pw.SuperWorkPackageId);
                            if (ppw != null)
                            {
                                name = ppw.PackageContent + "/" + name;
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(ppw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                            else
                            {
                                Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(pw.UnitWorkId);
                                if (u != null)
                                {
                                    name = u.UnitWorkName + "/" + name;
                                }
                            }
                        }
                        else
                        {
                            Model.WBS_UnitWork u = BLL.UnitWorkService.GetUnitWorkByUnitWorkId(w.UnitWorkId);
                            if (u != null)
                            {
                                name = u.UnitWorkName + "/" + name;
                            }
                        }
                    }
                }
            }
            return name;
        }
        /// <summary>
        /// 获取控制点级别
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertControlPoint(object ControlItemAndCycleId)
        {
            string controlPoint = string.Empty;
            if (ControlItemAndCycleId != null)
            {
                Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(ControlItemAndCycleId.ToString());
                if (c != null)
                {
                    controlPoint = c.ControlPoint;
                }
            }
            return controlPoint;
        }
        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdSpotCheckCode.Text))   //新增记录
            {
                this.hdSpotCheckCode.Text = SQLHelper.GetNewID(typeof(Model.Check_SpotCheck));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/CQMS/SpotCheck&menuId={2}", QuestionImg, this.hdSpotCheckCode.Text, BLL.Const.SpotDataCheckMenuId)));
        }

        #region 工序验收列表事件
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdIds.ClientID)
                                       + Window1.GetShowReference(String.Format("ShowWBS.aspx")));
        }

        /// <summary>
        /// 获取共检结果
        /// </summary>
        /// <param name="IsOK"></param>
        /// <returns></returns>
        protected string ConvertIsOK(object IsOK)
        {
            string isOK = string.Empty;
            if (IsOK != null)
            {
                if (IsOK.ToString().Equals("1"))
                {
                    isOK = "合格";
                }
                else
                {
                    isOK = "不合格";
                }
            }
            return isOK;
        }

        protected void btnDataOK_Click(object sender, EventArgs e)
        {

            AspNet.Button btn = sender as AspNet.Button;
            Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(btn.CommandArgument);
            if (detail != null)
            {
                detail.IsDataOK = "1";
                detail.DataConfirmDate = DateTime.Now;
                //detail.State = Const.SpotCheck_Complete;
                BLL.SpotCheckDetailService.UpdateSpotCheckDetail(detail);
            }
            this.Grid1.DataSource = BLL.SpotCheckDetailService.GetShowSpotCheckDetails(SpotCheckCode);
            this.Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].RowID;
                if (rowID.Count() > 0)
                {
                    Model.Check_SpotCheckDetail detail1 = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                    if (detail1.IsDataOK == "0")
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
            setHandType(1);
        }

        protected void btnNotDataOK_Click(object sender, EventArgs e)
        {
            AspNet.Button btn = sender as AspNet.Button;
            Model.Check_SpotCheckDetail detail = BLL.SpotCheckDetailService.GetSpotCheckDetail(btn.CommandArgument);
            if (detail != null)
            {
                detail.IsDataOK = "0";
                detail.DataConfirmDate = DateTime.Now;
                //detail.State = Const.SpotCheck_Audit5R;
                BLL.SpotCheckDetailService.UpdateSpotCheckDetail(detail);
            }
            this.Grid1.DataSource = BLL.SpotCheckDetailService.GetShowSpotCheckDetails(SpotCheckCode);
            this.Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string rowID = Grid1.Rows[i].RowID;
                if (rowID.Count() > 0)
                {
                    Model.Check_SpotCheckDetail detail1 = BLL.SpotCheckDetailService.GetSpotCheckDetail(rowID);
                    if (detail1.IsDataOK == "0")
                    {
                        Grid1.Rows[i].RowCssClass = " Yellow ";
                    }
                }
            }
            setHandType(1);
        }

        /// <summary>
        /// 明细数据验证
        /// </summary>
        protected void validate()
        {
            //if (Grid1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < Grid1.Rows.Count; i++)
            //    {
            //        GridRow row = Grid1.Rows[i];
            //        System.Web.UI.WebControls.DropDownList handtype = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleType"));
            //        System.Web.UI.WebControls.DropDownList handleMan = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleMan"));
            //        System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdHandleMan"));
            //        if (lblHandleMan.Value == CurrUser.UserId)
            //        {
            //            object[] keys = Grid1.DataKeys[i];
            //            var key = keys[0];
            //            if (handleMan.SelectedValue != "0")
            //            {
            //                Model.Check_SpotCheckDetail spdetail = SpotCheckDetailService.GetSpotCheckDetail(key.ToString());
            //                spdetail.State = handtype.SelectedValue;
            //                spdetail.HandleMan = handleMan.SelectedValue;
            //                SpotCheckDetailService.UpdateSpotCheckDetail(spdetail);
            //            }


            //        }

            //    }

            //}
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string itemId = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "delete")
            {
                foreach (Model.Check_SpotCheckDetail detail in details)
                {
                    if (detail.SpotCheckDetailId == itemId)
                    {
                        details.Remove(detail);
                        break;
                    }
                }
                Grid1.DataSource = details;
                Grid1.DataBind();
            }
            //if (e.CommandName == "attchUrl")
            //{

            //    GridRow row = Grid1.Rows[e.RowIndex];
            //    System.Web.UI.WebControls.Label url = (System.Web.UI.WebControls.Label)(row.FindControl("lblattchUrl"));
            //    HandleImg = Convert.ToInt32(url.Text);
            //    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HJGL/SpotCheck&menuId={1}&type={2}", itemId, BLL.Const.SpotDataCheckMenuId, HandleImg)));
            //}
        }


        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string projectId, string userId, string menuId, string buttonName)
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpotDataCheckMenuId, BLL.Const.BtnSave))
            {
                if (this.rblCheckDateType.SelectedValue == "1")
                {
                    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()))
                    {
                        Alert.ShowInTop("请选择共检时间！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()) || string.IsNullOrEmpty(this.txtSpotCheckDate2.Text.Trim()))
                    {
                        Alert.ShowInTop("请选择开始时间和结束时间！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (this.Grid1.Rows.Count == 0)
                {
                    Alert.ShowInTop("共检内容列表不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                SavePauseNotice("save");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                //Response.Redirect("/check/CheckList.aspx");
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpotDataCheckMenuId, BLL.Const.BtnSubmit))
            {
                if (this.rblCheckDateType.SelectedValue == "1")
                {
                    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()))
                    {
                        Alert.ShowInTop("请选择共检时间！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtSpotCheckDate.Text.Trim()) || string.IsNullOrEmpty(this.txtSpotCheckDate2.Text.Trim()))
                    {
                        Alert.ShowInTop("请选择开始时间和结束时间！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (this.Grid1.Rows.Count == 0)
                {
                    Alert.ShowInTop("共检内容列表不能为空！", MessageBoxIcon.Warning);
                    return;
                }
                if (State == BLL.Const.SpotCheck_Audit2)
                {
                    if (BLL.SpotCheckDetailService.GetNotOKSpotCheckDetailBySoptCheckCode(SpotCheckCode) != null)
                    {
                        Alert.ShowInTop("共检内容还有未合格项，无法提交下一步！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                //if (Grid1.Rows.Count > 0)
                //{
                //    for (int i = 0; i < Grid1.Rows.Count; i++)
                //    {
                //        GridRow row = Grid1.Rows[i];
                //        System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdHandleMan"));
                //        object[] keys = Grid1.DataKeys[i];
                //        var key = keys[0];
                //        if (lblHandleMan.Value == CurrUser.UserId)
                //        {
                //            if (!AttachFileService.Getfile(key.ToString(), BLL.Const.SpotDataCheckMenuId))   //办理项未上传附件
                //            {
                //                Alert.ShowInTop("请上传交工资料后再提交！", MessageBoxIcon.Warning);
                //                return;
                //            }
                //        }
                //    }
                //}
                SavePauseNotice("submit");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }

        private string GetStringByArray(string[] array)
        {
            string str = string.Empty;
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str += item + ",";
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        #region 保存处理
        /// <summary>
        /// 保存方法
        /// </summary>
        private void SavePauseNotice(string saveType)
        {
            //循环添加明细审批
            var spotCheck = BLL.SpotCheckService.GetSpotCheckBySpotCheckCode(SpotCheckCode);
            //string jointCheckMans = GetStringByArray(this.drpJointCheckMans.SelectedValueArray);
            //string jointCheckMans2 = GetStringByArray(this.drpJointCheckMans2.SelectedValueArray);
            //string jointCheckMans3 = GetStringByArray(this.drpJointCheckMans3.SelectedValueArray);
            if (!string.IsNullOrEmpty(SpotCheckCode) && spotCheck != null)
            {
                if (Grid1.Rows.Count > 0)
                {
                    for (int i = 0; i < Grid1.Rows.Count; i++)
                    {
                        GridRow row = Grid1.Rows[i];
                        System.Web.UI.WebControls.DropDownList handtype = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleType"));
                        System.Web.UI.WebControls.DropDownList handleMan = (System.Web.UI.WebControls.DropDownList)(row.FindControl("drpHandleMan"));
                        System.Web.UI.WebControls.HiddenField lblHandleMan = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdHandleMan"));
                        System.Web.UI.WebControls.HiddenField lblsite = (System.Web.UI.WebControls.HiddenField)(row.FindControl("hdState"));
                        object[] keys = Grid1.DataKeys[i];
                        var key = keys[0];
                        if (lblHandleMan.Value == CurrUser.UserId)
                        {

                            if (handtype.SelectedValue == Const.SpotCheck_Complete)
                            {
                                Model.Check_SpotCheckDetail spdetail = SpotCheckDetailService.GetSpotCheckDetail(key.ToString());
                                spdetail.State = handtype.SelectedValue;
                                spdetail.HandleMan = string.Empty;
                                SpotCheckDetailService.UpdateSpotCheckDetail(spdetail);
                                if (saveType == "submit")
                                {
                                    var appro = SpotCheckApproveService.GetSpotApproveBySpotCheckDetailId(row.RowID);
                                    if (appro != null && saveType == "submit")
                                    {
                                        appro.ApproveDate = DateTime.Now;
                                        appro.ApproveIdea = txtOpinions.Text.Trim();
                                        appro.Sign = "2";
                                        SpotCheckApproveService.UpdateSpotCheckApprove(appro);
                                    }
                                }
                            }
                            else
                            {
                                if (handleMan.SelectedValue != "0")
                                {
                                    Model.Check_SpotCheckDetail spdetail = SpotCheckDetailService.GetSpotCheckDetail(key.ToString());
                                    spdetail.State = handtype.SelectedValue;
                                    spdetail.HandleMan = handleMan.SelectedValue;
                                    SpotCheckDetailService.UpdateSpotCheckDetail(spdetail);
                                }
                                if (saveType == "submit")
                                {
                                    if (handleMan.SelectedValue != "0")
                                    {

                                        var appro = SpotCheckApproveService.GetSpotApproveBySpotCheckDetailId(row.RowID);

                                        if (appro != null && saveType == "submit")
                                        {
                                            appro.ApproveDate = DateTime.Now;
                                            appro.ApproveIdea = txtOpinions.Text.Trim();
                                            appro.Sign = "2";
                                            SpotCheckApproveService.UpdateSpotCheckApprove(appro);
                                        }

                                        Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
                                        approve.SpotCheckDetailId = key.ToString();
                                        approve.ApproveMan = handleMan.SelectedValue;
                                        approve.ApproveType = handtype.SelectedValue;
                                        approve.SpotCheckCode = SpotCheckCode;
                                        approve.Sign = "2";
                                        SpotCheckApproveService.AddSpotCheckApprove(approve);

                                    }



                                }
                            }

                        }


                    }

                }
                //循环判断明细是否都是已经完成是则处理主表状态已完成
                var details = SpotCheckDetailService.GetShowSpotCheckDetails(SpotCheckCode);
                if (details.Count > 0)
                {
                    var stat = true;
                    foreach (var item in details)
                    {
                        if (item.State != Const.SpotCheck_Complete)
                        {
                            stat = false;
                            break;
                        }
                    }
                    if (stat)
                    {
                        spotCheck.State2 = Const.SpotCheck_Complete;
                        SpotCheckService.UpdateSpotCheck(spotCheck);
                    }
                    else
                    {
                        if (saveType == "submit")  //非最后一步审批完成提交，主表状态为资料验收中
                        {
                            spotCheck.State2 = Const.SpotCheck_Z;
                            SpotCheckService.UpdateSpotCheck(spotCheck);
                        }
                    }
                }
            }
            //else
            //{
            //    spotCheck.CreateMan = this.CurrUser.UserId;

            //    if (saveType == "submit")
            //    {
            //        Model.Check_SpotCheckApprove approve1 = new Model.Check_SpotCheckApprove();
            //        approve1.SpotCheckCode = spotCheck.SpotCheckCode;
            //        approve1.ApproveDate = DateTime.Now;
            //        approve1.ApproveMan = this.CurrUser.UserId;
            //        approve1.ApproveType = BLL.Const.SpotCheck_Compile;
            //        BLL.SpotCheckApproveService.AddSpotCheckApprove(approve1);

            //        Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
            //        approve.SpotCheckCode = spotCheck.SpotCheckCode;



            //        BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
            //    }
            //    else
            //    {
            //        Model.Check_SpotCheckApprove approve1 = new Model.Check_SpotCheckApprove();
            //        approve1.SpotCheckCode = spotCheck.SpotCheckCode;
            //        approve1.ApproveMan = this.CurrUser.UserId;
            //        approve1.ApproveType = BLL.Const.SpotCheck_Compile;
            //        BLL.SpotCheckApproveService.AddSpotCheckApprove(approve1);
            //    }
            //    if (!string.IsNullOrEmpty(jointCheckMans))
            //    {
            //        string[] seeUsers = jointCheckMans.Split(',');
            //        foreach (var seeUser in seeUsers)
            //        {
            //            if (!string.IsNullOrEmpty(seeUser))
            //            {
            //                Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
            //                approve.SpotCheckCode = spotCheck.SpotCheckCode;
            //                approve.ApproveMan = seeUser;
            //                approve.ApproveType = "S";
            //                BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
            //            }
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(jointCheckMans2))
            //    {
            //        string[] seeUsers = jointCheckMans2.Split(',');
            //        foreach (var seeUser in seeUsers)
            //        {
            //            if (!string.IsNullOrEmpty(seeUser))
            //            {
            //                Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
            //                approve.SpotCheckCode = spotCheck.SpotCheckCode;
            //                approve.ApproveMan = seeUser;
            //                approve.ApproveType = "S";
            //                BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
            //            }
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(jointCheckMans3))
            //    {
            //        string[] seeUsers = jointCheckMans3.Split(',');
            //        foreach (var seeUser in seeUsers)
            //        {
            //            if (!string.IsNullOrEmpty(seeUser))
            //            {
            //                Model.Check_SpotCheckApprove approve = new Model.Check_SpotCheckApprove();
            //                approve.SpotCheckCode = spotCheck.SpotCheckCode;
            //                approve.ApproveMan = seeUser;
            //                approve.ApproveType = "S";
            //                BLL.SpotCheckApproveService.AddSpotCheckApprove(approve);
            //            }
            //        }
            //    }

            //    foreach (var detail in details)
            //    {
            //        detail.SpotCheckCode = spotCheck.SpotCheckCode;
            //        BLL.SpotCheckDetailService.AddSpotCheckDetail(detail);
            //    }
            //}
            //BLL.SpotCheckDetailService.DeleteAllSpotCheckDetail(spotCheck.SpotCheckCode);
            BLL.LogService.AddSys_Log(this.CurrUser, spotCheck.DocCode, SpotCheckCode, BLL.Const.SpotDataCheckMenuId, "编辑工序验收记录");
        }
        #endregion

        /// <summary>
        /// 办理人员的自动筛选
        /// </summary>


        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void drpHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rblCheckDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCheckDateType.SelectedValue == "1")
            {
                this.txtSpotCheckDate.Label = "共检时间";
                this.txtSpotCheckDate2.Hidden = true;
            }
            else
            {
                this.txtSpotCheckDate.Label = "开始时间";
                this.txtSpotCheckDate2.Hidden = false;
            }
        }

        protected void attchUrl_Click(object sender, EventArgs e)
        {

            AspNet.LinkButton btn = sender as AspNet.LinkButton;
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                AspNet.LinkButton btn1 = (AspNet.LinkButton)Grid1.Rows[i].FindControl("attchUrl");
                if (btn.ClientID == btn1.ClientID)
                {
                    GridRow row = Grid1.Rows[i];
                    System.Web.UI.WebControls.Label url = (System.Web.UI.WebControls.Label)(row.FindControl("lblattchUrl"));
                    HandleImg = Convert.ToInt32(url.Text);
                }
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/uploader.aspx?toKeyId={0}&path=FileUpload/CQMS/SpotCheck&menuId={1}&type={2}&fname={3}", btn.CommandArgument, BLL.Const.SpotDataCheckMenuId, HandleImg, string.IsNullOrWhiteSpace(btn.Text) ? "" : HttpUtility.UrlEncode(btn.Text))));

        }
    }
}