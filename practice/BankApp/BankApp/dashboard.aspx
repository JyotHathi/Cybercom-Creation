<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="BankApp.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <link href="css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <link href="css/StyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>
        <%-- Div Which Contain All Things --%>
        <div class="divDefaultDi">
            <h1 style="text-align: center">User Dashboard</h1>
            <br />
            <%-- To Print User Name and Logout option --%>
            <asp:Label Font-Size="Medium" Font-Bold="true" ID="lbluname" runat="server" CssClass="text-success"></asp:Label>
            <br />
            <asp:LinkButton runat="server" Font-Bold="true" ID="lbkbtnlogout" Text="Logout" OnClick="lbkbtnlogout_Click"></asp:LinkButton>
            <br />
            <br />
            <%-- Drop Down to Select Choice --%>
            <asp:UpdatePanel runat="server" ID="updtpnlSelection" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlselect">
                        <asp:DropDownList runat="server" ID="drodownSelectOption" CssClass="custom-select">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Check balance</asp:ListItem>
                            <asp:ListItem>Deposit Amount</asp:ListItem>
                            <asp:ListItem>Withdrawal Amount</asp:ListItem>
                            <asp:ListItem>Download Statement</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="opvali" ID="reqfilvalidrodownoption" ControlToValidate="drodownSelectOption" InitialValue="--Select--" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Select"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Button runat="server" ID="btnSubmit" ValidationGroup="opvali" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <%-- Panel For Input or Output Based on Choice --%>
            <%--<asp:UpdatePanel runat="server" ID="updtpnlop" UpdateMode="Conditional">
                <ContentTemplate>
            --%>        <asp:Panel runat="server" ID="pnlop">
                        <asp:Label runat="server" ID="lblopstatus"></asp:Label>
                        <asp:TextBox runat="server" ID="txtboxopdata" TextMode="Number" CssClass="form form-control" Placeholder="Enter Amount"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqfilvali" ControlToValidate="txtboxopdata" ValidationGroup="dashdata" ErrorMessage="Please Enter Amount" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator runat="server" ID="rangevali" MinimumValue="1" ValidationGroup="dashdata" Type="Integer" MaximumValue="9999999"  ControlToValidate="txtboxopdata" Display="Dynamic" ForeColor="Red" ErrorMessage="Minimum Mum Amout Atleast 1"></asp:RangeValidator>
                         <br />
                        <asp:Button runat="server"  ID="btnsubmitdata" CssClass="btn btn-primary" ValidationGroup="dashdata" OnClick="btnsubmitdata_Click" Text="Submit" />
                        <asp:Button runat="server" ID="BtnCancle" CssClass="btn btn-secondary" OnClick="BtnCancle_Click" Text="Cancle" />
                        <asp:Button runat="server" ID="BtnAll" CssClass="btn btn-success" OnClick="btnsubmitdata_Click" Text="All" />
                    </asp:Panel>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>

        </div>
    </form>
</body>
</html>
