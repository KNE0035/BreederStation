using System;
using System.Collections.Generic;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Cleaner : Person
    {
        public Cleaner() : base()
        {

        }
        public Cleaner(Person person) : base(person)
        {

        }

        public bool ChemicalQualification { get; set; }

        public IList<Cage> Cages { get; set; }
    }
}
