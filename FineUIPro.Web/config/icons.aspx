<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="icons.aspx.cs" Inherits="FineUIPro.Web.icons" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ul.icons {
            list-style-type: none;
            padding: 0;
            margin: 0;
        }

            ul.icons li {
                display: inline-block;
                margin: 0 10px 10px 0;
                text-align: center;
                border-style: solid;
                border-width: 1px;
                padding: 10px 5px;
                width: 150px;
            }

                ul.icons li img {
                    width: 16px;
                    height: 16px;
                }

                ul.icons li .title {
                    margin-top: 10px;
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
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <div class="mysearch">
            <f:TextBox ID="tbxSearch" ShowLabel="false" EmptyText="搜索图标" runat="server" Width="350px">
                <Listeners>
                    <f:Listener Event="change" Handler="onSearchBoxChange" />
                </Listeners>
            </f:TextBox>
        </div>
        <br />
        <asp:Literal EnableViewState="false" runat="server" ID="litIcons"></asp:Literal>
    </form>
    <script>


        function onSearchBoxChange(event) {
            var keyword = this.getValue().toLowerCase();

            $('ul.icons li').each(function () {
                var cnode = $(this), title = cnode.find('.title').text().toLowerCase();

                if (title.indexOf(keyword) >= 0) {
                    cnode.show();
                } else {
                    cnode.hide();
                }
            });
        }


        F.ready(function () {
            $('ul.icons li').hover(function () {
                $(this).addClass('ui-state-hover');
            }, function () {
                $(this).removeClass('ui-state-hover');
            });
        });

    </script>
</body>
</html>
