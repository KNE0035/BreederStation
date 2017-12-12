using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class AnimalGroup
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string BreedersInfo { get; set; }
        public string AnimalsInfo { get; set; }
    }
}
