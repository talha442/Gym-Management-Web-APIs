using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Eduvation.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Eduvation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;


        public ClientController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * From dbo.tblClient";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("GYMAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(Client clientinfo)
        {
            string query = @"Insert into tblClient(ClientName, ClientAddress, ClientEmail) 
                           values('"+ clientinfo.ClientName+"', '"+ clientinfo.ClientAddress+"', '"+ clientinfo.ClientEmail+"')";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("GYMAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Client clientinfo)
        {
            string query = @"Update dbo.tbl_Student set 
                           ClientName = '"+ clientinfo.ClientName+"', ClientAddress = '"+ clientinfo.ClientAddress+"', ClientEmail = '"+ clientinfo.ClientEmail+"' where ClientID = '"+ clientinfo.ClientID+"'";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("GYMAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{ID}")]
        public JsonResult Delete(int ID)
        {
            string query = @"Delete from dbo.tblClient where ClientID = '" + ID + @"'";

            DataTable dt = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("GYMAppCon");

            SqlDataReader reader;

            using (SqlConnection con = new SqlConnection(SqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);

                    reader.Close();
                    con.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}