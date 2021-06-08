<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrawEdit.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.DrawEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                        <f:TextBox ID="txtDrawCode" runat="server" Label="图纸号"
                            Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="110px">
                        </f:TextBox>
                        <f:TextBox ID="txtDrawName" runat="server" Label="图纸名称"
                            Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                 </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:DropDownList ID="drpMainItem" runat="server" Label="主项" LabelWidth="110px" LabelAlign="Right" EmptyText="--请选择--" AutoSelectFirstItem="false" EnableEdit="true">
                        </f:DropDownList>
                        <f:DropDownList ID="drpDesignCN" runat="server" Required="true" ShowRedStar="true" EmptyText="--请选择--" AutoSelectFirstItem="false" Label="设计专业" LabelWidth="110px" LabelAlign="Right" EnableEdit="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:NumberBox ID="txtEdition" Label="版次" runat="server" NoDecimal="true"
                            LabelWidth="110px" NoNegative="true" ShowRedStar="true" Required="true" AutoPostBack="true">
                        </f:NumberBox>
                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="接收日期" ID="txtAcceptDate"
                            LabelAlign="right" LabelWidth="110px" Required="true" ShowRedStar="true">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:RadioButtonList runat="server" ID="rblIsInvalid" Label="是否作废" ShowRedStar="true" AutoPostBack="true" LabelWidth="110px" OnSelectedIndexChanged="rblIsInvalid_SelectedIndexChanged">
                            <f:RadioItem Text="是" Value="true" />
                            <f:RadioItem Text="否" Value="false" Selected="true" />
                        </f:RadioButtonList>
                        <%--<f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="编制日期" ID="txtCompileDate"
                            LabelAlign="right" LabelWidth="110px" Required="true" ShowRedStar="true">
                        </f:DatePicker>--%>
                        <f:DropDownList ID="drprecover" runat="server" Label="作废图纸回收" LabelWidth="110px"   EmptyText="-请选择-" LabelAlign="Right" AutoSelectFirstItem="false" EnableEdit="true">
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
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
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
