using System;

namespace API_CRUD
{
    public class Alert
    {
        public Alert()
        {
            this.name = "";
            this.notifywhenarriving = "";
            this.notifywhenleaving = "";
            this.active = "";
        }
        public Alert(int p_id, String p_name, String p_notifywhenarriving, String p_notifywhenleaving, int p_enterpriseid, String p_active)
        {
            this.id = p_id;
            this.name = p_name;
            this.notifywhenarriving = p_notifywhenarriving;
            this.notifywhenleaving = p_notifywhenleaving;
            this.enterpriseid = p_enterpriseid;
            this.active = p_active;
        }
        public int id { get; set; }
        public String name { get; set; }
        public String notifywhenarriving{ get; set; }
        public String notifywhenleaving { get; set; }
        public int enterpriseid { get; set; }
        public String active { get; set; }
    }
}
