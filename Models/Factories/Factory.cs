using System.Collections.Generic;

namespace OilTeamProject.Models.Factories
{
    public class Factory
    {

        public int FactoryID { get; set; }
        public string FactoryName { get; set; }
        public string FactoryAdress { get; set; }
        public string FactoryPhoneNumber { get; set; }
        public string Owner { get; set; }

        //public virtual OilPress OilPress { get; set; }
        //public virtual Bottling Bottling { get; set; }

        public virtual ICollection<OilPress> OilPresses { get; set; }
        //public virtual ICollection<Bottling> Bottlings { get; set; }
        //public virtual ICollection<Product> Products { get; set; }

    }
}