using BreederStationBussinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreederStationAspView.Models
{
    public class Cleaner
    {
        public bool ChemicalQualification { get; set; }
        public IList<Cage> Cages { get; set; }
    }
}
