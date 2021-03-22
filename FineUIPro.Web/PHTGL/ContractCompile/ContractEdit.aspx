<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractEdit.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.ContractEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑合同基本信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false" Title="基本信息"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpProjectId" runat="server" Label="总承包合同编号" LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="drpProjectId_SelectedIndexChanged" LabelWidth="120px"></f:DropDownList>
                        <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true" LabelWidth="140px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtContractName" runat="server" Label="合同名称" LabelAlign="Right" MaxLength="200" LabelWidth="120px"></f:TextBox>
                        <f:TextBox ID="txtContractNum" runat="server" Label="合同编号" LabelAlign="Right" MaxLength="30" LabelWidth="140px"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow ColumnWidths="50% 30% 20%">
                    <Items>
                        <f:TextBox ID="txtParties" runat="server" Label="签约方" LabelAlign="Right" MaxLength="100" LabelWidth="120px"></f:TextBox>
                        <f:DropDownList ID="drpCurrency" runat="server" Label="（预计）合同金额" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                        <f:NumberBox ID="txtContractAmount" runat="server" LabelAlign="Right" NoNegative="true"></f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpDepartId" runat="server" Label="主办部门" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                        <f:DropDownList ID="drpAgent" runat="server" Label="经办人" LabelAlign="Right" LabelWidth="140px"></f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpContractType" runat="server" Label="合同类型" LabelAlign="Right" LabelWidth="120px"></f:DropDownList>
                        <f:Label ID="Label1" runat="server"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtRemark" runat="server" Label="合同摘要" LabelAlign="Right" MaxLength="1000" LabelWidth="120px"></f:TextArea>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
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
    </form>
</body>
</html>
