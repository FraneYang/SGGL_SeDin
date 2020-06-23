<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientJs.ascx.cs" Inherits="Web.Controls.ClientJs.ClientJs" %>

<script language="javascript" type="text/javascript">

function fPopUpCalendarDlg(ctrlobj)  //日期速查
{
	showx = event.screenX - event.offsetX - 4 ; // + deltaX;
	showy = event.screenY - event.offsetY + 18; // + deltaY;
	newWINwidth = 210 + 4 + 18;
	retval = window.showModalDialog("../Controls/ClientJs/CalendarDlg.htm", "", "dialogWidth:190px; dialogHeight:210px; dialogLeft:" + showx + "px; dialogTop:" + showy + "px; status:no; directories:yes;help:no;scrollbars:no;Resizable=no; ");
	if( retval != null ){
		ctrlobj.value = retval;
	}else{
		//alert("canceled");
	}
	ctrlobj.blur();
}  

</script>


