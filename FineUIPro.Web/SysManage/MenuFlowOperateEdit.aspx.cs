using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class MenuFlowOperateEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId
        {
            get
            {
                return (string)ViewState["MenuId"];
            }
            set
            {
                ViewState["MenuId"] = value;
            }
        }

        /// <summary>
        /// 流程ID
        /// </summary>
        public string FlowOperateId
        {
            get
            {
                return (string)ViewState["FlowOperateId"];
            }
            set
            {
                ViewState["FlowOperateId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MenuId = Request.Params["MenuId"];
                this.FlowOperateId = Request.Params["FlowOperateId"];
                this.BindDropDownBox();
                if (!string.IsNullOrEmpty(this.FlowOperateId))
                {
                    var menuFlowOperate = MenuFlowOperateService.GetMenuFlowOperateByFlowOperateId(this.FlowOperateId);
                    if (menuFlowOperate != null)
                    {
                        this.txtFlowStep.Text = menuFlowOperate.FlowStep.ToString();
                        this.txtGroupNum.Text = menuFlowOperate.GroupNum.ToString();
                        this.txtOrderNum.Text = menuFlowOperate.OrderNum.ToString();

                        this.txtAuditFlowName.Text = menuFlowOperate.AuditFlowName;
                        if (menuFlowOperate.IsFlowEnd != null)
                        {
                            this.IsFlowEnd.Checked = Convert.ToBoolean(menuFlowOperate.IsFlowEnd);
                        }
                        drpRoles.Value = menuFlowOperate.RoleId;
                    }

                    this.SetIsFlowEnd();
                }
                else
                {
                    this.SetFlowStep();
                }
                //if (LicensePublicService.lisenWorkList.Contains(this.MenuId))
                //{
                //    this.txtGroupNum.Hidden = false;
                //    this.txtOrderNum.Hidden = false;
                //    this.lbTemp.Hidden = false;
                //    this.lbTemp.Text = "说明：步骤、组号、组内序号请按照顺序维护，不可断号。";
                //}
            }
        }
        #endregion

        public void SetFlowStep()
        {
            int maxId = 0;
            string str = "SELECT (ISNULL(MAX(FlowStep),0)+1) FROM Sys_MenuFlowOperate WHERE MenuId='" + this.MenuId + "'";
            maxId = SQLHelper.GetIntValue(str);
            this.txtFlowStep.Text = maxId.ToString();
            this.txtGroupNum.Text = "1";
            this.txtOrderNum.Text = "1";
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string mes = checkValues();
            if (!string.IsNullOrEmpty(mes))
            {
                Alert.ShowInParent(mes);
                return;
            }
            Model.Sys_MenuFlowOperate newMenuFlowOperate = new Model.Sys_MenuFlowOperate
            {
                MenuId = this.MenuId,
                FlowStep = Funs.GetNewInt(this.txtFlowStep.Text) ?? 1,
                GroupNum = Funs.GetNewInt(this.txtGroupNum.Text) ?? 1,
                OrderNum = Funs.GetNewInt(this.txtOrderNum.Text) ?? 1,
                AuditFlowName = this.txtAuditFlowName.Text.Trim(),
            };

            string[] roleIds = drpRoles.Values;
            newMenuFlowOperate.RoleId = this.ConvertRole(roleIds);
            newMenuFlowOperate.IsFlowEnd = this.IsFlowEnd.Checked;
            if (string.IsNullOrEmpty(this.FlowOperateId))
            {
                BLL.MenuFlowOperateService.AddAuditFlow(newMenuFlowOperate);
                BLL.LogService.AddSys_Log(this.CurrUser,  newMenuFlowOperate.AuditFlowName, newMenuFlowOperate.FlowOperateId,BLL.Const.SysConstSetMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                newMenuFlowOperate.FlowOperateId = this.FlowOperateId;
                BLL.MenuFlowOperateService.UpdateAuditFlow(newMenuFlowOperate);
                BLL.LogService.AddSys_Log(this.CurrUser, newMenuFlowOperate.AuditFlowName, newMenuFlowOperate.FlowOperateId, BLL.Const.SysConstSetMenuId, BLL.Const.BtnModify);
            }

            ShowNotify("设置成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        
        /// <summary>
        /// 加载角色下拉框
        /// </summary>
        private void BindDropDownBox()
        {
            string strSql = @"SELECT RoleId,RoleName FROM Sys_Role ORDER BY RoleCode";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null);
            rbRoles.DataSource = tb;
            this.rbRoles.DataTextField = "RoleName";
            this.rbRoles.DataValueField = "RoleId";
            rbRoles.DataBind();
        }
       
        /// <summary>
        /// 得到角色ID字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertRole(string[] roleIds)
        {
            string roles = null;
            if (roleIds != null && roleIds.Count() > 0)
            {
                foreach (string roleId in roleIds)
                {
                    roles += roleId + ",";
                }
                if (roles != string.Empty)
                {
                    roles = roles.Substring(0, roles.Length - 1); ;
                }
            }

            return roles;
        }

        #region 是否审核结束
        /// <summary>
        /// 是否审核结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsFlowEnd_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.SetIsFlowEnd();
        }

        private void SetIsFlowEnd()
        {
            if (IsFlowEnd.Checked)
            {
                this.drpRoles.Value = null;
                this.drpRoles.Text = string.Empty;
                this.drpRoles.Hidden = true;
                if (string.IsNullOrEmpty(this.txtAuditFlowName.Text))
                {
                    this.txtAuditFlowName.Text = "完成";
                }
            }
            else
            {
                this.drpRoles.Hidden = false;
                var getIsEnd = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.FlowOperateId != this.FlowOperateId && x.IsFlowEnd == true);
                if (getIsEnd != null)
                {
                    this.IsFlowEnd.Enabled = false;
                }
            }
        }
        #endregion

        protected void txtFlowStep_TextChanged(object sender, EventArgs e)
        {
            int maxId = 0;
            string str = "SELECT (ISNULL(MAX(GroupNum),0)+1) FROM Sys_MenuFlowOperate WHERE MenuId='" + this.MenuId + "' AND FlowStep=" + this.txtFlowStep.Text;
            maxId = SQLHelper.GetIntValue(str);
            this.txtGroupNum.Text = maxId.ToString();
            this.txtOrderNum.Text = "1";
        }

        protected void txtGroupNum_TextChanged(object sender, EventArgs e)
        {
            int maxId = 0;
            string str = "SELECT (ISNULL(MAX(OrderNum),0)+1) FROM Sys_MenuFlowOperate WHERE MenuId='" + this.MenuId + "' AND FlowStep=" + this.txtFlowStep.Text + " AND GroupNum=" + this.txtGroupNum.Text;
            maxId = SQLHelper.GetIntValue(str);
            this.txtOrderNum.Text = maxId.ToString();
        }

        protected string checkValues()
        {
            string mes = string.Empty;
            int FlowStep = Funs.GetNewInt(this.txtFlowStep.Text) ?? 1;
            int GroupNum = Funs.GetNewInt(this.txtGroupNum.Text) ?? 1;
            int OrderNum = Funs.GetNewInt(this.txtOrderNum.Text) ?? 1;            
            var getFlows = from x in Funs.DB.Sys_MenuFlowOperate where x.MenuId ==this.MenuId select x;            
            if (getFlows.Count() > 0)
            {
                if (FlowStep != 1)
                {
                    var sort = getFlows.FirstOrDefault(x => x.FlowStep == FlowStep || x.FlowStep == (FlowStep - 1));
                    if (sort == null)
                    {
                        mes += "步骤断号情况，请修改后再保存。";
                    }
                }
                if (GroupNum != 1)
                {
                    var group = getFlows.FirstOrDefault(x => x.FlowStep == FlowStep && (x.GroupNum == GroupNum || x.GroupNum == (GroupNum - 1)));
                    if (group == null)
                    {
                        mes += "组号断号情况，请修改后再保存。";
                    }
                }
                if (OrderNum != 1)
                {
                    var order = getFlows.FirstOrDefault(x => x.FlowStep == FlowStep && x.GroupNum == GroupNum  && x.OrderNum == (OrderNum-1));
                    if (order == null)
                    {
                        mes += "组内序号断号情况，请修改后再保存。";
                    }
                }
            }
            else
            {
                if (FlowStep != 1)
                {
                    mes += "步骤需从1开始。";
                }
                if (GroupNum != 1)
                {
                    mes += "组号需从1开始。";
                }
                if (OrderNum != 1)
                {
                    mes += "组内序号需从1开始。";
                }
            }

            return mes;
        }
    }
}