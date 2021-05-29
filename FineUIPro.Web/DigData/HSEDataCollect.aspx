<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSEDataCollect.aspx.cs"
    Inherits="FineUIPro.Web.DigData.HSEDataCollect" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目HSE数据汇总表</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="项目HSE数据汇总表" EnableCollapse="true" 
                    EnableCollapseEvent="true" EnableExpandEvent="true" OnCollapse="Grid1_Collapse" OnExpand="Grid1_Collapse" 
                    runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="HSEDataCollectItemId"  
                    DataParentIDField="ParentId"  EnableTree="true" TreeColumn="HSEContent"                  
                    DataIDField="HSEDataCollectItemId" AllowSorting="true" SortField="IntS" SortDirection="ASC"
                    AllowPaging="false" IsDatabasePaging="true" PageSize="100"  ForceFit="true"
                    EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>                                
                                <f:DropDownList ID="drpYear" runat="server" Label="年份" LabelAlign="Right" Width="250px"
                                    AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                 <f:Button runat ="server" ID="btnRefresh" Icon="ArrowRefresh" IconAlign="Left"
                                        OnClick="btnRefresh_Click" Hidden="true" ></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="55px" ColumnID="SortIndex" DataField="SortIndex"
                            SortField="SortIndex" FieldType="String" HeaderText="序号" HeaderTextAlign="Center"
                            TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="240px" ColumnID="HSEContent" DataField="HSEContent"
                            FieldType="String" HeaderText="HSE管理内容" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="MeasureUnit" DataField="MeasureUnit"
                            FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month1" DataField="Month1"
                            FieldType="String" HeaderText="1月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month2" DataField="Month2"
                            FieldType="String" HeaderText="2月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month3" DataField="Month3"
                            FieldType="String" HeaderText="3月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month4" DataField="Month4"
                            FieldType="String" HeaderText="4月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month5" DataField="Month5"
                            FieldType="String" HeaderText="5月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month6" DataField="Month6"
                            FieldType="String" HeaderText="6月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month7" DataField="Month7"
                            FieldType="String" HeaderText="7月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month8" DataField="Month8"
                            FieldType="String" HeaderText="8月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month9" DataField="Month9"
                            FieldType="String" HeaderText="9月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month10" DataField="Month10"
                            FieldType="String" HeaderText="10月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month11" DataField="Month11"
                            FieldType="String" HeaderText="11月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="60px" ColumnID="Month12" DataField="Month12"
                            FieldType="String" HeaderText="12月" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="80px" ColumnID="MonthSum" DataField="MonthSum"
                            FieldType="String" HeaderText="年度合计" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                    </Columns>
                </f:Grid>
            </Items>
            <Items>
                <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="true" Title="项目HSE月报提交情况" 
                    EnableCollapse="true" Collapsed="true" EnableCollapseEvent="true" EnableExpandEvent="true" OnCollapse="Grid2_Collapse" OnExpand="Grid2_Collapse"
                    runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="HSEDataCollectSubmissionId" ForceFit="true"
                    DataIDField="HSEDataCollectSubmissionId" AllowSorting="true" SortField="ProjectCode" SortDirection="ASC"
                    AllowPaging="false" IsDatabasePaging="true" PageSize="50"  EnableTextSelection="True" OnRowCommand="Grid2_RowCommand">
                    <Columns>
                          <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="60px" HeaderTextAlign="Center"
                         TextAlign="Center" />
                        <f:RenderField Width="140px" ColumnID="ProjectName" DataField="ProjectName"
                            FieldType="String" HeaderText="项目名称" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:GroupField ColumnID="xx" HeaderText="提交日期" TextAlign="Center">
                            <Columns>
                                <f:LinkButtonField Width="60px" ColumnID="Month1" DataTextField="Month1" CommandName="1"
                                     HeaderText="1月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month2" DataTextField="Month2"  CommandName="2"
                                     HeaderText="2月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month3" DataTextField="Month3"  CommandName="3"
                                     HeaderText="3月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month4" DataTextField="Month4"  CommandName="4"
                                     HeaderText="4月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month5" DataTextField="Month5"  CommandName="5"
                                     HeaderText="5月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month6" DataTextField="Month6"  CommandName="6"
                                     HeaderText="6月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month7" DataTextField="Month7"  CommandName="7"
                                     HeaderText="7月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month8" DataTextField="Month8"  CommandName="8"
                                     HeaderText="8月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month9" DataTextField="Month9"  CommandName="9"
                                     HeaderText="9月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month10" DataTextField="Month10"  CommandName="10"
                                     HeaderText="10月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month11" DataTextField="Month11"  CommandName="11"
                                     HeaderText="11月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                                <f:LinkButtonField Width="60px" ColumnID="Month12" DataTextField="Month12"  CommandName="12"
                                    HeaderText="12月" HeaderTextAlign="Center" TextAlign="Center" EnableAjax="false">
                                </f:LinkButtonField>
                            </Columns>
                        </f:GroupField>
                         <f:RenderField Width="1px" ColumnID="ProjectId" DataField="ProjectId" Hidden="true"
                            FieldType="String" HeaderText="项目名称" HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
         <f:Window ID="Window1" Title="月报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true"
        Width="1300px" Height="600px">
    </f:Window>
        <f:Menu ID="Menu1" runat="server">
            <%--<f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="Pencil" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>    --%>
        </f:Menu>
    </form>
    <script type="text/javascript">      

</script>
</body>
</html>
