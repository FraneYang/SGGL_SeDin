<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonTrainingTaskItem.aspx.cs" Inherits="FineUIPro.Web.Person.PersonTrainingTaskItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-colheader-text {
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

                <f:Grid ID="Grid1"  ShowBorder="true" EnableAjax="false" ShowHeader="false" Title="施工图纸" EnableCollapse="true"
                    runat="server" BoxFlex="1" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="CompanyTrainingItemId" AllowSorting="true" SortField="CompanyTrainingItemId"
                    SortDirection="DESC" EnableColumnLines="true" ForceFit="true"
                    AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                    EnableRowDoubleClickEvent="true" AllowFilters="true" EnableTextSelection="True" >
                     <Columns>
                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" 
                            TextAlign="Center"/>
                        <f:RenderField Width="180px" ColumnID="UserName" DataField="UserName"
                            FieldType="String" HeaderText="姓名" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="TrainingPlanTitle" DataField="TrainingPlanTitle"
                            FieldType="String" HeaderText="培训标题" TextAlign="Center"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="CompanyTrainingName" DataField="CompanyTrainingName"
                            FieldType="String" HeaderText="教材类型" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="CompanyTrainingItemName" DataField="CompanyTrainingItemName"
                            FieldType="String" HeaderText="教材名称" TextAlign="Center" HeaderTextAlign="Center">
                        </f:RenderField>
                    </Columns>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="15" Value="15" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="25" Value="25" />
                            <f:ListItem Text="所有行" Value="100000" />
                        </f:DropDownList>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>

                    </PageItems>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>
</html>