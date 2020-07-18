<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolePower.aspx.cs" Inherits="FineUIPro.Web.SysManage.Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>角色授权</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region3" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 1 0 0" Layout="Fit" runat="server">                              
                <Toolbars>
                     <f:Toolbar ID="Toolbar1" Position="Top"  runat="server">                       
                        <Items>
                            <f:DropDownBox runat="server" ID="drpRole" Label="角色" EmptyText="请从下拉表格中选择" MatchFieldWidth="false" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="drpRoleId_TextChanged"  LabelWidth="70px">
                                <PopPanel>
                                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="RoleId" DataTextField="RoleName"
                                        DataKeyNames="RoleId"  AllowSorting="true" SortField="RoleCode" SortDirection="ASC" EnableColumnLines="true"
                                        Hidden="true" Width="600px" Height="400px" PageSize="300">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                                <Items>
                                                    <f:TextBox runat="server" Label="名称" ID="txtRoleName" EmptyText="输入查询条件" 
                                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                                                     </f:TextBox>                                                          
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                                          <f:RenderField Width="100px" ColumnID="RoleCode" DataField="RoleCode" EnableFilter="true"
                                                SortField="RoleCode" FieldType="String" HeaderText="编码" HeaderTextAlign="Center"
                                                TextAlign="Left">                      
                                            </f:RenderField>
                                            <f:RenderField Width="250px" ColumnID="RoleName" DataField="RoleName" EnableFilter="true"
                                                SortField="RoleName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center"
                                                TextAlign="Left">                      
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="Def" DataField="Def" SortField="Def" FieldType="String"
                                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </PopPanel>
                            </f:DropDownBox>
                              <f:RadioButtonList ID="rbMenuType" Label="菜单类型" runat="server"  LabelWidth="80px"
                                  AutoPostBack="true" OnSelectedIndexChanged="rbMenuType_SelectedIndexChanged">
                                  <f:RadioItem  Selected="true"  Text="本部菜单" Value="MenuType_S"/>
                                  <f:RadioItem  Text="项目菜单" Value="MenuType_P"/>
                              </f:RadioButtonList>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true" ToolTip="保存"
                                OnClick="btnSave_Click">
                            </f:Button> 
                            <f:Button ID="btnCancel" Icon="Cancel" runat="server" ToolTip="取消" Hidden="true" ValidateForms="SimpleForm1"
                                    OnClick="btnCancel_Click">
                            </f:Button>  
                        <%--     <f:Button ID="btnHelp" runat="server" ToolTip="点击下载本页面使用说明" Icon="Help" Text="帮助">
                                <Listeners>
                                    <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                                </Listeners>
                            </f:Button>              --%>         
                        </Items>
                    </f:Toolbar>                    
                    <f:Toolbar ID="Toolbar3" Position="Top"  runat="server" >
                        <Items>
                             <f:CheckBoxList ID="ckMenuType" runat="server" AutoColumnWidth="true"
                                 LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="ckMenuType_OnSelectedIndexChanged">                             
                                <Listeners>
                                    <f:Listener Event="change" Handler="onCheckBoxListChange" />
                                </Listeners>
                            </f:CheckBoxList>
                        </Items>
                    </f:Toolbar>
                </Toolbars>                         
            </f:Region>           
            <f:Region ID="Region2" ShowBorder="true" ShowHeader="true" RegionPosition="Center"
                 BodyPadding="0 5 0 0" Layout="Fit" runat="server" Title="菜单按钮树">
                <Items>
                    <f:Tree ID="tvMenu" EnableCollapse="true" ShowHeader="false" Title="系统菜单"  EnableCheckBox="true"
                            AutoLeafIdentification="true" runat="server" EnableIcons ="true" AutoScroll="true"
                             EnableSingleClickExpand ="true" OnNodeCheck="tvMenu_NodeCheck" >
                     </f:Tree>
                </Items>
            </f:Region>           
        </Regions>
    </f:RegionPanel>
    </form>
    <script type="text/javascript">
        // 同时只能选中一项
        function onCheckBoxListChange(event, checkbox, isChecked) {
            var me = this;
            // 当前操作是：选中
            if (isChecked) {
                // 仅选中这一项
                me.setValue(checkbox.getInputValue());
            }
           // __doPostBack('', 'CheckBoxList1Change');
        }
        //// 点击标题栏工具图标 - 查看帮助
        //function onToolSourceCodeClick(event) {
        //    window.open('../Doc/用户权限怎么设置.doc', '_blank');
        //}
    </script>
</body>
</html>
