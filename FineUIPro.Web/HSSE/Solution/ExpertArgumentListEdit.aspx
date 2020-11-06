<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertArgumentListEdit.aspx.cs"
    ValidateRequest="false" Inherits="FineUIPro.Web.HSSE.Solution.ExpertArgumentListEdit" %>
<!DOCTYPE html>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑专家论证清单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHazardCode" runat="server" Label="文件编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                     <f:TextBox ID="txtVersionNo" runat="server" Label="版本"  MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="编制日期" ID="txtRecordTime">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" 
                        DataIDField="LargerHazardListItemId" DataKeyNames="LargerHazardListItemId"   
                        SortDirection="ASC" AllowSorting="true" SortField="SortIndex" ForceFit="true"
                        Height="400px" EnableColumnLines="true" >
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                     <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server">
                                      </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                            
                            <f:RenderField Width="50px" ColumnID="SortIndex" DataField="SortIndex" 
                                FieldType="String" HeaderText="序号" TextAlign="Center" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="145px" ColumnID="UnitWorkName" DataField="UnitWorkName"
                                FieldType="String" HeaderText="单位工程名称" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="145px" ColumnID="PackageContent" DataField="PackageContent" 
                                FieldType="String" HeaderText="分部分项工程名称" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>  
                              <f:RenderField Width="150px" ColumnID="WorkPackageSize" DataField="WorkPackageSize" 
                                FieldType="String" HeaderText="分部分项工程规模" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>  
                            <f:RenderField Width="260px" ColumnID="ExpectedTime" DataField="ExpectedTime"
                                FieldType="String" HeaderText="预计施工起止时间" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>  
                             <f:RenderField Width="80px" ColumnID="IsArgumentName" DataField="IsArgumentName" 
                                FieldType="String" HeaderText="是否需要</br>专家论证" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>  
                            <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" 
                                FieldType="String" HeaderText="施工单位" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField> 
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>                  
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" 
                        ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" 
                        ValidateForms="SimpleForm1"  OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" 
                        runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="900px" OnClose="Window1_Close"
        Height="400px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">  
        <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" 
            Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server"  Icon="Delete" 
            Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
