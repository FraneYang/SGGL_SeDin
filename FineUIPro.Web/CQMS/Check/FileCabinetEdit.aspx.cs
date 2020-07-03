using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CQMS.Check
{
    public partial class FileCabinetEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string FileCabinetId
        {
            get
            {
                return (string)ViewState["FileCabinetId"];
            }
            set
            {
                ViewState["FileCabinetId"] = value;
            }
        }
        public string ImgId
        {
            get
            {
                return (string)ViewState["ImgId"];
            }
            set
            {
                ViewState["ImgId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileCabinetId = Request.Params["FileCabinetId"];
                ImgId = "0";
                if (!string.IsNullOrWhiteSpace(Request.Params["action"]))
                {
                    if (Request.Params["action"].Equals("view"))
                    {
                        txtFileCode.Enabled = false;
                        txtFileDate.Enabled = false;
                        txtFileContent.Enabled = false;
                        txtFileCode.Enabled = false;
                        txtFileCode.Enabled = false;
                        btnSave.Hidden = true;
                        ImgId = "-1";
                    }
                }

                if (!string.IsNullOrWhiteSpace(FileCabinetId))
                {
                    HFileCabinetId.Text = FileCabinetId;
                    var getFileCabinet = FileCabinetService.getInfo(FileCabinetId);
                    txtFileCode.Text = getFileCabinet.FileCode;
                    txtFileContent.Text = getFileCabinet.FileContent;
                    txtFileDate.Text = string.Format("{0:yyyy-MM-dd}", getFileCabinet.FileDate);
                }
                else
                {
                    txtFileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    txtFileCode.Text = SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Project_FileCabinet", "FileCode", CurrUser.LoginProjectId);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(CurrUser.LoginProjectId, CurrUser.UserId, Const.FileCabinetMenuId, Const.BtnSave))
            {
                SaveData();
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);

            }
        }

        /// <summary>
        /// 保存开工报告
        /// </summary>
        private void SaveData()
        {
            Model.Project_FileCabinet newFileCabinet = new Model.Project_FileCabinet
            {
                ProjectId = CurrUser.LoginProjectId,
                FileType = "-1",
                FileCode = txtFileCode.Text.Trim(),
                FileContent = txtFileContent.Text.Trim(),
                CreateManId = CurrUser.UserId,
            };
            if (!string.IsNullOrEmpty(txtFileDate.Text.Trim()))
            {
                newFileCabinet.FileDate = Convert.ToDateTime(txtFileDate.Text.Trim());
            }

            if (!string.IsNullOrEmpty(FileCabinetId))
            {
                newFileCabinet.FileCabinetId = FileCabinetId;
                FileCabinetService.UpdateFileCabinet(newFileCabinet);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(HFileCabinetId.Text))
                {
                    newFileCabinet.FileCabinetId = HFileCabinetId.Text;
                }
                else
                {
                    newFileCabinet.FileCabinetId = SQLHelper.GetNewID(typeof(Model.Project_FileCabinet));
                }
                FileCabinetService.AddFileCabinet(newFileCabinet);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            LogService.AddSys_Log(CurrUser, txtFileCode.Text, FileCabinetId, Const.FileCabinetMenuId, "编辑重要文件");
        }

        protected void imgBtnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HFileCabinetId.Text))   //新增记录
            {
                this.HFileCabinetId.Text = SQLHelper.GetNewID(typeof(Model.Project_FileCabinet));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../../AttachFile/webuploader.aspx?type={0}&toKeyId={1}&path=FileUpload/FileCabinet&menuId={2}",
                ImgId, HFileCabinetId.Text, Const.FileCabinetMenuId)));
        }
    }
}