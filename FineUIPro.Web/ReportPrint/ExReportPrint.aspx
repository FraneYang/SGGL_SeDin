<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExReportPrint.aspx.cs" Inherits="Web.ReportPrint.ExReportPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>报表打印</title>
    <style type="text/css">
		A:link    {text-decoration:none; color:"black"; font-size:10pt}
		A:visited {text-decoration:none; color:"black"; font-size:10pt}
		A:active  {text-decoration:none; color:"black"; font-size:10pt}
		A:hover   {text-decoration:none; color:"#FF0000"; font-size:10pt}
	</style>
    <script src="js/Common.js" type="text/jscript"></script>
</head>

<script type="text/javascript">
    window.onload = function () {
        setInit('ReadExReportFile.aspx?reportId='+<%=reportId %>);
        ReplaceParameter('<%=replaceParameter %>');
        CalculateTab('<%=reportId %>');
//        SetCellValue();
        SetCellValUseVarName('<%=varValue %>')
     }

</script>

<body>
<form id ="form1" runat="server">
        <table style="width: 100%;" border="0">
            <tr>
                <td>
					<p>
						<img alt="" style="vertical-align:middle" src="images/printsetup.gif" width="16" height="16"/><a href="#" onclick="PrintSetup()" >打印设置</a> | 
						<img alt="" style="vertical-align:middle" src="images/print.gif" width="16" height="16"/><a href="#" onclick="FilePrint()" >打印</a> | 
						<img alt="" style="vertical-align:middle" src="images/printpreview.gif" width="16" height="16"/><a href="#" onclick="Preview()" >打印预览</a> | 
						<img alt="" style="vertical-align:middle" src="images/export.gif" width="16" height="16"/><a  href="#" onclick="onFileSave()" >输出</a> |                
					</p>
				</td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <object id="ChinaExcel" style="left: 0px; top: 0px; width: 100%; height:500px;text-align:center;" classid="CLSID:15261F9B-22CC-4692-9089-0C40ACBDFDD8"
                        name="ChinaExcel" CODEBASE="../downloads/chinaexcelweb.cab#version=3,8,9,2">
                    </object>
                    
                </td>
            </tr>
        </table>
        &nbsp;
    </form>
</body>
</html>
