﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWorkView.aspx.cs" Inherits="FineUIPro.Web.ProjectData.UnitWorkView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>单位工程</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                 <f:FormRow runat="server">
                    <Items>
                        <f:TextBox ID="txtUnitWorkCode" runat="server" Label="单位工程编号"
                            Required="true" MaxLength="100" ShowRedStar="true" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:TextBox ID="txtUnitWorkName" runat="server" Label="单位工程名称"
                            Required="true" MaxLength="100" ShowRedStar="true" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                       <f:TextBox ID="txtProjectType" runat="server" Label="所属工程" Readonly="true"
                            Required="true" MaxLength="50" ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                       <f:TextBox ID="txtCosts" runat="server" Label="建安工程费（万元）" Readonly="true"
                            MaxLength="50" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                       <f:TextBox ID="txtUnit" runat="server" Label="施工单位" Readonly="true"
                             MaxLength="50" ShowRedStar="true" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                       <f:TextBox ID="txtSupervisorUnit" runat="server" Label="监理单位" Readonly="true"
                             MaxLength="50" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                       <f:TextBox ID="txtNDEUnit" runat="server" Label="检测单位" Readonly="true"
                             MaxLength="50" LabelWidth="150px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" ID="txtMainItemAndDesignProfessional" Label="对应主项及设计专业" LabelWidth="150px"></f:Label>
                   </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckerId" runat="server">
                        </f:HiddenField>
                        <f:Button ID="btnClose" EnablePostBack="false" 
                            runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
