<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleItem.aspx.cs" Inherits="FineUIPro.Web.SysManage.RoleItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="历史角色" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RoleId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="RoleItemId" AllowSorting="true" SortField="RoleItemId"
                SortDirection="ASC"  AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" ForceFit="true" EnableRowDoubleClickEvent="true"  EnableTextSelection="True">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:TemplateField ColumnID="RoleId" Width="180px" HeaderText="角色名称" HeaderTextAlign="Center" TextAlign="Center"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertRoleName(Eval("RoleId")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                    <f:RenderField Width="140px" ColumnID="用户名" DataField="UserName" EnableFilter="true"
                        SortField="UserName" FieldType="String" HeaderText="用户名" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="入场时间" DataField="IntoDate" EnableFilter="true"
                        SortField="IntoDate" FieldType="String" HeaderText="入场时间" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="出场时间" DataField="OutDate" EnableFilter="true"
                        SortField="OutDate" FieldType="String" HeaderText="出场时间" HeaderTextAlign="Center"
                        TextAlign="Left">                      
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
    </form>
</html>
