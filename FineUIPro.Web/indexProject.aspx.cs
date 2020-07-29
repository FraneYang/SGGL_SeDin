﻿using BLL;
using System;
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

            //// 从Cookie中读取 - 左侧菜单类型
            //HttpCookie menuCookie = Request.Cookies["MenuStyle_Pro"];
            //if (menuCookie != null)
            //{
            //    _menuType = menuCookie.Value;
            //}
            
            //// 从Cookie中读取 - 是否启用紧凑模式
            //HttpCookie menuCompactMode = Request.Cookies["EnableCompactMode_Pro"];
            //if (menuCompactMode != null)
            //{
            //    _compactMode = Convert.ToBoolean(menuCompactMode.Value);
            //}
            //// 从Cookie中读取 - 搜索文本
            //HttpCookie searchText = Request.Cookies["SearchText_Pro"];
            //if (searchText != null)
            //{
            //    _searchText = HttpUtility.UrlDecode(searchText.Value);
            //}

            if (!IsPostBack)
            {
                
            }
        }

        #endregion
        
        #region InitAccordionMenu

        //private Accordion InitAccordionMenu()
        //{
        //    Accordion accordionMenu = new Accordion
        //    {
        //        ID = "accordionMenu",
        //        EnableFill = false,
        //        ShowBorder = false,
        //        ShowHeader = false
        //    };

        //    leftPanel.Items.Add(accordionMenu);
        //    XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        //    XmlNodeList xmlNodes = xmlDoc.SelectNodes("/Tree/TreeNode");
        //    foreach (XmlNode xmlNode in xmlNodes)
        //    {
        //        //if (xmlNode.HasChildNodes)
        //        //{
        //            string accordionPaneTitle = xmlNode.Attributes["Text"].Value;
        //            if (GetIsNewHtml(xmlNode))
        //            {
        //                accordionPaneTitle = "<span class=\"isnew\">" + accordionPaneTitle + "</span>";
        //                if (xmlNode.ParentNode != null)
        //                {
        //                    xmlNode.ParentNode.Attributes["Text"].Value = "<span class=\"isnew\">" + xmlNode.ParentNode.Attributes["Text"].Value + "</span>";
        //                }
        //            }

        //        AccordionPane accordionPane = new AccordionPane
        //        {
        //            Title = accordionPaneTitle,
        //            //Layout = Layout.Fit,
        //            ShowBorder = false
        //        };
        //        var accordionPaneIconAttr = xmlNode.Attributes["Icon"];
        //            if (accordionPaneIconAttr != null)
        //            {
        //                accordionPane.Icon = (Icon)Enum.Parse(typeof(Icon), accordionPaneIconAttr.Value, true);
        //            }

        //            accordionMenu.Items.Add(accordionPane);
        //        Tree innerTree = new Tree
        //        {
        //            ShowBorder = false,
        //            ShowHeader = false,
        //            EnableIcons = false,
        //            AutoScroll = true,
        //            EnableSingleClickExpand = true
        //        };
        //        accordionPane.Items.Add(innerTree);
        //            XmlDocument doc = new XmlDocument();
        //            doc.LoadXml(String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Tree>{0}</Tree>", xmlNode.InnerXml));
        //            ResolveXmlDocument(doc);
        //            // 绑定AccordionPane内部的树控件
        //            innerTree.NodeDataBound += treeMenu_NodeDataBound;
        //            innerTree.PreNodeDataBound += treeMenu_PreNodeDataBound;
        //            innerTree.DataSource = doc;
        //            innerTree.DataBind();
        //        //}
        //    }

        //    return accordionMenu;
        //}

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
               ProjectService.InitAllProjectShortNameDropDownList(this.drpProject,this.CurrUser.UserId, false);
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.drpProject.SelectedValue = Request.Params["projectId"];
                }
                this.MenuSwitchMethod(Request.Params["menuType"]);           
                this.InitMenuStyleButton();
                this.InitMenuModeButton();
                this.InitLangMenuButton();
             
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
            //MenuSwitchMethod(string.Empty);
            this.CurrUser.LoginProjectId = this.drpProject.SelectedValue;
            PageContext.RegisterStartupScript("parent.removeActiveTab();");
        }

        /// <summary>
        ///  功能模块切换方法
        /// </summary>
        /// <param name="type"></param>
        protected void MenuSwitchMethod(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                if (!string.IsNullOrEmpty(this.drpProject.SelectedValue))
                {
                    this.CurrUser.LoginProjectId = this.drpProject.SelectedValue;
                }
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
                        this.Tab1.IFrameUrl = "~/ProjectData/ProjectSet.aspx";
                    }
                }
                else
                {
                    Alert.ShowInParent("您没有此模块操作权限，请联系管理员授权！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                this.CurrUser.LoginProjectId = null;
                this.XmlDataSource1.DataFile = "common/Menu_Personal.xml";
                this.leftPanel.Hidden = true;
                this.Tab1.IFrameUrl = "~/common/mainProject.aspx";
            }

            UserService.UpdateLastUserInfo(this.CurrUser.UserId, type, false, this.drpProject.SelectedValue);            
            InitTreeMenu();
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CurrUser.LoginProjectId)
                && ((this.CurrUser.UnitId == Const.UnitId_SEDIN && this.CurrUser.IsOffice ==true) || this.CurrUser.UserId == Const.sysglyId || this.CurrUser.UserId == Const.hfnbdId))
            {
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
