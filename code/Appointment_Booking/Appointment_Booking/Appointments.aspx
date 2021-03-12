<%@ Page Title="Appoinments" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="Appointment_Booking.Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card cardModify" id="Appoinments">
        <div class="card-body">
            <!----------------------------Form Starts--------------------->
            <h1>Appointments</h1>
            <br />
            <p style="color: red">* Indicate Mandatory Field</p>
            <br />
            <br />
            <!--APatient Name-->
            <asp:Label ID="LblPtName" runat="server" Text="Patient Name<b style='color:red'> *</b>"></asp:Label>
            <asp:TextBox ID="TxtBoxPtName" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="TxtBoxPtName" Display="Dynamic" ForeColor="Red" ValidationGroup="valiAppointments" ID="ReqvaliPtName" runat="server" ErrorMessage="Please Enter Patient Name"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="TxtBoxPtName" Display="Dynamic" ForeColor="Red" ValidationExpression="^([a-zA-Z]+\s)*[a-zA-Z]+$" ID="RegexValiPtName" runat="server" ValidationGroup="valiAppointments" ErrorMessage="Please Enetr Name Properly"></asp:RegularExpressionValidator>
            <br />
            <br />
            <!---Date---->
            <asp:Label ID="LblDate" runat="server" Text="Appointment Date<b style='color:red'> *</b>"></asp:Label>
            <asp:TextBox ID="TxtBoxDate" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqValiDate" runat="server" ErrorMessage="Please Enter Date" ValidationGroup="valiAppointments" ControlToValidate="TxtBoxDate" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            <!----With---->
            <asp:Label ID="LblDoctor" runat="server" Text="Appointment With<b style='color:red'> *</b>"></asp:Label>
            <asp:DropDownList ID="DroDrownDoctor" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DroDrownDoctor_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqValiAptWith" runat="server" ErrorMessage="Please Select Doctor" ControlToValidate="DroDrownDoctor" InitialValue="-1" Display="Dynamic" ForeColor="Red" ValidationGroup="valiAppointments"></asp:RequiredFieldValidator>
            <br />
            <br />
            <!---Time--->
            <asp:Label ID="Label3" runat="server" Text="Time Slots<b style='color:red'> *</b>"></asp:Label>
            <asp:DropDownList ID="DrowDownSlots" CssClass="form-control" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="DrowDownSlots" ID="ReqValiTime" runat="server" ErrorMessage="ReqValiTime" InitialValue="-1" Display="Dynamic" ForeColor="Red" ValidationGroup="valiAppointments"></asp:RequiredFieldValidator>
            <br />

            <br />
            <br />

            <asp:Button ID="BtnSubmit" ValidationGroup="valiAppointments" OnClick="BtnSubmit_Click" CssClass="btn btn-primary" runat="server" Text="Submit" />
            <!-------------------------Form End------------------------------------>
            <br />
            <br />
            <!-----------Data--------->
            <table>
                <thead>
                    <tr class="trthead">
                        <th>Appointment With</th>
                        <th>Appointment Date</th>
                        <th>Appointment Time</th>
                        <th>Patient Name</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <!-----Repeter Starts--------->
                    <asp:Repeater runat="server" ID="RptrAppointments" OnItemCommand="RptrAppointments_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text='<%#Eval("[Doctor Name]") %>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" Text='<%#Convert.ToDateTime(Eval("Appointment_Date")).ToString("dd-MMM-yyyy") %>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" Text='<%#Eval("Appointment_Time") %>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" Text='<%#Eval("Patient_Name") %>'></asp:Label></td>
                                <td>
                                    <asp:LinkButton runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%#Eval("Appointment_Id")%>'></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <!-----Repeter Ends----------->
                </tbody>
            </table>
            <!---------------------------------------Data Filling Ends------------>
        </div>
    </div>

     <!-- Delete Modal Poup Confirmation ---->
    <div class="modal" tabindex="-1" id="DeleteAppoinmentModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Warning For Deletion</h5>
                    <button type="button" class="close fonts" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p style="color: red">Are You Sure?</p>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-danger" runat="server" ID="BtnWarningYes" OnClick="BtnWarningYes_Click" Text="Yes" />
                    <button type="button" class="btn btn-primary" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>
    <!------------------------------------------------------------->
</asp:Content>
