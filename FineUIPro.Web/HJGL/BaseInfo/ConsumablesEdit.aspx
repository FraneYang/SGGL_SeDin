<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumablesEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.BaseInfo.ConsumablesEdit" %>

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
                        Required="true" MaxLength="50" ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtConsumablesName" runat="server" Label="焊材牌号"
                        Required="true" MaxLength="50" ShowRedStar="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items> 
                    <f:TextBox ID="txtSteelType" Label="焊材类别" Required="true"  ShowRedStar="true"
                        runat="server" LabelAlign="right" LabelWidth="120px"></f:TextBox>
                     <f:TextBox ID="txtStandard" runat="server" Label="焊材标准"
                        MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                  <f:TextBox ID="txtSteelFormat" runat="server" Label="焊材规格"
                        MaxLength="50" LabelWidth="120px">
                    </f:TextBox>
                    <f:DropDownList ID="drpConsumablesType" Label="焊材类型"
                        runat="server" LabelAlign="right" ShowRedStar="true" Required="true" LabelWidth="120px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" MaxLength="500" LabelWidth="120px">
                    </f:TextBox>
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
