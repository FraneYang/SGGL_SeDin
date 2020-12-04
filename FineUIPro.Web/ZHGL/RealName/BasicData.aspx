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
                                            OnSelectedIndexChanged="drpBaseType_SelectedIndexChanged">
                                            <f:ListItem  Text="参建单位类型" Value="CANJIAN_TYPE" Selected ="true"/>
                                            <f:ListItem  Text="证件类型" Value="ZHENGJIAN_TYPE" />
                                            <f:ListItem  Text="施工队类型" Value="TEAM_TYPE" />
                                            <f:ListItem  Text="文化程度" Value="EDU_LEVEL" />
                                            <f:ListItem  Text="婚姻状况" Value="MARITAL_STATUS" />
                                            <f:ListItem  Text="人员类型" Value="LAB_USER_TYPE" />
                                            <f:ListItem  Text="工种类型" Value="LAB_WORK_TYPE" />
                                            <f:ListItem  Text="考勤类别" Value="KAOHELEIBIE_TYPE" />
                                            <f:ListItem  Text="进出方向" Value="JINCHUFANGXIANG_TYPE" />
                                            <f:ListItem  Text="考勤方式" Value="KAOQINGFANSHI_TYPE" />
                                        </f:DropDownList>
                                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnDatabaseGo" Icon="DatabaseGo" runat="server" ToolTip="获取" 
                                            OnClick="btnDatabaseGo_Click">
                                        </f:Button>
                                        <f:Button ID="btnDatabaseRefresh" Icon="DatabaseRefresh" runat="server" ToolTip="全部获取" 
                                            Hidden="true" OnClick="btnDatabaseRefresh_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                         <f:Tab ID="Tab2" Title="国家基础信息" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>                               
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab3" Title="省份基础信息" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>                               
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab4" Title="项目数据" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>                               
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab5" Title="施工队数据" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>                               
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar5" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
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
