<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectShutdownEdit.aspx.cs"
    Inherits="FineUIPro.Web.ProjectData.ProjectShutdownEdit" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑项目状态及软件关闭</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="项目状态及软件关闭" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>                    
                    <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" Required="true" ShowRedStar="true"
                        LabelAlign="Right" Readonly="true">
                    </f:TextBox>                    
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>                   
                    <f:TextBox ID="txtOldProjectState" runat="server" Label="原状态" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
                <Items>                   
                    <f:DropDownList ID="drpProjectState" runat="server" Label="申请状态" LabelAlign="Right" >
                        <f:ListItem Text="施工" Value="1" />
                        <f:ListItem Text="暂停" Value="2" />
                        <f:ListItem Text="完工" Value="3" />
                    </f:DropDownList>
                </Items>
            </f:FormRow>      
             <f:FormRow>
                 <Items>                   
                    <f:TextBox ID="txtCompileMan" runat="server" Label="申请人" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
                <Items>         
                    <f:TextBox ID="txtCompileDate" runat="server" Label="申请时间" LabelAlign="Right" Readonly="true">
                    </f:TextBox> 
                </Items>
            </f:FormRow>      
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>   
                    <f:HiddenField runat="server" ID="hdOldProjectState"></f:HiddenField>                                   
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>
                     <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                     <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
