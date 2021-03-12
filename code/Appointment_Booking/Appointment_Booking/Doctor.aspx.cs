using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Appointment_Booking
{
    public partial class Doctor : System.Web.UI.Page
    {
        DoctorMaster doctorMaster = new DoctorMaster();
        AppointmentMaster appointmentMaster = new AppointmentMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["DoctorsData"] == null)
                {
                    LoadData();
                }
                else
                {
                    RptrDoctors.DataSource =(DataTable)Session["DoctorsData"];
                    RptrDoctors.DataBind();
                }
            }
        }

        // To Handle View & View Today's Appointments
        protected void RptrDoctors_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DataTable dt = (DataTable)Session["DoctorsData"];
            DataRow dataRow = dt.Select($"Doctor_Id='{e.CommandArgument}'")[0];
            if (e.CommandName.Equals("View"))
            {
                LblValDocName.Text = dataRow["Doctor Name"].ToString();
                LblVValDocDesignation.Text = dataRow["Doctor_Designation"].ToString();
                LblValEmail.Text = dataRow["Doctor Email"].ToString();
                LblValAvailFrom.Text = dataRow["From Time"].ToString();
                LblValAvilTill.Text = dataRow["To time"].ToString();
                LblValMobileNumber.Text = dataRow["Doctor ContactNo."].ToString();
                DocImg.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])dataRow["Doctor Image"]);
                LblValSlot.Text = dataRow["SlotText"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewPopup", "$('#ViewDoctorModal').modal('show');", true);
            }
            else if(e.CommandName.Equals("ViewApointments"))
            {
                appointmentMaster.DoctorId = Convert.ToInt32(e.CommandArgument);
                DataSet ds = appointmentMaster.GetAppoinment();
                appointmentMaster.DoctorId = Convert.ToInt32(e.CommandArgument);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        RptrListofAppointments.DataSource = ds.Tables[0];
                        RptrListofAppointments.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewPopup", "$('#AppoinmentList').modal('show');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoData", "alert('No Appoinments');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('Please Try After Some Time');", true);
                }
            }
            
        }

        // To Load Doctor's Information
        protected void LoadData()
        {
            DataSet ds = doctorMaster.SelectDoctors();
            if (ds != null)
            {
                Session["DoctorsData"] = ds.Tables[0];
                RptrDoctors.DataSource = ds.Tables[0];
                RptrDoctors.DataBind();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Try After Some Time');", true);
        }

    }
}