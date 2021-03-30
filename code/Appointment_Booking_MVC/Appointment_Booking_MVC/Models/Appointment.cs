using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Appointment_Booking_MVC.Models
{
    public class Appointment
    {
        [Required(ErrorMessage ="Please Enter Date", AllowEmptyStrings = false)]
        [RegularExpression(pattern:"^[a-zA-z/+s]+$",ErrorMessage ="Please Enter Name Properly")]
        [DataType(DataType.Date)]
        public DateTime Appointment_Date { get; set; }

        [Required(ErrorMessage ="Please Select Appointment Time", AllowEmptyStrings = false)]
        public string Appointment_Time { get; set; }
        
        public int Appointment_Id { get; set; }

        [Required(ErrorMessage ="Please Enter Patient Name", AllowEmptyStrings = false)]
        public string Patient_Name { get; set; }
        public string Doctor_Name { get; set; }

        [Required(ErrorMessage = "Please Select Doctor",AllowEmptyStrings=false)]
        public int Doctor_Id { get; set; }
    }
}