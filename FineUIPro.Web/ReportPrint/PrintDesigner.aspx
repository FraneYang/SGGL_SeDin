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
            <f:FormRow ColumnWidths="20% 10% 40% 10% 20%">
                <Items>
                     <f:Label runat="server" ></f:Label>
                    <f:DropDownList ID="drpReportModule" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="drpReportModule_SelectedIndexChanged">
                        <f:ListItem  Text="焊接报表" Value="1" />
                    </f:DropDownList>
                    <f:DropDownList ID="drpPrintReport" runat="server"  Label="报表" EnableEdit="true" LabelAlign="Left" >
                    </f:DropDownList> 
                    <f:Button ID="btnDesigner" Text="模板设计" ToolTip="报表模板设计" Icon="ApplicationEdit" runat="server"
                       OnClick="btnDesigner_Click">
                            </f:Button>
                    <f:Label runat="server" ></f:Label>
                </Items>
            </f:FormRow>           
        </Rows>        
    </f:Form>
    </form>
</body>
</html>
