﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotProessFeedback.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.HotProcessHard.HotProessFeedback" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>热处理反馈及硬度委托</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="320px" Title="热处理反馈及硬度委托"
                ShowBorder="true" Layout="VBox" ShowHeader="false" AutoScroll="true" BodyPadding="5px"
                IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox ID="txtSearchNo" runat="server" EmptyText="输入查询条件"
                                AutoPostBack="true" Label="委托单号" LabelWidth="100px"
                                OnTextChanged="Tree_TextChanged" Width="300px" LabelAlign="Right">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvControlItem" ShowHeader="false" Title="热处理委托节点树" OnNodeCommand="tvControlItem_NodeCommand"
                        Height="470px" runat="server" ShowBorder="false" EnableCollapse="true" 
                        AutoLeafIdentification="true" EnableSingleExpand="true" EnableTextSelection="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="热处理反馈及硬度委托"
                TitleToolTip="热处理反馈及硬度委托" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:HiddenField runat="server" ID="hdHotProessTrustId">
                            </f:HiddenField>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click" ToolTip="提交">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtHotProessTrustNo" Label="委托单号"
                                        runat="server" LabelAlign="Right" LabelWidth="100px">
                                    </f:Label>
                                    <f:Label ID="txtProessDate" Label="热处理日期" runat="server"
                                        LabelAlign="Right" LabelWidth="100px">
                                    </f:Label>     
                                    <f:Label ID="txtProessMethod" Label="热处理方法" runat="server"
                                        LabelAlign="Right" LabelWidth="100px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                          
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtProessEquipment" Label="热处理设备" runat="server"
                                        LabelAlign="Right" LabelWidth="100px">
                                    </f:Label>  
                                    <f:Label ID="txtTabler" Label="制表人" runat="server" LabelAlign="Right"
                                        LabelWidth="100px">
                                    </f:Label>
                                    <f:Label ID="txtRemark" Label="备注" runat="server" LabelAlign="Right"
                                        LabelWidth="100px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="热处理反馈及硬度委托"
                        EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="HotProessTrustItemId"
                        AllowCellEditing="true" AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2"
                        DataIDField="HotProessTrustItemId" AllowSorting="true" SortField="PipelineCode,WeldJointCode"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:TextBox ID="txtIsoNo" Label="管线号" runat="server"
                                        LabelWidth="90px" LabelAlign="Right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                                    </f:TextBox>
                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                    </f:ToolbarFill>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号"
                                Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField HeaderText="管线号" ColumnID="PipelineCode"
                                DataField="PipelineCode" SortField="PipelineCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="180px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="110px" Locked="true">
                            </f:RenderField>
                            <f:RenderField HeaderText="规格" ColumnID="Specification"
                                DataField="Specification" SortField="Specification" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="120px">
                            </f:RenderField>
                            <f:RenderField HeaderText="材质代号" ColumnID="MaterialCode"
                                DataField="MaterialCode" SortField="MaterialCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Center" Width="120px">
                            </f:RenderField>
                            <f:RenderField HeaderText="委托时间" ColumnID="ProessDate"
                                DataField="ProessDate" SortField="ProessDate" FieldType="Date" Renderer="Date"
                                HeaderTextAlign="Center" TextAlign="Left" Width="110px">
                            </f:RenderField>
                            <f:CheckBoxField ColumnID="IsCompleted" Width="120px" RenderAsStaticField="false" AutoPostBack="true"
                                HeaderText="是否完成" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:CheckBoxField ColumnID="IsPass" Width="120px" RenderAsStaticField="true" DataField="IsPass" 
                                HeaderText="是否合格" HeaderTextAlign="Center" TextAlign="Center" />
                        </Columns>
                        <PageItems>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <f:ListItem Text="10" Value="10" />
                                <f:ListItem Text="15" Value="15" />
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <script type="text/javascript">
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
