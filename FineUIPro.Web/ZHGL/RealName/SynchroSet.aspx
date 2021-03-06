﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SynchroSet.aspx.cs" Inherits="FineUIPro.Web.ZHGL.RealName.SynchroSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtapiUrl" runat="server" Label="接口地址" MaxLength="500" 
                            Required="true" ShowRedStar="true" LabelWidth="120px"  Readonly="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
               <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtUserName" runat="server" Label="对接账号" MaxLength="50"  
                            LabelWidth="120px"  Required="true" ShowRedStar="true">
                        </f:TextBox>
                          <f:TextBox ID="txtword" runat="server" Label="对接密钥" MaxLength="50"  
                              LabelWidth="120px"  Required="true" ShowRedStar="true">
                        </f:TextBox>                         
                    </Items>
                </f:FormRow>
               <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtclientId" runat="server" Label="请求ID" MaxLength="50" 
                            LabelWidth="120px" Required="true" ShowRedStar="true">
                        </f:TextBox>     
                         <f:NumberBox ID="txtintervaltime" runat="server" Label="同步频率(分钟)" LabelWidth="120px"
                              NoDecimal="true"  NoNegative="true" Readonly="true">
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>                               
                         <f:Button ID="btnConnect" Icon="Connect" runat="server" ToolTip="连接" 
                             Hidden="true" OnClick="btnConnect_Click">
                        </f:Button>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" 
                             Hidden="true" OnClick="btnSave_Click">
                        </f:Button>
                        <f:DropDownList runat="server" Width="350px" ID="drpProject" EnableEdit="true" Label="项目"
                                LabelWidth="60px" LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="drpProject_SelectedIndexChanged">
                        </f:DropDownList>
                         <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnCompany" IconFont="_Link" runat="server" Text="推送参建企业" 
                             Hidden="true" OnClick="btnCompany_Click">
                        </f:Button>
                        <f:Button ID="btnProCollCompany" IconFont="_Link" runat="server" Text="推送项目参建企业" 
                          Hidden="true"   OnClick="btnProCollCompany_Click">
                        </f:Button>
                        <f:Button ID="btnCollTeam" IconFont="_Link" runat="server" Text="推送施工队" 
                          Hidden="true"   OnClick="btnCollTeam_Click">
                        </f:Button>
                        <f:Button ID="btnPersons" IconFont="_Link" runat="server" Text="推送人员" 
                            Hidden="true" OnClick="btnPersons_Click">
                        </f:Button>
                        <f:Button ID="btnAttendance" IconFont="_Link" runat="server" Text="推送考勤"
                            Hidden="true" OnClick="btnAttendance_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>        
    </form>
</body>
</html>
