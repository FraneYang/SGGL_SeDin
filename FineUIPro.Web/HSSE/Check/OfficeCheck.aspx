<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfficeCheck.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.OfficeCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>总部检查</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Controls/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" Title="安全隐患及整改要求" AutoScroll="true"
            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" AllowCellEditing="true" ClicksToEdit="1"
                            ForceFit="true" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1"
                            DataKeyNames="RectifyItemId,IsRectify" DataIDField="RectifyItemId" AllowSorting="true" SortField="RectifyItemId"
                            SortDirection="ASC" EnableTextSelection="True" MinHeight="240px" PageSize="500"
                            EnableRowDoubleClickEvent="true" OnRowCommand="Grid1_RowCommand">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Left" runat="server">
                                    <Items>
                                        <f:RadioButtonList ID="rbStates" runat="server" Label="状态" LabelAlign="Right" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                            <f:RadioItem Text="待整改" Value="1" Selected="true" />
                                            <f:RadioItem Text="已完成" Value="3" />
                                        </f:RadioButtonList>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RenderField HeaderText="主键" ColumnID="RectifyItemId" DataField="RectifyItemId"
                                    SortField="RectifyItemId" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                                    Hidden="true">
                                </f:RenderField>
                                <f:RenderField Width="300px" ColumnID="WrongContent" DataField="WrongContent" FieldType="string"
                                    HeaderText="具体位置及隐患内容">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="Requirement" DataField="Requirement" FieldType="string"
                                    HeaderText="整改要求">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="LimitTime" DataField="LimitTime" FieldType="Date" Renderer="Date"
                                    HeaderText="整改期限">
                                </f:RenderField>
                                <f:LinkButtonField HeaderText="整改前" ConfirmTarget="Top" Width="80" CommandName="AttachUrl"
                                    TextAlign="Center" ToolTip="整改照片" Text="详细" />
                                <f:RenderField Width="100px" ColumnID="RectifyResults" DataField="RectifyResults" FieldType="string"
                                    HeaderText="整改结果">
                                </f:RenderField>
                                <f:LinkButtonField ColumnID="ReAttachUrl" HeaderText="整改后" ConfirmTarget="Top" Width="80" CommandName="ReAttachUrl"
                                    TextAlign="Center" ToolTip="整改照片" Text="整改后" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>
