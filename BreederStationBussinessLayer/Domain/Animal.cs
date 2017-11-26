using BreederStationBussinessLayer.Domain.Enums;
using System;
using System.Text;

namespace BreederStationBussinessLayer.Domain
{
    public class Animal
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Race { get; set; }
        public SexEnum Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public AnimalGroup AnimalGroup { get; set; }
        public Cage Cage { get; set; }
        public Food Food { get; set; }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("id zvirete: " + Id + Environment.NewLine);
            ret.Append("jmeno: " + Name + Environment.NewLine);
            ret.Append("rasa: " + Race + Environment.NewLine);
            ret.Append("pohlavi: " + Sex.ToString() + Environment.NewLine);
            ret.Append("datum narozeni: " + BirthDate + Environment.NewLine);
            ret.Append("aktivni:  " + Active + Environment.NewLine);
            if (LastActiveDate != null)
            {
                ret.Append("Posledni aktivita:  " + LastActiveDate + Environment.NewLine);
            }

            if (AnimalGroup != null)
            {
                ret.Append(AnimalGroup.ToString());
            }

            if (Cage != null)
            {
                ret.Append(Cage.ToString());
            }

            if (Food != null)
            {
                ret.Append(Food.ToString());
            }
            return ret.ToString();
        }

        public int GetAnimalAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-age)) age--;
            return age;
        }
    }
}
