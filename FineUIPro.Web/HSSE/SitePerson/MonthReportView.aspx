<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportView.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.MonthReportView" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看人工时月报</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtCompileDate" runat="server" Label="日期" Readonly="true" >
                    </f:TextBox>
                    <f:Label runat="server" ID="lb1"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" DataIDField="MonthReportDetailId"
                        DataKeyNames="MonthReportDetailId" EnableMultiSelect="false" ShowGridHeader="true" Height="300px"
                        EnableColumnLines="true" > 
                        <Columns>                           
                             <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="单位名称" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="RealPersonNum" DataField="RealPersonNum" SortField="RealPersonNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="当月人数">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="PersonWorkTime" DataField="PersonWorkTime" SortField="PersonWorkTime"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="当月人工时">
                            </f:RenderField>
                        </Columns>                  
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
    <script>
        
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
          //  F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
