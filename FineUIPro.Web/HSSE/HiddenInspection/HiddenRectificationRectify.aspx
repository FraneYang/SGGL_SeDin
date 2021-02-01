<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenRectificationRectify.aspx.cs" Inherits="FineUIPro.Web.HiddenInspection.HiddenRectificationRectify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>日常巡检</title>
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
                    <f:TextArea ID="txtRegisterDef" runat="server" Label="问题描述" Readonly="true"
                        ShowRedStar="true" Required="true" LabelWidth="110px" Height="64px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckManName" runat="server" Label="检查人" Readonly="true" LabelWidth="110px">
                    </f:TextBox>
                      <f:TextBox ID="txtCheckTime" runat="server" Label="检查时间" LabelWidth="110px" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server"  Label="危害因素" ID="drpRegisterTypes2" Readonly="true" LabelWidth="110px">
                    </f:TextBox>    
                    <f:TextBox runat="server"  Label="问题类型" ID="drpRegisterTypes" Readonly="true" LabelWidth="110px">
                    </f:TextBox>    
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                     <f:TextBox runat="server"  Label="作业内容" ID="drpRegisterTypes3" Readonly="true" LabelWidth="110px">
                    </f:TextBox>    
                    <f:TextBox runat="server"  Label="风险值" ID="drpHazardValue" Readonly="true" LabelWidth="110px">
                    </f:TextBox>    
                </Items>
            </f:FormRow>
              <f:FormRow>
                <Items>
                     <f:TextBox runat="server"  Label="导致伤害/事故" ID="drpRegisterTypes4" Readonly="true" LabelWidth="110px">
                    </f:TextBox>    
                    <f:TextBox runat="server" ID="txtRequirements" Label="整改要求" Readonly="true" LabelWidth="110px"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Readonly="true" Label="责任单位" ID="drpUnit" LabelWidth="110px">
                    </f:TextBox>
                     <f:TextBox runat="server" Readonly="true" Label="责任人" ID="drpResponsibleMan" LabelWidth="110px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Readonly="true" Label="单位工程" ID="drpWorkArea" LabelWidth="110px">
                    </f:TextBox>   
                    <f:TextBox ID="txtRectificationPeriod" runat="server" Label="整改期限"  LabelWidth="110px" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" ID="txtHandleIdea" Label="复检问题描述" Hidden="true" Readonly="true"
                        LabelWidth="110px"></f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                     <f:TextBox runat="server"  Label="抄送" ID="drpCCManIds" LabelWidth="110px" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRectification" runat="server" Label="采取措施" FocusOnPageLoad="true" 
                        MaxLength="500" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                      <f:LinkButton ID="UploadAttach" runat="server" Label="整改前照片"  LabelWidth="110px"
                        Text="查看" OnClick="btnAttachUrl_Click" LabelAlign="Right">
                </f:LinkButton>
                    <f:LinkButton ID="UploadAttachR" runat="server" Label="整改后照片" Text="上传" 
                        OnClick="btnAttachUrlR_Click" LabelAlign="Right">
                    </f:LinkButton>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
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
