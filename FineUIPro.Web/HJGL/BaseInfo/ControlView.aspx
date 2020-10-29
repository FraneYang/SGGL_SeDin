<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlView.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.ControlView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑直径寸径对照</title>
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
                    <f:TextBox ID="txtDN" runat="server" Label="公称尺寸(DN)" Readonly="true"></f:TextBox>
                    <f:TextBox ID="txtPipeSize" runat="server" Label="公称尺寸(NPS)" Readonly="true">
                    </f:TextBox>
                    <f:NumberBox ID="txtOutSizeDia" runat="server" Label="外径" Readonly="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtSCH10" runat="server" Label="SCH10" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH20" runat="server" Label="SCH20" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH30" runat="server" Label="SCH30" Readonly="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtSTD" runat="server" Label="STD" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH40" runat="server" Label="SCH40" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH60" runat="server" Label="SCH60" Readonly="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtXS" runat="server" Label="XS" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH80" runat="server" Label="SCH80" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH100" runat="server" Label="SCH100" Readonly="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtSCH120" runat="server" Label="SCH120" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH140" runat="server" Label="SCH140" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtSCH160" runat="server" Label="SCH160" Readonly="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtXXS" runat="server" Label="XXS" Readonly="true">
                    </f:NumberBox>
                     <f:NumberBox ID="txtSize" runat="server" Label="尺寸系列" Readonly="true">
                    </f:NumberBox>
                    <f:NumberBox ID="txtthickness" runat="server" Label="壁厚" Readonly="true">
                    </f:NumberBox>
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
