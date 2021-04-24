<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl9.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件9" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件9  施工分包商组织机构人员配置表及关键人员名单" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label7" runat="server" Text="9-1：施工分包商组织机构人员配置表"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" Title="9-1：施工分包商组织机构人员配置表" ShowBorder="false"
                            BodyPadding="1px" EnableCollapse="true" ShowHeader="false" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" LabelAlign="Top" BodyPadding="1px" ShowBorder="false" ShowHeader="false" runat="server">
                                <Items>
                                    <f:Grid ID="Grid1" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="施工分包商组织机构人员配置表" EnableCollapse="false"
                                        runat="server" DataKeyNames="AttachUrlItemId" AllowCellEditing="true" ClicksToEdit="1"
                                        EnableColumnLines="true" DataIDField="AttachUrlItemId" Height="200px">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar2" runat="server" Position="Top" ToolbarAlign="Left">
                                                <Items>
                                                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                                    <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                    <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:RowNumberField HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center" Width="60px"></f:RowNumberField>
                                            <f:RenderField Width="200px" ColumnID="WorkPostName" DataField="WorkPostName" FieldType="String"
                                                HeaderText="项目职务" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:TextBox ID="txtWorkPostName" runat="server"></f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="Number" DataField="Number" FieldType="Int"
                                                HeaderText="配置数量" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtNumber" runat="server" NoNegative="true" NoDecimal="true"></f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="Arrivaltime" DataField="Arrivaltime" FieldType="Date" Renderer="Date"
                                                HeaderText="到位时间" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:DatePicker ID="txtArrivaltime" runat="server"></f:DatePicker>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="Remarks" DataField="Remarks" ExpandUnusedSpace="true"
                                                HeaderText="备注" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:TextBox ID="txtRemarks" runat="server">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                    <f:Label runat="server" ID="Label210" Text="注：施工分包商人员实际配置数量不得少于上述数量，到位时间根据总承包商要求可以进行适当调整。其中：智慧化工地建设信息专员不得少于1人。"></f:Label>
                                </Items>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label8" runat="server" Text="9-2：施工分包商组织机构关键人员名单"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                            <f:Form ID="Form3" LabelAlign="Top" BodyPadding="0px" ShowBorder="false" ShowHeader="false" runat="server">
                                <Items>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtProjectManager" Label="项目经理" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtProjectEngineer" Label="项目总工" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtConstructionManager" Label="施工经理" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtQualityManager" Label="质量经理" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtHSEManager" Label="HSE经理" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtPersonnel_Technician" Label="技术员" runat="server" LabelWidth="70px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtPersonnel_Civil_engineering" Label="土建" runat="server" LabelWidth="60px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtPersonnel_Installation" Label="安装" runat="server" LabelWidth="60px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtPersonnel_Electrical" Label="电气" runat="server" LabelWidth="60px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtPersonnel_meter" Label="仪表" runat="server" LabelWidth="60px">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:Label runat="server" ID="Label2" Text="施工分包商擅自更换上述的施工分包商项目经理及其他主要项目管理人员，施工分包商承担违约责任："></f:Label>
                                    <f:Label runat="server" ID="Label3" Text="（1）	施工分包商将擅自更换人员复位。"></f:Label>
                                    <f:Label runat="server" ID="Label4" Text="（2）	若施工分包商不能在3天内将更换人员复位，总承包商对施工分包商进行罚款，罚款标准为："></f:Label>
                                    <f:Label runat="server" ID="Label5" Text="项目经理         50万元"></f:Label>
                                    <f:Label runat="server" ID="Label6" Text="其他主要项目管理人员（上述在列人员）30万元"></f:Label>
                                </Items>
                            </f:Form>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
