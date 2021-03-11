using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
namespace Appointment_Booking
{
    public partial class ManageDoctors : System.Web.UI.Page
    {
        DoctorMaster doctorMaster = new DoctorMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Designations"] == null)
                {
                    LoadDesgnations();
                }
                else
                {
                    DroDownDesignation.DataTextField = "Designation";
                    DroDownDesignation.DataValueField = "Designation_Id";
                    DroDownDesignation.DataSource = (DataTable)Session["Designations"];
                    DroDownDesignation.DataBind();
                    ListItem listItem = new ListItem("Select Designation", "-1");
                    DroDownDesignation.Items.Add(listItem);
                    Session["Designations"] = DesignationMaster.GetDesignations().Tables[0];
                }
                LoadData();
                ClearControls();
            }
        }

        protected void LoadDesgnations()
        {
            DroDownDesignation.DataTextField = "Designation";
            DroDownDesignation.DataValueField = "Designation_Id";
            if (DesignationMaster.GetDesignations() != null)
            {
                DroDownDesignation.DataSource = DesignationMaster.GetDesignations().Tables[0];
                DroDownDesignation.DataBind();
                ListItem listItem = new ListItem("Select Designation", "-1");
                DroDownDesignation.Items.Add(listItem);
                Session["Designations"] = DesignationMaster.GetDesignations().Tables[0];
            }


        }
        protected void RptrDoctors_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DataTable dt = (DataTable)Session["DoctorsData"];
            DataRow dataRow = dt.Select($"Doctor_Id='{e.CommandArgument}'")[0];
            if (e.CommandName.Equals("Edit"))
            {
                TxtBoxUDocName.Text = dataRow["Doctor Name"].ToString();
                DroDownUDesignation.DataTextField = "Designation";
                DroDownUDesignation.DataValueField = "Designation_Id";
                DroDownUDesignation.DataSource = (DataTable)Session["Designations"];
                DroDownUDesignation.DataBind();
                ListItem listItem = new ListItem("Select Designation", "-1");
                DroDownUDesignation.Items.Add(listItem);
                DroDownUDesignation.SelectedValue = dataRow["Doc_Desg"].ToString();
                TxtBoxUEmail.Text = dataRow["Doctor Email"].ToString();
                TxtBoxUMobNum.Text = dataRow["Doctor ContactNo."].ToString();
                TxtBoxUToTime.Text= dataRow["To time"].ToString();
                TxtBoxFromUTime.Text = dataRow["From time"].ToString();
                Session["DocterId"] = Convert.ToInt32(e.CommandArgument);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#EditDoctorModal').modal('show');", true);
            }
            else if (e.CommandName.Equals("View"))
            {
                LblValDocName.Text = dataRow["Doctor Name"].ToString();
                LblVValDocDesignation.Text = dataRow["Doctor_Designation"].ToString();
                LblValEmail.Text = dataRow["Doctor Email"].ToString();
                LblValAvailFrom.Text = dataRow["From Time"].ToString();
                LblValAvilTill.Text = dataRow["To time"].ToString();
                LblValMobileNumber.Text = dataRow["Doctor ContactNo."].ToString();
                DocImg.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])dataRow["Doctor Image"]);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewPopup", "$('#ViewDoctorModal').modal('show');", true);
            }
            else if (e.CommandName.Equals("Delete"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteWarning", "$('#DeleteDoctorModal').modal('show');", true);
                Session["DocterId"] = Convert.ToInt32(e.CommandArgument);
            }
            LoadData();
        }

        protected void LoadData()
        {
            DataSet ds = doctorMaster.SelectDoctors();
            if (ds != null)
            {
                Session["DoctorsData"] = ds.Tables[0];
                RptrDoctors.DataSource = ds.Tables[0];
                RptrDoctors.DataBind();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", "alert('Try After Some Time');", true);
        }
        protected void BtnWarningYes_Click(object sender, EventArgs e)
        {
            doctorMaster.DoctorId = (int)Session["DocterId"];
            doctorMaster.DeleteDoctor();
            Session.Remove("DoctorId");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Deleted Sucessfully')", true);
        }

        #region Insert
        protected void CusValiFromTime_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dateTime = Convert.ToDateTime(TxtBoxFromTime.Text);
            if (dateTime.Hour > 8)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CusValiToTime_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dateTime = Convert.ToDateTime(TxtBoxFromTime.Text);
            DateTime dateTime2 = Convert.ToDateTime(TxtBoxToTime.Text);
            if (dateTime.Hour < dateTime2.Hour)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        protected void CusValiPhoto_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (FileUpldPhoto.HasFile &&
                FileUpldPhoto.PostedFile.ContentLength < 50000
                && (Path.GetExtension(FileUpldPhoto.PostedFile.FileName).ToUpper().Equals(".JPG") ||
                Path.GetExtension(FileUpldPhoto.PostedFile.FileName).ToUpper().Equals(".PNG")))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                DoctorMaster doctorMaster = new DoctorMaster();
                doctorMaster.DoctorName = TxtBoxDocName.Text;
                doctorMaster.Designation = Convert.ToInt32(DroDownDesignation.SelectedValue);
                doctorMaster.DoctorEmail = TxtBoxEmail.Text;
                doctorMaster.DoctorMobileNumber = TxtBoxMobNum.Text;
                doctorMaster.FromTime = Convert.ToDateTime(TxtBoxFromTime.Text);
                doctorMaster.ToTime = Convert.ToDateTime(TxtBoxToTime.Text);
                Stream stream = FileUpldPhoto.PostedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                doctorMaster.DoctorImage = binaryReader.ReadBytes((int)stream.Length);

                if (doctorMaster.InsertData())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Added Sucessfully')", true);
                    LoadData();
                }
                else
                {
                    if (doctorMaster.Flag == -99)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ExistsMessage", "alert('Already Exits')", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ErorrMessage", "alert('Please Try Again Some Erorr Ocuured')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErorrMessage", "alert('Please Fill Data Properly')", true);
            }
        }
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        protected void ClearControls()
        {
            TxtBoxDocName.Text = "";
            TxtBoxEmail.Text = "";
            TxtBoxFromTime.Text = "";
            TxtBoxMobNum.Text = "";
            TxtBoxToTime.Text = "";
            DroDownDesignation.SelectedValue = "-1";
        }
        #endregion

        #region Update
        protected void CusValiUPhoto_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (FileUpldUPhoto.HasFile &&
                FileUpldUPhoto.PostedFile.ContentLength < 50000
                && (Path.GetExtension(FileUpldUPhoto.PostedFile.FileName).ToUpper().Equals(".JPG") ||
                Path.GetExtension(FileUpldUPhoto.PostedFile.FileName).ToUpper().Equals(".PNG")))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }
        protected void CusValiFromUTime_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dateTime = Convert.ToDateTime(TxtBoxFromUTime.Text);
            if (dateTime.Hour > 8)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

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
                Stream stream = FileUpldUPhoto.PostedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                doctorMaster.DoctorImage = binaryReader.ReadBytes((int)stream.Length);
                doctorMaster.DoctorId = (int)Session["DocterId"];
                if (doctorMaster.UpdateDoctor())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Updated Sucessfully')", true);
                    LoadData();
                }
                else
                {
                    if (doctorMaster.Flag == -98)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ExistsMessage", "alert('Error In Update Record')", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ErorrMessage", "alert('Please Try Again Some Erorr Ocuured')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPopup", "$('#EditDoctorModal').modal('show');", true);
            }
        }
        #endregion
    }
}