<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentalSelectCloumn.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.EnvironmentalSelectCloumn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>环境因素危险源导出选择列</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="False" ShowHeader="false" Layout="Anchor"
        BodyPadding="5px">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="取消" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:Button ID="btnImport" ToolTip="导出" Icon="FolderUp" runat="server" OnClick="btnImport_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="2">
                <f:CheckItem Text="能源资源/污染类" Value="能源资源/污染类" Selected="true" />
                <f:CheckItem Text="因素类型" Value="因素类型" Selected="true" />
                <f:CheckItem Text="分项工程/活动点" Value="分项工程/活动点" Selected="true" />
                <f:CheckItem Text="环境因素" Value="环境因素" Selected="true" />
                <f:CheckItem Text="A值" Value="A值" Selected="true" />
                <f:CheckItem Text="B值" Value="B值" Selected="true" />
                <f:CheckItem Text="C值" Value="C值" Selected="true" />
                <f:CheckItem Text="D值" Value="D值" Selected="true" />
                <f:CheckItem Text="E值" Value="E值" Selected="true" />
                <f:CheckItem Text="Σ" Value="Σ" Selected="true" />
                 <f:CheckItem Text="F值" Value="F值" Selected="true" />
                <f:CheckItem Text="G值" Value="G值" Selected="true" />
                <f:CheckItem Text="Σ" Value="Σ" Selected="true" />               
                <f:CheckItem Text="重要" Value="重要" Selected="true" />
                <f:CheckItem Text="安全措施" Value="安全措施" Selected="true" />
                <f:CheckItem Text="备注" Value="备注" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
