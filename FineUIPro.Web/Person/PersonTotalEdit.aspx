<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonTotalEdit.aspx.cs" Inherits="FineUIPro.Web.Person.PersonTotalEdit" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑人员总结</title>
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
                        <%--AutoPostBack="true" OnSelectedIndexChanged="drpUser_SelectedIndexChanged"--%>
                        <f:DropDownList ID="drpUser" runat="server" Label="人员" EnableEdit="true" ForceSelection="false"
                            Required="true" ShowRedStar="true" LabelWidth="110px" 
                            >
                        </f:DropDownList>
                        <f:DropDownList ID="drpRoleName" runat="server" Label="岗位" LabelWidth="110px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker runat="server" Label="开始日期" ID="txtStartTime" LabelWidth="110px" Readonly="true" Required="true" ShowRedStar="true"></f:DatePicker>
                        <f:DatePicker runat="server" Label="结束日期" ID="txtEndTime" LabelWidth="110px" Readonly="true" Required="true" ShowRedStar="true"></f:DatePicker>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:HtmlEditor runat="server" Label="总结内容" ID="txtContents" ShowLabel="false"
                            Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="400" LabelAlign="Right">
                        </f:HtmlEditor>
                    </Items>
                </f:FormRow>

            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                            OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:Button runat="server" ID="btnDownLoad" Icon="ArrowDown" Text="模板下载" ToolTip="模板下载">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="ButtonClick" />
                                    </Listeners>
                                </f:Button>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" ToolTip="保存" runat="server" ValidateForms="SimpleForm1"
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
    <script type="text/javascript">
        function ButtonClick(event) {
            // 第一个参数 false 用来指定当前不是AJAX请求
            __doPostBack(false, '', 'ButtonClick');
        }
    </script>
</body>
</html>
