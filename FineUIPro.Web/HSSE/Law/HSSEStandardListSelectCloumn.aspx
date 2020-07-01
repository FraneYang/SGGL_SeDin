<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardListSelectCloumn.aspx.cs" Inherits="FineUIPro.Web.Law.HSSEStandardListSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全标准规范导出选择列</title>
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
                <f:CheckItem Text="标准号" Value="标准号" Selected="true" />
                <f:CheckItem Text="标准名称" Value="标准名称" Selected="true" />
                <f:CheckItem Text="分类" Value="分类" Selected="true" />
                <f:CheckItem Text="标准级别" Value="标准级别" Selected="true" />
                <%--<f:CheckItem Text="对应HSSE索引" Value="对应HSSE索引" Selected="true" />--%>
               <%-- <f:CheckItem Text="01" Value="01" Selected="true" />
                <f:CheckItem Text="02" Value="02" Selected="true" />
                <f:CheckItem Text="03" Value="03" Selected="true" />
                <f:CheckItem Text="04" Value="04" Selected="true" />
                <f:CheckItem Text="05" Value="05" Selected="true" />
                <f:CheckItem Text="06" Value="06" Selected="true" />
                <f:CheckItem Text="07" Value="07" Selected="true" />
                <f:CheckItem Text="08" Value="08" Selected="true" />
                <f:CheckItem Text="09" Value="09" Selected="true" />
                <f:CheckItem Text="10" Value="10" Selected="true" />
                <f:CheckItem Text="11" Value="11" Selected="true" />
                <f:CheckItem Text="12" Value="12" Selected="true" />
                <f:CheckItem Text="13" Value="13" Selected="true" />
                <f:CheckItem Text="14" Value="14" Selected="true" />
                <f:CheckItem Text="15" Value="15" Selected="true" />
                <f:CheckItem Text="16" Value="16" Selected="true" />
                <f:CheckItem Text="17" Value="17" Selected="true" />
                <f:CheckItem Text="18" Value="18" Selected="true" />
                <f:CheckItem Text="19" Value="19" Selected="true" />
                <f:CheckItem Text="20" Value="20" Selected="true" />
                <f:CheckItem Text="21" Value="21" Selected="true" />
                <f:CheckItem Text="22" Value="22" Selected="true" />
                <f:CheckItem Text="23" Value="23" Selected="true" />
                <f:CheckItem Text="90" Value="90" Selected="true" />--%>
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
