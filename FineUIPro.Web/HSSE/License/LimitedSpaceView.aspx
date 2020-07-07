<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LimitedSpaceView.aspx.cs"
    Inherits="FineUIPro.Web.HSSE.License.LimitedSpaceView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>受限空间作业票</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .formtitle .f-field-body {
            text-align: center;           
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" TitleAlign="Center"
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>
             <f:FormRow>
                 <Items>
                   <f:Label ID="lbLicenseCode" runat="server" Label="编号" LabelWidth="120px">
                    </f:Label>
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtApplyUnit" runat="server" Label="申请单位" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                      <f:TextBox ID="txtApplyManName" runat="server" Label="申请人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>                    
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtWorkPalce" runat="server" Label="作业地点" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtFireWatchManName" runat="server" Label="监护人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
              <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtWorkMeasures" runat="server" Label="作业内容" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtWorkDate" runat="server" Label="作业时间" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>          
             <f:FormRow>
                 <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="检查措施" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="LicenseItemId" EnableColumnLines="true"
                        DataIDField="LicenseItemId" AllowSorting="true" SortField="SortIndex"
                        SortDirection="ASC" AllowPaging="false" >                       
                        <Columns>  
                             <f:RenderField Width="90px" ColumnID="SortIndex" DataField="SortIndex" FieldType="Int"
                                HeaderText="序号" HeaderTextAlign="Center" TextAlign="Left" >
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="SafetyMeasures" DataField="SafetyMeasures" FieldType="String"
                                HeaderText="检查单" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                            </f:RenderField>                            
                            <f:RenderField Width="100px" ColumnID="ConfirmManName" DataField="ConfirmManName" FieldType="String"
                                HeaderText="确认执行" HeaderTextAlign="Center" TextAlign="Left" >
                            </f:RenderField>  
                            <f:RenderField Width="100px" ColumnID="NoUsedName" DataField="NoUsedName" FieldType="String"
                                HeaderText="不适用" HeaderTextAlign="Center" TextAlign="Left" >
                            </f:RenderField>  
                        </Columns>
                    </f:Grid>
                </Items>
             </f:FormRow>            
             <f:FormRow>
                 <Items>
                   <f:Form ID="txtForm1" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="施工单位安全人员意见：">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                 <f:TextArea ID="txtOpinion1" runat="server"  Readonly="true" Height="50px">
                                 </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow >
                            <Items>
                                  <f:Label ID="Label6" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtName1" runat="server" Label="签字" LabelWidth="60px">
                                  </f:Label>
                                  <f:Label ID="txtTime1" runat="server" Text="年月日时分">
                                  </f:Label>
                            </Items>
                        </f:FormRow>
                     </Rows>
                    </f:Form>
                    <f:Form ID="txtForm2" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="总包单位安全人员意见：">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                     <f:TextArea ID="txtOpinion2" runat="server"  Readonly="true" Height="50px">
                                     </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow >
                                <Items>
                                      <f:Label ID="Label1" runat="server" >
                                      </f:Label>
                                      <f:Label ID="txtName2" runat="server" Label="签字" LabelWidth="60px">
                                      </f:Label>
                                      <f:Label ID="txtTime2" runat="server" Text="年月日时分">
                                      </f:Label>
                                </Items>
                            </f:FormRow>
                         </Rows>
                        </f:Form>
                     </Items>
             </f:FormRow>  
            <f:FormRow>
              <Items>
                <f:Form ID="txtForm3" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="总包单位专业工程师意见：">
                    <Rows>
                    <f:FormRow>
                        <Items>
                            <f:TextArea ID="txtOpinion3" runat="server"  Readonly="true" Height="50px">
                            </f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>
                            <f:Label ID="Label2" runat="server" >
                            </f:Label>
                            <f:Label ID="txtName3" runat="server" Label="签字" LabelWidth="60px">
                            </f:Label>
                            <f:Label ID="txtTime3" runat="server" Text="年月日时分">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="txtForm4" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="总包单位施工经理意见：">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextArea ID="txtOpinion4" runat="server"  Readonly="true" Height="50px">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow >
                            <Items>
                                <f:Label ID="Label5" runat="server" >
                                </f:Label>
                                <f:Label ID="txtName4" runat="server" Label="签字" LabelWidth="60px">
                                </f:Label>
                                <f:Label ID="txtTime4" runat="server" Text="年月日时分">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>
                   <f:Form ID="txtForm5" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="监理单位意见：">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextArea ID="txtOpinion5" runat="server"  Readonly="true" Height="50px">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow >
                            <Items>
                                <f:Label ID="Label9" runat="server" >
                                </f:Label>
                                <f:Label ID="txtName5" runat="server" Label="签字" LabelWidth="60px">
                                </f:Label>
                                <f:Label ID="txtTime5" runat="server" Text="年月日时分">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                     </Rows>
                    </f:Form>
                    <f:Form ID="txtForm6" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="业主单位意见：">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                     <f:TextArea ID="txtOpinion6" runat="server"  Readonly="true" Height="50px">
                                     </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow >
                                <Items>
                                    <f:Label ID="Label12" runat="server" >
                                    </f:Label>
                                    <f:Label ID="txtName6" runat="server" Label="签字" LabelWidth="60px">
                                    </f:Label>
                                    <f:Label ID="txtTime6" runat="server" Text="年月日时分">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                         </Rows>
                        </f:Form>
                     </Items>
                 </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtCance" runat="server" Label="取消" Readonly="true" LabelWidth="90px">
                        </f:TextBox>
                    </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtClose" runat="server" Label="关闭" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>       
         <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        </f:Form>
         <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
            Height="500px">
        </f:Window>
        </form>
    </body>
</html>
