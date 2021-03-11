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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewPopup", "$('#ViewDoctorModal').modal('show');", true);
            }
            else if(e.CommandName.Equals("ViewApointments"))
            {
                AppointmentMaster appointmentMaster = new AppointmentMaster();
                DataSet ds=appointmentMaster.GetAppoineMents();
                if(ds!=null)
                {
                    
                    if(ds.Tables[0].Rows.Count!=0)
                    {
                        DataRow[] datarow = ds.Tables[0].Select($"Doctor_Id='{Convert.ToInt32(e.CommandArgument)}' AND Appointment_Date='{DateTime.Now.Date}'");
                        if (datarow.Length!=0)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Time");
                            dataTable.Columns.Add("PatientName");
                            foreach(DataRow dr in datarow)
                            {
                                dataTable.Rows.Add(dr["Appointment_Time"], dr["Patient_Name"]);
                            }
                            RptrListofAppointments.DataSource = dataTable;
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoData", "alert('No Appoinments');", true);
                    }
                }
                
            }
            
        }
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