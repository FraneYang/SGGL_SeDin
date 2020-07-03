<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldEdit.aspx.cs" Inherits="FineUIPro.Web.HJGL.BaseInfo.WeldEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑焊缝类型</title>
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
                        <f:TextBox ID="txtWeldTypeCode" runat="server" Label="焊缝类型代号" Required="true" MaxLength="50"
                            ShowRedStar="true" FocusOnPageLoad="true" LabelWidth="190px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtWeldTypeName" runat="server" Label="焊缝类型名称" Required="true" MaxLength="50"
                            ShowRedStar="true" LabelWidth="190px">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                 <f:FormRow>
                    <Items>
                        <f:NumberBox ID="txtThickness" Label="壁厚(mm)" runat="server" 
                            LabelWidth="190px" DecimalPrecision="4" NoNegative="true" >
                        </f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpDetectionType" Label="探伤类型" runat="server" EnableCheckBoxSelect="true" EnableMultiSelect="true"
                            ShowRedStar="true" Required="true" EnableEdit="true" LabelWidth="190px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
               
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtRemark" runat="server" Label="备注" MaxLength="500" LabelWidth="190px">
                        </f:TextArea>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server" LabelWidth="190px" Label="说明"  Text ="针对于对接焊缝壁厚大于设定值探伤类型为UT，否则为RT" >

                        </f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
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
