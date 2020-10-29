<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonInfoEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.PersonInfoEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑现场考勤人员考勤信息</title>
   <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                    <f:RadioButtonList ID="rblCheck" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblCheck_SelectedIndexChanged">
                        <f:RadioItem Value="手动" Text="手动" Selected="true" />
                        <f:RadioItem Value="自动" Text="自动" />
                    </f:RadioButtonList>
                    <f:Label ID="Label2" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownBox runat="server" ID="drpPersonId" Label="人员姓名" EmptyText="请从下拉表格中选择"
                        MatchFieldWidth="false" LabelAlign="Right" AutoPostBack="true" OnTextChanged="drpPersonId_TextChanged"
                        EnableMultiSelect="false" Required="true" ShowRedStar="true" >
                        <PopPanel>
                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="PersonId"
                                DataTextField="PersonName" DataKeyNames="PersonId" AllowSorting="true" SortField="CardNo"
                                SortDirection="ASC" EnableColumnLines="true" Hidden="true" Width="900px"  PageSize="10000"
                                Height="300px" EnableMultiSelect="false">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="卡号" ID="txtCardNo" EmptyText="输入查询条件" AutoPostBack="true"
                                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelAlign="Right">
                                            </f:TextBox>
                                            <f:TextBox runat="server" Label="姓名" ID="txtPersonName" EmptyText="输入查询条件" AutoPostBack="true"
                                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelAlign="Right">
                                            </f:TextBox>
                                            <f:TextBox runat="server" Label="身份证号" ID="txtIdentityCard" EmptyText="输入查询条件" AutoPostBack="true"
                                                OnTextChanged="TextBox_TextChanged" Width="300px" LabelAlign="Right">
                                            </f:TextBox>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                        TextAlign="Center" />
                                    <f:RenderField Width="100px" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                                        FieldType="String" HeaderText="卡号" HeaderTextAlign="Center" TextAlign="Left">
                                    </f:RenderField>
                                    <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" EnableFilter="true"
                                        SortField="PersonName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                        TextAlign="Left">
                                    </f:RenderField>
                                    <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                        FieldType="String" HeaderText="所在单位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                                        FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Left">
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="40% 25% 35%">
                <Items>
                    <f:TextBox ID="txtWorkArea" runat="server" Label="单位工程" MaxLength="100" ShowRedStar="true"
                        Required="true" LabelAlign="Right">
                    </f:TextBox>
                    <f:DropDownList ID="drpWorkArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpWorkArea_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:Label ID="Label4" runat="server" Text="说明：检查区域可从下拉框选择也可手动编辑。" CssClass="lab" MarginLeft="5px">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtAddress" runat="server" Label="进出地点" MaxLength="50" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <%--<f:Label ID="Label3" runat="server">
                    </f:Label>--%>
                    <f:DropDownList ID="drpType" runat="server" Label="进出时间">
                        <f:ListItem Value="1" Text="入场时间" />
                        <f:ListItem Value="2" Text="出场时间" />
                    </f:DropDownList>
                    <f:DatePicker ID="txtTime" runat="server" >
                    </f:DatePicker>
                    <f:TextBox ID="txtTime2" runat="server">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                         OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="连接读卡器" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="600px"
        Height="300px">
    </f:Window>
    </form>
</body>
</html>
