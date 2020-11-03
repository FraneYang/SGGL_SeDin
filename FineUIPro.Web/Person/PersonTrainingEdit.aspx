<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonTrainingEdit.aspx.cs" Inherits="FineUIPro.Web.Person.PersonTrainingEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑应急流程</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="培训计划" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtTrainingPlanCode" runat="server" Label="编号" LabelAlign="Right" ShowRedStar="true"
                            MaxLength="50" Readonly="true">
                        </f:TextBox>
                        <f:TextBox ID="txtTrainingPlanTitle" runat="server" Label="标题" Required="true" ShowRedStar="true"
                            LabelAlign="Right" MaxLength="50" Readonly="true">
                        </f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DatePicker runat="server" Label="开始时间" ID="txtStartTime" Readonly="true"></f:DatePicker>
                        <f:DatePicker runat="server" Label="结束时间" ID="txtEndTime" Readonly="true"></f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" ID="txtTrainingPlanContent" Label="主要内容" MaxLength="50" Readonly="true"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:LinkButton ID="UploadAttach" runat="server" Label="培训课件" Text="上传和查看" OnClick="btnAttachUrl_Click" LabelAlign="Right">
                                    </f:LinkButton>
                        <f:Label runat="server" ></f:Label>
                        <f:HiddenField runat="server" ID="hdTrainingPlanId"></f:HiddenField>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" Title="员工" AutoScroll="true" BodyPadding="5px"
                            runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:Grid ID="gvPerson" ShowBorder="true" ShowHeader="false" EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true" runat="server" BoxFlex="1" 
                                            DataIDField="UserId" AllowSorting="true" PageSize="1000" ForceFit="true"
                                            SortDirection="ASC" EnableTextSelection="True" Height="200px"
                                            EnableRowDoubleClickEvent="true" >
                                            <Columns>
                                                <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                                                <f:RenderField MinWidth="100px" ColumnID="UserName" DataField="UserName"
                                                    FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Center">
                                                </f:RenderField>
                                                <f:RenderField Width="120px" ColumnID="PostTitleName" DataField="PostTitleName"
                                                    FieldType="String" HeaderText="职称" HeaderTextAlign="Center" TextAlign="Center">
                                                </f:RenderField>
                                                <f:TemplateField ColumnID="Certificate" Width="160px" HeaderText="职业资格证书" HeaderTextAlign="Center" TextAlign="Center"
                                                    EnableLock="true" Locked="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertCertificateName(Eval("UserId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </f:TemplateField>
                                                <f:TemplateField ColumnID="WorkPost" Width="330px" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Center"
                                                    EnableLock="true" Locked="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# ConvertWorkPostName(Eval("WorkPostId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </f:TemplateField>
                                            </Columns>
                                        </f:Grid>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>
                </f:FormRow>
                <f:FormRow >
                    <Items>
                        <f:RadioButtonList runat="server" ID="rdbIsAgree" Label="是否同意" ShowRedStar="true" LabelWidth="110px" AutoPostBack="true" OnSelectedIndexChanged="rdbIsAgree_SelectedIndexChanged">
                            <f:RadioItem Text="同意" Value="true" Selected="true" />
                            <f:RadioItem Text="不同意" Value="false" />
                        </f:RadioButtonList>
                        <f:Label runat="server" ID="txtHandelMan" Label="打回制定人" Hidden="true"></f:Label>
                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                            OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
        <f:Window ID="Window1" Title="员工培训计划" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="800px"
            Height="650px" >
        </f:Window>
         <f:Window ID="Window2" Title="文件上传" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="false" runat="server" IsModal="true" Width="680px"
        Height="480px">
    </f:Window>
    </form>
</body>
</html>
