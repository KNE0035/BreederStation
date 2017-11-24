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

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("----------------------------" + Environment.NewLine);
            ret.Append("login: " + Login + Environment.NewLine);
            ret.Append("jmeno: " + FirstName + Environment.NewLine);
            ret.Append("prijmeni: " + LastName + Environment.NewLine);
            ret.Append("telefon: " + Phone + Environment.NewLine);
            ret.Append("datum narozeni: " + BirthDate + Environment.NewLine);
            ret.Append("aktivni:  " + Active + Environment.NewLine);
            if(LastActiveDate != null)
            {
                ret.Append("Posledni aktivita:  " + LastActiveDate + Environment.NewLine);
            }

            if (Role != null)
            {
                ret.Append(Role.ToString());
            }
            ret.Append("----------------------------" + Environment.NewLine);
            return ret.ToString();
        }

        public string RoleType
        {
            get { return Role.Type.ToString(); }
        }
    }
}
