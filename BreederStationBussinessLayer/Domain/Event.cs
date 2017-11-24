using System;
using System.Collections.Generic;
using System.Text;

namespace BreederStationBussinessLayer.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Breeder Breeder { get; set; }
        public IList<int> AnimalIds { get; set; }

        public int BreederId
        {
            get { return Breeder.Id; }
        }

        public string AnimalIdsString {
            get {
                return string.Join<int>(", ", AnimalIds);
            }
        }

            
        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("id udalosti: " + Id + Environment.NewLine);
            ret.Append("popis udalosti: " + Description + Environment.NewLine);
            ret.Append("Datum zacatku udalosti: " + StartDate + Environment.NewLine);
            if (EndDate != null)
            {
                ret.Append("Datum konce udalosti:  " + EndDate + Environment.NewLine);
            } else
            {
                ret.Append("Datum konce udalosti:  Zatim nebylo stanoveno" + Environment.NewLine);
            }
            ret.Append("Id chovatele ridici udalost: " + Breeder.Id + Environment.NewLine);

            ret.Append("Id zucastnenych zvirat: ");
            foreach(int id in AnimalIds)
            {
                ret.Append(id + ", ");
            }
            ret.Remove(ret.Length - 2, 2);
            ret.Append(Environment.NewLine);
            return ret.ToString();
        }
    }
}
