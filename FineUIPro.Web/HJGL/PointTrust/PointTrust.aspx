<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointTrust.aspx.cs" Inherits="FineUIPro.Web.HJGL.PointTrust.PointTrust" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="委托单"
                TitleToolTip="委托单" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:HiddenField runat="server" ID="hdItemsString">
                            </f:HiddenField>
                            <f:HiddenField runat="server" ID="hdTablerId">
                            </f:HiddenField>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server"
                                OnClick="btnSave_Click">
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
                                    <f:TextBox ID="txtTrustBatchCode" Label="委托单号" Readonly="true"
                                         runat="server" LabelWidth="130px" >
                                    </f:TextBox>
                                    <f:TextBox ID="txtUnit" Label="单位名称" Readonly="true"
                                         runat="server" LabelWidth="130px" >
                                    </f:TextBox>
                                    <f:TextBox ID="txtUnitWork" Label="单位工程名称" Readonly="true"
                                         runat="server" LabelWidth="130px" >
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtTrustDate" Label="委托日期" Readonly="true"
                                         runat="server" LabelWidth="130px" >
                                    </f:TextBox>
                                    <f:TextBox ID="txtDetectionType" Label="检测方法" Readonly="true"
                                         runat="server" LabelWidth="130px" >
                                    </f:TextBox>
                                    <f:DropDownList ID="drpNDEUnit" Label="检测单位" runat="server"
                                        ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="130px">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="无损委托单"
                            EnableCollapse="true" runat="server" BoxFlex="1" DataKeyNames="PointBatchItemId"
                            AllowCellEditing="true" AllowColumnLocking="true" EnableColumnLines="true" ClicksToEdit="2"
                            DataIDField="PointBatchItemId" AllowSorting="true" SortField="PipelineCode,WeldJointCode"
                            SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                            PageSize="25" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                            <Columns>
                                <f:RenderField Width="160px" ColumnID="PipelineCode" DataField="PipelineCode" FieldType="String"
                                    HeaderText="管线号" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="PipelineCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="PipingClassCode" DataField="PipingClassCode"
                                    FieldType="String" HeaderText="管道等级" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="PipingClassCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="WeldJointCode" DataField="WeldJointCode" FieldType="String"
                                    HeaderText="焊口号" HeaderTextAlign="Center"
                                    TextAlign="Left" SortField="WeldJointCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="JointArea" DataField="JointArea" FieldType="String"
                                    HeaderText="焊接区域" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="JointArea">
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="WelderCode" DataField="WelderCode" FieldType="String"
                                    HeaderText="焊工号" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="WelderCode">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="WeldTypeCode" DataField="WeldTypeCode" FieldType="String"
                                    HeaderText="焊缝类型" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="WeldTypeCode">
                                </f:RenderField>
                                <f:RenderField Width="70px" ColumnID="Dia" DataField="Dia" FieldType="Double" HeaderText="外径"
                                    HeaderTextAlign="Center" TextAlign="Left" SortField="Dia">
                                </f:RenderField>
                                <f:RenderField Width="70px" ColumnID="Thickness" DataField="Thickness" FieldType="Double"
                                    HeaderText="壁厚" HeaderTextAlign="Center" TextAlign="Left"
                                    SortField="Thickness">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="PointDate" DataField="PointDate" SortField="PointDate"
                                    FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="点口日期"
                                    HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField Width="80px" ColumnID="AcceptLevel" DataField="AcceptLevel" FieldType="String" HeaderText="合格等级"
                                    HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
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
                                    <f:ListItem Text="所有行" Value="10000" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
