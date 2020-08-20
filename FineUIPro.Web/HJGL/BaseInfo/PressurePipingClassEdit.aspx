﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PressurePipingClassEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.PressurePipingClassEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑压力管道级别</title>
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
                    <f:TextBox ID="txtPressurePipingClassCode" runat="server" Label="压力管道级别代号"
                        Required="true" MaxLength="50" ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpPressurePipingType" runat="server" Label="压力管道级别类型"
                        Required="true" MaxLength="50" ShowRedStar="true" LabelWidth="140px">
                        <f:ListItem  Value="工业管道(GC)" Text="工业管道(GC)" Selected="true"/>
                        <f:ListItem  Value="长输管道(GA)" Text="长输管道(GA)"/>
                        <f:ListItem  Value="公用管道(GB)" Text="公用管道(GB)"/>
                        <f:ListItem  Value="动力管道(GD)" Text="动力管道(GD)"/>
                    </f:DropDownList>
                     
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="500" LabelWidth="140px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
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
