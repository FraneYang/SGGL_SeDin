<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardRegisterTypesEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.HazardRegisterTypesEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑安全巡检类型</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtTypeCode" runat="server" Label="编号" MaxLength="100" FocusOnPageLoad="true">
                 </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtRegisterTypesName" runat="server" Label="名称" Required="true" MaxLength="20"
                        ShowRedStar="true" >
                    </f:TextBox>
                 </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                      <f:RadioButtonList runat="server" ID="rblType" Label="类型" AutoColumnWidth="true"
                            LabelWidth="50px" Width="200px">
                            <f:RadioItem Text="问题类型" Value="1"  Selected="true"/>
                            <f:RadioItem Text="危害因素" Value="2" />
                            <f:RadioItem Text="作业内容" Value="3" />
                            <f:RadioItem Text="导致伤害/事故" Value="4" />
                        </f:RadioButtonList>
                   <%--  <f:CheckBox ID="chkIsPunished" runat="server" Label="是否处罚">
                    </f:CheckBox>--%>
                 </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存数据" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
