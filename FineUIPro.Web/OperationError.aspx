<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationError.aspx.cs" Inherits="FineUIPro.Web.OperationError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
 <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="67%" id="LeftTDTable" runat="server" style="background-color:White">
                
                 <table cellpadding=0 cellspacing=0 style="height: 380px" width=100%  id="LeftTable" runat="server">                        
                   <tr>
                    <td><div align="center"><asp:HyperLink id="HyperLink1" NavigateUrl="LogOff.aspx" runat="server">退出并重新登录</asp:HyperLink></div></td>
                   </tr>
                    <tr>
                        <td style="height: 390px" valign="top">
                            <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/Images/wrong.jpg" />
                        </td>
                   </tr>
                </table>
                
            </td>
         </tr>   
    </table>   
    </div>
    </form>
</body>
</html>  
