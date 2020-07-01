<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LawRegulationListEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Law.LawRegulationListEdit" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑法律法规</title>
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
                    <f:TextBox ID="txtLawRegulationCode" runat="server" FocusOnPageLoad="true" Label="编号"
                        Required="true" ShowRedStar="true" MaxLength="50">
                    </f:TextBox>
                    <f:DropDownList ID="ddlLawsRegulationsTypeId" runat="server" Height="22px" Label="类别">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtLawRegulationName" runat="server" Label="名称" Required="true" ShowRedStar="true"
                        MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="批准日" EmptyText="请选择批准日"
                        ID="dpkApprovalDate">
                    </f:DatePicker>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="生效日" ID="dpkEffectiveDate"
                        EmptyText="请选择生效日" LabelWidth="120px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtDescription" runat="server" Height="100px" Label="简介及重点关注条款" MaxLength="1000">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="txtCompileMan" runat="server" Label="整理人">
                    </f:Label>
                    <f:Label ID="txtCompileDate" runat="server" Label="整理时间">
                    </f:Label>
                </Items>
            </f:FormRow>           
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnUploadResources_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>                   
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
      <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="true" runat="server"
            IsModal="true" Width="700px" Height="500px">
       </f:Window>
    </form>
</body>
</html>
