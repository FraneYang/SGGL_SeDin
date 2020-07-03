<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeQRImage.aspx.cs" Inherits="FineUIPro.Web.HJGL.PersonManage.SeeQRImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>二维码查看</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
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
    <f:PageManager ID="PageManager1" runat="server" />
         <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
                   <f:Form ID="SimpleForm1" LabelAlign="Top" MessageTarget="Qtip" RedStarPosition="BeforeText"
                             BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server"
                            AutoScroll="false">
                              <Items>
                               <f:Panel ID="Panel5" Title="面板1" BoxFlex="3" runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox">
                                <Items>
                                    <f:Image ID="Image1" CssClass="userphoto" ImageUrl="~/res/images/blank_180.png" runat="server"
                                        BoxFlex="1">
                                    </f:Image>
                                </Items>                                                                                                                             
                            </f:Panel>                              
                        </Items>
                       
                </f:Form>
            </Items>
      </f:Panel>
    </form>
</body>
</html>