<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LargerHazardEdit.aspx.cs" ValidateRequest="false" Inherits="FineUIPro.Web.HSSE.Solution.LargerHazardEdit" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑危险性较大的工程清单</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtLargerHazardCode" runat="server" Label="文件编号" Readonly="true" MaxLength="50" Required="true" ShowRedStar="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpHazardType" runat="server" Label="类型"  Required="true" ShowRedStar="true">
                    </f:DropDownList>
                       <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="预计施工" ID="txtExpectedTime" Required="true" ShowRedStar="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                        <f:RadioButtonList ID="rblIsArgument" runat="server" Label="专家论证" LabelWidth="100px"  Required="true" ShowRedStar="true">
                    </f:RadioButtonList>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="编制时间" ID="txtRecordTime">
                    </f:DatePicker>
                    <f:TextBox ID="txtAddress" runat="server" Label="地点" MaxLength="50" >
                    </f:TextBox>                 
                </Items>
                </f:FormRow>
            <f:FormRow>
                <Items>                   
                    <f:TextArea ID="txtDescriptions" runat="server" Label="描述" 
                        LabelAlign="Right" MaxLength="500" Height="100px"></f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>                   
                    <f:HtmlEditor runat="server" Label="" ID="txtRemark" ShowLabel="false" Hidden="true"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="230px" LabelAlign="Right">
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
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
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
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
