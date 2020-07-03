<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentalEdit.aspx.cs" Inherits="FineUIPro.Web.HSSE.Technique.EnvironmentalEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑环境因素危险源</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
        .lab
        {           
            font-size:small;
            color:Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>                   
                    <f:TextBox ID="txtCode" runat="server" Label="危险源代码" Required="true" ShowRedStar="true" FocusOnPageLoad="true"
                        MaxLength="50" LabelWidth="120px" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                    <f:DropDownList ID="drpSmallType" runat="server" Label="危险源类型"   FocusOnPageLoad="true"
                        LabelWidth="120px" AutoPostBack="true" ShowRedStar="true" Required="true" 
                        OnSelectedIndexChanged="drpSmallType_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="drpEType" runat="server" Label="环境类型" LabelWidth="120px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtActivePoint" runat="server" Label="分项工程/活动点" LabelWidth="120px" MaxLength="500" ShowRedStar="true" Required="true">
                    </f:TextBox>
                    <f:TextBox ID="txtEnvironmentalFactors" runat="server" Label="环境因素" LabelWidth="120px" ShowRedStar="true" Required="true"
                        MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="fRow1" Hidden="true">
                <Items>
                    <f:NumberBox ID="txtAValue" runat="server" Label="A值" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" MaxValue="5" MinValue="1" NoDecimal="true" NoNegative="true" Increment="1" LabelWidth="120px">
                    </f:NumberBox>
                    <f:NumberBox ID="txtBValue" runat="server" Label="B值" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" MaxValue="5" MinValue="1" NoDecimal="true" NoNegative="true" Increment="2" LabelWidth="120px">
                    </f:NumberBox>
                     <f:NumberBox ID="txtCValue" runat="server" Label="C值" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" MaxValue="5" MinValue="1" NoDecimal="true" NoNegative="true" Increment="1" LabelWidth="120px">
                    </f:NumberBox>
                    <f:NumberBox ID="txtDValue" runat="server" Label="D值" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" MaxValue="5" MinValue="1" NoDecimal="true" NoNegative="true" Increment="2" LabelWidth="120px">
                    </f:NumberBox>
                    <f:NumberBox ID="txtEValue" runat="server" Label="E值" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" MaxValue="5" MinValue="1" NoDecimal="true" NoNegative="true" Increment="1" LabelWidth="120px">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="fRow2">
                <Items>
                    <f:NumberBox ID="txtFValue" runat="server" Label="F值" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" MaxValue="5" MinValue="1" NoDecimal="true" NoNegative="true" Increment="2" LabelWidth="120px">
                    </f:NumberBox>
                    <f:NumberBox ID="txtGValue" runat="server" Label="G值" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" MaxValue="5" MinValue="1" NoDecimal="true" NoNegative="true" Increment="2" LabelWidth="120px">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtZValue" runat="server" NoDecimal="true" Readonly="true" Label="Z值" LabelWidth="120px">
                    </f:NumberBox>
                    <f:RadioButtonList ID="rblIsImportant" runat="server" Label="重要环境因素" LabelWidth="130px" Readonly="true" Width="200px">
                    </f:RadioButtonList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtControlMeasures" runat="server" Label="安全措施" LabelWidth="120px" MaxLength="800" >
                    </f:TextBox>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" LabelWidth="120px" MaxLength="800">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="lbShow" Text="说明：（1）环境因素分为污染类环境因素和能源资源类环境因素，采用多因子评分法进行评价。
                        污染类环境因素评价：当 A 或 B 或 D=5 时，或∑(A+ B+ C +D +E)≥15时，一般定为重要环境因素；
                        能源资源类环境影响因素的评价，当F=5或G=5或∑(F+G）＞7时，该环境因素确定为重要环境因素，其余为一般环境因素。" CssClass="lab" MarginLeft="50px"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label1" Text="（2）A值、C值、E值取值范围为【1、2、3、4、5】" CssClass="lab" MarginLeft="60px"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label2" Text="（3）B值、D值、F值、G值取值范围为【1、3、5】" CssClass="lab" MarginLeft="60px"></f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdCompileMan" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
