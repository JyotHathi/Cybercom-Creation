<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="BankApp.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <link href="css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <link href="css/StyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <%-- Div Which Contain Registration Form Input --%>
        <div class="divRegi">
            <h1>Registration Form</h1>
            <br />
            <br />
            <%-- Form Input Controls --%>
            <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox runat="server" ID="txtboxName" Placeholder="Enter Name" CssClass="form form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="regivalid" runat="server" ID="reqfilvalitxtboxName" ControlToValidate="txtboxName" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Name"></asp:RequiredFieldValidator>
            

            <br />
            <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number"></asp:Label>
            <asp:TextBox runat="server" ID="txtboxmobilenumber" TextMode="Phone" Placeholder="Enter Mobile Number" CssClass="form form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="regivalid" runat="server" ID="reqfilvalitxtboxmobilenumber" ControlToValidate="txtboxmobilenumber" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Mobile Number"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup="regivalid" runat="server" ID="regexfilvalitxtboxmobilenumber" ControlToValidate="txtboxmobilenumber" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Mobile Number Properly" ValidationExpression="^[0-9]{10}$" ></asp:RegularExpressionValidator>

            <br />
            <asp:Label ID="lblPin" runat="server" Text="PIN (4-Digit)"></asp:Label>
            <asp:TextBox runat="server" ID="txtboxpin" TextMode="Password" Placeholder="4-Digit PIN" CssClass="form form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="regivalid" runat="server" ID="reqfilvalitxtboxpin" ControlToValidate="txtboxpin" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter PIN"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup="regivalid" runat="server" ID="regexfilvalitxtboxpin" ControlToValidate="txtboxpin" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter PIN Properly" ValidationExpression="^[1-9][0-9]{3}$" ></asp:RegularExpressionValidator>

            <br />
            <asp:Label ID="lblAmount" runat="server" Text="Intial Deposit"></asp:Label>
            <asp:TextBox runat="server" ID="txtboxamount" TextMode="Number" Placeholder="Enter Initial Deposit Amount" CssClass="form form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="regivalid" runat="server" ID="reqfilvalitxtboxamount" ControlToValidate="txtboxamount" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Intial Deposit Amount"></asp:RequiredFieldValidator>
            <asp:RangeValidator ValidationGroup="regivalid" runat="server" ID="regexfilvalitxtboxamount" ControlToValidate="txtboxamount" Display="Dynamic" ForeColor="Red" ErrorMessage="Minimum Deposit 1000" MinimumValue="1000" MaximumValue="999999999" ></asp:RangeValidator>

            <br />
            <asp:Label ID="lblMinWithDrawAmount" runat="server" Text="Minimum Withdrawal Amount"></asp:Label>
            <asp:TextBox runat="server" ID="txtboxMinWithDrawAmount" TextMode="Number" Placeholder="Minimum Withdrawal Amount" CssClass="form form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="regivalid" runat="server" ID="reqfilvalitxtboxMinWithDrawAmount" ControlToValidate="txtboxMinWithDrawAmount" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Minimum Withdrawal Amount"></asp:RequiredFieldValidator>
            <asp:RangeValidator ValidationGroup="regivalid" runat="server" ID="regexfilvalitxtboxMinWithDrawAmount" ControlToValidate="txtboxMinWithDrawAmount" Display="Dynamic" ForeColor="Red" ErrorMessage="Amount Must Between 100 - 5000" MinimumValue="100" MaximumValue="5000" ></asp:RangeValidator>
            <br />

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="regivalid" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary" OnClick="btnReset_Click" />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
