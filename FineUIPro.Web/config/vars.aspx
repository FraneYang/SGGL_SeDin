<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register TagPrefix="f" Namespace="FineUIPro" Assembly="FineUIPro" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>获取服务器参数</title>
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StringBuilder sb = new StringBuilder();
				NameValueCollection vars = Page.Request.ServerVariables;
				sb.Append("<table>");
				foreach (string key in vars.AllKeys)
				{
					sb.Append("<tr>");
					sb.AppendFormat("<td>{0}</td><td>{1}</td>", key, vars[key]);
					sb.Append("</tr>");
				}
				sb.Append("</table>");
				
				Label1.Text = sb.ToString();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"></f:PageManager>
		<f:Label runat="server" ID="Label1" EncodeText="false"></f:Label>
    </form>
</body>
</html>
