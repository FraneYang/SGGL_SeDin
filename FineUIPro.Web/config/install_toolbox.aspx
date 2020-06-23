<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="install_toolbox.aspx.cs" Inherits="FineUIPro.Web.config.install_toolbox" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ol li {
            margin-bottom: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <div>
            安装工具箱
        </div>
        <ol>
            <li>打开 Visual Studio，新建一个 Web 项目； </li>
            <li>打开 Default.aspx 页面；</li>
            <li>打开工具箱，在空白处点击右键选择“添加选项卡”，输入“FineUI”； </li>
            <li>在“FineUI”选项卡内，单击右键选择“选择项...”； </li>
            <li>在弹出的窗口的下面，选择“浏览...”按钮；</li>
            <li>打开“FineUIPro.dll”并确定。 </li>
        </ol>
        <div>
            安装完成后的截图：
            <br />
            <img alt="FineUI toolbox" src="../res/images/toolbox.png" />
        </div>
    </form>
</body>
</html>
