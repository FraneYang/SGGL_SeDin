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
                    <f:TextArea ID="txtRegisterDef" runat="server" Label="问题描述" FocusOnPageLoad="true" 
                        ShowRedStar="true" Required="true" LabelWidth="110px" Height="64px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckManName" runat="server" Label="检查人" Readonly="true" LabelWidth="110px">
                    </f:TextBox>
                      <f:DatePicker ID="txtCheckTime" runat="server" Label="检查时间" ShowTime="true"
                            LabelAlign="Right" Required="True" ShowRedStar="true" LabelWidth="110px" 
                           DateFormatString="yyyy-MM-dd HH:mm">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                       <f:DropDownList runat="server"  Label="危害因素" ID="drpRegisterTypes2" EnableEdit="true" LabelWidth="110px">
                    </f:DropDownList>    
                    <f:DropDownList runat="server"  Label="问题类型" ID="drpRegisterTypes" EnableEdit="true" LabelWidth="110px">
                    </f:DropDownList>    
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                     <f:DropDownList runat="server"  Label="作业内容" ID="drpRegisterTypes3" EnableEdit="true" LabelWidth="110px">
                    </f:DropDownList>    
                    <f:DropDownList runat="server"  Label="风险值" ID="drpHazardValue" EnableEdit="true" LabelWidth="110px">
                        <f:ListItem Value="0.3" Text="0.3"/>
                        <f:ListItem Value="1" Text="1"/>
                        <f:ListItem Value="3" Text="3"/>
                    </f:DropDownList>    
                </Items>
            </f:FormRow>
              <f:FormRow>
                <Items>
                      <f:DropDownList runat="server"  Label="导致伤害/事故" ID="drpRegisterTypes4" EnableEdit="true" LabelWidth="110px">
                    </f:DropDownList>    
                       <f:TextBox runat="server" ID="txtRequirements" Label="整改要求" LabelWidth="110px"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList runat="server" EnableEdit="true" Label="责任单位" ID="drpUnit" LabelWidth="110px"
                        AutoPostBack="true" OnSelectedIndexChanged="drpUnit_OnSelectedIndexChanged">
                    </f:DropDownList>
                     <f:DropDownList runat="server" EnableEdit="true" Label="责任人" ID="drpResponsibleMan" LabelWidth="110px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList runat="server" EnableEdit="true" Label="单位工程" ID="drpWorkArea" LabelWidth="110px">
                    </f:DropDownList>   
                    <f:DatePicker ID="txtRectificationPeriod" runat="server" Label="整改期限"  LabelWidth="110px"
                             LabelAlign="Right" Required="True" ShowRedStar="true" ShowTime="true"
                             DateFormatString="yyyy-MM-dd HH:mm">
                    </f:DatePicker>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" ID="txtHandleIdea" Label="复检问题描述" Hidden="true" LabelWidth="110px"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:LinkButton ID="UploadAttach" runat="server" Label="整改前照片"  LabelWidth="110px"
                        Text="上传和查看" OnClick="btnAttachUrl_Click" LabelAlign="Right">
                </f:LinkButton>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                     <f:DropDownList runat="server" EnableMultiSelect="True" EnableCheckBoxSelect="true" EnableEdit="true"
                         Label="抄送" ID="drpCCManIds" LabelWidth="110px" AutoPostBack="true" 
                         OnSelectedIndexChanged="drpCCManIds_SelectedIndexChanged">
                    </f:DropDownList>
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
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="680px"
        Height="480px">
    </f:Window>
    </form>
</body>
</html>
