<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmergencyTeamAndTrainView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Emergency.EmergencyTeamAndTrainView" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>应急组织</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="应急组织" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtUnit" runat="server" Label="单位" LabelAlign="Right" Readonly="true"></f:TextBox>
                    <f:TextBox ID="txtFileCode" runat="server" Label="编号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtFileName" runat="server" Label="名称" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>  
           <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                            EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="EmergencyTeamItemId" 
                            DataIDField="EmergencyTeamItemId" AllowSorting="true" SortField="Job,PersonName" 
                            SortDirection="ASC" EnableTextSelection="True"  Height="200px">                                  
                            <Columns>                        
                                <f:RenderField MinWidth="250px" ColumnID="PersonName" DataField="PersonName" 
                                    FieldType="String" HeaderText="姓名"  HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField MinWidth="250px" ColumnID="Job" DataField="Job" 
                                    FieldType="String" HeaderText="职务"  HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                    <f:RenderField MinWidth="250px" ColumnID="Tel" DataField="Tel"  ExpandUnusedSpace="true"
                                        FieldType="String" HeaderText="电话"  HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                <f:RenderField  HeaderText="PersonId" ColumnID="PersonId" DataField="PersonId" 
                                    FieldType="String" Hidden="true"></f:RenderField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
             <f:FormRow>
                <Items>                   
                    <f:TextBox ID="drpCompileMan" runat="server" Label="整理人" LabelAlign="Right" Readonly="true"></f:TextBox>                    
                     <f:TextBox ID="txtCompileDate" runat="server" Label="整理时间" LabelAlign="Right" Readonly="true"></f:TextBox>
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
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>                   
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
