<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstructSolutionEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Solution.ConstructSolutionEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑施工方案/审查</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="施工方案/审查" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtConstructSolutionCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50">
                    </f:TextBox>
                    <f:DropDownList ID="drpInvestigateType" runat="server" Label="审查类型" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true" FocusOnPageLoad="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpSolutinType" runat="server" Label="方案类别" LabelAlign="Right"
                        EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="drpSolutinType_SelectedIndexChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="50% 45% 5%">
                <Items>
                    <f:TextBox ID="txtConstructSolutionName" runat="server" Label="文件名" LabelAlign="Right"
                        MaxLength="150">
                    </f:TextBox>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                    <f:Button ID="btnSeeLaw" ToolTip="查看对应法律法规标准规范" Icon="Find" runat="server" OnClick="btnSeeLaw_Click"
                        ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCompileManName" runat="server" Label="编制人" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtCompileDate" runat="server" Label="编制时间" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="500"
                        Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="方案模板" ID="txtFileContents" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="220" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="对应标准规范/法律法规" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="900px"
        Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
