<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkPackageSet2In.aspx.cs" Inherits="FineUIPro.Web.CQMS.WBS.WorkPackageSet2In" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入</title>
    <link href="~/res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="数据导入" ValidateForms="SimpleForm1"
                            OnClick="btnAudit_Click">
                        </f:Button>
                        <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSaveNew" runat="server" Text=""
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                            LabelWidth="150px">
                        </f:FileUpload>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                            runat="server" BoxFlex="1" DataKeyNames="WorkPackageId,WorkPackageCode" AllowSorting="true" EnableColumnLines="true"
                            SortField="WorkPackageId" SortDirection="ASC" AllowCellEditing="true" ClicksToEdit="1" ForceFit="true"
                            ShowSelectedCell="true" DataIDField="WorkPackageId" AllowPaging="false" IsDatabasePaging="false"
                            PageSize="200" Hidden="true">

                            <Columns>
                                <f:RenderField Width="200px" ColumnID="PackageContent" DataField="PackageContent" FieldType="String"
                                    HeaderText="第2级" HeaderTextAlign="Center" TextAlign="Center">
                                </f:RenderField>
                                <f:RenderField HeaderText="定制" ColumnID="SuperWorkPack" DataField="SuperWorkPack" SortField="SuperWorkPack"
                                    HeaderTextAlign="Center" TextAlign="Center" Width="200px" FieldType="String">
                                    <Editor>
                                        <f:TextBox runat="server" ID="txtName">
                                        </f:TextBox>
                                    </Editor>
                                </f:RenderField>
                                <f:RenderField HeaderText="权重%" ColumnID="Weights" DataField="Weights"
                                    SortField="Weights" HeaderTextAlign="Center" TextAlign="Center" Width="90px"
                                    FieldType="String">
                                    <Editor>
                                        <f:NumberBox ID="txtWeights" runat="server" NoNegative="true" TrimEndZero="false" NoDecimal="false">
                                        </f:NumberBox>
                                    </Editor>
                                </f:RenderField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:HiddenField ID="hdFileName" runat="server">
                        </f:HiddenField>
                        <f:HiddenField ID="hdCheckResult" runat="server">
                        </f:HiddenField>
                        <f:HiddenField runat="server" ID="hdTotalValue"></f:HiddenField>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="lblBottom" runat="server" Text="说明：1 导入模板为.xls后缀的EXCEL文件，黑体字为必填项。2 数据导入完成，成功后自动返回，如果有不成功数据页面弹出提示框">
                        </f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
    </form>
</body>
</html>
