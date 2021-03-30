using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Appointment_Booking_MVC.Models; 

namespace Appointment_Booking_MVC.Controllers
{
    public class AppointmentsController : Controller
    {
        // GET: Appointments
        public ActionResult Index()
        {
            Session["DoctorIdToDelete"] = -1;
            ViewBag.ConfirmationMessage = "";
            var context = new AppointmentBookingEntities();
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@queryval", 2);
            List<Appointment> appointments = context.Database.SqlQuery<Appointment>("exec spAppointemntMasterMVC @query=@queryval", paras).ToList();
            Session["Appointments"] = appointments;
            
            return View();
        }
        public ActionResult DeleteAppointment()
        {
            var context = new AppointmentBookingEntities();
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@queryval", 3);
            paras[1] = new SqlParameter("@appointmentval",Convert.ToInt32(Session["DoctorIdToDelete"]));
            try
            {
                int status=context.Database.SqlQuery<int>("exec spAppointemntMasterMVC @query=@queryval,@appoinmentid=@appointmentval", paras).ToArray()[0];
                if (status == 0)
                {
                    Session["Appointments"] = context.Database.SqlQuery<Appointment>("exec spAppointemntMasterMVC @query=@queryval", new SqlParameter("@queryval", 2)).ToList();
                    ViewBag.ConfirmationMessage = JavaScript("alert('Deeleted Successfully')").Script;   
                }
                else
                {
                    ViewBag.ConfirmationMessage = JavaScript("alert('You Can Not Delete this')").Script;
                }
            }
            catch
            {
                ViewBag.ConfirmationMessage = JavaScript("alert('Please Try After Sometime')").Script;
            }
            return View("Index");
        }
        public ViewResult Confirmation()
        {
            Session["DoctorIdToDelete"] = Url.RequestContext.RouteData.Values["id"];
            ViewBag.ConfirmationMessage = JavaScript("$('#ModalConfirmation').modal('show');").Script;
            return View("Index");
        }

        [HttpPost]
        public ViewResult SubmitData(Appointment appointment)
        {
            if(!ValidateAppointment(appointment))
            {
                ViewBag.ValidationMessage = JavaScript("alert('Please Select date / time Properly');").Script;
            }
            else
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter("@qval", 1);
                sqlParameters[1] = new SqlParameter("@apwith", appointment.Doctor_Id);
                sqlParameters[2] = new SqlParameter("@apdate",appointment.Appointment_Date);
                sqlParameters[3] = new SqlParameter("@aptime",appointment.Appointment_Time);
                sqlParameters[4] = new SqlParameter("@ptname",appointment.Patient_Name);
                try
                {
                    var context = new AppointmentBookingEntities();
                    int status = context.Database.SqlQuery<int>("exec spAppointemntMasterMVC @query=@qval,@appointment_with=@apwith,@appointment_date=@apdate,@appointment_time=@aptime,@patient_Name=@ptname", sqlParameters).ToArray()[0];
                    if(status!=-99)
                    {
                        Session["Appointments"] = context.Database.SqlQuery<Appointment>("exec spAppointemntMasterMVC @query=@queryval", new SqlParameter("@queryval", 2)).ToList();
                        ViewBag.SuccessMessage = JavaScript("alert('Added Succssfully');").Script;
                    }
                    else
                    {
                        ViewBag.SuccessMessage = JavaScript("alert('Slot Booked');").Script;
                    }
                }
                catch
                {
                    ViewBag.SuccessMessage = JavaScript("alert('Please Try Agian');").Script;
                }
                
            }
            return View("Index");
        }

        [NonAction]
        private bool ValidateAppointment(Appointment appointment)
        {
            if(appointment.Appointment_Date<DateTime.Now)
            {
                return false;
            }
            else if(Convert.ToDateTime(appointment.Appointment_Time.Substring(0,8))<DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}