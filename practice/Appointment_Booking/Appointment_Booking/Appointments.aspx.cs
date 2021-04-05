using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Newtonsoft.Json.Serialization;
using System.Web.Helpers;

namespace Appointment_Booking
{
    public partial class Appointments : System.Web.UI.Page
    {
        AppointmentMaster appointmentMaster = new AppointmentMaster();
        DoctorMaster doctor = new DoctorMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert",
                    "document.getElementById('displayAlert').style.display = 'none';", true);
                if (Session["DoctorsData2"] == null)
                {
                    LoadDoctorsDropDown();
                }
                else
                {
                    DroDrownDoctor.DataTextField = "Doctor Name";
                    DroDrownDoctor.DataValueField = "Doctor_Id";
                    DroDrownDoctor.DataSource = (DataTable)Session["DoctorsData2"];
                    DroDrownDoctor.DataBind();
                    ListItem listItem = new ListItem("Select Doctor", "-1");
                    DroDrownDoctor.Items.Add(listItem);
                    DroDrownDoctor.SelectedValue = "-1";
                    DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
                }
            }

        }

        #region Load or Clear Controls

        // To Clear All The Controls
        protected void ClearControls()
        {
            TxtBoxDate.Text = "";
            TxtBoxPtName.Text = "";
            DroDrownDoctor.SelectedValue = "-1";
            DrowDownSlots.Items.Clear();
            DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
            TxtBoxPtName.Enabled = true;

        }
        // To Fill DropDown Of Doctor
        private void LoadDoctorsDropDown()
        {
            DataSet ds2 = doctor.SelectDoctors(true);
            if (ds2 != null)
            {
                Session["DoctorsData2"] = ds2.Tables[0];
                DroDrownDoctor.DataTextField = "Doctor Name";
                DroDrownDoctor.DataValueField = "Doctor_Id";
                DroDrownDoctor.DataSource = ds2.Tables[0];
                DroDrownDoctor.DataBind();
                ListItem listItem = new ListItem("Select Doctor", "-1");
                DroDrownDoctor.Items.Add(listItem);
                DroDrownDoctor.SelectedValue = "-1";
                DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert",
                     "document.getElementById('displayAlert').style.display = 'block';" +
                     "document.getElementById('displayAlert').innerHTML = 'Error Occures, Please Try After Sometime';" +
                     "document.getElementById('displayAlert').className = 'alert alert-danger';", true);
            }
        }

        #endregion

        #region Dropdown Events

        // if Confirm The Deletion
        protected void BtnWarningYes_Click(object sender, EventArgs e)
        {
            appointmentMaster.AppoinmentId = (int)Session["AppointmentId"];
            bool isdeleted = appointmentMaster.DeleteAppoinment();
            if (isdeleted)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage",
                    "document.getElementById('displayAlert').style.display = 'block';" +
                    "document.getElementById('displayAlert').innerHTML = 'Deleted Successfully';" +
                    "document.getElementById('displayAlert').className = 'alert alert-success';", true);
                //LoadData();
            }
            else
            {
                if (appointmentMaster.Flag == -97)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Warning",
                    "document.getElementById('displayAlert').style.display = 'block';" +
                    "document.getElementById('displayAlert').innerHTML = 'You Can Not Delete this Appointment';" +
                    "document.getElementById('displayAlert').className = 'alert alert-danger';", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Warning",
                    "document.getElementById('displayAlert').style.display = 'block';" +
                    "document.getElementById('displayAlert').innerHTML = 'Error Occured, Please Try After Some Time.';" +
                    "document.getElementById('displayAlert').className = 'alert alert-danger';", true);
            }
        }

        //To Fill Time Slots Dynamically
        protected void DroDrownDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DroDrownDoctor.SelectedValue != "-1")
            {
                DrowDownSlots.Items.Clear();
                DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
                DataRow dataRow = ((DataTable)Session["DoctorsData2"]).Select($"Doctor_Id='{DroDrownDoctor.SelectedValue}'")[0];
                DateTime time1 = Convert.ToDateTime(dataRow["From Time"].ToString());
                DateTime time2 = Convert.ToDateTime(dataRow["To Time"].ToString());
                int intervalMins = Convert.ToInt32(dataRow["SlotTime"].ToString());
                DevideFillSlots(time1, time2, intervalMins);
            }
            else
            {
                DrowDownSlots.Items.Clear();
                DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#AppoinymentModal').modal('show');", true);
        }

        //Commands
        protected void BtnCommands_Click(object sender, EventArgs e)
        {
            try
            {
                if (HidenCommand.Value.ToString() == "Edit")
                {
                    Session["AppointmentId"] = Convert.ToInt32(HidenId.Value.ToString());
                    appointmentMaster.AppoinmentId = Convert.ToInt32(HidenId.Value.ToString());
                    if (appointmentMaster.IsUpdateTable())
                    {
                        DataRow dataRow = (appointmentMaster.GetAppoineMents(false).Tables[0]).Select($"Appointment_Id='{appointmentMaster.AppoinmentId}'")[0];
                        TxtBoxDate.Text = Convert.ToDateTime(dataRow["Appointment_Date"]).ToString("yyyy-MM-dd");
                        TxtBoxPtName.Text = dataRow["Patient_Name"].ToString();
                        DroDrownDoctor.SelectedValue = dataRow["Doctor_Id"].ToString();

                        doctor.DoctorId = Convert.ToInt32(DroDrownDoctor.SelectedValue);
                        DataRow dataRow1 = (doctor.SelectDoctors(false).Tables[0]).Select($"Doctor_Id='{DroDrownDoctor.SelectedValue}'")[0];
                        DateTime time1 = Convert.ToDateTime(dataRow1["From Time"].ToString());
                        DateTime time2 = Convert.ToDateTime(dataRow1["To Time"].ToString());
                        int intervalMins = Convert.ToInt32(dataRow1["SlotTime"].ToString());
                        DevideFillSlots(time1, time2, intervalMins);
                        DrowDownSlots.SelectedValue = dataRow["Appointment_Time"].ToString(); ;

                        BtnSubmit.Visible = false;
                        BtnReset.Visible = false;
                        BtnUpdate.Visible = true;
                        TxtBoxPtName.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "" +
                    "document.getElementById('displayAlertAppointment').style.display = 'none';" +
                    "$('#AppoinymentModal').modal('show');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Warning",
                        "document.getElementById('displayAlert').style.display = 'block';" +
                        "document.getElementById('displayAlert').innerHTML = 'You Can Not Reschedule this Appointment';" +
                        "document.getElementById('displayAlert').className = 'alert alert-danger';", true);
                    }

                }
                else if (HidenCommand.Value.ToString() == "Delete")
                {
                    Session["AppointmentId"] = Convert.ToInt32(HidenId.Value.ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowEditPopup", "$('#DeleteAppoinmentModal').modal('show')", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert",
                    "document.getElementById('displayAlert').style.display = 'block';" +
                            "document.getElementById('displayAlert').innerHTML = 'Something Went Wrong.';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';", true);

            }
        }

        //To Divide In Time Slots
        protected void DevideFillSlots(DateTime time1, DateTime time2, int intervalMins)
        {
            for (; time1 <= time2 && time1.AddMinutes(30) <= time2; time1 = time1.AddMinutes(intervalMins))
            {
                DrowDownSlots.Items.Add(new ListItem(time1.ToString("hh:mm tt") + " - " + time1.AddMinutes(intervalMins).ToString("hh:mm tt"), time1.ToString("hh:mm tt") + " - " + time1.AddMinutes(intervalMins).ToString("hh:mm tt")));
            }
        }
        #endregion

        #region Insert Update
        // To Submit Appoinment
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                appointmentMaster.PatientName = TxtBoxPtName.Text;
                appointmentMaster.Date = Convert.ToDateTime(TxtBoxDate.Text);
                appointmentMaster.Time = DrowDownSlots.SelectedValue;
                appointmentMaster.AppointmentWith = Convert.ToInt32(DroDrownDoctor.SelectedValue);

                if (appointmentMaster.InsertAppointement())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success",
                        "document.getElementById('displayAlert').style.display = 'block';" +
                        "document.getElementById('displayAlert').innerHTML = 'Added Successfully';" +
                        "document.getElementById('displayAlert').className = 'alert alert-success';", true);
                    ClearControls();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#AppoinymentModal').modal('hide');", true);
                }
                else
                {
                    if (appointmentMaster.Flag == -99)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Exits",
                            "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Slot is Already Booked.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-primary';", true);
                    }
                    else if (appointmentMaster.Flag == -96)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Exits",
                            "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Can Not Book Slot As Time is Passed.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-primary';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error",
                            "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-danger';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Error Occured, Please Try After Some Time';", true);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#AppoinymentModal').modal('show');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert",
                    "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Please Fill Data Properly.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-danger';", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#AppoinymentModal').modal('show');", true);
            }
        }

        // Reset The Form
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        // Open Popup for Addition
        protected void ImgAppointments_Click(object sender, ImageClickEventArgs e)
        {
            BtnReset.Visible = true;
            BtnSubmit.Visible = true;
            BtnUpdate.Visible = false;
            ClearControls();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "" +
                "document.getElementById('displayAlertAppointment').style.display = 'none';" +
                "$('#AppoinymentModal').modal('show');", true);
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                appointmentMaster.PatientName = TxtBoxPtName.Text;
                appointmentMaster.Date = Convert.ToDateTime(TxtBoxDate.Text);
                appointmentMaster.Time = DrowDownSlots.SelectedValue;
                appointmentMaster.AppoinmentId = (int)Session["AppointmentId"];
                appointmentMaster.AppointmentWith = Convert.ToInt32(DroDrownDoctor.SelectedValue);

                if (appointmentMaster.UpdateAppointment())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success",
                        "document.getElementById('displayAlert').style.display = 'block';" +
                        "document.getElementById('displayAlert').innerHTML = 'Updated Successfully';" +
                        "document.getElementById('displayAlert').className = 'alert alert-success';", true);
                    ClearControls();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#AppoinymentModal').modal('hide');", true);
                }
                else
                {
                    if (appointmentMaster.Flag == -99)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Exits",
                            "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Slot is Already Booked.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-primary';", true);
                    }
                    else if (appointmentMaster.Flag == -96)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Exits",
                            "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Can Not Book Slot As Time is Passed.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-primary';", true);
                    }
                    else if (appointmentMaster.Flag == -97)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "warning",
                            "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Can Not Reschedule This Slot.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-primary';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error",
                            "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Error Occured, Please Try After Sometime.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-danger';", true);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#AppoinymentModal').modal('show');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert",
                    "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlertAppointment').innerHTML = 'Please Fill Data Properly.';" +
                            "document.getElementById('displayAlertAppointment').className = 'alert alert-danger';", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#AppoinymentModal').modal('show');", true);
            }
        }
        #endregion

        #region Others Like Validators
        protected void CompareDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Convert.ToDateTime(args.Value).Date >= DateTime.Now.Date)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }
        #endregion

        #region Web Methods

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string AppointmentsData()
        {
            AppointmentMaster appointment = new AppointmentMaster();
            DataSet dataSet = appointment.GetAppoineMents(false);
            List<Appointment> appointments = new List<Appointment>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                appointments.Add(new Appointment
                {
                    Appointment_Date = Convert.ToDateTime(dataRow["Appointment_Date"]),
                    Appointment_Id = Convert.ToInt32(dataRow["Appointment_Id"]),
                    Appointment_Time = dataRow["Appointment_Time"].ToString(),
                    Doctor_Id = Convert.ToInt32(dataRow["Doctor_Id"]),
                    Doctor_Name = dataRow["Doctor Name"].ToString(),
                    Patient_Name = dataRow["Patient_Name"].ToString()
                });
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string jsonData = javaScriptSerializer.Serialize(appointments);
            return jsonData;
        }
        #endregion
    }
}