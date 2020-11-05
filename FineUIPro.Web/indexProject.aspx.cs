using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace FineUIPro.Web
{
    public partial class indexProject : PageBase
    {
        #region Page_Init

        private string _menuType = "menu";
        //   private bool _compactMode = false;
        private int _examplesCount = 0;
        private string _searchText = "";
        #region Page_Init

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////////
            string themeStr = Request.QueryString["theme"];
            string menuStr = Request.QueryString["menu"];
            if (!String.IsNullOrEmpty(themeStr) || !String.IsNullOrEmpty(menuStr))
            {
                if (!String.IsNullOrEmpty(themeStr))
                {
                    HttpCookie cookie = new HttpCookie("Theme_Pro", "Cupertino")
                    {
                        Expires = DateTime.Now.AddYears(1)
                    };
                    Response.Cookies.Add(cookie);
                }

                if (!String.IsNullOrEmpty(menuStr))
                {
                    HttpCookie cookie = new HttpCookie("MenuStyle_Pro", menuStr)
                    {
                        Expires = DateTime.Now.AddYears(1)
                    };
                    Response.Cookies.Add(cookie);
                }

                PageContext.Redirect("~/default.aspx");
                return;
            }
            ////////////////////////////////////////////////////////////////
            if (!IsPostBack)
            {

            }
        }

        #endregion

        #region InitTreeMenu
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Tree InitTreeMenu()
        {
            Tree treeMenu = new Tree
            {
                ID = "treeMenu",
                ShowBorder = false,
                ShowHeader = false,
                EnableIcons = true,
                AutoScroll = true,
                EnableSingleClickExpand = true
            };
            if (_menuType == "tree" || _menuType == "tree_minimode")
            {
                treeMenu.HideHScrollbar = true;
                treeMenu.ExpanderToRight = true;
                treeMenu.HeaderStyle = true;
                if (_menuType == "tree_minimode")
                {
                    treeMenu.MiniMode = true;
                    treeMenu.MiniModePopWidth = Unit.Pixel(300);

                    leftPanelToolGear.Hidden = true;
                    leftPanelToolCollapse.IconFont = IconFont.ChevronCircleRight;
                    leftPanel.Width = Unit.Pixel(50);
                    leftPanel.CssClass = "minimodeinside";
                }
            }

            leftPanel.Items.Add(treeMenu);
            XmlDocument doc = XmlDataSource1.GetXmlDocument();
            ResolveXmlDocument(doc);
            // 绑定 XML 数据源到树控件
            treeMenu.NodeDataBound += treeMenu_NodeDataBound;
            treeMenu.PreNodeDataBound += treeMenu_PreNodeDataBound;
            treeMenu.DataSource = doc;
            treeMenu.DataBind();
            return treeMenu;
        }

        #endregion

        #region ResolveXmlDocument

        private void ResolveXmlDocument(XmlDocument doc)
        {
            ResolveXmlDocument(doc, doc.DocumentElement.ChildNodes);
        }

        private int ResolveXmlDocument(XmlDocument doc, XmlNodeList nodes)
        {
            // nodes 中渲染到页面上的节点个数
            int nodeVisibleCount = 0;
            foreach (XmlNode node in nodes)
            {
                // Only process Xml elements (ignore comments, etc)
                if (node.NodeType == XmlNodeType.Element)
                {
                    XmlAttribute removedAttr;
                    // 是否叶子节点
                    bool isLeaf = node.ChildNodes.Count == 0;
                    // 所有过滤条件均是对叶子节点而言，而是否显示目录，要看是否存在叶子节点
                    if (isLeaf)
                    {
                        // 存在搜索关键字
                        if (!String.IsNullOrEmpty(_searchText))
                        {
                            XmlAttribute textAttr = node.Attributes["Text"];
                            if (textAttr != null)
                            {
                                if (!textAttr.Value.Contains(_searchText) && isLeaf)
                                {
                                    removedAttr = doc.CreateAttribute("Removed");
                                    removedAttr.Value = "true";
                                    node.Attributes.Append(removedAttr);
                                }
                            }
                        }
                    }

                    // 存在子节点
                    if (!isLeaf)
                    {
                        // 递归
                        int childVisibleCount = ResolveXmlDocument(doc, node.ChildNodes);

                        if (childVisibleCount == 0)
                        {
                            removedAttr = doc.CreateAttribute("Removed");
                            removedAttr.Value = "true";

                            node.Attributes.Append(removedAttr);
                        }
                    }

                    removedAttr = node.Attributes["Removed"];
                    if (removedAttr == null)
                    {
                        nodeVisibleCount++;
                    }
                }
            }

            return nodeVisibleCount;
        }
        #endregion

        #region treeMenu_NodeDataBound treeMenu_PreNodeDataBound
        /// <summary>
        /// 树节点的绑定后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_NodeDataBound(object sender, TreeNodeEventArgs e)
        {
            // 是否叶子节点
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;
            if (!String.IsNullOrEmpty(e.Node.Text))
            {
                if (GetIsNewHtml(e.XmlNode))
                {
                    e.Node.Text = "<span class=\"isnew\">" + e.Node.Text + "</span>";
                    if (e.Node.ParentNode != null)
                    {
                        e.Node.ParentNode.Text = "<span class=\"isnew\">" + e.Node.ParentNode.Text + "</span>";
                    }
                }
            }

            if (isLeaf)
            {
                // 设置节点的提示信息
                e.Node.ToolTip = e.Node.Text;
            }
            // 如果仅显示最新示例，或者存在搜索文本
            if (!String.IsNullOrEmpty(_searchText))
            {
                e.Node.Expanded = true;
            }
        }

        /// <summary>
        /// 树节点的预绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_PreNodeDataBound(object sender, TreePreNodeEventArgs e)
        {
            // 是否叶子节点
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;
            XmlAttribute removedAttr = e.XmlNode.Attributes["Removed"];
            if (removedAttr != null)
            {
                e.Cancelled = true;
            }

            bool isShow = GetIsPowerMenu(e.XmlNode);
            if (!isShow)
            {
                e.Cancelled = true;
            }

            if (isLeaf && !e.Cancelled)
            {
                _examplesCount++;
            }
        }
        #endregion

        #region 该节点是否显示
        /// <summary>
        /// 该节点是否显示
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool GetIsPowerMenu(XmlNode node)
        {
            bool result = true;
            XmlAttribute isNewAttr = node.Attributes["id"];
            if (isNewAttr != null)
            {
                result = BLL.CommonService.ReturnMenuByUserIdMenuId(this.CurrUser.UserId, isNewAttr.Value.ToString(), this.CurrUser.LoginProjectId);
            }

            return result;
        }
        #endregion

        #region GetIsNewHtml 是否必填项
        /// <summary>
        /// 是否必填项
        /// </summary>
        /// <param name="titleText"></param>
        /// <returns></returns>
        private bool GetIsNewHtml(XmlNode xmlNode)
        {
            bool isShow = false;
            if (xmlNode.Attributes["Text"].Value.Contains("*"))
            {
                isShow = true;
            }
            else
            {

            }

            return isShow;
        }
        #endregion

        #endregion

        #region Page_Load
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProjectService.InitAllProjectShortNameDropDownList(this.drpProject, this.CurrUser.UserId, false);
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.drpProject.SelectedValue = Request.Params["projectId"];
                }

                if (!string.IsNullOrEmpty(Request.Params["projectName"]))
                {
                    var getproject = ProjectService.GetProjectByProjectName(Request.Params["projectName"]);
                    if (getproject != null)
                    {
                        this.drpProject.SelectedValue = getproject.ProjectId;
                    }
                }

                this.MenuSwitchMethod(Request.Params["menuType"]);
                this.InitMenuStyleButton();
                this.InitMenuModeButton();
                this.InitLangMenuButton();

                GetWeather();
            }
        }

        private void GetWeather()
        {
            string appkey = "cc220b45380a453a08cb79fd2d40ea3e"; //配置您申请的appkey
                                                                //1.根据城市查询天气
            //string url1 = "http://op.juhe.cn/onebox/weather/query";
            //var parameters1 = new Dictionary<string, string>();
            Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.drpProject.SelectedValue);
            string city = "太原";
            if (project != null && !string.IsNullOrEmpty(project.City))
            {
                city = project.City;
            }
            if (!string.IsNullOrEmpty(city))
            {
                //parameters1.Add("cityname", city); //要查询的城市，如：温州、上海、北京
                //parameters1.Add("key", appkey);//你申请的key
                //parameters1.Add("dtype", ""); //返回数据的格式,xml或json，默认json

                //string result1 = sendPost(url1, parameters1, "get");
                string result = BLL.CommonService.CreateGetHttpResponse("http://apis.juhe.cn/simpleWeather/query?city=" + city + "&key=" + appkey);
                var j2 = JsonConvert.DeserializeObject<dynamic>(result);
                if (j2.reason == "查询成功!")
                {
                    string tem = j2.result.realtime.temperature;
                    string weather = j2.result.realtime.info;
                    if (weather == "多云")
                    {
                        this.btnWeather.IconFont = IconFont.Cloud;
                    }
                    else if (weather == "晴")
                    {
                        this.btnWeather.IconFont = IconFont.SunO;
                    }
                    else if (weather == "阴")
                    {
                        this.btnWeather.IconFont = IconFont.Cloud;
                    }
                    else if (weather.Contains("雪"))
                    {
                        this.btnWeather.IconFont = IconFont.SnowflakeO;
                    }
                    string alltem = j2.result.future[0].temperature;
                    this.btnWeather.Text = tem;
                    string date = DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日";
                    this.btnWeather.ToolTip = date + " " + city + " " + weather + " " + alltem;
                }
            }
        }

        /// <summary>
        /// Http (GET/POST)
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="method">请求方法</param>
        /// <returns>响应内容</returns>
        static string sendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 5000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        /// <summary>
        /// 菜单树样式
        /// </summary>
        private void InitMenuStyleButton()
        {
            string menuStyle = "tree";
            HttpCookie menuStyleCookie = Request.Cookies["MenuStyle_Pro"];
            if (menuStyleCookie != null)
            {
                menuStyle = menuStyleCookie.Value;
            }

            SetSelectedMenuItem(MenuStyle, menuStyle);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitMenuModeButton()
        {
            string menuMode = "normal";
            HttpCookie menuModeCookie = Request.Cookies["MenuMode_Pro"];
            if (menuModeCookie != null)
            {
                menuMode = menuModeCookie.Value;
            }

            SetSelectedMenuItem(MenuMode, menuMode);
        }

        /// <summary>
        /// 加载菜单语言
        /// </summary>
        private void InitLangMenuButton()
        {
            string language = "zh_CN";
            HttpCookie languageCookie = Request.Cookies["Language_Pro"];
            if (languageCookie != null)
            {
                language = languageCookie.Value;
            }

            SetSelectedMenuItem(MenuLang, language);
        }

        /// <summary>
        /// 过滤菜单
        /// </summary>
        /// <param name="menuButton"></param>
        /// <param name="selectedDataTag"></param>
        private void SetSelectedMenuItem(MenuButton menuButton, string selectedDataTag)
        {
            foreach (MenuItem item in menuButton.Menu.Items)
            {
                MenuCheckBox checkBox = (item as MenuCheckBox);
                if (checkBox != null)
                {
                    checkBox.Checked = checkBox.AttributeDataTag == selectedDataTag;
                }
            }
        }
        #endregion

        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Tab1.RefreshIFrame();
            //this.CurrUser.LoginProjectId = this.drpProject.SelectedValue;            
            PageContext.RegisterStartupScript("parent.removeActiveTab();");
            MenuSwitchMethod(this.CurrUser.LastMenuType);
            GetWeather();
        }

        /// <summary>
        ///  功能模块切换方法
        /// </summary>
        /// <param name="type"></param>
        protected void MenuSwitchMethod(string type)
        {
            this.CurrUser.LoginProjectId = this.drpProject.SelectedValue;
            this.XmlDataSource1.DataFile = "common/Menu_Personal.xml";
            this.leftPanel.Hidden = true;
            this.Tab1.IFrameUrl = "~/common/mainProject.aspx";
            this.CurrUser.LastProjectId = null;
            if (!string.IsNullOrEmpty(type))
            {
                this.CurrUser.LastProjectId = this.CurrUser.LoginProjectId;
                if (CommonService.IsHaveSystemPower(this.CurrUser.UserId, type, this.CurrUser.LoginProjectId) || type == Const.Menu_Personal)
                {
                    this.XmlDataSource1.DataFile = "common/" + type + ".xml";
                    this.leftPanel.Hidden = false;
                    this.Tab1.IFrameUrl = "~/common/main" + type + ".aspx";
                    if (type == Const.Menu_Personal)
                    {
                        this.Tab1.IFrameUrl = "~/Personal/PersonalInfo.aspx";
                    }
                    else if (type == Const.Menu_ProjectSet)
                    {
                        this.Tab1.IFrameUrl = "~/ProjectData/ProjectSetView.aspx";
                    }
                    if (type == Const.Menu_PZHGL)
                    {
                        this.Tab1.IFrameUrl = "~/PZHGL/InformationProject/ConstructionPlan.aspx";
                        this.Tab1.Title = "总承包商施工计划";
                    }
                }
                else
                {
                    Alert.ShowInParent("您没有此模块操作权限，请联系管理员授权！", MessageBoxIcon.Warning);
                    return;
                }
            }

            this.CurrUser.LastMenuType = type;
            UserService.UpdateLastUserInfo(this.CurrUser.UserId, type, false, this.CurrUser.LoginProjectId);
            InitTreeMenu();
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CurrUser.LastProjectId)
                && ((this.CurrUser.UnitId == Const.UnitId_SEDIN && this.CurrUser.IsOffice == true) || this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId == Const.hfnbdId))
            {
                UserService.UpdateLastUserInfo(this.CurrUser.UserId, this.CurrUser.LastMenuType, false, this.CurrUser.LoginProjectId);
                this.CurrUser.LastProjectId = this.CurrUser.LoginProjectId;
                PageContext.Redirect("~/index.aspx", "_top");
            }
            else
            {
                this.MenuSwitchMethod(string.Empty);
            }
        }
        protected void btnCQMS_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_CQMS);
        }

        protected void btnPersonal_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_Personal);
        }

        protected void btnProjectSet_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_ProjectSet);
        }

        protected void btnHSSE_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_HSSE);
        }

        protected void btnHJGL_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_HJGL);
        }

        protected void btnPHTGL_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_PHTGL);
        }

        protected void btnTestRun_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_TestRun);
        }

        protected void btnPZHGL_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_PZHGL);
        }

        protected void btnDigitalSite_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_DigitalSite);
        }

        protected void btnPDigData_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_PDigData);
        }

        protected void btnJDGL_Click(object sender, EventArgs e)
        {
            this.MenuSwitchMethod(Const.Menu_JDGL);
        }
    }
}
