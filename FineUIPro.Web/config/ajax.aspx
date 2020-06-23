<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajax.aspx.cs" Inherits="FineUIPro.Web.ajax" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ul.mainlist {
            list-style-type: none;
            margin: 0;
            padding: 0;
        }

            ul.mainlist > li {
                display: inline-block;
                width: 250px;
                margin: 0 10px 10px 0;
                vertical-align: top;
            }

                ul.mainlist > li .ui-widget {
                    border-style: solid;
                    border-width: 1px;
                }

                ul.mainlist > li .ui-widget-header {
                    padding: 5px 10px;
                    border-bottom-width: 1px;
                    border-bottom-style: solid;
                }

                ul.mainlist > li .ajaxlist-container {
                    height: 220px;
                    overflow-y: auto;
                }

        ul.ajaxlist {
            list-style-type: none;
            margin: 5px 10px;
            padding: 0;
        }

            ul.ajaxlist > li {
                margin-bottom: 5px;
            }

        .mysearch {
            text-align: left;
        }

            .mysearch .f-field-textbox {
                height: 46px;
                font-size: 24px;
                line-height: 28px;
                padding: 8px 12px;
            }
    </style>
</head>
<body class="f-body">
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <div class="mysearch">
            <f:TextBox ID="tbxSearch" ShowLabel="false" EmptyText="搜索类名" runat="server" Width="350px">
                <Listeners>
                    <f:Listener Event="change" Handler="onSearchBoxChange" />
                </Listeners>
            </f:TextBox>
        </div>
        <br />
        <asp:Literal ID="litResult" EnableViewState="false" runat="server"></asp:Literal>
        <%--<br />
        FineUIPro 有原生的 AJAX 支持，也就是说不需要做任何配置，控件属性在服务器端的改变都能以 AJAX 的方式影响到前端界面的显示。
        <br />
        但是，并非控件的所有属性都支持 AJAX 改变，下面列表展示了每个控件有哪些属性支持 AJAX 改变。--%>
        <br />
        <br />
    </form>
    <script>


        function onSearchBoxChange(event) {
            var keyword = this.getValue().toLowerCase();

            $('ul.mainlist > li').each(function () {
                var cnode = $(this), title = cnode.find('.ui-widget-header').text().toLowerCase();

                if (title.indexOf(keyword) >= 0) {
                    cnode.show();
                } else {
                    cnode.hide();
                }
            });
        }


        
    </script>
</body>
</html>
