<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstructionLogEdit.aspx.cs" Inherits="FineUIPro.Web.PZHGL.InformationProject.ConstructionLogEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="项目级施工日志" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>

                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpWeather" ShowRedStar="true" Required="true" runat="server" LabelWidth="150px" Label="天气状况" LabelAlign="Right" EnableEdit="true">
                                </f:DropDownList>
                                            <f:NumberBox runat="server" ShowRedStar="true" Required="true" NoDecimal="true" LabelWidth="120px" Label="最低温度(℃)" ID="txtTemperatureMin"></f:NumberBox>
                                            <f:NumberBox runat="server" ShowRedStar="true" Required="true" NoDecimal="true" LabelWidth="120px" Label="最高温度(℃)" ID="txtTemperatureMax"></f:NumberBox>
                                            <f:DatePicker ID="txtCompileDate" runat="server" ShowRedStar="true" Required="true" Label="编制日期" LabelAlign="Right"
                                                EnableEdit="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtMainWork" runat="server" Label="主要工作摘要" MaxLength="3000" LabelWidth="150px" Readonly="true">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtMainProblems" runat="server" Label="主要问题及处理措施" LabelWidth="150px" MaxLength="3000">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="3000" LabelWidth="150px">
                                            </f:TextArea>
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
                        <f:Button ID="imgBtnFile" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="imgBtnFile_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:HiddenField ID="HFConstructionLogId" runat="server"></f:HiddenField>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
                        <%--  <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" OnClick="btnClose_Click" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
