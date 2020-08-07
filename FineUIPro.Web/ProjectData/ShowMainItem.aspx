<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMainItem.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ShowMainItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
            <Regions>
                <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server" BodyPadding="0 5 0 0">
                    <Items>
                        <f:Panel runat="server" ID="panel2" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                            Width="400" Title="主项及设计专业" TitleToolTip="主项及设计专业" ShowBorder="false" ShowHeader="false"
                            BodyPadding="5px" IconFont="ArrowCircleLeft">
                            <Items>
                                <f:Tree ID="tvMenu" Width="580" Height="500px" EnableCollapse="true" ShowHeader="false"
                                    AutoLeafIdentification="true" EnableCheckBox="true" EnableSingleClickExpand ="true" OnNodeCheck="tvMenu_NodeCheck"
                                    runat="server">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                            <Items>
                                                <f:Button ID="btnSave" ToolTip="保存" Icon="SystemSave" runat="server" Text=""
                                                    OnClick="btnSave_Click">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                </f:Tree>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
</body>
</html>
