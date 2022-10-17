using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using FourthHW.Model;

namespace FourthHW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        //private readonly static Dictionary<string, string> _contentTypes = new Dictionary<string, string>
        //{
            //{".jpg", "image/jpeg"}
        //};
        public SensorController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from sealdb.Sensor
            ";  //設定 Mysql 指令

            DataTable table = new DataTable();      //新增一個table來存放資料
            string sqlDataSource = _configuration.GetConnectionString("Connection");     //設定連線
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();   //讀取資料庫
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);       //將 table 以 Json 的形式顯示出來
        }

        [HttpGet("{id}")]
        public JsonResult GetId(int id)
        {
            string query = @"
                        select SensorId,SensorName,SensorValue,SensorFileName
                        from sealdb.Sensor
                        where SensorId = @SensorId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Connection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@SensorId", id);     //將讀取到的 id 帶入

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Sensor sensor)
        {
            string query = @"
                        insert into sealdb.Sensor (SensorId,SensorName,SensorValue,SensorFileName) 
                        values (@SensorId,@SensorName,@SensorValue,@SensorFileName);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Connection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@SensorId", sensor.SensorId);        //將讀到的值寫進 myCommand 裡
                    myCommand.Parameters.AddWithValue("@SensorName", sensor.SensorName);
                    myCommand.Parameters.AddWithValue("@SensorValue", sensor.SensorValue);
                    myCommand.Parameters.AddWithValue("@SensorFileName", sensor.SensorFileName);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Sensor sensor)
        {
            string query = @"
                        update sealdb.Sensor set 
                        SensorName = @SensorName,SensorValue = @SensorValue,SensorFileName = @SensorFileName
                        where SensorId = @SensorId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Connection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@SensorId", sensor.SensorId);
                    myCommand.Parameters.AddWithValue("@SensorName", sensor.SensorName);
                    myCommand.Parameters.AddWithValue("@SensorValue", sensor.SensorValue);
                    myCommand.Parameters.AddWithValue("@SensorFileName", sensor.SensorFileName);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from sealdb.Sensor 
                        where SensorId = @SensorId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Connection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@SensorId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {

                var httpReuest = Request.Form;
                var postedFile = httpReuest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photo/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

        [Route("SaveFile")]
        [HttpGet]
        public JsonResult GetFile()
        {
            try
            {
                var httpReuest = Request.Form;
                var postedFile = httpReuest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photo/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }
    }
}
