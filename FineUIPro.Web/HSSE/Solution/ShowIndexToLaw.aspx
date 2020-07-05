<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowIndexToLaw.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Solution.ShowIndexToLaw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看对应标准规范</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:TabStrip ID="TabStrip1" Width="860px" Height="430px" ShowBorder="true" TabPosition="Top"
        EnableTabCloseMenu="false" runat="server" ActiveTabIndex="1">
        <Tabs>
            <f:Tab ID="Tab1" Title="法律法规" BodyPadding="5px" Layout="Fit" runat="server">
                <Items>
                    <f:Grid ID="Grid2" Title="法律法规" ShowHeader="false" EnableCollapse="true" ShowBorder="true"
                        EnableColumnLines="true" IsDatabasePaging="true" runat="server" DataKeyNames="LawRegulationId"
                        DataIDField="LawRegulationId" ForceFit="true">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize+Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="80px" HeaderText="类别" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblLawsRegulationsTypeName" runat="server" Text='<%# ConvertLawsRegulationsType(Eval("LawsRegulationsTypeId")) %>'
                                        ToolTip='<%# ConvertLawsRegulationsType(Eval("LawsRegulationsTypeId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("LawRegulationName") %>' ToolTip='<%# Bind("LawRegulationName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="100px" ColumnID="ApprovalDate" DataField="ApprovalDate" SortField="ApprovalDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="批准日"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="生效日"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField Width="400px" HeaderText="简介及重点关注条款" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Description") %>' ToolTip='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Tab>
            <f:Tab ID="Tab2" Title="标准规范" BodyPadding="5px" Layout="Fit" runat="server">
                <Items>
                    <f:Grid ID="Grid1" Title="标准规范" ShowHeader="false" EnableCollapse="true" ShowBorder="true"
                        IsDatabasePaging="true" runat="server" DataKeyNames="StandardId" DataIDField="StandardId" ForceFit="true"
                        EnableColumnLines="true">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize+Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="StandardGrade" DataField="StandardGrade" FieldType="String"
                                HeaderText="标准级别" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="StandardNo" DataField="StandardNo" FieldType="String"
                                HeaderText="标准号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:TemplateField Width="440px" HeaderText="标准名称" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StandardName") %>' ToolTip='<%# Bind("StandardName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Tab>
        </Tabs>
    </f:TabStrip>
    </form>
</body>
</html>
