using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace API_CRUD
{
    public class AlertDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Notifywhenarriving{ get; set; }
        public String Notifywhenleaving { get; set; }
        //public int enterpriseid { get; set; }
        public EnterpriseDTO Enterprise{ get; set; }
        public String Active { get; set; }
    }
}
