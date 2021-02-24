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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Action if not Page Postback is not there 
            if (!IsPostBack)
            {
                ClearControls();
            }
        }

        // Action When User Submit The Data For Login
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString);
                SqlCommand cmd = new SqlCommand("sptblBankUserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try 
                {
                    cmd.Parameters.AddWithValue("@query",3);
                    cmd.Parameters.AddWithValue("@MobNumber",txtboxmobilenumber.Text);
                    cmd.Parameters.AddWithValue("@PIN",txtboxpin.Text);
                    con.Open();
                    int IsUserExits = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if(IsUserExits!=0)
                    {
                        Session.Add("UserId", IsUserExits);
                        Response.Redirect("~/dashboard.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Wardning", "alert('Invalid Credentials or User Not Registered');", true);
                    }
                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Wardning", "alert('Please Try again"+ex.Message+"');", true);
                }
                finally { cmd.Dispose();con.Close();con.Dispose();}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "InvalidPage", "alert('Please Fill data Properly');", true);
            }
        }
        // To Handle Reset The Data
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        // To Clear The Controls
        protected void ClearControls()
        {
            txtboxmobilenumber.Text = "";
            txtboxpin.Text = "";
        }
    }
}