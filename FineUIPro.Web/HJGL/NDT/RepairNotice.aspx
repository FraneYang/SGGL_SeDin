<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairNotice.aspx.cs" Inherits="FineUIPro.Web.HJGL.NDT.RepairNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />

        <f:Panel runat="server" ID="panel1" RegionPosition="Center" ShowBorder="true" Layout="VBox"
            ShowHeader="false" IconFont="PlusCircle" Title="返修通知单" TitleToolTip="返修通知单" AutoScroll="true"
            BoxConfigAlign="Stretch" >
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server" Position="Top">
                    <Items>
                        <f:Button ID="btnSubmit" Text="提交" ToolTip="提交返修通知单" ValidateForms="SimpleForm1"
                            Icon="SystemSave" runat="server" OnClick="btnSubmit_Click">
                        </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="txtPipeCode" Label="管线号" runat="server" LabelWidth="75px">
                                </f:Label>
                                <f:Label ID="txtWeldJointCode" Label="焊口号" runat="server" LabelWidth="75px">
                                </f:Label>
                                 <f:Label ID="txtRepairLocation" Label="底片号" runat="server" LabelWidth="75px">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                 <f:Label ID="txtRepairMark" Label="返修标记" runat="server" LabelWidth="75px">
                                </f:Label>
                                <f:Label ID="txtWelder" Label="施焊焊工" runat="server" LabelWidth="75px">
                                </f:Label>
                                <f:Label ID="txtJudgeGrade" Label="合格等级" runat="server" LabelWidth="75px">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Label ID="txtCheckDefects" Label="缺陷性质" runat="server" LabelWidth="75px">
                                </f:Label>
                                <f:FileUpload ID="filePhoto" runat="server" ButtonText="上传底片" ButtonOnly="true"
                                    AutoPostBack="true" OnFileSelected="filePhoto_FileSelected">
                                </f:FileUpload>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                              <f:Image ID="imgPhoto" CssClass="userphoto" runat="server" BoxFlex="1" MarginLeft="10px" Height="400px">
                                </f:Image>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>

        </f:Panel>
    </form>
    <f:Window ID="Window1" Title="打印焊缝返修通知单" Hidden="true" EnableIFrame="true" EnableMaximize="false"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="600px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
