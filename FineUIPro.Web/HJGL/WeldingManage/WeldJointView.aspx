<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldJointView.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.WeldJointView" %>

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
            <Items>
                <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="管线数据" runat="server">
                    <Items>
                        <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" BodyPadding="10px"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtPipelineCode" Label="管线号"
                                            runat="server" LabelWidth="120px" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtPipingClass" Label="管道等级"
                                            runat="server" LabelWidth="120px" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtDetectionRate" Label="探伤比例"
                                            runat="server" LabelWidth="120px" Readonly="true">
                                        </f:TextBox>
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
                                        <f:TextBox ID="txtWeldJointCode" Label="焊口号" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtMaterial1" Label="材质1" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtMaterial2" Label="材质2" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtDia" Label="外径" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtSize" Label="达因" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtThickness" Label="壁厚" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtSpecification" Label="规格" runat="server" Readonly="true"
                                            LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtWeldTypeCode" Label="焊缝类型" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtDetectionType2" Label="检测类型" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtComponent1" Label="组件1号" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtComponent2" Label="组件2号" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtDesignIsHotProess" Label="是否热处理" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
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
                                        <f:TextBox ID="txtWeldingMethod" Label="焊接方法" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtWPQCode" Label="WPS编号" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtGrooveType" Label="坡口类型" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtWeldingRod" Label="焊条" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtWeldingWire" Label="焊丝" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtPreTemperature" runat="server" Label="预热温度" MaxLength="50" LabelAlign="Right" Readonly="true"
                                            LabelWidth="100px">
                                        </f:TextBox>
                                        <f:TextBox ID="txtIsHotProess" Label="是否热处理" Readonly="true"
                                            runat="server" LabelWidth="100px" LabelAlign="Right">
                                        </f:TextBox>
                                        <f:TextBox ID="txtRemark" runat="server" Label="备注" MaxLength="200" LabelAlign="Right" Readonly="true"
                                            LabelWidth="100px">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtWpqId" Label="" runat="server"
                                            LabelWidth="100px" ShowRedStar="true" Required="true" Hidden="true">
                                        </f:TextBox>
                                        <f:TextBox runat="server" ID="hdWeldingMethodId" Hidden="true"></f:TextBox>
                                        <f:TextBox runat="server" ID="hdGrooveType" Hidden="true"></f:TextBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:GroupPanel>

            </Items>
        </f:Form>
    </form>
</body>
</html>
