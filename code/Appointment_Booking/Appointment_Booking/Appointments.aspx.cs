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
                if (Session["AppoinmentsData"] == null)
                { LoadData();
                 }
                else
                {
                    RptrAppointments.DataSource =(DataTable)Session["AppoinmentsData"];
                    RptrAppointments.DataBind();
                }
                if (Session["DoctorsData2"] == null)
                {
                    LoadDoctorsDropDown();
                }
                else
                {
                    DroDrownDoctor.DataTextField = "Doctor Name";
                    DroDrownDoctor.DataValueField = "Doctor_Id";
                    DroDrownDoctor.DataSource =(DataTable) Session["DoctorsData2"];
                    DroDrownDoctor.DataBind();
                    ListItem listItem = new ListItem("Select Doctor", "-1");
                    DroDrownDoctor.Items.Add(listItem);
                    DroDrownDoctor.SelectedValue = "-1";
                    DrowDownSlots.Items.Add(new ListItem("Select Slot", "-1"));
                }
            }
        }

        #region Load or Clear Controls

        // Load Appoinments
        protected void LoadData()
        {
            DataSet ds = appointmentMaster.GetAppoineMents();
            if (ds != null)
            {
                if (ds.Tables.Count != 0)
                {
                    Session["AppoinmentsData"] = (DataTable)ds.Tables[0];
                    RptrAppointments.DataSource = ds;
                    RptrAppointments.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert", "alert('Please Try After After Sometime')", true);
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

        #endregion

        #region Control Events
        
        //Repeter Item Commands
        protected void RptrAppointments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName.Equals("Delete"))
            {
                Session["AppointmentId"] = Convert.ToInt32(e.CommandArgument);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowEditPopup", "$('#DeleteAppoinmentModal').modal('show')", true);
            }
        }
       
        // if Confirm The Deletion
        protected void BtnWarningYes_Click(object sender, EventArgs e)
        {
            appointmentMaster.AppoinmentId = (int)Session["AppointmentId"];
            bool isdeleted=appointmentMaster.DeleteAppoinment();
            if (isdeleted)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Deleted Sucessfully')", true);
                LoadData();
            }
            else
            {
                if(appointmentMaster.Flag==-97)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NotDeletable", "alert('You Can Not Delete This')", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('Please Try After Some Time')", true);
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
                int intervalMins = Convert.ToInt32(dataRow["SlotTime"].ToString());
                for (; time1 <= time2 && time1.AddMinutes(30) <= time2; time1 = time1.AddMinutes(intervalMins))
                {
                    DrowDownSlots.Items.Add(new ListItem(time1.ToString("hh:mm tt") +" - "+time1.AddMinutes(intervalMins).ToString("hh:mm tt"), time1.ToString("hh:mm tt")+ " - "+time1.AddMinutes(intervalMins).ToString("hh:mm tt")));
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
                appointmentMaster.Time = DrowDownSlots.SelectedValue;
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

        #endregion

        
    }
}