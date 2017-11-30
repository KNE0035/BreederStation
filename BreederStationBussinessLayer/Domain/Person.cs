using System;
using System.Text;


namespace BreederStationBussinessLayer.Domain
{
    public class Person
    {
        public Person(){}
        public Person(Person p)
        {
            this.Id = p.Id;
            this.Password = p.Password;
            this.Login = p.Login;
            this.FirstName = p.FirstName;
            this.LastName = p.LastName;
            this.Phone = p.Phone;
            this.BirthDate = p.BirthDate;
            this.Active = p.Active;
            this.LastActiveDate = p.LastActiveDate;
            this.Role = p.Role;
        }
        public int Id { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active{ get; set; }
        public DateTime? LastActiveDate { get; set; }
        public Role Role { get; set; }

        public string RoleType
        {
            get { return Role.Type.ToString(); }
        }

        public int GetPersonAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-age)) age--;
            return age;
        }
    }
}
