<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUnitView.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectUnitView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" LabelWidth="140px"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <rows>
            <f:FormRow>
                <Items>                 
                   <f:Label ID="lbProjectName" runat="server" Label="项目名称"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>                 
                   <f:Label ID="lbUnitName" runat="server" Label="单位名称"></f:Label>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                    <f:TextBox ID="drpUnitType" Label="单位类型" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCollCropCode" Label="社会统一信用代码" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>   
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtLinkName" Label="联系人姓名" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="drpIdcardType" Label="联系人证件类型" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIdcardNumber" Label="联系人证件号" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtLinkMobile" Label="联系人电话" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>                 
                <Items>
                    <f:RadioButtonList runat="server" ID="rblIsChina" Label="是否中国企业">
                        <f:RadioItem Value="Y" Text="是" Selected="true"/>
                        <f:RadioItem Value="N" Text="否"  />
                    </f:RadioButtonList>
                    <f:RadioButtonList runat="server" ID="rblCollCropStatus" Label="是否黑名单企业">
                        <f:RadioItem Value="Y" Text="是" />
                        <f:RadioItem Value="N" Text="否" Selected="true" />
                    </f:RadioButtonList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>  
                 <f:TextBox runat="server" Label="入场时间" ID="txtInTime"  Readonly="true"></f:TextBox>
                     <f:TextBox runat="server" Label="出场时间" ID="txtOutTime"  Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow>                                     
        </rows>
        <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                 
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    </form>
</body>
</html>
