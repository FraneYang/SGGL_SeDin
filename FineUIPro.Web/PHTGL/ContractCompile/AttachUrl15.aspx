<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl15.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl15" %>

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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件15" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件15   施工现场总图管理规定 " CssClass="formtitle f-widget-header" ></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                            <f:Label runat="server" ID="Label3" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1  范围 "></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label2" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本标准适用于公司EPC工程总承包及不同承包模式的工程项目现场总图的管理。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label4" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label5" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2  职责"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label6" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.1  全厂永久性工程、临时生产设施、生活设施和办公设施的施工总平面图由总承包商现场工程项目部统一布置规划、集中统一管理。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label7" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.2  已分包给施工分包商的永久性工程、施工分包商的临时设施施工总平面由施工分包商负责管理，同时接受总承包商现场工程项目部的监督、检查和指导。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label8" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.3  各单位应设专人负责现场总图管理。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label9" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3  管理内容"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.1  制定项目现场施工总图管理规定。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label11" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.2  依据业主提供的原始坐标点、水准点，布设施工测量控制网，向施工分包商提供坐标控制点和高程控制点数据。详见《施工测量管理规定》。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label12" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.3  编制施工总平面布置图。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label13" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.4  审查施工分包商的施工组织设计中有关总图管理部分的内容。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label14" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.5  综合协调现场土石方平衡方案的实施。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label15" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.6  临时用电、临时用水的布置和实施。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label16" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.7监督、检查施工总平面图的实施，推进施工总平面管理的不断深化。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label17" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.8按工程施工进度及各项施工条件的变化情况，及时、合理地调整、修改和补充施工总平面图。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label18" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.9工程施工中建筑物、构筑物、地下管线等测量定位放线检查，高程测量检查。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label19" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4管理规定"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label20" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1  施工开工前，总承包商依据设计总平面布置图、地形图和施工计划，编制“施工总平面布置规划图”，确定办公设施、生活设施、生产设施、设备材料库的占地区域和平面坐标。如果项目现场临时设施用地面积不足，向业主提出临时用地计划。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label21" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2  各施工分包商根据批准的“施工总平面布置规划图”向总承包商提交“临建布置申请表”（见附件1），并附详细临时设施布置图，总承包商审批后，确定各施工分包商的临时用地区域和平面坐标。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label22" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.3  各施工分包商应按批准的施工总平面布置图占用施工场地和搭设临时设施。临时设施应符合集团公司《工程项目现场临建设施标准》的规定。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label23" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.4各施工分包商在正式送水和供电前确认各项工作准备情况，向总承包商提交“临时用水/用电申请表”（附件2），总承包商审批后，正式送水和供电。并按月抄表，填写“水电表抄表记录”（附件3）。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label24" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.5总承包商做好土石方的平衡工作，布置各施工分包商取土、弃土的地点、数量和运输路线。施工分包商不得擅自决定把厂区内的土方运出厂区外弃土或作为其它工程的回填土。若因工程急需必须运出厂外的，要书面报告总承包商批准。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label25" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.6总承包商应检查施工总平面图管理规定的贯彻执行情况，督促各施工分包商按施工总平面图的管理规定建设和拆除临时设施、堆放材料和设备以及调运土石方。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label26" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.7施工分包商在需要切断施工道路、动土挖掘沟渠、切断水、电时，应提前48小时申请，提交“施工现场断路/断水/断电申请表”（见附件4），经总承包商审批后，按批准地点、时间内执行。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label27" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.8现场设置的工程测量控制网和水准点、坐标点，各施工分包商应妥善保护，不得以任何理由拆除或破坏，如造成损坏，要赔偿由此造成的一切损失。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label28" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.9施工分包商不得随意弃土、取土和堆放材料（包括建筑垃圾），如不按指定位置堆放，打乱现场平面秩序，影响其他单位施工者，应对责任单位处罚，并责令迅速清理。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label29" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.10  大件设备进场前，总承包商应组织施工分包商召开专题会，确认运输大件设备的车辆进场日期、时间、路线及卸车地点。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label30" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.11施工分包商应及时向总承包商工程项目部提交更新的现场施工临时供电电缆及供电设备、临时供水管线布置图。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label31" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.12  进入施工现场运送材料的车辆在现场内的停留时间应尽量短，不允许占道卸货阻碍交通。各施工分包商的机动车辆应停在指定的位置，不允许在施工现场道路边随意停放。施工机械及设备应在规定的区域内作业，不得阻碍交通，当需要临时占用共用道路时，应提前报总承包商批准。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label32" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.13  地下工程施工中，发现有古墓、古建筑及地下文物时，要监督保护现场并及时报告，然后按文物管理部门的指示妥善处理，详见《施工现场发现文物的保护及处理规定》。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label33" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.14  已施工完混凝土面层或沥青面层的道路，禁止履带式车辆碾压。因工程需要必须通过时，要用钢板铺垫后，才允许通过。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label34" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.15  对地下工程完工隐蔽前，检查其位置的坐标和标高，记录在总平面图上，防止其他地下工程施工时受到损坏。各分包商动土施工前，应办理“动土作业票”，经总承包商书面批准后准许开挖。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label35" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.16  不得随意非组织排水，凡在现场擅自放水乱流，造成道路泥泞，影响文明施工及道路通行者，应由责任者限期清理。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label36" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.17  施工现场必须做到工作完材料清，工作完场地清。并按进度腾出场地，交付其他施工分包商或业主。施工垃圾、生活垃圾应运送至指定的垃圾回收站。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label37" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.18  所有施工分包商必须接受现场总图管理人员的监督，服从指挥。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label38" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.19  总承包商应定期召开总图管理专题会议，做到互相监督，互相促进，及时解决现场总图管理中的问题。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label39" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5 附件"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label40" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.1  附表1：施工分包商临建布置申请表。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label41" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.2  附表2和附表3：施工分包商用水、用电的申请和记录。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label42" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.3  附表4：施工现场断路/断水/断电申请表。"></f:Label>
                            <br />
 
                            </f:ContentPanel>
                    </Items>
   
                </f:FormRow>
                <f:FormRow>
                    <Items>
                    <f:ContentPanel ID="ContentPanel3" Title="附表1：施工分包商临建布置申请表。" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                              <f:Form ID="Form2" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server" >
                                    <Items>
                                    <f:Panel ID="Panel2" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="Sch1_ProjectName" Label="项目名称" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                            <f:TextBox ID="Sch1_ContractId" Label="编号" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:Panel>
                                     <f:TextArea ID="txtAttachUrlContent" Height="250px" Required="true" ShowRedStar="true" Label="消息正文" runat="server" Text="致：赛鼎工程有限公司XXX项目部
根据我方工程量和现场准备结果，特对施工总平面及相关内容提出以下需求，该内容已经我方项目经理部技术负责人审查批准，现提交，请予以审查批复。

施工单位（章）
项目经理：
日期：
附件：临时设施布置图（含临时水管、供电线路、通讯光纤、排水沟渠走向）
">
                                    </f:TextArea>
                                   <f:Grid ID="Grid1" ShowBorder="true" EnableAjax="false" ShowHeader="true"   EnableCollapse="true"
                                    runat="server" BoxFlex="1" DataKeyNames="AttachUrlId" AllowCellEditing="true"
                                    ClicksToEdit="2" DataIDField="AttachUrlId" EnableColumnLines="true"
                                    EnableTextSelection="True" >
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar2" runat="server" Position="Top" ToolbarAlign="Left">
                                                <Items>
                                                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                                    <f:Button ID="btnNew" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                    <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                  <Columns>
                                   
                                     <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                    EnableLock="true" Locked="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </f:TemplateField>
                                <f:RenderField ColumnID="Type" DataField="Type" Width="150px" FieldType="String" HeaderText="类别" TextAlign="Center"
                                    HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField ColumnID="MainPoints" DataField="MainPoints" Width="320px" FieldType="String" HeaderText="要点" TextAlign="Left"
                                    HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                </f:RenderField>

                            </Columns>
                           </f:Grid>
                                   <f:TextArea ID="Sch1_Opinion" Height="250px" Required="true" ShowRedStar="true" runat="server" Text="总承包商审查意见：



总承包单位（章）
施工经理：
日期：

">
                                    </f:TextArea>
                                </Items>
                           </f:Form>
                         </f:ContentPanel>

                        </Items>
 
                </f:FormRow>
                <f:FormRow>
                    <Items>
                    <f:ContentPanel ID="ContentPanel2" Title="附表2：临时用水/用电申请表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                              <f:Form ID="Form3" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server" >
                                    <Items>
                                    <f:Panel ID="Panel1" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="Sch2_ProjectName" Label="项目名称" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                            <f:TextBox ID="Sch2_ContractId" Label="编号" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:Panel>
                                     <f:TextBox ID="Sch2_Company" Label="申请单位" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                     </f:TextBox>
                                        <f:TextBox ID="Sch2_ConstructionTask" Label="施工任务" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                     </f:TextBox>
                                    <f:Panel ID="Panel3" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:NumberBox  ID="Sch2_Maxcapacitance" Label="最大用电容量(kVA)" Margin="0 5 0 0" Required="true" ShowRedStar="true"   ColumnWidth="50%" runat="server">
                                            </f:NumberBox>
                                            <f:NumberBox ID="Sch2_MaxuseWtater" Label="最大用水量(L/s)" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:NumberBox>
                                        </Items>
                                    </f:Panel>
                                     <f:Panel ID="Panel4" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="Sch2_elemeterPosition" Label="电表安装位置" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server" >
                                            </f:TextBox>
                                            <f:TextBox ID="Sch2_WatermeterPosition" Label="水表安装位置" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:Panel>
                                       <f:Panel ID="Panel5" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:NumberBox ID="Sch2_elemeterRead" Label="电表初始读数" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:NumberBox>
                                            <f:NumberBox ID="Sch2_WatermeterRead" Label="水表初始读数" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:NumberBox>
                                        </Items>
                                    </f:Panel>
                                        <f:RadioButtonList ID="Sch2_IsApproval" Label="临时用电组织设计" runat="server">
                                            <f:RadioItem Text="已审批" Value="1" />
                                            <f:RadioItem Text="未审批" Value="0" />
                                        </f:RadioButtonList>
                                        <f:RadioButtonList ID="Sch2_IsLineLayout" Label="线路布置" runat="server">
                                            <f:RadioItem Text="合格" Value="1" />
                                            <f:RadioItem Text="不合格" Value="0" />
                                        </f:RadioButtonList>
                                        <f:RadioButtonList ID="Sch2_IsPowerBox" Label="配电箱" runat="server">
                                            <f:RadioItem Text="合格" Value="1" />
                                            <f:RadioItem Text="不合格" Value="0" />
                                        </f:RadioButtonList>
                                        <f:RadioButtonList ID="Sch2_IsProfessional_ele" Label="专职电工" runat="server">
                                            <f:RadioItem Text="合格" Value="1" />
                                            <f:RadioItem Text="不合格" Value="0" />
                                        </f:RadioButtonList>
                                        <f:RadioButtonList ID="Sch2_IsLineInstall" Label="管线安装" runat="server">
                                            <f:RadioItem Text="合格" Value="1" />
                                            <f:RadioItem Text="不合格" Value="0" />
                                        </f:RadioButtonList>
                                        <f:RadioButtonList ID="Sch2_IsValve" Label="阀门" runat="server">
                                            <f:RadioItem Text="合格" Value="1" />
                                            <f:RadioItem Text="不合格" Value="0" />
                                        </f:RadioButtonList>
                                        <f:Panel ID="Panel6" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="Sch2_Terminalnumber" Label="接线端子编号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                            <f:TextBox ID="Sch2_LineCabinetNumber" Label="接线柜编号" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                        </Items>
                                       </f:Panel>
                                        <f:TextArea ID="Sch2_electricPrice" Height="80px" Required="true" ShowRedStar="true"  Label="用电价格" runat="server" Text="按               文件执行，                元/度">
                                        </f:TextArea>
                                        <f:TextArea ID="Sch2_WaterPrice" Height="80px" Required="true" ShowRedStar="true"  Label="用水价格" runat="server" Text="按               文件执行，                元/度">
                                        </f:TextArea>
                                        <f:Panel ID="Panel7" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:Label ID="Label" runat ="server" Text="申请单位:"></f:Label>
                                            <f:TextArea ID="TextArea5" Height="120px" Required="true" ShowRedStar="true"  runat="server" Text="申请时间：
                                                年  月  日">
                                        </f:TextArea>
                                            <f:TextArea ID="TextArea6" Height="120px" Required="true" ShowRedStar="true"  runat="server" Text="负责人签字：">
                                        </f:TextArea>
                                             <f:TextArea ID="TextArea7" Height="120px" Required="true" ShowRedStar="true"  runat="server" Text="单位盖章：">
                                        </f:TextArea>
                                        </Items>
                                       </f:Panel>
                                          <f:Panel ID="Panel8" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                             <f:Label ID="Label43" runat ="server" Text="审批单位:"></f:Label>
                                            <f:TextArea ID="TextArea8" Height="120px" Required="true" ShowRedStar="true" runat="server" Text="起用时间：
                                                年  月  日">
                                        </f:TextArea>
                                            <f:TextArea ID="TextArea9" Height="120px" Required="true" ShowRedStar="true"  runat="server" Text="负责人签字：">
                                        </f:TextArea>
                                             <f:TextArea ID="TextArea10" Height="120px" Required="true" ShowRedStar="true"  runat="server" Text="单位盖章：">
                                        </f:TextArea>
                                        </Items>
                                       </f:Panel>
                                   
                                </Items>
                           </f:Form>
                         </f:ContentPanel>

                        </Items>
 
                </f:FormRow>

                  <f:FormRow>
                    <Items>
                    <f:ContentPanel ID="ContentPanel4" Title="附表3：水电表抄表记录" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                              <f:Form ID="Form4" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server" >
                                    <Items>
                                    <f:Grid ID="Grid3" ShowBorder="true" EnableAjax="false" ShowHeader="true"   Title="施工分包商：" EnableCollapse="true"
                                    runat="server" BoxFlex="1" DataKeyNames="AttachUrlId" AllowCellEditing="true"
                                    ClicksToEdit="2" DataIDField="AttachUrlId" EnableColumnLines="true"
                                    EnableTextSelection="True" >
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar4" runat="server" Position="Top" ToolbarAlign="Left">
                                                <Items>
                                                    <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                                    <f:Button ID="btnAdd" ToolTip="增加" Icon="Add" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                    <f:Button ID="btnDel" ToolTip="删除" Icon="Delete" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                  <Columns>
                                   
                                     <f:TemplateField ColumnID="tfPageIndex" Width="55px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                                       EnableLock="true" Locked="False">
                                      <ItemTemplate>
                                        <asp:Label ID="Label44" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                      </ItemTemplate>
                                      </f:TemplateField>

                                        <f:GroupField HeaderText="时间" TextAlign="Center">
                                        <Columns>
                                            <f:BoundField Width="100px" DataField="StartTime" HeaderText="起" />
                                            <f:BoundField Width="100px" DataField="Endtime" HeaderText="止" />
                                        </Columns>
                                        </f:GroupField>
                                      <f:GroupField HeaderText="水表数" TextAlign="Center">
                                        <Columns>
                                            <f:BoundField Width="100px" DataField="Watermeter_Start" HeaderText="起" />
                                            <f:BoundField Width="100px" DataField="Watermeter_End" HeaderText="止" />
                                             <f:BoundField Width="100px" DataField="Watermeter_Read" HeaderText="量" />
                                        </Columns>
                                        </f:GroupField>
                                      <f:GroupField HeaderText="电表数" TextAlign="Center">
                                        <Columns>
                                            <f:BoundField Width="100px" DataField="Elemeter_Start" HeaderText="起" />
                                            <f:BoundField Width="100px" DataField="Elemeter_End" HeaderText="止" />
                                            <f:BoundField Width="100px" DataField="Elemeter_Read" HeaderText="量" />
                                        </Columns>
                                        </f:GroupField>
                                      
                                <f:RenderField ColumnID="SubcontractorsName" DataField="SubcontractorsName" Width="150px" FieldType="String" HeaderText="施工分包商签字" TextAlign="Center"
                                    HeaderTextAlign="Center">
                                </f:RenderField>
                                <f:RenderField ColumnID="GeneralContractorName" DataField="GeneralContractorName" Width="150px" FieldType="String" HeaderText="总承包商签字" TextAlign="Left"
                                    HeaderTextAlign="Center" >
                                </f:RenderField>
                                <f:RenderField ColumnID="Remark" DataField="Remark" Width="150px" FieldType="String" HeaderText="备注" TextAlign="Left"
                                    HeaderTextAlign="Center" >
                                </f:RenderField>
                              
                            </Columns>
                           </f:Grid>

                                     </Items>
                                  </f:Form>
                        </f:ContentPanel>
                        </Items>
                      </f:FormRow>
                        <f:FormRow>
                            <Items>
                            <f:ContentPanel ID="ContentPanel5" Title="附表4：施工现场断路/断水/断电申请表" ShowBorder="true"
                                    BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                                    runat="server">
                                      <f:Form ID="Form5" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server" >
                                            <Items>
                                               <f:Panel ID="Panel9" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                               <Items>
                                                <f:TextBox ID="Sch4_ProjectName" Label="项目名称" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Sch4_ContractId" Label="编号" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                 </Items>
                                              </f:Panel>
                                                <f:TextBox ID="Sch4_SubcontractorsName" Label="施工分包商名称" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:Panel ID="Panel10" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                 <Items>
                                                    <f:RadioButtonList ID="Sch4_Type" Label="类别" runat="server">
                                                        <f:RadioItem Text="断路" Value="0" />
                                                        <f:RadioItem Text="断水" Value="1" />
                                                         <f:RadioItem Text="断电" Value="2" />
                                                    </f:RadioButtonList>
                                                     <f:DatePicker runat="server" ID="Sch4_Time" Label="时间"></f:DatePicker>
                                                 </Items>
                                                </f:Panel>
                                                  <f:Panel ID="Panel12" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                 <Items>
                                                        <f:TextBox ID="Sch4_Reason" Label="原因" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                        </f:TextBox>
                                                        <f:TextBox ID="Sch4_Position" Label="位置/区域" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                        </f:TextBox>
                                                 </Items>
                                                </f:Panel>
                                                 <f:TextArea ID="Sch4_ImpPlan" Height="80px" Required="true" ShowRedStar="true"  Label="实施计划" runat="server" Text="">
                                                 </f:TextArea>
                                                <f:TextArea ID="Sch4_Recoverymeasures" Height="80px" Required="true" ShowRedStar="true"  Label="恢复措施" runat="server" Text="">
                                                 </f:TextArea>
                                                <f:TextArea ID="Sch4_Caption" Height="80px" Required="true" ShowRedStar="true"  Label="说明" runat="server" Text="">
                                                 </f:TextArea>
                                                <f:Panel ID="Panel11" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                <Items>
                                                   <%-- <f:Label ID="Label45" runat ="server" Text="申请单位:"></f:Label>--%>
                                                    <f:TextArea ID="TextArea14" Height="200px"  Width="400px"  Required  ="true" ShowRedStar="true"  Label="申请" runat="server" Text=" （施工分包商）
                                                        年  月  日">
                                                </f:TextArea>
                                                    <f:TextArea ID="TextArea15" Height="200px" Width="400px" Required="true" ShowRedStar="true" Label="批准" runat="server" Text=" （总承包商工程项目部）
                                                       年  月  日 ">
                                                </f:TextArea>
                                                     
                                                </Items>
                                               </f:Panel>


                                            </Items>
                                          </f:Form>
                                </f:ContentPanel>
                                </Items>
                              </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
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
