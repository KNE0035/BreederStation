using BreederStationBussinessLayer.Domain.Enums;

namespace BreederStationBussinessLayer.SelectCriteria
{
    public class PersonCriteria
    {
        public RoleEnum? Role { get; set; }
        public string WholeName { get; set; }
    }
}
