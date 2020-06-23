<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOff.aspx.cs" Inherits="FineUIPro.Web.LogOff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<HTML>
	<HEAD id="head1" runat="server">
		<title>退出系统</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			您已经安全地退出系统！
			<asp:HyperLink id="HyperLink1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 56px"
				runat="server">重新登录</asp:HyperLink>
		</form>
	</body>
</HTML>