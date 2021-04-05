using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointment_Booking_MVC.Models
{
    public class DesignationMaster
    {
        public int Designation_Id { get; set; }
        public string Designation { get; set; }
    }
    public class SlotMaster
    {
        public int SlotTime { get; set; }
        public int SlotId { get; set; }
        public string SlotText { get; set; }
    }
}