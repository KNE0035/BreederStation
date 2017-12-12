using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Animal
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Race { get; set; }
        public SexEnum Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public AnimalGroup AnimalGroup { get; set; }
        public Cage Cage { get; set; }
        public Food Food { get; set; }
    }
}
