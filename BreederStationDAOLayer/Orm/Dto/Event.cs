using System;
using System.Collections.Generic;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Event
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Breeder Breeder { get; set; }
        public IList<Animal> animals { get; set; }
    }
}
