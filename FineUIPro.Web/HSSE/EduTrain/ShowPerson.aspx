<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPerson.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.ShowPerson" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查找人员</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="true" ShowHeader="false" Layout="HBox"
        Margin="5px" BodyPadding="5px">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="PersonId"   EnableCheckBoxSelect="true" ForceFit="true"
                EnableColumnLines="true"  DataIDField="PersonId" AllowSorting="true" PageSize="10000"
                SortField="InTime,PersonName" SortDirection="DESC" OnSort="Grid1_Sort"                
				 EnableTextSelection="True" >
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DropDownList ID="drpUnit" runat="server" Label="单位" AutoPostBack="true" Width="300px"
                                LabelWidth="50px" LabelAlign="right" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:TextBox runat="server" EmptyText="输入姓名" AutoPostBack="True" Label="姓名" LabelWidth="50px"
                                Width="180px" ID="txtPersonName" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                            <f:TextBox runat="server" EmptyText="输入卡号" AutoPostBack="True" Label="卡号" LabelWidth="50px"
                                Width="180px" ID="txtCardNo" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox> 
                             <f:TextBox runat="server" EmptyText="输入岗位" AutoPostBack="True" Label="岗位" LabelWidth="50px"
                                Width="180px" ID="txtWorkPostName" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox> 
                            <f:CheckBox ID="ckPostType2" runat="server" Label="特岗人员" LabelWidth="80px"
                                Width="80px" OnCheckedChanged="TextBox_TextChanged" AutoPostBack="true">
                            </f:CheckBox>
                            <f:CheckBox ID="ckIsHsse" runat="server" Label="安全人员" LabelWidth="80px"
                                Width="80px" OnCheckedChanged="TextBox_TextChanged" AutoPostBack="true">
                            </f:CheckBox>                                            
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSave" ToolTip="确认" Icon="Accept" runat="server" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>  
                     <f:RenderField HeaderText="单位名称" ColumnID="UnitName" DataField="UnitName" SortField="UnitName" ExpandUnusedSpace="true"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="200px">
                    </f:RenderField> 
                    <f:RenderField Width="90px" ColumnID="PersonName" DataField="PersonName" FieldType="String"
                        HeaderText="人员姓名" HeaderTextAlign="Center" TextAlign="Left" SortField="PersonName">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="CardNo" DataField="CardNo" FieldType="String"
                        HeaderText="卡号" HeaderTextAlign="Center" TextAlign="Left" SortField="CardNo">
                    </f:RenderField>
                    <f:RenderField HeaderText="性别" ColumnID="SexName" DataField="SexName" SortField="SexName" FieldType="String"
                        HeaderTextAlign="Center" TextAlign="Left" Width="60px">
                    </f:RenderField>
                    <f:RenderField HeaderText="岗位名称" ColumnID="WorkPostName" DataField="WorkPostName"
                        SortField="WorkPostName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                        Width="120px">
                    </f:RenderField>
                    <f:RenderField HeaderText="身份证号" ColumnID="IdentityCard" DataField="IdentityCard"
                        SortField="IdentityCard" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                        Width="170px">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="InTime" DataField="InTime"
                        SortField="InTime" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="入场时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField HeaderText="所在班组" ColumnID="TeamGroupName" DataField="TeamGroupName"
                        SortField="TeamGroupName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                        Width="110px">
                    </f:RenderField>
                    <f:RenderField HeaderText="单位工程" ColumnID="WorkAreaName" DataField="WorkAreaName"
                        SortField="WorkAreaName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                        Width="110px">
                    </f:RenderField>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>    
</body>
</html>
