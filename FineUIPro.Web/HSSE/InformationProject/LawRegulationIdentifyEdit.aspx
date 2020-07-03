<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LawRegulationIdentifyEdit.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.InformationProject.LawRegulationIdentifyEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编制法律法规辨识记录</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
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
                    <f:TextBox ID="txtLawRegulationIdentifyCode" runat="server" Label="编号" Readonly="true"
                        FocusOnPageLoad="true" MaxLength="30" LabelWidth="70px">
                    </f:TextBox>
                    <f:DatePicker runat="server" Label="实施日期" ID="txtCheckDate" 
                        EnableEdit="true" FocusOnPageLoad="true" LabelWidth="80px">
                    </f:DatePicker>
                    <f:TextBox ID="txtCompileMan" runat="server" Label="整理人" Readonly="true" LabelWidth="70px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="法律法规清单" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="LawRegulationId,LawRegulationSelectedItemId"
                        Height="240px" AllowCellEditing="true" EnableColumnLines="true" ClicksToEdit="1"
                        DataIDField="LawRegulationSelectedItemId" AllowSorting="true" SortField="LawRegulationCode">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnReSet" runat="server" Icon="ShapeSquareSelect" ToolTip="选择" OnClick="btnReSet_Click">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                    <f:HiddenField runat="server" ID="hdLawRegulationId"></f:HiddenField>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>  
                            <f:RenderField Width="70px" ColumnID="LawRegulationCode" DataField="LawRegulationCode"
                                FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="LawsRegulationsTypeId" DataField="LawsRegulationsTypeName"
                                FieldType="String" HeaderText="级别" HeaderTextAlign="Center">
                                <Editor>
                                    <f:DropDownList ID="drpLawsRegulationsType" runat="server">
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="LawRegulationName" DataField="LawRegulationName"
                                FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="ApprovalDate" DataField="ApprovalDate" SortField="ApprovalDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="批准日"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="生效日"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField ColumnID="tfDescription" Width="300px" HeaderText="简介及重点关注条款" HeaderTextAlign="Center"
                                TextAlign="Left" SortField="Description"  ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ShortDescription") %>'
                                        ToolTip='<%#Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="FitPerfomance" DataField="FitPerfomance" SortField="FitPerfomance"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="符合性评审" >
                                <Editor>
                                    <f:TextBox ID="txtFitPerfomance" MaxLength="100" Text='<%# Bind("FitPerfomance")%>'
                                        runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="1px" ColumnID="LawRegulationId" DataField="LawRegulationId" 
                                FieldType="String" HeaderText="主键"  Hidden="true" HeaderTextAlign="Center">                                        
                            </f:RenderField>
                             <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" 
                                 Text="附件" ToolTip="附件上传查看" DataIFrameUrlFields="LawRegulationId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/LawRegulation&menuId=F4B02718-0616-4623-ABCE-885698DDBEB1"/>                           
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" MaxLength="150" Label="备注" Height="70px" LabelWidth="70px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="法律法规清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1200px" Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
