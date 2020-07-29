<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunLog.aspx.cs" Inherits="FineUIPro.Web.Personal.RunLog" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true" Layout="VBox"
                 ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="操作日志" TitleToolTip="操作日志">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="LogId" EnableColumnLines="true" DataIDField="LogId" AllowSorting="true" SortField="OperationTime" SortDirection="DESC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" 
                        EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True">
                         <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:TextBox runat="server" Label="项目名称" ID="txtProject" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                        LabelAlign="right">
                                    </f:TextBox> 
                                    <f:TextBox runat="server" Label="操作人员" ID="txtUser" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                        LabelAlign="right">
                                    </f:TextBox> 
                                     <f:TextBox runat="server" Label="操作内容" ID="txtOperationLog" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                        LabelAlign="right">
                                    </f:TextBox>                                    
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">                              
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                                FieldType="String" HeaderText="操作人员" HeaderTextAlign="Center" TextAlign="Left">                               
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                                FieldType="String" HeaderText="操作项目" HeaderTextAlign="Center" TextAlign="Left">                              
                            </f:RenderField>                           
                            <f:RenderField Width="150px" ColumnID="OperationTime" DataField="OperationTime" SortField="OperationTime" HeaderText="操作时间" HeaderTextAlign="Center" 
                                TextAlign="Left" >
                            </f:RenderField>                            
                            <f:RenderField ColumnID="OperationLog" DataField="OperationLog" SortField="OperationLog" HeaderTextAlign="Center" TextAlign="Left"
                                FieldType="String" HeaderText="操作日志" Width="320px"  ExpandUnusedSpace="true">
                            </f:RenderField>
                        </Columns>
                        <PageItems >
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">        
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
