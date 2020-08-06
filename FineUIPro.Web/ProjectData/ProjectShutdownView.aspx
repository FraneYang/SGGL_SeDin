<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectShutdownView.aspx.cs"
    Inherits="FineUIPro.Web.ProjectData.ProjectShutdownView" ValidateRequest="false" %>
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
                    <f:TextBox ID="txtOldProjectState" runat="server" Label="项目原状态" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
                <Items>   
                    <f:TextBox ID="txtProjectState" runat="server" Label="项目申请状态" LabelAlign="Right" Readonly="true">
                    </f:TextBox>   
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
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>                    
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
