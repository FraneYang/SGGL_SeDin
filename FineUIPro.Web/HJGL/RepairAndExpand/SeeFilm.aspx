<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeFilm.aspx.cs" Inherits="FineUIPro.Web.HJGL.RepairAndExpand.SeeFilm" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />

        <f:Panel runat="server" ID="panel1" RegionPosition="Center" ShowBorder="true" Layout="VBox"
            ShowHeader="false" IconFont="PlusCircle" Title="返修片子" TitleToolTip="返修片子" AutoScroll="true"
            BoxConfigAlign="Stretch" >
            
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                    <Rows>
                        <f:FormRow>
                            <Items>
                              <f:Image ID="imgPhoto" CssClass="userphoto" runat="server" BoxFlex="1" MarginLeft="10px" Height="640px" Width="1150">
                                </f:Image>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>

        </f:Panel>
    </form>
</body>
</html>
