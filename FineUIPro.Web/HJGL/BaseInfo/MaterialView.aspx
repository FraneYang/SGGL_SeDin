﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialView.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.MaterialView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看材质定义</title>
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
                    <f:TextBox ID="txtMaterialCode" runat="server" Label="材质代号"
                        Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSteelType" runat="server" Label="材质类型"
                        Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMaterialType" runat="server" Label="材料标准"
                        Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
           
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMaterialClass" runat="server" Label="材质类别"
                        Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMaterialGroup" runat="server" Label="材质组别"
                        Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" Readonly="true"
                        LabelWidth="200px">
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