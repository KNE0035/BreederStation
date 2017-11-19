using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class Company
    {
        public int Id { get; set; }
        public string Trademark { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("Znacka: " + Trademark + Environment.NewLine);

            if (Phone != null) {
                ret.Append("telefon firmy: " + Phone + Environment.NewLine);
            }

            if(Email != null)
            {
                ret.Append("Email firmy: " + Email + Environment.NewLine);
            }

            if (Address != null)
            {
                ret.Append(Address.ToString());
            }
            return ret.ToString();
        }
    }
}
