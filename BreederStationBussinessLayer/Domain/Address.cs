using System;
using System.Text;

namespace BreederStationBussinessLayer.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("id adresy: " + Id + Environment.NewLine);
            ret.Append("mesto: " + City + Environment.NewLine);
            ret.Append("ulice: " + Street + Environment.NewLine);
            ret.Append("zipcode: " + Zipcode + Environment.NewLine);

            return ret.ToString();
        }
    }
}
