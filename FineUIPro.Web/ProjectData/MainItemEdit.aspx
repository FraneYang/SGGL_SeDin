<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainItemEdit.aspx.cs" Inherits="FineUIPro.Web.ProjectData.MainItemEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>项目主项</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" OnClick="btnSave_Click" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Rows>
                <f:FormRow>
                    <Items>
                            <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow runat="server">
                                        <Items>
                                            <f:TextBox ID="txtProjectName" runat="server" Label="项目" MaxLength="70" LabelWidth="130px" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtMainItemCode" runat="server" Label="主项编号"
                                                Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="130px" AutoPostBack="true" OnTextChanged="txtMainItemCode_TextChanged">
                                            </f:TextBox>
                                           
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                             <f:TextBox ID="txtMainItemName" runat="server" Label="主项名称"
                                                Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="130px">
                                            </f:TextBox>
                                            <f:DropDownBox runat="server" Label="单位工程" ShowRedStar="true"
                                                Required="true" ID="txtCarryUnit" EmptyText="--请选择--" EnableMultiSelect="true" MatchFieldWidth="true" LabelWidth="130px">
                                                <PopPanel>
                                                    <f:Grid ID="gvCarryUnit" DataIDField="UnitWorkId"
                                                        EnableMultiSelect="true" KeepCurrentSelection="true" Height="300px" Hidden="true" SortField="UnitWorkId" DataTextField="UnitWorkName"
                                                        ShowBorder="true" ShowHeader="false" ForceFit="true"
                                                        runat="server" EnableCheckBoxSelect="true">
                                                        <Columns>
                                                            <f:BoundField Width="100px" DataField="UnitWorkId" SortField="UnitWorkId" DataFormatString="{0}" Hidden="true" />
                                                            <f:BoundField Width="100px" DataField="UnitWorkName" SortField="UnitWorkName" DataFormatString="{0}"
                                                                 HeaderText="单位工程名称" />
                                                        </Columns>
                                                    </f:Grid>
                                                </PopPanel>
                                            </f:DropDownBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea runat="server" ID="txtRemark" Label="备注" LabelWidth="130px"></f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                    </Items>

                </f:FormRow>
            </Rows>

        </f:Form>
    </form>
</body>
</html>
