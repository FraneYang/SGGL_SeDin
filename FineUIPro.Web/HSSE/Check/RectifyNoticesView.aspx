<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNoticesView.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.RectifyNoticesView1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel4" />
        <f:Panel ID="Panel4" runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false" AutoScroll="true">
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" MaxLength="70" LabelWidth="160px" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="txtRectifyNoticesCode" runat="server" Label="编号" MaxLength="70" LabelWidth="160px" Readonly="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtUnitId" runat="server" Label="受检单位名称" LabelWidth="160px" LabelAlign="Right" Readonly="true">
                                </f:TextBox>
                                <f:TextBox ID="txtWorkAreaId" runat="server" Label="单位工程名称" LabelWidth="160px" LabelAlign="Right"  Readonly="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtCheckPersonId" runat="server" Label="检查人员" LabelWidth="160px" LabelAlign="Right"  Readonly="true">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="检察人员" ID="txtCheckPerson" Readonly="true" LabelWidth="160px"></f:TextBox>
                               
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                 <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckedDate"
                                    LabelAlign="right" LabelWidth="160px" ShowRedStar="true" Readonly="true">
                                </f:DatePicker>
                                <f:DropDownList ID="drpHiddenHazardType" runat="server" Label="隐患类别" LabelWidth="160px" LabelAlign="Right" EnableEdit="true" EmptyText="--请选择--" Readonly="true">
                                    <f:ListItem Text="一般" Value="1" />
                                    <f:ListItem Text="较大" Value="2" />
                                    <f:ListItem Text="重大" Value="3" />
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" Title="安全隐患及整改要求" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="RectifyNoticesItemId" DataIDField="RectifyNoticesItemId" AllowSorting="true" SortField="RectifyNoticesItemId" SortDirection="ASC" EnableTextSelection="True" Height="260px"
                                    EnableRowDoubleClickEvent="true">
                                    <Columns>
                                        <f:RenderField Width="200px" ColumnID="WrongContent" DataField="WrongContent"
                                            FieldType="String" HeaderText="具体位置及隐患内容" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="Requirement" DataField="Requirement" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="整改要求" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="LimitTime" DataField="LimitTime"
                                            FieldType="string" HeaderText="整改期限" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:TemplateField ColumnID="tfImageUrl1" Width="120px" HeaderText="整改前" HeaderTextAlign="Center"
                                            TextAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("RectifyNoticesItemId")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:RenderField Width="150px" ColumnID="RectifyResults" DataField="RectifyResults" FieldType="string" HeaderText="整改结果" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:TemplateField ColumnID="tfImageUrl2" Width="120px" HeaderText="整改后" HeaderTextAlign="Center"
                                            TextAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertImgUrlByImage(Eval("RectifyNoticesItemId")) %>'> %>'></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:RenderField Width="150px" ColumnID="IsRectify" DataField="IsRectify" FieldType="string" HeaderText="是否合格" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>

                <f:Form ID="Form4" ShowBorder="true" ShowHeader="true" Title="审核意见" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true"> 
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Grid ID="gvFlowOperate" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server" DataIDField="FlowOperateId" AllowSorting="true" SortField="OperateTime" SortDirection="ASC" EnableTextSelection="True" Height="260px" ForceFit="true" EnableTableStyle="true">
                                    <Columns>
                                        <f:RenderField Width="200px" ColumnID="OperateName" DataField="OperateName" FieldType="String" HeaderText="操作步骤"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="UserName" DataField="UserName" FieldType="String" HeaderText="操作人" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="200px" ColumnID="Opinion" DataField="Opinion" FieldType="string" HeaderText="操作意见" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:RenderField Width="150px" ColumnID="OperateTime" DataField="OperateTime" FieldType="string" HeaderText="操作时间" HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>
