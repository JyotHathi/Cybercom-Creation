<%@ Page Title="Doctors" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="Appointment_Booking.Doctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function viewDoctor(docId) {
            document.getElementById('<%=HidenId.ClientID%>').value = docId;
            document.getElementById('<%=HidenCommand.ClientID%>').value = "View";
            document.getElementById('<%=BtnCommands.ClientID%>').click();
        }
        function editDoctor(docId) {
            document.getElementById('<%=HidenId.ClientID%>').value = docId;
            document.getElementById('<%=HidenCommand.ClientID%>').value = "Edit";
            document.getElementById('<%=BtnCommands.ClientID%>').click();
        }
        function deleteDoctor(docId) {
            document.getElementById('<%=HidenId.ClientID%>').value = docId;
            document.getElementById('<%=HidenCommand.ClientID%>').value = "Delete";
            document.getElementById('<%=BtnCommands.ClientID%>').click();
        }
        function appointmentOfDoctor(docId) {
            document.getElementById('<%=HidenId.ClientID%>').value = docId;
            document.getElementById('<%=HidenCommand.ClientID%>').value = "ViewApointments";
            document.getElementById('<%=BtnCommands.ClientID%>').click();
        }
        $(document).ready(
            function () {
                $.ajax(
                    {
                        url: 'Doctor.aspx/DoctorsData',
                        method: 'post',
                        contentType: 'application/json',
                        dataType: 'json',
                        success: onSucess,
                        error: function (data) { alert('Error!!!'); }
                    }
                )
            }
        )
        function onSucess(data) {
            var doctors = JSON.parse(data.d);
            $('#DoctorAjaxTable').DataTable(
                {
                    data: doctors,
                    lengthMenu: [[5, 10, 15, 20, 30, -1], [5, 10, 15, 20, 30, "All"]],
                    columns: [
                        { data: 'Doctor_Name', className: 'centerText' },
                        { data: 'Doctor_Designation', className: 'centerText' },
                        {
                            data: 'Doctor_Id', orderable: false, searchable: false, className: 'centerText',
                            render: function (docId) {
                                return "<img class='actionIcon' src='/Images/Icons/appointments.png' onclick='appointmentOfDoctor(" + docId + ")' />" +
                                    "<img class='actionIcon' src='/Images/Icons/view.png' onclick='viewDoctor(" + docId + ")' />" +
                                    "<img class='actionIcon' src='/Images/Icons/edit.png' onclick='editDoctor(" + docId + ")' />" +
                                    "<img class='actionIcon' src='/Images/Icons/delete.png' onclick='deleteDoctor(" + docId + ")' />";
                            }
                        }
                    ],
                    createdRow: function (row) {
                        $(row).addClass('tr');
                    }
                }
            )
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card cardModify" id="Doctors">
        <div class="card-body">
            <!-----------------------Data------------------------------->
            <div style="display:none">
                <asp:Button runat="server" ID="BtnCommands" OnClick="BtnCommands_Click" />
                <asp:HiddenField runat="server" ID="HidenId" />
                <asp:HiddenField runat="server" ID="HidenCommand" />
            </div>
            <h1>Doctors</h1>
            <br />
            <asp:ImageButton OnClick="ImgAddDoctor_Click" runat="server" ID="ImgAddDoctor" ImageUrl="~/Images/Icons/add.png" CssClass="mainIcon" />
            <br />
            <label id="displayAlert"></label>
            <table class="table" id="DoctorAjaxTable" >
                <thead>
                    <tr class="trthead">
                        <th class="centerText">Doctor Name</th>
                        <th class="centerText">Doctor Designation</th>
                        <th class="centerText">Action</th>
                    </tr>
                </thead>
            </table>

        </div>
    </div>
    <!---------------------------------------------------------------------------------------------->

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
                    <asp:Image ID="DocImg" Width="100px" runat="server" />
                    <br />
                    <asp:Label ID="LblVDocName" Font-Bold="true" Text="Doctor's Name:" runat="server"></asp:Label>
                    <asp:Label ID="LblValDocName" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVDocDesignation" Font-Bold="true" runat="server" Text="Designation:"></asp:Label>
                    <asp:Label ID="LblVValDocDesignation" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVEmail" Font-Bold="true" runat="server" Text="Email:"></asp:Label>
                    <asp:Label ID="LblValEmail" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVMobileNumber" Font-Bold="true" runat="server" Text="Contact Number:"></asp:Label>
                    <asp:Label ID="LblValMobileNumber" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVAvailFrom" Font-Bold="true" runat="server" Text="Avail From:"></asp:Label>
                    <asp:Label ID="LblValAvailFrom" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVAvilTill" Font-Bold="true" runat="server" Text="Avail Till:"></asp:Label>
                    <asp:Label ID="LblValAvilTill" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="LblVSlot" runat="server" Font-Bold="true" Text="Slot Interval Duration"></asp:Label>
                    <asp:Label ID="LblValSlot" runat="server"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-primary">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!---------------------------------------------------------------------------------------------->

    <!--Appoinment List---------------->
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
                    <table style="width: auto" class="table">
                        <tr>
                            <th>Appointment time
                            </th>
                            <th>Patient Name
                            </th>
                        </tr>
                        <!--Repeter of Doctors Data Show-->
                        <asp:Repeater runat="server" ID="RptrListofAppointments">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text='<%#Eval("Appointment_Time") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text='<%#Eval("Patient_Name") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <!--Repeter Ends-->
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-primary">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!---------------------------------------------------------------------------------------------->

    <!--Delete Doctor---------------->
    <div class="modal" tabindex="-1" id="DeleteDoctorModal">
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
    <!---------------------------------------------------------------------------------------------->

    <!-- Update or Insert ---->
    <div class="modal fade bd-example-modal-lg" tabindex="-1" id="EditDoctorModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Manage Doctor</h5>
                    <button type="button" class="close fonts" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!-------------------------- Update Form Starts ----------------->
                    <p style="color: red">* Indicate Mandatory Field</p>
                    <br />
                    <label id="displayInUpAlert"></label>
                    <br />
                    <!--Name-->
                    <asp:Label runat="server" ID="LblUDocName" Text="Doctor's Name<b style='color:red'> *</b>" AssociatedControlID="TxtBoxUDocName"></asp:Label>
                    <asp:TextBox ID="TxtBoxUDocName" CssClass="form form-control" runat="server" Placeholder="Enter Doctor's Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TxtBoxUDocName" ID="ReqFilValiUDocName" runat="server" ErrorMessage="Please Enter Doctor's Name" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocUpdate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegexFilValiUDocName" runat="server" ErrorMessage="Please Enter Name Properly, Alphabets and Space Only" ControlToValidate="TxtBoxUDocName" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocUpdate" ValidationExpression="^([a-zA-Z]+\s)*[a-zA-Z]+"></asp:RegularExpressionValidator>
                    <br />

                    <!--Mobile Number-->
                    <asp:Label runat="server" ID="LblUMobNum" Text="Mobile Number<b style='color:red'> *</b>" AssociatedControlID="TxtBoxUMobNum"></asp:Label>
                    <asp:TextBox ID="TxtBoxUMobNum" Placeholder="Enter Mobile Number" TextMode="Phone" CssClass="form form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqFilValiUMobNum" runat="server" ErrorMessage="Please Enter Mobile Number" ControlToValidate="TxtBoxUMobNum" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocUpdate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegexFilValiUMobnum" ValidationExpression="^((\+){1}91){1}[1-9]{1}[0-9]{9}$" runat="server" ErrorMessage="Please Enter Mobile Number Properly, +91xxxxxxxxx" ControlToValidate="TxtBoxUMobNum" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocUpdate"></asp:RegularExpressionValidator>
                    <br />

                    <!--Email-->
                    <asp:Label runat="server" ID="LblUEmail" Text="Email <b style='color:red'> *</b>" AssociatedControlID="TxtBoxUEmail"></asp:Label>
                    <asp:TextBox ID="TxtBoxUEmail" TextMode="Email" PlaceHolder="Enter Your Email" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqFilValiUEmail" runat="server" ErrorMessage="Please Enter Email" ControlToValidate="TxtBoxUEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocUpdate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegexFilValiUEmail" runat="server" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ErrorMessage="Please Enter Valid Email" ControlToValidate="TxtBoxUEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocUpdate"></asp:RegularExpressionValidator>
                    <br />

                    <!--Designation-->
                    <asp:Label runat="server" ID="LblUDesignation" Text="Designation<b style='color:red'> *</b>" AssociatedControlID="DroDownUDesignation"></asp:Label>
                    <asp:DropDownList runat="server" ID="DroDownUDesignation" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="DroDownUDesignation" ID="ReqFilValiUDesignation" runat="server" ErrorMessage="Please Select Designation" InitialValue="-1" ValidationGroup="valiDocUpdate" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <br />

                    <!--From Time-->
                    <asp:Label runat="server" Text="Available From Time<b style='color:red'> *</b>" ID="LblFromUTime" AssociatedControlID="TxtBoxFromuTime"></asp:Label>
                    <asp:TextBox ID="TxtBoxFromUTime" TextMode="Time" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TxtBoxFromUTime" ForeColor="Red" Display="Dynamic" ValidationGroup="valiDocUpdate" ID="ReqFilValiUFromTime" runat="server" ErrorMessage="Please Select Available Time"></asp:RequiredFieldValidator><br />

                    <!--To Time-->
                    <asp:Label runat="server" AssociatedControlID="TxtBoxUToTime" ID="LblUToTime" Text="Available Till<b style='color:red'> *</b>"></asp:Label>
                    <asp:TextBox ID="TxtBoxUToTime" CssClass="form-control" TextMode="Time" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TxtBoxUToTime" ForeColor="Red" Display="Dynamic" ValidationGroup="valiDocUpdate" ID="ReqFilvaliUToTime" runat="server" ErrorMessage="Please Select Available Till Time"></asp:RequiredFieldValidator><br />
                    <asp:CustomValidator ControlToValidate="TxtBoxUToTime" ForeColor="Red" Display="Dynamic" ID="CusValiUToTime" OnServerValidate="CusValiUToTime_ServerValidate" ValidationGroup="valiDocUpdate" runat="server" ErrorMessage="Please Select Time Properly,Greater Then Avil From Time"></asp:CustomValidator>
                    <br />

                    <!--Slot-->
                    <asp:Label runat="server" ID="LbluSlot" Text="Slot Interval Duration<b style='color:red'> *</b>" AssociatedControlID="DroDownUSlot"></asp:Label>
                    <asp:DropDownList runat="server" ID="DroDownUSlot" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="DroDownUSlot" ID="ReqFilValiSlotTime" runat="server" ErrorMessage="Please Select SlotTimings" InitialValue="-1" ValidationGroup="valiDocUpdate" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <br />

                    <!--Image-->
                    <asp:Label ID="LbluPhoto" runat="server" Text="Photo<b style='color:red'> *</b>"></asp:Label>
                    <asp:FileUpload ID="FileUpldUPhoto" CssClass="form-control" runat="server" />
                    <asp:CustomValidator ValidationGroup="valiDocUpdate" ValidateEmptyText="true" ControlToValidate="FileUpldUPhoto" ForeColor="Red" Display="Dynamic" ID="CusvALIUPhoto" OnServerValidate="CusValiUPhoto_ServerValidate" runat="server" ErrorMessage="Please Upload Photo in .png  / .jpg and Size Less Then 50kb"></asp:CustomValidator>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnUpdate" Visible="false" CssClass="btn btn-primary" OnClick="BtnUpdate_Click" ValidationGroup="valiDocUpdate" runat="server" Text="Update" />
                    <asp:Button ID="BtnSubmit" Visible="false" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" ValidationGroup="valiDocUpdate" runat="server" Text="Submit" />
                    <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" Visible="false" CssClass="btn btn-secondary" />
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                </div>
                <!-------------------------------Update Form Ends--------------------->
            </div>
        </div>
    </div>
    <!------------------------------------------------------------->

    <script type="text/javascript">

        // For Mobile Number and Email 
        document.getElementById('<%= TxtBoxUMobNum.ClientID%>').addEventListener('change', CheckStatusofUser.bind(event, 1));
        document.getElementById('<%= TxtBoxUEmail.ClientID%>').addEventListener('change', CheckStatusofUser.bind(event, 2));

        function CheckStatusofUser(condition) {
            var inputData;
            if (condition === 1) {
                inputData = [document.getElementById('<%= TxtBoxUMobNum.ClientID%>').value, ""];
            }
            else if (condition === 2) {
                inputData = ["", document.getElementById('<%= TxtBoxUEmail.ClientID%>').value];
            }
            $.ajax({
                url: "Doctor.aspx/IsUserExits",
                data: "{'mobileNumber':'" + inputData[0] + "','email':'" + inputData[1] + "'}",
                type: "POST",
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {
                    var result = data.d;
                    if (result === true) {
                        document.getElementById("displayInUpAlert").style.display = 'block';
                        document.getElementById("displayInUpAlert").className = 'alert alert-danger';
                        document.getElementById("displayInUpAlert").innerHTML = 'User Already Exists !!';
                    }
                    else {
                        document.getElementById("displayInUpAlert").style.display = 'none';
                    }
                },
                error: function (data) {
                    alert("Error Occured");
                }
            });
        }

        


    </script>
</asp:Content>
