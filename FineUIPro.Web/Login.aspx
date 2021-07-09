<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="FineUIPro.Web.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <link href="~/res/index/css/index.css" rel="stylesheet" />
    <link href="~/res/index/css/login.css" rel="stylesheet" />
    <link href="~/res/index/css/plugin.css" rel="stylesheet" />
</head>
<body>
    <div id="wrapper" class="dec-login bi-absolute-layout dec-login-fresh">
        <img alt="" class="bi-single bi-img display-block"
            src="./images/login.png"
            style="height: 100%; left: 0px; top: 0px; bottom: 0px; position: absolute; display: none;" />
        <div class="bi-flex-center-adapt-layout login-area"
            style="width: 23%; right: 10%; top: 0px; bottom: 0px; position: absolute;">
            <!-- <div class="bi-absolute-layout edit-area" height: 440px;-->
            <div class="bi-absolute-layout "
                style="width: 100%; height: 420px; position: relative; flex-shrink: 0; margin: 0px;">
                <div class="bi-horizon-auto-layout"
                    style="height: 100px; left: 0px; right: 0px; top: 30px; position: absolute;">
                    <div class="bi-flex-center-adapt-layout" style="position: relative; margin: 0px auto;">
                        <img
                            class="bi-single bi-img display-block"
                            src="res/index/images/logo.png"
                            style="width: auto; height: 50px; position: relative; flex-shrink: 0; margin: 0px;">
                    </div>
                    <!-- <div class="bi-single bi-label dec-login-visual-title bi-text"
            style="height: 24px; line-height: 24px; text-align: center; white-space: nowrap; text-overflow: ellipsis; position: relative; margin: 5px auto 0px;">
            管理系统</div> -->
                </div>
                <div class="dec-login-index bi-tab bi-v-tape-layout edit-area"
                    style="left: 0px; right: 0px; top: 85px; bottom: 0px; position: absolute;">
                    <div class="bi-card-layout" style="position: absolute; left: 0px; right: 0px; top: 0px; bottom: 0px;">
                        <div class="dec-login-login bi-absolute-layout"
                            style="position: relative; top: 0px; left: 0px; width: 100%; height: 100%;">
                            <div class="bi-vertical-layout"
                                style="overflow: hidden auto; left: 40px; right: 40px; top: 0px; bottom: 0px; position: absolute;">
                                <div class="bi-vertical-layout" style="overflow: hidden auto; position: relative; margin-top: 50px;">
                                    <div class="dec-login-item bi-vertical-layout"
                                        style="overflow: hidden auto; position: relative; margin-bottom: 10px;">
                                        <div class="bi-h-tape-layout" style="height: 36px; position: relative;">
                                            <div
                                                class="bi-flex-vertical-center-adapt-layout item-icon login-username-font bi-flex-horizontal-layout v-middle h-left"
                                                style="width: 26px; position: absolute; top: 0px; bottom: 0px; left: 0px;">
                                                <div class="bi-flex-center-adapt-layout" style="position: relative; flex-shrink: 0;">
                                                    <i
                                                        class="bi-single x-icon b-font horizon-center display-block"
                                                        style="position: relative; flex-shrink: 0; margin: 0px;"></i>
                                                </div>
                                            </div>
                                            <div class="bi-absolute-layout"
                                                style="position: absolute; top: 0px; bottom: 0px; left: 30px; right: 0px;">
                                                <input type="text" id="user"
                                                    placeholder="用户名" class="bi-single bi-input display-block overflow-dot" autocomplete="off"
                                                    style="left: 0px; right: 0px; top: 0px; bottom: 0px; position: absolute; font-size: 16px;" />
                                            </div>
                                        </div>
                                        <div class="bi-single bi-label bi-border-top bi-text"
                                            style="height: 24px; line-height: 24px; text-align: center; white-space: nowrap; text-overflow: ellipsis; position: relative;">
                                        </div>
                                        <div class="bi-single bi-label bi-border-top error bi-text"
                                            style="height: 24px; display: none; line-height: 24px; text-align: left; white-space: nowrap; text-overflow: ellipsis; position: relative;">
                                        </div>
                                    </div>
                                    <div class="dec-login-item bi-vertical-layout"
                                        style="display: none; overflow: hidden auto; position: relative; margin-bottom: 10px;">
                                    </div>
                                </div>
                                <div class="bi-vertical-layout" style="overflow: hidden auto; position: relative;">
                                    <div class="dec-login-item bi-vertical-layout"
                                        style="overflow: hidden auto; position: relative; margin-bottom: 10px;">
                                        <div class="bi-h-tape-layout" style="height: 36px; position: relative;">
                                            <div
                                                class="bi-flex-vertical-center-adapt-layout item-icon login-password-font bi-flex-horizontal-layout v-middle h-left"
                                                style="width: 26px; position: absolute; top: 0px; bottom: 0px; left: 0px;">
                                                <div class="bi-flex-center-adapt-layout" style="position: relative; flex-shrink: 0;">
                                                    <i
                                                        class="bi-single x-icon b-font horizon-center display-block"
                                                        style="position: relative; flex-shrink: 0; margin: 0px;"></i>
                                                </div>
                                            </div>
                                            <div class="bi-absolute-layout"
                                                style="position: absolute; top: 0px; bottom: 0px; left: 30px; right: 0px;">
                                                <input id="pwd" autocomplete="off"
                                                    type="password" placeholder="密码" class="bi-single bi-input display-block overflow-dot"
                                                    style="left: 0px; right: 0px; top: 0px; bottom: 0px; position: absolute; font-size: 16px;" />
                                            </div>
                                        </div>
                                        <div class="bi-single bi-label bi-border-top bi-text"
                                            style="height: 24px; line-height: 24px; text-align: center; white-space: nowrap; text-overflow: ellipsis; position: relative;">
                                        </div>
                                        <div class="bi-single bi-label bi-border-top error bi-text"
                                            style="height: 24px; display: none; line-height: 24px; text-align: left; white-space: nowrap; text-overflow: ellipsis; position: relative;">
                                        </div>
                                    </div>
                                    <div class="dec-login-item bi-vertical-layout"
                                        style="display: none; overflow: hidden auto; position: relative; margin-bottom: 10px;">
                                    </div>
                                </div>
                                <div class="bi-horizon-auto-layout" style="position: relative;">
                                    <div class="dec-login-slider bi-absolute-layout"
                                        style="display: none; width: 190px; height: 26px; position: relative; margin: 0px auto;">
                                    </div>
                                </div>
                                <div class="bi-single bi-label login-error bi-text"
                                    style="height: 20px; display: none; line-height: 20px; text-align: left; white-space: nowrap; text-overflow: ellipsis; position: relative;">
                                </div>
                                <div class="bi-left-right-vertical-adapt-layout bi-float-left-layout clearfix bi-float-right-layout"
                                    style="position: relative; margin-bottom: 30px;">
                                    <div id='activewrap' class="bi-flex-vertical-center-adapt-layout bi-flex-horizontal-layout v-middle h-left active"
                                        style="height: 100%; position: relative; float: left;">
                                        <div
                                            class="bi-single bi-basic-button cursor-pointer bi-multi-select-item bi-flex-vertical-center-adapt-layout bi-flex-horizontal-layout v-middle h-left"
                                            style="height: 16px; position: relative; flex-shrink: 0; margin-left: -5px;">
                                            <div class="bi-flex-center-adapt-layout" style="width: 26px; position: relative; flex-shrink: 0;">
                                                <div id='active' class="bi-single bi-basic-button cursor-pointer bi-checkbox bi-flex-center-adapt-layout active"
                                                    style="width: 16px; height: 16px; position: relative; flex-shrink: 0; margin: 0px;">
                                                    <div class="checkbox-content"
                                                        style="width: 14px; height: 14px; position: relative; flex-shrink: 0; margin: 0px;">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="bi-single bi-label list-item-text bi-text"
                                                style="height: 16px; line-height: 16px; text-align: left; white-space: nowrap; text-overflow: ellipsis; position: relative; flex-shrink: 0;">
                                                一周内自动登录
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bi-flex-vertical-center-adapt-layout bi-flex-horizontal-layout v-middle h-left"
                                        style="height: 100%; position: relative; float: right;">
                                        <div
                                            class="bi-single bi-basic-button cursor-pointer bi-button button-common bi-label bi-flex-center-adapt-layout clear"
                                            style="height: 16px; line-height: 16px; position: relative; flex-shrink: 0;">
                                            忘记密码？
                                        </div>
                                    </div>
                                </div>
                                <div class="bi-horizon-auto-layout" style="position: relative;">
                                    <div
                                        class="bi-single bi-basic-button cursor-pointer bi-button login-button button-common bi-label bi-flex-center-adapt-layout"
                                        style="width: 190px; height: 40px; line-height: 38px; min-width: 80px; position: relative; margin: 0px auto;">
                                        <div id="login" class="bi-single bi-text"
                                            style="padding-left: 10px; padding-right: 10px; max-width: 100%; text-align: left; white-space: nowrap; text-overflow: ellipsis; position: relative; flex-shrink: 0; margin: 0px;">
                                            登录
                                        </div>
                                    </div>
                                </div>
                                <div class="bi-single bi-label list-item-text bi-text"
                                    style="height: 16px; line-height: 16px; text-align: center; white-space: nowrap; text-overflow: ellipsis; position: relative; flex-shrink: 0;">
                                    (账号：工号；初始密码：123)
                                </div>
                                <div id="error" class="bi-vertical-layout login-error"
                                    style="display: none; position: relative; margin-top: 20px;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
<script type="text/javascript" src="res/index/js/jquery-3.4.1.min.js"></script>
<script type="text/javascript">
    var $activeWrap = document.getElementById('activewrap')
    var $active = document.getElementById('active')
    $activeWrap.onclick = function () {
        if ($activeWrap.className.indexOf('active') !== -1) {
            $activeWrap.classList.remove("active")
            $active.classList.remove("active")
        } else {
            $activeWrap.classList.add("active")
            $active.classList.add("active")
        }
    }
</script>
<script type="text/javascript">
    $(document).ready(function () {
        var user = getCookie("u") || "";
        var pwd = getCookie("p") || "";
        $("#user").val(user)
        $("#pwd").val(pwd)
    })
    $("#login").click(function () {
        login()
    })
    function login() {
        var user = $("#user").val()
        var pwd = $("#pwd").val()
        $.ajax({
            url: "Login.aspx/LoginPost",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                user: user,
                pwd: pwd
            }),
            success: function (data) {
                if (data.d == '' || data.d == undefined) {
                    alert("账号密码不正确！");
                } else {
                    var $activeWrap = document.getElementById('activewrap')
                    if ($activeWrap.className.indexOf('active') !== -1) {
                        setCookie("u", user);
                        setCookie("p", pwd);
                    } else {
                        setCookie("u", '');
                        setCookie("p", '');
                    }
                    top.window.location.href = data.d;
                }
            }
        })
    }

    function setCookie(name, value) {
        var Days = 7;
        var exp = new Date();
        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    }
    function getCookie(name) {
        var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
        if (arr = document.cookie.match(reg))
            return unescape(arr[2]);
        else
            return null;
    }
</script>
</html>
