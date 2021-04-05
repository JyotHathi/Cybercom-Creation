using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appointment_Booking_MVC.Models;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script;
using System.Web.Services;
using System.Web.Script.Services;

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
            
            List<DesignationMaster> designationMasters= context.Database.SqlQuery<DesignationMaster>("spDesignationMaster").ToList();
            List<SelectListItem> designations = new List<SelectListItem>();
            designations.Add(new SelectListItem { Text = "--Select Designation--", Value = "", Selected = true });
            foreach(DesignationMaster designationMaster in designationMasters)
            {
                designations.Add(new SelectListItem { Text =designationMaster.Designation, Value = designationMaster.Designation_Id.ToString(),}); 
            }
            Session["Designations"] = designations;
            ViewData["Doc_Desg"] = designations;
            

            List<SlotMaster> slotmasters = context.Database.SqlQuery<SlotMaster>("spSlotIntervalMaster").ToList();
            List<SelectListItem> slots = new List<SelectListItem>();
            slots.Add(new SelectListItem { Text = "--Select Slot Interval--", Value = "", Selected = true });
            foreach (SlotMaster slotmaster in slotmasters)
            {
                slots.Add(new SelectListItem { Text = slotmaster.SlotText, Value = slotmaster.SlotId.ToString(), });
            }
            Session["Slots"] = slots;
            ViewData["SlotIntervalID"] = slots;
            
            return View();

        }
        public ActionResult ViewDoctorDetails()
        {
            ViewData["Doc_Desg"] = Session["Designations"];
            ViewData["SlotIntervalID"] = Session["slots"];
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
            ViewData["Doc_Desg"] = Session["Designations"];
            ViewData["SlotIntervalID"] = Session["slots"];
            Session["DoctorToDelete"] = Url.RequestContext.RouteData.Values["id"];
            ViewBag.ConfirmationMessage = JavaScript("$('#ModalConfirmation').modal('show');").Script;
            return View("Index");
        }
        public ActionResult DeleteDoctor()
        {
            ViewData["Doc_Desg"] = Session["Designations"];
            ViewData["SlotIntervalID"] = Session["slots"];
            var context = new AppointmentBookingEntities();
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@queryval", 2);
            paras[1] = new SqlParameter("@doctoridval", Convert.ToInt32(Session["DoctorToDelete"]));
            try
            {
                context.Database.ExecuteSqlCommand("exec [spDoctorMasterMVC] @query=@queryval,@doctorid=@doctoridval", paras);
                Session["Doctors"] = context.Database.SqlQuery<Doctor>("exec spDoctorMasterMVC @query=4").ToList();
                ViewBag.ConfirmationMessage = JavaScript("alert('Deeleted Successfully');").Script;
            }
            catch
            {
                ViewBag.ConfirmationMessage = JavaScript("alert('Please Try After Sometime');").Script;
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult SubmitData(Doctor doctor)
        {
            ViewData["Doc_Desg"] = Session["Designations"];
            ViewData["SlotIntervalID"] = Session["slots"];
            if (!ModelState.IsValid)
            {
                ViewBag.VaidationMessage = JavaScript("alert('Enter Data Properly');").Script;
                return View("Index", doctor);
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
                        ("exec spDoctorMasterMVC @query=@qval,@doctorname=@docName," +
                        "@designationId=@desId,@doctormobilenumber=@docMobileNumber,@doctoremail=@docEmail,@doctorimage=@docImg," +
                        "@fromtime=@froTime,@totime=@ToTime,@slotIntervalID=@slotId", sqlParameters).ToArray()[0];
                    if(status!=-99)
                    {
                        ModelState.Clear();
                        Session["Doctors"] = context.Database.SqlQuery<Doctor>("exec spDoctorMasterMVC @query=4").ToList();
                        ViewBag.SuccssMessage = JavaScript("alert('Added Succssfully !!');").Script;
                    }
                    else
                    {
                        ViewBag.SuccssMessage = JavaScript("alert('Already Exists !!');").Script;
                    }
                    
                }
                catch
                {
                    ViewBag.SuccssMessage = JavaScript("alert('Please Try Again !!');").Script;
                }
                return View("Index");
            }
            

        }

        public ActionResult EditDoctor()
        {
            ViewData["Doc_Desg"] = Session["Designations"];
            ViewData["SlotIntervalID"] = Session["slots"];
            List<Doctor> doctors = (List<Doctor>)Session["Doctors"];
            int doctorId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            Doctor foundDoctor=null;
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Doctor_Id == doctorId)
                {
                    foundDoctor = doctor;   
                    break;
                }
            }
            if (foundDoctor != null)
            {
                ViewBag.Scripts = JavaScript("document.getElementById('Doc_Desg').value='" + foundDoctor.Doc_Desg+"';").Script + JavaScript("document.getElementById('SlotIntervalID').value='" + foundDoctor.Doc_Desg+"';").Script;
                return View(foundDoctor);
            }
            else
            {
                ViewBag.ModalShow = JavaScript("alert('No Result Found');").Script;
                return View("Index");
            }
            
        }

        [HttpPost]
        public ActionResult UpdateData(Doctor doctor)
        {
            ViewData["Doc_Desg"] = Session["Designations"];
            ViewData["SlotIntervalID"] = Session["slots"];
            if (!ModelState.IsValid)
            {
                ViewBag.VaidationMessage = JavaScript("alert('Enter Data Properly');").Script;
                return View("EditDoctor", doctor);
            }
            else
            {
                try
                {
                    Stream stream = doctor.Image.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    doctor.Doctor_Image = binaryReader.ReadBytes((int)stream.Length);
                    var context = new AppointmentBookingEntities();
                    SqlParameter[] sqlParameters = new SqlParameter[10];
                    sqlParameters[0] = new SqlParameter("@qval", 3);
                    sqlParameters[1] = new SqlParameter("@docName", doctor.Doctor_Name);
                    sqlParameters[2] = new SqlParameter("@desId", doctor.Doc_Desg);
                    sqlParameters[3] = new SqlParameter("@docMobileNumber", doctor.Doctor_ContactNo);
                    sqlParameters[4] = new SqlParameter("@docEmail", doctor.Doctor_Email);
                    sqlParameters[5] = new SqlParameter("@docImg", doctor.Doctor_Image);
                    sqlParameters[6] = new SqlParameter("@froTime", doctor.From_Time);
                    sqlParameters[7] = new SqlParameter("@ToTime", doctor.To_Time);
                    sqlParameters[8] = new SqlParameter("@slotId", doctor.SlotIntervalID);
                    sqlParameters[9] = new SqlParameter("@docid", doctor.Doctor_Id);
                    int status = context.Database.SqlQuery<int>
                        ("exec spDoctorMasterMVC @query=@qval,@doctorname=@docName," +
                        "@designationId=@desId,@doctormobilenumber=@docMobileNumber,@doctoremail=@docEmail,@doctorimage=@docImg," +
                        "@fromtime=@froTime,@totime=@ToTime,@slotIntervalID=@slotId,@doctorid=@docid", sqlParameters).ToArray()[0];
                    if (status != -98)
                    {
                        ModelState.Clear();
                        Session["Doctors"] = context.Database.SqlQuery<Doctor>("exec spDoctorMasterMVC @query=4").ToList();
                        ViewBag.SuccssMessage = JavaScript("alert('Updated Succssfully !!');").Script;
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.SuccssMessage = JavaScript("alert('Already Exists, So you Can Not Update This !!');").Script;
                        return View("EditDoctor", doctor);
                    }

                }
                catch
                {
                    ViewBag.SuccssMessage = JavaScript("alert('Please Try Again !!');").Script;
                    return View("EditDoctor", doctor);
                }
                
            }


        }

        [HttpPost]
        public JsonResult IsUserExits(string mobileNumber, string email)
        {
            var contxet = new AppointmentBookingEntities();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@qval",5);
            paras[1] = new SqlParameter("@docemail",email);
            paras[2] = new SqlParameter("@docmobnum", mobileNumber);
            int count=contxet.Database.SqlQuery<int>("exec spDoctorMaster @query=@qval,@doctoremail=@docemail,@doctormobilenumber=@docmobnum",paras).ToArray()[0];
            return Json(count != 0, JsonRequestBehavior.AllowGet);
        }
    }
}