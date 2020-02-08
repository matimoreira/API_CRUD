using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace API_CRUD
{
    public class AlertDAO : DAOBase
    {
        private List<AlertDTO> getAlert(SqlDataReader reader)
        {
            var alerts = new List<AlertDTO>();
            while (reader.Read())
            {
                var alert = new AlertDTO
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Notifywhenarriving = reader.GetString(2),
                    Notifywhenleaving =  reader.GetString(3),
                    Active = reader.GetString(5)
                };
                    //Id = reader[reader.GetOrdinal("a.id")] as int? ?? default(int)
                var enterprise = new EnterpriseDTO(
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
        public List<AlertDTO> getAllAlert()
        {
            var sql = @$"SELECT * 
                           FROM alert as a
                           INNER JOIN enterprise as e ON(e.id = a.enterpriseid)";
            var reader = this.GetReader(sql);
            return getAlert(reader); 
        }
        public List<AlertDTO> getTopAlert(int top)
        {
            var sql = @$"SELECT top {top} * 
                           FROM alert as a
                           INNER JOIN enterprise as e ON(e.id = a.enterpriseid)";
            var reader = this.GetReader(sql);
            return getAlert(reader);
        }      
        public int removeAlert(int id)
        {
            var sql = @$"DELETE 
                        FROM alert 
                        WHERE id = {id}";
            return this.GetNonQuery(sql);
        }
        public int addAlert(AlertDTO alert)
        {
            var sql = $"INSERT INTO alert(id, name, notifywhenarriving, notifywhenleaving, enterpriseid, active) values ({alert.Id}, '{alert.Name}', '{alert.Notifywhenarriving}', '{alert.Notifywhenleaving}', '{alert.Enterprise.Id}', '{alert.Active}')";
            return this.GetNonQuery(sql);
        }
        internal int editAlert(int id, AlertDTO alert)
        {
            var sql = @$"UPDATE alert
                        set name = '{alert.Name}', notifywhenarriving = '{alert.Notifywhenarriving}', 
                        notifywhenleaving = '{alert.Notifywhenleaving}',
                        enterpriseid = {alert.Enterprise.Id}, active = '{alert.Active}'
                        WHERE id = {id}";
            return this.GetNonQuery(sql);
        }
    }
}
