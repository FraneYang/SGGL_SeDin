<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardListSave.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.Law.HSSEStandardListSave" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtStandardNo" runat="server" Label="标准号" Required="true" ShowRedStar="true"
                        FocusOnPageLoad="true" MaxLength="25">
                    </f:TextBox>
                    <f:TextBox ID="txtStandardName" runat="server" Label="标准名称" Required="true" ShowRedStar="true"
                        MaxLength="100" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtStandardGrade" runat="server" Label="标准级别" MaxLength="12">
                    </f:TextBox>
                    <f:DropDownList ID="drpType" Label="分类" AutoPostBack="false" EnableSimulateTree="true"
                        runat="server">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCompileMan" runat="server" Label="整理人" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker runat="server" Label="整理时间" ID="txtCompileDate">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="Label25" runat="server" Text="对应HSSE索引" CssClass="labcenter">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Rows>
            <f:FormRow>                
                <Items>
                    <f:CheckBox ID="ckb01" runat="server" Label="01地基处理" LabelWidth="90px" > 
                    </f:CheckBox>
                    <f:CheckBox ID="ckb02" runat="server" Label="02打桩" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb03" runat="server" Label="03基坑支护与降水工程" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb04" runat="server" Label="04土方开挖工程" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb05" runat="server" Label="05模板工程" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb06" runat="server" Label="06基础施工" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb07" runat="server" Label="07钢筋混凝土结构" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb08" runat="server" Label="08地下管道" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb09" runat="server" Label="09钢结构" LabelWidth="90px" > 
                    </f:CheckBox>                     
                </Items>              
            </f:FormRow>
            <f:FormRow >              
                <Items>
                    <f:CheckBox ID="ckb10" runat="server" Label="10设备安装" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb11" runat="server" Label="11大型起重吊装工程" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb12" runat="server" Label="12脚手架工程" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb13" runat="server" Label="13机械安装" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb14" runat="server" Label="14管道安装" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb15" runat="server" Label="15电气仪表安装" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb16" runat="server" Label="16防腐保温防火" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb17" runat="server" Label="17拆除" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb18" runat="server" Label="18爆破工程" LabelWidth="90px" >
                    </f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow>               
                <Items>
                    <f:CheckBox ID="ckb19" runat="server" Label="19试压" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb20" runat="server" Label="20吹扫" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb21" runat="server" Label="21试车" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb22" runat="server" Label="22无损检测" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb23" runat="server" Label="23其他危险性较大的工程" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb24" runat="server" Label="24设计" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb25" runat="server" Label="25工厂运行" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:CheckBox ID="ckb90" runat="server"  Label="90全部标准" LabelWidth="90px" >
                    </f:CheckBox>
                    <f:Label runat="server" ID="labTem" LabelWidth="90px" ></f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnUploadResources_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
                    </f:Button>            
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdCompileMan" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="false" runat="server"
            IsModal="true" Width="700px" Height="500px">
       </f:Window>
    </form>
</body>
</html>
