using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appointment_Booking_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Appointment_Booking_MVC.Controllers
{
    public class DoctorsController : Controller
    {
        // GET: Doctors
        [OutputCache(Duration = 300)]
        public ActionResult Index()
        {
            var context = new AppointmentBookingEntities();
            List<Doctor> doctors = context.Database.SqlQuery<Doctor>("exec spDoctorMasterMVC @query=4").ToList();
            Session["Doctors"] = doctors;
            
            return View();
        }
        
        public ActionResult ViewDoctorDetails()
        {
            List<Doctor> doctors = (List<Doctor>)Session["Doctors"];
            int doctorId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            ViewBag.Doctor = null;
            foreach (Doctor doctor in doctors)
            {
                if(doctor.Doctor_Id== doctorId)
                {
                    ViewBag.Doctor = doctor;
                    break;
                }
            }
            if(ViewBag.Doctor!=null)
            {
                ViewBag.ModalShow = JavaScript("$('#ViewDoctorModal').modal('show');").Script;
            }
            else
            {
                ViewBag.ModalShow = JavaScript("alert('No Result Found');").Script;
            }
            return View("Index");
        }
        public ActionResult ViewDoctorAppointments()
        {
            int doctorId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            var context =new AppointmentBookingEntities();
            ViewBag.Appoinments = null;
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@queryval", 2);
            paras[1] = new SqlParameter("@docidval", doctorId);
            List<Appointment> appointments = context.Database.SqlQuery<Appointment>("exec spAppointemntMasterMVC @query=@queryval, @doctorid=@docidval", paras).ToList();
            ViewBag.Appointments = appointments;
            if(appointments.Count!=0)
            {
                ViewBag.ModalShow = JavaScript("$('#AppoinmentList').modal('show');").Script;
            }
            else
            {
                ViewBag.ModalShow = JavaScript("alert('No Appointments Found');").Script;
            }
            return View("Index");
        }
    }
}