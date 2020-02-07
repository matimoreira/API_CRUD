using System;

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
    }
}
