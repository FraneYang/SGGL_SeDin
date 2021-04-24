<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test3.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.test3" %>

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
                                        <f:Panel ID="Panel6" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="TextBox3" Label="身份证号码" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                                <f:TextBox ID="TextBox4" Label="文化程度" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                                </f:TextBox>
                                            </Items>
                                        </f:Panel>
                                        <f:TextBox ID="TextBox10" Label="工作单位" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
                                        <f:TextBox ID="TextBox11" Label="工作单位地址" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                        </f:TextBox>
 
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
                        <f:TextBox ID="TextBox12" Label="通信地址" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                        </f:TextBox>
                        <f:Panel ID="Panel4" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:TextBox ID="TextBox1" Label="邮政编码" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="TextBox2" Label="联系电话" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel10" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:TextBox ID="TextBox13" Label="申请作业项目" Margin="0 5 0 0" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="TextBox14" Label="申请项目代号" Required="true" ShowRedStar="true" ColumnWidth="50%" runat="server">
                                </f:TextBox>
                            </Items>
                        </f:Panel>


                        <f:TextArea ID="TextArea1" Height="200px" Required="true" Label="工作简历" ShowRedStar="true" runat="server" Text="">
                        </f:TextArea>

                        <f:CheckBoxList ID="CheckBoxList3" Label="相关材料" runat="server" ColumnNumber="1">
                            <f:CheckItem Text="身份证明（复印件1份）" Value="value1" />
                            <f:CheckItem Text="学历证明（毕业证复印件1份）" Value="value3" />
                            <f:CheckItem Text="体检报告（1份，相应大纲考试有要求的）" Value="value3" />

                        </f:CheckBoxList>
                        <f:TextArea ID="TextArea2" Height="200px" Required="true" Label="用人单位意见" ShowRedStar="true" runat="server" Text="">
                        </f:TextArea>
                        <f:TextArea ID="TextArea3" Height="200px" Required="true" Label="" ShowRedStar="true" runat="server" Text="本人声明，以上填写信息及所提交的资料均合法、真实、有效，并承诺对填写的内容负责">
                        </f:TextArea>

                

                        <f:Panel ID="Panel28" Layout="Column" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:TextBox ID="TextBox46" Label="申请人签字" Margin="0 5 0 0" ShowRedStar="true" runat="server" ColumnWidth="50%">
                                </f:TextBox>
                                <f:DatePicker ID="DatePicker1" runat="server" Label="日期" ColumnWidth="50%">
                                </f:DatePicker>


                            </Items>
                        </f:Panel>

                    </Items>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Left" Position="Bottom">
                    <Items>
                        <f:Label runat="server" ID="Label1" Text="注：申请人在网上申请的，填写申请表后打印盖章签字并扫描上传"></f:Label>

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
