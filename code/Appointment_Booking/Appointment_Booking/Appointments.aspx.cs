using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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
                LoadData();
                LoadDoctorsDropDown();
            }
        }

        // Load Appoinments
        protected void LoadData()
        {
            DataSet ds = appointmentMaster.GetAppoineMents();
            if (ds != null)
            {
                if (ds.Tables.Count != 0)
                {
                    RptrAppointments.DataSource = ds;
                    RptrAppointments.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert", "alert('Please Try After After Sometime')", true);
            }
        }

        // To Fill DropDown Of Doctor
        private void LoadDoctorsDropDown()
        {
            DataSet ds2 = doctor.SelectDoctors();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert", "alert('Please Try After After Sometime')", true);
            }
        }

        // To Fill Time Slots Dynamically
        protected void DroDrownDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DroDrownDoctor.SelectedValue != "-1")
            {
                DrowDownSlots.Items.Clear();
                DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
                DataRow dataRow = ((DataTable)Session["DoctorsData2"]).Select($"Doctor_Id='{DroDrownDoctor.SelectedValue}'")[0];
                DateTime time1 = Convert.ToDateTime(dataRow["From Time"].ToString());
                DateTime time2 = Convert.ToDateTime(dataRow["To Time"].ToString());
                for (; time1 <= time2 && time1.AddMinutes(30) <= time2; time1 = time1.AddHours(1))
                {
                    DrowDownSlots.Items.Add(new ListItem(time1.ToString("HH:mm"), time1.ToString("HH:mm")));
                }
            }
            else
            {
                DrowDownSlots.Items.Clear();
                DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
            }
        }

        // To Submit Appoinment
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                appointmentMaster.PatientName = TxtBoxPtName.Text;
                appointmentMaster.Date = Convert.ToDateTime(TxtBoxDate.Text);
                appointmentMaster.Time = Convert.ToDateTime(DrowDownSlots.SelectedValue);
                appointmentMaster.AppointmentWith = Convert.ToInt32(DroDrownDoctor.SelectedValue);

                if (appointmentMaster.InsertAppointement())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Added Successfully Properly')", true);
                    ClearControls();
                    LoadData();
                }
                else
                {
                    if (appointmentMaster.Flag == -99)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Exits", "alert('Slot Already Booked')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('Please Try After Some Time')", true);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert", "alert('Fill Data Properly')", true);
            }
        }
       
        // To Clear All The Controls
        protected void ClearControls()
        {
            TxtBoxDate.Text = "";
            TxtBoxPtName.Text = "";
            DroDrownDoctor.SelectedValue = "-1";
            DrowDownSlots.Items.Clear();
            DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));

        }
    }
}