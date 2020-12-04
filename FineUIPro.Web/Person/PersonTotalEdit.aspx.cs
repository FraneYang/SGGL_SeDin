using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Person
{
    public partial class PersonTotalEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 用户主键
        /// </summary>
        public string PersonTotalId
        {
            get
            {
                return (string)ViewState["PersonTotalId"];
            }
            set
            {
                ViewState["PersonTotalId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 用户编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.PersonTotalId = Request.Params["PersonTotalId"];
                BLL.UserService.InitUserUnitIdDropDownList(drpUser, Const.UnitId_SEDIN, true);
                WorkPostService.InitPersonTotalPostDropDownList(drpRoleName, true);
                if (!string.IsNullOrEmpty(this.PersonTotalId))
                {
                    var totle = BLL.PersonTotalService.GetPersonByPersonTotalId(this.PersonTotalId);
                    if (totle != null)
                    {
                        if (!string.IsNullOrEmpty(totle.UserId))
                        {
                            this.drpUser.SelectedValue = totle.UserId;
                        }
                        if (totle.StartTime.HasValue)
                        {
                            this.txtStartTime.Text = string.Format("{0:yyyy-MM-dd}", totle.StartTime);
                        }
                        if (totle.EndTime.HasValue)
                        {
                            this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", totle.EndTime);
                        }
                        if (!string.IsNullOrEmpty(totle.RoleName))
                        {
                            this.drpRoleName.SelectedValue = totle.RoleName;
                        }
                        this.txtContents.Text = HttpUtility.HtmlDecode(totle.Content);
                    }
                }
                else
                {
                    this.txtContents.Text = HttpUtility.HtmlDecode("&lt;p style=&quot;margin: 8px 0px; text-align: center; text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;strong&gt;&lt;span style=&quot;line-height: 150%; font-family: 黑体; font-size: 21px;&quot;&gt;工作总结&lt;/span&gt;&lt;/strong&gt;&lt;/p&gt;&lt;p style=&quot;margin: 0px 0px 0px 24px; line-height: normal; -ms-layout-grid-mode: both;&quot;&gt;&lt;strong&gt;&lt;span style=&quot;font-family:;&quot;&gt;&lt;span&gt;1&lt;span times=&quot;&quot; new=&quot;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;/span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/strong&gt;&lt;strong&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;员工年度工作情况&lt;/span&gt;&lt;/strong&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;strong&gt;&lt;span style=&quot;line-height: 150%; font-family:;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; 1.1&lt;span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/strong&gt;&lt;strong&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;基本情况&lt;/span&gt;&lt;/strong&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; （&lt;/span&gt;&lt;span style=&quot;line-height: 150%; font-family:;&quot;&gt;1&lt;/span&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;）所在项目名称（或本部），工作岗位及职责，工作分管范围。&lt;/span&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; （&lt;/span&gt;&lt;span style=&quot;line-height: 150%; font-family:;&quot;&gt;2&lt;/span&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;）一年来，所在项目工程进展概述或本部工作进展概述。&lt;/span&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;strong&gt;&lt;span style=&quot;line-height: 150%; font-family:;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; 1.2&lt;span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/strong&gt;&lt;strong&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;工作详述&lt;/span&gt;&lt;/strong&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&lt;strong&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;/strong&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;（1）一年来，在项目施工质量管理、&lt;/span&gt;&lt;span style=&quot;font-family:;&quot;&gt;HSE&lt;/span&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;管理、进度管理、费用管理、技术管理、分包管理、组织协调上开展的工 作或在项目试车技术服务上开展的工作。或者一年来，在部室基础业务建设、精细化&lt;/span&gt;&lt;span style=&quot;font-family:;&quot;&gt;/&lt;/span&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;信息化建设、施工招投标、施工合同、党建、群团等开展的工作。&lt;/span&gt;&lt;/span&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; （2）&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;兼职非施工管理部职责岗位开展的工作。&lt;/span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp; &amp;nbsp;（3）&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;工作重点、难点、亮点，开展的创新措施及取得的效果，收获（经验和教训）等。 &lt;/span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/span&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;strong&gt;&lt;span style=&quot;line-height: 150%; font-family:;&quot;&gt;1.3&lt;span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/strong&gt;&lt;strong&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;工作中发现的问题及建议&lt;/span&gt;&lt;/strong&gt;&lt;/span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/span&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;strong&gt;2&amp;nbsp; &lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;个人自我综合评价&lt;/span&gt;&lt;/strong&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;strong&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &amp;nbsp;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;在自我学习、工作绩效、工作态度、廉洁自律、团队协作上分别做出自我评价。&lt;/span&gt;&lt;/span&gt;&lt;/strong&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;strong&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&lt;br/&gt;&lt;/span&gt;&lt;/span&gt;&lt;/strong&gt;&lt;/p&gt;&lt;p style=&quot;text-indent: 0px; -ms-layout-grid-mode: both;&quot;&gt;&lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;&lt;span style=&quot;line-height: 150%; font-family: 宋体; font-size: 14px;&quot;&gt;&lt;strong&gt;&amp;nbsp;&amp;nbsp;&lt;/strong&gt; &lt;strong&gt;3&amp;nbsp; &lt;span style=&quot;font-family: 宋体; font-size: 14px;&quot;&gt;对公司或部门下一年度工作建议或诉求&lt;/span&gt;&lt;/strong&gt;&lt;/span&gt;&lt;/span&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;/p&gt;");
                    this.drpUser.SelectedValue = this.CurrUser.UserId;
                    DateTime now = DateTime.Now;
                    int year = now.Year;
                    int month = now.Month;
                    if (month == 1)
                    {
                        year = year - 1;
                    }
                    this.txtStartTime.Text = year.ToString() + "-01-01";
                    this.txtEndTime.Text = year.ToString() + "-12-31";
                }
            }
            else
            {
                var eventArgs = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                if (eventArgs.StartsWith("ButtonClick"))
                {
                    string rootPath = Server.MapPath("~/");
                    string path = Const.PersonTotalTemplateUrl;
                    string uploadfilepath = rootPath + path;
                    string fileName = Path.GetFileName(uploadfilepath);
                    FileInfo fileInfo = new FileInfo(uploadfilepath);
                    FileInfo info = new FileInfo(uploadfilepath);
                    long fileSize = info.Length;
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.AddHeader("Content-Length", fileSize.ToString());
                    Response.TransmitFile(uploadfilepath, 0, fileSize);
                    Response.Flush();
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUser.SelectedValue == Const._Null)
            {
                Alert.ShowInParent("请选择人员！", MessageBoxIcon.Warning);
                return;
            }
            if (!BLL.AttachFileService.Getfile(this.PersonTotalId, BLL.Const.PersonTotalMenuId))
            {
                Alert.ShowInParent("请先上传附件！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(Request.Params["PersonTotalId"]))   //新增记录
            {
                var oldPersonTotal = BLL.PersonTotalService.GetPersonByUserIdAndStartTime(this.drpUser.SelectedValue, Funs.GetNewDateTime(this.txtStartTime.Text.Trim()));
                if (oldPersonTotal != null)
                {
                    Alert.ShowInParent("当年度员工总结已存在，无法增加！", MessageBoxIcon.Warning);
                    return;
                }
            }
            Model.PersonTotal newTotal = new Model.PersonTotal
            {
                UserId = this.drpUser.SelectedValue,
                Content = HttpUtility.HtmlEncode(this.txtContents.Text),
                CompiledManId = this.CurrUser.UserId,
                CompiledDate = DateTime.Now,
                RoleName = this.drpRoleName.SelectedValue
            };
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                newTotal.StartTime = Funs.GetNewDateTime(this.txtStartTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                newTotal.EndTime = Convert.ToDateTime(this.txtEndTime.Text.Trim());
            }
            if (string.IsNullOrEmpty(this.PersonTotalId))
            {
                this.PersonTotalId = SQLHelper.GetNewID(typeof(Model.PersonTotal));
                newTotal.PersonTotalId = this.PersonTotalId;
                PersonTotalService.AddPersonTotal(newTotal);
            }
            else
            {
                var Total = BLL.PersonTotalService.GetPersonByPersonTotalId(this.PersonTotalId);
                if (Total == null)
                {
                    newTotal.PersonTotalId = this.PersonTotalId;
                    PersonTotalService.AddPersonTotal(newTotal);
                }
                else
                {
                    PersonTotalService.UpdatePersonTotal(newTotal);
                }

            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUser.SelectedValue != Const._Null)
            {
                var roleId = BLL.UserService.GetUserByUserId(this.drpUser.SelectedValue).RoleId;
                if (!string.IsNullOrEmpty(roleId))
                {
                    var roleName = BLL.RoleService.getRoleNamesRoleIds(roleId);
                    this.drpRoleName.SelectedValue = roleName;
                }
            }
        }

        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonTotalId))
            {
                this.PersonTotalId = SQLHelper.GetNewID(typeof(Model.PersonTotal));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Person&menuId={1}&type=0", PersonTotalId, BLL.Const.PersonTotalMenuId)));
        }
    }
}