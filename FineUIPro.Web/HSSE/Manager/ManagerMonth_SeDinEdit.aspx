<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerMonth_SeDinEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Manager.ManagerMonth_SeDinEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑管理月报</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .table {
            border-collapse: collapse;
            text-align: center;
            table-layout: fixed;
            width: 100%;
        }

        .input {
            border: none;
            width: 100%;
            background-color: transparent;
            text-align: center;
        }

        .table td, .table th {
            border: 1px solid #cad9ea;
            color: #666;
            height: 30px;
        }

        .table thead th {
            /*                background-color: #CCE8EB;*/
            width: 200px;
        }

        .table tr {
            /*                background: #fff;*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow BlockMD="12">
                    <Items>
                        <f:ContentPanel ID="ContentPanel7" BodyPadding="10" ShowBorder="true" EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="基本信息" runat="server">
                            <Items>
                                <f:Panel ID="Panel1" Layout="Block" CssClass="blockpanel myblockform" BlockMD="12" ShowHeader="false" ShowBorder="false"
                                    BlockConfigSpace="5px" BodyPadding="5px" EnableCollapse="true" runat="server">
                                    <Items>
                                        <f:DatePicker runat="server"  DateFormatString="yyyy-MM" Label="月报月份" EmptyText="请选择年月" BlockMD="4"
                                            ID="ReporMonth" LabelAlign="right" DisplayType="Month" ShowTodayButton="false">
                                        </f:DatePicker>
                                        <f:DatePicker runat="server" Label="报告日期" BlockMD="4" ID="StartDate" LabelAlign="right" >
                                        </f:DatePicker>
                                        <f:DatePicker runat="server" Label="至" BlockMD="4" ID="EndDate"
                                            LabelAlign="right" LabelWidth="50px" >
                                        </f:DatePicker>
                                    </Items>
                                </f:Panel>

                                <f:Panel ID="Panel2" Layout="Block" CssClass="blockpanel myblockform" BlockMD="12" ShowHeader="false" ShowBorder="false"
                                    BlockConfigSpace="5px" BodyPadding="5px" EnableCollapse="true" runat="server">
                                    <Items>
                                        <f:DropDownList runat="server" ID="CompileManId"  LabelAlign="Right"  BlockMD="3" Label="报告人">
                                        </f:DropDownList>
                                        <f:DropDownList runat="server" ID="AuditManId"  LabelAlign="Right"  BlockMD="3" Label="审核人">
                                        </f:DropDownList>
                                        <f:DropDownList runat="server" ID="ApprovalManId"  LabelAlign="Right"  BlockMD="3" Label="批准人">
                                        </f:DropDownList>
                                        <f:DatePicker runat="server" Label="报告截至日期" LabelWidth="140px" BlockMD="3" ID="DueDate" LabelAlign="Right">
                                        </f:DatePicker>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>

                <f:FormRow >
                    <Items>
                        <f:Panel ID="pan" Layout="Block" CssClass="blockpanel myblockform" BlockMD="12"
                            BlockConfigSpace="10px" BodyPadding="5px" Title="项目信息" EnableCollapse="true" runat="server">
                            <Items>

                                <f:TextBox ID="projectCode" runat="server" Label="项目编号" BlockMD="4" Readonly="true" MaxLength="50">
                                </f:TextBox>
                                <f:Label ID="projectName" Label="项目名称" runat="server" BlockMD="4"></f:Label>
                                <f:TextBox ID="projectType" runat="server" Label="项目类型"  BlockMD="4" MaxLength="50">
                                </f:TextBox>

                                <f:TextBox ID="ContractAmount" runat="server" Label="合同额" BlockMD="4" MaxLength="50">
                                </f:TextBox>
                                <f:TextBox ID="ConstructionStage" runat="server" Label="所处的施工阶段"  LabelWidth="120px" BlockMD="4" MaxLength="50">
                                </f:TextBox>
                                <f:TextBox ID="ProjectAddress" runat="server" Label="项目所在地"  BlockMD="4" MaxLength="50">
                                </f:TextBox>

                                <f:TextBox ID="ProjectManager" runat="server" Label="项目经理及联系方式" LabelWidth="150px" BlockMD="3" MaxLength="50">
                                </f:TextBox>
                                <f:TextBox ID="HsseManager" runat="server" Label="安全经理及联系方式" LabelWidth="150px" BlockMD="3" MaxLength="50">
                                </f:TextBox>
                                <f:DatePicker runat="server"  BlockMD="3" LabelAlign="right" DateFormatString="yyyy-MM-dd" Label="合同工期时间"
                                    EmptyText="请选择开始日期" LabelWidth="120px"
                                    ID="pStartDate">
                                </f:DatePicker>
                                <f:DatePicker runat="server" Label="至" ID="pEndDate" BlockMD="3" EmptyText="请选择结束日期"
                                    LabelWidth="40px" >
                                </f:DatePicker>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" BodyPadding="10" ShowBorder="true" EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目安全工时统计" runat="server">
                            <table style="width: 100%" id="SeDinMonthReport3Item" runat="server">
                                <tr>
                                    <td>
                                        <f:TextBox ID="MonthWorkTime" runat="server" Label="当月安全人工时" LabelAlign="Right" LabelWidth="160px" BlockMD="4" MaxLength="50">
                                        </f:TextBox>
                                    </td>
                                    <td>
                                        <f:TextBox ID="YearWorkTime" runat="server" Label="年度累计安全人工时" LabelAlign="Right" BlockMD="4" LabelWidth="160px" MaxLength="50">
                                        </f:TextBox>
                                    </td>
                                    <td>
                                        <f:TextBox ID="ProjectWorkTime" runat="server" Label="项目累计安全人工时" LabelAlign="Right" BlockMD="4" LabelWidth="160px" MaxLength="50">
                                        </f:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <f:TextBox ID="TotalLostTime" runat="server" Label="总损失工时" LabelWidth="160px" LabelAlign="Right" BlockMD="4" MaxLength="50">
                                        </f:TextBox>

                                    </td>
                                    <td>
                                        <f:TextBox ID="MillionLossRate" runat="server" Label="百万工时损失率" BlockMD="4" LabelAlign="Right" LabelWidth="160px" MaxLength="50">
                                        </f:TextBox>

                                    </td>
                                    <td>
                                        <f:TextBox ID="TimeAccuracyRate" runat="server" Label="工时统计准确率" BlockMD="4" LabelAlign="Right" LabelWidth="160px" MaxLength="50">
                                        </f:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: unset; padding-left: 30px">
                                <tr>                                    
                                    <td>
                                        <f:DatePicker runat="server"  LabelWidth="150px" LabelAlign="right" DateFormatString="yyyy-MM-dd"
                                            EmptyText="请选择开始日期" Label="本项目" 
                                            ID="PsafeStartDate">
                                        </f:DatePicker>
                                    </td>
                                    <td>
                                        <f:DatePicker runat="server" Label="至" ID="PsafeEndDate" EmptyText="请选择结束日期"
                                            LabelWidth="40px" >
                                        </f:DatePicker>
                                    </td>                                  
                                    <td>
                                        <f:TextBox ID="SafeWorkTime" runat="server" MaxLength="50" Label="安全生产" LabelWidth="80px" Width="250px">
                                        </f:TextBox>
                                    </td>
                                    <td>
                                        <f:Label runat="server" Text="人工时，无可记录事故"></f:Label>
                                    </td>
                                </tr>

                            </table>
                        </f:ContentPanel>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" BodyPadding="10" ShowBorder="true" EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目HSE事故、事件统计" runat="server">
                            <table class="table">
                                <tr>
                                    <th colspan="2">事故类型
                                    </th>
                                    <th>本月次数
                                    </th>
                                    <th>次数累计
                                    </th>
                                    <th>损失工时（本月）
                                    </th>
                                    <th>损失工时（累计）
                                    </th>
                                    <th>经济损失（本月）
                                    </th>
                                    <th>经济损失（累计）
                                    </th>
                                    <th>人数当月
                                    </th>
                                    <th>人数累计
                                    </th>
                                </tr>
                                <tr>
                                    <td rowspan="4">
                                        <label id="BigType" runat="server" ></label>
                                    </td>
                                    <td>
                                        <label id="AccidentType1" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" id="MonthTimes1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" id="TotalTimes1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" id="MonthLossTime1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" id="TotalLossTime1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" id="MonthMoney1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" id="TotalMoney1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" id="MonthPersons1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" id="TotalPersons1" class="input" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="AccidentType2" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons2" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="AccidentType3" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons3" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label id="AccidentType4" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons4" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="AccidentType5" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons5" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="AccidentType6" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons6" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="AccidentType7" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons7" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="AccidentType8" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons8" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="AccidentType9" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons9" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="AccidentType10" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons10" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label id="AccidentType11" runat="server"></label>
                                    </td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthTimes11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalTimes11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthLossTime11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalLossTime11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthMoney11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalMoney11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthPersons11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="TotalPersons11" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="10" style="color: red">事故综述（含未遂事故、事件）</td>

                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <textarea id="AccidentsSummary" class="input" runat="server" style="text-align: left"></textarea></td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" BodyPadding="10" ShowBorder="true" EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="本月人员投入情况" runat="server">

                            <f:Grid ID="GvSeDinMonthReport4Item" CssClass="blockpanel" AllowCellEditing="true" EnableColumnLines="true" ShowBorder="true" ShowHeader="false" ForceFit="true" EnableCollapse="false" runat="server">
                                <Columns>
                                    <f:BoundField ColumnID="UnitName" DataField="UnitName" Width="80px" HeaderText="单位名称" />
                                    <f:GroupField ColumnID="henan" HeaderText="管理人员" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="100px" ColumnID="SafeManangerNum" DataField="SafeManangerNum" FieldType="Int"
                                                HeaderText="安全管理">
                                                <Editor>
                                                    <f:NumberBox ID="SafeMananger" NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="OtherManangerNum" DataField="OtherManangerNum" FieldType="Int"
                                                HeaderText="其他管理">
                                                <Editor>
                                                    <f:NumberBox ID="OtherMananger" NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>

                                        </Columns>

                                    </f:GroupField>
                                    <f:GroupField ColumnID="Special" HeaderText="作业人员" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="100px" ColumnID="SpecialWorkerNum" DataField="SpecialWorkerNum" FieldType="Int"
                                                HeaderText="特种作业">
                                                <Editor>
                                                    <f:NumberBox ID="nbSpecialWorkerNum" NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="GeneralWorkerNum" DataField="GeneralWorkerNum" FieldType="Int"
                                                HeaderText="一般作业">
                                                <Editor>
                                                    <f:NumberBox ID="nbGeneralWorkerNum" NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>

                                        </Columns>

                                    </f:GroupField>
                                    <f:BoundField ColumnID="TotalNum" DataField="TotalNum" Width="80px" HeaderText="合计" />
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
                        <f:ContentPanel ID="ContentPanel4" BodyPadding="10" ShowBorder="true" EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="本月大型、特种设备投入情况" runat="server">
                            <f:Grid ID="GvSeDinMonthReport5Item"  CssClass="table" ShowBorder="true" AllowCellEditing="true" EnableColumnLines="true" ShowHeader="false" ForceFit="true" EnableCollapse="false" runat="server">
                                <Columns>
                                    <f:BoundField ColumnID="UnitName" DataField="UnitName" Width="80px" HeaderText="单位名称" />

                                    <f:GroupField ColumnID="henan" HeaderText="特种设备T" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="100px" ColumnID="T01" DataField="T01" FieldType="Int"
                                                HeaderText="汽车吊T-001">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="T02" DataField="T02" FieldType="Int"
                                                HeaderText="履带吊">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="T03" DataField="T03" FieldType="Int"
                                                HeaderText="塔吊">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="T04" DataField="T04" FieldType="Int"
                                                HeaderText="门式起重机">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="T05" DataField="T05" FieldType="Int"
                                                HeaderText="升降机">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="T06" DataField="T06" FieldType="Int"
                                                HeaderText="叉车">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        
                                        </Columns>

                                    </f:GroupField>
                                    <f:GroupField ColumnID="Special" HeaderText="大型机具设备" TextAlign="Center">
                                        <Columns>
                                              <f:RenderField Width="100px" ColumnID="D01" DataField="D01" FieldType="Int"
                                                HeaderText="挖掘机">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                              <f:RenderField Width="100px" ColumnID="D02" DataField="D02" FieldType="Int"
                                                HeaderText="装载机">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                              <f:RenderField Width="100px" ColumnID="D03" DataField="D03" FieldType="Int"
                                                HeaderText="拖板车">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                              <f:RenderField Width="100px" ColumnID="D04" DataField="D04" FieldType="Int"
                                                HeaderText="桩机">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        
                                        </Columns>

                                    </f:GroupField>
                                    <f:GroupField ColumnID="Sp" HeaderText="大型机具设备" TextAlign="Center">
                                        <Columns>
                                              <f:RenderField Width="100px" ColumnID="S01" DataField="S01" FieldType="Int"
                                                HeaderText="吊篮">
                                                <Editor>
                                                    <f:NumberBox NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                         
                                        </Columns>
                                    </f:GroupField>
                                </Columns>

                            </f:Grid>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel5" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="安全生产费用投入情况" runat="server">
                            <table class="table">
                                <tr>
                                    <td></td>
                                    <td>安全防护投入</td>
                                    <td>劳动保护及职业健康投入</td>
                                    <td>安全技术进步投入</td>
                                    <td>安全教育培训投入</td>
                                    <td>合计</td>
                                    <td>完成合同额</td>
                                </tr>
                                <tr>
                                    <td>本月</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SafetyMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SafetyYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SafetyTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="LaborMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="LaborYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="LaborTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>年度累计</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ProgressMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ProgressYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ProgressTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EducationMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EducationYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EducationTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>项目累计</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SumMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SumYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SumTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ContractMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ContractYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ContractTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="3">工程造价占比</td>
                                    <td colspan="4">
                                        <input type="text" style="text-align: left" runat="server" class="input" id="ConstructionCost" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel6" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目HSE培训统计" runat="server">
                            <table class="table">
                                <tr>
                                    <td rowspan="2">培训课程类型</td>
                                    <td colspan="3">次数</td>
                                    <td colspan="3">参加人次</td>
                                </tr>
                                <tr>
                                    <td>本月</td>
                                    <td>本年度</td>
                                    <td>项目累计</td>
                                    <td>本月</td>
                                    <td>本年度</td>
                                    <td>项目累计</td>
                                </tr>
                                <tr>
                                    <td>专项安全培训</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialMontNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialYearNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialMontPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialYearPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialTotalPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>员工入场安全培训</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EmployeeMontNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EmployeeYearNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EmployeeTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EmployeeMontPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EmployeeYearPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="EmployeeTotalPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="CpReport8" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目HSE会议统计" runat="server">
                            <table class="table">
                                <tr>
                                    <td>会议类型</td>
                                    <td>次数（本月）</td>
                                    <td>次数（累计）</td>
                                    <td>参会人次（本月）</td>
                                </tr>
                                <tr>
                                    <td>周例会</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8WeekMontNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8WeekTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8WeekMontPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>月例会（安委会）</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8MonthMontNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8MonthTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8MonthMontPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>专题会议</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8SpecialMontNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8SpecialTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="Report8SpecialMontPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                            </table>
                            <f:Grid ID="GvSeDinMonthReport8Item" CssClass="blockpanel table" AllowCellEditing="true" ShowBorder="true" EnableColumnLines="true" ShowHeader="true" Title="班前会" ForceFit="true" EnableCollapse="false" runat="server">          
                                <Columns>
                                    <f:BoundField ColumnID="UnitName" DataField="UnitName" Width="80px" HeaderText="单位名称" />
                                    <f:BoundField ColumnID="TeamName" DataField="TeamName" Width="80px" HeaderText="班组名称" />
                                     <f:RenderField Width="100px" ColumnID="ClassNum" DataField="ClassNum" FieldType="Int"
                                                HeaderText="会议次数（本月）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                     <f:RenderField Width="100px" ColumnID="ClassPersonNum" DataField="ClassPersonNum" FieldType="Int"
                                                HeaderText="参会人数累计（本月）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
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
                        <f:ContentPanel ID="CpSeDinMonthReport9Item" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目HSE检查统计" runat="server">
                            <table class="table">
                                <tr>
                                    <td colspan="2">检查类型</td>
                                    <td>次数（本月）</td>
                                    <td>次数（本年度累计）</td>
                                    <td>次数（项目总累计）</td>
                                </tr>
                                <tr>
                                    <td colspan="2">日常巡检</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="DailyMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="DailyYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="DailyTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">周联合检查</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="WeekMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="WeekYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="WeekTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">专项检查</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SpecialTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">月综合HSE检查</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthlyMonth" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthlyYear" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MonthlyTotal" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>


                            </table>
                            <f:Grid ID="GvSeDinMonthReport9ItemSpecial" CssClass="blockpanel table" AllowCellEditing="true" ShowBorder="true" EnableColumnLines="true" ShowHeader="true" Title="专项检查" ForceFit="true" EnableCollapse="false" runat="server">
                             
                                <Columns>
                                    <f:BoundField ColumnID="TypeName" DataField="TypeName" Width="80px" HeaderText="类型" />
                                     <f:RenderField Width="100px" ColumnID="CheckMonth" DataField="CheckMonth" FieldType="Int"
                                                HeaderText="检查次数（本月）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                     <f:RenderField Width="100px" ColumnID="CheckYear" DataField="CheckYear" FieldType="Int"
                                                HeaderText="次数（本年度累计）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                     <f:RenderField Width="100px" ColumnID="CheckTotal" DataField="CheckTotal" FieldType="Int"
                                                HeaderText="次数（项目总累计）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                </Columns>
                            </f:Grid>

                            <f:Grid ID="GvSeDinMonthReport9ItemRect" CssClass="blockpanel table" AllowCellEditing="true" ShowBorder="true" EnableColumnLines="true" ShowHeader="true" Title="隐患整改单" ForceFit="true" EnableCollapse="false" runat="server">           
                                <Columns>
                                    <f:BoundField ColumnID="UnitName" DataField="UnitName" Width="80px" HeaderText="单位名称" />
                                     <f:RenderField Width="100px" ColumnID="IssuedMonth" DataField="IssuedMonth" FieldType="Int"
                                                HeaderText="下发数量（本月）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                     <f:RenderField Width="100px" ColumnID="RectificationMoth" DataField="RectificationMoth" FieldType="Int"
                                                HeaderText="整改完成数量（本月）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                     <f:RenderField Width="100px" ColumnID="IssuedTotal" DataField="IssuedTotal" FieldType="Int"
                                                HeaderText="下发数量（累计）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                     <f:RenderField Width="100px" ColumnID="RectificationTotal" DataField="RectificationTotal" FieldType="Int"
                                                HeaderText="整改完成数量（累计）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                               <%--     <f:BoundField ColumnID="IssuedMonth" DataField="IssuedMonth" Width="80px" HeaderText="下发数量（本月）" />
                                    <f:BoundField ColumnID="RectificationMoth" DataField="RectificationMoth" Width="80px" HeaderText="整改完成数量（本月）" />
                                    <f:BoundField ColumnID="IssuedTotal" DataField="IssuedTotal" Width="80px" HeaderText="下发数量（累计）" />
                                    <f:BoundField ColumnID="RectificationTotal" DataField="RectificationTotal" Width="80px" HeaderText="改完成数量（累计）" />--%>
                                </Columns>
                            </f:Grid>

                            <f:Grid ID="GvSeDinMonthReport9ItemStoppage" CssClass="blockpanel table" AllowCellEditing="true" ShowBorder="true" EnableColumnLines="true" ShowHeader="true" Title="停工令" ForceFit="true" EnableCollapse="false" runat="server">
                               
                                <Columns>
                                    <f:BoundField ColumnID="UnitName" DataField="UnitName" Width="80px" HeaderText="单位名称" />
                                      <f:RenderField Width="100px" ColumnID="IssuedMonth" DataField="IssuedMonth" FieldType="Int"
                                                HeaderText="下发数量（本月）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                      <f:RenderField Width="100px" ColumnID="StoppageMonth" DataField="StoppageMonth" FieldType="Int"
                                                HeaderText="停工天数（本月）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                      <f:RenderField Width="100px" ColumnID="IssuedTotal" DataField="IssuedTotal" FieldType="Int"
                                                HeaderText="下发数量（累计）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                                      <f:RenderField Width="100px" ColumnID="StoppageTotal" DataField="StoppageTotal" FieldType="Int"
                                                HeaderText="停工天数（累计）">
                                                <Editor>
                                                    <f:NumberBox  NoDecimal="true" NoNegative="true"
                                                        MaxValue="10000" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                       </f:RenderField>
                               <%--     <f:BoundField ColumnID="IssuedMonth" DataField="IssuedMonth" Width="80px" HeaderText="下发数量（本月）" />
                                    <f:BoundField ColumnID="StoppageMonth" DataField="StoppageMonth" Width="80px" HeaderText="停工天数（本月）" />
                                    <f:BoundField ColumnID="IssuedTotal" DataField="IssuedTotal" Width="80px" HeaderText="下发数量（累计）" />
                                    <f:BoundField ColumnID="StoppageTotal" DataField="StoppageTotal" Width="80px" HeaderText="停工天数（累计）" />--%>
                                </Columns>
                            </f:Grid>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel9" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目奖惩情况统计" runat="server">
                            <table class="table">
                                <tr>
                                    <td>类型</td>
                                    <td>内容</td>
                                    <td>次数（本月）</td>
                                    <td>次数（累计）</td>
                                    <td>金额（本月）</td>
                                    <td>金额（累计）</td>
                                </tr>
                                <tr>
                                    <td rowspan="3">奖励</td>
                                    <td>安全工时奖</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SafeMonthNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SafeTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SafeMonthMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SafeTotalMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>HSE绩效考核奖励</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="HseMonthNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="HseTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="HseMonthMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="HseTotalMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>安全生产先进个人奖</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ProduceMonthNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ProduceTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ProduceMonthMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ProduceTotalMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td rowspan="3">处罚</td>
                                    <td>事故责任处罚</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="AccidentMonthNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="AccidentTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="AccidentMonthMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="AccidentTotalMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>违章处罚</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ViolationMonthNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ViolationTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ViolationMonthMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ViolationTotalMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                                <tr>
                                    <td>安全管理处罚</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ManageMonthNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ManageTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ManageMonthMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="ManageTotalMoney" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel10" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目危大工程施工情况" runat="server">
                            <table class="table">
                                <tr>
                                    <td>类别</td>
                                    <td>本月正在施工</td>
                                    <td>已完工</td>
                                    <td>下月施工计划</td>
                                </tr>
                                <tr>
                                    <td>危险性较大分部分项工程</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="RiskWorkNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="RiskFinishedNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="RiskWorkNext" /></td>
                                </tr>
                                <tr>
                                    <td>超过一定规模危大工程</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="LargeWorkNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="LargeFinishedNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="LargeWorkNext" /></td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel11" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="项目应急演练情况" runat="server">
                            <table class="table">
                                <tr>
                                    <th colspan="2">类别</th>
                                    <th>直接投入</th>
                                    <th>参演人数</th>
                                    <th>本月次数</th>
                                    <th>项目累计次数</th>
                                    <th>下月计划</th>
                                </tr>
                                <tr>
                                    <td rowspan="2">综合演练</td>
                                    <td>现场演练</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleSiteInput" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleSitePerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleSiteNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleSiteTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <textarea runat="server" class="input" style="text-align: left" id="MultipleSiteNext"></textarea>
                                    </td>


                                </tr>
                                <tr>
                                    <td>桌面演练</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleDesktopInput" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleDesktopPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleDesktopNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="MultipleDesktopTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <textarea runat="server" class="input" style="text-align: left" id="MultipleDesktopNext"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2">单项演练</td>
                                    <td>现场演练</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleSiteInput" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleSitePerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleSiteNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleSiteTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <textarea runat="server" class="input" style="text-align: left" id="SingleSiteNext"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td>桌面演练</td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleDesktopInput" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleDesktopPerson" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleDesktopNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <input type="text" runat="server" class="input" id="SingleDesktopTotalNum" maxlength="10" oninput="value=value.replace(/[^\d]/g,'')" /></td>
                                    <td>
                                        <textarea runat="server" class="input"  style="text-align: left" id="SingleDesktopNext"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel12" BodyPadding="10" ShowBorder="true"
                            EnableCollapse="true" CssClass="blockpanel myblockform" ShowHeader="true" Title="本月HSE活动综述及下月HSE工作计划" runat="server">
                            <f:TextArea Label="本月HSE活动综述" ID="ThisSummary"  runat="server" LabelWidth="140px"></f:TextArea>
                            <f:TextArea Label="下月HSE工作计划" ID="NextPlan" runat="server" LabelWidth="140px"></f:TextArea>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" OnClick="btnSave_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:Button ID="btnSysSubmit" OnClick="btnSysSubmit_Click" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>


                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="wdSeDinMonthReport4Item" Title="本月人员投入情况" Hidden="true" EnableIFrame="false"
            EnableMaximize="true" EnableResize="true" runat="server"
            IsModal="true" Width="600px">
            <Items>
                <f:Form BodyPadding="10px" ID="Form4" LabelWidth="100px" ShowBorder="false" ShowHeader="false"
                    runat="server">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:DropDownList ID="drpUnit" runat="server" Label="单位名称" EmptyText="--请选择--"  ShowRedStar="true" AutoSelectFirstItem="false">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ColumnWidths="40% 60%">
                            <Items>
                                <f:NumberBox Label="安全管理" ID="SafeManangerNum" runat="server" MaxLength="5"
                                    NoDecimal="true" NoNegative="true"  ShowRedStar="true" />
                                <f:NumberBox Label="其他管理" ID="OtherManangerNum" runat="server" NoDecimal="true" NoNegative="true"
                                     ShowRedStar="true" MaxLength="5" />

                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="40% 60%">
                            <Items>
                                <f:NumberBox Label="特种作业" ID="SpecialWorkerNum" runat="server" MaxLength="5"
                                    NoDecimal="true" NoNegative="true"  ShowRedStar="true" />
                                <f:NumberBox Label="一般作业" ID="GeneralWorkerNum" runat="server"
                                    NoDecimal="true" NoNegative="true"  ShowRedStar="true" MaxLength="5" />
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Button ID="btnMonthReport4Item" runat="server" Width="150px" ValidateForms="Form4" OnClick="btnMonthReport4Item_Click" Text="提交">
                                </f:Button>
                            </Items>
                        </f:FormRow>

                    </Rows>
                </f:Form>

            </Items>
        </f:Window>
    </form>
</body>

</html>
<script type="text/javascript">

    function onGridAfterEdit(event, value, params) {
        var me = this, columnId = params.columnId, rowId = params.rowId;
        var SafeManangerNum = me.getCellValue(rowId, 'SafeManangerNum');
        var OtherManangerNum = me.getCellValue(rowId, 'OtherManangerNum');
        var SpecialWorkerNum = me.getCellValue(rowId, 'SpecialWorkerNum');
        var GeneralWorkerNum = me.getCellValue(rowId, 'GeneralWorkerNum');
        me.updateCellValue(rowId, 'TotalNum', (SafeManangerNum + OtherManangerNum + SpecialWorkerNum + GeneralWorkerNum));
    }





</script>
