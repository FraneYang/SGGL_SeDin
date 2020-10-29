<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSetSave.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectSetSave" %>

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
                   <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" Required="true"  MaxLength="100" 
                    ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"  FocusOnPageLoad="true" ></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
               <Items>
                   <f:TextBox ID="txtProjectCode" runat="server" Label="项目号" Required="true" MaxLength="50" 
                    ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"></f:TextBox>
                </Items>
                <Items>
                   <f:TextBox ID="txtShortName" runat="server" Label="简称" MaxLength="200" LabelWidth="150px"></f:TextBox>
                </Items>
            </f:FormRow>                     
            <f:FormRow>
                 <Items>               
                   <f:DropDownList ID="drpProjectType" Label="项目类型" runat="server">
                   </f:DropDownList>
                       <f:DropDownList ID="drpProjectState" runat="server" Label="项目状态" LabelAlign="Right" LabelWidth="150px">
                        <f:ListItem Text="在建" Value="1"  Selected="true"/>
                        <f:ListItem Text="停工" Value="2" />
                        <f:ListItem Text="竣工" Value="3" />
                    </f:DropDownList>
                </Items>                                   
            </f:FormRow> 
               <f:FormRow>
                <Items>
                    <f:DatePicker runat="server" Label="开工日期" ID="txtStartDate" 
                        Required="true" ShowRedStar="true" AutoPostBack="true"  OnTextChanged="txtStartDate_Blur"></f:DatePicker>             
                   <f:DatePicker runat="server" Label="竣工日期" ID="txtEndDate" LabelWidth="150px"
                       AutoPostBack="true"  OnTextChanged="txtStartDate_Blur"></f:DatePicker>
               </Items>
            </f:FormRow>   
            <f:FormRow>
                 <Items>               
                   <f:TextBox ID="txtContractNo" runat="server" Label="合同号"></f:TextBox>            
                   <f:NumberBox runat="server" ID="txtDuration" Label="项目建设工期(月)"  DecimalPrecision="1"
                        NoNegative="true" LabelWidth="150px">
                    </f:NumberBox>
               </Items>                                         
            </f:FormRow>   
            <f:FormRow>              
               <Items>
                    <f:DropDownList ID="drpProjectManager" runat="server" Label="项目经理" 
                        EnableEdit="true" Required="true" ShowRedStar="true">
                    </f:DropDownList>     
                      <f:DropDownList ID="drpConstructionManager" runat="server" Label="施工经理" LabelWidth="150px" EnableEdit="true">
                    </f:DropDownList>     
                </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpHSSEManager" runat="server" Label="安全经理" EnableEdit="true">
                    </f:DropDownList>     
                   <f:TextBox ID="txtProjectAddress" runat="server" Label="项目地址" LabelWidth="150px" MaxLength="500" ></f:TextBox>

                </Items>
            </f:FormRow>  
            <f:FormRow>
                 <Items>
                   <f:DropDownList ID="drpUnit" Label="所属单位" runat="server" EnableEdit="true">
                   </f:DropDownList>
                     <f:CheckBox runat="server" ID="ckbIsForeign" Label="海外项目" LabelWidth="150px"></f:CheckBox>                     
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtWorkRange" runat="server" Label="工作范围" MaxLength="500" Height="50px"></f:TextArea>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="项目概况" MaxLength="500" Height="50px"></f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>                   
                    <f:NumberBox runat="server" ID="txtProjectMoney" Label="合同额(万元)" NoNegative="true" ></f:NumberBox>
                     <f:NumberBox runat="server" ID="txtConstructionMoney" Label="施工合同额(万元)" LabelWidth="150px"
                         NoNegative="true" ></f:NumberBox>
                </Items>
            </f:FormRow>   
            <f:FormRow>
               <Items>
                   <f:TextBox ID="txtTelephone" runat="server" Label="项目部电话" MaxLength="50" 
                    ></f:TextBox>
                </Items>
                <Items>
                   <f:TextBox ID="txtCountry" runat="server" Label="国家" MaxLength="50" LabelWidth="150px" 
                    ></f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
               <Items>
                   <f:TextBox ID="txtProvince" runat="server" Label="省份" MaxLength="50" 
                    ></f:TextBox>
                </Items>
                <Items>
                   <f:TextBox ID="txtCity" runat="server" Label="城市" MaxLength="50" LabelWidth="150px"
                    ></f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>   
                <Items>
                     <f:CheckBox ID="ckIsUpTotalMonth" runat="server" Label="上报月总结" Checked="true" Hidden="true">
                    </f:CheckBox>
                      <f:TextBox ID="txtMapCoordinates" runat="server" Label="坐标" MaxLength="50"></f:TextBox>                     
                    <f:ContentPanel ID="bottomPanel"  RegionPosition="Bottom" ShowBorder="false" ShowHeader="false" EnableCollapse="false" runat="server">
                        <a href="http://api.map.baidu.com/lbsapi/getpoint/index.html" target="_blank" >拾取坐标</a>
                   </f:ContentPanel>
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
