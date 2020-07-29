<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleListEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.RoleListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑角色</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtRoleCode" runat="server" Label="编码" MaxLength="50" LabelWidth="90px" FocusOnPageLoad="true">
                        </f:TextBox>
                        <f:TextBox ID="txtRoleName" runat="server" Label="名称" Required="true" ShowRedStar="true" MaxLength="50"
                            AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="90px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpRoleType" runat="server" Label="角色类型" EnableEdit="true"
                            ForceSelection="false" Required="true" ShowRedStar="true" LabelWidth="90px">
                        </f:DropDownList>
                        <f:CheckBox ID="chkIsAuditFlow" runat="server" Label="参与审批" LabelWidth="90px">
                        </f:CheckBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownBox runat="server" Label="对口专业" ID="txtCNCodes" EmptyText="--请选择--" EnableMultiSelect="true" MatchFieldWidth="true" LabelWidth="90px">
                            <PopPanel>
                                <f:Grid ID="gvCNCodes" DataIDField="CNProfessionalId" ForceFit="true"
                                    EnableMultiSelect="true" KeepCurrentSelection="true" Height="300px" Hidden="true" SortField="SortIndex" DataTextField="ProfessionalName"
                                    ShowBorder="true" ShowHeader="false"
                                    runat="server" EnableCheckBoxSelect="true">
                                    <Columns>
                                        <f:BoundField Width="100px" DataField="CNProfessionalId" SortField="CNProfessionalId" DataFormatString="{0}" Hidden="true" />
                                        <f:BoundField Width="100px" DataField="ProfessionalName" SortField="ProfessionalName" DataFormatString="{0}"
                                            HeaderText="专业名称" />
                                    </Columns>
                                </f:Grid>
                            </PopPanel>
                        </f:DropDownBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtDef" runat="server" Label="备注" MaxLength="100" LabelWidth="90px"></f:TextBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click" Hidden="true">
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
