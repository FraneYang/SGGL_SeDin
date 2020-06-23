<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="player.aspx.cs" Inherits="FineUIPro.Web.AttachFile.player" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="dist/skin/blue.monday/css/jplayer.blue.monday.min.css" rel="stylesheet" />
   <script type="text/javascript" src="lib/jquery.min.js"></script>
   <script type="text/javascript" src="dist/jplayer/jquery.jplayer.min.js"></script>
   <script type="text/javascript">
       var videoUrl = '<%=videoUrl %>';
       var videoTitle = '<%=videoTitle %>';
   </script>
   <script type="text/javascript">
//<![CDATA[
       function myBrowser() {
           var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
           var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器
           var isIE = userAgent.indexOf("compatible") > -1
            && userAgent.indexOf("MSIE") > -1 && !isOpera; //判断是否IE浏览器
           var isEdge = userAgent.indexOf("Edge") > -1; //判断是否IE的Edge浏览器
           var isFF = userAgent.indexOf("Firefox") > -1; //判断是否Firefox浏览器
           var isSafari = userAgent.indexOf("Safari") > -1
            && userAgent.indexOf("Chrome") == -1; //判断是否Safari浏览器
           var isChrome = userAgent.indexOf("Chrome") > -1
            && userAgent.indexOf("Safari") > -1; //判断Chrome浏览器

           if (isIE) {
               var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
               reIE.test(userAgent);
               var fIEVersion = parseFloat(RegExp["$1"]);
               if (fIEVersion == 7) {
                   return "IE7";
               } else if (fIEVersion == 8) {
                   return "IE8";
               } else if (fIEVersion == 9) {
                   return "IE9";
               } else if (fIEVersion == 10) {
                   return "IE10";
               } else if (fIEVersion == 11) {
                   return "IE11";
               } else {
                   return "0";
               } //IE版本过低
               return "IE";
           }
           if (isOpera) {
               return "Opera";
           }
           if (isEdge) {
               return "Edge";
           }
           if (isFF) {
               return "FF";
           }
           if (isSafari) {
               return "Safari";
           }
           if (isChrome) {
               return "Chrome";
           }

       }
       var varSolution = 'html';
       //var browser = myBrowser();
       //if (browser.substr(0, 2) == 'IE') {
       //    if (videoUrl.indexOf('.mp4') != -1) {
       //        varSolution = 'flash';
       //    }
       //}

       $(document).ready(function () {

           $("#jquery_jplayer_1").jPlayer({
               ready: function () {
                   $(this).jPlayer("setMedia", {
                       title: videoTitle,
                       m4v: videoUrl,
                       mp4: videoUrl
                   }).jPlayer("play") ;
               },
               swfPath: "dist/jplayer",
               supplied: "m4v,mp4",
               size: {
                   width: "640px",
                   height: "360px",
                   cssClass: "jp-video-360p"
               },
               solution: varSolution,
               useStateClassSkin: true,
               autoBlur: false,
               smoothPlayBar: true,
               keyEnabled: true,
               remainingDuration: true,
               toggleDuration: true
           });
       });
//]]>
</script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="jp_container_1" class="jp-video jp-video-360p" role="application" aria-label="media player">
	<div class="jp-type-single">
		<div id="jquery_jplayer_1" class="jp-jplayer"></div>
		<div class="jp-gui">
			<div class="jp-video-play">
				<button class="jp-video-play-icon" role="button" tabindex="0">play</button>
			</div>
			<div class="jp-interface">
				<div class="jp-progress">
					<div class="jp-seek-bar">
						<div class="jp-play-bar"></div>
					</div>
				</div>
				<div class="jp-current-time" role="timer" aria-label="time">&nbsp;</div>
				<div class="jp-duration" role="timer" aria-label="duration">&nbsp;</div>
				<div class="jp-controls-holder">
					<div class="jp-controls">
						<button class="jp-play" role="button" tabindex="0">play</button>
						<button class="jp-stop" role="button" tabindex="0">stop</button>
					</div>
					<div class="jp-volume-controls">
						<button class="jp-mute" role="button" tabindex="0">mute</button>
						<button class="jp-volume-max" role="button" tabindex="0">max volume</button>
						<div class="jp-volume-bar">
							<div class="jp-volume-bar-value"></div>
						</div>
					</div>
					<div class="jp-toggles">
						<button class="jp-repeat" role="button" tabindex="0">repeat</button>
						<button class="jp-full-screen" role="button" tabindex="0">full screen</button>
					</div>
				</div>
				<div class="jp-details">
					<div class="jp-title" aria-label="title">&nbsp;</div>
				</div>
			</div>
		</div>
		<div class="jp-no-solution">
			<span>需要更新</span>
			需要下载 <a href="http://get.adobe.com/flashplayer/" target="_blank">Flash plugin</a>.
		</div>
	</div>
</div>

    </div>
    </form>
</body>
</html>
