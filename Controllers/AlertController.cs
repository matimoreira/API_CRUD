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
        public IEnumerable<AlertDTO> Get(int top)
        {
            var dao = new AlertDAO();
            return dao.getTopAlert(top);
        }

        //GET: api/Alert
        [HttpGet()]
        public IEnumerable<AlertDTO> Get()
        {
            var dao = new AlertDAO();            
            return dao.getAllAlert();
        }


        // POST: api/Alert
        [HttpPost]
        public ActionResult Post([FromBody] AlertDTO alert)
        {
            var dao = new AlertDAO();
            int resultNonQuery;
            try
            {
                resultNonQuery = dao.addAlert(alert);
            }
            catch (Exception e)
            {
                return this.BadRequest($"Operacion de Modificar fallo: {e.Message}");
                throw;
            }
            return this.GetQueryResponse(resultNonQuery, "Agregar");
        }


        // PUT: api/Alert/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AlertDTO alert)
        {
            var dao = new AlertDAO();
            int resultNonQuery;
            try
            {
                resultNonQuery = dao.editAlert(id, alert);
            }
            catch (Exception e)
            {
                return this.BadRequest($"Operacion de Modificar fallo: {e.Message}");
                throw;
            }
            return this.GetQueryResponse(resultNonQuery, "Modificar");
            /*
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
            */
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var dao = new AlertDAO();
            int resultNonQuery;
            try
            {
                resultNonQuery = dao.removeAlert(id);
            }
            catch (Exception e)
            {
                return this.BadRequest($"Operacion de eliminar fallo: {e.Message}");
                throw;
            }
            return this.GetQueryResponse(resultNonQuery, "Eliminar");
        }


        //Methods
        private ActionResult GetQueryResponse(int resultNonQuery, string operationName)
        {
            string message;
            message = (resultNonQuery == 0 ? "No se vio afectado ningun registro" : $"Operacion de {operationName} se ha efectuado exitosamente");
            return this.Ok(message);
        }

    }
}
