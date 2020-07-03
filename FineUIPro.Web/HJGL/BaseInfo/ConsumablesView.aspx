<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumablesView.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.BaseInfo.ConsumablesView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑焊接耗材</title>
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
                    <f:TextBox ID="txtConsumablesCode" runat="server" Label="焊材型号"
                        Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtConsumablesName" runat="server" Label="焊材牌号"
                        Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items> 
                    <f:TextBox ID="txtSteelType" runat="server" Label="焊材类别"
                        Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtStandard" runat="server" Label="焊材标准"
                        MaxLength="50" LabelWidth="120px" Readonly="true">
                    </f:TextBox>
                   
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSteelFormat" runat="server" Label="焊材规格"
                        Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtConsumablesType" runat="server" Label="焊材类型"
                        Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" Readonly="true"
                        LabelWidth="120px">
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
