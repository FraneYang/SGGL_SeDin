<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendCardView.aspx.cs" Inherits="FineUIPro.Web.HSSE.SitePerson.SendCardView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="~/Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pagesetup_null() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
            } catch (e) { }
        }
        //设置网页打印的页眉页脚为默认值
        function pagesetup_default() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&w&b页码，&p/&P")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d")
            } catch (e) { }
        }
        function printpr() //预览函数
        {
            pagesetup_null(); //预览之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";
            var a = window.open('about:blank', 'Print');
            a.document.write('<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>' + document.getElementById('Table5').innerHTML);
            //ar WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            a.document.all.WebBrowser1.ExecWB(7, 1);
            a.close();
            //document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            //WebBrowser1.ExecWB(7, 1); //打印预览
            //WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //预览结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
        function print() //打印函数
        {
            pagesetup_null(); //打印之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";

            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            WebBrowser1.ExecWB(6, 6); //打印
            WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //打印结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="420px" cellpadding="0" cellspacing="0" align="center"
        border="0">
        <tr>
            <td style="width: 100%; background: url('~/Images/bg-1.gif')">
                <div id="div1">
                    <table id="tabbtn" runat="server" width="100%"  cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" valign="middle" style="width: 100%; height: 30px;">
                                <img src="~/Images/PageSetup.gif" runat="server" id="Img2" onclick="document.all.WebBrowser.ExecWB(8,1)"
                                    alt="页面设置" style="cursor: pointer" />
                                <img src="~/Images/PrintSetup.gif" runat="server" id="btnPrint" onclick="document.all.WebBrowser.ExecWB(6,1)"
                                    alt="打印设置" style="cursor: pointer" />
                                <img src="~/Images/PrintPreview.gif" runat="server" id="Img1" onclick="printpr()"
                                    alt="打印预览" style="cursor: pointer" />
                                <img src="~/Images/Print.gif" runat="server" id="Img3" onclick="print()" alt="打印"
                                    style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table5" runat="server" width="100%" border="1" cellpadding="0" cellspacing="0"> 
                    <tr style="height:32px">
                        <td  align="center" >
                             <table id="Table2" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"> 
                                 <tr style="height:25px">
                                     <td  align="center" style="width: 25%;" rowspan="2" >
                                        <img alt="" runat="server" id="Img4" src="~/Images/SUBimages/CNCEC.png" width="80" height="80"/>
                                    </td>
                                     <td align="center" style="width: 50%; ">
                                        <asp:Label runat="server" ID="lbThisUnit" Font-Bold="true" Font-Size="12"></asp:Label>
                                    </td>
                                    <td  align="center" style="width:25%; " rowspan="2">
                                        <img alt="" runat="server" id="Img5" src="~/Images/SUBimages/二维码图片.png" width="80" height="80"/>
                                    </td>
                                </tr>       
                                  <tr style="height:20px">
                                     <td align="center">
                                        <asp:Label runat="server" ID="lbProject" Font-Bold="true" Font-Size="12"></asp:Label>
                                    </td>
                                </tr>       
                             </table>
                        </td>
                    </tr>                   
                    <tr>
                       <td>
                          <table id="Table3" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color:cornflowerblue"> 
                               <tr>
                                    <td align="right" style="width: 30%; ">
                                        <asp:Label runat="server" ID="Label2" Text="单      位：" Font-Size="11" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 50%; ">
                                        <asp:Label ID="txtUnit" runat="server" Width="95%" CssClass="textboxStyle" Font-Size="11" Font-Bold="true">
                                        </asp:Label>
                                    </td>
                                    <td  align="center" style="width: 30%;" rowspan="4">
                                        <img alt="" runat="server" id="Img6" src="~/res/images/my_face_80.jpg" width="160" height="160"/>
                                    </td>
                               </tr>
                                <tr>
                                    <td align="right" style="width: 30%; ">
                                        <asp:Label runat="server" ID="Label4" Text="工种/职务：" Font-Size="11" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 50%; ">
                                        <asp:Label ID="txtWorkPost" runat="server" Width="95%" CssClass="textboxStyle" Font-Size="11" Font-Bold="true">
                                        </asp:Label>
                                    </td>
                               </tr>
                              <tr>
                                    <td align="right" style="width: 30%; ">
                                        <asp:Label runat="server" ID="Label6" Text="姓      名：" Font-Size="11" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 50%; ">
                                        <asp:Label ID="txtName" runat="server" Width="95%" CssClass="textboxStyle" Font-Size="11" Font-Bold="true">
                                        </asp:Label>
                                    </td>
                               </tr>
                                <tr>
                                    <td align="right" style="width: 30%; ">
                                        <asp:Label runat="server" ID="Label8" Text="编      号：" Font-Size="11" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 50%; ">
                                        <asp:Label ID="txtCardNo" runat="server" Width="95%" CssClass="textboxStyle" Font-Size="11" Font-Bold="true">
                                        </asp:Label>
                                    </td>
                               </tr>
                            </table>
                        </td>
                    </tr>
                </table>               
            </td>
        </tr>
    </table>
    <object id="WebBrowser" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2">
    </object>
    </form>
</body>
</html>
