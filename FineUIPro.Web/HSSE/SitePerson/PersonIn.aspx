﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonIn.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.PersonIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入人员信息</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="审核" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>
                    <f:Button ID="btnImport" Icon="ApplicationGet" runat="server" ToolTip="导入" ValidateForms="SimpleForm1"
                        OnClick="btnImport_Click">
                    </f:Button>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                    </f:Button>
                    <%--<f:Button ID="btnOut" Icon="Pencil" runat="server" Text="导出" ToolTip="导出错误列表" ValidateForms="SimpleForm1"
                        OnClick="btnOut_Click" Hidden="true"></f:Button>--%>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="PersonId" DataIDField="PersonId" 
                        AllowSorting="true" SortField="CardNo"  PageSize="500" Height="400px">
                        <Columns>
                            <f:TemplateField Width="55px" HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="70px" ColumnID="CardNo" DataField="CardNo" FieldType="String"
                                HeaderText="卡号" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="PersonName" DataField="PersonName"
                                SortField="PersonName" FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="70px" ColumnID="SexName" DataField="SexName"
                                SortField="SexName" FieldType="String" HeaderText="性别" HeaderTextAlign="Center"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="IdcardTypeName" DataField="IdcardTypeName" SortField="IdcardTypeName"
                                FieldType="String" HeaderText="证件类型" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                                FieldType="String" HeaderText="证件号码" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IdcardStartDate" DataField="IdcardStartDate" SortField="IdcardStartDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="证件开始日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IdcardForeverStr" DataField="IdcardForeverStr" SortField="IdcardForeverStr"
                                FieldType="String" HeaderText="证件是否永久有效" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="100px" ColumnID="IdcardEndDate" DataField="IdcardEndDate" SortField="IdcardEndDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="证件有效日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IdcardAddress" DataField="IdcardAddress" SortField="IdcardAddress"
                                FieldType="String" HeaderText="发证机关" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="PostName" DataField="PostName" SortField="PostName"
                                FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="TeamGroupName" DataField="TeamGroupName" FieldType="String"
                                HeaderText="所在班组" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="WorkAreaName" DataField="WorkAreaName" FieldType="String"
                                HeaderText="单位工程" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="InTime" DataField="InTime" SortField="InTime"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="入场时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="PositionName" DataField="PositionName" FieldType="String"
                                HeaderText="所属职务" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="PostTitleName" DataField="PostTitleName" FieldType="String"
                                HeaderText="所属职称" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="EduLevelName" DataField="EduLevelName" FieldType="String"
                                HeaderText="文化程度" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="MaritalStatusName" DataField="MaritalStatusName" FieldType="String"
                                HeaderText="婚姻状况" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="PoliticsStatusName" DataField="PoliticsStatusName" FieldType="String"
                                HeaderText="政治面貌" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="NationName" DataField="NationName" FieldType="String"
                                HeaderText="民族" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="CountryName" DataField="CountryName" FieldType="String"
                                HeaderText="国家" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="ProvinceName" DataField="ProvinceName" FieldType="String"
                                HeaderText="省或地区" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="MainCNProfessionalName" DataField="MainCNProfessionalName" FieldType="String"
                                HeaderText="主专业" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="ViceCNProfessionalName" DataField="ViceCNProfessionalName" FieldType="String"
                                HeaderText="副专业" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="120px" ColumnID="CertificateName" DataField="CertificateName" SortField="CertificateName"
                                FieldType="String" HeaderText="特岗证书" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="CertificateCode" DataField="CertificateCode" SortField="CertificateCode"
                                FieldType="String" HeaderText="证书编号" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="CertificateLimitTime" DataField="CertificateLimitTime" SortField="CertificateLimitTime"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="证书有效期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Birthday" DataField="Birthday" SortField="Birthday"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="出生日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                                FieldType="String" HeaderText="电话" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="Address" DataField="Address" FieldType="String"
                                HeaderText="家庭地址" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="OutTime" DataField="OutTime" SortField="OutTime"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="出场时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="OutResult" DataField="OutResult" SortField="OutResult"
                                FieldType="String" HeaderText="出场原因" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsForeignStr" DataField="IsForeignStr" FieldType="String"
                                HeaderText="是否外籍" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsOutsideStr" DataField="IsOutsideStr" FieldType="String"
                                HeaderText="是否外聘" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="IsUsedName" DataField="IsUsedName" FieldType="String"
                                HeaderText="人员在场" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="IsCardUsedName" DataField="IsCardUsedName"
                                FieldType="String" HeaderText="考勤卡启用" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdCheckResult" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：1 人员信息导入模板中，灰色项为必填项。2 身份证号码必须为15或18位，所属单位、所在班组、单位工程、岗位、特岗证书等必须与基础信息中对应类型的名称一致,否则无法导入。3 如需修改已有人员信息，请到系统中修改。4 数据导入后，点击“保存”，即可完成人员信息导入。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="审核人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
</body>
</html>
