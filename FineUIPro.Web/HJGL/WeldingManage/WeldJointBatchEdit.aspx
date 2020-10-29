﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldJointBatchEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.WeldingManage.WeldJointBatchEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" BodyPadding="10px"
            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
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
        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" BodyPadding="10px"
            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">

            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWeldJointCode" Label="焊口号" EmptyText="前缀字符(可选择输入)"
                            runat="server" FocusOnPageLoad="true" LabelWidth="100px">
                        </f:TextBox>
                        <f:NumberBox ID="txtWeldJointCode1" Label="起" runat="server"
                            NoDecimal="true" NoNegative="true" ShowRedStar="true" Required="true" LabelWidth="100px">
                        </f:NumberBox>
                        <f:NumberBox ID="txtWeldJointCode2" Label="止" runat="server"
                            NoDecimal="true" NoNegative="true" CompareControl="txtWeldJointCode1" CompareType="Int"
                            CompareMessage="止数应大于等于起数" CompareOperator="GreaterThanEqual"
                            ShowRedStar="true" Required="true" LabelWidth="100px">
                        </f:NumberBox>
                        <f:TextBox ID="txtZhanwei" 
                            runat="server" LabelWidth="100px"  Hidden="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        
                        <f:DropDownList ID="drpMaterial1" Label="材质1"
                            runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="100px">
                        </f:DropDownList>
                        <f:DropDownList ID="drpMaterial2" Label="材质2"
                            runat="server" EnableEdit="true" LabelWidth="100px">
                        </f:DropDownList>
                        <f:DropDownList ID="drpWeldTypeCode" Label="焊缝类型"
                            runat="server" EnableEdit="true" AutoPostBack="true" Required="true" ShowRedStar="true" LabelWidth="100px" 
                            OnSelectedIndexChanged="drpWeldTypeCode_SelectedIndexChanged">
                        </f:DropDownList>
                       <f:NumberBox ID="txtDia" Label="外径" runat="server"
                            LabelWidth="100px" DecimalPrecision="4" NoNegative="true" ShowRedStar="true" Required="true"
                             EnableBlurEvent="true" OnBlur="txtText_TextChanged" >
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                       <f:NumberBox ID="txtSize" Label="达因" runat="server"
                            LabelWidth="100px" DecimalPrecision="4" NoNegative="true" ShowRedStar="true" Required="true">
                        </f:NumberBox>
                        <f:NumberBox ID="txtThickness" Label="壁厚" runat="server"
                            LabelWidth="100px" DecimalPrecision="4" NoNegative="true" ShowRedStar="true" Required="true"
                             EnableBlurEvent="true" OnBlur="txtText_TextChanged" >
                        </f:NumberBox>
                        <f:TextBox ID="txtSpecification" Label="规格" runat="server"
                            LabelWidth="100px" ShowRedStar="true" Required="true">
                        </f:TextBox>
                        <f:DropDownList ID="drpJointArea" Label="焊接区域"
                            runat="server" EnableEdit="true" LabelWidth="100px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>

                <f:FormRow>
                    <Items>
                        
                        <f:TextBox ID="txtDetectionTypeId" Label="检测类型" Readonly="true"
                            runat="server" LabelWidth="100px"  Required="true" ShowRedStar="true">
                        </f:TextBox>
                        <f:DropDownList ID="drpWeldingMethodId" Label="焊接方法"
                            runat="server" ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="100px">
                        </f:DropDownList>
                       <f:DropDownList ID="drpGrooveType" Label="坡口类型" runat="server"
                            EnableEdit="true" LabelWidth="100px">
                        </f:DropDownList>
                          <f:Panel ID="Panel3" Width="300px" ShowHeader="false" ShowBorder="false" Layout="Column" CssClass="" runat="server">
                            <Items>
                                <f:TextBox ID="txtWPQCode" Label="WPS编号" runat="server"
                                    LabelWidth="100px" Width="250px">
                                </f:TextBox>
                                <f:Button ID="search" OnClick="search_Click" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" runat="server">
                                </f:Button>
                            </Items>
                        </f:Panel>
                        
                        <%----%>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                      
                        <f:DropDownList ID="drpComponent1" Label="组件1号" runat="server"
                            LabelWidth="100px" >
                        </f:DropDownList>
                        <f:DropDownList ID="drpComponent2" Label="组件2号" runat="server"
                            LabelWidth="100px" >
                        </f:DropDownList>
                        <f:TextBox ID="txtHeartNo1" Label="炉批1号" runat="server"
                            LabelWidth="100px">
                        </f:TextBox>
                       <f:TextBox ID="txtHeartNo2" Label="炉批2号" runat="server"
                            LabelWidth="100px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                       
                        <f:DropDownList ID="drpWeldingRod" Label="焊条" runat="server"
                            EnableEdit="true" LabelWidth="100px" AutoPostBack="true" OnSelectedIndexChanged="drpWeldingRod_SelectedIndexChanged">
                        </f:DropDownList>
                        <f:DropDownList ID="drpWeldingWire" Label="焊丝" runat="server"
                            EnableEdit="true" LabelWidth="100px" AutoPostBack="true" OnSelectedIndexChanged="drpWeldingWire_SelectedIndexChanged">
                        </f:DropDownList>
                        <f:TextBox ID="txtPreTemperature" runat="server" Label="预热温度" MaxLength="500" LabelAlign="Right"
                            LabelWidth="100px">
                        </f:TextBox>
                        <f:DropDownList ID="drpIsHotProess" runat="server" Label="是否热处理" LabelAlign="Right"
                            LabelWidth="100px">
                             <f:ListItem Value="False" Text="否" Selected="true"/>
                             <f:ListItem Value="True" Text="是"/>
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWPQId" Label="" runat="server"
                            LabelWidth="100px" ShowRedStar="true" Required="true" Hidden="true">
                        </f:TextBox>
                         <f:DropDownList ID="drpJointAttribute" Label="焊口属性" runat="server"
                            EnableEdit="true" LabelWidth="100px"  Hidden="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="false" runat="server"
            IsModal="true" Width="1200px" Height="620px">
        </f:Window>
    </form>
</body>
</html>
