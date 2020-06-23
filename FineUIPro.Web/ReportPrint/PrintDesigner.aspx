<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDesigner.aspx.cs"
    Inherits="Web.ReportPrint.PrintDesigner" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpPrintReport" runat="server" Label="报表" EnableEdit="true" ForceSelection="false"
                        Required="true" ShowRedStar="true" OnSelectedIndexChanged="drpPrintReport_SelectedIndexChanged" AutoPostBack="true">
                    </f:DropDownList> 
                    <f:Label runat="server"></f:Label>
                </Items>
            </f:FormRow>           
        </Rows>        
    </f:Form>
    </form>
</body>
</html>
