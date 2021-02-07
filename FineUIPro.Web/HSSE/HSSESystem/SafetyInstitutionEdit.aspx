<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyInstitutionEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.HSSESystem.SafetyInstitutionEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑安全制度</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="安全制度" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpReleaseStates" runat="server" Label="状态" Required="true" ShowRedStar="true">
                        </f:DropDownList>
                        <f:TextBox ID="txtName" runat="server" Label="名称" Required="true" ShowRedStar="true"
                            MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtCode" runat="server" Label="编号" Required="true" ShowRedStar="true"
                            MaxLength="50">
                        </f:TextBox>
                        <f:DropDownList ID="ddlManageRuleTypeId" runat="server" Label="类型">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtReleaseUnit" runat="server" Label="发布机构" MaxLength="500">
                        </f:TextBox>
                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="发布日期" EmptyText="请选择发布日期"
                            ID="dpkApprovalDate">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="生效日期" ID="dpkEffectiveDate"
                            EmptyText="请选择生效日期">
                        </f:DatePicker>
                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="废止日期" EmptyText="请选择废止日期"
                            ID="txtAbolitionDate">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtReplaceInfo" runat="server" Label="替换内容" MaxLength="1000">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtDescription" runat="server" Label="简介及重点关注条款" MaxLength="1000">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpIndexesIds" runat="server" Label="索引"
                            EnableEdit="true" EnableMultiSelect="true" EnableCheckBoxSelect="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtCompileMan" runat="server" Label="上传人" MaxLength="50">
                        </f:TextBox>
                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="上传时间" EmptyText="请选择上传时间"
                            ID="txtCompileDate">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server" Margin="0 50 30 50">
                    <Items>
                        <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                            OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                        </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>
