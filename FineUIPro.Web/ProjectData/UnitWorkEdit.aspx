<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWorkEdit.aspx.cs" Inherits="FineUIPro.Web.ProjectData.UnitWorkEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>单位工程</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow runat="server">
                    <Items>
                        <f:TextBox ID="txtUnitWorkCode" runat="server" Label="单位工程编号"
                            Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:TextBox ID="txtUnitWorkName" runat="server" Label="单位工程名称"
                            Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="110px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow runat="server">
                    <Items>
                        <f:DropDownList ID="drpProjectType" runat="server" EmptyText="--请选择--"  Required="true"  AutoSelectFirstItem="false" Label="所属工程" LabelWidth="110px" ShowRedStar="true" LabelAlign="Right" EnableEdit="true">
                            <f:ListItem Text="建筑工程" Value="1" />
                            <f:ListItem Text="安装工程" Value="2" />
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:NumberBox runat="server" ID="txtWeights" Label="权重%" LabelWidth="110px" NoDecimal="false" NoNegative="true"></f:NumberBox>
                   </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList runat="server" ID="drpUnit" Label="施工单位" LabelWidth="110px" ShowRedStar="true"></f:DropDownList>
                   </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList runat="server" ID="drpSupervisorUnit" Label="监理单位" LabelWidth="110px"></f:DropDownList>
                   </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList runat="server" ID="drpNDEUnit" Label="检测单位" LabelWidth="110px"></f:DropDownList>
                   </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdCheckerId" runat="server">
                        </f:HiddenField>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false"
                            runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
