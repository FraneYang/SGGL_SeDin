<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SynchroSet.aspx.cs" Inherits="FineUIPro.Web.ZHGL.RealName.SynchroSet" %>

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
                            Required="true" ShowRedStar="true" LabelWidth="120px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
               <f:FormRow>
                    <Items>
                        <f:TextBox ID="txt1" runat="server" Label="对接账号" MaxLength="50"  LabelWidth="120px">
                        </f:TextBox>
                          <f:TextBox ID="txtword" runat="server" Label="对接密钥" MaxLength="50"  
                              LabelWidth="120px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
               <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtclientId" runat="server" Label="请求ID" MaxLength="50"  LabelWidth="120px">
                        </f:TextBox>
                          <f:NumberBox ID="txtintervaltime" runat="server" Label="同步频率(分钟)" LabelWidth="120px"
                              NoDecimal="true"  NoNegative="true" >
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                         <f:Button ID="btnConnect" Icon="Connect" runat="server" ToolTip="连接" 
                             Hidden="true" OnClick="btnConnect_Click">
                        </f:Button>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存"
                            Hidden="true"  OnClick="btnSave_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>        
    </form>
</body>
</html>
