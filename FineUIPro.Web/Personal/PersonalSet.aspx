<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalSet.aspx.cs" Inherits="FineUIPro.Web.Personal.PersonalSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .userphoto .f-field-label
        {
            margin-top: 0;
        }
        
        .userphoto img
        {
            width: 150px;
            height: 180px;            
        }
        
        .uploadbutton .f-btn
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">   
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>         
      <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="480px" ShowBorder="true"
                TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">              
                   <Tabs>                   
                        <f:Tab ID="Tab1" Title="基础信息" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                        <Items>                        
                          <f:Form ID="SimpleForm1" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText"
                            LabelWidth="90px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server"
                            AutoScroll="false" EnableTableStyle="true">
                                <Items>
                                    <f:Panel ID="Panel3" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox"
                                        BoxConfigAlign="StretchMax">
                                        <Items>
                                            <f:Panel ID="Panel1" Title="面板1" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                                                Width="200px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtUserName" runat="server" Label="姓名" FocusOnPageLoad="true" ShowRedStar="true" Required="true" MaxLength="20">
                                                    </f:TextBox>
                                                     <f:TextBox ID="txtUserCode" runat="server" Label="用户编号" MaxLength="20">
                                                    </f:TextBox>
                                                    <f:DropDownList ID="drpSex" Label="性别" runat="server">
                                                    </f:DropDownList>
                                                    <f:DatePicker ID="dpBirthDay" Label="出生日期" EmptyText="请选择日期" runat="server" EnableEdit="true">
                                                    </f:DatePicker>                                                
                                                    <f:DropDownList ID="drpMarriage" EnableEdit="true" Label="婚姻状况" runat="server">
                                                    </f:DropDownList>
                                                    <f:DropDownList ID="drpNation" EnableEdit="true" Label="民族" runat="server">
                                                    </f:DropDownList>
                                                    <f:DropDownList ID="drpUnit" EnableEdit="true" Label="所在单位" runat="server" ShowRedStar="true" Required="true">
                                                    </f:DropDownList>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel4" runat="server" BoxFlex="5" ShowBorder="false" ShowHeader="false"
                                                Width="200px" MarginRight="5px" Layout="VBox">
                                                <Items>       
                                                    <f:TextBox ID="txtAccount" runat="server" Label="登录账号" ShowRedStar="true" Required="true">
                                                    </f:TextBox>                               
                                                    <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号" MaxLength="50" ShowRedStar="true">
                                                    </f:TextBox>
                                                    <f:TextBox ID="txtEmail" runat="server" Label="邮箱" MaxLength="100">
                                                    </f:TextBox>
                                                    <f:TextBox ID="txtTelephone" runat="server" Label="手机号" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:DropDownList ID="drpEducation" EnableEdit="true" Label="文化程度" runat="server">
                                                    </f:DropDownList>
                                                    <f:TextBox ID="txtHometown" runat="server" Label="籍贯" MaxLength="50">
                                                    </f:TextBox>
                                                    <f:DropDownList ID="drpPosition" EnableEdit="true" Label="职务" runat="server">
                                                    </f:DropDownList>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel5" Title="面板1" BoxFlex="3" runat="server" ShowBorder="false" ShowHeader="false"
                                                Layout="VBox">
                                                <Items>
                                                    <f:Image ID="Image1" CssClass="userphoto" ImageUrl="~/res/images/blank_180.png" runat="server"
                                                        BoxFlex="1">
                                                    </f:Image>
                                                </Items>
                                                <Items>                                              
                                                    <f:FileUpload runat="server" ID="filePhoto" EmptyText="请选择照片" OnFileSelected="btnPhoto_Click" AutoPostBack="true">
                                                    </f:FileUpload>
                                                </Items>
                                            </f:Panel>                                                     
                                        </Items>
                                    </f:Panel>
                                </Items>  
                                <Items>
                                  <f:Form ID="Form7" ShowBorder="true" ShowHeader="false" runat="server">
                                        <Rows>                                                                            
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea ID="txtPerformance" runat="server" Label="个人简历" MaxLength="800">
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>                                      
                                        </Rows>
                                    </f:Form>
                                </Items>   
                            </f:Form> 
                           </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                                   <Items>
                                     <f:DropDownList runat="server" ID="drpPageSize" LabelWidth="150px" Label="列表每页显示记录数">
                                        <f:ListItem Text="10" Value="10" />
                                        <f:ListItem Text="15" Value="15" />
                                        <f:ListItem Text="20" Value="20" />
                                        <f:ListItem Text="25" Value="25" />
                                        <f:ListItem Text="所有行" Value="100000" />
                                    </f:DropDownList>      
                                    <f:Image ID="Image2" ImageUrl="~/res/images/Signature0.png" runat="server" Height="35px" Width="100px"
                                        BoxFlex="1" Hidden="true">
                                    </f:Image>
                                    <f:FileUpload runat="server" ID="fileSignature" EmptyText="请选择签名" Hidden="true"
                                        OnFileSelected="btnSignature_Click" AutoPostBack="true" Width="150px">
                                    </f:FileUpload>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnTab1Save" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                        OnClick="btnTab1Save_Click"  >
                                    </f:Button>                                     
                                </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab2" Title="密码管理" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Items>
                                <f:Form ID="SimpleForm2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:Label ID="LabelName" runat="server" Label="姓名">
                                                </f:Label>
                                                <f:Label ID="LabelAccount" runat="server" Label="账号">
                                                </f:Label>
                                            </Items>
                                         </f:FormRow>                                       
                                        <f:FormRow>
                                            <Items>
                                                <f:TextBox ID="txtOldPassword" Label="原密码" TextMode="Password" FocusOnPageLoad="true"
                                                            EmptyText="输入原密码" Required="true" ShowRedStar="true" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow>
                                             <Items>
                                                <f:TextBox ID="txtNewPassword" Label="新密码" TextMode="Password"
                                                            EmptyText="输入新密码" Required="true" ShowRedStar="true" runat="server" >
                                                </f:TextBox>
                                            </Items>  
                                        </f:FormRow>
           
                                         <f:FormRow>
                                            <Items>
                                              <f:TextBox ID="txtConfirmPassword" Label="确认密码" TextMode="Password" runat="server"
                                                CompareControl="txtNewPassword" CompareOperator="Equal" CompareMessage="确认密码输入不一致！"
                                                 EmptyText="再次输入新密码" Required="true" ShowRedStar="true" > 
                                              </f:TextBox>
                                            </Items>               
                                        </f:FormRow>                    
                                    </Rows>
                                </f:Form>
                            </Items>
                             <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                                   <Items>
                                    <f:Button ID="btnTab2Save" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm2"
                                        OnClick="btnTab2Save_Click">
                                    </f:Button>                                     
                                </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>                                                
                    </Tabs>
                </f:TabStrip>
              </Items>
        </f:Panel>
    </form>
</body>
</html>
