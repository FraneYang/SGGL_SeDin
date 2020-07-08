using System;
using System.IO;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;
using WIA;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace FineUIPro.Web.AttachFile
{
    public partial class uploader : PageBase
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
        public string Source
        {
            get
            {
                return (string)ViewState["Source"];
            }
            set
            {
                ViewState["Source"] = value;
            }

        }
        public string Fname
        {
            get
            {
                return (string)ViewState["Fname"];
            }
            set
            {
                ViewState["Fname"] = value;
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
                this.AttachPath = Request.QueryString["path"];
                this.ParamStr = sessionName + "|" + AttachPath;
                this.MenuId = Request.QueryString["menuId"];
                this.Type = Request.Params["type"];
                this.Fname = Request.Params["fname"];//表格名称'
                Source = Request.QueryString["source"];//如果等于1则是文件柜
                //Request.QueryString["type"]; ////类型：0时是上传资源页面，附件权限不需要判断 -1时只查看权限 -2查看集团公司
                this.GetButtonPower();
                this.BindGrid();
                if (!string.IsNullOrWhiteSpace(Fname))
                {
                    fname.Text = HttpUtility.UrlDecode(Fname);
                }
                else
                {
                    fname.Hidden = true;
                }

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
            if (Source == "1")
            {
                Grid1.DataSource = SourceData();
            }
            else
            {
                Grid1.DataSource = GetSourceData();
            }

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
                            string url = BLL.Funs.RootPath + AttachPath + "\\" + savedName;
                            FileInfo info = new FileInfo(url);
                            if (!info.Exists || string.IsNullOrEmpty(savedName))
                            {
                                url = BLL.Funs.RootPath + "Images//Null.jpg";
                                info = new FileInfo(url);
                            }

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
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 文件柜专用
        /// </summary>
        /// <returns></returns>
        private JArray SourceData()
        {
            if (Session[sessionName] == null && !string.IsNullOrEmpty(ToKeyId))
            {
                //Session[sessionName] = new JArray();
                StringBuilder stxt = new StringBuilder();
                IList<Model.AttachFile> sourlist = AttachFileService.Getfilelist(ToKeyId, MenuId);
                if (sourlist.Count > 0)
                {
                    stxt.Append("[");

                    foreach (var sour in sourlist)
                    {
                        int index = sourlist.IndexOf(sour);
                        string str = string.Empty;
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
                                    if (sourlist.Count == 1)
                                    {
                                        str = sour.AttachSource.Substring(0, sour.AttachSource.Length - 1);
                                        str = str.Substring(1);
                                    }
                                    else if (sourlist.Count > 1)
                                    {

                                        var st = sour.AttachSource.Substring(0, sour.AttachSource.Length - 1);
                                        st = st.Substring(1);
                                        if (sourlist.Count - 1 != index)
                                        {
                                            st += ",";
                                        }
                                        str += st;

                                    }

                                }
                            }
                            stxt.Append(str);
                        }
                    }

                    stxt.Append("]");

                    Session[sessionName] = JArray.Parse(stxt.ToString());

                }
            }

            return (JArray)Session[sessionName];
        }


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
                sour = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == ToKeyId);
                //if (!string.IsNullOrEmpty(this.MenuId))
                //{
                //    sour = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == ToKeyId && x.MenuId == this.MenuId);
                //}
                //else
                //{
                //    sour = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == ToKeyId);
                //}

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
                    attachUrl += AttachPath + "\\" + item.Value<string>("savedName") + ",";
                }
                ///保存方法
                this.SaveData(source.ToString(), attachUrl);
                ShowNotify("保存成功!", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="attachUrl"></param>
        private void SaveData(string source, string attachUrl)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            List<Model.AttachFile> sour = new List<Model.AttachFile>();
            if (!string.IsNullOrEmpty(this.MenuId))
            {
                sour = (from x in db.AttachFile where x.ToKeyId == ToKeyId && x.MenuId == this.MenuId select x).ToList();
            }
            else
            {
                sour = (from x in db.AttachFile where x.ToKeyId == ToKeyId select x).ToList();
            }

            if (sour.Count() == 0)
            {
                Model.AttachFile att = new Model.AttachFile
                {
                    AttachFileId = BLL.SQLHelper.GetNewID(typeof(Model.AttachFile)),
                    ToKeyId = ToKeyId,
                    AttachSource = source.ToString(),
                    AttachUrl = attachUrl,
                    MenuId = MenuId
                };
                db.AttachFile.InsertOnSubmit(att);
                db.SubmitChanges();
            }
            else
            {
                Model.AttachFile att = db.AttachFile.FirstOrDefault(x => x.AttachFileId == sour.First().AttachFileId);
                if (att != null)
                {
                    att.ToKeyId = ToKeyId;
                    att.AttachSource = source.ToString();
                    att.AttachUrl = attachUrl;
                    att.MenuId = MenuId;
                    db.SubmitChanges();
                }
            }
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
                                string oldSrouce = string.Empty;
                                string FullPath = string.Empty;
                                Model.AttachFile att = new Model.SGGLDB(Funs.ConnString).AttachFile.FirstOrDefault(x => x.ToKeyId == this.ToKeyId);
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
                if (this.CurrUser != null)
                {
                    var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, MenuId);
                    if (buttonList.Count > 0)
                    {
                        if (buttonList.Contains(BLL.Const.BtnSave) || buttonList.Contains(BLL.Const.BtnAuditing))
                        {
                            this.toolBar.Hidden = false;
                        }
                    }
                }
            }
        }
        #endregion
    }
}