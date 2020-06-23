<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="GridNavgator.ascx.cs" Inherits="Web.Controls.GridNavgator" %>
<table style="FONT-SIZE:9pt" id="Table1" cellspacing="0" cellpadding="0"  border="0">
  <tr>
    <td style="WIDTH: 11px" align="left"></td>
    <td style="WIDTH: 95px" align="left">页次:<asp:Label id="Label2" runat="server" Font-Bold="True">1</asp:Label>/<asp:Label id="Label3" runat="server" Font-Bold="True">1</asp:Label><asp:Label ID=ye runat='server'></asp:Label></td>
    <td style="WIDTH: 45px"><asp:LinkButton id="LinkButton1" CommandName="Page" CommandArgument="First" runat="server">首页</asp:LinkButton></td>
    <td style="WIDTH: 50px"><asp:LinkButton CssClass="GridNavaLink" id="LinkButton2" CommandName="Page" CommandArgument="Prev"  runat="server">上一页</asp:LinkButton></td>
    <td style="WIDTH: 50px"><asp:LinkButton CssClass="GridNavaLink" id="LinkButton3" CommandName="Page" CommandArgument="Next" runat="server">下一页</asp:LinkButton></td>
    <td style="WIDTH: 45px"><asp:LinkButton CssClass="GridNavaLink" id="LinkButton4" CommandName="Page" CommandArgument="Last"  runat="server">尾页</asp:LinkButton></td>
    <td style="WIDTH: 85px" id="pagelines" runat="server"><strong><asp:Label id="Label1" Font-Bold="True" runat="server">1</asp:Label></strong>条信息/页</td>
    <td style="WIDTH: 55px" align="right"><asp:Label ID="GOTO" runat="server">转到:</asp:Label></td>
    <td style="WIDTH: 20px" align="left"><asp:TextBox id="TextBox1" runat="server" Width="30px"></asp:TextBox></td>
    <td style="WIDTH: 20px" align="left"><asp:Label ID="ye2" runat="server">页</asp:Label></td>
    <td style="WIDTH: 15px"><asp:Button id="Button1" CommandName="Page" CommandArgument="1" runat="server" 
            Text="Go" onclick="Button1_Click"></asp:Button></td>
     <%--<td style="width:40%"></td>  --%>     
 </tr>
</table>
<script type="text/javascript">
            function SetNavigate() {
                var gv=<%=GridView.ClientID %>;
                var ye=<%=ye.ClientID %>;
                var ye2=<%=ye2.ClientID %>;
                var firstpage=<%=LinkButton1.ClientID %>;
                var prevpage=<%=LinkButton2.ClientID %>;
                var nextpage=<%=LinkButton3.ClientID %>;
                var lastpage=<%=LinkButton4.ClientID %>;
                var inputye=document.all.<%=TextBox1.ClientID %>;
                var pagelines =document.all.<%=pagelines.ClientID %>;
                var GOTO =document.all.<%=GOTO.ClientID %>;
                
//                if (document.documentElement.offsetWidth<500)
//                {
//                    ye.style.display='none';
//                    ye2.style.display=ye.style.display;
//                    firstpage.style.display=ye.style.display;
//                    lastpage.style.display=ye.style.display;
//                    pagelines.style.display=ye.style.display;
//                    prevpage.innerText='上页';
//                    nextpage.innerText='下页';
//                    inputye.style.width='12px';
//                    GOTO.innerText='到:';
//                }
//                else
//                {
//                    ye.style.display='block';
//                    ye2.style.display=ye.style.display;
//                    firstpage.style.display=ye.style.display;
//                    lastpage.style.display=ye.style.display;
//                    pagelines.style.display=ye.style.display;
//                    prevpage.innerText='上一页';
//                    nextpage.innerText='下一页';
//                    inputye.style.width='30px';
//                    GOTO.innerText='转到：';
//                }
            }
        </script>
       <script for="window" event="onload" type="text/javascript">
            SetNavigate();
       </script>
