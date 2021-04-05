using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace Appointment_Booking
{
    public partial class Doctor : System.Web.UI.Page
    {
        DoctorMaster doctorMaster = new DoctorMaster();
        AppointmentMaster appointmentMaster = new AppointmentMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAlert", "document.getElementById('displayAlert').style.display = 'none';", true);

                if (Session["Designations"] == null)
                {
                    LoadDesgnations();
                }
                if (Session["Slots"] == null)
                {
                    LoadIntervals();
                }
            }
        }
        #region LoadData
        // To Load Designation Information
        protected void LoadDesgnations()
        {
           
            if (DesignationMaster.GetDesignations() != null)
            {
                Session["Designations"] = DesignationMaster.GetDesignations().Tables[0];
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage",
                    "document.getElementById('displayAlert').innerHTML = 'Error Occured, Please Try After Sometime';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayAlert').style.display = 'block';", true);
        }

        // To Load Slot Interval Information
        protected void LoadIntervals()
        {
            DataSet ds = SlotIntervalMaster.SelectSlots();
            if (ds != null)
            {
                ListItem listItem = new ListItem("Select Slot Interval Duration", "-1");
                Session["Slots"] = ds.Tables[0];
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage",
                    "document.getElementById('displayAlert').innerHTML = 'Error Occured, Please Try After Sometime';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayAlert').style.display = 'block';", true);
        }
        
        #endregion

        #region Commands and Action Based on Command
        //Commands
        protected void BtnCommands_Click(object sender, EventArgs e)
        {
            try
            {
                int commandArgument = Convert.ToInt32(HidenId.Value.ToString());
                string commandName = HidenCommand.Value.ToString();
                doctorMaster.DoctorId = commandArgument;
                DataTable dt = doctorMaster.SelectDoctors(false).Tables[0];
                DataRow dataRow = dt.Select($"Doctor_Id='{commandArgument}'")[0];
                if (commandName.Equals("View"))
                {
                    LblValDocName.Text = dataRow["Doctor Name"].ToString();
                    LblVValDocDesignation.Text = dataRow["Doctor_Designation"].ToString();
                    LblValEmail.Text = dataRow["Doctor Email"].ToString();
                    LblValAvailFrom.Text = dataRow["From Time"].ToString();
                    LblValAvilTill.Text = dataRow["To time"].ToString();
                    LblValMobileNumber.Text = dataRow["Doctor ContactNo."].ToString();
                    DocImg.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])dataRow["Doctor Image"]);
                    LblValSlot.Text = dataRow["SlotText"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewPopup", "$('#ViewDoctorModal').modal('show');", true);
                }
                else if (commandName.Equals("ViewApointments"))
                {
                    appointmentMaster.DoctorId = Convert.ToInt32(commandArgument);
                    DataSet ds = appointmentMaster.GetAppoinment();
                    appointmentMaster.DoctorId = Convert.ToInt32(commandArgument);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            RptrListofAppointments.DataSource = ds.Tables[0];
                            RptrListofAppointments.DataBind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewPopup", "document.getElementById('displayAlert').style.display = 'none';$('#AppoinmentList').modal('show');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "NoData", "" +
                                "document.getElementById('displayAlert').innerHTML = 'No Appointments';" +
                                "document.getElementById('displayAlert').style.display = 'block';" +
                                "document.getElementById('displayAlert').className = 'alert alert-primary';", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('Please Try After Some Time');", true);
                    }
                }
                else if (commandName.Equals("Delete"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteWarning", "$('#DeleteDoctorModal').modal('show');", true);
                    Session["DocterId"] = Convert.ToInt32(commandArgument);
                }
                else if (commandName.Equals("Edit"))
                {
                    TxtBoxUDocName.Text = dataRow["Doctor Name"].ToString();

                    DroDownUDesignation.DataTextField = "Designation";
                    DroDownUDesignation.DataValueField = "Designation_Id";
                    DroDownUDesignation.DataSource = (DataTable)Session["Designations"];
                    Session["DoctorImage"] = (byte[])dataRow["Doctor Image"];
                    DroDownUDesignation.DataBind();
                    ListItem listItem = new ListItem("Select Designation", "-1");

                    DroDownUDesignation.Items.Add(listItem);
                    DroDownUDesignation.SelectedValue = dataRow["Doc_Desg"].ToString();
                    TxtBoxUEmail.Text = dataRow["Doctor Email"].ToString();
                    TxtBoxUMobNum.Text = dataRow["Doctor ContactNo."].ToString();
                    TxtBoxUToTime.Text = dataRow["To time"].ToString();
                    TxtBoxFromUTime.Text = dataRow["From time"].ToString();

                    DroDownUSlot.DataSource = (DataTable)Session["Slots"];
                    DroDownUSlot.DataTextField = "SlotText";
                    DroDownUSlot.DataValueField = "SlotId";
                    DroDownUSlot.DataBind();
                    DroDownUSlot.Items.Add(new ListItem("Select Slot Interval Duration", "-1"));
                    DroDownUSlot.SelectedValue = dataRow["SlotIntervalID"].ToString();


                    Session["DocterId"] = Convert.ToInt32(commandArgument);
                    BtnReset.Visible = false;
                    BtnSubmit.Visible = false;
                    BtnUpdate.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "" +
                        "document.getElementById('displayInUpAlert').style.display = 'none';" +
                        "$('#EditDoctorModal').modal('show');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FillDataAlert",
                    "document.getElementById('displayAlertAppointment').style.display = 'block';" +
                            "document.getElementById('displayAlert').innerHTML = 'Something Went Wrong.';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';", true);

            }
        }

        // To Handle Deletion Confirmation
        protected void BtnWarningYes_Click(object sender, EventArgs e)
        {
            doctorMaster.DoctorId = (int)Session["DocterId"];
            doctorMaster.DeleteDoctor();
            Session.Remove("DoctorId");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage",
                "document.getElementById('displayAlert').innerHTML = 'User Deleted Successfully';" +
                "document.getElementById('displayAlert').style.display = 'block';" +
                "document.getElementById('displayAlert').className = 'alert alert-success';", true);
        }

        #endregion
        
        #region Update
        // To Handle Server Validation of Photo Update Popup
        protected void CusValiUPhoto_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((FileUpldUPhoto.HasFile &&
                FileUpldUPhoto.PostedFile.ContentLength < 50000
                && (Path.GetExtension(FileUpldUPhoto.PostedFile.FileName).ToUpper().Equals(".JPG") ||
                Path.GetExtension(FileUpldUPhoto.PostedFile.FileName).ToUpper().Equals(".PNG"))) || Session["DoctorImage"] != null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }
        // To Handle Server Validation of To Time in Update Popup
        protected void CusValiUToTime_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dateTime = Convert.ToDateTime(TxtBoxFromUTime.Text);
            DateTime dateTime2 = Convert.ToDateTime(TxtBoxUToTime.Text);
            if (dateTime.Hour < dateTime2.Hour)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        // To Submit Updated Data
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                DoctorMaster doctorMaster = new DoctorMaster();
                doctorMaster.DoctorName = TxtBoxUDocName.Text;
                doctorMaster.Designation = Convert.ToInt32(DroDownUDesignation.SelectedValue);
                doctorMaster.DoctorEmail = TxtBoxUEmail.Text;
                doctorMaster.DoctorMobileNumber = TxtBoxUMobNum.Text;
                doctorMaster.FromTime = Convert.ToDateTime(TxtBoxFromUTime.Text);
                doctorMaster.ToTime = Convert.ToDateTime(TxtBoxUToTime.Text);
                doctorMaster.SlotIntervalID = Convert.ToInt32(DroDownUSlot.SelectedValue);
                if (FileUpldUPhoto.HasFile)
                {
                    Stream stream = FileUpldUPhoto.PostedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    doctorMaster.DoctorImage = binaryReader.ReadBytes((int)stream.Length);
                }
                else
                {
                    doctorMaster.DoctorImage = (byte[])Session["DoctorImage"];

                }
                doctorMaster.DoctorId = (int)Session["DocterId"];
                if (doctorMaster.UpdateDoctor())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage",
                        "document.getElementById('displayAlert').innerHTML = 'Updated Sucessfully';" +
                        "document.getElementById('displayAlert').style.display = 'block';" +
                        "document.getElementById('displayAlert').className = 'alert alert-success';" +
                        "", true);
                }
                else
                {
                    if (doctorMaster.Flag == -98)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ExistsMessage",
                            "document.getElementById('displayAlert').innerHTML = 'Already User Exits, Update Fail !!';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayAlert').style.display = 'block';"
                            , true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ErorrMessage",
                            "document.getElementById('displayAlert').innerHTML = 'Error Occured, Please Try After Sometime';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayAlert').style.display = 'block';"
                            , true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#EditDoctorModal').modal('show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErorrMessage",
                            "document.getElementById('displayInUpAlert').innerHTML = 'Error Occured, Please Try After Sometime';" +
                            "document.getElementById('displayInUpAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayInUpAlert').style.display = 'block';"
                            , true);
            }
        }
        #endregion

        #region Insert 
        protected void ImgAddDoctor_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideAlert", "document.getElementById('displayInUpAlert').style.display = 'none';", true);
            Session["DoctorImage"] = null;
            BtnUpdate.Click +=new EventHandler(BtnSubmit_Click);
            BtnReset.Visible = true;
            BtnSubmit.Visible = true;
            BtnUpdate.Visible = false;

            DroDownUDesignation.DataTextField = "Designation";
            DroDownUDesignation.DataValueField = "Designation_Id";
            DroDownUDesignation.DataSource = (DataTable)Session["Designations"];
            DroDownUDesignation.DataBind();
            ListItem listItem = new ListItem("Select Designation", "-1");
            DroDownUDesignation.Items.Add(listItem);
            DroDownUDesignation.SelectedValue = "-1";

            DroDownUSlot.DataSource = (DataTable)Session["Slots"];
            DroDownUSlot.DataTextField = "SlotText";
            DroDownUSlot.DataValueField = "SlotId";
            DroDownUSlot.DataBind();
            DroDownUSlot.Items.Add(new ListItem("Select Slot Interval Duration", "-1"));
            DroDownUSlot.SelectedValue = "-1";

            ClearControls();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#EditDoctorModal').modal('show');", true);
        }

        // To Reset The Data Resest Event
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        // To Submit / Insert The Data
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                DoctorMaster doctorMaster = new DoctorMaster();
                doctorMaster.DoctorName = TxtBoxUDocName.Text;
                doctorMaster.Designation = Convert.ToInt32(DroDownUDesignation.SelectedValue);
                doctorMaster.DoctorEmail = TxtBoxUEmail.Text;
                doctorMaster.DoctorMobileNumber = TxtBoxUMobNum.Text;
                doctorMaster.FromTime = Convert.ToDateTime(TxtBoxFromUTime.Text);
                doctorMaster.ToTime = Convert.ToDateTime(TxtBoxUToTime.Text);
                doctorMaster.SlotIntervalID = Convert.ToInt32(DroDownUSlot.SelectedValue);
                Stream stream = FileUpldUPhoto.PostedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                doctorMaster.DoctorImage = binaryReader.ReadBytes((int)stream.Length);

                if (doctorMaster.InsertData())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage",
                        "document.getElementById('displayAlert').innerHTML = 'Added Sucessfully';" +
                        "document.getElementById('displayAlert').style.display = 'block';" +
                        "document.getElementById('displayAlert').className = 'alert alert-success';" +
                        "", true);
                }
                else
                {
                    if (doctorMaster.Flag == -99)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ExistsMessage",
                            "document.getElementById('displayAlert').innerHTML = 'Already Exists';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayAlert').style.display = 'block';"
                            , true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ErorrMessage",
                            "document.getElementById('displayAlert').innerHTML = 'Error Occured, Please Try After Sometime';" +
                            "document.getElementById('displayAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayAlert').style.display = 'block';"
                            , true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#EditDoctorModal').modal('show');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErorrMessage",
                            "document.getElementById('displayInUpAlert').innerHTML = 'Error Occured, Please Try After Sometime';" +
                            "document.getElementById('displayInUpAlert').className = 'alert alert-danger';" +
                            "document.getElementById('displayInUpAlert').style.display = 'block';"
                            , true);
            }
        }
        // To Clear Controls
        protected void ClearControls()
        {
            TxtBoxUDocName.Text = "";
            TxtBoxUEmail.Text = "";
            TxtBoxFromUTime.Text = "";
            TxtBoxUMobNum.Text = "";
            TxtBoxUToTime.Text = "";
            DroDownUDesignation.SelectedValue = "-1";
            DroDownUSlot.SelectedValue = "-1";
        }
        #endregion

        #region Web Methods
        [WebMethod, ScriptMethod()]
        public static bool IsUserExits(string mobileNumber, string email)
        {
            DoctorMaster doctorMaster = new DoctorMaster();
            doctorMaster.DoctorEmail = email.ToString();
            doctorMaster.DoctorMobileNumber = mobileNumber.ToString();
            bool IsUser = doctorMaster.IsUserExists();
            return IsUser;
        }

        [WebMethod,ScriptMethod(ResponseFormat =ResponseFormat.Json)]
        public static string DoctorsData()
        {
            DoctorMaster doctorMaster = new DoctorMaster();
            DataSet doctors= doctorMaster.SelectDoctors(true);
            List<DoctorBrief> doctorBriefs = new List<DoctorBrief>();
            foreach(DataRow dataRow in doctors.Tables[0].Rows)
            {
                doctorBriefs.Add(new DoctorBrief
                {
                    Doctor_Designation = dataRow["Doctor_Designation"].ToString(),
                    Doctor_Name = dataRow["Doctor Name"].ToString(),
                    Doctor_Id = Convert.ToInt32(dataRow["Doctor_Id"])
                });
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(doctorBriefs);
        }
        #endregion
        
    }
}