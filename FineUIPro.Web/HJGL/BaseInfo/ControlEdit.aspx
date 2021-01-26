<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.ControlEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑直径寸径对照</title>
    <base target="_self" />
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" LabelWidth="120px"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtDN" runat="server" Label="公称尺寸(DN)" NoNegative="true" DecimalPrecision="0">
                        </f:NumberBox>
                        <f:NumberBox ID="txtPipeSize" runat="server" Label="公称尺寸(NPS)" DecimalPrecision="3">
                        </f:NumberBox>
                        <f:NumberBox ID="txtOutSizeDia" runat="server" Label="外径(mm)" Required="true"
                            NoNegative="true" DecimalPrecision="2" ShowRedStar="true">
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtSCH5S" runat="server" Label="SCH5S/SCH5" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH10S" runat="server" Label="SCH10S" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH10" runat="server" Label="SCH10" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtSCH20" runat="server" Label="SCH20" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH30" runat="server" Label="SCH30" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH40S" runat="server" Label="SCH40S" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtSCH40" runat="server" Label="SCH40" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH60" runat="server" Label="SCH60" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH80S" runat="server" Label="SCH80S" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtSCH80" runat="server" Label="SCH80" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH100" runat="server" Label="SCH100" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH120" runat="server" Label="SCH120" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtSCH140" runat="server" Label="SCH140" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="txtSCH160" runat="server" Label="SCH160" NoNegative="true" DecimalPrecision="2">
                        </f:NumberBox>
                        <f:NumberBox ID="NumberBox5" runat="server" Label="SCH160" NoNegative="true" DecimalPrecision="2" Hidden="true">
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="壁厚(mm)" runat="server">
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" BodyPadding="10px"
                                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:NumberBox ID="txtThickness1" runat="server" Label="1" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtThickness2" runat="server" Label="2" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtThickness3" runat="server" Label="3" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:NumberBox ID="txtThickness4" runat="server" Label="1" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtThickness5" runat="server" Label="2" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtThickness6" runat="server" Label="3" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:NumberBox ID="txtThickness7" runat="server" Label="1" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtThickness8" runat="server" Label="2" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtThickness9" runat="server" Label="3" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                            <Items>
                                                <f:NumberBox ID="txtThickness10" runat="server" Label="1" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="txtThickness11" runat="server" Label="2" NoNegative="true" DecimalPrecision="2">
                                                </f:NumberBox>
                                                <f:NumberBox ID="NumberBox3" runat="server" Label="3" NoNegative="true" DecimalPrecision="2" Hidden="true">
                                                </f:NumberBox>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭"
                            runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
