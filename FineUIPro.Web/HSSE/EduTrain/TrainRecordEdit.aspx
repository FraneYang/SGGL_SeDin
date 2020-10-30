<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainRecordEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.EduTrain.TrainRecordEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑培训记录</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
         .f-grid-row.Red {
            background-color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTrainingCode" runat="server" Label="编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:DropDownList ID="drpTrainType" runat="server" Label="培训类型" ShowRedStar="true"
                        Required="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpTrainLevel" runat="server" Label="培训级别" >
                    </f:DropDownList>
                    <f:NumberBox ID="txtTeachHour" NoDecimal="false" NoNegative="true" MaxValue="100"
                        DecimalPrecision="1" MinValue="0" runat="server" Label="学时" ShowRedStar="true" Required="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTrainTitle" runat="server" Label="标题" MaxLength="200" >
                    </f:TextBox>
                    <f:TextBox ID="txtTeachAddress" runat="server" Label="培训地点" MaxLength="100">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="培训日期" ID="txtTrainStartDate">
                    </f:DatePicker>
                   <f:NumberBox ID="txtTrainPersonNum" NoDecimal="true" NoNegative="true" MinValue="0" Readonly="true" runat="server" Label="培训人数" >
                    </f:NumberBox>
                    <f:TextBox ID="txtTeachMan" runat="server" Label="授课人" MaxLength="50">
                    </f:TextBox>
                    <%--<f:DropDownList ID="drpTrainStates" runat="server" Label="培训状态" Hidden="true" Readonly="true">
                         <f:ListItem Text="计划中" Value="0"/>
                         <f:ListItem Text="待考试" Value="1"/>
                         <f:ListItem Text="考试中" Value="2"/>
                         <f:ListItem Text="已结束" Value="3" Selected="true"/>
                    </f:DropDownList>--%>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnits" runat="server" Label="培训单位" EnableCheckBoxSelect="true"
                        EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trWorkPost">
                <Items>
                    <f:DropDownList ID="drpWorkPostIds" runat="server" Label="培训岗位" EnableCheckBoxSelect="true"
                        EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTrainContent" runat="server" Label="培训内容" LabelAlign="right" Height="50px" MaxLength="500">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" AllowCellEditing="true"
                        DataIDField="TrainDetailId" DataKeyNames="TrainDetailId" EnableMultiSelect="true" 
                        AllowSorting="true" SortField="UnitName,WorkPostCode,PersonName"
                        ShowGridHeader="true" Height="200px" EnableColumnLines="true" >
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                  <%--  <f:Button ID="btnTrainingType" runat="server" ToolTip="教材类型" Icon="BorderDraw" 
                                        OnClick="btnTrainingType_Click">
                                    </f:Button>--%>
                                    <f:Button ID="btnTrainTest" runat="server" ToolTip="培训试卷" Icon="ApplicationFormEdit" Hidden="true"
                                        OnClick="btnTrainTest_Click">
                                    </f:Button>
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="PageExcel" runat="server" ValidateForms="SimpleForm1"
                                            OnClick="btnImport_Click">
                                    </f:Button>
                                    <f:Button ID="btnSelect" Icon="ShapeSquareSelect" runat="server" ToolTip="选择培训人员" ValidateForms="SimpleForm1"
                                        OnClick="btnSelect_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="60px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="单位" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                                FieldType="String" HeaderText="岗位" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>  
                            <f:RenderField Width="150px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderText="培训人员" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>                       
                            <f:RenderField Width="150px" ColumnID="CheckResult" DataField="CheckResult" FieldType="Int"
                                    RendererFunction="renderGender" HeaderText="考核结果">
                                    <Editor>
                                        <f:DropDownList ID="drpCheckResult" Required="true" runat="server">
                                            <f:ListItem Text="合格" Value="1" />
                                            <f:ListItem Text="未合格" Value="0" />
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField> 
                                <f:TemplateField Width="100px" HeaderText="成绩1" HeaderTextAlign="Center" TextAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbTrainDetailId" runat="server" Text='<%#GetCheckScore(Eval("TrainDetailId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField Width="120px" ColumnID="CheckScore" DataField="CheckScore" FieldType="Double"
                                    HeaderText="成绩" HeaderTextAlign="Center" TextAlign="Left">
                                    <Editor>
                                        <f:NumberBox ID="txtCheckScore" NoDecimal="false" NoNegative="true" MinValue="0"
                                            DecimalPrecision="1" runat="server" Required="true">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                               <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" 
                                     Text="试卷" ToolTip="上传查看" DataIFrameUrlFields="TrainDetailId" DataIFrameUrlFormatString="~/AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecord&menuId=1182E353-FAB9-4DB1-A1EC-F41A00892128"/>
                                <f:RenderField Width="1px" ColumnID="TrainDetailId" DataField="TrainDetailId" 
                                    FieldType="String" HeaderText="主键"  Hidden="true" HeaderTextAlign="Center">                                        
                                </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
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
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="选择培训人员" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1300px" Height="520px">
    </f:Window>
    <f:Window ID="Window2" Title="培训试卷" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="1200px" Height="520px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">  
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看"  Icon="TableGo" Hidden="true">
        </f:MenuButton>   
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function renderGender(value) {
            return value == 1 ? '合格' : '不合格';
        }
        function onGridDataLoad(event) {
             this.mergeColumns(['UnitName'], { depends: true });
             this.mergeColumns(['WorkPostName'], { depends: true });
        }
    </script>
</body>
</html>
