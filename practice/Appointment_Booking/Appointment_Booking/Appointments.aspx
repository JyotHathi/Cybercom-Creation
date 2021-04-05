<%@ Page Title="Appoinments" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="Appointment_Booking.Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        // To Load Data When Page Get Loaded
        $(document).ready(
            function ()
            {
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July',
                    'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
                $('#appointmentsAjaxTable').DataTable(
                    {
                        bServerSide: true,
                        sAjaxSource: "AppointmentBooking/GetAppointments",
                        sServerMethod: 'post',
                        createdRow: function (row) {
                            $(row).addClass('tr')
                        },
                        columns: [
                            {
                                'data': 'Doctor_Name',
                                'className': 'centerText'
                            },
                            {
                                'data': 'Appointment_Date',
                                'render': function (apdate) {
                                    var appDate = new Date(parseInt(apdate.substr(6)));
                                    return appDate.getDate() + '-' + months[appDate.getMonth()] + '-' + appDate.getFullYear();
                                },
                                'className': 'centerText'
                            },
                            { 'data': 'Appointment_Time', 'className': 'centerText' },
                            { 'data': 'Patient_Name', 'className': 'centerText' },
                            {
                                'data': 'Appointment_Id',
                                'className': 'centerText', 'searchable': false,
                                orderable: false,
                                render: function (apoId) {
                                    return "<img class='actionIcon' src='/Images/Icons/edit.png' onclick='editAppointment(" + apoId + ")' />" +
                                        "<img class='actionIcon' src='/Images/Icons/delete.png' onclick='deleteAppontment(" + apoId + ")' />";
                                }
                            }
                        ],
                        lengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "ALL"]]
                    });
            }
        );

        // To Handle Deletion
        function deleteAppontment(apoId) {
            document.getElementById('<%=HidenId.ClientID%>').value = apoId;
            document.getElementById('<%=HidenCommand.ClientID%>').value = "Delete";
            document.getElementById('<%=BtnCommands.ClientID%>').click();
        }

        // To Edit The Data
        function editAppointment(apoId) {
            document.getElementById('<%=HidenId.ClientID%>').value = apoId;
            document.getElementById('<%=HidenCommand.ClientID%>').value = "Edit";
            document.getElementById('<%=BtnCommands.ClientID%>').click();
        }
        // To Handle Doctor Change
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card cardModify" id="Appoinments">
        <div class="card-body">
            <h1>Appointments</h1>
            <br />
            <a data-target="" data-toggle="" id=""></a>
            <asp:ImageButton OnClick="ImgAppointments_Click" runat="server" ID="ImgAppointments" ImageUrl="~/Images/Icons/add.png" CssClass="mainIcon" />
            <br />
            <label id="displayAlert"></label>
            <br />
            <div style="display: none">
                <asp:Button runat="server" ID="BtnCommands" OnClick="BtnCommands_Click" />
                <asp:HiddenField runat="server" ID="HidenId" />
                <asp:HiddenField runat="server" ID="HidenCommand" />
            </div>
            <!-----------Data--------->
            <table class="table" id="appointmentsAjaxTable">
                <thead>
                    <tr class="trthead">
                        <th class="centerText">Appointment With</th>
                        <th class="centerText">Appointment Date</th>
                        <th class="centerText">Appointment Time</th>
                        <th class="centerText">Patient Name</th>
                        <th class="centerText">Action</th>
                    </tr>
                </thead>
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
    <!-- Insert and Update ---->
    <div class="modal fade bd-example-modal-lg" tabindex="-1" id="AppoinymentModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Manage Appointments</h5>
                    <button type="button" class="close fonts" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <!-------------------------- Form Starts ------------------------>
                    <p style="color: red">* Indicate Mandatory Field</p>
                    <br />
                    <label id="displayAlertAppointment"></label>
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
                    <asp:CustomValidator runat="server" ID="CompareDate" ErrorMessage="Please Select date Properly" ValidationGroup="valiAppointments" ControlToValidate="TxtBoxDate" Display="Dynamic" ForeColor="Red" OnServerValidate="CompareDate_ServerValidate"></asp:CustomValidator>
                    <br />
                    <br />
                    <!----With---->
                    <asp:Label ID="LblDoctor" runat="server" Text="Appointment With<b style='color:red'> *</b>"></asp:Label>
                    <asp:DropDownList ID="DroDrownDoctor" AutoPostBack="true" OnSelectedIndexChanged="DroDrownDoctor_SelectedIndexChanged" CssClass="form-control" runat="server" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ReqValiAptWith" runat="server" ErrorMessage="Please Select Doctor" ControlToValidate="DroDrownDoctor" InitialValue="-1" Display="Dynamic" ForeColor="Red" ValidationGroup="valiAppointments"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <!---Time--->
                    <asp:Label ID="Label3" runat="server" Text="Time Slots<b style='color:red'> *</b>"></asp:Label>
                    <asp:DropDownList ID="DrowDownSlots" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="DrowDownSlots" ID="ReqValiTime" runat="server" ErrorMessage="Please Select Time Slot" InitialValue="-1" Display="Dynamic" ForeColor="Red" ValidationGroup="valiAppointments"></asp:RequiredFieldValidator>
                    <br />

                    <!--------------------------------------------------------------->
                    <div class="modal-footer">
                        <asp:Button ID="BtnSubmit" Visible="false" ValidationGroup="valiAppointments" OnClick="BtnSubmit_Click" CssClass="btn btn-primary" runat="server" Text="Submit" />
                        <asp:Button ID="BtnUpdate" Visible="false" ValidationGroup="valiAppointments" OnClick="BtnUpdate_Click" CssClass="btn btn-primary" runat="server" Text="Update" />
                        <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" Visible="false" CssClass="btn btn-secondary" />
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                    </div>
                    <!-------------------------------Update Form Ends--------------------->
                </div>
            </div>
        </div>
    </div>
    <!----------------------------------------------------------------------------->
    
</asp:Content>
