<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Response.aspx.cs" Inherits="FineUIPro.Web.HSSE.Hazard.Response" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>响应</title>
       <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="260" Title="职业健康安全危险源辨识与评价" TitleToolTip="职业健康安全危险源辨识与评价"
                ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="tvHazardTemplate" Width="250px" Height="550px" EnableCollapse="true"
                        ShowBorder="false" ShowHeader="false" AutoScroll="true" Expanded="true" Title="危险源辨识与评价清单"
                        AutoLeafIdentification="true" runat="server" EnableTextSelection="True" OnNodeCommand="tvHazardTemplate_NodeCommand">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="危险源辨识与评价清单明细"
                TitleToolTip="危险源辨识与评价清单明细" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="HazardId" EnableColumnLines="true"
                        DataIDField="HazardId" AllowSorting="true" SortField="HazardId" SortDirection="ASC">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="选择" HeaderTextAlign="Center" TextAlign="Center"  EnableLock="true" Locked="true">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbHazard" runat="server" />
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="130px" HeaderText="危险源代码" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHazardId" runat="server" Text='<%# ConvertHazardCode(Eval("HazardId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="160px" HeaderText="工作阶段" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# ConvertWorkStage(Eval("WorkStage")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="工作阶段" HeaderTextAlign="Center" TextAlign="Left" Hidden="true">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdWorkStage" runat="server" Value='<%#Bind("WorkStage") %>' />
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="HazardItems" DataField="HazardItems" SortField="HazardItems"
                                FieldType="String" HeaderText="危险因素明细" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DefectsType" DataField="DefectsType" SortField="DefectsType"
                                FieldType="String" HeaderText="缺陷类型" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="MayLeadAccidents" DataField="MayLeadAccidents"
                                SortField="MayLeadAccidents" FieldType="String" HeaderText="可能导致的事故" TextAlign="Left"
                                HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HelperMethod" DataField="HelperMethod"
                                SortField="HelperMethod" FieldType="String" HeaderText="辅助方法" TextAlign="Left"
                                HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField Width="100px" HeaderText="危险评价(L)" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="HazardJudge_L">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("HazardJudge_L") %>' ToolTip='<%# Bind("HazardJudge_L") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="100px" HeaderText="危险评价(E)" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="HazardJudge_E">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("HazardJudge_E") %>' ToolTip='<%# Bind("HazardJudge_E") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="100px" HeaderText="危险评价(C)" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="HazardJudge_C">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("HazardJudge_C") %>' ToolTip='<%# Bind("HazardJudge_C") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="100px" HeaderText="危险评价(D)" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="HazardJudge_D">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("HazardJudge_D") %>' ToolTip='<%# Bind("HazardJudge_D") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="100px" HeaderText="危险级别" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="HazardLevel">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("HazardLevel") %>' ToolTip='<%# Bind("HazardLevel") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="150px" HeaderText="控制措施" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="ControlMeasures">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("ControlMeasures") %>' ToolTip='<%# Bind("ControlMeasures") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="PromptTime" DataField="PromptTime" FieldType="String"
                                HeaderText="提示时间" HeaderTextAlign="Center" TextAlign="Left" RendererFunction="renderPromptTime">
                            </f:RenderField>     
                            <f:TemplateField Width="100px" HeaderText="响应" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnResponse" runat="server" CommandArgument='<%# Bind("HazardId") %>'
                                        OnClick="lbtnResponse_click" CommandName="response" ToolTip="响应" ></asp:LinkButton>
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("WorkStage") %>' />
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                    </f:Grid>
                </Items>
                <Items>
                <f:Label ID="lb1" runat="server" Text="注 1:D值>320,为极其危险，不能继续作业；D值在160-320之间，为高度危险，要立即整改；D值在70-160之间，为显著危险，需要整改；D值在20-70之间，为一般危险，需要注意；D值<20，为稍有危险，可以接受   2:辅助方法判断依据：Ⅰ：不符合法律法规及其他要求；Ⅱ：曾发生过事故，仍未采取有效措施；Ⅲ：相关方合理抱怨或要求；Ⅳ：直接观察到的危险；Ⅴ：定量评价LEC法"></f:Label>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <f:Window ID="Window1" Title="响应记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="400px" Height="250px">
    </f:Window>
    <script type="text/javascript">
        function renderPromptTime(value) {
            var returnValue = null;
            if (value == 1) {
                returnValue = '一周';
            } else if (value == 2) {
                returnValue = '两周';
            } else if (value == 3) {
                returnValue = '三周';
            } else if (value == 4) {
                returnValue = '四周';
            } else if (value == 5) {
                returnValue = '五周';
            }
            return returnValue;
        }
    </script>
</body>
</html>
