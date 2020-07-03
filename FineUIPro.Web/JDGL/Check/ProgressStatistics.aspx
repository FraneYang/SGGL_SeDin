<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgressStatistics.aspx.cs" Inherits="FineUIPro.Web.JDGL.Check.ProgressStatistics" %>
<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .labcenter {
            text-align: center;
        }

        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }

        .f-grid-row.red {
            background-color: #FF7575;
            background-image: none;
        }

        .Yellow {
            background-color: #FFFF93;
        }

        .fontred {
            color: #FF7575;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" AjaxAspnetControls="divEV" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="true" Title="施工进度赢得值曲线" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpProjectType" runat="server" Label="单位工程类别"  AutoPostBack="true" OnSelectedIndexChanged="drpProjectType_SelectedIndexChanged" LabelAlign="Right" EnableEdit="true" LabelWidth="110px" EnableMultiSelect="true">
                            <f:ListItem Text="建筑工程" Value="1" />
                            <f:ListItem Text="安装工程" Value="2" />
                        </f:DropDownList>
                        <f:DropDownList ID="drpUnitWork" runat="server" Label="单位工程" LabelAlign="Right" EnableMultiSelect="true" EnableEdit="true"
                            AutoPostBack="true" OnSelectedIndexChanged="drpUnitWork_SelectedIndexChanged">
                        </f:DropDownList>
                        <f:CheckBox runat="server" ID="ckb1" Text="按“实体合格”计量进度" Label="" Checked="true" Enabled="false"></f:CheckBox>
                        <f:CheckBox runat="server" ID="ckbData" Text="按“资料合格”计量进度"></f:CheckBox>
                        
                        
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:RadioButtonList runat="server" ID="rblType" Label="统计方式">
                            <f:RadioItem Text="月" Value="Month" Selected="true" />
                            <f:RadioItem Text="周" Value="Week" Selected="true" />
                            <f:RadioItem Text="天" Value="Day" Selected="true" />
                        </f:RadioButtonList>
                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                            LabelAlign="right">
                        </f:DatePicker>
                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                            LabelAlign="right">
                        </f:DatePicker>
                        <f:Button ID="btnQuery" OnClick="btnQuery_Click" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" runat="server" >
                                </f:Button>
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
        <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="false"
            Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle"
            TitleToolTip="" AutoScroll="true">
            <Items>
                <f:ContentPanel ShowHeader="false" runat="server" ID="cpEV" Margin="0 0 0 0">
                    <div id="divEV" style="height:580px; width:1300px;">
                        <uc1:ChartControl ID="ChartEV" runat="server" />
                    </div>
                </f:ContentPanel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
