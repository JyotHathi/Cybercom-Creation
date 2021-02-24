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
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Only if Page Postaback is Not There
            if (!IsPostBack)
            {
                // Go Only if Proper Session Is ther
                if (Session["UserId"] == null)
                {
                    Response.Redirect("./Login.aspx");
                }
                else
                {
                    if (Session["UserData"] == null)
                    {
                        SessonData();
                    }
                    else
                    {
                        lbluname.Text = "Welcome, " + ((DataTable)Session["UserData"]).Rows[0]["UserName"].ToString();
                    }
                }
                ClearData();
            }
        }

        // Action based on Submission of Choice 
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            pnlop.Visible = true;
            pnlop.Enabled = true;
            pnlselect.Enabled = false;
            if (drodownSelectOption.SelectedValue.Equals("Check balance"))
            {
                btnsubmitdata.Visible = false;
                txtboxopdata.Enabled = false;
                lblopstatus.Text = "Your Blance";
                txtboxopdata.Text = ((DataTable)Session["UserData"]).Rows[0]["Balance"].ToString();
                pnlselect.Enabled = true;
            }
            else if (drodownSelectOption.SelectedValue.Equals("Deposit Amount"))
            {
                btnsubmitdata.Visible = true;
                txtboxopdata.Enabled = true;
                lblopstatus.Text = "Enter Amount";
                txtboxopdata.Text = "";
            }
            else if (drodownSelectOption.SelectedValue.Equals("Withdrawal Amount"))
            {
                btnsubmitdata.Visible = true;
                txtboxopdata.Enabled = true;
                lblopstatus.Text = "Enter Amount";
                txtboxopdata.Text = "";
            }
            else
            {
                Session.RemoveAll();
                Response.Redirect("~/Login.aspx");
            }

            updtpnlSelection.Update();
            updtpnlop.Update();
        }
        
        // Action In Case of Deposit to Widhdrawal amount
        protected void btnsubmitdata_Click(object sender, EventArgs e)
        {
            pnlop.Enabled = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sptblBankUserDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.Clear();
                if (drodownSelectOption.SelectedValue.Equals("Deposit Amount"))
                {
                    cmd.Parameters.AddWithValue("@query", 5);
                    cmd.Parameters.AddWithValue("@InitAmount", txtboxopdata.Text);
                    cmd.Parameters.AddWithValue("@userid", (int)Session["UserId"]);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Amount Deposited Sucessfully');", true);
                }
                else if (drodownSelectOption.SelectedValue.Equals("Withdrawal Amount"))
                {
                    DataTable tbl = (DataTable)Session["UserData"];
                    int amount = Convert.ToInt32(txtboxopdata.Text);
                    if (Convert.ToInt32(tbl.Rows[0]["MinWidthAmount"]) >= amount
                        && Convert.ToInt32(tbl.Rows[0]["Balance"])-amount >=1000)
                    {
                        cmd.Parameters.AddWithValue("@query",4);
                        cmd.Parameters.AddWithValue("@InitAmount", txtboxopdata.Text);
                        cmd.Parameters.AddWithValue("@userid", (int)Session["UserId"]);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "alert('Amount Widthdrawaled Sucessfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Warning", "alert('Exceeds Minimum Widthdrawal Amount or After Widthrawal Minimum Balance Not Maintain');", true);
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Warning", "alert('Please Try again');", true);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
                pnlselect.Enabled = true;
                SessonData();
                updtpnlSelection.Update();
                updtpnlop.Update();
            }
        }

        // To Clear Controls
        protected void ClearData()
        {
            pnlop.Visible = false;
            txtboxopdata.Text = "";
            drodownSelectOption.SelectedValue = "--Select--";

            updtpnlSelection.Update();
            updtpnlop.Update();
        }

        // To Fill The Session Data
        protected void SessonData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString);
            SqlCommand command = new SqlCommand("sptblBankUserDetails", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                command.Parameters.AddWithValue("@query", 7);
                command.Parameters.AddWithValue("@userid", (int)Session["UserId"]);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "BankUser");
                Session["UserData"] = dataSet.Tables["BankUser"];
                lbluname.Text = "Welcome, " + dataSet.Tables["BankUser"].Rows[0]["UserName"].ToString();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Warning", "alert('Please Try again');window.location.assign('./Login.aspx')", true);
            }
            finally
            {
                adapter.Dispose();
                command.Dispose();
                con.Close();
                con.Dispose();

                updtpnlSelection.Update();
                updtpnlop.Update();
            }
        }

        // For Logout
        protected void lbkbtnlogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }
    }
}