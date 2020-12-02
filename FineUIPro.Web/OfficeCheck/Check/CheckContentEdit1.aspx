<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckContentEdit1.aspx.cs" Inherits="FineUIPro.Web.OfficeCheck.Check.CheckContentEdit1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>现场安全检查</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <script src="../res/js/jquery-3.3.1.min.js" type="text/javascript"></script>
     <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" MarginBottom="5px"
            BodyPadding="1px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left" EnableTableStyle="true">
        <Toolbars>     
            <f:Toolbar ID="Toolbar5" runat="server" ToolbarAlign="Right">
                <Items>      
                    <f:Label runat="server" ID="lbTitle" Text="现场安全检查"></f:Label>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>   
                     <%--<f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Text="导出" Icon="FolderUp"
                            EnableAjax="false" DisableControlBeforePostBack="false">
                        </f:Button>--%>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存" OnClick="btnSave_Click" ValidateForms="SimpleForm1" Hidden="true">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpSubjectProject" runat="server" Height="22px" Label="项目名称" 
                        EnableEdit="true"  Required="true" ShowRedStar="true" LabelWidth="90px">
                    </f:DropDownList>
                    <f:Label ID="lbTemp" runat="server"></f:Label>
                </Items>
            </f:FormRow>   
            <f:FormRow>
                <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="检查标准" EnableCollapse="false" runat="server"
                    BoxFlex="1" DataKeyNames="ID" AllowCellEditing="true" EnableColumnLines="true"
                    ClicksToEdit="1" DataIDField="ID" AllowSorting="true" SortField="SortIndex" SortDirection="ASC"
                    OnSort="Grid1_Sort" EnableTextSelection="True" EnableSummary="true" SummaryPosition="Bottom">                       
                    <Columns>          
                        <f:RenderField HeaderText="主键" ColumnID="ID" DataField="ID" Hidden="true">
                        </f:RenderField>                  
                        <f:RenderField HeaderText="序号" ColumnID="SortIndex" DataField="SortIndex"
                            HeaderTextAlign="Center" TextAlign="Center" Width="60px">
                        </f:RenderField>
                        <f:RenderField HeaderText="检查项目" ColumnID="CheckItem" DataField="CheckItem"
                            HeaderTextAlign="Center"  TextAlign="Left" Width="120px">                                
                        </f:RenderField>
                        <f:RenderField HeaderText="检查标准" ColumnID="CheckStandard" DataField="CheckStandard"
                            HeaderTextAlign="Center"  TextAlign="Left" Width="250px" ExpandUnusedSpace="true">                               
                        </f:RenderField>
                        <f:RenderField HeaderText="检查方法" ColumnID="CheckMethod" DataField="CheckMethod"
                            HeaderTextAlign="Center"  TextAlign="Left" Width="130px" >                               
                        </f:RenderField>
                        <f:RenderField HeaderText="检查结果" ColumnID="CheckResult" DataField="CheckResult"
                            HeaderTextAlign="Center"  TextAlign="Left" Width="160px">
                            <Editor>
                                <f:TextArea ID="txtCheckResult" runat="server">
                                </f:TextArea>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField HeaderText="基准分" ColumnID="BaseScore" DataField="BaseScore"
                            HeaderTextAlign="Center"  TextAlign="Left" Width="70px" FieldType="Double"> 
                            <Editor>
                                <f:NumberBox ID="txtBaseScore"  runat="server" NoDecimal="false" NoNegative="true" DecimalPrecision="2">
                                </f:NumberBox>
                            </Editor>                               
                        </f:RenderField> 
                        <f:RenderField HeaderText="扣减分" ColumnID="DeletScore" DataField="DeletScore"
                            HeaderTextAlign="Center"  TextAlign="Left" Width="70px" FieldType="Double">
                            <Editor>
                                <f:NumberBox ID="txtDeletScore"  runat="server" NoDecimal="false" NoNegative="true" DecimalPrecision="2">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField> 
                        <f:RenderField HeaderText="实得分" ColumnID="GetScore" DataField="GetScore"
                             HeaderTextAlign="Center"  TextAlign="Left" Width="70px" FieldType="Double">                                
                        </f:RenderField>                             
                    </Columns>
                    <Listeners>
                   <%-- <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />--%>
                    <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
                </Listeners>
               </f:Grid>
               </Items>
            </f:FormRow>
            <f:FormRow>
            <Items>                                    
                <f:Label ID="lbTotal100Score" runat="server"></f:Label>
                <f:HiddenField ID="hdTotalDeletScore6_7" runat="server"></f:HiddenField>                                    
            </Items>
        </f:FormRow>
        <f:FormRow ColumnWidths="66% 34%">
            <Items>                                    
                <f:Label ID="lbTotalLastScore" runat="server"></f:Label>
                <f:Label ID="lbEvaluationResult" runat="server" Label="评定结论" LabelWidth="90px" ToolTip="合格（80分以上）；基本合格（71-79分）；不合格（70分以下。"></f:Label>
            </Items>
        </f:FormRow>
        <f:FormRow>
            <Items>
                <f:TextBox ID="txtCheckMan" runat="server" Label="检查人员" LabelWidth="90px">
                </f:TextBox>
                <f:Label ID="Label1" runat="server"></f:Label>
                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="日期" ID="txtCheckDate" LabelWidth="90px">
                </f:DatePicker>
            </Items>
        </f:FormRow> 
        <f:FormRow>
            <Items>
                <f:TextBox ID="txtCheckLeader" runat="server" Label="检查组长" LabelWidth="90px">
                </f:TextBox>
                <f:TextBox ID="txtSubjectUnitMan" runat="server" Label="受检单位负责人" LabelWidth="130px">
                </f:TextBox>
                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="日期" ID="txtSubjectUnitDate" LabelWidth="90px">
                </f:DatePicker>
            </Items>
        </f:FormRow>  
        </Rows>
    </f:Form>
   </form>
    <script type="text/javascript">
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        //合并列
        function onGridDataLoad(event) {
            this.mergeColumns(['SortIndex', 'CheckItem', 'CheckMethod', 'CheckResult','BaseScore','DeletScore','GetScore'], {
                dependsFirst: true
            });
        }

        function onGridAfterEdit(event, value, params) {           
            var me = this, columnId = params.columnId, rowId = params.rowId;
            if (columnId === 'DeletScore' || columnId === 'BaseScore') {
                var baseS = me.getCellValue(rowId, 'BaseScore');
                var deleteS = me.getCellValue(rowId, 'DeletScore');
                me.updateCellValue(rowId, 'GetScore', (baseS - deleteS).toFixed(2));
            }
            //updateSummary();
        }

        //function updateSummary() {
        //     var me = F(grid1ClientID), baseScoreTotal = 0, deletScoreTotal = 0, getScoreTotal = 0;
        //   me.getRowEls().each(function (index, tr) {
        //       baseScoreTotal += me.getCellValue(tr, 'BaseScore');
        //       deletScoreTotal += me.getCellValue(tr, 'DeletScore');
        //       getScoreTotal += me.getCellValue(tr, 'GetScore');
        //    });

        //   // 第三个参数 true，强制更新，不显示左上角的更改标识
        //   me.updateSummaryCellValue('CheckItem', '合计分', true);
        //   me.updateSummaryCellValue('BaseScore', baseScoreTotal, true);
        //   me.updateSummaryCellValue('DeletScore', deletScoreTotal, true);
        //   me.updateSummaryCellValue('GetScore', getScoreTotal, true);
           
        //   var pValue=((getScoreTotal / baseScoreTotal) * 100).toFixed(2);
        //   var lab100 = "本表百分制得分 = （实查项实得分之和/实查项应得满分之和*100） " + pValue + "   分";
        //   F(lbTotal100ScoreClientID).setValue(lab100);

        //   var lastScore = pValue - F(hdTotalDeletScore6_7ClientID).value;
        //   var lablast = "综合评定得分 = 本表得分 - 负面清单罚分 =   " + lastScore + "     分";
        //   F(lbTotalLastScoreClientID).setValue(lablast);

        //   var str= "";
        //   if (lastScore >= 80) {
        //       str = "合格";
        //   }
        //   else if (lastScore >= 71 && lastScore <= 79) {
        //       str = "基本合格";
        //   }
        //   else if (lastScore <= 70) {
        //       str = "不合格";
        //   }
        //   F(lbEvaluationResultClientID).setValue(str);
        //}
    </script>
</body>
</html>