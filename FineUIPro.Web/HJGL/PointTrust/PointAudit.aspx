<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointAudit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.PointTrust.PointAudit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点口审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>            
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="焊口信息"
                TitleToolTip="焊口信息" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="焊口信息"
                        EnableCollapse="true" KeepCurrentSelection="true" runat="server" BoxFlex="1"
                        DataKeyNames="PointBatchItemId" AllowColumnLocking="true" EnableColumnLines="true"
                        DataIDField="PointBatchItemId" EnableTextSelection="True" AllowSorting="true" SortField="PointBatchCode,WeldJointCode"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="false" IsDatabasePaging="true"
                        PageSize="1000" EnableCheckBoxSelect="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>                                    
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnAccept" Icon="Accept" Text="审核" runat="server" ToolTip="审核"
                                        OnClick="btnAccept_Click">
                                    </f:Button>
                                    <f:Button ID="取消审核" Icon="ArrowUndo" Text="取消审核" runat="server" ToolTip="取消审核"
                                        OnClick="btnCancelAccept_Click">
                                    </f:Button>
                                    <f:Button ID="btnGenerate" Text="生成" ToolTip="生成" Icon="TableEdit" runat="server"
                                       OnClick="btnGenerate_Click" >
                            </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RenderField HeaderText="单位工程编号" ColumnID="UnitWorkCode"
                                DataField="UnitWorkCode" SortField="UnitWorkCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单位" ColumnID="UnitCode"
                                DataField="UnitCode" SortField="UnitCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="90px" >
                            </f:RenderField>
                             <f:RenderField HeaderText="批号" ColumnID="PointBatchCode"
                                DataField="PointBatchCode" SortField="PointBatchCode" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="280px" >
                            </f:RenderField>
                            <f:RenderField HeaderText="焊口号" ColumnID="WeldJointCode"
                                DataField="WeldJointCode" SortField="WeldJointCode" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="90px">
                            </f:RenderField>
                           <f:RenderField HeaderText="区域" ColumnID="JointArea" DataField="JointArea"
                                SortField="JointArea" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                Width="100px">
                            </f:RenderField>
                           <f:RenderField HeaderText="焊接日期" ColumnID="WeldingDate"
                                DataField="WeldingDate" SortField="WeldingDate" FieldType="Date" Renderer="Date"
                                HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                             <f:RenderField HeaderText="管道等级" ColumnID="PipingClassName"
                                DataField="PipingClassName" SortField="PipingClassName" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField HeaderText="点口审核" ColumnID="PointIsAudit" DataField="PointIsAudit"
                                SortField="PointIsAudit" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                Width="90px">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
