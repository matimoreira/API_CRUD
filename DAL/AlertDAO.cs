using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace API_CRUD
{
    public class AlertDAO
    {
        ~AlertDAO()
        {
            Connection.disconnect();
        }
        private List<Alert> getAlert(SqlDataReader reader)
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
        public List<Alert> getAllAlert()
        {
            var sql = @$"SELECT * 
                           FROM alert as a
                           INNER JOIN enterprise as e ON(e.id = a.enterpriseid)";
            var reader = Connection.readInfo(sql);
            return getAlert(reader); 
        }
        public List<Alert> getTopAlert(int top)
        {
            var sql = @$"SELECT top {top} * 
                           FROM alert as a
                           INNER JOIN enterprise as e ON(e.id = a.enterpriseid)";
            var reader = Connection.readInfo(sql);
            return getAlert(reader);
        }
        private String GetQueryResponse(int commandResult, string operationName)
        {
            string message;
            try
            {
                message = (commandResult == 0 ? "No se vio afectado ningun registro" : $"Operacion de {operationName} se ha efectuado exitosamente");
            }
            catch (Exception e)
            {
                message = $"Operacion de {operationName} fallo: {e.Message}";
                throw;
            }
            return message;
        }
        public string removeAlert(int id)
        {
            var sql = @$"DELETE 
                        FROM alert 
                        WHERE id = {id}";

            string response = GetQueryResponse(Connection.deleteInfo(sql), "Eliminar");
            return response;
        }

    }
}
