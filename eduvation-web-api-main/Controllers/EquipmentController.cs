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

namespace Eduvation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public EquipmentController(IConfiguration configuration) 
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select EquipmentID, EquipmentName From dbo.tblEquipment";

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
        public JsonResult Post(Equipment equipmentinfo)
        {
            string query = @"Insert into dbo.tblEquipment values('"+ equipmentinfo.EquipmentName+@"')";

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
        public JsonResult Put(Equipment equipmentinfo)
        {
            string query = @"Update dbo.tblEquipment set EquipmentName = '" + equipmentinfo.EquipmentName + @"'
                           where EquipmentID = '"+ equipmentinfo.EquipmentID+@"'
                           ";

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
            string query = @"Delete from dbo.tblEquipment
                           where EquipmentID = '" + ID + @"'
                           ";

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