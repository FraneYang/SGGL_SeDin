<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CheckSpecialEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.CheckSpecialEdit" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑专项检查</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }               
         .f-grid-row.burlywood
        {
            background-color: burlywood;
            background-image: none;
        }
        
        .fontred
        {
            color: #FF7575;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="35% 10% 30% 30%">
                <Items>
                    <f:TextBox ID="txtCheckSpecialCode" runat="server" Label="检查编号" Readonly="true" MaxLength="50">
                    </f:TextBox>       
                     <f:RadioButtonList runat="server" ID="rbType" 
                         AutoPostBack="true" OnSelectedIndexChanged="rbType_SelectedIndexChanged">                               
                        <f:RadioItem Text="专项" Value="0" Selected="true" />
                        <f:RadioItem Text="联合" Value="1" />
                    </f:RadioButtonList>
                     <f:DropDownList ID="drpSupCheckItemSet" runat="server" Label="检查类别" 
                        AutoPostBack="true" OnSelectedIndexChanged="drpSupCheckItemSet_SelectedIndexChanged"
                        EnableEdit="true">
                    </f:DropDownList>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckDate">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="35% 35% 30%">
                <Items>
                  <f:DropDownList ID="drpPartInPersons" runat="server" Label="参检人员" EnableEdit="true" EnableMultiSelect="true"
                        ForceSelection="false" MaxLength="2000" EnableCheckBoxSelect="true"
                      AutoPostBack="true"   OnSelectedIndexChanged="drpPartInPersons_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:TextBox  runat="server" ID="txtPartInPersonNames" MaxLength="1000" ></f:TextBox>
                     <f:Button ID="btnNew" Text="新增" Icon="Add" EnablePostBack="false" runat="server" MarginLeft="50px">
                     </f:Button>
                    <%--    <f:Button ID="btnDelete" Text="删除" Icon="Delete" EnablePostBack="false" runat="server">
                        </f:Button>--%>
                </Items>
            </f:FormRow>      
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" DataIDField="CheckSpecialDetailId"
                        DataKeyNames="CheckSpecialDetailId" ShowGridHeader="true"  SortField="SortIndex" SortDirection="ASC" 
                        Height="350px" AllowCellEditing="true" AllowSorting="true"  EnableColumnLines="true"  OnPreDataBound="Grid1_PreDataBound" 
                        EnableTextSelection="True" >   
                        <Columns>                       
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                             <f:RenderField Width="110px" ColumnID="CheckAreaName" DataField="CheckAreaName" SortField="CheckAreaName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="单位工程">
                                <Editor>
                                    <f:DropDownList ID="drpCheckArea" Required="true" runat="server" EnableEdit="true" ShowRedStar="true">
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="受检单位">
                                    <Editor>
                                        <f:DropDownList ID="drpWorkUnit" Required="true" runat="server" EnableEdit="true" ShowRedStar="true">
                                        </f:DropDownList>
                                    </Editor>
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="Unqualified" DataField="Unqualified" 
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="问题描述" ExpandUnusedSpace="true">
                                 <Editor>
                                    <f:TextBox ID="txtUnqualified" runat="server" ShowRedStar="true">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>                            
                            <f:RenderField Width="150px" ColumnID="CheckItemName" DataField="CheckItemName" 
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="问题类型">
                                <Editor>
                                    <f:DropDownList ID="drpCheckItem" runat="server" EnableEdit="true" AutoPostBack="true" 
                                        OnSelectedIndexChanged="drpCheckItem_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="CompleteStatusName" DataField="CompleteStatusName" SortField="CompleteStatusName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="处理结果">
                                <Editor>
                                  <f:DropDownList ID="drpCompleteStatus" Required="true" runat="server" EnableEdit="true" ShowRedStar="true">
                                      <f:ListItem Text="待整改" Value="待整改" Selected="true"/>
                                      <f:ListItem Text="已整改" Value="已整改"/>
                                    </f:DropDownList>
                                    </Editor>
                            </f:RenderField> 
                            <f:RenderField Width="150px" ColumnID="HandleStepStr" DataField="HandleStepStr" SortField="HandleStepStr"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="处理措施" >
                                 <Editor>
                                        <f:DropDownList ID="drpHandleStep" Required="true" EnableCheckBoxSelect="true" runat="server" EnableEdit="true" EnableMultiSelect="true">
                                        </f:DropDownList>
                                    </Editor>
                            </f:RenderField>    
                            <f:RenderField Width="80px" ColumnID="HiddenHazardTypeName" DataField="HiddenHazardTypeName" SortField="HiddenHazardType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="隐患类别">
                                <Editor>
                                  <f:DropDownList ID="drpHiddenHazardType" Required="true" runat="server" EnableEdit="true" ShowRedStar="true">
                                      <f:ListItem Text="一般" Value="一般" Selected="true"/>
                                      <f:ListItem Text="较大" Value="较大"/>
                                      <f:ListItem Text="重大" Value="重大"/>
                                    </f:DropDownList>
                                    </Editor>
                            </f:RenderField>
                             <f:LinkButtonField ColumnID="Delete" Width="50px" EnablePostBack="false" Icon="Delete"
                                HeaderTextAlign="Center" HeaderText="删除" />
                        </Columns>
                        <Listeners>
                             <%-- <f:Listener Event="beforeedit" Handler="onGridBeforeEdit" />--%>
                                  <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>         
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
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
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

         var grid1ClientID = '<%= Grid1.ClientID %>';
        var drpCompleteStatusClientID = '<%= drpCompleteStatus.ClientID %>';
        var drpHandleStepClientID = '<%= drpHandleStep.ClientID %>';
        var drpHiddenHazardTypeClientID = '<%= drpHiddenHazardType.ClientID %>';

        function onGridAfterEdit(event, value, params) {
            var grid = F(grid1ClientID);
            var drpHiddenHazardType = F(drpHiddenHazardTypeClientID);
              var drpHandleStep = F(drpHandleStepClientID);
            if (params.columnId === 'CompleteStatusName') {              
                var CompleteStatusName = grid.getCellValue(params.rowId, 'CompleteStatusName');                
                if (CompleteStatusName.indexOf("待整改") != -1) {
                    drpHandleStep.enable();
                    drpHandleStep.setEmptyText('');
                    // drpHandleStep.loadData(shidata);
                } else {
                    drpHandleStep.setEmptyText('');
                    drpHandleStep.disable();
                    drpHiddenHazardType.setEmptyText('');
                    drpHiddenHazardType.disable();
                }
            }

            if (params.columnId === 'HandleStepStr') {
                var HandleStepStr = grid.getCellValue(params.rowId, 'HandleStepStr');                
                if (HandleStepStr.indexOf("整改通知单") != -1) {
                    drpHiddenHazardType.enable();
                    drpHiddenHazardType.setEmptyText('一般');
                   // drpHandleStep.loadData(shidata);
                } else {
                    drpHiddenHazardType.setEmptyText('');
                    drpHiddenHazardType.disable();
                }
            }
        }

    </script>
</body>
</html>
