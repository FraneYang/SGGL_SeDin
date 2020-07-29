<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectInformation.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .info {
            padding: 10px;
        }

            .info .item {
                display: -webkit-flex;
                display: flex;
                display: -moz-box;
                display: -ms-flexbox;
                color: #000;
                font-size: 14px;
                letter-spacing: 1px;
                line-height: 22px;
            }

                .info .item .val {
                    -webkit-flex: 1;
                    -ms-flex: 1;
                    -moz-box-flex: 1;
                    min-width: 0;
                    overflow: hidden;
                    text-overflow: ellipsis;
                    white-space: nowrap;
                    line-height: 22px
                }

        .bg-item-fix {
            padding: 0 10px 10px;
            box-sizing: border-box;
        }

        .bg-item:last-child {
            margin-bottom: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Left"
                    Title="项目施工综合信息" BodyPadding="0 5 0 0" Width="300px" Layout="Fit" runat="server"
                    EnableCollapse="true">
                    <Items>
                        <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                            EnableCollapse="true" Title="项目列表" ShowBorder="true" Layout="Fit"
                            ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                            <Items>
                                <f:Tree ID="tvControlItem" ShowHeader="false" Title="项目列表" OnNodeCommand="tvControlItem_NodeCommand"
                                    runat="server" Width="290px" Height="600px" ShowBorder="false" EnableCollapse="true" EnableSingleClickExpand="true"
                                    AutoLeafIdentification="true" EnableSingleExpand="true" EnableTextSelection="true">
                                </f:Tree>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="Fit"
                    BoxConfigAlign="Stretch" BodyPadding="0 5 0 0" BoxConfigPosition="Left" runat="server">
                    <Items>
                        <f:Panel ID="Panel1" IsFluid="true" CssClass="mytable blockpanel" runat="server" Height="600px" ShowBorder="false"
                            Layout="Region" ShowHeader="false">
                            <Items>
                                <f:Panel ID="Panel7" IsFluid="true" RegionPosition="Left" Width="400px" CssClass="mytable blockpanel" runat="server" Height="600px" ShowBorder="false"
                                    Layout="VBox" ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="Panel8" Title="Panel1" Height="400px"
                                            runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                                            <Items>
                                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false">
                                                    <div>
                                                        <div class="bg-item">
                                                            <div class="info">
                                                                <div class="row">
                                                                    <div class="item">
                                                                        <div class="tit">项目名称：</div>
                                                                        <div class="val">测试项目</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">项目地址：</div>
                                                                        <div class="val">盘原地XXX弄XX号</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">监理单位：</div>
                                                                        <div class="val">单位二</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">总承包单位：</div>
                                                                        <div class="val">单位三</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">施工分包单位：</div>
                                                                        <div class="val">单位四</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">总承包单位：</div>
                                                                        <div class="val">单位五</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">总承包单位：</div>
                                                                        <div class="val">单位六</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">项目合同额：</div>
                                                                        <div class="val">1.28亿</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">合同开工时间：</div>
                                                                        <div class="val">2020-01-02</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">合同竣工时间：</div>
                                                                        <div class="val">2022-01-02</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">项目类型：</div>
                                                                        <div class="val">EPC</div>
                                                                    </div>
                                                                    <div class="item">
                                                                        <div class="tit">项目状态：</div>
                                                                        <div class="val">在建</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <table style="width: 400px; margin: 0px auto; position: fixed" id="LogOnTable" runat="server"
                                                        border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="height: 50px;"></td>
                                                        </tr>
                                                        <tr style="height: 60px">
                                                            <td style="width: 110px"></td>
                                                            <td style="width: 110px"></td>
                                                            <td style="width: 150px">
                                                                <f:Button runat="server" ID="IntoProject" Text="进入项目" Width="150px" Height="50px" OnClick="IntoProject_Click"></f:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </f:ContentPanel>

                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel9" Title="Panel2" Width="430px" Height="270px"
                                            runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                                            <Items>
                                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" Width="430px">
                                                    <div style="margin: 0 auto; width: 150px;">项目主要管理团队</div>

                                                </f:ContentPanel>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel2" CssClass="mytable blockpanel" RegionPosition="Right" Width="700px" IsFluid="true" runat="server" Height="600px" ShowBorder="false"
                                    Layout="Table" TableConfigColumns="2" ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="Panel3" Title="Panel2" Width="700px" Height="330px"
                                            TableColspan="2" runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                                            <Items>
                                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" Width="700px">
                                                    <div style="margin: 0 auto; width: 100px;">进度信息</div>
                                                    <div style="margin: 0 auto; padding-bottom: 300px; width: 500px;">
                                                        <div style="padding-top: 100px; float: left">赢得值曲线</div>
                                                        <div style="padding-top: 100px; float: right">形象进度图片</div>
                                                    </div>

                                                </f:ContentPanel>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel4" Title="Panel3" Width="350px" Height="340px"
                                            runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                                            <Items>
                                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" Width="350px">
                                                    <div style="margin: 0 auto; width: 100px;">安全管理</div>

                                                </f:ContentPanel>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel5" Title="Panel4" Width="350px" Height="340px"
                                            runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                                            <Items>
                                                <f:ContentPanel runat="server" ShowHeader="false" ShowBorder="false" Width="350px">
                                                    <div style="margin: 0 auto; width: 100px;">质量管理</div>

                                                </f:ContentPanel>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
</body>
</html>
