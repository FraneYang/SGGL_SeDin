<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LawRegulationListSelectCloumn.aspx.cs" Inherits="FineUIPro.Web.HSSE.Law.LawRegulationListSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全法律法规导出选择列</title>
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="取消" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:Button ID="btnImport" ToolTip="导出" Icon="FolderUp" runat="server" OnClick="btnImport_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="2">
                <f:CheckItem Text="编号" Value="编号" Selected="true" />
                <f:CheckItem Text="名称" Value="名称" Selected="true" />
                <f:CheckItem Text="类别" Value="类别" Selected="true" />
                <f:CheckItem Text="批准日" Value="批准日" Selected="true" />
                <f:CheckItem Text="生效日" Value="生效日" Selected="true" />
                <f:CheckItem Text="简介及重点关注条款" Value="简介及重点关注条款" Selected="true" />
                <f:CheckItem Text="整理人" Value="整理人" Selected="true" />
                <f:CheckItem Text="整理日期" Value="整理日期" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
