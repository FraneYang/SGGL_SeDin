<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUrl17.aspx.cs" Inherits="FineUIPro.Web.PHTGL.ContractCompile.AttachUrl17" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑附件</title>
    <style>
        .widthBlod {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="编辑附件17" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="Label1" runat="server" Text="附件17    保密协议书" CssClass="widthBlod"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
                            
                            <f:Label runat="server" ID="Label26" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;根据《中华人民共和国民法典》、《中华人民共和国建筑法》及有关法律规定，遵循平等、自愿、公平和诚实信用的原则，双方就本合同执行过程中和本合同执行终止后约定如下保密协议："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label2" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、除非对方书面同意，合同双方应保密，不应把对方直接或间接提供的与合同有关的文件、数据或信息泄露给第三方。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label3" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、施工分包商可以把从总承包商处得到的相关文件、数据和其他资料在其履行分包合同时将分包工程范围内所需要的相关文件、数据和其他资料提供给其分包单位，同时施工分包商应从其分包单位处获得类似的保密保证，且施工分包商对其分包单位的保密义务承担连带责任。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label4" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;以下情形，不属于违反保密义务："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label5" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（1）	非分包合同当事人过失，现在或以后公开的资料；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label6" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（2）	能够证明在泄密出现时已为一方所有，并且不是以前直接或间接从其他方获得的资料；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label7" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（3）	在无保密义务下从合同当事人之外的第四方合法转到合同对方的资料。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label8" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;分包合同解除或终止的，保密条款继续有效。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label9" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、双方均不能将从对方处获得的文件、数据和其他资料用于履行本合同之外的其它用处。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label10" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4、上述1-3项条款所规定的任何一方义务并不适应以下资料："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label11" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.1非哪一方过失，现在或以后公开的资料；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label12" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.2能够证明在泄密出现时已为某一方所有，并且不是以前直接或间接从其他方获得的资料；"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label13" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.3在无保密义务下从第三方合法转到另一方的资料。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label14" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5、因施工分包商原因双方解除合同时，总承包商及其指定的其他施工包商有权使用贵公司施工文件中他们认为合适的部分。"></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label15" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6、协议有效期限：自合同签定至工程竣工验收后三年内。"></f:Label>
                            <br />
                            <br />
                            <br />
                            <f:Label runat="server" ID="Label16" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总承包商：                                          施工分包商："></f:Label>
                            <br />
                            <f:Label runat="server" ID="Label17" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（公章或合同专用章）                                         （公章或合同专用章）"></f:Label>
                            <f:Label runat="server" ID="Label18" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;法定代表人或其委托代理人：                                         法定代表人或其委托代理人："></f:Label>
                            <br />
                            <br />
                            <f:Label runat="server" ID="Label27" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期：    年    月    日" LabelAlign="Left"></f:Label>

                            </f:ContentPanel>
                        

                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:ToolbarFill runat="server">
                        </f:ToolbarFill>
                        <%--<f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                            OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>--%>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>
    </form>
</body>
</html>
