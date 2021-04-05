using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace Appointment_Booking_MVC.Models
{
    public class Doctor
    {
        
        public int Doctor_Id { get; set; }
        public string Doctor_Designation { get; set; }
        
        [Required(ErrorMessage ="Please Select Desingnation"),Display(Name ="Desingnation")]
        public int Doc_Desg { get; set; }

        [Required(ErrorMessage ="Please Enter Contat Number"),DataType(DataType.PhoneNumber),RegularExpression(pattern: @"^((\+){1}91){1}[1-9]{1}[0-9]{9}$", ErrorMessage ="Please Enter Number Properly")]
        [Display(Name ="Contact Number")]
        public string Doctor_ContactNo { get; set; }
        
        [Required(ErrorMessage ="Please Eneter Email"),EmailAddress(ErrorMessage = "Please Enter Email Properly"),DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Doctor_Email { get; set; }
        public byte[] Doctor_Image { get; set; }
        
        [Required(ErrorMessage ="Please Ener From time"),DataType(DataType.Time)]
        [Display(Name ="From Time")]
        public TimeSpan From_Time { get; set; }

        [Required(ErrorMessage = "Please Ener To time"), DataType(DataType.Time)]
        [Display(Name = "To Time")]
        [TimeCompareValidation(CompareWith ="From_Time",ErrorMessage ="Please Select To Time Properly")]
        public TimeSpan To_Time { get; set; }
        
        [Required(ErrorMessage ="Please Enter Doctor Name")]
        [RegularExpression(pattern: @"^([a-zA-Z]+\s)*[a-zA-Z]+$", ErrorMessage = "Please Enter Name Properly")]
        [Display(Name ="Doctor Name")]
        public string Doctor_Name { get; set; }
        public int SlotTime { get; set; }
        public string SlotText { get; set; }
        
        [Required(ErrorMessage ="Please Select Slot Interval")]
        [Display(Name ="Slot Interval")]
        public int SlotIntervalID { get; set; }

        [Required(ErrorMessage ="Please Select Image")]
        [Display(Name ="Image")]
        [ImageValidation(ErrorMessage ="File Must be of Type .Jpg or .Png & Size less then 20Kb")]
        public HttpPostedFileBase Image { get; set; }

    }
    public class ImageValidation:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = (value as HttpPostedFileBase);
            if(file!=null && (file.ContentLength<=20000
                && Path.GetExtension(file.FileName).ToUpper().Equals(".JPG") ||
                   Path.GetExtension(file.FileName).ToUpper().Equals(".PNG")))
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }
    }
    public class TimeCompareValidation : ValidationAttribute
    {
        public string CompareWith { get; set; }
       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime = Convert.ToDateTime(validationContext.ObjectType.GetProperty(CompareWith).GetValue(validationContext.ObjectInstance, null).ToString());
            DateTime dateTime1 = Convert.ToDateTime(value.ToString());
            return dateTime < dateTime1 ? ValidationResult.Success : new ValidationResult("From Time Must Be Graeter Then To Time");
        }
    }
}