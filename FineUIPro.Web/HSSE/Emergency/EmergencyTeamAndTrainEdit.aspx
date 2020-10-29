<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmergencyTeamAndTrainEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Emergency.EmergencyTeamAndTrainEdit" ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急组织</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="应急组织" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="25% 18% 20% 17% 20%">
                <Items>
                     <f:DropDownList ID="drpUnit" runat="server" Label="单位"  EnableEdit="true" LabelAlign="Right"
                         AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged" LabelWidth="70px">
                    </f:DropDownList>      
                    <f:TextBox ID="txtFileCode" runat="server" Label="编号" LabelAlign="Right" LabelWidth="70px"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtFileName" runat="server" Label="名称" Required="true" ShowRedStar="true" LabelWidth="70px"
                        LabelAlign="Right" MaxLength="200" FocusOnPageLoad="true">
                    </f:TextBox>
                       <f:DropDownList ID="drpCompileMan" runat="server" Label="整理人" EnableEdit="true" LabelAlign="Right" LabelWidth="70px">
                    </f:DropDownList>
                     <f:DatePicker ID="txtCompileDate" runat="server" Label="整理时间" LabelAlign="Right" LabelWidth="80px"
                        EnableEdit="true">
                    </f:DatePicker> 
                </Items>
            </f:FormRow>                  
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" Title="队伍" AutoScroll="true"
                       runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
                    <Rows>
                        <f:FormRow ColumnWidths="30% 30% 30% 5%">
                            <Items>
                                <f:HiddenField runat="server" ID="hdEmergencyTeamItemId"></f:HiddenField>
                                <f:DropDownList runat="server" ID="drpPserson" Label="人员"  EnableEdit="true" LabelAlign="Right"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpPserson_SelectedIndexChanged">
                                </f:DropDownList>                 
                                 <f:TextBox ID="txtJob" runat="server" Label="职务"  LabelAlign="Right" MaxLength="50" >
                                 </f:TextBox>
                                <f:TextBox ID="txtTel" runat="server" Label="电话"  LabelAlign="Right" MaxLength="50" >
                                 </f:TextBox>
                                 <f:Button ID="btnSure" Icon="Accept" runat="server"  ValidateForms="SimpleForm1" 
                                    OnClick="btnSure_Click" ToolTip="确认" >
                                </f:Button>       
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                                    EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="EmergencyTeamItemId" 
                                    DataIDField="EmergencyTeamItemId" AllowSorting="true" SortField="Job,PersonName" 
                                    SortDirection="ASC" EnableTextSelection="True"  Height="200px" 
                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">                                  
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
                                        <Listeners>
                                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                        </Listeners>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
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
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
            <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="修改" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
             ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
            Icon="Delete">
        </f:MenuButton>
    </f:Menu>
    </form>
   
    
    <script type="text/javascript">      
         var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
