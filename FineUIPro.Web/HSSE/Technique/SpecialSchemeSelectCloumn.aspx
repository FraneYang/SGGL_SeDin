<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialSchemeSelectCloumn.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Technique.SpecialSchemeSelectCloumn" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专项方案导出选择列</title>
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
                    <f:Button ID="btnClose" EnablePostBack="false" Text="取消" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:Button ID="btnImport" Text="导出" runat="server" Icon="SystemSave" OnClick="btnImport_Click"
                        Hidden="true">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="3">
                <f:CheckItem Text="编号" Value="编号" Selected="true" />
                <f:CheckItem Text="名称" Value="名称" Selected="true" />
                <f:CheckItem Text="类型" Value="类型" Selected="true" />
                <f:CheckItem Text="摘要" Value="摘要" Selected="true" />
                <f:CheckItem Text="备注" Value="备注" Selected="true" />
                <f:CheckItem Text="整理人" Value="整理人" Selected="true" />
                <f:CheckItem Text="整理日期" Value="整理日期" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
