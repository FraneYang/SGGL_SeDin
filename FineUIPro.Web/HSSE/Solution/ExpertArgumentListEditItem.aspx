<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertArgumentListEditItem.aspx.cs" ValidateRequest="false" Inherits="FineUIPro.Web.HSSE.Solution.ExpertArgumentListEditItem" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑危险性较大的工程清单</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtSortIndex" runat="server" Label="序号" NoDecimal="true" NoNegative="true"
                        Required="true" ShowRedStar="true">
                    </f:NumberBox>
                    <f:DropDownList ID="drpUnitWorkId" runat="server" Label="单位工程"  EnableEdit="true"
                        Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpUnitWorkId_SelectedIndexChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpWorkPackageId" runat="server" Label="分部分项</br>工程名称"  EnableEdit="true"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                     <f:TextBox ID="txtWorkPackageSize" runat="server" Label="分部分项</br>工程规模"  MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtExpectedStartTime" ShowRedStar="true" DateFormatString="yyyy-MM-dd HH:mm" runat="server"
                        Label="预计施工</br>开始时间" Required="true" LabelAlign="Right" ShowTime="true" ShowSecond="false">
                        </f:DatePicker>
                     <f:DatePicker ID="txtExpectedEndTime" ShowRedStar="true" DateFormatString="yyyy-MM-dd HH:mm" runat="server"
                        Label="预计施工</br>结束时间" Required="true" LabelAlign="Right" ShowTime="true" ShowSecond="false">
                        </f:DatePicker>
                </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>
                     <f:RadioButtonList ID="rblIsArgument" runat="server" Label="专家论证"  Required="true" ShowRedStar="true">
                    </f:RadioButtonList>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="施工单位"  EnableEdit="true"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
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
