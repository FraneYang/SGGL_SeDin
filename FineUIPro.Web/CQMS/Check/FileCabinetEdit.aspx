<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileCabinetEdit.aspx.cs" Inherits="FineUIPro.Web.CQMS.Check.FileCabinetEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>重要文件</title>
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
                        <f:ContentPanel ID="ContentPanel2" Title="重要文件" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtFileCode" runat="server" Required="true" ShowRedStar="true" Label="编号" LabelAlign="Right"
                                                MaxLength="50">
                                            </f:TextBox>
                                            <f:DatePicker ID="txtFileDate" ShowRedStar="true" Required="true" runat="server" Label="时间" LabelAlign="Right"
                                                EnableEdit="true">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtFileContent" ShowRedStar="true" Required="true" runat="server" Label="事由" MaxLength="3000">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Panel ID="plfile" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                <Items>
                                                    <f:Panel ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                                                        <Items>
                                                            <f:Label runat="server" Text="附件：" ShowRedStar="true"  CssStyle="padding-left:53px" Width="110px" CssClass="marginr" ShowLabel="false"></f:Label>
                                                            <f:Button ID="imgBtnFile" Text="附件" ToolTip="上传及查看" OnClick="imgBtnFile_Click" Icon="TableCell" runat="server">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Panel>
                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </f:ContentPanel>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:HiddenField ID="HFileCabinetId" runat="server"></f:HiddenField>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存"  OnClick="btnSave_Click" ValidateForms="SimpleForm1">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
      <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false"  runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
</body>
</html>
