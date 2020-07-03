<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardListOut.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.HazardListOut" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险源清单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                            EnableAjax="false" DisableControlBeforePostBack="false">
                        </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>        
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="HazardId"  DataIDField="HazardId" 
                        AllowSorting="true" SortField="STHazardListTypeCode,HazardListTypeCode,HazardCode"
                        PageSize="20" AllowPaging="true" IsDatabasePaging="true" Height="540px">
                        <Columns>
                              <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                             <f:TemplateField Width="110px" HeaderText="类别代码</br>(一级)" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfSTHazardListTypeCode"
                                SortField="STHazardListTypeCode">
                                <ItemTemplate>
                                    <asp:Label ID="lbSTHazardListTypeCode" runat="server" Text='<%# Bind("STHazardListTypeCode") %>' ToolTip='<%#Bind("STHazardListTypeCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                             <f:TemplateField Width="120px" HeaderText="类别名称</br>(一级)" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfSTHazardListTypeName"
                                SortField="STHazardListTypeName">
                                <ItemTemplate>
                                    <asp:Label ID="lbSTHazardListTypeName" runat="server" Text='<%# Bind("STHazardListTypeName") %>' ToolTip='<%#Bind("STHazardListTypeName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="110px" HeaderText="类别代码</br>(二级)" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHazardListTypeCode"
                                SortField="HazardListTypeCode">
                                <ItemTemplate>
                                    <asp:Label ID="lbHazardListTypeCode" runat="server" Text='<%# Bind("HazardListTypeCode") %>' ToolTip='<%#Bind("HazardListTypeCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                             <f:TemplateField Width="120px" HeaderText="类别名称</br>(二级)" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHazardListTypeName"
                                SortField="HazardListTypeName">
                                <ItemTemplate>
                                    <asp:Label ID="lbHazardListTypeName" runat="server" Text='<%# Bind("HazardListTypeName") %>' ToolTip='<%#Bind("HazardListTypeName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="130px" HeaderText="代码" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHazardCode"
                                SortField="HazardCode">
                                <ItemTemplate>
                                    <asp:Label ID="lbHazardCode" runat="server" Text='<%# Bind("HazardCode") %>' ToolTip='<%#Bind("HazardCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="危险因素明细" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHazardItems">
                                <ItemTemplate>
                                    <asp:Label ID="lbHazardItems" runat="server" Text='<%# Bind("HazardItems") %>' ToolTip='<%#Bind("HazardItems") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="100px" HeaderText="缺陷类型" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfDefectsType"
                                SortField="DefectsType">
                                <ItemTemplate>
                                    <asp:Label ID="lbDefectsType" runat="server" Text='<%# Bind("DefectsType") %>' ToolTip='<%#Bind("DefectsType") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="可能导致的事故" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfMayLeadAccidents"
                                SortField="MayLeadAccidents">
                                <ItemTemplate>
                                    <asp:Label ID="lbMayLeadAccidents" runat="server" Text='<%# Bind("MayLeadAccidents") %>'
                                        ToolTip='<%#Bind("MayLeadAccidents") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="80px" HeaderText="辅助方法" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfHelperMethod"
                                SortField="HelperMethod">
                                <ItemTemplate>
                                    <asp:Label ID="lbHelperMethod" runat="server" Text='<%# Bind("HelperMethod") %>'
                                        ToolTip='<%#Bind("HelperMethod") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" SortField="HazardJudge_L"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="评价(L)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_E" DataField="HazardJudge_E" SortField="HazardJudge_E"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="评价(E)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_C" DataField="HazardJudge_C" SortField="HazardJudge_C"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="评价(C)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardJudge_D" DataField="HazardJudge_D" SortField="HazardJudge_D"
                                FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="评价(D)">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="HazardLevel" DataField="HazardLevel" SortField="HazardLevel"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险级别">
                            </f:RenderField>
                            <f:TemplateField Width="180px" HeaderText="控制措施" HeaderTextAlign="Center" TextAlign="Left" ColumnID="tfControlMeasures"
                                SortField="ControlMeasures">
                                <ItemTemplate>
                                    <asp:Label ID="lbControlMeasures" runat="server" Text='<%# Bind("ControlMeasures") %>'
                                        ToolTip='<%#Bind("ControlMeasures") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                            <PageItems>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">                              
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
