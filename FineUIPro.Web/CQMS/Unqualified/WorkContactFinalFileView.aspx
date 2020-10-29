<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkContactFinalFileView.aspx.cs" Inherits="FineUIPro.Web.CQMS.Unqualified.WorkContactFinalFileView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>已定稿文件</title>
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
                        <f:TextBox ID="txtCode" runat="server" Readonly="true" Label="编号" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="drpUnit" runat="server" Readonly="true" Label="提出单位" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtMainSendUnit" runat="server" Readonly="true" Label="主送单位" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtCCUnit" runat="server" Readonly="true" Label="抄送单位" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>

                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtCompileDate" runat="server" Readonly="true" Label="编制日期" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                  <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtIsReply" runat="server" Readonly="true" Label="是否答复" LabelAlign="Right"
                            MaxLength="50">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>

                        <f:TextBox ID="txtCause" runat="server" Required="true" Readonly="true" ShowRedStar="true" Label="事由" LabelAlign="Right"
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

        </f:Form>
    </form>
</body>
<f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
    Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
    Height="500px">
</f:Window>
</html>
