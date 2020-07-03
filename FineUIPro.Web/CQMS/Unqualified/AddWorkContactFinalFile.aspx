<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWorkContactFinalFile.aspx.cs" Inherits="FineUIPro.Web.CQMS.Unqualified.AddWorkContactFinalFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>工作联系单</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-row.red {
            background-color: #FF7575;
            background-image: none;
        }

        .fontred {
            color: #FF7575;
            background-image: none;
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
                        <f:TextBox ID="txtCode" runat="server" Required="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpUnit" ShowRedStar="true" runat="server" Required="true" EmptyText="--请选择--" AutoSelectFirstItem="false" Label="提出单位" LabelAlign="Right" EnableEdit="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownBox runat="server" Label="主送单位" ID="txtMainSendUnit" ShowRedStar="true" Required="true" EmptyText="--请选择--" DataControlID="txtCNProfessional" EnableMultiSelect="true" MatchFieldWidth="true">
                            <PopPanel>
                                <f:Grid ID="gvMainSendUnit" BoxFlex="1"
                                    DataIDField="UnitId" DataTextField="UnitName" EnableMultiSelect="true" KeepCurrentSelection="true"
                                    ShowBorder="true" ShowHeader="false" ForceFit="true"
                                    runat="server" EnableCheckBoxSelect="true" DataKeyNames="UnitId" Hidden="true">
                                    <Columns>
                                        <%--<f:RowNumberField />--%>
                                        <f:BoundField Width="100px" DataField="UnitId" SortField="UnitId" DataFormatString="{0}" Hidden="true" />
                                        <f:BoundField Width="100px" DataField="UnitName" SortField="UnitName" DataFormatString="{0}"
                                            HeaderText="名称" />
                                    </Columns>
                                </f:Grid>
                            </PopPanel>
                        </f:DropDownBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownBox runat="server" Label="抄送单位" ID="txtCCUnit" EmptyText="--请选择--" DataControlID="txtCCUnit" EnableMultiSelect="true" MatchFieldWidth="true">
                            <PopPanel>
                                <f:Grid ID="gvCCUnit" BoxFlex="1"
                                    DataIDField="UnitId" DataTextField="UnitName" EnableMultiSelect="true" KeepCurrentSelection="true"
                                    ShowBorder="true" ShowHeader="false" ForceFit="true"
                                    runat="server" EnableCheckBoxSelect="true" DataKeyNames="UnitId" Hidden="true">
                                    <Columns>
                                        <%--<f:RowNumberField />--%>
                                        <f:BoundField Width="100px" DataField="UnitId" SortField="UnitId" DataFormatString="{0}" Hidden="true" />
                                        <f:BoundField Width="100px" DataField="UnitName" SortField="UnitName" DataFormatString="{0}"
                                            HeaderText="名称" />
                                    </Columns>
                                </f:Grid>
                            </PopPanel>
                        </f:DropDownBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtCompileDate" ShowRedStar="true" runat="server" Label="编制日期" Required="true" LabelAlign="Right"
                            EnableEdit="true">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                         <f:RadioButtonList ID="rblIsReply" Label="答复" runat="server" ShowRedStar="true" Required="true">
                                                <f:RadioItem Text="需要回复" Value="1" Selected="true" />
                                                <f:RadioItem Text="不需回复" Value="2" />
                                            </f:RadioButtonList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>

                        <f:TextBox ID="txtCause" runat="server" Required="true" ShowRedStar="true" Label="事由" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Panel ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                            <Items>
                                <f:Label runat="server" ID="lbfile" Text="附件：" CssStyle="padding-left:52px" Width="100px" CssClass="marginr" ShowLabel="false"></f:Label>
                                <f:Button ID="imgBtnFile" Text="附件" ToolTip="上传及查看" Icon="TableCell" runat="server" OnClick="imgBtnFile_Click">
                                </f:Button>
                            </Items>
                        </f:Panel>

                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" OnClick="btnSave_Click" ValidateForms="SimpleForm1">
                        </f:Button>

                        <%--  <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" OnClick="btnClose_Click" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
<f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
    Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
    Height="500px">
</f:Window>
</html>
