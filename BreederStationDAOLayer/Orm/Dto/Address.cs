using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
    }
}
