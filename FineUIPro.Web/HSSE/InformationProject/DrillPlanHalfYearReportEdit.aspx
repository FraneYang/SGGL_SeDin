<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillPlanHalfYearReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.InformationProject.DrillPlanHalfYearReportEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急演练工作计划半年报</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">      
        <Rows>
            <f:FormRow>
                <Items>
                   <f:TextBox ID="txtprojectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:DropDownList ID="ddlYearId" runat="server" Label="年度" Required="True" ShowRedStar="True">
                    </f:DropDownList>
                    <f:DropDownList ID="ddlHalfYearId" runat="server" Label="半年" Required="True" ShowRedStar="True">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCompileMan" runat="server" MaxLength="50" Label="联系人" LabelAlign="Right" Required="true" ShowRedStar="true" >
                    </f:TextBox>
                    <f:TextBox ID="txtTel" runat="server" MaxLength="50" Label="联系电话" LabelAlign="Right">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="制表时间" ID="txtCompileDate"
                        Required="True" ShowRedStar="True" LabelAlign="Right">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="DrillPlanHalfYearReportItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="DrillPlanHalfYearReportItemId" EnableColumnLines="true"
                        OnRowCommand="Grid1_RowCommand" EnableHeaderMenu="false" >
                        <Columns>
                            <f:LinkButtonField Width="40px" ConfirmTarget="Parent" CommandName="Add" Icon="Add"
                                TextAlign="Center" />
                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                Icon="Delete" TextAlign="Center" />
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="180px" ColumnID="DrillPlanName" DataField="DrillPlanName" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练名称">
                                <Editor>
                                    <f:TextBox ID="txtDrillPlanName" MaxLength="200" Text='<%# Eval("DrillPlanName")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="OrganizationUnit" DataField="OrganizationUnit"
                                SortField="IndustryType" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="组织单位">
                                <Editor>
                                    <f:TextBox ID="txtOrganizationUnit" MaxLength="100" Text='<%# Eval("OrganizationUnit")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DrillPlanDate" DataField="DrillPlanDate" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练时间">
                                <Editor>
                                    <f:TextBox ID="txtDrillPlanDate" MaxLength="50" Text='<%# Eval("DrillPlanDate")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="380px" ColumnID="AccidentScene" DataField="AccidentScene" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="主要事故情景" ExpandUnusedSpace="true">
                                <Editor>
                                    <f:TextBox ID="txtAccidentScene" MaxLength="1000" Text='<%# Eval("AccidentScene")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="220px" ColumnID="ExerciseWay" DataField="ExerciseWay" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练方式">
                                <Editor>
                                    <f:TextBox ID="txtExerciseWay" MaxLength="50" Text='<%# Eval("ExerciseWay")%>' runat="server">
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
    </form>
</body>
</html>
