using System;
using System.Text;

namespace BreederStationDataLayer.Orm.Dto
{
    public class AnimalGroup
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string BreedersInfo { get; set; }
        public string AnimalsInfo { get; set; }
        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("id skupiny:  " + Id + Environment.NewLine);
            ret.Append("popis skupiny:  " + Description + Environment.NewLine);

            if (BreedersInfo != null)
            {
                if (BreedersInfo != " ")
                {
                    ret.Append("Chovatele ve skupine: " + BreedersInfo + Environment.NewLine);
                }
                else
                {
                    ret.Append("skupina nema zadne chovatele" + BreedersInfo + Environment.NewLine);
                }
            }

            if (AnimalsInfo != null)
            {
                if (AnimalsInfo != " ")
                {
                    ret.Append("Zvirata ve skupine:" + AnimalsInfo + Environment.NewLine);
                }
                else
                {
                    ret.Append("skupina nema zadne zvirata" + AnimalsInfo + Environment.NewLine);
                }
                    
            }
            return ret.ToString();
        }
    }
}
