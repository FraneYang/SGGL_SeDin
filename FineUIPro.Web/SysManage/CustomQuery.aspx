<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomQuery.aspx.cs" Inherits="FineUIPro.Web.SysManage.CustomQuery" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>自定义查询</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1"  AjaxAspnetControls="divHazard"/>
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                <Items>
                    <f:TextArea runat="server" ID="txtCustomQuery" Height="50px" Width="1000px"></f:TextArea>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button runat="server" ID="btnQuery" Icon="SystemSearch" OnClick="btnQuery_Click"></f:Button>
                    <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                        EnableAjax="false" DisableControlBeforePostBack="false" >
                    </f:Button>
                </Items>
            </f:Toolbar>
            <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server">
                <Items>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Label runat="server" ID="lbCount"></f:Label>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
             <f:ContentPanel ShowHeader="false" runat="server" ID="ContentPanel1" Margin="0 0 0 0" AutoScroll="true">
                <div id="divHazard" runat="server" style="height:410px" >                                 
                    <asp:GridView ID="gvHazard" runat="server" AllowSorting="false" HorizontalAlign="Justify" Width="100%" >                                                
                        <Columns>
                        </Columns>                                                
                        <RowStyle CssClass="GridRow" />
                        <PagerStyle HorizontalAlign="Left" />                        
                    </asp:GridView>
                </div>
            </f:ContentPanel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>