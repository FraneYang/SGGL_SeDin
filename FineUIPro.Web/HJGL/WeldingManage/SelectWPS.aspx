<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectWPS.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.SelectWPS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="555"
                    TitleToolTip="666" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="提交数据" ValidateForms="Form2" OnClick="btnSave_Click"
                           >
                        </f:Button>
                            </Items>
                        </f:Toolbar>
                        
                    </Toolbars>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="表单" DataKeyNames="WPQId" DataIDField="WPQId"
                            EnableCollapse="true" Height="520px" runat="server" EnableCheckBoxSelect="true" EnableMultiSelect="false">
                            <Columns>
                                <f:RenderField Width="120px" ColumnID="WPQCode" DataField="WPQCode"
                                    FieldType="String" HeaderText="WPS编号" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField HeaderText="坡口类型" ColumnID="GrooveTypeName"
                                    DataField="GrooveTypeName" SortField="GrooveTypeName" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊接方法" ColumnID="WeldingMethodCode"
                                    DataField="WeldingMethodCode" SortField="WeldingMethodCode" FieldType="String"
                                    HeaderTextAlign="Center" TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊条类型" ColumnID="WeldingWire"
                                    DataField="WeldingWire" SortField="WeldingWire" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="焊丝类型" ColumnID="WeldingRod"
                                    DataField="WeldingRod" SortField="WeldingRod" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="材质1" ColumnID="MaterialCode1"
                                    DataField="MaterialCode1" SortField="MaterialCode1" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
                                </f:RenderField>
                                <f:RenderField HeaderText="材质2" ColumnID="MaterialCode2"
                                    DataField="MaterialCode2" SortField="MaterialCode2" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="90px">
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
                                
                                <f:RenderField HeaderText="是否加热" ColumnID="IsHotProess"
                                    DataField="IsHotProess" FieldType="String" HeaderTextAlign="Center"
                                    TextAlign="Left" Width="110px">
                                </f:RenderField>

                            </Columns>

                        </f:Grid>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
    <script>
        
    </script>
</body>
</html>
