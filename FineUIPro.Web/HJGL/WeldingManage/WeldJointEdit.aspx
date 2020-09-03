<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldJointEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.WeldJointEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Form runat="server" ShowBorder="true" ShowHeader="false">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="管线数据" runat="server">
                    <Items>
                        <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" BodyPadding="10px"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtPipelineCode" Label="管线号"
                                            runat="server" FocusOnPageLoad="true" LabelWidth="120px" Readonly="true">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpPipingClass" Label="管道等级" runat="server"
                                            LabelWidth="120px" Readonly="true">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpDetectionRate" Label="探伤比例" runat="server"
                                            LabelWidth="120px" Readonly="true">
                                        </f:DropDownList>
                                        <f:TextBox ID="txtDetectionType" Label="探伤类型" runat="server" LabelWidth="120px" Readonly="true">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="设计基础数据" runat="server">
                    <Items>
                        <f:Form runat="server" ShowHeader="false" ShowBorder="false">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtWeldJointCode" Label="焊口号"
                                            runat="server" FocusOnPageLoad="true" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpMaterial1" Label="材质1"
                                            runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="100px" LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpMaterial2" Label="材质2"
                                            runat="server" EnableEdit="true" LabelWidth="100px" LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:NumberBox ID="txtDia" Label="外径" runat="server"
                                            LabelWidth="100px" DecimalPrecision="4" NoNegative="true" ShowRedStar="true" Required="true"
                                            EnableBlurEvent="true" OnBlur="txtText_TextChanged" LabelAlign="Right">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:NumberBox ID="txtSize" Label="达因" runat="server"
                                            LabelWidth="100px" DecimalPrecision="4" NoNegative="true" ShowRedStar="true" Required="true" LabelAlign="Right">
                                        </f:NumberBox>
                                        <f:NumberBox ID="txtThickness" Label="壁厚" runat="server"
                                            LabelWidth="100px" DecimalPrecision="4" NoNegative="true" ShowRedStar="true" Required="true"
                                            EnableBlurEvent="true" OnBlur="txtText_TextChanged" LabelAlign="Right">
                                        </f:NumberBox>
                                        <f:TextBox ID="txtSpecification" Label="规格" runat="server"
                                            LabelWidth="100px" ShowRedStar="true" Required="true" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpWeldTypeCode" Label="焊缝类型"
                                            runat="server" EnableEdit="true" AutoPostBack="true" Required="true" ShowRedStar="true" LabelWidth="100px" OnSelectedIndexChanged="drpWeldTypeCode_SelectedIndexChanged" LabelAlign="Right">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtDetectionTypeId" Label="检测类型" Readonly="true"
                                            runat="server" LabelWidth="100px" Required="true" ShowRedStar="true" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpComponent1" Label="组件1号" runat="server"
                                            LabelWidth="100px" LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpComponent2" Label="组件2号" runat="server"
                                            LabelWidth="100px" LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpDesignIsHotProess" runat="server" Label="是否热处理" LabelAlign="Right"
                                            LabelWidth="100px" AutoPostBack="true" OnSelectedIndexChanged="drpDesignIsHotProess_SelectedIndexChanged">
                                            <f:ListItem Value="False" Text="否" Selected="true" />
                                            <f:ListItem Value="True" Text="是" />
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                
                            </Rows>
                        </f:Form>
                    </Items>
                </f:GroupPanel>
                <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="施工基础数据" runat="server">
                    <Items>
                        <f:Form runat="server" ShowHeader="false" ShowBorder="false">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpWeldingMethodId" Label="焊接方法"
                                            runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="100px" LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:Panel ID="Panel3" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                                            <Items>
                                                <f:TextBox ID="txtWPQCode" Label="WPS编号" runat="server"
                                                    LabelWidth="100px" Width="250px" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:Button ID="search" OnClick="search_Click" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Panel>
                                        <f:DropDownList ID="drpGrooveType" Label="坡口类型" runat="server"
                                            EnableEdit="true" LabelWidth="100px" LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpWeldingRod" Label="焊条" runat="server"
                                            EnableEdit="true" LabelWidth="100px" AutoPostBack="true" OnSelectedIndexChanged="drpWeldingRod_SelectedIndexChanged" LabelAlign="Right">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:DropDownList ID="drpWeldingWire" Label="焊丝" runat="server"
                                            EnableEdit="true" LabelWidth="100px" AutoPostBack="true" OnSelectedIndexChanged="drpWeldingWire_SelectedIndexChanged" LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:TextBox ID="txtPreTemperature" runat="server" Label="预热温度" MaxLength="50" LabelAlign="Right"
                                            LabelWidth="100px">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpJointArea" Label="焊接区域"
                                            runat="server" EnableEdit="true" LabelWidth="100px"  LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:DropDownList ID="drpIsHotProess" runat="server" Label="是否热处理" LabelAlign="Right"
                                            LabelWidth="100px" >
                                            <f:ListItem Value="False" Text="否" Selected="true" />
                                            <f:ListItem Value="True" Text="是" />
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtRemark" runat="server" Label="备注" MaxLength="200" LabelAlign="Right"
                                            LabelWidth="100px">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpJointAttribute" Label="焊口属性" runat="server"
                                            EnableEdit="true" LabelWidth="100px"  LabelAlign="Right">
                                        </f:DropDownList>
                                        <f:TextBox ID="txtWpqId" Label="" runat="server"
                                            LabelWidth="100px" ShowRedStar="true" Required="true" Hidden="true">
                                        </f:TextBox>
                                        <f:TextBox runat="server" ID="txtIsHotProess" Hidden="true"></f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:GroupPanel>

            </Items>
        </f:Form>
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server"
            IsModal="true" Width="1200px" Height="620px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
