<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectPageDataSave.aspx.cs" Inherits="FineUIPro.Web.PZHGL.ProjectData.ProjectPageDataSave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <rows>
            <f:FormRow>
               <Items>
                   <f:NumberBox ID="txtSafeHours" runat="server" Label="安全人工时" LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                   <f:NumberBox ID="txtSitePersonNum" runat="server" Label="当日现场人数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                </Items>               
            </f:FormRow>                     
          <f:FormRow>
               <Items>
                   <f:NumberBox ID="txtSpecialEquipmentNum" runat="server" Label="大型及特种设备"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                   <f:NumberBox ID="txtEntryTrainingNum" runat="server" Label="累计入场培训人次"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                </Items>               
            </f:FormRow> 
            <f:FormRow>
               <Items>
                   <f:NumberBox ID="txtHiddenDangerNum" runat="server" Label="隐患整改单总数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                   <f:NumberBox ID="txtRectificationNum" runat="server" Label="隐患整改单整改数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                </Items>               
            </f:FormRow> 
          <f:FormRow>
               <Items>
                   <f:NumberBox ID="txtRiskI" runat="server" Label="一级风险数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                   <f:NumberBox ID="txtRiskII" runat="server" Label="二级风险数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                </Items>               
            </f:FormRow> 
            <f:FormRow>
               <Items>
                   <f:NumberBox ID="txtRiskIII" runat="server" Label="三级风险数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                   <f:NumberBox ID="txtRiskIV" runat="server" Label="四级风险数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                </Items>               
            </f:FormRow> 
            <f:FormRow>
               <Items>
                   <f:NumberBox ID="txtRiskV" runat="server" Label="五级风险数"   LabelWidth="140px"
                       NoDecimal="true" NoNegative="true"></f:NumberBox>
                  <f:Label runat="server" ID="lb"></f:Label>
                </Items>               
            </f:FormRow> 
            
        </rows>
        <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" 
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                     <f:HiddenField ID="hdCompileMan" runat="server"></f:HiddenField>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    </form>
</body>
</html>
