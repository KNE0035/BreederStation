using BreederStationBussinessLayer.Domain;
using System;


namespace BreederStationAspView.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public Role Role { get; set; }
        public Breeder Breeder {get; set;}
        public Cleaner Cleaner {get; set;}
    }
}
