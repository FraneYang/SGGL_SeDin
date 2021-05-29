<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuFlowOperateEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.MenuFlowOperateEdit" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="20px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
              <f:FormRow>
                <Items>                 
                    <f:NumberBox ID="txtFlowStep" runat="server" Label="步骤" AutoPostBack="true" OnTextChanged="txtFlowStep_TextChanged"
                        Required="true" NoDecimal="true" NoNegative="true"></f:NumberBox>
                     <f:NumberBox ID="txtGroupNum" runat="server" Label="组号" AutoPostBack="true" OnTextChanged="txtGroupNum_TextChanged"
                        Required="true" NoDecimal="true" NoNegative="true" ></f:NumberBox>
                     <f:NumberBox ID="txtOrderNum" runat="server" Label="组内序号" 
                        Required="true" NoDecimal="true" NoNegative="true" ></f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
               <Items>
                     <f:TextBox ID="txtAuditFlowName" runat="server" Label="名称" 
                         Required="true" ShowRedStar="true" FocusOnPageLoad="true">
                    </f:TextBox>                  
                </Items>  
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:DropDownBox runat="server" ID="drpRoles" DataControlID="rbRoles"
                        EnableMultiSelect="true" ShowLabel="true" Label="审批角色" >
                        <PopPanel>
                            <f:SimpleForm ID="SimpleForm2" BodyPadding="10px" runat="server" AutoScroll="true"
                                ShowBorder="True" ShowHeader="false" Hidden="true">
                                <Items>
                                    <f:Label ID="Label1" runat="server" Text="请选择审批角色：">
                                    </f:Label>
                                    <f:CheckBoxList ID="rbRoles" ColumnNumber="3" runat="server">                                       
                                    </f:CheckBoxList>
                                </Items>
                            </f:SimpleForm>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:CheckBox ID="IsFlowEnd" MarginLeft="40px" runat="server" Text="流程是否结束" 
                         OnCheckedChanged="IsFlowEnd_CheckedChanged" AutoPostBack="true" >
                    </f:CheckBox>
                </Items> 
            </f:FormRow>
            <f:FormRow>
                <Items>
                     <f:Label runat="server" ID="lbTemp"></f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
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
