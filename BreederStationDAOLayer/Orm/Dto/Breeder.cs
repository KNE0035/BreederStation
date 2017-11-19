using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Breeder : Person
    {
        public Breeder() : base()
        {

        }
        public Breeder(Person person) : base(person)
        {

        }
        public AnimalGroup AnimalGroup { get; set; }

    }
}
