﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl12.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl12" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件12" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件12   预付款担保格式" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtAttachUrlContent" runat="server" Text="（执行银行格式）。" Height="400px"></f:TextArea>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>