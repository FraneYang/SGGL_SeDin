<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateLawRegulationIdentify.aspx.cs" Inherits="FineUIPro.Web.HSSE.InformationProject.CreateLawRegulationIdentify" %>
<!DOCTYPE html>  
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编制法律法规清单</title>
   <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="true" ShowHeader="false" Layout="VBox"
        Margin="5px" BodyPadding="5px">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="false" ShowHeader="false" Title="法律法规清单" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="LawRegulationId" AllowCellEditing="true" 
                EnableColumnLines="true" ClicksToEdit="1" DataIDField="LawRegulationId" AllowSorting="true"
                SortField="LawRegulationCode" EnableCheckBoxSelect="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:TextBox runat="server" EmptyText="按编号查询" AutoPostBack="True" Label="编号" LabelWidth="70px"
                                Width="250px" ID="txtLawRegulationCode" OnTextChanged="TextBox_TextChanged" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox runat="server" EmptyText="按级别查询" AutoPostBack="True" Label="级别" LabelWidth="70px"
                                Width="250px" ID="txtLawsRegulationsTypeName" OnTextChanged="TextBox_TextChanged" LabelAlign="Right">
                            </f:TextBox>
                             <f:TextBox runat="server" EmptyText="按名称查询" AutoPostBack="True" Label="名称" LabelWidth="70px"
                                Width="250px" ID="txtLawRegulationName" OnTextChanged="TextBox_TextChanged" LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="Accept" runat="server" ToolTip="确认选中项" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RenderField Width="80px" ColumnID="LawRegulationCode" DataField="LawRegulationCode"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                
                    <f:RenderField Width="100px" ColumnID="LawsRegulationsTypeName" DataField="LawsRegulationsTypeName"
                        FieldType="String" HeaderText="级别" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="LawRegulationName" DataField="LawRegulationName"
                        FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ApprovalDate" DataField="ApprovalDate" SortField="ApprovalDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="批准日"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="生效日"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField Width="400px" HeaderText="简介及重点关注条款" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Description" ExpandUnusedSpace="true">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ShortDescription") %>' ToolTip='<%#Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                   <%-- <f:RenderField Width="225px" ColumnID="FitPerfomance" DataField="FitPerfomance" SortField="FitPerfomance"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="符合性评审" ExpandUnusedSpace="true">
                        <Editor>
                            <f:TextBox ID="txtFitPerfomance" MaxLength="100" Text='<%# Eval("FitPerfomance")%>'
                                runat="server">
                            </f:TextBox>
                        </Editor>
                    </f:RenderField>--%>
                     <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" 
                          Text="附件" ToolTip="附件上传查看" DataIFrameUrlFields="LawRegulationId" DataIFrameUrlFormatString="../../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/LawRegulation&menuId=F4B02718-0616-4623-ABCE-885698DDBEB1">
                      </f:WindowField>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
