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
            
            List<Doctor> doctors = context.Database.SqlQuery<Doctor>("exec spDoctorMasterMVC @query=4").ToList();
            Session["Doctors"] = doctors;
            
            List<SelectListItem> doctorsList = new List<SelectListItem>();
            doctorsList.Add(new SelectListItem { Text = "--Select Doctor--", Value = "", Selected = true });
            foreach (Doctor doctor in doctors)
            {
                doctorsList.Add(new SelectListItem { Text = doctor.Doctor_Name, Value = doctor.Doctor_Id.ToString() });
            }
            Session["DoctorsDropDown"] = doctorsList;
            ViewData["Doctor_Id"] = doctorsList;
            Session["AppointmentTimeData"]= (new List<SelectListItem>() { new SelectListItem { Text = "--Select Time Slot--", Value = "", Selected = true } });
            ViewData["Appointment_Time"] = (new List<SelectListItem>() { new SelectListItem { Text = "--Select Time Slot--", Value = "", Selected = true } });
            return View();
        }
        public ActionResult DeleteAppointment()
        {
            ViewData["Doctor_Id"] = Session["DoctorsDropDown"];
            ViewData["Appointment_Time"] = Session["AppointmentTimeData"];
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
                    ViewBag.ConfirmationMessage = JavaScript("alert('Delete Succssfully');").Script;   
                }
                else
                {
                    ViewBag.ConfirmationMessage = JavaScript("alert('You Can Not Delete this');").Script;
                }
            }
            catch
            {
                ViewBag.ConfirmationMessage = JavaScript("alert('Please Try After Sometime');").Script;
            }
            return View("Index");
        }
        public ViewResult Confirmation()
        {
            ViewData["Doctor_Id"] = Session["DoctorsDropDown"];
            ViewData["Appointment_Time"] = Session["AppointmentTimeData"];
            Session["DoctorIdToDelete"] = Url.RequestContext.RouteData.Values["id"];
            ViewBag.ConfirmationMessage = JavaScript("$('#ModalConfirmation').modal('show');").Script;
            return View("Index");
        }

        [HttpPost]
        public ViewResult SubmitData(Appointment appointment)
        {
            ViewData["Doctor_Id"] = Session["DoctorsDropDown"];
            ViewData["Appointment_Time"] = Session["AppointmentTimeData"];
            if (!ModelState.IsValid)
            {
                ViewBag.ValidationMessage = JavaScript("alert('Please Enter data Properly');").Script;
                return View("Index", appointment);
            }
            else
            {
                ModelState.Clear();
                Session["AppointmentTimeData"] = (new List<SelectListItem>() { new SelectListItem { Text = "--Select Time Slot--", Value = "", Selected = true } });
                ViewData["Appointment_Time"] = (new List<SelectListItem>() { new SelectListItem { Text = "--Select Time Slot--", Value = "", Selected = true } });
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
                return View("Index");
            }
            
            
        }
        


        [HttpPost]
        public JsonResult GetSlots(Appointment appointment)
        {
            ViewData["Doctor_Id"] = Session["DoctorsDropDown"];
            List<SelectListItem> timeSlots = new List<SelectListItem>();
            timeSlots.Add(new SelectListItem { Text = "--Select Time Slots--", Value = "", Selected = true });
            if (appointment.Doctor_Id != 0)
            {
                
                int doctorId = appointment.Doctor_Id;
                Doctor doctor = ((List<Doctor>)Session["Doctors"]).Find(x => x.Doctor_Id == doctorId);
                DateTime time1 = Convert.ToDateTime(doctor.From_Time.ToString());
                DateTime time2 = Convert.ToDateTime(doctor.To_Time.ToString());
                int intervalMins = Convert.ToInt32(doctor.SlotTime); 
                
                for (; time1 <= time2 && time1.AddMinutes(30) <= time2; time1 = time1.AddMinutes(intervalMins))
                {
                    timeSlots.Add(new SelectListItem
                    {
                        Text = time1.ToString("hh:mm tt") + " - " + time1.AddMinutes(intervalMins).ToString("hh:mm tt"),
                        Value = time1.ToString("hh:mm tt") + " - " + time1.AddMinutes(intervalMins).ToString("hh:mm tt")
                    });
                }
                
            }
            
            Session["AppointmentTimeData"] = timeSlots;
            ViewData["Appointment_Time"] = timeSlots;
            return Json(timeSlots.ToArray(),JsonRequestBehavior.AllowGet);
        }
    }
}