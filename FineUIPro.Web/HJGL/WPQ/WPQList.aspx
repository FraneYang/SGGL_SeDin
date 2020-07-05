﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WPQList.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WPQ.WPQList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊接工艺评定台账</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊接工艺评定台账" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="WPQId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="WPQId" AllowSorting="true" SortField="WPQCode"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="评定编号" ID="txtWeldingProcedureCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="300px" LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" Text="增加" Icon="Add" runat="server" OnClick="btnNew_Click">
                            </f:Button>
                            <f:Button ID="btnImport" Text="导入" ToolTip="导入" Icon="PackageIn" runat="server" OnClick="btnImport_Click">
                            </f:Button>
                            <f:Button ID="btnExport" OnClick="btnExport_Click" runat="server" Text="导出" ToolTip="导出"
                                Icon="NoteGo" EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="120px" ColumnID="WPQCode" DataField="WPQCode"
                        FieldType="String" HeaderText="评定编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField> 
                    <f:RenderField Width="160px" ColumnID="UnitName" DataField="UnitName"
                        FieldType="String" HeaderText="编制单位" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField> 
                    <f:RenderField Width="120px" ColumnID="MaterialCode1" DataField="MaterialCode1" FieldType="String"
                        HeaderText="材质1" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MaterialClass1" DataField="MaterialClass1" FieldType="String"
                        HeaderText="材质1类别" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MaterialGroup1" DataField="MaterialGroup1" FieldType="String"
                        HeaderText="材质1组别" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MaterialCode2" DataField="MaterialCode2" FieldType="String"
                        HeaderText="材质2" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MaterialClass2" DataField="MaterialClass2" FieldType="String"
                        HeaderText="材质2类别" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MaterialGroup2" DataField="MaterialGroup2" FieldType="String"
                        HeaderText="材质2组别" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="Specifications" DataField="Specifications"
                        FieldType="String" HeaderText="规格" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="WeldingRod" DataField="WeldingRod" FieldType="String"
                        HeaderText="焊丝类别" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="WeldingWire" DataField="WeldingWire" FieldType="String"
                        HeaderText="焊条类别" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="GrooveType" DataField="GrooveTypeName"
                        FieldType="String" HeaderText="坡口类型" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="JointType" DataField="JointType"
                        FieldType="String" HeaderText="接头形式" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="WeldingMethodCode" DataField="WeldingMethodCode"
                        FieldType="String" HeaderText="焊接方法" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MinImpactDia" DataField="MinImpactDia" FieldType="String"
                        HeaderText="外径最小值" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MaxImpactDia" DataField="MaxImpactDia" FieldType="String"
                        HeaderText="外径最大值" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="170px" ColumnID="MinImpactThickness" DataField="MinImpactThickness"
                        FieldType="String" HeaderText="冲击时覆盖厚度最小值" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="170px" ColumnID="MaxImpactThickness" DataField="MaxImpactThickness"
                        FieldType="String" HeaderText="冲击时覆盖厚度最大值" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="170px" ColumnID="NoMinImpactThickness" DataField="NoMinImpactThickness"
                        FieldType="String" HeaderText="不冲击时覆盖厚度最小值" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="170px" ColumnID="NoMaxImpactThickness" DataField="NoMaxImpactThickness"
                        FieldType="String" HeaderText="不冲击时覆盖厚度最大值" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="WeldingPosition" DataField="WeldingPosition"
                        FieldType="String" HeaderText="焊接位置" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="IsHotProess" DataField="IsHotProess"
                        FieldType="String" HeaderText="是否热处理" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ProtectiveGas" DataField="ProtectiveGas"
                        FieldType="String" HeaderText="保护气体" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="WPQStandard" DataField="WPQStandard" FieldType="String"
                        HeaderText="评定标准" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="PreTemperature" DataField="PreTemperature" FieldType="String"
                        HeaderText="预热温度" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>
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
    <f:Window ID="Window1" Title="编辑焊接工艺评定台账" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="900px" Height="660px">
    </f:Window>
    <f:Window ID="Window2" Title="导入焊接工艺评定台账" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="660px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除">
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