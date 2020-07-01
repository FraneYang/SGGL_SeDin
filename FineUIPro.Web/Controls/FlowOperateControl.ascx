<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlowOperateControl.ascx.cs" Inherits="FineUIPro.Web.Controls.FlowOperateControl" %>
<f:Panel ID="Panel1" runat="server" BodyPadding="0px" ShowBorder="false"
    ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
    <Items>
        <f:Panel ID="Panel3" AutoScroll="true" runat="server" ShowBorder="false" ShowHeader="false">               
            <Items>                    
                <f:GroupPanel runat="server" Title="审核流程" BodyPadding="1px" ID="GroupPanel1" 
                    EnableCollapse="True" Collapsed="false" EnableCollapseEvent="true" OnCollapse="GroupPanel_Collapse"
                    EnableExpandEvent="true" OnExpand="GroupPanel_Expand">
                <Items>
                    <f:Form ID="Form1" ShowBorder="false" ShowHeader="false"  AutoScroll="true"
                        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtOpinions" runat="server" Label="我的意见" LabelAlign="Right" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="13% 25% 45% 17%">
                                <Items>
                                    <f:DropDownList ID="rblFlowOperate" runat="server" MarginLeft="35px" OnSelectedIndexChanged="rblFlowOperate_SelectedIndexChanged" AutoPostBack="true">
                                        <f:ListItem Value="1" Text="下一步" Selected="true" />
                                        <f:ListItem Value="2" Text="审批完成" />
                                    </f:DropDownList>
                                    <f:TextBox runat="server" Label="办理步骤" ID="txtAuditFlowName"></f:TextBox>                                                           
                                    <f:DropDownList ID="drpPerson" runat="server" Label="办理人员" EnableEdit="true">
                                    </f:DropDownList>                                                                  
                                    <%--<f:CheckBox ID="IsFileCabinetA" Hidden="true" runat="server" Text="【文件柜A】" ></f:CheckBox>--%>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>                      
                </Items>
            </f:GroupPanel>
            <f:GroupPanel ID="GroupPanel2" Title="审核意见" runat="server" EnableCollapse="True" Collapsed="true" 
                        EnableCollapseEvent="true" OnCollapse="GroupPanel2_Collapse" AutoScroll="true"
                        EnableExpandEvent="true" OnExpand="GroupPanel2_Expand" Height="160px">
                <Items>                      
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                        EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="FlowOperateId" DataIDField="FlowOperateId" 
                        AllowSorting="true" SortField="SortIndex" SortDirection="ASC" AllowPaging="true"
                        EnableTextSelection="True">                           
                        <Columns>
                            <f:RenderField  HeaderText="序号" ColumnID="SortIndex" DataField="SortIndex" FieldType="Int" 
                                HeaderTextAlign="Center" TextAlign="Center" Width="50px">
                            </f:RenderField>
                            <f:RenderField  HeaderText="审批步骤" ColumnID="AuditFlowName" DataField="AuditFlowName" FieldType="String" 
                                HeaderTextAlign="Center" TextAlign="Left" Width="180px">
                            </f:RenderField>
                            <f:RenderField  HeaderText="处理人" ColumnID="OperaterName" DataField="OperaterName" FieldType="String" 
                                HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>   
                            <f:RenderField HeaderText="处理时间" ColumnID="OperaterTime" DataField="OperaterTime"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                                HeaderTextAlign="Center" TextAlign="Center" Width="100px" >
                            </f:RenderField> 
                            <f:RenderField  HeaderText="意见" ColumnID="Opinion" DataField="Opinion" FieldType="String" 
                                HeaderTextAlign="Center" TextAlign="Left" Width="90px" ExpandUnusedSpace="true">
                            </f:RenderField>                             
                        </Columns>
                    </f:Grid>
                </Items>
              </f:GroupPanel>                          
            </Items>
        </f:Panel>            
    </Items>
</f:Panel>