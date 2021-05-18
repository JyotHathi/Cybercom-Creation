using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InventoryData.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        [Route("today/{pageNumber:int:min(1)}")]
        public IHttpActionResult GetTodaysPlacedOrder([FromUri] int pageNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spOrders", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 2);
                sqlCommand.Parameters.AddWithValue("@pageNumber", pageNumber);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@queryNo", 4);
                con.Open();
                int totalCount = Convert.ToInt32(sqlCommand.ExecuteScalar());
                con.Close();

                var result = new
                {
                    totalRecords = totalCount,
                    data = dataSet.Tables[0]
                };
                return Ok(result);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Route("page/{pageNumber:int:min(1)}")]
        public IHttpActionResult GetOrders([FromUri]int pageNumber,[FromUri]string fromDate="",[FromUri]string todate="",[FromUri]string dir="")
        {
            try
            {
                
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spOrders", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 1);
                sqlCommand.Parameters.AddWithValue("@pageNumber", pageNumber);
                sqlCommand.Parameters.AddWithValue("@fromDate",fromDate);
                sqlCommand.Parameters.AddWithValue("@toDate", todate);
                sqlCommand.Parameters.AddWithValue("@dir", dir);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@queryNo", 3);
                sqlCommand.Parameters.AddWithValue("@fromDate", fromDate);
                sqlCommand.Parameters.AddWithValue("@toDate", todate);
                sqlCommand.Parameters.AddWithValue("@dir", dir);
                con.Open();
                int totalCount = Convert.ToInt32(sqlCommand.ExecuteScalar());
                con.Close();

                var result = new
                {
                    totalRecords = totalCount,
                    data = dataSet.Tables[0]
                };
                return Ok(result);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
