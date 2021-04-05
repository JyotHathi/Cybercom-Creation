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
        [CustomeDate(ErrorMessage ="Please Select Date Properly, Date Must Be Grater or Equal to today")]
        [DataType(DataType.Date)]
        [Display(Name = "Apppointment Date")]
        public DateTime Appointment_Date { get; set; }

        [Required(ErrorMessage ="Please Select Appointment Time", AllowEmptyStrings = false)]
        [Display(Name = "Apppointment Time")]
        public string Appointment_Time { get; set; }
        
        public int Appointment_Id { get; set; }

        [Required(ErrorMessage ="Please Enter Patient Name", AllowEmptyStrings = false)]
        [RegularExpression(pattern: @"^([a-zA-Z]+\s)*[a-zA-Z]+$", ErrorMessage = "Please Enter Name Properly")]
        [Display(Name = "Patient Name")]
        [DataType(DataType.Text)]
        public string Patient_Name { get; set; }
        public string Doctor_Name { get; set; }

        [Display(Name ="Apppointment With")]
        [Required(ErrorMessage = "Please Select Doctor")]
        public int Doctor_Id { get; set; }
    }
    public class CustomeDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime.Date >= DateTime.Now.Date;
        }
    }
}