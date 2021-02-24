<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestTrainingSave.aspx.cs" Inherits="FineUIPro.Web.HSSE.EduTrain.TestTrainingSave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                   <f:TextBox ID="txtSupTraining" runat="server" Label="上级" Required="true" ShowRedStar="true" MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"></f:TextBox>
                        <f:TextBox ID="txtTrainingCode" runat="server" Label="编号" 
                       Required="true" ShowRedStar="true" FocusOnPageLoad="true" MaxLength="50"></f:TextBox>  
                </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>
                   <f:TextArea ID="txtTrainingName" runat="server" Label="名称" Required="true" ShowRedStar="true" 
                       MaxLength="100" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"></f:TextArea>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                   <f:CheckBox ID="ckIsEndLever" runat="server" Label="末级" Checked="true"></f:CheckBox>
                      <f:CheckBox ID="ckIsOffice" runat="server" Label="公司" Checked="true"></f:CheckBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>                    
                      <f:DropDownList ID="drpMenuType" runat="server"  LabelAlign="Right"  Label="适用系统">                             
                                <f:ListItem  Value="Menu_HSSE" Text="安全"/>
                                <f:ListItem  Value="Menu_CQMS" Text="质量"/>
                    </f:DropDownList>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
