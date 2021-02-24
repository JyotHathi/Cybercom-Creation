using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BankApp
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Perfrom Only if Not Postabck
            if(!IsPostBack)
            {
                ClearControls();
            }
        }

        // Event When User Submit The data
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Do Only if Page Is Valid
            if(Page.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString);
                SqlCommand cmd = new SqlCommand("sptblBankUserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    // First Check user with Same Mobile Number is exits if Yes Then Don't Allow New Registration
                    cmd.Parameters.AddWithValue("@query", 1);
                    cmd.Parameters.AddWithValue("@MobNumber", txtboxmobilenumber.Text);
                    con.Open();
                    int isUserExists=Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                    // If User is Not Registered
                    if (isUserExists == 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@query", 2);
                        cmd.Parameters.AddWithValue("@Name", txtboxName.Text);
                        cmd.Parameters.AddWithValue("@MobNumber", txtboxmobilenumber.Text);
                        cmd.Parameters.AddWithValue("@PIN", txtboxpin.Text);
                        cmd.Parameters.AddWithValue("@InitAmount", txtboxamount.Text);
                        cmd.Parameters.AddWithValue("@MinWithAmount", txtboxMinWithDrawAmount.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SucessMessage", "alert('User Registered Successfully');window.location.assign('./default.aspx');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "UserExitsMessage", "alert('User Already Exists');", true);
                        ClearControls();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Warning", "alert('Please Try Again " + ex.Message + "');window.location.assign('./default.aspx')", true);
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "InvalidPage", "alert('Please Fill data Properly');", true);
            }
        }

        // To Handle Reset Event
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        
        // To Clear Controls.
        protected void ClearControls()
        {
            txtboxamount.Text = "";
            txtboxMinWithDrawAmount.Text = "";
            txtboxmobilenumber.Text = "";
            txtboxName.Text = "";
            txtboxpin.Text = "";
        }
    }
}