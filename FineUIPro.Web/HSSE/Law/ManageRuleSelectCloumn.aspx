<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRuleSelectCloumn.aspx.cs" Inherits="FineUIPro.Web.Law.ManageRuleSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全管理规定导出选择列</title>
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
                    <f:Button ID="btnImport" Text="导出" runat="server" Icon="SystemSave" OnClick="btnImport_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="2">
                <f:CheckItem Text="文件编号" Value="文件编号" Selected="true" />
                <f:CheckItem Text="文件名称" Value="文件名称" Selected="true" />
                <f:CheckItem Text="分类" Value="分类" Selected="true" />
                <f:CheckItem Text="摘要" Value="摘要" Selected="true" />
                <f:CheckItem Text="整理人" Value="整理人" Selected="true" />
                <f:CheckItem Text="整理日期" Value="整理日期" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
