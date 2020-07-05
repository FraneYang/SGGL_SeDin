<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponseItem.aspx.cs" Inherits="FineUIPro.Web.HSSE.Hazard.ResponseItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">   
<head runat="server">
    <title>响应</title>
      <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
           <f:FormRow>
                <Items>
                  <f:RadioButtonList ID="rbtnIsResponse" runat="server" Label="是否响应" AutoPostBack="true" OnSelectedIndexChanged="rbtnIsResponse_SelectedIndexChanged">
                  <f:RadioItem Value="True" Text="是" />
                  <f:RadioItem Value="False" Text="否" Selected="true" />
                  </f:RadioButtonList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtResponseRecode" runat="server" Label="响应记录" MaxLength="100">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存数据" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
