//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Appointment_Booking_MVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDesignationMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDesignationMaster()
        {
            this.tblDoctorMasters = new HashSet<tblDoctorMaster>();
        }
    
        public int Designation_Id { get; set; }
        public string Designation { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDoctorMaster> tblDoctorMasters { get; set; }
    }
}
