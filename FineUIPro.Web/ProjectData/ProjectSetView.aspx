<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSetView.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectSetView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <rows>
            <f:FormRow>
                <Items>
                   <f:TextBox ID="txtProjectName" runat="server" Label="项目名称"  Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:TextBox ID="txtProjectCode" runat="server" Label="项目号" Readonly="true"></f:TextBox>
                </Items>
                 <Items>
                   <f:TextBox ID="txtShortName" runat="server" Label="简称" Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>               
                  <f:TextBox ID="txtProjectType" runat="server" Label="项目类型" Readonly="true"></f:TextBox>
                </Items> 
                <Items>
                   <f:TextBox ID="txtProjectState" runat="server" Label="项目状态" Readonly="true"></f:TextBox>
               </Items>                                         
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Label="开工日期" ID="txtStartDate"  Readonly="true"></f:TextBox>
                </Items>
                 <Items>
                   <f:TextBox runat="server" Label="竣工日期" ID="txtEndDate" Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                   <f:TextBox ID="txtPostCode" runat="server" Label="邮编" Readonly="true"></f:TextBox>
               </Items>    
               <Items>
                   <f:TextBox ID="txtProjectManager" runat="server" Label="项目经理" Readonly="true"></f:TextBox>   
                </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>    
                    <f:TextBox ID="txtConstructionManager" runat="server" Label="施工经理" Readonly="true"></f:TextBox>   
                </Items>
                <Items>                    
                    <f:TextBox ID="txtHSSEManager" runat="server" Label="安全经理" Readonly="true"></f:TextBox>      
                </Items>
            </f:FormRow>  
            <f:FormRow>
                 <Items>
                   <f:TextBox ID="txtProjectAddress" runat="server" Label="项目地址" Readonly="true"></f:TextBox>
                   <f:TextBox ID="txtUnitName" runat="server" Label="所属单位" Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                 <Items>
                   <f:TextArea ID="txtRemark" runat="server" Label="项目概况" Readonly="true"></f:TextArea>
                </Items>
            </f:FormRow>    
            <f:FormRow>
                 <Items>
                   <f:CheckBox runat="server" ID="ckbIsForeign" Label="海外项目" Enabled="false"></f:CheckBox>
                    <f:TextBox ID="txtMapCoordinates" runat="server" Label="坐标" Readonly="true"></f:TextBox>
                </Items>
            </f:FormRow>     
        </rows>
        <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                    
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    </form>
</body>
</html>
