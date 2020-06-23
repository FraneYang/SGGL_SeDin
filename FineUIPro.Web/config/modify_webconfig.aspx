<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modify_webconfig.aspx.cs" Inherits="FineUIPro.Web.config.modify_webconfig" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ol li,
        ul li {
            margin-bottom: 5px;
        }

        pre {
            border: none;
            margin: 0;
            padding: 10px 5px;
            font-family: Consolas, Courier New, monospace;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <div>
            在开发项目之前请配置好 Web.config：
        </div>
        <ol>
            <li><strong>设置 configuration 配置节：</strong>
                <br />
                <pre>
    &lt;configSections&gt;
        &lt;section name="FineUIPro" type="FineUIPro.ConfigSection, FineUIPro"/&gt;
    &lt;/configSections&gt;

    &lt;!-- 可用的配置项（这里列的都是默认值）:
            Theme="Default" 
            Language="zh_CN" 
            DebugMode="false"
            FormMessageTarget="Qtip" 
            FormOffsetRight="0" 
            FormLabelWidth="100" 
            FormLabelSeparator="：" 
            FormLabelAlign="Left" 
            FormRedStarPosition="AfterText"  
            EnableAjax="true" 
            AjaxTimeout="120" 
            EnableAjaxLoading="true" 
            AjaxLoadingType="Default" 
            AjaxLoading 
            ShowAjaxLoadingMaskText=false
            AjaxLoadingMask 
            CustomTheme="" 
            IconBasePath="~/res/icon" 
            CustomThemeBasePath="~/res/themes" 
            JSBasePath="~/res/js"
            IEEdge="true"  
            EnableShim="false"  
            EnableCompactMode="false"
    --&gt;
    &lt;FineUIPro DebugMode="false" /&gt;
            </pre>
                FineUIPro 配置节中的参数：
            <br />
                <ul>
                    <li>Theme: 控件主题，内置 30 种主题（其中 6 种 Metro 主题，24 种 jQueryUI 官方主题，默认值：Default）</li>
                    <li>Language: 控件语言（en/zh_CN/zh_TW，默认值：zh_CN）</li>
                    <li>FormMessageTarget: 表单字段错误提示信息的显示位置（Title/Side/Qtip，默认值：Side）</li>
                    <li>FormLabelWidth: 表单字段标签的宽度（默认值：100px）</li>
                    <li>FormLabelAlign: 表单字段标签的位置（Left/Right/Top，默认值：Left）</li>
                    <li>FormRedStarPosition: 表单字段红色星号的位置（AfterText/BeforeText/AfterSeparator，默认值：AfterText）</li>
                    <li>FormLabelSeparator: 表单字段标签与内容的分隔符（默认值："："）</li>
                    <li>EnableAjax: 是否启用AJAX（默认值：true）</li>
                    <li>AjaxTimeout: Ajax超时时间（单位：秒，默认值：120s）</li>
                    <li>DebugMode: 是否开发模式，启用时格式化输出页面的JavaScript代码，便于调试（默认值：false）</li>
                    <li>EnableAjaxLoading: 是否启用Ajax提示（默认值：true）</li>
                    <li>AjaxLoadingType: Ajax提示类型，默认在页面顶部显示黄色提示框（Default/Mask，默认值：Default）</li>
                    <li>EnableShim: 是否启用遮罩层，防止ActiveX、Flash等对象覆盖弹出窗体（默认值：false）</li>
                    <li>EnableCompactMode: 是否启用紧凑模式（默认值：false）</li>
                </ul>
                <br />
                <br />
            </li>
            <li><strong>设置 system.web 配置节：</strong>
                <pre>
&lt;system.web&gt;
    &lt;pages&gt;
      &lt;controls&gt;
        &lt;add assembly="FineUIPro" namespace="FineUIPro" tagPrefix="f"/&gt;
      &lt;/controls&gt;
    &lt;/pages&gt;
    
    &lt;httpModules&gt;
      &lt;add name="FineUIProScriptModule" type="FineUIPro.ScriptModule, FineUIPro"/&gt;
    &lt;/httpModules&gt;

    &lt;httpHandlers&gt;
      &lt;add verb="GET" path="res.axd" type="FineUIPro.ResourceHandler, FineUIPro" validate="false"/&gt;
    &lt;/httpHandlers&gt;
&lt;system.web&gt;
        </pre>
            </li>
            <li><strong>完成。</strong></li>
        </ol>
        <br />
        <hr />
        <br />
        <div style="font-weight: bold;">
            特别提醒
        </div>
        <br />
        Net4.0以上的项目，一定要为Web.config中&lt;page&gt;标签添加controlRenderingCompatibilityVersion和clientIDMode两个属性。
    <pre>
    &lt;pages controlRenderingCompatibilityVersion="4.0" clientIDMode="AutoID"&gt;
        &lt;controls&gt;
        &lt;add assembly="FineUIPro" namespace="FineUIPro" tagPrefix="f" /&gt;
        &lt;/controls&gt;
    &lt;/pages&gt;
    </pre>
        <br />
        <div style="font-weight: bold;">
            注意引用的Newtonsoft.Json.dll版本
        </div>
        <br />
        FineUIPro.dll只有一个版本，无论你的项目是2.0、3.5、4.0、4.5，都只需要引用同一个FineUIPro.dll即可。
    <br />
        <br />
        Newtonsoft.Json.dll为每个Net版本创建不同的DLL，比如你的项目是基于Net2.0的，就要引用json.net\Net20\Newtonsoft.Json.dll，如果你的项目是基于Net4.0的，就要引用json.net\Net40\Newtonsoft.Json.dll。
    <br />
        <br />
        <br />
        <br />
        <br />
        <div style="font-weight: bold; color: Red;">
            更多常见问题：<a href="http://fineui.com/bbs/forum.php?mod=viewthread&tid=655" target="_blank">http://fineui.com/bbs/forum.php?mod=viewthread&tid=655</a>
        </div>
        <br />
    </form>
</body>
</html>
