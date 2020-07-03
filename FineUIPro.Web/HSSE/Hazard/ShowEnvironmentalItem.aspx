<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowEnvironmentalItem.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Hazard.ShowEnvironmentalItem" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>环境因素危险源</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />        
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="环境因素危险源" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="EnvironmentalId" EnableColumnLines="true"  DataIDField="EnvironmentalId"
                AllowSorting="true" SortField="SmallTypeName,Code" SortDirection="ASC" OnSort="Grid1_Sort"
                AllowPaging="true" OnRowCommand="Grid1_RowCommand" IsDatabasePaging="true" PageSize="100"
                OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True" CheckBoxSelectOnly="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:DropDownList ID="drpSmallType" runat="server" Label="类型" AutoPostBack="true"
                                EnableEdit="true" LabelWidth="60px" Width="230px" LabelAlign="right" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:TextBox ID="txtActivePoint" runat="server" Label="分项工程/活动点" EmptyText="输入查询名称"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="300px" LabelWidth="140px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtEnvironmentalFactors" runat="server" Label="环境因素" EmptyText="输入查询名称"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                           <f:RadioButtonList ID="rblIsCompany" runat="server" Width="150px" 
                                        AutoPostBack="true" OnSelectedIndexChanged="rblIsCompany_SelectedIndexChanged" >
                                        <f:RadioItem Value="0" Text="内置"  />
                                        <f:RadioItem Value="1" Text="本公司"  Selected="true"/>
                                    </f:RadioButtonList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:CheckBox ID="ckALL" runat="server" Text="全选" MarginRight="10px" 
                                AttributeDataTag="ckbIsSelected" AutoPostBack="true" OnCheckedChanged="all_OnCheckedChanged">
                                <Listeners>
                                    <f:Listener Event="change" Handler="onQuickSetCheckChange" />
                                </Listeners>
                            </f:CheckBox>
                            <f:Button ID="btnSave" ToolTip="确定" Icon="Accept" runat="server" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false"
                        AutoPostBack="true" CommandName="IsSelected" HeaderText="选择" HeaderTextAlign="Center" />
                    <f:RenderField Width="90px" ColumnID="Code" DataField="Code" FieldType="String" HeaderText="代码"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="70px" ColumnID="SmallTypeName" DataField="SmallTypeName" FieldType="String"
                        HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ActivePoint" DataField="ActivePoint" FieldType="String"
                        ExpandUnusedSpace="true" HeaderText="分项工程/活动点" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="EnvironmentalFactors" DataField="EnvironmentalFactors"
                        FieldType="String" HeaderText="环境因素" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:GroupField HeaderText="污染类" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="45px" ColumnID="AValue" DataField="AValue" FieldType="String"
                                HeaderText="A值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="BValue" DataField="BValue" FieldType="String"
                                HeaderText="B值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="CValue" DataField="CValue" FieldType="String"
                                HeaderText="C值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="DValue" DataField="DValue" FieldType="String"
                                HeaderText="D值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="EValue" DataField="EValue" FieldType="String"
                                HeaderText="E值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="ZValue1" DataField="ZValue1" FieldType="String"
                                HeaderText="Σ" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField HeaderText="能源资源类" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="45px" ColumnID="FValue" DataField="FValue" FieldType="String"
                                HeaderText="F值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="GValue" DataField="GValue" FieldType="String"
                                HeaderText="G值" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="45px" ColumnID="ZValue2" DataField="ZValue2" FieldType="String"
                                HeaderText="Σ" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:CheckBoxField Width="60px" RenderAsStaticField="true" TextAlign="Center" DataField="IsImportant"
                        HeaderText="重要" />
                    <f:RenderField Width="100px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                        FieldType="String" HeaderText="安全措施" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>                  
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
                     <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>
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
    <script type="text/javascript">
        function renderBool(value) {
            return value == "True" ? '是' : '否';
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['SmallTypeName']);
        }
    </script>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        var grid1ClientID = '<%= Grid1.ClientID %>';
        function onQuickSetCheckChange() {
            var grid1 = F(grid1ClientID);
            var checkboxFieldID = this.getAttr('data-tag'), checked = this.getValue();

            var checkboxEls = grid1.el.find('.f-grid-cell-' + checkboxFieldID + ' .f-grid-checkbox');
            checkboxEls.toggleClass('f-checked', checked);
        }
    </script>
</body>
</html>
