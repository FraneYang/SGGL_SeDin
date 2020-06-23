<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="FineUIPro.Web.Personal.PersonalInfo" %>

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
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="440px" ShowBorder="true"
                TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">              
                   <Tabs>                   
                      <f:Tab ID="Tab1" Title="基础信息" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                        <Items>                        
                          <f:Form ID="SimpleForm1" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText"
                            LabelWidth="90px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server"
                            AutoScroll="false">
                                <Items>
                                    <f:Panel ID="Panel3" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox"
                                        BoxConfigAlign="StretchMax">
                                        <Items>
                                            <f:Panel ID="Panel1" Title="面板1" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                                                Width="200px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtUserName" runat="server" Label="姓名"  Readonly="true"></f:TextBox>                                                   
                                                    <f:TextBox ID="txtUserCode" runat="server" Label="用户编号"  Readonly="true"></f:TextBox>
                                                    <f:TextBox ID="drpSex" Label="性别" runat="server"  Readonly="true"></f:TextBox>
                                                    <f:TextBox ID="dpBirthDay" Label="出生日期"  runat="server"  Readonly="true"></f:TextBox>
                                                    <f:TextBox ID="drpMarriage"  Readonly="true" Label="婚姻状况" runat="server">
                                                    </f:TextBox>
                                                    <f:TextBox ID="drpNation"  Readonly="true" Label="民族" runat="server"></f:TextBox>
                                                    <f:TextBox ID="drpUnit"  Readonly="true" Label="所在单位" runat="server" ></f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel4" runat="server" BoxFlex="5" ShowBorder="false" ShowHeader="false"
                                                Width="200px" MarginRight="5px" Layout="VBox">
                                                <Items>       
                                                    <f:TextBox ID="txtAccount" runat="server" Label="登录账号"  Readonly="true">
                                                    </f:TextBox>                               
                                                    <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号"  Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox ID="txtEmail" runat="server" Label="邮箱"  Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox ID="txtTelephone" runat="server" Label="手机号"  Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox ID="drpEducation"  Label="文化程度" runat="server"  Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox ID="txtHometown" runat="server" Label="籍贯"  Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox ID="drpPosition" Label="职务" runat="server"  Readonly="true">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                           <f:Panel ID="Panel5" Title="面板1" BoxFlex="3" runat="server" ShowBorder="false" ShowHeader="false"
                                            Layout="VBox">
                                            <Items>
                                                <f:Image ID="Image1" CssClass="userphoto" ImageUrl="~/res/images/blank_180.png" runat="server"
                                                    BoxFlex="1">
                                                </f:Image>
                                            </Items>                                                   
                                        </f:Panel>                                                    
                                        </Items>
                                    </f:Panel>
                                </Items>  
                                <Items>
                                  <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" runat="server">
                                        <Rows>                                                                            
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea ID="txtPerformance" runat="server" Label="个人简历" Readonly="true">
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>                                      
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Form> 
                           </Items>                          
                        </f:Tab>
                        <f:Tab ID="Tab2" Title="其他信息" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Items>
                                <f:Form ID="SimpleForm2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>                                               
                                            </Items>
                                         </f:FormRow>                                                                                                                                 
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:Tab>                                                
                    </Tabs>
                </f:TabStrip>
              </Items>
        </f:Panel>
    </form>
</body>
</html>
