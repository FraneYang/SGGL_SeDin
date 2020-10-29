<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WPQEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WPQ.WPQEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑焊接工艺评定台账</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Form2" runat="server" />
        <f:Form ID="Form2" ShowBorder="False" BodyPadding="5px" ShowHeader="False" runat="server" AutoScroll="true">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWeldingProcedureCode" runat="server" Label="评定编号" LabelAlign="Right"
                            MaxLength="50" Required="true" ShowRedStar="true" LabelWidth="180px">
                        </f:TextBox>

                        <f:DropDownList ID="drpUnit" runat="server" Required="true" ShowRedStar="true"
                            Label="编制单位" LabelAlign="Right" LabelWidth="180px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        
                        <f:DropDownList ID="drpSteel1" runat="server" Label="材质1" LabelAlign="Right" Required="true"
                            ShowRedStar="true" LabelWidth="180px" AutoPostBack="true" OnSelectedIndexChanged="drpSteel1_SelectedIndexChanged">
                        </f:DropDownList>
                        <f:DropDownList ID="drpSteel2" runat="server" Label="材质2" LabelAlign="Right" LabelWidth="180px"
                           Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpSteel2_SelectedIndexChanged">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtMaterialClass1" runat="server" Label="材质1类别" LabelAlign="Right"
                            LabelWidth="180px" >
                        </f:TextBox>
                        <f:TextBox ID="txtMaterialClass2" runat="server" Label="材质2类别" LabelAlign="Right"
                            LabelWidth="180px" >
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtMaterialGroup1" runat="server" Label="材质1组别" LabelAlign="Right"
                            LabelWidth="180px" >
                        </f:TextBox>
                        <f:TextBox ID="txtMaterialGroup2" runat="server" Label="材质2组别" LabelAlign="Right"
                            LabelWidth="180px" >
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtSpecifications" runat="server" Label="规格" LabelAlign="Right" MaxLength="100"
                            LabelWidth="180px">
                        </f:TextBox>
                        <f:DropDownList ID="drpWeldingWire" runat="server" Label="焊丝" LabelAlign="Right" Required="true"
                            ShowRedStar="true" LabelWidth="180px" AutoPostBack="true" OnSelectedIndexChanged="drpWeldingWire_SelectedIndexChanged">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpWeldingRod" runat="server" Label="焊条" LabelAlign="Right"
                            AutoPostBack="true" OnSelectedIndexChanged="drpWeldingRod_SelectedIndexChanged" LabelWidth="180px" Required="true" ShowRedStar="true">
                        </f:DropDownList>
                            <f:DropDownList ID="drpGrooveType" runat="server" Label="坡口类型" LabelAlign="Right"
                            AutoPostBack="true" OnSelectedIndexChanged="drpGrooveType_SelectedIndexChanged"  LabelWidth="180px" Required="true" ShowRedStar="true">
                        </f:DropDownList>
                        
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpWeldType" runat="server" Label="接头形式" LabelAlign="Right" LabelWidth="180px">
                            <f:ListItem Text="对接焊缝" Value="对接焊缝" />
                            <f:ListItem Text="角焊缝" Value="角焊缝" />
                            <f:ListItem Text="支管连接焊缝" Value="支管连接焊缝" />
                        </f:DropDownList>
                        <f:DropDownList ID="drpWeldingMethodId" runat="server" Label="焊接方法" LabelAlign="Right"
                            LabelWidth="180px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtMinImpactDia" runat="server" Label="管径覆盖最小值(对接焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                        <f:NumberBox ID="txtMaxImpactDia" runat="server" Label="管径覆盖最大值(对接焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtMinImpactThickness" runat="server" Label="壁厚覆盖最小值(对接焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                        <f:NumberBox ID="txtMaxImpactThickness" runat="server" Label="壁厚覆盖最大值(对接焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                 <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtMinCImpactDia" runat="server" Label="管径覆盖最小值(角焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                        <f:NumberBox ID="txtMaxCImpactDia" runat="server" Label="管径覆盖最大值(角焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtNoMinImpactThickness" runat="server" Label="壁厚覆盖最小值(角焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                        <f:NumberBox ID="txtNoMaxImpactThickness" runat="server" Label="壁厚覆盖最大值(角焊缝)" LabelAlign="Right"
                            NoNegative="false" LabelWidth="180px">
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWeldingPosition" runat="server" Label="焊接位置" LabelAlign="Right"
                            MaxLength="50" LabelWidth="180px">
                        </f:TextBox>
                        <f:CheckBox ID="cbkIsHotTreatment" runat="server" Label="是否热处理" LabelAlign="Right"
                            LabelWidth="180px">
                        </f:CheckBox>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWPQStandard" runat="server" Label="评定标准" LabelAlign="Right" MaxLength="50" LabelWidth="170px"></f:TextBox>
                        <f:TextBox ID="txtProtectiveGas" runat="server" Label="保护气体" LabelAlign="Right" MaxLength="50" LabelWidth="170px"></f:TextBox>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtCompileDate" runat="server" Label="编制时间" LabelAlign="Right"
                            LabelWidth="170px">
                        </f:DatePicker>
                        <f:TextBox ID="txtPreTemperature" runat="server" Label="预热温度" MaxLength="500" LabelAlign="Right"
                            LabelWidth="170px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtRemark" runat="server" Label="备注" MaxLength="500" LabelAlign="Right"
                            LabelWidth="170px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:LinkButton ID="UploadAttach" runat="server" Label="附件" Text="上传和查看" OnClick="btnAttachUrl_Click"
                            LabelAlign="Right" LabelWidth="170px">
                        </f:LinkButton>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="提交数据" ValidateForms="Form2"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="文件上传" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="680px"
            Height="480px">
        </f:Window>
    </form>
</body>
</html>
