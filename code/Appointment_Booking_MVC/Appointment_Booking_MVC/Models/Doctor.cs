using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Appointment_Booking_MVC.Models
{
    public class Doctor
    {
        public int Doctor_Id { get; set; }
        public string Doctor_Designation { get; set; }
        
        [Required(ErrorMessage ="Please Select Desingnation")]
        public int Doc_Desg { get; set; }

        [Required(ErrorMessage ="Please Enter Contat Number"),DataType(DataType.PhoneNumber),Phone(ErrorMessage ="Please Enter Number Properly")]
        public string Doctor_ContactNo { get; set; }
        
        [Required(ErrorMessage ="Please Eneter Email"),EmailAddress]
        public string Doctor_Email { get; set; }
        public byte[] Doctor_Image { get; set; }
        
        [Required,DataType(DataType.Time)]
        public TimeSpan From_Time { get; set; }

        [Required, DataType(DataType.Time)]
        public TimeSpan To_Time { get; set; }
        
        [Required(ErrorMessage ="Please Enter Doctor Name")]
        public string Doctor_Name { get; set; }
        public int SlotTime { get; set; }
        public string SlotText { get; set; }
        
        [Required]
        public int SlotIntervalID { get; set; }

        public HttpPostedFileBase Image { get; set; }

    }
}