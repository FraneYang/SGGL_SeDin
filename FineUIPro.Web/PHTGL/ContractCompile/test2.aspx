<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test2.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.test2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            padding-left: 500px;
        }

        .formtitle .f-field-body {
            text-align: center;
            font-size: 20px;
            line-height: 1.2em;
            margin: 10px 0;
        }

        .f-field-body f-widget-header {
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
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="表二" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="金属材料焊接操作技能考试检验记录表" CssClass="formtitle f-widget-header"></f:Label>
                    </Items>

                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label37" runat="server" Text="姓名"></f:Label>
                        <f:Label ID="Label38" runat="server" Text="考试编号"></f:Label>

                    </Items>
                </f:FormRow>


                <f:FormRow>
                    <Items>
                        <f:Form ID="Form2" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>
                                <f:Panel ID="Panel2" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="Sch1_ProjectName" Label="焊接方法" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                        <f:CheckBoxList ID="CheckBoxList1" Label="机动化程度" runat="server" ColumnNumber="2" ColumnWidth="50%">
                                            <f:CheckItem Text="自动焊" Value="value1" />
                                            <f:CheckItem Text="机动焊" Value="value2" />
                                            <f:CheckItem Text="手工焊" Value="value3" />
                                        </f:CheckBoxList>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel13" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox1" Label="焊接作业指导书编号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox2" Label="试件金属材料类别代号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel14" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox3" Label="试件板材厚度" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox4" Label="试件管材外径与壁厚" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel15" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox5" Label="螺柱直径" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox6" Label="填充金属材料类别代号、型号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                    </Items>
                                </f:Panel>
                                <f:TextBox ID="TextBox7" Label="考试项目代号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                                <f:Label runat="server" ID="Label4" Text="试件外观检查" CssClass=" f-field-body f-widget-header"></f:Label>
                                <f:Panel ID="Panel16" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Label5" Text="焊缝表面状况" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label6" Text="焊缝余高" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label7" Text="焊缝余高差" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label8" Text="比坡口每侧增宽" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label9" Text="宽度差" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label10" Text="焊缝边缘直线度" ColumnWidth="17.5%"></f:Label>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel9" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox8" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox9" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox10" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox11" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox12" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox13" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel10" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Label11" Text="背面焊缝余高" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label12" Text="裂纹" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label13" Text="未熔合" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label14" Text="夹渣" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label15" Text="咬边" ColumnWidth="17.5%"></f:Label>
                                        <f:Label runat="server" ID="Label16" Text="未焊透" ColumnWidth="17.5%"></f:Label>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel11" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox14" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox15" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox16" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox17" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox18" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox19" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel12" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Label17" Text="背面凹坑" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label18" Text="气孔" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label19" Text="焊瘤" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label20" Text="变形角度" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label21" Text="错变量" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label22" Text="" ColumnWidth="20%"></f:Label>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel17" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox20" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox21" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox22" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox23" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox24" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>

                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel18" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Label23" Text="角焊缝凹凸度" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label24" Text="焊脚" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label25" Text="堆焊焊道接头不平度" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label26" Text="堆焊焊道高度差" ColumnWidth="20%"></f:Label>
                                        <f:Label runat="server" ID="Label27" Text="堆焊凹下量" ColumnWidth="20%"></f:Label>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel19" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox26" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox27" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox28" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox29" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox30" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                        </f:TextBox>

                                    </Items>
                                </f:Panel>

                                <f:TextArea ID="TextArea1" Height="250px" Required="true" Label="工作简历" ShowRedStar="true" runat="server" Text="">
                                </f:TextArea>
                                <f:Label runat="server" ID="Label28" Text="无损检测" CssClass="f-field-body f-widget-header"></f:Label>
                                <f:Panel ID="Panel20" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Label29" Text="射线透照质量等级" ColumnWidth="25%"></f:Label>
                                        <f:Label runat="server" ID="Label30" Text="焊缝缺陷等级" ColumnWidth="25%"></f:Label>
                                        <f:Label runat="server" ID="Label31" Text="报告编号与日期" ColumnWidth="25%"></f:Label>
                                        <f:Label runat="server" ID="Label32" Text="结果" ColumnWidth="25%"></f:Label>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel21" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox25" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox31" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox32" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                        </f:TextBox>
                                        <f:CheckBoxList ID="CheckBoxList2" Label="" runat="server" ColumnNumber="2" ColumnWidth="25%">
                                            <f:CheckItem Text="合格" Value="value1" />
                                            <f:CheckItem Text="不合格" Value="value2" />
                                        </f:CheckBoxList>

                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel22" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Label33" Text="渗透检测方法" ColumnWidth="25%"></f:Label>
                                        <f:Label runat="server" ID="Label34" Text="渗透检测结果" ColumnWidth="25%"></f:Label>
                                        <f:Label runat="server" ID="Label35" Text="报告编号与日期" ColumnWidth="25%"></f:Label>
                                        <f:Label runat="server" ID="Label36" Text="结果" ColumnWidth="25%"></f:Label>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel23" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                    <Items>
                                        <f:TextBox ID="TextBox33" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox34" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox35" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                        </f:TextBox>
                                        <f:CheckBoxList ID="CheckBoxList3" Label="" runat="server" ColumnNumber="2" ColumnWidth="25%">
                                            <f:CheckItem Text="合格" Value="value1" />
                                            <f:CheckItem Text="不合格" Value="value2" />
                                        </f:CheckBoxList>

                                    </Items>
                                </f:Panel>
                                <f:TextBox ID="TextBox36" Label="无损检测人员" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                </f:TextBox>
                                <f:DatePicker ID="TextBox37" runat="server" Label="证书有效期" LabelAlign="Right">
                                </f:DatePicker>
                                <f:TextBox ID="TextBox38" Label="无损检测人员证书号" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                </f:TextBox>
                            </Items>
                        </f:Form>


                    </Items>

                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" Title="金属材料焊接操作技能考试检验记录表" ShowBorder="true"
                            BodyPadding="10px" EnableCollapse="true" ShowHeader="true" AutoScroll="true"
                            runat="server">
                            <f:Form ID="Form3" LabelAlign="Top" BodyPadding="10px" ShowBorder="false" ShowHeader="false" runat="server">
                                <Items>
                                    <f:Panel ID="Panel33" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="TextBox77" Label="姓名" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                            <f:TextBox ID="TextBox78" Label="考试编号" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                            </f:TextBox>
                                        </Items>
                                    </f:Panel>
                                    <f:Label runat="server" ID="Label70" Text="弯曲实验" CssClass=" f-field-body f-widget-header"></f:Label>

                                    <f:Panel ID="Panel31" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:Label runat="server" ID="Label66" Text="面弯" ColumnWidth="20%"></f:Label>
                                            <f:Label runat="server" ID="Label67" Text="背弯" ColumnWidth="20%"></f:Label>
                                            <f:Label runat="server" ID="Label71" Text="侧弯" ColumnWidth="20%"></f:Label>
                                            <f:Label runat="server" ID="Label68" Text="报告编号与日期" ColumnWidth="20%"></f:Label>
                                            <f:Label runat="server" ID="Label69" Text="结果" ColumnWidth="20%"></f:Label>
                                        </Items>
                                    </f:Panel>
                                    <f:Panel ID="Panel32" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="TextBox72" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="20%">
                                            </f:TextBox>
                                            <f:TextBox ID="TextBox73" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="20%">
                                            </f:TextBox>
                                            <f:TextBox ID="TextBox74" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="20%">
                                            </f:TextBox>
                                            <f:TextBox ID="TextBox79" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="20%">
                                            </f:TextBox>
                                            <f:CheckBoxList ID="CheckBoxList6" Label="" runat="server" ColumnNumber="2" ColumnWidth="20%">
                                                <f:CheckItem Text="合格" Value="value1" />
                                                <f:CheckItem Text="不合格" Value="value2" />
                                            </f:CheckBoxList>

                                        </Items>
                                    </f:Panel>
                                    <f:Panel ID="Panel34" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="TextBox80" Label="检验员" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="50%">
                                            </f:TextBox>
                                            <f:DatePicker ID="DatePicker2" runat="server" Label="证书有效期" ColumnWidth="50%">
                                            </f:DatePicker>


                                        </Items>
                                    </f:Panel>

                                    <f:Label runat="server" ID="Label72" Text="金相检验" CssClass=" f-field-body f-widget-header"></f:Label>
                                    <f:Panel ID="Panel35" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="StretchMax">
                                        <Items>
                                            <f:Panel ID="Panel36" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="Label73" Text="检验结果"></f:Label>
                                                    <f:Panel ID="Panel8" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>
                                                            <f:Label runat="server" ID="Label44" Text="金相面Ⅰ" ColumnWidth="25%"></f:Label>
                                                            <f:Label runat="server" ID="Label45" Text="金相面Ⅱ" ColumnWidth="25%"></f:Label>
                                                            <f:Label runat="server" ID="Label46" Text="金相面Ⅲ" ColumnWidth="25%"></f:Label>
                                                            <f:Label runat="server" ID="Label47" Text="金相面Ⅳ" ColumnWidth="25%"></f:Label>

                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel24" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>
                                                            <f:TextBox ID="TextBox53" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                            <f:TextBox ID="TextBox54" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                            <f:TextBox ID="TextBox55" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                            <f:TextBox ID="TextBox56" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel37" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false" ShowHeader="false">
                                                <Items>
                                                    <f:Panel ID="Panel29" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>

                                                            <f:Label runat="server" ID="Label64" Text="报告编号与日期" ColumnWidth="50%"></f:Label>
                                                            <f:Label runat="server" ID="Label65" Text="结果" ColumnWidth="50%"></f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel30" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>

                                                            <f:TextBox ID="TextBox71" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="50%">
                                                            </f:TextBox>
                                                            <f:CheckBoxList ID="CheckBoxList5" Label="" runat="server" ColumnNumber="2" ColumnWidth="50%">
                                                                <f:CheckItem Text="合格" Value="value1" />
                                                                <f:CheckItem Text="不合格" Value="value2" />
                                                            </f:CheckBoxList>

                                                        </Items>
                                                    </f:Panel>

                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:Panel>
                                    <f:Panel ID="Panel38" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="TextBox57" Label="检验员" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="50%">
                                            </f:TextBox>
                                            <f:DatePicker ID="DatePicker3" runat="server" Label="日期" ColumnWidth="50%">
                                            </f:DatePicker>
                                        </Items>
                                    </f:Panel>

                                    <f:Label runat="server" ID="Label2" Text="螺柱折弯实验" CssClass=" f-field-body f-widget-header"></f:Label>
                                    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="StretchMax">
                                        <Items>
                                            <f:Panel ID="Panel27" BoxFlex="2" MarginRight="5px" runat="server" ShowBorder="false" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="Label49" Text="折弯方法"></f:Label>
                                                    <f:Label runat="server" ID="Label50" Text=""></f:Label>
                                                    <f:TextBox ID="TextBox45" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server">
                                                    </f:TextBox>


                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel3" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="Label3" Text="检验结果"></f:Label>
                                                    <f:Panel ID="Panel4" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>
                                                            <f:Label runat="server" ID="Label39" Text="试件Ⅰ" ColumnWidth="25%"></f:Label>
                                                            <f:Label runat="server" ID="Label40" Text="试件Ⅱ" ColumnWidth="25%"></f:Label>
                                                            <f:Label runat="server" ID="Label41" Text="试件Ⅲ" ColumnWidth="25%"></f:Label>
                                                            <f:Label runat="server" ID="Label42" Text="试件Ⅳ" ColumnWidth="25%"></f:Label>

                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel5" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>
                                                            <f:TextBox ID="TextBox39" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                            <f:TextBox ID="TextBox40" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                            <f:TextBox ID="TextBox41" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                            <f:TextBox ID="TextBox42" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="25%">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel6" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false" ShowHeader="false">
                                                <Items>
                                                    <f:Panel ID="Panel7" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>

                                                            <f:Label runat="server" ID="Label43" Text="报告编号与日期" ColumnWidth="50%"></f:Label>
                                                            <f:Label runat="server" ID="Label48" Text="结果" ColumnWidth="50%"></f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel25" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                                        <Items>

                                                            <f:TextBox ID="TextBox43" Label="" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="50%">
                                                            </f:TextBox>
                                                            <f:CheckBoxList ID="CheckBoxList4" Label="" runat="server" ColumnNumber="2" ColumnWidth="50%">
                                                                <f:CheckItem Text="合格" Value="value1" />
                                                                <f:CheckItem Text="不合格" Value="value2" />
                                                            </f:CheckBoxList>

                                                        </Items>
                                                    </f:Panel>

                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:Panel>
                                    <f:Panel ID="Panel26" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="TextBox44" Label="检验员" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="50%">
                                            </f:TextBox>
                                            <f:DatePicker ID="DatePicker4" runat="server" Label="日期" ColumnWidth="50%">
                                            </f:DatePicker>


                                        </Items>
                                    </f:Panel>

                                    <f:TextArea ID="TextArea2" Height="250px" Required="true" Label="" ShowRedStar="true" runat="server" Text="本考试机构确认该焊接操作人员按照《特种设备焊接操作人员考试细则》进行焊接操作技能考试试件检验，数据正确，记录无误。

该项目焊接操作技能考试结果评为: (合格、不合格)">
                                    </f:TextArea>
                                    <f:Panel ID="Panel28" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                        <Items>
                                            <f:TextBox ID="TextBox46" Label="考试机构负责人" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="50%">
                                            </f:TextBox>
                                            <f:DatePicker ID="DatePicker1" runat="server" Label="日期" ColumnWidth="50%">
                                            </f:DatePicker>


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
                        <%--           <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
