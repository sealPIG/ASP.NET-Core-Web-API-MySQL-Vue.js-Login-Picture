using System.Data;
using FourthHW.Model;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace FourthHW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StaffController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from sealdb.Staff
            ";  //設定 Mysql 指令

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Connection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{StaffEmail}/{StaffPassword}")]
        public int GetId(String StaffEmail, String StaffPassword)
        {
            string query = @"
                        select StaffName,StaffEmail,StaffPassword,StaffMode
                        from sealdb.Staff
                        where StaffEmail = @StaffEmail
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Connection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@StaffEmail", StaffEmail);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    myReader.Close();
                    mycon.Close();
                }
            }

            if (table.Rows.Count.Equals(0))
                return 0;

            if (StaffPassword.Equals(table.Select()[0]["StaffPassword"].ToString()))
                return (int)table.Select()[0]["StaffMode"];
            else
                return 0;
        }

        [HttpPost]
        public JsonResult Post(Staff staff)
        {
            string query = @"
                        insert into sealdb.Staff (StaffName,StaffEmail,StaffPassword,StaffMode) 
                        values (@StaffName,@StaffEmail,@StaffPassword,@StaffMode);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Connection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@StaffName", staff.StaffName);
                    myCommand.Parameters.AddWithValue("@StaffEmail", staff.StaffEmail);
                    myCommand.Parameters.AddWithValue("@StaffPassword", staff.StaffPassword);
                    myCommand.Parameters.AddWithValue("@StaffMode", staff.StaffMode);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
    }
}
