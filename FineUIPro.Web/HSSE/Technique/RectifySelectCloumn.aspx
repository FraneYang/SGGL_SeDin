<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifySelectCloumn.aspx.cs" Inherits="FineUIPro.Web.Technique.RectifySelectCloumn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全隐患导出选择列</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="False" ShowHeader="false" Layout="Anchor"
        BodyPadding="5px">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="取消" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:Button ID="btnImport" Text="导出" runat="server" Icon="SystemSave" OnClick="btnImport_Click" Hidden="true">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="3">
                <f:CheckItem Text="作业类别" Value="作业类别" Selected="true" />
                <f:CheckItem Text="隐患源点" Value="隐患源点" Selected="true" />
                <f:CheckItem Text="风险分析" Value="风险分析" Selected="true" />
                <f:CheckItem Text="风险防范" Value="风险防范" Selected="true" />
                <f:CheckItem Text="同类风险" Value="同类风险" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

