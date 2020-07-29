<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CheckSpecialView.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.CheckSpecialView" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看专项检查</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
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
                     <f:TextBox ID="txtCheckSpecialCode" runat="server" Label="检查编号" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtSupCheckItemSet" runat="server" Label="检查类别" Readonly="true" >
                    </f:TextBox>
                      <f:TextBox ID="txtCheckDate" runat="server" Label="检查日期" Readonly="true" >
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtPartInPersons" runat="server" Label="参检人员" Readonly="true" MaxLength="200">
                    </f:TextBox>
                        <f:TextBox ID="txtPartInPersonNames" runat="server"  MaxLength="200" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" DataIDField="CheckSpecialDetailId"
                        DataKeyNames="CheckSpecialDetailId" ShowGridHeader="true" Height="350px"       
                        AllowSorting="true" SortField="SortIndex" SortDirection="ASC" 
                        EnableColumnLines="true"  EnableTextSelection="True" OnRowCommand="Grid1_RowCommand">                           
                        <Columns>      
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                  TextAlign="Center" />
                            <f:RenderField Width="110px" ColumnID="WorkArea" DataField="WorkArea" SortField="WorkArea"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="单位工程">
                            </f:RenderField>
                            <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="受检单位">
                            </f:RenderField>
                                  <f:RenderField Width="200px" ColumnID="Unqualified" DataField="Unqualified" SortField="Unqualified"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="问题描述" ExpandUnusedSpace="true">                              
                            </f:RenderField>
                            <f:RenderField Width="160px" ColumnID="CheckItemName" DataField="CheckItemName" SortField="CheckItemName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="问题类型">                                
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="CompleteStatusName" DataField="CompleteStatusName" SortField="CompleteStatusName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="处理结果">
                            </f:RenderField>   
                            <f:LinkButtonField Width="160px" ColumnID="HandleStepLink" DataTextField="HandleStepLink" DataToolTipField="HandleStepLink" HeaderText="处理措施"
                                 CommandName="click" EnableAjax="false"  />                            
                            <f:RenderField Width="80px" ColumnID="HiddenHazardTypeName" DataField="HiddenHazardTypeName" SortField="HiddenHazardTypeName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="隐患类别">
                            </f:RenderField>
                            <%--<f:LinkButtonField Width="140px" HeaderText="处理措施" ConfirmTarget="Parent" CommandName="click"
                                 TextAlign="Center"  DataTextField="HandleStepLink" ColumnID="HandleStepLink" />    --%>
                        </Columns>
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
        <f:Window ID="Window1" Title="详细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" 
        Width="1100px" Height="500px">
    </f:Window>
    </form>
    <script>
        function onGridDataLoad(event) {
            this.mergeColumns(['CheckItemType']);
        }
    </script>
</body>
</html>
