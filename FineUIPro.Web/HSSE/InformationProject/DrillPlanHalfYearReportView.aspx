<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillPlanHalfYearReportView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.InformationProject.DrillPlanHalfYearReportView" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看应急演练工作计划半年报</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtprojectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtYearId" runat="server" Label="年度" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtHalfYearId" runat="server" Label="半年" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCompileMan" runat="server" Label="联系人" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTel" runat="server" Label="联系电话" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCompileDate" runat="server" Label="制表时间" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="DrillPlanHalfYearReportItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="DrillPlanHalfYearReportItemId" EnableColumnLines="true"
                        EnableHeaderMenu="false"  Height="330px">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:RenderField Width="180px" ColumnID="DrillPlanName" DataField="DrillPlanName" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练名称">
                                <Editor>
                                    <f:TextBox ID="txtDrillPlanName" Readonly="true" Text='<%# Eval("DrillPlanName")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="OrganizationUnit" DataField="OrganizationUnit"
                                SortField="IndustryType" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="组织单位">
                                <Editor>
                                    <f:TextBox ID="txtOrganizationUnit" Readonly="true" Text='<%# Eval("OrganizationUnit")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DrillPlanDate" DataField="DrillPlanDate" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练时间">
                                <Editor>
                                    <f:TextBox ID="txtDrillPlanDate" Readonly="true" Text='<%# Eval("DrillPlanDate")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="380px" ColumnID="AccidentScene" DataField="AccidentScene" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="主要事故情景">
                                <Editor>
                                    <f:TextBox ID="txtAccidentScene" Readonly="true" Text='<%# Eval("AccidentScene")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="ExerciseWay" DataField="ExerciseWay" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练方式">
                                <Editor>
                                    <f:TextBox ID="txtExerciseWay" Readonly="true" Text='<%# Eval("ExerciseWay")%>' runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DrillPlanHalfYearReportItemId" DataField="DrillPlanHalfYearReportItemId"
                                FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtDrillPlanHalfYearReportItemId" Text='<%# Eval("DrillPlanHalfYearReportItemId")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
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
            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
