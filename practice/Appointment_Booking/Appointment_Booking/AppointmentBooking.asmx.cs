using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
namespace Appointment_Booking
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AppointmentBooking : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetDoctos(int iDisplayLength, int iDisplayStart, int iSortCol_0,
             string sSortDir_0, string sSearch)
        {
            Context.Response.Write(new JavaScriptSerializer().Serialize("Hello"));
        }
        [WebMethod]
        public void GetAppointments(int iDisplayLength, int iDisplayStart, int iSortCol_0,
             string sSortDir_0, string sSearch)
        {

        }
    }
}
