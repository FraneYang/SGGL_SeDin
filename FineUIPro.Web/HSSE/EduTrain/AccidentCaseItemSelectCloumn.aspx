﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentCaseItemSelectCloumn.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.AccidentCaseItemSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>事故案例导出选择列</title>
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
                    <f:Button ID="btnImport" Text="导出" runat="server" Icon="SystemSave" OnClick="btnImport_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="3">
                <f:CheckItem Text="作业活动" Value="作业活动" Selected="true" />
                <f:CheckItem Text="事故类别" Value="事故类别" Selected="true" />
                <f:CheckItem Text="事故名称" Value="事故名称" Selected="true" />
                <f:CheckItem Text="事故简况" Value="事故简况" Selected="true" />
                <f:CheckItem Text="事故点评" Value="事故点评" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>

