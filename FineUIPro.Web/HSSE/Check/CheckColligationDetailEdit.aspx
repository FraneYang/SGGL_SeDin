<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckColligationDetailEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.Check.CheckColligationDetailEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑检查项明细</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckItemType" runat="server" Label="检查类型" Readonly="true" >
                    </f:TextBox>
                </Items>
                <Items>
                   <f:TextBox ID="txtCheckItem" runat="server" Label="检查项" ShowRedStar="true"  FocusOnPageLoad="true" MaxLength="3000">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtUnqualified" runat="server" MaxLength="3000" Label="不合格项描述">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtSuggestions" runat="server" MaxLength="3000" Label="整改要求">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="35% 15% 50%">
                <Items>
                    <f:TextBox ID="txtWorkArea" runat="server" Label="单位工程" MaxLength="3000" ShowRedStar="true" Required="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpWorkArea" runat="server" Label="" AutoPostBack="true" 
                        OnSelectedIndexChanged="drpWorkArea_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="drpUnit" runat="server" Label="责任单位" ShowRedStar="true" Required="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpHandleStep" runat="server" Label="处理措施" ShowRedStar="true" Required="true"
                        AutoPostBack="true" OnSelectedIndexChanged="drpHandleStep_SelectedIndexChanged">
                    </f:DropDownList>
                </Items>
                <Items>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="限时时间" ID="txtLimitedDate"
                        Enabled="false">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpCompleteStatus" runat="server" Label="整改完成情况">
                    </f:DropDownList>
                    <f:Label runat="server" ID="l1" Hidden="true"></f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                     <f:Label ID="Label1" runat="server" Text="说明：单位工程可从下拉框选择也可手动编辑。" CssClass="lab" MarginLeft="5px">
                      </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
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
