using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointment_Booking
{
    public class Appointment
    {
        public DateTime Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public int Appointment_Id { get; set; }
        public string Patient_Name { get; set; }
        public string Doctor_Name { get; set; }
        public int Doctor_Id { get; set; }
    }
    public class Doctors
    {
        public string Doctor_Designation { get; set; }
        public int Doctor_Id { get; set; }
        public int Doc_Desg { get; set; }
        public string Doctor_ContactNo { get; set; }
        public string Doctor_Email { get; set; }
        public byte[] Doctor_Image { get; set; }
        public TimeSpan From_Time { get; set; }
        public TimeSpan To_Time { get; set; }
        public string Doctor_Name { get; set; }
        public int SlotTime { get; set; }
        public int SlotIntervalID { get; set; }
        public string SlotText { get; set; }

    }
    public class DoctorBrief
    {
        public string Doctor_Designation { get; set; }
        public string Doctor_Name { get; set; }
        public int Doctor_Id { get; set; }
    }
}