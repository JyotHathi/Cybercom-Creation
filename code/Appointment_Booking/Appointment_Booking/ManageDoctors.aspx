<%@ Page Title="Manage Doctors" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ManageDoctors.aspx.cs" Inherits="Appointment_Booking.ManageDoctors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card cardModify" id="ManageDoctors">
        <div class="card-body">
            <h1>Manage Doctors</h1>
            <br />
            <p style="color: red">* Indicate Mandatory Field</p>
            <br />
            <br />
            <!--Name-->
            <asp:Label runat="server" ID="LblDocName" Text="Doctor's Name<b style='color:red'> *</b>" AssociatedControlID="TxtBoxDocName"></asp:Label>
            <asp:TextBox ID="TxtBoxDocName" CssClass="form form-control" runat="server" Placeholder="Enter Doctor's Name"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="TxtBoxDocName" ID="ReqFilValiDocName" runat="server" ErrorMessage="Please Enter Doctor's Name" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocInsert"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegexFilValiDocName" runat="server" ErrorMessage="Please Enter Name Properly, Alphabets and Space Only" ControlToValidate="TxtBoxDocName" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocInsert" ValidationExpression="^([a-zA-Z]+\s)*[a-zA-Z]+$"></asp:RegularExpressionValidator>
            <br />

            <!--Mobile Number-->
            <asp:Label runat="server" ID="LblMobNum" Text="Mobile Number<b style='color:red'> *</b>" AssociatedControlID="TxtBoxMobNum"></asp:Label>
            <asp:TextBox ID="TxtBoxMobNum" Placeholder="Enter Mobile Number" TextMode="Phone" CssClass="form form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqFilValiMobNum" runat="server" ErrorMessage="Please Enter Mobile Number" ControlToValidate="TxtBoxMobNum" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocInsert"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegexFilValiMobnum" ValidationExpression="^((\+){1}91){1}[1-9]{1}[0-9]{9}$" runat="server" ErrorMessage="Please Enter Mobile Number Properly(+91xxxxxxxxx)" ControlToValidate="TxtBoxMobNum" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocInsert"></asp:RegularExpressionValidator>
            <br />

            <!--Email-->
            <asp:Label runat="server" ID="LblEmail" Text="Email <b style='color:red'> *</b>" AssociatedControlID="TxtBoxEmail"></asp:Label>
            <asp:TextBox ID="TxtBoxEmail" TextMode="Email" PlaceHolder="Enter Your Email" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqFilValiEmail" runat="server" ErrorMessage="Please Enter Email" ControlToValidate="TxtBoxEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocInsert"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegexFilValiEmail" runat="server" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ErrorMessage="Please Enter Valid Email" ControlToValidate="TxtBoxEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="valiDocInsert"></asp:RegularExpressionValidator>
            <br />

            <!--Designation-->
            <asp:Label runat="server" ID="LblDesignation" Text="Designation<b style='color:red'> *</b>" AssociatedControlID="DroDownDesignation"></asp:Label>
            <asp:DropDownList runat="server" ID="DroDownDesignation" CssClass="form-control"></asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="DroDownDesignation" ID="ReqFilValiDesignation" runat="server" ErrorMessage="Please Select Designation" InitialValue="-1" ValidationGroup="valiDocInsert" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <br />

            <!--From Time-->
            <asp:Label runat="server" Text="Available From Time<b style='color:red'> *</b>" ID="LblFromTime" AssociatedControlID="TxtBoxFromTime"></asp:Label>
            <asp:TextBox ID="TxtBoxFromTime" TextMode="Time" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="TxtBoxFromTime" ForeColor="Red" Display="Dynamic" ValidationGroup="valiDocInsert" ID="ReqFilValiFromTime" runat="server" ErrorMessage="Please Select Available Time"></asp:RequiredFieldValidator><br />
            <asp:CustomValidator ControlToValidate="TxtBoxFromTime" ForeColor="Red" Display="Dynamic" ID="CusValiFromTime" OnServerValidate="CusValiFromTime_ServerValidate" ValidationGroup="valiDocInsert" runat="server" ErrorMessage="Please Select Time Properly (After 8:00AM Only)"></asp:CustomValidator>

            <!--To Time-->
            <asp:Label runat="server" AssociatedControlID="TxtBoxToTime" ID="LblToTime" Text="Available Till<b style='color:red'> *</b>"></asp:Label>
            <asp:TextBox ID="TxtBoxToTime" CssClass="form-control" TextMode="Time" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="TxtBoxToTime" ForeColor="Red" Display="Dynamic" ValidationGroup="valiDocInsert" ID="ReqFilvaliToTime" runat="server" ErrorMessage="Please Select Available Till Time"></asp:RequiredFieldValidator><br />
            <asp:CustomValidator ValidationGroup="valiDocInsert" ControlToValidate="TxtBoxToTime" ForeColor="Red" Display="Dynamic" ID="CusValiToTime" OnServerValidate="CusValiToTime_ServerValidate" runat="server" ErrorMessage="Please Select Time Properly, Time After 8:00AM Only And Greater Then From Time"></asp:CustomValidator>
            <br />

            <!--Image-->
            <asp:Label ID="LblPhoto" runat="server" Text="Photo<b style='color:red'> *</b>"></asp:Label>
            <asp:FileUpload ID="FileUpldPhoto" CssClass="form-control" runat="server" />
            <asp:CustomValidator ValidationGroup="valiDocInsert" ValidateEmptyText="true" ControlToValidate="FileUpldPhoto" ForeColor="Red" Display="Dynamic" ID="CustomValidator1" OnServerValidate="CusValiPhoto_ServerValidate" runat="server" ErrorMessage="Please Upload Photo in .png  / .jpg and Size Less Then 50kb"></asp:CustomValidator>
            <br />
            <br />


            <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" ValidationGroup="valiDocInsert" CssClass="btn btn-primary" runat="server" Text="Submit" />
            <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" CssClass="btn btn-secondary" />
            <br />
            <br />
            <!---------------Data---------------------->
            <table>
                <thead>
                    <tr>
                        <th>Doctor Name</th>
                        <th>Doctor Designation</th>
                        <th></th>
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
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Doctor_Id")%>' CommandName="Edit">Edit</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Doctor_Id")%>' CommandName="Delete">Delete</asp:LinkButton>
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
                    <asp:Label ID="LblImage" Text="Image:" Font-Bold="true" runat="server"></asp:Label>
                    <asp:Image ID="DocImg" Width="50px" runat="server" />
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
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-primary">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Update ---->
    <div class="modal" tabindex="-1" id="EditDoctorModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modal title</h5>
                    <button type="button" class="close fonts" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p style="color: red">* Indicate Mandatory Field</p>
                    <br />
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
                    <asp:CustomValidator ControlToValidate="TxtBoxFromuTime" ForeColor="Red" Display="Dynamic" ID="CusValiuFromTime" OnServerValidate="CusValiFromUTime_ServerValidate" ValidationGroup="valiDocUpdate" runat="server" ErrorMessage="Please Select Time Properly, Time Must be After 8:00 AM"></asp:CustomValidator>

                    <!--To Time-->
                    <asp:Label runat="server" AssociatedControlID="TxtBoxUToTime" ID="LblUToTime" Text="Available Till<b style='color:red'> *</b>"></asp:Label>
                    <asp:TextBox ID="TxtBoxUToTime" CssClass="form-control" TextMode="Time" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TxtBoxUToTime" ForeColor="Red" Display="Dynamic" ValidationGroup="valiDocUpdate" ID="ReqFilvaliUToTime" runat="server" ErrorMessage="Please Select Available Till Time"></asp:RequiredFieldValidator><br />
                    <asp:CustomValidator ValidationGroup="valiUDocUpdate" ControlToValidate="TxtBoxUToTime" ForeColor="Red" Display="Dynamic" ID="CusValiUToTime" OnServerValidate="CusValiUToTime_ServerValidate" runat="server" ErrorMessage="Please Select Time Properly, Time Must be After 8:00AM & Greater Then From Time"></asp:CustomValidator>
                    <br />

                    <!--Image-->
                    <asp:Label ID="LbluPhoto" runat="server" Text="Photo<b style='color:red'> *</b>"></asp:Label>
                    <asp:FileUpload ID="FileUpldUPhoto" CssClass="form-control" runat="server" />
                    <asp:CustomValidator ValidationGroup="valiDocUpdate" ValidateEmptyText="true" ControlToValidate="FileUpldUPhoto" ForeColor="Red" Display="Dynamic" ID="CusvALIUPhoto" OnServerValidate="CusValiUPhoto_ServerValidate" runat="server" ErrorMessage="Please Upload Photo in .png  / .jpg and Size Less Then 50kb"></asp:CustomValidator>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" OnClick="BtnUpdate_Click" ValidationGroup="valiDocUpdate" runat="server" Text="Update" />
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Delete ---->
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

</asp:Content>
