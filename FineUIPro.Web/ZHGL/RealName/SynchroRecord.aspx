<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SynchroRecord.aspx.cs"
    Inherits="FineUIPro.Web.ZHGL.RealName.SynchroRecord" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实名制数据推送记录</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="实名制数据推送日志" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="PushLogId"  AllowPaging="true"
                IsDatabasePaging="true" PageSize="20" OnPageIndexChange="Grid1_PageIndexChange"
                DataIDField="PushLogId" AllowSorting="true" SortField="PushTime"  EnableTextSelection="True"
                SortDirection="DESC"  EnableColumnLines="true" OnSort="Grid1_Sort" ForceFit="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="开始日期" LabelWidth="80px" Width="220px">
                            </f:DatePicker>
                            <f:DatePicker ID="txtEndDate" runat="server" Label="结束日期"  LabelWidth="80px" Width="220px">
                            </f:DatePicker> 
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSearch" runat="server" Icon="SystemSearch" ToolTip="查询" OnClick="btnSearch_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                       </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="60px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="ProjectCode" DataField="ProjectCode"
                        SortField="ProjectCode" FieldType="String" HeaderText="项目号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="230px" ColumnID="PushType" DataField="PushType" 
                        SortField="PushType" FieldType="String" HeaderText="数据类型" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                      <f:RenderField Width="150px" ColumnID="PushTime" DataField="PushTime" SortField="PushTime"
                        HeaderText="时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="Success" DataField="Success"
                        SortField="Success" FieldType="String" HeaderText="是否成功" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                      <f:RenderField Width="100px" ColumnID="Code" DataField="Code"
                        SortField="Code" FieldType="String" HeaderText="返回代号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="Message" DataField="Message" SortField="Message"
                        HeaderText="返回信息" HeaderTextAlign="Center" TextAlign="Left" FieldType="String">
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
</body>
</html>
