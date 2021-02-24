<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BankApp._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default Page</title>
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <link href="css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <link href="css/StyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <%-- Div Which Contain Select Option Create or Login --%>
        <div class="divDefaultDi">
            <h1>Please Select :</h1>
            <%-- Drop Dwon To Select  --%>
            <asp:DropDownList runat="server" ID="drodownSelectOption" CssClass="custom-select">
                <asp:ListItem>--Select--</asp:ListItem>
                <asp:ListItem>Register</asp:ListItem>
                <asp:ListItem>Login</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ValidationGroup="opvali" ID="reqfilvalidrodownoption" ControlToValidate="drodownSelectOption" InitialValue="--Select--" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Select"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button runat="server" ID="btnSubmit" ValidationGroup="opvali" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
        </div>
    </form>
</body>
</html>
