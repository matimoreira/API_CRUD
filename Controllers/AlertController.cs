using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        // GET: api/Alert
        [HttpGet("{top}")]
        public IEnumerable<Alert> Get(int top)
        {
            var result = new List<Alert>();

            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = $"SELECT top {top}* FROM alert";
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                result.Add(
                    new Alert(
                            reader.GetInt32(0), 
                            reader.GetString(1), 
                            reader.GetString(2), 
                            reader.GetString(3), 
                            reader.GetInt32(4), 
                            reader.GetString(5)
                        )
                    );
            }

            reader.Close();
            connection.Close();

            return result;
        }
        //GET: api/Alert
        [HttpGet()]
        public IEnumerable<Alert> Get()
        {
            var result = new List<Alert>();

            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = $"SELECT * FROM alert";
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                result.Add(
                    new Alert(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetInt32(4),
                            reader.GetString(5)
                        )
                    );
            }

            reader.Close();
            connection.Close();

            return result;
        }

        // GET: api/Alert/5
        /*
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST: api/Alert
        [HttpPost]
        public OkObjectResult Post([FromBody] Alert alert)
        {
            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = $"INSERT INTO alert(id, name, notifywhenarriving, notifywhenleaving, enterpriseid, active) values ({alert.id}, '{alert.name}', '{alert.notifywhenarriving}', '{alert.notifywhenleaving}', '{alert.enterpriseid}', '{alert.active}')";
            var command = new SqlCommand(sql, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return this.Ok($"No se inserto: {e.Message}");
                throw;
            }
            connection.Close();
            return this.Ok("Se inserto correctamente");

        } 

        // PUT: api/Alert/5
        [HttpPut("{id}")]
        public OkObjectResult Put(int id, [FromBody] Alert alert)
        {
            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql =   @$"UPDATE alert
                        set name = '{alert.name}', notifywhenarriving = '{alert.notifywhenarriving}', 
                        notifywhenleaving = '{alert.notifywhenleaving}',
                        enterpriseid = {alert.enterpriseid}, active = '{alert.active}'
                        WHERE id = {id}";
            var command = new SqlCommand(sql, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return this.Ok($"Los datos no se actualizaron: {e.Message}");
                throw;
            }
            connection.Close();
            return this.Ok("Los datos se actualizaron correctamente");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public OkObjectResult Delete(int id)
        {
            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();
            string mensage;
            var sql =@$"DELETE 
                        FROM alert 
                        WHERE id = {id}";
            var command = new SqlCommand(sql, connection);
            try
            {
                mensage = (command.ExecuteNonQuery() <= 0 ? "No se vio afectado ningun registro" : "Los datos se eliminaron correctamente");
            }
            catch (Exception e)
            {
                mensage = $"Los datos no se eliminaron: {e.Message}";
                throw;
            }
            connection.Close();
            return Ok(mensage);
            
        }
    }
}
