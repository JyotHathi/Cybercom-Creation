<%@ Page Title="Doctors" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="Appointment_Booking.Doctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card cardModify" id="Doctors">
        <div class="card-body">
            <!---------------Data---------------------->
            <table>
                <thead>
                    <tr>
                        <th>Doctor Name</th>
                        <th>Doctor Designation</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="RptrDoctors" OnItemCommand="RptrDoctors_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text='<%#Eval("Doctor Name") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text='<%#Eval("Doctor_Designation") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" Text="View" CommandArgument='<%#Eval("Doctor_Id")%>' CommandName="View">View</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Doctor_Id")%>' CommandName="ViewApointments">View Todays Apointments</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    <!---------------Modal Popup------------------>
    <!-- view ---->
    <div class="modal" tabindex="-1" id="ViewDoctorModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Doctor Details</h5>
                    <button type="button" class="close fonts" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="LblImage" Text="Image:" runat="server"></asp:Label>
                    <asp:Image ID="DocImg" Width="50px" runat="server" />
                    <br />
                    <asp:Label ID="LblVDocName" Text="Doctor's Name:" runat="server"></asp:Label>
                    <asp:Label ID="LblValDocName" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVDocDesignation" runat="server" Text="Designation:"></asp:Label>
                    <asp:Label ID="LblVValDocDesignation" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:Label ID="LblValEmail" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVMobileNumber" runat="server" Text="Contact Number:"></asp:Label>
                    <asp:Label ID="LblValMobileNumber" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVAvailFrom" runat="server" Text="Avail From:"></asp:Label>
                    <asp:Label ID="LblValAvailFrom" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVAvilTill" runat="server" Text="Avail Till:"></asp:Label>
                    <asp:Label ID="LblValAvilTill" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-primary">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" tabindex="-1" id="AppoinmentList">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Appointment List</h5>
                    <button type="button" class="close fonts" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table style="width:auto">
                        <tr>
                            <th>
                                Appointment time
                            </th>
                            <th>
                                Patient Name
                            </th>
                        </tr>
                    <asp:Repeater runat="server" ID="RptrListofAppointments">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text='<%#Eval("Time") %>'></asp:Label>
                                </td>
                                <td>
                                     <asp:Label runat="server" Text='<%#Eval("PatientName") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                        </table>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-primary">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
