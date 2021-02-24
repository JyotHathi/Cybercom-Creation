using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankApp
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                drodownSelectOption.SelectedValue = "--Select--";
            }
        }

        // Based On Choice Subbmition
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(drodownSelectOption.SelectedValue.Equals("Register"))
            {
                Response.Redirect("~/Registration.aspx");
            }
            else if (drodownSelectOption.SelectedValue.Equals("Login"))
            {
                Response.Redirect("~/Login.aspx");
            }
            else 
            { 
               Response.Redirect("~/default.aspx");
            }
        }
    }
}