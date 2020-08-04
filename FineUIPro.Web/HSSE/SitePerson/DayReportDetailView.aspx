<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayReportDetailView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.SitePerson.DayReportDetailView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑单位人工时日报</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>           
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" AllowCellEditing="true" ForceFit="true"
                        ClicksToEdit="1" DataIDField="DayReportUnitDetailId" DataKeyNames="DayReportUnitDetailId,PostType,IsHsse"
                        EnableMultiSelect="false" ShowGridHeader="true" Height="400px" EnableColumnLines="true">                       
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="200px" ColumnID="PostName" DataField="PostName" SortField="PostName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="岗位">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="RealPersonNum" DataField="RealPersonNum" SortField="RealPersonNum"
                                FieldType="Int" HeaderTextAlign="Center" TextAlign="Left" HeaderText="人员">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="PersonWorkTime" DataField="PersonWorkTime"
                                SortField="PersonWorkTime" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="工时">
                            </f:RenderField>
                        </Columns>                       
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
