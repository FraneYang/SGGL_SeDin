<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainItemView.aspx.cs" Inherits="FineUIPro.Web.ProjectData.MainItemView" %>

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
                        <f:Button ID="btnClose" EnablePostBack="false" 
                            runat="server" Icon="SystemClose">
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
                                            <f:TextBox ID="txtMainItemCode" runat="server" Label="主项编号" Readonly="true"
                                                Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="130px" >
                                            </f:TextBox>
                                           
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server">
                                        <Items>
                                             <f:TextBox ID="txtMainItemName" runat="server" Label="主项名称" Readonly="true"
                                                Required="true" MaxLength="70" ShowRedStar="true" LabelWidth="130px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtDesignProfessional" runat="server" Label="包含设计专业" Readonly="true"
                                                Required="true" ShowRedStar="true" LabelWidth="130px" >
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea runat="server" ID="txtRemark" Label="备注" LabelWidth="130px" Readonly="true"></f:TextArea>
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
