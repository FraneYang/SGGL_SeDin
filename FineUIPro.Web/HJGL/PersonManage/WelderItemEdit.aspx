<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelderItemEdit.aspx.cs"
    Inherits="FineUIPro.Web.HJGL.PersonManage.WelderItemEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊工资质</title>
    <base target="_self" />
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtWelderCode" runat="server" Label="焊工号"
                        Readonly="true" LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtQualificationItem" runat="server" Label="合格项目"  LabelWidth="140px"
                        Required="true" ShowRedStar="true"  EnableBlurEvent="true"  OnBlur="txtQualificationItem_OnBlur" ></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:DatePicker ID="txtCheckDate" runat="server" Label="批准日期"
                        LabelWidth="140px" Required="true" ShowRedStar="true">
                    </f:DatePicker>
                    <f:DatePicker ID="txtLimitDate" runat="server" Label="有效日期"
                        LabelWidth="140px" Required="true" ShowRedStar="true">
                    </f:DatePicker>
                  
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>  
                    <f:TextBox ID="txtWeldingMethod" runat="server"  Label="焊接方法"  LabelWidth="140px"></f:TextBox>
                    <f:TextBox ID="txtMaterialType" runat="server" Label="型号、牌号、级别" LabelWidth="140px"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items> 
                    <f:TextBox ID="txtWeldingLocation" runat="server" Label="焊接位置" LabelWidth="140px"></f:TextBox>
                    <f:NumberBox ID="txtThicknessMax" runat="server" Label="管径覆盖范围（最大值）"
                        NoNegative="true" LabelAlign="Right" LabelWidth="140px">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtSizesMin" runat="server" Label="壁厚覆盖范围（最小值）" NoNegative="true" LabelAlign="Right" LabelWidth="140px">
                    </f:NumberBox> 
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" 
                        LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:HiddenField ID="hdWelderId" runat="server">
                    </f:HiddenField>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭"
                        runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
