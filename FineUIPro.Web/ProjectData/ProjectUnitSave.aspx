<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUnitSave.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectUnitSave" %>

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
                    <f:DropDownList ID="drpUnitType" Label="单位类型" runat="server" FocusOnPageLoad="true">
                    </f:DropDownList> 
                    <f:TextBox ID="txtCollCropCode" runat="server" Label="社会统一信用代码" Required="true"
                         MaxLength="50" ShowRedStar="true" >
                    </f:TextBox>
                </Items>
            </f:FormRow>  
            <f:FormRow>                 
                <Items>
                    <f:TextBox ID="txtLinkName" runat="server" Label="联系人姓名"  
                         MaxLength="50" >
                    </f:TextBox>
                    <f:DropDownList ID="drpIdcardType" runat="server" Label="联系人证件类型" EnableEdit="true"
                       >
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>                 
                <Items>
                    <f:TextBox ID="txtIdcardNumber" runat="server" Label="联系人证件号"  
                         MaxLength="50" >
                    </f:TextBox>
                    <f:TextBox ID="txtLinkMobile" runat="server" Label="联系人电话"  
                         MaxLength="50" >
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
                 <f:DatePicker runat="server" Label="入场时间" ID="txtInTime"></f:DatePicker>
                     <f:DatePicker runat="server" Label="出场时间" ID="txtOutTime"></f:DatePicker>
                </Items>
            </f:FormRow>    
            <f:FormRow>
                <Items>  
                 <f:NumberBox ID="nbPlanCostA" NoDecimal="false" NoNegative="false" MinValue="0" runat="server" 
                        Label="安全生产费</br>计划额（总额）"  LabelWidth="120px" Hidden="true">
                    </f:NumberBox>
                    <f:NumberBox ID="nbPlanCostB" NoDecimal="false" NoNegative="false" MinValue="0" runat="server" 
                        Label="文明施工措施费</br>计划额（总额）"  LabelWidth="120px" Hidden="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>   
            <f:FormRow>
                <Items>  
                 <f:TextArea runat="server" ID="txtContractRange" Label="承包范围" Height="60px">
                            </f:TextArea>
                </Items>
            </f:FormRow>                 
        </rows>
        <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    </form>
</body>
</html>
