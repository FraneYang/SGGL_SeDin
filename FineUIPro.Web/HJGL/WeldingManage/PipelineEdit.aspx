<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PipelineEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.WeldingManage.PipelineEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑管线信息</title>
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="管线信息"
                TitleToolTip="管线信息" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtPipelineCode" Label="管线号" ShowRedStar="true"
                                        Required="true" runat="server" FocusOnPageLoad="true" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtUnitName" Label="单位名称" Readonly="true"
                                        runat="server" LabelWidth="140px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtWorkAreaCode" Label="单位工程编号" Readonly="true"
                                        runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSingleNumber" Label="单线图号" runat="server"
                                        LabelWidth="140px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                     <f:DropDownList ID="drpMedium" Label="介质名称" runat="server"
                                       ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpPipingClass" Label="管道等级" runat="server"
                                       ShowRedStar="true" Required="true"  EnableEdit="true" LabelWidth="140px" >
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                     <f:DropDownList ID="drpDetectionRate" Label="探伤比例" runat="server"
                                       ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpDetectionType" Label="探伤类型" runat="server" EnableCheckBoxSelect="true"  EnableMultiSelect="true" 
                                       ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="140px"  AutoSelectFirstItem="false">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow> 
                              <f:FormRow>
                                <Items> 
                                    <f:NumberBox ID="numDesignTemperature" Label="设计温度℃" runat="server" LabelWidth="120px"></f:NumberBox>
                                     <f:NumberBox ID="numDesignPress" Label="设计压力 MPa(g)" runat="server"  LabelWidth="140px">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                     <f:DropDownList ID="drpTestMedium" Label="压力试验介质" runat="server"
                                        EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList>
                                    <f:NumberBox ID="numTestPressure" Label="压力试验压力 MPa(g)" runat="server" LabelWidth="140px"></f:NumberBox>
                                </Items>
                            </f:FormRow>
                             <f:FormRow>
                                <Items>
                                     <f:DropDownList ID="drpPressurePipingClass" Label="压力管道级别" runat="server"
                                        EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList>
                                    <f:NumberBox ID="numPipeLenth" Label="管线长度(m)" runat="server" LabelWidth="140px"></f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                     <f:DropDownList ID="drpLeakMedium" Label="泄露试验介质" runat="server"
                                        EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList>
                                    <f:NumberBox ID="numLeakPressure" Label="泄露试验压力 MPa(g)" runat="server" LabelWidth="140px"></f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                     <f:DropDownList ID="drpPCMedium" Label="吹洗要求" runat="server"
                                        EnableEdit="true" LabelWidth="120px">
                                    </f:DropDownList>
                                    <f:NumberBox ID="numVacuumPressure" Label="真空试验压力 MPa(g)" runat="server" LabelWidth="140px"></f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtRemark" Label="备注" runat="server" LabelWidth="120px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
  
    </form>
    <script type="text/javascript">
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
