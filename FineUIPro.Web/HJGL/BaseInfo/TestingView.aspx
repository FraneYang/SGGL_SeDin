﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingView.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.BaseInfo.TestingView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看探伤类型</title>
    <base target="_self" />
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDetectionTypeCode" runat="server" Label="检测方法代号"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtDetectionTypeName" runat="server" Label="检测方法名称"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSysType" runat="server" Label="系统类型"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtSecuritySpace" runat="server" Label="安全距离"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtInjuryDegree" runat="server" Label="伤害程度"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭"
                        runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
