<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BankApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <link href="css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <link href="css/StyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <%-- Div Which Contains Input Field for Login --%>
        <div class="divDefaultDi">
            <h1 style="text-align:center">Login</h1>
            <br />
            <br />
            <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number"></asp:Label>
            <asp:TextBox runat="server" ID="txtboxmobilenumber" TextMode="Phone" Placeholder="Enter Mobile Number" CssClass="form form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="regivalid" runat="server" ID="reqfilvalitxtboxmobilenumber" ControlToValidate="txtboxmobilenumber" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Mobile Number"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup="loginvali" runat="server" ID="regexfilvalitxtboxmobilenumber" ControlToValidate="txtboxmobilenumber" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Mobile Number Properly" ValidationExpression="^[0-9]{10}$" ></asp:RegularExpressionValidator>

            <br />
            <asp:Label ID="lblPin" runat="server" Text="PIN (4-Digit)"></asp:Label>
            <asp:TextBox runat="server" ID="txtboxpin" TextMode="Password" Placeholder="4-Digit PIN" CssClass="form form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="regivalid" runat="server" ID="reqfilvalitxtboxpin" ControlToValidate="txtboxpin" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter PIN"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup="loginvali" runat="server" ID="regexfilvalitxtboxpin" ControlToValidate="txtboxpin" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter PIN Properly" ValidationExpression="^[1-9][0-9]{3}$" ></asp:RegularExpressionValidator>

            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="loginvali" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
