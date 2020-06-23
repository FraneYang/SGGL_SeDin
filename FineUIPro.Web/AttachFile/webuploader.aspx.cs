using System;
using System.IO;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;
using WIA;
using System.Collections.Generic;

namespace FineUIPro.Web.AttachFile
{
    public partial class webuploader : PageBase
    {
        private static readonly string sessionName = "AttachFile.webuploader";
        protected string ParamStr;

        #region 定义项
        public string ToKeyId
        {
            get
            {
                return (string)ViewState["ToKeyId"];
            }
            set
            {
                ViewState["ToKeyId"] = value;
            }
        }

        public string AttachPath
        {
            get
            {
                return (string)ViewState["AttachPath"];
            }
            set
            {
                ViewState["AttachPath"] = value;
            }
        }

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
        public string Type
        {
            get
            {
                return (string)ViewState["Type"];
            }
            set
            {
                ViewState["Type"] = value;
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
                // 删除选中行
                this.btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                this.btnDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;个文件吗？", Grid1.GetSelectedCountReference());
                Session[sessionName] = null;
                this.ToKeyId = Request.QueryString["toKeyId"];
                if (!string.IsNullOrEmpty(Request.QueryString["strParam"]))
                {
                    this.ToKeyId = this.ToKeyId +"#"+ Request.QueryString["strParam"];
                }

                this.AttachPath = Request.QueryString["path"];
                this.ParamStr = sessionName + "|" + AttachPath;
                this.MenuId = Request.QueryString["menuId"];
                this.Type = Request.Params["type"]; 
                //Request.QueryString["type"]; ////类型：0时是上传资源页面，附件权限不需要判断 -1时只查看权限 -2查看集团公司
                this.GetButtonPower();
                this.BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "RebindGrid")
                {
                    this.BindGrid();
                }
            }
        }
        #endregion

        #region BindGrid
        /// <summary>
        /// 绑定GV
        /// </summary>
        private void BindGrid()
        {
            Grid1.DataSource = GetSourceData();
            Grid1.DataBind();
        }

        #endregion

        #region Events
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (string rowId in Grid1.SelectedRowIDArray)
            {
                DeleteRow(rowId);
            }

            BindGrid();
        }

        /// <summary>
        /// 行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                DeleteRow(e.RowID);
                BindGrid();
            }

            if (e.CommandName == "Attach")
            {
                JArray source = GetSourceData();
                for (int i = 0, count = source.Count; i < count; i++)
                {
                    JObject item = source[i] as JObject;
                    if (item.Value<string>("id") == e.RowID)
                    {
                        try
                        {
                            string savedName = item.Value<string>("savedName");
                            string folder = item.Value<string>("folder");
                            string  xnUrl= AttachPath + "\\" + savedName;
                        
                            if (!string.IsNullOrEmpty(folder))
                            {
                                xnUrl = folder + savedName;
                            }

                            string url = Funs.RootPath + xnUrl;
                            if (savedName.Contains("FileUpLoad"))
                            {
                                url = Funs.RootPath + savedName.Replace('/','\\');
                            }
                            FileInfo info = new FileInfo(url);                           
                            if (!info.Exists || string.IsNullOrEmpty(savedName))
                            {
                                url = Funs.RootPath + "Images//Null.jpg";
                                info = new FileInfo(url);
                            }

                            if (Path.GetExtension(savedName) == ".mp4" || Path.GetExtension(savedName).ToLower() == ".mp4" || Path.GetExtension(savedName) == ".m4v")
                            {
                                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../AttachFile/player.aspx?url={0}", xnUrl.Replace('\\', '/'), "播放 - ")));
                            }
                            else
                            {
                                string fileName = Path.GetFileName(url);
                                long fileSize = info.Length;
                                System.Web.HttpContext.Current.Response.Clear();
                                //System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                                System.Web.HttpContext.Current.Response.TransmitFile(url, 0, fileSize);
                                System.Web.HttpContext.Current.Response.Flush();
                                System.Web.HttpContext.Current.Response.End();
                                break;
                            }
                        }
                        catch (Exception)
                        {
                           
                        }
                    }
                }
            }
        }
        #endregion

        #region GetSourceData
        /// <summary>
        /// 得到Session
        /// </summary>
        /// <returns></returns>
        private JArray GetSourceData()
        {
            if (Session[sessionName] == null && !string.IsNullOrEmpty(ToKeyId))
            {
                Session[sessionName] = new JArray();
                Model.AttachFile sour = new Model.AttachFile();
                if (this.MenuId == Const.ProjectPunishNoticeMenuId || this.MenuId == Const.ProjectPunishNoticeStatisticsMenuId)
                {
                    sour = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == ToKeyId && x.MenuId == this.MenuId);
                }
                else
                {
                    sour = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == ToKeyId);
                }

                if (sour != null)
                {
                    string url = sour.AttachUrl.Replace('\\', '/');
                    List<string> list = Funs.GetStrListByStr(url, ',');
                    if (list.Count() > 0)
                    {
                        int i = 0;
                        foreach (var item in list)
                        {
                            string atturl = Funs.RootPath + item.Replace(';', ' ').Trim();
                            if (File.Exists(atturl))
                            {
                                i += 1;
                                break;
                            }
                        }
                        if (i > 0)
                        {
                            Session[sessionName] = JArray.Parse(sour.AttachSource);
                        }
                    }
                }
            }

            return (JArray)Session[sessionName];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowId"></param>
        private void DeleteRow(string rowId)
        {
            JArray source = GetSourceData();
            for (int i = 0, count = source.Count; i < count; i++)
            {
                JObject item = source[i] as JObject;
                if (item.Value<string>("id") == rowId)
                {
                    try
                    {
                        string savedName = item.Value<string>("savedName");
                        File.Delete(Server.MapPath("~/" + AttachPath + "\\" + savedName));
                        BLL.LogService.AddSys_Log(this.CurrUser, "删除附件！", null, this.MenuId, BLL.Const.BtnDelete);
                    }
                    catch (Exception)
                    {
                        // 尝试删除物理文件失败，不做处理
                    }
                    source.RemoveAt(i);
                    break;
                }
            }
            Session[sessionName] = source;
        }
        #endregion

        #region 保存按钮事件
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            JArray source = GetSourceData();
            if (!string.IsNullOrEmpty(source.ToString()))
            {
                string attachUrl = string.Empty;
                for (int i = 0, count = source.Count; i < count; i++)
                {
                    JObject item = source[i] as JObject;
                    if (!string.IsNullOrEmpty(item.Value<string>("folder")))
                    {
                        attachUrl += item.Value<string>("folder") + "/" + item.Value<string>("savedName") + ",";                        
                    }
                    else
                    {
                        attachUrl += AttachPath + "/" + DateTime.Now.ToString("yyyy-MM") + "/" + item.Value<string>("savedName") + ",";
                    }                
                }
                if (!string.IsNullOrEmpty(attachUrl))
                {
                    attachUrl = attachUrl.Substring(0, attachUrl.LastIndexOf(",")).Replace('\\','/');
                }
                ///保存方法
                this.SaveData(source.ToString(), attachUrl);
                ShowNotify("保存成功!", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        /// <summary>
        ///  保存附件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="attachUrl"></param>
        private void SaveData(string source, string attachUrl)
        {
            UploadFileService.SaveAttachUrl(source, attachUrl, MenuId, ToKeyId);
        }

        /// <summary>
        /// 扫描文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImageMagnify_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceManager manager = new DeviceManagerClass();
                Device device = null;
                foreach (DeviceInfo info in manager.DeviceInfos)
                {
                    if (info.Type != WiaDeviceType.ScannerDeviceType)
                        continue;
                    device = info.Connect();
                    break;
                }
                Item item = device.Items[1];
                CommonDialogClass cdc = new WIA.CommonDialogClass();
                ImageFile imageFile = null;
                imageFile = cdc.ShowAcquireImage(WIA.WiaDeviceType.ScannerDeviceType,
                                                 WIA.WiaImageIntent.TextIntent,
                                                 WIA.WiaImageBias.MaximizeQuality,
                                                 "{00000000-0000-0000-0000-000000000000}",
                                                 true,
                                                 true,
                                                 false);
                if (imageFile != null)
                {
                    var buffer = imageFile.FileData.get_BinaryData() as byte[];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(buffer, 0, buffer.Length);
                        string filePath = Server.MapPath("~/") + AttachPath;  ///文件夹
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string name = "\\";
                        var menu = BLL.SysMenuService.GetSysMenuByMenuId(this.MenuId);
                        if (menu != null)
                        {
                            name += menu.MenuName;
 
                        }
                        name += Funs.GetNewFileName() + ".jpg";
                        string url = filePath + name;
                        if (!string.IsNullOrEmpty(url))
                        {
                            using (FileStream fs = new FileStream(url, FileMode.Create, FileAccess.Write))
                            {
                                ms.WriteTo(fs);
                                string attachUrl = AttachPath + name;
                                if (!string.IsNullOrEmpty(attachUrl))
                                {
                                    attachUrl = attachUrl.Replace('/', '\\');
                                }
                                string oldSrouce =string.Empty;
                                string FullPath = string.Empty;
                                Model.AttachFile att = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == this.ToKeyId);
                                if (att != null && !string.IsNullOrEmpty(att.AttachUrl))
                                {
                                    FullPath = att.AttachUrl + "," + attachUrl;
                                    oldSrouce = att.AttachSource;
                                }
                                else
                                {
                                    FullPath = attachUrl;
                                }
                                string source = BLL.UploadFileService.GetSourceByAttachUrl(attachUrl, buffer.Length, oldSrouce);
                                //this.SaveData(source, FullPath); ///保存方法
                                Session[sessionName] = JArray.Parse(source);                                                                 
                            }

                            this.BindGrid();
                            ShowNotify("扫描完成!", MessageBoxIcon.Success);
                           
                        }
                    }
                }
            }
            catch
            {
                ShowNotify("请检查扫描仪是否连接正确!", MessageBoxIcon.Warning);
            }
        } 

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (this.Type == "0")
            {
                this.toolBar.Hidden = false;
            }
            else if (this.Type == "-1")
            {
                this.toolBar.Hidden = true;
            }
            else
            {
                if (this.CurrUser != null && this.MenuId != Const.ProjectPunishNoticeStatisticsMenuId)
                {
                    var buttonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, MenuId);
                    if (buttonList.Count > 0)
                    {
                        if (buttonList.Contains(Const.BtnSave) || buttonList.Contains(Const.BtnAuditing))
                        {
                            this.toolBar.Hidden = false;
                        }
                    }
                }
            }
        }
        #endregion

        protected void btnPlay_Click(object sender, EventArgs e)
        {

        }
    }
}
