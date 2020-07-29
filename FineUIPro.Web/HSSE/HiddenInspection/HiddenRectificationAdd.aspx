<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenRectificationAdd.aspx.cs" Inherits="FineUIPro.Web.HSSE.HiddenInspection.HiddenRectificationAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>日常巡检</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList runat="server" EnableSimulateTree="True" Label="检查项" ID="drpRegisterTypes"
                        >
                    </f:DropDownList>
                    <f:RadioButtonList ID="ckType" runat="server" Label="巡检周期"
                                Width="330px">
                                <f:RadioItem Value="D" Selected="True" Text="日检" />
                                <f:RadioItem Value="W" Text="周检" />
                                <f:RadioItem Value="M" Text="月检" />
                            </f:RadioButtonList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList runat="server" EnableSimulateTree="True" Label="责任单位" ID="drpUnit"
                        AutoPostBack="true" OnSelectedIndexChanged="drpUnit_OnSelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList runat="server" EnableSimulateTree="True" Label="单位工程" ID="drpWorkArea">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRegisterDef" runat="server" Label="问题描述" ShowRedStar="true" Required="true">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" ID="txtHandleIdea" Label="复检问题描述" Hidden="true"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList runat="server" EnableSimulateTree="True" Label="责任人" ID="drpResponsibleMan"
                        >
                    </f:DropDownList>
                    <f:DatePicker ID="txtRectificationPeriod" runat="server" Label="整改期限" LabelAlign="Right" Required="True" ShowRedStar="true"
                        >
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckManName" runat="server" Label="检查人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckTime" runat="server" Label="检查时间" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:LinkButton ID="UploadAttach" runat="server" Label="整改前照片" Text="上传和查看" OnClick="btnAttachUrl_Click" LabelAlign="Right">
                                    </f:LinkButton>
                    <f:NumberBox ID="txtCutPayment" runat="server" Label="罚款金额" LabelAlign="Right" Text="0" NoNegative="true" NoDecimal="true"></f:NumberBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:HiddenField ID="hdCheckManId" runat="server"></f:HiddenField>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="文件上传" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="680px"
        Height="480px">
    </f:Window>
    </form>
</body>
</html>
