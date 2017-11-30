using System;
using System.Text;

namespace BreederStationBussinessLayer.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Trademark { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
