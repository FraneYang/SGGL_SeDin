<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl13.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl13" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            padding-left:500px;   
        }
                .formtitle .f-field-body {
            text-align: center;
            font-size: 20px;
            line-height: 1.2em;
            margin: 10px 0;
        }
 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件13" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件13    工程质量保修书 " CssClass="formtitle f-widget-header" ></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                            
                            <f:TextBox ID="txtGeneralContractorName" Label="总承包商（全称）" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                            </f:TextBox>
                            <br />
                            <f:TextBox ID="txtSubcontractorsName" Label="施工分包商（全称）" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                            </f:TextBox>
                            <br />
                            <f:Label runat="server" ID="Label3" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商和施工分包商根据《中华人民共和国建筑法》和《建设工程质量管理条例》，经协商一致就  "></f:Label>
                            <br />
                             <f:TextBox ID="txtProjectName" Label="" Required="true" ShowRedStar="true" runat="server">
                            </f:TextBox>
                            <f:Label runat="server" ID="Label2" Text="签订工程质量保修书。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label4" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;　一、工程质量保修范围和内容 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label5" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;　施工分包商在质量保修期内，按照有关法律规定和合同约定，承担工程质量保修责任 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label6" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;　质量保修范围包括地基基础工程、主体结构工程，屋面防水工程、有防水要求的卫生间、房间和外墙面的防渗漏，供热与供冷系统，电气管线、给排水管道、设备安装和装修工程，以及双方约定的其他项目。 "></f:Label>
                            <br />
                            <f:Label ID="txtWarrantyContent"    runat="server" Text="具体保修的内容，双方约定如下。" >
                             </f:Label>
                            <br />
                            <f:Label runat="server" ID="Label7" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 二、质量保修期 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label8" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 根据《建设工程质量管理条例》及有关规定，工程的质量保修期如下： "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label9" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 1、基础设施工程、房屋建筑的地基基础工程和主体结构工程，为设计文件规定的该工程的合理使用年限； "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2、屋面防水工程、有防水要求的卫生间、房间和外墙面的防渗漏，为5年； "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label11" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3、供热与供冷系统，为2个采暖期、供冷期； "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label12" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4、电气管线、给排水管道、设备安装和装修工程，为2年。 "></f:Label>
                            <br />
                            <f:TextArea runat="server" ID="txtOtherWarrantyPeriod" EmptyText="" LabelWidth="330" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 5．其他项目保修期限约定如下："
                                        AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                             </f:TextArea>
                            <br />
                            <f:Label runat="server" ID="Label14" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;质量保修期自  "></f:Label>
                            <f:TextBox ID="txtWarrantyPeriodDate" Label="" Required="true" ShowRedStar="true" runat="server">
                            </f:TextBox>
                            <f:Label runat="server" ID="Label19" Text="起计算。 "></f:Label>
                            <br />

                            <f:Label runat="server" ID="Label15" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 三、缺陷责任期 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label16" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工程缺陷责任期为  "></f:Label>
                             <f:NumberBox ID="txtDefectLiabilityPeriod" runat="server"></f:NumberBox>
                            <f:Label runat="server" ID="Label20" Text="个月，缺陷责任期自"></f:Label>
                            <f:TextBox ID="txtDefectLiabilityDate" Label="" Required="true" ShowRedStar="true" runat="server">
                            </f:TextBox>
                            <f:Label runat="server" ID="Label21" Text="起计算。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label13" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 缺陷责任期终止后，总承包商应退还剩余的质量保证金。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label17" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  四、质量保修责任 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label18" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 1．属于保修范围、内容的项目，施工分包商应当在接到保修通知之日起7天内派人保修。施工分包商不在约定期限内派人保修的，总承包商可以委托他人修理。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label22" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2．发生紧急事故需抢修的，施工分包商在接到事故通知后，应当立即到达事故现场抢修。 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label23" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3．对于涉及结构安全的质量问题，应当按照《建设工程质量管理条例》的规定，立即向当地建设行政主管部门和有关部门报告，采取安全防范措施，并由原设计人或者具有相应资质等级的设计人提出保修方案，施工分包商实施保修。  "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label24" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4．质量保修完成后，由总承包商组织验收。  "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label25" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 五、保修费用 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label26" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 保修费用由造成质量缺陷的责任方承担。 "></f:Label>
                            <br />
                                                        <f:Label runat="server" ID="Label27" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  六、双方约定的其他工程质量保修事项"></f:Label>

<%--                             <f:TextArea runat="server" ID="txtOtherqualityWarranty" EmptyText="" LabelWidth="330" Label="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  六、双方约定的其他工程质量保修事项"
                              AutoGrowHeight="true" AutoGrowHeightMin="50" AutoGrowHeightMax="600">
                             </f:TextArea>--%>
                            <br />
                            <f:Label runat="server" ID="Label28" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工程质量保修书由总承包商、施工分包商在工程竣工验收前共同签署，作为施工合同附件，其有效期限至保修期满。  "></f:Label>
                            <br />
                            <br />
                            <br />
                            <f:Label runat="server" ID="Label29" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商：                                          施工分包商："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label30" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（公章或合同专用章）                                         （公章或合同专用章）"></f:Label>
                            <f:Label runat="server" ID="Label31" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;法定代表人或其委托代理人：                                         法定代表人或其委托代理人："></f:Label>
                            <br />
                            <br />
                            <f:Label runat="server" ID="Label32" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期：    年    月    日" LabelAlign="Left"></f:Label>


                            </f:ContentPanel>
                    </Items>
   
                </f:FormRow>
                
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" Text="保存" ValidateForms="SimpleForm1" Size="Medium"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" Text ="关闭" runat="server" Icon="SystemClose" Size="Medium">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
