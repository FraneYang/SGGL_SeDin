<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckItemDetailEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.CheckItemDetailEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                    <f:TextArea ID="txtCheckContent" runat="server" Label="检查内容"  LabelWidth="90px" Required="true" ShowRedStar="true" FocusOnPageLoad="true">
                    </f:TextArea> 
                </Items>             
            </f:FormRow>
             <f:FormRow>
                <Items>
                   <f:NumberBox ID="txtSortIndex" runat="server" Label="排列序号" LabelWidth="90px"></f:NumberBox>
                </Items>
            </f:FormRow>       
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
