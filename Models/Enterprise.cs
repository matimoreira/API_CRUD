using System;

namespace API_CRUD
{
    public class Enterprise
    {

        public Enterprise()
        {
            this.Id = 0;
            this.Name = "";
            this.Address = 0;
            this.Active = "";
            this.Reseller = 0;
        }
        public Enterprise(int p_id,string p_name,int p_adress,string p_active,int p_reseller)
        {
            this.Id = p_id;
            this.Name = p_name;
            this.Address = p_adress;
            this.Active = p_active;
            this.Reseller = p_reseller;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int  Address{ get; set; }
        public string Active{ get; set; }
        public int Reseller{ get; set; }
    }
}
