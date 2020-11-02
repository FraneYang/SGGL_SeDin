<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shunt.aspx.cs" Inherits="FineUIPro.Web.Person.Shunt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>分流管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
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
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="分流管理" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="UserId"  EnableColumnLines="true" AllowCellEditing="true" ClicksToEdit="1"
                     DataIDField="UserId" AllowSorting="true" SortField="UserCode" ForceFit="true"
                    SortDirection="ASC" OnSort="Grid1_Sort" 
                    PageSize="1000" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                    OnRowDoubleClick="Grid1_RowDoubleClick"  EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                            <Items>
                                <f:DropDownList ID="drpProject" runat="server" Label="拟聘项目" Width="300px" LabelWidth="100px"
                            EnableEdit="true" ShowRedStar="true" Required="true">
                        </f:DropDownList>
                                <f:DropDownList ID="drpWorkPost" runat="server" Label="岗位" Width="250px" LabelWidth="60px" LabelAlign="Right"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="drpPostTitle" runat="server" Label="职称" Width="250px" LabelWidth="60px" LabelAlign="Right"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="drpCertificate" runat="server" Label="职业资格证书" Width="250px" LabelWidth="105px" LabelAlign="Right"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:ToolbarFill runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnView" ToolTip="查看历史记录" Icon="Find" runat="server" OnClick="btnView_Click">
                                </f:Button>
                                <f:Button ID="btnSure" ToolTip="确定" Icon="Accept" Hidden="true" runat="server" OnClick="btnSure_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:TemplateField ColumnID="Check" Width="50px" HeaderText="选择" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="cbSelect" />
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center"
                            TextAlign="Center" />
                        <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                            FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                           <f:RenderField Width="120px" ColumnID="RoleName" DataField="RoleName" SortField="RoleName"
                            FieldType="String" HeaderText="本部角色" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:CheckBoxField Width="60px" SortField="IsPost" RenderAsStaticField="true" DataField="IsPost"
                            HeaderText="在岗" HeaderTextAlign="Center" TextAlign="Center">
                        </f:CheckBoxField>
                            <f:RenderField Width="100px" ColumnID="PostTitleName" DataField="PostTitleName" 
                            FieldType="String" HeaderText="职称" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:TemplateField ColumnID="Certificate" Width="130px" HeaderText="职业资格证书" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertCertificateName(Eval("UserId")) %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                        <f:RenderField Width="150px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                            FieldType="String" HeaderText="当前所在项目" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="130px" ColumnID="WorkPostName" DataField="WorkPostName" 
                            FieldType="String" HeaderText="当前项目岗位" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="WorkPost" DataField="WorkPost" FieldType="String"
                            HeaderText="拟聘岗位" HeaderTextAlign="Center">
                            <Editor>
                                <f:DropDownList ID="drpWP" Required="true" runat="server">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                    </Columns>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">                    
                        </f:DropDownList>
                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="查看历史记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="1000px" OnClose="Window1_Close"
            Height="560px">
        </f:Window>
    </form>
    <script type="text/jscript">
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
