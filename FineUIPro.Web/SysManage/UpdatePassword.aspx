<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="FineUIPro.Web.SysManage.UpdatePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="修改密码" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="UserId" EnableColumnLines="true" DataIDField="UserId" 
                AllowSorting="true" SortField="UnitCode,UserCode" SortDirection="ASC" OnSort="Grid1_Sort" ForceFit="true"
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>      
                            <f:TextBox runat="server" Label="用户名称" ID="txtUserName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>                     
                            <f:Button ID="btnEdit" ToolTip="修改选中项密码" Icon="Pencil" runat="server" OnClick="btnEdit_Click">
                            </f:Button>  
                             <f:Button ID="btnArrowRefresh" ToolTip="重置选中项密码" Icon="ArrowRefresh" Hidden="true"
                                 ConfirmText="确定恢复当前用户原始密码？" OnClick="btnArrowRefresh_Click"
                                runat="server">
                            </f:Button>                         
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                     <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" 
                        SortField="UnitName" FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">                        
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="UserCode" DataField="UserCode"
                        SortField="UserCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" 
                          SortField="UserName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">                      
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="Account" DataField="Account"
                        SortField="Account" FieldType="String" HeaderText="账号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="200px" ColumnID="RoleName" DataField="RoleName"
                        SortField="RoleName" FieldType="String" HeaderText="角色" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="RoleTypeName" DataField="RoleTypeName" 
                        SortField="RoleTypeName" FieldType="String" HeaderText="角色类型" HeaderTextAlign="Center" TextAlign="Left">                      
                    </f:RenderField>                   
                     <f:RenderField Width="80px" ColumnID="IsPostName" DataField="IsPostName"
                        SortField="IsPostName" FieldType="String" HeaderText="在岗" HeaderTextAlign="Center" TextAlign="Left">                        
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
    <f:Window ID="Window1" Title="修改密码" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="false" runat="server"  IsModal="true"
        Width="550px" Height="300px">
    </f:Window>
    </form>
</body>
</html>