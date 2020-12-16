<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.UnitEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑单位设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" LabelWidth="140px"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitCode" runat="server" Label="代码" Required="true" 
                        MaxLength="100" ShowRedStar="true" FocusOnPageLoad="true" >
                    </f:TextBox>
                     <f:TextBox ID="txtUnitName" runat="server" Label="名称" Required="true" 
                         MaxLength="200" ShowRedStar="true" >
                    </f:TextBox>
                </Items>
            </f:FormRow>         
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlUnitTypeId" runat="server" Label="类型" EnableEdit="true"
                        ForceSelection="false">
                    </f:DropDownList>
                      <f:TextBox ID="txtCorporate" runat="server" Label="法人代表" MaxLength="100">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
           
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTelephone" runat="server" Label="电话" MaxLength="20">
                    </f:TextBox>
                    <f:TextBox ID="txtEMail" runat="server" Label="邮箱" MaxLength="200">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtFax" runat="server" Label="传真" MaxLength="20">
                    </f:TextBox>
                    <f:TextBox ID="txtAddress" runat="server" Label="地址" MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>                 
                <Items>
                    <f:RadioButtonList runat="server" ID="rblIsBranch" Label="分公司">
                        <f:RadioItem Value="true" Text="是" />
                        <f:RadioItem Value="false" Text="否" Selected="true" />
                    </f:RadioButtonList>
                    <f:TextBox ID="txtShortUnitName" runat="server" Label="单位简称" Required="true" 
                         MaxLength="6" ShowRedStar="true" >
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>                 
                <Items>
                    <f:RadioButtonList runat="server" ID="rblIsChina" Label="是否中国企业">
                        <f:RadioItem Value="Y" Text="是" Selected="true"/>
                        <f:RadioItem Value="N" Text="否"  />
                    </f:RadioButtonList>
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
                    <f:TextArea ID="txtProjectRange" runat="server" Height="100px" Label="工程范围" MaxLength="1000">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>                 
                <Items>
                    <f:RadioButtonList runat="server" ID="rblCollCropStatus" Label="是否黑名单企业">
                        <f:RadioItem Value="Y" Text="是" />
                        <f:RadioItem Value="N" Text="否" Selected="true" />
                    </f:RadioButtonList>
                    <f:Label runat="server"></f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"  Hidden="true"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" >
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
