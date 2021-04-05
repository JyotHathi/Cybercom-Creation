using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace Appointment_Booking
{
    /// <summary>
    /// For StoredProcedure spDoctorMaster
    /// </summary>
    public class DoctorMaster
    {
        #region Properties
        int flag = 0;
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorMobileNumber { get; set; }
        public byte[] DoctorImage { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public bool IsDeleted { get; set; }
        public int Designation { get; set; }
        public int Flag { get { return flag; } }
        public int SlotIntervalID { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// To Add Doctor, Have To Must Set Properties of DoctorMaster Object
        /// </summary>
        /// <returns>true: If Inserted Successfully<br />false:If Any Eroor Occur</returns>
        public bool InsertData()
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spDoctorMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    flag = 0;
                    command.Parameters.AddWithValue("@query", 1);
                    command.Parameters.AddWithValue("@doctorname", this.DoctorName);
                    command.Parameters.AddWithValue("@designationId", this.Designation);
                    command.Parameters.AddWithValue("@doctormobilenumber", this.DoctorMobileNumber);
                    command.Parameters.AddWithValue("@doctoremail", this.DoctorEmail);
                    command.Parameters.AddWithValue("@doctorimage", this.DoctorImage);
                    command.Parameters.AddWithValue("@fromtime", this.FromTime.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@totime", this.ToTime.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@slotIntervalID", SlotIntervalID);
                    con.Open();
                    flag= Convert.ToInt32(command.ExecuteScalar());
                    con.Close();
                    if(Flag!=-99)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// To Delete Doctor, Have To Must Set Properties of DoctorMaster Object
        /// </summary>
        /// <returns>true: If Inserted Successfully<br />false:If Any Eroor Occur</returns>
        public bool DeleteDoctor()
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spDoctorMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.Parameters.AddWithValue("@query", 2);
                    command.Parameters.AddWithValue("@doctorid", this.DoctorId);
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To Update Doctor, Have To Must Set Properties of DoctorMaster Object
        /// </summary>
        /// <returns>true: If Inserted Successfully<br />false:If Any Eroor Occur</returns>
        public bool UpdateDoctor()
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spDoctorMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.Parameters.AddWithValue("@query", 3);
                    command.Parameters.AddWithValue("@doctorid", this.DoctorId);
                    command.Parameters.AddWithValue("@doctorname", this.DoctorName);
                    command.Parameters.AddWithValue("@designationId", this.Designation);
                    command.Parameters.AddWithValue("@doctormobilenumber", this.DoctorMobileNumber);
                    command.Parameters.AddWithValue("@doctoremail", this.DoctorEmail);
                    command.Parameters.AddWithValue("@doctorimage", this.DoctorImage);
                    command.Parameters.AddWithValue("@fromtime", this.FromTime.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@totime", this.ToTime.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@slotIntervalID", SlotIntervalID);
                    con.Open();
                    flag=Convert.ToInt32(command.ExecuteScalar());
                    con.Close();
                    if (Flag != -98)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To Get Doctor
        /// </summary>
        /// <returns>true:DataSet which have List of Doctor, Null if Error Occured</returns>
        public DataSet SelectDoctors(bool all)
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spDoctorMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                try
                {
                    command.Parameters.AddWithValue("@query", 4);
                    if (all == false)
                    {
                        command.Parameters.AddWithValue("@doctorid", DoctorId);
                    }
                    
                    sqlData.Fill(ds);
                    return ds;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    sqlData.Dispose();
                    command.Dispose();
                    con.Dispose();
                }

            }
            catch
            {
                return null;
            }
        }

        public bool IsUserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spDoctorMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.Parameters.AddWithValue("@query", 5);
                    command.Parameters.AddWithValue("@doctormobilenumber", this.DoctorMobileNumber);
                    command.Parameters.AddWithValue("@doctoremail", this.DoctorEmail);
                    con.Open();
                    flag = Convert.ToInt32(command.ExecuteScalar());
                    con.Close();
                    if (flag != 0)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
    
    
    /// <summary>
    /// For StoredProcedure spDesignationMaster
    /// </summary>
    public class DesignationMaster
    {
        public static DataSet GetDesignations()
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spDesignationMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                try
                {
                    sqlData.Fill(ds);
                    return ds;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    sqlData.Dispose();
                    command.Dispose();
                    con.Dispose();
                }

            }
            catch
            {
                return null;
            }
        }

    }


    /// <summary>
    /// For StoredProcedure spAppoinmentMaster
    /// </summary>
    public class AppointmentMaster
    {
        #region Propeties
        int flag = 0;
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int AppointmentWith { get; set; }
        public string PatientName { get; set; }
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int Flag { get { return flag; } }

        public int AppoinmentId { get; set; }
        #endregion

        #region Methods
        public bool InsertAppointement()
        {
            try
            {
                flag = 0;
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spAppointemntMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.Parameters.AddWithValue("@query", 1);
                    command.Parameters.AddWithValue("@appointment_with", this.AppointmentWith);
                    command.Parameters.AddWithValue("@appointment_date", this.Date);
                    command.Parameters.AddWithValue("@appointment_time", this.Time);
                    command.Parameters.AddWithValue("@patient_Name", this.PatientName);
                    con.Open();
                    flag = Convert.ToInt32(command.ExecuteScalar());
                    con.Close();
                    if (flag !=-99 && flag!=-96)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }
        }
        public DataSet GetAppoineMents(bool all)
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spAppointemntMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                try
                {
                    command.Parameters.AddWithValue("@query",2);
                    if(all==false)
                    {
                        command.Parameters.AddWithValue("@appoinmentid", AppoinmentId);
                    }
                    sqlDataAdapter.Fill(dataSet);
                    return dataSet;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    sqlDataAdapter.Dispose();
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return null;
            }
        }
        public DataSet GetAppoinment()
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spAppointemntMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                try
                {
                    command.Parameters.AddWithValue("@query", 2);
                    command.Parameters.AddWithValue("@doctorid", this.DoctorId);
                    sqlDataAdapter.Fill(dataSet);
                    return dataSet;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    sqlDataAdapter.Dispose();
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteAppoinment()
        {
            try
            {
                flag = 0;
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spAppointemntMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.Parameters.AddWithValue("@query", 3);
                    command.Parameters.AddWithValue("@appoinmentid", this.AppoinmentId);
                    con.Open();
                    flag = Convert.ToInt32(command.ExecuteScalar());
                    con.Close();
                    if (flag == 0)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateAppointment()
        {
            try
            {
                flag = 0;
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spAppointemntMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.Parameters.AddWithValue("@query", 4);
                    command.Parameters.AddWithValue("@appoinmentid", this.AppoinmentId);
                    command.Parameters.AddWithValue("@appointment_with", this.AppointmentWith);
                    command.Parameters.AddWithValue("@appointment_date", this.Date);
                    command.Parameters.AddWithValue("@appointment_time", this.Time);
                    command.Parameters.AddWithValue("@patient_Name", this.PatientName);
                    con.Open();
                    flag = Convert.ToInt32(command.ExecuteScalar());
                    con.Close();
                    if (flag == 0)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }
        }
        public bool IsUpdateTable()
        {
            try
            {
                flag = 0;
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spAppointemntMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.Parameters.AddWithValue("@query", 5);
                    command.Parameters.AddWithValue("@appoinmentid", this.AppoinmentId);
                    con.Open();
                    flag = Convert.ToInt32(command.ExecuteScalar());
                    con.Close();
                    if (flag == 0)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
    public class SlotIntervalMaster
    {
        public static DataSet SelectSlots()
        {
            try
            {
                SqlConnection con = new SqlConnection(StoredProcedre.ConnectionString);
                SqlCommand command = new SqlCommand("spSlotIntervalMaster", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                try
                {
                    sqlData.Fill(ds);
                    return ds;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    sqlData.Dispose();
                    command.Dispose();
                    con.Dispose();
                }

            }
            catch
            {
                return null;
            }
        }
    }

    public class StoredProcedre
    {
        static internal readonly string ConnectionString =
             ConfigurationManager.ConnectionStrings["AppointmentLocalDb"].ConnectionString;
    }
}