<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl8.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl8" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件8</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Panel ID="Panel4" IsFluid="true" CssClass="blockpanel" BodyPadding="10" Layout="VBox" MaxHeight="550" BoxConfigChildMargin="0 0 5 0" AutoScroll="true"
            EnableCollapse="true" Title="附件8    总承包商关键人员一览表" runat="server">
            <Items>
                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" Title="" AutoScroll="true"
                    runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                    <Items>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="Label1" runat="server" Text="附件8    总承包商关键人员一览表" CssClass="widthBlod"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:HtmlEditor runat="server" Label=" " ID="txtAttachUrlContent" ShowLabel="false"
                                    Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="500px" LabelAlign="Right" Text="">
                                </f:HtmlEditor>
                            </Items>
                        </f:FormRow>
                        <%--<f:FormRow>
                    <Items> 
                     <f:ContentPanel ID="ContentPanel1" Title="总承包商关键人员一览表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                              <f:Form ID="Form3" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server" >
                                <Items>
                                   <f:TextBox ID="txtProjectManager" Label="项目经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtProjectManager_deputy" Label="项目副经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtSafetyDirector" Label="安全总监" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtControlManager" Label="控制经理" Margin="0 5 0 0" AutoPostBack="true"  Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtDesignManager" Label="设计经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                    <f:TextBox ID="txtPurchasingManager" Label="采购经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                    <f:TextBox ID="txtConstructionManager" Label="施工经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                    <f:TextBox ID="txtConstructionManager_deputy" Label="施工副经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                    <f:TextBox ID="txtQualityManager" Label="质量经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtHSEManager" Label="HSE经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtDrivingManager" Label="开车经理 " Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtFinancialManager" Label="财务经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
                                     <f:TextBox ID="txtOfficeManager" Label="行政经理" Margin="0 5 0 0" AutoPostBack="true" Required="true" ShowRedStar="true"   runat="server">
                                     </f:TextBox>
   
                               </Items>
                           </f:Form>
                        </f:ContentPanel>
                   </Items>
                </f:FormRow>--%>
                     </Items>
                </f:Form>
             </Items>
            
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:HiddenField ID="hdAttachUrlItemId" runat="server" ></f:HiddenField>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text ="保存" Size="Medium" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text="关闭" Size="Medium" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Panel>

    </form>
</body>
</html>
