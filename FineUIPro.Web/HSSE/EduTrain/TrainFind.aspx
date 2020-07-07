<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainFind.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TrainFind" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>            
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员培训查询" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ID" DataIDField="ID" AllowSorting="true" SortField="CardNo" SortDirection="ASC"
                EnableColumnLines="true" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" ForceFit="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>  
                            <f:CheckBoxList runat="server" ID="cbIssue" ShowLabel="false" Width="150px" LabelAlign="Right"
                                OnSelectedIndexChanged="Text_OnTextChanged" AutoPostBack="true">
                                <f:CheckItem Text="不合格" Value="0"  />
                                <f:CheckItem Text="合格" Value="1" Selected="true"/>
                            </f:CheckBoxList>                      
                             <f:TextBox ID="txtPersonName" runat="server" Label="姓名" 
                                 AutoPostBack="true" OnTextChanged="Text_OnTextChanged" LabelWidth="50px" Width="150px">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" 
                                OnSelectedIndexChanged="Text_OnTextChanged" LabelWidth="50px" Width="250px">
                            </f:DropDownList>                            
                            <f:TextBox ID="txtTeachMan" runat="server" Label="授课人" LabelAlign="right" AutoPostBack="true"
                                OnTextChanged="Text_OnTextChanged"  LabelWidth="70px" Width="140px">
                            </f:TextBox>
                            <f:DropDownList ID="drpTrainType" Label="类别" LabelAlign="right" EnableEdit="true"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="Text_OnTextChanged"  LabelWidth="50px" Width="180px">
                            </f:DropDownList>
                           <%--  <f:DropDownList ID="drpTrainLevel" runat="server" Label="级别" Width="150px" LabelWidth="50px" LabelAlign="Right"
                                AutoPostBack="true" OnSelectedIndexChanged="Text_OnTextChanged">
                            </f:DropDownList>--%>
                             <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                     <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                   
                    <f:RenderField Width="90px" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                        FieldType="String" HeaderText="卡号" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                        FieldType="String" HeaderText="姓名" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField Width="240px" ColumnID="tfUnitName" HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbUnitName" runat="server" Text='<%# Bind("UnitName") %>' ToolTip='<%#Bind("UnitName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="100px" ColumnID="tfWorkPostName" HeaderText="岗位名称" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbWorkPostName" runat="server" Text='<%# Bind("WorkPostName") %>' ToolTip='<%#Bind("WorkPostName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                     <f:TemplateField Width="130px" ColumnID="tfTrainTitle" HeaderText="培训标题" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbTrainTitle" runat="server" Text='<%# Bind("TrainTitle") %>' ToolTip='<%#Bind("TrainTitle") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="100px" ColumnID="tfTrainTypeName" HeaderText="培训类别" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbTrainTypeName" runat="server" Text='<%# Bind("TrainTypeName") %>' ToolTip='<%#Bind("TrainTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                 <%--   <f:TemplateField Width="80px" ColumnID="tfTrainLevelName" HeaderText="培训级别" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbTrainLevelName" runat="server" Text='<%# Bind("TrainLevelName") %>' ToolTip='<%#Bind("TrainLevelName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>        --%>           
                    <f:RenderField Width="100px" ColumnID="TrainStartDate" DataField="TrainStartDate"
                        SortField="TrainStartDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="培训日期" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="60px" ColumnID="TeachHour" DataField="TeachHour" SortField="TeachHour"
                        FieldType="Float" HeaderText="学时" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="TeachMan" DataField="TeachMan" SortField="TeachMan"
                        FieldType="String" HeaderText="授课人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField Width="80px" ColumnID="tfCheckResult" HeaderText="考核结果" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbCheckResult" runat="server" Text='<%# ConvertCheckResult(Eval("CheckResult")) %>'
                                ToolTip='<%# ConvertCheckResult(Eval("CheckResult")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
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
