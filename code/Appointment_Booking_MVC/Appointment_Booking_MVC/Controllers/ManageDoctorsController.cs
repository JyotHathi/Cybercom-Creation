using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appointment_Booking_MVC.Models;
using System.Data.SqlClient;
using System.IO;
namespace Appointment_Booking_MVC.Controllers
{
    public class ManageDoctorsController : Controller
    {
        // GET: ManageDoctors
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
                if (doctor.Doctor_Id == doctorId)
                {
                    ViewBag.Doctor = doctor;
                    break;
                }
            }
            if (ViewBag.Doctor != null)
            {
                ViewBag.ModalShow = JavaScript("$('#ViewDoctorModal').modal('show');").Script;
            }
            else
            {
                ViewBag.ModalShow = JavaScript("alert('No Result Found');").Script;
            }
            return View("Index");
        }
        public ViewResult Confirmation()
        {
            Session["DoctorToDelete"] = Url.RequestContext.RouteData.Values["id"];
            ViewBag.ConfirmationMessage = JavaScript("$('#ModalConfirmation').modal('show');").Script;
            return View("Index");
        }
        public ActionResult DeleteDoctor()
        {
            var context = new AppointmentBookingEntities();
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@queryval", 2);
            paras[1] = new SqlParameter("@doctoridval", Convert.ToInt32(Session["DoctorToDelete"]));
            try
            {
                context.Database.ExecuteSqlCommand("exec [spDoctorMasterMVC] @query=@queryval,@doctorid=@doctoridval", paras);
                Session["Doctors"] = context.Database.SqlQuery<Doctor>("exec spDoctorMasterMVC @query=4").ToList();
                ViewBag.ConfirmationMessage = JavaScript("alert('Deeleted Successfully')").Script;
            }
            catch
            {
                ViewBag.ConfirmationMessage = JavaScript("alert('Please Try After Sometime')").Script;
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult SubmitData(Doctor doctor)
        {
            if (!ValidateDoctor(doctor))
            {
                ViewBag.VaidationMessage = JavaScript("alert('Please Upload File Properly: Size < 20Kb and in .jpg or .png only')");
            }
            else
            {
                try
                {
                    Stream stream = doctor.Image.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    doctor.Doctor_Image = binaryReader.ReadBytes((int)stream.Length);
                    var context = new AppointmentBookingEntities();
                    SqlParameter[] sqlParameters = new SqlParameter[9];
                    sqlParameters[0] = new SqlParameter("@qval", 1);
                    sqlParameters[1] = new SqlParameter("@docName", doctor.Doctor_Name);
                    sqlParameters[2] = new SqlParameter("@desId", doctor.Doc_Desg);
                    sqlParameters[3] = new SqlParameter("@docMobileNumber", doctor.Doctor_ContactNo);
                    sqlParameters[4] = new SqlParameter("@docEmail", doctor.Doctor_Email);
                    sqlParameters[5] = new SqlParameter("@docImg", doctor.Doctor_Image);
                    sqlParameters[6] = new SqlParameter("@froTime", doctor.From_Time);
                    sqlParameters[7] = new SqlParameter("@ToTime", doctor.To_Time);
                    sqlParameters[8] = new SqlParameter("@slotId", doctor.SlotIntervalID);
                    int status = context.Database.SqlQuery<int>
                        ("exec spDoctorMasterMVC @query=@qval,@doctorname=@docName" +
                        "@designationId=@desId,@doctormobilenumber=@docMobileNumber,@doctoremail=@docEmail,@doctorimage=@docImg," +
                        "@fromtime=@froTime,@totime=@ToTime,@slotIntervalID=@slotId", sqlParameters).ToArray()[0];
                    if(status!=-99)
                    {
                        Session["Doctors"] = context.Database.SqlQuery<Doctor>("exec spDoctorMasterMVC @query=4").ToList();
                        ViewBag.SuccssMessage = JavaScript("alert('Added Succssfully !!')").Script;
                    }
                    else
                    {
                        ViewBag.SuccssMessage = JavaScript("alert('Already Exists !!')").Script;
                    }
                }
                catch
                {
                    ViewBag.SuccssMessage = JavaScript("alert('Please Try Again !!')").Script;
                }
            }
            return View();
        }
        public bool ValidateDoctor(Doctor doctor)
        {
            if (doctor.Image.InputStream == null)
            {
                return false;
            }
            else if (doctor.Image.ContentLength > 20000)
            {
                return false;
            }
            else if (!(Path.GetExtension(doctor.Image.FileName).ToUpper().Equals(".JPG")
                ||
                Path.GetExtension(doctor.Image.FileName).ToUpper().Equals(".PNG")))
            {
                return false;
            }
            else if(doctor.From_Time>doctor.To_Time)
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