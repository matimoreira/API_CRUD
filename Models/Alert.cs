using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace API_CRUD
{
    public class Alert
    {
        public Alert()
        {
            this.Name = "";
            this.Notifywhenarriving = "";
            this.Notifywhenleaving = "";
            this.Active = "";
            this.Enterprise = new Enterprise();
        }
        public Alert(int p_id, String p_name, String p_notifywhenarriving, String p_notifywhenleaving, int p_enterpriseid, String p_active)
        {
            this.Id = p_id;
            this.Name = p_name;
            this.Notifywhenarriving = p_notifywhenarriving;
            this.Notifywhenleaving = p_notifywhenleaving;
            this.enterpriseid = p_enterpriseid;
            this.Active = p_active;
            this.Enterprise = new Enterprise();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Notifywhenarriving{ get; set; }
        public String Notifywhenleaving { get; set; }
        public int enterpriseid { get; set; }
        public Enterprise Enterprise{ get; set; }
        public String Active { get; set; }

       
        public List<Alert> getAllAlert(string sql)
        {
            
            var reader = Connection.readInfo(sql);
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
    }
}
