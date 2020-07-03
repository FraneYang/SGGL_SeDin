<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrawView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.DrawView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                 <f:FormRow runat="server">
                    <Items>
                         <f:TextBox ID="txtDrawCode" runat="server" Label="图纸号" MaxLength="70" LabelWidth="110px" Readonly="true">
                        </f:TextBox>
                        <f:TextBox ID="txtDrawName" runat="server" Label="图纸名称" MaxLength="70" LabelWidth="110px" Readonly="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                       <f:DropDownList ID="drpMainItem" runat="server" Label="主项" LabelWidth="110px" LabelAlign="Right" EnableEdit="true" Readonly="true">
                        </f:DropDownList>
                        <f:DropDownList ID="drpDesignCN" runat="server" Label="设计专业" LabelWidth="110px" LabelAlign="Right" EnableEdit="true" Readonly="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                      <f:TextBox ID="txtEdition" runat="server" Label="版次"
                            MaxLength="70" LabelWidth="110px" Readonly="true">
                        </f:TextBox>
                         <f:TextBox ID="txtAcceptDate" runat="server" Label="接收日期" LabelAlign="right" LabelWidth="110px" Readonly="true">
                                </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                     <Items>
                        <f:RadioButtonList runat="server" ID="rblIsInvalid" Label="是否作废" ShowRedStar="true" AutoPostBack="true" LabelWidth="110px" Readonly="true">
                            <f:RadioItem Text="是" Value="true" />
                            <f:RadioItem Text="否" Value="false" Selected="true" />
                        </f:RadioButtonList>
                        <f:DropDownList ID="drprecover" runat="server" Label="作废图纸回收" LabelWidth="110px"   EmptyText="-请选择-" LabelAlign="Right" AutoSelectFirstItem="false" EnableEdit="true" Readonly="true">
                            <f:ListItem Text="已回收" Value="已回收" />
                            <f:ListItem Text="未回收" Value="未回收" />
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckerId" runat="server">
                        </f:HiddenField>
                        <f:Button ID="btnClose" EnablePostBack="false" 
                            runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
