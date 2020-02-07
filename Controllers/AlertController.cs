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

            var sql =   @$"SELECT top {top} * 
                           FROM alert as a
                           INNER JOIN enterprise as e ON(e.id = a.enterpriseid)";
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            result = this.AddAlert(reader);

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

            var sql = @$"SELECT * 
                           FROM alert as a
                           INNER JOIN enterprise as e ON(e.id = a.enterpriseid)";
            
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            result = this.AddAlert(reader);

            reader.Close();
            connection.Close();

            return result;
        }


        // POST: api/Alert
        [HttpPost]
        public OkObjectResult Post([FromBody] Alert alert)
        {
            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = $"INSERT INTO alert(id, name, notifywhenarriving, notifywhenleaving, enterpriseid, active) values ({alert.Id}, '{alert.Name}', '{alert.Notifywhenarriving}', '{alert.Notifywhenleaving}', '{alert.enterpriseid}', '{alert.Active}')";
            
            var command = new SqlCommand(sql, connection);
            string response = GetQueryResponse(command, "Agregar");
            
            connection.Close();
            return Ok(response);

        } 


        // PUT: api/Alert/5
        [HttpPut("{id}")]
        public OkObjectResult Put(int id, [FromBody] Alert alert)
        {
            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql =   @$"UPDATE alert
                        set name = '{alert.Name}', notifywhenarriving = '{alert.Notifywhenarriving}', 
                        notifywhenleaving = '{alert.Notifywhenleaving}',
                        enterpriseid = {alert.enterpriseid}, active = '{alert.Active}'
                        WHERE id = {id}";

            var command = new SqlCommand(sql, connection);            
            string response = GetQueryResponse(command, "Modificar");

            connection.Close();
            return Ok(response);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public OkObjectResult Delete(int id)
        {
            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql =@$"DELETE 
                        FROM alert 
                        WHERE id = {id}";

            var command = new SqlCommand(sql, connection);
            string response = GetQueryResponse(command, "Eliminar");

            connection.Close();
            return Ok(response);
            
        }


        //Methods
        private List<Alert> AddAlert(SqlDataReader reader)
        {
            var alerts = new List<Alert>();
            while (reader.Read())
            {
                var alert = new Alert(
                    //Id = reader[reader.GetOrdinal("a.id")] as int? ?? default(int)
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt32(4),
                    reader.GetString(5)
                );
                var enterprise = new Enterprise(
                    reader.GetInt32(6),
                    reader[7] as string,
                    reader[8] as int? ?? default(int),
                    reader[9] as string,
                    reader[10] as int? ?? default(int)
                    );
                alert.Enterprise = enterprise;
                alerts.Add(alert);
            }
            return alerts;
        }

        private String GetQueryResponse(SqlCommand command, string operationName)
        {
            string message;
            try
            {
                message = (command.ExecuteNonQuery() == 0 ? "No se vio afectado ningun registro" : $"Operacion de {operationName} se ha efectuado exitosamente");
            }
            catch (Exception e)
            {
                message = $"Operacion de {operationName} fallo: {e.Message}";
                throw;
            }
            return message;
        }

    }
}
