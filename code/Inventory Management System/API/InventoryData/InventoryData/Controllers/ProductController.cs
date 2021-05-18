using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using InventoryData.Models;

namespace InventoryData.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("available/{pagenumber:int:min(1)}")]
        public IHttpActionResult GetAvailableProducts([FromUri]int pageNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spProducts", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 1);
                sqlCommand.Parameters.AddWithValue("@pageNumber", pageNumber);
                
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@queryNo", 7);
                
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

        [HttpGet]
        [Route("less/{pagenumber:int:min(1)}")]
        public IHttpActionResult GetLessStockProducts([FromUri] int pageNumber)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spProducts", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 2);
                sqlCommand.Parameters.AddWithValue("@pageNumber", pageNumber);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@queryNo", 8);
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

        [HttpGet]
        [Route("page/{pagenumber:int:min(1)}")]
        public IHttpActionResult GetProducts([FromUri] int pageNumber, [FromUri] string search = "")
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spProducts", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 3);
                sqlCommand.Parameters.AddWithValue("@pageNumber", pageNumber);
                sqlCommand.Parameters.AddWithValue("@search", search);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@queryNo", 9);
                sqlCommand.Parameters.AddWithValue("@search", search);
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

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetProduct([FromUri]int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spProducts", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 6);
                sqlCommand.Parameters.AddWithValue("@productId", id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                return Ok(dataSet.Tables[0]);
            }
            catch
            {
                return InternalServerError();
            }

        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spProducts", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 4);
                sqlCommand.Parameters.AddWithValue("@name", product.Name);
                sqlCommand.Parameters.AddWithValue("@stock", product.Stock);
                sqlCommand.Parameters.AddWithValue("@price", product.Price);
                con.Open();
                int data = Convert.ToInt32(sqlCommand.ExecuteScalar());
                con.Close();
                return Ok(new { id = data });
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Put([FromUri] int id, [FromBody] Product product)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InvManSysDb"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand("spProducts", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@queryNo", 5);
                sqlCommand.Parameters.AddWithValue("@name", product.Name);
                sqlCommand.Parameters.AddWithValue("@stock", product.Stock);
                sqlCommand.Parameters.AddWithValue("@price", product.Price);
                sqlCommand.Parameters.AddWithValue("@productId", id);
                con.Open();
                sqlCommand.ExecuteNonQuery();
                con.Close();
                return Ok(new { sucess = "Updated Successfully" });
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
