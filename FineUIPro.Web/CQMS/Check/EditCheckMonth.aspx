<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCheckMonth.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.EditCheckMonth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质量月报</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
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
                        <f:TextBox runat="server" ID="lbMonths" Readonly="true" Width="180px" LabelWidth="50px" Label="月份" LabelAlign="Left" CssStyle="padding-right:75%"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="1 本月质量管理概况" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:TextArea ID="txtManagementOverview" ShowRedStar="true" Required="true" runat="server" Width="1230px" MaxLength="3000">
                            </f:TextArea>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" Title=" 2 本月项目施工质量管理信息统计  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <Items>
                                <f:ContentPanel runat="server" Title=" 2.1 质量事故情况(无事故时，填“无”)<font color='red'>(*)</font>    "></f:ContentPanel>
                            </Items>
                            <f:TextArea ID="txtAccidentSituation" ShowRedStar="true" Required="true" runat="server" Width="1230px" MaxLength="3000">
                            </f:TextArea>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" Title=" 2.2 质量缺陷/不合格项整改关闭情况 " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="GridRectify" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                EnableColumnLines="true" ForceFit="true" AllowCellEditing="true" ClicksToEdit="1">
                                <Columns>
                                    <f:RenderField Width="150px" ColumnID="Depart" DataField="Depart"
                                        FieldType="String" HeaderText="部门" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="ThisRectifyNum" DataField="ThisRectifyNum"
                                        FieldType="String" HeaderText="本月发出整改项" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtThisRectifyNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("ThisRectifyNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="ThisOKRectifyNum" DataField="ThisOKRectifyNum"
                                        FieldType="String" HeaderText="本月关闭整改项" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtThisOKRectifyNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("ThisOKRectifyNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="TotalRectifyNum" DataField="TotalRectifyNum"
                                        FieldType="String" HeaderText="累计发出整改项" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalRectifyNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalRectifyNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="TotalOKRectifyNum" DataField="TotalOKRectifyNum" FieldType="string" HeaderText="累计关闭整改项" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalOKRectifyNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalOKRectifyNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel4" Title="  2.3 无损检测情况  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="GridNDTCheck" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="" EnableColumnLines="true" EnableSummary="true" SummaryPosition="Flow" ForceFit="true" AllowCellEditing="true" ClicksToEdit="1">
                                <Columns>
                                    <f:TemplateField ColumnID="UnitName" Width="130px" HeaderText="施工分包商" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertUnitName(Eval("UnitId")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:GroupField HeaderText="本月无损检测" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="110px" ColumnID="FilmNum" DataField="FilmNum"
                                                FieldType="String" HeaderText="拍片总数" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtFilmNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("FilmNum") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="NotOKFileNum" DataField="NotOKFileNum"
                                                FieldType="String" HeaderText="不合格(张)" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtNotOKFileNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("NotOKFileNum") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="RepairFileNum" DataField="RepairFileNum"
                                                FieldType="String" HeaderText="已返修(张)" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtRepairFileNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("RepairFileNum") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="OneOKRate" DataField="OneOKRate"
                                                FieldType="String" HeaderText="一次合格率" TextAlign="Center" HeaderTextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>

                                    <f:RenderField Width="120px" ColumnID="TotalFilmNum" DataField="TotalFilmNum"
                                        FieldType="String" HeaderText="累计拍片总数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalFilmNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalFilmNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="TotalNotOKFileNum" DataField="TotalNotOKFileNum"
                                        FieldType="String" HeaderText="累计不合格(张)" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalNotOKFileNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalNotOKFileNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="TotalOneOKRate" DataField="TotalOneOKRate" SortField="AcceptDate" FieldType="string" HeaderText="累计一次合格率" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="110px" ColumnID="UnitId" DataField="UnitId" Hidden="true"
                                        FieldType="String" HeaderText="单位Id" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                                <Listeners>
                                    <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                                </Listeners>
                            </f:Grid>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel5" Title=" 2.4 焊工资格评定情况  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="GridWelder" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false" EnableSummary="true" SummaryPosition="Flow"
                                DataKeyNames="" EnableColumnLines="true" ForceFit="true" AllowCellEditing="true" ClicksToEdit="1">
                                <Columns>
                                    <f:TemplateField ColumnID="UnitName" Width="130px" HeaderText="施工分包商" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# ConvertUnitName(Eval("UnitId")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:GroupField HeaderText="本月焊工入场评定" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="110px" ColumnID="ThisPersonNum" DataField="ThisPersonNum"
                                                FieldType="String" HeaderText="总人数" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtThisPersonNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("ThisPersonNum") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="ThisOKPersonNum" DataField="ThisOKPersonNum"
                                                FieldType="String" HeaderText="合格人数" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtThisOKPersonNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("ThisOKPersonNum") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="ThisOKRate" DataField="ThisOKRate"
                                                FieldType="String" HeaderText="合格率" TextAlign="Center" HeaderTextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField HeaderText="累计" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="110px" ColumnID="TotalPersonNum" DataField="TotalPersonNum"
                                                FieldType="String" HeaderText="总人数" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtTotalPersonNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalPersonNum") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="TotalOKPersonNum" DataField="TotalOKPersonNum"
                                                FieldType="String" HeaderText="合格人数" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtTotalOKPersonNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalOKPersonNum") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="TotalOKRate" DataField="TotalOKRate"
                                                FieldType="String" HeaderText="合格率" TextAlign="Center" HeaderTextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:RenderField Width="110px" ColumnID="UnitId" DataField="UnitId" Hidden="true"
                                        FieldType="String" HeaderText="单位Id" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                                <Listeners>
                                    <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                                </Listeners>
                            </f:Grid>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel6" Title=" 2.5 质量验收情况  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="GridSpotCheckDetail" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false"
                                DataKeyNames="" EnableColumnLines="true" ForceFit="true" AllowCellEditing="true" ClicksToEdit="1">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                        <Items>
                                            <f:TextBox ID="txtMonthOk" runat="server" Label="本月质量验收一次合格率" Width="400px" LabelWidth="190px"></f:TextBox>
                                            <f:TextBox ID="txtAllOk" runat="server" Label="本月质量验收一次合格率" Width="400px" LabelWidth="190px"></f:TextBox>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# GridSpotCheckDetail.PageIndex * GridSpotCheckDetail.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:RenderField Width="180px" ColumnID="ControlPoint" DataField="ControlPoint"
                                        FieldType="String" HeaderText="控制点等级" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="180px" ColumnID="TotalSpotNum" DataField="TotalNum"
                                        FieldType="String" HeaderText="总数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalSpotNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="180px" ColumnID="ThisSpotOKNum" DataField="ThisOKNum"
                                        FieldType="String" HeaderText="本月完成" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtThisSpotOKNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("ThisOKNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="180px" ColumnID="TotalSpotOKNum" DataField="TotalOKNum"
                                        FieldType="String" HeaderText="累计完成" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtTotalSpotOKNum" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalOKNum") %>'>
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="180px" ColumnID="TotalSpotOKRate" DataField="TotalOKRate"
                                        FieldType="String" HeaderText="累计完成百分比" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>

                                </Columns>
                                <Listeners>
                                    <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                                </Listeners>
                            </f:Grid>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel7" Title=" 2.6 特种设备安装告知、监检情况（在现场图纸不全时，总数为动态数字，是根据实际到场的图纸统计特种设备的总数，当图纸全部到现场后，总数即为固定数字）  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="GridSpecialEquipmentDetail" IsFluid="true" CssClass="blockpanel" ClicksToEdit="1" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false" DataIDField="SpecialEquipmentDetailId"
                                ForceFit="true" EnableColumnLines="true" AllowCellEditing="true">
                                <Columns>

                                    <f:RenderField Width="130px" ColumnID="SpecialEquipmentName" DataField="SpecialEquipmentName"
                                        FieldType="String" HeaderText="设备名称" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="130px" ColumnID="TotalNum" DataField="TotalNum"
                                        HeaderText="总数" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:TextBox ID="txtTotalNum" Required="true" runat="server" Text='<%# Bind("TotalNum") %>'>
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>

                                    <f:GroupField HeaderText="安装告知" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="110px" ColumnID="ThisCompleteNum1" DataField="ThisCompleteNum1" FieldType="String" HeaderText="本月完成" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtThisCompleteNum1" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("ThisCompleteNum1") %>'>
                                                    </f:NumberBox>
                                                </Editor>

                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="TotalCompleteNum1" DataField="TotalCompleteNum1" FieldType="String" HeaderText="累计完成" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtTotalCompleteNum1" runat="server" NoDecimal="true" NoNegative="true" Text='<%# Bind("TotalCompleteNum1") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="TotalRate1" DataField="TotalRate1"
                                                FieldType="String" HeaderText="累计完成%" TextAlign="Center" HeaderTextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField HeaderText="安装监检" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="110px" ColumnID="ThisCompleteNum2" DataField="ThisCompleteNum2" FieldType="String" HeaderText="本月完成" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtThisCompleteNum2" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("ThisCompleteNum2") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="TotalCompleteNum2" DataField="TotalCompleteNum2" FieldType="String" HeaderText="累计完成" TextAlign="Center" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtTotalCompleteNum2" NoDecimal="true" NoNegative="true" runat="server" Text='<%# Bind("TotalCompleteNum2") %>'>
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="110px" ColumnID="TotalRate2" DataField="TotalRate2"
                                                FieldType="String" HeaderText="累计完成%" TextAlign="Center" HeaderTextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                </Columns>
                                <Listeners>
                                    <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                                </Listeners>
                            </f:Grid>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel8" Title="2.7 设计变更情况  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Grid ID="GridDesign" IsFluid="true" CssClass="blockpanel" Height="250px" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="false" OnRowCommand="GridDesign_RowCommand"
                                DataKeyNames="MonthDesignId" DataIDField="MonthDesignId" EnableColumnLines="true" ForceFit="true" AllowCellEditing="true" ClicksToEdit="1">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                                        <Items>
                                            <f:Button ID="btnNew" ToolTip="新增" Text="新增" Icon="Add" EnablePostBack="true"
                                                runat="server" OnClick="btnNew_Click">
                                            </f:Button>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                        EnableLock="true" Locked="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# GridDesign.PageIndex * GridDesign.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:RenderField Width="100px" ColumnID="DesignCode" DataField="DesignCode"
                                        FieldType="String" HeaderText="变更编号" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:TextBox ID="txtDesignCode" runat="server" Text='<%# Bind("DesignCode") %>'>
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="100px" ColumnID="MainItemId" DataField="MainItemId"
                                        FieldType="String" HeaderText="主项" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                        <Editor>
                                            <f:DropDownList runat="server" ID="drpMainItemId" EnableEdit="true"></f:DropDownList>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="110px" ColumnID="DesignProfessionalId" DataField="DesignProfessionalId"
                                        FieldType="String" HeaderText="专业" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:DropDownList runat="server" ID="drpDesignProfessionalId" EnableEdit="true"></f:DropDownList>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="110px" ColumnID="State" DataField="State"
                                        FieldType="String" HeaderText="施工完成情况" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:DropDownList runat="server" ID="drpState" EnableEdit="true">
                                                <f:ListItem Text="已完成" Value="已完成" />
                                                <f:ListItem Text="未完成" Value="未完成" />
                                            </f:DropDownList>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="110px" ColumnID="Remark" DataField="Remark"
                                        FieldType="String" HeaderText="备注" TextAlign="Center" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>'>
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:LinkButtonField Width="30px" TextAlign="Center" HeaderText="删除" ToolTip="删除" CommandName="del"
                                        Icon="Delete" />
                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel11" Title="2.8 施工资料编制情况<font color='red'>(*)</font>  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">

                            <Items>
                                <f:TextBox ID="txtMonthDataOk" runat="server" Label="本月质量记录同步率" Width="600px" LabelWidth="160px"></f:TextBox>
                                <f:TextBox ID="txtAllDataOk" runat="server" Label="本月质量记录同步率" Width="600px" LabelWidth="160px"></f:TextBox>
                                <f:TextArea ID="txtConstructionData" ShowRedStar="true" Required="true" runat="server" Width="1230px" MaxLength="3000"></f:TextArea>
                            </Items>


                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel9" Title=" 3 下月项目质量管理主要工作计划安排（根据本月质量检查过程中发现的施工质量体系运行出现的问题及施工过程中经常出现的质量问题和下月施工进度计划，填写下月的质量重点检查计划）<font color='red'>(*)</font> " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:TextArea ID="txtNextMonthPlan" ShowRedStar="true" Required="true" runat="server" Width="1230px" MaxLength="3000">
                            </f:TextArea>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel10" Title=" 4 需要解决和协调的事项  " ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:TextArea ID="txtNeedSolved" runat="server" Width="1230px" MaxLength="3000">
                            </f:TextArea>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckerId" runat="server">
                        </f:HiddenField>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false"
                            runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
    <script type="text/javascript">
        function onGridAfterEdit(event, value, params) {
            var me = this, columnId = params.columnId, rowId = params.rowId;

            if (columnId === 'TotalNum' || columnId === 'TotalCompleteNum1') {
                var TotalNum = me.getCellValue(rowId, 'TotalNum');
                var TotalCompleteNum1 = me.getCellValue(rowId, 'TotalCompleteNum1');
                if (TotalNum.toString() != "" && TotalCompleteNum1.toString() != "") {
                    if (TotalNum !== "0") {
                        me.updateCellValue(rowId, 'TotalRate1', (TotalCompleteNum1 / TotalNum * 100).toFixed(2) + "%");
                    } else {
                        me.updateCellValue(rowId, 'TotalRate1', (""));
                    }

                }
            }

            if (columnId === 'TotalNum' || columnId === 'TotalCompleteNum2') {
                var TotalNum = me.getCellValue(rowId, 'TotalNum');
                var TotalCompleteNum2 = me.getCellValue(rowId, 'TotalCompleteNum2');

                if (TotalNum.toString() != "" && TotalCompleteNum2.toString() != "") {
                    if (TotalNum !== "0") {
                        me.updateCellValue(rowId, 'TotalRate2', (TotalCompleteNum2 / TotalNum * 100).toFixed(2) + "%");
                    } else {
                        me.updateCellValue(rowId, 'TotalRate2', (""));
                    }

                }
            }

            if (columnId === 'FilmNum' || columnId === 'NotOKFileNum') {
                var FilmNum = me.getCellValue(rowId, 'FilmNum');
                var NotOKFileNum = me.getCellValue(rowId, 'NotOKFileNum');
                if (FilmNum.toString() != "" && NotOKFileNum.toString() != "") {
                    if (FilmNum !== "0") {
                        me.updateCellValue(rowId, 'OneOKRate', ((FilmNum - NotOKFileNum) / FilmNum * 100).toFixed(2) + "%");
                    } else {
                        me.updateCellValue(rowId, 'OneOKRate', (""));
                    }
                }
                 updateSummary();
            }
            if (columnId === 'TotalFilmNum' || columnId === 'TotalNotOKFileNum') {
                var TotalFilmNum = me.getCellValue(rowId, 'TotalFilmNum');
                var TotalNotOKFileNum = me.getCellValue(rowId, 'TotalNotOKFileNum');
                if (TotalFilmNum.toString() != "" && TotalNotOKFileNum.toString() != "") {
                    if (TotalFilmNum !== "0") {
                        me.updateCellValue(rowId, 'TotalOneOKRate', ((TotalFilmNum - TotalNotOKFileNum) / TotalFilmNum * 100).toFixed(2) + "%");
                    } else {
                        me.updateCellValue(rowId, 'TotalOneOKRate', (""));
                    }
                }
                 updateSummary();
            }
            if (columnId === 'ThisPersonNum' || columnId === 'ThisOKPersonNum') {
                var ThisPersonNum = me.getCellValue(rowId, 'ThisPersonNum');
                var ThisOKPersonNum = me.getCellValue(rowId, 'ThisOKPersonNum');
                if (ThisPersonNum.toString() != "" && ThisOKPersonNum.toString() != "") {
                    if (ThisPersonNum !== "0") {
                        me.updateCellValue(rowId, 'ThisOKRate', (ThisOKPersonNum / ThisPersonNum * 100).toFixed(2) + "%");
                    } else {
                        me.updateCellValue(rowId, 'ThisOKRate', (""));
                    }
                }
                updateSummary();
            }

            if (columnId === 'TotalPersonNum' || columnId === 'TotalOKPersonNum') {
                var TotalPersonNum = me.getCellValue(rowId, 'TotalPersonNum');
                var TotalOKPersonNum = me.getCellValue(rowId, 'TotalOKPersonNum');
                if (TotalPersonNum.toString() != "" && TotalOKPersonNum.toString() != "") {
                    if (TotalPersonNum !== "0") {
                        me.updateCellValue(rowId, 'TotalOKRate', (TotalOKPersonNum / TotalPersonNum * 100).toFixed(2) + "%");
                    } else {
                        me.updateCellValue(rowId, 'TotalOKRate', (""));
                    }
                }
                updateSummary();
            }
            if (columnId === 'TotalSpotNum' || columnId === 'TotalSpotOKNum') {
                var TotalSpotNum = me.getCellValue(rowId, 'TotalSpotNum');
                var TotalSpotOKNum = me.getCellValue(rowId, 'TotalSpotOKNum');
                if (TotalSpotNum.toString() != "" && TotalSpotOKNum.toString() != "") {
                    if (TotalSpotNum !== "0") {
                        me.updateCellValue(rowId, 'TotalSpotOKRate', ((TotalSpotOKNum) / TotalSpotNum * 100).toFixed(2) + "%");
                    } else {
                        me.updateCellValue(rowId, 'TotalSpotOKRate', (""));
                    }
                }
            }
        }

        function updateSummary() {
            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
        }
    </script>
</body>
</html>
