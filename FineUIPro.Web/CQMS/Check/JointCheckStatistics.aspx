<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointCheckStatistics.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.JointCheckStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="质量问题统计" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="JointCheckDetailId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="JointCheckDetailId" AllowSorting="true" SortField="CheckDate"
                    SortDirection="DESC" EnableColumnLines="true" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpSponsorUnit" runat="server" Label="施工单位" Width="300px" EnableEdit="true" LabelAlign="right" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime" LabelAlign="right" LabelWidth="110px">
                                </f:DatePicker>
                                <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime" LabelAlign="right" LabelWidth="110px">
                                </f:DatePicker>
                                <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" Width="300px" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                

                            </Items>
                        </f:Toolbar>

                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:DropDownList ID="drpCNProfessional" runat="server" Width="300px" Label="专业" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                                <f:DropDownList ID="drpCheckType" runat="server" Label="检查类别" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                </f:DropDownList>
                               <f:DropDownList ID="drpState" runat="server" Label="审批状态" LabelAlign="Right" EnableEdit="true" LabelWidth="110px">
                                        <f:ListItem Text="已闭合" Value="1" />
                                        <f:ListItem Text="未闭合" Value="0" />
                                </f:DropDownList>
                                <f:DropDownList ID="drpQuestionType" runat="server" Label="问题类别" Width="300px" LabelAlign="Right" EnableEdit="true" LabelWidth="110px" >
                                   </f:DropDownList>
                                <f:ToolbarFill runat="server"></f:ToolbarFill>

                                <f:Button ID="btnSearch" Icon="SystemSearch" ToolTip="查询"
                                    EnablePostBack="true" runat="server" OnClick="btnSearch_Click">
                                </f:Button>
                                <f:Button ID="btnRset" ToolTip="重置" Icon="ArrowUndo" EnablePostBack="true" runat="server" OnClick="btnRset_Click">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                            EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName"
                            SortField="UnitName" FieldType="String" HeaderText="施工单位" TextAlign="center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate" FieldType="Date" HeaderText="检查日期" TextAlign="Center" HeaderTextAlign="Center" RendererArgument="yyyy-MM-dd">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="CheckTypeStr" DataField="CheckTypeStr"
                            SortField="CheckTypeStr" FieldType="String" HeaderText="检查类别" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="UnitWorkName" DataField="UnitWorkName" SortField="UnitWorkName" FieldType="String" HeaderText="单位工程" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="ProfessionalName" DataField="ProfessionalName"
                            FieldType="String" HeaderText="专业" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="QuestionTypeStr" DataField="QuestionTypeStr"
                            FieldType="String" HeaderText="问题类别" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="CheckSite" DataField="CheckSite"
                            FieldType="String" HeaderText="部位" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="QuestionDef" DataField="QuestionDef"
                            FieldType="String" HeaderText="问题描述" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:LinkButtonField HeaderText="问题图片" ConfirmTarget="Top" Width="80" CommandName="attchUrl" ColumnID="AttchUrl"
                            TextAlign="Center" ToolTip="问题图片" Text="问题图片" />

                        <f:RenderField Width="110px" ColumnID="HandleWay" DataField="HandleWay"
                            FieldType="String" HeaderText="整改方案" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:LinkButtonField HeaderText="整改照片" ConfirmTarget="Top" Width="80" CommandName="ReAttachUrl" ColumnID="ReAttachUrl"
                            TextAlign="Center" ToolTip="整改照片" Text="整改照片" />
                        <f:TemplateField Width="110px" HeaderText="审批状态" HeaderTextAlign="Center" ColumnID="lbState"
                            TextAlign="Center" SortField="DetectionType">
                            <ItemTemplate>
                                <asp:Label ID="lbState" runat="server" Text='<%# ConvertState(Eval("JointCheckDetailId")) %>'></asp:Label>
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
        <f:Window ID="Window1" Title="文件查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="650px" Height="500px">
        </f:Window>
    </form>
</body>
</html>
