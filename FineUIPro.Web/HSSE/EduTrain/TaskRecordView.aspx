<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskRecordView.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TaskRecordView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
   <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" AjaxAspnetControls="divPictureUrl,divBeImageUrl,divAttachUrl" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="40% 40% 20%">
                <Items>
                    <f:TextBox ID="txtTrainingEduItemCode" runat="server" Label="编号" Readonly="true">
                    </f:TextBox>
                     <f:TextBox ID="txtTrainingEduItemName" runat="server" Label="名称" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <%--<f:FormRow>
                 <Items>
                    <f:TextBox runat="server" ID="txtInstallationNames" Label="适合岗位</br>(装置/科室)" Readonly="true"></f:TextBox>                 
                </Items> 
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtSummary" runat="server" Label="内容" Height="72px" Readonly="true"></f:TextArea>
                </Items>
            </f:FormRow>--%>            
            <%--<f:FormRow>
                <Items>                 
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="图片">
                        <table>
                            <tr style="height: 25px">
                                <td align="left">
                                    <div id="divPictureUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                             <tr style="height:130px" runat="server" id="trImageUrl" visible="false">
                                <td style="text-align:left">
                                    <div id="divBeImageUrl" runat="server">
                                    </div>
                                </td>                               
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>--%>  
            <f:FormRow>
                <Items>                  
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="附件">
                        <table>
                            <tr style="height: 25px">
                                <td align="left">
                                    <div id="divAttachUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>  
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="Button1" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrlC_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>                  
                    <f:Button ID="btnClose" EnablePostBack="false" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
