<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicData.aspx.cs" Inherits="FineUIPro.Web.ZHGL.RealName.BasicData" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>基础数据</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .LabelColor {
            color: Red;
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
                <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="550px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server"
                    ActiveTabIndex="0">
                    <Tabs>
                        <f:Tab ID="Tab1" Title="字典数据" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>   
                                <f:Grid ID="Grid1" ShowBorder="true"  EnableCollapse="true" ShowHeader="false" runat="server" BoxFlex="1" 
                                    DataKeyNames="BasicDataId"  EnableColumnLines="true" DataIDField="BasicDataId" AllowSorting="true" SortField="dictCode"
                                        SortDirection="ASC"  PageSize="500" ForceFit="true"  EnableTextSelection="True">                                        
                                        <Columns>
                                            <f:RenderField Width="200px" ColumnID="dictTypeCode" DataField="dictTypeCode" 
                                                FieldType="String" HeaderText="类型编码" HeaderTextAlign="Center" TextAlign="Left">
                                            </f:RenderField>
                                            <f:RenderField Width="300px" ColumnID="dictName" DataField="dictName" TextAlign="Left"
                                                FieldType="String" HeaderText="名称" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="dictCode" DataField="dictCode"
                                                FieldType="String" HeaderText="编码" HeaderTextAlign="Center" TextAlign="Left">
                                           </f:RenderField>                 
                                        </Columns>
                                    </f:Grid>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:DropDownList runat="server" ID="drpBaseType" Label="数据类型" AutoPostBack="true" 
                                            OnSelectedIndexChanged="drpBaseType_SelectedIndexChanged" EnableEdit="true">
                                            <f:ListItem  Text="参建单位类型" Value="CANJIAN_TYPE" Selected ="true"/>
                                            <f:ListItem  Text="证件类型" Value="ZHENGJIAN_TYPE" />
                                            <f:ListItem  Text="施工队类型" Value="TEAM_TYPE" />
                                            <f:ListItem  Text="政治面貌" Value="POLITICAL_LANDSCAPE" />
                                            <f:ListItem  Text="民族" Value="MINZU_TYPE" />
                                            <f:ListItem  Text="文化程度" Value="EDU_LEVEL" />
                                            <f:ListItem  Text="婚姻状况" Value="MARITAL_STATUS" />
                                            <f:ListItem  Text="人员类型" Value="LAB_USER_TYPE" />
                                            <f:ListItem  Text="工种类型" Value="LAB_WORK_TYPE" />
                                            <f:ListItem  Text="考勤类别" Value="KAOQINLEIBIE_TYPE" />
                                            <f:ListItem  Text="进出方向" Value="JINCHUFANGXIANG_TYPE" />
                                            <f:ListItem  Text="考勤方式" Value="KAOQINGFANSHI_TYPE" />
                                        </f:DropDownList>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnDatabaseGo" Icon="DatabaseGo" runat="server" ToolTip="获取" 
                                            Hidden="true" OnClick="btnDatabaseGo_Click">
                                        </f:Button>
                                    <%--    <f:Button ID="btnDatabaseRefresh" Icon="DatabaseRefresh" runat="server" ToolTip="全部获取" 
                                            Hidden="true" OnClick="btnDatabaseRefresh_Click">
                                        </f:Button>--%>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab2" Title="国家基础数据" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>   
                                <f:Grid ID="Grid2" ShowBorder="true"  EnableCollapse="true" ShowHeader="false" runat="server" BoxFlex="1" 
                                    DataKeyNames="ID"  EnableColumnLines="true" DataIDField="ID" AllowSorting="true" SortField="CountryId"
                                        SortDirection="ASC"  PageSize="500" ForceFit="true"  EnableTextSelection="True">                                        
                                        <Columns>
                                                <f:RenderField Width="100px" ColumnID="CountryId" DataField="CountryId" TextAlign="Left"
                                                FieldType="String" HeaderText="序号" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="countryCode" DataField="countryCode" TextAlign="Left"
                                                FieldType="String" HeaderText="代码" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="300px" ColumnID="cname" DataField="cname" TextAlign="Left"
                                                FieldType="String" HeaderText="国家中文名称" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="dictCode" DataField="dictCode"
                                                FieldType="String" HeaderText="国家名称" HeaderTextAlign="Center" TextAlign="Left">
                                           </f:RenderField>                 
                                        </Columns>
                                    </f:Grid>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnDatabaseGo2" Icon="DatabaseGo" runat="server" ToolTip="获取" 
                                            Hidden="true" OnClick="btnDatabaseGo2_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab3" Title="省份基础数据" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>   
                                <f:Grid ID="Grid3" ShowBorder="true"  EnableCollapse="true" ShowHeader="false" runat="server" BoxFlex="1" 
                                        DataKeyNames="ID"  EnableColumnLines="true" DataIDField="ID" AllowSorting="true" SortField="provinceCode"
                                        SortDirection="ASC"  PageSize="500" ForceFit="true"  EnableTextSelection="True">                                        
                                        <Columns>
                                            <f:RenderField Width="150px" ColumnID="provinceCode" DataField="provinceCode" 
                                                FieldType="String" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Left">
                                            </f:RenderField>
                                         <%--   <f:RenderField Width="150px" ColumnID="cityCode" DataField="cityCode" 
                                                FieldType="String" HeaderText="编码" HeaderTextAlign="Center" TextAlign="Left">
                                            </f:RenderField>--%>
                                            <f:RenderField Width="200px" ColumnID="cname" DataField="cname" TextAlign="Left"
                                                FieldType="String" HeaderText="中文名" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="cnShortName" DataField="cnShortName" TextAlign="Left"
                                                FieldType="String" HeaderText="简称" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="name" DataField="name"
                                                FieldType="String" HeaderText="英文名" HeaderTextAlign="Center" TextAlign="Left">
                                           </f:RenderField>                 
                                        </Columns>
                                    </f:Grid>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:DropDownList runat="server" ID="drpCountry" Label="国家" AutoPostBack="true"  LabelWidth="80px"
                                            OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" EnableEdit="true">
                                        </f:DropDownList>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnDatabaseGo3" Icon="DatabaseGo" runat="server" ToolTip="获取" 
                                           Hidden="true" OnClick="btnDatabaseGo3_Click">
                                        </f:Button>
                                        <f:Button ID="btnDatabaseGo31" Icon="DatabaseRefresh" runat="server" ToolTip="全部获取" 
                                            Hidden="true" OnClick="btnDatabaseGo31_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab4" Title="项目数据" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>   
                                <f:Grid ID="Grid4" ShowBorder="true"  EnableCollapse="true" ShowHeader="false" runat="server" BoxFlex="1" 
                                    DataKeyNames="ID"  EnableColumnLines="true" DataIDField="ID" AllowSorting="true" SortField="proCode"
                                        SortDirection="ASC"  PageSize="500" ForceFit="true"  EnableTextSelection="True">                                        
                                        <Columns>
                                                <f:RenderField Width="150px" ColumnID="proCode" DataField="proCode" TextAlign="Left"
                                                FieldType="String" HeaderText="编码" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="400px" ColumnID="proName" DataField="proName" TextAlign="Left"
                                                FieldType="String" HeaderText="名称" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="proShortName" DataField="proShortName"
                                                FieldType="String" HeaderText="简称" HeaderTextAlign="Center" TextAlign="Left">
                                           </f:RenderField>                 
                                        </Columns>
                                    </f:Grid>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnDatabaseGo4" Icon="DatabaseGo" runat="server" ToolTip="获取" 
                                            Hidden="true" OnClick="btnDatabaseGo4_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab5" Title="施工队数据" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>   
                                <f:Grid ID="Grid5" ShowBorder="true"  EnableCollapse="true" ShowHeader="false" runat="server" BoxFlex="1" 
                                        DataKeyNames="ID"  EnableColumnLines="true" DataIDField="ID" AllowSorting="true" SortField="proCode,teamName"
                                        SortDirection="ASC"  PageSize="500" ForceFit="true"  EnableTextSelection="True">                                        
                                        <Columns>
                                            <f:RenderField Width="150px" ColumnID="teamId" DataField="teamId" 
                                                FieldType="String" HeaderText="id" HeaderTextAlign="Center" TextAlign="Left">
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="proCode" DataField="proCode" 
                                                FieldType="String" HeaderText="项目编码" HeaderTextAlign="Center" TextAlign="Left">
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="teamName" DataField="teamName" TextAlign="Left"
                                                FieldType="String" HeaderText="施工队名称" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="teamLeaderName" DataField="teamLeaderName" TextAlign="Left"
                                                FieldType="String" HeaderText="队长姓名" HeaderTextAlign="Center">                       
                                            </f:RenderField>
                                                <f:RenderField Width="150px" ColumnID="teamLeaderMobile" DataField="teamLeaderMobile" TextAlign="Left"
                                                FieldType="String" HeaderText="队长联系电话" HeaderTextAlign="Center">                       
                                            </f:RenderField>       
                                        </Columns>
                                    </f:Grid>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar5" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:DropDownList runat="server" ID="drpProject" Label="项目" AutoPostBack="true" 
                                            Width="600px" LabelWidth="80px"
                                            OnSelectedIndexChanged="drpProject_SelectedIndexChanged" EnableEdit="true">
                                        </f:DropDownList>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnDatabaseGo5" Icon="DatabaseGo" runat="server" ToolTip="获取" 
                                           Hidden="true" OnClick="btnDatabaseGo5_Click">
                                        </f:Button>
                                        <f:Button ID="btnDatabaseGo51" Icon="DatabaseRefresh" runat="server" ToolTip="全部获取" 
                                            Hidden="true" OnClick="btnDatabaseGo51_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
