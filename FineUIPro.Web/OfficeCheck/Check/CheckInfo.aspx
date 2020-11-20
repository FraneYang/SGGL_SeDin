<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckInfo.aspx.cs" Inherits="FineUIPro.Web.OfficeCheck.Check.CheckInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发起检查</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                    EnableCollapse="true" Width="260px" Title="监督检查" TitleToolTip="监督检查" ShowBorder="true"
                    ShowHeader="false" BodyPadding="5px" IconFont="ArrowCircleLeft" Layout="VBox" AutoScroll="true">
                    <Items>
                        <f:DatePicker ID="txtCheckStartTimeS" runat="server" Label="开始时间" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                            DateFormatString="yyyy-MM-dd" LabelWidth="80px">
                        </f:DatePicker>
                        <f:DatePicker ID="txtCheckEndTimeS" runat="server" Label="结束时间" AutoPostBack="true" OnTextChanged="Tree_TextChanged"
                            DateFormatString="yyyy-MM-dd" LabelWidth="80px">
                        </f:DatePicker>
                    </Items>
                    <Items>
                        <f:Tree ID="tvControlItem" EnableCollapse="true" ShowHeader="true" Title="监督检查节点树"
                            OnNodeCommand="tvControlItem_NodeCommand" AutoLeafIdentification="true"
                            runat="server" EnableTextSelection="true" AutoScroll="true">
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="监督检查"
                    TitleToolTip="监督检查" AutoScroll="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                            <Items>
                                <%--<f:Button ID="btnFind" Text="安全监督检查管理办法" Icon="Find" runat="server" OnClick="btnFind_Click">
                            </f:Button>--%>
                                <f:HiddenField runat="server" ID="hdCheckNoticeId"></f:HiddenField>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnEdit" ToolTip="修改检查信息" Icon="TableEdit" runat="server" OnClick="btnEdit_Click" Hidden="true">
                                </f:Button>
                                <f:Button ID="btnDelete" ToolTip="删除检查" ConfirmText="确认删除此检查?" ConfirmTarget="Top" Hidden="true"
                                    Icon="Delete" runat="server" OnClick="btnDelete_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="drpSubjectUnit" Label="受检单位" runat="server" LabelWidth="80px">
                                        </f:Label>
                                        <f:Label ID="txtSubjectObject" ShowLabel="false" runat="server" MarginLeft="140px"></f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="txtSubjectUnitMan" Label="受检单位负责人" runat="server" LabelWidth="120px">
                                        </f:Label>
                                        <f:Label ID="txtSubjectUnitTel" Label="受检单位负责人电话" runat="server" LabelWidth="150px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:Label ID="txtSubjectUnitAdd" Label="受检单位地址" runat="server" LabelWidth="110px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="txtCheckStartTime" Label="检查开始日期" runat="server" LabelWidth="110px">
                                        </f:Label>
                                        <f:Label ID="txtCheckEndTime" Label="检查结束日期" runat="server" LabelWidth="110px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="40% 60%">
                                    <Items>
                                        <f:Label ID="txtCompileMan" Label="编制人" runat="server" LabelWidth="100px">
                                        </f:Label>
                                        <f:Label ID="txtCompileDate" Label="编制日期" runat="server" LabelWidth="100px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow ColumnWidths="30% 70%">
                                    <Items>
                                        <f:Label ID="Label1" runat="server" Text="现场安全检查" Label="检查项名称">
                                        </f:Label>
                                        <f:Label ID="lblSubjectUnitId" runat="server" Label="受检对象"></f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="30% 70%">
                                    <Items>
                                        <f:Label ID="lblCheckDate" runat="server" Label="生效日">
                                        </f:Label>
                                        <f:Label ID="lblResult" runat="server" Label="检查结果">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnCheck1" ToolTip="编辑现场安全检查" Icon="TableEdit" runat="server" OnClick="btnCheck1_Click" Hidden="true">
                                        </f:Button>
                                        <f:Button ID="btnView1" ToolTip="查看现场安全检查" Icon="Find" runat="server" OnClick="btnView1_Click" Hidden="true">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Form>
                    </Items>
                    <Items>
                        <f:Form ID="Form4" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow ColumnWidths="30% 70%">
                                    <Items>
                                        <f:Label ID="Label9" runat="server" Text="检查报告" Label="检查项名称">
                                        </f:Label>
                                        <f:Label ID="lblCheckObject" runat="server" Label="受检对象"></f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="30% 70%">
                                    <Items>
                                        <f:Label ID="lblCheckStartTime" runat="server" Label="生效日">
                                        </f:Label>
                                        <f:Label ID="lblCheckReportResult" runat="server" Label="检查结果">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill4" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnCheck3" ToolTip="编辑检查报告" Icon="TableEdit" runat="server" OnClick="btnCheck3_Click" Hidden="true">
                                        </f:Button>
                                        <f:Button ID="btnView3" ToolTip="查看检查报告" Icon="Find" runat="server" OnClick="btnView3_Click" Hidden="true">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Form>
                    </Items>

                    <Items>
                        <f:Form ID="Form3" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                            <Rows>
                                <f:FormRow ColumnWidths="30% 70%">
                                    <Items>
                                        <f:Label ID="Label5" runat="server" Text="隐患整改" Label="检查项名称">
                                        </f:Label>
                                        <f:Label ID="lblUnitId" runat="server" Label="受检对象"></f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ColumnWidths="30% 70%">
                                    <Items>
                                        <f:Label ID="lblCheckedDate" runat="server" Label="生效日">
                                        </f:Label>
                                        <f:Label ID="lblCheckResult" runat="server" Label="检查结果">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                                        <f:Button ID="btnCheck2" ToolTip="编辑隐患整改" Icon="TableEdit" runat="server" OnClick="btnCheck2_Click" Hidden="true">
                                        </f:Button>
                                        <f:Button ID="btnView2" ToolTip="查看隐患整改" Icon="Find" runat="server" OnClick="btnView2_Click" Hidden="true">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Form>
                    </Items>
                    
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="检查维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="800px" Height="400px">
        </f:Window>
        <f:Window ID="Window2" Title="检查内容项" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
            Width="1200px" Height="650px">
        </f:Window>
    </form>
    <script type="text/javascript">
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
