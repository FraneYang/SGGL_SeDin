<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.test1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        .userphoto .f-field-label {
            margin-top: 0;
        }

        .userphoto img {
            width: 100%;
        }

        .uploadbutton .f-btn {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="_form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
           <f:Panel ID="Panel8" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Form runat="server" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText" LabelWidth="90px"
                    ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Panel ID="Panel2" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox"
                            BoxConfigAlign="StretchMax">
                            <Items>
                                <f:Panel ID="Panel1" Title="面板1" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                                    ShowHeader="false">
                                    <Items>
                                        <f:Panel ID="Panel3" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="Sch2_ProjectName" Label="申请人姓名" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="Sch2_ContractId" Label="性别" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:CheckBoxList ID="CheckBoxList1" Label="申请考试性质" runat="server">
                                            <f:CheckItem Text="首次考试" Value="value1" />
                                            <f:CheckItem Text="重新考试" Value="value2" />
                                            <f:CheckItem Text="补考" Value="value3" />
                                            <f:CheckItem Text="增项" Value="value3" />
                                            <f:CheckItem Text="抽考" Value="value3" />
                                        </f:CheckBoxList>
                                        <f:Panel ID="Panel4" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="TextBox1" Label="学历" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="TextBox2" Label="邮政编码" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel6" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="TextBox3" Label="公民身份证号码" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="TextBox4" Label="联系电话" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Panel>
                                <f:Panel ID="Panel5" Title="面板1" BoxFlex="2" runat="server" ShowBorder="false" ShowHeader="false"
                                    Layout="VBox">
                                    <Items>
                                        <f:Image ID="imgPhoto" CssClass="userphoto" ImageUrl="~/res/images/blank_150.png"
                                            runat="server" BoxFlex="1">
                                        </f:Image>
                                   <%--     <f:FileUpload ID="filePhoto" CssClass="uploadbutton" runat="server" ButtonText="上传照片"
                                            ButtonOnly="true" AutoPostBack="true" OnFileSelected="filePhoto_FileSelected" Hidden="true">
                                        </f:FileUpload>--%>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:Panel>
                        <f:TextArea ID="Sch1_Opinion" Height="250px" Required="true" Label="申请操作技能考试项目" ShowRedStar="true" runat="server" Text="">
                        </f:TextArea>
                        <f:TextBox ID="TextBox5" Label="用人机构（或者培训机构）名称" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                        </f:TextBox>
                        <f:TextBox ID="TextBox6" Label="单位地址" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                        </f:TextBox>
                        <f:Panel ID="Panel7" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:TextBox ID="TextBox7" Label="单位联系人" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="TextBox8" Label="联系电话" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                            </Items>
                        </f:Panel>
                        <f:CheckBoxList ID="CheckBoxList2" Label="申请考试性质" runat="server">
                            <f:CheckItem Text="是" Value="value1" />
                            <f:CheckItem Text="否" Value="value2" />

                        </f:CheckBoxList>
                        <f:TextArea ID="TextArea1" Height="250px" Required="true" Label="工作简历" ShowRedStar="true" runat="server" Text="">
                        </f:TextArea>
                        <f:TextArea ID="TextArea2" Height="250px" Required="true" Label="用人机构（或者培训机构）意见" ShowRedStar="true" runat="server" Text="">
                        </f:TextArea>
                        <f:CheckBoxList ID="CheckBoxList3" Label="相关材料" runat="server" ColumnNumber="1">
                            <f:CheckItem Text="居民身份证（复印件，2份）" Value="value1" />
                            <f:CheckItem Text="照片（近期2寸、正面免冠、白色彩底照片，3张）" Value="value2" />
                            <f:CheckItem Text="学历证明（毕业证复印件，2份）" Value="value3" />
                            <f:CheckItem Text="安全教育和培训证明（1份）" Value="value1" />
                            <f:CheckItem Text="实习证明（1份）" Value="value2" />
                            <f:CheckItem Text="体检报告（1份，含视力、色盲等内容）" Value="value3" />
                            <f:CheckItem Text="其他" Value="value1" />
                        </f:CheckBoxList>
                        <f:Panel ID="Panel9" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                   <f:Label runat="server" ID="Label3" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></f:Label>
                                   <f:Label runat="server" ID="Label2" Text="声明：本人对所填写的内容和所提交材料的真实性负责"></f:Label>
                            </Items>
                        </f:Panel>
                        <f:TextBox ID="TextBox9" Label="申请人签字" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                        </f:TextBox>


 
                    </Items>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Left" Position="Bottom">
                    <Items>
                     <f:Label runat="server" ID="Label1" Text="注：用人单位（或者培训机构）应当明确申请人经过安全教育和培训情况，并且确认申请人独立承担焊接工作能力"></f:Label>

            <%--            <f:Button ID="Button1" IconFont="_Save" Text="保存信息" ValidateForms="Form1" ValidateMessageBox="false" runat="server">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Panel>
        <br />
        <br />
    </form>
</body>
</html>
