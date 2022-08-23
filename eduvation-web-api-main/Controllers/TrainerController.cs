using Eduvation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Eduvation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;


        public TrainerController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * From dbo.tblTrainer";

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
        public JsonResult Post(Trainer trainerinfo)
        {
            string query = @"Insert into tblTrainer(TrainerName, TrainerEmail, TrainerSalary) 
                           values('" + trainerinfo.TrainerName + "', '" + trainerinfo.TrainerEmail + "', " + trainerinfo.TrainerSalary+ ")";

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
        public JsonResult Put(Trainer trainerinfo)
        {
            string query = @"Update dbo.tblTrainer set 
                           TrainerName = '" + trainerinfo.TrainerName + "', TrainerEmail = '" + trainerinfo.TrainerEmail + "'," +
                           " TeacherSalary = " + trainerinfo.TrainerSalary + " where TrainerID = " + trainerinfo.TrainerID + "";

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
            string query = @"Delete from dbo.tblTrainer where TrainerID = '" + ID + @"'";

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