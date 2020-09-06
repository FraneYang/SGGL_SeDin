<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpotCheckStatistics.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.SpotCheckStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-row.red {
            background-color: #FF7575;
            background-image: none;
        }

        .Yellow {
            background-color: #FFFF93;
        }

        .fontred {
            color: #FF7575;
            background-image: none;
        }
        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="工序验收统计" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="SpotCheckDetailId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="SpotCheckDetailId" AllowSorting="true" SortField="CreateDate"
                    SortDirection="DESC" EnableColumnLines="true" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUnit" runat="server" Label="施工单位" Width="350px" EnableEdit="true" LabelAlign="right" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime" LabelAlign="right" LabelWidth="110px">
                                </f:DatePicker>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime" LabelAlign="right" LabelWidth="110px">
                                </f:DatePicker>
                                <f:DropDownList ID="drpControlPoint" runat="server" Label="控制点等级" EnableMultiSelect="true" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                    <f:ListItem Text="A" Value="A" />
                                    <f:ListItem Text="AR" Value="AR" />
                                    <f:ListItem Text="B" Value="B" />  
                                    <f:ListItem Text="BR" Value="BR" />
                                    <f:ListItem Text="C" Value="C" />
                                    <f:ListItem Text="CR" Value="CR" />
                                </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="TableGo" 
                            AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true" AjaxLoadingMaskText="正在导出数据到服务器，请稍候"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" Width="350px" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpCNProfessional" runat="server" Label="专业" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpIsOK" runat="server" Label="实体验收结果" EnableEdit="true" LabelAlign="right" LabelWidth="110px">
                                    <f:ListItem Text="合格" Value="1" />
                                    <f:ListItem Text="不合格" Value="0" />
                                </f:DropDownList>
                                <f:DropDownList ID="drpIsDataOK" runat="server" Label="资料验收结果" EnableEdit="true" LabelAlign="right" LabelWidth="110px">
                                    <f:ListItem Text="合格" Value="1" />
                                    <f:ListItem Text="不合格" Value="0" />
                                    <f:ListItem Text="不需要" Value="2" />
                                </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>
                                <f:Button ID="btnSearch" Icon="SystemSearch" ToolTip="查询"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnRset" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server" OnClick="btnRset_Click">
                                </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName"
                            SortField="UnitName" FieldType="String" HeaderText="施工单位" TextAlign="center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="ProfessionalName" DataField="ProfessionalName"
                            FieldType="String" HeaderText="专业" TextAlign="Center" 
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="ControlItemAndCycleId" Width="400px" HeaderText="共检项目名称" HeaderTextAlign="Center" TextAlign="Center"
                           >
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertDetailName(Eval("ControlItemAndCycleId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="ControlPoint" Width="80px" HeaderText="控制点级别" HeaderTextAlign="Center" TextAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# ConvertControlPoint(Eval("ControlItemAndCycleId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                        <f:TemplateField ColumnID="SpotCheckDate" Width="150px" HeaderText="共检时间" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# ConvertSpotCheckDate(Eval("SpotCheckCode")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField ColumnID="IsOKStr" Width="90px" HeaderText="共检结果" HeaderTextAlign="Center" TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertIsOK(Eval("IsOK")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField HeaderText="质量不合格描述" ColumnID="RectifyDescription" DataField="RectifyDescription"
                            SortField="RectifyDescription" FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="100px">
                        </f:RenderField>
                        <f:LinkButtonField Width="90px" HeaderText="交工资料" ConfirmTarget="Top" CommandName="attchUrl"
                            TextAlign="Center" ToolTip="交工资料" Text="交工资料" />
                        <f:TemplateField ColumnID="IsDataOKStr" Width="100px" HeaderText="资料情况" HeaderTextAlign="Center" TextAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# ConvertIsDataOK(Eval("IsDataOK")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                    </Columns>
                    <%--<Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>--%>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="15" Value="15" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>

                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>
