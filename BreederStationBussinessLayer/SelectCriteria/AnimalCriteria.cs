using BreederStationBussinessLayer.Domain.Enums;

namespace BreederStationBussinessLayer.SelectCriteria
{
    public class AnimalCriteria
    {
        public int? AnimalGroupId { get; set; }
        public int? CageId { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public SexEnum? Sex { get; set; }
        public int? Age { get; set; }
    }
}
