<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertArgumentListView.aspx.cs"
    ValidateRequest="false" Inherits="FineUIPro.Web.HSSE.Solution.ExpertArgumentListView" %>
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
                    <f:TextBox ID="txtHazardCode" runat="server" Label="文件编号" Readonly="true">
                    </f:TextBox>
                     <f:TextBox ID="txtVersionNo" runat="server" Label="版本"  Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtRecordTime" runat="server" Label="版本"  Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" 
                        DataIDField="LargerHazardListItemId" DataKeyNames="LargerHazardListItemId"   
                        SortDirection="ASC" AllowSorting="true" SortField="SortIndex" ForceFit="true"
                        Height="400px" EnableColumnLines="true" >
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
                    <f:Button ID="btnCancel" Icon="PageCancel" runat="server" ToolTip="作废"  Hidden="true"
                            OnClick="btnCancel_Click" Text="作废" ConfirmText="确定作废当前记录？">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" 
                        runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
    <script type="text/jscript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
