<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeQRImage.aspx.cs" Inherits="FineUIPro.Web.Controls.SeeQRImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>二维码查看</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .userphoto .f-field-label
        {
            margin-top: 0;
        }
        
        .userphoto img
        {
            width: 150px;
            height: 180px;            
        }
        
        .uploadbutton .f-btn
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
         <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false">
            <Items>
                 <f:Panel ID="Panel5" Title="面板1" BoxFlex="3" runat="server" ShowBorder="false" ShowHeader="false" Layout="Fit" >
                    <Items>
                        <f:Image ID="Image1" CssClass="userphoto" ImageUrl="~/res/images/blank_180.png" runat="server"
                            BoxFlex="1" MarginLeft="50px" Width="160px" Height="160px">
                        </f:Image>
                    </Items>
                        <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Center">
                            <Items>                                        
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnReSave" Icon="ArrowRefresh" runat="server" ToolTip="重新生成"  OnClick="btnReSave_Click" >
                                </f:Button>
                                <f:Button ID="btnPrint" Icon="Printer" runat="server" ToolTip="打印"  OnClick="btnPrint_Click" >
                                </f:Button>                            
                            </Items>
                        </f:Toolbar>
                    </Toolbars> 
                </f:Panel>  
            </Items>
      </f:Panel>
      <f:Window ID="Window1" Title="二维码打印" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" 
        Width="600px" Height="400px">
    </f:Window>
    </form>
</body>
</html>
