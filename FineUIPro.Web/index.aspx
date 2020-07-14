<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FineUIPro.Web.index" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>施工管理信息平台</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link type="text/css" rel="stylesheet" href="~/res/css/default.css" />
    <link href="res/index/css/home.css" rel="stylesheet" />
    <style type="text/css">
        .f-tabstrip-header-clip {
            width: 100%;
            height: 35px;
        }

        .up-wrap {
            height: 55px;
            padding: 0 10px;
        }

        .item-s {
            padding: 0;
        }

        .f-btn .f-btn-text {
            vertical-align: baseline !important;
        }

        .f-state-hover {
            background-color: transparent !important;
        }

        .bgbtn {
            /*background-image: url(../res/images/login.png) !important;*/
            width: 100%;
            height: 36px;
            line-height: 36px;
            border-width: 0;
            color: rgb(0,162,233) !important;
            padding: 0;
            vertical-align: baseline;
            background-color: transparent;
        }
        .activeClick{
            font-weight: 800;
            color: white !important;
        }

            .bgbtn .x-frame-ml, .bgbtn .x-frame-mc, .bgbtn .x-frame-mr,
            .bgbtn .x-frame-tl, .bgbtn .x-frame-tc, .bgbtn .x-frame-tr,
            .bgbtn .x-frame-bl, .bgbtn .x-frame-bc, .bgbtn .x-frame-br,
            .bgbtn a:visited, .bgbtn .f-state-focus {
                background-image: none;
                background-color: transparent;
            }

        .f-state-default, .f-widget-content .f-state-default, .f-widget-header .f-state-default {
            background-image: none;
            background-color: transparent;
        }

        .header .layui-nav {
            padding: 0;
            margin: 0;
        }

        .bgbtntop.f-btn.f-state-default .f-icon, .bgbtntop.f-btn.f-state-hover .f-icon, .bgbtntop.f-btn.f-state-focus .f-icon, .bgbtntop.f-btn.f-state-active .f-icon {
            color: #37a6ff;
        }
         .activeClick .f-icon{
            color: red !important;
        }

        .f-panel, .f-widget-header, .f-tree-headerstyle .f-panel-body {
            background-color: rgb(23, 68, 122);
            color: #fff;
            border: none !important;
        }

        .f-menu-item-text, .f-tree-cell-text, .f-widget-content a, .f-qtip-content {
            color: #fff;
        }

        .f-corner-all {
            background-color: rgb(23, 68, 122);
        }

        .f-state-hover.f-tree-node a {
            color: #37a6ff;
        }

        .f-state-default, .f-widget-content .f-state-default, .f-widget-header .f-state-default {
            border-color: transparent;
        }

        .bgbtn2 {
            position: absolute;
            width: 100%;
            height: 100%;
            left: 0;
            top: 0;
        }

        .f-tabstrip-align-left .f-tab-header.f-first {
            background-color: rgb(23, 68, 122);
            color: #fff;
            border: none !important;
        }
    </style>
    <style type="text/css">
        .titler {
            font-size: smaller;
        }

        .projcet-select {
            float: left;
        }

        .f-field-dropdownlist, .f-field-dropdownlist-pop {
            /*background-color:#f2f5f7;*/
            color: #fff;
        }

        .f-field-dropdownlist-wrap .f-field-textbox {
            /*width:auto;*/
            border: none !important;
        }
    </style>
</head>
<body class="wrap">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"></f:PageManager>
        <f:Panel ID="Panel1" Layout="Region" ShowBorder="false" ShowHeader="false" runat="server">
            <Items>
                <f:ContentPanel ID="topPanel" CssClass="topregion" RegionPosition="Top" ShowBorder="false" ShowHeader="false" EnableCollapse="true" runat="server">
                    <%--头部 开始--%>
                    <f:ContentPanel ID="ContentPanel1" CssClass="bodyregion" ShowBorder="false" ShowHeader="false" runat="server">
                        <div id="header2" class="f-widget-header f-mainheader">
                            <div class="headerwrap">
                                <div class="header">
                                    <div class="baseInfo">
                                        <ul class="layui-nav navSelect">
                                            <li class="layui-nav-item">
                                                <f:Button runat="server" CssClass="bgbtntop" Text="收藏夹" IconFont="StarO" ToolTip="收藏夹"
                                                    EnablePostBack="false" EnableDefaultState="true" EnableDefaultCorner="false" ID="Button19" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                            <li class="layui-nav-item">
                                                <f:Button runat="server" CssClass="bgbtntop" Text="我的" IconFont="User" OnClick="btnPersonal_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" ID="btnPersonal" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                            <li class="layui-nav-item">
                                                <f:Button runat="server" CssClass="bgbtntop" Text="设置" ToolTip="设置" IconFont="Gear" OnClick="btnSysSet_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" ID="btnSysSet" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                            <li class="layui-nav-item">
                                                <f:Button runat="server" CssClass="bgbtntop" Text="退出" ToolTip="退出" IconFont="PowerOff"
                                                    EnablePostBack="false" EnableDefaultState="true" EnableDefaultCorner="false" ID="Button18">
                                                    <Listeners>
                                                        <f:Listener Event="click" Handler="onToolSignOutClick" />
                                                    </Listeners>
                                                </f:Button>
                                            </li>
                                            <li class="layui-nav-item">
                                                <f:Button runat="server" CssClass="bgbtntop" Text="刷新" ToolTip="刷新菜单" IconFont="Retweet" OnClick="btnRetweet_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" ID="btnRetweet" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="up-wrap">
                                    <div class="flex2">
                                        <ul class="item-s">
                                            <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="项目清单" ID="btnProject" OnClick="btnProject_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                                 <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="大数据中心" ID="btnDigData" OnClick="btnDigData_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                              <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="合同管理" ID="btnContract" OnClick="btnContract_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                            <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="本部检查" ID="btnServer" OnClick="btnServer_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="item-big">
                                        <f:Button runat="server" CssClass="bgbtn2" EnablePostBack="true" OnClick="btnHome_Click"
                                            EnableDefaultState="true" EnableDefaultCorner="false" ID="btnHome" OnClientClick="parent.removeActiveTab();">
                                        </f:Button>
                                        <%--<img src="res/index/images/bigtitle3.png" alt="施工管理信息平台" />--%>
                                    </div>
                                    <div class="flex2">
                                        <ul class="item-s">
                                            <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="通知管理" ID="btnNotice" OnClick="btnNotice_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                            <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="员工管理" ID="btnPerson" OnClick="btnPerson_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>                                       
                                            <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="综合管理" ID="btnZHGL" OnClick="btnZHGL_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                            <li>
                                                <f:Button runat="server" CssClass="bgbtn" Text="党工群团" ID="btnParty" OnClick="btnParty_Click"
                                                    EnablePostBack="true" EnableDefaultState="true" EnableDefaultCorner="false" OnClientClick="parent.removeActiveTab();">
                                                </f:Button>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </f:ContentPanel>
                    <%--头部 结束--%>
                </f:ContentPanel>
                <f:Panel ID="leftPanel" CssClass="leftregion" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="3px"
                    ShowBorder="true" Width="250px" ShowHeader="true" Title="系统菜单"
                    EnableCollapse="false" Collapsed="false" Layout="Fit" runat="server">
                    <Tools>
                        <%--自定义展开折叠工具图标--%>
                        <f:Tool ID="leftPanelToolCollapse" runat="server" IconFont="ChevronCircleLeft"
                            EnablePostBack="false" ToolTip="展开/折叠" Hidden="true">
                            <Listeners>
                                <f:Listener Event="click" Handler="onLeftPanelToolCollapseClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool ID="leftPanelToolGear" runat="server" IconFont="Gear" EnablePostBack="false" Hidden="true" ToolTip="设置">
                            <Menu runat="server" ID="menuSettings">
                                <f:MenuButton ID="btnExpandAll" Text="展开菜单" EnablePostBack="false" runat="server">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onExpandAllClick" />
                                    </Listeners>
                                </f:MenuButton>
                                <f:MenuButton ID="btnCollapseAll" Text="折叠菜单" EnablePostBack="false" runat="server">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onCollapseAllClick" />
                                    </Listeners>
                                </f:MenuButton>
                                <f:MenuSeparator runat="server">
                                </f:MenuSeparator>
                                <f:MenuButton runat="server" EnablePostBack="false" ID="MenuMode" Text="显示模式">
                                    <Menu runat="server">
                                        <Items>
                                            <f:MenuCheckBox Text="普通模式" ID="MenuModeNormal" AttributeDataTag="normal" Checked="true" GroupName="MenuMode" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="紧凑模式" ID="MenuModeCompact" AttributeDataTag="compact" GroupName="MenuMode" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="大字体模式" ID="MenuModeLarge" AttributeDataTag="large" GroupName="MenuMode" runat="server">
                                            </f:MenuCheckBox>
                                        </Items>
                                        <Listeners>
                                            <f:Listener Event="checkchange" Handler="onMenuModeCheckChange" />
                                        </Listeners>
                                    </Menu>
                                </f:MenuButton>
                                <f:MenuButton EnablePostBack="false" Text="菜单样式" ID="MenuStyle" runat="server">
                                    <Menu runat="server">
                                        <Items>
                                            <f:MenuCheckBox Text="智能树菜单" ID="MenuStyleTree" AttributeDataTag="tree" GroupName="MenuStyle" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="智能树菜单（默认折叠）" ID="MenuStyleMiniModeTree" AttributeDataTag="tree_minimode" GroupName="MenuStyle" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="树菜单" ID="MenuStylePlainTree" AttributeDataTag="plaintree" GroupName="MenuStyle" runat="server" Checked="true">
                                            </f:MenuCheckBox>
                                            <%-- <f:MenuCheckBox Text="手风琴+树菜单" ID="MenuStyleAccordion" AttributeDataTag="accordion" GroupName="MenuStyle" runat="server">
                                            </f:MenuCheckBox>--%>
                                        </Items>
                                        <Listeners>
                                            <f:Listener Event="checkchange" Handler="onMenuStyleCheckChange" />
                                        </Listeners>
                                    </Menu>
                                </f:MenuButton>
                                <f:MenuButton EnablePostBack="false" Text="语言" ID="MenuLang" runat="server">
                                    <Menu ID="Menu2" runat="server">
                                        <Items>
                                            <f:MenuCheckBox Text="简体中文" ID="MenuLangZHCN" AttributeDataTag="zh_CN" Checked="true" GroupName="MenuLang" runat="server">
                                            </f:MenuCheckBox>
                                        </Items>
                                        <Listeners>
                                            <f:Listener Event="checkchange" Handler="onMenuLangCheckChange" />
                                        </Listeners>
                                    </Menu>
                                </f:MenuButton>
                            </Menu>
                        </f:Tool>
                    </Tools>
                </f:Panel>
                <f:TabStrip ID="mainTabStrip" CssClass="centerregion" RegionPosition="Center" ShowTabHeader="false"
                    ShowBorder="true" EnableTabCloseMenu="true" runat="server">
                    <Tabs>
                        <%--.f-tabstrip-noheader>.f-panel-header--%>
                        <f:Tab ID="Tab1" Title="首页" IconFont="Home"
                            EnableIFrame="true" IFrameUrl="~/common/main.aspx" runat="server">
                        </f:Tab>
                    </Tabs>
                    <Tools>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Refresh" MarginRight="5" CssClass="tabtool" ToolTip="刷新本页" ID="toolRefresh">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolRefreshClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Share" MarginRight="5" CssClass="tabtool" ToolTip="在新标签页中打开" ID="toolNewWindow">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolNewWindowClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Expand" CssClass="tabtool" ToolTip="最大化" ID="toolMaximize" Hidden="true">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolMaximizeClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="SignOut" Hidden="true"
                            CssClass="tabtool" ToolTip="注销" ID="toolSignOut">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolSignOutClick" />
                            </Listeners>
                        </f:Tool>
                    </Tools>
                </f:TabStrip>
            </Items>
        </f:Panel>
        <f:Window ID="windowCustomQuery" Title="自定义查询" Hidden="true" EnableIFrame="true" IFrameUrl="./SysManage/CustomQuery.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1200px" Height="620px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="false"></asp:XmlDataSource>
    </form>
    <script type="text/javascript">
        var toolRefreshClientID = '<%= toolRefresh.ClientID %>';
        var toolNewWindowClientID = '<%= toolNewWindow.ClientID %>';
        var mainTabStripClientID = '<%= mainTabStrip.ClientID %>';
        var windowCustomQueryClientID = '<%= windowCustomQuery.ClientID %>';
        var MenuStyleClientID = '<%= MenuStyle.ClientID %>';
        var MenuLangClientID = '<%= MenuLang.ClientID %>';
        var topPanelClientID = '<%= topPanel.ClientID %>';
        var leftPanelClientID = '<%= leftPanel.ClientID %>';
        var leftPanelToolGearClientID = '<%= leftPanelToolGear.ClientID %>';
        var leftPanelToolCollapseClientID = '<%= leftPanelToolCollapse.ClientID %>';
        var tab1ClientID = '<%= Tab1.ClientID %>';
        // 展开左侧面板
        function expandLeftPanel() {
            var leftPanel = F(leftPanelClientID);

            // 获取左侧树控件实例
            var leftMenuTree = leftPanel.items[0];
            leftMenuTree.miniMode = false;
            leftPanel.el.removeClass('minimodeinside');
            leftPanel.setWidth(220);
            F(leftPanelToolGearClientID).show();
            F(leftPanelToolCollapseClientID).setIconFont('chevron-circle-left');
            // 重新加载树菜单
            leftMenuTree.loadData();
        }

        // 展开左侧面板
        function collapseLeftPanel() {
            var leftPanel = F(leftPanelClientID);
            // 获取左侧树控件实例
            var leftMenuTree = leftPanel.items[0];
            leftMenuTree.miniMode = true;
            leftMenuTree.miniModePopWidth = 220;
            leftPanel.el.addClass('minimodeinside');
            leftPanel.setWidth(50);
            F(leftPanelToolGearClientID).hide();
            F(leftPanelToolCollapseClientID).setIconFont('chevron-circle-right');
            // 重新加载树菜单
            leftMenuTree.loadData();
        }

        // 自定义展开折叠工具图标
        function onLeftPanelToolCollapseClick(event) {
            var leftPanel = F(leftPanelClientID);
            var menuStyle = F.cookie('MenuStyle_Pro') || 'tree';

            if (menuStyle === 'tree' || menuStyle === 'tree_minimode') {
                // 获取左侧树控件实例
                var leftMenuTree = leftPanel.items[0];

                // 设置 miniMode 模式
                if (leftMenuTree.miniMode) {
                    expandLeftPanel();
                } else {
                    collapseLeftPanel();
                }

                // 对左侧面板重新布局
                leftPanel.doLayout();
            } else {
                leftPanel.toggleCollapse();
            }
        }

        // 点击展开菜单
        function onExpandAllClick(event) {
            var leftPanel = F(leftPanelClientID);
            var firstChild = leftPanel.items[0];

            if (firstChild.isType('tree')) {
                // 左侧为树控件
                firstChild.expandAll();
            } else {
                // 左侧为树控件+手风琴控件
                var activePane = firstChild.getActivePane();
                if (activePane) {
                    activePane.items[0].expandAll();
                }
            }
        }

        // 点击折叠菜单
        function onCollapseAllClick(event) {
            var leftPanel = F(leftPanelClientID);
            var firstChild = leftPanel.items[0];

            if (firstChild.isType('tree')) {
                // 左侧为树控件
                firstChild.collapseAll();
            } else {
                // 左侧为树控件+手风琴控件
                var activePane = firstChild.getActivePane();
                if (activePane) {
                    activePane.items[0].collapseAll();
                }
            }
        }

        function onSearchTrigger1Click(event) {
            F.removeCookie('SearchText_Pro');
            top.window.location.reload();
        }

        function onSearchTrigger2Click(event) {
            F.cookie('SearchText_Pro', this.getValue(), {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }
        // 点击标题栏工具图标 - 刷新
        function onToolRefreshClick(event) {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab.iframe) {
                var iframeWnd = activeTab.getIFrameWindow();
                iframeWnd.location.reload();
            }
        }

        // 点击标题栏工具图标 - 在新标签页中打开
        function onToolNewWindowClick(event) {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab.iframe) {
                var iframeUrl = activeTab.getIFrameUrl();
                iframeUrl = iframeUrl.replace(/\/mobile\/\?file=/ig, '/mobile/');
                window.open(iframeUrl, '_blank');
            }
        }

        // 点击标题栏工具图标 - 注销
        function onToolSignOutClick(event) {
            var bConfirmed = confirm('您确定要退出吗?');
            if (bConfirmed) { window.open('login.aspx', '_top'); }
        }

        // 点击标题栏工具图标 - 最大化
        function onToolMaximizeClick(event) {
            var topPanel = F(topPanelClientID);
            var leftPanel = F(leftPanelClientID);

            var currentTool = this;
            if (currentTool.iconFont.indexOf('expand') >= 0) {
                topPanel.collapse();
                currentTool.setIconFont('compress');

                collapseLeftPanel();
            } else {
                topPanel.expand();
                currentTool.setIconFont('expand');

                expandLeftPanel();
            }
        }

        // 添加示例标签页
        // id： 选项卡ID
        // iframeUrl: 选项卡IFrame地址 
        // title： 选项卡标题
        // icon： 选项卡图标
        // createToolbar： 创建选项卡前的回调函数（接受tabOptions参数）
        // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
        // iconFont： 选项卡图标字体
        function addExampleTab(tabOptions) {
            if (typeof (tabOptions) === 'string') {
                tabOptions = {
                    id: arguments[0],
                    iframeUrl: arguments[1],
                    title: arguments[2],
                    icon: arguments[3],
                    createToolbar: arguments[4],
                    refreshWhenExist: arguments[5],
                    iconFont: arguments[6]
                };
            }

            F.addMainTab(F(mainTabStripClientID), tabOptions);
        }

        // 移除选中标签页
        function removeActiveTab() {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab) {
                if (activeTab.id != tab1ClientID) {
                    activeTab.hide();
                    removeActiveTab();
                }
            }
        }

        // 获取当前激活选项卡的ID
        function getActiveTabId() {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab) {
                return activeTab.id;
            }
            return '';
        }

        // 激活选项卡，并刷新其中的内容，示例：表格控件->杂项->在新标签页中打开（关闭后刷新父选项卡）
        function activeTabAndRefresh(tabId) {
            var mainTabStrip = F(mainTabStripClientID);
            var targetTab = mainTabStrip.getTab(tabId);
            if (targetTab) {
                mainTabStrip.activeTab(targetTab);
                targetTab.refreshIFrame();
            }
        }

        // 激活选项卡，并刷新其中的内容，示例：表格控件->杂项->在新标签页中打开（关闭后更新父选项卡中的表格）
        function activeTabAndUpdate(tabId, param1) {
            var mainTabStrip = F(mainTabStripClientID);
            var targetTab = mainTabStrip.getTab(tabId);
            if (targetTab) {
                mainTabStrip.activeTab(targetTab);
                targetTab.getIFrameWindow().updatePage(param1);
            }
        }

        // 通知框
        function notify(msg) {
            F.notify({
                message: msg,
                messageIcon: 'information',
                target: '_top',
                header: false,
                displayMilliseconds: 3 * 1000,
                positionX: 'center',
                positionY: 'center'
            });
        }

        // 点击菜单样式
        function onMenuStyleCheckChange(event, item, checked) {
            var menuStyle = item.getAttr('data-tag');
            F.cookie('MenuStyle_Pro', menuStyle, {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }

        // 点击显示模式
        function onMenuModeCheckChange(event, item, checked) {
            var menuMode = item.getAttr('data-tag');

            F.cookie('MenuMode_Pro', menuMode, {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }

        // 点击语言
        function onMenuLangCheckChange(event, item, checked) {
            var lang = item.getAttr('data-tag');
            F.cookie('Language_Pro', lang, {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }

        // 点击标题栏工具图标 - 退出
        //function onSignOutClick(event) {
        //    var bConfirmed = confirm('您确定要退出吗?');
        //    if (bConfirmed) { window.close(); }
        //}

        ///个人信息
        function onUserProfileClick() {
            var windowUserProfile = F(windowUserProfileClientID);
            windowUserProfile.show();
        }

        F.ready(function () {
            $(".item-s li").click(function () {
                var $item = $('.f-tabstrip-header')
                $item.attr('style', ";display:block !important;")
            });

            $(".layui-nav-item").click(function () {
                var $item = $('.f-tabstrip-header')
                $item.attr('style', ";display:block !important;")
            });

            $(".item-big").click(function () {
                var $item = $('.f-tabstrip-header')
                $item.attr('style', ";display:none !important;")
                   });

             $(".bgbtn,.bgbtntop").click(function () {
                 // 切换下tab样式
                 var $this = $(this)
                 if (!$this.hasClass('activeClick')) {
                     if ($this.hasClass('bgbtn')) {
                         $(".bgbtn").removeClass('activeClick')
                     } else {
                         $(".bgbtntop").removeClass('activeClick')
                     }
                     $this.addClass('activeClick')
                }
            });

            var mainTabStrip = F(mainTabStripClientID);
            var leftPanel = F(leftPanelClientID);
            var mainMenu = leftPanel.items[0];

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // updateLocationHash: 切换Tab时，是否更新地址栏Hash值
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame
            F.initTreeTabStrip(mainMenu, mainTabStrip, true, false, false);
            var themeTitle = F.cookie('Theme_Pro_Title');
            var themeName = F.cookie('Theme_Pro');
            if (themeTitle) {
                F.removeCookie('Theme_Pro_Title');
                //notify('主题更改为：' + themeTitle + '（' + themeName + '）');
            }

        });
    </script>
</body>
</html>
