<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowUpFile.aspx.cs" Inherits="FineUIPro.Web.common.ShowUpFile" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>请点击下方保存下载附件到本地</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>            
            <f:FormRow>
                <Items>        
                    <f:HiddenField ID="hdFileUrl" runat="server"></f:HiddenField>                      
                 </Items>
               </f:FormRow>
            </Rows>
    </f:Form>
    </form>
</body>
</html>
